package p018jp.colopl.iab;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/* renamed from: jp.colopl.iab.Inventory */
public class Inventory {
    Map<String, Purchase> mPurchaseMap = new HashMap();
    Map<String, SkuDetails> mSkuMap = new HashMap();

    Inventory() {
    }

    /* access modifiers changed from: 0000 */
    public void addPurchase(Purchase purchase) {
        this.mPurchaseMap.put(purchase.getSku(), purchase);
    }

    /* access modifiers changed from: 0000 */
    public void addSkuDetails(SkuDetails skuDetails) {
        this.mSkuMap.put(skuDetails.getSku(), skuDetails);
    }

    public void erasePurchase(String str) {
        if (this.mPurchaseMap.containsKey(str)) {
            this.mPurchaseMap.remove(str);
        }
    }

    /* access modifiers changed from: 0000 */
    public List<String> getAllOwnedSkus() {
        return new ArrayList(this.mPurchaseMap.keySet());
    }

    /* access modifiers changed from: 0000 */
    public List<String> getAllOwnedSkus(String str) {
        ArrayList arrayList = new ArrayList();
        for (Purchase purchase : this.mPurchaseMap.values()) {
            if (purchase.getItemType().equals(str)) {
                arrayList.add(purchase.getSku());
            }
        }
        return arrayList;
    }

    public List<Purchase> getAllPurchases() {
        return new ArrayList(this.mPurchaseMap.values());
    }

    public Purchase getPurchase(String str) {
        return (Purchase) this.mPurchaseMap.get(str);
    }

    public SkuDetails getSkuDetails(String str) {
        return (SkuDetails) this.mSkuMap.get(str);
    }

    public boolean hasDetails(String str) {
        return this.mSkuMap.containsKey(str);
    }

    public boolean hasPurchase(String str) {
        return this.mPurchaseMap.containsKey(str);
    }
}
