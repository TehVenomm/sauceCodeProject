package com.google.android.gms.internal;

import android.accounts.Account;
import com.google.android.gms.common.api.Status;

final class zzara extends zzard {
    private /* synthetic */ zzaqz zzdyb;

    zzara(zzaqz zzaqz) {
        this.zzdyb = zzaqz;
    }

    public final void zzd(Account account) {
        this.zzdyb.setResult(new zzare(account != null ? Status.zzfhp : zzaqx.zzdya, account));
    }
}
