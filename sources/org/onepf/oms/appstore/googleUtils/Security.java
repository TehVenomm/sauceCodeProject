package org.onepf.oms.appstore.googleUtils;

import android.text.TextUtils;
import java.security.InvalidKeyException;
import java.security.KeyFactory;
import java.security.NoSuchAlgorithmException;
import java.security.PublicKey;
import java.security.Signature;
import java.security.SignatureException;
import java.security.spec.X509EncodedKeySpec;
import org.jetbrains.annotations.NotNull;
import org.onepf.oms.util.Logger;

public class Security {
    private static final String KEY_FACTORY_ALGORITHM = "RSA";
    private static final String SIGNATURE_ALGORITHM = "SHA1withRSA";

    public static PublicKey generatePublicKey(@NotNull String str) {
        try {
            return KeyFactory.getInstance(KEY_FACTORY_ALGORITHM).generatePublic(new X509EncodedKeySpec(Base64.decode(str)));
        } catch (Throwable e) {
            throw new RuntimeException(e);
        } catch (Throwable e2) {
            Logger.m1002e("Invalid key specification.");
            throw new IllegalArgumentException(e2);
        } catch (Throwable e22) {
            Logger.m1002e("Base64 decoding failed.");
            throw new IllegalArgumentException(e22);
        }
    }

    public static boolean verify(PublicKey publicKey, @NotNull String str, @NotNull String str2) {
        try {
            Signature instance = Signature.getInstance(SIGNATURE_ALGORITHM);
            instance.initVerify(publicKey);
            instance.update(str.getBytes());
            if (instance.verify(Base64.decode(str2))) {
                return true;
            }
            Logger.m1002e("Signature verification failed.");
            return false;
        } catch (NoSuchAlgorithmException e) {
            Logger.m1002e("NoSuchAlgorithmException.");
            return false;
        } catch (InvalidKeyException e2) {
            Logger.m1002e("Invalid key specification.");
            return false;
        } catch (SignatureException e3) {
            Logger.m1002e("Signature exception.");
            return false;
        } catch (Base64DecoderException e4) {
            Logger.m1002e("Base64 decoding failed.");
            return false;
        }
    }

    public static boolean verifyPurchase(@NotNull String str, @NotNull String str2, @NotNull String str3) {
        if (!TextUtils.isEmpty(str2) && !TextUtils.isEmpty(str) && !TextUtils.isEmpty(str3)) {
            return verify(generatePublicKey(str), str2, str3);
        }
        Logger.m1002e("Purchase verification failed: missing data.");
        return false;
    }
}
