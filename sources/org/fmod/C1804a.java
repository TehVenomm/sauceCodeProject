package org.fmod;

import android.media.AudioRecord;
import android.util.Log;
import java.nio.ByteBuffer;

/* renamed from: org.fmod.a */
final class C1804a implements Runnable {

    /* renamed from: a */
    private final FMODAudioDevice f1445a;

    /* renamed from: b */
    private final ByteBuffer f1446b;

    /* renamed from: c */
    private final int f1447c;

    /* renamed from: d */
    private final int f1448d;

    /* renamed from: e */
    private final int f1449e = 2;

    /* renamed from: f */
    private volatile Thread f1450f;

    /* renamed from: g */
    private volatile boolean f1451g;

    /* renamed from: h */
    private AudioRecord f1452h;

    /* renamed from: i */
    private boolean f1453i;

    C1804a(FMODAudioDevice fMODAudioDevice, int i, int i2) {
        this.f1445a = fMODAudioDevice;
        this.f1447c = i;
        this.f1448d = i2;
        this.f1446b = ByteBuffer.allocateDirect(AudioRecord.getMinBufferSize(i, i2, 2));
    }

    /* renamed from: d */
    private void m1021d() {
        if (this.f1452h != null) {
            if (this.f1452h.getState() == 1) {
                this.f1452h.stop();
            }
            this.f1452h.release();
            this.f1452h = null;
        }
        this.f1446b.position(0);
        this.f1453i = false;
    }

    /* renamed from: a */
    public final int mo24424a() {
        return this.f1446b.capacity();
    }

    /* renamed from: b */
    public final void mo24425b() {
        if (this.f1450f != null) {
            mo24426c();
        }
        this.f1451g = true;
        this.f1450f = new Thread(this);
        this.f1450f.start();
    }

    /* renamed from: c */
    public final void mo24426c() {
        while (this.f1450f != null) {
            this.f1451g = false;
            try {
                this.f1450f.join();
                this.f1450f = null;
            } catch (InterruptedException e) {
            }
        }
    }

    public final void run() {
        int i;
        int i2 = 3;
        while (this.f1451g) {
            if (!this.f1453i && i2 > 0) {
                m1021d();
                this.f1452h = new AudioRecord(1, this.f1447c, this.f1448d, this.f1449e, this.f1446b.capacity());
                this.f1453i = this.f1452h.getState() == 1;
                if (this.f1453i) {
                    this.f1446b.position(0);
                    this.f1452h.startRecording();
                    i = 3;
                    if (this.f1453i || this.f1452h.getRecordingState() != 3) {
                        i2 = i;
                    } else {
                        this.f1445a.fmodProcessMicData(this.f1446b, this.f1452h.read(this.f1446b, this.f1446b.capacity()));
                        this.f1446b.position(0);
                        i2 = i;
                    }
                } else {
                    Log.e("FMOD", "AudioRecord failed to initialize (status " + this.f1452h.getState() + ")");
                    i2--;
                    m1021d();
                }
            }
            i = i2;
            if (this.f1453i) {
            }
            i2 = i;
        }
        m1021d();
    }
}
