package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.content.Context;
import android.view.View;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient.Builder;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.internal.zzcpn;
import java.util.Collections;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public final class zzq {
    private final Account zzdva;
    private final String zzdxd;
    private final Set<Scope> zzfgv;
    private final int zzfgx;
    private final View zzfgy;
    private final String zzfgz;
    private final Set<Scope> zzftl;
    private final Map<Api<?>, zzs> zzftm;
    private final zzcpn zzftn;
    private Integer zzfto;

    public zzq(Account account, Set<Scope> set, Map<Api<?>, zzs> map, int i, View view, String str, String str2, zzcpn zzcpn) {
        Map map2;
        this.zzdva = account;
        this.zzfgv = set == null ? Collections.EMPTY_SET : Collections.unmodifiableSet(set);
        if (map == null) {
            map2 = Collections.EMPTY_MAP;
        }
        this.zzftm = map2;
        this.zzfgy = view;
        this.zzfgx = i;
        this.zzdxd = str;
        this.zzfgz = str2;
        this.zzftn = zzcpn;
        Set hashSet = new HashSet(this.zzfgv);
        for (zzs zzs : this.zzftm.values()) {
            hashSet.addAll(zzs.zzecn);
        }
        this.zzftl = Collections.unmodifiableSet(hashSet);
    }

    public static zzq zzcd(Context context) {
        return new Builder(context).zzafq();
    }

    public final Account getAccount() {
        return this.zzdva;
    }

    @Deprecated
    public final String getAccountName() {
        return this.zzdva != null ? this.zzdva.name : null;
    }

    public final Account zzajp() {
        return this.zzdva != null ? this.zzdva : new Account("<<default account>>", "com.google");
    }

    public final int zzajq() {
        return this.zzfgx;
    }

    public final Set<Scope> zzajr() {
        return this.zzfgv;
    }

    public final Set<Scope> zzajs() {
        return this.zzftl;
    }

    public final Map<Api<?>, zzs> zzajt() {
        return this.zzftm;
    }

    public final String zzaju() {
        return this.zzdxd;
    }

    public final String zzajv() {
        return this.zzfgz;
    }

    public final View zzajw() {
        return this.zzfgy;
    }

    public final zzcpn zzajx() {
        return this.zzftn;
    }

    public final Integer zzajy() {
        return this.zzfto;
    }

    public final Set<Scope> zzc(Api<?> api) {
        zzs zzs = (zzs) this.zzftm.get(api);
        if (zzs == null || zzs.zzecn.isEmpty()) {
            return this.zzfgv;
        }
        Set<Scope> hashSet = new HashSet(this.zzfgv);
        hashSet.addAll(zzs.zzecn);
        return hashSet;
    }

    public final void zzc(Integer num) {
        this.zzfto = num;
    }
}
