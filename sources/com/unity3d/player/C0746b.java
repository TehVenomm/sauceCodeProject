package com.unity3d.player;

import android.app.Activity;
import android.view.SurfaceHolder;
import android.view.SurfaceHolder.Callback;
import android.view.SurfaceView;

/* renamed from: com.unity3d.player.b */
abstract class C0746b implements Callback {
    /* renamed from: a */
    private final Activity f483a = ((Activity) C0780t.f564a.m537a());
    /* renamed from: b */
    private final int f484b = 3;
    /* renamed from: c */
    private SurfaceView f485c;

    /* renamed from: com.unity3d.player.b$1 */
    final class C07491 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ C0746b f499a;

        C07491(C0746b c0746b) {
            this.f499a = c0746b;
        }

        public final void run() {
            if (this.f499a.f485c == null) {
                this.f499a.f485c = new SurfaceView(C0780t.f564a.m537a());
                this.f499a.f485c.getHolder().setType(this.f499a.f484b);
                this.f499a.f485c.getHolder().addCallback(this.f499a);
                C0780t.f564a.m538a(this.f499a.f485c);
                this.f499a.f485c.setVisibility(0);
            }
        }
    }

    /* renamed from: com.unity3d.player.b$2 */
    final class C07502 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ C0746b f500a;

        C07502(C0746b c0746b) {
            this.f500a = c0746b;
        }

        public final void run() {
            if (this.f500a.f485c != null) {
                C0780t.f564a.m539b(this.f500a.f485c);
            }
            this.f500a.f485c = null;
        }
    }

    C0746b(int i) {
    }

    /* renamed from: a */
    final void m461a() {
        this.f483a.runOnUiThread(new C07491(this));
    }

    /* renamed from: b */
    final void m462b() {
        this.f483a.runOnUiThread(new C07502(this));
    }

    public void surfaceChanged(SurfaceHolder surfaceHolder, int i, int i2, int i3) {
    }

    public void surfaceDestroyed(SurfaceHolder surfaceHolder) {
    }
}
