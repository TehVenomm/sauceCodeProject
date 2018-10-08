package net.gogame.gowrap.integrations.zopim;

import android.app.Activity;
import android.content.Context;
import com.zopim.android.sdk.api.ZopimChat.SessionConfig;
import io.fabric.sdk.android.services.events.EventsFilesManager;
import java.util.UUID;
import net.gogame.chat.chatbot.ChatBotConfig;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import net.gogame.gowrap.integrations.CanChat;
import net.gogame.gowrap.integrations.Config;
import net.gogame.gowrap.integrations.IntegrationSupport.IntegrationContext;
import net.gogame.zopim.client.base.ZopimMainActivity;

public class CustomZopimSupport extends AbstractIntegrationSupport implements CanChat {
    public static final String CONFIG_ACCOUNT_KEY = "accountKey";
    public static final CustomZopimSupport INSTANCE = new CustomZopimSupport();
    private IntegrationContext integrationContext;

    private CustomZopimSupport() {
        super("zopim");
    }

    public boolean isIntegrated() {
        return ZopimHelper.isIntegrated();
    }

    protected void doInit(Activity activity, Config config, IntegrationContext integrationContext) {
        this.integrationContext = integrationContext;
        ZopimHelper.initChat(activity, config.getString(CONFIG_ACCOUNT_KEY));
    }

    public void startChat() {
        ChatBotConfig chatBotConfig;
        SessionConfig sessionConfig = null;
        Context currentActivity = this.integrationContext.getCurrentActivity();
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
