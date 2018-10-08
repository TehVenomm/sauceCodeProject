package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.view.View;

/* renamed from: com.zopim.android.sdk.anim.f */
final class C0790f extends AnimatorListenerAdapter {
    /* renamed from: a */
    final /* synthetic */ View f612a;

    C0790f(View view) {
        this.f612a = view;
    }

    public void onAnimationStart(Animator animator) {
        super.onAnimationStart(animator);
        this.f612a.setVisibility(0);
    }
}
