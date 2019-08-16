package com.google.android.gms.common.api.internal;

import android.app.Activity;
import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.p000v4.util.ArrayMap;
import com.google.android.gms.internal.common.zze;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.lang.ref.WeakReference;
import java.util.Map;
import java.util.Map.Entry;
import java.util.WeakHashMap;

public final class zza extends Fragment implements LifecycleFragment {
    private static WeakHashMap<Activity, WeakReference<zza>> zzbe = new WeakHashMap<>();
    private Map<String, LifecycleCallback> zzbf = new ArrayMap();
    /* access modifiers changed from: private */
    public int zzbg = 0;
    /* access modifiers changed from: private */
    public Bundle zzbh;

    /* JADX WARNING: Code restructure failed: missing block: B:3:0x0010, code lost:
        if (r0 != null) goto L_0x0012;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static com.google.android.gms.common.api.internal.zza zza(android.app.Activity r3) {
        /*
            java.util.WeakHashMap<android.app.Activity, java.lang.ref.WeakReference<com.google.android.gms.common.api.internal.zza>> r0 = zzbe
            java.lang.Object r0 = r0.get(r3)
            java.lang.ref.WeakReference r0 = (java.lang.ref.WeakReference) r0
            if (r0 == 0) goto L_0x0013
            java.lang.Object r0 = r0.get()
            com.google.android.gms.common.api.internal.zza r0 = (com.google.android.gms.common.api.internal.zza) r0
            if (r0 == 0) goto L_0x0013
        L_0x0012:
            return r0
        L_0x0013:
            android.app.FragmentManager r0 = r3.getFragmentManager()     // Catch:{ ClassCastException -> 0x0048 }
            java.lang.String r1 = "LifecycleFragmentImpl"
            android.app.Fragment r0 = r0.findFragmentByTag(r1)     // Catch:{ ClassCastException -> 0x0048 }
            com.google.android.gms.common.api.internal.zza r0 = (com.google.android.gms.common.api.internal.zza) r0     // Catch:{ ClassCastException -> 0x0048 }
            if (r0 == 0) goto L_0x0027
            boolean r1 = r0.isRemoving()
            if (r1 == 0) goto L_0x003d
        L_0x0027:
            com.google.android.gms.common.api.internal.zza r0 = new com.google.android.gms.common.api.internal.zza
            r0.<init>()
            android.app.FragmentManager r1 = r3.getFragmentManager()
            android.app.FragmentTransaction r1 = r1.beginTransaction()
            java.lang.String r2 = "LifecycleFragmentImpl"
            android.app.FragmentTransaction r1 = r1.add(r0, r2)
            r1.commitAllowingStateLoss()
        L_0x003d:
            java.util.WeakHashMap<android.app.Activity, java.lang.ref.WeakReference<com.google.android.gms.common.api.internal.zza>> r1 = zzbe
            java.lang.ref.WeakReference r2 = new java.lang.ref.WeakReference
            r2.<init>(r0)
            r1.put(r3, r2)
            goto L_0x0012
        L_0x0048:
            r0 = move-exception
            java.lang.IllegalStateException r1 = new java.lang.IllegalStateException
            java.lang.String r2 = "Fragment with tag LifecycleFragmentImpl is not a LifecycleFragmentImpl"
            r1.<init>(r2, r0)
            throw r1
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.common.api.internal.zza.zza(android.app.Activity):com.google.android.gms.common.api.internal.zza");
    }

    public final void addCallback(String str, @NonNull LifecycleCallback lifecycleCallback) {
        if (!this.zzbf.containsKey(str)) {
            this.zzbf.put(str, lifecycleCallback);
            if (this.zzbg > 0) {
                new zze(Looper.getMainLooper()).post(new zzb(this, lifecycleCallback, str));
                return;
            }
            return;
        }
        throw new IllegalArgumentException(new StringBuilder(String.valueOf(str).length() + 59).append("LifecycleCallback with tag ").append(str).append(" already added to this fragment.").toString());
    }

    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        super.dump(str, fileDescriptor, printWriter, strArr);
        for (LifecycleCallback dump : this.zzbf.values()) {
            dump.dump(str, fileDescriptor, printWriter, strArr);
        }
    }

    public final <T extends LifecycleCallback> T getCallbackOrNull(String str, Class<T> cls) {
        return (LifecycleCallback) cls.cast(this.zzbf.get(str));
    }

    public final Activity getLifecycleActivity() {
        return getActivity();
    }

    public final boolean isCreated() {
        return this.zzbg > 0;
    }

    public final boolean isStarted() {
        return this.zzbg >= 2;
    }

    public final void onActivityResult(int i, int i2, Intent intent) {
        super.onActivityResult(i, i2, intent);
        for (LifecycleCallback onActivityResult : this.zzbf.values()) {
            onActivityResult.onActivityResult(i, i2, intent);
        }
    }

    public final void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        this.zzbg = 1;
        this.zzbh = bundle;
        for (Entry entry : this.zzbf.entrySet()) {
            ((LifecycleCallback) entry.getValue()).onCreate(bundle != null ? bundle.getBundle((String) entry.getKey()) : null);
        }
    }

    public final void onDestroy() {
        super.onDestroy();
        this.zzbg = 5;
        for (LifecycleCallback onDestroy : this.zzbf.values()) {
            onDestroy.onDestroy();
        }
    }

    public final void onResume() {
        super.onResume();
        this.zzbg = 3;
        for (LifecycleCallback onResume : this.zzbf.values()) {
            onResume.onResume();
        }
    }

    public final void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        if (bundle != null) {
            for (Entry entry : this.zzbf.entrySet()) {
                Bundle bundle2 = new Bundle();
                ((LifecycleCallback) entry.getValue()).onSaveInstanceState(bundle2);
                bundle.putBundle((String) entry.getKey(), bundle2);
            }
        }
    }

    public final void onStart() {
        super.onStart();
        this.zzbg = 2;
        for (LifecycleCallback onStart : this.zzbf.values()) {
            onStart.onStart();
        }
    }

    public final void onStop() {
        super.onStop();
        this.zzbg = 4;
        for (LifecycleCallback onStop : this.zzbf.values()) {
            onStop.onStop();
        }
    }
}
