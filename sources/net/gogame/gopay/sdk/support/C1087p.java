package net.gogame.gopay.sdk.support;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.BitmapFactory.Options;
import android.os.AsyncTask;
import java.io.File;

/* renamed from: net.gogame.gopay.sdk.support.p */
final class C1087p extends AsyncTask {
    /* renamed from: a */
    final /* synthetic */ String f1245a;
    /* renamed from: b */
    final /* synthetic */ String f1246b;
    /* renamed from: c */
    final /* synthetic */ String f1247c;
    /* renamed from: d */
    final /* synthetic */ C1030q f1248d;

    C1087p(String str, String str2, String str3, C1030q c1030q) {
        this.f1245a = str;
        this.f1246b = str2;
        this.f1247c = str3;
        this.f1248d = c1030q;
    }

    protected final /* synthetic */ Object doInBackground(Object[] objArr) {
        Object obj = null;
        if (!isCancelled()) {
            String str = C1084m.f1236b + "/assets/" + this.f1245a;
            if (new File(str).exists()) {
                obj = BitmapFactory.decodeFile(str, new Options());
            }
            if (obj == null && this.f1246b != null && this.f1246b.length() > 0) {
                obj = C1084m.m928b(this.f1246b, this.f1247c);
            }
            if (obj != null) {
                C1084m.f1240f.put(this.f1245a, obj);
            }
        }
        return obj;
    }

    protected final /* synthetic */ void onPostExecute(Object obj) {
        Bitmap bitmap = (Bitmap) obj;
        if (!isCancelled() && this.f1248d != null) {
            this.f1248d.mo4403a(bitmap);
        }
    }
}
