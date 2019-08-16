package com.google.android.gms.games.quest;

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
@Class(creator = "MilestoneEntityCreator")
@Reserved({1000})
@Deprecated
public final class MilestoneEntity extends zzd implements Milestone {
    public static final Creator<MilestoneEntity> CREATOR = new zza();
    @Field(getter = "getState", mo13990id = 5)
    private final int state;
    @Field(getter = "getEventId", mo13990id = 6)
    private final String zzgl;
    @Field(getter = "getMilestoneId", mo13990id = 1)
    private final String zzqw;
    @Field(getter = "getCurrentProgress", mo13990id = 2)
    private final long zzqx;
    @Field(getter = "getTargetProgress", mo13990id = 3)
    private final long zzqy;
    @Field(getter = "getCompletionRewardData", mo13990id = 4)
    private final byte[] zzqz;

    public MilestoneEntity(Milestone milestone) {
        this.zzqw = milestone.getMilestoneId();
        this.zzqx = milestone.getCurrentProgress();
        this.zzqy = milestone.getTargetProgress();
        this.state = milestone.getState();
        this.zzgl = milestone.getEventId();
        byte[] completionRewardData = milestone.getCompletionRewardData();
        if (completionRewardData == null) {
            this.zzqz = null;
            return;
        }
        this.zzqz = new byte[completionRewardData.length];
        System.arraycopy(completionRewardData, 0, this.zzqz, 0, completionRewardData.length);
    }

    @Constructor
    MilestoneEntity(@Param(mo13993id = 1) String str, @Param(mo13993id = 2) long j, @Param(mo13993id = 3) long j2, @Param(mo13993id = 4) byte[] bArr, @Param(mo13993id = 5) int i, @Param(mo13993id = 6) String str2) {
        this.zzqw = str;
        this.zzqx = j;
        this.zzqy = j2;
        this.zzqz = bArr;
        this.state = i;
        this.zzgl = str2;
    }

    static int zza(Milestone milestone) {
        return Objects.hashCode(milestone.getMilestoneId(), Long.valueOf(milestone.getCurrentProgress()), Long.valueOf(milestone.getTargetProgress()), Integer.valueOf(milestone.getState()), milestone.getEventId());
    }

    static boolean zza(Milestone milestone, Object obj) {
        if (!(obj instanceof Milestone)) {
            return false;
        }
        if (milestone == obj) {
            return true;
        }
        Milestone milestone2 = (Milestone) obj;
        return Objects.equal(milestone2.getMilestoneId(), milestone.getMilestoneId()) && Objects.equal(Long.valueOf(milestone2.getCurrentProgress()), Long.valueOf(milestone.getCurrentProgress())) && Objects.equal(Long.valueOf(milestone2.getTargetProgress()), Long.valueOf(milestone.getTargetProgress())) && Objects.equal(Integer.valueOf(milestone2.getState()), Integer.valueOf(milestone.getState())) && Objects.equal(milestone2.getEventId(), milestone.getEventId());
    }

    static String zzb(Milestone milestone) {
        return Objects.toStringHelper(milestone).add("MilestoneId", milestone.getMilestoneId()).add("CurrentProgress", Long.valueOf(milestone.getCurrentProgress())).add("TargetProgress", Long.valueOf(milestone.getTargetProgress())).add("State", Integer.valueOf(milestone.getState())).add("CompletionRewardData", milestone.getCompletionRewardData()).add("EventId", milestone.getEventId()).toString();
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

    public final byte[] getCompletionRewardData() {
        return this.zzqz;
    }

    public final long getCurrentProgress() {
        return this.zzqx;
    }

    public final String getEventId() {
        return this.zzgl;
    }

    public final String getMilestoneId() {
        return this.zzqw;
    }

    public final int getState() {
        return this.state;
    }

    public final long getTargetProgress() {
        return this.zzqy;
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
        SafeParcelWriter.writeString(parcel, 1, getMilestoneId(), false);
        SafeParcelWriter.writeLong(parcel, 2, getCurrentProgress());
        SafeParcelWriter.writeLong(parcel, 3, getTargetProgress());
        SafeParcelWriter.writeByteArray(parcel, 4, getCompletionRewardData(), false);
        SafeParcelWriter.writeInt(parcel, 5, getState());
        SafeParcelWriter.writeString(parcel, 6, getEventId(), false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
