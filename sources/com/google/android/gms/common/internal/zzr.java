package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.support.v4.util.ArraySet;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.internal.zzcpn;
import java.util.Collection;

public final class zzr {
    private Account zzdva;
    private String zzdxd;
    private int zzfgx = 0;
    private String zzfgz;
    private zzcpn zzftn = zzcpn.zzjnd;
    private ArraySet<Scope> zzftp;

    public final zzq zzajz() {
        return new zzq(this.zzdva, this.zzftp, null, 0, null, this.zzdxd, this.zzfgz, this.zzftn);
    }

    public final zzr zze(Account account) {
        this.zzdva = account;
        return this;
    }

    public final zzr zze(Collection<Scope> collection) {
        if (this.zzftp == null) {
            this.zzftp = new ArraySet();
        }
        this.zzftp.addAll((Collection) collection);
        return this;
    }

    public final zzr zzfy(String str) {
        this.zzdxd = str;
        return this;
    }

    public final zzr zzfz(String str) {
        this.zzfgz = str;
        return this;
    }
}
