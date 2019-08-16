package net.gogame.gopay.sdk.iab;

import android.os.AsyncTask;
import net.gogame.gopay.sdk.C1406j;
import net.gogame.gopay.sdk.support.C1415m;
import org.onepf.oms.appstore.googleUtils.IabException;

/* renamed from: net.gogame.gopay.sdk.iab.at */
final class C1376at extends AsyncTask {

    /* renamed from: a */
    final /* synthetic */ C1365a f1066a;

    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1067b;

    C1376at(PurchaseActivity purchaseActivity, C1365a aVar) {
        this.f1067b = purchaseActivity;
        this.f1066a = aVar;
    }

    /* renamed from: a */
    private Void m845a() {
        try {
            if (this.f1067b.f1044r == null || this.f1067b.f1036j == null || this.f1067b.f1037k == null) {
                this.f1067b.f1048v.post(new C1614av(this, C1406j.m866a(this.f1067b.f1033g, this.f1067b.f1032f, this.f1067b.f1031e, this.f1066a.getId(), C1406j.m858a(), this.f1067b.f1034h)));
            } else {
                this.f1067b.f1048v.post(new C1613au(this, C1406j.m867a(this.f1067b.f1033g, this.f1067b.f1032f, this.f1067b.f1044r, this.f1067b.f1036j, this.f1067b.f1037k, this.f1066a.getId(), C1406j.m858a(), this.f1067b.f1034h)));
            }
        } catch (IabException e) {
            if (!this.f1067b.f1019J && !this.f1067b.f1030d) {
                this.f1067b.m810b();
                this.f1067b.f1029c = true;
                this.f1067b.f1018I = false;
                this.f1067b.f1017H.setError(404, "This Payment Method is not working at the moment. Please try another Payment Method.");
                this.f1067b.f1017H.setFailedUrl(null);
                String j = C1415m.m936j();
                if (j != null) {
                    this.f1067b.f1048v.post(new C1615aw(this, j));
                }
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ Object doInBackground(Object[] objArr) {
        return m845a();
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ void onPostExecute(Object obj) {
        this.f1067b.f1047u = null;
    }

    /* access modifiers changed from: protected */
    public final void onPreExecute() {
        this.f1067b.f1048v.removeCallbacks(this.f1067b.f1050x);
    }
}
