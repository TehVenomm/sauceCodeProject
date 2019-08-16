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

/* renamed from: com.unity3d.player.n */
public final class C1119n extends FrameLayout implements OnBufferingUpdateListener, OnCompletionListener, OnPreparedListener, OnVideoSizeChangedListener, Callback, MediaPlayerControl {
    /* access modifiers changed from: private */

    /* renamed from: a */
    public static boolean f614a = false;

    /* renamed from: b */
    private final Context f615b;

    /* renamed from: c */
    private final SurfaceView f616c;

    /* renamed from: d */
    private final SurfaceHolder f617d;

    /* renamed from: e */
    private final String f618e;

    /* renamed from: f */
    private final int f619f;

    /* renamed from: g */
    private final int f620g;

    /* renamed from: h */
    private final boolean f621h;

    /* renamed from: i */
    private final long f622i;

    /* renamed from: j */
    private final long f623j;

    /* renamed from: k */
    private final FrameLayout f624k;

    /* renamed from: l */
    private final Display f625l;

    /* renamed from: m */
    private int f626m;

    /* renamed from: n */
    private int f627n;

    /* renamed from: o */
    private int f628o;

    /* renamed from: p */
    private int f629p;

    /* renamed from: q */
    private MediaPlayer f630q;

    /* renamed from: r */
    private MediaController f631r;

    /* renamed from: s */
    private boolean f632s = false;

    /* renamed from: t */
    private boolean f633t = false;

    /* renamed from: u */
    private int f634u = 0;

    /* renamed from: v */
    private boolean f635v = false;

    /* renamed from: w */
    private boolean f636w = false;

    /* renamed from: x */
    private C1120a f637x;

    /* renamed from: y */
    private C1121b f638y;

    /* renamed from: z */
    private volatile int f639z = 0;

    /* renamed from: com.unity3d.player.n$a */
    public interface C1120a {
        /* renamed from: a */
        void mo20433a(int i);
    }

    /* renamed from: com.unity3d.player.n$b */
    public final class C1121b implements Runnable {

        /* renamed from: b */
        private C1119n f641b;

        /* renamed from: c */
        private boolean f642c = false;

        public C1121b(C1119n nVar) {
            this.f641b = nVar;
        }

        /* renamed from: a */
        public final void mo20564a() {
            this.f642c = true;
        }

        public final void run() {
            try {
                Thread.sleep(5000);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
            if (!this.f642c) {
                if (C1119n.f614a) {
                    C1119n.m563b("Stopping the video player due to timeout.");
                }
                this.f641b.CancelOnPrepare();
            }
        }
    }

    protected C1119n(Context context, String str, int i, int i2, int i3, boolean z, long j, long j2, C1120a aVar) {
        super(context);
        this.f637x = aVar;
        this.f615b = context;
        this.f624k = this;
        this.f616c = new SurfaceView(context);
        this.f617d = this.f616c.getHolder();
        this.f617d.addCallback(this);
        this.f617d.setType(3);
        this.f624k.setBackgroundColor(i);
        this.f624k.addView(this.f616c);
        this.f625l = ((WindowManager) this.f615b.getSystemService("window")).getDefaultDisplay();
        this.f618e = str;
        this.f619f = i2;
        this.f620g = i3;
        this.f621h = z;
        this.f622i = j;
        this.f623j = j2;
        if (f614a) {
            m563b("fileName: " + this.f618e);
        }
        if (f614a) {
            m563b("backgroundColor: " + i);
        }
        if (f614a) {
            m563b("controlMode: " + this.f619f);
        }
        if (f614a) {
            m563b("scalingMode: " + this.f620g);
        }
        if (f614a) {
            m563b("isURL: " + this.f621h);
        }
        if (f614a) {
            m563b("videoOffset: " + this.f622i);
        }
        if (f614a) {
            m563b("videoLength: " + this.f623j);
        }
        setFocusable(true);
        setFocusableInTouchMode(true);
    }

    /* renamed from: a */
    private void m561a(int i) {
        this.f639z = i;
        if (this.f637x != null) {
            this.f637x.mo20433a(this.f639z);
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: b */
    public static void m563b(String str) {
        Log.i("Video", "VideoPlayer: " + str);
    }

    /* renamed from: c */
    private void m565c() {
        if (this.f630q != null) {
            this.f630q.setDisplay(this.f617d);
            if (!this.f635v) {
                if (f614a) {
                    m563b("Resuming playback");
                }
                this.f630q.start();
                return;
            }
            return;
        }
        m561a(0);
        doCleanUp();
        try {
            this.f630q = new MediaPlayer();
            if (this.f621h) {
                this.f630q.setDataSource(this.f615b, Uri.parse(this.f618e));
            } else if (this.f623j != 0) {
                FileInputStream fileInputStream = new FileInputStream(this.f618e);
                this.f630q.setDataSource(fileInputStream.getFD(), this.f622i, this.f623j);
                fileInputStream.close();
            } else {
                try {
                    AssetFileDescriptor openFd = getResources().getAssets().openFd(this.f618e);
                    this.f630q.setDataSource(openFd.getFileDescriptor(), openFd.getStartOffset(), openFd.getLength());
                    openFd.close();
                } catch (IOException e) {
                    FileInputStream fileInputStream2 = new FileInputStream(this.f618e);
                    this.f630q.setDataSource(fileInputStream2.getFD());
                    fileInputStream2.close();
                }
            }
            this.f630q.setDisplay(this.f617d);
            this.f630q.setScreenOnWhilePlaying(true);
            this.f630q.setOnBufferingUpdateListener(this);
            this.f630q.setOnCompletionListener(this);
            this.f630q.setOnPreparedListener(this);
            this.f630q.setOnVideoSizeChangedListener(this);
            this.f630q.setAudioStreamType(3);
            this.f630q.prepareAsync();
            this.f638y = new C1121b(this);
            new Thread(this.f638y).start();
        } catch (Exception e2) {
            if (f614a) {
                m563b("error: " + e2.getMessage() + e2);
            }
            m561a(2);
        }
    }

    /* renamed from: d */
    private void m566d() {
        if (!isPlaying()) {
            m561a(1);
            if (f614a) {
                m563b("startVideoPlayback");
            }
            updateVideoLayout();
            if (!this.f635v) {
                start();
            }
        }
    }

    public final void CancelOnPrepare() {
        m561a(2);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: a */
    public final boolean mo20541a() {
        return this.f635v;
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

    /* access modifiers changed from: protected */
    public final void destroyPlayer() {
        if (f614a) {
            m563b("destroyPlayer");
        }
        if (!this.f635v) {
            pause();
        }
        doCleanUp();
    }

    /* access modifiers changed from: protected */
    public final void doCleanUp() {
        if (this.f638y != null) {
            this.f638y.mo20564a();
            this.f638y = null;
        }
        if (this.f630q != null) {
            this.f630q.release();
            this.f630q = null;
        }
        this.f628o = 0;
        this.f629p = 0;
        this.f633t = false;
        this.f632s = false;
    }

    public final int getBufferPercentage() {
        if (this.f621h) {
            return this.f634u;
        }
        return 100;
    }

    public final int getCurrentPosition() {
        if (this.f630q == null) {
            return 0;
        }
        return this.f630q.getCurrentPosition();
    }

    public final int getDuration() {
        if (this.f630q == null) {
            return 0;
        }
        return this.f630q.getDuration();
    }

    public final boolean isPlaying() {
        boolean z = this.f633t && this.f632s;
        return this.f630q == null ? !z : this.f630q.isPlaying() || !z;
    }

    public final void onBufferingUpdate(MediaPlayer mediaPlayer, int i) {
        if (f614a) {
            m563b("onBufferingUpdate percent:" + i);
        }
        this.f634u = i;
    }

    public final void onCompletion(MediaPlayer mediaPlayer) {
        if (f614a) {
            m563b("onCompletion called");
        }
        destroyPlayer();
        m561a(3);
    }

    public final boolean onKeyDown(int i, KeyEvent keyEvent) {
        if (i != 4 && (this.f619f != 2 || i == 0 || keyEvent.isSystem())) {
            return this.f631r != null ? this.f631r.onKeyDown(i, keyEvent) : super.onKeyDown(i, keyEvent);
        }
        destroyPlayer();
        m561a(3);
        return true;
    }

    public final void onPrepared(MediaPlayer mediaPlayer) {
        if (f614a) {
            m563b("onPrepared called");
        }
        if (this.f638y != null) {
            this.f638y.mo20564a();
            this.f638y = null;
        }
        if (this.f619f == 0 || this.f619f == 1) {
            this.f631r = new MediaController(this.f615b);
            this.f631r.setMediaPlayer(this);
            this.f631r.setAnchorView(this);
            this.f631r.setEnabled(true);
            this.f631r.show();
        }
        this.f633t = true;
        if (this.f633t && this.f632s) {
            m566d();
        }
    }

    public final boolean onTouchEvent(MotionEvent motionEvent) {
        int action = motionEvent.getAction();
        if (this.f619f != 2 || (action & 255) != 0) {
            return this.f631r != null ? this.f631r.onTouchEvent(motionEvent) : super.onTouchEvent(motionEvent);
        }
        destroyPlayer();
        m561a(3);
        return true;
    }

    public final void onVideoSizeChanged(MediaPlayer mediaPlayer, int i, int i2) {
        if (f614a) {
            m563b("onVideoSizeChanged called " + i + "x" + i2);
        }
        if (i != 0 && i2 != 0) {
            this.f632s = true;
            this.f628o = i;
            this.f629p = i2;
            if (this.f633t && this.f632s) {
                m566d();
            }
        } else if (f614a) {
            m563b("invalid video width(" + i + ") or height(" + i2 + ")");
        }
    }

    public final void pause() {
        if (this.f630q != null) {
            if (this.f636w) {
                this.f630q.pause();
            }
            this.f635v = true;
        }
    }

    public final void seekTo(int i) {
        if (this.f630q != null) {
            this.f630q.seekTo(i);
        }
    }

    public final void start() {
        if (f614a) {
            m563b("Start");
        }
        if (this.f630q != null) {
            if (this.f636w) {
                this.f630q.start();
            }
            this.f635v = false;
        }
    }

    public final void surfaceChanged(SurfaceHolder surfaceHolder, int i, int i2, int i3) {
        if (f614a) {
            m563b("surfaceChanged called " + i + " " + i2 + "x" + i3);
        }
        if (this.f626m != i2 || this.f627n != i3) {
            this.f626m = i2;
            this.f627n = i3;
            if (this.f636w) {
                updateVideoLayout();
            }
        }
    }

    public final void surfaceCreated(SurfaceHolder surfaceHolder) {
        if (f614a) {
            m563b("surfaceCreated called");
        }
        this.f636w = true;
        m565c();
    }

    public final void surfaceDestroyed(SurfaceHolder surfaceHolder) {
        if (f614a) {
            m563b("surfaceDestroyed called");
        }
        this.f636w = false;
    }

    /* access modifiers changed from: protected */
    public final void updateVideoLayout() {
        if (f614a) {
            m563b("updateVideoLayout");
        }
        if (this.f630q != null) {
            if (this.f626m == 0 || this.f627n == 0) {
                WindowManager windowManager = (WindowManager) this.f615b.getSystemService("window");
                this.f626m = windowManager.getDefaultDisplay().getWidth();
                this.f627n = windowManager.getDefaultDisplay().getHeight();
            }
            int i = this.f626m;
            int i2 = this.f627n;
            float f = ((float) this.f628o) / ((float) this.f629p);
            float f2 = ((float) this.f626m) / ((float) this.f627n);
            if (this.f620g == 1) {
                if (f2 <= f) {
                    i2 = (int) (((float) this.f626m) / f);
                } else {
                    i = (int) (((float) this.f627n) * f);
                }
            } else if (this.f620g == 2) {
                if (f2 >= f) {
                    i2 = (int) (((float) this.f626m) / f);
                } else {
                    i = (int) (((float) this.f627n) * f);
                }
            } else if (this.f620g == 0) {
                i = this.f628o;
                i2 = this.f629p;
            }
            if (f614a) {
                m563b("frameWidth = " + i + "; frameHeight = " + i2);
            }
            this.f624k.updateViewLayout(this.f616c, new LayoutParams(i, i2, 17));
        }
    }
}
