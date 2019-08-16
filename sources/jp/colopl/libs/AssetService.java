package p018jp.colopl.libs;

import android.app.IntentService;
import android.content.Intent;

/* renamed from: jp.colopl.libs.AssetService */
public class AssetService extends IntentService {

    /* renamed from: a */
    private static boolean f980a = false;

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
        if (intent.getStringExtra("asset").equals("start") || !f980a) {
            asset();
            f980a = true;
        }
    }
}
