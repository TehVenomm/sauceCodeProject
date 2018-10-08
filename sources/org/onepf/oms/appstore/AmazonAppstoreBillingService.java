package org.onepf.oms.appstore;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import com.amazon.device.iap.PurchasingListener;
import com.amazon.device.iap.PurchasingService;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.Product;
import com.amazon.device.iap.model.ProductDataResponse;
import com.amazon.device.iap.model.ProductDataResponse.RequestStatus;
import com.amazon.device.iap.model.ProductType;
import com.amazon.device.iap.model.PurchaseResponse;
import com.amazon.device.iap.model.PurchaseUpdatesResponse;
import com.amazon.device.iap.model.Receipt;
import com.amazon.device.iap.model.RequestId;
import com.amazon.device.iap.model.UserData;
import com.amazon.device.iap.model.UserDataResponse;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Queue;
import java.util.Set;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.CountDownLatch;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.json.JSONObject;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.SkuManager;
import org.onepf.oms.appstore.googleUtils.IabHelper$OnIabPurchaseFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.appstore.googleUtils.Purchase;
import org.onepf.oms.appstore.googleUtils.SkuDetails;
import org.onepf.oms.util.Logger;

public class AmazonAppstoreBillingService implements AppstoreInAppBillingService, PurchasingListener {
    public static final String JSON_KEY_ORDER_ID = "orderId";
    public static final String JSON_KEY_PRODUCT_ID = "productId";
    public static final String JSON_KEY_PURCHASE_STATUS = "purchaseStatus";
    public static final String JSON_KEY_RECEIPT_ITEM_TYPE = "itemType";
    public static final String JSON_KEY_RECEIPT_PURCHASE_TOKEN = "purchaseToken";
    public static final String JSON_KEY_USER_ID = "userId";
    private final Context context;
    private String currentUserId;
    private final Inventory inventory = new Inventory();
    private final Queue<CountDownLatch> inventoryLatchQueue = new ConcurrentLinkedQueue();
    private final Map<RequestId, IabHelper$OnIabPurchaseFinishedListener> requestListeners = new HashMap();
    private final Map<RequestId, String> requestSkuMap = new HashMap();
    @Nullable
    private OnIabSetupFinishedListener setupListener;

    /* renamed from: org.onepf.oms.appstore.AmazonAppstoreBillingService$1 */
    static /* synthetic */ class C16241 {
        /* renamed from: $SwitchMap$com$amazon$device$iap$model$ProductDataResponse$RequestStatus */
        static final /* synthetic */ int[] f3766x4eb80c9f = new int[RequestStatus.values().length];
        static final /* synthetic */ int[] $SwitchMap$com$amazon$device$iap$model$ProductType = new int[ProductType.values().length];
        /* renamed from: $SwitchMap$com$amazon$device$iap$model$PurchaseResponse$RequestStatus */
        static final /* synthetic */ int[] f3767xc71ab397 = new int[PurchaseResponse.RequestStatus.values().length];
        /* renamed from: $SwitchMap$com$amazon$device$iap$model$PurchaseUpdatesResponse$RequestStatus */
        static final /* synthetic */ int[] f3768xe10c5bef = new int[PurchaseUpdatesResponse.RequestStatus.values().length];
        /* renamed from: $SwitchMap$com$amazon$device$iap$model$UserDataResponse$RequestStatus */
        static final /* synthetic */ int[] f3769x29a859ab = new int[UserDataResponse.RequestStatus.values().length];

        static {
            try {
                f3767xc71ab397[PurchaseResponse.RequestStatus.SUCCESSFUL.ordinal()] = 1;
            } catch (NoSuchFieldError e) {
            }
            try {
                f3767xc71ab397[PurchaseResponse.RequestStatus.INVALID_SKU.ordinal()] = 2;
            } catch (NoSuchFieldError e2) {
            }
            try {
                f3767xc71ab397[PurchaseResponse.RequestStatus.ALREADY_PURCHASED.ordinal()] = 3;
            } catch (NoSuchFieldError e3) {
            }
            try {
                f3767xc71ab397[PurchaseResponse.RequestStatus.FAILED.ordinal()] = 4;
            } catch (NoSuchFieldError e4) {
            }
            try {
                f3767xc71ab397[PurchaseResponse.RequestStatus.NOT_SUPPORTED.ordinal()] = 5;
            } catch (NoSuchFieldError e5) {
            }
            try {
                f3766x4eb80c9f[RequestStatus.SUCCESSFUL.ordinal()] = 1;
            } catch (NoSuchFieldError e6) {
            }
            try {
                f3766x4eb80c9f[RequestStatus.FAILED.ordinal()] = 2;
            } catch (NoSuchFieldError e7) {
            }
            try {
                f3766x4eb80c9f[RequestStatus.NOT_SUPPORTED.ordinal()] = 3;
            } catch (NoSuchFieldError e8) {
            }
            try {
                $SwitchMap$com$amazon$device$iap$model$ProductType[ProductType.CONSUMABLE.ordinal()] = 1;
            } catch (NoSuchFieldError e9) {
            }
            try {
                $SwitchMap$com$amazon$device$iap$model$ProductType[ProductType.ENTITLED.ordinal()] = 2;
            } catch (NoSuchFieldError e10) {
            }
            try {
                $SwitchMap$com$amazon$device$iap$model$ProductType[ProductType.SUBSCRIPTION.ordinal()] = 3;
            } catch (NoSuchFieldError e11) {
            }
            try {
                f3768xe10c5bef[PurchaseUpdatesResponse.RequestStatus.SUCCESSFUL.ordinal()] = 1;
            } catch (NoSuchFieldError e12) {
            }
            try {
                f3768xe10c5bef[PurchaseUpdatesResponse.RequestStatus.FAILED.ordinal()] = 2;
            } catch (NoSuchFieldError e13) {
            }
            try {
                f3769x29a859ab[UserDataResponse.RequestStatus.SUCCESSFUL.ordinal()] = 1;
            } catch (NoSuchFieldError e14) {
            }
            try {
                f3769x29a859ab[UserDataResponse.RequestStatus.FAILED.ordinal()] = 2;
            } catch (NoSuchFieldError e15) {
            }
            try {
                f3769x29a859ab[UserDataResponse.RequestStatus.NOT_SUPPORTED.ordinal()] = 3;
            } catch (NoSuchFieldError e16) {
            }
        }
    }

    public AmazonAppstoreBillingService(@NotNull Context context) {
        this.context = context.getApplicationContext();
    }

    private String generateOriginalJson(@NotNull PurchaseResponse purchaseResponse) {
        JSONObject jSONObject = new JSONObject();
        try {
            Receipt receipt = purchaseResponse.getReceipt();
            jSONObject.put(JSON_KEY_ORDER_ID, purchaseResponse.getRequestId());
            jSONObject.put(JSON_KEY_PRODUCT_ID, receipt.getSku());
            PurchaseResponse.RequestStatus requestStatus = purchaseResponse.getRequestStatus();
            if (requestStatus != null) {
                jSONObject.put(JSON_KEY_PURCHASE_STATUS, requestStatus.name());
            }
            UserData userData = purchaseResponse.getUserData();
            if (userData != null) {
                jSONObject.put(JSON_KEY_USER_ID, userData.getUserId());
            }
            ProductType productType = receipt.getProductType();
            if (productType != null) {
                jSONObject.put(JSON_KEY_RECEIPT_ITEM_TYPE, productType.name());
            }
            jSONObject.put(JSON_KEY_RECEIPT_PURCHASE_TOKEN, receipt.getReceiptId());
            Logger.m4026d("generateOriginalJson(): JSON\n", jSONObject);
        } catch (Throwable e) {
            Logger.m4028e("generateOriginalJson() failed to generate JSON", e);
        }
        return jSONObject.toString();
    }

    @NotNull
    private Purchase getPurchase(@Nullable Receipt receipt) {
        Purchase purchase = new Purchase(OpenIabHelper.NAME_AMAZON);
        if (receipt != null) {
            String sku = receipt.getSku();
            purchase.setSku(SkuManager.getInstance().getSku(OpenIabHelper.NAME_AMAZON, sku));
            purchase.setToken(receipt.getReceiptId());
            switch (C16241.$SwitchMap$com$amazon$device$iap$model$ProductType[receipt.getProductType().ordinal()]) {
                case 1:
                case 2:
                    purchase.setItemType("inapp");
                    Logger.m4026d("Add to inventory SKU: ", sku);
                    break;
                case 3:
                    purchase.setItemType("subs");
                    purchase.setSku(SkuManager.getInstance().getSku(OpenIabHelper.NAME_AMAZON, sku));
                    Logger.m4026d("Add subscription to inventory SKU: ", sku);
                    break;
                default:
                    break;
            }
        }
        return purchase;
    }

    @NotNull
    private SkuDetails getSkuDetails(@NotNull Product product) {
        String sku = product.getSku();
        String str = product.getPrice().toString();
        Logger.m4025d(String.format("Item: %s\n Type: %s\n SKU: %s\n Price: %s\n Description: %s\n", new Object[]{product.getTitle(), product.getProductType(), sku, str, product.getDescription()}));
        return new SkuDetails(product.getProductType() == ProductType.SUBSCRIPTION ? "subs" : "inapp", SkuManager.getInstance().getSku(OpenIabHelper.NAME_AMAZON, sku), product.getTitle(), str, product.getDescription());
    }

    public void consume(Purchase purchase) {
        PurchasingService.notifyFulfillment(purchase.getToken(), FulfillmentResult.FULFILLED);
    }

    public void dispose() {
        this.setupListener = null;
    }

    public boolean handleActivityResult(int i, int i2, Intent intent) {
        return false;
    }

    public void launchPurchaseFlow(Activity activity, String str, String str2, int i, IabHelper$OnIabPurchaseFinishedListener iabHelper$OnIabPurchaseFinishedListener, String str3) {
        RequestId purchase = PurchasingService.purchase(str);
        this.requestSkuMap.put(purchase, str);
        this.requestListeners.put(purchase, iabHelper$OnIabPurchaseFinishedListener);
    }

    public void onProductDataResponse(@NotNull ProductDataResponse productDataResponse) {
        Logger.m4026d("onItemDataResponse() reqStatus: ", productDataResponse.getRequestStatus(), ", reqId: ", productDataResponse.getRequestId());
        switch (C16241.f3766x4eb80c9f[productDataResponse.getRequestStatus().ordinal()]) {
            case 1:
                Map productData = productDataResponse.getProductData();
                for (String str : productData.keySet()) {
                    this.inventory.addSkuDetails(getSkuDetails((Product) productData.get(str)));
                }
                break;
        }
        CountDownLatch countDownLatch = (CountDownLatch) this.inventoryLatchQueue.poll();
        if (countDownLatch != null) {
            countDownLatch.countDown();
        }
    }

    public void onPurchaseResponse(@NotNull PurchaseResponse purchaseResponse) {
        IabResult iabResult;
        PurchaseResponse.RequestStatus requestStatus = purchaseResponse.getRequestStatus();
        RequestId requestId = purchaseResponse.getRequestId();
        Logger.m4026d("onPurchaseResponse() PurchaseRequestStatus:", requestStatus, ", reqId: ", requestId);
        String str = (String) this.requestSkuMap.remove(requestId);
        Receipt receipt = purchaseResponse.getReceipt();
        Purchase purchase = getPurchase(receipt);
        switch (C16241.f3767xc71ab397[requestStatus.ordinal()]) {
            case 1:
                if (!purchaseResponse.getUserData().getUserId().equals(this.currentUserId)) {
                    Logger.m4036w("onPurchaseResponse() Current UserId: ", this.currentUserId, ", purchase UserId: ", purchaseResponse.getUserData().getUserId());
                    iabResult = new IabResult(6, "Current UserId doesn't match purchase UserId");
                    break;
                }
                purchase.setOriginalJson(generateOriginalJson(purchaseResponse));
                purchase.setOrderId(requestId.toString());
                ProductType productType = receipt.getProductType();
                String sku = receipt.getSku();
                SkuManager instance = SkuManager.getInstance();
                if (productType != ProductType.SUBSCRIPTION) {
                    str = sku;
                }
                purchase.setSku(instance.getSku(OpenIabHelper.NAME_AMAZON, str));
                purchase.setItemType(productType == ProductType.SUBSCRIPTION ? "subs" : "inapp");
                iabResult = new IabResult(0, "Success");
                break;
            case 2:
                iabResult = new IabResult(4, "Invalid SKU");
                break;
            case 3:
                iabResult = new IabResult(7, "Item is already purchased");
                break;
            case 4:
                iabResult = new IabResult(6, "Purchase failed");
                break;
            case 5:
                iabResult = new IabResult(3, "This call is not supported");
                break;
            default:
                iabResult = null;
                break;
        }
        IabHelper$OnIabPurchaseFinishedListener iabHelper$OnIabPurchaseFinishedListener = (IabHelper$OnIabPurchaseFinishedListener) this.requestListeners.remove(requestId);
        if (iabHelper$OnIabPurchaseFinishedListener != null) {
            iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, purchase);
        } else {
            Logger.m4027e("Something went wrong: PurchaseFinishedListener is not found");
        }
    }

    public void onPurchaseUpdatesResponse(PurchaseUpdatesResponse purchaseUpdatesResponse) {
        Logger.m4026d("onPurchaseUpdatesResponse() reqStatus: ", purchaseUpdatesResponse.getRequestStatus(), "reqId: ", purchaseUpdatesResponse.getRequestId());
        switch (C16241.f3768xe10c5bef[purchaseUpdatesResponse.getRequestStatus().ordinal()]) {
            case 1:
                for (String erasePurchase : this.inventory.getAllOwnedSkus()) {
                    this.inventory.erasePurchase(erasePurchase);
                }
                if (!purchaseUpdatesResponse.getUserData().getUserId().equals(this.currentUserId)) {
                    Logger.m4036w("onPurchaseUpdatesResponse() Current UserId: ", this.currentUserId, ", purchase UserId: ", purchaseUpdatesResponse.getUserData().getUserId());
                    break;
                }
                for (Receipt purchase : purchaseUpdatesResponse.getReceipts()) {
                    this.inventory.addPurchase(getPurchase(purchase));
                }
                if (purchaseUpdatesResponse.hasMore()) {
                    PurchasingService.getPurchaseUpdates(false);
                    Logger.m4025d("Initiating Another Purchase Updates with offset: ");
                    return;
                }
                break;
        }
        CountDownLatch countDownLatch = (CountDownLatch) this.inventoryLatchQueue.poll();
        if (countDownLatch != null) {
            countDownLatch.countDown();
        }
    }

    public void onUserDataResponse(UserDataResponse userDataResponse) {
        IabResult iabResult;
        Logger.m4026d("onUserDataResponse() reqId: ", userDataResponse.getRequestId(), ", status: ", userDataResponse.getRequestStatus());
        switch (C16241.f3769x29a859ab[userDataResponse.getRequestStatus().ordinal()]) {
            case 1:
                this.currentUserId = userDataResponse.getUserData().getUserId();
                iabResult = new IabResult(0, "Setup successful.");
                Logger.m4026d("Set current userId: ", r1);
                break;
            case 2:
            case 3:
                iabResult = new IabResult(6, "Unable to get userId");
                Logger.m4025d("onUserDataResponse() Unable to get user ID");
                break;
            default:
                iabResult = new IabResult(3, "Unknown response code");
                break;
        }
        if (this.setupListener != null) {
            this.setupListener.onIabSetupFinished(iabResult);
            this.setupListener = null;
        }
    }

    public Inventory queryInventory(boolean z, @Nullable List<String> list, @Nullable List<String> list2) {
        Logger.m4026d("queryInventory() querySkuDetails: ", Boolean.valueOf(z), " moreItemSkus: ", list, " moreSubsSkus: ", list2);
        CountDownLatch countDownLatch = new CountDownLatch(1);
        this.inventoryLatchQueue.offer(countDownLatch);
        PurchasingService.getPurchaseUpdates(true);
        try {
            countDownLatch.await();
            if (z) {
                Set<String> hashSet = new HashSet(this.inventory.getAllOwnedSkus());
                if (list != null) {
                    hashSet.addAll(list);
                }
                if (list2 != null) {
                    hashSet.addAll(list2);
                }
                if (!hashSet.isEmpty()) {
                    Set hashSet2 = new HashSet(hashSet.size());
                    for (String storeSku : hashSet) {
                        hashSet2.add(SkuManager.getInstance().getStoreSku(OpenIabHelper.NAME_AMAZON, storeSku));
                    }
                    countDownLatch = new CountDownLatch(1);
                    this.inventoryLatchQueue.offer(countDownLatch);
                    PurchasingService.getProductData(hashSet2);
                    try {
                        countDownLatch.await();
                    } catch (InterruptedException e) {
                        Logger.m4034w("queryInventory() SkuDetails fetching interrupted");
                        return null;
                    }
                }
            }
            Logger.m4026d("queryInventory() finished. Inventory size: ", Integer.valueOf(this.inventory.getAllOwnedSkus().size()));
            return this.inventory;
        } catch (InterruptedException e2) {
            Logger.m4027e("queryInventory() await interrupted");
            return null;
        }
    }

    public void startSetup(OnIabSetupFinishedListener onIabSetupFinishedListener) {
        this.setupListener = onIabSetupFinishedListener;
        PurchasingService.registerListener(this.context, this);
        PurchasingService.getUserData();
    }

    public boolean subscriptionsSupported() {
        return true;
    }
}
