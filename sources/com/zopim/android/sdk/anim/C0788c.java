package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.view.View;

/* renamed from: com.zopim.android.sdk.anim.c */
final class C0788c extends AnimatorListenerAdapter {
    /* renamed from: a */
    final /* synthetic */ View f607a;
    /* renamed from: b */
    final /* synthetic */ float f608b;
    /* renamed from: c */
    final /* synthetic */ float f609c;

    C0788c(View view, float f, float f2) {
        this.f607a = view;
        this.f608b = f;
        this.f609c = f2;
    }

    public void onAnimationEnd(Animator animator) {
        super.onAnimationEnd(animator);
        this.f607a.setVisibility(4);
        this.f607a.setTranslationX(this.f608b);
        this.f607a.setTranslationY(this.f609c);
    }

    public void onAnimationStart(Animator animator) {
        super.onAnimationStart(animator);
        this.f607a.setVisibility(0);
    }
}
