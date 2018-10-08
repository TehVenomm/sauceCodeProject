package com.zopim.android.sdk.widget;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;

/* renamed from: com.zopim.android.sdk.widget.a */
class C0903a extends AnimatorListenerAdapter {
    /* renamed from: a */
    final /* synthetic */ ChatWidgetService f929a;

    C0903a(ChatWidgetService chatWidgetService) {
        this.f929a = chatWidgetService;
    }

    public void onAnimationEnd(Animator animator) {
        super.onAnimationEnd(animator);
        this.f929a.mTypingIndicatorView.setScaleX(1.0f);
        this.f929a.mTypingIndicatorView.setScaleY(1.0f);
    }
}
