package net.gogame.gopay.sdk.support;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.BitmapFactory.Options;
import android.os.AsyncTask;
import java.io.File;

/* renamed from: net.gogame.gopay.sdk.support.p */
final class C1651p extends AsyncTask {

    /* renamed from: a */
    final /* synthetic */ String f1303a;

    /* renamed from: b */
    final /* synthetic */ String f1304b;

    /* renamed from: c */
    final /* synthetic */ String f1305c;

    /* renamed from: d */
    final /* synthetic */ C1652q f1306d;

    C1651p(String str, String str2, String str3, C1652q qVar) {
        this.f1303a = str;
        this.f1304b = str2;
        this.f1305c = str3;
        this.f1306d = qVar;
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ Object doInBackground(Object[] objArr) {
        Bitmap bitmap = null;
        if (!isCancelled()) {
            String str = C1415m.f1167b + "/assets/" + this.f1303a;
            if (new File(str).exists()) {
                bitmap = BitmapFactory.decodeFile(str, new Options());
            }
            if (bitmap == null && this.f1304b != null && this.f1304b.length() > 0) {
                bitmap = C1415m.m925b(this.f1304b, this.f1305c);
            }
            if (bitmap != null) {
                C1415m.f1171f.put(this.f1303a, bitmap);
            }
        }
        return bitmap;
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ void onPostExecute(Object obj) {
        Bitmap bitmap = (Bitmap) obj;
        if (!isCancelled() && this.f1306d != null) {
            this.f1306d.mo22645a(bitmap);
        }
    }
}
