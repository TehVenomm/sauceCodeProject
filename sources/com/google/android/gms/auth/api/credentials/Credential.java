package com.google.android.gms.auth.api.credentials;

import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import java.util.List;
import javax.annotation.Nonnull;

@Class(creator = "CredentialCreator")
@Reserved({1000})
public class Credential extends AbstractSafeParcelable implements ReflectedParcelable {
    public static final Creator<Credential> CREATOR = new zzc();
    public static final String EXTRA_KEY = "com.google.android.gms.credentials.Credential";
    /* access modifiers changed from: private */
    @Field(getter = "getId", mo13990id = 1)
    @Nonnull
    public final String mId;
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getName", mo13990id = 2)
    public final String mName;
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getProfilePictureUri", mo13990id = 3)
    public final Uri zzo;
    /* access modifiers changed from: private */
    @Field(getter = "getIdTokens", mo13990id = 4)
    @Nonnull
    public final List<IdToken> zzp;
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getPassword", mo13990id = 5)
    public final String zzq;
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getAccountType", mo13990id = 6)
    public final String zzr;
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getGivenName", mo13990id = 9)
    public final String zzs;
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getFamilyName", mo13990id = 10)
    public final String zzt;

    public static class Builder {
        private final String mId;
        private String mName;
        private Uri zzo;
        private List<IdToken> zzp;
        private String zzq;
        private String zzr;
        private String zzs;
        private String zzt;

        public Builder(Credential credential) {
            this.mId = credential.mId;
            this.mName = credential.mName;
            this.zzo = credential.zzo;
            this.zzp = credential.zzp;
            this.zzq = credential.zzq;
            this.zzr = credential.zzr;
            this.zzs = credential.zzs;
            this.zzt = credential.zzt;
        }

        public Builder(String str) {
            this.mId = str;
        }

        public Credential build() {
            return new Credential(this.mId, this.mName, this.zzo, this.zzp, this.zzq, this.zzr, this.zzs, this.zzt);
        }

        public Builder setAccountType(String str) {
            this.zzr = str;
            return this;
        }

        public Builder setName(String str) {
            this.mName = str;
            return this;
        }

        public Builder setPassword(String str) {
            this.zzq = str;
            return this;
        }

        public Builder setProfilePictureUri(Uri uri) {
            this.zzo = uri;
            return this;
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:20:0x005c  */
    @com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor
    /* Code decompiled incorrectly, please refer to instructions dump. */
    Credential(@com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param(mo13993id = 1) java.lang.String r6, @com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param(mo13993id = 2) java.lang.String r7, @com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param(mo13993id = 3) android.net.Uri r8, @com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param(mo13993id = 4) java.util.List<com.google.android.gms.auth.api.credentials.IdToken> r9, @com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param(mo13993id = 5) java.lang.String r10, @com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param(mo13993id = 6) java.lang.String r11, @com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param(mo13993id = 9) java.lang.String r12, @com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param(mo13993id = 10) java.lang.String r13) {
        /*
            r5 = this;
            r1 = 0
            r5.<init>()
            java.lang.String r0 = "credential identifier cannot be null"
            java.lang.Object r0 = com.google.android.gms.common.internal.Preconditions.checkNotNull(r6, r0)
            java.lang.String r0 = (java.lang.String) r0
            java.lang.String r2 = r0.trim()
            java.lang.String r0 = "credential identifier cannot be empty"
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r2, r0)
            if (r10 == 0) goto L_0x0025
            boolean r0 = android.text.TextUtils.isEmpty(r10)
            if (r0 == 0) goto L_0x0025
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.String r1 = "Password must not be empty if set"
            r0.<init>(r1)
            throw r0
        L_0x0025:
            if (r11 == 0) goto L_0x0080
            boolean r0 = android.text.TextUtils.isEmpty(r11)
            if (r0 != 0) goto L_0x007e
            android.net.Uri r0 = android.net.Uri.parse(r11)
            boolean r3 = r0.isAbsolute()
            if (r3 == 0) goto L_0x0051
            boolean r3 = r0.isHierarchical()
            if (r3 == 0) goto L_0x0051
            java.lang.String r3 = r0.getScheme()
            boolean r3 = android.text.TextUtils.isEmpty(r3)
            if (r3 != 0) goto L_0x0051
            java.lang.String r3 = r0.getAuthority()
            boolean r3 = android.text.TextUtils.isEmpty(r3)
            if (r3 == 0) goto L_0x0064
        L_0x0051:
            r0 = r1
        L_0x0052:
            java.lang.Boolean r0 = java.lang.Boolean.valueOf(r0)
            boolean r0 = r0.booleanValue()
            if (r0 != 0) goto L_0x0080
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.String r1 = "Account type must be a valid Http/Https URI"
            r0.<init>(r1)
            throw r0
        L_0x0064:
            java.lang.String r3 = "http"
            java.lang.String r4 = r0.getScheme()
            boolean r3 = r3.equalsIgnoreCase(r4)
            if (r3 != 0) goto L_0x007c
            java.lang.String r3 = "https"
            java.lang.String r0 = r0.getScheme()
            boolean r0 = r3.equalsIgnoreCase(r0)
            if (r0 == 0) goto L_0x007e
        L_0x007c:
            r0 = 1
            goto L_0x0052
        L_0x007e:
            r0 = r1
            goto L_0x0052
        L_0x0080:
            boolean r0 = android.text.TextUtils.isEmpty(r11)
            if (r0 != 0) goto L_0x0094
            boolean r0 = android.text.TextUtils.isEmpty(r10)
            if (r0 != 0) goto L_0x0094
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.String r1 = "Password and AccountType are mutually exclusive"
            r0.<init>(r1)
            throw r0
        L_0x0094:
            if (r7 == 0) goto L_0x00a1
            java.lang.String r0 = r7.trim()
            boolean r0 = android.text.TextUtils.isEmpty(r0)
            if (r0 == 0) goto L_0x00a1
            r7 = 0
        L_0x00a1:
            r5.mName = r7
            r5.zzo = r8
            if (r9 != 0) goto L_0x00b8
            java.util.List r0 = java.util.Collections.emptyList()
        L_0x00ab:
            r5.zzp = r0
            r5.mId = r2
            r5.zzq = r10
            r5.zzr = r11
            r5.zzs = r12
            r5.zzt = r13
            return
        L_0x00b8:
            java.util.List r0 = java.util.Collections.unmodifiableList(r9)
            goto L_0x00ab
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.auth.api.credentials.Credential.<init>(java.lang.String, java.lang.String, android.net.Uri, java.util.List, java.lang.String, java.lang.String, java.lang.String, java.lang.String):void");
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof Credential)) {
                return false;
            }
            Credential credential = (Credential) obj;
            if (!TextUtils.equals(this.mId, credential.mId) || !TextUtils.equals(this.mName, credential.mName) || !Objects.equal(this.zzo, credential.zzo) || !TextUtils.equals(this.zzq, credential.zzq) || !TextUtils.equals(this.zzr, credential.zzr)) {
                return false;
            }
        }
        return true;
    }

    @Nullable
    public String getAccountType() {
        return this.zzr;
    }

    @Nullable
    public String getFamilyName() {
        return this.zzt;
    }

    @Nullable
    public String getGivenName() {
        return this.zzs;
    }

    @Nonnull
    public String getId() {
        return this.mId;
    }

    @Nonnull
    public List<IdToken> getIdTokens() {
        return this.zzp;
    }

    @Nullable
    public String getName() {
        return this.mName;
    }

    @Nullable
    public String getPassword() {
        return this.zzq;
    }

    @Nullable
    public Uri getProfilePictureUri() {
        return this.zzo;
    }

    public int hashCode() {
        return Objects.hashCode(this.mId, this.mName, this.zzo, this.zzq, this.zzr);
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeString(parcel, 1, getId(), false);
        SafeParcelWriter.writeString(parcel, 2, getName(), false);
        SafeParcelWriter.writeParcelable(parcel, 3, getProfilePictureUri(), i, false);
        SafeParcelWriter.writeTypedList(parcel, 4, getIdTokens(), false);
        SafeParcelWriter.writeString(parcel, 5, getPassword(), false);
        SafeParcelWriter.writeString(parcel, 6, getAccountType(), false);
        SafeParcelWriter.writeString(parcel, 9, getGivenName(), false);
        SafeParcelWriter.writeString(parcel, 10, getFamilyName(), false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
