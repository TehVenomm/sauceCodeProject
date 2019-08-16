package com.google.android.gms.games.appcontent;

import android.os.Bundle;
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
import com.google.android.gms.games.internal.zzc;
import com.google.android.gms.games.internal.zzd;
import java.util.ArrayList;
import java.util.List;

@UsedByReflection("GamesClientImpl.java")
@Class(creator = "AppContentCardEntityCreator")
@Reserved({1000})
public final class AppContentCardEntity extends zzd implements zze {
    public static final Creator<AppContentCardEntity> CREATOR = new zzf();
    @Field(getter = "getDescription", mo13990id = 6)
    private final String description;
    @Field(getter = "getExtras", mo13990id = 7)
    private final Bundle extras;
    @Field(getter = "getType", mo13990id = 13)
    private final String type;
    @Field(getter = "getTitle", mo13990id = 11)
    private final String zzcd;
    @Field(getter = "getConditions", mo13990id = 3)
    private final ArrayList<AppContentConditionEntity> zzfp;
    @Field(getter = "getContentDescription", mo13990id = 4)
    private final String zzfq;
    @Field(getter = "getId", mo13990id = 14)
    private final String zzfr;
    @Field(getter = "getActions", mo13990id = 1)
    private final ArrayList<AppContentActionEntity> zzga;
    @Field(getter = "getAnnotations", mo13990id = 2)
    private final ArrayList<AppContentAnnotationEntity> zzgb;
    @Field(getter = "getCurrentProgress", mo13990id = 5)
    private final int zzgc;
    @Field(getter = "getSubtitle", mo13990id = 10)
    private final String zzgd;
    @Field(getter = "getTotalProgress", mo13990id = 12)
    private final int zzge;

    @Constructor
    AppContentCardEntity(@Param(mo13993id = 1) ArrayList<AppContentActionEntity> arrayList, @Param(mo13993id = 2) ArrayList<AppContentAnnotationEntity> arrayList2, @Param(mo13993id = 3) ArrayList<AppContentConditionEntity> arrayList3, @Param(mo13993id = 4) String str, @Param(mo13993id = 5) int i, @Param(mo13993id = 6) String str2, @Param(mo13993id = 7) Bundle bundle, @Param(mo13993id = 10) String str3, @Param(mo13993id = 11) String str4, @Param(mo13993id = 12) int i2, @Param(mo13993id = 13) String str5, @Param(mo13993id = 14) String str6) {
        this.zzga = arrayList;
        this.zzgb = arrayList2;
        this.zzfp = arrayList3;
        this.zzfq = str;
        this.zzgc = i;
        this.description = str2;
        this.extras = bundle;
        this.zzfr = str6;
        this.zzgd = str3;
        this.zzcd = str4;
        this.zzge = i2;
        this.type = str5;
    }

    public final boolean equals(Object obj) {
        if (obj instanceof zze) {
            if (this == obj) {
                return true;
            }
            zze zze = (zze) obj;
            if (Objects.equal(zze.getActions(), getActions()) && Objects.equal(zze.zzai(), zzai()) && Objects.equal(zze.zzz(), zzz()) && Objects.equal(zze.zzaa(), zzaa()) && Objects.equal(Integer.valueOf(zze.zzaj()), Integer.valueOf(zzaj())) && Objects.equal(zze.getDescription(), getDescription()) && zzc.zza(zze.getExtras(), getExtras()) && Objects.equal(zze.getId(), getId()) && Objects.equal(zze.zzak(), zzak()) && Objects.equal(zze.getTitle(), getTitle()) && Objects.equal(Integer.valueOf(zze.zzal()), Integer.valueOf(zzal())) && Objects.equal(zze.getType(), getType())) {
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

    public final List<zza> getActions() {
        return new ArrayList(this.zzga);
    }

    public final String getDescription() {
        return this.description;
    }

    public final Bundle getExtras() {
        return this.extras;
    }

    public final String getId() {
        return this.zzfr;
    }

    public final String getTitle() {
        return this.zzcd;
    }

    public final String getType() {
        return this.type;
    }

    public final int hashCode() {
        return Objects.hashCode(getActions(), zzai(), zzz(), zzaa(), Integer.valueOf(zzaj()), getDescription(), Integer.valueOf(zzc.zza(getExtras())), getId(), zzak(), getTitle(), Integer.valueOf(zzal()), getType());
    }

    public final boolean isDataValid() {
        return true;
    }

    public final String toString() {
        return Objects.toStringHelper(this).add("Actions", getActions()).add("Annotations", zzai()).add("Conditions", zzz()).add("ContentDescription", zzaa()).add("CurrentSteps", Integer.valueOf(zzaj())).add("Description", getDescription()).add("Extras", getExtras()).add("Id", getId()).add("Subtitle", zzak()).add("Title", getTitle()).add("TotalSteps", Integer.valueOf(zzal())).add("Type", getType()).toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeTypedList(parcel, 1, getActions(), false);
        SafeParcelWriter.writeTypedList(parcel, 2, zzai(), false);
        SafeParcelWriter.writeTypedList(parcel, 3, zzz(), false);
        SafeParcelWriter.writeString(parcel, 4, this.zzfq, false);
        SafeParcelWriter.writeInt(parcel, 5, this.zzgc);
        SafeParcelWriter.writeString(parcel, 6, this.description, false);
        SafeParcelWriter.writeBundle(parcel, 7, this.extras, false);
        SafeParcelWriter.writeString(parcel, 10, this.zzgd, false);
        SafeParcelWriter.writeString(parcel, 11, this.zzcd, false);
        SafeParcelWriter.writeInt(parcel, 12, this.zzge);
        SafeParcelWriter.writeString(parcel, 13, this.type, false);
        SafeParcelWriter.writeString(parcel, 14, this.zzfr, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }

    public final String zzaa() {
        return this.zzfq;
    }

    public final List<zzc> zzai() {
        return new ArrayList(this.zzgb);
    }

    public final int zzaj() {
        return this.zzgc;
    }

    public final String zzak() {
        return this.zzgd;
    }

    public final int zzal() {
        return this.zzge;
    }

    public final List<zzg> zzz() {
        return new ArrayList(this.zzfp);
    }
}
