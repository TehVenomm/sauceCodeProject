package com.google.android.gms.drive.metadata.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveId;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class ParentDriveIdSet extends zza implements ReflectedParcelable {
    public static final Creator<ParentDriveIdSet> CREATOR = new zzn();
    final List<zzq> zzgkr;

    public ParentDriveIdSet() {
        this(new ArrayList());
    }

    ParentDriveIdSet(List<zzq> list) {
        this.zzgkr = list;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 2, this.zzgkr, false);
        zzd.zzai(parcel, zze);
    }

    public final Set<DriveId> zzab(long j) {
        Set<DriveId> hashSet = new HashSet();
        for (zzq zzq : this.zzgkr) {
            hashSet.add(new DriveId(zzq.zzgdj, zzq.zzgdk, j, zzq.zzgdl));
        }
        return hashSet;
    }
}
