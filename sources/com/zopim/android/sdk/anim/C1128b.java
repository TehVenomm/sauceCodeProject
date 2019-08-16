package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.view.View;

/* renamed from: com.zopim.android.sdk.anim.b */
final class C1128b extends AnimatorListenerAdapter {

    /* renamed from: a */
    final /* synthetic */ View f650a;

    C1128b(View view) {
        this.f650a = view;
    }

    public void onAnimationStart(Animator animator) {
        super.onAnimationStart(animator);
        this.f650a.setVisibility(0);
    }
}
