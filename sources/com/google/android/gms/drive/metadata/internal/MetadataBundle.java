package com.google.android.gms.drive.metadata.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.data.BitmapTeleporter;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.metadata.MetadataField;
import com.google.android.gms.internal.zzbjv;
import com.google.android.gms.internal.zzbnr;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public final class MetadataBundle extends zza implements ReflectedParcelable {
    public static final Creator<MetadataBundle> CREATOR = new zzj();
    private Bundle zzgkp;

    MetadataBundle(Bundle bundle) {
        this.zzgkp = (Bundle) zzbp.zzu(bundle);
        this.zzgkp.setClassLoader(getClass().getClassLoader());
        List arrayList = new ArrayList();
        for (String str : this.zzgkp.keySet()) {
            String str2;
            if (zzf.zzgr(str2) == null) {
                arrayList.add(str2);
                str2 = String.valueOf(str2);
                zzbjv.zzy("MetadataBundle", str2.length() != 0 ? "Ignored unknown metadata field in bundle: ".concat(str2) : new String("Ignored unknown metadata field in bundle: "));
            }
        }
        ArrayList arrayList2 = (ArrayList) arrayList;
        int size = arrayList2.size();
        int i = 0;
        while (i < size) {
            Object obj = arrayList2.get(i);
            i++;
            this.zzgkp.remove((String) obj);
        }
    }

    public static MetadataBundle zzant() {
        return new MetadataBundle(new Bundle());
    }

    public static <T> MetadataBundle zzb(MetadataField<T> metadataField, T t) {
        MetadataBundle zzant = zzant();
        zzant.zzc(metadataField, t);
        return zzant;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof MetadataBundle)) {
            return false;
        }
        MetadataBundle metadataBundle = (MetadataBundle) obj;
        Set<String> keySet = this.zzgkp.keySet();
        if (!keySet.equals(metadataBundle.zzgkp.keySet())) {
            return false;
        }
        for (String str : keySet) {
            if (!zzbf.equal(this.zzgkp.get(str), metadataBundle.zzgkp.get(str))) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        int i = 1;
        for (String str : this.zzgkp.keySet()) {
            i = this.zzgkp.get(str).hashCode() + (i * 31);
        }
        return i;
    }

    public final void setContext(Context context) {
        BitmapTeleporter bitmapTeleporter = (BitmapTeleporter) zza(zzbnr.zzgly);
        if (bitmapTeleporter != null) {
            bitmapTeleporter.zzc(context.getCacheDir());
        }
    }

    public final String toString() {
        String valueOf = String.valueOf(this.zzgkp);
        return new StringBuilder(String.valueOf(valueOf).length() + 24).append("MetadataBundle [values=").append(valueOf).append("]").toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgkp, false);
        zzd.zzai(parcel, zze);
    }

    public final <T> T zza(MetadataField<T> metadataField) {
        return metadataField.zzl(this.zzgkp);
    }

    public final MetadataBundle zzanu() {
        return new MetadataBundle(new Bundle(this.zzgkp));
    }

    public final Set<MetadataField<?>> zzanv() {
        Set<MetadataField<?>> hashSet = new HashSet();
        for (String zzgr : this.zzgkp.keySet()) {
            hashSet.add(zzf.zzgr(zzgr));
        }
        return hashSet;
    }

    public final <T> void zzc(MetadataField<T> metadataField, T t) {
        if (zzf.zzgr(metadataField.getName()) == null) {
            String valueOf = String.valueOf(metadataField.getName());
            throw new IllegalArgumentException(valueOf.length() != 0 ? "Unregistered field: ".concat(valueOf) : new String("Unregistered field: "));
        } else {
            metadataField.zza(t, this.zzgkp);
        }
    }

    public final boolean zzc(MetadataField<?> metadataField) {
        return this.zzgkp.containsKey(metadataField.getName());
    }
}
