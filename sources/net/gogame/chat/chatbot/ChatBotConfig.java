package net.gogame.chat.chatbot;

import java.io.Serializable;

public class ChatBotConfig implements Serializable {
    private String appId;
    private String guid;

    public String getAppId() {
        return this.appId;
    }

    public void setAppId(String str) {
        this.appId = str;
    }

    public String getGuid() {
        return this.guid;
    }

    public void setGuid(String str) {
        this.guid = str;
    }
}
