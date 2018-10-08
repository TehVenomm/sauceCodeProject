package org.onepf.oms;

import android.content.Intent;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

public abstract class DefaultAppstore implements Appstore {
    public boolean areOutsideLinksAllowed() {
        return false;
    }

    @Nullable
    public AppstoreInAppBillingService getInAppBillingService() {
        return null;
    }

    @Nullable
    public Intent getProductPageIntent(String str) {
        return null;
    }

    @Nullable
    public Intent getRateItPageIntent(String str) {
        return null;
    }

    @Nullable
    public Intent getSameDeveloperPageIntent(String str) {
        return null;
    }

    @NotNull
    public String toString() {
        return "Store {name: " + getAppstoreName() + "}";
    }
}
