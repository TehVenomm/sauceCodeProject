package io.fabric.sdk.android;

import android.util.Log;

public class DefaultLogger implements Logger {
    private int logLevel;

    public DefaultLogger() {
        this.logLevel = 4;
    }

    public DefaultLogger(int i) {
        this.logLevel = i;
    }

    /* renamed from: d */
    public void mo4289d(String str, String str2) {
        mo4290d(str, str2, null);
    }

    /* renamed from: d */
    public void mo4290d(String str, String str2, Throwable th) {
        if (isLoggable(str, 3)) {
            Log.d(str, str2, th);
        }
    }

    /* renamed from: e */
    public void mo4291e(String str, String str2) {
        mo4292e(str, str2, null);
    }

    /* renamed from: e */
    public void mo4292e(String str, String str2, Throwable th) {
        if (isLoggable(str, 6)) {
            Log.e(str, str2, th);
        }
    }

    public int getLogLevel() {
        return this.logLevel;
    }

    /* renamed from: i */
    public void mo4294i(String str, String str2) {
        mo4295i(str, str2, null);
    }

    /* renamed from: i */
    public void mo4295i(String str, String str2, Throwable th) {
        if (isLoggable(str, 4)) {
            Log.i(str, str2, th);
        }
    }

    public boolean isLoggable(String str, int i) {
        return this.logLevel <= i;
    }

    public void log(int i, String str, String str2) {
        log(i, str, str2, false);
    }

    public void log(int i, String str, String str2, boolean z) {
        if (z || isLoggable(str, i)) {
            Log.println(i, str, str2);
        }
    }

    public void setLogLevel(int i) {
        this.logLevel = i;
    }

    /* renamed from: v */
    public void mo4300v(String str, String str2) {
        mo4301v(str, str2, null);
    }

    /* renamed from: v */
    public void mo4301v(String str, String str2, Throwable th) {
        if (isLoggable(str, 2)) {
            Log.v(str, str2, th);
        }
    }

    /* renamed from: w */
    public void mo4302w(String str, String str2) {
        mo4303w(str, str2, null);
    }

    /* renamed from: w */
    public void mo4303w(String str, String str2, Throwable th) {
        if (isLoggable(str, 5)) {
            Log.w(str, str2, th);
        }
    }
}
