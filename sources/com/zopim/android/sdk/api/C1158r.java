package com.zopim.android.sdk.api;

import com.zopim.android.sdk.api.HttpRequest.ProgressListener;

/* renamed from: com.zopim.android.sdk.api.r */
class C1158r implements ProgressListener {

    /* renamed from: a */
    int f707a = 0;

    /* renamed from: b */
    final /* synthetic */ C1156p f708b;

    C1158r(C1156p pVar) {
        this.f708b = pVar;
    }

    public void onProgressUpdate(int i) {
        if (i > this.f707a) {
            this.f707a = i;
            this.f708b.publishProgress(new Integer[]{Integer.valueOf(i)});
        }
    }
}
