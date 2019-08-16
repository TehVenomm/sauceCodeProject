package p017io.fabric.sdk.android;

import android.util.Log;

/* renamed from: io.fabric.sdk.android.DefaultLogger */
public class DefaultLogger implements Logger {
    private int logLevel;

    public DefaultLogger() {
        this.logLevel = 4;
    }

    public DefaultLogger(int i) {
        this.logLevel = i;
    }

    /* renamed from: d */
    public void mo20969d(String str, String str2) {
        mo20970d(str, str2, null);
    }

    /* renamed from: d */
    public void mo20970d(String str, String str2, Throwable th) {
        if (isLoggable(str, 3)) {
            Log.d(str, str2, th);
        }
    }

    /* renamed from: e */
    public void mo20971e(String str, String str2) {
        mo20972e(str, str2, null);
    }

    /* renamed from: e */
    public void mo20972e(String str, String str2, Throwable th) {
        if (isLoggable(str, 6)) {
            Log.e(str, str2, th);
        }
    }

    public int getLogLevel() {
        return this.logLevel;
    }

    /* renamed from: i */
    public void mo20974i(String str, String str2) {
        mo20975i(str, str2, null);
    }

    /* renamed from: i */
    public void mo20975i(String str, String str2, Throwable th) {
        if (isLoggable(str, 4)) {
            Log.i(str, str2, th);
        }
    }

    public boolean isLoggable(String str, int i) {
        return this.logLevel <= i || Log.isLoggable(str, i);
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
    public void mo20980v(String str, String str2) {
        mo20981v(str, str2, null);
    }

    /* renamed from: v */
    public void mo20981v(String str, String str2, Throwable th) {
        if (isLoggable(str, 2)) {
            Log.v(str, str2, th);
        }
    }

    /* renamed from: w */
    public void mo20982w(String str, String str2) {
        mo20983w(str, str2, null);
    }

    /* renamed from: w */
    public void mo20983w(String str, String str2, Throwable th) {
        if (isLoggable(str, 5)) {
            Log.w(str, str2, th);
        }
    }
}
