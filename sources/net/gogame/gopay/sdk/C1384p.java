package net.gogame.gopay.sdk;

import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.content.Context;
import android.os.Build.VERSION;
import android.view.View;
import android.view.View.OnLongClickListener;
import net.gogame.gopay.sdk.support.C1388a;
import net.gogame.gopay.sdk.support.C1389b;
import org.apache.commons.lang3.StringUtils;

/* renamed from: net.gogame.gopay.sdk.p */
final class C1384p implements OnLongClickListener {
    /* renamed from: a */
    final /* synthetic */ StoreActivity f3576a;

    C1384p(StoreActivity storeActivity) {
        this.f3576a = storeActivity;
    }

    public final boolean onLongClick(View view) {
        Context context = this.f3576a;
        try {
            Class cls = Class.forName("net.gogame.gopay.sdk.b");
            Object obj = "Version: " + C1388a.m3914a(cls, "VERSION") + "\nBuild date: " + C1388a.m3914a(cls, "BUILD_DATE") + "\n\nBranch: " + C1388a.m3914a(cls, "GIT_BRANCH") + "\nCommit ID: " + C1388a.m3914a(cls, "GIT_COMMIT_ID_ABBREV") + "\nCommit date: " + C1388a.m3914a(cls, "GIT_COMMIT_DATE") + StringUtils.LF;
            AlertDialog show = new Builder(context).setTitle("Info").setMessage(obj).show();
            if (VERSION.SDK_INT >= 11) {
                show.findViewById(16908290).setOnLongClickListener(new C1389b(obj, context));
            }
        } catch (ClassNotFoundException e) {
        }
        return false;
    }
}
