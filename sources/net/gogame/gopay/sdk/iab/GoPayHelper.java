package net.gogame.gopay.sdk.iab;

import android.content.Context;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.OpenIabHelper.Options.Builder;
import org.onepf.oms.appstore.GooglePlay;

public final class GoPayHelper {

    /* renamed from: a */
    private static GoGameStore f1211a;

    /* renamed from: b */
    private static boolean f1212b = false;

    /* renamed from: c */
    private static String f1213c;

    /* renamed from: d */
    private static String f1214d;

    /* renamed from: e */
    private static String f1215e;

    /* renamed from: f */
    private static String f1216f;

    /* renamed from: g */
    private static String f1217g;

    /* renamed from: h */
    private static String f1218h;

    private GoPayHelper() {
    }

    public static String getEmail() {
        return f1216f;
    }

    public static String getGameLanguage() {
        return f1217g;
    }

    public static String getGoPayAppId() {
        return f1213c;
    }

    public static String getGoPayAppSecret() {
        return f1214d;
    }

    public static String getGooglePlayPublicKey() {
        return f1218h;
    }

    public static String getGuid() {
        return f1215e;
    }

    public static GoGameStore getStoreInstance() {
        return f1211a;
    }

    public static boolean isDisable3rdParty() {
        return f1212b;
    }

    public static OpenIabHelper newOpenIabHelper(Context context) {
        if (f1212b) {
            return new OpenIabHelper(context, new Builder().addAvailableStoreNames(OpenIabHelper.NAME_GOOGLE).addAvailableStores(new GooglePlay(context, f1218h)).setStoreSearchStrategy(1).build());
        }
        Builder addAvailableStoreNames = new Builder().addAvailableStoreNames("GoGameStore");
        GoGameStore goGameStore = new GoGameStore(context, f1213c, f1215e, f1214d, f1216f, f1217g);
        f1211a = goGameStore;
        return new OpenIabHelper(context, addAvailableStoreNames.addAvailableStores(goGameStore).setCheckInventory(false).setStoreSearchStrategy(1).build());
    }

    public static void setDisable3rdParty(boolean z) {
        f1212b = z;
    }

    public static void setEmail(String str) {
        f1216f = str;
    }

    public static void setGameLanguage(String str) {
        f1217g = str;
    }

    public static void setGoPayAppId(String str) {
        f1213c = str;
    }

    public static void setGoPayAppSecret(String str) {
        f1214d = str;
    }

    public static void setGooglePlayPublicKey(String str) {
        f1218h = str;
    }

    public static void setGuid(String str) {
        f1215e = str;
    }
}
