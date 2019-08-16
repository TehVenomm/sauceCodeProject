package com.google.android.gms.internal.measurement;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.OnSharedPreferenceChangeListener;
import android.support.annotation.GuardedBy;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public final class zzcs implements zzce {
    @GuardedBy("SharedPreferencesLoader.class")
    static final Map<String, zzcs> zzabd = new HashMap();
    private final Object zzaaj = new Object();
    private volatile Map<String, ?> zzaak;
    @GuardedBy("this")
    private final List<zzcf> zzaal = new ArrayList();
    private final SharedPreferences zzabe;
    private final OnSharedPreferenceChangeListener zzabf = new zzcv(this);

    private zzcs(SharedPreferences sharedPreferences) {
        this.zzabe = sharedPreferences;
        this.zzabe.registerOnSharedPreferenceChangeListener(this.zzabf);
    }

    static zzcs zze(Context context, String str) {
        zzcs zzcs;
        SharedPreferences sharedPreferences;
        if (!((!zzcb.zzri() || str.startsWith("direct_boot:")) ? true : zzcb.isUserUnlocked(context))) {
            return null;
        }
        synchronized (zzcs.class) {
            try {
                zzcs = (zzcs) zzabd.get(str);
                if (zzcs == null) {
                    if (str.startsWith("direct_boot:")) {
                        if (zzcb.zzri()) {
                            context = context.createDeviceProtectedStorageContext();
                        }
                        sharedPreferences = context.getSharedPreferences(str.substring(12), 0);
                    } else {
                        sharedPreferences = context.getSharedPreferences(str, 0);
                    }
                    zzcs zzcs2 = new zzcs(sharedPreferences);
                    zzabd.put(str, zzcs2);
                    zzcs = zzcs2;
                }
            } finally {
                Class<zzcs> cls = zzcs.class;
            }
        }
        return zzcs;
    }

    /* access modifiers changed from: 0000 */
    public final /* synthetic */ void zza(SharedPreferences sharedPreferences, String str) {
        synchronized (this.zzaaj) {
            this.zzaak = null;
            zzcm.zzrl();
        }
        synchronized (this) {
            for (zzcf zzrk : this.zzaal) {
                zzrk.zzrk();
            }
        }
    }

    public final Object zzdd(String str) {
        Map<String, ?> map = this.zzaak;
        if (map == null) {
            synchronized (this.zzaaj) {
                map = this.zzaak;
                if (map == null) {
                    map = this.zzabe.getAll();
                    this.zzaak = map;
                }
            }
        }
        if (map != null) {
            return map.get(str);
        }
        return null;
    }
}
