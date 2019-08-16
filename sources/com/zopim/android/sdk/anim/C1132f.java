package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.view.View;

/* renamed from: com.zopim.android.sdk.anim.f */
final class C1132f extends AnimatorListenerAdapter {

    /* renamed from: a */
    final /* synthetic */ View f656a;

    C1132f(View view) {
        this.f656a = view;
    }

    public void onAnimationStart(Animator animator) {
        super.onAnimationStart(animator);
        this.f656a.setVisibility(0);
    }
}
