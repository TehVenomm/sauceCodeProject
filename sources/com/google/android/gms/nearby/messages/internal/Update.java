package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import android.support.v4.util.ArraySet;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.internal.zzclm;
import com.google.android.gms.nearby.messages.Message;
import java.util.Arrays;
import java.util.Set;

public class Update extends zza implements ReflectedParcelable {
    public static final Creator<Update> CREATOR = new zzbg();
    private int zzdxt;
    public final Message zzjfy;
    private int zzjgv;
    @Nullable
    public final zze zzjgw;
    @Nullable
    public final zza zzjgx;
    @Nullable
    public final zzclm zzjgy;

    Update(int i, int i2, Message message, @Nullable zze zze, @Nullable zza zza, @Nullable zzclm zzclm) {
        boolean z = true;
        this.zzdxt = i;
        this.zzjgv = i2;
        if (zzdz(1) && zzdz(2)) {
            z = false;
        }
        zzbp.zza(z, (Object) "Update cannot represent both FOUND and LOST.");
        this.zzjfy = message;
        this.zzjgw = zze;
        this.zzjgx = zza;
        this.zzjgy = zzclm;
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof Update)) {
                return false;
            }
            Update update = (Update) obj;
            if (this.zzjgv != update.zzjgv || !zzbf.equal(this.zzjfy, update.zzjfy) || !zzbf.equal(this.zzjgw, update.zzjgw) || !zzbf.equal(this.zzjgx, update.zzjgx)) {
                return false;
            }
            if (!zzbf.equal(this.zzjgy, update.zzjgy)) {
                return false;
            }
        }
        return true;
    }

    public int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.zzjgv), this.zzjfy, this.zzjgw, this.zzjgx, this.zzjgy});
    }

    public String toString() {
        Set arraySet = new ArraySet();
        if (zzdz(1)) {
            arraySet.add("FOUND");
        }
        if (zzdz(2)) {
            arraySet.add("LOST");
        }
        if (zzdz(4)) {
            arraySet.add("DISTANCE");
        }
        if (zzdz(8)) {
            arraySet.add("BLE_SIGNAL");
        }
        if (zzdz(16)) {
            arraySet.add("DEVICE");
        }
        String valueOf = String.valueOf(arraySet);
        String valueOf2 = String.valueOf(this.zzjfy);
        String valueOf3 = String.valueOf(this.zzjgw);
        String valueOf4 = String.valueOf(this.zzjgx);
        String valueOf5 = String.valueOf(this.zzjgy);
        return new StringBuilder(((((String.valueOf(valueOf).length() + 56) + String.valueOf(valueOf2).length()) + String.valueOf(valueOf3).length()) + String.valueOf(valueOf4).length()) + String.valueOf(valueOf5).length()).append("Update{types=").append(valueOf).append(", message=").append(valueOf2).append(", distance=").append(valueOf3).append(", bleSignal=").append(valueOf4).append(", device=").append(valueOf5).append("}").toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zzc(parcel, 2, this.zzjgv);
        zzd.zza(parcel, 3, this.zzjfy, i, false);
        zzd.zza(parcel, 4, this.zzjgw, i, false);
        zzd.zza(parcel, 5, this.zzjgx, i, false);
        zzd.zza(parcel, 6, this.zzjgy, i, false);
        zzd.zzai(parcel, zze);
    }

    public final boolean zzdz(int i) {
        return (this.zzjgv & i) != 0;
    }
}
