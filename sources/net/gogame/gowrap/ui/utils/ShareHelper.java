package net.gogame.gowrap.p019ui.utils;

import android.content.Context;
import android.content.Intent;
import net.gogame.gowrap.p019ui.common.C1685R;

/* renamed from: net.gogame.gowrap.ui.utils.ShareHelper */
public class ShareHelper {
    public static void share(Context context, String str) {
        Intent intent = new Intent();
        intent.setAction("android.intent.action.SEND");
        intent.putExtra("android.intent.extra.TEXT", str);
        intent.setType("text/plain");
        context.startActivity(Intent.createChooser(intent, context.getResources().getString(C1685R.string.net_gogame_gowrap_share_prompt)));
    }
}
