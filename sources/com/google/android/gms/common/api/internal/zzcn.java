package com.google.android.gms.common.api.internal;

import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.internal.zzbp;
import java.util.Collections;
import java.util.Set;
import java.util.WeakHashMap;

public final class zzcn {
    private final Set<zzcj<?>> zzeue = Collections.newSetFromMap(new WeakHashMap());

    public static <L> zzcl<L> zza(@NonNull L l, @NonNull String str) {
        zzbp.zzb((Object) l, (Object) "Listener must not be null");
        zzbp.zzb((Object) str, (Object) "Listener type must not be null");
        zzbp.zzh(str, "Listener type must not be empty");
        return new zzcl(l, str);
    }

    public static <L> zzcj<L> zzb(@NonNull L l, @NonNull Looper looper, @NonNull String str) {
        zzbp.zzb((Object) l, (Object) "Listener must not be null");
        zzbp.zzb((Object) looper, (Object) "Looper must not be null");
        zzbp.zzb((Object) str, (Object) "Listener type must not be null");
        return new zzcj(looper, l, str);
    }

    public final void release() {
        for (zzcj clear : this.zzeue) {
            clear.clear();
        }
        this.zzeue.clear();
    }

    public final <L> zzcj<L> zza(@NonNull L l, @NonNull Looper looper, @NonNull String str) {
        zzcj<L> zzb = zzb(l, looper, str);
        this.zzeue.add(zzb);
        return zzb;
    }
}
