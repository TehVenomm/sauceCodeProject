package com.amazon.device.iap.internal.util;

import com.amazon.android.Kiwi;
import com.amazon.device.iap.PurchasingService;
import com.amazon.device.iap.internal.model.ReceiptBuilder;
import com.amazon.device.iap.internal.p005b.C0357a;
import com.amazon.device.iap.internal.p005b.C0373d;
import com.amazon.device.iap.model.ProductType;
import com.amazon.device.iap.model.Receipt;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.util.a */
public class C0404a {

    /* renamed from: a */
    private static final String f122a = C0404a.class.getSimpleName();

    /* renamed from: a */
    private static Receipt m153a(JSONObject jSONObject) throws JSONException {
        Date date = null;
        String optString = jSONObject.optString("token");
        String string = jSONObject.getString("sku");
        ProductType valueOf = ProductType.valueOf(jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE).toUpperCase());
        String optString2 = jSONObject.optString("startDate");
        Date b = m155a(optString2) ? null : m158b(optString2);
        String optString3 = jSONObject.optString("endDate");
        if (!m155a(optString3)) {
            date = m158b(optString3);
        }
        return new ReceiptBuilder().setReceiptId(optString).setSku(string).setProductType(valueOf).setPurchaseDate(b).setCancelDate(date).build();
    }

    /* renamed from: a */
    public static Receipt m154a(JSONObject jSONObject, String str, String str2) throws C0357a, C0373d, IllegalArgumentException {
        switch (m156b(jSONObject)) {
            case V1:
                return m159c(jSONObject, str, str2);
            case LEGACY:
                return m157b(jSONObject, str, str2);
            default:
                return m160d(jSONObject, str, str2);
        }
    }

    /* renamed from: a */
    protected static boolean m155a(String str) {
        return str == null || str.trim().length() == 0;
    }

    /* renamed from: b */
    private static C0407c m156b(JSONObject jSONObject) {
        return !C0408d.m167a(jSONObject.optString("receiptId")) ? C0407c.V2 : C0408d.m167a(jSONObject.optString("DeviceId")) ? C0407c.LEGACY : C0407c.V1;
    }

    /* renamed from: b */
    private static Receipt m157b(JSONObject jSONObject, String str, String str2) throws C0357a, C0373d {
        String optString = jSONObject.optString("signature");
        if (C0408d.m167a(optString)) {
            C0409e.m170b(f122a, "a signature was not found in the receipt for request ID " + str2);
            MetricsHelper.submitReceiptVerificationFailureMetrics(str2, "NO Signature found", optString);
            throw new C0373d(str2, null, optString);
        }
        try {
            Receipt a = m153a(jSONObject);
            String str3 = str + "-" + a.getReceiptId();
            boolean isSignedByKiwi = Kiwi.isSignedByKiwi(str3, optString);
            C0409e.m168a(f122a, "stringToVerify/legacy:\n" + str3 + "\nsignature:\n" + optString);
            if (isSignedByKiwi) {
                return a;
            }
            MetricsHelper.submitReceiptVerificationFailureMetrics(str2, str3, optString);
            throw new C0373d(str2, str3, optString);
        } catch (JSONException e) {
            throw new C0357a(str2, jSONObject.toString(), e);
        }
    }

    /* renamed from: b */
    protected static Date m158b(String str) throws JSONException {
        try {
            Date parse = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss").parse(str);
            if (0 == parse.getTime()) {
                return null;
            }
            return parse;
        } catch (ParseException e) {
            throw new JSONException(e.getMessage());
        }
    }

    /* renamed from: c */
    private static Receipt m159c(JSONObject jSONObject, String str, String str2) throws C0357a, C0373d {
        Date date = null;
        String optString = jSONObject.optString("DeviceId");
        String optString2 = jSONObject.optString("signature");
        if (C0408d.m167a(optString2)) {
            C0409e.m170b(f122a, "a signature was not found in the receipt for request ID " + str2);
            MetricsHelper.submitReceiptVerificationFailureMetrics(str2, "NO Signature found", optString2);
            throw new C0373d(str2, null, optString2);
        }
        try {
            Receipt a = m153a(jSONObject);
            ProductType productType = a.getProductType();
            String sku = a.getSku();
            String receiptId = a.getReceiptId();
            Date date2 = ProductType.SUBSCRIPTION == a.getProductType() ? a.getPurchaseDate() : null;
            if (ProductType.SUBSCRIPTION == a.getProductType()) {
                date = a.getCancelDate();
            }
            String format = String.format("%s|%s|%s|%s|%s|%s|%s|%tQ|%tQ", new Object[]{PurchasingService.SDK_VERSION, str, optString, productType, sku, receiptId, str2, date2, date});
            C0409e.m168a(f122a, "stringToVerify/v1:\n" + format + "\nsignature:\n" + optString2);
            if (Kiwi.isSignedByKiwi(format, optString2)) {
                return a;
            }
            MetricsHelper.submitReceiptVerificationFailureMetrics(str2, format, optString2);
            throw new C0373d(str2, format, optString2);
        } catch (JSONException e) {
            throw new C0357a(str2, jSONObject.toString(), e);
        }
    }

    /* renamed from: d */
    private static Receipt m160d(JSONObject jSONObject, String str, String str2) throws C0357a, C0373d {
        Date date = null;
        String optString = jSONObject.optString("DeviceId");
        String optString2 = jSONObject.optString("signature");
        if (C0408d.m167a(optString2)) {
            C0409e.m170b(f122a, "a signature was not found in the receipt for request ID " + str2);
            MetricsHelper.submitReceiptVerificationFailureMetrics(str2, "NO Signature found", optString2);
            throw new C0373d(str2, null, optString2);
        }
        try {
            String string = jSONObject.getString("receiptId");
            String string2 = jSONObject.getString("sku");
            ProductType valueOf = ProductType.valueOf(jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE).toUpperCase());
            String optString3 = jSONObject.optString("purchaseDate");
            Date b = m155a(optString3) ? null : m158b(optString3);
            String optString4 = jSONObject.optString("cancelDate");
            if (!m155a(optString4)) {
                date = m158b(optString4);
            }
            Receipt build = new ReceiptBuilder().setReceiptId(string).setSku(string2).setProductType(valueOf).setPurchaseDate(b).setCancelDate(date).build();
            String format = String.format("%s|%s|%s|%s|%s|%tQ|%tQ", new Object[]{str, optString, build.getProductType(), build.getSku(), build.getReceiptId(), build.getPurchaseDate(), build.getCancelDate()});
            C0409e.m168a(f122a, "stringToVerify/v2:\n" + format + "\nsignature:\n" + optString2);
            if (Kiwi.isSignedByKiwi(format, optString2)) {
                return build;
            }
            MetricsHelper.submitReceiptVerificationFailureMetrics(str2, format, optString2);
            throw new C0373d(str2, format, optString2);
        } catch (JSONException e) {
            throw new C0357a(str2, jSONObject.toString(), e);
        }
    }
}
