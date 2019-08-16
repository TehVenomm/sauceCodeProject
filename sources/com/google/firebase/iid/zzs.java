package com.google.firebase.iid;

import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.annotation.VisibleForTesting;
import android.util.Log;
import com.appsflyer.AppsFlyerProperties;
import com.facebook.appevents.AppEventsConstants;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;
import com.google.android.gms.tasks.Tasks;
import com.google.firebase.FirebaseApp;
import com.google.firebase.platforminfo.UserAgentPublisher;
import java.io.IOException;
import java.util.concurrent.Executor;
import p018jp.colopl.gcm.RegistrarHelper;

final class zzs implements MessagingChannel {
    private final Executor executor;
    private final FirebaseApp zzau;
    private final zzan zzav;
    private final zzau zzbq;
    private final UserAgentPublisher zzbr;

    @VisibleForTesting
    private zzs(FirebaseApp firebaseApp, zzan zzan, Executor executor2, zzau zzau2, UserAgentPublisher userAgentPublisher) {
        this.zzau = firebaseApp;
        this.zzav = zzan;
        this.zzbq = zzau2;
        this.executor = executor2;
        this.zzbr = userAgentPublisher;
    }

    zzs(FirebaseApp firebaseApp, zzan zzan, Executor executor2, UserAgentPublisher userAgentPublisher) {
        this(firebaseApp, zzan, executor2, new zzau(firebaseApp.getApplicationContext(), zzan), userAgentPublisher);
    }

    private final Task<Bundle> zza(String str, String str2, String str3, Bundle bundle) {
        bundle.putString("scope", str3);
        bundle.putString("sender", str2);
        bundle.putString("subtype", str2);
        bundle.putString(AppsFlyerProperties.APP_ID, str);
        bundle.putString("gmp_app_id", this.zzau.getOptions().getApplicationId());
        bundle.putString("gmsv", Integer.toString(this.zzav.zzaf()));
        bundle.putString("osv", Integer.toString(VERSION.SDK_INT));
        bundle.putString("app_ver", this.zzav.zzad());
        bundle.putString("app_ver_name", this.zzav.zzae());
        bundle.putString("cliv", "fiid-12451000");
        bundle.putString("Firebase-Client", this.zzbr.getUserAgent());
        TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        this.executor.execute(new zzr(this, bundle, taskCompletionSource));
        return taskCompletionSource.getTask();
    }

    /* access modifiers changed from: private */
    public static String zza(Bundle bundle) throws IOException {
        if (bundle == null) {
            throw new IOException("SERVICE_NOT_AVAILABLE");
        }
        String string = bundle.getString(RegistrarHelper.PROPERTY_REG_ID);
        if (string == null) {
            string = bundle.getString("unregistered");
            if (string == null) {
                String string2 = bundle.getString("error");
                if ("RST".equals(string2)) {
                    throw new IOException("INSTANCE_ID_RESET");
                } else if (string2 != null) {
                    throw new IOException(string2);
                } else {
                    String valueOf = String.valueOf(bundle);
                    Log.w("FirebaseInstanceId", new StringBuilder(String.valueOf(valueOf).length() + 21).append("Unexpected response: ").append(valueOf).toString(), new Throwable());
                    throw new IOException("SERVICE_NOT_AVAILABLE");
                }
            }
        }
        return string;
    }

    private final <T> Task<Void> zzb(Task<T> task) {
        return task.continueWith(zzh.zzd(), new zzu(this));
    }

    private final Task<String> zzc(Task<Bundle> task) {
        return task.continueWith(this.executor, new zzt(this));
    }

    public final Task<Void> ackMessage(String str) {
        return null;
    }

    public final Task<Void> buildChannel(String str, String str2) {
        return Tasks.forResult(null);
    }

    public final Task<Void> deleteInstanceId(String str) {
        Bundle bundle = new Bundle();
        bundle.putString("iid-operation", "delete");
        bundle.putString("delete", AppEventsConstants.EVENT_PARAM_VALUE_YES);
        return zzb(zzc(zza(str, "*", "*", bundle)));
    }

    public final Task<Void> deleteToken(String str, String str2, String str3, String str4) {
        Bundle bundle = new Bundle();
        bundle.putString("delete", AppEventsConstants.EVENT_PARAM_VALUE_YES);
        return zzb(zzc(zza(str, str3, str4, bundle)));
    }

    public final Task<String> getToken(String str, String str2, String str3, String str4) {
        return zzc(zza(str, str3, str4, new Bundle()));
    }

    public final boolean isAvailable() {
        return this.zzav.zzac() != 0;
    }

    public final boolean isChannelBuilt() {
        return true;
    }

    public final boolean needsRefresh() {
        return false;
    }

    public final Task<Void> subscribeToTopic(String str, String str2, String str3) {
        Bundle bundle = new Bundle();
        String valueOf = String.valueOf("/topics/");
        String valueOf2 = String.valueOf(str3);
        bundle.putString("gcm.topic", valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf));
        String valueOf3 = String.valueOf("/topics/");
        String valueOf4 = String.valueOf(str3);
        return zzb(zzc(zza(str, str2, valueOf4.length() != 0 ? valueOf3.concat(valueOf4) : new String(valueOf3), bundle)));
    }

    public final Task<Void> unsubscribeFromTopic(String str, String str2, String str3) {
        Bundle bundle = new Bundle();
        String valueOf = String.valueOf("/topics/");
        String valueOf2 = String.valueOf(str3);
        bundle.putString("gcm.topic", valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf));
        bundle.putString("delete", AppEventsConstants.EVENT_PARAM_VALUE_YES);
        String valueOf3 = String.valueOf("/topics/");
        String valueOf4 = String.valueOf(str3);
        return zzb(zzc(zza(str, str2, valueOf4.length() != 0 ? valueOf3.concat(valueOf4) : new String(valueOf3), bundle)));
    }

    /* access modifiers changed from: 0000 */
    public final /* synthetic */ void zza(Bundle bundle, TaskCompletionSource taskCompletionSource) {
        try {
            taskCompletionSource.setResult(this.zzbq.zzc(bundle));
        } catch (IOException e) {
            taskCompletionSource.setException(e);
        }
    }
}
