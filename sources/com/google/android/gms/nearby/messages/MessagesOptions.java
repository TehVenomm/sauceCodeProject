package com.google.android.gms.nearby.messages;

import android.support.annotation.Nullable;
import com.google.android.gms.common.api.Api.ApiOptions.Optional;

public class MessagesOptions implements Optional {
    @Nullable
    public final String zzjdz;
    public final boolean zzjea;
    public final int zzjeb;
    public final String zzjec;

    public static class Builder {
        private int zzjed = -1;

        public MessagesOptions build() {
            return new MessagesOptions();
        }

        public Builder setPermissions(int i) {
            this.zzjed = i;
            return this;
        }
    }

    private MessagesOptions(Builder builder) {
        this.zzjdz = null;
        this.zzjea = false;
        this.zzjeb = builder.zzjed;
        this.zzjec = null;
    }
}
