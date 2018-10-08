package im.getsocial.sdk.ui.internal.views;

import android.annotation.TargetApi;
import android.content.Context;
import android.support.v4.media.session.PlaybackStateCompat;
import android.support.v4.view.ViewCompat;
import android.util.AttributeSet;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.view.animation.Animation;
import android.view.animation.LinearInterpolator;
import android.view.animation.RotateAnimation;
import android.widget.FrameLayout;
import android.widget.ImageView;
import im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL;

public class LoadingIndicator extends FrameLayout implements OnClickListener {
    /* renamed from: a */
    private static final Object f3139a = new Object();
    /* renamed from: b */
    private FrameLayout f3140b;

    public LoadingIndicator(Context context) {
        super(context);
        m3517a();
    }

    public LoadingIndicator(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        m3517a();
    }

    public LoadingIndicator(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        m3517a();
    }

    @TargetApi(21)
    public LoadingIndicator(Context context, AttributeSet attributeSet, int i, int i2) {
        super(context, attributeSet, i, i2);
        m3517a();
    }

    /* renamed from: a */
    public static LoadingIndicator m3515a(View view) {
        if (!(view instanceof ViewGroup)) {
            return null;
        }
        LoadingIndicator loadingIndicator;
        ViewGroup viewGroup = (ViewGroup) view;
        LoadingIndicator a = m3516a(viewGroup);
        if (a == null) {
            View loadingIndicator2 = new LoadingIndicator(view.getContext());
            loadingIndicator2.setTag(f3139a);
            viewGroup.addView(loadingIndicator2, new LayoutParams(-1, -1));
            loadingIndicator = loadingIndicator2;
        } else {
            loadingIndicator = a;
        }
        loadingIndicator.bringToFront();
        return loadingIndicator;
    }

    /* renamed from: a */
    private static LoadingIndicator m3516a(ViewGroup viewGroup) {
        return (LoadingIndicator) viewGroup.findViewWithTag(f3139a);
    }

    /* renamed from: a */
    private void m3517a() {
        setOnClickListener(this);
        this.f3140b = new FrameLayout(getContext());
        this.f3140b.setLayoutParams(new FrameLayout.LayoutParams(-1, -1));
        this.f3140b.setBackgroundColor(ViewCompat.MEASURED_STATE_MASK);
        this.f3140b.setAlpha(0.2f);
        addView(this.f3140b);
        LayoutParams layoutParams = new FrameLayout.LayoutParams(upgqDBbsrL.m3237a().m3253b(36.0f), upgqDBbsrL.m3237a().m3253b(36.0f));
        layoutParams.gravity = 17;
        View c12021 = new ImageView(this, getContext()) {
            /* renamed from: a */
            final /* synthetic */ LoadingIndicator f3138a;

            protected void onMeasure(int i, int i2) {
                super.onMeasure(i2, i2);
            }
        };
        c12021.setLayoutParams(layoutParams);
        jjbQypPegg.m3596a(c12021, upgqDBbsrL.m3237a().m3254b(getContext(), upgqDBbsrL.m3237a().m3255b().m3212c().m3141y().m3173a()));
        addView(c12021);
        Animation rotateAnimation = new RotateAnimation(0.0f, 360.0f, 1, 0.5f, 1, 0.5f);
        rotateAnimation.setInterpolator(new LinearInterpolator());
        rotateAnimation.setDuration(PlaybackStateCompat.ACTION_PLAY_FROM_MEDIA_ID);
        rotateAnimation.setRepeatCount(-1);
        c12021.startAnimation(rotateAnimation);
    }

    /* renamed from: b */
    public static void m3518b(View view) {
        if (view instanceof ViewGroup) {
            ViewGroup viewGroup = (ViewGroup) view;
            View a = m3516a(viewGroup);
            if (a != null) {
                viewGroup.removeView(a);
            }
        }
    }

    /* renamed from: a */
    public final void m3519a(int i, float f) {
        this.f3140b.setAlpha(1.0f);
        this.f3140b.setBackgroundColor(0);
    }

    public void onClick(View view) {
    }
}
