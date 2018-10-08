package com.google.android.gms.nearby;

import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions.NoOptions;
import com.google.android.gms.internal.zzcgf;
import com.google.android.gms.internal.zzcgh;
import com.google.android.gms.internal.zzchp;
import com.google.android.gms.nearby.connection.Connections;
import com.google.android.gms.nearby.messages.Messages;
import com.google.android.gms.nearby.messages.MessagesOptions;
import com.google.android.gms.nearby.messages.internal.zzak;
import com.google.android.gms.nearby.messages.internal.zzaw;
import com.google.android.gms.nearby.messages.zzd;

public final class Nearby {
    public static final Api<NoOptions> CONNECTIONS_API = new Api("Nearby.CONNECTIONS_API", zzchp.zzdwr, zzchp.zzdwq);
    public static final Connections Connections = new zzchp();
    public static final Api<MessagesOptions> MESSAGES_API = new Api("Nearby.MESSAGES_API", zzak.zzdwr, zzak.zzdwq);
    public static final Messages Messages = zzak.zzjgd;
    private static zzd zzjae = new zzaw();
    private static Api<NoOptions> zzjaf = new Api("Nearby.BOOTSTRAP_API", zzcgh.zzdwr, zzcgh.zzdwq);
    private static zzcgf zzjag = new zzcgh();

    private Nearby() {
    }
}
