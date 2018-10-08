package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.view.View;

/* renamed from: com.zopim.android.sdk.anim.d */
final class C0789d extends AnimatorListenerAdapter {
    /* renamed from: a */
    final /* synthetic */ View f610a;

    C0789d(View view) {
        this.f610a = view;
    }

    public void onAnimationStart(Animator animator) {
        super.onAnimationStart(animator);
        this.f610a.setVisibility(0);
    }
}
