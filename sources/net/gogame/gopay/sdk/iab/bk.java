package net.gogame.gopay.sdk.iab;

import android.os.AsyncTask;
import net.gogame.gopay.sdk.C1033g;
import net.gogame.gopay.sdk.C1062j;
import net.gogame.gopay.sdk.Country;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabResult;

final class bk extends AsyncTask {
    /* renamed from: a */
    IabResult f1103a;
    /* renamed from: b */
    final /* synthetic */ boolean f1104b;
    /* renamed from: c */
    final /* synthetic */ bq f1105c;
    /* renamed from: d */
    final /* synthetic */ PurchaseActivity f1106d;

    bk(PurchaseActivity purchaseActivity, boolean z, bq bqVar) {
        this.f1106d = purchaseActivity;
        this.f1104b = z;
        this.f1105c = bqVar;
    }

    /* renamed from: a */
    private C1033g m847a() {
        try {
            return C1062j.m864a(this.f1106d.f1028g, this.f1106d.f1027f, this.f1106d.f1026e, C1062j.m858a(), !this.f1104b);
        } catch (IabException e) {
            this.f1103a = e.getResult();
            return null;
        }
    }

    protected final /* synthetic */ Object doInBackground(Object[] objArr) {
        return m847a();
    }

    protected final /* synthetic */ void onPostExecute(Object obj) {
        C1033g c1033g = (C1033g) obj;
        super.onPostExecute(c1033g);
        if (c1033g != null && this.f1106d.m797a(c1033g.f979f)) {
            return;
        }
        if (this.f1106d.f1017M > this.f1106d.f1018N || c1033g == null || c1033g.f976c == null || ((c1033g.f976c.size() == 0 && this.f1104b) || (!this.f1104b && (c1033g.f978e == null || c1033g.f978e.size() == 0)))) {
            if (this.f1103a == null) {
                this.f1103a = new IabResult(-1002, "GetProductDetails Bad Response!");
            }
            this.f1106d.m785a(this.f1103a.getResponse(), this.f1103a.getMessage());
        } else if (c1033g.f976c.size() != 0 || this.f1104b) {
            this.f1106d.f1017M = 0;
            C1062j.m868a(c1033g.f974a);
            this.f1106d.f1043v.post(new bl(this, c1033g));
        } else {
            this.f1106d.f1017M = this.f1106d.f1017M + 1;
            C1062j.m868a(((Country) c1033g.f978e.get(0)).getCode());
            this.f1106d.m796a(this.f1105c, false);
        }
    }
}
