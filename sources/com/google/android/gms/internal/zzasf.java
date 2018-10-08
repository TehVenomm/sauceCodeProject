package com.google.android.gms.internal;

import com.google.android.gms.auth.api.credentials.Credential;
import com.google.android.gms.auth.api.credentials.CredentialRequestResult;
import com.google.android.gms.common.api.Status;

public final class zzasf implements CredentialRequestResult {
    private final Status mStatus;
    private final Credential zzebh;

    public zzasf(Status status, Credential credential) {
        this.mStatus = status;
        this.zzebh = credential;
    }

    public static zzasf zzf(Status status) {
        return new zzasf(status, null);
    }

    public final Credential getCredential() {
        return this.zzebh;
    }

    public final Status getStatus() {
        return this.mStatus;
    }
}
