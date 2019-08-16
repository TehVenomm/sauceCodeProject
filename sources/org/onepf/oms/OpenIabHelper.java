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
import java.util.Iterator;
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
    /* access modifiers changed from: private */
    @Nullable
    public Activity activity;
    @Nullable
    private volatile AppstoreInAppBillingService appStoreBillingService;
    /* access modifiers changed from: private */
    public final Map<String, AppstoreFactory> appStoreFactoryMap;
    /* access modifiers changed from: private */
    @Nullable
    public volatile Appstore appStoreInSetup;
    /* access modifiers changed from: private */
    public final Map<String, String> appStorePackageMap;
    /* access modifiers changed from: private */
    @Nullable
    public volatile Appstore appstore;
    /* access modifiers changed from: private */
    public final Set<Appstore> availableAppstores;
    /* access modifiers changed from: private */
    public final Context context;
    /* access modifiers changed from: private */
    public final Handler handler;
    /* access modifiers changed from: private */
    @NotNull
    public final ExecutorService inventoryExecutor;
    /* access modifiers changed from: private */
    public final Options options;
    private final PackageManager packageManager;
    @Nullable
    private ExecutorService setupExecutorService;
    /* access modifiers changed from: private */
    public volatile int setupState;

    private interface AppstoreFactory {
        @Nullable
        Appstore get();
    }

    public interface OnInitListener {
        void onInitFinished();
    }

    public interface OnOpenIabHelperInitFinished {
        void onOpenIabHelperInitFinished();
    }

    public interface OpenStoresDiscoveredListener {
        void openStoresDiscovered(@NotNull List<Appstore> list);
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
                addAvailableStoreNames((Collection<String>) Arrays.asList(strArr));
                return this;
            }

            @NotNull
            public Builder addAvailableStores(@NotNull Collection<Appstore> collection) {
                this.availableStores.addAll(collection);
                return this;
            }

            @NotNull
            public Builder addAvailableStores(@NotNull Appstore... appstoreArr) {
                addAvailableStores((Collection<Appstore>) Arrays.asList(appstoreArr));
                return this;
            }

            @NotNull
            public Builder addPreferredStoreName(@NotNull Collection<String> collection) {
                this.preferredStoreNames.addAll(collection);
                return this;
            }

            @NotNull
            public Builder addPreferredStoreName(@NotNull String... strArr) {
                addPreferredStoreName((Collection<String>) Arrays.asList(strArr));
                return this;
            }

            @NotNull
            public Builder addStoreKey(@NotNull String str, @NotNull String str2) {
                try {
                    Security.generatePublicKey(str2);
                    this.storeKeys.put(str, str2);
                    return this;
                } catch (Exception e) {
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

    public OpenIabHelper(@NotNull Context context2, @NotNull Map<String, String> map) {
        this(context2, new Builder().addStoreKeys(map).build());
    }

    public OpenIabHelper(@NotNull Context context2, @NotNull Map<String, String> map, String[] strArr) {
        this(context2, new Builder().addStoreKeys(map).addPreferredStoreName(strArr).build());
    }

    public OpenIabHelper(@NotNull Context context2, @NotNull Map<String, String> map, String[] strArr, Appstore[] appstoreArr) {
        this(context2, new Builder().addStoreKeys(map).addPreferredStoreName(strArr).addAvailableStores(appstoreArr).build());
    }

    public OpenIabHelper(@NotNull Context context2, Options options2) {
        this.setupState = -1;
        this.handler = new Handler(Looper.getMainLooper());
        this.availableAppstores = new LinkedHashSet();
        this.inventoryExecutor = Executors.newSingleThreadExecutor();
        this.appStorePackageMap = new HashMap();
        this.appStoreFactoryMap = new HashMap();
        this.appStorePackageMap.put(NAME_YANDEX, NAME_YANDEX);
        this.appStorePackageMap.put(NAME_APTOIDE, NAME_APTOIDE);
        this.appStoreFactoryMap.put(NAME_FORTUMO, new AppstoreFactory() {
            @NotNull
            public Appstore get() {
                return new FortumoStore(OpenIabHelper.this.context);
            }
        });
        this.appStorePackageMap.put("com.android.vending", NAME_GOOGLE);
        this.appStoreFactoryMap.put(NAME_GOOGLE, new AppstoreFactory() {
            @NotNull
            public Appstore get() {
                return new GooglePlay(OpenIabHelper.this.context, OpenIabHelper.this.options.getVerifyMode() != 1 ? (String) OpenIabHelper.this.options.getStoreKeys().get(OpenIabHelper.NAME_GOOGLE) : null);
            }
        });
        this.appStorePackageMap.put(AmazonAppstore.AMAZON_INSTALLER, NAME_AMAZON);
        this.appStoreFactoryMap.put(NAME_AMAZON, new AppstoreFactory() {
            @NotNull
            public Appstore get() {
                return new AmazonAppstore(OpenIabHelper.this.context);
            }
        });
        this.appStorePackageMap.put(SamsungApps.SAMSUNG_INSTALLER, NAME_SAMSUNG);
        this.appStoreFactoryMap.put(NAME_SAMSUNG, new AppstoreFactory() {
            @Nullable
            public Appstore get() {
                return new SamsungApps(OpenIabHelper.this.activity, OpenIabHelper.this.options);
            }
        });
        this.appStorePackageMap.put(NokiaStore.NOKIA_INSTALLER, NAME_NOKIA);
        this.appStoreFactoryMap.put(NAME_NOKIA, new AppstoreFactory() {
            @NotNull
            public Appstore get() {
                return new NokiaStore(OpenIabHelper.this.context);
            }
        });
        this.appStorePackageMap.put("com.skubit.android", "com.skubit.android");
        this.appStoreFactoryMap.put("com.skubit.android", new AppstoreFactory() {
            @NotNull
            public Appstore get() {
                return new SkubitAppstore(OpenIabHelper.this.context);
            }
        });
        this.appStorePackageMap.put("net.skubit.android", "net.skubit.android");
        this.appStoreFactoryMap.put("net.skubit.android", new AppstoreFactory() {
            @NotNull
            public Appstore get() {
                return new SkubitTestAppstore(OpenIabHelper.this.context);
            }
        });
        this.context = context2.getApplicationContext();
        this.packageManager = context2.getPackageManager();
        this.options = options2;
        if (context2 instanceof Activity) {
            this.activity = (Activity) context2;
        }
        checkOptions();
    }

    private void checkAmazon() {
        boolean z;
        if (VERSION.SDK_INT >= 21) {
            Logger.m1025d("checkAmazon() Android Lollipop not supported, ignoring amazon wrapper.");
            this.appStoreFactoryMap.remove(NAME_AMAZON);
            return;
        }
        try {
            OpenIabHelper.class.getClassLoader().loadClass("com.amazon.device.iap.PurchasingService");
            z = true;
        } catch (ClassNotFoundException e) {
            z = false;
        }
        Logger.m1026d("checkAmazon() amazon sdk available: ", Boolean.valueOf(z));
        if (!z) {
            boolean z2 = this.options.getAvailableStoreByName(NAME_AMAZON) != null || this.options.getAvailableStoreNames().contains(NAME_AMAZON) || this.options.getPreferredStoreNames().contains(NAME_AMAZON);
            Logger.m1026d("checkAmazon() amazon billing required: ", Boolean.valueOf(z2));
            if (z2) {
                throw new IllegalStateException("You must satisfy amazon sdk dependency.");
            }
            Logger.m1025d("checkAmazon() ignoring amazon wrapper.");
            this.appStoreFactoryMap.remove(NAME_AMAZON);
        }
    }

    /* access modifiers changed from: private */
    public void checkBillingAndFinish(@NotNull final OnIabSetupFinishedListener onIabSetupFinishedListener, @NotNull final Collection<Appstore> collection) {
        if (this.setupState != 3) {
            throw new IllegalStateException("Can't check billing. Current state: " + setupStateToString(this.setupState));
        }
        final String packageName = this.context.getPackageName();
        if (collection.isEmpty()) {
            finishSetup(onIabSetupFinishedListener);
        } else {
            this.setupExecutorService.execute(this.options.isCheckInventory() ? new Runnable() {
                public void run() {
                    final ArrayList arrayList = new ArrayList();
                    for (Appstore appstore : collection) {
                        OpenIabHelper.this.appStoreInSetup = appstore;
                        if (appstore.isBillingAvailable(packageName) && OpenIabHelper.this.versionOk(appstore)) {
                            arrayList.add(appstore);
                        }
                    }
                    final Appstore access$1500 = OpenIabHelper.this.checkInventory(new HashSet(arrayList));
                    if (access$1500 == null) {
                        access$1500 = arrayList.isEmpty() ? null : (Appstore) arrayList.get(0);
                    }
                    final C15091 r2 = new OnIabSetupFinishedListener() {
                        public void onIabSetupFinished(IabResult iabResult) {
                            ArrayList arrayList = new ArrayList(arrayList);
                            if (access$1500 != null) {
                                arrayList.remove(access$1500);
                            }
                            OpenIabHelper.this.dispose(arrayList);
                            onIabSetupFinishedListener.onIabSetupFinished(iabResult);
                        }
                    };
                    OpenIabHelper.this.handler.post(new Runnable() {
                        public void run() {
                            OpenIabHelper.this.finishSetup(r2, access$1500);
                        }
                    });
                }
            } : new Runnable() {
                public void run() {
                    final Appstore appstore;
                    Iterator it = collection.iterator();
                    while (true) {
                        if (!it.hasNext()) {
                            appstore = null;
                            break;
                        }
                        appstore = (Appstore) it.next();
                        OpenIabHelper.this.appStoreInSetup = appstore;
                        if (appstore.isBillingAvailable(packageName) && OpenIabHelper.this.versionOk(appstore)) {
                            break;
                        }
                    }
                    final C15121 r1 = new OnIabSetupFinishedListener() {
                        public void onIabSetupFinished(IabResult iabResult) {
                            ArrayList arrayList = new ArrayList(collection);
                            if (appstore != null) {
                                arrayList.remove(appstore);
                            }
                            OpenIabHelper.this.dispose(arrayList);
                            if (appstore != null) {
                                appstore.getInAppBillingService().startSetup(onIabSetupFinishedListener);
                            } else {
                                onIabSetupFinishedListener.onIabSetupFinished(iabResult);
                            }
                        }
                    };
                    OpenIabHelper.this.handler.post(new Runnable() {
                        public void run() {
                            OpenIabHelper.this.finishSetup(r1, appstore);
                        }
                    });
                }
            });
        }
    }

    /* access modifiers changed from: private */
    public void checkBillingAndFinish(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener, @Nullable Appstore appstore2) {
        if (appstore2 == null) {
            finishSetup(onIabSetupFinishedListener);
            return;
        }
        checkBillingAndFinish(onIabSetupFinishedListener, (Collection<Appstore>) Arrays.asList(new Appstore[]{appstore2}));
    }

    private void checkFortumo() {
        boolean z;
        try {
            OpenIabHelper.class.getClassLoader().loadClass("mp.PaymentRequest");
            z = true;
        } catch (ClassNotFoundException e) {
            z = false;
        }
        Logger.m1026d("checkFortumo() fortumo sdk available: ", Boolean.valueOf(z));
        if (!z) {
            boolean z2 = this.options.getAvailableStoreByName(NAME_FORTUMO) != null || this.options.getAvailableStoreNames().contains(NAME_FORTUMO) || this.options.getPreferredStoreNames().contains(NAME_FORTUMO);
            Logger.m1026d("checkFortumo() fortumo billing required: ", Boolean.valueOf(z2));
            if (z2) {
                throw new IllegalStateException("You must satisfy fortumo sdk dependency.");
            }
            Logger.m1025d("checkFortumo() ignoring fortumo wrapper.");
            this.appStoreFactoryMap.remove(NAME_FORTUMO);
        }
    }

    private void checkGoogle() {
        boolean z = false;
        Logger.m1025d("checkGoogle() verify mode = " + this.options.getVerifyMode());
        if (this.options.getVerifyMode() != 1) {
            boolean containsKey = this.options.getStoreKeys().containsKey(NAME_GOOGLE);
            Logger.m1026d("checkGoogle() google key available = ", Boolean.valueOf(containsKey));
            if (!containsKey) {
                if (this.options.getAvailableStoreByName(NAME_GOOGLE) != null || this.options.getAvailableStoreNames().contains(NAME_GOOGLE) || this.options.getPreferredStoreNames().contains(NAME_GOOGLE)) {
                    z = true;
                }
                if (!z || this.options.getVerifyMode() != 0) {
                    Logger.m1025d("checkGoogle() ignoring GooglePlay wrapper.");
                    this.appStoreFactoryMap.remove(NAME_GOOGLE);
                    return;
                }
                throw new IllegalStateException("You must supply Google verification key");
            }
        }
    }

    /* access modifiers changed from: private */
    @Nullable
    public Appstore checkInventory(@NotNull Set<Appstore> set) {
        if (Utils.uiThread()) {
            throw new IllegalStateException("Must not be called from UI thread");
        }
        final Semaphore semaphore = new Semaphore(0);
        final Appstore[] appstoreArr = new Appstore[1];
        for (final Appstore appstore2 : set) {
            final AppstoreInAppBillingService inAppBillingService = appstore2.getInAppBillingService();
            final C151615 r0 = new OnIabSetupFinishedListener() {
                public void onIabSetupFinished(@NotNull IabResult iabResult) {
                    if (!iabResult.isSuccess()) {
                        semaphore.release();
                        return;
                    }
                    OpenIabHelper.this.inventoryExecutor.execute(new Runnable() {
                        public void run() {
                            try {
                                Inventory queryInventory = inAppBillingService.queryInventory(false, null, null);
                                if (queryInventory != null && !queryInventory.getAllPurchases().isEmpty()) {
                                    appstoreArr[0] = appstore2;
                                    Logger.dWithTimeFromUp("inventoryCheck() in ", appstore2.getAppstoreName(), " found: ", Integer.valueOf(queryInventory.getAllPurchases().size()), " purchases");
                                }
                            } catch (IabException e) {
                                Logger.m1030e("inventoryCheck() failed for ", appstore2.getAppstoreName() + " : ", e);
                            }
                            semaphore.release();
                        }
                    });
                }
            };
            this.handler.post(new Runnable() {
                public void run() {
                    inAppBillingService.startSetup(r0);
                }
            });
            try {
                semaphore.acquire();
                if (appstoreArr[0] != null) {
                    return appstoreArr[0];
                }
            } catch (InterruptedException e) {
                Logger.m1028e("checkInventory() Error during inventory check: ", (Throwable) e);
                return null;
            }
        }
        return null;
    }

    private void checkNokia() {
        boolean hasRequestedPermission = Utils.hasRequestedPermission(this.context, NokiaStore.NOKIA_BILLING_PERMISSION);
        Logger.m1026d("checkNokia() has permission = ", Boolean.valueOf(hasRequestedPermission));
        if (!hasRequestedPermission) {
            if (this.options.getAvailableStoreByName(NAME_NOKIA) != null || this.options.getAvailableStoreNames().contains(NAME_NOKIA) || this.options.getPreferredStoreNames().contains(NAME_NOKIA)) {
                throw new IllegalStateException("Nokia permission \"com.nokia.payment.BILLING\" NOT REQUESTED");
            }
            Logger.m1025d("checkNokia() ignoring Nokia wrapper");
            this.appStoreFactoryMap.remove(NAME_NOKIA);
        }
    }

    private void checkSamsung() {
        Logger.m1026d("checkSamsung() activity = ", this.activity);
        if (this.activity == null) {
            if (this.options.getAvailableStoreByName(NAME_SAMSUNG) != null || this.options.getAvailableStoreNames().contains(NAME_SAMSUNG) || this.options.getPreferredStoreNames().contains(NAME_SAMSUNG)) {
                throw new IllegalArgumentException("You must supply Activity object as context in order to use com.samsung.apps store");
            }
            Logger.m1025d("checkSamsung() ignoring Samsung wrapper");
            this.appStoreFactoryMap.remove(NAME_SAMSUNG);
        }
    }

    @NotNull
    @Deprecated
    public static List<Appstore> discoverOpenStores(Context context2, List<Appstore> list, Options options2) {
        throw new UnsupportedOperationException("This action is no longer supported.");
    }

    /* access modifiers changed from: private */
    public void discoverOpenStores(@NotNull final OpenStoresDiscoveredListener openStoresDiscoveredListener, @NotNull final Queue<Intent> queue, @NotNull final List<Appstore> list) {
        while (!queue.isEmpty()) {
            Intent intent = (Intent) queue.poll();
            C151514 r1 = new ServiceConnection() {
                public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
                    OpenAppstore openAppstore;
                    try {
                        openAppstore = OpenIabHelper.this.getOpenAppstore(componentName, iBinder, this);
                    } catch (RemoteException e) {
                        Logger.m1035w("onServiceConnected() Error creating appsotre: ", e);
                        openAppstore = null;
                    }
                    if (openAppstore != null) {
                        list.add(openAppstore);
                    }
                    OpenIabHelper.this.discoverOpenStores(openStoresDiscoveredListener, queue, list);
                }

                public void onServiceDisconnected(ComponentName componentName) {
                }
            };
            if (!this.context.bindService(intent, r1, 1)) {
                this.context.unbindService(r1);
                Logger.m1027e("discoverOpenStores() Couldn't connect to open store: " + intent);
            } else {
                return;
            }
        }
        openStoresDiscoveredListener.openStoresDiscovered(Collections.unmodifiableList(list));
    }

    /* access modifiers changed from: private */
    public void dispose(@NotNull Collection<Appstore> collection) {
        for (Appstore appstore2 : collection) {
            appstore2.getInAppBillingService().dispose();
            Logger.m1026d("dispose() was called for ", appstore2.getAppstoreName());
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

    /* access modifiers changed from: private */
    public void finishSetup(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener, @Nullable Appstore appstore2) {
        finishSetup(onIabSetupFinishedListener, appstore2 == null ? new IabResult(3, "No suitable appstore was found") : new IabResult(0, "Setup ok"), appstore2);
    }

    private void finishSetup(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener, @NotNull IabResult iabResult, @Nullable Appstore appstore2) {
        if (!Utils.uiThread()) {
            throw new IllegalStateException("Must be called from UI thread.");
        }
        this.activity = null;
        this.appStoreInSetup = null;
        this.setupExecutorService.shutdownNow();
        this.setupExecutorService = null;
        if (this.setupState == 2) {
            if (appstore2 != null) {
                dispose(Arrays.asList(new Appstore[]{appstore2}));
            }
        } else if (this.setupState != 3) {
            throw new IllegalStateException("Setup is not started or already finished.");
        } else {
            boolean isSuccess = iabResult.isSuccess();
            this.setupState = isSuccess ? 0 : 1;
            if (isSuccess) {
                if (appstore2 == null) {
                    throw new IllegalStateException("Appstore can't be null if setup is successful");
                }
                this.appstore = appstore2;
                this.appStoreBillingService = appstore2.getInAppBillingService();
            }
            Logger.dWithTimeFromUp("finishSetup() === SETUP DONE === result: ", iabResult, " Appstore: ", appstore2);
            onIabSetupFinishedListener.onIabSetupFinished(iabResult);
        }
    }

    private void finishSetupWithError(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener) {
        finishSetupWithError(onIabSetupFinishedListener, null);
    }

    private void finishSetupWithError(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener, @Nullable Exception exc) {
        Logger.m1030e("finishSetupWithError() error occurred during setup", exc == null ? "" : " : " + exc);
        finishSetup(onIabSetupFinishedListener, new IabResult(6, "Error occured, setup failed"), null);
    }

    @Nullable
    public static List<String> getAllStoreSkus(@NotNull String str) {
        List allStoreSkus = SkuManager.getInstance().getAllStoreSkus(str);
        return allStoreSkus == null ? Collections.emptyList() : new ArrayList(allStoreSkus);
    }

    /* access modifiers changed from: private */
    @Nullable
    public Appstore getAvailableStoreByName(@NotNull String str) {
        for (Appstore appstore2 : this.availableAppstores) {
            if (str.equals(appstore2.getAppstoreName())) {
                return appstore2;
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

    /* access modifiers changed from: private */
    @Nullable
    public OpenAppstore getOpenAppstore(ComponentName componentName, IBinder iBinder, ServiceConnection serviceConnection) throws RemoteException {
        IOpenAppstore asInterface = Stub.asInterface(iBinder);
        String appstoreName = asInterface.getAppstoreName();
        Intent billingServiceIntent = asInterface.getBillingServiceIntent();
        int verifyMode = this.options.getVerifyMode();
        String str = verifyMode == 1 ? null : (String) this.options.getStoreKeys().get(appstoreName);
        if (TextUtils.isEmpty(appstoreName)) {
            Logger.m1026d("getOpenAppstore() Appstore doesn't have name. Skipped. ComponentName: ", componentName);
            return null;
        } else if (billingServiceIntent == null) {
            Logger.m1026d("getOpenAppstore() billing is not supported by store: ", componentName);
            return null;
        } else if (verifyMode != 0 || !TextUtils.isEmpty(str)) {
            OpenAppstore openAppstore = new OpenAppstore(this.context, appstoreName, asInterface, billingServiceIntent, str, serviceConnection);
            openAppstore.componentName = componentName;
            Logger.m1026d("getOpenAppstore() returns ", openAppstore.getAppstoreName());
            return openAppstore;
        } else {
            Logger.m1030e("getOpenAppstore() verification is required but publicKey is not provided: ", componentName);
            return null;
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
        ArrayList arrayList = new ArrayList();
        for (ResolveInfo resolveInfo : queryIntentServices) {
            arrayList.add(resolveInfo.serviceInfo);
        }
        return Collections.unmodifiableList(arrayList);
    }

    /* access modifiers changed from: private */
    public void setup(@NotNull final OnIabSetupFinishedListener onIabSetupFinishedListener) {
        final LinkedHashSet linkedHashSet = new LinkedHashSet();
        final Set<String> availableStoreNames = this.options.getAvailableStoreNames();
        if (!this.availableAppstores.isEmpty() || !availableStoreNames.isEmpty()) {
            for (String availableStoreByName : availableStoreNames) {
                Appstore availableStoreByName2 = getAvailableStoreByName(availableStoreByName);
                if (availableStoreByName2 != null) {
                    linkedHashSet.add(availableStoreByName2);
                }
            }
            linkedHashSet.addAll(this.availableAppstores);
            checkBillingAndFinish(onIabSetupFinishedListener, (Collection<Appstore>) linkedHashSet);
            return;
        }
        discoverOpenStores(new OpenStoresDiscoveredListener() {
            public void openStoresDiscovered(@NotNull List<Appstore> list) {
                ArrayList arrayList = new ArrayList(list);
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
                for (String str4 : availableStoreNames) {
                    Iterator it = arrayList.iterator();
                    while (true) {
                        if (!it.hasNext()) {
                            break;
                        }
                        Appstore appstore = (Appstore) it.next();
                        if (TextUtils.equals(appstore.getAppstoreName(), str4)) {
                            linkedHashSet.add(appstore);
                            break;
                        }
                    }
                }
                linkedHashSet.addAll(arrayList);
                OpenIabHelper.this.checkBillingAndFinish(onIabSetupFinishedListener, (Collection<Appstore>) linkedHashSet);
            }
        });
    }

    /* JADX WARNING: Removed duplicated region for block: B:13:0x0041  */
    /* JADX WARNING: Removed duplicated region for block: B:19:0x0055  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void setupForPackage(@org.jetbrains.annotations.NotNull final org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener r5, @org.jetbrains.annotations.NotNull java.lang.String r6, final boolean r7) {
        /*
            r4 = this;
            r1 = 0
            android.content.Context r0 = r4.context
            boolean r0 = org.onepf.oms.util.Utils.packageInstalled(r0, r6)
            if (r0 != 0) goto L_0x0013
            if (r7 == 0) goto L_0x000f
            r4.setup(r5)
        L_0x000e:
            return
        L_0x000f:
            r4.finishSetup(r5)
            goto L_0x000e
        L_0x0013:
            java.util.Map<java.lang.String, java.lang.String> r0 = r4.appStorePackageMap
            boolean r0 = r0.containsKey(r6)
            if (r0 == 0) goto L_0x00a2
            java.util.Map<java.lang.String, java.lang.String> r0 = r4.appStorePackageMap
            java.lang.Object r0 = r0.get(r6)
            java.lang.String r0 = (java.lang.String) r0
            java.util.Set<org.onepf.oms.Appstore> r2 = r4.availableAppstores
            boolean r2 = r2.isEmpty()
            if (r2 == 0) goto L_0x0045
            java.util.Map<java.lang.String, org.onepf.oms.OpenIabHelper$AppstoreFactory> r2 = r4.appStoreFactoryMap
            boolean r2 = r2.containsKey(r0)
            if (r2 == 0) goto L_0x00a2
            java.util.Map<java.lang.String, org.onepf.oms.OpenIabHelper$AppstoreFactory> r2 = r4.appStoreFactoryMap
            java.lang.Object r0 = r2.get(r0)
            org.onepf.oms.OpenIabHelper$AppstoreFactory r0 = (org.onepf.oms.OpenIabHelper.AppstoreFactory) r0
            org.onepf.oms.Appstore r0 = r0.get()
        L_0x003f:
            if (r0 == 0) goto L_0x0055
            r4.checkBillingAndFinish(r5, r0)
            goto L_0x000e
        L_0x0045:
            org.onepf.oms.Appstore r0 = r4.getAvailableStoreByName(r0)
            if (r0 != 0) goto L_0x003f
            if (r7 == 0) goto L_0x0051
            r4.setup(r5)
            goto L_0x000e
        L_0x0051:
            r4.finishSetup(r5)
            goto L_0x000e
        L_0x0055:
            java.util.List r0 = r4.queryOpenStoreServices()
            java.util.Iterator r2 = r0.iterator()
        L_0x005d:
            boolean r0 = r2.hasNext()
            if (r0 == 0) goto L_0x00a0
            java.lang.Object r0 = r2.next()
            android.content.pm.ServiceInfo r0 = (android.content.pm.ServiceInfo) r0
            java.lang.String r3 = r0.packageName
            boolean r3 = android.text.TextUtils.equals(r3, r6)
            if (r3 == 0) goto L_0x005d
            android.content.Intent r0 = r4.getBindServiceIntent(r0)
        L_0x0075:
            if (r0 != 0) goto L_0x0081
            if (r7 == 0) goto L_0x007d
            r4.setup(r5)
            goto L_0x000e
        L_0x007d:
            r4.finishSetup(r5)
            goto L_0x000e
        L_0x0081:
            android.content.Context r1 = r4.context
            org.onepf.oms.OpenIabHelper$9 r2 = new org.onepf.oms.OpenIabHelper$9
            r2.<init>(r7, r5)
            r3 = 1
            boolean r0 = r1.bindService(r0, r2, r3)
            if (r0 != 0) goto L_0x000e
            java.lang.String r0 = "setupForPackage() Error binding to open store service"
            org.onepf.oms.util.Logger.m1027e(r0)
            if (r7 == 0) goto L_0x009b
            r4.setup(r5)
            goto L_0x000e
        L_0x009b:
            r4.finishSetupWithError(r5)
            goto L_0x000e
        L_0x00a0:
            r0 = r1
            goto L_0x0075
        L_0x00a2:
            r0 = r1
            goto L_0x003f
        */
        throw new UnsupportedOperationException("Method not decompiled: org.onepf.oms.OpenIabHelper.setupForPackage(org.onepf.oms.appstore.googleUtils.IabHelper$OnIabSetupFinishedListener, java.lang.String, boolean):void");
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

    /* access modifiers changed from: private */
    public void setupWithStrategy(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener) {
        int storeSearchStrategy = this.options.getStoreSearchStrategy();
        Logger.m1026d("setupWithStrategy() store search strategy = ", Integer.valueOf(storeSearchStrategy));
        String packageName = this.context.getPackageName();
        Logger.m1026d("setupWithStrategy() package name = ", packageName);
        String installerPackageName = this.packageManager.getInstallerPackageName(packageName);
        Logger.m1026d("setupWithStrategy() package installer = ", installerPackageName);
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

    /* access modifiers changed from: private */
    public boolean versionOk(@NotNull Appstore appstore2) {
        try {
            int i = this.context.getPackageManager().getPackageInfo(this.context.getPackageName(), 0).versionCode;
        } catch (NameNotFoundException e) {
        }
        return true;
    }

    public void checkOptions() {
        Logger.m1026d("checkOptions() ", this.options);
        checkGoogle();
        checkSamsung();
        checkNokia();
        checkFortumo();
        checkAmazon();
    }

    /* access modifiers changed from: 0000 */
    public void checkSetupDone(String str) {
        if (!setupSuccessful()) {
            String str2 = setupStateToString(this.setupState);
            Logger.m1030e("Illegal state for operation (", str, "): ", str2);
            throw new IllegalStateException(str2 + " Can't perform operation: " + str);
        }
    }

    public void consume(@NotNull Purchase purchase) throws IabException {
        Appstore appstore2 = this.appstore;
        AppstoreInAppBillingService appstoreInAppBillingService = this.appStoreBillingService;
        if (this.setupState == 0 && appstore2 != null && appstoreInAppBillingService != null) {
            Purchase purchase2 = (Purchase) purchase.clone();
            purchase2.setSku(SkuManager.getInstance().getStoreSku(appstore2.getAppstoreName(), purchase.getSku()));
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

    /* access modifiers changed from: 0000 */
    public void consumeAsyncInternal(@NotNull final List<Purchase> list, @Nullable final OnConsumeFinishedListener onConsumeFinishedListener, @Nullable final OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
        checkSetupDone("consume");
        if (list.isEmpty()) {
            throw new IllegalArgumentException("Nothing to consume.");
        }
        new Thread(new Runnable() {
            public void run() {
                final ArrayList arrayList = new ArrayList();
                for (Purchase purchase : list) {
                    try {
                        OpenIabHelper.this.consume(purchase);
                        arrayList.add(new IabResult(0, "Successful consume of sku " + purchase.getSku()));
                    } catch (IabException e) {
                        arrayList.add(e.getResult());
                        Logger.m1028e("consumeAsyncInternal() Error : ", (Throwable) e);
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
        final ArrayList arrayList = new ArrayList();
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
        LinkedList linkedList = new LinkedList();
        for (ServiceInfo bindServiceIntent : queryOpenStoreServices) {
            linkedList.add(getBindServiceIntent(bindServiceIntent));
        }
        discoverOpenStores(openStoresDiscoveredListener, (Queue<Intent>) linkedList, (List<Appstore>) new ArrayList<Appstore>());
    }

    public void dispose() {
        Logger.m1025d("Disposing.");
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
        if (this.appstore == null) {
            return null;
        }
        return this.appstore.getAppstoreName();
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
        Logger.m1026d("handleActivityResult() setup is not done. requestCode: ", Integer.valueOf(i), " resultCode: ", Integer.valueOf(i2), " data: ", intent);
        return false;
    }

    public void launchPurchaseFlow(Activity activity2, @NotNull String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener) {
        launchPurchaseFlow(activity2, str, i, onIabPurchaseFinishedListener, "");
    }

    public void launchPurchaseFlow(Activity activity2, @NotNull String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2) {
        launchPurchaseFlow(activity2, str, "inapp", i, onIabPurchaseFinishedListener, str2);
    }

    public void launchPurchaseFlow(Activity activity2, @NotNull String str, String str2, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str3) {
        checkSetupDone("launchPurchaseFlow");
        this.appStoreBillingService.launchPurchaseFlow(activity2, SkuManager.getInstance().getStoreSku(this.appstore.getAppstoreName(), str), str2, i, onIabPurchaseFinishedListener, str3);
    }

    public void launchSubscriptionPurchaseFlow(Activity activity2, @NotNull String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener) {
        launchSubscriptionPurchaseFlow(activity2, str, i, onIabPurchaseFinishedListener, "");
    }

    public void launchSubscriptionPurchaseFlow(Activity activity2, @NotNull String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2) {
        launchPurchaseFlow(activity2, str, "subs", i, onIabPurchaseFinishedListener, str2);
    }

    @Nullable
    public Inventory queryInventory(boolean z, @Nullable List<String> list) throws IabException {
        return queryInventory(z, list, null);
    }

    @Nullable
    public Inventory queryInventory(boolean z, @Nullable List<String> list, @Nullable List<String> list2) throws IabException {
        List list3;
        List list4;
        if (Utils.uiThread()) {
            throw new IllegalStateException("Must not be called from the UI thread");
        }
        Appstore appstore2 = this.appstore;
        AppstoreInAppBillingService appstoreInAppBillingService = this.appStoreBillingService;
        if (this.setupState != 0 || appstore2 == null || appstoreInAppBillingService == null) {
            return null;
        }
        SkuManager instance = SkuManager.getInstance();
        if (list != null) {
            list3 = new ArrayList(list.size());
            for (String storeSku : list) {
                list3.add(instance.getStoreSku(appstore2.getAppstoreName(), storeSku));
            }
        } else {
            list3 = null;
        }
        if (list2 != null) {
            ArrayList arrayList = new ArrayList(list2.size());
            for (String storeSku2 : list2) {
                arrayList.add(instance.getStoreSku(appstore2.getAppstoreName(), storeSku2));
            }
            list4 = arrayList;
        } else {
            list4 = null;
        }
        return appstoreInAppBillingService.queryInventory(z, list3, list4);
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
                final IabResult result;
                final Inventory inventory = null;
                try {
                    inventory = OpenIabHelper.this.queryInventory(z2, list3, list4);
                    result = new IabResult(0, "Inventory refresh successful.");
                } catch (IabException e) {
                    IabException iabException = e;
                    result = iabException.getResult();
                    Logger.m1028e("queryInventoryAsync() Error : ", (Throwable) iabException);
                }
                OpenIabHelper.this.handler.post(new Runnable() {
                    public void run() {
                        if (OpenIabHelper.this.setupState == 0) {
                            queryInventoryFinishedListener2.onQueryInventoryFinished(result, inventory);
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
            Logger.m1026d("startSetup() options = ", this.options);
        }
        if (onIabSetupFinishedListener == null) {
            throw new IllegalArgumentException("Setup listener must be not null!");
        } else if (this.setupState == -1 || this.setupState == 1) {
            this.setupState = 3;
            this.setupExecutorService = Executors.newSingleThreadExecutor();
            this.availableAppstores.clear();
            this.availableAppstores.addAll(this.options.getAvailableStores());
            final ArrayList arrayList = new ArrayList(this.options.getAvailableStoreNames());
            for (Appstore appstoreName : this.availableAppstores) {
                arrayList.remove(appstoreName.getAppstoreName());
            }
            final ArrayList arrayList2 = new ArrayList();
            for (String str : this.options.getAvailableStoreNames()) {
                if (this.appStoreFactoryMap.containsKey(str)) {
                    Appstore appstore2 = ((AppstoreFactory) this.appStoreFactoryMap.get(str)).get();
                    arrayList2.add(appstore2);
                    this.availableAppstores.add(appstore2);
                    arrayList.remove(str);
                }
            }
            if (!arrayList.isEmpty()) {
                discoverOpenStores(new OpenStoresDiscoveredListener() {
                    public void openStoresDiscovered(@NotNull List<Appstore> list) {
                        for (Appstore appstore : list) {
                            if (arrayList.contains(appstore.getAppstoreName())) {
                                OpenIabHelper.this.availableAppstores.add(appstore);
                            } else {
                                AppstoreInAppBillingService inAppBillingService = appstore.getInAppBillingService();
                                if (inAppBillingService != null) {
                                    inAppBillingService.dispose();
                                    Logger.m1026d("startSetup() billing service disposed for ", appstore.getAppstoreName());
                                }
                            }
                        }
                        OpenIabHelper.this.setupWithStrategy(new OnIabSetupFinishedListener() {
                            public void onIabSetupFinished(IabResult iabResult) {
                                onIabSetupFinishedListener.onIabSetupFinished(iabResult);
                                arrayList2.remove(OpenIabHelper.this.appstore);
                                for (Appstore appstore : arrayList2) {
                                    AppstoreInAppBillingService inAppBillingService = appstore.getInAppBillingService();
                                    if (inAppBillingService != null) {
                                        inAppBillingService.dispose();
                                        Logger.m1026d("startSetup() billing service disposed for ", appstore.getAppstoreName());
                                    }
                                }
                            }
                        });
                    }
                });
            } else {
                setupWithStrategy(onIabSetupFinishedListener);
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
