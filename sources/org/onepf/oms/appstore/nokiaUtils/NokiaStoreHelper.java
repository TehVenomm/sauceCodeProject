package org.onepf.oms.appstore.nokiaUtils;

import android.app.Activity;
import android.app.PendingIntent;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.IBinder;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import com.nokia.payment.iap.aidl.INokiaIAPService;
import com.nokia.payment.iap.aidl.INokiaIAPService.Stub;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.Appstore;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.SkuManager;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;
import org.onepf.oms.appstore.NokiaStore;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabHelper$OnIabPurchaseFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.appstore.googleUtils.Purchase;
import org.onepf.oms.appstore.googleUtils.SkuDetails;
import org.onepf.oms.util.CollectionUtils;
import org.onepf.oms.util.Logger;

public class NokiaStoreHelper implements AppstoreInAppBillingService {
    public static final int RESULT_BAD_RESPONSE = -1002;
    public static final int RESULT_BILLING_UNAVAILABLE = 3;
    public static final int RESULT_DEVELOPER_ERROR = 5;
    public static final int RESULT_ERROR = 6;
    public static final int RESULT_ITEM_ALREADY_OWNED = 7;
    public static final int RESULT_ITEM_NOT_OWNED = 8;
    public static final int RESULT_ITEM_UNAVAILABLE = 4;
    public static final int RESULT_NO_SIM = 9;
    public static final int RESULT_OK = 0;
    public static final int RESULT_USER_CANCELED = 1;
    private final Context mContext;
    @Nullable
    private IabHelper$OnIabPurchaseFinishedListener mPurchaseListener = null;
    int mRequestCode;
    @Nullable
    private INokiaIAPService mService = null;
    @Nullable
    private ServiceConnection mServiceConn = null;

    public NokiaStoreHelper(Context context, Appstore appstore) {
        this.mContext = context;
    }

    @NotNull
    private Intent getServiceIntent() {
        Intent intent = new Intent(NokiaStore.VENDING_ACTION);
        intent.setPackage(NokiaStore.NOKIA_INSTALLER);
        return intent;
    }

    private void processDetailsList(@NotNull List<String> list, @NotNull Inventory inventory) throws JSONException {
        Logger.m4031i("NokiaStoreHelper.processDetailsList");
        for (String jSONObject : list) {
            JSONObject jSONObject2 = new JSONObject(jSONObject);
            inventory.addSkuDetails(new SkuDetails("inapp", SkuManager.getInstance().getSku(OpenIabHelper.NAME_NOKIA, jSONObject2.getString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID)), jSONObject2.getString("title"), jSONObject2.getString(Param.PRICE), jSONObject2.getString("shortdescription")));
        }
    }

    private void processPurchaseSuccess(String str) {
        Logger.m4031i("NokiaStoreHelper.processPurchaseSuccess");
        Logger.m4026d("purchaseData = ", str);
        try {
            JSONObject jSONObject = new JSONObject(str);
            Logger.m4026d("sku = ", SkuManager.getInstance().getSku(OpenIabHelper.NAME_NOKIA, jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID)));
            Purchase purchase = new Purchase(OpenIabHelper.NAME_NOKIA);
            purchase.setItemType("inapp");
            purchase.setOrderId(jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID));
            purchase.setPackageName(jSONObject.getString("packageName"));
            purchase.setSku(r1);
            purchase.setToken(jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_PURCHASE_TOKEN));
            purchase.setDeveloperPayload(jSONObject.getString("developerPayload"));
            if (this.mPurchaseListener != null) {
                this.mPurchaseListener.onIabPurchaseFinished(new NokiaResult(0, "Success"), purchase);
            }
        } catch (Throwable e) {
            Logger.m4029e(e, "JSONException: ", e);
            IabResult nokiaResult = new NokiaResult(-1002, "Failed to parse purchase data.");
            if (this.mPurchaseListener != null) {
                this.mPurchaseListener.onIabPurchaseFinished(nokiaResult, null);
            }
        }
    }

    private void processPurchasedList(@NotNull ArrayList<String> arrayList, @NotNull Inventory inventory) {
        Logger.m4031i("NokiaStoreHelper.processPurchasedList");
        Iterator it = arrayList.iterator();
        while (it.hasNext()) {
            try {
                JSONObject jSONObject = new JSONObject((String) it.next());
                Purchase purchase = new Purchase(OpenIabHelper.NAME_NOKIA);
                purchase.setItemType("inapp");
                purchase.setSku(SkuManager.getInstance().getSku(OpenIabHelper.NAME_NOKIA, jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID)));
                purchase.setToken(jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_PURCHASE_TOKEN));
                purchase.setPackageName(getPackageName());
                purchase.setPurchaseState(0);
                purchase.setDeveloperPayload(jSONObject.optString("developerPayload", ""));
                inventory.addPurchase(purchase);
            } catch (Throwable e) {
                Logger.m4029e(e, "Exception: ", e);
            }
        }
    }

    private void refreshItemDetails(@Nullable List<String> list, @NotNull Inventory inventory) throws IabException {
        Logger.m4031i("NokiaStoreHelper.refreshItemDetails");
        Bundle bundle = new Bundle(32);
        ArrayList arrayList = new ArrayList(32);
        Collection allStoreSkus = SkuManager.getInstance().getAllStoreSkus(OpenIabHelper.NAME_NOKIA);
        if (!CollectionUtils.isEmpty(allStoreSkus)) {
            arrayList.addAll(allStoreSkus);
        }
        if (list != null) {
            for (String storeSku : list) {
                arrayList.add(SkuManager.getInstance().getStoreSku(OpenIabHelper.NAME_NOKIA, storeSku));
            }
        }
        bundle.putStringArrayList("ITEM_ID_LIST", arrayList);
        try {
            if (this.mService == null) {
                Logger.m4027e("Unable to refresh item details.");
                throw new IabException(-1002, "Error refreshing item details.");
            }
            Bundle productDetails = this.mService.getProductDetails(3, getPackageName(), "inapp", bundle);
            int i = productDetails.getInt("RESPONSE_CODE");
            List stringArrayList = productDetails.getStringArrayList("DETAILS_LIST");
            Logger.m4026d("responseCode = ", Integer.valueOf(i));
            Logger.m4026d("detailsList = ", stringArrayList);
            if (i != 0) {
                throw new IabException(new NokiaResult(i, "Error refreshing inventory (querying prices of items)."));
            }
            processDetailsList(stringArrayList, inventory);
        } catch (Throwable e) {
            Logger.m4029e(e, "Exception: ", e);
        } catch (Throwable e2) {
            Logger.m4029e(e2, "Exception: ", e2);
        }
    }

    private void refreshPurchasedItems(@Nullable List<String> list, @NotNull Inventory inventory) throws IabException {
        Logger.m4031i("NokiaStoreHelper.refreshPurchasedItems");
        ArrayList arrayList = new ArrayList(SkuManager.getInstance().getAllStoreSkus(OpenIabHelper.NAME_NOKIA));
        Bundle bundle = new Bundle(32);
        if (list != null) {
            for (String add : list) {
                arrayList.add(add);
            }
        }
        bundle.putStringArrayList("ITEM_ID_LIST", arrayList);
        try {
            if (this.mService == null) {
                Logger.m4027e("Unable to refresh purchased items.");
                throw new IabException(-1002, "Error refreshing inventory (querying owned items).");
            }
            Bundle purchases = this.mService.getPurchases(3, getPackageName(), "inapp", bundle, null);
            int i = purchases.getInt("RESPONSE_CODE");
            ArrayList stringArrayList = purchases.getStringArrayList("INAPP_PURCHASE_ITEM_LIST");
            ArrayList stringArrayList2 = purchases.getStringArrayList("INAPP_PURCHASE_DATA_LIST");
            Logger.m4026d("responseCode = ", Integer.valueOf(i));
            Logger.m4026d("purchasedItemList = ", stringArrayList);
            Logger.m4026d("purchasedDataList = ", stringArrayList2);
            if (i != 0) {
                throw new IabException(new NokiaResult(i, "Error refreshing inventory (querying owned items)."));
            }
            processPurchasedList(stringArrayList2, inventory);
        } catch (Throwable e) {
            Logger.m4029e(e, "Exception: ", e);
        }
    }

    public void consume(@NotNull Purchase purchase) throws IabException {
        int consumePurchase;
        Logger.m4031i("NokiaStoreHelper.consume");
        String token = purchase.getToken();
        String sku = purchase.getSku();
        String packageName = purchase.getPackageName();
        Logger.m4026d("productId = ", sku);
        Logger.m4026d("token = ", token);
        Logger.m4026d("packageName = ", packageName);
        try {
            consumePurchase = this.mService.consumePurchase(3, packageName, sku, token);
        } catch (Throwable e) {
            Logger.m4029e(e, "RemoteException: ", e);
            consumePurchase = 0;
        }
        if (consumePurchase == 0) {
            Logger.m4026d("Successfully consumed productId: ", sku);
            Logger.m4025d("consume: done");
            return;
        }
        Logger.m4026d("Error consuming consuming productId ", sku, ". Code: ", Integer.valueOf(consumePurchase));
        throw new IabException(new NokiaResult(consumePurchase, "Error consuming productId " + sku));
    }

    public void dispose() {
        Logger.m4031i("NokiaStoreHelper.dispose");
        if (this.mServiceConn != null) {
            if (this.mContext != null) {
                this.mContext.unbindService(this.mServiceConn);
            }
            this.mServiceConn = null;
            this.mService = null;
        }
    }

    public String getPackageName() {
        return this.mContext.getPackageName();
    }

    public boolean handleActivityResult(int i, int i2, @Nullable Intent intent) {
        Logger.m4031i("NokiaStoreHelper.handleActivityResult");
        if (i != this.mRequestCode) {
            return false;
        }
        IabResult nokiaResult;
        if (intent == null) {
            Logger.m4027e("Null data in IAB activity result.");
            nokiaResult = new NokiaResult(-1002, "Null data in IAB result");
            if (this.mPurchaseListener == null) {
                return true;
            }
            this.mPurchaseListener.onIabPurchaseFinished(nokiaResult, null);
            return true;
        }
        int intExtra = intent.getIntExtra("RESPONSE_CODE", 0);
        String stringExtra = intent.getStringExtra("INAPP_PURCHASE_DATA");
        Logger.m4026d("responseCode = ", Integer.valueOf(intExtra));
        Logger.m4026d("purchaseData = ", stringExtra);
        if (i2 == -1 && intExtra == 0) {
            processPurchaseSuccess(stringExtra);
            return true;
        } else if (i2 == -1) {
            processPurchaseFail(intExtra);
            return true;
        } else if (i2 == 0) {
            Logger.m4026d("Purchase canceled - Response: ", Integer.valueOf(intExtra));
            nokiaResult = new NokiaResult(-1005, "User canceled.");
            if (this.mPurchaseListener == null) {
                return true;
            }
            this.mPurchaseListener.onIabPurchaseFinished(nokiaResult, null);
            return true;
        } else {
            Logger.m4030e("Purchase failed. Result code: ", Integer.valueOf(i2));
            nokiaResult = new NokiaResult(-1006, "Unknown purchase response.");
            if (this.mPurchaseListener == null) {
                return true;
            }
            this.mPurchaseListener.onIabPurchaseFinished(nokiaResult, null);
            return true;
        }
    }

    public void launchPurchaseFlow(@NotNull Activity activity, String str, @NotNull String str2, int i, @Nullable IabHelper$OnIabPurchaseFinishedListener iabHelper$OnIabPurchaseFinishedListener, String str3) {
        IabResult iabResult;
        Logger.m4031i("NokiaStoreHelper.launchPurchaseFlow");
        if (str2.equals("subs")) {
            iabResult = new IabResult(-1009, "Subscriptions are not available.");
            if (iabHelper$OnIabPurchaseFinishedListener != null) {
                iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, null);
                return;
            }
            return;
        }
        try {
            if (this.mService != null) {
                Bundle buyIntent = this.mService.getBuyIntent(3, getPackageName(), str, "inapp", str3);
                Logger.m4026d("buyIntentBundle = ", buyIntent);
                int i2 = buyIntent.getInt("RESPONSE_CODE", 0);
                PendingIntent pendingIntent = (PendingIntent) buyIntent.getParcelable("BUY_INTENT");
                if (i2 == 0) {
                    this.mRequestCode = i;
                    this.mPurchaseListener = iabHelper$OnIabPurchaseFinishedListener;
                    activity.startIntentSenderForResult(pendingIntent.getIntentSender(), i, new Intent(), 0, 0, 0);
                } else if (iabHelper$OnIabPurchaseFinishedListener != null) {
                    iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(new NokiaResult(i2, "Failed to get buy intent."), null);
                }
            } else if (iabHelper$OnIabPurchaseFinishedListener != null) {
                Logger.m4027e("Unable to buy item, Error response: service is not connected.");
                iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(new NokiaResult(6, "Unable to buy item"), null);
            }
        } catch (Throwable e) {
            Logger.m4029e(e, "RemoteException: ", e);
            iabResult = new NokiaResult(-1004, "Failed to send intent.");
            if (iabHelper$OnIabPurchaseFinishedListener != null) {
                iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, null);
            }
        } catch (Throwable e2) {
            Logger.m4029e(e2, "SendIntentException: ", e2);
            iabResult = new NokiaResult(-1001, "Remote exception while starting purchase flow");
            if (iabHelper$OnIabPurchaseFinishedListener != null) {
                iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, null);
            }
        }
    }

    public void processPurchaseFail(int i) {
        Logger.m4026d("Result code was OK but in-app billing response was not OK: ", Integer.valueOf(i));
        if (this.mPurchaseListener != null) {
            this.mPurchaseListener.onIabPurchaseFinished(new NokiaResult(i, "Problem purchashing item."), null);
        }
    }

    public Inventory queryInventory(boolean z, List<String> list, List<String> list2) throws IabException {
        Inventory inventory = new Inventory();
        Logger.m4031i("NokiaStoreHelper.queryInventory");
        Logger.m4026d("querySkuDetails = ", Boolean.valueOf(z));
        Logger.m4026d("moreItemSkus = ", list);
        if (z) {
            refreshItemDetails(list, inventory);
        }
        refreshPurchasedItems(list, inventory);
        return inventory;
    }

    public void startSetup(@Nullable final OnIabSetupFinishedListener onIabSetupFinishedListener) {
        Logger.m4031i("NokiaStoreHelper.startSetup");
        this.mServiceConn = new ServiceConnection() {
            public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
                Logger.m4031i("NokiaStoreHelper:startSetup.onServiceConnected");
                Logger.m4025d("name = " + componentName);
                NokiaStoreHelper.this.mService = Stub.asInterface(iBinder);
                try {
                    int isBillingSupported = NokiaStoreHelper.this.mService.isBillingSupported(3, NokiaStoreHelper.this.getPackageName(), "inapp");
                    if (isBillingSupported != 0) {
                        if (onIabSetupFinishedListener != null) {
                            onIabSetupFinishedListener.onIabSetupFinished(new NokiaResult(isBillingSupported, "Error checking for billing support."));
                        }
                    } else if (onIabSetupFinishedListener != null) {
                        onIabSetupFinishedListener.onIabSetupFinished(new NokiaResult(0, "Setup successful."));
                    }
                } catch (Throwable e) {
                    if (onIabSetupFinishedListener != null) {
                        onIabSetupFinishedListener.onIabSetupFinished(new NokiaResult(-1001, "RemoteException while setting up in-app billing."));
                    }
                    Logger.m4029e(e, "Exception: ", e);
                }
            }

            public void onServiceDisconnected(ComponentName componentName) {
                Logger.m4031i("NokiaStoreHelper:startSetup.onServiceDisconnected");
                Logger.m4026d("name = ", componentName);
                NokiaStoreHelper.this.mService = null;
            }
        };
        Intent serviceIntent = getServiceIntent();
        List queryIntentServices = this.mContext.getPackageManager().queryIntentServices(serviceIntent, 0);
        if (queryIntentServices != null && !queryIntentServices.isEmpty()) {
            try {
                this.mContext.bindService(serviceIntent, this.mServiceConn, 1);
            } catch (Throwable e) {
                Logger.m4028e("Can't bind to the service", e);
                if (onIabSetupFinishedListener != null) {
                    onIabSetupFinishedListener.onIabSetupFinished(new NokiaResult(3, "Billing service unavailable on device due to lack of the permission \"com.nokia.payment.BILLING\"."));
                }
            }
        } else if (onIabSetupFinishedListener != null) {
            onIabSetupFinishedListener.onIabSetupFinished(new NokiaResult(3, "Billing service unavailable on device."));
        }
    }

    public boolean subscriptionsSupported() {
        return false;
    }
}
