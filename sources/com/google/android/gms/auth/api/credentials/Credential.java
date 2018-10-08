package com.google.android.gms.auth.api.credentials;

import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

public class Credential extends zza implements ReflectedParcelable {
    public static final Creator<Credential> CREATOR = new zza();
    public static final String EXTRA_KEY = "com.google.android.gms.credentials.Credential";
    @Nullable
    private final String mName;
    private final String zzbsx;
    @Nullable
    private final String zzdzs;
    @Nullable
    private final Uri zzeab;
    private final List<IdToken> zzeac;
    @Nullable
    private final String zzead;
    @Nullable
    private final String zzeae;
    @Nullable
    private final String zzeaf;
    @Nullable
    private final String zzeag;
    @Nullable
    private final String zzeah;

    public static class Builder {
        private String mName;
        private final String zzbsx;
        private String zzdzs;
        private Uri zzeab;
        private List<IdToken> zzeac;
        private String zzead;
        private String zzeae;
        private String zzeaf;
        private String zzeag;
        private String zzeah;

        public Builder(Credential credential) {
            this.zzbsx = credential.zzbsx;
            this.mName = credential.mName;
            this.zzeab = credential.zzeab;
            this.zzeac = credential.zzeac;
            this.zzead = credential.zzead;
            this.zzdzs = credential.zzdzs;
            this.zzeae = credential.zzeae;
            this.zzeaf = credential.zzeaf;
            this.zzeag = credential.zzeag;
            this.zzeah = credential.zzeah;
        }

        public Builder(String str) {
            this.zzbsx = str;
        }

        public Credential build() {
            return new Credential(this.zzbsx, this.mName, this.zzeab, this.zzeac, this.zzead, this.zzdzs, this.zzeae, this.zzeaf, this.zzeag, this.zzeah);
        }

        public Builder setAccountType(String str) {
            this.zzdzs = str;
            return this;
        }

        public Builder setName(String str) {
            this.mName = str;
            return this;
        }

        public Builder setPassword(String str) {
            this.zzead = str;
            return this;
        }

        public Builder setProfilePictureUri(Uri uri) {
            this.zzeab = uri;
            return this;
        }
    }

    Credential(String str, String str2, Uri uri, List<IdToken> list, String str3, String str4, String str5, String str6, String str7, String str8) {
        String trim = ((String) zzbp.zzb((Object) str, (Object) "credential identifier cannot be null")).trim();
        zzbp.zzh(trim, "credential identifier cannot be empty");
        if (str3 == null || !TextUtils.isEmpty(str3)) {
            if (str4 != null) {
                boolean z;
                if (!TextUtils.isEmpty(str4)) {
                    Uri parse = Uri.parse(str4);
                    if (!parse.isAbsolute() || !parse.isHierarchical() || TextUtils.isEmpty(parse.getScheme()) || TextUtils.isEmpty(parse.getAuthority())) {
                        z = false;
                        if (!Boolean.valueOf(z).booleanValue()) {
                            throw new IllegalArgumentException("Account type must be a valid Http/Https URI");
                        }
                    } else if ("http".equalsIgnoreCase(parse.getScheme()) || "https".equalsIgnoreCase(parse.getScheme())) {
                        z = true;
                        if (Boolean.valueOf(z).booleanValue()) {
                            throw new IllegalArgumentException("Account type must be a valid Http/Https URI");
                        }
                    }
                }
                z = false;
                if (Boolean.valueOf(z).booleanValue()) {
                    throw new IllegalArgumentException("Account type must be a valid Http/Https URI");
                }
            }
            if (TextUtils.isEmpty(str4) || TextUtils.isEmpty(str3)) {
                if (str2 != null && TextUtils.isEmpty(str2.trim())) {
                    str2 = null;
                }
                this.mName = str2;
                this.zzeab = uri;
                this.zzeac = list == null ? Collections.emptyList() : Collections.unmodifiableList(list);
                this.zzbsx = trim;
                this.zzead = str3;
                this.zzdzs = str4;
                this.zzeae = str5;
                this.zzeaf = str6;
                this.zzeag = str7;
                this.zzeah = str8;
                return;
            }
            throw new IllegalArgumentException("Password and AccountType are mutually exclusive");
        }
        throw new IllegalArgumentException("Password must not be empty if set");
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof Credential)) {
                return false;
            }
            Credential credential = (Credential) obj;
            if (!TextUtils.equals(this.zzbsx, credential.zzbsx) || !TextUtils.equals(this.mName, credential.mName) || !zzbf.equal(this.zzeab, credential.zzeab) || !TextUtils.equals(this.zzead, credential.zzead) || !TextUtils.equals(this.zzdzs, credential.zzdzs)) {
                return false;
            }
            if (!TextUtils.equals(this.zzeae, credential.zzeae)) {
                return false;
            }
        }
        return true;
    }

    @Nullable
    public String getAccountType() {
        return this.zzdzs;
    }

    @Nullable
    public String getFamilyName() {
        return this.zzeah;
    }

    @Nullable
    public String getGeneratedPassword() {
        return this.zzeae;
    }

    @Nullable
    public String getGivenName() {
        return this.zzeag;
    }

    public String getId() {
        return this.zzbsx;
    }

    public List<IdToken> getIdTokens() {
        return this.zzeac;
    }

    @Nullable
    public String getName() {
        return this.mName;
    }

    @Nullable
    public String getPassword() {
        return this.zzead;
    }

    @Nullable
    public Uri getProfilePictureUri() {
        return this.zzeab;
    }

    public int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzbsx, this.mName, this.zzeab, this.zzead, this.zzdzs, this.zzeae});
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getId(), false);
        zzd.zza(parcel, 2, getName(), false);
        zzd.zza(parcel, 3, getProfilePictureUri(), i, false);
        zzd.zzc(parcel, 4, getIdTokens(), false);
        zzd.zza(parcel, 5, getPassword(), false);
        zzd.zza(parcel, 6, getAccountType(), false);
        zzd.zza(parcel, 7, getGeneratedPassword(), false);
        zzd.zza(parcel, 8, this.zzeaf, false);
        zzd.zza(parcel, 9, getGivenName(), false);
        zzd.zza(parcel, 10, getFamilyName(), false);
        zzd.zzai(parcel, zze);
    }
}
