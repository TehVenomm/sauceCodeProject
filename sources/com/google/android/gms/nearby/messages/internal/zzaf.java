package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.nearby.messages.Message;
import java.util.Arrays;

public final class zzaf extends zza {
    public static final Creator<zzaf> CREATOR = new zzag();
    private int zzdxt;
    private Message zzjfy;

    zzaf(int i, Message message) {
        this.zzdxt = i;
        this.zzjfy = (Message) zzbp.zzu(message);
    }

    public static final zzaf zza(Message message) {
        return new zzaf(1, message);
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzaf)) {
            return false;
        }
        return zzbf.equal(this.zzjfy, ((zzaf) obj).zzjfy);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjfy});
    }

    public final String toString() {
        String message = this.zzjfy.toString();
        return new StringBuilder(String.valueOf(message).length() + 24).append("MessageWrapper{message=").append(message).append("}").toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjfy, i, false);
        zzd.zzc(parcel, 1000, this.zzdxt);
        zzd.zzai(parcel, zze);
    }
}
