package com.facebook.unity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

public abstract class FBUnityAppLinkBaseActivity extends Activity {
    private Class<?> getMainActivityClass() {
        try {
            return Class.forName(getPackageManager().getLaunchIntentForPackage(getPackageName()).getComponent().getClassName());
        } catch (Exception e) {
            Log.e(C0849FB.TAG, "Unable to find Main Activity Class");
            return null;
        }
    }

    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        requestWindowFeature(1);
        Log.v(C0849FB.TAG, "Saving deep link from deep linking activity");
        C0849FB.SetIntent(getIntent());
        Log.v(C0849FB.TAG, "Returning to main activity");
        startActivity(new Intent(this, getMainActivityClass()));
        finish();
    }
}
