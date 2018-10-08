package com.google.android.gms.common.api;

import android.support.annotation.NonNull;

public class Response<T extends Result> {
    private T zzfhl;

    protected Response(@NonNull T t) {
        this.zzfhl = t;
    }

    @NonNull
    protected T getResult() {
        return this.zzfhl;
    }

    public void setResult(@NonNull T t) {
        this.zzfhl = t;
    }
}
