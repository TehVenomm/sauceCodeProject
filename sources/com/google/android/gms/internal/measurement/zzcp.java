package com.google.android.gms.internal.measurement;

import android.util.Log;

final class zzcp extends zzcm<Long> {
    zzcp(zzct zzct, String str, Long l) {
        super(zzct, str, l, null);
    }

    /* access modifiers changed from: private */
    /* renamed from: zzd */
    public final Long zzc(Object obj) {
        if (obj instanceof Long) {
            return (Long) obj;
        }
        if (obj instanceof String) {
            try {
                return Long.valueOf(Long.parseLong((String) obj));
            } catch (NumberFormatException e) {
            }
        }
        String zzrm = super.zzrm();
        String valueOf = String.valueOf(obj);
        Log.e("PhenotypeFlag", new StringBuilder(String.valueOf(zzrm).length() + 25 + String.valueOf(valueOf).length()).append("Invalid long value for ").append(zzrm).append(": ").append(valueOf).toString());
        return null;
    }
}
