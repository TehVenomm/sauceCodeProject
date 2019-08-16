package com.google.android.gms.measurement.internal;

import android.os.Build;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Size;
import android.support.annotation.WorkerThread;
import android.text.TextUtils;
import android.util.Pair;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.internal.measurement.zzbs.zzc;
import com.google.android.gms.internal.measurement.zzbs.zzd;
import com.google.android.gms.internal.measurement.zzbs.zze;
import com.google.android.gms.internal.measurement.zzbs.zzf;
import com.google.android.gms.internal.measurement.zzbs.zzf.zza;
import com.google.android.gms.internal.measurement.zzbs.zzg;
import com.google.android.gms.internal.measurement.zzbs.zzh;
import com.google.android.gms.internal.measurement.zzbs.zzk;
import com.google.android.gms.internal.measurement.zzey;
import java.io.IOException;
import java.util.Arrays;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;

final class zzhp extends zzjh {
    public zzhp(zzjg zzjg) {
        super(zzjg);
    }

    private static String zzo(String str, String str2) {
        throw new SecurityException("This implementation should not be used.");
    }

    @WorkerThread
    public final byte[] zzb(@NonNull zzai zzai, @Size(min = 1) String str) {
        zzjp zzjp;
        long j;
        zzae zzw;
        zzo();
        this.zzj.zzl();
        Preconditions.checkNotNull(zzai);
        Preconditions.checkNotEmpty(str);
        if (!zzad().zze(str, zzak.zzio)) {
            zzab().zzgr().zza("Generating ScionPayload disabled. packageName", str);
            return new byte[0];
        } else if ("_iap".equals(zzai.name) || "_iapx".equals(zzai.name)) {
            zza zznj = zzf.zznj();
            zzgy().beginTransaction();
            try {
                zzf zzab = zzgy().zzab(str);
                if (zzab == null) {
                    zzab().zzgr().zza("Log and bundle not available. package_name", str);
                    return new byte[0];
                } else if (!zzab.isMeasurementEnabled()) {
                    zzab().zzgr().zza("Log and bundle disabled. package_name", str);
                    byte[] bArr = new byte[0];
                    zzgy().endTransaction();
                    return bArr;
                } else {
                    zzg.zza zzcc = zzg.zzpr().zzp(1).zzcc("android");
                    if (!TextUtils.isEmpty(zzab.zzag())) {
                        zzcc.zzch(zzab.zzag());
                    }
                    if (!TextUtils.isEmpty(zzab.zzan())) {
                        zzcc.zzcg(zzab.zzan());
                    }
                    if (!TextUtils.isEmpty(zzab.zzal())) {
                        zzcc.zzci(zzab.zzal());
                    }
                    if (zzab.zzam() != -2147483648L) {
                        zzcc.zzv((int) zzab.zzam());
                    }
                    zzcc.zzas(zzab.zzao()).zzax(zzab.zzaq());
                    if (!TextUtils.isEmpty(zzab.getGmpAppId())) {
                        zzcc.zzcm(zzab.getGmpAppId());
                    } else if (!TextUtils.isEmpty(zzab.zzah())) {
                        zzcc.zzcq(zzab.zzah());
                    }
                    zzcc.zzau(zzab.zzap());
                    if (this.zzj.isEnabled() && zzs.zzbv() && zzad().zzl(zzcc.zzag())) {
                        zzcc.zzag();
                        if (!TextUtils.isEmpty(null)) {
                            zzcc.zzcp(null);
                        }
                    }
                    Pair zzap = zzac().zzap(zzab.zzag());
                    if (zzab.zzbe() && zzap != null && !TextUtils.isEmpty((CharSequence) zzap.first)) {
                        zzcc.zzcj(zzo((String) zzap.first, Long.toString(zzai.zzfu)));
                        if (zzap.second != null) {
                            zzcc.zzm(((Boolean) zzap.second).booleanValue());
                        }
                    }
                    zzw().zzbi();
                    zzg.zza zzce = zzcc.zzce(Build.MODEL);
                    zzw().zzbi();
                    zzce.zzcd(VERSION.RELEASE).zzt((int) zzw().zzcq()).zzcf(zzw().zzcr());
                    try {
                        zzcc.zzck(zzo(zzab.getAppInstanceId(), Long.toString(zzai.zzfu)));
                        if (!TextUtils.isEmpty(zzab.getFirebaseInstanceId())) {
                            zzcc.zzcn(zzab.getFirebaseInstanceId());
                        }
                        String zzag = zzab.zzag();
                        List zzaa = zzgy().zzaa(zzag);
                        Iterator it = zzaa.iterator();
                        while (true) {
                            if (!it.hasNext()) {
                                zzjp = null;
                                break;
                            }
                            zzjp = (zzjp) it.next();
                            if ("_lte".equals(zzjp.name)) {
                                break;
                            }
                        }
                        if (zzjp == null || zzjp.value == null) {
                            zzjp zzjp2 = new zzjp(zzag, "auto", "_lte", zzx().currentTimeMillis(), Long.valueOf(0));
                            zzaa.add(zzjp2);
                            zzgy().zza(zzjp2);
                        }
                        if (zzad().zze(zzag, zzak.zzij)) {
                            zzjo zzgw = zzgw();
                            zzgw.zzab().zzgs().zzao("Checking account type status for ad personalization signals");
                            if (zzgw.zzw().zzcu()) {
                                String zzag2 = zzab.zzag();
                                if (zzab.zzbe() && zzgw.zzgz().zzba(zzag2)) {
                                    zzgw.zzab().zzgr().zzao("Turning off ad personalization due to account type");
                                    Iterator it2 = zzaa.iterator();
                                    while (true) {
                                        if (it2.hasNext()) {
                                            if ("_npa".equals(((zzjp) it2.next()).name)) {
                                                it2.remove();
                                                break;
                                            }
                                        } else {
                                            break;
                                        }
                                    }
                                    zzaa.add(new zzjp(zzag2, "auto", "_npa", zzgw.zzx().currentTimeMillis(), Long.valueOf(1)));
                                }
                            }
                        }
                        zzk[] zzkArr = new zzk[zzaa.size()];
                        for (int i = 0; i < zzaa.size(); i++) {
                            zzk.zza zzbk = zzk.zzqu().zzdb(((zzjp) zzaa.get(i)).name).zzbk(((zzjp) zzaa.get(i)).zztr);
                            zzgw().zza(zzbk, ((zzjp) zzaa.get(i)).value);
                            zzkArr[i] = (zzk) ((zzey) zzbk.zzug());
                        }
                        zzcc.zzb(Arrays.asList(zzkArr));
                        Bundle zzcv = zzai.zzfq.zzcv();
                        zzcv.putLong("_c", 1);
                        zzab().zzgr().zzao("Marking in-app purchase as real-time");
                        zzcv.putLong("_r", 1);
                        zzcv.putString("_o", zzai.origin);
                        if (zzz().zzbr(zzcc.zzag())) {
                            zzz().zza(zzcv, "_dbg", (Object) Long.valueOf(1));
                            zzz().zza(zzcv, "_r", (Object) Long.valueOf(1));
                        }
                        zzae zzc = zzgy().zzc(str, zzai.name);
                        if (zzc == null) {
                            j = 0;
                            zzw = new zzae(str, zzai.name, 0, 0, zzai.zzfu, 0, null, null, null, null);
                        } else {
                            j = zzc.zzfj;
                            zzw = zzc.zzw(zzai.zzfu);
                        }
                        zzgy().zza(zzw);
                        zzaf zzaf = new zzaf(this.zzj, zzai.origin, str, zzai.name, zzai.zzfu, j, zzcv);
                        zzc.zza zzah = zzc.zzmq().zzag(zzaf.timestamp).zzbx(zzaf.name).zzah(zzaf.zzfp);
                        Iterator it3 = zzaf.zzfq.iterator();
                        while (it3.hasNext()) {
                            String str2 = (String) it3.next();
                            zze.zza zzbz = zze.zzng().zzbz(str2);
                            zzgw().zza(zzbz, zzaf.zzfq.get(str2));
                            zzah.zza(zzbz);
                        }
                        zzcc.zza(zzah).zza(zzh.zzpt().zza(zzd.zzms().zzak(zzw.zzfg).zzby(zzai.name)));
                        zzcc.zzc(zzgx().zza(zzab.zzag(), Collections.emptyList(), zzcc.zzno()));
                        if (zzah.zzml()) {
                            zzcc.zzao(zzah.getTimestampMillis()).zzap(zzah.getTimestampMillis());
                        }
                        long zzak = zzab.zzak();
                        if (zzak != 0) {
                            zzcc.zzar(zzak);
                        }
                        long zzaj = zzab.zzaj();
                        if (zzaj != 0) {
                            zzcc.zzaq(zzaj);
                        } else if (zzak != 0) {
                            zzcc.zzaq(zzak);
                        }
                        zzab.zzau();
                        zzcc.zzu((int) zzab.zzar()).zzat(zzad().zzao()).zzan(zzx().currentTimeMillis()).zzn(Boolean.TRUE.booleanValue());
                        zznj.zza(zzcc);
                        zzab.zze(zzcc.zznq());
                        zzab.zzf(zzcc.zznr());
                        zzgy().zza(zzab);
                        zzgy().setTransactionSuccessful();
                        zzgy().endTransaction();
                        try {
                            return zzgw().zzc(((zzf) ((zzey) zznj.zzug())).toByteArray());
                        } catch (IOException e) {
                            zzab().zzgk().zza("Data loss. Failed to bundle and serialize. appId", zzef.zzam(str), e);
                            return null;
                        }
                    } catch (SecurityException e2) {
                        zzab().zzgr().zza("app instance id encryption failed", e2.getMessage());
                        byte[] bArr2 = new byte[0];
                        zzgy().endTransaction();
                        return bArr2;
                    }
                }
            } catch (SecurityException e3) {
                zzab().zzgr().zza("Resettable device id encryption failed", e3.getMessage());
                return new byte[0];
            } finally {
                zzgy().endTransaction();
            }
        } else {
            zzab().zzgr().zza("Generating a payload for this event is not available. package_name, event_name", str, zzai.name);
            return null;
        }
    }

    /* access modifiers changed from: protected */
    public final boolean zzbk() {
        return false;
    }
}
