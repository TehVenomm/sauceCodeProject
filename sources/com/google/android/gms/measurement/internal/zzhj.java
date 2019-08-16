package com.google.android.gms.measurement.internal;

import android.annotation.TargetApi;
import android.app.Activity;
import android.app.Application.ActivityLifecycleCallbacks;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.MainThread;
import android.text.TextUtils;

@TargetApi(14)
@MainThread
final class zzhj implements ActivityLifecycleCallbacks {
    private final /* synthetic */ zzgp zzpt;

    private zzhj(zzgp zzgp) {
        this.zzpt = zzgp;
    }

    /* synthetic */ zzhj(zzgp zzgp, zzgo zzgo) {
        this(zzgp);
    }

    public final void onActivityCreated(Activity activity, Bundle bundle) {
        Bundle bundle2;
        Bundle bundle3 = null;
        boolean z = true;
        try {
            this.zzpt.zzab().zzgs().zzao("onActivityCreated");
            Intent intent = activity.getIntent();
            if (intent != null) {
                Uri data = intent.getData();
                if (data == null || !data.isHierarchical()) {
                    this.zzpt.zzt().onActivityCreated(activity, bundle);
                    return;
                }
                this.zzpt.zzz();
                String str = zzjs.zzc(intent) ? "gs" : "auto";
                String queryParameter = data.getQueryParameter("referrer");
                if (!this.zzpt.zzad().zza(zzak.zzje) && !this.zzpt.zzad().zza(zzak.zzjg)) {
                    bundle2 = null;
                } else if (TextUtils.isEmpty(queryParameter)) {
                    bundle2 = null;
                } else if (queryParameter.contains("gclid") || queryParameter.contains("utm_campaign") || queryParameter.contains("utm_source") || queryParameter.contains("utm_medium")) {
                    zzjs zzz = this.zzpt.zzz();
                    String valueOf = String.valueOf(queryParameter);
                    bundle2 = zzz.zza(Uri.parse(valueOf.length() != 0 ? "https://google.com/search?".concat(valueOf) : new String("https://google.com/search?")));
                    if (bundle2 != null) {
                        bundle2.putString("_cis", "referrer");
                    }
                } else {
                    this.zzpt.zzab().zzgr().zzao("Activity created with data 'referrer' without required params");
                    bundle2 = null;
                }
                if (bundle == null) {
                    bundle3 = this.zzpt.zzz().zza(data);
                    if (bundle3 != null) {
                        bundle3.putString("_cis", "intent");
                        if (this.zzpt.zzad().zza(zzak.zzje) && !bundle3.containsKey("gclid") && bundle2 != null && bundle2.containsKey("gclid")) {
                            bundle3.putString("_cer", String.format("gclid=%s", new Object[]{bundle2.getString("gclid")}));
                        }
                        this.zzpt.logEvent(str, "_cmp", bundle3);
                    }
                }
                if (this.zzpt.zzad().zza(zzak.zzjg) && bundle2 != null && bundle2.containsKey("gclid") && (bundle3 == null || !bundle3.containsKey("gclid"))) {
                    this.zzpt.zzb("auto", "_lgclid", (Object) bundle2.getString("gclid"), true);
                }
                if (TextUtils.isEmpty(queryParameter)) {
                    this.zzpt.zzt().onActivityCreated(activity, bundle);
                    return;
                }
                if (!queryParameter.contains("gclid") || (!queryParameter.contains("utm_campaign") && !queryParameter.contains("utm_source") && !queryParameter.contains("utm_medium") && !queryParameter.contains("utm_term") && !queryParameter.contains("utm_content"))) {
                    z = false;
                }
                if (!z) {
                    this.zzpt.zzab().zzgr().zzao("Activity created with data 'referrer' without required params");
                    this.zzpt.zzt().onActivityCreated(activity, bundle);
                    return;
                }
                this.zzpt.zzab().zzgr().zza("Activity created with referrer", queryParameter);
                if (!TextUtils.isEmpty(queryParameter)) {
                    this.zzpt.zzb("auto", "_ldl", (Object) queryParameter, true);
                }
                this.zzpt.zzt().onActivityCreated(activity, bundle);
            }
        } catch (Exception e) {
            this.zzpt.zzab().zzgk().zza("Throwable caught in onActivityCreated", e);
        } finally {
            this.zzpt.zzt().onActivityCreated(activity, bundle);
        }
    }

    public final void onActivityDestroyed(Activity activity) {
        this.zzpt.zzt().onActivityDestroyed(activity);
    }

    @MainThread
    public final void onActivityPaused(Activity activity) {
        this.zzpt.zzt().onActivityPaused(activity);
        zziw zzv = this.zzpt.zzv();
        zzv.zzaa().zza((Runnable) new zzja(zzv, zzv.zzx().elapsedRealtime()));
    }

    @MainThread
    public final void onActivityResumed(Activity activity) {
        this.zzpt.zzt().onActivityResumed(activity);
        zziw zzv = this.zzpt.zzv();
        zzv.zzaa().zza((Runnable) new zzjb(zzv, zzv.zzx().elapsedRealtime()));
    }

    public final void onActivitySaveInstanceState(Activity activity, Bundle bundle) {
        this.zzpt.zzt().onActivitySaveInstanceState(activity, bundle);
    }

    public final void onActivityStarted(Activity activity) {
    }

    public final void onActivityStopped(Activity activity) {
    }
}
