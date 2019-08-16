package net.gogame.gopay.sdk.iab;

import com.appsflyer.share.Constants;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: net.gogame.gopay.sdk.iab.br */
final class C1393br {

    /* renamed from: a */
    int f1098a;

    /* renamed from: b */
    String f1099b;

    /* renamed from: c */
    JSONObject f1100c = new JSONObject();

    public C1393br(String str, String str2, String str3) {
        this.f1100c.put("packageName", str3);
        this.f1100c.put(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, str);
        String[] split = str2.split(Constants.URL_PATH_DELIMITER);
        if (split.length >= 5) {
            this.f1100c.put(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, split[1]);
            this.f1100c.put(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_PURCHASE_TOKEN, split[2]);
            this.f1100c.put("purchaseTime", split[3]);
            if (split.length == 6) {
                this.f1100c.put("developerPayload", split[4]);
                this.f1098a = Integer.parseInt(split[5]);
            } else {
                this.f1098a = Integer.parseInt(split[4]);
            }
            this.f1099b = split[2];
        } else {
            this.f1100c.put(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, split[0]);
            this.f1098a = Integer.parseInt(split[1]);
        }
        this.f1100c.put("purchaseState", this.f1098a);
    }

    public C1393br(String str, String str2, String str3, String str4, long j, String str5, int i) {
        this.f1098a = i;
        this.f1099b = str4;
        this.f1100c.put(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, str);
        this.f1100c.put(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, str3);
        this.f1100c.put("purchaseState", i);
        this.f1100c.put("packageName", str2);
        this.f1100c.put("developerPayload", str5);
        this.f1100c.put("purchaseTime", j);
        this.f1100c.put(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_PURCHASE_TOKEN, str4);
    }

    public final String toString() {
        return this.f1100c.toString();
    }
}
