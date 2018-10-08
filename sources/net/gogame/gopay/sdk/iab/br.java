package net.gogame.gopay.sdk.iab;

import com.appsflyer.share.Constants;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

final class br {
    /* renamed from: a */
    int f3501a;
    /* renamed from: b */
    String f3502b;
    /* renamed from: c */
    JSONObject f3503c = new JSONObject();

    public br(String str, String str2, String str3) {
        this.f3503c.put("packageName", str3);
        this.f3503c.put(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, str);
        String[] split = str2.split(Constants.URL_PATH_DELIMITER);
        if (split.length >= 5) {
            this.f3503c.put(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, split[1]);
            this.f3503c.put(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_PURCHASE_TOKEN, split[2]);
            this.f3503c.put("purchaseTime", split[3]);
            if (split.length == 6) {
                this.f3503c.put("developerPayload", split[4]);
                this.f3501a = Integer.parseInt(split[5]);
            } else {
                this.f3501a = Integer.parseInt(split[4]);
            }
            this.f3502b = split[2];
        } else {
            this.f3503c.put(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, split[0]);
            this.f3501a = Integer.parseInt(split[1]);
        }
        this.f3503c.put("purchaseState", this.f3501a);
    }

    public br(String str, String str2, String str3, String str4, long j, String str5, int i) {
        this.f3501a = i;
        this.f3502b = str4;
        this.f3503c.put(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, str);
        this.f3503c.put(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, str3);
        this.f3503c.put("purchaseState", i);
        this.f3503c.put("packageName", str2);
        this.f3503c.put("developerPayload", str5);
        this.f3503c.put("purchaseTime", j);
        this.f3503c.put(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_PURCHASE_TOKEN, str4);
    }

    public final String toString() {
        return this.f3503c.toString();
    }
}
