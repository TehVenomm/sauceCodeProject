package com.unity3d.player;

import android.app.Activity;
import android.content.ComponentName;
import android.content.pm.ActivityInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Bundle;

/* renamed from: com.unity3d.player.o */
public final class C0770o {
    /* renamed from: a */
    private final Bundle f530a;

    public C0770o(Activity activity) {
        Bundle bundle = Bundle.EMPTY;
        PackageManager packageManager = activity.getPackageManager();
        ComponentName componentName = activity.getComponentName();
        try {
            ActivityInfo activityInfo = packageManager.getActivityInfo(componentName, 128);
            if (!(activityInfo == null || activityInfo.metaData == null)) {
                bundle = activityInfo.metaData;
            }
        } catch (NameNotFoundException e) {
            C0767m.Log(6, "Unable to retreive meta data for activity '" + componentName + "'");
        }
        this.f530a = new Bundle(bundle);
    }

    /* renamed from: a */
    private static String m508a(String str) {
        return String.format("%s.%s", new Object[]{"unityplayer", str});
    }

    /* renamed from: a */
    public final boolean m509a() {
        return this.f530a.getBoolean(C0770o.m508a("ForwardNativeEventsToDalvik"));
    }
}
