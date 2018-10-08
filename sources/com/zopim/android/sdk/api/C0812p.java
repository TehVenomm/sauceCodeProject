package com.zopim.android.sdk.api;

import android.os.AsyncTask;
import android.util.Log;
import android.util.Pair;
import com.zopim.android.sdk.api.HttpRequest.ProgressListener;
import java.io.File;
import java.net.URL;

/* renamed from: com.zopim.android.sdk.api.p */
final class C0812p extends AsyncTask<Pair<File, URL>, Integer, Void> {
    /* renamed from: c */
    private static final String f658c = C0812p.class.getSimpleName();
    /* renamed from: a */
    ProgressListener f659a;
    /* renamed from: b */
    C0799u<Void> f660b;
    /* renamed from: d */
    private ErrorResponse f661d;
    /* renamed from: e */
    private boolean f662e;

    C0812p() {
    }

    /* renamed from: a */
    protected Void m615a(Pair<File, URL>... pairArr) {
        if (pairArr == null || pairArr.length == 0) {
            Log.w(f658c, "File - URL pair validation failed. Will not start file upload.");
        } else {
            File file = (File) pairArr[0].first;
            URL url = (URL) pairArr[0].second;
            C0815s c0815s = new C0815s();
            c0815s.m625a(new C0813q(this));
            c0815s.m624a(new C0814r(this));
            c0815s.m626a(file, url);
        }
        return null;
    }

    /* renamed from: a */
    public void m616a(C0799u<Void> c0799u) {
        this.f660b = c0799u;
    }

    /* renamed from: a */
    protected void m617a(Void voidR) {
        super.onPostExecute(voidR);
        if (this.f660b == null) {
            return;
        }
        if (this.f661d != null) {
            this.f660b.m576b(this.f661d);
        } else if (this.f662e) {
            this.f660b.m577b(null);
        }
    }

    /* renamed from: a */
    protected void m618a(Integer... numArr) {
        super.onProgressUpdate(numArr);
        if (this.f659a != null) {
            this.f659a.onProgressUpdate(numArr[0].intValue());
        }
    }

    protected /* synthetic */ Object doInBackground(Object[] objArr) {
        return m615a((Pair[]) objArr);
    }

    protected /* synthetic */ void onPostExecute(Object obj) {
        m617a((Void) obj);
    }

    protected /* synthetic */ void onProgressUpdate(Object[] objArr) {
        m618a((Integer[]) objArr);
    }
}
