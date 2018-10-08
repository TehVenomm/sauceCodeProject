package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.view.View;

/* renamed from: com.zopim.android.sdk.anim.e */
final class C0790e extends AnimatorListenerAdapter {
    /* renamed from: a */
    final /* synthetic */ View f611a;

    C0790e(View view) {
        this.f611a = view;
    }

    public void onAnimationEnd(Animator animator) {
        super.onAnimationEnd(animator);
        this.f611a.setVisibility(4);
    }
}
