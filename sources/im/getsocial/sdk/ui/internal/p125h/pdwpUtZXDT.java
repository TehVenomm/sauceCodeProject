package im.getsocial.sdk.ui.internal.p125h;

import android.animation.Animator;
import android.animation.Animator.AnimatorListener;
import android.animation.AnimatorSet;
import android.animation.ObjectAnimator;
import android.view.View;
import android.view.animation.AccelerateInterpolator;
import android.view.animation.DecelerateInterpolator;

/* renamed from: im.getsocial.sdk.ui.internal.h.pdwpUtZXDT */
public final class pdwpUtZXDT {
    private pdwpUtZXDT() {
    }

    /* renamed from: a */
    public static void m3362a(View view, View view2, AnimatorListener animatorListener) {
        ObjectAnimator ofFloat = ObjectAnimator.ofFloat(view, "translationX", new float[]{0.0f, (float) (-view.getWidth())});
        ObjectAnimator ofFloat2 = ObjectAnimator.ofFloat(view2, "translationX", new float[]{(float) view.getWidth(), 0.0f});
        AnimatorSet animatorSet = new AnimatorSet();
        animatorSet.playTogether(new Animator[]{ofFloat, ofFloat2});
        animatorSet.setInterpolator(new AccelerateInterpolator());
        animatorSet.setDuration(128);
        animatorSet.addListener(animatorListener);
        animatorSet.start();
    }

    /* renamed from: b */
    public static void m3363b(View view, View view2, AnimatorListener animatorListener) {
        ObjectAnimator ofFloat = ObjectAnimator.ofFloat(view, "translationX", new float[]{(float) (-view.getWidth()), 0.0f});
        ObjectAnimator ofFloat2 = ObjectAnimator.ofFloat(view2, "translationX", new float[]{0.0f, (float) view.getWidth()});
        AnimatorSet animatorSet = new AnimatorSet();
        animatorSet.playTogether(new Animator[]{ofFloat, ofFloat2});
        animatorSet.setInterpolator(new DecelerateInterpolator());
        animatorSet.setDuration(128);
        animatorSet.addListener(animatorListener);
        animatorSet.start();
    }
}
