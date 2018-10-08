package com.google.android.gms.nearby.messages;

import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.zzbp;

public final class PublishOptions {
    public static final PublishOptions DEFAULT = new Builder().build();
    private final Strategy zzjee;
    @Nullable
    private final PublishCallback zzjef;

    public static class Builder {
        private Strategy zzjee = Strategy.DEFAULT;
        @Nullable
        private PublishCallback zzjef;

        public PublishOptions build() {
            return new PublishOptions(this.zzjee, this.zzjef);
        }

        public Builder setCallback(PublishCallback publishCallback) {
            this.zzjef = (PublishCallback) zzbp.zzu(publishCallback);
            return this;
        }

        public Builder setStrategy(Strategy strategy) {
            this.zzjee = (Strategy) zzbp.zzu(strategy);
            return this;
        }
    }

    private PublishOptions(Strategy strategy, @Nullable PublishCallback publishCallback) {
        this.zzjee = strategy;
        this.zzjef = publishCallback;
    }

    @Nullable
    public final PublishCallback getCallback() {
        return this.zzjef;
    }

    public final Strategy getStrategy() {
        return this.zzjee;
    }
}
