package org.onepf.oms.appstore;

import android.app.Activity;
import android.content.ComponentName;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.IBinder;
import com.appsflyer.share.Constants;
import com.sec.android.iap.IAPConnector;
import com.sec.android.iap.IAPConnector.Stub;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;
import java.util.Locale;
import java.util.Set;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.json.JSONObject;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.OpenIabHelper.Options;
import org.onepf.oms.SkuManager;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabPurchaseFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.appstore.googleUtils.Purchase;
import org.onepf.oms.appstore.googleUtils.SkuDetails;
import org.onepf.oms.util.CollectionUtils;
import org.onepf.oms.util.Logger;

public class SamsungAppsBillingService implements AppstoreInAppBillingService {
    public static final String ACCOUNT_ACTIVITY_NAME = "com.sec.android.iap.activity.AccountActivity";
    private static final int CURRENT_MODE = (SamsungApps.isSamsungTestMode ? 1 : 0);
    public static final int FLAG_INCLUDE_STOPPED_PACKAGES = 32;
    public static final int IAP_ERROR_ALREADY_PURCHASED = -1003;
    public static final int IAP_ERROR_COMMON = -1002;
    public static final int IAP_ERROR_CONFIRM_INBOX = -1006;
    public static final int IAP_ERROR_INITIALIZATION = -1000;
    public static final int IAP_ERROR_NEED_APP_UPGRADE = -1001;
    public static final int IAP_ERROR_NONE = 0;
    public static final int IAP_ERROR_PRODUCT_DOES_NOT_EXIST = -1005;
    public static final int IAP_ERROR_WHILE_RUNNING = -1004;
    public static final int IAP_MODE_COMMERCIAL = 0;
    public static final int IAP_MODE_TEST_FAIL = -1;
    public static final int IAP_MODE_TEST_SUCCESS = 1;
    public static final int IAP_PAYMENT_IS_CANCELED = 1;
    public static final int IAP_RESPONSE_RESULT_OK = 0;
    public static final int IAP_RESPONSE_RESULT_UNAVAILABLE = 2;
    public static final String IAP_SERVICE_NAME = "com.sec.android.iap.service.iapService";
    private static final int ITEM_RESPONSE_COUNT = 100;
    public static final String ITEM_TYPE_ALL = "10";
    public static final String ITEM_TYPE_CONSUMABLE = "00";
    public static final String ITEM_TYPE_NON_CONSUMABLE = "01";
    public static final String ITEM_TYPE_SUBSCRIPTION = "02";
    public static final String JSON_KEY_CURRENCY_UNIT = "mCurrencyUnit";
    public static final String JSON_KEY_ITEM_DESC = "mItemDesc";
    public static final String JSON_KEY_ITEM_DOWNLOAD_URL = "mItemDownloadUrl";
    public static final String JSON_KEY_ITEM_ID = "mItemId";
    public static final String JSON_KEY_ITEM_IMAGE_URL = "mItemImageUrl";
    public static final String JSON_KEY_ITEM_NAME = "mItemName";
    public static final String JSON_KEY_ITEM_PRICE = "mItemPrice";
    public static final String JSON_KEY_ITEM_PRICE_STRING = "mItemPriceString";
    public static final String JSON_KEY_PAYMENT_ID = "mPaymentId";
    public static final String JSON_KEY_PURCHASE_DATE = "mPurchaseDate";
    public static final String JSON_KEY_PURCHASE_ID = "mPurchaseId";
    public static final String JSON_KEY_TYPE = "mType";
    public static final String KEY_NAME_ERROR_STRING = "ERROR_STRING";
    public static final String KEY_NAME_IAP_UPGRADE_URL = "IAP_UPGRADE_URL";
    public static final String KEY_NAME_ITEM_GROUP_ID = "ITEM_GROUP_ID";
    public static final String KEY_NAME_ITEM_ID = "ITEM_ID";
    public static final String KEY_NAME_RESULT_LIST = "RESULT_LIST";
    public static final String KEY_NAME_RESULT_OBJECT = "RESULT_OBJECT";
    public static final String KEY_NAME_STATUS_CODE = "STATUS_CODE";
    public static final String KEY_NAME_THIRD_PARTY_NAME = "THIRD_PARTY_NAME";
    public static final String PAYMENT_ACTIVITY_NAME = "com.sec.android.iap.activity.PaymentMethodListActivity";
    public static final int REQUEST_CODE_IS_ACCOUNT_CERTIFICATION = 899;
    public static final int REQUEST_CODE_IS_IAP_PAYMENT = 1;
    private Activity activity;
    private volatile boolean isBound;
    private String mExtraData;
    @Nullable
    private IAPConnector mIapConnector;
    private String mItemGroupId;
    @Nullable
    private OnIabPurchaseFinishedListener mPurchaseListener = null;
    private int mRequestCode;
    private Options options;
    private String purchasingItemType;
    @Nullable
    private ServiceConnection serviceConnection;
    @Nullable
    private OnIabSetupFinishedListener setupListener = null;

    /* renamed from: org.onepf.oms.appstore.SamsungAppsBillingService$1 */
    class C13221 implements ServiceConnection {
        C13221() {
        }

        public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
            SamsungAppsBillingService.this.mIapConnector = Stub.asInterface(iBinder);
            if (SamsungAppsBillingService.this.mIapConnector != null) {
                SamsungAppsBillingService.this.initIap();
            } else {
                SamsungAppsBillingService.this.setupListener.onIabSetupFinished(new IabResult(6, "IAP service bind failed"));
            }
        }

        public void onServiceDisconnected(ComponentName componentName) {
        }
    }

    public SamsungAppsBillingService(Activity activity, Options options) {
        this.activity = activity;
        this.options = options;
    }

    private void bindIapService() {
        this.serviceConnection = new C13221();
        this.isBound = this.activity.getApplicationContext().bindService(new Intent("com.sec.android.iap.service.iapService"), this.serviceConnection, 1);
    }

    private String getItemGroupId(@NotNull String str) {
        SamsungApps.checkSku(str);
        return str.split(Constants.URL_PATH_DELIMITER)[0];
    }

    private String getItemId(@NotNull String str) {
        SamsungApps.checkSku(str);
        return str.split(Constants.URL_PATH_DELIMITER)[1];
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void initIap() {
        /*
        r8 = this;
        r1 = 0;
        r2 = 6;
        r0 = "Init IAP service failed";
        r3 = r8.mIapConnector;	 Catch:{ RemoteException -> 0x0039 }
        r4 = CURRENT_MODE;	 Catch:{ RemoteException -> 0x0039 }
        r3 = r3.init(r4);	 Catch:{ RemoteException -> 0x0039 }
        if (r3 == 0) goto L_0x0041;
    L_0x000e:
        r4 = "STATUS_CODE";
        r4 = r3.getInt(r4);	 Catch:{ RemoteException -> 0x0039 }
        r5 = 2;
        r5 = new java.lang.Object[r5];	 Catch:{ RemoteException -> 0x0039 }
        r6 = 0;
        r7 = "Init IAP connection status code: ";
        r5[r6] = r7;	 Catch:{ RemoteException -> 0x0039 }
        r6 = 1;
        r7 = java.lang.Integer.valueOf(r4);	 Catch:{ RemoteException -> 0x0039 }
        r5[r6] = r7;	 Catch:{ RemoteException -> 0x0039 }
        org.onepf.oms.util.Logger.m1001d(r5);	 Catch:{ RemoteException -> 0x0039 }
        r5 = "ERROR_STRING";
        r0 = r3.getString(r5);	 Catch:{ RemoteException -> 0x0039 }
        if (r4 != 0) goto L_0x0041;
    L_0x002e:
        r2 = r8.setupListener;
        r3 = new org.onepf.oms.appstore.googleUtils.IabResult;
        r3.<init>(r1, r0);
        r2.onIabSetupFinished(r3);
        return;
    L_0x0039:
        r1 = move-exception;
        r3 = "Init IAP: ";
        org.onepf.oms.util.Logger.m1003e(r3, r1);
        r1 = r2;
        goto L_0x002e;
    L_0x0041:
        r1 = r2;
        goto L_0x002e;
        */
        throw new UnsupportedOperationException("Method not decompiled: org.onepf.oms.appstore.SamsungAppsBillingService.initIap():void");
    }

    private boolean processItemsBundle(@Nullable Bundle bundle, String str, @NotNull Inventory inventory, boolean z, boolean z2, boolean z3, @Nullable Set<String> set) {
        if (bundle == null || bundle.getInt(KEY_NAME_STATUS_CODE) != 0) {
            return false;
        }
        ArrayList stringArrayList = bundle.getStringArrayList(KEY_NAME_RESULT_LIST);
        Iterator it = stringArrayList.iterator();
        while (it.hasNext()) {
            try {
                JSONObject jSONObject = new JSONObject((String) it.next());
                String string = jSONObject.getString(JSON_KEY_ITEM_ID);
                if (set == null || set.contains(string)) {
                    String string2 = jSONObject.getString(JSON_KEY_TYPE);
                    if (!string2.equals(ITEM_TYPE_CONSUMABLE) || z3) {
                        String str2 = string2.equals(ITEM_TYPE_SUBSCRIPTION) ? "subs" : "inapp";
                        if (z2) {
                            Purchase purchase = new Purchase(OpenIabHelper.NAME_SAMSUNG);
                            purchase.setItemType(str2);
                            purchase.setSku(SkuManager.getInstance().getSku(OpenIabHelper.NAME_SAMSUNG, str + '/' + string));
                            purchase.setPackageName(this.activity.getPackageName());
                            purchase.setPurchaseState(0);
                            purchase.setDeveloperPayload("");
                            purchase.setOrderId(jSONObject.getString(JSON_KEY_PAYMENT_ID));
                            purchase.setPurchaseTime(Long.parseLong(jSONObject.getString(JSON_KEY_PURCHASE_DATE)));
                            purchase.setToken(jSONObject.getString(JSON_KEY_PURCHASE_ID));
                            inventory.addPurchase(purchase);
                        }
                        if (!z2 || z) {
                            inventory.addSkuDetails(new SkuDetails(str2, SkuManager.getInstance().getSku(OpenIabHelper.NAME_SAMSUNG, str + '/' + string), jSONObject.getString(JSON_KEY_ITEM_NAME), jSONObject.getString(JSON_KEY_ITEM_PRICE_STRING), jSONObject.getString(JSON_KEY_ITEM_DESC)));
                        }
                    }
                }
            } catch (Throwable e) {
                Logger.m1003e("JSON parse error", e);
            }
        }
        return stringArrayList.size() == 100;
    }

    public void consume(Purchase purchase) throws IabException {
    }

    public void dispose() {
        if (this.serviceConnection != null && this.isBound) {
            this.activity.getApplicationContext().unbindService(this.serviceConnection);
            this.isBound = false;
        }
        this.serviceConnection = null;
        this.mIapConnector = null;
    }

    public boolean handleActivityResult(int i, int i2, @Nullable Intent intent) {
        if (i == this.options.getSamsungCertificationRequestCode()) {
            if (i2 == -1) {
                bindIapService();
            } else if (i2 == 0) {
                this.setupListener.onIabSetupFinished(new IabResult(1, "Account certification canceled"));
            } else {
                this.setupListener.onIabSetupFinished(new IabResult(6, "Unknown error. Result code: " + i2));
            }
            return true;
        } else if (i != this.mRequestCode) {
            return false;
        } else {
            String str;
            int i3;
            String str2;
            int i4 = 6;
            String str3 = "Unknown error";
            Purchase purchase = new Purchase(OpenIabHelper.NAME_SAMSUNG);
            if (intent != null) {
                Bundle extras = intent.getExtras();
                if (extras != null) {
                    int i5 = extras.getInt(KEY_NAME_STATUS_CODE);
                    str3 = extras.getString(KEY_NAME_ERROR_STRING);
                    String string = extras.getString(KEY_NAME_ITEM_ID);
                    switch (i2) {
                        case -1:
                            switch (i5) {
                                case -1005:
                                    i4 = 4;
                                    break;
                                case -1003:
                                    i4 = 7;
                                    break;
                                case 0:
                                    i4 = 0;
                                    break;
                                default:
                                    break;
                            }
                        case 0:
                            i4 = 1;
                            break;
                    }
                    String string2 = extras.getString(KEY_NAME_RESULT_OBJECT);
                    try {
                        JSONObject jSONObject = new JSONObject(string2);
                        purchase.setOriginalJson(string2);
                        purchase.setOrderId(jSONObject.getString(JSON_KEY_PAYMENT_ID));
                        purchase.setPurchaseTime(Long.parseLong(jSONObject.getString(JSON_KEY_PURCHASE_DATE)));
                        purchase.setToken(jSONObject.getString(JSON_KEY_PURCHASE_ID));
                    } catch (Throwable e) {
                        Logger.m1003e("JSON parse error: ", e);
                    }
                    purchase.setItemType(this.purchasingItemType);
                    purchase.setSku(SkuManager.getInstance().getSku(OpenIabHelper.NAME_SAMSUNG, this.mItemGroupId + '/' + string));
                    purchase.setPackageName(this.activity.getPackageName());
                    purchase.setPurchaseState(0);
                    purchase.setDeveloperPayload(this.mExtraData);
                    str = str3;
                    i3 = i4;
                    str2 = str;
                    Logger.m1001d("Samsung result code: ", Integer.valueOf(i3), ", msg: ", str2);
                    this.mPurchaseListener.onIabPurchaseFinished(new IabResult(i3, str2), purchase);
                    return true;
                }
            }
            str = str3;
            i3 = 6;
            str2 = str;
            Logger.m1001d("Samsung result code: ", Integer.valueOf(i3), ", msg: ", str2);
            this.mPurchaseListener.onIabPurchaseFinished(new IabResult(i3, str2), purchase);
            return true;
        }
    }

    public void launchPurchaseFlow(@NotNull Activity activity, @NotNull String str, String str2, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str3) {
        String itemGroupId = getItemGroupId(str);
        String itemId = getItemId(str);
        Bundle bundle = new Bundle();
        bundle.putString(KEY_NAME_THIRD_PARTY_NAME, activity.getPackageName());
        bundle.putString(KEY_NAME_ITEM_GROUP_ID, itemGroupId);
        bundle.putString(KEY_NAME_ITEM_ID, itemId);
        Logger.m1001d("launchPurchase: itemGroupId = ", itemGroupId, ", itemId = ", itemId);
        ComponentName componentName = new ComponentName(SamsungApps.IAP_PACKAGE_NAME, PAYMENT_ACTIVITY_NAME);
        Intent intent = new Intent("android.intent.action.MAIN");
        intent.addCategory("android.intent.category.LAUNCHER");
        intent.setComponent(componentName);
        intent.putExtras(bundle);
        this.mRequestCode = i;
        this.mPurchaseListener = onIabPurchaseFinishedListener;
        this.purchasingItemType = str2;
        this.mItemGroupId = itemGroupId;
        this.mExtraData = str3;
        Logger.m1001d("Request code: ", Integer.valueOf(i));
        activity.startActivityForResult(intent, i);
    }

    public Inventory queryInventory(boolean z, @Nullable List<String> list, @Nullable List<String> list2) throws IabException {
        Inventory inventory = new Inventory();
        String format = new SimpleDateFormat("yyyyMMdd", Locale.getDefault()).format(new Date());
        Set<String> hashSet = new HashSet();
        Collection<String> allStoreSkus = SkuManager.getInstance().getAllStoreSkus(OpenIabHelper.NAME_SAMSUNG);
        if (!CollectionUtils.isEmpty((Collection) allStoreSkus)) {
            for (String itemGroupId : allStoreSkus) {
                hashSet.add(getItemGroupId(itemGroupId));
            }
        }
        for (String str : hashSet) {
            int i = 1;
            int i2 = 100;
            Bundle bundle;
            do {
                bundle = null;
                try {
                    Logger.m1001d("getItemsInbox, startNum = ", Integer.valueOf(i), ", endNum = ", Integer.valueOf(i2));
                    bundle = this.mIapConnector.getItemsInbox(this.activity.getPackageName(), str, i, i2, "19700101", format);
                } catch (Throwable e) {
                    Logger.m1003e("Samsung getItemsInbox: ", e);
                }
                i += 100;
                i2 += 100;
            } while (processItemsBundle(bundle, str, inventory, z, true, false, null));
        }
        if (z) {
            hashSet = new HashSet();
            Set hashSet2 = new HashSet();
            if (list != null) {
                for (String itemGroupId2 : list) {
                    hashSet.add(getItemGroupId(itemGroupId2));
                    hashSet2.add(getItemId(itemGroupId2));
                }
            }
            if (list2 != null) {
                for (String itemGroupId22 : list2) {
                    hashSet.add(getItemGroupId(itemGroupId22));
                    hashSet2.add(getItemId(itemGroupId22));
                }
            }
            if (!hashSet2.isEmpty()) {
                for (String str2 : hashSet) {
                    i2 = 1;
                    int i3 = 100;
                    while (true) {
                        Bundle itemList;
                        try {
                            itemList = this.mIapConnector.getItemList(CURRENT_MODE, this.activity.getPackageName(), str2, i2, i3, ITEM_TYPE_ALL);
                        } catch (Throwable e2) {
                            Logger.m1003e("Samsung getItemList: ", e2);
                            itemList = null;
                        }
                        int i4 = i2 + 100;
                        int i5 = i3 + 100;
                        if (processItemsBundle(itemList, str2, inventory, z, false, true, hashSet2)) {
                            i2 = i4;
                            i3 = i5;
                        }
                    }
                }
            }
        }
        return inventory;
    }

    public void startSetup(OnIabSetupFinishedListener onIabSetupFinishedListener) {
        this.setupListener = onIabSetupFinishedListener;
        ComponentName componentName = new ComponentName(SamsungApps.IAP_PACKAGE_NAME, ACCOUNT_ACTIVITY_NAME);
        Intent intent = new Intent();
        intent.setComponent(componentName);
        this.activity.startActivityForResult(intent, this.options.getSamsungCertificationRequestCode());
    }

    public boolean subscriptionsSupported() {
        return true;
    }
}
