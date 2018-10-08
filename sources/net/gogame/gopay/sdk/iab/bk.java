package net.gogame.gopay.sdk.iab;

import android.os.AsyncTask;
import net.gogame.gopay.sdk.C1349g;
import net.gogame.gopay.sdk.C1378j;
import net.gogame.gopay.sdk.Country;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabResult;

final class bk extends AsyncTask {
    /* renamed from: a */
    IabResult f3491a;
    /* renamed from: b */
    final /* synthetic */ boolean f3492b;
    /* renamed from: c */
    final /* synthetic */ bq f3493c;
    /* renamed from: d */
    final /* synthetic */ PurchaseActivity f3494d;

    bk(PurchaseActivity purchaseActivity, boolean z, bq bqVar) {
        this.f3494d = purchaseActivity;
        this.f3492b = z;
        this.f3493c = bqVar;
    }

    /* renamed from: a */
    private C1349g m3872a() {
        try {
            return C1378j.m3889a(this.f3494d.f3416g, this.f3494d.f3415f, this.f3494d.f3414e, C1378j.m3883a(), !this.f3492b);
        } catch (IabException e) {
            this.f3491a = e.getResult();
            return null;
        }
    }

    protected final /* synthetic */ Object doInBackground(Object[] objArr) {
        return m3872a();
    }

    protected final /* synthetic */ void onPostExecute(Object obj) {
        C1349g c1349g = (C1349g) obj;
        super.onPostExecute(c1349g);
        if (c1349g != null && this.f3494d.m3822a(c1349g.f3367f)) {
            return;
        }
        if (this.f3494d.f3405M > this.f3494d.f3406N || c1349g == null || c1349g.f3364c == null || ((c1349g.f3364c.size() == 0 && this.f3492b) || (!this.f3492b && (c1349g.f3366e == null || c1349g.f3366e.size() == 0)))) {
            if (this.f3491a == null) {
                this.f3491a = new IabResult(-1002, "GetProductDetails Bad Response!");
            }
            this.f3494d.m3810a(this.f3491a.getResponse(), this.f3491a.getMessage());
        } else if (c1349g.f3364c.size() != 0 || this.f3492b) {
            this.f3494d.f3405M = 0;
            C1378j.m3893a(c1349g.f3362a);
            this.f3494d.f3431v.post(new bl(this, c1349g));
        } else {
            this.f3494d.f3405M = this.f3494d.f3405M + 1;
            C1378j.m3893a(((Country) c1349g.f3366e.get(0)).getCode());
            this.f3494d.m3821a(this.f3493c, false);
        }
    }
}
