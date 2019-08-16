package com.google.android.gms.games.multiplayer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.games.internal.zzd;

@Class(creator = "ParticipantResultCreator")
@Reserved({1000})
public final class ParticipantResult extends zzd {
    public static final Creator<ParticipantResult> CREATOR = new zzd();
    public static final int MATCH_RESULT_DISAGREED = 5;
    public static final int MATCH_RESULT_DISCONNECT = 4;
    public static final int MATCH_RESULT_LOSS = 1;
    public static final int MATCH_RESULT_NONE = 3;
    public static final int MATCH_RESULT_TIE = 2;
    public static final int MATCH_RESULT_UNINITIALIZED = -1;
    public static final int MATCH_RESULT_WIN = 0;
    public static final int PLACING_UNINITIALIZED = -1;
    @Field(getter = "getParticipantId", mo13990id = 1)
    private final String zzpg;
    @Field(getter = "getResult", mo13990id = 2)
    private final int zzpm;
    @Field(getter = "getPlacing", mo13990id = 3)
    private final int zzpn;

    @Constructor
    public ParticipantResult(@Param(mo13993id = 1) String str, @Param(mo13993id = 2) int i, @Param(mo13993id = 3) int i2) {
        boolean z;
        this.zzpg = (String) Preconditions.checkNotNull(str);
        switch (i) {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                z = true;
                break;
            default:
                z = false;
                break;
        }
        Preconditions.checkState(z);
        this.zzpm = i;
        this.zzpn = i2;
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof ParticipantResult)) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        ParticipantResult participantResult = (ParticipantResult) obj;
        return participantResult.getPlacing() == getPlacing() && participantResult.getResult() == getResult() && Objects.equal(participantResult.getParticipantId(), getParticipantId());
    }

    public final String getParticipantId() {
        return this.zzpg;
    }

    public final int getPlacing() {
        return this.zzpn;
    }

    public final int getResult() {
        return this.zzpm;
    }

    public final int hashCode() {
        return Objects.hashCode(Integer.valueOf(getPlacing()), Integer.valueOf(getResult()), getParticipantId());
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeString(parcel, 1, getParticipantId(), false);
        SafeParcelWriter.writeInt(parcel, 2, getResult());
        SafeParcelWriter.writeInt(parcel, 3, getPlacing());
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
