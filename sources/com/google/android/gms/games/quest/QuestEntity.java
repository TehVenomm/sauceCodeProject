package com.google.android.gms.games.quest;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.util.zzg;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameEntity;
import com.google.android.gms.games.internal.zzc;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

@Deprecated
public final class QuestEntity extends zzc implements Quest {
    public static final Creator<QuestEntity> CREATOR = new zzc();
    private final String mName;
    private final int mState;
    private final String zzdmz;
    private final int zzeda;
    private final long zzhdu;
    private final GameEntity zzhiw;
    private final String zzhnr;
    private final long zzhns;
    private final Uri zzhnt;
    private final String zzhnu;
    private final long zzhnv;
    private final Uri zzhnw;
    private final String zzhnx;
    private final long zzhny;
    private final long zzhnz;
    private final ArrayList<MilestoneEntity> zzhoa;

    QuestEntity(GameEntity gameEntity, String str, long j, Uri uri, String str2, String str3, long j2, long j3, Uri uri2, String str4, String str5, long j4, long j5, int i, int i2, ArrayList<MilestoneEntity> arrayList) {
        this.zzhiw = gameEntity;
        this.zzhnr = str;
        this.zzhns = j;
        this.zzhnt = uri;
        this.zzhnu = str2;
        this.zzdmz = str3;
        this.zzhnv = j2;
        this.zzhdu = j3;
        this.zzhnw = uri2;
        this.zzhnx = str4;
        this.mName = str5;
        this.zzhny = j4;
        this.zzhnz = j5;
        this.mState = i;
        this.zzeda = i2;
        this.zzhoa = arrayList;
    }

    public QuestEntity(Quest quest) {
        this.zzhiw = new GameEntity(quest.getGame());
        this.zzhnr = quest.getQuestId();
        this.zzhns = quest.getAcceptedTimestamp();
        this.zzdmz = quest.getDescription();
        this.zzhnt = quest.getBannerImageUri();
        this.zzhnu = quest.getBannerImageUrl();
        this.zzhnv = quest.getEndTimestamp();
        this.zzhnw = quest.getIconImageUri();
        this.zzhnx = quest.getIconImageUrl();
        this.zzhdu = quest.getLastUpdatedTimestamp();
        this.mName = quest.getName();
        this.zzhny = quest.zzarx();
        this.zzhnz = quest.getStartTimestamp();
        this.mState = quest.getState();
        this.zzeda = quest.getType();
        List zzarw = quest.zzarw();
        int size = zzarw.size();
        this.zzhoa = new ArrayList(size);
        for (int i = 0; i < size; i++) {
            this.zzhoa.add((MilestoneEntity) ((Milestone) zzarw.get(i)).freeze());
        }
    }

    static int zza(Quest quest) {
        return Arrays.hashCode(new Object[]{quest.getGame(), quest.getQuestId(), Long.valueOf(quest.getAcceptedTimestamp()), quest.getBannerImageUri(), quest.getDescription(), Long.valueOf(quest.getEndTimestamp()), quest.getIconImageUri(), Long.valueOf(quest.getLastUpdatedTimestamp()), quest.zzarw(), quest.getName(), Long.valueOf(quest.zzarx()), Long.valueOf(quest.getStartTimestamp()), Integer.valueOf(quest.getState())});
    }

    static boolean zza(Quest quest, Object obj) {
        if (!(obj instanceof Quest)) {
            return false;
        }
        if (quest == obj) {
            return true;
        }
        Quest quest2 = (Quest) obj;
        return zzbf.equal(quest2.getGame(), quest.getGame()) && zzbf.equal(quest2.getQuestId(), quest.getQuestId()) && zzbf.equal(Long.valueOf(quest2.getAcceptedTimestamp()), Long.valueOf(quest.getAcceptedTimestamp())) && zzbf.equal(quest2.getBannerImageUri(), quest.getBannerImageUri()) && zzbf.equal(quest2.getDescription(), quest.getDescription()) && zzbf.equal(Long.valueOf(quest2.getEndTimestamp()), Long.valueOf(quest.getEndTimestamp())) && zzbf.equal(quest2.getIconImageUri(), quest.getIconImageUri()) && zzbf.equal(Long.valueOf(quest2.getLastUpdatedTimestamp()), Long.valueOf(quest.getLastUpdatedTimestamp())) && zzbf.equal(quest2.zzarw(), quest.zzarw()) && zzbf.equal(quest2.getName(), quest.getName()) && zzbf.equal(Long.valueOf(quest2.zzarx()), Long.valueOf(quest.zzarx())) && zzbf.equal(Long.valueOf(quest2.getStartTimestamp()), Long.valueOf(quest.getStartTimestamp())) && zzbf.equal(Integer.valueOf(quest2.getState()), Integer.valueOf(quest.getState()));
    }

    static String zzb(Quest quest) {
        return zzbf.zzt(quest).zzg("Game", quest.getGame()).zzg("QuestId", quest.getQuestId()).zzg("AcceptedTimestamp", Long.valueOf(quest.getAcceptedTimestamp())).zzg("BannerImageUri", quest.getBannerImageUri()).zzg("BannerImageUrl", quest.getBannerImageUrl()).zzg("Description", quest.getDescription()).zzg("EndTimestamp", Long.valueOf(quest.getEndTimestamp())).zzg("IconImageUri", quest.getIconImageUri()).zzg("IconImageUrl", quest.getIconImageUrl()).zzg("LastUpdatedTimestamp", Long.valueOf(quest.getLastUpdatedTimestamp())).zzg("Milestones", quest.zzarw()).zzg("Name", quest.getName()).zzg("NotifyTimestamp", Long.valueOf(quest.zzarx())).zzg("StartTimestamp", Long.valueOf(quest.getStartTimestamp())).zzg("State", Integer.valueOf(quest.getState())).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final Quest freeze() {
        return this;
    }

    public final long getAcceptedTimestamp() {
        return this.zzhns;
    }

    public final Uri getBannerImageUri() {
        return this.zzhnt;
    }

    public final String getBannerImageUrl() {
        return this.zzhnu;
    }

    public final Milestone getCurrentMilestone() {
        return (Milestone) zzarw().get(0);
    }

    public final String getDescription() {
        return this.zzdmz;
    }

    public final void getDescription(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzdmz, charArrayBuffer);
    }

    public final long getEndTimestamp() {
        return this.zzhnv;
    }

    public final Game getGame() {
        return this.zzhiw;
    }

    public final Uri getIconImageUri() {
        return this.zzhnw;
    }

    public final String getIconImageUrl() {
        return this.zzhnx;
    }

    public final long getLastUpdatedTimestamp() {
        return this.zzhdu;
    }

    public final String getName() {
        return this.mName;
    }

    public final void getName(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.mName, charArrayBuffer);
    }

    public final String getQuestId() {
        return this.zzhnr;
    }

    public final long getStartTimestamp() {
        return this.zzhnz;
    }

    public final int getState() {
        return this.mState;
    }

    public final int getType() {
        return this.zzeda;
    }

    public final int hashCode() {
        return zza(this);
    }

    public final boolean isDataValid() {
        return true;
    }

    public final boolean isEndingSoon() {
        return this.zzhny <= System.currentTimeMillis() + 1800000;
    }

    public final String toString() {
        return zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getGame(), i, false);
        zzd.zza(parcel, 2, getQuestId(), false);
        zzd.zza(parcel, 3, getAcceptedTimestamp());
        zzd.zza(parcel, 4, getBannerImageUri(), i, false);
        zzd.zza(parcel, 5, getBannerImageUrl(), false);
        zzd.zza(parcel, 6, getDescription(), false);
        zzd.zza(parcel, 7, getEndTimestamp());
        zzd.zza(parcel, 8, getLastUpdatedTimestamp());
        zzd.zza(parcel, 9, getIconImageUri(), i, false);
        zzd.zza(parcel, 10, getIconImageUrl(), false);
        zzd.zza(parcel, 12, getName(), false);
        zzd.zza(parcel, 13, this.zzhny);
        zzd.zza(parcel, 14, getStartTimestamp());
        zzd.zzc(parcel, 15, getState());
        zzd.zzc(parcel, 16, this.zzeda);
        zzd.zzc(parcel, 17, zzarw(), false);
        zzd.zzai(parcel, zze);
    }

    public final List<Milestone> zzarw() {
        return new ArrayList(this.zzhoa);
    }

    public final long zzarx() {
        return this.zzhny;
    }
}
