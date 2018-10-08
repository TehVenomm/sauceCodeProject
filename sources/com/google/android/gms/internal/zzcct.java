package com.google.android.gms.internal;

import android.os.Binder;
import android.support.annotation.BinderThread;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzv;
import com.google.android.gms.common.zzo;
import com.google.android.gms.common.zzp;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.concurrent.ExecutionException;

public final class zzcct extends zzcbh {
    private final zzcco zzikb;
    private Boolean zzity;
    @Nullable
    private String zzitz;

    public zzcct(zzcco zzcco) {
        this(zzcco, null);
    }

    private zzcct(zzcco zzcco, @Nullable String str) {
        zzbp.zzu(zzcco);
        this.zzikb = zzcco;
        this.zzitz = null;
    }

    @BinderThread
    private final void zzb(zzcak zzcak, boolean z) {
        zzbp.zzu(zzcak);
        zzg(zzcak.packageName, false);
        this.zzikb.zzaug().zzkb(zzcak.zziln);
    }

    @BinderThread
    private final void zzg(String str, boolean z) {
        boolean z2 = false;
        if (TextUtils.isEmpty(str)) {
            this.zzikb.zzauk().zzayc().log("Measurement Service called without app package");
            throw new SecurityException("Measurement Service called without app package");
        }
        if (z) {
            try {
                if (this.zzity == null) {
                    if ("com.google.android.gms".equals(this.zzitz) || zzv.zzf(this.zzikb.getContext(), Binder.getCallingUid()) || zzp.zzca(this.zzikb.getContext()).zza(this.zzikb.getContext().getPackageManager(), Binder.getCallingUid())) {
                        z2 = true;
                    }
                    this.zzity = Boolean.valueOf(z2);
                }
                if (this.zzity.booleanValue()) {
                    return;
                }
            } catch (SecurityException e) {
                this.zzikb.zzauk().zzayc().zzj("Measurement Service called with invalid calling package. appId", zzcbo.zzjf(str));
                throw e;
            }
        }
        if (this.zzitz == null && zzo.zzb(this.zzikb.getContext(), Binder.getCallingUid(), str)) {
            this.zzitz = str;
        }
        if (!str.equals(this.zzitz)) {
            throw new SecurityException(String.format("Unknown calling package name '%s'.", new Object[]{str}));
        }
    }

    @BinderThread
    public final List<zzcfl> zza(zzcak zzcak, boolean z) {
        Object e;
        zzb(zzcak, false);
        try {
            List<zzcfn> list = (List) this.zzikb.zzauj().zzd(new zzcdi(this, zzcak)).get();
            List<zzcfl> arrayList = new ArrayList(list.size());
            for (zzcfn zzcfn : list) {
                if (z || !zzcfo.zzkd(zzcfn.mName)) {
                    arrayList.add(new zzcfl(zzcfn));
                }
            }
            return arrayList;
        } catch (InterruptedException e2) {
            e = e2;
            this.zzikb.zzauk().zzayc().zze("Failed to get user attributes. appId", zzcbo.zzjf(zzcak.packageName), e);
            return null;
        } catch (ExecutionException e3) {
            e = e3;
            this.zzikb.zzauk().zzayc().zze("Failed to get user attributes. appId", zzcbo.zzjf(zzcak.packageName), e);
            return null;
        }
    }

    @BinderThread
    public final List<zzcan> zza(String str, String str2, zzcak zzcak) {
        Object e;
        zzb(zzcak, false);
        try {
            return (List) this.zzikb.zzauj().zzd(new zzcdb(this, zzcak, str, str2)).get();
        } catch (InterruptedException e2) {
            e = e2;
        } catch (ExecutionException e3) {
            e = e3;
        }
        this.zzikb.zzauk().zzayc().zzj("Failed to get conditional user properties", e);
        return Collections.emptyList();
    }

    @BinderThread
    public final List<zzcfl> zza(String str, String str2, String str3, boolean z) {
        Object e;
        zzg(str, true);
        try {
            List<zzcfn> list = (List) this.zzikb.zzauj().zzd(new zzcda(this, str, str2, str3)).get();
            List<zzcfl> arrayList = new ArrayList(list.size());
            for (zzcfn zzcfn : list) {
                if (z || !zzcfo.zzkd(zzcfn.mName)) {
                    arrayList.add(new zzcfl(zzcfn));
                }
            }
            return arrayList;
        } catch (InterruptedException e2) {
            e = e2;
            this.zzikb.zzauk().zzayc().zze("Failed to get user attributes. appId", zzcbo.zzjf(str), e);
            return Collections.emptyList();
        } catch (ExecutionException e3) {
            e = e3;
            this.zzikb.zzauk().zzayc().zze("Failed to get user attributes. appId", zzcbo.zzjf(str), e);
            return Collections.emptyList();
        }
    }

    @BinderThread
    public final List<zzcfl> zza(String str, String str2, boolean z, zzcak zzcak) {
        Object e;
        zzb(zzcak, false);
        try {
            List<zzcfn> list = (List) this.zzikb.zzauj().zzd(new zzccz(this, zzcak, str, str2)).get();
            List<zzcfl> arrayList = new ArrayList(list.size());
            for (zzcfn zzcfn : list) {
                if (z || !zzcfo.zzkd(zzcfn.mName)) {
                    arrayList.add(new zzcfl(zzcfn));
                }
            }
            return arrayList;
        } catch (InterruptedException e2) {
            e = e2;
            this.zzikb.zzauk().zzayc().zze("Failed to get user attributes. appId", zzcbo.zzjf(zzcak.packageName), e);
            return Collections.emptyList();
        } catch (ExecutionException e3) {
            e = e3;
            this.zzikb.zzauk().zzayc().zze("Failed to get user attributes. appId", zzcbo.zzjf(zzcak.packageName), e);
            return Collections.emptyList();
        }
    }

    @BinderThread
    public final void zza(long j, String str, String str2, String str3) {
        this.zzikb.zzauj().zzg(new zzcdk(this, str2, str3, str, j));
    }

    @BinderThread
    public final void zza(zzcak zzcak) {
        zzb(zzcak, false);
        Runnable zzcdj = new zzcdj(this, zzcak);
        if (this.zzikb.zzauj().zzayr()) {
            zzcdj.run();
        } else {
            this.zzikb.zzauj().zzg(zzcdj);
        }
    }

    @BinderThread
    public final void zza(zzcan zzcan, zzcak zzcak) {
        zzbp.zzu(zzcan);
        zzbp.zzu(zzcan.zzima);
        zzb(zzcak, false);
        zzcan zzcan2 = new zzcan(zzcan);
        zzcan2.packageName = zzcak.packageName;
        if (zzcan.zzima.getValue() == null) {
            this.zzikb.zzauj().zzg(new zzccv(this, zzcan2, zzcak));
        } else {
            this.zzikb.zzauj().zzg(new zzccw(this, zzcan2, zzcak));
        }
    }

    @BinderThread
    public final void zza(zzcbc zzcbc, zzcak zzcak) {
        zzbp.zzu(zzcbc);
        zzb(zzcak, false);
        this.zzikb.zzauj().zzg(new zzcdd(this, zzcbc, zzcak));
    }

    @BinderThread
    public final void zza(zzcbc zzcbc, String str, String str2) {
        zzbp.zzu(zzcbc);
        zzbp.zzgf(str);
        zzg(str, true);
        this.zzikb.zzauj().zzg(new zzcde(this, zzcbc, str));
    }

    @BinderThread
    public final void zza(zzcfl zzcfl, zzcak zzcak) {
        zzbp.zzu(zzcfl);
        zzb(zzcak, false);
        if (zzcfl.getValue() == null) {
            this.zzikb.zzauj().zzg(new zzcdg(this, zzcfl, zzcak));
        } else {
            this.zzikb.zzauj().zzg(new zzcdh(this, zzcfl, zzcak));
        }
    }

    @BinderThread
    public final byte[] zza(zzcbc zzcbc, String str) {
        Object e;
        zzbp.zzgf(str);
        zzbp.zzu(zzcbc);
        zzg(str, true);
        this.zzikb.zzauk().zzayh().zzj("Log and bundle. event", this.zzikb.zzauf().zzjc(zzcbc.name));
        long nanoTime = this.zzikb.zzvu().nanoTime() / 1000000;
        try {
            byte[] bArr = (byte[]) this.zzikb.zzauj().zze(new zzcdf(this, zzcbc, str)).get();
            if (bArr == null) {
                this.zzikb.zzauk().zzayc().zzj("Log and bundle returned null. appId", zzcbo.zzjf(str));
                bArr = new byte[0];
            }
            this.zzikb.zzauk().zzayh().zzd("Log and bundle processed. event, size, time_ms", this.zzikb.zzauf().zzjc(zzcbc.name), Integer.valueOf(bArr.length), Long.valueOf((this.zzikb.zzvu().nanoTime() / 1000000) - nanoTime));
            return bArr;
        } catch (InterruptedException e2) {
            e = e2;
            this.zzikb.zzauk().zzayc().zzd("Failed to log and bundle. appId, event, error", zzcbo.zzjf(str), this.zzikb.zzauf().zzjc(zzcbc.name), e);
            return null;
        } catch (ExecutionException e3) {
            e = e3;
            this.zzikb.zzauk().zzayc().zzd("Failed to log and bundle. appId, event, error", zzcbo.zzjf(str), this.zzikb.zzauf().zzjc(zzcbc.name), e);
            return null;
        }
    }

    @BinderThread
    public final void zzb(zzcak zzcak) {
        zzb(zzcak, false);
        this.zzikb.zzauj().zzg(new zzccu(this, zzcak));
    }

    @BinderThread
    public final void zzb(zzcan zzcan) {
        zzbp.zzu(zzcan);
        zzbp.zzu(zzcan.zzima);
        zzg(zzcan.packageName, true);
        zzcan zzcan2 = new zzcan(zzcan);
        if (zzcan.zzima.getValue() == null) {
            this.zzikb.zzauj().zzg(new zzccx(this, zzcan2));
        } else {
            this.zzikb.zzauj().zzg(new zzccy(this, zzcan2));
        }
    }

    @BinderThread
    public final String zzc(zzcak zzcak) {
        zzb(zzcak, false);
        return this.zzikb.zzjs(zzcak.packageName);
    }

    @BinderThread
    public final List<zzcan> zzj(String str, String str2, String str3) {
        Object e;
        zzg(str, true);
        try {
            return (List) this.zzikb.zzauj().zzd(new zzcdc(this, str, str2, str3)).get();
        } catch (InterruptedException e2) {
            e = e2;
        } catch (ExecutionException e3) {
            e = e3;
        }
        this.zzikb.zzauk().zzayc().zzj("Failed to get conditional user properties", e);
        return Collections.emptyList();
    }
}
