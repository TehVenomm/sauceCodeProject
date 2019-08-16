package com.zopim.android.sdk.api;

import android.os.AsyncTask;
import android.util.Log;
import android.util.Pair;
import com.zopim.android.sdk.api.HttpRequest.ProgressListener;
import java.io.File;
import java.net.URL;

/* renamed from: com.zopim.android.sdk.api.p */
final class C1156p extends AsyncTask<Pair<File, URL>, Integer, Void> {
    /* access modifiers changed from: private */

    /* renamed from: c */
    public static final String f701c = C1156p.class.getSimpleName();

    /* renamed from: a */
    ProgressListener f702a;

    /* renamed from: b */
    C1161u<Void> f703b;
    /* access modifiers changed from: private */

    /* renamed from: d */
    public ErrorResponse f704d;
    /* access modifiers changed from: private */

    /* renamed from: e */
    public boolean f705e;

    C1156p() {
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public Void doInBackground(Pair<File, URL>... pairArr) {
        if (pairArr == null || pairArr.length == 0) {
            Log.w(f701c, "File - URL pair validation failed. Will not start file upload.");
        } else {
            File file = (File) pairArr[0].first;
            URL url = (URL) pairArr[0].second;
            C1159s sVar = new C1159s();
            sVar.mo20677a((C1161u<Void>) new C1157q<Void>(this));
            sVar.mo20676a((ProgressListener) new C1158r(this));
            sVar.mo20678a(file, url);
        }
        return null;
    }

    /* renamed from: a */
    public void mo20669a(C1161u<Void> uVar) {
        this.f703b = uVar;
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public void onPostExecute(Void voidR) {
        super.onPostExecute(voidR);
        if (this.f703b == null) {
            return;
        }
        if (this.f704d != null) {
            this.f703b.mo20679b(this.f704d);
        } else if (this.f705e) {
            this.f703b.mo20680b(null);
        }
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public void onProgressUpdate(Integer... numArr) {
        super.onProgressUpdate(numArr);
        if (this.f702a != null) {
            this.f702a.onProgressUpdate(numArr[0].intValue());
        }
    }
}
