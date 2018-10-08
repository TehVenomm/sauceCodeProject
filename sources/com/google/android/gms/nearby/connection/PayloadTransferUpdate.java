package com.google.android.gms.nearby.connection;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.util.Arrays;

public final class PayloadTransferUpdate extends zza {
    public static final Creator<PayloadTransferUpdate> CREATOR = new zze();
    private final int status;
    private final long zzjau;
    private final long zzjav;
    private final long zzjaw;

    @Retention(RetentionPolicy.SOURCE)
    public @interface Status {
        public static final int FAILURE = 2;
        public static final int IN_PROGRESS = 3;
        public static final int SUCCESS = 1;
    }

    public PayloadTransferUpdate(long j, int i, long j2, long j3) {
        this.zzjau = j;
        this.status = i;
        this.zzjav = j2;
        this.zzjaw = j3;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof PayloadTransferUpdate)) {
            return false;
        }
        PayloadTransferUpdate payloadTransferUpdate = (PayloadTransferUpdate) obj;
        return zzbf.equal(Long.valueOf(this.zzjau), Long.valueOf(payloadTransferUpdate.zzjau)) && zzbf.equal(Integer.valueOf(this.status), Integer.valueOf(payloadTransferUpdate.status)) && zzbf.equal(Long.valueOf(this.zzjav), Long.valueOf(payloadTransferUpdate.zzjav)) && zzbf.equal(Long.valueOf(this.zzjaw), Long.valueOf(payloadTransferUpdate.zzjaw));
    }

    public final long getBytesTransferred() {
        return this.zzjaw;
    }

    public final long getPayloadId() {
        return this.zzjau;
    }

    public final int getStatus() {
        return this.status;
    }

    public final long getTotalBytes() {
        return this.zzjav;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Long.valueOf(this.zzjau), Integer.valueOf(this.status), Long.valueOf(this.zzjav), Long.valueOf(this.zzjaw)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getPayloadId());
        zzd.zzc(parcel, 2, getStatus());
        zzd.zza(parcel, 3, getTotalBytes());
        zzd.zza(parcel, 4, getBytesTransferred());
        zzd.zzai(parcel, zze);
    }
}
