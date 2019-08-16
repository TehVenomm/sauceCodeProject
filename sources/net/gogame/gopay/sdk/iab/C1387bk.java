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
    IabResult f1090a;

    /* renamed from: b */
    final /* synthetic */ boolean f1091b;

    /* renamed from: c */
    final /* synthetic */ C1392bq f1092c;

    /* renamed from: d */
    final /* synthetic */ PurchaseActivity f1093d;

    C1387bk(PurchaseActivity purchaseActivity, boolean z, C1392bq bqVar) {
        this.f1093d = purchaseActivity;
        this.f1091b = z;
        this.f1092c = bqVar;
    }

    /* renamed from: a */
    private C1362g m849a() {
        try {
            return C1406j.m864a(this.f1093d.f1039g, this.f1093d.f1038f, this.f1093d.f1037e, C1406j.m858a(), !this.f1091b);
        } catch (IabException e) {
            this.f1090a = e.getResult();
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
        if (gVar != null && this.f1093d.m805a(gVar.f1005f)) {
            return;
        }
        if (this.f1093d.f1028M > this.f1093d.f1029N || gVar == null || gVar.f1002c == null || ((gVar.f1002c.size() == 0 && this.f1091b) || (!this.f1091b && (gVar.f1004e == null || gVar.f1004e.size() == 0)))) {
            if (this.f1090a == null) {
                this.f1090a = new IabResult(-1002, "GetProductDetails Bad Response!");
            }
            this.f1093d.m793a(this.f1090a.getResponse(), this.f1090a.getMessage());
        } else if (gVar.f1002c.size() != 0 || this.f1091b) {
            this.f1093d.f1028M = 0;
            C1406j.m868a(gVar.f1000a);
            this.f1093d.f1054v.post(new C1620bl(this, gVar));
        } else {
            this.f1093d.f1028M = this.f1093d.f1028M + 1;
            C1406j.m868a(((Country) gVar.f1004e.get(0)).getCode());
            this.f1093d.m804a(this.f1092c, false);
        }
    }
}
