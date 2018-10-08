package com.unity3d.player;

import android.view.Choreographer;
import android.view.Choreographer.FrameCallback;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

/* renamed from: com.unity3d.player.l */
public final class C0767l implements C0758h {
    /* renamed from: a */
    private Choreographer f521a = null;
    /* renamed from: b */
    private long f522b = 0;
    /* renamed from: c */
    private FrameCallback f523c;
    /* renamed from: d */
    private Lock f524d = new ReentrantLock();

    /* renamed from: a */
    public final void mo4201a() {
        this.f524d.lock();
        if (this.f521a != null) {
            this.f521a.removeFrameCallback(this.f523c);
        }
        this.f521a = null;
        this.f524d.unlock();
    }

    /* renamed from: a */
    public final void mo4202a(final UnityPlayer unityPlayer) {
        this.f524d.lock();
        if (this.f521a == null) {
            this.f521a = Choreographer.getInstance();
            if (this.f521a != null) {
                C0768m.Log(4, "Choreographer available: Enabling VSYNC timing");
                this.f523c = new FrameCallback(this) {
                    /* renamed from: b */
                    final /* synthetic */ C0767l f520b;

                    public final void doFrame(long j) {
                        UnityPlayer.lockNativeAccess();
                        if (C0782v.m545c()) {
                            unityPlayer.nativeAddVSyncTime(j);
                        }
                        UnityPlayer.unlockNativeAccess();
                        this.f520b.f524d.lock();
                        if (this.f520b.f521a != null) {
                            this.f520b.f521a.postFrameCallback(this.f520b.f523c);
                        }
                        this.f520b.f524d.unlock();
                    }
                };
                this.f521a.postFrameCallback(this.f523c);
            }
        }
        this.f524d.unlock();
    }
}
