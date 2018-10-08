package com.amazon.device.iap.model;

import com.amazon.device.iap.internal.model.PurchaseUpdatesResponseBuilder;
import com.amazon.device.iap.internal.util.C0243d;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public final class PurchaseUpdatesResponse {
    private static final String HAS_MORE = "HAS_MORE";
    private static final String RECEIPTS = "RECEIPTS";
    private static final String REQUEST_ID = "REQUEST_ID";
    private static final String REQUEST_STATUS = "REQUEST_STATUS";
    private static final String TO_STRING_FORMAT = "(%s, requestId: \"%s\", requestStatus: \"%s\", userData: \"%s\", receipts: %s, hasMore: \"%b\")";
    private static final String USER_DATA = "USER_DATA";
    private final boolean hasMore;
    private final List<Receipt> receipts;
    private final RequestId requestId;
    private final RequestStatus requestStatus;
    private final UserData userData;

    public enum RequestStatus {
        SUCCESSFUL,
        FAILED,
        NOT_SUPPORTED
    }

    public PurchaseUpdatesResponse(PurchaseUpdatesResponseBuilder purchaseUpdatesResponseBuilder) {
        C0243d.m169a(purchaseUpdatesResponseBuilder.getRequestId(), "requestId");
        C0243d.m169a(purchaseUpdatesResponseBuilder.getRequestStatus(), "requestStatus");
        if (RequestStatus.SUCCESSFUL == purchaseUpdatesResponseBuilder.getRequestStatus()) {
            C0243d.m169a(purchaseUpdatesResponseBuilder.getUserData(), "userData");
            C0243d.m169a(purchaseUpdatesResponseBuilder.getReceipts(), "receipts");
        }
        this.requestId = purchaseUpdatesResponseBuilder.getRequestId();
        this.requestStatus = purchaseUpdatesResponseBuilder.getRequestStatus();
        this.userData = purchaseUpdatesResponseBuilder.getUserData();
        this.receipts = purchaseUpdatesResponseBuilder.getReceipts() == null ? new ArrayList() : purchaseUpdatesResponseBuilder.getReceipts();
        this.hasMore = purchaseUpdatesResponseBuilder.hasMore();
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

    public JSONObject toJSON() throws JSONException {
        JSONObject jSONObject = new JSONObject();
        jSONObject.put(REQUEST_ID, this.requestId);
        jSONObject.put(REQUEST_STATUS, this.requestStatus);
        jSONObject.put(USER_DATA, this.userData != null ? this.userData.toJSON() : "");
        JSONArray jSONArray = new JSONArray();
        if (this.receipts != null) {
            for (Receipt toJSON : this.receipts) {
                jSONArray.put(toJSON.toJSON());
            }
        }
        jSONObject.put(RECEIPTS, jSONArray);
        jSONObject.put(HAS_MORE, this.hasMore);
        return jSONObject;
    }

    public String toString() {
        String obj = super.toString();
        RequestId requestId = this.requestId;
        RequestStatus requestStatus = this.requestStatus;
        UserData userData = this.userData;
        String arrays = this.receipts != null ? Arrays.toString(this.receipts.toArray()) : "null";
        return String.format(TO_STRING_FORMAT, new Object[]{obj, requestId, requestStatus, userData, arrays, Boolean.valueOf(this.hasMore)});
    }
}
