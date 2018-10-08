package com.google.android.gms.auth.account;

import android.app.Activity;
import android.content.Context;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions.NoOptions;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zzf;
import com.google.android.gms.internal.zzaqx;
import com.google.android.gms.internal.zzarh;

public class WorkAccount {
    public static final Api<NoOptions> API = new Api("WorkAccount.API", zzdwr, zzdwq);
    @Deprecated
    public static final WorkAccountApi WorkAccountApi = new zzaqx();
    private static final zzf<zzarh> zzdwq = new zzf();
    private static final zza<zzarh, NoOptions> zzdwr = new zzf();

    private WorkAccount() {
    }

    public static WorkAccountClient getClient(@NonNull Activity activity) {
        return new WorkAccountClient(activity);
    }

    public static WorkAccountClient getClient(@NonNull Context context) {
        return new WorkAccountClient(context);
    }
}
