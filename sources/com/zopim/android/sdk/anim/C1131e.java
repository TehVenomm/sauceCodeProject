package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.view.View;

/* renamed from: com.zopim.android.sdk.anim.e */
final class C1131e extends AnimatorListenerAdapter {

    /* renamed from: a */
    final /* synthetic */ View f655a;

    C1131e(View view) {
        this.f655a = view;
    }

    public void onAnimationEnd(Animator animator) {
        super.onAnimationEnd(animator);
        this.f655a.setVisibility(4);
    }
}
