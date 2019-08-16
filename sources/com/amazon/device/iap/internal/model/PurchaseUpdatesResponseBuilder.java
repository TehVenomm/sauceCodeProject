package com.amazon.device.iap.internal.model;

import com.amazon.device.iap.model.PurchaseUpdatesResponse;
import com.amazon.device.iap.model.PurchaseUpdatesResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.device.iap.model.RequestId;
import com.amazon.device.iap.model.UserData;
import java.util.List;

public class PurchaseUpdatesResponseBuilder {
    private boolean hasMore;
    private List<Receipt> receipts;
    private RequestId requestId;
    private RequestStatus requestStatus;
    private UserData userData;

    public PurchaseUpdatesResponse build() {
        return new PurchaseUpdatesResponse(this);
    }

    public List<Receipt> getReceipts() {
        return this.receipts;
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

    public boolean hasMore() {
        return this.hasMore;
    }

    public PurchaseUpdatesResponseBuilder setHasMore(boolean z) {
        this.hasMore = z;
        return this;
    }

    public PurchaseUpdatesResponseBuilder setReceipts(List<Receipt> list) {
        this.receipts = list;
        return this;
    }

    public PurchaseUpdatesResponseBuilder setRequestId(RequestId requestId2) {
        this.requestId = requestId2;
        return this;
    }

    public PurchaseUpdatesResponseBuilder setRequestStatus(RequestStatus requestStatus2) {
        this.requestStatus = requestStatus2;
        return this;
    }

    public PurchaseUpdatesResponseBuilder setUserData(UserData userData2) {
        this.userData = userData2;
        return this;
    }
}
