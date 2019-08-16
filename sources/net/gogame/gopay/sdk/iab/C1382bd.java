package net.gogame.gopay.sdk.iab;

import android.os.AsyncTask;
import net.gogame.gopay.sdk.C1406j;
import net.gogame.gopay.sdk.support.C1415m;
import net.gogame.gopay.sdk.support.C1654t;

/* renamed from: net.gogame.gopay.sdk.iab.bd */
final class C1382bd extends AsyncTask {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1082a;

    C1382bd(PurchaseActivity purchaseActivity) {
        this.f1082a = purchaseActivity;
    }

    /* renamed from: a */
    private static C1622bu m847a() {
        try {
            return C1406j.m871b();
        } catch (Exception e) {
            return null;
        }
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ Object doInBackground(Object[] objArr) {
        return m847a();
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ void onPostExecute(Object obj) {
        C1622bu buVar = (C1622bu) obj;
        if (buVar != null) {
            try {
                float f = this.f1082a.f1052t.getFloat("_version_", 0.0f);
                float f2 = buVar.f1269a;
                if (!buVar.f1271c && buVar.f1270b != null && buVar.f1270b.length() > 0 && (f2 > f || !C1415m.m923a())) {
                    new C1654t(this.f1082a.getFilesDir().getPath(), new C1618be(this, f2)).execute(new String[]{buVar.f1270b});
                    return;
                }
            } catch (Exception e) {
            }
        }
        this.f1082a.m804a((C1392bq) new C1386bi(this.f1082a), false);
    }
}
