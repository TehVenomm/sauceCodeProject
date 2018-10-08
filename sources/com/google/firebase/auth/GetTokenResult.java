package com.google.firebase.auth;

import android.support.annotation.Nullable;

public class GetTokenResult {
    private String zzdxu;

    public GetTokenResult(String str) {
        this.zzdxu = str;
    }

    @Nullable
    public String getToken() {
        return this.zzdxu;
    }
}
