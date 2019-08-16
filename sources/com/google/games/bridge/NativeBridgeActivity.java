package com.google.games.bridge;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;

public final class NativeBridgeActivity extends Activity {
    private static final int BG_COLOR = 1090519039;
    private static final String BRIDGED_INTENT = "BRIDGED_INTENT";
    private static final int GPG_RESPONSE_CODE = 4673607;
    private static final String TAG = "NativeBridgeActivity";
    private boolean pendingResult;

    static {
        System.loadLibrary("gpg");
    }

    private native void forwardActivityResult(int i, int i2, Intent intent);

    public static void launchBridgeIntent(Activity activity, Intent intent) {
        Log.d(TAG, "Launching bridge activity: parent:" + activity + " intent " + intent);
        Intent intent2 = new Intent(activity, NativeBridgeActivity.class);
        intent2.putExtra(BRIDGED_INTENT, intent);
        activity.startActivity(intent2);
    }

    /* access modifiers changed from: protected */
    public void onActivityResult(int i, int i2, Intent intent) {
        if (i == GPG_RESPONSE_CODE) {
            Log.d(TAG, "Forwarding activity result to native SDK.");
            forwardActivityResult(i, i2, intent);
            this.pendingResult = false;
        } else {
            Log.d(TAG, "onActivityResult for unknown request code: " + i + " calling finish()");
        }
        finish();
        super.onActivityResult(i, i2, intent);
    }

    public void onCreate(Bundle bundle) {
        View view = new View(this);
        view.setBackgroundColor(BG_COLOR);
        setContentView(view);
        super.onCreate(bundle);
    }

    /* access modifiers changed from: protected */
    public void onDestroy() {
        if (this.pendingResult) {
            Log.w(TAG, "onDestroy called with pendingResult == true.  forwarding canceled result");
            forwardActivityResult(GPG_RESPONSE_CODE, 0, null);
            this.pendingResult = false;
        }
        super.onDestroy();
    }

    /* access modifiers changed from: protected */
    public void onStart() {
        Intent intent = (Intent) getIntent().getParcelableExtra(BRIDGED_INTENT);
        if (intent != null) {
            startActivityForResult(intent, GPG_RESPONSE_CODE);
        }
        super.onStart();
    }

    public void startActivityForResult(Intent intent, int i) {
        this.pendingResult = i == GPG_RESPONSE_CODE;
        if (this.pendingResult) {
            Log.d(TAG, "starting GPG activity: " + intent);
        } else {
            Log.i(TAG, "starting non-GPG activity: " + i + " " + intent);
        }
        super.startActivityForResult(intent, i);
    }
}
