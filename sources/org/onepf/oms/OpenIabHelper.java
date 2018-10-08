package org.onepf.oms;

import android.app.Activity;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.ResolveInfo;
import android.content.pm.ServiceInfo;
import android.os.Build.VERSION;
import android.os.Handler;
import android.os.IBinder;
import android.os.Looper;
import android.os.RemoteException;
import android.text.TextUtils;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Queue;
import java.util.Set;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.Semaphore;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.onepf.oms.IOpenAppstore.Stub;
import org.onepf.oms.appstore.AmazonAppstore;
import org.onepf.oms.appstore.FortumoStore;
import org.onepf.oms.appstore.GooglePlay;
import org.onepf.oms.appstore.NokiaStore;
import org.onepf.oms.appstore.OpenAppstore;
import org.onepf.oms.appstore.SamsungApps;
import org.onepf.oms.appstore.SamsungAppsBillingService;
import org.onepf.oms.appstore.SkubitAppstore;
import org.onepf.oms.appstore.SkubitTestAppstore;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnConsumeFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnConsumeMultiFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabPurchaseFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.QueryInventoryFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.appstore.googleUtils.Purchase;
import org.onepf.oms.appstore.googleUtils.Security;
import org.onepf.oms.util.Logger;
import org.onepf.oms.util.Utils;

public class OpenIabHelper {
    public static final int BILLING_RESPONSE_RESULT_BILLING_UNAVAILABLE = 3;
    public static final int BILLING_RESPONSE_RESULT_ERROR = 6;
    public static final int BILLING_RESPONSE_RESULT_OK = 0;
    private static final String BIND_INTENT = "org.onepf.oms.openappstore.BIND";
    public static final String ITEM_TYPE_INAPP = "inapp";
    public static final String ITEM_TYPE_SUBS = "subs";
    public static final String NAME_AMAZON = "com.amazon.apps";
    public static final String NAME_APPLAND = "Appland";
    public static final String NAME_APTOIDE = "cm.aptoide.pt";
    public static final String NAME_FORTUMO = "com.fortumo.billing";
    public static final String NAME_GOOGLE = "com.google.play";
    public static final String NAME_NOKIA = "com.nokia.nstore";
    public static final String NAME_SAMSUNG = "com.samsung.apps";
    public static final String NAME_SKUBIT = "com.skubit.android";
    public static final String NAME_SKUBIT_TEST = "net.skubit.android";
    public static final String NAME_SLIDEME = "SlideME";
    public static final String NAME_YANDEX = "com.yandex.store";
    public static final int SETUP_DISPOSED = 2;
    public static final int SETUP_IN_PROGRESS = 3;
    public static final int SETUP_RESULT_FAILED = 1;
    public static final int SETUP_RESULT_NOT_STARTED = -1;
    public static final int SETUP_RESULT_SUCCESSFUL = 0;
    @Nullable
    private Activity activity;
    @Nullable
    private volatile AppstoreInAppBillingService appStoreBillingService;
    private final Map<String, AppstoreFactory> appStoreFactoryMap;
    @Nullable
    private volatile Appstore appStoreInSetup;
    private final Map<String, String> appStorePackageMap;
    @Nullable
    private volatile Appstore appstore;
    private final Set<Appstore> availableAppstores;
    private final Context context;
    private final Handler handler;
    @NotNull
    private final ExecutorService inventoryExecutor;
    private final Options options;
    private final PackageManager packageManager;
    @Nullable
    private ExecutorService setupExecutorService;
    private volatile int setupState;

    public interface OpenStoresDiscoveredListener {
        void openStoresDiscovered(@NotNull List<Appstore> list);
    }

    private interface AppstoreFactory {
        @Nullable
        Appstore get();
    }

    /* renamed from: org.onepf.oms.OpenIabHelper$1 */
    class C13051 implements AppstoreFactory {
        C13051() {
        }

        @NotNull
        public Appstore get() {
            return new FortumoStore(OpenIabHelper.this.context);
        }
    }

    /* renamed from: org.onepf.oms.OpenIabHelper$2 */
    class C13062 implements AppstoreFactory {
        C13062() {
        }

        @NotNull
        public Appstore get() {
            return new GooglePlay(OpenIabHelper.this.context, OpenIabHelper.this.options.getVerifyMode() != 1 ? (String) OpenIabHelper.this.options.getStoreKeys().get(OpenIabHelper.NAME_GOOGLE) : null);
        }
    }

    /* renamed from: org.onepf.oms.OpenIabHelper$3 */
    class C13073 implements AppstoreFactory {
        C13073() {
        }

        @NotNull
        public Appstore get() {
            return new AmazonAppstore(OpenIabHelper.this.context);
        }
    }

    /* renamed from: org.onepf.oms.OpenIabHelper$4 */
    class C13084 implements AppstoreFactory {
        C13084() {
        }

        @Nullable
        public Appstore get() {
            return new SamsungApps(OpenIabHelper.this.activity, OpenIabHelper.this.options);
        }
    }

    /* renamed from: org.onepf.oms.OpenIabHelper$5 */
    class C13095 implements AppstoreFactory {
        C13095() {
        }

        @NotNull
        public Appstore get() {
            return new NokiaStore(OpenIabHelper.this.context);
        }
    }

    /* renamed from: org.onepf.oms.OpenIabHelper$6 */
    class C13106 implements AppstoreFactory {
        C13106() {
        }

        @NotNull
        public Appstore get() {
            return new SkubitAppstore(OpenIabHelper.this.context);
        }
    }

    /* renamed from: org.onepf.oms.OpenIabHelper$7 */
    class C13117 implements AppstoreFactory {
        C13117() {
        }

        @NotNull
        public Appstore get() {
            return new SkubitTestAppstore(OpenIabHelper.this.context);
        }
    }

    public interface OnInitListener {
        void onInitFinished();
    }

    public interface OnOpenIabHelperInitFinished {
        void onOpenIabHelperInitFinished();
    }

    public static class Options {
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
            public Options build() {
                return new Options(Collections.unmodifiableSet(this.availableStores), Collections.unmodifiableSet(this.availableStoresNames), Collections.unmodifiableMap(this.storeKeys), this.checkInventory, this.verifyMode, Collections.unmodifiableSet(this.preferredStoreNames), this.samsungCertificationRequestCode, this.storeSearchStrategy);
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

        public Options() {
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

        private Options(Set<Appstore> set, Set<String> set2, Map<String, String> map, boolean z, int i, Set<String> set3, int i2, int i3) {
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

    public OpenIabHelper(@NotNull Context context, @NotNull Map<String, String> map) {
        this(context, new Builder().addStoreKeys(map).build());
    }

    public OpenIabHelper(@NotNull Context context, @NotNull Map<String, String> map, String[] strArr) {
        this(context, new Builder().addStoreKeys(map).addPreferredStoreName(strArr).build());
    }

    public OpenIabHelper(@NotNull Context context, @NotNull Map<String, String> map, String[] strArr, Appstore[] appstoreArr) {
        this(context, new Builder().addStoreKeys(map).addPreferredStoreName(strArr).addAvailableStores(appstoreArr).build());
    }

    public OpenIabHelper(@NotNull Context context, Options options) {
        this.setupState = -1;
        this.handler = new Handler(Looper.getMainLooper());
        this.availableAppstores = new LinkedHashSet();
        this.inventoryExecutor = Executors.newSingleThreadExecutor();
        this.appStorePackageMap = new HashMap();
        this.appStoreFactoryMap = new HashMap();
        this.appStorePackageMap.put(NAME_YANDEX, NAME_YANDEX);
        this.appStorePackageMap.put(NAME_APTOIDE, NAME_APTOIDE);
        this.appStoreFactoryMap.put(NAME_FORTUMO, new C13051());
        this.appStorePackageMap.put("com.android.vending", NAME_GOOGLE);
        this.appStoreFactoryMap.put(NAME_GOOGLE, new C13062());
        this.appStorePackageMap.put(AmazonAppstore.AMAZON_INSTALLER, NAME_AMAZON);
        this.appStoreFactoryMap.put(NAME_AMAZON, new C13073());
        this.appStorePackageMap.put(SamsungApps.SAMSUNG_INSTALLER, NAME_SAMSUNG);
        this.appStoreFactoryMap.put(NAME_SAMSUNG, new C13084());
        this.appStorePackageMap.put(NokiaStore.NOKIA_INSTALLER, NAME_NOKIA);
        this.appStoreFactoryMap.put(NAME_NOKIA, new C13095());
        this.appStorePackageMap.put("com.skubit.android", "com.skubit.android");
        this.appStoreFactoryMap.put("com.skubit.android", new C13106());
        this.appStorePackageMap.put("net.skubit.android", "net.skubit.android");
        this.appStoreFactoryMap.put("net.skubit.android", new C13117());
        this.context = context.getApplicationContext();
        this.packageManager = context.getPackageManager();
        this.options = options;
        if (context instanceof Activity) {
            this.activity = (Activity) context;
        }
        checkOptions();
    }

    private void checkAmazon() {
        if (VERSION.SDK_INT >= 21) {
            Logger.m1000d("checkAmazon() Android Lollipop not supported, ignoring amazon wrapper.");
            this.appStoreFactoryMap.remove(NAME_AMAZON);
            return;
        }
        boolean z;
        try {
            OpenIabHelper.class.getClassLoader().loadClass("com.amazon.device.iap.PurchasingService");
            z = true;
        } catch (ClassNotFoundException e) {
            z = false;
        }
        Logger.m1001d("checkAmazon() amazon sdk available: ", Boolean.valueOf(z));
        if (!z) {
            z = this.options.getAvailableStoreByName(NAME_AMAZON) != null || this.options.getAvailableStoreNames().contains(NAME_AMAZON) || this.options.getPreferredStoreNames().contains(NAME_AMAZON);
            Logger.m1001d("checkAmazon() amazon billing required: ", Boolean.valueOf(z));
            if (z) {
                throw new IllegalStateException("You must satisfy amazon sdk dependency.");
            }
            Logger.m1000d("checkAmazon() ignoring amazon wrapper.");
            this.appStoreFactoryMap.remove(NAME_AMAZON);
        }
    }

    private void checkBillingAndFinish(@NotNull final OnIabSetupFinishedListener onIabSetupFinishedListener, @NotNull final Collection<Appstore> collection) {
        if (this.setupState != 3) {
            throw new IllegalStateException("Can't check billing. Current state: " + setupStateToString(this.setupState));
        }
        final String packageName = this.context.getPackageName();
        if (collection.isEmpty()) {
            finishSetup(onIabSetupFinishedListener);
        } else {
            this.setupExecutorService.execute(this.options.isCheckInventory() ? new Runnable() {
                public void run() {
                    final List arrayList = new ArrayList();
                    for (Appstore appstore : collection) {
                        OpenIabHelper.this.appStoreInSetup = appstore;
                        if (appstore.isBillingAvailable(packageName) && OpenIabHelper.this.versionOk(appstore)) {
                            arrayList.add(appstore);
                        }
                    }
                    Appstore appstore2 = OpenIabHelper.this.checkInventory(new HashSet(arrayList));
                    if (appstore2 == null) {
                        appstore2 = arrayList.isEmpty() ? null : (Appstore) arrayList.get(0);
                    }
                    final OnIabSetupFinishedListener c12971 = new OnIabSetupFinishedListener() {
                        public void onIabSetupFinished(IabResult iabResult) {
                            Collection arrayList = new ArrayList(arrayList);
                            if (appstore2 != null) {
                                arrayList.remove(appstore2);
                            }
                            OpenIabHelper.this.dispose(arrayList);
                            onIabSetupFinishedListener.onIabSetupFinished(iabResult);
                        }
                    };
                    OpenIabHelper.this.handler.post(new Runnable() {
                        public void run() {
                            OpenIabHelper.this.finishSetup(c12971, appstore2);
                        }
                    });
                }
            } : new Runnable() {
                public void run() {
                    for (Appstore appstore : collection) {
                        OpenIabHelper.this.appStoreInSetup = appstore;
                        if (appstore.isBillingAvailable(packageName) && OpenIabHelper.this.versionOk(appstore)) {
                            break;
                        }
                    }
                    Appstore appstore2 = null;
                    final OnIabSetupFinishedListener c12991 = new OnIabSetupFinishedListener() {
                        public void onIabSetupFinished(IabResult iabResult) {
                            Collection arrayList = new ArrayList(collection);
                            if (appstore2 != null) {
                                arrayList.remove(appstore2);
                            }
                            OpenIabHelper.this.dispose(arrayList);
                            if (appstore2 != null) {
                                appstore2.getInAppBillingService().startSetup(onIabSetupFinishedListener);
                            } else {
                                onIabSetupFinishedListener.onIabSetupFinished(iabResult);
                            }
                        }
                    };
                    OpenIabHelper.this.handler.post(new Runnable() {
                        public void run() {
                            OpenIabHelper.this.finishSetup(c12991, appstore2);
                        }
                    });
                }
            });
        }
    }

    private void checkBillingAndFinish(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener, @Nullable Appstore appstore) {
        if (appstore == null) {
            finishSetup(onIabSetupFinishedListener);
            return;
        }
        checkBillingAndFinish(onIabSetupFinishedListener, Arrays.asList(new Appstore[]{appstore}));
    }

    private void checkFortumo() {
        boolean z;
        try {
            OpenIabHelper.class.getClassLoader().loadClass("mp.PaymentRequest");
            z = true;
        } catch (ClassNotFoundException e) {
            z = false;
        }
        Logger.m1001d("checkFortumo() fortumo sdk available: ", Boolean.valueOf(z));
        if (!z) {
            z = this.options.getAvailableStoreByName(NAME_FORTUMO) != null || this.options.getAvailableStoreNames().contains(NAME_FORTUMO) || this.options.getPreferredStoreNames().contains(NAME_FORTUMO);
            Logger.m1001d("checkFortumo() fortumo billing required: ", Boolean.valueOf(z));
            if (z) {
                throw new IllegalStateException("You must satisfy fortumo sdk dependency.");
            }
            Logger.m1000d("checkFortumo() ignoring fortumo wrapper.");
            this.appStoreFactoryMap.remove(NAME_FORTUMO);
        }
    }

    private void checkGoogle() {
        int i = 0;
        Logger.m1000d("checkGoogle() verify mode = " + this.options.getVerifyMode());
        if (this.options.getVerifyMode() != 1) {
            Logger.m1001d("checkGoogle() google key available = ", Boolean.valueOf(this.options.getStoreKeys().containsKey(NAME_GOOGLE)));
            if (!this.options.getStoreKeys().containsKey(NAME_GOOGLE)) {
                if (this.options.getAvailableStoreByName(NAME_GOOGLE) != null || this.options.getAvailableStoreNames().contains(NAME_GOOGLE) || this.options.getPreferredStoreNames().contains(NAME_GOOGLE)) {
                    i = 1;
                }
                if (i == 0 || this.options.getVerifyMode() != 0) {
                    Logger.m1000d("checkGoogle() ignoring GooglePlay wrapper.");
                    this.appStoreFactoryMap.remove(NAME_GOOGLE);
                    return;
                }
                throw new IllegalStateException("You must supply Google verification key");
            }
        }
    }

    @Nullable
    private Appstore checkInventory(@NotNull Set<Appstore> set) {
        if (Utils.uiThread()) {
            throw new IllegalStateException("Must not be called from UI thread");
        }
        final Semaphore semaphore = new Semaphore(0);
        final Appstore[] appstoreArr = new Appstore[1];
        for (final Appstore appstore : set) {
            final AppstoreInAppBillingService inAppBillingService = appstore.getInAppBillingService();
            final OnIabSetupFinishedListener anonymousClass15 = new OnIabSetupFinishedListener() {

                /* renamed from: org.onepf.oms.OpenIabHelper$15$1 */
                class C13011 implements Runnable {
                    C13011() {
                    }

                    public void run() {
                        try {
                            Inventory queryInventory = inAppBillingService.queryInventory(false, null, null);
                            if (!(queryInventory == null || queryInventory.getAllPurchases().isEmpty())) {
                                appstoreArr[0] = appstore;
                                Logger.dWithTimeFromUp("inventoryCheck() in ", appstore.getAppstoreName(), " found: ", Integer.valueOf(queryInventory.getAllPurchases().size()), " purchases");
                            }
                        } catch (IabException e) {
                            Logger.m1005e("inventoryCheck() failed for ", appstore.getAppstoreName() + " : ", e);
                        }
                        semaphore.release();
                    }
                }

                public void onIabSetupFinished(@NotNull IabResult iabResult) {
                    if (iabResult.isSuccess()) {
                        OpenIabHelper.this.inventoryExecutor.execute(new C13011());
                        return;
                    }
                    semaphore.release();
                }
            };
            this.handler.post(new Runnable() {
                public void run() {
                    inAppBillingService.startSetup(anonymousClass15);
                }
            });
            try {
                semaphore.acquire();
                if (appstoreArr[0] != null) {
                    return appstoreArr[0];
                }
            } catch (Throwable e) {
                Logger.m1003e("checkInventory() Error during inventory check: ", e);
                return null;
            }
        }
        return null;
    }

    private void checkNokia() {
        Logger.m1001d("checkNokia() has permission = ", Boolean.valueOf(Utils.hasRequestedPermission(this.context, NokiaStore.NOKIA_BILLING_PERMISSION)));
        if (!Utils.hasRequestedPermission(this.context, NokiaStore.NOKIA_BILLING_PERMISSION)) {
            if (this.options.getAvailableStoreByName(NAME_NOKIA) != null || this.options.getAvailableStoreNames().contains(NAME_NOKIA) || this.options.getPreferredStoreNames().contains(NAME_NOKIA)) {
                throw new IllegalStateException("Nokia permission \"com.nokia.payment.BILLING\" NOT REQUESTED");
            }
            Logger.m1000d("checkNokia() ignoring Nokia wrapper");
            this.appStoreFactoryMap.remove(NAME_NOKIA);
        }
    }

    private void checkSamsung() {
        Logger.m1001d("checkSamsung() activity = ", this.activity);
        if (this.activity == null) {
            if (this.options.getAvailableStoreByName(NAME_SAMSUNG) != null || this.options.getAvailableStoreNames().contains(NAME_SAMSUNG) || this.options.getPreferredStoreNames().contains(NAME_SAMSUNG)) {
                throw new IllegalArgumentException("You must supply Activity object as context in order to use com.samsung.apps store");
            }
            Logger.m1000d("checkSamsung() ignoring Samsung wrapper");
            this.appStoreFactoryMap.remove(NAME_SAMSUNG);
        }
    }

    @NotNull
    @Deprecated
    public static List<Appstore> discoverOpenStores(Context context, List<Appstore> list, Options options) {
        throw new UnsupportedOperationException("This action is no longer supported.");
    }

    private void discoverOpenStores(@NotNull final OpenStoresDiscoveredListener openStoresDiscoveredListener, @NotNull final Queue<Intent> queue, @NotNull final List<Appstore> list) {
        while (!queue.isEmpty()) {
            Intent intent = (Intent) queue.poll();
            ServiceConnection anonymousClass14 = new ServiceConnection() {
                public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
                    Object access$600;
                    try {
                        access$600 = OpenIabHelper.this.getOpenAppstore(componentName, iBinder, this);
                    } catch (Throwable e) {
                        Logger.m1010w("onServiceConnected() Error creating appsotre: ", e);
                        access$600 = null;
                    }
                    if (access$600 != null) {
                        list.add(access$600);
                    }
                    OpenIabHelper.this.discoverOpenStores(openStoresDiscoveredListener, queue, list);
                }

                public void onServiceDisconnected(ComponentName componentName) {
                }
            };
            if (!this.context.bindService(intent, anonymousClass14, 1)) {
                this.context.unbindService(anonymousClass14);
                Logger.m1002e("discoverOpenStores() Couldn't connect to open store: " + intent);
            } else {
                return;
            }
        }
        openStoresDiscoveredListener.openStoresDiscovered(Collections.unmodifiableList(list));
    }

    private void dispose(@NotNull Collection<Appstore> collection) {
        for (Appstore inAppBillingService : collection) {
            inAppBillingService.getInAppBillingService().dispose();
            Logger.m1001d("dispose() was called for ", inAppBillingService.getAppstoreName());
        }
    }

    public static void enableDebugLogging(boolean z) {
        enableDebuglLogging(z, null);
    }

    public static void enableDebuglLogging(boolean z, String str) {
        Logger.setLogTag(str);
        Logger.setLoggable(z);
    }

    private void finishSetup(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener) {
        finishSetup(onIabSetupFinishedListener, null);
    }

    private void finishSetup(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener, @Nullable Appstore appstore) {
        finishSetup(onIabSetupFinishedListener, appstore == null ? new IabResult(3, "No suitable appstore was found") : new IabResult(0, "Setup ok"), appstore);
    }

    private void finishSetup(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener, @NotNull IabResult iabResult, @Nullable Appstore appstore) {
        if (Utils.uiThread()) {
            this.activity = null;
            this.appStoreInSetup = null;
            this.setupExecutorService.shutdownNow();
            this.setupExecutorService = null;
            if (this.setupState == 2) {
                if (appstore != null) {
                    dispose(Arrays.asList(new Appstore[]{appstore}));
                    return;
                }
                return;
            } else if (this.setupState != 3) {
                throw new IllegalStateException("Setup is not started or already finished.");
            } else {
                boolean isSuccess = iabResult.isSuccess();
                this.setupState = isSuccess ? 0 : 1;
                if (isSuccess) {
                    if (appstore == null) {
                        throw new IllegalStateException("Appstore can't be null if setup is successful");
                    }
                    this.appstore = appstore;
                    this.appStoreBillingService = appstore.getInAppBillingService();
                }
                Logger.dWithTimeFromUp("finishSetup() === SETUP DONE === result: ", iabResult, " Appstore: ", appstore);
                onIabSetupFinishedListener.onIabSetupFinished(iabResult);
                return;
            }
        }
        throw new IllegalStateException("Must be called from UI thread.");
    }

    private void finishSetupWithError(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener) {
        finishSetupWithError(onIabSetupFinishedListener, null);
    }

    private void finishSetupWithError(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener, @Nullable Exception exception) {
        String str = exception == null ? "" : " : " + exception;
        Logger.m1005e("finishSetupWithError() error occurred during setup", str);
        finishSetup(onIabSetupFinishedListener, new IabResult(6, "Error occured, setup failed"), null);
    }

    @Nullable
    public static List<String> getAllStoreSkus(@NotNull String str) {
        Collection allStoreSkus = SkuManager.getInstance().getAllStoreSkus(str);
        return allStoreSkus == null ? Collections.emptyList() : new ArrayList(allStoreSkus);
    }

    @Nullable
    private Appstore getAvailableStoreByName(@NotNull String str) {
        for (Appstore appstore : this.availableAppstores) {
            if (str.equals(appstore.getAppstoreName())) {
                return appstore;
            }
        }
        return null;
    }

    @NotNull
    private Intent getBindServiceIntent(@NotNull ServiceInfo serviceInfo) {
        Intent intent = new Intent(BIND_INTENT);
        intent.setClassName(serviceInfo.packageName, serviceInfo.name);
        return intent;
    }

    @Nullable
    private OpenAppstore getOpenAppstore(ComponentName componentName, IBinder iBinder, ServiceConnection serviceConnection) throws RemoteException {
        IOpenAppstore asInterface = Stub.asInterface(iBinder);
        Object appstoreName = asInterface.getAppstoreName();
        Intent billingServiceIntent = asInterface.getBillingServiceIntent();
        int verifyMode = this.options.getVerifyMode();
        if (verifyMode == 1) {
            Object obj = null;
        } else {
            String str = (String) this.options.getStoreKeys().get(appstoreName);
        }
        if (TextUtils.isEmpty(appstoreName)) {
            Logger.m1001d("getOpenAppstore() Appstore doesn't have name. Skipped. ComponentName: ", componentName);
            return null;
        } else if (billingServiceIntent == null) {
            Logger.m1001d("getOpenAppstore() billing is not supported by store: ", componentName);
            return null;
        } else if (verifyMode == 0 && TextUtils.isEmpty(obj)) {
            Logger.m1005e("getOpenAppstore() verification is required but publicKey is not provided: ", componentName);
            return null;
        } else {
            OpenAppstore openAppstore = new OpenAppstore(this.context, appstoreName, asInterface, billingServiceIntent, obj, serviceConnection);
            openAppstore.componentName = componentName;
            Logger.m1001d("getOpenAppstore() returns ", openAppstore.getAppstoreName());
            return openAppstore;
        }
    }

    @NotNull
    public static String getSku(@NotNull String str, @NotNull String str2) {
        return SkuManager.getInstance().getSku(str, str2);
    }

    @NotNull
    public static String getStoreSku(@NotNull String str, @NotNull String str2) {
        return SkuManager.getInstance().getStoreSku(str, str2);
    }

    public static boolean isDebugLog() {
        return Logger.isLoggable();
    }

    public static void mapSku(String str, String str2, @NotNull String str3) {
        SkuManager.getInstance().mapSku(str, str2, str3);
    }

    @NotNull
    private List<ServiceInfo> queryOpenStoreServices() {
        List<ResolveInfo> queryIntentServices = this.context.getPackageManager().queryIntentServices(new Intent(BIND_INTENT), 0);
        List arrayList = new ArrayList();
        for (ResolveInfo resolveInfo : queryIntentServices) {
            arrayList.add(resolveInfo.serviceInfo);
        }
        return Collections.unmodifiableList(arrayList);
    }

    private void setup(@NotNull final OnIabSetupFinishedListener onIabSetupFinishedListener) {
        final Collection linkedHashSet = new LinkedHashSet();
        final Set<String> availableStoreNames = this.options.getAvailableStoreNames();
        if (this.availableAppstores.isEmpty() && availableStoreNames.isEmpty()) {
            discoverOpenStores(new OpenStoresDiscoveredListener() {
                public void openStoresDiscovered(@NotNull List<Appstore> list) {
                    Collection<Appstore> arrayList = new ArrayList(list);
                    for (String str : OpenIabHelper.this.appStorePackageMap.keySet()) {
                        String str2 = (String) OpenIabHelper.this.appStorePackageMap.get(str);
                        if (!TextUtils.isEmpty(str2) && OpenIabHelper.this.appStoreFactoryMap.containsKey(str2) && Utils.packageInstalled(OpenIabHelper.this.context, str)) {
                            arrayList.add(((AppstoreFactory) OpenIabHelper.this.appStoreFactoryMap.get(str2)).get());
                        }
                    }
                    for (String str3 : OpenIabHelper.this.appStoreFactoryMap.keySet()) {
                        if (!OpenIabHelper.this.appStorePackageMap.values().contains(str3)) {
                            arrayList.add(((AppstoreFactory) OpenIabHelper.this.appStoreFactoryMap.get(str3)).get());
                        }
                    }
                    for (String str32 : availableStoreNames) {
                        for (Appstore appstore : arrayList) {
                            if (TextUtils.equals(appstore.getAppstoreName(), str32)) {
                                linkedHashSet.add(appstore);
                                break;
                            }
                        }
                    }
                    linkedHashSet.addAll(arrayList);
                    OpenIabHelper.this.checkBillingAndFinish(onIabSetupFinishedListener, linkedHashSet);
                }
            });
            return;
        }
        for (String availableStoreByName : availableStoreNames) {
            Appstore availableStoreByName2 = getAvailableStoreByName(availableStoreByName);
            if (availableStoreByName2 != null) {
                linkedHashSet.add(availableStoreByName2);
            }
        }
        linkedHashSet.addAll(this.availableAppstores);
        checkBillingAndFinish(onIabSetupFinishedListener, linkedHashSet);
    }

    private void setupForPackage(@NotNull final OnIabSetupFinishedListener onIabSetupFinishedListener, @NotNull String str, final boolean z) {
        if (Utils.packageInstalled(this.context, str)) {
            Appstore availableStoreByName;
            Intent bindServiceIntent;
            if (this.appStorePackageMap.containsKey(str)) {
                String str2 = (String) this.appStorePackageMap.get(str);
                if (!this.availableAppstores.isEmpty()) {
                    availableStoreByName = getAvailableStoreByName(str2);
                    if (availableStoreByName == null) {
                        if (z) {
                            setup(onIabSetupFinishedListener);
                            return;
                        } else {
                            finishSetup(onIabSetupFinishedListener);
                            return;
                        }
                    }
                } else if (this.appStoreFactoryMap.containsKey(str2)) {
                    availableStoreByName = ((AppstoreFactory) this.appStoreFactoryMap.get(str2)).get();
                }
                if (availableStoreByName == null) {
                    checkBillingAndFinish(onIabSetupFinishedListener, availableStoreByName);
                }
                for (ServiceInfo serviceInfo : queryOpenStoreServices()) {
                    if (TextUtils.equals(serviceInfo.packageName, str)) {
                        bindServiceIntent = getBindServiceIntent(serviceInfo);
                        break;
                    }
                }
                bindServiceIntent = null;
                if (bindServiceIntent != null) {
                    if (z) {
                        finishSetup(onIabSetupFinishedListener);
                        return;
                    } else {
                        setup(onIabSetupFinishedListener);
                        return;
                    }
                } else if (!this.context.bindService(bindServiceIntent, new ServiceConnection() {
                    public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
                        Appstore access$600;
                        try {
                            access$600 = OpenIabHelper.this.getOpenAppstore(componentName, iBinder, this);
                            if (access$600 != null) {
                                String appstoreName = access$600.getAppstoreName();
                                if (!OpenIabHelper.this.availableAppstores.isEmpty()) {
                                    access$600 = OpenIabHelper.this.getAvailableStoreByName(appstoreName);
                                }
                            } else {
                                access$600 = null;
                            }
                        } catch (Throwable e) {
                            Logger.m1003e("setupForPackage() Error binding to open store service : ", e);
                            access$600 = null;
                        }
                        if (access$600 == null && z) {
                            OpenIabHelper.this.setup(onIabSetupFinishedListener);
                        } else {
                            OpenIabHelper.this.checkBillingAndFinish(onIabSetupFinishedListener, access$600);
                        }
                    }

                    public void onServiceDisconnected(ComponentName componentName) {
                    }
                }, 1)) {
                    Logger.m1002e("setupForPackage() Error binding to open store service");
                    if (z) {
                        finishSetupWithError(onIabSetupFinishedListener);
                        return;
                    } else {
                        setup(onIabSetupFinishedListener);
                        return;
                    }
                } else {
                    return;
                }
            }
            availableStoreByName = null;
            if (availableStoreByName == null) {
                for (ServiceInfo serviceInfo2 : queryOpenStoreServices()) {
                    if (TextUtils.equals(serviceInfo2.packageName, str)) {
                        bindServiceIntent = getBindServiceIntent(serviceInfo2);
                        break;
                    }
                }
                bindServiceIntent = null;
                if (bindServiceIntent != null) {
                    if (!this.context.bindService(bindServiceIntent, /* anonymous class already generated */, 1)) {
                        Logger.m1002e("setupForPackage() Error binding to open store service");
                        if (z) {
                            finishSetupWithError(onIabSetupFinishedListener);
                            return;
                        } else {
                            setup(onIabSetupFinishedListener);
                            return;
                        }
                    }
                    return;
                } else if (z) {
                    finishSetup(onIabSetupFinishedListener);
                    return;
                } else {
                    setup(onIabSetupFinishedListener);
                    return;
                }
            }
            checkBillingAndFinish(onIabSetupFinishedListener, availableStoreByName);
        } else if (z) {
            setup(onIabSetupFinishedListener);
        } else {
            finishSetup(onIabSetupFinishedListener);
        }
    }

    private static String setupStateToString(int i) {
        if (i == -1) {
            return " IAB helper is not set up.";
        }
        if (i == 2) {
            return "IAB helper was disposed of.";
        }
        if (i == 0) {
            return "IAB helper is set up.";
        }
        if (i == 1) {
            return "IAB helper setup failed.";
        }
        if (i == 3) {
            return "IAB helper setup is in progress.";
        }
        throw new IllegalStateException("Wrong setup state: " + i);
    }

    private void setupWithStrategy(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener) {
        int storeSearchStrategy = this.options.getStoreSearchStrategy();
        Logger.m1001d("setupWithStrategy() store search strategy = ", Integer.valueOf(storeSearchStrategy));
        Logger.m1001d("setupWithStrategy() package name = ", this.context.getPackageName());
        String installerPackageName = this.packageManager.getInstallerPackageName(r0);
        Logger.m1001d("setupWithStrategy() package installer = ", installerPackageName);
        boolean z = !TextUtils.isEmpty(installerPackageName);
        if (storeSearchStrategy == 0) {
            if (z) {
                setupForPackage(onIabSetupFinishedListener, installerPackageName, false);
            } else {
                finishSetup(onIabSetupFinishedListener);
            }
        } else if (storeSearchStrategy != 2) {
            setup(onIabSetupFinishedListener);
        } else if (z) {
            setupForPackage(onIabSetupFinishedListener, installerPackageName, true);
        } else {
            setup(onIabSetupFinishedListener);
        }
    }

    private boolean versionOk(@NotNull Appstore appstore) {
        try {
            int i = this.context.getPackageManager().getPackageInfo(this.context.getPackageName(), 0).versionCode;
        } catch (NameNotFoundException e) {
        }
        return true;
    }

    public void checkOptions() {
        Logger.m1001d("checkOptions() ", this.options);
        checkGoogle();
        checkSamsung();
        checkNokia();
        checkFortumo();
        checkAmazon();
    }

    void checkSetupDone(String str) {
        if (!setupSuccessful()) {
            Logger.m1005e("Illegal state for operation (", str, "): ", setupStateToString(this.setupState));
            throw new IllegalStateException(setupStateToString(this.setupState) + " Can't perform operation: " + str);
        }
    }

    public void consume(@NotNull Purchase purchase) throws IabException {
        Appstore appstore = this.appstore;
        AppstoreInAppBillingService appstoreInAppBillingService = this.appStoreBillingService;
        if (this.setupState == 0 && appstore != null && appstoreInAppBillingService != null) {
            Purchase purchase2 = (Purchase) purchase.clone();
            purchase2.setSku(SkuManager.getInstance().getStoreSku(appstore.getAppstoreName(), purchase.getSku()));
            appstoreInAppBillingService.consume(purchase2);
        }
    }

    public void consumeAsync(@NotNull List<Purchase> list, @NotNull OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
        if (onConsumeMultiFinishedListener == null) {
            throw new IllegalArgumentException("Consume listener must be not null!");
        }
        consumeAsyncInternal(list, null, onConsumeMultiFinishedListener);
    }

    public void consumeAsync(@NotNull Purchase purchase, @NotNull OnConsumeFinishedListener onConsumeFinishedListener) {
        consumeAsyncInternal(Arrays.asList(new Purchase[]{purchase}), onConsumeFinishedListener, null);
    }

    void consumeAsyncInternal(@NotNull final List<Purchase> list, @Nullable final OnConsumeFinishedListener onConsumeFinishedListener, @Nullable final OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
        checkSetupDone("consume");
        if (list.isEmpty()) {
            throw new IllegalArgumentException("Nothing to consume.");
        }
        new Thread(new Runnable() {
            public void run() {
                final List arrayList = new ArrayList();
                for (Purchase purchase : list) {
                    try {
                        OpenIabHelper.this.consume(purchase);
                        arrayList.add(new IabResult(0, "Successful consume of sku " + purchase.getSku()));
                    } catch (Throwable e) {
                        arrayList.add(e.getResult());
                        Logger.m1003e("consumeAsyncInternal() Error : ", e);
                    }
                }
                if (onConsumeFinishedListener != null) {
                    OpenIabHelper.this.handler.post(new Runnable() {
                        public void run() {
                            if (OpenIabHelper.this.setupState == 0) {
                                onConsumeFinishedListener.onConsumeFinished((Purchase) list.get(0), (IabResult) arrayList.get(0));
                            }
                        }
                    });
                }
                if (onConsumeMultiFinishedListener != null) {
                    OpenIabHelper.this.handler.post(new Runnable() {
                        public void run() {
                            if (OpenIabHelper.this.setupState == 0) {
                                onConsumeMultiFinishedListener.onConsumeMultiFinished(list, arrayList);
                            }
                        }
                    });
                }
            }
        }).start();
    }

    @Nullable
    public List<Appstore> discoverOpenStores() {
        if (Utils.uiThread()) {
            throw new IllegalStateException("Must not be called from UI thread");
        }
        final List<Appstore> arrayList = new ArrayList();
        final CountDownLatch countDownLatch = new CountDownLatch(1);
        discoverOpenStores(new OpenStoresDiscoveredListener() {
            public void openStoresDiscovered(@NotNull List<Appstore> list) {
                arrayList.addAll(list);
                countDownLatch.notify();
            }
        });
        try {
            countDownLatch.await();
            return arrayList;
        } catch (InterruptedException e) {
            return null;
        }
    }

    public void discoverOpenStores(@NotNull OpenStoresDiscoveredListener openStoresDiscoveredListener) {
        List<ServiceInfo> queryOpenStoreServices = queryOpenStoreServices();
        Queue linkedList = new LinkedList();
        for (ServiceInfo bindServiceIntent : queryOpenStoreServices) {
            linkedList.add(getBindServiceIntent(bindServiceIntent));
        }
        discoverOpenStores(openStoresDiscoveredListener, linkedList, new ArrayList());
    }

    public void dispose() {
        Logger.m1000d("Disposing.");
        if (this.appStoreBillingService != null) {
            this.appStoreBillingService.dispose();
        }
        this.appstore = null;
        this.appStoreBillingService = null;
        this.activity = null;
        this.setupState = 2;
    }

    @Nullable
    public String getConnectedAppstoreName() {
        return this.appstore == null ? null : this.appstore.getAppstoreName();
    }

    public int getSetupState() {
        return this.setupState;
    }

    public boolean handleActivityResult(int i, int i2, Intent intent) {
        Logger.dWithTimeFromUp("handleActivityResult() requestCode: ", Integer.valueOf(i), " resultCode: ", Integer.valueOf(i2), " data: ", intent);
        if (i == this.options.samsungCertificationRequestCode && this.appStoreInSetup != null) {
            return this.appStoreInSetup.getInAppBillingService().handleActivityResult(i, i2, intent);
        }
        if (this.setupState == 0) {
            return this.appStoreBillingService.handleActivityResult(i, i2, intent);
        }
        Logger.m1001d("handleActivityResult() setup is not done. requestCode: ", Integer.valueOf(i), " resultCode: ", Integer.valueOf(i2), " data: ", intent);
        return false;
    }

    public void launchPurchaseFlow(Activity activity, @NotNull String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener) {
        launchPurchaseFlow(activity, str, i, onIabPurchaseFinishedListener, "");
    }

    public void launchPurchaseFlow(Activity activity, @NotNull String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2) {
        launchPurchaseFlow(activity, str, "inapp", i, onIabPurchaseFinishedListener, str2);
    }

    public void launchPurchaseFlow(Activity activity, @NotNull String str, String str2, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str3) {
        checkSetupDone("launchPurchaseFlow");
        this.appStoreBillingService.launchPurchaseFlow(activity, SkuManager.getInstance().getStoreSku(this.appstore.getAppstoreName(), str), str2, i, onIabPurchaseFinishedListener, str3);
    }

    public void launchSubscriptionPurchaseFlow(Activity activity, @NotNull String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener) {
        launchSubscriptionPurchaseFlow(activity, str, i, onIabPurchaseFinishedListener, "");
    }

    public void launchSubscriptionPurchaseFlow(Activity activity, @NotNull String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2) {
        launchPurchaseFlow(activity, str, "subs", i, onIabPurchaseFinishedListener, str2);
    }

    @Nullable
    public Inventory queryInventory(boolean z, @Nullable List<String> list) throws IabException {
        return queryInventory(z, list, null);
    }

    @Nullable
    public Inventory queryInventory(boolean z, @Nullable List<String> list, @Nullable List<String> list2) throws IabException {
        if (Utils.uiThread()) {
            throw new IllegalStateException("Must not be called from the UI thread");
        }
        Appstore appstore = this.appstore;
        AppstoreInAppBillingService appstoreInAppBillingService = this.appStoreBillingService;
        if (this.setupState != 0 || appstore == null || appstoreInAppBillingService == null) {
            return null;
        }
        List arrayList;
        List list3;
        SkuManager instance = SkuManager.getInstance();
        if (list != null) {
            arrayList = new ArrayList(list.size());
            for (String storeSku : list) {
                arrayList.add(instance.getStoreSku(appstore.getAppstoreName(), storeSku));
            }
        } else {
            arrayList = null;
        }
        if (list2 != null) {
            List arrayList2 = new ArrayList(list2.size());
            for (String storeSku2 : list2) {
                arrayList2.add(instance.getStoreSku(appstore.getAppstoreName(), storeSku2));
            }
            list3 = arrayList2;
        } else {
            list3 = null;
        }
        return appstoreInAppBillingService.queryInventory(z, arrayList, list3);
    }

    public void queryInventoryAsync(@NotNull QueryInventoryFinishedListener queryInventoryFinishedListener) {
        queryInventoryAsync(true, queryInventoryFinishedListener);
    }

    public void queryInventoryAsync(boolean z, @Nullable List<String> list, @Nullable List<String> list2, @NotNull QueryInventoryFinishedListener queryInventoryFinishedListener) {
        checkSetupDone("queryInventory");
        if (queryInventoryFinishedListener == null) {
            throw new IllegalArgumentException("Inventory listener must be not null");
        }
        final boolean z2 = z;
        final List<String> list3 = list;
        final List<String> list4 = list2;
        final QueryInventoryFinishedListener queryInventoryFinishedListener2 = queryInventoryFinishedListener;
        new Thread(new Runnable() {
            public void run() {
                IabResult iabResult;
                Inventory inventory = null;
                try {
                    inventory = OpenIabHelper.this.queryInventory(z2, list3, list4);
                    iabResult = new IabResult(0, "Inventory refresh successful.");
                } catch (Throwable e) {
                    Throwable th = e;
                    iabResult = th.getResult();
                    Logger.m1003e("queryInventoryAsync() Error : ", th);
                }
                OpenIabHelper.this.handler.post(new Runnable() {
                    public void run() {
                        if (OpenIabHelper.this.setupState == 0) {
                            queryInventoryFinishedListener2.onQueryInventoryFinished(iabResult, inventory);
                        }
                    }
                });
            }
        }).start();
    }

    public void queryInventoryAsync(boolean z, @Nullable List<String> list, @NotNull QueryInventoryFinishedListener queryInventoryFinishedListener) {
        queryInventoryAsync(z, list, null, queryInventoryFinishedListener);
    }

    public void queryInventoryAsync(boolean z, @NotNull QueryInventoryFinishedListener queryInventoryFinishedListener) {
        queryInventoryAsync(z, null, queryInventoryFinishedListener);
    }

    public boolean setupSuccessful() {
        return this.setupState == 0;
    }

    public void startSetup(@NotNull final OnIabSetupFinishedListener onIabSetupFinishedListener) {
        if (this.options != null) {
            Logger.m1001d("startSetup() options = ", this.options);
        }
        if (onIabSetupFinishedListener == null) {
            throw new IllegalArgumentException("Setup listener must be not null!");
        } else if (this.setupState == -1 || this.setupState == 1) {
            this.setupState = 3;
            this.setupExecutorService = Executors.newSingleThreadExecutor();
            this.availableAppstores.clear();
            this.availableAppstores.addAll(this.options.getAvailableStores());
            final List arrayList = new ArrayList(this.options.getAvailableStoreNames());
            for (Appstore appstoreName : this.availableAppstores) {
                arrayList.remove(appstoreName.getAppstoreName());
            }
            final List arrayList2 = new ArrayList();
            for (String str : this.options.getAvailableStoreNames()) {
                if (this.appStoreFactoryMap.containsKey(str)) {
                    Appstore appstore = ((AppstoreFactory) this.appStoreFactoryMap.get(str)).get();
                    arrayList2.add(appstore);
                    this.availableAppstores.add(appstore);
                    arrayList.remove(str);
                }
            }
            if (arrayList.isEmpty()) {
                setupWithStrategy(onIabSetupFinishedListener);
            } else {
                discoverOpenStores(new OpenStoresDiscoveredListener() {

                    /* renamed from: org.onepf.oms.OpenIabHelper$8$1 */
                    class C13121 implements OnIabSetupFinishedListener {
                        C13121() {
                        }

                        public void onIabSetupFinished(IabResult iabResult) {
                            onIabSetupFinishedListener.onIabSetupFinished(iabResult);
                            arrayList2.remove(OpenIabHelper.this.appstore);
                            for (Appstore inAppBillingService : arrayList2) {
                                AppstoreInAppBillingService inAppBillingService2 = inAppBillingService.getInAppBillingService();
                                if (inAppBillingService2 != null) {
                                    inAppBillingService2.dispose();
                                    Logger.m1001d("startSetup() billing service disposed for ", inAppBillingService.getAppstoreName());
                                }
                            }
                        }
                    }

                    public void openStoresDiscovered(@NotNull List<Appstore> list) {
                        for (Appstore appstore : list) {
                            if (arrayList.contains(appstore.getAppstoreName())) {
                                OpenIabHelper.this.availableAppstores.add(appstore);
                            } else {
                                AppstoreInAppBillingService inAppBillingService = appstore.getInAppBillingService();
                                if (inAppBillingService != null) {
                                    inAppBillingService.dispose();
                                    Logger.m1001d("startSetup() billing service disposed for ", appstore.getAppstoreName());
                                }
                            }
                        }
                        OpenIabHelper.this.setupWithStrategy(new C13121());
                    }
                });
            }
        } else {
            throw new IllegalStateException("Couldn't be set up. Current state: " + setupStateToString(this.setupState));
        }
    }

    public boolean subscriptionsSupported() {
        checkSetupDone("subscriptionsSupported");
        if (this.setupState == 0) {
            return this.appStoreBillingService.subscriptionsSupported();
        }
        throw new IllegalStateException("OpenIabHelper is not set up.");
    }
}
