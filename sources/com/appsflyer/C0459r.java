package com.appsflyer;

import com.google.firebase.messaging.cpp.SerializedEventUnion;
import java.security.MessageDigest;
import java.util.Formatter;
import p017io.fabric.sdk.android.services.common.CommonUtils;

/* renamed from: com.appsflyer.r */
final class C0459r {
    C0459r() {
    }

    /* renamed from: ॱ */
    public static String m341(String str) {
        boolean z = false;
        try {
            MessageDigest instance = MessageDigest.getInstance(CommonUtils.SHA1_INSTANCE);
            instance.reset();
            instance.update(str.getBytes("UTF-8"));
            return m339(instance.digest());
        } catch (Exception e) {
            AFLogger.afErrorLog(new StringBuilder("Error turning ").append(str.substring(0, 6)).append(".. to SHA1").toString(), e);
            return z;
        }
    }

    /* renamed from: ˏ */
    public static String m340(String str) {
        boolean z = false;
        try {
            MessageDigest instance = MessageDigest.getInstance("MD5");
            instance.reset();
            instance.update(str.getBytes("UTF-8"));
            return m339(instance.digest());
        } catch (Exception e) {
            AFLogger.afErrorLog(new StringBuilder("Error turning ").append(str.substring(0, 6)).append(".. to MD5").toString(), e);
            return z;
        }
    }

    /* renamed from: ˋ */
    public static String m338(String str) {
        try {
            MessageDigest instance = MessageDigest.getInstance(CommonUtils.SHA256_INSTANCE);
            instance.update(str.getBytes());
            byte[] digest = instance.digest();
            StringBuffer stringBuffer = new StringBuffer();
            for (byte b : digest) {
                stringBuffer.append(Integer.toString((b & 255) + SerializedEventUnion.NONE, 16).substring(1));
            }
            return stringBuffer.toString();
        } catch (Exception e) {
            AFLogger.afErrorLog(new StringBuilder("Error turning ").append(str.substring(0, 6)).append(".. to SHA-256").toString(), e);
            return null;
        }
    }

    /* renamed from: ˎ */
    private static String m339(byte[] bArr) {
        Formatter formatter = new Formatter();
        for (byte valueOf : bArr) {
            formatter.format("%02x", new Object[]{Byte.valueOf(valueOf)});
        }
        String obj = formatter.toString();
        formatter.close();
        return obj;
    }
}
