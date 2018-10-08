package com.google.android.gms.auth.api.signin.internal;

import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.games.quest.Quests;
import com.google.android.gms.internal.zzef;
import com.google.android.gms.internal.zzeg;

public abstract class zzs extends zzef implements zzr {
    public zzs() {
        attachInterface(this, "com.google.android.gms.auth.api.signin.internal.ISignInCallbacks");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case Quests.SELECT_COMPLETED_UNCLAIMED /*101*/:
                zza((GoogleSignInAccount) zzeg.zza(parcel, GoogleSignInAccount.CREATOR), (Status) zzeg.zza(parcel, Status.CREATOR));
                break;
            case 102:
                zzi((Status) zzeg.zza(parcel, Status.CREATOR));
                break;
            case Quests.SELECT_RECENTLY_FAILED /*103*/:
                zzj((Status) zzeg.zza(parcel, Status.CREATOR));
                break;
            default:
                return false;
        }
        parcel2.writeNoException();
        return true;
    }
}
