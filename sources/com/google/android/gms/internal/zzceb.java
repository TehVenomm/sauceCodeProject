package com.google.android.gms.internal;

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
final class zzceb implements ActivityLifecycleCallbacks {
    private /* synthetic */ zzcdo zziup;

    private zzceb(zzcdo zzcdo) {
        this.zziup = zzcdo;
    }

    public final void onActivityCreated(Activity activity, Bundle bundle) {
        try {
            this.zziup.zzauk().zzayi().log("onActivityCreated");
            Intent intent = activity.getIntent();
            if (intent != null) {
                Uri data = intent.getData();
                if (data != null && data.isHierarchical()) {
                    if (bundle == null) {
                        Bundle zzq = this.zziup.zzaug().zzq(data);
                        this.zziup.zzaug();
                        String str = zzcfo.zzl(intent) ? "gs" : "auto";
                        if (zzq != null) {
                            this.zziup.zzc(str, "_cmp", zzq);
                        }
                    }
                    CharSequence queryParameter = data.getQueryParameter("referrer");
                    if (!TextUtils.isEmpty(queryParameter)) {
                        Object obj = (queryParameter.contains("gclid") && (queryParameter.contains("utm_campaign") || queryParameter.contains("utm_source") || queryParameter.contains("utm_medium") || queryParameter.contains("utm_term") || queryParameter.contains("utm_content"))) ? 1 : null;
                        if (obj == null) {
                            this.zziup.zzauk().zzayh().log("Activity created with data 'referrer' param without gclid and at least one utm field");
                            return;
                        }
                        this.zziup.zzauk().zzayh().zzj("Activity created with referrer", queryParameter);
                        if (!TextUtils.isEmpty(queryParameter)) {
                            this.zziup.zzb("auto", "_ldl", queryParameter);
                        }
                    } else {
                        return;
                    }
                }
            }
        } catch (Throwable th) {
            this.zziup.zzauk().zzayc().zzj("Throwable caught in onActivityCreated", th);
        }
        zzcec zzauc = this.zziup.zzauc();
        if (bundle != null) {
            Bundle bundle2 = bundle.getBundle("com.google.firebase.analytics.screen_service");
            if (bundle2 != null) {
                zzcef zzq2 = zzauc.zzq(activity);
                zzq2.zziki = bundle2.getLong("id");
                zzq2.zzikg = bundle2.getString("name");
                zzq2.zzikh = bundle2.getString("referrer_name");
            }
        }
    }

    public final void onActivityDestroyed(Activity activity) {
        this.zziup.zzauc().onActivityDestroyed(activity);
    }

    @MainThread
    public final void onActivityPaused(Activity activity) {
        this.zziup.zzauc().onActivityPaused(activity);
        zzcdl zzaui = this.zziup.zzaui();
        zzaui.zzauj().zzg(new zzcfh(zzaui, zzaui.zzvu().elapsedRealtime()));
    }

    @MainThread
    public final void onActivityResumed(Activity activity) {
        this.zziup.zzauc().onActivityResumed(activity);
        zzcdl zzaui = this.zziup.zzaui();
        zzaui.zzauj().zzg(new zzcfg(zzaui, zzaui.zzvu().elapsedRealtime()));
    }

    public final void onActivitySaveInstanceState(Activity activity, Bundle bundle) {
        this.zziup.zzauc().onActivitySaveInstanceState(activity, bundle);
    }

    public final void onActivityStarted(Activity activity) {
    }

    public final void onActivityStopped(Activity activity) {
    }
}
