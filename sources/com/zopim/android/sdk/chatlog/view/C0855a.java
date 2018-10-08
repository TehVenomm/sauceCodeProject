package com.zopim.android.sdk.chatlog.view;

import android.graphics.drawable.AnimationDrawable;

/* renamed from: com.zopim.android.sdk.chatlog.view.a */
class C0855a implements Runnable {
    /* renamed from: a */
    final /* synthetic */ AnimationDrawable f836a;
    /* renamed from: b */
    final /* synthetic */ TypingIndicatorView f837b;

    C0855a(TypingIndicatorView typingIndicatorView, AnimationDrawable animationDrawable) {
        this.f837b = typingIndicatorView;
        this.f836a = animationDrawable;
    }

    public void run() {
        this.f836a.start();
    }
}
