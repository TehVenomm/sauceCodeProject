package com.zopim.android.sdk.widget;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;

/* renamed from: com.zopim.android.sdk.widget.a */
class C1272a extends AnimatorListenerAdapter {

    /* renamed from: a */
    final /* synthetic */ ChatWidgetService f973a;

    C1272a(ChatWidgetService chatWidgetService) {
        this.f973a = chatWidgetService;
    }

    public void onAnimationEnd(Animator animator) {
        super.onAnimationEnd(animator);
        this.f973a.mTypingIndicatorView.setScaleX(1.0f);
        this.f973a.mTypingIndicatorView.setScaleY(1.0f);
    }
}
