package net.gogame.gopay.sdk.support;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.BitmapFactory.Options;
import android.os.AsyncTask;
import java.io.File;

/* renamed from: net.gogame.gopay.sdk.support.p */
final class C1651p extends AsyncTask {

    /* renamed from: a */
    final /* synthetic */ String f1315a;

    /* renamed from: b */
    final /* synthetic */ String f1316b;

    /* renamed from: c */
    final /* synthetic */ String f1317c;

    /* renamed from: d */
    final /* synthetic */ C1652q f1318d;

    C1651p(String str, String str2, String str3, C1652q qVar) {
        this.f1315a = str;
        this.f1316b = str2;
        this.f1317c = str3;
        this.f1318d = qVar;
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ Object doInBackground(Object[] objArr) {
        Bitmap bitmap = null;
        if (!isCancelled()) {
            String str = C1415m.f1173b + "/assets/" + this.f1315a;
            if (new File(str).exists()) {
                bitmap = BitmapFactory.decodeFile(str, new Options());
            }
            if (bitmap == null && this.f1316b != null && this.f1316b.length() > 0) {
                bitmap = C1415m.m925b(this.f1316b, this.f1317c);
            }
            if (bitmap != null) {
                C1415m.f1177f.put(this.f1315a, bitmap);
            }
        }
        return bitmap;
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ void onPostExecute(Object obj) {
        Bitmap bitmap = (Bitmap) obj;
        if (!isCancelled() && this.f1318d != null) {
            this.f1318d.mo22645a(bitmap);
        }
    }
}
