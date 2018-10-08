package com.google.android.gms.nearby.messages;

import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.zzbp;

public final class SubscribeOptions {
    public static final SubscribeOptions DEFAULT = new Builder().build();
    private final Strategy zzjee;
    private final MessageFilter zzjet;
    @Nullable
    private final SubscribeCallback zzjeu;
    public final boolean zzjev;
    public final int zzjew;

    public static class Builder {
        private Strategy zzjee = Strategy.DEFAULT;
        private MessageFilter zzjet = MessageFilter.INCLUDE_ALL_MY_TYPES;
        @Nullable
        private SubscribeCallback zzjeu;

        public SubscribeOptions build() {
            return new SubscribeOptions(this.zzjee, this.zzjet, this.zzjeu);
        }

        public Builder setCallback(SubscribeCallback subscribeCallback) {
            this.zzjeu = (SubscribeCallback) zzbp.zzu(subscribeCallback);
            return this;
        }

        public Builder setFilter(MessageFilter messageFilter) {
            this.zzjet = messageFilter;
            return this;
        }

        public Builder setStrategy(Strategy strategy) {
            this.zzjee = strategy;
            return this;
        }
    }

    private SubscribeOptions(Strategy strategy, MessageFilter messageFilter, @Nullable SubscribeCallback subscribeCallback, boolean z, int i) {
        this.zzjee = strategy;
        this.zzjet = messageFilter;
        this.zzjeu = subscribeCallback;
        this.zzjev = z;
        this.zzjew = i;
    }

    @Nullable
    public final SubscribeCallback getCallback() {
        return this.zzjeu;
    }

    public final MessageFilter getFilter() {
        return this.zzjet;
    }

    public final Strategy getStrategy() {
        return this.zzjee;
    }
}
