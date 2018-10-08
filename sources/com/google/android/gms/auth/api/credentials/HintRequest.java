package com.google.android.gms.auth.api.credentials;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class HintRequest extends zza implements ReflectedParcelable {
    public static final Creator<HintRequest> CREATOR = new zzg();
    private int zzdxt;
    private final String[] zzean;
    private final boolean zzeaq;
    private final String zzear;
    private final String zzeas;
    private final CredentialPickerConfig zzeau;
    private final boolean zzeav;
    private final boolean zzeaw;

    public static final class Builder {
        private String[] zzean;
        private boolean zzeaq = false;
        private String zzear;
        private String zzeas;
        private CredentialPickerConfig zzeau = new com.google.android.gms.auth.api.credentials.CredentialPickerConfig.Builder().build();
        private boolean zzeav;
        private boolean zzeaw;

        public final HintRequest build() {
            if (this.zzean == null) {
                this.zzean = new String[0];
            }
            if (this.zzeav || this.zzeaw || this.zzean.length != 0) {
                return new HintRequest();
            }
            throw new IllegalStateException("At least one authentication method must be specified");
        }

        public final Builder setAccountTypes(String... strArr) {
            if (strArr == null) {
                strArr = new String[0];
            }
            this.zzean = strArr;
            return this;
        }

        public final Builder setEmailAddressIdentifierSupported(boolean z) {
            this.zzeav = z;
            return this;
        }

        public final Builder setHintPickerConfig(@NonNull CredentialPickerConfig credentialPickerConfig) {
            this.zzeau = (CredentialPickerConfig) zzbp.zzu(credentialPickerConfig);
            return this;
        }

        public final Builder setIdTokenNonce(@Nullable String str) {
            this.zzeas = str;
            return this;
        }

        public final Builder setIdTokenRequested(boolean z) {
            this.zzeaq = z;
            return this;
        }

        public final Builder setPhoneNumberIdentifierSupported(boolean z) {
            this.zzeaw = z;
            return this;
        }

        public final Builder setServerClientId(@Nullable String str) {
            this.zzear = str;
            return this;
        }
    }

    HintRequest(int i, CredentialPickerConfig credentialPickerConfig, boolean z, boolean z2, String[] strArr, boolean z3, String str, String str2) {
        this.zzdxt = i;
        this.zzeau = (CredentialPickerConfig) zzbp.zzu(credentialPickerConfig);
        this.zzeav = z;
        this.zzeaw = z2;
        this.zzean = (String[]) zzbp.zzu(strArr);
        if (this.zzdxt < 2) {
            this.zzeaq = true;
            this.zzear = null;
            this.zzeas = null;
            return;
        }
        this.zzeaq = z3;
        this.zzear = str;
        this.zzeas = str2;
    }

    private HintRequest(Builder builder) {
        this(2, builder.zzeau, builder.zzeav, builder.zzeaw, builder.zzean, builder.zzeaq, builder.zzear, builder.zzeas);
    }

    @NonNull
    public final String[] getAccountTypes() {
        return this.zzean;
    }

    @NonNull
    public final CredentialPickerConfig getHintPickerConfig() {
        return this.zzeau;
    }

    @Nullable
    public final String getIdTokenNonce() {
        return this.zzeas;
    }

    @Nullable
    public final String getServerClientId() {
        return this.zzear;
    }

    public final boolean isEmailAddressIdentifierSupported() {
        return this.zzeav;
    }

    public final boolean isIdTokenRequested() {
        return this.zzeaq;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getHintPickerConfig(), i, false);
        zzd.zza(parcel, 2, isEmailAddressIdentifierSupported());
        zzd.zza(parcel, 3, this.zzeaw);
        zzd.zza(parcel, 4, getAccountTypes(), false);
        zzd.zza(parcel, 5, isIdTokenRequested());
        zzd.zza(parcel, 6, getServerClientId(), false);
        zzd.zza(parcel, 7, getIdTokenNonce(), false);
        zzd.zzc(parcel, 1000, this.zzdxt);
        zzd.zzai(parcel, zze);
    }
}
