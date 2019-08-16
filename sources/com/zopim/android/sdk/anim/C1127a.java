package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.view.View;

/* renamed from: com.zopim.android.sdk.anim.a */
final class C1127a extends AnimatorListenerAdapter {

    /* renamed from: a */
    final /* synthetic */ View f649a;

    C1127a(View view) {
        this.f649a = view;
    }

    public void onAnimationEnd(Animator animator) {
        super.onAnimationEnd(animator);
        this.f649a.setVisibility(8);
    }

    public void onAnimationStart(Animator animator) {
        super.onAnimationStart(animator);
        this.f649a.setVisibility(0);
    }
}
