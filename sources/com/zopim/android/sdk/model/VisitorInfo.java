package com.zopim.android.sdk.model;

import android.support.annotation.Nullable;
import java.io.Serializable;

public class VisitorInfo implements Serializable {
    private static final long serialVersionUID = 8250425043423370849L;
    private String email;
    private String name;
    private String phoneNumber;

    public static class Builder {
        String email;
        String name;
        String phoneNumber;

        public VisitorInfo build() {
            return new VisitorInfo();
        }

        public Builder email(String str) {
            this.email = str;
            return this;
        }

        public Builder name(String str) {
            this.name = str;
            return this;
        }

        public Builder phoneNumber(String str) {
            this.phoneNumber = str;
            return this;
        }
    }

    private VisitorInfo() {
    }

    private VisitorInfo(Builder builder) {
        this.name = builder.name;
        this.email = builder.email;
        this.phoneNumber = builder.phoneNumber;
    }

    @Nullable
    public String getEmail() {
        return this.email;
    }

    @Nullable
    public String getName() {
        return this.name;
    }

    @Nullable
    public String getPhoneNumber() {
        return this.phoneNumber;
    }

    public void setEmail(String str) {
        this.email = str;
    }

    public void setName(String str) {
        this.name = str;
    }

    public void setPhoneNumber(String str) {
        this.phoneNumber = str;
    }
}
