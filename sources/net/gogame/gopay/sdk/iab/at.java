package net.gogame.gopay.sdk.iab;

import android.os.AsyncTask;
import net.gogame.gopay.sdk.C1378j;
import net.gogame.gopay.sdk.support.C1400m;
import org.onepf.oms.appstore.googleUtils.IabException;

final class at extends AsyncTask {
    /* renamed from: a */
    final /* synthetic */ C1341a f3461a;
    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f3462b;

    at(PurchaseActivity purchaseActivity, C1341a c1341a) {
        this.f3462b = purchaseActivity;
        this.f3461a = c1341a;
    }

    /* renamed from: a */
    private Void m3864a() {
        try {
            if (this.f3462b.f3427r == null || this.f3462b.f3419j == null || this.f3462b.f3420k == null) {
                this.f3462b.f3431v.post(new av(this, C1378j.m3891a(this.f3462b.f3416g, this.f3462b.f3415f, this.f3462b.f3414e, this.f3461a.getId(), C1378j.m3883a(), this.f3462b.f3417h)));
                return null;
            }
            this.f3462b.f3431v.post(new au(this, C1378j.m3892a(this.f3462b.f3416g, this.f3462b.f3415f, this.f3462b.f3427r, this.f3462b.f3419j, this.f3462b.f3420k, this.f3461a.getId(), C1378j.m3883a(), this.f3462b.f3417h)));
            return null;
        } catch (IabException e) {
            if (!(this.f3462b.f3402J || this.f3462b.f3413d)) {
                this.f3462b.m3827b();
                this.f3462b.f3412c = true;
                this.f3462b.f3401I = false;
                this.f3462b.f3400H.setError(404, "This Payment Method is not working at the moment. Please try another Payment Method.");
                this.f3462b.f3400H.setFailedUrl(null);
                String j = C1400m.m3964j();
                if (j != null) {
                    this.f3462b.f3431v.post(new aw(this, j));
                }
            }
        }
    }

    protected final /* synthetic */ Object doInBackground(Object[] objArr) {
        return m3864a();
    }

    protected final /* synthetic */ void onPostExecute(Object obj) {
        this.f3462b.f3430u = null;
    }

    protected final void onPreExecute() {
        this.f3462b.f3431v.removeCallbacks(this.f3462b.f3433x);
    }
}
