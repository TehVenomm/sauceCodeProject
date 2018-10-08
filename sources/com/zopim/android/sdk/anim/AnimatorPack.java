package com.zopim.android.sdk.anim;

import android.animation.Animator;
import android.animation.Animator.AnimatorListener;
import android.animation.AnimatorSet;
import android.animation.ObjectAnimator;
import android.annotation.TargetApi;
import android.app.Activity;
import android.graphics.Rect;
import android.util.Log;
import android.view.View;
import java.util.ArrayList;
import java.util.Collection;

@TargetApi(11)
public class AnimatorPack {
    private static final long DURATION = 400;
    private static final String TAG = AnimatorPack.class.getSimpleName();

    public enum Direction {
        LEFT,
        TOP,
        RIGHT,
        BOTTOM
    }

    public static AnimatorSet crossfade(View view, View view2) {
        return crossfade(view, view2, null, null);
    }

    public static AnimatorSet crossfade(View view, View view2, AnimatorListener animatorListener, AnimatorListener animatorListener2) {
        Animator ofFloat = ObjectAnimator.ofFloat(view, "alpha", new float[]{1.0f, 0.5f});
        Animator ofFloat2 = ObjectAnimator.ofFloat(view2, "alpha", new float[]{0.5f, 1.0f});
        Animator scale = scale(view, 1.0f, 0.5f);
        Animator scale2 = scale(view2, 0.5f, 1.0f);
        AnimatorSet animatorSet = new AnimatorSet();
        animatorSet.setDuration(DURATION);
        animatorSet.play(ofFloat).with(scale).before(ofFloat2).before(scale2);
        if (animatorListener != null) {
            ofFloat.addListener(animatorListener);
        }
        if (animatorListener2 != null) {
            ofFloat2.addListener(animatorListener2);
        }
        ofFloat.addListener(new C0785a(view));
        ofFloat2.addListener(new C0786b(view2));
        return animatorSet;
    }

    public static Animator fadeIn(View view) {
        if (view == null) {
            return new AnimatorSet();
        }
        Animator ofFloat = ObjectAnimator.ofFloat(view, "alpha", new float[]{0.0f, 1.0f});
        Animator scale = scale(view, 0.5f, 1.0f);
        Animator animatorSet = new AnimatorSet();
        animatorSet.setDuration(DURATION);
        animatorSet.play(ofFloat).with(scale);
        animatorSet.addListener(new C0790f(view));
        return animatorSet;
    }

    public static Animator fadeOut(View view) {
        if (view == null) {
            return new AnimatorSet();
        }
        Animator ofFloat = ObjectAnimator.ofFloat(view, "alpha", new float[]{1.0f, 0.0f});
        Animator scale = scale(view, 1.0f, 0.5f);
        Animator animatorSet = new AnimatorSet();
        animatorSet.setDuration(DURATION);
        animatorSet.play(ofFloat).with(scale);
        animatorSet.addListener(new C0789e(view));
        return animatorSet;
    }

    public static AnimatorSet scale(View view, float f, float f2) {
        Animator ofFloat = ObjectAnimator.ofFloat(view, "scaleX", new float[]{f, f2});
        Animator ofFloat2 = ObjectAnimator.ofFloat(view, "scaleY", new float[]{f, f2});
        AnimatorSet animatorSet = new AnimatorSet();
        animatorSet.play(ofFloat).with(ofFloat2);
        animatorSet.setDuration(DURATION);
        return animatorSet;
    }

    public static Animator slideIn(View view, Direction direction) {
        if (direction == null || view == null) {
            return null;
        }
        Animator ofFloat;
        float translationX = view.getTranslationX();
        float translationY = view.getTranslationY();
        int[] iArr = new int[2];
        view.getLocationOnScreen(iArr);
        Rect rect = new Rect(iArr[0], iArr[1], iArr[0] + view.getWidth(), iArr[1] + view.getHeight());
        Rect rect2 = new Rect();
        try {
            ((Activity) view.getContext()).getWindow().getDecorView().getWindowVisibleDisplayFrame(rect2);
        } catch (Exception e) {
            Log.w(TAG, "Can not get activity visible rectangle, will use phone view. " + e.getMessage());
            view.getRootView().getLocalVisibleRect(rect2);
        }
        switch (direction) {
            case LEFT:
                ofFloat = ObjectAnimator.ofFloat(view, "translationX", new float[]{(float) (-Math.abs(rect2.left - rect.right)), translationX});
                break;
            case TOP:
                ofFloat = ObjectAnimator.ofFloat(view, "translationY", new float[]{(float) (-Math.abs(rect2.top - rect.bottom)), translationY});
                break;
            case RIGHT:
                ofFloat = ObjectAnimator.ofFloat(view, "translationX", new float[]{(float) Math.abs(rect2.right - rect.left), translationX});
                break;
            case BOTTOM:
                ofFloat = ObjectAnimator.ofFloat(view, "translationY", new float[]{(float) Math.abs(rect2.bottom - rect.top), translationY});
                break;
            default:
                ofFloat = null;
                break;
        }
        if (ofFloat == null) {
            return ofFloat;
        }
        ofFloat.setDuration(DURATION);
        ofFloat.addListener(new C0788d(view));
        return ofFloat;
    }

    public static Animator slideInSequentially(Direction direction, long j, boolean z, boolean z2, View... viewArr) {
        if (direction == null || viewArr == null) {
            return new AnimatorSet();
        }
        long j2 = 0;
        Collection arrayList = new ArrayList();
        for (View view : viewArr) {
            AnimatorSet animatorSet = new AnimatorSet();
            animatorSet.setStartDelay(j2);
            animatorSet.play(slideIn(view, direction));
            if (z) {
                animatorSet.play(ObjectAnimator.ofFloat(view, "alpha", new float[]{0.0f, 1.0f}).setDuration(DURATION));
            }
            if (z2) {
                animatorSet.play(scale(view, 0.5f, 1.0f));
            }
            arrayList.add(animatorSet);
            j2 += j;
        }
        Animator animatorSet2 = new AnimatorSet();
        animatorSet2.playTogether(arrayList);
        return animatorSet2;
    }

    public static Animator slideOut(View view, Direction direction) {
        if (direction == null || view == null) {
            return null;
        }
        Animator ofFloat;
        float translationX = view.getTranslationX();
        float translationY = view.getTranslationY();
        int[] iArr = new int[2];
        view.getLocationOnScreen(iArr);
        Rect rect = new Rect(iArr[0], iArr[1], iArr[0] + view.getWidth(), iArr[1] + view.getHeight());
        Rect rect2 = new Rect();
        try {
            ((Activity) view.getContext()).getWindow().getDecorView().getWindowVisibleDisplayFrame(rect2);
        } catch (Exception e) {
            Log.w(TAG, "Can not get activity visible rectangle, will use phone view. " + e.getMessage());
            view.getRootView().getLocalVisibleRect(rect2);
        }
        switch (direction) {
            case LEFT:
                ofFloat = ObjectAnimator.ofFloat(view, "translationX", new float[]{translationX, translationX - ((float) Math.abs(rect2.left - rect.right))});
                break;
            case TOP:
                ofFloat = ObjectAnimator.ofFloat(view, "translationY", new float[]{translationY, translationY - ((float) Math.abs(rect2.top - rect.bottom))});
                break;
            case RIGHT:
                ofFloat = ObjectAnimator.ofFloat(view, "translationX", new float[]{translationX, ((float) Math.abs(rect2.right - rect.left)) + translationX});
                break;
            case BOTTOM:
                ofFloat = ObjectAnimator.ofFloat(view, "translationY", new float[]{translationY, ((float) Math.abs(rect2.bottom - rect.top)) + translationY});
                break;
            default:
                ofFloat = null;
                break;
        }
        ofFloat.setDuration(DURATION);
        ofFloat.addListener(new C0787c(view, translationX, translationY));
        return ofFloat;
    }

    public static Animator slideOutSequentially(Direction direction, long j, boolean z, boolean z2, View... viewArr) {
        if (direction == null || viewArr == null) {
            return new AnimatorSet();
        }
        long j2 = 0;
        Collection arrayList = new ArrayList();
        for (View view : viewArr) {
            AnimatorSet animatorSet = new AnimatorSet();
            animatorSet.setStartDelay(j2);
            animatorSet.play(slideOut(view, direction));
            if (z) {
                animatorSet.play(ObjectAnimator.ofFloat(view, "alpha", new float[]{1.0f, 0.0f}).setDuration(DURATION));
            }
            if (z2) {
                animatorSet.play(scale(view, 1.0f, 0.5f));
            }
            arrayList.add(animatorSet);
            j2 += j;
        }
        Animator animatorSet2 = new AnimatorSet();
        animatorSet2.playTogether(arrayList);
        return animatorSet2;
    }
}
