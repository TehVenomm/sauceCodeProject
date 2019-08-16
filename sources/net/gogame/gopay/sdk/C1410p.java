package net.gogame.gopay.sdk;

import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.os.Build.VERSION;
import android.view.View;
import android.view.View.OnLongClickListener;
import net.gogame.gopay.sdk.support.C1638a;
import net.gogame.gopay.sdk.support.C1639b;
import org.apache.commons.lang3.StringUtils;

/* renamed from: net.gogame.gopay.sdk.p */
final class C1410p implements OnLongClickListener {

    /* renamed from: a */
    final /* synthetic */ StoreActivity f1135a;

    C1410p(StoreActivity storeActivity) {
        this.f1135a = storeActivity;
    }

    public final boolean onLongClick(View view) {
        StoreActivity storeActivity = this.f1135a;
        try {
            Class cls = Class.forName("net.gogame.gopay.sdk.b");
            String str = "Version: " + C1638a.m957a(cls, "VERSION") + "\nBuild date: " + C1638a.m957a(cls, "BUILD_DATE") + "\n\nBranch: " + C1638a.m957a(cls, "GIT_BRANCH") + "\nCommit ID: " + C1638a.m957a(cls, "GIT_COMMIT_ID_ABBREV") + "\nCommit date: " + C1638a.m957a(cls, "GIT_COMMIT_DATE") + StringUtils.f1199LF;
            AlertDialog show = new Builder(storeActivity).setTitle("Info").setMessage(str).show();
            if (VERSION.SDK_INT >= 11) {
                show.findViewById(16908290).setOnLongClickListener(new C1639b(str, storeActivity));
            }
        } catch (ClassNotFoundException e) {
        }
        return false;
    }
}
