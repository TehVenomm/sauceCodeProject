package im.getsocial.sdk.internal.p033c.p057g;

import com.google.android.gms.nearby.messages.Strategy;

/* renamed from: im.getsocial.sdk.internal.c.g.cjrhisSQCL */
public interface cjrhisSQCL {

    /* renamed from: im.getsocial.sdk.internal.c.g.cjrhisSQCL$jjbQypPegg */
    public enum jjbQypPegg {
        OFF(0),
        ERROR(100),
        WARN(200),
        INFO(Strategy.TTL_SECONDS_DEFAULT),
        DEBUG(400),
        ALL(Strategy.TTL_SECONDS_INFINITE);
        
        private final int _value;

        private jjbQypPegg(int i) {
            this._value = i;
        }

        public final int value() {
            return this._value;
        }
    }

    /* renamed from: a */
    void mo4387a(String str);

    /* renamed from: a */
    void mo4388a(String str, Object... objArr);

    /* renamed from: a */
    void mo4389a(Throwable th);

    /* renamed from: b */
    void mo4390b(String str);

    /* renamed from: b */
    void mo4391b(String str, Object... objArr);

    /* renamed from: b */
    void mo4392b(Throwable th);

    /* renamed from: c */
    void mo4393c(String str);

    /* renamed from: c */
    void mo4394c(String str, Object... objArr);

    /* renamed from: c */
    void mo4395c(Throwable th);

    /* renamed from: d */
    void mo4396d(String str);
}
