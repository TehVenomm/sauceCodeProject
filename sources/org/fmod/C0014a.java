package org.fmod;

import android.media.AudioRecord;
import android.util.Log;
import java.nio.ByteBuffer;

/* renamed from: org.fmod.a */
final class C0014a implements Runnable {
    /* renamed from: a */
    private final FMODAudioDevice f19a;
    /* renamed from: b */
    private final ByteBuffer f20b;
    /* renamed from: c */
    private final int f21c;
    /* renamed from: d */
    private final int f22d;
    /* renamed from: e */
    private final int f23e = 2;
    /* renamed from: f */
    private volatile Thread f24f;
    /* renamed from: g */
    private volatile boolean f25g;
    /* renamed from: h */
    private AudioRecord f26h;
    /* renamed from: i */
    private boolean f27i;

    C0014a(FMODAudioDevice fMODAudioDevice, int i, int i2) {
        this.f19a = fMODAudioDevice;
        this.f21c = i;
        this.f22d = i2;
        this.f20b = ByteBuffer.allocateDirect(AudioRecord.getMinBufferSize(i, i2, 2));
    }

    /* renamed from: d */
    private void m0d() {
        if (this.f26h != null) {
            if (this.f26h.getState() == 1) {
                this.f26h.stop();
            }
            this.f26h.release();
            this.f26h = null;
        }
        this.f20b.position(0);
        this.f27i = false;
    }

    /* renamed from: a */
    public final int m1a() {
        return this.f20b.capacity();
    }

    /* renamed from: b */
    public final void m2b() {
        if (this.f24f != null) {
            m3c();
        }
        this.f25g = true;
        this.f24f = new Thread(this);
        this.f24f.start();
    }

    /* renamed from: c */
    public final void m3c() {
        while (this.f24f != null) {
            this.f25g = false;
            try {
                this.f24f.join();
                this.f24f = null;
            } catch (InterruptedException e) {
            }
        }
    }

    public final void run() {
        int i = 3;
        while (this.f25g) {
            int i2;
            if (!this.f27i && i > 0) {
                m0d();
                this.f26h = new AudioRecord(1, this.f21c, this.f22d, this.f23e, this.f20b.capacity());
                this.f27i = this.f26h.getState() == 1;
                if (this.f27i) {
                    this.f20b.position(0);
                    this.f26h.startRecording();
                    i2 = 3;
                    if (this.f27i || this.f26h.getRecordingState() != 3) {
                        i = i2;
                    } else {
                        this.f19a.fmodProcessMicData(this.f20b, this.f26h.read(this.f20b, this.f20b.capacity()));
                        this.f20b.position(0);
                        i = i2;
                    }
                } else {
                    Log.e("FMOD", "AudioRecord failed to initialize (status " + this.f26h.getState() + ")");
                    i--;
                    m0d();
                }
            }
            i2 = i;
            if (this.f27i) {
            }
            i = i2;
        }
        m0d();
    }
}
