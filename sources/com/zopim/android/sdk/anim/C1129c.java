package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.view.View;

/* renamed from: com.zopim.android.sdk.anim.c */
final class C1129c extends AnimatorListenerAdapter {

    /* renamed from: a */
    final /* synthetic */ View f651a;

    /* renamed from: b */
    final /* synthetic */ float f652b;

    /* renamed from: c */
    final /* synthetic */ float f653c;

    C1129c(View view, float f, float f2) {
        this.f651a = view;
        this.f652b = f;
        this.f653c = f2;
    }

    public void onAnimationEnd(Animator animator) {
        super.onAnimationEnd(animator);
        this.f651a.setVisibility(4);
        this.f651a.setTranslationX(this.f652b);
        this.f651a.setTranslationY(this.f653c);
    }

    public void onAnimationStart(Animator animator) {
        super.onAnimationStart(animator);
        this.f651a.setVisibility(0);
    }
}
