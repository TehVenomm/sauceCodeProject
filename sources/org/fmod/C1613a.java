package org.fmod;

import android.media.AudioRecord;
import android.util.Log;
import java.nio.ByteBuffer;

/* renamed from: org.fmod.a */
final class C1613a implements Runnable {
    /* renamed from: a */
    private final FMODAudioDevice f3757a;
    /* renamed from: b */
    private final ByteBuffer f3758b;
    /* renamed from: c */
    private final int f3759c;
    /* renamed from: d */
    private final int f3760d;
    /* renamed from: e */
    private final int f3761e = 2;
    /* renamed from: f */
    private volatile Thread f3762f;
    /* renamed from: g */
    private volatile boolean f3763g;
    /* renamed from: h */
    private AudioRecord f3764h;
    /* renamed from: i */
    private boolean f3765i;

    C1613a(FMODAudioDevice fMODAudioDevice, int i, int i2) {
        this.f3757a = fMODAudioDevice;
        this.f3759c = i;
        this.f3760d = i2;
        this.f3758b = ByteBuffer.allocateDirect(AudioRecord.getMinBufferSize(i, i2, 2));
    }

    /* renamed from: d */
    private void m4021d() {
        if (this.f3764h != null) {
            if (this.f3764h.getState() == 1) {
                this.f3764h.stop();
            }
            this.f3764h.release();
            this.f3764h = null;
        }
        this.f3758b.position(0);
        this.f3765i = false;
    }

    /* renamed from: a */
    public final int m4022a() {
        return this.f3758b.capacity();
    }

    /* renamed from: b */
    public final void m4023b() {
        if (this.f3762f != null) {
            m4024c();
        }
        this.f3763g = true;
        this.f3762f = new Thread(this);
        this.f3762f.start();
    }

    /* renamed from: c */
    public final void m4024c() {
        while (this.f3762f != null) {
            this.f3763g = false;
            try {
                this.f3762f.join();
                this.f3762f = null;
            } catch (InterruptedException e) {
            }
        }
    }

    public final void run() {
        int i = 3;
        while (this.f3763g) {
            int i2;
            if (!this.f3765i && i > 0) {
                m4021d();
                this.f3764h = new AudioRecord(1, this.f3759c, this.f3760d, this.f3761e, this.f3758b.capacity());
                this.f3765i = this.f3764h.getState() == 1;
                if (this.f3765i) {
                    this.f3758b.position(0);
                    this.f3764h.startRecording();
                    i2 = 3;
                    if (this.f3765i || this.f3764h.getRecordingState() != 3) {
                        i = i2;
                    } else {
                        this.f3757a.fmodProcessMicData(this.f3758b, this.f3764h.read(this.f3758b, this.f3758b.capacity()));
                        this.f3758b.position(0);
                        i = i2;
                    }
                } else {
                    Log.e("FMOD", "AudioRecord failed to initialize (status " + this.f3764h.getState() + ")");
                    i--;
                    m4021d();
                }
            }
            i2 = i;
            if (this.f3765i) {
            }
            i = i2;
        }
        m4021d();
    }
}
