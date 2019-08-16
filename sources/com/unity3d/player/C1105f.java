package com.unity3d.player;

import android.app.Activity;
import android.app.FragmentTransaction;
import android.content.pm.ActivityInfo;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageItemInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Bundle;
import com.unity3d.plugin.PermissionFragment;
import java.util.LinkedList;

/* renamed from: com.unity3d.player.f */
public final class C1105f implements C1102c {
    /* renamed from: a */
    private static boolean m536a(PackageItemInfo packageItemInfo) {
        try {
            return packageItemInfo.metaData.getBoolean("unityplayer.SkipPermissionsDialog");
        } catch (Exception e) {
            return false;
        }
    }

    /* renamed from: a */
    public final void mo20512a(Activity activity, Runnable runnable) {
        String[] strArr;
        if (activity != null) {
            PackageManager packageManager = activity.getPackageManager();
            try {
                ActivityInfo activityInfo = packageManager.getActivityInfo(activity.getComponentName(), 128);
                ApplicationInfo applicationInfo = packageManager.getApplicationInfo(activity.getPackageName(), 128);
                if (m536a(activityInfo) || m536a(applicationInfo)) {
                    runnable.run();
                    return;
                }
            } catch (Exception e) {
            }
            try {
                PackageInfo packageInfo = packageManager.getPackageInfo(activity.getPackageName(), 4096);
                if (packageInfo.requestedPermissions == null) {
                    packageInfo.requestedPermissions = new String[0];
                }
                LinkedList linkedList = new LinkedList();
                for (String str : packageInfo.requestedPermissions) {
                    try {
                        if (!((packageManager.getPermissionInfo(str, 128).protectionLevel & 1) == 0 || activity.checkCallingOrSelfPermission(str) == 0)) {
                            linkedList.add(str);
                        }
                    } catch (NameNotFoundException e2) {
                        C1104e.Log(5, "Failed to get permission info for " + str + ", manifest likely missing custom permission declaration");
                        C1104e.Log(5, "Permission " + str + " ignored");
                    }
                }
                if (linkedList.isEmpty()) {
                    runnable.run();
                    return;
                }
                C1106g gVar = new C1106g(runnable);
                Bundle bundle = new Bundle();
                bundle.putStringArray(PermissionFragment.PERMISSION_NAMES, (String[]) linkedList.toArray(new String[0]));
                gVar.setArguments(bundle);
                FragmentTransaction beginTransaction = activity.getFragmentManager().beginTransaction();
                beginTransaction.add(0, gVar);
                beginTransaction.commit();
            } catch (Exception e3) {
                C1104e.Log(6, "Unable to query for permission: " + e3.getMessage());
            }
        }
    }
}
