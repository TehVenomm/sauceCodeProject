package com.google.android.gms.auth.api.signin.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class SignInConfiguration extends zza implements ReflectedParcelable {
    public static final Creator<SignInConfiguration> CREATOR = new zzw();
    private final String zzedd;
    private GoogleSignInOptions zzede;

    public SignInConfiguration(String str, GoogleSignInOptions googleSignInOptions) {
        this.zzedd = zzbp.zzgf(str);
        this.zzede = googleSignInOptions;
    }

    public final boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        try {
            SignInConfiguration signInConfiguration = (SignInConfiguration) obj;
            if (!this.zzedd.equals(signInConfiguration.zzedd)) {
                return false;
            }
            if (this.zzede == null) {
                if (signInConfiguration.zzede != null) {
                    return false;
                }
            } else if (!this.zzede.equals(signInConfiguration.zzede)) {
                return false;
            }
            return true;
        } catch (ClassCastException e) {
            return false;
        }
    }

    public final int hashCode() {
        return new zzo().zzo(this.zzedd).zzo(this.zzede).zzaan();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzedd, false);
        zzd.zza(parcel, 5, this.zzede, i, false);
        zzd.zzai(parcel, zze);
    }

    public final GoogleSignInOptions zzaap() {
        return this.zzede;
    }
}
