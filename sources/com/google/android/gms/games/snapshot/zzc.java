package com.google.android.gms.games.snapshot;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;

public final class zzc implements Creator<SnapshotEntity> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        int zzd = zzb.zzd(parcel);
        zza zza = null;
        SnapshotMetadata snapshotMetadata = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    snapshotMetadata = (SnapshotMetadataEntity) zzb.zza(parcel, readInt, SnapshotMetadataEntity.CREATOR);
                    break;
                case 3:
                    zza = (zza) zzb.zza(parcel, readInt, zza.CREATOR);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new SnapshotEntity(snapshotMetadata, zza);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new SnapshotEntity[i];
    }
}
