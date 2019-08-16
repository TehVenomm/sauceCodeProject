package com.zopim.android.sdk.api;

/* renamed from: com.zopim.android.sdk.api.u */
abstract class C1161u<T> {

    /* renamed from: a */
    private boolean f715a = true;

    C1161u() {
    }

    /* renamed from: a */
    public abstract void mo20643a(ErrorResponse errorResponse);

    /* renamed from: a */
    public abstract void mo20644a(T t);

    /* access modifiers changed from: 0000 */
    /* renamed from: b */
    public void mo20679b(ErrorResponse errorResponse) {
        if (this.f715a) {
            mo20643a(errorResponse);
        }
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: b */
    public void mo20680b(T t) {
        if (this.f715a) {
            mo20644a(t);
        }
    }
}
