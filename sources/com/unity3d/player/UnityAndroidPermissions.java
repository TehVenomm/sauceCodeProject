package com.unity3d.player;

import android.app.Activity;
import android.app.Fragment;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.Intent;
import android.net.Uri;
import android.os.Build.VERSION;
import android.os.Bundle;
import com.google.android.gms.drive.DriveFile;

public class UnityAndroidPermissions {
    private static final int PERMISSIONS_REQUEST_CODE = 15887;

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

    public void RequestPermissionAsync(Activity activity, final String[] strArr, final IPermissionRequestResult iPermissionRequestResult) {
        if (VERSION.SDK_INT >= 23 && activity != null && strArr != null && iPermissionRequestResult != null) {
            final FragmentManager fragmentManager = activity.getFragmentManager();
            Fragment c07301 = new Fragment() {
                public void onCreate(Bundle bundle) {
                    super.onCreate(bundle);
                    if (VERSION.SDK_INT >= 23) {
                        requestPermissions(strArr, UnityAndroidPermissions.PERMISSIONS_REQUEST_CODE);
                    }
                }

                public void onRequestPermissionsResult(int i, String[] strArr, int[] iArr) {
                    if (i == UnityAndroidPermissions.PERMISSIONS_REQUEST_CODE) {
                        int i2 = 0;
                        while (i2 < strArr.length && i2 < iArr.length) {
                            if (iArr[i2] == 0) {
                                iPermissionRequestResult.OnPermissionGranted(strArr[i2]);
                            } else {
                                iPermissionRequestResult.OnPermissionDenied(strArr[i2]);
                            }
                            i2++;
                        }
                        FragmentTransaction beginTransaction = fragmentManager.beginTransaction();
                        beginTransaction.remove(this);
                        beginTransaction.commit();
                    }
                }
            };
            FragmentTransaction beginTransaction = fragmentManager.beginTransaction();
            beginTransaction.add(0, c07301);
            beginTransaction.commit();
        }
    }

    public boolean ShouldShowRequestPermission(Activity activity, String str) {
        return VERSION.SDK_INT < 23 ? true : activity == null ? false : activity.shouldShowRequestPermissionRationale(str);
    }
}
