package com.unity3d.player;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;
import com.unity3d.plugin.PermissionFragment;

/* renamed from: com.unity3d.player.g */
public final class C1106g extends Fragment {

    /* renamed from: a */
    private final Runnable f579a;

    public C1106g() {
        this.f579a = null;
    }

    public C1106g(Runnable runnable) {
        this.f579a = runnable;
    }

    public final void onActivityCreated(Bundle bundle) {
        super.onActivityCreated(bundle);
        if (this.f579a == null) {
            getFragmentManager().beginTransaction().remove(this).commit();
        } else {
            requestPermissions(getArguments().getStringArray(PermissionFragment.PERMISSION_NAMES), 15881);
        }
    }

    public final void onRequestPermissionsResult(int i, String[] strArr, int[] iArr) {
        if (i == 15881) {
            int i2 = 0;
            while (i2 < strArr.length && i2 < iArr.length) {
                C1104e.Log(4, strArr[i2] + (iArr[i2] == 0 ? " granted" : " denied"));
                i2++;
            }
            FragmentTransaction beginTransaction = getActivity().getFragmentManager().beginTransaction();
            beginTransaction.remove(this);
            beginTransaction.commit();
            this.f579a.run();
        }
    }
}
