package com.google.android.gms.common.api.internal;

import android.content.Context;
import android.content.res.Resources;
import android.text.TextUtils;
import com.google.android.gms.C0603R;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbe;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzbz;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

@Deprecated
public final class zzca {
    private static final Object zzaqm = new Object();
    private static zzca zzfod;
    private final String mAppId;
    private final Status zzfoe;
    private final boolean zzfof;
    private final boolean zzfog;

    private zzca(Context context) {
        boolean z = true;
        boolean z2 = false;
        Resources resources = context.getResources();
        int identifier = resources.getIdentifier("google_app_measurement_enable", "integer", resources.getResourcePackageName(C0603R.string.common_google_play_services_unknown_issue));
        if (identifier != 0) {
            boolean z3 = resources.getInteger(identifier) != 0;
            if (!z3) {
                z2 = true;
            }
            this.zzfog = z2;
            z = z3;
        } else {
            this.zzfog = false;
        }
        this.zzfof = z;
        Object zzcg = zzbe.zzcg(context);
        if (zzcg == null) {
            zzcg = new zzbz(context).getString("google_app_id");
        }
        if (TextUtils.isEmpty(zzcg)) {
            this.zzfoe = new Status(10, "Missing google app id value from from string resources with name google_app_id.");
            this.mAppId = null;
            return;
        }
        this.mAppId = zzcg;
        this.zzfoe = Status.zzfhp;
    }

    public static String zzaid() {
        return zzfs("getGoogleAppId").mAppId;
    }

    public static boolean zzaie() {
        return zzfs("isMeasurementExplicitlyDisabled").zzfog;
    }

    public static Status zzcc(Context context) {
        Status status;
        zzbp.zzb((Object) context, (Object) "Context must not be null.");
        synchronized (zzaqm) {
            if (zzfod == null) {
                zzfod = new zzca(context);
            }
            status = zzfod.zzfoe;
        }
        return status;
    }

    private static zzca zzfs(String str) {
        zzca zzca;
        synchronized (zzaqm) {
            if (zzfod == null) {
                throw new IllegalStateException(new StringBuilder(String.valueOf(str).length() + 34).append("Initialize must be called before ").append(str).append(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER).toString());
            }
            zzca = zzfod;
        }
        return zzca;
    }
}
