package com.unity3d.player;

import android.app.Activity;
import android.view.SurfaceHolder;
import android.view.SurfaceHolder.Callback;
import android.view.SurfaceView;

/* renamed from: com.unity3d.player.b */
abstract class C0745b implements Callback {
    /* renamed from: a */
    private final Activity f483a = ((Activity) C0779t.f564a.m537a());
    /* renamed from: b */
    private final int f484b = 3;
    /* renamed from: c */
    private SurfaceView f485c;

    /* renamed from: com.unity3d.player.b$1 */
    final class C07481 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ C0745b f499a;

        C07481(C0745b c0745b) {
            this.f499a = c0745b;
        }

        public final void run() {
            if (this.f499a.f485c == null) {
                this.f499a.f485c = new SurfaceView(C0779t.f564a.m537a());
                this.f499a.f485c.getHolder().setType(this.f499a.f484b);
                this.f499a.f485c.getHolder().addCallback(this.f499a);
                C0779t.f564a.m538a(this.f499a.f485c);
                this.f499a.f485c.setVisibility(0);
            }
        }
    }

    /* renamed from: com.unity3d.player.b$2 */
    final class C07492 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ C0745b f500a;

        C07492(C0745b c0745b) {
            this.f500a = c0745b;
        }

        public final void run() {
            if (this.f500a.f485c != null) {
                C0779t.f564a.m539b(this.f500a.f485c);
            }
            this.f500a.f485c = null;
        }
    }

    C0745b(int i) {
    }

    /* renamed from: a */
    final void m461a() {
        this.f483a.runOnUiThread(new C07481(this));
    }

    /* renamed from: b */
    final void m462b() {
        this.f483a.runOnUiThread(new C07492(this));
    }

    public void surfaceChanged(SurfaceHolder surfaceHolder, int i, int i2, int i3) {
    }

    public void surfaceDestroyed(SurfaceHolder surfaceHolder) {
    }
}
