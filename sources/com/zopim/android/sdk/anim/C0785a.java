package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.view.View;

/* renamed from: com.zopim.android.sdk.anim.a */
final class C0785a extends AnimatorListenerAdapter {
    /* renamed from: a */
    final /* synthetic */ View f605a;

    C0785a(View view) {
        this.f605a = view;
    }

    public void onAnimationEnd(Animator animator) {
        super.onAnimationEnd(animator);
        this.f605a.setVisibility(8);
    }

    public void onAnimationStart(Animator animator) {
        super.onAnimationStart(animator);
        this.f605a.setVisibility(0);
    }
}
