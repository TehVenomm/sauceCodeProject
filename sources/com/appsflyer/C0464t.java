package com.appsflyer;

/* renamed from: com.appsflyer.t */
final class C0464t implements C0465a {

    /* renamed from: ॱ */
    private C0465a f341 = this;

    /* renamed from: com.appsflyer.t$a */
    interface C0465a {
        /* renamed from: ˋ */
        Class<?> mo6633(String str) throws ClassNotFoundException;
    }

    /* renamed from: com.appsflyer.t$c */
    enum C0466c {
        UNITY("android_unity", "com.unity3d.player.UnityPlayer"),
        REACT_NATIVE("android_reactNative", "com.facebook.react.ReactApplication"),
        CORDOVA("android_cordova", "org.apache.cordova.CordovaActivity"),
        SEGMENT("android_segment", "com.segment.analytics.integrations.Integration"),
        COCOS2DX("android_cocos2dx", "org.cocos2dx.lib.Cocos2dxActivity"),
        DEFAULT("android_native", "android_native");
        
        /* access modifiers changed from: private */

        /* renamed from: ʻ */
        public String f349;
        /* access modifiers changed from: private */

        /* renamed from: ॱॱ */
        public String f350;

        private C0466c(String str, String str2) {
            this.f349 = str;
            this.f350 = str2;
        }
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public final String mo6634() {
        C0466c[] values;
        for (C0466c cVar : C0466c.values()) {
            if (mo6635(cVar.f350)) {
                return cVar.f349;
            }
        }
        return C0466c.DEFAULT.f349;
    }

    C0464t() {
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public final boolean mo6635(String str) {
        try {
            this.f341.mo6633(str);
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
    public final Class<?> mo6633(String str) throws ClassNotFoundException {
        return Class.forName(str);
    }
}
