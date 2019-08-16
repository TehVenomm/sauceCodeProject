package com.crashlytics.android.core;

import java.io.InputStream;
import p017io.fabric.sdk.android.services.network.PinningInfoProvider;

class CrashlyticsPinningInfoProvider implements PinningInfoProvider {
    private final PinningInfoProvider pinningInfo;

    public CrashlyticsPinningInfoProvider(PinningInfoProvider pinningInfoProvider) {
        this.pinningInfo = pinningInfoProvider;
    }

    public String getKeyStorePassword() {
        return this.pinningInfo.getKeyStorePassword();
    }

    public InputStream getKeyStoreStream() {
        return this.pinningInfo.getKeyStoreStream();
    }

    public long getPinCreationTimeInMillis() {
        return -1;
    }

    public String[] getPins() {
        return this.pinningInfo.getPins();
    }
}
