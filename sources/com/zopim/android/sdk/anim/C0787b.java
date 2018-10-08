package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.view.View;

/* renamed from: com.zopim.android.sdk.anim.b */
final class C0787b extends AnimatorListenerAdapter {
    /* renamed from: a */
    final /* synthetic */ View f606a;

    C0787b(View view) {
        this.f606a = view;
    }

    public void onAnimationStart(Animator animator) {
        super.onAnimationStart(animator);
        this.f606a.setVisibility(0);
    }
}
