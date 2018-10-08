package com.appsflyer;

import io.fabric.sdk.android.services.common.CommonUtils;
import java.security.MessageDigest;
import java.util.Formatter;

/* renamed from: com.appsflyer.r */
final class C0291r {
    C0291r() {
    }

    /* renamed from: ॱ */
    public static String m348(String str) {
        String str2 = null;
        try {
            MessageDigest instance = MessageDigest.getInstance(CommonUtils.SHA1_INSTANCE);
            instance.reset();
            instance.update(str.getBytes("UTF-8"));
            str2 = C0291r.m346(instance.digest());
        } catch (Throwable e) {
            AFLogger.afErrorLog(new StringBuilder("Error turning ").append(str.substring(0, 6)).append(".. to SHA1").toString(), e);
        }
        return str2;
    }

    /* renamed from: ˏ */
    public static String m347(String str) {
        String str2 = null;
        try {
            MessageDigest instance = MessageDigest.getInstance(CommonUtils.MD5_INSTANCE);
            instance.reset();
            instance.update(str.getBytes("UTF-8"));
            str2 = C0291r.m346(instance.digest());
        } catch (Throwable e) {
            AFLogger.afErrorLog(new StringBuilder("Error turning ").append(str.substring(0, 6)).append(".. to MD5").toString(), e);
        }
        return str2;
    }

    /* renamed from: ˋ */
    public static String m345(String str) {
        String str2 = null;
        try {
            MessageDigest instance = MessageDigest.getInstance("SHA-256");
            instance.update(str.getBytes());
            byte[] digest = instance.digest();
            StringBuffer stringBuffer = new StringBuffer();
            for (byte b : digest) {
                stringBuffer.append(Integer.toString((b & 255) + 256, 16).substring(1));
            }
            str2 = stringBuffer.toString();
        } catch (Throwable e) {
            AFLogger.afErrorLog(new StringBuilder("Error turning ").append(str.substring(0, 6)).append(".. to SHA-256").toString(), e);
        }
        return str2;
    }

    /* renamed from: ˎ */
    private static String m346(byte[] bArr) {
        Formatter formatter = new Formatter();
        int length = bArr.length;
        for (int i = 0; i < length; i++) {
            formatter.format("%02x", new Object[]{Byte.valueOf(bArr[i])});
        }
        String obj = formatter.toString();
        formatter.close();
        return obj;
    }
}
