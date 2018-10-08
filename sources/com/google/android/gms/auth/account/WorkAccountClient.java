package com.google.android.gms.auth.account;

import android.accounts.Account;
import android.app.Activity;
import android.content.Context;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Api.ApiOptions.NoOptions;
import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.common.api.GoogleApi.zza;
import com.google.android.gms.common.internal.zzbi;
import com.google.android.gms.internal.zzaqx;
import com.google.android.gms.tasks.Task;

public class WorkAccountClient extends GoogleApi<NoOptions> {
    private final WorkAccountApi zzdxz = new zzaqx();

    WorkAccountClient(@NonNull Activity activity) {
        super(activity, WorkAccount.API, null, zza.zzfgq);
    }

    WorkAccountClient(@NonNull Context context) {
        super(context, WorkAccount.API, null, zza.zzfgq);
    }

    public Task<Account> addWorkAccount(String str) {
        return zzbi.zza(this.zzdxz.addWorkAccount(zzafk(), str), new zzg(this));
    }

    public Task<Void> removeWorkAccount(Account account) {
        return zzbi.zzb(this.zzdxz.removeWorkAccount(zzafk(), account));
    }

    public Task<Void> setWorkAuthenticatorEnabled(boolean z) {
        return zzbi.zzb(this.zzdxz.setWorkAuthenticatorEnabledWithResult(zzafk(), z));
    }
}
