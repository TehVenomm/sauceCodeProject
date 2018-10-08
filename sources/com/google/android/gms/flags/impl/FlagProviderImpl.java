package com.google.android.gms.flags.impl;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.PackageManager.NameNotFoundException;
import android.util.Log;
import com.google.android.gms.common.util.DynamiteApi;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.dynamic.zzn;
import com.google.android.gms.internal.zzbvc;

@DynamiteApi
public class FlagProviderImpl extends zzbvc {
    private boolean zzaqo = false;
    private SharedPreferences zzbfr;

    public boolean getBooleanFlagValue(String str, boolean z, int i) {
        return !this.zzaqo ? z : zzb.zza(this.zzbfr, str, Boolean.valueOf(z)).booleanValue();
    }

    public int getIntFlagValue(String str, int i, int i2) {
        return !this.zzaqo ? i : zzd.zza(this.zzbfr, str, Integer.valueOf(i)).intValue();
    }

    public long getLongFlagValue(String str, long j, int i) {
        return !this.zzaqo ? j : zzf.zza(this.zzbfr, str, Long.valueOf(j)).longValue();
    }

    public String getStringFlagValue(String str, String str2, int i) {
        return !this.zzaqo ? str2 : zzh.zza(this.zzbfr, str, str2);
    }

    public void init(IObjectWrapper iObjectWrapper) {
        Context context = (Context) zzn.zzab(iObjectWrapper);
        if (!this.zzaqo) {
            try {
                this.zzbfr = zzj.zzcz(context.createPackageContext("com.google.android.gms", 0));
                this.zzaqo = true;
            } catch (NameNotFoundException e) {
            } catch (Exception e2) {
                String valueOf = String.valueOf(e2.getMessage());
                Log.w("FlagProviderImpl", valueOf.length() != 0 ? "Could not retrieve sdk flags, continuing with defaults: ".concat(valueOf) : new String("Could not retrieve sdk flags, continuing with defaults: "));
            }
        }
    }
}
