package org.fmod;

import android.media.AudioTrack;
import android.util.Log;
import java.nio.ByteBuffer;

public class FMODAudioDevice implements Runnable {
    /* renamed from: h */
    private static int f3746h = 0;
    /* renamed from: i */
    private static int f3747i = 1;
    /* renamed from: j */
    private static int f3748j = 2;
    /* renamed from: k */
    private static int f3749k = 3;
    /* renamed from: a */
    private volatile Thread f3750a = null;
    /* renamed from: b */
    private volatile boolean f3751b = false;
    /* renamed from: c */
    private AudioTrack f3752c = null;
    /* renamed from: d */
    private boolean f3753d = false;
    /* renamed from: e */
    private ByteBuffer f3754e = null;
    /* renamed from: f */
    private byte[] f3755f = null;
    /* renamed from: g */
    private volatile C1613a f3756g;

    private native int fmodGetInfo(int i);

    private native int fmodProcess(ByteBuffer byteBuffer);

    private void releaseAudioTrack() {
        if (this.f3752c != null) {
            if (this.f3752c.getState() == 1) {
                this.f3752c.stop();
            }
            this.f3752c.release();
            this.f3752c = null;
        }
        this.f3754e = null;
        this.f3755f = null;
        this.f3753d = false;
    }

    public void close() {
        synchronized (this) {
            stop();
        }
    }

    native int fmodProcessMicData(ByteBuffer byteBuffer, int i);

    public boolean isRunning() {
        return this.f3750a != null && this.f3750a.isAlive();
    }

    public void run() {
        int i = 3;
        while (this.f3751b) {
            int i2;
            if (this.f3753d || i <= 0) {
                i2 = i;
            } else {
                releaseAudioTrack();
                int fmodGetInfo = fmodGetInfo(f3746h);
                int round = Math.round(((float) AudioTrack.getMinBufferSize(fmodGetInfo, 3, 2)) * 1.1f) & -4;
                int fmodGetInfo2 = fmodGetInfo(f3747i);
                i2 = fmodGetInfo(f3748j);
                if ((fmodGetInfo2 * i2) * 4 > round) {
                    round = (i2 * fmodGetInfo2) * 4;
                }
                this.f3752c = new AudioTrack(3, fmodGetInfo, 3, 2, round, 1);
                this.f3753d = this.f3752c.getState() == 1;
                if (this.f3753d) {
                    this.f3754e = ByteBuffer.allocateDirect((fmodGetInfo2 * 2) * 2);
                    this.f3755f = new byte[this.f3754e.capacity()];
                    this.f3752c.play();
                    i2 = 3;
                } else {
                    Log.e("FMOD", "AudioTrack failed to initialize (status " + this.f3752c.getState() + ")");
                    releaseAudioTrack();
                    i2 = i - 1;
                }
            }
            if (!this.f3753d) {
                i = i2;
            } else if (fmodGetInfo(f3749k) == 1) {
                fmodProcess(this.f3754e);
                this.f3754e.get(this.f3755f, 0, this.f3754e.capacity());
                this.f3752c.write(this.f3755f, 0, this.f3754e.capacity());
                this.f3754e.position(0);
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
            if (this.f3750a != null) {
                stop();
            }
            this.f3750a = new Thread(this, "FMODAudioDevice");
            this.f3750a.setPriority(10);
            this.f3751b = true;
            this.f3750a.start();
            if (this.f3756g != null) {
                this.f3756g.m4023b();
            }
        }
    }

    public int startAudioRecord(int i, int i2, int i3) {
        int a;
        synchronized (this) {
            if (this.f3756g == null) {
                this.f3756g = new C1613a(this, i, i2);
                this.f3756g.m4023b();
            }
            a = this.f3756g.m4022a();
        }
        return a;
    }

    public void stop() {
        synchronized (this) {
            while (this.f3750a != null) {
                this.f3751b = false;
                try {
                    this.f3750a.join();
                    this.f3750a = null;
                } catch (InterruptedException e) {
                }
            }
            if (this.f3756g != null) {
                this.f3756g.m4024c();
            }
        }
    }

    public void stopAudioRecord() {
        synchronized (this) {
            if (this.f3756g != null) {
                this.f3756g.m4024c();
                this.f3756g = null;
            }
        }
    }
}
