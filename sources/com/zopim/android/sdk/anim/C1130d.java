package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.view.View;

/* renamed from: com.zopim.android.sdk.anim.d */
final class C1130d extends AnimatorListenerAdapter {

    /* renamed from: a */
    final /* synthetic */ View f654a;

    C1130d(View view) {
        this.f654a = view;
    }

    public void onAnimationStart(Animator animator) {
        super.onAnimationStart(animator);
        this.f654a.setVisibility(0);
    }
}
