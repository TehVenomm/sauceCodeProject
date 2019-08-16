package p018jp.colopl.drapro;

import android.app.Activity;
import android.content.Context;
import p018jp.colopl.util.Util;

/* renamed from: jp.colopl.drapro.AppConsts */
public final class AppConsts {
    public static final String GATrackingID = "drapro";
    public static final String SHOPITEM_PREFIX = "net.gogame.dragon.";
    public static Context appContext;
    public static String[] itemCodeId;
    public static String[] itemNameId;
    public static String versionName = "";

    static {
        System.loadLibrary("appconsts");
    }

    public static void SetUpShopItem(Activity activity) {
        int intValue = Integer.valueOf(activity.getResources().getString(activity.getResources().getIdentifier("ShopItemCount", "string", activity.getPackageName()))).intValue();
        itemNameId = new String[intValue];
        itemCodeId = new String[intValue];
        for (int i = 0; i < intValue; i++) {
            itemNameId[i] = activity.getResources().getString(activity.getResources().getIdentifier("ShopItemName" + i, "string", activity.getPackageName()));
            itemCodeId[i] = activity.getResources().getString(activity.getResources().getIdentifier("ShopItem" + i, "string", activity.getPackageName()));
        }
    }

    public static native String getAppLicenseKey();

    public static String getProductNameById(String str, Activity activity) {
        boolean z = false;
        int i = 0;
        while (i < itemCodeId.length) {
            try {
                if (itemCodeId[i].equalsIgnoreCase(str)) {
                    StringBuilder sb = new StringBuilder();
                    if (activity == null) {
                        z = true;
                    }
                    Util.dLog(null, sb.append(z).append("  ").append(str).toString());
                    return itemNameId[i];
                }
                i++;
            } catch (Exception e) {
                Util.eLog(null, e.toString());
                return "";
            }
        }
        return "";
    }
}
