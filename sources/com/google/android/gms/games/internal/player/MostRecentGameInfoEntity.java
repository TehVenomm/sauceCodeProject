package com.google.android.gms.games.internal.player;

import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.apps.common.proguard.UsedByReflection;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.games.internal.zzd;

@UsedByReflection("GamesClientImpl.java")
@Class(creator = "MostRecentGameInfoEntityCreator")
@Reserved({1000})
public final class MostRecentGameInfoEntity extends zzd implements zza {
    public static final Creator<MostRecentGameInfoEntity> CREATOR = new zzb();
    @Field(getter = "getGameId", mo13990id = 1)
    private final String zzlw;
    @Field(getter = "getGameName", mo13990id = 2)
    private final String zzlx;
    @Field(getter = "getActivityTimestampMillis", mo13990id = 3)
    private final long zzly;
    @Field(getter = "getGameIconImageUri", mo13990id = 4)
    private final Uri zzlz;
    @Field(getter = "getGameHiResImageUri", mo13990id = 5)
    private final Uri zzma;
    @Field(getter = "getGameFeaturedImageUri", mo13990id = 6)
    private final Uri zzmb;

    public MostRecentGameInfoEntity(zza zza) {
        this.zzlw = zza.zzdb();
        this.zzlx = zza.zzdc();
        this.zzly = zza.zzdd();
        this.zzlz = zza.zzde();
        this.zzma = zza.zzdf();
        this.zzmb = zza.zzdg();
    }

    @Constructor
    MostRecentGameInfoEntity(@Param(mo13993id = 1) String str, @Param(mo13993id = 2) String str2, @Param(mo13993id = 3) long j, @Param(mo13993id = 4) Uri uri, @Param(mo13993id = 5) Uri uri2, @Param(mo13993id = 6) Uri uri3) {
        this.zzlw = str;
        this.zzlx = str2;
        this.zzly = j;
        this.zzlz = uri;
        this.zzma = uri2;
        this.zzmb = uri3;
    }

    static int zza(zza zza) {
        return Objects.hashCode(zza.zzdb(), zza.zzdc(), Long.valueOf(zza.zzdd()), zza.zzde(), zza.zzdf(), zza.zzdg());
    }

    static boolean zza(zza zza, Object obj) {
        if (!(obj instanceof zza)) {
            return false;
        }
        if (zza == obj) {
            return true;
        }
        zza zza2 = (zza) obj;
        return Objects.equal(zza2.zzdb(), zza.zzdb()) && Objects.equal(zza2.zzdc(), zza.zzdc()) && Objects.equal(Long.valueOf(zza2.zzdd()), Long.valueOf(zza.zzdd())) && Objects.equal(zza2.zzde(), zza.zzde()) && Objects.equal(zza2.zzdf(), zza.zzdf()) && Objects.equal(zza2.zzdg(), zza.zzdg());
    }

    static String zzb(zza zza) {
        return Objects.toStringHelper(zza).add("GameId", zza.zzdb()).add("GameName", zza.zzdc()).add("ActivityTimestampMillis", Long.valueOf(zza.zzdd())).add("GameIconUri", zza.zzde()).add("GameHiResUri", zza.zzdf()).add("GameFeaturedUri", zza.zzdg()).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final /* bridge */ /* synthetic */ Object freeze() {
        if (this != null) {
            return this;
        }
        throw null;
    }

    public final int hashCode() {
        return zza(this);
    }

    public final boolean isDataValid() {
        return true;
    }

    public final String toString() {
        return zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeString(parcel, 1, this.zzlw, false);
        SafeParcelWriter.writeString(parcel, 2, this.zzlx, false);
        SafeParcelWriter.writeLong(parcel, 3, this.zzly);
        SafeParcelWriter.writeParcelable(parcel, 4, this.zzlz, i, false);
        SafeParcelWriter.writeParcelable(parcel, 5, this.zzma, i, false);
        SafeParcelWriter.writeParcelable(parcel, 6, this.zzmb, i, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }

    public final String zzdb() {
        return this.zzlw;
    }

    public final String zzdc() {
        return this.zzlx;
    }

    public final long zzdd() {
        return this.zzly;
    }

    public final Uri zzde() {
        return this.zzlz;
    }

    public final Uri zzdf() {
        return this.zzma;
    }

    public final Uri zzdg() {
        return this.zzmb;
    }
}
