package com.zopim.android.sdk.chatlog;

import com.squareup.picasso.Callback;

class ad implements Callback {
    /* renamed from: a */
    final /* synthetic */ ab f771a;
    /* renamed from: b */
    final /* synthetic */ VisitorMessageHolder f772b;

    ad(VisitorMessageHolder visitorMessageHolder, ab abVar) {
        this.f772b = visitorMessageHolder;
        this.f771a = abVar;
    }

    public void onError() {
        this.f772b.f738g.setVisibility(4);
    }

    public void onSuccess() {
        this.f772b.m663a(this.f771a.f767c);
    }
}
