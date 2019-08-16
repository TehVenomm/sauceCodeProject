package com.google.android.gms.games.internal.player;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.apps.common.proguard.UsedByReflection;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.games.Players.zza;
import com.google.android.gms.games.internal.zzd;

@UsedByReflection("GamesClientImpl.java")
@Class(creator = "ProfileSettingsEntityCreator")
@Reserved({1000})
public class ProfileSettingsEntity extends zzd implements zza {
    public static final Creator<ProfileSettingsEntity> CREATOR = new zze();
    @Field(getter = "isProfileVisible", mo13990id = 4)
    private final boolean zzcg;
    @Field(getter = "getGamerTag", mo13990id = 2)
    private final String zzci;
    @Field(getter = "getStatus", mo13990id = 1)
    private final Status zzhl;
    @Field(getter = "isGamerTagExplicitlySet", mo13990id = 3)
    private final boolean zznj;
    @Field(getter = "isVisibilityExplicitlySet", mo13990id = 5)
    private final boolean zznk;
    @Field(getter = "getStockProfileImage", mo13990id = 6)
    private final StockProfileImageEntity zznl;
    @Field(getter = "isProfileDiscoverable", mo13990id = 7)
    private final boolean zznm;
    @Field(getter = "isAutoSignInEnabled", mo13990id = 8)
    private final boolean zznn;
    @Field(getter = "getHttpErrorCode", mo13990id = 9)
    private final int zzno;
    @Field(getter = "isSettingsChangesProhibited", mo13990id = 10)
    private final boolean zznp;

    @Constructor
    ProfileSettingsEntity(@Param(mo13993id = 1) Status status, @Param(mo13993id = 2) String str, @Param(mo13993id = 3) boolean z, @Param(mo13993id = 4) boolean z2, @Param(mo13993id = 5) boolean z3, @Param(mo13993id = 6) StockProfileImageEntity stockProfileImageEntity, @Param(mo13993id = 7) boolean z4, @Param(mo13993id = 8) boolean z5, @Param(mo13993id = 9) int i, @Param(mo13993id = 10) boolean z6) {
        this.zzhl = status;
        this.zzci = str;
        this.zznj = z;
        this.zzcg = z2;
        this.zznk = z3;
        this.zznl = stockProfileImageEntity;
        this.zznm = z4;
        this.zznn = z5;
        this.zzno = i;
        this.zznp = z6;
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof zza)) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        zza zza = (zza) obj;
        return Objects.equal(this.zzci, zza.zzh()) && Objects.equal(Boolean.valueOf(this.zznj), Boolean.valueOf(zza.zzr())) && Objects.equal(Boolean.valueOf(this.zzcg), Boolean.valueOf(zza.zzk())) && Objects.equal(Boolean.valueOf(this.zznk), Boolean.valueOf(zza.zzp())) && Objects.equal(this.zzhl, zza.getStatus()) && Objects.equal(this.zznl, zza.zzq()) && Objects.equal(Boolean.valueOf(this.zznm), Boolean.valueOf(zza.zzs())) && Objects.equal(Boolean.valueOf(this.zznn), Boolean.valueOf(zza.zzt())) && this.zzno == zza.zzv() && this.zznp == zza.zzu();
    }

    public Status getStatus() {
        return this.zzhl;
    }

    public int hashCode() {
        return Objects.hashCode(this.zzci, Boolean.valueOf(this.zznj), Boolean.valueOf(this.zzcg), Boolean.valueOf(this.zznk), this.zzhl, this.zznl, Boolean.valueOf(this.zznm), Boolean.valueOf(this.zznn), Integer.valueOf(this.zzno), Boolean.valueOf(this.zznp));
    }

    public String toString() {
        return Objects.toStringHelper(this).add("GamerTag", this.zzci).add("IsGamerTagExplicitlySet", Boolean.valueOf(this.zznj)).add("IsProfileVisible", Boolean.valueOf(this.zzcg)).add("IsVisibilityExplicitlySet", Boolean.valueOf(this.zznk)).add("Status", this.zzhl).add("StockProfileImage", this.zznl).add("IsProfileDiscoverable", Boolean.valueOf(this.zznm)).add("AutoSignIn", Boolean.valueOf(this.zznn)).add("httpErrorCode", Integer.valueOf(this.zzno)).add("IsSettingsChangesProhibited", Boolean.valueOf(this.zznp)).toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeParcelable(parcel, 1, getStatus(), i, false);
        SafeParcelWriter.writeString(parcel, 2, this.zzci, false);
        SafeParcelWriter.writeBoolean(parcel, 3, this.zznj);
        SafeParcelWriter.writeBoolean(parcel, 4, this.zzcg);
        SafeParcelWriter.writeBoolean(parcel, 5, this.zznk);
        SafeParcelWriter.writeParcelable(parcel, 6, this.zznl, i, false);
        SafeParcelWriter.writeBoolean(parcel, 7, this.zznm);
        SafeParcelWriter.writeBoolean(parcel, 8, this.zznn);
        SafeParcelWriter.writeInt(parcel, 9, this.zzno);
        SafeParcelWriter.writeBoolean(parcel, 10, this.zznp);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }

    public final String zzh() {
        return this.zzci;
    }

    public final boolean zzk() {
        return this.zzcg;
    }

    public final boolean zzp() {
        return this.zznk;
    }

    public final StockProfileImage zzq() {
        return this.zznl;
    }

    public final boolean zzr() {
        return this.zznj;
    }

    public final boolean zzs() {
        return this.zznm;
    }

    public final boolean zzt() {
        return this.zznn;
    }

    public final boolean zzu() {
        return this.zznp;
    }

    public final int zzv() {
        return this.zzno;
    }
}
