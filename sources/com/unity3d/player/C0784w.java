package com.unity3d.player;

import android.content.Context;
import android.content.res.AssetFileDescriptor;
import android.media.MediaPlayer;
import android.media.MediaPlayer.OnBufferingUpdateListener;
import android.media.MediaPlayer.OnCompletionListener;
import android.media.MediaPlayer.OnPreparedListener;
import android.media.MediaPlayer.OnVideoSizeChangedListener;
import android.net.Uri;
import android.util.Log;
import android.view.Display;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceHolder.Callback;
import android.view.SurfaceView;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.FrameLayout.LayoutParams;
import android.widget.MediaController;
import android.widget.MediaController.MediaPlayerControl;
import java.io.FileInputStream;
import java.io.IOException;

/* renamed from: com.unity3d.player.w */
public final class C0784w extends FrameLayout implements OnBufferingUpdateListener, OnCompletionListener, OnPreparedListener, OnVideoSizeChangedListener, Callback, MediaPlayerControl {
    /* renamed from: a */
    private static boolean f576a = false;
    /* renamed from: b */
    private final UnityPlayer f577b;
    /* renamed from: c */
    private final Context f578c;
    /* renamed from: d */
    private final SurfaceView f579d;
    /* renamed from: e */
    private final SurfaceHolder f580e;
    /* renamed from: f */
    private final String f581f;
    /* renamed from: g */
    private final int f582g;
    /* renamed from: h */
    private final int f583h;
    /* renamed from: i */
    private final boolean f584i;
    /* renamed from: j */
    private final long f585j;
    /* renamed from: k */
    private final long f586k;
    /* renamed from: l */
    private final FrameLayout f587l;
    /* renamed from: m */
    private final Display f588m;
    /* renamed from: n */
    private int f589n;
    /* renamed from: o */
    private int f590o;
    /* renamed from: p */
    private int f591p;
    /* renamed from: q */
    private int f592q;
    /* renamed from: r */
    private MediaPlayer f593r;
    /* renamed from: s */
    private MediaController f594s;
    /* renamed from: t */
    private boolean f595t = false;
    /* renamed from: u */
    private boolean f596u = false;
    /* renamed from: v */
    private int f597v = 0;
    /* renamed from: w */
    private boolean f598w = false;
    /* renamed from: x */
    private int f599x = 0;
    /* renamed from: y */
    private boolean f600y;

    /* renamed from: com.unity3d.player.w$1 */
    final class C07831 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ C0784w f575a;

        C07831(C0784w c0784w) {
            this.f575a = c0784w;
        }

        public final void run() {
            this.f575a.f577b.hideVideoPlayer();
        }
    }

    protected C0784w(UnityPlayer unityPlayer, Context context, String str, int i, int i2, int i3, boolean z, long j, long j2) {
        super(context);
        this.f577b = unityPlayer;
        this.f578c = context;
        this.f587l = this;
        this.f579d = new SurfaceView(context);
        this.f580e = this.f579d.getHolder();
        this.f580e.addCallback(this);
        this.f580e.setType(3);
        this.f587l.setBackgroundColor(i);
        this.f587l.addView(this.f579d);
        this.f588m = ((WindowManager) this.f578c.getSystemService("window")).getDefaultDisplay();
        this.f581f = str;
        this.f582g = i2;
        this.f583h = i3;
        this.f584i = z;
        this.f585j = j;
        this.f586k = j2;
        if (f576a) {
            C0784w.m555a("fileName: " + this.f581f);
        }
        if (f576a) {
            C0784w.m555a("backgroundColor: " + i);
        }
        if (f576a) {
            C0784w.m555a("controlMode: " + this.f582g);
        }
        if (f576a) {
            C0784w.m555a("scalingMode: " + this.f583h);
        }
        if (f576a) {
            C0784w.m555a("isURL: " + this.f584i);
        }
        if (f576a) {
            C0784w.m555a("videoOffset: " + this.f585j);
        }
        if (f576a) {
            C0784w.m555a("videoLength: " + this.f586k);
        }
        setFocusable(true);
        setFocusableInTouchMode(true);
        this.f600y = true;
    }

    /* renamed from: a */
    private void m554a() {
        doCleanUp();
        try {
            this.f593r = new MediaPlayer();
            if (this.f584i) {
                this.f593r.setDataSource(this.f578c, Uri.parse(this.f581f));
            } else if (this.f586k != 0) {
                FileInputStream fileInputStream = new FileInputStream(this.f581f);
                this.f593r.setDataSource(fileInputStream.getFD(), this.f585j, this.f586k);
                fileInputStream.close();
            } else {
                try {
                    AssetFileDescriptor openFd = getResources().getAssets().openFd(this.f581f);
                    this.f593r.setDataSource(openFd.getFileDescriptor(), openFd.getStartOffset(), openFd.getLength());
                    openFd.close();
                } catch (IOException e) {
                    FileInputStream fileInputStream2 = new FileInputStream(this.f581f);
                    this.f593r.setDataSource(fileInputStream2.getFD());
                    fileInputStream2.close();
                }
            }
            this.f593r.setDisplay(this.f580e);
            this.f593r.setScreenOnWhilePlaying(true);
            this.f593r.setOnBufferingUpdateListener(this);
            this.f593r.setOnCompletionListener(this);
            this.f593r.setOnPreparedListener(this);
            this.f593r.setOnVideoSizeChangedListener(this);
            this.f593r.setAudioStreamType(3);
            this.f593r.prepare();
            if (this.f582g == 0 || this.f582g == 1) {
                this.f594s = new MediaController(this.f578c);
                this.f594s.setMediaPlayer(this);
                this.f594s.setAnchorView(this);
                this.f594s.setEnabled(true);
                this.f594s.show();
            }
        } catch (Exception e2) {
            if (f576a) {
                C0784w.m555a("error: " + e2.getMessage() + e2);
            }
            onDestroy();
        }
    }

    /* renamed from: a */
    private static void m555a(String str) {
        Log.v("Video", "VideoPlayer: " + str);
    }

    /* renamed from: b */
    private void m556b() {
        if (!isPlaying()) {
            if (f576a) {
                C0784w.m555a("startVideoPlayback");
            }
            updateVideoLayout();
            if (!this.f598w) {
                start();
            }
        }
    }

    public final boolean canPause() {
        return true;
    }

    public final boolean canSeekBackward() {
        return true;
    }

    public final boolean canSeekForward() {
        return true;
    }

    protected final void doCleanUp() {
        if (this.f593r != null) {
            this.f593r.release();
            this.f593r = null;
        }
        this.f591p = 0;
        this.f592q = 0;
        this.f596u = false;
        this.f595t = false;
    }

    public final int getBufferPercentage() {
        return this.f584i ? this.f597v : 100;
    }

    public final int getCurrentPosition() {
        return this.f593r == null ? 0 : this.f593r.getCurrentPosition();
    }

    public final int getDuration() {
        return this.f593r == null ? 0 : this.f593r.getDuration();
    }

    public final boolean isPlaying() {
        boolean z = this.f596u && this.f595t;
        return this.f593r == null ? !z : this.f593r.isPlaying() || !z;
    }

    public final void onBufferingUpdate(MediaPlayer mediaPlayer, int i) {
        if (f576a) {
            C0784w.m555a("onBufferingUpdate percent:" + i);
        }
        this.f597v = i;
    }

    public final void onCompletion(MediaPlayer mediaPlayer) {
        if (f576a) {
            C0784w.m555a("onCompletion called");
        }
        onDestroy();
    }

    public final void onControllerHide() {
    }

    protected final void onDestroy() {
        onPause();
        doCleanUp();
        UnityPlayer.m426a(new C07831(this));
    }

    public final boolean onKeyDown(int i, KeyEvent keyEvent) {
        if (i != 4 && (this.f582g != 2 || i == 0 || keyEvent.isSystem())) {
            return this.f594s != null ? this.f594s.onKeyDown(i, keyEvent) : super.onKeyDown(i, keyEvent);
        } else {
            onDestroy();
            return true;
        }
    }

    protected final void onPause() {
        if (f576a) {
            C0784w.m555a("onPause called");
        }
        if (!this.f598w) {
            pause();
            this.f598w = false;
        }
        if (this.f593r != null) {
            this.f599x = this.f593r.getCurrentPosition();
        }
        this.f600y = false;
    }

    public final void onPrepared(MediaPlayer mediaPlayer) {
        if (f576a) {
            C0784w.m555a("onPrepared called");
        }
        this.f596u = true;
        if (this.f596u && this.f595t) {
            m556b();
        }
    }

    protected final void onResume() {
        if (f576a) {
            C0784w.m555a("onResume called");
        }
        if (!(this.f600y || this.f598w)) {
            start();
        }
        this.f600y = true;
    }

    public final boolean onTouchEvent(MotionEvent motionEvent) {
        int action = motionEvent.getAction();
        if (this.f582g != 2 || (action & 255) != 0) {
            return this.f594s != null ? this.f594s.onTouchEvent(motionEvent) : super.onTouchEvent(motionEvent);
        } else {
            onDestroy();
            return true;
        }
    }

    public final void onVideoSizeChanged(MediaPlayer mediaPlayer, int i, int i2) {
        if (f576a) {
            C0784w.m555a("onVideoSizeChanged called " + i + "x" + i2);
        }
        if (i != 0 && i2 != 0) {
            this.f595t = true;
            this.f591p = i;
            this.f592q = i2;
            if (this.f596u && this.f595t) {
                m556b();
            }
        } else if (f576a) {
            C0784w.m555a("invalid video width(" + i + ") or height(" + i2 + ")");
        }
    }

    public final void pause() {
        if (this.f593r != null) {
            this.f593r.pause();
            this.f598w = true;
        }
    }

    public final void seekTo(int i) {
        if (this.f593r != null) {
            this.f593r.seekTo(i);
        }
    }

    public final void start() {
        if (this.f593r != null) {
            this.f593r.start();
            this.f598w = false;
        }
    }

    public final void surfaceChanged(SurfaceHolder surfaceHolder, int i, int i2, int i3) {
        if (f576a) {
            C0784w.m555a("surfaceChanged called " + i + " " + i2 + "x" + i3);
        }
        if (this.f589n != i2 || this.f590o != i3) {
            this.f589n = i2;
            this.f590o = i3;
            updateVideoLayout();
        }
    }

    public final void surfaceCreated(SurfaceHolder surfaceHolder) {
        if (f576a) {
            C0784w.m555a("surfaceCreated called");
        }
        m554a();
        seekTo(this.f599x);
    }

    public final void surfaceDestroyed(SurfaceHolder surfaceHolder) {
        if (f576a) {
            C0784w.m555a("surfaceDestroyed called");
        }
        doCleanUp();
    }

    protected final void updateVideoLayout() {
        if (f576a) {
            C0784w.m555a("updateVideoLayout");
        }
        if (this.f589n == 0 || this.f590o == 0) {
            WindowManager windowManager = (WindowManager) this.f578c.getSystemService("window");
            this.f589n = windowManager.getDefaultDisplay().getWidth();
            this.f590o = windowManager.getDefaultDisplay().getHeight();
        }
        int i = this.f589n;
        int i2 = this.f590o;
        float f = ((float) this.f591p) / ((float) this.f592q);
        float f2 = ((float) this.f589n) / ((float) this.f590o);
        if (this.f583h == 1) {
            if (f2 <= f) {
                i2 = (int) (((float) this.f589n) / f);
            } else {
                i = (int) (((float) this.f590o) * f);
            }
        } else if (this.f583h == 2) {
            if (f2 >= f) {
                i2 = (int) (((float) this.f589n) / f);
            } else {
                i = (int) (((float) this.f590o) * f);
            }
        } else if (this.f583h == 0) {
            i = this.f591p;
            i2 = this.f592q;
        }
        if (f576a) {
            C0784w.m555a("frameWidth = " + i + "; frameHeight = " + i2);
        }
        this.f587l.updateViewLayout(this.f579d, new LayoutParams(i, i2, 17));
    }
}
