package net.gogame.gopay.sdk.iab;

import android.content.Context;
import org.onepf.oms.Appstore;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.OpenIabHelper$Options.Builder;
import org.onepf.oms.appstore.GooglePlay;

public final class GoPayHelper {
    /* renamed from: a */
    private static GoGameStore f3380a;
    /* renamed from: b */
    private static boolean f3381b = false;
    /* renamed from: c */
    private static String f3382c;
    /* renamed from: d */
    private static String f3383d;
    /* renamed from: e */
    private static String f3384e;
    /* renamed from: f */
    private static String f3385f;
    /* renamed from: g */
    private static String f3386g;
    /* renamed from: h */
    private static String f3387h;

    private GoPayHelper() {
    }

    public static String getEmail() {
        return f3385f;
    }

    public static String getGameLanguage() {
        return f3386g;
    }

    public static String getGoPayAppId() {
        return f3382c;
    }

    public static String getGoPayAppSecret() {
        return f3383d;
    }

    public static String getGooglePlayPublicKey() {
        return f3387h;
    }

    public static String getGuid() {
        return f3384e;
    }

    public static GoGameStore getStoreInstance() {
        return f3380a;
    }

    public static boolean isDisable3rdParty() {
        return f3381b;
    }

    public static OpenIabHelper newOpenIabHelper(Context context) {
        if (f3381b) {
            return new OpenIabHelper(context, new Builder().addAvailableStoreNames(new String[]{OpenIabHelper.NAME_GOOGLE}).addAvailableStores(new Appstore[]{new GooglePlay(context, f3387h)}).setStoreSearchStrategy(1).build());
        }
        Builder addAvailableStoreNames = new Builder().addAvailableStoreNames(new String[]{"GoGameStore"});
        f3380a = new GoGameStore(context, f3382c, f3384e, f3383d, f3385f, f3386g);
        return new OpenIabHelper(context, addAvailableStoreNames.addAvailableStores(new Appstore[]{r0}).setCheckInventory(false).setStoreSearchStrategy(1).build());
    }

    public static void setDisable3rdParty(boolean z) {
        f3381b = z;
    }

    public static void setEmail(String str) {
        f3385f = str;
    }

    public static void setGameLanguage(String str) {
        f3386g = str;
    }

    public static void setGoPayAppId(String str) {
        f3382c = str;
    }

    public static void setGoPayAppSecret(String str) {
        f3383d = str;
    }

    public static void setGooglePlayPublicKey(String str) {
        f3387h = str;
    }

    public static void setGuid(String str) {
        f3384e = str;
    }
}
