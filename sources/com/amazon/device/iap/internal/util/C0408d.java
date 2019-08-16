package com.amazon.device.iap.internal.util;

import java.util.Collection;

/* renamed from: com.amazon.device.iap.internal.util.d */
public class C0408d {
    /* renamed from: a */
    public static void m164a(Object obj, String str) {
        if (obj == null) {
            throw new IllegalArgumentException(str + " must not be null");
        }
    }

    /* renamed from: a */
    public static void m165a(String str, String str2) {
        if (m167a(str)) {
            throw new IllegalArgumentException(str2 + " must not be null or empty");
        }
    }

    /* renamed from: a */
    public static void m166a(Collection<? extends Object> collection, String str) {
        if (collection.isEmpty()) {
            throw new IllegalArgumentException(str + " must not be empty");
        }
    }

    /* renamed from: a */
    public static boolean m167a(String str) {
        return str == null || str.trim().length() == 0;
    }
}
