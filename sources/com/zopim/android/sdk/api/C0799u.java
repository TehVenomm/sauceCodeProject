package com.zopim.android.sdk.api;

/* renamed from: com.zopim.android.sdk.api.u */
abstract class C0799u<T> {
    /* renamed from: a */
    private boolean f629a = true;

    C0799u() {
    }

    /* renamed from: a */
    public abstract void mo4228a(ErrorResponse errorResponse);

    /* renamed from: a */
    public abstract void mo4229a(T t);

    /* renamed from: b */
    void m576b(ErrorResponse errorResponse) {
        if (this.f629a) {
            mo4228a(errorResponse);
        }
    }

    /* renamed from: b */
    void m577b(T t) {
        if (this.f629a) {
            mo4229a((Object) t);
        }
    }
}
