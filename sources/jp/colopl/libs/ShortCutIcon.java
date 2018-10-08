package jp.colopl.libs;

import android.content.Context;
import android.content.Intent;
import android.content.Intent.ShortcutIconResource;
import android.net.Uri;
import com.facebook.internal.NativeProtocol;
import com.google.android.gms.drive.DriveFile;
import jp.colopl.drapro.StartActivity;
import net.gogame.gowrap.InternalConstants;

public class ShortCutIcon {
    public static final String DEFAULT_ACTIVITY = "StartActivity";

    public static void createAppShortCutIcon(Context context) {
        String packageName = context.getPackageName();
        createShortCutIcon(context, createMainIntent(context, packageName, DEFAULT_ACTIVITY), context.getString(context.getResources().getIdentifier(NativeProtocol.BRIDGE_ARG_APP_NAME_STRING, "string", context.getPackageName())), context.getResources().getIdentifier("app_icon", "drawable", context.getPackageName()));
    }

    private static Intent createMainIntent(Context context, String str, String str2) {
        Intent intent = new Intent(context, StartActivity.class);
        intent.setAction("android.intent.action.MAIN");
        intent.addFlags(DriveFile.MODE_READ_ONLY);
        intent.addFlags(InternalConstants.DISKLRUCACHE_MAXSIZE);
        return intent;
    }

    public static void createShortCutIcon(Context context, Intent intent, String str, int i) {
        Intent intent2 = new Intent();
        intent2.putExtra("android.intent.extra.shortcut.INTENT", intent);
        intent2.putExtra("android.intent.extra.shortcut.NAME", str);
        intent2.putExtra("android.intent.extra.shortcut.ICON_RESOURCE", ShortcutIconResource.fromContext(context, i));
        intent2.setAction("com.android.launcher.action.INSTALL_SHORTCUT");
        context.sendBroadcast(intent2);
    }

    public static void createShortCutIcon(Context context, String str, String str2, int i) {
        createShortCutIcon(context, createShortCutIntent(context, context.getPackageName(), DEFAULT_ACTIVITY, str2), str, i);
    }

    private static Intent createShortCutIntent(Context context, String str, String str2, String str3) {
        if (str3 == null) {
            return null;
        }
        Intent intent = new Intent(context, StartActivity.class);
        intent.setAction("android.intent.action.VIEW");
        intent.addFlags(DriveFile.MODE_READ_ONLY);
        intent.addFlags(InternalConstants.DISKLRUCACHE_MAXSIZE);
        intent.setData(Uri.parse(str3));
        return intent;
    }
}
