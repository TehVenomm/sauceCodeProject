package com.google.android.gms.common.api;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;

public class ApiException extends Exception {
    protected final Status mStatus;

    public ApiException(@NonNull Status status) {
        int statusCode = status.getStatusCode();
        String str = status.getStatusMessage() != null ? status.getStatusMessage() : "";
        super(new StringBuilder(String.valueOf(str).length() + 13).append(statusCode).append(": ").append(str).toString());
        this.mStatus = status;
    }

    public int getStatusCode() {
        return this.mStatus.getStatusCode();
    }

    @Nullable
    @Deprecated
    public String getStatusMessage() {
        return this.mStatus.getStatusMessage();
    }
}
