package com.zopim.android.sdk.data;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.os.Looper;
import android.util.Log;
import android.util.Pair;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.api.ChatSession;
import com.zopim.android.sdk.api.FileTransfers;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.ChatLog.Option;
import com.zopim.android.sdk.model.ChatLog.Type;
import java.io.IOException;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.Map;
import java.util.Map.Entry;

public class LivechatChatLogPath extends Path<LinkedHashMap<String, ChatLog>> {
    /* access modifiers changed from: private */
    public static final LivechatChatLogPath INSTANCE = new LivechatChatLogPath();
    /* access modifiers changed from: private */
    public static final String LOG_TAG = LivechatChatLogPath.class.getSimpleName();
    Pair<String, ChatLog> mChatRatingEntry;
    private final Object mLock;
    private C1231b mTimeoutManager;
    private LinkedList<Pair<String, ChatLog>> mUnmatchedAgentQuestionnaire;
    Map<String, String> mUploadedFiles;

    public static class ChatTimeoutReceiver extends BroadcastReceiver {
        public void onReceive(Context context, Intent intent) {
            if (intent == null || !ChatSession.ACTION_CHAT_SESSION_TIMEOUT.equals(intent.getAction())) {
                Log.w(LivechatChatLogPath.LOG_TAG, "onReceive: intent was null or getAction() was mismatched");
                return;
            }
            ChatLog chatLog = new ChatLog(null, Type.CHAT_MSG_TRIGGER, context.getResources().getString(C1122R.string.chat_session_timeout_message));
            ((LinkedHashMap) LivechatChatLogPath.INSTANCE.mData).put(chatLog.getTimestamp().toString(), chatLog);
            LivechatChatLogPath.INSTANCE.broadcast(LivechatChatLogPath.INSTANCE.getData());
        }
    }

    /* renamed from: com.zopim.android.sdk.data.LivechatChatLogPath$a */
    class C1230a implements Runnable {

        /* renamed from: a */
        String f887a;

        /* renamed from: b */
        ChatLog f888b;

        C1230a(String str, ChatLog chatLog) {
            this.f888b = chatLog;
            this.f887a = str;
        }

        public void run() {
            Log.v(LivechatChatLogPath.LOG_TAG, "Message failed to send. Timeout occurred");
            this.f888b.setFailed(true);
            LinkedHashMap linkedHashMap = new LinkedHashMap(1);
            linkedHashMap.put(this.f887a, this.f888b);
            LivechatChatLogPath.this.updateInternal(linkedHashMap);
        }
    }

    /* renamed from: com.zopim.android.sdk.data.LivechatChatLogPath$b */
    class C1231b {

        /* renamed from: b */
        private Handler f891b = new Handler(Looper.myLooper());

        /* renamed from: c */
        private Map<String, C1230a> f892c = new HashMap();

        C1231b() {
        }

        /* access modifiers changed from: 0000 */
        /* renamed from: a */
        public synchronized void mo20805a(String str) {
            Logger.m577v(LivechatChatLogPath.LOG_TAG, "Removing timeout runnable");
            this.f891b.removeCallbacks((Runnable) this.f892c.get(str));
        }

        /* access modifiers changed from: 0000 */
        /* renamed from: a */
        public synchronized void mo20806a(String str, ChatLog chatLog) {
            if (str == null) {
                Log.w(LivechatChatLogPath.LOG_TAG, "Can not add chat log without an id");
            } else if (chatLog == null) {
                Log.w(LivechatChatLogPath.LOG_TAG, "Can not add chat log that is null");
            } else {
                C1230a aVar = (C1230a) this.f892c.get(str);
                if (aVar != null) {
                    Logger.m577v(LivechatChatLogPath.LOG_TAG, "Removing previous timeout");
                    this.f891b.removeCallbacks(aVar);
                }
                C1230a aVar2 = new C1230a(str, chatLog);
                this.f892c.put(str, aVar2);
                Logger.m577v(LivechatChatLogPath.LOG_TAG, "Scheduling timeout runnable");
                this.f891b.postDelayed(aVar2, 5000);
            }
        }
    }

    private LivechatChatLogPath() {
        this.mLock = new Object();
        this.mTimeoutManager = new C1231b();
        this.mUploadedFiles = new HashMap();
        this.mUnmatchedAgentQuestionnaire = new LinkedList<>();
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
                        boolean z = chatLog.getTimestamp().longValue() > chatLog2.getTimestamp().longValue();
                        if (!equals || !z) {
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
        } catch (JsonProcessingException e) {
            Log.w(LOG_TAG, "Failed to process json. Chat log record could not be updated.", e);
            return null;
        } catch (IOException e2) {
            Log.w(LOG_TAG, "IO error. Chat log record could not be updated.", e2);
            return null;
        }
    }

    /* access modifiers changed from: private */
    public void updateInternal(LinkedHashMap<String, ChatLog> linkedHashMap) {
        int i;
        Exception e;
        if (linkedHashMap == null) {
            Log.i(LOG_TAG, "Passed parameter must not be null. Aborting update.");
            return;
        }
        synchronized (this.mLock) {
            int i2 = 0;
            for (Entry entry : linkedHashMap.entrySet()) {
                String str = (String) entry.getKey();
                ChatLog chatLog = (ChatLog) entry.getValue();
                String str2 = (chatLog == null || chatLog.getType() != Type.CHAT_RATING || this.mChatRatingEntry == null) ? str : (String) this.mChatRatingEntry.first;
                if (((LinkedHashMap) this.mData).containsKey(str2)) {
                    ChatLog chatLog2 = (ChatLog) ((LinkedHashMap) this.mData).get(str2);
                    if (chatLog != null) {
                        ChatLog mergeEntries = mergeEntries(chatLog2, chatLog);
                        if (mergeEntries == null) {
                            ((LinkedHashMap) this.mData).remove(str2);
                        } else {
                            ((LinkedHashMap) this.mData).put(str2, mergeEntries);
                            boolean z = Type.CHAT_MSG_VISITOR == mergeEntries.getType();
                            boolean booleanValue = mergeEntries.isFailed() == null ? false : mergeEntries.isFailed().booleanValue();
                            if (z && !booleanValue) {
                                if (mergeEntries.isUnverified() == null ? true : mergeEntries.isUnverified().booleanValue()) {
                                    this.mTimeoutManager.mo20806a(str2, chatLog);
                                } else {
                                    this.mTimeoutManager.mo20805a(str2);
                                }
                            }
                        }
                    } else if (((ChatLog) ((LinkedHashMap) this.mData).get(str2)).getType() != Type.ATTACHMENT_UPLOAD) {
                        ((LinkedHashMap) this.mData).remove(str2);
                        i2--;
                        this.mTimeoutManager.mo20805a(str2);
                    }
                } else {
                    if (chatLog != null) {
                        try {
                            boolean z2 = Type.CHAT_MSG_VISITOR == chatLog.getType();
                            boolean z3 = chatLog.getMessage() != null && chatLog.getMessage().trim().isEmpty();
                            if (!z2 || !z3) {
                                Pair findAgentQuestionnaire = findAgentQuestionnaire(chatLog);
                                if (findAgentQuestionnaire != null) {
                                    Option[] options = ((ChatLog) findAgentQuestionnaire.second).getOptions();
                                    int i3 = 0;
                                    while (true) {
                                        if (i3 >= options.length) {
                                            break;
                                        } else if (chatLog.getMessage().equals(options[i3].getLabel())) {
                                            options[i3].select();
                                            ((LinkedHashMap) this.mData).put(findAgentQuestionnaire.first, findAgentQuestionnaire.second);
                                            break;
                                        } else {
                                            i3++;
                                        }
                                    }
                                } else {
                                    if (chatLog != null) {
                                        if (chatLog.getType() == Type.ATTACHMENT_UPLOAD) {
                                            String fileName = chatLog.getFileName();
                                            if (fileName != null) {
                                                chatLog.setFile(FileTransfers.INSTANCE.findFile(fileName));
                                                this.mUploadedFiles.put(fileName, str2);
                                            }
                                        }
                                    }
                                    if (chatLog.getType() == Type.CHAT_MSG_VISITOR) {
                                        String str3 = chatLog.getAttachment() != null ? chatLog.getAttachment().getName() : null;
                                        if (str3 != null) {
                                            ChatLog chatLog3 = (ChatLog) ((LinkedHashMap) this.mData).get((String) this.mUploadedFiles.get(str3));
                                            if (chatLog3 != null) {
                                                chatLog3.setProgress(100);
                                            }
                                        }
                                    }
                                    if (chatLog.getType() == Type.CHAT_RATING) {
                                        this.mChatRatingEntry = new Pair<>(str2, chatLog);
                                    }
                                    ((LinkedHashMap) this.mData).put(str2, chatLog);
                                    i = i2 + 1;
                                    if (z2) {
                                        try {
                                            if (chatLog.isUnverified() != null ? chatLog.isUnverified().booleanValue() : false) {
                                                this.mTimeoutManager.mo20806a(str2, chatLog);
                                            }
                                        } catch (Exception e2) {
                                            e = e2;
                                            i2 = i;
                                            Log.w(LOG_TAG, "Failed to process json. Chat log record could not be created.", e);
                                            this.mUnmatchedAgentQuestionnaire.addFirst(new Pair(str2, chatLog));
                                        }
                                    }
                                }
                            }
                        } catch (Exception e3) {
                            e = e3;
                            Log.w(LOG_TAG, "Failed to process json. Chat log record could not be created.", e);
                            this.mUnmatchedAgentQuestionnaire.addFirst(new Pair(str2, chatLog));
                        }
                    } else {
                        i = i2;
                    }
                    i2 = i;
                }
                if (chatLog != null && chatLog.getOptions() != null && chatLog.getOptions().length > 0) {
                    this.mUnmatchedAgentQuestionnaire.addFirst(new Pair(str2, chatLog));
                }
            }
            if (i2 >= 0) {
                broadcast(getData());
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public void clear() {
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
        return this.mData != null ? new LinkedHashMap((Map) this.mData) : new LinkedHashMap<>();
    }

    /* access modifiers changed from: 0000 */
    public void update(String str) {
        if (isClearRequired(str)) {
            clear();
        } else if (!str.isEmpty()) {
            updateInternal((LinkedHashMap) this.PARSER.parse(str, (TypeReference<T>) new C1235d<T>(this)));
        }
    }
}
