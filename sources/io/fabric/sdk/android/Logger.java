package io.fabric.sdk.android;

public interface Logger {
    /* renamed from: d */
    void mo4753d(String str, String str2);

    /* renamed from: d */
    void mo4754d(String str, String str2, Throwable th);

    /* renamed from: e */
    void mo4755e(String str, String str2);

    /* renamed from: e */
    void mo4756e(String str, String str2, Throwable th);

    int getLogLevel();

    /* renamed from: i */
    void mo4758i(String str, String str2);

    /* renamed from: i */
    void mo4759i(String str, String str2, Throwable th);

    boolean isLoggable(String str, int i);

    void log(int i, String str, String str2);

    void log(int i, String str, String str2, boolean z);

    void setLogLevel(int i);

    /* renamed from: v */
    void mo4764v(String str, String str2);

    /* renamed from: v */
    void mo4765v(String str, String str2, Throwable th);

    /* renamed from: w */
    void mo4766w(String str, String str2);

    /* renamed from: w */
    void mo4767w(String str, String str2, Throwable th);
}
