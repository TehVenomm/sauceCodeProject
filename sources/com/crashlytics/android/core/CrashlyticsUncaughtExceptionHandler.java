package com.crashlytics.android.core;

import java.lang.Thread.UncaughtExceptionHandler;
import java.util.concurrent.atomic.AtomicBoolean;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.services.settings.SettingsData;

class CrashlyticsUncaughtExceptionHandler implements UncaughtExceptionHandler {
    private final CrashListener crashListener;
    private final UncaughtExceptionHandler defaultHandler;
    private final boolean firebaseCrashlyticsClientFlag;
    private final AtomicBoolean isHandlingException = new AtomicBoolean(false);
    private final SettingsDataProvider settingsDataProvider;

    interface CrashListener {
        void onUncaughtException(SettingsDataProvider settingsDataProvider, Thread thread, Throwable th, boolean z);
    }

    interface SettingsDataProvider {
        SettingsData getSettingsData();
    }

    public CrashlyticsUncaughtExceptionHandler(CrashListener crashListener2, SettingsDataProvider settingsDataProvider2, boolean z, UncaughtExceptionHandler uncaughtExceptionHandler) {
        this.crashListener = crashListener2;
        this.settingsDataProvider = settingsDataProvider2;
        this.firebaseCrashlyticsClientFlag = z;
        this.defaultHandler = uncaughtExceptionHandler;
    }

    /* access modifiers changed from: 0000 */
    public boolean isHandlingException() {
        return this.isHandlingException.get();
    }

    public void uncaughtException(Thread thread, Throwable th) {
        this.isHandlingException.set(true);
        try {
            this.crashListener.onUncaughtException(this.settingsDataProvider, thread, th, this.firebaseCrashlyticsClientFlag);
        } catch (Exception e) {
            Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "An error occurred in the uncaught exception handler", e);
        } finally {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Crashlytics completed exception processing. Invoking default exception handler.");
            this.defaultHandler.uncaughtException(thread, th);
            this.isHandlingException.set(false);
        }
    }
}
