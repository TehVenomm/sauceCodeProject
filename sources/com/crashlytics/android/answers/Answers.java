package com.crashlytics.android.answers;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.os.Build.VERSION;
import java.io.File;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.common.CommonUtils;
import p017io.fabric.sdk.android.services.common.Crash.FatalException;
import p017io.fabric.sdk.android.services.common.Crash.LoggedException;
import p017io.fabric.sdk.android.services.common.DataCollectionArbiter;
import p017io.fabric.sdk.android.services.common.FirebaseInfo;
import p017io.fabric.sdk.android.services.common.IdManager;
import p017io.fabric.sdk.android.services.settings.Settings;
import p017io.fabric.sdk.android.services.settings.SettingsData;

public class Answers extends Kit<Boolean> {
    static final String CRASHLYTICS_API_ENDPOINT = "com.crashlytics.ApiEndpoint";
    public static final String TAG = "Answers";
    SessionAnalyticsManager analyticsManager;
    boolean firebaseEnabled = false;

    public static Answers getInstance() {
        return (Answers) Fabric.getKit(Answers.class);
    }

    private void logFirebaseModeEnabledWarning(String str) {
        Fabric.getLogger().mo20982w(TAG, "Method " + str + " is not supported when using Crashlytics through Firebase.");
    }

    /* access modifiers changed from: protected */
    public Boolean doInBackground() {
        if (!DataCollectionArbiter.getInstance(getContext()).isDataCollectionEnabled()) {
            Fabric.getLogger().mo20969d(Fabric.TAG, "Analytics collection disabled, because data collection is disabled by Firebase.");
            this.analyticsManager.disable();
            return Boolean.valueOf(false);
        }
        try {
            SettingsData awaitSettingsData = Settings.getInstance().awaitSettingsData();
            if (awaitSettingsData == null) {
                Fabric.getLogger().mo20971e(TAG, "Failed to retrieve settings");
                return Boolean.valueOf(false);
            } else if (awaitSettingsData.featuresData.collectAnalytics) {
                Fabric.getLogger().mo20969d(TAG, "Analytics collection enabled");
                this.analyticsManager.setAnalyticsSettingsData(awaitSettingsData.analyticsSettingsData, getOverridenSpiEndpoint());
                return Boolean.valueOf(true);
            } else {
                Fabric.getLogger().mo20969d(TAG, "Analytics collection disabled");
                this.analyticsManager.disable();
                return Boolean.valueOf(false);
            }
        } catch (Exception e) {
            Fabric.getLogger().mo20972e(TAG, "Error dealing with settings", e);
            return Boolean.valueOf(false);
        }
    }

    public String getIdentifier() {
        return "com.crashlytics.sdk.android:answers";
    }

    /* access modifiers changed from: 0000 */
    public String getOverridenSpiEndpoint() {
        return CommonUtils.getStringsFileValue(getContext(), CRASHLYTICS_API_ENDPOINT);
    }

    public String getVersion() {
        return "1.4.7.32";
    }

    public void logAddToCart(AddToCartEvent addToCartEvent) {
        if (addToCartEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logAddToCart");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onPredefined(addToCartEvent);
        }
    }

    public void logContentView(ContentViewEvent contentViewEvent) {
        if (contentViewEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logContentView");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onPredefined(contentViewEvent);
        }
    }

    public void logCustom(CustomEvent customEvent) {
        if (customEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logCustom");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onCustom(customEvent);
        }
    }

    public void logInvite(InviteEvent inviteEvent) {
        if (inviteEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logInvite");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onPredefined(inviteEvent);
        }
    }

    public void logLevelEnd(LevelEndEvent levelEndEvent) {
        if (levelEndEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logLevelEnd");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onPredefined(levelEndEvent);
        }
    }

    public void logLevelStart(LevelStartEvent levelStartEvent) {
        if (levelStartEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logLevelStart");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onPredefined(levelStartEvent);
        }
    }

    public void logLogin(LoginEvent loginEvent) {
        if (loginEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logLogin");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onPredefined(loginEvent);
        }
    }

    public void logPurchase(PurchaseEvent purchaseEvent) {
        if (purchaseEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logPurchase");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onPredefined(purchaseEvent);
        }
    }

    public void logRating(RatingEvent ratingEvent) {
        if (ratingEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logRating");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onPredefined(ratingEvent);
        }
    }

    public void logSearch(SearchEvent searchEvent) {
        if (searchEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logSearch");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onPredefined(searchEvent);
        }
    }

    public void logShare(ShareEvent shareEvent) {
        if (shareEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logShare");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onPredefined(shareEvent);
        }
    }

    public void logSignUp(SignUpEvent signUpEvent) {
        if (signUpEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logSignUp");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onPredefined(signUpEvent);
        }
    }

    public void logStartCheckout(StartCheckoutEvent startCheckoutEvent) {
        if (startCheckoutEvent == null) {
            throw new NullPointerException("event must not be null");
        } else if (this.firebaseEnabled) {
            logFirebaseModeEnabledWarning("logStartCheckout");
        } else if (this.analyticsManager != null) {
            this.analyticsManager.onPredefined(startCheckoutEvent);
        }
    }

    public void onException(FatalException fatalException) {
        if (this.analyticsManager != null) {
            this.analyticsManager.onCrash(fatalException.getSessionId(), fatalException.getExceptionName());
        }
    }

    public void onException(LoggedException loggedException) {
        if (this.analyticsManager != null) {
            this.analyticsManager.onError(loggedException.getSessionId());
        }
    }

    /* access modifiers changed from: protected */
    @SuppressLint({"NewApi"})
    public boolean onPreExecute() {
        try {
            Context context = getContext();
            PackageManager packageManager = context.getPackageManager();
            String packageName = context.getPackageName();
            PackageInfo packageInfo = packageManager.getPackageInfo(packageName, 0);
            this.analyticsManager = SessionAnalyticsManager.build(this, context, getIdManager(), Integer.toString(packageInfo.versionCode), packageInfo.versionName == null ? IdManager.DEFAULT_VERSION_NAME : packageInfo.versionName, VERSION.SDK_INT >= 9 ? packageInfo.firstInstallTime : new File(packageManager.getApplicationInfo(packageName, 0).sourceDir).lastModified());
            this.analyticsManager.enable();
            this.firebaseEnabled = new FirebaseInfo().isFirebaseCrashlyticsEnabled(context);
            return true;
        } catch (Exception e) {
            Fabric.getLogger().mo20972e(TAG, "Error retrieving app properties", e);
            return false;
        }
    }
}
