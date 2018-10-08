package im.getsocial.sdk.ui.internal.p125h;

import android.content.Context;
import android.media.MediaPlayer;
import android.media.MediaPlayer.OnCompletionListener;
import android.media.MediaPlayer.OnErrorListener;
import android.media.MediaPlayer.OnPreparedListener;
import android.media.MediaPlayer.OnSeekCompleteListener;
import android.view.Surface;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p089m.EmkjBpiUfq;
import java.io.IOException;

/* renamed from: im.getsocial.sdk.ui.internal.h.jjbQypPegg */
public class jjbQypPegg {
    /* renamed from: i */
    private static final cjrhisSQCL f2989i = upgqDBbsrL.m1274a(jjbQypPegg.class);
    /* renamed from: a */
    private jjbQypPegg f2990a;
    /* renamed from: b */
    private OnPreparedListener f2991b;
    /* renamed from: c */
    private OnCompletionListener f2992c;
    /* renamed from: d */
    private OnErrorListener f2993d;
    /* renamed from: e */
    private OnSeekCompleteListener f2994e;
    /* renamed from: f */
    private MediaPlayer f2995f = new MediaPlayer();
    /* renamed from: g */
    private upgqDBbsrL f2996g;
    /* renamed from: h */
    private String f2997h;

    /* renamed from: im.getsocial.sdk.ui.internal.h.jjbQypPegg$1 */
    class C11561 implements OnPreparedListener {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2984a;

        C11561(jjbQypPegg jjbqyppegg) {
            this.f2984a = jjbqyppegg;
        }

        public void onPrepared(MediaPlayer mediaPlayer) {
            this.f2984a.m3339a(upgqDBbsrL.PREPARED);
            if (this.f2984a.f2991b != null) {
                this.f2984a.f2991b.onPrepared(mediaPlayer);
            }
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.h.jjbQypPegg$2 */
    class C11572 implements OnCompletionListener {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2985a;

        C11572(jjbQypPegg jjbqyppegg) {
            this.f2985a = jjbqyppegg;
        }

        public void onCompletion(MediaPlayer mediaPlayer) {
            this.f2985a.m3339a(upgqDBbsrL.PLAYBACK_COMPLETED);
            zoToeBNOjF.m3366a(this.f2985a);
            if (this.f2985a.f2992c != null) {
                this.f2985a.f2992c.onCompletion(mediaPlayer);
            }
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.h.jjbQypPegg$3 */
    class C11583 implements OnErrorListener {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2986a;

        C11583(jjbQypPegg jjbqyppegg) {
            this.f2986a = jjbqyppegg;
        }

        public boolean onError(MediaPlayer mediaPlayer, int i, int i2) {
            this.f2986a.m3339a(upgqDBbsrL.ERROR);
            jjbQypPegg.f2989i.mo4387a("error: [" + i + "] - [" + i2 + "]");
            if (this.f2986a.f2993d != null) {
                this.f2986a.f2993d.onError(mediaPlayer, i, i2);
            }
            return false;
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.h.jjbQypPegg$4 */
    class C11594 implements OnSeekCompleteListener {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2987a;

        C11594(jjbQypPegg jjbqyppegg) {
            this.f2987a = jjbqyppegg;
        }

        public void onSeekComplete(MediaPlayer mediaPlayer) {
            if (this.f2987a.f2994e != null) {
                this.f2987a.f2994e.onSeekComplete(mediaPlayer);
            }
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.h.jjbQypPegg$5 */
    class C11605 implements EmkjBpiUfq.upgqDBbsrL {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2988a;

        C11605(jjbQypPegg jjbqyppegg) {
            this.f2988a = jjbqyppegg;
        }

        /* renamed from: a */
        public final void mo4569a(String str) {
            try {
                this.f2988a.f2995f.setDataSource(str);
                this.f2988a.f2995f.prepareAsync();
                this.f2988a.m3339a(upgqDBbsrL.PREPARING);
            } catch (IOException e) {
                jjbQypPegg.f2989i.mo4396d("Could not set video as datasource, error: " + e.getMessage());
            }
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.h.jjbQypPegg$jjbQypPegg */
    public interface jjbQypPegg {
        /* renamed from: a */
        void mo4741a();
    }

    /* renamed from: im.getsocial.sdk.ui.internal.h.jjbQypPegg$upgqDBbsrL */
    public enum upgqDBbsrL {
        IDLE,
        INITIALIZED,
        PREPARING,
        PREPARED,
        STARTED,
        STOPPED,
        PAUSED,
        PLAYBACK_COMPLETED,
        END,
        ERROR
    }

    public jjbQypPegg() {
        this.f2995f.setLooping(false);
        m3339a(upgqDBbsrL.IDLE);
        this.f2995f.setOnPreparedListener(new C11561(this));
        this.f2995f.setOnCompletionListener(new C11572(this));
        this.f2995f.setOnErrorListener(new C11583(this));
        this.f2995f.setOnSeekCompleteListener(new C11594(this));
    }

    /* renamed from: a */
    private void m3339a(upgqDBbsrL upgqdbbsrl) {
        f2989i.mo4387a("setting mp state [" + this.f2996g + "] -> [" + upgqdbbsrl + "]");
        this.f2996g = upgqdbbsrl;
    }

    /* renamed from: a */
    public final void m3346a() {
        zoToeBNOjF.m3367b(this);
    }

    /* renamed from: a */
    public final void m3347a(int i) {
        this.f2995f.seekTo(100);
    }

    /* renamed from: a */
    public final void m3348a(Context context) {
        try {
            EmkjBpiUfq.m2104b(context, this.f2997h, new C11605(this));
        } catch (IllegalStateException e) {
            f2989i.mo4387a("Cannot set state to " + upgqDBbsrL.PREPARING + ", when state is " + this.f2996g);
        }
    }

    /* renamed from: a */
    public final void m3349a(OnCompletionListener onCompletionListener) {
        this.f2992c = onCompletionListener;
    }

    /* renamed from: a */
    public final void m3350a(OnErrorListener onErrorListener) {
        this.f2993d = onErrorListener;
    }

    /* renamed from: a */
    public final void m3351a(OnPreparedListener onPreparedListener) {
        this.f2991b = onPreparedListener;
    }

    /* renamed from: a */
    public final void m3352a(OnSeekCompleteListener onSeekCompleteListener) {
        this.f2994e = onSeekCompleteListener;
    }

    /* renamed from: a */
    public final void m3353a(Surface surface) {
        this.f2995f.setSurface(surface);
    }

    /* renamed from: a */
    public final void m3354a(jjbQypPegg jjbqyppegg) {
        this.f2990a = jjbqyppegg;
    }

    /* renamed from: a */
    public final void m3355a(String str) {
        this.f2997h = str;
        m3339a(upgqDBbsrL.INITIALIZED);
    }

    /* renamed from: b */
    public final void m3356b() {
        try {
            m3339a(upgqDBbsrL.STARTED);
            this.f2995f.start();
        } catch (IllegalStateException e) {
            f2989i.mo4387a("Cannot set state to " + upgqDBbsrL.STARTED + ", when state is " + this.f2996g);
        }
    }

    /* renamed from: c */
    public final boolean m3357c() {
        return this.f2996g == upgqDBbsrL.STARTED;
    }

    /* renamed from: d */
    public final void m3358d() {
        zoToeBNOjF.m3366a(this);
        this.f2995f.release();
        m3339a(upgqDBbsrL.END);
    }

    /* renamed from: e */
    public final void m3359e() {
        this.f2995f.stop();
        m3339a(upgqDBbsrL.STOPPED);
        if (this.f2990a != null) {
            this.f2990a.mo4741a();
        }
    }

    /* renamed from: f */
    public final int m3360f() {
        return this.f2995f.getVideoHeight();
    }

    /* renamed from: g */
    public final int m3361g() {
        return this.f2995f.getVideoWidth();
    }
}
