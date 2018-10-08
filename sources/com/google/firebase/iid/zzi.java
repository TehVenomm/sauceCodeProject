package com.google.firebase.iid;

import android.support.annotation.Nullable;

@Deprecated
public final class zzi {
    private final FirebaseInstanceId zzmiv;

    private zzi(FirebaseInstanceId firebaseInstanceId) {
        this.zzmiv = firebaseInstanceId;
    }

    public static zzi zzbyh() {
        return new zzi(FirebaseInstanceId.getInstance());
    }

    public final String getId() {
        return this.zzmiv.getId();
    }

    @Nullable
    public final String getToken() {
        return this.zzmiv.getToken();
    }
}
