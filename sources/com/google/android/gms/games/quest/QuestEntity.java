package com.google.android.gms.games.quest;

import android.database.CharArrayBuffer;
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
import com.google.android.gms.common.util.DataUtils;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameEntity;
import com.google.android.gms.games.internal.zzd;
import java.util.ArrayList;
import java.util.List;

@UsedByReflection("GamesClientImpl.java")
@Class(creator = "QuestEntityCreator")
@Reserved({1000})
@Deprecated
public final class QuestEntity extends zzd implements Quest {
    public static final Creator<QuestEntity> CREATOR = new zzc();
    @Field(getter = "getDescription", mo13990id = 6)
    private final String description;
    @Field(getter = "getName", mo13990id = 12)
    private final String name;
    @Field(getter = "getState", mo13990id = 15)
    private final int state;
    @Field(getter = "getType", mo13990id = 16)
    private final int type;
    @Field(getter = "getLastUpdatedTimestamp", mo13990id = 8)
    private final long zzfm;
    @Field(getter = "getGame", mo13990id = 1)
    private final GameEntity zzlp;
    @Field(getter = "getQuestId", mo13990id = 2)
    private final String zzra;
    @Field(getter = "getAcceptedTimestamp", mo13990id = 3)
    private final long zzrb;
    @Field(getter = "getBannerImageUri", mo13990id = 4)
    private final Uri zzrc;
    @Field(getter = "getBannerImageUrl", mo13990id = 5)
    private final String zzrd;
    @Field(getter = "getEndTimestamp", mo13990id = 7)
    private final long zzre;
    @Field(getter = "getIconImageUri", mo13990id = 9)
    private final Uri zzrf;
    @Field(getter = "getIconImageUrl", mo13990id = 10)
    private final String zzrg;
    @Field(getter = "getNotifyTimestamp", mo13990id = 13)
    private final long zzrh;
    @Field(getter = "getStartTimestamp", mo13990id = 14)
    private final long zzri;
    @Field(getter = "getMilestones", mo13990id = 17)
    private final ArrayList<MilestoneEntity> zzrj;

    @Constructor
    QuestEntity(@Param(mo13993id = 1) GameEntity gameEntity, @Param(mo13993id = 2) String str, @Param(mo13993id = 3) long j, @Param(mo13993id = 4) Uri uri, @Param(mo13993id = 5) String str2, @Param(mo13993id = 6) String str3, @Param(mo13993id = 7) long j2, @Param(mo13993id = 8) long j3, @Param(mo13993id = 9) Uri uri2, @Param(mo13993id = 10) String str4, @Param(mo13993id = 12) String str5, @Param(mo13993id = 13) long j4, @Param(mo13993id = 14) long j5, @Param(mo13993id = 15) int i, @Param(mo13993id = 16) int i2, @Param(mo13993id = 17) ArrayList<MilestoneEntity> arrayList) {
        this.zzlp = gameEntity;
        this.zzra = str;
        this.zzrb = j;
        this.zzrc = uri;
        this.zzrd = str2;
        this.description = str3;
        this.zzre = j2;
        this.zzfm = j3;
        this.zzrf = uri2;
        this.zzrg = str4;
        this.name = str5;
        this.zzrh = j4;
        this.zzri = j5;
        this.state = i;
        this.type = i2;
        this.zzrj = arrayList;
    }

    public QuestEntity(Quest quest) {
        this.zzlp = new GameEntity(quest.getGame());
        this.zzra = quest.getQuestId();
        this.zzrb = quest.getAcceptedTimestamp();
        this.description = quest.getDescription();
        this.zzrc = quest.getBannerImageUri();
        this.zzrd = quest.getBannerImageUrl();
        this.zzre = quest.getEndTimestamp();
        this.zzrf = quest.getIconImageUri();
        this.zzrg = quest.getIconImageUrl();
        this.zzfm = quest.getLastUpdatedTimestamp();
        this.name = quest.getName();
        this.zzrh = quest.zzdr();
        this.zzri = quest.getStartTimestamp();
        this.state = quest.getState();
        this.type = quest.getType();
        List zzdq = quest.zzdq();
        int size = zzdq.size();
        this.zzrj = new ArrayList<>(size);
        for (int i = 0; i < size; i++) {
            this.zzrj.add((MilestoneEntity) ((Milestone) zzdq.get(i)).freeze());
        }
    }

    static int zza(Quest quest) {
        return Objects.hashCode(quest.getGame(), quest.getQuestId(), Long.valueOf(quest.getAcceptedTimestamp()), quest.getBannerImageUri(), quest.getDescription(), Long.valueOf(quest.getEndTimestamp()), quest.getIconImageUri(), Long.valueOf(quest.getLastUpdatedTimestamp()), quest.zzdq(), quest.getName(), Long.valueOf(quest.zzdr()), Long.valueOf(quest.getStartTimestamp()), Integer.valueOf(quest.getState()));
    }

    static boolean zza(Quest quest, Object obj) {
        if (!(obj instanceof Quest)) {
            return false;
        }
        if (quest == obj) {
            return true;
        }
        Quest quest2 = (Quest) obj;
        return Objects.equal(quest2.getGame(), quest.getGame()) && Objects.equal(quest2.getQuestId(), quest.getQuestId()) && Objects.equal(Long.valueOf(quest2.getAcceptedTimestamp()), Long.valueOf(quest.getAcceptedTimestamp())) && Objects.equal(quest2.getBannerImageUri(), quest.getBannerImageUri()) && Objects.equal(quest2.getDescription(), quest.getDescription()) && Objects.equal(Long.valueOf(quest2.getEndTimestamp()), Long.valueOf(quest.getEndTimestamp())) && Objects.equal(quest2.getIconImageUri(), quest.getIconImageUri()) && Objects.equal(Long.valueOf(quest2.getLastUpdatedTimestamp()), Long.valueOf(quest.getLastUpdatedTimestamp())) && Objects.equal(quest2.zzdq(), quest.zzdq()) && Objects.equal(quest2.getName(), quest.getName()) && Objects.equal(Long.valueOf(quest2.zzdr()), Long.valueOf(quest.zzdr())) && Objects.equal(Long.valueOf(quest2.getStartTimestamp()), Long.valueOf(quest.getStartTimestamp())) && Objects.equal(Integer.valueOf(quest2.getState()), Integer.valueOf(quest.getState()));
    }

    static String zzb(Quest quest) {
        return Objects.toStringHelper(quest).add("Game", quest.getGame()).add("QuestId", quest.getQuestId()).add("AcceptedTimestamp", Long.valueOf(quest.getAcceptedTimestamp())).add("BannerImageUri", quest.getBannerImageUri()).add("BannerImageUrl", quest.getBannerImageUrl()).add("Description", quest.getDescription()).add("EndTimestamp", Long.valueOf(quest.getEndTimestamp())).add("IconImageUri", quest.getIconImageUri()).add("IconImageUrl", quest.getIconImageUrl()).add("LastUpdatedTimestamp", Long.valueOf(quest.getLastUpdatedTimestamp())).add("Milestones", quest.zzdq()).add("Name", quest.getName()).add("NotifyTimestamp", Long.valueOf(quest.zzdr())).add("StartTimestamp", Long.valueOf(quest.getStartTimestamp())).add("State", Integer.valueOf(quest.getState())).toString();
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

    public final long getAcceptedTimestamp() {
        return this.zzrb;
    }

    public final Uri getBannerImageUri() {
        return this.zzrc;
    }

    public final String getBannerImageUrl() {
        return this.zzrd;
    }

    public final Milestone getCurrentMilestone() {
        return (Milestone) zzdq().get(0);
    }

    public final String getDescription() {
        return this.description;
    }

    public final void getDescription(CharArrayBuffer charArrayBuffer) {
        DataUtils.copyStringToBuffer(this.description, charArrayBuffer);
    }

    public final long getEndTimestamp() {
        return this.zzre;
    }

    public final Game getGame() {
        return this.zzlp;
    }

    public final Uri getIconImageUri() {
        return this.zzrf;
    }

    public final String getIconImageUrl() {
        return this.zzrg;
    }

    public final long getLastUpdatedTimestamp() {
        return this.zzfm;
    }

    public final String getName() {
        return this.name;
    }

    public final void getName(CharArrayBuffer charArrayBuffer) {
        DataUtils.copyStringToBuffer(this.name, charArrayBuffer);
    }

    public final String getQuestId() {
        return this.zzra;
    }

    public final long getStartTimestamp() {
        return this.zzri;
    }

    public final int getState() {
        return this.state;
    }

    public final int getType() {
        return this.type;
    }

    public final int hashCode() {
        return zza(this);
    }

    public final boolean isDataValid() {
        return true;
    }

    public final boolean isEndingSoon() {
        return this.zzrh <= System.currentTimeMillis() + 1800000;
    }

    public final String toString() {
        return zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeParcelable(parcel, 1, getGame(), i, false);
        SafeParcelWriter.writeString(parcel, 2, getQuestId(), false);
        SafeParcelWriter.writeLong(parcel, 3, getAcceptedTimestamp());
        SafeParcelWriter.writeParcelable(parcel, 4, getBannerImageUri(), i, false);
        SafeParcelWriter.writeString(parcel, 5, getBannerImageUrl(), false);
        SafeParcelWriter.writeString(parcel, 6, getDescription(), false);
        SafeParcelWriter.writeLong(parcel, 7, getEndTimestamp());
        SafeParcelWriter.writeLong(parcel, 8, getLastUpdatedTimestamp());
        SafeParcelWriter.writeParcelable(parcel, 9, getIconImageUri(), i, false);
        SafeParcelWriter.writeString(parcel, 10, getIconImageUrl(), false);
        SafeParcelWriter.writeString(parcel, 12, getName(), false);
        SafeParcelWriter.writeLong(parcel, 13, this.zzrh);
        SafeParcelWriter.writeLong(parcel, 14, getStartTimestamp());
        SafeParcelWriter.writeInt(parcel, 15, getState());
        SafeParcelWriter.writeInt(parcel, 16, this.type);
        SafeParcelWriter.writeTypedList(parcel, 17, zzdq(), false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }

    public final List<Milestone> zzdq() {
        return new ArrayList(this.zzrj);
    }

    public final long zzdr() {
        return this.zzrh;
    }
}
