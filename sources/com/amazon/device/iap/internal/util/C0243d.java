package com.amazon.device.iap.internal.util;

import java.util.Collection;

/* renamed from: com.amazon.device.iap.internal.util.d */
public class C0243d {
    /* renamed from: a */
    public static void m169a(Object obj, String str) {
        if (obj == null) {
            throw new IllegalArgumentException(str + " must not be null");
        }
    }

    /* renamed from: a */
    public static void m170a(String str, String str2) {
        if (C0243d.m172a(str)) {
            throw new IllegalArgumentException(str2 + " must not be null or empty");
        }
    }

    /* renamed from: a */
    public static void m171a(Collection<? extends Object> collection, String str) {
        if (collection.isEmpty()) {
            throw new IllegalArgumentException(str + " must not be empty");
        }
    }

    /* renamed from: a */
    public static boolean m172a(String str) {
        return str == null || str.trim().length() == 0;
    }
}
