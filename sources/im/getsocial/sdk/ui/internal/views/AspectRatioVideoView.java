package im.getsocial.sdk.ui.internal.views;

import android.content.Context;
import android.graphics.Matrix;
import android.graphics.SurfaceTexture;
import android.media.MediaPlayer;
import android.media.MediaPlayer.OnCompletionListener;
import android.media.MediaPlayer.OnErrorListener;
import android.media.MediaPlayer.OnPreparedListener;
import android.media.MediaPlayer.OnSeekCompleteListener;
import android.util.AttributeSet;
import android.view.Surface;
import android.view.TextureView;
import android.view.TextureView.SurfaceTextureListener;
import android.view.View;
import android.view.View.MeasureSpec;
import android.view.View.OnClickListener;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.p125h.jjbQypPegg;

public class AspectRatioVideoView extends TextureView implements OnPreparedListener, SurfaceTextureListener {
    /* renamed from: a */
    private static final cjrhisSQCL f3113a = upgqDBbsrL.m1274a(AspectRatioVideoView.class);
    /* renamed from: b */
    private jjbQypPegg f3114b;
    /* renamed from: c */
    private String f3115c;
    /* renamed from: d */
    private double f3116d = 1.0d;
    /* renamed from: e */
    private OnVideoStateListener f3117e;
    /* renamed from: f */
    private SurfaceTexture f3118f;

    /* renamed from: im.getsocial.sdk.ui.internal.views.AspectRatioVideoView$1 */
    class C11961 implements OnClickListener {
        /* renamed from: a */
        final /* synthetic */ AspectRatioVideoView f3108a;

        C11961(AspectRatioVideoView aspectRatioVideoView) {
            this.f3108a = aspectRatioVideoView;
        }

        public void onClick(View view) {
            if (this.f3108a.m3497c()) {
                this.f3108a.m3498d();
            }
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.views.AspectRatioVideoView$2 */
    class C11972 implements jjbQypPegg.jjbQypPegg {
        /* renamed from: a */
        final /* synthetic */ AspectRatioVideoView f3109a;

        C11972(AspectRatioVideoView aspectRatioVideoView) {
            this.f3109a = aspectRatioVideoView;
        }

        /* renamed from: a */
        public final void mo4741a() {
            this.f3109a.f3117e.mo4743b();
            this.f3109a.m3496b();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.views.AspectRatioVideoView$3 */
    class C11983 implements OnCompletionListener {
        /* renamed from: a */
        final /* synthetic */ AspectRatioVideoView f3110a;

        C11983(AspectRatioVideoView aspectRatioVideoView) {
            this.f3110a = aspectRatioVideoView;
        }

        public void onCompletion(MediaPlayer mediaPlayer) {
            this.f3110a.f3117e.mo4743b();
            this.f3110a.m3496b();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.views.AspectRatioVideoView$4 */
    class C11994 implements OnErrorListener {
        /* renamed from: a */
        final /* synthetic */ AspectRatioVideoView f3111a;

        C11994(AspectRatioVideoView aspectRatioVideoView) {
            this.f3111a = aspectRatioVideoView;
        }

        public boolean onError(MediaPlayer mediaPlayer, int i, int i2) {
            AspectRatioVideoView.f3113a.mo4387a("error in mediaplayer");
            this.f3111a.f3117e.mo4744c();
            return false;
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.views.AspectRatioVideoView$5 */
    class C12005 implements OnSeekCompleteListener {
        /* renamed from: a */
        final /* synthetic */ AspectRatioVideoView f3112a;

        C12005(AspectRatioVideoView aspectRatioVideoView) {
            this.f3112a = aspectRatioVideoView;
        }

        public void onSeekComplete(MediaPlayer mediaPlayer) {
            this.f3112a.f3117e.mo4742a();
        }
    }

    public interface OnVideoStateListener {
        /* renamed from: a */
        void mo4742a();

        /* renamed from: b */
        void mo4743b();

        /* renamed from: c */
        void mo4744c();

        /* renamed from: d */
        void mo4745d();
    }

    public AspectRatioVideoView(Context context) {
        super(context);
        m3492f();
    }

    public AspectRatioVideoView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        m3492f();
    }

    public AspectRatioVideoView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        m3492f();
    }

    /* renamed from: a */
    private void m3490a(SurfaceTexture surfaceTexture) {
        try {
            this.f3114b = new jjbQypPegg();
            this.f3114b.m3355a(this.f3115c);
            this.f3114b.m3351a((OnPreparedListener) this);
            this.f3114b.m3354a(new C11972(this));
            this.f3114b.m3349a(new C11983(this));
            this.f3114b.m3350a(new C11994(this));
            this.f3114b.m3352a(new C12005(this));
            this.f3114b.m3353a(new Surface(surfaceTexture));
            this.f3114b.m3348a(getContext());
        } catch (Throwable e) {
            f3113a.mo4389a(e);
        }
    }

    /* renamed from: f */
    private void m3492f() {
        setSurfaceTextureListener(this);
        setOnClickListener(new C11961(this));
    }

    /* renamed from: a */
    public final void m3493a() {
        if (this.f3114b != null) {
            this.f3114b.m3346a();
        }
    }

    /* renamed from: a */
    public final void m3494a(double d) {
        this.f3116d = d;
    }

    /* renamed from: a */
    public final void m3495a(String str, OnVideoStateListener onVideoStateListener) {
        if (this.f3114b != null && this.f3115c.equals(str)) {
            this.f3117e.mo4743b();
            m3496b();
        }
        this.f3115c = str;
        this.f3117e = onVideoStateListener;
        if (this.f3118f != null) {
            m3490a(this.f3118f);
        }
    }

    /* renamed from: b */
    public final void m3496b() {
        this.f3115c = null;
        if (this.f3114b != null) {
            this.f3114b.m3358d();
        }
        this.f3114b = null;
    }

    /* renamed from: c */
    public final boolean m3497c() {
        return this.f3114b != null && this.f3114b.m3357c();
    }

    /* renamed from: d */
    public final void m3498d() {
        if (this.f3114b != null) {
            this.f3114b.m3359e();
        }
    }

    protected void onMeasure(int i, int i2) {
        if (this.f3116d == 1.0d) {
            super.onMeasure(i, i2);
            return;
        }
        int size = MeasureSpec.getSize(i2);
        setMeasuredDimension((int) (((double) size) * this.f3116d), size);
    }

    public void onPrepared(MediaPlayer mediaPlayer) {
        float f = 1.0f;
        if (this.f3114b != null) {
            float g = (float) this.f3114b.m3361g();
            float f2 = (float) this.f3114b.m3360f();
            float width = ((float) getWidth()) / ((float) getHeight());
            if (width > g / f2) {
                g = (f2 / g) * width;
            } else {
                float f3 = (g / f2) / width;
                g = 1.0f;
                f = f3;
            }
            int width2 = getWidth() / 2;
            int height = getHeight() / 2;
            Matrix matrix = new Matrix();
            matrix.setScale(f, g, (float) width2, (float) height);
            setTransform(matrix);
        }
        if (this.f3114b != null) {
            this.f3114b.m3347a(100);
        }
    }

    public void onSurfaceTextureAvailable(SurfaceTexture surfaceTexture, int i, int i2) {
        this.f3118f = surfaceTexture;
        m3490a(this.f3118f);
    }

    public boolean onSurfaceTextureDestroyed(SurfaceTexture surfaceTexture) {
        this.f3117e.mo4745d();
        return true;
    }

    public void onSurfaceTextureSizeChanged(SurfaceTexture surfaceTexture, int i, int i2) {
    }

    public void onSurfaceTextureUpdated(SurfaceTexture surfaceTexture) {
        this.f3118f = surfaceTexture;
    }
}
