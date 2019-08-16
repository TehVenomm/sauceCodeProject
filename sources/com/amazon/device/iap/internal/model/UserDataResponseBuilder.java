package com.amazon.device.iap.internal.model;

import com.amazon.device.iap.model.RequestId;
import com.amazon.device.iap.model.UserData;
import com.amazon.device.iap.model.UserDataResponse;
import com.amazon.device.iap.model.UserDataResponse.RequestStatus;

public class UserDataResponseBuilder {
    private RequestId requestId;
    private RequestStatus requestStatus;
    private UserData userData;

    public UserDataResponse build() {
        return new UserDataResponse(this);
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

    public UserDataResponseBuilder setRequestId(RequestId requestId2) {
        this.requestId = requestId2;
        return this;
    }

    public UserDataResponseBuilder setRequestStatus(RequestStatus requestStatus2) {
        this.requestStatus = requestStatus2;
        return this;
    }

    public UserDataResponseBuilder setUserData(UserData userData2) {
        this.userData = userData2;
        return this;
    }
}
