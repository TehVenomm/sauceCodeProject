package com.amazon.device.iap.model;

import com.amazon.device.iap.internal.model.UserDataResponseBuilder;
import com.amazon.device.iap.internal.util.C0408d;
import org.json.JSONException;
import org.json.JSONObject;

public final class UserDataResponse {
    private static final String REQUEST_ID = "REQUEST_ID";
    private static final String REQUEST_STATUS = "REQUEST_STATUS";
    private static final String TO_STRING_FORMAT = "(%s, requestId: \"%s\", requestStatus: \"%s\", userData: \"%s\")";
    private static final String USER_DATA = "USER_DATA";
    private final RequestId requestId;
    private final RequestStatus requestStatus;
    private final UserData userData;

    public enum RequestStatus {
        SUCCESSFUL,
        FAILED,
        NOT_SUPPORTED
    }

    public UserDataResponse(UserDataResponseBuilder userDataResponseBuilder) {
        C0408d.m164a((Object) userDataResponseBuilder.getRequestId(), "requestId");
        C0408d.m164a((Object) userDataResponseBuilder.getRequestStatus(), "requestStatus");
        this.requestId = userDataResponseBuilder.getRequestId();
        this.requestStatus = userDataResponseBuilder.getRequestStatus();
        this.userData = userDataResponseBuilder.getUserData();
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
        return jSONObject;
    }

    public String toString() {
        return String.format(TO_STRING_FORMAT, new Object[]{super.toString(), this.requestId, this.requestStatus != null ? this.requestStatus.toString() : "null", this.userData != null ? this.userData.toString() : "null"});
    }
}
