package jp.colopl.libs;

import android.app.IntentService;
import android.content.Intent;
import io.fabric.sdk.android.services.settings.SettingsJsonConstants;

public class AnalyticsService extends IntentService {
    static {
        System.loadLibrary(SettingsJsonConstants.ANALYTICS_KEY);
    }

    public AnalyticsService() {
        super("AnalyticsService");
    }

    public AnalyticsService(String str) {
        super(str);
    }

    public native void ana();

    protected void onHandleIntent(Intent intent) {
        ana();
    }
}
