package org.onepf.oms.appstore;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import com.amazon.device.iap.PurchasingListener;
import com.amazon.device.iap.PurchasingService;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.Product;
import com.amazon.device.iap.model.ProductDataResponse;
import com.amazon.device.iap.model.ProductType;
import com.amazon.device.iap.model.PurchaseResponse;
import com.amazon.device.iap.model.PurchaseResponse.RequestStatus;
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
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.CountDownLatch;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.SkuManager;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabPurchaseFinishedListener;
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
    private final Map<RequestId, OnIabPurchaseFinishedListener> requestListeners = new HashMap();
    private final Map<RequestId, String> requestSkuMap = new HashMap();
    @Nullable
    private OnIabSetupFinishedListener setupListener;

    public AmazonAppstoreBillingService(@NotNull Context context2) {
        this.context = context2.getApplicationContext();
    }

    private String generateOriginalJson(@NotNull PurchaseResponse purchaseResponse) {
        JSONObject jSONObject = new JSONObject();
        try {
            Receipt receipt = purchaseResponse.getReceipt();
            jSONObject.put(JSON_KEY_ORDER_ID, purchaseResponse.getRequestId());
            jSONObject.put(JSON_KEY_PRODUCT_ID, receipt.getSku());
            RequestStatus requestStatus = purchaseResponse.getRequestStatus();
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
            Logger.m1026d("generateOriginalJson(): JSON\n", jSONObject);
        } catch (JSONException e) {
            Logger.m1028e("generateOriginalJson() failed to generate JSON", (Throwable) e);
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
            switch (receipt.getProductType()) {
                case CONSUMABLE:
                case ENTITLED:
                    purchase.setItemType("inapp");
                    Logger.m1026d("Add to inventory SKU: ", sku);
                    break;
                case SUBSCRIPTION:
                    purchase.setItemType("subs");
                    purchase.setSku(SkuManager.getInstance().getSku(OpenIabHelper.NAME_AMAZON, sku));
                    Logger.m1026d("Add subscription to inventory SKU: ", sku);
                    break;
            }
        }
        return purchase;
    }

    @NotNull
    private SkuDetails getSkuDetails(@NotNull Product product) {
        String sku = product.getSku();
        String str = product.getPrice().toString();
        String title = product.getTitle();
        String description = product.getDescription();
        ProductType productType = product.getProductType();
        Logger.m1025d(String.format("Item: %s\n Type: %s\n SKU: %s\n Price: %s\n Description: %s\n", new Object[]{title, productType, sku, str, description}));
        return new SkuDetails(productType == ProductType.SUBSCRIPTION ? "subs" : "inapp", SkuManager.getInstance().getSku(OpenIabHelper.NAME_AMAZON, sku), title, str, description);
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

    public void launchPurchaseFlow(Activity activity, String str, String str2, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str3) {
        RequestId purchase = PurchasingService.purchase(str);
        this.requestSkuMap.put(purchase, str);
        this.requestListeners.put(purchase, onIabPurchaseFinishedListener);
    }

    public void onProductDataResponse(@NotNull ProductDataResponse productDataResponse) {
        ProductDataResponse.RequestStatus requestStatus = productDataResponse.getRequestStatus();
        Logger.m1026d("onItemDataResponse() reqStatus: ", requestStatus, ", reqId: ", productDataResponse.getRequestId());
        switch (requestStatus) {
            case SUCCESSFUL:
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
        RequestStatus requestStatus = purchaseResponse.getRequestStatus();
        RequestId requestId = purchaseResponse.getRequestId();
        Logger.m1026d("onPurchaseResponse() PurchaseRequestStatus:", requestStatus, ", reqId: ", requestId);
        String str = (String) this.requestSkuMap.remove(requestId);
        Receipt receipt = purchaseResponse.getReceipt();
        Purchase purchase = getPurchase(receipt);
        switch (requestStatus) {
            case SUCCESSFUL:
                String userId = purchaseResponse.getUserData().getUserId();
                if (userId.equals(this.currentUserId)) {
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
                } else {
                    Logger.m1036w("onPurchaseResponse() Current UserId: ", this.currentUserId, ", purchase UserId: ", userId);
                    iabResult = new IabResult(6, "Current UserId doesn't match purchase UserId");
                    break;
                }
            case INVALID_SKU:
                iabResult = new IabResult(4, "Invalid SKU");
                break;
            case ALREADY_PURCHASED:
                iabResult = new IabResult(7, "Item is already purchased");
                break;
            case FAILED:
                iabResult = new IabResult(6, "Purchase failed");
                break;
            case NOT_SUPPORTED:
                iabResult = new IabResult(3, "This call is not supported");
                break;
            default:
                iabResult = null;
                break;
        }
        OnIabPurchaseFinishedListener onIabPurchaseFinishedListener = (OnIabPurchaseFinishedListener) this.requestListeners.remove(requestId);
        if (onIabPurchaseFinishedListener != null) {
            onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, purchase);
        } else {
            Logger.m1027e("Something went wrong: PurchaseFinishedListener is not found");
        }
    }

    public void onPurchaseUpdatesResponse(PurchaseUpdatesResponse purchaseUpdatesResponse) {
        PurchaseUpdatesResponse.RequestStatus requestStatus = purchaseUpdatesResponse.getRequestStatus();
        Logger.m1026d("onPurchaseUpdatesResponse() reqStatus: ", requestStatus, "reqId: ", purchaseUpdatesResponse.getRequestId());
        switch (requestStatus) {
            case SUCCESSFUL:
                for (String erasePurchase : this.inventory.getAllOwnedSkus()) {
                    this.inventory.erasePurchase(erasePurchase);
                }
                String userId = purchaseUpdatesResponse.getUserData().getUserId();
                if (!userId.equals(this.currentUserId)) {
                    Logger.m1036w("onPurchaseUpdatesResponse() Current UserId: ", this.currentUserId, ", purchase UserId: ", userId);
                    break;
                } else {
                    for (Receipt purchase : purchaseUpdatesResponse.getReceipts()) {
                        this.inventory.addPurchase(getPurchase(purchase));
                    }
                    if (purchaseUpdatesResponse.hasMore()) {
                        PurchasingService.getPurchaseUpdates(false);
                        Logger.m1025d("Initiating Another Purchase Updates with offset: ");
                        return;
                    }
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
        Logger.m1026d("onUserDataResponse() reqId: ", userDataResponse.getRequestId(), ", status: ", userDataResponse.getRequestStatus());
        switch (userDataResponse.getRequestStatus()) {
            case SUCCESSFUL:
                String userId = userDataResponse.getUserData().getUserId();
                this.currentUserId = userId;
                iabResult = new IabResult(0, "Setup successful.");
                Logger.m1026d("Set current userId: ", userId);
                break;
            case FAILED:
            case NOT_SUPPORTED:
                iabResult = new IabResult(6, "Unable to get userId");
                Logger.m1025d("onUserDataResponse() Unable to get user ID");
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
        Logger.m1026d("queryInventory() querySkuDetails: ", Boolean.valueOf(z), " moreItemSkus: ", list, " moreSubsSkus: ", list2);
        CountDownLatch countDownLatch = new CountDownLatch(1);
        this.inventoryLatchQueue.offer(countDownLatch);
        PurchasingService.getPurchaseUpdates(true);
        try {
            countDownLatch.await();
            if (z) {
                HashSet<String> hashSet = new HashSet<>(this.inventory.getAllOwnedSkus());
                if (list != null) {
                    hashSet.addAll(list);
                }
                if (list2 != null) {
                    hashSet.addAll(list2);
                }
                if (!hashSet.isEmpty()) {
                    HashSet hashSet2 = new HashSet(hashSet.size());
                    for (String storeSku : hashSet) {
                        hashSet2.add(SkuManager.getInstance().getStoreSku(OpenIabHelper.NAME_AMAZON, storeSku));
                    }
                    CountDownLatch countDownLatch2 = new CountDownLatch(1);
                    this.inventoryLatchQueue.offer(countDownLatch2);
                    PurchasingService.getProductData(hashSet2);
                    try {
                        countDownLatch2.await();
                    } catch (InterruptedException e) {
                        Logger.m1034w("queryInventory() SkuDetails fetching interrupted");
                        return null;
                    }
                }
            }
            Logger.m1026d("queryInventory() finished. Inventory size: ", Integer.valueOf(this.inventory.getAllOwnedSkus().size()));
            return this.inventory;
        } catch (InterruptedException e2) {
            Logger.m1027e("queryInventory() await interrupted");
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
