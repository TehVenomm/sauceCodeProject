package com.unity3d.plugin;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;

public class PermissionFragment extends Fragment {
    private static final int PERMISSIONS_REQUEST_CODE = 15887;
    public static final String PERMISSION_NAMES = "PermissionNames";
    private final IPermissionRequestResult m_ResultCallbacks;

    public PermissionFragment() {
        this.m_ResultCallbacks = null;
    }

    public PermissionFragment(IPermissionRequestResult iPermissionRequestResult) {
        this.m_ResultCallbacks = iPermissionRequestResult;
    }

    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        if (this.m_ResultCallbacks == null) {
            getFragmentManager().beginTransaction().remove(this).commit();
        } else {
            requestPermissions(getArguments().getStringArray(PERMISSION_NAMES), PERMISSIONS_REQUEST_CODE);
        }
    }

    public void onRequestPermissionsResult(int i, String[] strArr, int[] iArr) {
        if (i == PERMISSIONS_REQUEST_CODE) {
            int i2 = 0;
            while (i2 < strArr.length && i2 < iArr.length) {
                if (iArr[i2] == 0) {
                    this.m_ResultCallbacks.OnPermissionGranted(strArr[i2]);
                } else {
                    this.m_ResultCallbacks.OnPermissionDenied(strArr[i2]);
                }
                i2++;
            }
            FragmentTransaction beginTransaction = getFragmentManager().beginTransaction();
            beginTransaction.remove(this);
            beginTransaction.commit();
        }
    }
}
