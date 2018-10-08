package org.onepf.oms;

import android.text.TextUtils;
import java.util.Arrays;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.Map;
import java.util.Set;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.onepf.oms.appstore.SamsungAppsBillingService;
import org.onepf.oms.appstore.googleUtils.Security;

public class OpenIabHelper$Options {
    public static final int SEARCH_STRATEGY_BEST_FIT = 1;
    public static final int SEARCH_STRATEGY_INSTALLER = 0;
    public static final int SEARCH_STRATEGY_INSTALLER_THEN_BEST_FIT = 2;
    public static final int VERIFY_EVERYTHING = 0;
    public static final int VERIFY_ONLY_KNOWN = 2;
    public static final int VERIFY_SKIP = 1;
    private final Set<String> availableStoreNames;
    public final Set<Appstore> availableStores;
    public final boolean checkInventory;
    public final int checkInventoryTimeoutMs;
    public final int discoveryTimeoutMs;
    public final Set<String> preferredStoreNames;
    public final int samsungCertificationRequestCode;
    private final Map<String, String> storeKeys;
    private final int storeSearchStrategy;
    public final int verifyMode;

    /* renamed from: org.onepf.oms.OpenIabHelper$Options$Builder */
    public static final class Builder {
        private final Set<Appstore> availableStores = new HashSet();
        private final Set<String> availableStoresNames = new LinkedHashSet();
        private boolean checkInventory = false;
        private final Set<String> preferredStoreNames = new LinkedHashSet();
        private int samsungCertificationRequestCode = SamsungAppsBillingService.REQUEST_CODE_IS_ACCOUNT_CERTIFICATION;
        private final Map<String, String> storeKeys = new HashMap();
        private int storeSearchStrategy = 0;
        private int verifyMode = 0;

        @NotNull
        public Builder addAvailableStoreNames(@NotNull Collection<String> collection) {
            this.availableStoresNames.addAll(collection);
            return this;
        }

        @NotNull
        public Builder addAvailableStoreNames(@NotNull String... strArr) {
            addAvailableStoreNames(Arrays.asList(strArr));
            return this;
        }

        @NotNull
        public Builder addAvailableStores(@NotNull Collection<Appstore> collection) {
            this.availableStores.addAll(collection);
            return this;
        }

        @NotNull
        public Builder addAvailableStores(@NotNull Appstore... appstoreArr) {
            addAvailableStores(Arrays.asList(appstoreArr));
            return this;
        }

        @NotNull
        public Builder addPreferredStoreName(@NotNull Collection<String> collection) {
            this.preferredStoreNames.addAll(collection);
            return this;
        }

        @NotNull
        public Builder addPreferredStoreName(@NotNull String... strArr) {
            addPreferredStoreName(Arrays.asList(strArr));
            return this;
        }

        @NotNull
        public Builder addStoreKey(@NotNull String str, @NotNull String str2) {
            try {
                Security.generatePublicKey(str2);
                this.storeKeys.put(str, str2);
                return this;
            } catch (Throwable e) {
                throw new IllegalArgumentException(String.format("Invalid publicKey for store: %s, key: %s.", new Object[]{str, str2}), e);
            }
        }

        @NotNull
        public Builder addStoreKeys(@NotNull Map<String, String> map) {
            for (String str : map.keySet()) {
                String str2 = (String) map.get(str);
                if (!TextUtils.isEmpty(str2)) {
                    addStoreKey(str, str2);
                }
            }
            return this;
        }

        @NotNull
        public OpenIabHelper$Options build() {
            return new OpenIabHelper$Options(Collections.unmodifiableSet(this.availableStores), Collections.unmodifiableSet(this.availableStoresNames), Collections.unmodifiableMap(this.storeKeys), this.checkInventory, this.verifyMode, Collections.unmodifiableSet(this.preferredStoreNames), this.samsungCertificationRequestCode, this.storeSearchStrategy);
        }

        @NotNull
        public Builder setCheckInventory(boolean z) {
            this.checkInventory = z;
            return this;
        }

        @NotNull
        @Deprecated
        public Builder setCheckInventoryTimeout(int i) {
            return this;
        }

        @NotNull
        @Deprecated
        public Builder setDiscoveryTimeout(int i) {
            return this;
        }

        @NotNull
        public Builder setSamsungCertificationRequestCode(int i) {
            if (i <= 0) {
                throw new IllegalArgumentException("Value '" + i + "' can't be request code. Request code must be a positive value.");
            }
            this.samsungCertificationRequestCode = i;
            return this;
        }

        @NotNull
        public Builder setStoreSearchStrategy(int i) {
            this.storeSearchStrategy = i;
            return this;
        }

        @NotNull
        public Builder setVerifyMode(int i) {
            this.verifyMode = i;
            return this;
        }
    }

    public OpenIabHelper$Options() {
        this.discoveryTimeoutMs = 0;
        this.checkInventoryTimeoutMs = 0;
        this.checkInventory = false;
        this.availableStores = Collections.emptySet();
        this.availableStoreNames = Collections.emptySet();
        this.storeKeys = Collections.emptyMap();
        this.preferredStoreNames = Collections.emptySet();
        this.verifyMode = 1;
        this.samsungCertificationRequestCode = SamsungAppsBillingService.REQUEST_CODE_IS_ACCOUNT_CERTIFICATION;
        this.storeSearchStrategy = 0;
    }

    private OpenIabHelper$Options(Set<Appstore> set, Set<String> set2, Map<String, String> map, boolean z, int i, Set<String> set3, int i2, int i3) {
        this.discoveryTimeoutMs = 0;
        this.checkInventoryTimeoutMs = 0;
        this.checkInventory = z;
        this.availableStores = set;
        this.availableStoreNames = set2;
        this.storeKeys = map;
        this.preferredStoreNames = set3;
        this.verifyMode = i;
        this.samsungCertificationRequestCode = i2;
        this.storeSearchStrategy = i3;
    }

    @Nullable
    public Appstore getAvailableStoreByName(@NotNull String str) {
        for (Appstore appstore : this.availableStores) {
            if (str.equals(appstore.getAppstoreName())) {
                return appstore;
            }
        }
        return null;
    }

    public Set<String> getAvailableStoreNames() {
        return this.availableStoreNames;
    }

    @NotNull
    public Set<Appstore> getAvailableStores() {
        return this.availableStores;
    }

    @Deprecated
    public long getCheckInventoryTimeout() {
        return 0;
    }

    @Deprecated
    public long getDiscoveryTimeout() {
        return 0;
    }

    @NotNull
    public Set<String> getPreferredStoreNames() {
        return this.preferredStoreNames;
    }

    public int getSamsungCertificationRequestCode() {
        return this.samsungCertificationRequestCode;
    }

    @NotNull
    public Map<String, String> getStoreKeys() {
        return this.storeKeys;
    }

    public int getStoreSearchStrategy() {
        return this.storeSearchStrategy;
    }

    public int getVerifyMode() {
        return this.verifyMode;
    }

    public boolean isCheckInventory() {
        return this.checkInventory;
    }

    public String toString() {
        return "Options={availableStores=" + this.availableStores + ", availableStoreNames=" + this.availableStoreNames + ", preferredStoreNames=" + this.preferredStoreNames + ", discoveryTimeoutMs=" + 0 + ", checkInventory=" + this.checkInventory + ", checkInventoryTimeoutMs=" + 0 + ", verifyMode=" + this.verifyMode + ", storeSearchStrategy=" + this.storeSearchStrategy + ", storeKeys=" + this.storeKeys + ", samsungCertificationRequestCode=" + this.samsungCertificationRequestCode + '}';
    }
}
