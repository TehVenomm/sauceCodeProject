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
import org.onepf.oms.OpenIabHelper$Options.Builder;
import org.onepf.oms.appstore.AmazonAppstore;
import org.onepf.oms.appstore.NokiaStore;
import org.onepf.oms.appstore.OpenAppstore;
import org.onepf.oms.appstore.SamsungApps;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabHelper$OnIabPurchaseFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnConsumeFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnConsumeMultiFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.QueryInventoryFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.appstore.googleUtils.Purchase;
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
    private final Map<String, OpenIabHelper$AppstoreFactory> appStoreFactoryMap;
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
    private final OpenIabHelper$Options options;
    private final PackageManager packageManager;
    @Nullable
    private ExecutorService setupExecutorService;
    private volatile int setupState;

    public OpenIabHelper(@NotNull Context context, @NotNull Map<String, String> map) {
        this(context, new Builder().addStoreKeys(map).build());
    }

    public OpenIabHelper(@NotNull Context context, @NotNull Map<String, String> map, String[] strArr) {
        this(context, new Builder().addStoreKeys(map).addPreferredStoreName(strArr).build());
    }

    public OpenIabHelper(@NotNull Context context, @NotNull Map<String, String> map, String[] strArr, Appstore[] appstoreArr) {
        this(context, new Builder().addStoreKeys(map).addPreferredStoreName(strArr).addAvailableStores(appstoreArr).build());
    }

    public OpenIabHelper(@NotNull Context context, OpenIabHelper$Options openIabHelper$Options) {
        this.setupState = -1;
        this.handler = new Handler(Looper.getMainLooper());
        this.availableAppstores = new LinkedHashSet();
        this.inventoryExecutor = Executors.newSingleThreadExecutor();
        this.appStorePackageMap = new HashMap();
        this.appStoreFactoryMap = new HashMap();
        this.appStorePackageMap.put(NAME_YANDEX, NAME_YANDEX);
        this.appStorePackageMap.put(NAME_APTOIDE, NAME_APTOIDE);
        this.appStoreFactoryMap.put(NAME_FORTUMO, new OpenIabHelper$1(this));
        this.appStorePackageMap.put("com.android.vending", NAME_GOOGLE);
        this.appStoreFactoryMap.put(NAME_GOOGLE, new OpenIabHelper$2(this));
        this.appStorePackageMap.put(AmazonAppstore.AMAZON_INSTALLER, NAME_AMAZON);
        this.appStoreFactoryMap.put(NAME_AMAZON, new OpenIabHelper$3(this));
        this.appStorePackageMap.put(SamsungApps.SAMSUNG_INSTALLER, NAME_SAMSUNG);
        this.appStoreFactoryMap.put(NAME_SAMSUNG, new OpenIabHelper$4(this));
        this.appStorePackageMap.put(NokiaStore.NOKIA_INSTALLER, NAME_NOKIA);
        this.appStoreFactoryMap.put(NAME_NOKIA, new OpenIabHelper$5(this));
        this.appStorePackageMap.put("com.skubit.android", "com.skubit.android");
        this.appStoreFactoryMap.put("com.skubit.android", new OpenIabHelper$6(this));
        this.appStorePackageMap.put("net.skubit.android", "net.skubit.android");
        this.appStoreFactoryMap.put("net.skubit.android", new OpenIabHelper$7(this));
        this.context = context.getApplicationContext();
        this.packageManager = context.getPackageManager();
        this.options = openIabHelper$Options;
        if (context instanceof Activity) {
            this.activity = (Activity) context;
        }
        checkOptions();
    }

    private void checkAmazon() {
        if (VERSION.SDK_INT >= 21) {
            Logger.d("checkAmazon() Android Lollipop not supported, ignoring amazon wrapper.");
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
        Logger.d(new Object[]{"checkAmazon() amazon sdk available: ", Boolean.valueOf(z)});
        if (!z) {
            z = this.options.getAvailableStoreByName(NAME_AMAZON) != null || this.options.getAvailableStoreNames().contains(NAME_AMAZON) || this.options.getPreferredStoreNames().contains(NAME_AMAZON);
            Logger.d(new Object[]{"checkAmazon() amazon billing required: ", Boolean.valueOf(z)});
            if (z) {
                throw new IllegalStateException("You must satisfy amazon sdk dependency.");
            }
            Logger.d("checkAmazon() ignoring amazon wrapper.");
            this.appStoreFactoryMap.remove(NAME_AMAZON);
        }
    }

    private void checkBillingAndFinish(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener, @NotNull Collection<Appstore> collection) {
        if (this.setupState != 3) {
            throw new IllegalStateException("Can't check billing. Current state: " + setupStateToString(this.setupState));
        }
        String packageName = this.context.getPackageName();
        if (collection.isEmpty()) {
            finishSetup(onIabSetupFinishedListener);
        } else {
            this.setupExecutorService.execute(this.options.isCheckInventory() ? new OpenIabHelper$11(this, collection, packageName, onIabSetupFinishedListener) : new OpenIabHelper$12(this, collection, packageName, onIabSetupFinishedListener));
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
        Logger.d(new Object[]{"checkFortumo() fortumo sdk available: ", Boolean.valueOf(z)});
        if (!z) {
            z = this.options.getAvailableStoreByName(NAME_FORTUMO) != null || this.options.getAvailableStoreNames().contains(NAME_FORTUMO) || this.options.getPreferredStoreNames().contains(NAME_FORTUMO);
            Logger.d(new Object[]{"checkFortumo() fortumo billing required: ", Boolean.valueOf(z)});
            if (z) {
                throw new IllegalStateException("You must satisfy fortumo sdk dependency.");
            }
            Logger.d("checkFortumo() ignoring fortumo wrapper.");
            this.appStoreFactoryMap.remove(NAME_FORTUMO);
        }
    }

    private void checkGoogle() {
        int i = 0;
        Logger.d("checkGoogle() verify mode = " + this.options.getVerifyMode());
        if (this.options.getVerifyMode() != 1) {
            Logger.d(new Object[]{"checkGoogle() google key available = ", Boolean.valueOf(this.options.getStoreKeys().containsKey(NAME_GOOGLE))});
            if (!this.options.getStoreKeys().containsKey(NAME_GOOGLE)) {
                if (this.options.getAvailableStoreByName(NAME_GOOGLE) != null || this.options.getAvailableStoreNames().contains(NAME_GOOGLE) || this.options.getPreferredStoreNames().contains(NAME_GOOGLE)) {
                    i = 1;
                }
                if (i == 0 || this.options.getVerifyMode() != 0) {
                    Logger.d("checkGoogle() ignoring GooglePlay wrapper.");
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
        Semaphore semaphore = new Semaphore(0);
        Appstore[] appstoreArr = new Appstore[1];
        for (Appstore appstore : set) {
            AppstoreInAppBillingService inAppBillingService = appstore.getInAppBillingService();
            this.handler.post(new OpenIabHelper$16(this, inAppBillingService, new OpenIabHelper$15(this, semaphore, inAppBillingService, appstoreArr, appstore)));
            try {
                semaphore.acquire();
                if (appstoreArr[0] != null) {
                    return appstoreArr[0];
                }
            } catch (Throwable e) {
                Logger.e("checkInventory() Error during inventory check: ", e);
                return null;
            }
        }
        return null;
    }

    private void checkNokia() {
        Logger.d(new Object[]{"checkNokia() has permission = ", Boolean.valueOf(Utils.hasRequestedPermission(this.context, NokiaStore.NOKIA_BILLING_PERMISSION))});
        if (!Utils.hasRequestedPermission(this.context, NokiaStore.NOKIA_BILLING_PERMISSION)) {
            if (this.options.getAvailableStoreByName(NAME_NOKIA) != null || this.options.getAvailableStoreNames().contains(NAME_NOKIA) || this.options.getPreferredStoreNames().contains(NAME_NOKIA)) {
                throw new IllegalStateException("Nokia permission \"com.nokia.payment.BILLING\" NOT REQUESTED");
            }
            Logger.d("checkNokia() ignoring Nokia wrapper");
            this.appStoreFactoryMap.remove(NAME_NOKIA);
        }
    }

    private void checkSamsung() {
        Logger.d(new Object[]{"checkSamsung() activity = ", this.activity});
        if (this.activity == null) {
            if (this.options.getAvailableStoreByName(NAME_SAMSUNG) != null || this.options.getAvailableStoreNames().contains(NAME_SAMSUNG) || this.options.getPreferredStoreNames().contains(NAME_SAMSUNG)) {
                throw new IllegalArgumentException("You must supply Activity object as context in order to use com.samsung.apps store");
            }
            Logger.d("checkSamsung() ignoring Samsung wrapper");
            this.appStoreFactoryMap.remove(NAME_SAMSUNG);
        }
    }

    @NotNull
    @Deprecated
    public static List<Appstore> discoverOpenStores(Context context, List<Appstore> list, OpenIabHelper$Options openIabHelper$Options) {
        throw new UnsupportedOperationException("This action is no longer supported.");
    }

    private void discoverOpenStores(@NotNull OpenIabHelper$OpenStoresDiscoveredListener openIabHelper$OpenStoresDiscoveredListener, @NotNull Queue<Intent> queue, @NotNull List<Appstore> list) {
        while (!queue.isEmpty()) {
            Intent intent = (Intent) queue.poll();
            ServiceConnection openIabHelper$14 = new OpenIabHelper$14(this, list, openIabHelper$OpenStoresDiscoveredListener, queue);
            if (!this.context.bindService(intent, openIabHelper$14, 1)) {
                this.context.unbindService(openIabHelper$14);
                Logger.e("discoverOpenStores() Couldn't connect to open store: " + intent);
            } else {
                return;
            }
        }
        openIabHelper$OpenStoresDiscoveredListener.openStoresDiscovered(Collections.unmodifiableList(list));
    }

    private void dispose(@NotNull Collection<Appstore> collection) {
        for (Appstore inAppBillingService : collection) {
            inAppBillingService.getInAppBillingService().dispose();
            Logger.d(new Object[]{"dispose() was called for ", inAppBillingService.getAppstoreName()});
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
                Logger.dWithTimeFromUp(new Object[]{"finishSetup() === SETUP DONE === result: ", iabResult, " Appstore: ", appstore});
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
        Logger.e(new Object[]{"finishSetupWithError() error occurred during setup", str});
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
            Logger.d(new Object[]{"getOpenAppstore() Appstore doesn't have name. Skipped. ComponentName: ", componentName});
            return null;
        } else if (billingServiceIntent == null) {
            Logger.d(new Object[]{"getOpenAppstore() billing is not supported by store: ", componentName});
            return null;
        } else if (verifyMode == 0 && TextUtils.isEmpty(obj)) {
            Logger.e(new Object[]{"getOpenAppstore() verification is required but publicKey is not provided: ", componentName});
            return null;
        } else {
            OpenAppstore openAppstore = new OpenAppstore(this.context, appstoreName, asInterface, billingServiceIntent, obj, serviceConnection);
            openAppstore.componentName = componentName;
            Logger.d(new Object[]{"getOpenAppstore() returns ", openAppstore.getAppstoreName()});
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

    private void setup(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener) {
        Collection linkedHashSet = new LinkedHashSet();
        Set<String> availableStoreNames = this.options.getAvailableStoreNames();
        if (this.availableAppstores.isEmpty() && availableStoreNames.isEmpty()) {
            discoverOpenStores(new OpenIabHelper$10(this, availableStoreNames, linkedHashSet, onIabSetupFinishedListener));
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

    private void setupForPackage(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener, @NotNull String str, boolean z) {
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
                    availableStoreByName = ((OpenIabHelper$AppstoreFactory) this.appStoreFactoryMap.get(str2)).get();
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
                } else if (!this.context.bindService(bindServiceIntent, new OpenIabHelper$9(this, z, onIabSetupFinishedListener), 1)) {
                    Logger.e("setupForPackage() Error binding to open store service");
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
                    if (!this.context.bindService(bindServiceIntent, new OpenIabHelper$9(this, z, onIabSetupFinishedListener), 1)) {
                        Logger.e("setupForPackage() Error binding to open store service");
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
        Logger.d(new Object[]{"setupWithStrategy() store search strategy = ", Integer.valueOf(storeSearchStrategy)});
        Logger.d(new Object[]{"setupWithStrategy() package name = ", this.context.getPackageName()});
        String installerPackageName = this.packageManager.getInstallerPackageName(r0);
        Logger.d(new Object[]{"setupWithStrategy() package installer = ", installerPackageName});
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
        Logger.d(new Object[]{"checkOptions() ", this.options});
        checkGoogle();
        checkSamsung();
        checkNokia();
        checkFortumo();
        checkAmazon();
    }

    void checkSetupDone(String str) {
        if (!setupSuccessful()) {
            Logger.e(new Object[]{"Illegal state for operation (", str, "): ", setupStateToString(this.setupState)});
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

    void consumeAsyncInternal(@NotNull List<Purchase> list, @Nullable OnConsumeFinishedListener onConsumeFinishedListener, @Nullable OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
        checkSetupDone("consume");
        if (list.isEmpty()) {
            throw new IllegalArgumentException("Nothing to consume.");
        }
        new Thread(new OpenIabHelper$18(this, list, onConsumeFinishedListener, onConsumeMultiFinishedListener)).start();
    }

    @Nullable
    public List<Appstore> discoverOpenStores() {
        if (Utils.uiThread()) {
            throw new IllegalStateException("Must not be called from UI thread");
        }
        List<Appstore> arrayList = new ArrayList();
        CountDownLatch countDownLatch = new CountDownLatch(1);
        discoverOpenStores(new OpenIabHelper$13(this, arrayList, countDownLatch));
        try {
            countDownLatch.await();
            return arrayList;
        } catch (InterruptedException e) {
            return null;
        }
    }

    public void discoverOpenStores(@NotNull OpenIabHelper$OpenStoresDiscoveredListener openIabHelper$OpenStoresDiscoveredListener) {
        List<ServiceInfo> queryOpenStoreServices = queryOpenStoreServices();
        Queue linkedList = new LinkedList();
        for (ServiceInfo bindServiceIntent : queryOpenStoreServices) {
            linkedList.add(getBindServiceIntent(bindServiceIntent));
        }
        discoverOpenStores(openIabHelper$OpenStoresDiscoveredListener, linkedList, new ArrayList());
    }

    public void dispose() {
        Logger.d("Disposing.");
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
        Logger.dWithTimeFromUp(new Object[]{"handleActivityResult() requestCode: ", Integer.valueOf(i), " resultCode: ", Integer.valueOf(i2), " data: ", intent});
        if (i == this.options.samsungCertificationRequestCode && this.appStoreInSetup != null) {
            return this.appStoreInSetup.getInAppBillingService().handleActivityResult(i, i2, intent);
        }
        if (this.setupState == 0) {
            return this.appStoreBillingService.handleActivityResult(i, i2, intent);
        }
        Logger.d(new Object[]{"handleActivityResult() setup is not done. requestCode: ", Integer.valueOf(i), " resultCode: ", Integer.valueOf(i2), " data: ", intent});
        return false;
    }

    public void launchPurchaseFlow(Activity activity, @NotNull String str, int i, IabHelper$OnIabPurchaseFinishedListener iabHelper$OnIabPurchaseFinishedListener) {
        launchPurchaseFlow(activity, str, i, iabHelper$OnIabPurchaseFinishedListener, "");
    }

    public void launchPurchaseFlow(Activity activity, @NotNull String str, int i, IabHelper$OnIabPurchaseFinishedListener iabHelper$OnIabPurchaseFinishedListener, String str2) {
        launchPurchaseFlow(activity, str, "inapp", i, iabHelper$OnIabPurchaseFinishedListener, str2);
    }

    public void launchPurchaseFlow(Activity activity, @NotNull String str, String str2, int i, IabHelper$OnIabPurchaseFinishedListener iabHelper$OnIabPurchaseFinishedListener, String str3) {
        checkSetupDone("launchPurchaseFlow");
        this.appStoreBillingService.launchPurchaseFlow(activity, SkuManager.getInstance().getStoreSku(this.appstore.getAppstoreName(), str), str2, i, iabHelper$OnIabPurchaseFinishedListener, str3);
    }

    public void launchSubscriptionPurchaseFlow(Activity activity, @NotNull String str, int i, IabHelper$OnIabPurchaseFinishedListener iabHelper$OnIabPurchaseFinishedListener) {
        launchSubscriptionPurchaseFlow(activity, str, i, iabHelper$OnIabPurchaseFinishedListener, "");
    }

    public void launchSubscriptionPurchaseFlow(Activity activity, @NotNull String str, int i, IabHelper$OnIabPurchaseFinishedListener iabHelper$OnIabPurchaseFinishedListener, String str2) {
        launchPurchaseFlow(activity, str, "subs", i, iabHelper$OnIabPurchaseFinishedListener, str2);
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
        new Thread(new OpenIabHelper$17(this, z, list, list2, queryInventoryFinishedListener)).start();
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

    public void startSetup(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener) {
        if (this.options != null) {
            Logger.d(new Object[]{"startSetup() options = ", this.options});
        }
        if (onIabSetupFinishedListener == null) {
            throw new IllegalArgumentException("Setup listener must be not null!");
        } else if (this.setupState == -1 || this.setupState == 1) {
            this.setupState = 3;
            this.setupExecutorService = Executors.newSingleThreadExecutor();
            this.availableAppstores.clear();
            this.availableAppstores.addAll(this.options.getAvailableStores());
            List arrayList = new ArrayList(this.options.getAvailableStoreNames());
            for (Appstore appstoreName : this.availableAppstores) {
                arrayList.remove(appstoreName.getAppstoreName());
            }
            List arrayList2 = new ArrayList();
            for (String str : this.options.getAvailableStoreNames()) {
                if (this.appStoreFactoryMap.containsKey(str)) {
                    Appstore appstore = ((OpenIabHelper$AppstoreFactory) this.appStoreFactoryMap.get(str)).get();
                    arrayList2.add(appstore);
                    this.availableAppstores.add(appstore);
                    arrayList.remove(str);
                }
            }
            if (arrayList.isEmpty()) {
                setupWithStrategy(onIabSetupFinishedListener);
            } else {
                discoverOpenStores(new OpenIabHelper$8(this, arrayList, onIabSetupFinishedListener, arrayList2));
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
