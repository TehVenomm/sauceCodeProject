package net.gogame.gowrap.integrations.zopim;

import android.app.Activity;
import com.zopim.android.sdk.api.ZopimChat.SessionConfig;
import java.util.UUID;
import net.gogame.chat.chatbot.ChatBotConfig;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import net.gogame.gowrap.integrations.CanChat;
import net.gogame.gowrap.integrations.Config;
import net.gogame.gowrap.integrations.IntegrationSupport.IntegrationContext;
import net.gogame.zopim.client.base.ZopimMainActivity;
import p017io.fabric.sdk.android.services.events.EventsFilesManager;

public class CustomZopimSupport extends AbstractIntegrationSupport implements CanChat {
    public static final String CONFIG_ACCOUNT_KEY = "accountKey";
    private IntegrationContext integrationContext;

    public CustomZopimSupport() {
        super("zopim");
    }

    public boolean isIntegrated() {
        return ZopimHelper.isIntegrated();
    }

    /* access modifiers changed from: protected */
    public void doInit(Activity activity, Config config, IntegrationContext integrationContext2) {
        this.integrationContext = integrationContext2;
        ZopimHelper.initChat(activity, config.getString(CONFIG_ACCOUNT_KEY));
    }

    public void startChat() {
        ChatBotConfig chatBotConfig;
        SessionConfig sessionConfig = null;
        Activity currentActivity = this.integrationContext.getCurrentActivity();
        if (this.integrationContext.isChatBotEnabled()) {
            ChatBotConfig chatBotConfig2 = new ChatBotConfig();
            String guid = this.integrationContext.getGuid();
            if (guid == null) {
                guid = UUID.randomUUID().toString() + EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR + System.currentTimeMillis();
            }
            chatBotConfig2.setAppId(this.integrationContext.getAppId());
            chatBotConfig2.setGuid(guid);
            chatBotConfig = chatBotConfig2;
        } else {
            chatBotConfig = null;
        }
        if (this.integrationContext.isVip() || this.integrationContext.isForceEnableChat()) {
            sessionConfig = ZopimHelper.getSessionConfig(currentActivity, this.integrationContext.getGuid());
        }
        if (chatBotConfig != null || sessionConfig != null) {
            ZopimMainActivity.startActivity(currentActivity, chatBotConfig, sessionConfig);
        }
    }
}
