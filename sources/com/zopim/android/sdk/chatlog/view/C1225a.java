package com.zopim.android.sdk.chatlog.view;

import android.graphics.drawable.AnimationDrawable;

/* renamed from: com.zopim.android.sdk.chatlog.view.a */
class C1225a implements Runnable {

    /* renamed from: a */
    final /* synthetic */ AnimationDrawable f880a;

    /* renamed from: b */
    final /* synthetic */ TypingIndicatorView f881b;

    C1225a(TypingIndicatorView typingIndicatorView, AnimationDrawable animationDrawable) {
        this.f881b = typingIndicatorView;
        this.f880a = animationDrawable;
    }

    public void run() {
        this.f880a.start();
    }
}
