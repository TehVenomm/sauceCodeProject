package com.google.android.gms.internal;

import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zzf;
import com.google.android.gms.common.api.Scope;

public final class zzcpj {
    public static final Api<zzcpn> API = new Api("SignIn.API", zzdwr, zzdwq);
    private static zzf<zzcpw> zzdwq = new zzf();
    public static final zza<zzcpw, zzcpn> zzdwr = new zzcpk();
    private static Scope zzece = new Scope(Scopes.PROFILE);
    private static Scope zzecf = new Scope("email");
    private static Api<Object> zzgde = new Api("SignIn.INTERNAL_API", zzjnc, zzjnb);
    private static zzf<zzcpw> zzjnb = new zzf();
    private static zza<zzcpw, Object> zzjnc = new zzcpl();
}
