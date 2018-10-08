package com.google.android.gms.internal;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.firebase.FirebaseApp;
import com.google.firebase.FirebaseOptions;
import java.util.Collections;
import java.util.Set;
import java.util.concurrent.atomic.AtomicReference;

public final class zzeab {
    private static final AtomicReference<zzeab> zzlen = new AtomicReference();

    private zzeab(Context context) {
    }

    @Nullable
    public static zzeab zzbyr() {
        return (zzeab) zzlen.get();
    }

    public static Set<String> zzbys() {
        return Collections.emptySet();
    }

    public static void zze(@NonNull FirebaseApp firebaseApp) {
    }

    public static zzeab zzeo(Context context) {
        zzlen.compareAndSet(null, new zzeab(context));
        return (zzeab) zzlen.get();
    }

    public static FirebaseOptions zzqb(@NonNull String str) {
        return null;
    }
}
