package net.gogame.gopay.sdk.iab;

import android.os.AsyncTask;
import net.gogame.gopay.sdk.C1406j;
import net.gogame.gopay.sdk.support.C1415m;
import org.onepf.oms.appstore.googleUtils.IabException;

/* renamed from: net.gogame.gopay.sdk.iab.at */
final class C1376at extends AsyncTask {

    /* renamed from: a */
    final /* synthetic */ C1365a f1072a;

    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1073b;

    C1376at(PurchaseActivity purchaseActivity, C1365a aVar) {
        this.f1073b = purchaseActivity;
        this.f1072a = aVar;
    }

    /* renamed from: a */
    private Void m845a() {
        try {
            if (this.f1073b.f1050r == null || this.f1073b.f1042j == null || this.f1073b.f1043k == null) {
                this.f1073b.f1054v.post(new C1614av(this, C1406j.m866a(this.f1073b.f1039g, this.f1073b.f1038f, this.f1073b.f1037e, this.f1072a.getId(), C1406j.m858a(), this.f1073b.f1040h)));
            } else {
                this.f1073b.f1054v.post(new C1613au(this, C1406j.m867a(this.f1073b.f1039g, this.f1073b.f1038f, this.f1073b.f1050r, this.f1073b.f1042j, this.f1073b.f1043k, this.f1072a.getId(), C1406j.m858a(), this.f1073b.f1040h)));
            }
        } catch (IabException e) {
            if (!this.f1073b.f1025J && !this.f1073b.f1036d) {
                this.f1073b.m810b();
                this.f1073b.f1035c = true;
                this.f1073b.f1024I = false;
                this.f1073b.f1023H.setError(404, "This Payment Method is not working at the moment. Please try another Payment Method.");
                this.f1073b.f1023H.setFailedUrl(null);
                String j = C1415m.m936j();
                if (j != null) {
                    this.f1073b.f1054v.post(new C1615aw(this, j));
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
        this.f1073b.f1053u = null;
    }

    /* access modifiers changed from: protected */
    public final void onPreExecute() {
        this.f1073b.f1054v.removeCallbacks(this.f1073b.f1056x);
    }
}
