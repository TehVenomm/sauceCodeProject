package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.app.AlarmManager;
import android.app.PendingIntent;
import android.app.job.JobInfo;
import android.app.job.JobInfo.Builder;
import android.app.job.JobScheduler;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.os.Build.VERSION;
import android.os.PersistableBundle;
import com.facebook.internal.NativeProtocol;
import com.google.android.gms.common.util.zzd;

public final class zzcfj extends zzcdm {
    private final AlarmManager zzdrc = ((AlarmManager) getContext().getSystemService("alarm"));
    private final zzcau zziwr;
    private Integer zziws;

    protected zzcfj(zzcco zzcco) {
        super(zzcco);
        this.zziwr = new zzcfk(this, zzcco);
    }

    private final int getJobId() {
        if (this.zziws == null) {
            String valueOf = String.valueOf(getContext().getPackageName());
            this.zziws = Integer.valueOf((valueOf.length() != 0 ? "measurement".concat(valueOf) : new String("measurement")).hashCode());
        }
        return this.zziws.intValue();
    }

    @TargetApi(24)
    private final void zzazu() {
        JobScheduler jobScheduler = (JobScheduler) getContext().getSystemService("jobscheduler");
        zzauk().zzayi().zzj("Cancelling job. JobID", Integer.valueOf(getJobId()));
        jobScheduler.cancel(getJobId());
    }

    private final void zzazv() {
        Intent intent = new Intent();
        Context context = getContext();
        zzcap.zzawj();
        intent = intent.setClassName(context, "com.google.android.gms.measurement.AppMeasurementReceiver");
        intent.setAction("com.google.android.gms.measurement.UPLOAD");
        getContext().sendBroadcast(intent);
    }

    private final PendingIntent zzyh() {
        Intent intent = new Intent();
        Context context = getContext();
        zzcap.zzawj();
        intent = intent.setClassName(context, "com.google.android.gms.measurement.AppMeasurementReceiver");
        intent.setAction("com.google.android.gms.measurement.UPLOAD");
        return PendingIntent.getBroadcast(getContext(), 0, intent, 0);
    }

    public final void cancel() {
        zzwh();
        this.zzdrc.cancel(zzyh());
        this.zziwr.cancel();
        if (VERSION.SDK_INT >= 24) {
            zzazu();
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

    public final void zzs(long j) {
        zzwh();
        zzcap.zzawj();
        if (!zzccf.zzj(getContext(), false)) {
            zzauk().zzayh().log("Receiver not registered/enabled");
        }
        zzcap.zzawj();
        if (!zzcez.zzk(getContext(), false)) {
            zzauk().zzayh().log("Service not registered/enabled");
        }
        cancel();
        long elapsedRealtime = zzvu().elapsedRealtime();
        if (j < zzcap.zzaxa() && !this.zziwr.zzdp()) {
            zzauk().zzayi().log("Scheduling upload with DelayedRunnable");
            this.zziwr.zzs(j);
        }
        zzcap.zzawj();
        if (VERSION.SDK_INT >= 24) {
            zzauk().zzayi().log("Scheduling upload with JobScheduler");
            JobScheduler jobScheduler = (JobScheduler) getContext().getSystemService("jobscheduler");
            Builder builder = new Builder(getJobId(), new ComponentName(getContext(), "com.google.android.gms.measurement.AppMeasurementJobService"));
            builder.setMinimumLatency(j);
            builder.setOverrideDeadline(j << 1);
            PersistableBundle persistableBundle = new PersistableBundle();
            persistableBundle.putString(NativeProtocol.WEB_DIALOG_ACTION, "com.google.android.gms.measurement.UPLOAD");
            builder.setExtras(persistableBundle);
            JobInfo build = builder.build();
            zzauk().zzayi().zzj("Scheduling job. JobID", Integer.valueOf(getJobId()));
            jobScheduler.schedule(build);
            return;
        }
        zzauk().zzayi().log("Scheduling upload with AlarmManager");
        this.zzdrc.setInexactRepeating(2, elapsedRealtime + j, Math.max(zzcap.zzaxb(), j), zzyh());
    }

    public final /* bridge */ /* synthetic */ void zzug() {
        super.zzug();
    }

    protected final void zzuh() {
        this.zzdrc.cancel(zzyh());
        if (VERSION.SDK_INT >= 24) {
            zzazu();
        }
    }

    public final /* bridge */ /* synthetic */ zzd zzvu() {
        return super.zzvu();
    }
}
