package net.gogame.gowrap.bootstrap;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.p019ui.ActivityHelper;

public abstract class AbstractBootstrap {
    private boolean initialized = false;

    /* access modifiers changed from: protected */
    public abstract void doCustomInit(Activity activity);

    public void doInit(Activity activity, boolean z) {
        if (!this.initialized) {
            Log.v(Constants.TAG, "Initializing...");
            try {
                doCustomInit(activity);
                Log.v(Constants.TAG, "Initialized");
                if (z) {
                    try {
                        ActivityHelper.INSTANCE.onActivityCreated(activity, new Bundle());
                        ActivityHelper.INSTANCE.onActivityStarted(activity);
                        ActivityHelper.INSTANCE.onActivityResumed(activity);
                    } catch (Exception e) {
                        Log.e(Constants.TAG, "Exception", e);
                    }
                }
            } finally {
                if (z) {
                    try {
                        ActivityHelper.INSTANCE.onActivityCreated(activity, new Bundle());
                        ActivityHelper.INSTANCE.onActivityStarted(activity);
                        ActivityHelper.INSTANCE.onActivityResumed(activity);
                    } catch (Exception e2) {
                        Log.e(Constants.TAG, "Exception", e2);
                    }
                }
                this.initialized = true;
            }
        } else {
            Log.v(Constants.TAG, "Already initialized");
        }
    }

    public void doInit(Activity activity) {
        doInit(activity, false);
    }

    public void doUnityInit() {
        try {
            doInit((Activity) Class.forName("com.unity3d.player.UnityPlayer").getField("currentActivity").get(null), true);
        } catch (Exception e) {
        }
    }
}
