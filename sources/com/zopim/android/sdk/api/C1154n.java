package com.zopim.android.sdk.api;

import android.os.AsyncTask;
import android.util.Log;
import android.util.Pair;
import java.io.File;
import java.net.URL;

/* renamed from: com.zopim.android.sdk.api.n */
final class C1154n extends AsyncTask<Pair<URL, File>, Void, File> {
    /* access modifiers changed from: private */

    /* renamed from: b */
    public static final String f696b = C1154n.class.getSimpleName();

    /* renamed from: a */
    C1161u<File> f697a;
    /* access modifiers changed from: private */

    /* renamed from: c */
    public File f698c;
    /* access modifiers changed from: private */

    /* renamed from: d */
    public ErrorResponse f699d;

    C1154n() {
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public File doInBackground(Pair<URL, File>... pairArr) {
        if (pairArr == null || pairArr.length == 0) {
            Log.w(f696b, "File - URL pair validation failed. Will not start file upload.");
            return null;
        }
        URL url = (URL) pairArr[0].first;
        File file = (File) pairArr[0].second;
        C1149j jVar = new C1149j();
        jVar.mo20653a(new C1155o(this));
        jVar.mo20654a(url, file);
        return this.f698c;
    }

    /* renamed from: a */
    public void mo20663a(C1161u<File> uVar) {
        this.f697a = uVar;
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public void onPostExecute(File file) {
        super.onPostExecute(file);
        if (this.f697a == null) {
            return;
        }
        if (this.f699d != null) {
            this.f697a.mo20643a(this.f699d);
        } else if (file != null) {
            this.f697a.mo20644a(file);
        }
    }
}
