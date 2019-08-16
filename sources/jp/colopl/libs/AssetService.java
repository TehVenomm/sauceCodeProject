package p018jp.colopl.libs;

import android.app.IntentService;
import android.content.Intent;

/* renamed from: jp.colopl.libs.AssetService */
public class AssetService extends IntentService {

    /* renamed from: a */
    private static boolean f986a = false;

    static {
        System.loadLibrary("asset");
    }

    public AssetService() {
        super("AssetService");
    }

    public AssetService(String str) {
        super(str);
    }

    public native void asset();

    /* access modifiers changed from: protected */
    public void onHandleIntent(Intent intent) {
        if (intent.getStringExtra("asset").equals("start") || !f986a) {
            asset();
            f986a = true;
        }
    }
}
