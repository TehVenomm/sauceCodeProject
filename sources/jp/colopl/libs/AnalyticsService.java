package p018jp.colopl.libs;

import android.app.IntentService;
import android.content.Intent;
import p017io.fabric.sdk.android.services.settings.SettingsJsonConstants;

/* renamed from: jp.colopl.libs.AnalyticsService */
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

    /* access modifiers changed from: protected */
    public void onHandleIntent(Intent intent) {
        ana();
    }
}
