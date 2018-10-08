package com.zopim.android.sdk.api;

import com.zopim.android.sdk.api.HttpRequest.ProgressListener;

/* renamed from: com.zopim.android.sdk.api.r */
class C0815r implements ProgressListener {
    /* renamed from: a */
    int f664a = 0;
    /* renamed from: b */
    final /* synthetic */ C0813p f665b;

    C0815r(C0813p c0813p) {
        this.f665b = c0813p;
    }

    public void onProgressUpdate(int i) {
        if (i > this.f664a) {
            this.f664a = i;
            this.f665b.publishProgress(new Integer[]{Integer.valueOf(i)});
        }
    }
}
