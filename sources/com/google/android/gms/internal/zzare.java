package com.google.android.gms.internal;

import android.accounts.Account;
import com.google.android.gms.auth.account.WorkAccountApi.AddAccountResult;
import com.google.android.gms.common.api.Status;

final class zzare implements AddAccountResult {
    private final Status mStatus;
    private final Account zzdva;

    public zzare(Status status, Account account) {
        this.mStatus = status;
        this.zzdva = account;
    }

    public final Account getAccount() {
        return this.zzdva;
    }

    public final Status getStatus() {
        return this.mStatus;
    }
}
