package com.google.android.gms.internal;

import android.accounts.Account;
import android.os.Bundle;
import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.auth.AccountChangeEventsRequest;
import com.google.android.gms.auth.AccountChangeEventsResponse;

public final class zzej extends zzee implements zzeh {
    zzej(IBinder iBinder) {
        super(iBinder, "com.google.android.auth.IAuthManagerService");
    }

    public final Bundle zza(Account account) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) account);
        Parcel zza = zza(7, zzax);
        Bundle bundle = (Bundle) zzeg.zza(zza, Bundle.CREATOR);
        zza.recycle();
        return bundle;
    }

    public final Bundle zza(Account account, String str, Bundle bundle) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) account);
        zzax.writeString(str);
        zzeg.zza(zzax, (Parcelable) bundle);
        Parcel zza = zza(5, zzax);
        Bundle bundle2 = (Bundle) zzeg.zza(zza, Bundle.CREATOR);
        zza.recycle();
        return bundle2;
    }

    public final Bundle zza(String str, Bundle bundle) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzeg.zza(zzax, (Parcelable) bundle);
        Parcel zza = zza(2, zzax);
        Bundle bundle2 = (Bundle) zzeg.zza(zza, Bundle.CREATOR);
        zza.recycle();
        return bundle2;
    }

    public final AccountChangeEventsResponse zza(AccountChangeEventsRequest accountChangeEventsRequest) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) accountChangeEventsRequest);
        Parcel zza = zza(3, zzax);
        AccountChangeEventsResponse accountChangeEventsResponse = (AccountChangeEventsResponse) zzeg.zza(zza, AccountChangeEventsResponse.CREATOR);
        zza.recycle();
        return accountChangeEventsResponse;
    }
}
