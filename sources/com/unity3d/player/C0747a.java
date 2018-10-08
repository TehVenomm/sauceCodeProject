package com.unity3d.player;

import android.graphics.ImageFormat;
import android.hardware.Camera;
import android.hardware.Camera.Parameters;
import android.hardware.Camera.PreviewCallback;
import android.hardware.Camera.Size;
import android.view.SurfaceHolder;
import java.util.ArrayList;
import java.util.List;
import jp.colopl.util.ImageUtil;

/* renamed from: com.unity3d.player.a */
final class C0747a {
    /* renamed from: a */
    Camera f488a;
    /* renamed from: b */
    Parameters f489b;
    /* renamed from: c */
    Size f490c;
    /* renamed from: d */
    int f491d;
    /* renamed from: e */
    int[] f492e;
    /* renamed from: f */
    C0745b f493f;
    /* renamed from: g */
    private final Object[] f494g = new Object[0];
    /* renamed from: h */
    private final int f495h;
    /* renamed from: i */
    private final int f496i;
    /* renamed from: j */
    private final int f497j;
    /* renamed from: k */
    private final int f498k;

    /* renamed from: com.unity3d.player.a$a */
    interface C0743a {
        void onCameraFrame(C0747a c0747a, byte[] bArr);
    }

    /* renamed from: com.unity3d.player.a$2 */
    final class C07462 extends C0745b {
        /* renamed from: a */
        Camera f486a = this.f487b.f488a;
        /* renamed from: b */
        final /* synthetic */ C0747a f487b;

        C07462(C0747a c0747a) {
            this.f487b = c0747a;
            super(3);
        }

        public final void surfaceCreated(SurfaceHolder surfaceHolder) {
            synchronized (this.f487b.f494g) {
                if (this.f487b.f488a != this.f486a) {
                    return;
                }
                try {
                    this.f487b.f488a.setPreviewDisplay(surfaceHolder);
                    this.f487b.f488a.startPreview();
                } catch (Exception e) {
                    C0767m.Log(6, "Unable to initialize webcam data stream: " + e.getMessage());
                }
            }
        }

        public final void surfaceDestroyed(SurfaceHolder surfaceHolder) {
            synchronized (this.f487b.f494g) {
                if (this.f487b.f488a != this.f486a) {
                    return;
                }
                this.f487b.f488a.stopPreview();
            }
        }
    }

    public C0747a(int i, int i2, int i3, int i4) {
        this.f495h = i;
        this.f496i = C0747a.m463a(i2, ImageUtil.SCALE_DOWN_SIZE);
        this.f497j = C0747a.m463a(i3, ImageUtil.THUMBNAIL_MAX_SIZE);
        this.f498k = C0747a.m463a(i4, 24);
    }

    /* renamed from: a */
    private static final int m463a(int i, int i2) {
        return i != 0 ? i : i2;
    }

    /* renamed from: a */
    private static void m464a(Parameters parameters) {
        if (parameters.getSupportedColorEffects() != null) {
            parameters.setColorEffect("none");
        }
        if (parameters.getSupportedFocusModes().contains("continuous-video")) {
            parameters.setFocusMode("continuous-video");
        }
    }

    /* renamed from: b */
    private void m466b(final C0743a c0743a) {
        synchronized (this.f494g) {
            this.f488a = Camera.open(this.f495h);
            this.f489b = this.f488a.getParameters();
            this.f490c = m469f();
            this.f492e = m468e();
            this.f491d = m467d();
            C0747a.m464a(this.f489b);
            this.f489b.setPreviewSize(this.f490c.width, this.f490c.height);
            this.f489b.setPreviewFpsRange(this.f492e[0], this.f492e[1]);
            this.f488a.setParameters(this.f489b);
            PreviewCallback c07441 = new PreviewCallback(this) {
                /* renamed from: a */
                long f480a = 0;
                /* renamed from: c */
                final /* synthetic */ C0747a f482c;

                public final void onPreviewFrame(byte[] bArr, Camera camera) {
                    if (this.f482c.f488a == camera) {
                        c0743a.onCameraFrame(this.f482c, bArr);
                    }
                }
            };
            int i = (((this.f490c.width * this.f490c.height) * this.f491d) / 8) + 4096;
            this.f488a.addCallbackBuffer(new byte[i]);
            this.f488a.addCallbackBuffer(new byte[i]);
            this.f488a.setPreviewCallbackWithBuffer(c07441);
        }
    }

    /* renamed from: d */
    private final int m467d() {
        this.f489b.setPreviewFormat(17);
        return ImageFormat.getBitsPerPixel(17);
    }

    /* renamed from: e */
    private final int[] m468e() {
        double d = (double) (this.f498k * 1000);
        List supportedPreviewFpsRange = this.f489b.getSupportedPreviewFpsRange();
        if (supportedPreviewFpsRange == null) {
            supportedPreviewFpsRange = new ArrayList();
        }
        int[] iArr = new int[]{this.f498k * 1000, this.f498k * 1000};
        double d2 = Double.MAX_VALUE;
        for (int[] iArr2 : r0) {
            int[] iArr22;
            double abs = Math.abs(Math.log(d / ((double) iArr22[0]))) + Math.abs(Math.log(d / ((double) iArr22[1])));
            if (abs < d2) {
                d2 = abs;
            } else {
                iArr22 = iArr;
            }
            iArr = iArr22;
        }
        return iArr;
    }

    /* renamed from: f */
    private final Size m469f() {
        double d = (double) this.f496i;
        double d2 = (double) this.f497j;
        Size size = null;
        double d3 = Double.MAX_VALUE;
        for (Size size2 : this.f489b.getSupportedPreviewSizes()) {
            Size size22;
            double abs = Math.abs(Math.log(d / ((double) size22.width))) + Math.abs(Math.log(d2 / ((double) size22.height)));
            if (abs >= d3) {
                size22 = size;
                abs = d3;
            }
            size = size22;
            d3 = abs;
        }
        return size;
    }

    /* renamed from: a */
    public final int m470a() {
        return this.f495h;
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    /* renamed from: a */
    public final void m471a(com.unity3d.player.C0747a.C0743a r4) {
        /*
        r3 = this;
        r1 = r3.f494g;
        monitor-enter(r1);
        r0 = r3.f488a;	 Catch:{ all -> 0x0031 }
        if (r0 != 0) goto L_0x000a;
    L_0x0007:
        r3.m466b(r4);	 Catch:{ all -> 0x0031 }
    L_0x000a:
        r0 = com.unity3d.player.C0773q.f535a;	 Catch:{ all -> 0x0031 }
        if (r0 == 0) goto L_0x001f;
    L_0x000e:
        r0 = com.unity3d.player.C0773q.f543i;	 Catch:{ all -> 0x0031 }
        r2 = r3.f488a;	 Catch:{ all -> 0x0031 }
        r0 = r0.mo4196a(r2);	 Catch:{ all -> 0x0031 }
        if (r0 == 0) goto L_0x001f;
    L_0x0018:
        r0 = r3.f488a;	 Catch:{ all -> 0x0031 }
        r0.startPreview();	 Catch:{ all -> 0x0031 }
        monitor-exit(r1);	 Catch:{ all -> 0x0031 }
    L_0x001e:
        return;
    L_0x001f:
        r0 = r3.f493f;	 Catch:{ all -> 0x0031 }
        if (r0 != 0) goto L_0x002f;
    L_0x0023:
        r0 = new com.unity3d.player.a$2;	 Catch:{ all -> 0x0031 }
        r0.<init>(r3);	 Catch:{ all -> 0x0031 }
        r3.f493f = r0;	 Catch:{ all -> 0x0031 }
        r0 = r3.f493f;	 Catch:{ all -> 0x0031 }
        r0.m461a();	 Catch:{ all -> 0x0031 }
    L_0x002f:
        monitor-exit(r1);	 Catch:{ all -> 0x0031 }
        goto L_0x001e;
    L_0x0031:
        r0 = move-exception;
        monitor-exit(r1);	 Catch:{ all -> 0x0031 }
        throw r0;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.unity3d.player.a.a(com.unity3d.player.a$a):void");
    }

    /* renamed from: a */
    public final void m472a(byte[] bArr) {
        synchronized (this.f494g) {
            if (this.f488a != null) {
                this.f488a.addCallbackBuffer(bArr);
            }
        }
    }

    /* renamed from: b */
    public final Size m473b() {
        return this.f490c;
    }

    /* renamed from: c */
    public final void m474c() {
        synchronized (this.f494g) {
            if (this.f488a != null) {
                this.f488a.setPreviewCallbackWithBuffer(null);
                this.f488a.stopPreview();
                this.f488a.release();
                this.f488a = null;
            }
            if (this.f493f != null) {
                this.f493f.m462b();
                this.f493f = null;
            }
        }
    }
}
