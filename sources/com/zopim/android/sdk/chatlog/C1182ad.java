package com.zopim.android.sdk.chatlog;

import com.squareup.picasso.Callback;

/* renamed from: com.zopim.android.sdk.chatlog.ad */
class C1182ad implements Callback {

    /* renamed from: a */
    final /* synthetic */ C1180ab f815a;

    /* renamed from: b */
    final /* synthetic */ VisitorMessageHolder f816b;

    C1182ad(VisitorMessageHolder visitorMessageHolder, C1180ab abVar) {
        this.f816b = visitorMessageHolder;
        this.f815a = abVar;
    }

    public void onError() {
        this.f816b.f782g.setVisibility(4);
    }

    public void onSuccess() {
        this.f816b.m676a(this.f815a.f811c);
    }
}
