package net.gogame.gopay.sdk;

import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.content.Context;
import android.os.Build.VERSION;
import android.view.View;
import android.view.View.OnLongClickListener;
import net.gogame.gopay.sdk.support.C1072a;
import net.gogame.gopay.sdk.support.C1073b;
import org.apache.commons.lang3.StringUtils;

/* renamed from: net.gogame.gopay.sdk.p */
final class C1068p implements OnLongClickListener {
    /* renamed from: a */
    final /* synthetic */ StoreActivity f1188a;

    C1068p(StoreActivity storeActivity) {
        this.f1188a = storeActivity;
    }

    public final boolean onLongClick(View view) {
        Context context = this.f1188a;
        try {
            Class cls = Class.forName("net.gogame.gopay.sdk.b");
            Object obj = "Version: " + C1072a.m889a(cls, "VERSION") + "\nBuild date: " + C1072a.m889a(cls, "BUILD_DATE") + "\n\nBranch: " + C1072a.m889a(cls, "GIT_BRANCH") + "\nCommit ID: " + C1072a.m889a(cls, "GIT_COMMIT_ID_ABBREV") + "\nCommit date: " + C1072a.m889a(cls, "GIT_COMMIT_DATE") + StringUtils.LF;
            AlertDialog show = new Builder(context).setTitle("Info").setMessage(obj).show();
            if (VERSION.SDK_INT >= 11) {
                show.findViewById(16908290).setOnLongClickListener(new C1073b(obj, context));
            }
        } catch (ClassNotFoundException e) {
        }
        return false;
    }
}
