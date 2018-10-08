package net.gogame.gopay.sdk.iab;

import android.os.AsyncTask;
import net.gogame.gopay.sdk.C1378j;
import net.gogame.gopay.sdk.support.C1400m;
import net.gogame.gopay.sdk.support.C1405t;

final class bd extends AsyncTask {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3479a;

    bd(PurchaseActivity purchaseActivity) {
        this.f3479a = purchaseActivity;
    }

    /* renamed from: a */
    private static bu m3866a() {
        try {
            return C1378j.m3896b();
        } catch (Exception e) {
            return null;
        }
    }

    protected final /* synthetic */ Object doInBackground(Object[] objArr) {
        return m3866a();
    }

    protected final /* synthetic */ void onPostExecute(Object obj) {
        bu buVar = (bu) obj;
        if (buVar != null) {
            try {
                float f = this.f3479a.f3429t.getFloat("_version_", 0.0f);
                float f2 = buVar.f3511a;
                if (!buVar.f3513c && buVar.f3512b != null && buVar.f3512b.length() > 0 && (f2 > f || !C1400m.m3951a())) {
                    new C1405t(this.f3479a.getFilesDir().getPath(), new be(this, f2)).execute(new String[]{buVar.f3512b});
                    return;
                }
            } catch (Exception e) {
            }
        }
        this.f3479a.m3821a(new bi(this.f3479a), false);
    }
}
