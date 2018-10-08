package com.google.android.gms.auth.api.credentials;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;

public final class CredentialRequest extends zza {
    public static final Creator<CredentialRequest> CREATOR = new zze();
    private int zzdxt;
    private final boolean zzeam;
    private final String[] zzean;
    private final CredentialPickerConfig zzeao;
    private final CredentialPickerConfig zzeap;
    private final boolean zzeaq;
    private final String zzear;
    private final String zzeas;
    private final boolean zzeat;

    public static final class Builder {
        private boolean zzeam;
        private String[] zzean;
        private CredentialPickerConfig zzeao;
        private CredentialPickerConfig zzeap;
        private boolean zzeaq = false;
        @Nullable
        private String zzear = null;
        @Nullable
        private String zzeas;
        private boolean zzeat = false;

        public final CredentialRequest build() {
            if (this.zzean == null) {
                this.zzean = new String[0];
            }
            if (this.zzeam || this.zzean.length != 0) {
                return new CredentialRequest();
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

        public final Builder setCredentialHintPickerConfig(CredentialPickerConfig credentialPickerConfig) {
            this.zzeap = credentialPickerConfig;
            return this;
        }

        public final Builder setCredentialPickerConfig(CredentialPickerConfig credentialPickerConfig) {
            this.zzeao = credentialPickerConfig;
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

        public final Builder setPasswordLoginSupported(boolean z) {
            this.zzeam = z;
            return this;
        }

        public final Builder setServerClientId(@Nullable String str) {
            this.zzear = str;
            return this;
        }

        @Deprecated
        public final Builder setSupportsPasswordLogin(boolean z) {
            return setPasswordLoginSupported(z);
        }
    }

    CredentialRequest(int i, boolean z, String[] strArr, CredentialPickerConfig credentialPickerConfig, CredentialPickerConfig credentialPickerConfig2, boolean z2, String str, String str2, boolean z3) {
        this.zzdxt = i;
        this.zzeam = z;
        this.zzean = (String[]) zzbp.zzu(strArr);
        if (credentialPickerConfig == null) {
            credentialPickerConfig = new com.google.android.gms.auth.api.credentials.CredentialPickerConfig.Builder().build();
        }
        this.zzeao = credentialPickerConfig;
        if (credentialPickerConfig2 == null) {
            credentialPickerConfig2 = new com.google.android.gms.auth.api.credentials.CredentialPickerConfig.Builder().build();
        }
        this.zzeap = credentialPickerConfig2;
        if (i < 3) {
            this.zzeaq = true;
            this.zzear = null;
            this.zzeas = null;
        } else {
            this.zzeaq = z2;
            this.zzear = str;
            this.zzeas = str2;
        }
        this.zzeat = z3;
    }

    private CredentialRequest(Builder builder) {
        this(4, builder.zzeam, builder.zzean, builder.zzeao, builder.zzeap, builder.zzeaq, builder.zzear, builder.zzeas, false);
    }

    @NonNull
    public final String[] getAccountTypes() {
        return this.zzean;
    }

    @NonNull
    public final Set<String> getAccountTypesSet() {
        return new HashSet(Arrays.asList(this.zzean));
    }

    @NonNull
    public final CredentialPickerConfig getCredentialHintPickerConfig() {
        return this.zzeap;
    }

    @NonNull
    public final CredentialPickerConfig getCredentialPickerConfig() {
        return this.zzeao;
    }

    @Nullable
    public final String getIdTokenNonce() {
        return this.zzeas;
    }

    @Nullable
    public final String getServerClientId() {
        return this.zzear;
    }

    @Deprecated
    public final boolean getSupportsPasswordLogin() {
        return isPasswordLoginSupported();
    }

    public final boolean isIdTokenRequested() {
        return this.zzeaq;
    }

    public final boolean isPasswordLoginSupported() {
        return this.zzeam;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, isPasswordLoginSupported());
        zzd.zza(parcel, 2, getAccountTypes(), false);
        zzd.zza(parcel, 3, getCredentialPickerConfig(), i, false);
        zzd.zza(parcel, 4, getCredentialHintPickerConfig(), i, false);
        zzd.zza(parcel, 5, isIdTokenRequested());
        zzd.zza(parcel, 6, getServerClientId(), false);
        zzd.zza(parcel, 7, getIdTokenNonce(), false);
        zzd.zzc(parcel, 1000, this.zzdxt);
        zzd.zza(parcel, 8, this.zzeat);
        zzd.zzai(parcel, zze);
    }
}
