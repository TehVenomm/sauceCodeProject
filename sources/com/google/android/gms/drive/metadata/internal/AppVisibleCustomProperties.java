package com.google.android.gms.drive.metadata.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.metadata.CustomPropertyKey;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public final class AppVisibleCustomProperties extends com.google.android.gms.common.internal.safeparcel.zza implements ReflectedParcelable, Iterable<zzc> {
    public static final Creator<AppVisibleCustomProperties> CREATOR = new zza();
    public static final AppVisibleCustomProperties zzgkj = new zza().zzanq();
    private List<zzc> zzgkk;

    public static final class zza {
        private final Map<CustomPropertyKey, zzc> zzgkl = new HashMap();

        public final zza zza(CustomPropertyKey customPropertyKey, String str) {
            zzbp.zzb((Object) customPropertyKey, (Object) "key");
            this.zzgkl.put(customPropertyKey, new zzc(customPropertyKey, str));
            return this;
        }

        public final zza zza(zzc zzc) {
            zzbp.zzb((Object) zzc, (Object) "property");
            this.zzgkl.put(zzc.zzgkm, zzc);
            return this;
        }

        public final AppVisibleCustomProperties zzanq() {
            return new AppVisibleCustomProperties(this.zzgkl.values());
        }
    }

    AppVisibleCustomProperties(Collection<zzc> collection) {
        zzbp.zzu(collection);
        this.zzgkk = new ArrayList(collection);
    }

    public final boolean equals(Object obj) {
        return this == obj ? true : (obj == null || obj.getClass() != getClass()) ? false : zzanp().equals(((AppVisibleCustomProperties) obj).zzanp());
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzgkk});
    }

    public final Iterator<zzc> iterator() {
        return this.zzgkk.iterator();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 2, this.zzgkk, false);
        zzd.zzai(parcel, zze);
    }

    public final Map<CustomPropertyKey, String> zzanp() {
        Map hashMap = new HashMap(this.zzgkk.size());
        for (zzc zzc : this.zzgkk) {
            hashMap.put(zzc.zzgkm, zzc.mValue);
        }
        return Collections.unmodifiableMap(hashMap);
    }
}
