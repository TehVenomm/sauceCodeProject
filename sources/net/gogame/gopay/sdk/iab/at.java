package net.gogame.gopay.sdk.iab;

import android.os.AsyncTask;
import net.gogame.gopay.sdk.C1062j;
import net.gogame.gopay.sdk.support.C1084m;
import org.onepf.oms.appstore.googleUtils.IabException;

final class at extends AsyncTask {
    /* renamed from: a */
    final /* synthetic */ C1025a f1073a;
    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1074b;

    at(PurchaseActivity purchaseActivity, C1025a c1025a) {
        this.f1074b = purchaseActivity;
        this.f1073a = c1025a;
    }

    /* renamed from: a */
    private Void m839a() {
        try {
            if (this.f1074b.f1039r == null || this.f1074b.f1031j == null || this.f1074b.f1032k == null) {
                this.f1074b.f1043v.post(new av(this, C1062j.m866a(this.f1074b.f1028g, this.f1074b.f1027f, this.f1074b.f1026e, this.f1073a.getId(), C1062j.m858a(), this.f1074b.f1029h)));
                return null;
            }
            this.f1074b.f1043v.post(new au(this, C1062j.m867a(this.f1074b.f1028g, this.f1074b.f1027f, this.f1074b.f1039r, this.f1074b.f1031j, this.f1074b.f1032k, this.f1073a.getId(), C1062j.m858a(), this.f1074b.f1029h)));
            return null;
        } catch (IabException e) {
            if (!(this.f1074b.f1014J || this.f1074b.f1025d)) {
                this.f1074b.m802b();
                this.f1074b.f1024c = true;
                this.f1074b.f1013I = false;
                this.f1074b.f1012H.setError(404, "This Payment Method is not working at the moment. Please try another Payment Method.");
                this.f1074b.f1012H.setFailedUrl(null);
                String j = C1084m.m939j();
                if (j != null) {
                    this.f1074b.f1043v.post(new aw(this, j));
                }
            }
        }
    }

    protected final /* synthetic */ Object doInBackground(Object[] objArr) {
        return m839a();
    }

    protected final /* synthetic */ void onPostExecute(Object obj) {
        this.f1074b.f1042u = null;
    }

    protected final void onPreExecute() {
        this.f1074b.f1043v.removeCallbacks(this.f1074b.f1045x);
    }
}
