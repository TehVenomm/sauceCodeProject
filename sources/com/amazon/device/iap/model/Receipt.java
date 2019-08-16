package com.amazon.device.iap.model;

import com.amazon.device.iap.internal.model.ReceiptBuilder;
import com.amazon.device.iap.internal.util.C0408d;
import java.util.Date;
import org.json.JSONException;
import org.json.JSONObject;

public final class Receipt {
    private static final String CANCEL_DATE = "endDate";
    private static final Date DATE_CANCELED = new Date(1);
    private static final String PRODUCT_TYPE = "itemType";
    private static final String PURCHASE_DATE = "purchaseDate";
    private static final String RECEIPT_ID = "receiptId";
    private static final String SKU = "sku";
    private final Date cancelDate;
    private final ProductType productType;
    private final Date purchaseDate;
    private final String receiptId;
    private final String sku;

    public Receipt(ReceiptBuilder receiptBuilder) {
        C0408d.m164a((Object) receiptBuilder.getSku(), SKU);
        C0408d.m164a((Object) receiptBuilder.getProductType(), "productType");
        if (ProductType.SUBSCRIPTION == receiptBuilder.getProductType()) {
            C0408d.m164a((Object) receiptBuilder.getPurchaseDate(), PURCHASE_DATE);
        }
        this.receiptId = receiptBuilder.getReceiptId();
        this.sku = receiptBuilder.getSku();
        this.productType = receiptBuilder.getProductType();
        this.purchaseDate = receiptBuilder.getPurchaseDate();
        this.cancelDate = receiptBuilder.getCancelDate();
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (obj == null || getClass() != obj.getClass()) {
                return false;
            }
            Receipt receipt = (Receipt) obj;
            if (this.cancelDate == null) {
                if (receipt.cancelDate != null) {
                    return false;
                }
            } else if (!this.cancelDate.equals(receipt.cancelDate)) {
                return false;
            }
            if (this.productType != receipt.productType) {
                return false;
            }
            if (this.purchaseDate == null) {
                if (receipt.purchaseDate != null) {
                    return false;
                }
            } else if (!this.purchaseDate.equals(receipt.purchaseDate)) {
                return false;
            }
            if (this.receiptId == null) {
                if (receipt.receiptId != null) {
                    return false;
                }
            } else if (!this.receiptId.equals(receipt.receiptId)) {
                return false;
            }
            if (this.sku == null) {
                if (receipt.sku != null) {
                    return false;
                }
            } else if (!this.sku.equals(receipt.sku)) {
                return false;
            }
        }
        return true;
    }

    public Date getCancelDate() {
        return this.cancelDate;
    }

    public ProductType getProductType() {
        return this.productType;
    }

    public Date getPurchaseDate() {
        return this.purchaseDate;
    }

    public String getReceiptId() {
        return this.receiptId;
    }

    public String getSku() {
        return this.sku;
    }

    public int hashCode() {
        int i = 0;
        int hashCode = this.cancelDate == null ? 0 : this.cancelDate.hashCode();
        int hashCode2 = this.productType == null ? 0 : this.productType.hashCode();
        int hashCode3 = this.purchaseDate == null ? 0 : this.purchaseDate.hashCode();
        int hashCode4 = this.receiptId == null ? 0 : this.receiptId.hashCode();
        if (this.sku != null) {
            i = this.sku.hashCode();
        }
        return ((((((((hashCode + 31) * 31) + hashCode2) * 31) + hashCode3) * 31) + hashCode4) * 31) + i;
    }

    public boolean isCanceled() {
        return this.cancelDate != null;
    }

    public JSONObject toJSON() {
        JSONObject jSONObject = new JSONObject();
        try {
            jSONObject.put(RECEIPT_ID, this.receiptId);
            jSONObject.put(SKU, this.sku);
            jSONObject.put("itemType", this.productType);
            jSONObject.put(PURCHASE_DATE, this.purchaseDate);
            jSONObject.put(CANCEL_DATE, this.cancelDate);
        } catch (JSONException e) {
        }
        return jSONObject;
    }

    public String toString() {
        boolean z = false;
        try {
            return toJSON().toString(4);
        } catch (JSONException e) {
            return z;
        }
    }
}
