package com.zopim.android.sdk.chatlog.view;

import android.annotation.TargetApi;
import android.content.Context;
import android.graphics.drawable.AnimationDrawable;
import android.graphics.drawable.Drawable;
import android.os.Build.VERSION;
import android.util.AttributeSet;
import android.util.Log;
import android.widget.ImageView;
import android.widget.LinearLayout;
import com.zopim.android.sdk.C0785R;
import java.util.concurrent.TimeUnit;

public class TypingIndicatorView extends LinearLayout {
    private static final String LOG_TAG = TypingIndicatorView.class.getSimpleName();
    private static final long TYPING_INDICATOR_MAX_DELAY = TimeUnit.SECONDS.toMillis(2);
    private AnimationDrawable[] mAnimations;
    private long mTransitionDelay = TYPING_INDICATOR_MAX_DELAY;

    public TypingIndicatorView(Context context) {
        super(context);
    }

    public TypingIndicatorView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
    }

    @TargetApi(16)
    private AnimationDrawable[] prepareAnimations() {
        int childCount = getChildCount();
        int integer = getResources().getInteger(C0785R.integer.typing_dot_duration);
        this.mTransitionDelay = ((long) integer) > TYPING_INDICATOR_MAX_DELAY ? TYPING_INDICATOR_MAX_DELAY : (long) integer;
        Drawable drawable = getResources().getDrawable(C0785R.drawable.ic_typing_dot_secondary);
        Drawable drawable2 = getResources().getDrawable(C0785R.drawable.ic_typing_dot_primary);
        AnimationDrawable[] animationDrawableArr = new AnimationDrawable[childCount];
        int i = 0;
        while (getChildAt(i) instanceof ImageView) {
            ImageView imageView = (ImageView) getChildAt(i);
            Drawable animationDrawable = new AnimationDrawable();
            animationDrawable.addFrame(drawable, (childCount - 1) * integer);
            animationDrawable.addFrame(drawable2, integer);
            animationDrawable.setOneShot(false);
            if (VERSION.SDK_INT < 16) {
                imageView.setBackgroundDrawable(animationDrawable);
            } else {
                imageView.setBackground(animationDrawable);
            }
            int i2 = i + 1;
            animationDrawableArr[i] = animationDrawable;
            i = i2;
        }
        return animationDrawableArr;
    }

    public void start() {
        this.mAnimations = prepareAnimations();
        long j = 0;
        for (AnimationDrawable c0856a : this.mAnimations) {
            postDelayed(new C0856a(this, c0856a), j);
            j += this.mTransitionDelay;
        }
    }

    public void stop() {
        if (this.mAnimations == null) {
            Log.w(LOG_TAG, "Animations are not initialized. Aborting stop.");
            return;
        }
        for (int i = 0; i < this.mAnimations.length; i++) {
            this.mAnimations[i].selectDrawable(0);
            this.mAnimations[i].stop();
        }
    }
}
