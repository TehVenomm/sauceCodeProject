package net.gogame.gopay.sdk;

import com.facebook.appevents.UserDataStore;

/* renamed from: net.gogame.gopay.sdk.q */
final class C1411q implements Runnable {

    /* renamed from: a */
    final /* synthetic */ C1363h f1130a;

    /* renamed from: b */
    final /* synthetic */ StoreActivity f1131b;

    C1411q(StoreActivity storeActivity, C1363h hVar) {
        this.f1131b = storeActivity;
        this.f1130a = hVar;
    }

    public final void run() {
        this.f1131b.f985d.mo21499a((String) this.f1130a.f1006d.get(UserDataStore.COUNTRY), this.f1130a.f1005c);
        this.f1131b.f986e.setData(this.f1130a);
    }
}
