package com.google.games.bridge;

import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;

public class TokenResult implements Result {
    private String authCode;
    private String email;
    private String idToken;
    private Status status;

    public TokenResult() {
    }

    TokenResult(String str, String str2, String str3, int i) {
        this.status = new Status(i);
        this.authCode = str;
        this.idToken = str3;
        this.email = str2;
    }

    public String getAuthCode() {
        return this.authCode == null ? "" : this.authCode;
    }

    public String getEmail() {
        return this.email == null ? "" : this.email;
    }

    public String getIdToken() {
        return this.idToken == null ? "" : this.idToken;
    }

    public Status getStatus() {
        return this.status;
    }

    public int getStatusCode() {
        return this.status.getStatusCode();
    }

    public void setAuthCode(String str) {
        this.authCode = str;
    }

    public void setEmail(String str) {
        this.email = str;
    }

    public void setIdToken(String str) {
        this.idToken = str;
    }

    public void setStatus(int i) {
        this.status = new Status(i);
    }

    public String toString() {
        return "Status: " + this.status + " email: " + (this.email == null ? "<null>" : this.email) + " id:" + (this.idToken == null ? "<null>" : this.idToken) + " access: " + (this.authCode == null ? "<null>" : this.authCode);
    }
}
