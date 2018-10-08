package com.google.android.gms.drive.metadata;

import android.os.Bundle;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;
import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.Set;

public abstract class zza<T> implements MetadataField<T> {
    private final String zzgke;
    private final Set<String> zzgkf;
    private final Set<String> zzgkg;
    private final int zzgkh;

    protected zza(String str, int i) {
        this.zzgke = (String) zzbp.zzb((Object) str, (Object) "fieldName");
        this.zzgkf = Collections.singleton(str);
        this.zzgkg = Collections.emptySet();
        this.zzgkh = i;
    }

    protected zza(String str, Collection<String> collection, Collection<String> collection2, int i) {
        this.zzgke = (String) zzbp.zzb((Object) str, (Object) "fieldName");
        this.zzgkf = Collections.unmodifiableSet(new HashSet(collection));
        this.zzgkg = Collections.unmodifiableSet(new HashSet(collection2));
        this.zzgkh = i;
    }

    public final String getName() {
        return this.zzgke;
    }

    public String toString() {
        return this.zzgke;
    }

    public final T zza(DataHolder dataHolder, int i, int i2) {
        return zzb(dataHolder, i, i2) ? zzc(dataHolder, i, i2) : null;
    }

    protected abstract void zza(Bundle bundle, T t);

    public final void zza(DataHolder dataHolder, MetadataBundle metadataBundle, int i, int i2) {
        zzbp.zzb((Object) dataHolder, (Object) "dataHolder");
        zzbp.zzb((Object) metadataBundle, (Object) "bundle");
        if (zzb(dataHolder, i, i2)) {
            metadataBundle.zzc(this, zzc(dataHolder, i, i2));
        }
    }

    public final void zza(T t, Bundle bundle) {
        zzbp.zzb((Object) bundle, (Object) "bundle");
        if (t == null) {
            bundle.putString(this.zzgke, null);
        } else {
            zza(bundle, (Object) t);
        }
    }

    public final Collection<String> zzano() {
        return this.zzgkf;
    }

    protected boolean zzb(DataHolder dataHolder, int i, int i2) {
        for (String str : this.zzgkf) {
            if (dataHolder.zzft(str)) {
                if (dataHolder.zzh(str, i, i2)) {
                }
            }
            return false;
        }
        return true;
    }

    protected abstract T zzc(DataHolder dataHolder, int i, int i2);

    public final T zzl(Bundle bundle) {
        zzbp.zzb((Object) bundle, (Object) "bundle");
        return bundle.get(this.zzgke) != null ? zzm(bundle) : null;
    }

    protected abstract T zzm(Bundle bundle);
}
