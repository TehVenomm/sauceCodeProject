package org.fmod;

import android.media.AudioRecord;
import android.util.Log;
import java.nio.ByteBuffer;

/* renamed from: org.fmod.a */
final class C1296a implements Runnable {
    /* renamed from: a */
    private final FMODAudioDevice f1369a;
    /* renamed from: b */
    private final ByteBuffer f1370b;
    /* renamed from: c */
    private final int f1371c;
    /* renamed from: d */
    private final int f1372d;
    /* renamed from: e */
    private final int f1373e = 2;
    /* renamed from: f */
    private volatile Thread f1374f;
    /* renamed from: g */
    private volatile boolean f1375g;
    /* renamed from: h */
    private AudioRecord f1376h;
    /* renamed from: i */
    private boolean f1377i;

    C1296a(FMODAudioDevice fMODAudioDevice, int i, int i2) {
        this.f1369a = fMODAudioDevice;
        this.f1371c = i;
        this.f1372d = i2;
        this.f1370b = ByteBuffer.allocateDirect(AudioRecord.getMinBufferSize(i, i2, 2));
    }

    /* renamed from: d */
    private void m996d() {
        if (this.f1376h != null) {
            if (this.f1376h.getState() == 1) {
                this.f1376h.stop();
            }
            this.f1376h.release();
            this.f1376h = null;
        }
        this.f1370b.position(0);
        this.f1377i = false;
    }

    /* renamed from: a */
    public final int m997a() {
        return this.f1370b.capacity();
    }

    /* renamed from: b */
    public final void m998b() {
        if (this.f1374f != null) {
            m999c();
        }
        this.f1375g = true;
        this.f1374f = new Thread(this);
        this.f1374f.start();
    }

    /* renamed from: c */
    public final void m999c() {
        while (this.f1374f != null) {
            this.f1375g = false;
            try {
                this.f1374f.join();
                this.f1374f = null;
            } catch (InterruptedException e) {
            }
        }
    }

    public final void run() {
        int i = 3;
        while (this.f1375g) {
            int i2;
            if (!this.f1377i && i > 0) {
                m996d();
                this.f1376h = new AudioRecord(1, this.f1371c, this.f1372d, this.f1373e, this.f1370b.capacity());
                this.f1377i = this.f1376h.getState() == 1;
                if (this.f1377i) {
                    this.f1370b.position(0);
                    this.f1376h.startRecording();
                    i2 = 3;
                    if (this.f1377i || this.f1376h.getRecordingState() != 3) {
                        i = i2;
                    } else {
                        this.f1369a.fmodProcessMicData(this.f1370b, this.f1376h.read(this.f1370b, this.f1370b.capacity()));
                        this.f1370b.position(0);
                        i = i2;
                    }
                } else {
                    Log.e("FMOD", "AudioRecord failed to initialize (status " + this.f1376h.getState() + ")");
                    i--;
                    m996d();
                }
            }
            i2 = i;
            if (this.f1377i) {
            }
            i = i2;
        }
        m996d();
    }
}
