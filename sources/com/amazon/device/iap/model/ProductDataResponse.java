package com.amazon.device.iap.model;

import com.amazon.device.iap.internal.model.ProductDataResponseBuilder;
import com.amazon.device.iap.internal.util.C0408d;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import org.json.JSONException;
import org.json.JSONObject;

public class ProductDataResponse {
    private static final String PRODUCT_DATA = "productData";
    private static final String REQUEST_ID = "requestId";
    private static final String REQUEST_STATUS = "requestStatus";
    private static final String TO_STRING_FORMAT = "(%s, requestId: \"%s\", unavailableSkus: %s, requestStatus: \"%s\", productData: %s)";
    private static final String UNAVAILABLE_SKUS = "UNAVAILABLE_SKUS";
    private final Map<String, Product> productData;
    private final RequestId requestId;
    private final RequestStatus requestStatus;
    private final Set<String> unavailableSkus;

    public enum RequestStatus {
        SUCCESSFUL,
        FAILED,
        NOT_SUPPORTED
    }

    public ProductDataResponse(ProductDataResponseBuilder productDataResponseBuilder) {
        C0408d.m164a((Object) productDataResponseBuilder.getRequestId(), REQUEST_ID);
        C0408d.m164a((Object) productDataResponseBuilder.getRequestStatus(), REQUEST_STATUS);
        if (productDataResponseBuilder.getUnavailableSkus() == null) {
            productDataResponseBuilder.setUnavailableSkus(new HashSet());
        }
        if (RequestStatus.SUCCESSFUL == productDataResponseBuilder.getRequestStatus()) {
            C0408d.m164a((Object) productDataResponseBuilder.getProductData(), PRODUCT_DATA);
        }
        this.requestId = productDataResponseBuilder.getRequestId();
        this.requestStatus = productDataResponseBuilder.getRequestStatus();
        this.unavailableSkus = productDataResponseBuilder.getUnavailableSkus();
        this.productData = productDataResponseBuilder.getProductData();
    }

    public Map<String, Product> getProductData() {
        return this.productData;
    }

    public RequestId getRequestId() {
        return this.requestId;
    }

    public RequestStatus getRequestStatus() {
        return this.requestStatus;
    }

    public Set<String> getUnavailableSkus() {
        return this.unavailableSkus;
    }

    public JSONObject toJSON() throws JSONException {
        JSONObject jSONObject = new JSONObject();
        jSONObject.put(REQUEST_ID, this.requestId);
        jSONObject.put(UNAVAILABLE_SKUS, this.unavailableSkus);
        jSONObject.put(REQUEST_STATUS, this.requestStatus);
        JSONObject jSONObject2 = new JSONObject();
        if (this.productData != null) {
            for (String str : this.productData.keySet()) {
                jSONObject2.put(str, ((Product) this.productData.get(str)).toJSON());
            }
        }
        jSONObject.put(PRODUCT_DATA, jSONObject2);
        return jSONObject;
    }

    public String toString() {
        return String.format(TO_STRING_FORMAT, new Object[]{super.toString(), this.requestId, this.unavailableSkus != null ? this.unavailableSkus.toString() : "null", this.requestStatus != null ? this.requestStatus.toString() : "null", this.productData != null ? this.productData.toString() : "null"});
    }
}
