package net.gogame.gowrap.p019ui;

import android.annotation.TargetApi;
import android.app.Activity;
import android.app.Application.ActivityLifecycleCallbacks;
import android.os.Bundle;
import android.util.Log;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.integrations.IntegrationSupport;

@TargetApi(11)
/* renamed from: net.gogame.gowrap.ui.ActivityHelper */
public final class ActivityHelper implements ActivityLifecycleCallbacks {
    public static final ActivityHelper INSTANCE = new ActivityHelper();
    private Activity currentActivity = null;

    private ActivityHelper() {
    }

    public Activity getCurrentActivity() {
        return this.currentActivity;
    }

    public void setCurrentActivity(Activity activity) {
        this.currentActivity = activity;
    }

    public void onActivityCreated(Activity activity, Bundle bundle) {
        if (GoWrapImpl.INSTANCE.getIntegrationSupportList() != null) {
            for (IntegrationSupport onActivityCreated : GoWrapImpl.INSTANCE.getIntegrationSupportList()) {
                try {
                    onActivityCreated.onActivityCreated(activity);
                } catch (Exception e) {
                    Log.e(Constants.TAG, "Exception", e);
                }
            }
        }
    }

    public void onActivityStarted(Activity activity) {
        if (GoWrapImpl.INSTANCE.getIntegrationSupportList() != null) {
            for (IntegrationSupport onActivityStarted : GoWrapImpl.INSTANCE.getIntegrationSupportList()) {
                try {
                    onActivityStarted.onActivityStarted(activity);
                } catch (Exception e) {
                    Log.e(Constants.TAG, "Exception", e);
                }
            }
        }
    }

    public void onActivityResumed(Activity activity) {
        this.currentActivity = activity;
        if (GoWrapImpl.INSTANCE.getIntegrationSupportList() != null) {
            for (IntegrationSupport onActivityResumed : GoWrapImpl.INSTANCE.getIntegrationSupportList()) {
                try {
                    onActivityResumed.onActivityResumed(activity);
                } catch (Exception e) {
                    Log.e(Constants.TAG, "Exception", e);
                }
            }
        }
    }

    public void onActivityPaused(Activity activity) {
        if (GoWrapImpl.INSTANCE.getIntegrationSupportList() != null) {
            for (IntegrationSupport onActivityPaused : GoWrapImpl.INSTANCE.getIntegrationSupportList()) {
                try {
                    onActivityPaused.onActivityPaused(activity);
                } catch (Exception e) {
                    Log.e(Constants.TAG, "Exception", e);
                }
            }
        }
    }

    public void onActivityStopped(Activity activity) {
        if (GoWrapImpl.INSTANCE.getIntegrationSupportList() != null) {
            for (IntegrationSupport onActivityStopped : GoWrapImpl.INSTANCE.getIntegrationSupportList()) {
                try {
                    onActivityStopped.onActivityStopped(activity);
                } catch (Exception e) {
                    Log.e(Constants.TAG, "Exception", e);
                }
            }
        }
    }

    public void onActivitySaveInstanceState(Activity activity, Bundle bundle) {
    }

    public void onActivityDestroyed(Activity activity) {
        if (GoWrapImpl.INSTANCE.getIntegrationSupportList() != null) {
            for (IntegrationSupport onActivityDestroyed : GoWrapImpl.INSTANCE.getIntegrationSupportList()) {
                try {
                    onActivityDestroyed.onActivityDestroyed(activity);
                } catch (Exception e) {
                    Log.e(Constants.TAG, "Exception", e);
                }
            }
        }
    }
}
