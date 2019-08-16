package net.gogame.gopay.sdk;

import com.facebook.appevents.UserDataStore;

/* renamed from: net.gogame.gopay.sdk.q */
final class C1411q implements Runnable {

    /* renamed from: a */
    final /* synthetic */ C1363h f1136a;

    /* renamed from: b */
    final /* synthetic */ StoreActivity f1137b;

    C1411q(StoreActivity storeActivity, C1363h hVar) {
        this.f1137b = storeActivity;
        this.f1136a = hVar;
    }

    public final void run() {
        this.f1137b.f991d.mo21499a((String) this.f1136a.f1012d.get(UserDataStore.COUNTRY), this.f1136a.f1011c);
        this.f1137b.f992e.setData(this.f1136a);
    }
}
