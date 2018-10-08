package com.amazon.device.iap.internal.util;

import com.amazon.android.Kiwi;
import com.amazon.device.iap.PurchasingService;
import com.amazon.device.iap.internal.model.ReceiptBuilder;
import com.amazon.device.iap.internal.p005b.C0199a;
import com.amazon.device.iap.internal.p005b.C0215d;
import com.amazon.device.iap.model.ProductType;
import com.amazon.device.iap.model.Receipt;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.util.a */
public class C0240a {
    /* renamed from: a */
    private static final String f104a = C0240a.class.getSimpleName();

    /* renamed from: a */
    private static Receipt m158a(JSONObject jSONObject) throws JSONException {
        Date date = null;
        String optString = jSONObject.optString("token");
        String string = jSONObject.getString("sku");
        ProductType valueOf = ProductType.valueOf(jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE).toUpperCase());
        String optString2 = jSONObject.optString("startDate");
        Date b = C0240a.m160a(optString2) ? null : C0240a.m163b(optString2);
        String optString3 = jSONObject.optString("endDate");
        if (!C0240a.m160a(optString3)) {
            date = C0240a.m163b(optString3);
        }
        return new ReceiptBuilder().setReceiptId(optString).setSku(string).setProductType(valueOf).setPurchaseDate(b).setCancelDate(date).build();
    }

    /* renamed from: a */
    public static Receipt m159a(JSONObject jSONObject, String str, String str2) throws C0199a, C0215d, IllegalArgumentException {
        switch (C0240a.m161b(jSONObject)) {
            case V1:
                return C0240a.m164c(jSONObject, str, str2);
            case LEGACY:
                return C0240a.m162b(jSONObject, str, str2);
            default:
                return C0240a.m165d(jSONObject, str, str2);
        }
    }

    /* renamed from: a */
    protected static boolean m160a(String str) {
        return str == null || str.trim().length() == 0;
    }

    /* renamed from: b */
    private static C0242c m161b(JSONObject jSONObject) {
        return !C0243d.m172a(jSONObject.optString("receiptId")) ? C0242c.V2 : C0243d.m172a(jSONObject.optString("DeviceId")) ? C0242c.LEGACY : C0242c.V1;
    }

    /* renamed from: b */
    private static Receipt m162b(JSONObject jSONObject, String str, String str2) throws C0199a, C0215d {
        String optString = jSONObject.optString("signature");
        if (C0243d.m172a(optString)) {
            C0244e.m175b(f104a, "a signature was not found in the receipt for request ID " + str2);
            MetricsHelper.submitReceiptVerificationFailureMetrics(str2, "NO Signature found", optString);
            throw new C0215d(str2, null, optString);
        }
        try {
            Receipt a = C0240a.m158a(jSONObject);
            String str3 = str + "-" + a.getReceiptId();
            boolean isSignedByKiwi = Kiwi.isSignedByKiwi(str3, optString);
            C0244e.m173a(f104a, "stringToVerify/legacy:\n" + str3 + "\nsignature:\n" + optString);
            if (isSignedByKiwi) {
                return a;
            }
            MetricsHelper.submitReceiptVerificationFailureMetrics(str2, str3, optString);
            throw new C0215d(str2, str3, optString);
        } catch (Throwable e) {
            throw new C0199a(str2, jSONObject.toString(), e);
        }
    }

    /* renamed from: b */
    protected static Date m163b(String str) throws JSONException {
        try {
            Date parse = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss").parse(str);
            return 0 == parse.getTime() ? null : parse;
        } catch (ParseException e) {
            throw new JSONException(e.getMessage());
        }
    }

    /* renamed from: c */
    private static Receipt m164c(JSONObject jSONObject, String str, String str2) throws C0199a, C0215d {
        String str3 = null;
        String optString = jSONObject.optString("DeviceId");
        String optString2 = jSONObject.optString("signature");
        if (C0243d.m172a(optString2)) {
            C0244e.m175b(f104a, "a signature was not found in the receipt for request ID " + str2);
            MetricsHelper.submitReceiptVerificationFailureMetrics(str2, "NO Signature found", optString2);
            throw new C0215d(str2, null, optString2);
        }
        try {
            Receipt a = C0240a.m158a(jSONObject);
            ProductType productType = a.getProductType();
            String sku = a.getSku();
            String receiptId = a.getReceiptId();
            Date purchaseDate = ProductType.SUBSCRIPTION == a.getProductType() ? a.getPurchaseDate() : null;
            if (ProductType.SUBSCRIPTION == a.getProductType()) {
                str3 = a.getCancelDate();
            }
            String format = String.format("%s|%s|%s|%s|%s|%s|%s|%tQ|%tQ", new Object[]{PurchasingService.SDK_VERSION, str, optString, productType, sku, receiptId, str2, purchaseDate, str3});
            C0244e.m173a(f104a, "stringToVerify/v1:\n" + format + "\nsignature:\n" + optString2);
            if (Kiwi.isSignedByKiwi(format, optString2)) {
                return a;
            }
            MetricsHelper.submitReceiptVerificationFailureMetrics(str2, format, optString2);
            throw new C0215d(str2, format, optString2);
        } catch (Throwable e) {
            throw new C0199a(str2, jSONObject.toString(), e);
        }
    }

    /* renamed from: d */
    private static Receipt m165d(JSONObject jSONObject, String str, String str2) throws C0199a, C0215d {
        Date date = null;
        String optString = jSONObject.optString("DeviceId");
        String optString2 = jSONObject.optString("signature");
        if (C0243d.m172a(optString2)) {
            C0244e.m175b(f104a, "a signature was not found in the receipt for request ID " + str2);
            MetricsHelper.submitReceiptVerificationFailureMetrics(str2, "NO Signature found", optString2);
            throw new C0215d(str2, null, optString2);
        }
        try {
            String string = jSONObject.getString("receiptId");
            String string2 = jSONObject.getString("sku");
            ProductType valueOf = ProductType.valueOf(jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE).toUpperCase());
            String optString3 = jSONObject.optString("purchaseDate");
            Date b = C0240a.m160a(optString3) ? null : C0240a.m163b(optString3);
            String optString4 = jSONObject.optString("cancelDate");
            if (!C0240a.m160a(optString4)) {
                date = C0240a.m163b(optString4);
            }
            optString3 = String.format("%s|%s|%s|%s|%s|%tQ|%tQ", new Object[]{str, optString, new ReceiptBuilder().setReceiptId(string).setSku(string2).setProductType(valueOf).setPurchaseDate(b).setCancelDate(date).build().getProductType(), new ReceiptBuilder().setReceiptId(string).setSku(string2).setProductType(valueOf).setPurchaseDate(b).setCancelDate(date).build().getSku(), new ReceiptBuilder().setReceiptId(string).setSku(string2).setProductType(valueOf).setPurchaseDate(b).setCancelDate(date).build().getReceiptId(), new ReceiptBuilder().setReceiptId(string).setSku(string2).setProductType(valueOf).setPurchaseDate(b).setCancelDate(date).build().getPurchaseDate(), new ReceiptBuilder().setReceiptId(string).setSku(string2).setProductType(valueOf).setPurchaseDate(b).setCancelDate(date).build().getCancelDate()});
            C0244e.m173a(f104a, "stringToVerify/v2:\n" + optString3 + "\nsignature:\n" + optString2);
            if (Kiwi.isSignedByKiwi(optString3, optString2)) {
                return r0;
            }
            MetricsHelper.submitReceiptVerificationFailureMetrics(str2, optString3, optString2);
            throw new C0215d(str2, optString3, optString2);
        } catch (Throwable e) {
            throw new C0199a(str2, jSONObject.toString(), e);
        }
    }
}
