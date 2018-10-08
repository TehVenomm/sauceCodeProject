package net.gogame.gopay.sdk.support;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.BitmapFactory.Options;
import android.os.AsyncTask;
import java.io.File;

/* renamed from: net.gogame.gopay.sdk.support.p */
final class C1403p extends AsyncTask {
    /* renamed from: a */
    final /* synthetic */ String f3633a;
    /* renamed from: b */
    final /* synthetic */ String f3634b;
    /* renamed from: c */
    final /* synthetic */ String f3635c;
    /* renamed from: d */
    final /* synthetic */ C1346q f3636d;

    C1403p(String str, String str2, String str3, C1346q c1346q) {
        this.f3633a = str;
        this.f3634b = str2;
        this.f3635c = str3;
        this.f3636d = c1346q;
    }

    protected final /* synthetic */ Object doInBackground(Object[] objArr) {
        Object obj = null;
        if (!isCancelled()) {
            String str = C1400m.f3624b + "/assets/" + this.f3633a;
            if (new File(str).exists()) {
                obj = BitmapFactory.decodeFile(str, new Options());
            }
            if (obj == null && this.f3634b != null && this.f3634b.length() > 0) {
                obj = C1400m.m3953b(this.f3634b, this.f3635c);
            }
            if (obj != null) {
                C1400m.f3628f.put(this.f3633a, obj);
            }
        }
        return obj;
    }

    protected final /* synthetic */ void onPostExecute(Object obj) {
        Bitmap bitmap = (Bitmap) obj;
        if (!isCancelled() && this.f3636d != null) {
            this.f3636d.mo4867a(bitmap);
        }
    }
}
