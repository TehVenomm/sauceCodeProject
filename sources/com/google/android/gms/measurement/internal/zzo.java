package com.google.android.gms.measurement.internal;

import com.google.android.gms.internal.measurement.zzbk.zzc.zzb;
import com.google.android.gms.internal.measurement.zzbk.zze.zza;

final /* synthetic */ class zzo {
    static final /* synthetic */ int[] zzdu = new int[zza.values().length];
    static final /* synthetic */ int[] zzdv = new int[zzb.values().length];

    static {
        try {
            zzdv[zzb.LESS_THAN.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            zzdv[zzb.GREATER_THAN.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            zzdv[zzb.EQUAL.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            zzdv[zzb.BETWEEN.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
        try {
            zzdu[zza.REGEXP.ordinal()] = 1;
        } catch (NoSuchFieldError e5) {
        }
        try {
            zzdu[zza.BEGINS_WITH.ordinal()] = 2;
        } catch (NoSuchFieldError e6) {
        }
        try {
            zzdu[zza.ENDS_WITH.ordinal()] = 3;
        } catch (NoSuchFieldError e7) {
        }
        try {
            zzdu[zza.PARTIAL.ordinal()] = 4;
        } catch (NoSuchFieldError e8) {
        }
        try {
            zzdu[zza.EXACT.ordinal()] = 5;
        } catch (NoSuchFieldError e9) {
        }
        try {
            zzdu[zza.IN_LIST.ordinal()] = 6;
        } catch (NoSuchFieldError e10) {
        }
    }
}
