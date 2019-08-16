package org.fmod;

import android.media.AudioTrack;
import android.util.Log;
import java.nio.ByteBuffer;

public class FMODAudioDevice implements Runnable {

    /* renamed from: h */
    private static int f1434h = 0;

    /* renamed from: i */
    private static int f1435i = 1;

    /* renamed from: j */
    private static int f1436j = 2;

    /* renamed from: k */
    private static int f1437k = 3;

    /* renamed from: a */
    private volatile Thread f1438a = null;

    /* renamed from: b */
    private volatile boolean f1439b = false;

    /* renamed from: c */
    private AudioTrack f1440c = null;

    /* renamed from: d */
    private boolean f1441d = false;

    /* renamed from: e */
    private ByteBuffer f1442e = null;

    /* renamed from: f */
    private byte[] f1443f = null;

    /* renamed from: g */
    private volatile C1804a f1444g;

    private native int fmodGetInfo(int i);

    private native int fmodProcess(ByteBuffer byteBuffer);

    private void releaseAudioTrack() {
        if (this.f1440c != null) {
            if (this.f1440c.getState() == 1) {
                this.f1440c.stop();
            }
            this.f1440c.release();
            this.f1440c = null;
        }
        this.f1442e = null;
        this.f1443f = null;
        this.f1441d = false;
    }

    public void close() {
        synchronized (this) {
            stop();
        }
    }

    /* access modifiers changed from: 0000 */
    public native int fmodProcessMicData(ByteBuffer byteBuffer, int i);

    public boolean isRunning() {
        return this.f1438a != null && this.f1438a.isAlive();
    }

    public void run() {
        int i;
        int i2 = 3;
        while (this.f1439b) {
            if (this.f1441d || i2 <= 0) {
                i = i2;
            } else {
                releaseAudioTrack();
                int fmodGetInfo = fmodGetInfo(f1434h);
                int round = Math.round(((float) AudioTrack.getMinBufferSize(fmodGetInfo, 3, 2)) * 1.1f) & -4;
                int fmodGetInfo2 = fmodGetInfo(f1435i);
                int fmodGetInfo3 = fmodGetInfo(f1436j);
                if (fmodGetInfo2 * fmodGetInfo3 * 4 > round) {
                    round = fmodGetInfo3 * fmodGetInfo2 * 4;
                }
                this.f1440c = new AudioTrack(3, fmodGetInfo, 3, 2, round, 1);
                this.f1441d = this.f1440c.getState() == 1;
                if (this.f1441d) {
                    this.f1442e = ByteBuffer.allocateDirect(fmodGetInfo2 * 2 * 2);
                    this.f1443f = new byte[this.f1442e.capacity()];
                    this.f1440c.play();
                    i = 3;
                } else {
                    Log.e("FMOD", "AudioTrack failed to initialize (status " + this.f1440c.getState() + ")");
                    releaseAudioTrack();
                    i = i2 - 1;
                }
            }
            if (!this.f1441d) {
                i2 = i;
            } else if (fmodGetInfo(f1437k) == 1) {
                fmodProcess(this.f1442e);
                this.f1442e.get(this.f1443f, 0, this.f1442e.capacity());
                this.f1440c.write(this.f1443f, 0, this.f1442e.capacity());
                this.f1442e.position(0);
                i2 = i;
            } else {
                releaseAudioTrack();
                i2 = i;
            }
        }
        releaseAudioTrack();
    }

    public void start() {
        synchronized (this) {
            if (this.f1438a != null) {
                stop();
            }
            this.f1438a = new Thread(this, "FMODAudioDevice");
            this.f1438a.setPriority(10);
            this.f1439b = true;
            this.f1438a.start();
            if (this.f1444g != null) {
                this.f1444g.mo24425b();
            }
        }
    }

    public int startAudioRecord(int i, int i2, int i3) {
        int a;
        synchronized (this) {
            if (this.f1444g == null) {
                this.f1444g = new C1804a(this, i, i2);
                this.f1444g.mo24425b();
            }
            a = this.f1444g.mo24424a();
        }
        return a;
    }

    public void stop() {
        synchronized (this) {
            while (this.f1438a != null) {
                this.f1439b = false;
                try {
                    this.f1438a.join();
                    this.f1438a = null;
                } catch (InterruptedException e) {
                }
            }
            if (this.f1444g != null) {
                this.f1444g.mo24426c();
            }
        }
    }

    public void stopAudioRecord() {
        synchronized (this) {
            if (this.f1444g != null) {
                this.f1444g.mo24426c();
                this.f1444g = null;
            }
        }
    }
}
