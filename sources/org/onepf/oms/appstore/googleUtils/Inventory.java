package org.onepf.oms.appstore.googleUtils;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import org.jetbrains.annotations.NotNull;

public class Inventory {
    private final Map<String, Purchase> mPurchaseMap = new ConcurrentHashMap();
    private final Map<String, SkuDetails> mSkuMap = new ConcurrentHashMap();

    public void addPurchase(@NotNull Purchase purchase) {
        this.mPurchaseMap.put(purchase.getSku(), purchase);
    }

    public void addSkuDetails(@NotNull SkuDetails skuDetails) {
        this.mSkuMap.put(skuDetails.getSku(), skuDetails);
    }

    public void erasePurchase(String str) {
        if (this.mPurchaseMap.containsKey(str)) {
            this.mPurchaseMap.remove(str);
        }
    }

    @NotNull
    public List<String> getAllOwnedSkus() {
        return new ArrayList(this.mPurchaseMap.keySet());
    }

    @NotNull
    public List<String> getAllOwnedSkus(String str) {
        ArrayList arrayList = new ArrayList();
        for (Purchase purchase : this.mPurchaseMap.values()) {
            if (purchase.getItemType().equals(str)) {
                arrayList.add(purchase.getSku());
            }
        }
        return arrayList;
    }

    @NotNull
    public List<Purchase> getAllPurchases() {
        return new ArrayList(this.mPurchaseMap.values());
    }

    public Purchase getPurchase(String str) {
        return (Purchase) this.mPurchaseMap.get(str);
    }

    public Map<String, Purchase> getPurchaseMap() {
        return Collections.unmodifiableMap(this.mPurchaseMap);
    }

    public SkuDetails getSkuDetails(String str) {
        return (SkuDetails) this.mSkuMap.get(str);
    }

    public Map<String, SkuDetails> getSkuMap() {
        return Collections.unmodifiableMap(this.mSkuMap);
    }

    public boolean hasDetails(String str) {
        return this.mSkuMap.containsKey(str);
    }

    public boolean hasPurchase(String str) {
        return this.mPurchaseMap.containsKey(str);
    }
}
