package com.google.android.gms.internal.measurement;

import android.util.Log;

final class zzcr extends zzcm<Double> {
    zzcr(zzct zzct, String str, Double d) {
        super(zzct, str, d, null);
    }

    /* access modifiers changed from: private */
    /* renamed from: zze */
    public final Double zzc(Object obj) {
        if (obj instanceof Double) {
            return (Double) obj;
        }
        if (obj instanceof Float) {
            return Double.valueOf(((Float) obj).doubleValue());
        }
        if (obj instanceof String) {
            try {
                return Double.valueOf(Double.parseDouble((String) obj));
            } catch (NumberFormatException e) {
            }
        }
        String zzrm = super.zzrm();
        String valueOf = String.valueOf(obj);
        Log.e("PhenotypeFlag", new StringBuilder(String.valueOf(zzrm).length() + 27 + String.valueOf(valueOf).length()).append("Invalid double value for ").append(zzrm).append(": ").append(valueOf).toString());
        return null;
    }
}
