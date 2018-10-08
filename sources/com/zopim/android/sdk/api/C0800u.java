package com.zopim.android.sdk.api;

/* renamed from: com.zopim.android.sdk.api.u */
abstract class C0800u<T> {
    /* renamed from: a */
    private boolean f629a = true;

    C0800u() {
    }

    /* renamed from: a */
    public abstract void mo4226a(ErrorResponse errorResponse);

    /* renamed from: a */
    public abstract void mo4227a(T t);

    /* renamed from: b */
    void m576b(ErrorResponse errorResponse) {
        if (this.f629a) {
            mo4226a(errorResponse);
        }
    }

    /* renamed from: b */
    void m577b(T t) {
        if (this.f629a) {
            mo4227a((Object) t);
        }
    }
}
