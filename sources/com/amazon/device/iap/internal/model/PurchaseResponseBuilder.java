package com.amazon.device.iap.internal.model;

import com.amazon.device.iap.model.PurchaseResponse;
import com.amazon.device.iap.model.PurchaseResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.device.iap.model.RequestId;
import com.amazon.device.iap.model.UserData;

public class PurchaseResponseBuilder {
    private Receipt receipt;
    private RequestId requestId;
    private RequestStatus requestStatus;
    private UserData userData;

    public PurchaseResponse build() {
        return new PurchaseResponse(this);
    }

    public Receipt getReceipt() {
        return this.receipt;
    }

    public RequestId getRequestId() {
        return this.requestId;
    }

    public RequestStatus getRequestStatus() {
        return this.requestStatus;
    }

    public UserData getUserData() {
        return this.userData;
    }

    public PurchaseResponseBuilder setReceipt(Receipt receipt2) {
        this.receipt = receipt2;
        return this;
    }

    public PurchaseResponseBuilder setRequestId(RequestId requestId2) {
        this.requestId = requestId2;
        return this;
    }

    public PurchaseResponseBuilder setRequestStatus(RequestStatus requestStatus2) {
        this.requestStatus = requestStatus2;
        return this;
    }

    public PurchaseResponseBuilder setUserData(UserData userData2) {
        this.userData = userData2;
        return this;
    }
}
