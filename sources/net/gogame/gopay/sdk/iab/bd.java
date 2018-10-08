package net.gogame.gopay.sdk.iab;

import android.os.AsyncTask;
import net.gogame.gopay.sdk.C1062j;
import net.gogame.gopay.sdk.support.C1084m;
import net.gogame.gopay.sdk.support.C1089t;

final class bd extends AsyncTask {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1091a;

    bd(PurchaseActivity purchaseActivity) {
        this.f1091a = purchaseActivity;
    }

    /* renamed from: a */
    private static bu m841a() {
        try {
            return C1062j.m871b();
        } catch (Exception e) {
            return null;
        }
    }

    protected final /* synthetic */ Object doInBackground(Object[] objArr) {
        return m841a();
    }

    protected final /* synthetic */ void onPostExecute(Object obj) {
        bu buVar = (bu) obj;
        if (buVar != null) {
            try {
                float f = this.f1091a.f1041t.getFloat("_version_", 0.0f);
                float f2 = buVar.f1123a;
                if (!buVar.f1125c && buVar.f1124b != null && buVar.f1124b.length() > 0 && (f2 > f || !C1084m.m926a())) {
                    new C1089t(this.f1091a.getFilesDir().getPath(), new be(this, f2)).execute(new String[]{buVar.f1124b});
                    return;
                }
            } catch (Exception e) {
            }
        }
        this.f1091a.m796a(new bi(this.f1091a), false);
    }
}
