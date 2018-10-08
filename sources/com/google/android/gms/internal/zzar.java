package com.google.android.gms.internal;

import java.io.UnsupportedEncodingException;

public class zzar extends zzp<String> {
    private final zzv<String> zzcd;

    public zzar(int i, String str, zzv<String> zzv, zzu zzu) {
        super(i, str, zzu);
        this.zzcd = zzv;
    }

    protected final zzt<String> zza(zzn zzn) {
        Object str;
        try {
            str = new String(zzn.data, zzam.zza(zzn.zzy));
        } catch (UnsupportedEncodingException e) {
            str = new String(zzn.data);
        }
        return zzt.zza(str, zzam.zzb(zzn));
    }

    protected final /* synthetic */ void zza(Object obj) {
        this.zzcd.zzb((String) obj);
    }
}
