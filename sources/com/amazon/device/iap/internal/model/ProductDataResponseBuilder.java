package com.amazon.device.iap.internal.model;

import com.amazon.device.iap.model.Product;
import com.amazon.device.iap.model.ProductDataResponse;
import com.amazon.device.iap.model.ProductDataResponse.RequestStatus;
import com.amazon.device.iap.model.RequestId;
import java.util.Map;
import java.util.Set;

public class ProductDataResponseBuilder {
    private Map<String, Product> productData;
    private RequestId requestId;
    private RequestStatus requestStatus;
    private Set<String> unavailableSkus;

    public ProductDataResponse build() {
        return new ProductDataResponse(this);
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

    public ProductDataResponseBuilder setProductData(Map<String, Product> map) {
        this.productData = map;
        return this;
    }

    public ProductDataResponseBuilder setRequestId(RequestId requestId) {
        this.requestId = requestId;
        return this;
    }

    public ProductDataResponseBuilder setRequestStatus(RequestStatus requestStatus) {
        this.requestStatus = requestStatus;
        return this;
    }

    public ProductDataResponseBuilder setUnavailableSkus(Set<String> set) {
        this.unavailableSkus = set;
        return this;
    }
}
