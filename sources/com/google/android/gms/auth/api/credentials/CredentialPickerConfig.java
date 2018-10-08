package com.google.android.gms.auth.api.credentials;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;

public final class CredentialPickerConfig extends zza implements ReflectedParcelable {
    public static final Creator<CredentialPickerConfig> CREATOR = new zzc();
    private final boolean mShowCancelButton;
    private int zzdxt;
    private final boolean zzeai;
    @Deprecated
    private final boolean zzeaj;
    private final int zzeak;

    public static class Builder {
        private boolean mShowCancelButton = true;
        private boolean zzeai = false;
        private int zzeal = 1;

        public CredentialPickerConfig build() {
            return new CredentialPickerConfig();
        }

        @Deprecated
        public Builder setForNewAccount(boolean z) {
            this.zzeal = z ? 3 : 1;
            return this;
        }

        public Builder setPrompt(int i) {
            this.zzeal = i;
            return this;
        }

        public Builder setShowAddAccountButton(boolean z) {
            this.zzeai = z;
            return this;
        }

        public Builder setShowCancelButton(boolean z) {
            this.mShowCancelButton = z;
            return this;
        }
    }

    @Retention(RetentionPolicy.SOURCE)
    public @interface Prompt {
        public static final int CONTINUE = 1;
        public static final int SIGN_IN = 2;
        public static final int SIGN_UP = 3;
    }

    CredentialPickerConfig(int i, boolean z, boolean z2, boolean z3, int i2) {
        int i3 = 3;
        boolean z4 = true;
        this.zzdxt = i;
        this.zzeai = z;
        this.mShowCancelButton = z2;
        if (i < 2) {
            this.zzeaj = z3;
            if (!z3) {
                i3 = 1;
            }
            this.zzeak = i3;
            return;
        }
        if (i2 != 3) {
            z4 = false;
        }
        this.zzeaj = z4;
        this.zzeak = i2;
    }

    private CredentialPickerConfig(Builder builder) {
        this(2, builder.zzeai, builder.mShowCancelButton, false, builder.zzeal);
    }

    @Deprecated
    public final boolean isForNewAccount() {
        return this.zzeak == 3;
    }

    public final boolean shouldShowAddAccountButton() {
        return this.zzeai;
    }

    public final boolean shouldShowCancelButton() {
        return this.mShowCancelButton;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, shouldShowAddAccountButton());
        zzd.zza(parcel, 2, shouldShowCancelButton());
        zzd.zza(parcel, 3, isForNewAccount());
        zzd.zzc(parcel, 4, this.zzeak);
        zzd.zzc(parcel, 1000, this.zzdxt);
        zzd.zzai(parcel, zze);
    }
}
