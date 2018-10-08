package im.getsocial.sdk.ui.internal.p125h;

import android.view.View;
import android.view.animation.AccelerateDecelerateInterpolator;
import android.view.animation.AccelerateInterpolator;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.view.animation.Animation.AnimationListener;
import android.view.animation.AnimationSet;
import android.view.animation.DecelerateInterpolator;
import android.view.animation.ScaleAnimation;
import im.getsocial.sdk.ui.internal.p131d.p132a.jjbQypPegg;
import im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL;

/* renamed from: im.getsocial.sdk.ui.internal.h.jMsobIMeui */
public class jMsobIMeui {
    /* renamed from: a */
    private Animation f2977a;
    /* renamed from: b */
    private Animation f2978b;
    /* renamed from: c */
    private Animation f2979c;
    /* renamed from: d */
    private Animation f2980d;
    /* renamed from: e */
    private jjbQypPegg f2981e = jjbQypPegg.NONE;
    /* renamed from: f */
    private final int f2982f;
    /* renamed from: g */
    private final int f2983g;

    public jMsobIMeui(int i, int i2) {
        this.f2982f = i;
        this.f2983g = i2;
        m3335a(upgqDBbsrL.m3237a().m3255b().m3212c().m3117a().m3166f(), 333);
    }

    /* renamed from: a */
    private void m3332a(int i) {
        this.f2979c = new AlphaAnimation(0.0f, 1.0f);
        this.f2979c.setDuration((long) i);
        this.f2979c.setInterpolator(new AccelerateDecelerateInterpolator());
        this.f2980d = new AlphaAnimation(1.0f, 0.0f);
        this.f2980d.setDuration((long) i);
        this.f2980d.setInterpolator(new AccelerateDecelerateInterpolator());
    }

    /* renamed from: a */
    public final int m3333a(View view, View view2) {
        if (this.f2981e == jjbQypPegg.NONE || this.f2977a == null) {
            return 0;
        }
        this.f2977a.setAnimationListener(null);
        view.startAnimation(this.f2979c);
        view2.startAnimation(this.f2977a);
        return (int) this.f2977a.getDuration();
    }

    /* renamed from: a */
    public final int m3334a(View view, View view2, AnimationListener animationListener) {
        if (this.f2981e == jjbQypPegg.NONE || this.f2978b == null) {
            if (animationListener != null) {
                animationListener.onAnimationStart(null);
                animationListener.onAnimationEnd(null);
            }
            return 0;
        }
        this.f2978b.setAnimationListener(animationListener);
        view.startAnimation(this.f2980d);
        view2.startAnimation(this.f2978b);
        return (int) this.f2978b.getDuration();
    }

    /* renamed from: a */
    public final void m3335a(jjbQypPegg jjbqyppegg, int i) {
        this.f2981e = jjbqyppegg;
        float f;
        float f2;
        switch (jjbqyppegg) {
            case FADE:
                this.f2977a = new AlphaAnimation(0.0f, 1.0f);
                this.f2977a.setDuration((long) 333);
                this.f2977a.setInterpolator(new AccelerateDecelerateInterpolator());
                this.f2978b = new AlphaAnimation(1.0f, 0.0f);
                this.f2978b.setDuration((long) 333);
                this.f2978b.setInterpolator(new AccelerateDecelerateInterpolator());
                m3332a(333);
                return;
            case FADE_AND_SCALE:
                Animation alphaAnimation = new AlphaAnimation(0.0f, 1.0f);
                alphaAnimation.setDuration((long) 333);
                alphaAnimation.setInterpolator(new AccelerateDecelerateInterpolator());
                f = ((float) this.f2982f) / 2.0f;
                f2 = ((float) this.f2983g) / 2.0f;
                Animation scaleAnimation = new ScaleAnimation(0.0f, 1.0f, 0.0f, 1.0f, f, f2);
                scaleAnimation.setInterpolator(new DecelerateInterpolator());
                Animation animationSet = new AnimationSet(false);
                animationSet.setDuration((long) 333);
                animationSet.addAnimation(alphaAnimation);
                animationSet.addAnimation(scaleAnimation);
                this.f2977a = animationSet;
                alphaAnimation = new AlphaAnimation(1.0f, 0.0f);
                alphaAnimation.setDuration((long) 333);
                alphaAnimation.setInterpolator(new AccelerateDecelerateInterpolator());
                scaleAnimation = new ScaleAnimation(1.0f, 0.0f, 1.0f, 0.0f, f, f2);
                scaleAnimation.setInterpolator(new AccelerateInterpolator());
                animationSet = new AnimationSet(false);
                animationSet.setDuration((long) 333);
                animationSet.addAnimation(alphaAnimation);
                animationSet.addAnimation(scaleAnimation);
                this.f2978b = animationSet;
                m3332a(333);
                return;
            case SCALE:
                f = ((float) this.f2982f) / 2.0f;
                f2 = ((float) this.f2983g) / 2.0f;
                this.f2977a = new ScaleAnimation(0.0f, 1.0f, 0.0f, 1.0f, f, f2);
                this.f2977a.setDuration((long) 333);
                this.f2977a.setInterpolator(new DecelerateInterpolator());
                this.f2978b = new ScaleAnimation(1.0f, 0.0f, 1.0f, 0.0f, f, f2);
                this.f2978b.setDuration((long) 333);
                this.f2978b.setInterpolator(new AccelerateInterpolator());
                m3332a(333);
                return;
            default:
                this.f2977a = null;
                this.f2978b = null;
                this.f2979c = null;
                this.f2980d = null;
                return;
        }
    }
}
