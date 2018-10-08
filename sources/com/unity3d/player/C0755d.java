package com.unity3d.player;

import android.graphics.SurfaceTexture;
import android.hardware.Camera;
import android.os.Handler;
import android.view.View;
import android.view.View.OnSystemUiVisibilityChangeListener;

/* renamed from: com.unity3d.player.d */
public final class C0755d implements C0754f {
    /* renamed from: a */
    private static final SurfaceTexture f505a = new SurfaceTexture(-1);
    /* renamed from: b */
    private static final int f506b = (C0773q.f540f ? 5894 : 1);
    /* renamed from: c */
    private volatile boolean f507c;

    /* renamed from: a */
    private void m481a(final View view, int i) {
        Handler handler = view.getHandler();
        if (handler == null) {
            mo4195a(view, this.f507c);
        } else {
            handler.postDelayed(new Runnable(this) {
                /* renamed from: b */
                final /* synthetic */ C0755d f504b;

                public final void run() {
                    this.f504b.mo4195a(view, this.f504b.f507c);
                }
            }, 1000);
        }
    }

    /* renamed from: a */
    public final void mo4194a(final View view) {
        if (!C0773q.f540f) {
            view.setOnSystemUiVisibilityChangeListener(new OnSystemUiVisibilityChangeListener(this) {
                /* renamed from: b */
                final /* synthetic */ C0755d f502b;

                public final void onSystemUiVisibilityChange(int i) {
                    this.f502b.m481a(view, 1000);
                }
            });
        }
    }

    /* renamed from: a */
    public final void mo4195a(View view, boolean z) {
        this.f507c = z;
        view.setSystemUiVisibility(this.f507c ? view.getSystemUiVisibility() | f506b : view.getSystemUiVisibility() & (f506b ^ -1));
    }

    /* renamed from: a */
    public final boolean mo4196a(Camera camera) {
        try {
            camera.setPreviewTexture(f505a);
            return true;
        } catch (Exception e) {
            return false;
        }
    }

    /* renamed from: b */
    public final void mo4197b(View view) {
        if (!C0773q.f540f && this.f507c) {
            mo4195a(view, false);
            this.f507c = true;
        }
        m481a(view, 1000);
    }
}
