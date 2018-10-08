package com.google.android.gms.games.multiplayer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.games.internal.zzc;

public final class ParticipantResult extends zzc {
    public static final Creator<ParticipantResult> CREATOR = new zzd();
    public static final int MATCH_RESULT_DISAGREED = 5;
    public static final int MATCH_RESULT_DISCONNECT = 4;
    public static final int MATCH_RESULT_LOSS = 1;
    public static final int MATCH_RESULT_NONE = 3;
    public static final int MATCH_RESULT_TIE = 2;
    public static final int MATCH_RESULT_UNINITIALIZED = -1;
    public static final int MATCH_RESULT_WIN = 0;
    public static final int PLACING_UNINITIALIZED = -1;
    private final String zzhfq;
    private final int zzhmk;
    private final int zzhml;

    public ParticipantResult(String str, int i, int i2) {
        boolean z;
        this.zzhfq = (String) zzbp.zzu(str);
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
        zzbp.zzbg(z);
        this.zzhmk = i;
        this.zzhml = i2;
    }

    public final String getParticipantId() {
        return this.zzhfq;
    }

    public final int getPlacing() {
        return this.zzhml;
    }

    public final int getResult() {
        return this.zzhmk;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getParticipantId(), false);
        zzd.zzc(parcel, 2, getResult());
        zzd.zzc(parcel, 3, getPlacing());
        zzd.zzai(parcel, zze);
    }
}
