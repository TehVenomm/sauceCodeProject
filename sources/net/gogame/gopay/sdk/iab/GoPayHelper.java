package net.gogame.gopay.sdk.iab;

import android.content.Context;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.OpenIabHelper.Options.Builder;
import org.onepf.oms.appstore.GooglePlay;

public final class GoPayHelper {
    /* renamed from: a */
    private static GoGameStore f992a;
    /* renamed from: b */
    private static boolean f993b = false;
    /* renamed from: c */
    private static String f994c;
    /* renamed from: d */
    private static String f995d;
    /* renamed from: e */
    private static String f996e;
    /* renamed from: f */
    private static String f997f;
    /* renamed from: g */
    private static String f998g;
    /* renamed from: h */
    private static String f999h;

    private GoPayHelper() {
    }

    public static String getEmail() {
        return f997f;
    }

    public static String getGameLanguage() {
        return f998g;
    }

    public static String getGoPayAppId() {
        return f994c;
    }

    public static String getGoPayAppSecret() {
        return f995d;
    }

    public static String getGooglePlayPublicKey() {
        return f999h;
    }

    public static String getGuid() {
        return f996e;
    }

    public static GoGameStore getStoreInstance() {
        return f992a;
    }

    public static boolean isDisable3rdParty() {
        return f993b;
    }

    public static OpenIabHelper newOpenIabHelper(Context context) {
        if (f993b) {
            return new OpenIabHelper(context, new Builder().addAvailableStoreNames(OpenIabHelper.NAME_GOOGLE).addAvailableStores(new GooglePlay(context, f999h)).setStoreSearchStrategy(1).build());
        }
        Builder addAvailableStoreNames = new Builder().addAvailableStoreNames("GoGameStore");
        f992a = new GoGameStore(context, f994c, f996e, f995d, f997f, f998g);
        return new OpenIabHelper(context, addAvailableStoreNames.addAvailableStores(r0).setCheckInventory(false).setStoreSearchStrategy(1).build());
    }

    public static void setDisable3rdParty(boolean z) {
        f993b = z;
    }

    public static void setEmail(String str) {
        f997f = str;
    }

    public static void setGameLanguage(String str) {
        f998g = str;
    }

    public static void setGoPayAppId(String str) {
        f994c = str;
    }

    public static void setGoPayAppSecret(String str) {
        f995d = str;
    }

    public static void setGooglePlayPublicKey(String str) {
        f999h = str;
    }

    public static void setGuid(String str) {
        f996e = str;
    }
}
