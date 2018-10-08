package io.fabric.sdk.android;

public interface Logger {
    /* renamed from: d */
    void mo4289d(String str, String str2);

    /* renamed from: d */
    void mo4290d(String str, String str2, Throwable th);

    /* renamed from: e */
    void mo4291e(String str, String str2);

    /* renamed from: e */
    void mo4292e(String str, String str2, Throwable th);

    int getLogLevel();

    /* renamed from: i */
    void mo4294i(String str, String str2);

    /* renamed from: i */
    void mo4295i(String str, String str2, Throwable th);

    boolean isLoggable(String str, int i);

    void log(int i, String str, String str2);

    void log(int i, String str, String str2, boolean z);

    void setLogLevel(int i);

    /* renamed from: v */
    void mo4300v(String str, String str2);

    /* renamed from: v */
    void mo4301v(String str, String str2, Throwable th);

    /* renamed from: w */
    void mo4302w(String str, String str2);

    /* renamed from: w */
    void mo4303w(String str, String str2, Throwable th);
}
