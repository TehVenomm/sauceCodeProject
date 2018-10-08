package com.google.android.gms.games.internal.experience;

import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameEntity;
import com.google.android.gms.games.internal.zzc;
import java.util.Arrays;

public final class ExperienceEventEntity extends zzc implements ExperienceEvent {
    public static final Creator<ExperienceEventEntity> CREATOR = new zza();
    private final int zzeda;
    private final Uri zzhbd;
    private final String zzhbo;
    private final String zzhiv;
    private final GameEntity zzhiw;
    private final String zzhix;
    private final String zzhiy;
    private final long zzhiz;
    private final long zzhja;
    private final long zzhjb;
    private final int zzhjc;

    ExperienceEventEntity(String str, GameEntity gameEntity, String str2, String str3, String str4, Uri uri, long j, long j2, long j3, int i, int i2) {
        this.zzhiv = str;
        this.zzhiw = gameEntity;
        this.zzhix = str2;
        this.zzhiy = str3;
        this.zzhbo = str4;
        this.zzhbd = uri;
        this.zzhiz = j;
        this.zzhja = j2;
        this.zzhjb = j3;
        this.zzeda = i;
        this.zzhjc = i2;
    }

    public final boolean equals(Object obj) {
        if (obj instanceof ExperienceEvent) {
            if (this == obj) {
                return true;
            }
            ExperienceEvent experienceEvent = (ExperienceEvent) obj;
            if (zzbf.equal(experienceEvent.zzarb(), zzarb()) && zzbf.equal(experienceEvent.getGame(), getGame()) && zzbf.equal(experienceEvent.zzarc(), zzarc()) && zzbf.equal(experienceEvent.zzard(), zzard()) && zzbf.equal(experienceEvent.getIconImageUrl(), getIconImageUrl()) && zzbf.equal(experienceEvent.getIconImageUri(), getIconImageUri()) && zzbf.equal(Long.valueOf(experienceEvent.zzare()), Long.valueOf(zzare())) && zzbf.equal(Long.valueOf(experienceEvent.zzarf()), Long.valueOf(zzarf())) && zzbf.equal(Long.valueOf(experienceEvent.zzarg()), Long.valueOf(zzarg())) && zzbf.equal(Integer.valueOf(experienceEvent.getType()), Integer.valueOf(getType())) && zzbf.equal(Integer.valueOf(experienceEvent.zzarh()), Integer.valueOf(zzarh()))) {
                return true;
            }
        }
        return false;
    }

    public final /* bridge */ /* synthetic */ Object freeze() {
        if (this != null) {
            return this;
        }
        throw null;
    }

    public final Game getGame() {
        return this.zzhiw;
    }

    public final Uri getIconImageUri() {
        return this.zzhbd;
    }

    public final String getIconImageUrl() {
        return this.zzhbo;
    }

    public final int getType() {
        return this.zzeda;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{zzarb(), getGame(), zzarc(), zzard(), getIconImageUrl(), getIconImageUri(), Long.valueOf(zzare()), Long.valueOf(zzarf()), Long.valueOf(zzarg()), Integer.valueOf(getType()), Integer.valueOf(zzarh())});
    }

    public final boolean isDataValid() {
        return true;
    }

    public final String toString() {
        return zzbf.zzt(this).zzg("ExperienceId", zzarb()).zzg("Game", getGame()).zzg("DisplayTitle", zzarc()).zzg("DisplayDescription", zzard()).zzg("IconImageUrl", getIconImageUrl()).zzg("IconImageUri", getIconImageUri()).zzg("CreatedTimestamp", Long.valueOf(zzare())).zzg("XpEarned", Long.valueOf(zzarf())).zzg("CurrentXp", Long.valueOf(zzarg())).zzg("Type", Integer.valueOf(getType())).zzg("NewLevel", Integer.valueOf(zzarh())).toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzhiv, false);
        zzd.zza(parcel, 2, this.zzhiw, i, false);
        zzd.zza(parcel, 3, this.zzhix, false);
        zzd.zza(parcel, 4, this.zzhiy, false);
        zzd.zza(parcel, 5, getIconImageUrl(), false);
        zzd.zza(parcel, 6, this.zzhbd, i, false);
        zzd.zza(parcel, 7, this.zzhiz);
        zzd.zza(parcel, 8, this.zzhja);
        zzd.zza(parcel, 9, this.zzhjb);
        zzd.zzc(parcel, 10, this.zzeda);
        zzd.zzc(parcel, 11, this.zzhjc);
        zzd.zzai(parcel, zze);
    }

    public final String zzarb() {
        return this.zzhiv;
    }

    public final String zzarc() {
        return this.zzhix;
    }

    public final String zzard() {
        return this.zzhiy;
    }

    public final long zzare() {
        return this.zzhiz;
    }

    public final long zzarf() {
        return this.zzhja;
    }

    public final long zzarg() {
        return this.zzhjb;
    }

    public final int zzarh() {
        return this.zzhjc;
    }
}
