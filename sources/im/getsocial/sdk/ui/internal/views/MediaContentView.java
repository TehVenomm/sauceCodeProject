package im.getsocial.sdk.ui.internal.views;

import android.content.Context;
import android.util.AttributeSet;
import android.view.View;
import android.view.View.MeasureSpec;
import android.view.View.OnClickListener;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.FrameLayout;
import android.widget.FrameLayout.LayoutParams;
import android.widget.ImageView;
import im.getsocial.sdk.internal.p030e.zoToeBNOjF;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;
import im.getsocial.sdk.ui.internal.views.AspectRatioVideoView.OnVideoStateListener;

public class MediaContentView extends FrameLayout {
    /* renamed from: a */
    private AspectRatioImageView f3144a;
    /* renamed from: b */
    private FrameLayout f3145b;
    /* renamed from: c */
    private ImageView f3146c;
    /* renamed from: d */
    private AspectRatioVideoView f3147d;
    /* renamed from: e */
    private KluUZYuxme f3148e;
    /* renamed from: f */
    private int f3149f;
    /* renamed from: g */
    private int f3150g;
    /* renamed from: h */
    private RoundedCornerMaskView f3151h;
    /* renamed from: i */
    private String f3152i;
    /* renamed from: j */
    private String f3153j;
    /* renamed from: k */
    private OnClickListener f3154k;
    /* renamed from: l */
    private double f3155l = 1.0d;

    /* renamed from: im.getsocial.sdk.ui.internal.views.MediaContentView$1 */
    class C12041 implements OnClickListener {
        /* renamed from: a */
        final /* synthetic */ MediaContentView f3143a;

        /* renamed from: im.getsocial.sdk.ui.internal.views.MediaContentView$1$1 */
        class C12031 implements OnVideoStateListener {
            /* renamed from: a */
            final /* synthetic */ C12041 f3142a;

            C12031(C12041 c12041) {
                this.f3142a = c12041;
            }

            /* renamed from: a */
            public final void mo4742a() {
                LoadingIndicator.m3518b(this.f3142a.f3143a);
                this.f3142a.f3143a.f3147d.m3493a();
                MediaContentView.m3534f(this.f3142a.f3143a);
            }

            /* renamed from: b */
            public final void mo4743b() {
                this.f3142a.f3143a.m3527a(this.f3142a.f3143a.f3152i, false);
                this.f3142a.f3143a.f3147d.setVisibility(8);
                this.f3142a.f3143a.f3145b.setVisibility(0);
                this.f3142a.f3143a.removeView(this.f3142a.f3143a.f3147d);
            }

            /* renamed from: c */
            public final void mo4744c() {
                LoadingIndicator.m3518b(this.f3142a.f3143a);
                this.f3142a.f3143a.m3538a(this.f3142a.f3143a.f3152i);
                this.f3142a.f3143a.removeView(this.f3142a.f3143a.f3147d);
                this.f3142a.f3143a.f3145b.setVisibility(0);
            }

            /* renamed from: d */
            public final void mo4745d() {
                this.f3142a.f3143a.m3527a(this.f3142a.f3143a.f3152i, false);
                this.f3142a.f3143a.f3147d.setVisibility(8);
                this.f3142a.f3143a.f3145b.setVisibility(0);
            }
        }

        C12041(MediaContentView mediaContentView) {
            this.f3143a = mediaContentView;
        }

        public void onClick(View view) {
            if (this.f3143a.f3154k != null) {
                this.f3143a.f3154k.onClick(view);
            }
            MediaContentView.m3529b(this.f3143a);
            this.f3143a.f3147d.setVisibility(0);
            this.f3143a.f3145b.setVisibility(8);
            LoadingIndicator.m3515a(this.f3143a);
            this.f3143a.f3147d.m3495a(this.f3143a.f3153j, new C12031(this));
        }
    }

    public MediaContentView(Context context) {
        super(context);
        m3528b();
    }

    public MediaContentView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        m3528b();
    }

    public MediaContentView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        m3528b();
    }

    /* renamed from: a */
    private void m3527a(String str, boolean z) {
        this.f3144a.setVisibility(0);
        this.f3144a.m3483a(this.f3155l);
        this.f3148e.m3315a(str, this.f3150g, this.f3149f, this.f3144a, z);
        this.f3148e.m3314a(this.f3151h);
        this.f3145b.setVisibility(4);
    }

    /* renamed from: b */
    private void m3528b() {
        this.f3148e = KluUZYuxme.m3299a(getContext());
        zoToeBNOjF b = this.f3148e.m3320b();
        this.f3150g = ((Integer) b.mo4497a()).intValue();
        this.f3149f = ((Integer) b.mo4498b()).intValue();
    }

    /* renamed from: b */
    static /* synthetic */ void m3529b(MediaContentView mediaContentView) {
        if (mediaContentView.f3147d != null) {
            mediaContentView.removeView(mediaContentView.f3147d);
        }
        mediaContentView.f3147d = new AspectRatioVideoView(mediaContentView.getContext());
        mediaContentView.f3147d.setLayoutParams(new LayoutParams(-1, -1));
        mediaContentView.f3147d.setVisibility(0);
        mediaContentView.f3147d.m3494a(mediaContentView.f3155l);
        mediaContentView.addView(mediaContentView.f3147d, 0);
    }

    /* renamed from: c */
    private void m3531c() {
        this.f3145b = (FrameLayout) findViewById(C1067R.id.video_overlay);
        this.f3145b.setContentDescription("post_content_video");
        this.f3146c = (ImageView) findViewById(C1067R.id.video_play_image);
        this.f3146c.setContentDescription("play_button");
        this.f3144a = (AspectRatioImageView) findViewById(C1067R.id.image_view_activity_image);
        this.f3144a.setContentDescription("post_content_image");
        this.f3151h = (RoundedCornerMaskView) findViewById(C1067R.id.rounded_corners_view);
    }

    /* renamed from: f */
    static /* synthetic */ void m3534f(MediaContentView mediaContentView) {
        Animation loadAnimation = AnimationUtils.loadAnimation(mediaContentView.getContext(), 17432577);
        loadAnimation.setDuration(400);
        mediaContentView.f3144a.startAnimation(loadAnimation);
        mediaContentView.f3144a.setVisibility(8);
    }

    /* renamed from: a */
    public final void m3536a() {
        if (this.f3145b != null) {
            this.f3145b.setVisibility(0);
        }
        if (this.f3147d != null) {
            this.f3147d.m3496b();
        }
    }

    /* renamed from: a */
    public final void m3537a(double d) {
        this.f3155l = d;
        if (this.f3144a != null) {
            this.f3144a.m3483a(this.f3155l);
        }
        if (this.f3147d != null) {
            this.f3147d.m3494a(this.f3155l);
        }
    }

    /* renamed from: a */
    public final void m3538a(String str) {
        m3531c();
        m3527a(str, true);
    }

    /* renamed from: a */
    public final void m3539a(String str, String str2, OnClickListener onClickListener) {
        this.f3154k = onClickListener;
        m3531c();
        this.f3153j = str2;
        this.f3152i = str;
        m3527a(str, false);
        this.f3145b.setVisibility(0);
        this.f3148e.m3308a(this.f3146c);
        this.f3148e.m3307a(this.f3145b);
        this.f3148e.m3314a(this.f3151h);
        this.f3145b.setOnClickListener(new C12041(this));
    }

    protected void onMeasure(int i, int i2) {
        if (this.f3155l == 1.0d) {
            super.onMeasure(i, i2);
            return;
        }
        int size = MeasureSpec.getSize(i);
        super.onMeasure(size, MeasureSpec.makeMeasureSpec((int) (((double) size) / this.f3155l), 1073741824));
    }
}
