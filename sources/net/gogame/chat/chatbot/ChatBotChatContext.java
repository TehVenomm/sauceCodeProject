package net.gogame.chat.chatbot;

import android.app.Activity;
import android.os.AsyncTask;
import android.os.SystemClock;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import com.facebook.AccessToken;
import com.google.android.gms.measurement.AppMeasurement.Param;
import io.fabric.sdk.android.services.events.EventsFilesManager;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;
import net.gogame.chat.AbstractChatContext;
import net.gogame.chat.AgentTypingEntry;
import net.gogame.chat.ChatAdapterViewFactory;
import net.gogame.chat.ChatContext.Rating;
import net.gogame.chat.Constants;
import net.gogame.chat.IOUtils;
import org.apache.commons.lang3.StringUtils;
import org.json.JSONArray;
import org.json.JSONObject;

public class ChatBotChatContext extends AbstractChatContext {
    private static final boolean DEBUG = false;
    private static final String DEFAULT_AGENT_AVATAR_URI = null;
    private static final String DEFAULT_AGENT_DISPLAY_NAME = "Sarah";
    private static final String DEFAULT_AGENT_ID = "default";
    private static final String SERVICE_URL = "https://gw-chat.gogame.net/webchat/receive/";
    private final Activity activity;
    private final AgentTypingEntry agentTypingEntry = new AgentTypingEntry(false);
    private final ChatBotConfig chatBotConfig;
    private final List<ChatLog> chatLogs = new ArrayList();
    private final String guid;
    private final ChatAdapterViewFactory viewFactory;

    public static class ChatLog {
        private String agentAvatarUri;
        private String agentDisplayName;
        private String agentId;
        private String message;
        private long timestamp;
        private Type type;

        enum Type {
            CHAT_MSG_VISITOR,
            CHAT_MSG_AGENT,
            CHAT_MSG_SYSTEM
        }

        public long getTimestamp() {
            return this.timestamp;
        }

        public void setTimestamp(long j) {
            this.timestamp = j;
        }

        public Type getType() {
            return this.type;
        }

        public void setType(Type type) {
            this.type = type;
        }

        public String getAgentId() {
            return this.agentId;
        }

        public void setAgentId(String str) {
            this.agentId = str;
        }

        public String getAgentDisplayName() {
            return this.agentDisplayName;
        }

        public void setAgentDisplayName(String str) {
            this.agentDisplayName = str;
        }

        public String getAgentAvatarUri() {
            return this.agentAvatarUri;
        }

        public void setAgentAvatarUri(String str) {
            this.agentAvatarUri = str;
        }

        public String getMessage() {
            return this.message;
        }

        public void setMessage(String str) {
            this.message = str;
        }
    }

    private class SendMessageTask extends AsyncTask<String, Void, Void> {
        private SendMessageTask() {
        }

        protected Void doInBackground(String... strArr) {
            for (String send : strArr) {
                send(send);
            }
            return null;
        }

        private void send(String str) {
            try {
                HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(ChatBotChatContext.SERVICE_URL + ChatBotChatContext.this.chatBotConfig.getAppId()).openConnection();
                try {
                    httpURLConnection.setDoOutput(true);
                    httpURLConnection.setChunkedStreamingMode(0);
                    httpURLConnection.setRequestProperty(HttpRequest.HEADER_CONTENT_TYPE, "application/json");
                    OutputStream bufferedOutputStream = new BufferedOutputStream(httpURLConnection.getOutputStream());
                    InputStream bufferedInputStream;
                    try {
                        JSONObject jSONObject = new JSONObject();
                        jSONObject.put(AccessToken.USER_ID_KEY, ChatBotChatContext.this.guid);
                        if (str != null) {
                            jSONObject.put("message", str);
                        }
                        bufferedOutputStream.write(jSONObject.toString().getBytes("UTF-8"));
                        bufferedOutputStream.flush();
                        bufferedOutputStream.close();
                        bufferedInputStream = new BufferedInputStream(httpURLConnection.getInputStream());
                        OutputStream byteArrayOutputStream = new ByteArrayOutputStream();
                        IOUtils.copy(bufferedInputStream, byteArrayOutputStream);
                        jSONObject = new JSONObject(new String(byteArrayOutputStream.toByteArray(), "UTF-8"));
                        final long optLong = jSONObject.optLong(Param.TIMESTAMP, System.currentTimeMillis());
                        final String optString = jSONObject.optString("agentId", "default");
                        final String optString2 = jSONObject.optString("agentDisplayName", ChatBotChatContext.DEFAULT_AGENT_DISPLAY_NAME);
                        final String optString3 = jSONObject.optString("agentAvatarUri", ChatBotChatContext.DEFAULT_AGENT_AVATAR_URI);
                        JSONArray optJSONArray = jSONObject.optJSONArray("response");
                        final List arrayList = new ArrayList();
                        if (optJSONArray != null) {
                            for (int i = 0; i < optJSONArray.length(); i++) {
                                String optString4 = optJSONArray.optString(i, null);
                                if (optString4 != null) {
                                    arrayList.add(optString4);
                                }
                            }
                        }
                        if (!arrayList.isEmpty()) {
                            ChatBotChatContext.this.activity.runOnUiThread(new Runnable() {
                                public void run() {
                                    for (String str : arrayList) {
                                        ChatLog chatLog = new ChatLog();
                                        chatLog.setType(Type.CHAT_MSG_AGENT);
                                        chatLog.setTimestamp(optLong);
                                        chatLog.setAgentId(optString);
                                        chatLog.setAgentDisplayName(optString2);
                                        chatLog.setAgentAvatarUri(optString3);
                                        chatLog.setMessage(str);
                                        ChatBotChatContext.this.chatLogs.add(chatLog);
                                    }
                                    ChatBotChatContext.this.notifyDataSetChanged();
                                }
                            });
                        }
                        IOUtils.closeQuietly(bufferedInputStream);
                        IOUtils.closeQuietly(bufferedOutputStream);
                    } catch (Throwable th) {
                        IOUtils.closeQuietly(bufferedOutputStream);
                    }
                } finally {
                    httpURLConnection.disconnect();
                }
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
                ChatLog chatLog = new ChatLog();
                chatLog.setType(Type.CHAT_MSG_SYSTEM);
                chatLog.setTimestamp(SystemClock.currentThreadTimeMillis());
                chatLog.setMessage("System currently unavailable, we apologize for the inconvenience.");
                ChatBotChatContext.this.addChatLog(chatLog);
            }
        }
    }

    public ChatBotChatContext(Activity activity, ChatBotConfig chatBotConfig, ChatAdapterViewFactory chatAdapterViewFactory) {
        String guid;
        this.activity = activity;
        this.chatBotConfig = chatBotConfig;
        this.viewFactory = chatAdapterViewFactory;
        if (chatBotConfig.getGuid() != null) {
            guid = chatBotConfig.getGuid();
        } else {
            guid = UUID.randomUUID().toString() + EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR + System.currentTimeMillis();
        }
        this.guid = guid;
    }

    public void start() {
        new SendMessageTask().execute(new String[]{(String) null});
    }

    public void stop() {
    }

    public boolean isAttachmentSupported() {
        return false;
    }

    public int getChatEntryCount() {
        return this.chatLogs.size();
    }

    public Object getChatEntry(int i) {
        if (i < 0 || i >= this.chatLogs.size()) {
            return null;
        }
        return this.chatLogs.get(i);
    }

    public AgentTypingEntry getAgentTypingEntry() {
        return this.agentTypingEntry;
    }

    public void send(String str) {
        if (StringUtils.trimToNull(str) != null) {
            ChatLog chatLog = new ChatLog();
            chatLog.setType(Type.CHAT_MSG_VISITOR);
            chatLog.setTimestamp(System.currentTimeMillis());
            chatLog.setMessage(str);
            addChatLog(chatLog);
            new SendMessageTask().execute(new String[]{str});
        }
    }

    public void send(File file) {
    }

    public void send(Rating rating) {
    }

    public View getView(Object obj, int i, View view, ViewGroup viewGroup) {
        boolean z = true;
        if (!(obj instanceof ChatLog)) {
            return null;
        }
        ChatLog chatLog = (ChatLog) obj;
        if (chatLog.getType() == null) {
            return this.viewFactory.getEmptyView(view, viewGroup);
        }
        switch (chatLog.getType()) {
            case CHAT_MSG_VISITOR:
                return this.viewFactory.getVisitorMessageView(view, viewGroup, chatLog.getMessage());
            case CHAT_MSG_AGENT:
                if (i != 0 && ((ChatLog) this.chatLogs.get(i - 1)).getType() == Type.CHAT_MSG_AGENT) {
                    z = false;
                }
                return this.viewFactory.getAgentMessageView(view, viewGroup, z, chatLog.getAgentDisplayName(), chatLog.getAgentAvatarUri(), chatLog.getMessage());
            case CHAT_MSG_SYSTEM:
                return this.viewFactory.getNotificationView(view, viewGroup, chatLog.getMessage());
            default:
                return this.viewFactory.getEmptyView(view, viewGroup);
        }
    }

    private void addChatLog(final ChatLog chatLog) {
        this.activity.runOnUiThread(new Runnable() {
            public void run() {
                ChatBotChatContext.this.chatLogs.add(chatLog);
                ChatBotChatContext.this.notifyDataSetChanged();
            }
        });
    }
}
