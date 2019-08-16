package com.unity3d.player;

import android.os.Build;
import java.lang.Thread.UncaughtExceptionHandler;

/* renamed from: com.unity3d.player.k */
final class C1115k implements UncaughtExceptionHandler {

    /* renamed from: a */
    private volatile UncaughtExceptionHandler f602a;

    C1115k() {
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: a */
    public final boolean mo20528a() {
        boolean z;
        synchronized (this) {
            UncaughtExceptionHandler defaultUncaughtExceptionHandler = Thread.getDefaultUncaughtExceptionHandler();
            if (defaultUncaughtExceptionHandler == this) {
                z = false;
            } else {
                this.f602a = defaultUncaughtExceptionHandler;
                Thread.setDefaultUncaughtExceptionHandler(this);
                z = true;
            }
        }
        return z;
    }

    public final void uncaughtException(Thread thread, Throwable th) {
        synchronized (this) {
            try {
                Error error = new Error(String.format("FATAL EXCEPTION [%s]\n", new Object[]{thread.getName()}) + String.format("Unity version     : %s\n", new Object[]{"5.6.7f1"}) + String.format("Device model      : %s %s\n", new Object[]{Build.MANUFACTURER, Build.MODEL}) + String.format("Device fingerprint: %s\n", new Object[]{Build.FINGERPRINT}));
                error.setStackTrace(new StackTraceElement[0]);
                error.initCause(th);
                this.f602a.uncaughtException(thread, error);
            } catch (Throwable th2) {
                this.f602a.uncaughtException(thread, th);
            }
        }
        return;
    }
}
