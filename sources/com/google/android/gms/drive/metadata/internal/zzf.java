package com.google.android.gms.drive.metadata.internal;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.metadata.MetadataField;
import com.google.android.gms.internal.zzbnr;
import com.google.android.gms.internal.zzboc;
import com.google.android.gms.internal.zzboe;
import com.google.android.gms.internal.zzbom;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;

public final class zzf {
    private static final Map<String, MetadataField<?>> zzgkn = new HashMap();
    private static final Map<String, zzg> zzgko = new HashMap();

    static {
        zzb(zzbnr.zzgkt);
        zzb(zzbnr.zzglz);
        zzb(zzbnr.zzglq);
        zzb(zzbnr.zzglx);
        zzb(zzbnr.zzgma);
        zzb(zzbnr.zzglg);
        zzb(zzbnr.zzglf);
        zzb(zzbnr.zzglh);
        zzb(zzbnr.zzgli);
        zzb(zzbnr.zzglj);
        zzb(zzbnr.zzgld);
        zzb(zzbnr.zzgll);
        zzb(zzbnr.zzglm);
        zzb(zzbnr.zzgln);
        zzb(zzbnr.zzglv);
        zzb(zzbnr.zzgku);
        zzb(zzbnr.zzgls);
        zzb(zzbnr.zzgkw);
        zzb(zzbnr.zzgle);
        zzb(zzbnr.zzgkx);
        zzb(zzbnr.zzgky);
        zzb(zzbnr.zzgkz);
        zzb(zzbnr.zzgla);
        zzb(zzbnr.zzglp);
        zzb(zzbnr.zzglk);
        zzb(zzbnr.zzglr);
        zzb(zzbnr.zzglt);
        zzb(zzbnr.zzglu);
        zzb(zzbnr.zzglw);
        zzb(zzbnr.zzgmb);
        zzb(zzbnr.zzgmc);
        zzb(zzbnr.zzglc);
        zzb(zzbnr.zzglb);
        zzb(zzbnr.zzgly);
        zzb(zzbnr.zzglo);
        zzb(zzbnr.zzgkv);
        zzb(zzbnr.zzgmd);
        zzb(zzbnr.zzgme);
        zzb(zzbnr.zzgmf);
        zzb(zzbnr.zzgmg);
        zzb(zzbnr.zzgmh);
        zzb(zzbnr.zzgmi);
        zzb(zzbnr.zzgmj);
        zzb(zzboe.zzgml);
        zzb(zzboe.zzgmn);
        zzb(zzboe.zzgmo);
        zzb(zzboe.zzgmp);
        zzb(zzboe.zzgmm);
        zzb(zzboe.zzgmq);
        zzb(zzbom.zzgms);
        zzb(zzbom.zzgmt);
        zza(zzo.zzgks);
        zza(zzboc.zzgmk);
    }

    private static void zza(zzg zzg) {
        if (zzgko.put(zzg.zzans(), zzg) != null) {
            String zzans = zzg.zzans();
            throw new IllegalStateException(new StringBuilder(String.valueOf(zzans).length() + 46).append("A cleaner for key ").append(zzans).append(" has already been registered").toString());
        }
    }

    public static Collection<MetadataField<?>> zzanr() {
        return Collections.unmodifiableCollection(zzgkn.values());
    }

    public static void zzb(DataHolder dataHolder) {
        for (zzg zzc : zzgko.values()) {
            zzc.zzc(dataHolder);
        }
    }

    private static void zzb(MetadataField<?> metadataField) {
        if (zzgkn.containsKey(metadataField.getName())) {
            String valueOf = String.valueOf(metadataField.getName());
            throw new IllegalArgumentException(valueOf.length() != 0 ? "Duplicate field name registered: ".concat(valueOf) : new String("Duplicate field name registered: "));
        } else {
            zzgkn.put(metadataField.getName(), metadataField);
        }
    }

    public static MetadataField<?> zzgr(String str) {
        return (MetadataField) zzgkn.get(str);
    }
}
