package com.google.firebase.iid;

import android.os.Bundle;
import com.facebook.share.internal.ShareConstants;

final class zzal extends zzaj<Bundle> {
    zzal(int i, int i2, Bundle bundle) {
        super(i, 1, bundle);
    }

    /* access modifiers changed from: 0000 */
    public final boolean zzab() {
        return false;
    }

    /* access modifiers changed from: 0000 */
    public final void zzb(Bundle bundle) {
        Bundle bundle2 = bundle.getBundle(ShareConstants.WEB_DIALOG_PARAM_DATA);
        if (bundle2 == null) {
            bundle2 = Bundle.EMPTY;
        }
        finish(bundle2);
    }
}
