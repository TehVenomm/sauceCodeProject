package com.google.android.gms.games.multiplayer.realtime;

import android.os.Parcel;
import android.os.Parcelable;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.zzbp;

public final class RealTimeMessage implements Parcelable {
    public static final Creator<RealTimeMessage> CREATOR = new zza();
    public static final int RELIABLE = 1;
    public static final int UNRELIABLE = 0;
    private final String zzhmm;
    private final byte[] zzhmn;
    private final int zzhmo;

    private RealTimeMessage(Parcel parcel) {
        this(parcel.readString(), parcel.createByteArray(), parcel.readInt());
    }

    private RealTimeMessage(String str, byte[] bArr, int i) {
        this.zzhmm = (String) zzbp.zzu(str);
        this.zzhmn = (byte[]) ((byte[]) zzbp.zzu(bArr)).clone();
        this.zzhmo = i;
    }

    public final int describeContents() {
        return 0;
    }

    public final byte[] getMessageData() {
        return this.zzhmn;
    }

    public final String getSenderParticipantId() {
        return this.zzhmm;
    }

    public final boolean isReliable() {
        return this.zzhmo == 1;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        parcel.writeString(this.zzhmm);
        parcel.writeByteArray(this.zzhmn);
        parcel.writeInt(this.zzhmo);
    }
}
