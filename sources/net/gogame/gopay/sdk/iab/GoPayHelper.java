package net.gogame.gopay.sdk.iab;

import android.content.Context;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.OpenIabHelper.Options.Builder;
import org.onepf.oms.appstore.GooglePlay;

public final class GoPayHelper {

    /* renamed from: a */
    private static GoGameStore f1223a;

    /* renamed from: b */
    private static boolean f1224b = false;

    /* renamed from: c */
    private static String f1225c;

    /* renamed from: d */
    private static String f1226d;

    /* renamed from: e */
    private static String f1227e;

    /* renamed from: f */
    private static String f1228f;

    /* renamed from: g */
    private static String f1229g;

    /* renamed from: h */
    private static String f1230h;

    private GoPayHelper() {
    }

    public static String getEmail() {
        return f1228f;
    }

    public static String getGameLanguage() {
        return f1229g;
    }

    public static String getGoPayAppId() {
        return f1225c;
    }

    public static String getGoPayAppSecret() {
        return f1226d;
    }

    public static String getGooglePlayPublicKey() {
        return f1230h;
    }

    public static String getGuid() {
        return f1227e;
    }

    public static GoGameStore getStoreInstance() {
        return f1223a;
    }

    public static boolean isDisable3rdParty() {
        return f1224b;
    }

    public static OpenIabHelper newOpenIabHelper(Context context) {
        if (f1224b) {
            return new OpenIabHelper(context, new Builder().addAvailableStoreNames(OpenIabHelper.NAME_GOOGLE).addAvailableStores(new GooglePlay(context, f1230h)).setStoreSearchStrategy(1).build());
        }
        Builder addAvailableStoreNames = new Builder().addAvailableStoreNames("GoGameStore");
        GoGameStore goGameStore = new GoGameStore(context, f1225c, f1227e, f1226d, f1228f, f1229g);
        f1223a = goGameStore;
        return new OpenIabHelper(context, addAvailableStoreNames.addAvailableStores(goGameStore).setCheckInventory(false).setStoreSearchStrategy(1).build());
    }

    public static void setDisable3rdParty(boolean z) {
        f1224b = z;
    }

    public static void setEmail(String str) {
        f1228f = str;
    }

    public static void setGameLanguage(String str) {
        f1229g = str;
    }

    public static void setGoPayAppId(String str) {
        f1225c = str;
    }

    public static void setGoPayAppSecret(String str) {
        f1226d = str;
    }

    public static void setGooglePlayPublicKey(String str) {
        f1230h = str;
    }

    public static void setGuid(String str) {
        f1227e = str;
    }
}
