package com.google.android.gms.auth.account;

import android.accounts.Account;
import com.google.android.gms.auth.account.WorkAccountApi.AddAccountResult;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.internal.zzbn;

final class zzg implements zzbn<AddAccountResult, Account> {
    zzg(WorkAccountClient workAccountClient) {
    }

    public final /* synthetic */ Object zzb(Result result) {
        return ((AddAccountResult) result).getAccount();
    }
}
