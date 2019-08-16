package net.gogame.gopay.sdk.iab;

import android.os.AsyncTask;
import net.gogame.gopay.sdk.C1362g;
import net.gogame.gopay.sdk.C1406j;
import net.gogame.gopay.sdk.Country;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabResult;

/* renamed from: net.gogame.gopay.sdk.iab.bk */
final class C1387bk extends AsyncTask {

    /* renamed from: a */
    IabResult f1084a;

    /* renamed from: b */
    final /* synthetic */ boolean f1085b;

    /* renamed from: c */
    final /* synthetic */ C1392bq f1086c;

    /* renamed from: d */
    final /* synthetic */ PurchaseActivity f1087d;

    C1387bk(PurchaseActivity purchaseActivity, boolean z, C1392bq bqVar) {
        this.f1087d = purchaseActivity;
        this.f1085b = z;
        this.f1086c = bqVar;
    }

    /* renamed from: a */
    private C1362g m849a() {
        try {
            return C1406j.m864a(this.f1087d.f1033g, this.f1087d.f1032f, this.f1087d.f1031e, C1406j.m858a(), !this.f1085b);
        } catch (IabException e) {
            this.f1084a = e.getResult();
            return null;
        }
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ Object doInBackground(Object[] objArr) {
        return m849a();
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ void onPostExecute(Object obj) {
        C1362g gVar = (C1362g) obj;
        super.onPostExecute(gVar);
        if (gVar != null && this.f1087d.m805a(gVar.f999f)) {
            return;
        }
        if (this.f1087d.f1022M > this.f1087d.f1023N || gVar == null || gVar.f996c == null || ((gVar.f996c.size() == 0 && this.f1085b) || (!this.f1085b && (gVar.f998e == null || gVar.f998e.size() == 0)))) {
            if (this.f1084a == null) {
                this.f1084a = new IabResult(-1002, "GetProductDetails Bad Response!");
            }
            this.f1087d.m793a(this.f1084a.getResponse(), this.f1084a.getMessage());
        } else if (gVar.f996c.size() != 0 || this.f1085b) {
            this.f1087d.f1022M = 0;
            C1406j.m868a(gVar.f994a);
            this.f1087d.f1048v.post(new C1620bl(this, gVar));
        } else {
            this.f1087d.f1022M = this.f1087d.f1022M + 1;
            C1406j.m868a(((Country) gVar.f998e.get(0)).getCode());
            this.f1087d.m804a(this.f1086c, false);
        }
    }
}
