package com.google.android.gms.internal.measurement;

import android.util.Log;

final class zzco extends zzcm<Boolean> {
    zzco(zzct zzct, String str, Boolean bool) {
        super(zzct, str, bool, null);
    }

    /* access modifiers changed from: 0000 */
    public final /* synthetic */ Object zzc(Object obj) {
        if (obj instanceof Boolean) {
            return (Boolean) obj;
        }
        if (obj instanceof String) {
            String str = (String) obj;
            if (zzbz.zzzw.matcher(str).matches()) {
                return Boolean.valueOf(true);
            }
            if (zzbz.zzzx.matcher(str).matches()) {
                return Boolean.valueOf(false);
            }
        }
        String zzrm = super.zzrm();
        String valueOf = String.valueOf(obj);
        Log.e("PhenotypeFlag", new StringBuilder(String.valueOf(zzrm).length() + 28 + String.valueOf(valueOf).length()).append("Invalid boolean value for ").append(zzrm).append(": ").append(valueOf).toString());
        return null;
    }
}
