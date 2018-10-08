package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import android.support.v4.util.SimpleArrayMap;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.nearby.messages.MessageListener;
import com.google.android.gms.nearby.messages.StatusCallback;

public final class zzcmc {
    private final SimpleArrayMap<Object, zzclq> zzjhe = new SimpleArrayMap(1);

    private static boolean zza(zzclq zzclq) {
        return zzclq != null && zzclq.zzbbb().zzadh();
    }

    @Nullable
    private final <C> C zzj(zzcj<C> zzcj) {
        C c;
        synchronized (this) {
            for (int i = 0; i < this.zzjhe.size(); i++) {
                C keyAt = this.zzjhe.keyAt(i);
                if (((zzclq) this.zzjhe.get(keyAt)).zzbbb().equals(zzcj)) {
                    c = keyAt;
                    break;
                }
            }
            c = null;
        }
        return c;
    }

    public final void clear() {
        synchronized (this) {
            this.zzjhe.clear();
        }
    }

    @Nullable
    public final <C> zzclq<C> zzae(@Nullable C c) {
        zzclq<C> zzclq;
        synchronized (this) {
            if (c == null) {
                zzclq = null;
            } else {
                zzclq = (zzclq) this.zzjhe.get(c);
                if (!zza(zzclq)) {
                    this.zzjhe.remove(c);
                    zzclq = null;
                }
            }
        }
        return zzclq;
    }

    @Nullable
    public final <C> zzclq<C> zzb(GoogleApiClient googleApiClient, @Nullable C c) {
        zzclq<C> zzclq;
        synchronized (this) {
            if (c == null) {
                zzclq = null;
            } else {
                zzclq = (zzclq) this.zzjhe.get(c);
                if (!zza(zzclq)) {
                    zzcj zzp = googleApiClient.zzp(c);
                    if (c instanceof StatusCallback) {
                        zzclq = new zzcly(zzp);
                    } else if (c instanceof MessageListener) {
                        zzclq = new zzclr(zzp);
                    } else {
                        String valueOf = String.valueOf(c.getClass().getName());
                        throw new IllegalArgumentException(valueOf.length() != 0 ? "Unknown callback of type ".concat(valueOf) : new String("Unknown callback of type "));
                    }
                    this.zzjhe.put(c, zzclq);
                }
            }
        }
        return zzclq;
    }

    @Nullable
    public final <C> zzclq<C> zzh(zzcj<C> zzcj) {
        zzclq<C> zzae;
        synchronized (this) {
            zzae = zzcj == null ? null : zzae(zzj(zzcj));
        }
        return zzae;
    }

    public final <C> void zzi(zzcj<C> zzcj) {
        synchronized (this) {
            zzcj.clear();
            this.zzjhe.remove(zzj(zzcj));
        }
    }
}
