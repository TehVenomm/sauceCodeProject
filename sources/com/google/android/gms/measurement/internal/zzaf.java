package com.google.android.gms.measurement.internal;

import android.os.Bundle;
import android.text.TextUtils;
import com.google.android.gms.common.internal.Preconditions;
import java.util.Iterator;

public final class zzaf {
    final String name;
    private final String origin;
    final long timestamp;
    final String zzce;
    final long zzfp;
    final zzah zzfq;

    zzaf(zzfj zzfj, String str, String str2, String str3, long j, long j2, Bundle bundle) {
        zzah zzah;
        Preconditions.checkNotEmpty(str2);
        Preconditions.checkNotEmpty(str3);
        this.zzce = str2;
        this.name = str3;
        if (TextUtils.isEmpty(str)) {
            str = null;
        }
        this.origin = str;
        this.timestamp = j;
        this.zzfp = j2;
        if (this.zzfp != 0 && this.zzfp > this.timestamp) {
            zzfj.zzab().zzgn().zza("Event created with reverse previous/current timestamps. appId", zzef.zzam(str2));
        }
        if (bundle == null || bundle.isEmpty()) {
            zzah = new zzah(new Bundle());
        } else {
            Bundle bundle2 = new Bundle(bundle);
            Iterator it = bundle2.keySet().iterator();
            while (it.hasNext()) {
                String str4 = (String) it.next();
                if (str4 == null) {
                    zzfj.zzab().zzgk().zzao("Param name can't be null");
                    it.remove();
                } else {
                    Object zzb = zzfj.zzz().zzb(str4, bundle2.get(str4));
                    if (zzb == null) {
                        zzfj.zzab().zzgn().zza("Param value can't be null", zzfj.zzy().zzak(str4));
                        it.remove();
                    } else {
                        zzfj.zzz().zza(bundle2, str4, zzb);
                    }
                }
            }
            zzah = new zzah(bundle2);
        }
        this.zzfq = zzah;
    }

    private zzaf(zzfj zzfj, String str, String str2, String str3, long j, long j2, zzah zzah) {
        Preconditions.checkNotEmpty(str2);
        Preconditions.checkNotEmpty(str3);
        Preconditions.checkNotNull(zzah);
        this.zzce = str2;
        this.name = str3;
        if (TextUtils.isEmpty(str)) {
            str = null;
        }
        this.origin = str;
        this.timestamp = j;
        this.zzfp = j2;
        if (this.zzfp != 0 && this.zzfp > this.timestamp) {
            zzfj.zzab().zzgn().zza("Event created with reverse previous/current timestamps. appId, name", zzef.zzam(str2), zzef.zzam(str3));
        }
        this.zzfq = zzah;
    }

    public final String toString() {
        String str = this.zzce;
        String str2 = this.name;
        String valueOf = String.valueOf(this.zzfq);
        return new StringBuilder(String.valueOf(str).length() + 33 + String.valueOf(str2).length() + String.valueOf(valueOf).length()).append("Event{appId='").append(str).append("', name='").append(str2).append("', params=").append(valueOf).append('}').toString();
    }

    /* access modifiers changed from: 0000 */
    public final zzaf zza(zzfj zzfj, long j) {
        return new zzaf(zzfj, this.origin, this.zzce, this.name, this.timestamp, j, this.zzfq);
    }
}
