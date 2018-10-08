package com.google.android.gms.common.internal;

import android.content.Context;
import android.content.res.Resources;
import com.google.android.gms.C0603R;

public final class zzbz {
    private final Resources zzfvz;
    private final String zzfwa = this.zzfvz.getResourcePackageName(C0603R.string.common_google_play_services_unknown_issue);

    public zzbz(Context context) {
        zzbp.zzu(context);
        this.zzfvz = context.getResources();
    }

    public final String getString(String str) {
        int identifier = this.zzfvz.getIdentifier(str, "string", this.zzfwa);
        return identifier == 0 ? null : this.zzfvz.getString(identifier);
    }
}
