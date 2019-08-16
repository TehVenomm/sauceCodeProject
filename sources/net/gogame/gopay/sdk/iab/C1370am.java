package net.gogame.gopay.sdk.iab;

/* renamed from: net.gogame.gopay.sdk.iab.am */
final class C1370am implements Runnable {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1066a;

    C1370am(PurchaseActivity purchaseActivity) {
        this.f1066a = purchaseActivity;
    }

    public final void run() {
        new C1382bd(this.f1066a).execute(new Void[0]);
    }
}
