package com.amazon.device.iap.model;

import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.util.C0243d;
import org.json.JSONException;
import org.json.JSONObject;

public final class PurchaseResponse {
    private static final String RECEIPT = "receipt";
    private static final String REQUEST_ID = "requestId";
    private static final String REQUEST_STATUS = "requestStatus";
    private static final String TO_STRING_FORMAT = "(%s, requestId: \"%s\", purchaseRequestStatus: \"%s\", userId: \"%s\", receipt: %s)";
    private static final String USER_DATA = "userData";
    private final Receipt receipt;
    private final RequestId requestId;
    private final RequestStatus requestStatus;
    private final UserData userData;

    public enum RequestStatus {
        SUCCESSFUL,
        FAILED,
        INVALID_SKU,
        ALREADY_PURCHASED,
        NOT_SUPPORTED;

        public static RequestStatus safeValueOf(String str) {
            return C0243d.m172a(str) ? null : "ALREADY_ENTITLED".equalsIgnoreCase(str) ? ALREADY_PURCHASED : valueOf(str.toUpperCase());
        }
    }

    public PurchaseResponse(PurchaseResponseBuilder purchaseResponseBuilder) {
        C0243d.m169a(purchaseResponseBuilder.getRequestId(), REQUEST_ID);
        C0243d.m169a(purchaseResponseBuilder.getRequestStatus(), REQUEST_STATUS);
        if (purchaseResponseBuilder.getRequestStatus() == RequestStatus.SUCCESSFUL) {
            C0243d.m169a(purchaseResponseBuilder.getReceipt(), RECEIPT);
            C0243d.m169a(purchaseResponseBuilder.getUserData(), USER_DATA);
        }
        this.requestId = purchaseResponseBuilder.getRequestId();
        this.userData = purchaseResponseBuilder.getUserData();
        this.receipt = purchaseResponseBuilder.getReceipt();
        this.requestStatus = purchaseResponseBuilder.getRequestStatus();
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

    public JSONObject toJSON() throws JSONException {
        JSONObject jSONObject = new JSONObject();
        jSONObject.put(REQUEST_ID, this.requestId);
        jSONObject.put(REQUEST_STATUS, this.requestStatus);
        jSONObject.put(USER_DATA, this.userData != null ? this.userData.toJSON() : "");
        jSONObject.put(RECEIPT, getReceipt() != null ? getReceipt().toJSON() : "");
        return jSONObject;
    }

    public String toString() {
        String obj = super.toString();
        RequestId requestId = this.requestId;
        String requestStatus = this.requestStatus != null ? this.requestStatus.toString() : "null";
        return String.format(TO_STRING_FORMAT, new Object[]{obj, requestId, requestStatus, this.userData, this.receipt});
    }
}
