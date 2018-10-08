package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.WorkerThread;
import com.google.android.gms.common.util.zzd;
import org.apache.commons.lang3.time.DateUtils;

public final class zzcfd extends zzcdm {
    private Handler mHandler;
    private long zziwn = zzvu().elapsedRealtime();
    private final zzcau zziwo = new zzcfe(this, this.zzikb);
    private final zzcau zziwp = new zzcff(this, this.zzikb);

    zzcfd(zzcco zzcco) {
        super(zzcco);
    }

    private final void zzazs() {
        synchronized (this) {
            if (this.mHandler == null) {
                this.mHandler = new Handler(Looper.getMainLooper());
            }
        }
    }

    @WorkerThread
    private final void zzazt() {
        zzug();
        zzbr(false);
        zzatw().zzaj(zzvu().elapsedRealtime());
    }

    @WorkerThread
    private final void zzbd(long j) {
        zzug();
        zzazs();
        this.zziwo.cancel();
        this.zziwp.cancel();
        zzauk().zzayi().zzj("Activity resumed, time", Long.valueOf(j));
        this.zziwn = j;
        if (zzvu().currentTimeMillis() - zzaul().zziqu.get() > zzaul().zziqw.get()) {
            zzaul().zziqv.set(true);
            zzaul().zziqx.set(0);
        }
        if (zzaul().zziqv.get()) {
            this.zziwo.zzs(Math.max(0, zzaul().zziqt.get() - zzaul().zziqx.get()));
        } else {
            this.zziwp.zzs(Math.max(0, DateUtils.MILLIS_PER_HOUR - zzaul().zziqx.get()));
        }
    }

    @WorkerThread
    private final void zzbe(long j) {
        zzug();
        zzazs();
        this.zziwo.cancel();
        this.zziwp.cancel();
        zzauk().zzayi().zzj("Activity paused, time", Long.valueOf(j));
        if (this.zziwn != 0) {
            zzaul().zziqx.set(zzaul().zziqx.get() + (j - this.zziwn));
        }
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    public final /* bridge */ /* synthetic */ void zzatt() {
        super.zzatt();
    }

    public final /* bridge */ /* synthetic */ void zzatu() {
        super.zzatu();
    }

    public final /* bridge */ /* synthetic */ void zzatv() {
        super.zzatv();
    }

    public final /* bridge */ /* synthetic */ zzcaf zzatw() {
        return super.zzatw();
    }

    public final /* bridge */ /* synthetic */ zzcam zzatx() {
        return super.zzatx();
    }

    public final /* bridge */ /* synthetic */ zzcdo zzaty() {
        return super.zzaty();
    }

    public final /* bridge */ /* synthetic */ zzcbj zzatz() {
        return super.zzatz();
    }

    public final /* bridge */ /* synthetic */ zzcaw zzaua() {
        return super.zzaua();
    }

    public final /* bridge */ /* synthetic */ zzceg zzaub() {
        return super.zzaub();
    }

    public final /* bridge */ /* synthetic */ zzcec zzauc() {
        return super.zzauc();
    }

    public final /* bridge */ /* synthetic */ zzcbk zzaud() {
        return super.zzaud();
    }

    public final /* bridge */ /* synthetic */ zzcaq zzaue() {
        return super.zzaue();
    }

    public final /* bridge */ /* synthetic */ zzcbm zzauf() {
        return super.zzauf();
    }

    public final /* bridge */ /* synthetic */ zzcfo zzaug() {
        return super.zzaug();
    }

    public final /* bridge */ /* synthetic */ zzcci zzauh() {
        return super.zzauh();
    }

    public final /* bridge */ /* synthetic */ zzcfd zzaui() {
        return super.zzaui();
    }

    public final /* bridge */ /* synthetic */ zzccj zzauj() {
        return super.zzauj();
    }

    public final /* bridge */ /* synthetic */ zzcbo zzauk() {
        return super.zzauk();
    }

    public final /* bridge */ /* synthetic */ zzcbz zzaul() {
        return super.zzaul();
    }

    public final /* bridge */ /* synthetic */ zzcap zzaum() {
        return super.zzaum();
    }

    @WorkerThread
    public final boolean zzbr(boolean z) {
        zzug();
        zzwh();
        long elapsedRealtime = zzvu().elapsedRealtime();
        zzaul().zziqw.set(zzvu().currentTimeMillis());
        long j = elapsedRealtime - this.zziwn;
        if (z || j >= 1000) {
            zzaul().zziqx.set(j);
            zzauk().zzayi().zzj("Recording user engagement, ms", Long.valueOf(j));
            Bundle bundle = new Bundle();
            bundle.putLong("_et", j);
            zzcec.zza(zzauc().zzazm(), bundle);
            zzaty().zzc("auto", "_e", bundle);
            this.zziwn = elapsedRealtime;
            this.zziwp.cancel();
            this.zziwp.zzs(Math.max(0, DateUtils.MILLIS_PER_HOUR - zzaul().zziqx.get()));
            return true;
        }
        zzauk().zzayi().zzj("Screen exposed for less than 1000 ms. Event not sent. time", Long.valueOf(j));
        return false;
    }

    public final /* bridge */ /* synthetic */ void zzug() {
        super.zzug();
    }

    protected final void zzuh() {
    }

    public final /* bridge */ /* synthetic */ zzd zzvu() {
        return super.zzvu();
    }
}
