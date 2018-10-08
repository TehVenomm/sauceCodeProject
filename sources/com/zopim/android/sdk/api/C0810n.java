package com.zopim.android.sdk.api;

import android.os.AsyncTask;
import android.util.Log;
import android.util.Pair;
import java.io.File;
import java.net.URL;

/* renamed from: com.zopim.android.sdk.api.n */
final class C0810n extends AsyncTask<Pair<URL, File>, Void, File> {
    /* renamed from: b */
    private static final String f653b = C0810n.class.getSimpleName();
    /* renamed from: a */
    C0799u<File> f654a;
    /* renamed from: c */
    private File f655c;
    /* renamed from: d */
    private ErrorResponse f656d;

    C0810n() {
    }

    /* renamed from: a */
    protected File m605a(Pair<URL, File>... pairArr) {
        if (pairArr == null || pairArr.length == 0) {
            Log.w(f653b, "File - URL pair validation failed. Will not start file upload.");
            return null;
        }
        URL url = (URL) pairArr[0].first;
        File file = (File) pairArr[0].second;
        C0805j c0805j = new C0805j();
        c0805j.m587a(new C0811o(this));
        c0805j.m588a(url, file);
        return this.f655c;
    }

    /* renamed from: a */
    public void m606a(C0799u<File> c0799u) {
        this.f654a = c0799u;
    }

    /* renamed from: a */
    protected void m607a(File file) {
        super.onPostExecute(file);
        if (this.f654a == null) {
            return;
        }
        if (this.f656d != null) {
            this.f654a.mo4228a(this.f656d);
        } else if (file != null) {
            this.f654a.mo4229a((Object) file);
        }
    }

    protected /* synthetic */ Object doInBackground(Object[] objArr) {
        return m605a((Pair[]) objArr);
    }

    protected /* synthetic */ void onPostExecute(Object obj) {
        m607a((File) obj);
    }
}
