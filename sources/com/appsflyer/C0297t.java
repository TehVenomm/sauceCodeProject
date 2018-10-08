package com.appsflyer;

/* renamed from: com.appsflyer.t */
final class C0297t implements C0295a {
    /* renamed from: ॱ */
    private C0295a f329 = this;

    /* renamed from: com.appsflyer.t$a */
    interface C0295a {
        /* renamed from: ˋ */
        Class<?> mo1220(String str) throws ClassNotFoundException;
    }

    /* renamed from: com.appsflyer.t$c */
    enum C0296c {
        UNITY("android_unity", "com.unity3d.player.UnityPlayer"),
        REACT_NATIVE("android_reactNative", "com.facebook.react.ReactApplication"),
        CORDOVA("android_cordova", "org.apache.cordova.CordovaActivity"),
        SEGMENT("android_segment", "com.segment.analytics.integrations.Integration"),
        COCOS2DX("android_cocos2dx", "org.cocos2dx.lib.Cocos2dxActivity"),
        DEFAULT("android_native", "android_native");
        
        /* renamed from: ʻ */
        private String f327;
        /* renamed from: ॱॱ */
        private String f328;

        private C0296c(String str, String str2) {
            this.f327 = str;
            this.f328 = str2;
        }
    }

    /* renamed from: ˎ */
    final String m367() {
        for (C0296c c0296c : C0296c.values()) {
            if (m368(c0296c.f328)) {
                return c0296c.f327;
            }
        }
        return C0296c.DEFAULT.f327;
    }

    C0297t() {
    }

    /* renamed from: ˎ */
    final boolean m368(String str) {
        try {
            this.f329.mo1220(str);
            AFLogger.afRDLog(new StringBuilder("Class: ").append(str).append(" is found.").toString());
            return true;
        } catch (ClassNotFoundException e) {
            return false;
        } catch (Throwable th) {
            AFLogger.afErrorLog(th.getMessage(), th);
            return false;
        }
    }

    /* renamed from: ˋ */
    public final Class<?> mo1220(String str) throws ClassNotFoundException {
        return Class.forName(str);
    }
}
