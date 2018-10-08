package com.zopim.android.sdk.data;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.os.Looper;
import android.util.Log;
import android.util.Pair;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.zopim.android.sdk.C0784R;
import com.zopim.android.sdk.api.ChatSession;
import com.zopim.android.sdk.api.FileTransfers;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.ChatLog.Option;
import com.zopim.android.sdk.model.ChatLog.Type;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.Map;
import java.util.Map.Entry;

public class LivechatChatLogPath extends Path<LinkedHashMap<String, ChatLog>> {
    private static final LivechatChatLogPath INSTANCE = new LivechatChatLogPath();
    private static final String LOG_TAG = LivechatChatLogPath.class.getSimpleName();
    Pair<String, ChatLog> mChatRatingEntry;
    private final Object mLock;
    private C0861b mTimeoutManager;
    private LinkedList<Pair<String, ChatLog>> mUnmatchedAgentQuestionnaire;
    Map<String, String> mUploadedFiles;

    public static class ChatTimeoutReceiver extends BroadcastReceiver {
        public void onReceive(Context context, Intent intent) {
            if (intent == null || !ChatSession.ACTION_CHAT_SESSION_TIMEOUT.equals(intent.getAction())) {
                Log.w(LivechatChatLogPath.LOG_TAG, "onReceive: intent was null or getAction() was mismatched");
                return;
            }
            ChatLog chatLog = new ChatLog(null, Type.CHAT_MSG_TRIGGER, context.getResources().getString(C0784R.string.chat_session_timeout_message));
            ((LinkedHashMap) LivechatChatLogPath.INSTANCE.mData).put(chatLog.getTimestamp().toString(), chatLog);
            LivechatChatLogPath.INSTANCE.broadcast(LivechatChatLogPath.INSTANCE.getData());
        }
    }

    /* renamed from: com.zopim.android.sdk.data.LivechatChatLogPath$a */
    class C0860a implements Runnable {
        /* renamed from: a */
        String f843a;
        /* renamed from: b */
        ChatLog f844b;
        /* renamed from: c */
        final /* synthetic */ LivechatChatLogPath f845c;

        C0860a(LivechatChatLogPath livechatChatLogPath, String str, ChatLog chatLog) {
            this.f845c = livechatChatLogPath;
            this.f844b = chatLog;
            this.f843a = str;
        }

        public void run() {
            Log.v(LivechatChatLogPath.LOG_TAG, "Message failed to send. Timeout occurred");
            this.f844b.setFailed(true);
            LinkedHashMap linkedHashMap = new LinkedHashMap(1);
            linkedHashMap.put(this.f843a, this.f844b);
            this.f845c.updateInternal(linkedHashMap);
        }
    }

    /* renamed from: com.zopim.android.sdk.data.LivechatChatLogPath$b */
    class C0861b {
        /* renamed from: a */
        final /* synthetic */ LivechatChatLogPath f846a;
        /* renamed from: b */
        private Handler f847b = new Handler(Looper.myLooper());
        /* renamed from: c */
        private Map<String, C0860a> f848c = new HashMap();

        C0861b(LivechatChatLogPath livechatChatLogPath) {
            this.f846a = livechatChatLogPath;
        }

        /* renamed from: a */
        synchronized void m696a(String str) {
            Logger.m564v(LivechatChatLogPath.LOG_TAG, "Removing timeout runnable");
            this.f847b.removeCallbacks((Runnable) this.f848c.get(str));
        }

        /* renamed from: a */
        synchronized void m697a(String str, ChatLog chatLog) {
            if (str == null) {
                Log.w(LivechatChatLogPath.LOG_TAG, "Can not add chat log without an id");
            } else if (chatLog == null) {
                Log.w(LivechatChatLogPath.LOG_TAG, "Can not add chat log that is null");
            } else {
                C0860a c0860a = (C0860a) this.f848c.get(str);
                if (c0860a != null) {
                    Logger.m564v(LivechatChatLogPath.LOG_TAG, "Removing previous timeout");
                    this.f847b.removeCallbacks(c0860a);
                }
                Runnable c0860a2 = new C0860a(this.f846a, str, chatLog);
                this.f848c.put(str, c0860a2);
                Logger.m564v(LivechatChatLogPath.LOG_TAG, "Scheduling timeout runnable");
                this.f847b.postDelayed(c0860a2, 5000);
            }
        }
    }

    private LivechatChatLogPath() {
        this.mLock = new Object();
        this.mTimeoutManager = new C0861b(this);
        this.mUploadedFiles = new HashMap();
        this.mUnmatchedAgentQuestionnaire = new LinkedList();
        this.mData = new LinkedHashMap();
    }

    private Pair<String, ChatLog> findAgentQuestionnaire(ChatLog chatLog) {
        if (chatLog == null) {
            Log.w(LOG_TAG, "RowItem must not be null");
            return null;
        } else if (chatLog.getType() != Type.CHAT_MSG_VISITOR) {
            return null;
        } else {
            if (chatLog.getMessage() == null || chatLog.getMessage().isEmpty()) {
                return null;
            }
            Iterator it = this.mUnmatchedAgentQuestionnaire.iterator();
            while (it.hasNext()) {
                Pair<String, ChatLog> pair = (Pair) it.next();
                ChatLog chatLog2 = (ChatLog) pair.second;
                if (chatLog2.getType() == Type.CHAT_MSG_AGENT) {
                    Option[] options = chatLog2.getOptions();
                    int length = options.length;
                    int i = 0;
                    while (i < length) {
                        boolean equals = chatLog.getMessage().equals(options[i].getLabel());
                        Object obj = chatLog.getTimestamp().longValue() > chatLog2.getTimestamp().longValue() ? 1 : null;
                        if (!equals || obj == null) {
                            i++;
                        } else {
                            this.mUnmatchedAgentQuestionnaire.remove(pair);
                            return pair;
                        }
                    }
                    continue;
                }
            }
            return null;
        }
    }

    public static synchronized LivechatChatLogPath getInstance() {
        LivechatChatLogPath livechatChatLogPath;
        synchronized (LivechatChatLogPath.class) {
            livechatChatLogPath = INSTANCE;
        }
        return livechatChatLogPath;
    }

    private ChatLog mergeEntries(ChatLog chatLog, ChatLog chatLog2) {
        if (chatLog == null) {
            return null;
        }
        if (chatLog2 == null) {
            return chatLog;
        }
        ObjectMapper mapper = this.PARSER.getMapper();
        try {
            return (ChatLog) mapper.readerForUpdating(chatLog).readValue(mapper.valueToTree(chatLog2));
        } catch (Throwable e) {
            Log.w(LOG_TAG, "Failed to process json. Chat log record could not be updated.", e);
            return null;
        } catch (Throwable e2) {
            Log.w(LOG_TAG, "IO error. Chat log record could not be updated.", e2);
            return null;
        }
    }

    private void updateInternal(LinkedHashMap<String, ChatLog> linkedHashMap) {
        Throwable th;
        if (linkedHashMap == null) {
            Log.i(LOG_TAG, "Passed parameter must not be null. Aborting update.");
            return;
        }
        synchronized (this.mLock) {
            int i = 0;
            for (Entry entry : linkedHashMap.entrySet()) {
                String str = (String) entry.getKey();
                ChatLog chatLog = (ChatLog) entry.getValue();
                String str2 = (chatLog == null || chatLog.getType() != Type.CHAT_RATING || this.mChatRatingEntry == null) ? str : (String) this.mChatRatingEntry.first;
                ChatLog chatLog2;
                Object obj;
                if (((LinkedHashMap) this.mData).containsKey(str2)) {
                    chatLog2 = (ChatLog) ((LinkedHashMap) this.mData).get(str2);
                    if (chatLog != null) {
                        ChatLog mergeEntries = mergeEntries(chatLog2, chatLog);
                        if (mergeEntries == null) {
                            ((LinkedHashMap) this.mData).remove(str2);
                        } else {
                            ((LinkedHashMap) this.mData).put(str2, mergeEntries);
                            obj = Type.CHAT_MSG_VISITOR == mergeEntries.getType() ? 1 : null;
                            boolean booleanValue = mergeEntries.isFailed() == null ? false : mergeEntries.isFailed().booleanValue();
                            if (!(obj == null || booleanValue)) {
                                if (mergeEntries.isUnverified() == null ? true : mergeEntries.isUnverified().booleanValue()) {
                                    this.mTimeoutManager.m697a(str2, chatLog);
                                } else {
                                    this.mTimeoutManager.m696a(str2);
                                }
                            }
                        }
                    } else if (((ChatLog) ((LinkedHashMap) this.mData).get(str2)).getType() != Type.ATTACHMENT_UPLOAD) {
                        ((LinkedHashMap) this.mData).remove(str2);
                        i--;
                        this.mTimeoutManager.m696a(str2);
                    }
                } else {
                    int i2;
                    if (chatLog != null) {
                        try {
                            obj = Type.CHAT_MSG_VISITOR == chatLog.getType() ? 1 : null;
                            Object obj2 = (chatLog.getMessage() == null || !chatLog.getMessage().trim().isEmpty()) ? null : 1;
                            if (obj == null || obj2 == null) {
                                Pair findAgentQuestionnaire = findAgentQuestionnaire(chatLog);
                                if (findAgentQuestionnaire != null) {
                                    Option[] options = ((ChatLog) findAgentQuestionnaire.second).getOptions();
                                    for (i2 = 0; i2 < options.length; i2++) {
                                        if (chatLog.getMessage().equals(options[i2].getLabel())) {
                                            options[i2].select();
                                            ((LinkedHashMap) this.mData).put(findAgentQuestionnaire.first, findAgentQuestionnaire.second);
                                            break;
                                        }
                                    }
                                } else {
                                    if (chatLog != null) {
                                        if (chatLog.getType() == Type.ATTACHMENT_UPLOAD) {
                                            str = chatLog.getFileName();
                                            if (str != null) {
                                                chatLog.setFile(FileTransfers.INSTANCE.findFile(str));
                                                this.mUploadedFiles.put(str, str2);
                                            }
                                        }
                                    }
                                    if (chatLog.getType() == Type.CHAT_MSG_VISITOR) {
                                        obj2 = chatLog.getAttachment() != null ? chatLog.getAttachment().getName() : null;
                                        if (obj2 != null) {
                                            chatLog2 = (ChatLog) ((LinkedHashMap) this.mData).get((String) this.mUploadedFiles.get(obj2));
                                            if (chatLog2 != null) {
                                                chatLog2.setProgress(100);
                                            }
                                        }
                                    }
                                    if (chatLog.getType() == Type.CHAT_RATING) {
                                        this.mChatRatingEntry = new Pair(str2, chatLog);
                                    }
                                    ((LinkedHashMap) this.mData).put(str2, chatLog);
                                    i2 = i + 1;
                                    if (obj != null) {
                                        try {
                                            if (chatLog.isUnverified() != null ? chatLog.isUnverified().booleanValue() : false) {
                                                this.mTimeoutManager.m697a(str2, chatLog);
                                            }
                                        } catch (Throwable e) {
                                            i = i2;
                                            th = e;
                                            Log.w(LOG_TAG, "Failed to process json. Chat log record could not be created.", th);
                                            this.mUnmatchedAgentQuestionnaire.addFirst(new Pair(str2, chatLog));
                                        }
                                    }
                                }
                            }
                        } catch (Exception e2) {
                            th = e2;
                            Log.w(LOG_TAG, "Failed to process json. Chat log record could not be created.", th);
                            this.mUnmatchedAgentQuestionnaire.addFirst(new Pair(str2, chatLog));
                        }
                    } else {
                        i2 = i;
                    }
                    i = i2;
                }
                if (chatLog != null && chatLog.getOptions() != null && chatLog.getOptions().length > 0) {
                    this.mUnmatchedAgentQuestionnaire.addFirst(new Pair(str2, chatLog));
                }
            }
            if (i >= 0) {
                broadcast(getData());
            }
        }
    }

    void clear() {
        if (this.mData != null) {
            ((LinkedHashMap) this.mData).clear();
        }
    }

    public int countMessages(Type... typeArr) {
        int i = 0;
        for (ChatLog type : getData().values()) {
            Type type2 = type.getType();
            int i2 = i;
            for (Type equals : typeArr) {
                if (equals.equals(type2)) {
                    i2++;
                }
            }
            i = i2;
        }
        return i;
    }

    public LinkedHashMap<String, ChatLog> getData() {
        return this.mData != null ? new LinkedHashMap((Map) this.mData) : new LinkedHashMap();
    }

    void update(String str) {
        if (isClearRequired(str)) {
            clear();
        } else if (!str.isEmpty()) {
            updateInternal((LinkedHashMap) this.PARSER.parse(str, new C0865d(this)));
        }
    }
}
