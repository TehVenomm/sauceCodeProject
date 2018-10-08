package com.amazon.device.iap.internal.model;

import com.amazon.device.iap.model.UserData;

public class UserDataBuilder {
    private String marketplace;
    private String userId;

    public UserData build() {
        return new UserData(this);
    }

    public String getMarketplace() {
        return this.marketplace;
    }

    public String getUserId() {
        return this.userId;
    }

    public UserDataBuilder setMarketplace(String str) {
        this.marketplace = str;
        return this;
    }

    public UserDataBuilder setUserId(String str) {
        this.userId = str;
        return this;
    }
}
