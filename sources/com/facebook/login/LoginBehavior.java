package com.facebook.login;

public enum LoginBehavior {
    NATIVE_WITH_FALLBACK(true, true, false, true, true),
    NATIVE_ONLY(true, false, false, false, true),
    WEB_ONLY(false, true, false, true, false),
    WEB_VIEW_ONLY(false, true, false, false, false),
    DEVICE_AUTH(false, false, true, false, false);
    
    private final boolean allowsCustomTabAuth;
    private final boolean allowsDeviceAuth;
    private final boolean allowsFacebookLiteAuth;
    private final boolean allowsKatanaAuth;
    private final boolean allowsWebViewAuth;

    private LoginBehavior(boolean z, boolean z2, boolean z3, boolean z4, boolean z5) {
        this.allowsKatanaAuth = z;
        this.allowsWebViewAuth = z2;
        this.allowsDeviceAuth = z3;
        this.allowsCustomTabAuth = z4;
        this.allowsFacebookLiteAuth = z5;
    }

    boolean allowsCustomTabAuth() {
        return this.allowsCustomTabAuth;
    }

    boolean allowsDeviceAuth() {
        return this.allowsDeviceAuth;
    }

    boolean allowsFacebookLiteAuth() {
        return this.allowsFacebookLiteAuth;
    }

    boolean allowsKatanaAuth() {
        return this.allowsKatanaAuth;
    }

    boolean allowsWebViewAuth() {
        return this.allowsWebViewAuth;
    }
}
