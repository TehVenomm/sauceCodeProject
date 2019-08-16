package net.gogame.gopay.sdk.iab;

/* renamed from: net.gogame.gopay.sdk.iab.am */
final class C1370am implements Runnable {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1060a;

    C1370am(PurchaseActivity purchaseActivity) {
        this.f1060a = purchaseActivity;
    }

    public final void run() {
        new C1382bd(this.f1060a).execute(new Void[0]);
    }
}
