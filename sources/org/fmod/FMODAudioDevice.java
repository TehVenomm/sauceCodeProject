package org.fmod;

import android.media.AudioTrack;
import android.util.Log;
import java.nio.ByteBuffer;

public class FMODAudioDevice implements Runnable {
    /* renamed from: h */
    private static int f1358h = 0;
    /* renamed from: i */
    private static int f1359i = 1;
    /* renamed from: j */
    private static int f1360j = 2;
    /* renamed from: k */
    private static int f1361k = 3;
    /* renamed from: a */
    private volatile Thread f1362a = null;
    /* renamed from: b */
    private volatile boolean f1363b = false;
    /* renamed from: c */
    private AudioTrack f1364c = null;
    /* renamed from: d */
    private boolean f1365d = false;
    /* renamed from: e */
    private ByteBuffer f1366e = null;
    /* renamed from: f */
    private byte[] f1367f = null;
    /* renamed from: g */
    private volatile C1296a f1368g;

    private native int fmodGetInfo(int i);

    private native int fmodProcess(ByteBuffer byteBuffer);

    private void releaseAudioTrack() {
        if (this.f1364c != null) {
            if (this.f1364c.getState() == 1) {
                this.f1364c.stop();
            }
            this.f1364c.release();
            this.f1364c = null;
        }
        this.f1366e = null;
        this.f1367f = null;
        this.f1365d = false;
    }

    public void close() {
        synchronized (this) {
            stop();
        }
    }

    native int fmodProcessMicData(ByteBuffer byteBuffer, int i);

    public boolean isRunning() {
        return this.f1362a != null && this.f1362a.isAlive();
    }

    public void run() {
        int i = 3;
        while (this.f1363b) {
            int i2;
            if (this.f1365d || i <= 0) {
                i2 = i;
            } else {
                releaseAudioTrack();
                int fmodGetInfo = fmodGetInfo(f1358h);
                int round = Math.round(((float) AudioTrack.getMinBufferSize(fmodGetInfo, 3, 2)) * 1.1f) & -4;
                int fmodGetInfo2 = fmodGetInfo(f1359i);
                i2 = fmodGetInfo(f1360j);
                if ((fmodGetInfo2 * i2) * 4 > round) {
                    round = (i2 * fmodGetInfo2) * 4;
                }
                this.f1364c = new AudioTrack(3, fmodGetInfo, 3, 2, round, 1);
                this.f1365d = this.f1364c.getState() == 1;
                if (this.f1365d) {
                    this.f1366e = ByteBuffer.allocateDirect((fmodGetInfo2 * 2) * 2);
                    this.f1367f = new byte[this.f1366e.capacity()];
                    this.f1364c.play();
                    i2 = 3;
                } else {
                    Log.e("FMOD", "AudioTrack failed to initialize (status " + this.f1364c.getState() + ")");
                    releaseAudioTrack();
                    i2 = i - 1;
                }
            }
            if (!this.f1365d) {
                i = i2;
            } else if (fmodGetInfo(f1361k) == 1) {
                fmodProcess(this.f1366e);
                this.f1366e.get(this.f1367f, 0, this.f1366e.capacity());
                this.f1364c.write(this.f1367f, 0, this.f1366e.capacity());
                this.f1366e.position(0);
                i = i2;
            } else {
                releaseAudioTrack();
                i = i2;
            }
        }
        releaseAudioTrack();
    }

    public void start() {
        synchronized (this) {
            if (this.f1362a != null) {
                stop();
            }
            this.f1362a = new Thread(this, "FMODAudioDevice");
            this.f1362a.setPriority(10);
            this.f1363b = true;
            this.f1362a.start();
            if (this.f1368g != null) {
                this.f1368g.m998b();
            }
        }
    }

    public int startAudioRecord(int i, int i2, int i3) {
        int a;
        synchronized (this) {
            if (this.f1368g == null) {
                this.f1368g = new C1296a(this, i, i2);
                this.f1368g.m998b();
            }
            a = this.f1368g.m997a();
        }
        return a;
    }

    public void stop() {
        synchronized (this) {
            while (this.f1362a != null) {
                this.f1363b = false;
                try {
                    this.f1362a.join();
                    this.f1362a = null;
                } catch (InterruptedException e) {
                }
            }
            if (this.f1368g != null) {
                this.f1368g.m999c();
            }
        }
    }

    public void stopAudioRecord() {
        synchronized (this) {
            if (this.f1368g != null) {
                this.f1368g.m999c();
                this.f1368g = null;
            }
        }
    }
}
