package com.unity3d.plugin;

import android.app.Activity;
import android.app.Fragment;
import android.app.FragmentTransaction;
import android.content.Intent;
import android.net.Uri;
import android.os.Build.VERSION;
import android.os.Bundle;
import com.google.android.gms.drive.DriveFile;

public class UnityAndroidPermissions {

    interface IPermissionRequestResult {
        void OnPermissionDenied(String str);

        void OnPermissionGranted(String str);
    }

    public boolean IsPermissionGranted(Activity activity, String str) {
        if (VERSION.SDK_INT >= 23) {
            if (activity == null) {
                return false;
            }
            if (activity.checkSelfPermission(str) != 0) {
                return false;
            }
        }
        return true;
    }

    public void OpenAppSetting(Activity activity) {
        Intent intent = new Intent("android.settings.APPLICATION_DETAILS_SETTINGS", Uri.fromParts("package", activity.getPackageName(), null));
        intent.addCategory("android.intent.category.DEFAULT");
        intent.setFlags(DriveFile.MODE_READ_ONLY);
        activity.startActivity(intent);
    }

    public void RequestPermissionAsync(Activity activity, String[] strArr, IPermissionRequestResult iPermissionRequestResult) {
        if (VERSION.SDK_INT >= 23 && activity != null && strArr != null && iPermissionRequestResult != null) {
            Fragment permissionFragment = new PermissionFragment(iPermissionRequestResult);
            Bundle bundle = new Bundle();
            bundle.putStringArray(PermissionFragment.PERMISSION_NAMES, strArr);
            permissionFragment.setArguments(bundle);
            FragmentTransaction beginTransaction = activity.getFragmentManager().beginTransaction();
            beginTransaction.add(0, permissionFragment);
            beginTransaction.commit();
        }
    }

    public boolean ShouldShowRequestPermission(Activity activity, String str) {
        return VERSION.SDK_INT < 23 ? true : activity == null ? false : activity.shouldShowRequestPermissionRationale(str);
    }
}
