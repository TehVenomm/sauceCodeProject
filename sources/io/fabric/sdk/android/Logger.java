package p017io.fabric.sdk.android;

/* renamed from: io.fabric.sdk.android.Logger */
public interface Logger {
    /* renamed from: d */
    void mo20969d(String str, String str2);

    /* renamed from: d */
    void mo20970d(String str, String str2, Throwable th);

    /* renamed from: e */
    void mo20971e(String str, String str2);

    /* renamed from: e */
    void mo20972e(String str, String str2, Throwable th);

    int getLogLevel();

    /* renamed from: i */
    void mo20974i(String str, String str2);

    /* renamed from: i */
    void mo20975i(String str, String str2, Throwable th);

    boolean isLoggable(String str, int i);

    void log(int i, String str, String str2);

    void log(int i, String str, String str2, boolean z);

    void setLogLevel(int i);

    /* renamed from: v */
    void mo20980v(String str, String str2);

    /* renamed from: v */
    void mo20981v(String str, String str2, Throwable th);

    /* renamed from: w */
    void mo20982w(String str, String str2);

    /* renamed from: w */
    void mo20983w(String str, String str2, Throwable th);
}
