package p018jp.colopl.util;

import com.google.firebase.messaging.cpp.SerializedEventUnion;
import java.security.MessageDigest;
import javax.crypto.Cipher;
import javax.crypto.spec.SecretKeySpec;

/* renamed from: jp.colopl.util.Crypto */
public class Crypto {
    private static final String algorithm = "AES";

    static {
        System.loadLibrary("cryptograph");
        System.loadLibrary("gstlockey");
    }

    public static String decrypt(String str) throws Exception {
        Cipher instance = Cipher.getInstance(algorithm);
        instance.init(2, (SecretKeySpec) getKeySpec());
        return new String(instance.doFinal(getByteArray(str)), "UTF-8");
    }

    public static String encrypt(String str) throws Exception {
        Cipher instance = Cipher.getInstance(algorithm);
        instance.init(1, (SecretKeySpec) getKeySpec());
        return getHexString(instance.doFinal(str.getBytes()));
    }

    public static String encryptMD5(String str) throws Exception {
        MessageDigest instance = MessageDigest.getInstance("MD5");
        instance.update(str.getBytes());
        return getHexString(instance.digest());
    }

    private static byte[] getByteArray(String str) {
        int length = str.length();
        byte[] bArr = new byte[(length / 2)];
        for (int i = 0; i < length; i += 2) {
            bArr[i / 2] = (byte) ((byte) ((Character.digit(str.charAt(i), 16) << 4) + Character.digit(str.charAt(i + 1), 16)));
        }
        return bArr;
    }

    public static native String getGuestSeed();

    private static String getHexString(byte[] bArr) throws Exception {
        String str = "";
        for (byte b : bArr) {
            str = str + Integer.toString((b & 255) + SerializedEventUnion.NONE, 16).substring(1);
        }
        return str;
    }

    public static native Object getKeySpec();

    public static String getMD5withSalt(String str, String str2) throws Exception {
        return encryptMD5(str + str2);
    }
}
