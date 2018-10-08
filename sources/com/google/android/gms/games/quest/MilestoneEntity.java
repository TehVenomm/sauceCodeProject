package com.google.android.gms.games.quest;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.games.internal.zzc;
import java.util.Arrays;

@Deprecated
public final class MilestoneEntity extends zzc implements Milestone {
    public static final Creator<MilestoneEntity> CREATOR = new zza();
    private final int mState;
    private final String zzhdw;
    private final String zzhft;
    private final long zzhno;
    private final long zzhnp;
    private final byte[] zzhnq;

    public MilestoneEntity(Milestone milestone) {
        this.zzhft = milestone.getMilestoneId();
        this.zzhno = milestone.getCurrentProgress();
        this.zzhnp = milestone.getTargetProgress();
        this.mState = milestone.getState();
        this.zzhdw = milestone.getEventId();
        Object completionRewardData = milestone.getCompletionRewardData();
        if (completionRewardData == null) {
            this.zzhnq = null;
            return;
        }
        this.zzhnq = new byte[completionRewardData.length];
        System.arraycopy(completionRewardData, 0, this.zzhnq, 0, completionRewardData.length);
    }

    MilestoneEntity(String str, long j, long j2, byte[] bArr, int i, String str2) {
        this.zzhft = str;
        this.zzhno = j;
        this.zzhnp = j2;
        this.zzhnq = bArr;
        this.mState = i;
        this.zzhdw = str2;
    }

    static int zza(Milestone milestone) {
        return Arrays.hashCode(new Object[]{milestone.getMilestoneId(), Long.valueOf(milestone.getCurrentProgress()), Long.valueOf(milestone.getTargetProgress()), Integer.valueOf(milestone.getState()), milestone.getEventId()});
    }

    static boolean zza(Milestone milestone, Object obj) {
        if (!(obj instanceof Milestone)) {
            return false;
        }
        if (milestone == obj) {
            return true;
        }
        Milestone milestone2 = (Milestone) obj;
        return zzbf.equal(milestone2.getMilestoneId(), milestone.getMilestoneId()) && zzbf.equal(Long.valueOf(milestone2.getCurrentProgress()), Long.valueOf(milestone.getCurrentProgress())) && zzbf.equal(Long.valueOf(milestone2.getTargetProgress()), Long.valueOf(milestone.getTargetProgress())) && zzbf.equal(Integer.valueOf(milestone2.getState()), Integer.valueOf(milestone.getState())) && zzbf.equal(milestone2.getEventId(), milestone.getEventId());
    }

    static String zzb(Milestone milestone) {
        return zzbf.zzt(milestone).zzg("MilestoneId", milestone.getMilestoneId()).zzg("CurrentProgress", Long.valueOf(milestone.getCurrentProgress())).zzg("TargetProgress", Long.valueOf(milestone.getTargetProgress())).zzg("State", Integer.valueOf(milestone.getState())).zzg("CompletionRewardData", milestone.getCompletionRewardData()).zzg("EventId", milestone.getEventId()).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final Milestone freeze() {
        return this;
    }

    public final byte[] getCompletionRewardData() {
        return this.zzhnq;
    }

    public final long getCurrentProgress() {
        return this.zzhno;
    }

    public final String getEventId() {
        return this.zzhdw;
    }

    public final String getMilestoneId() {
        return this.zzhft;
    }

    public final int getState() {
        return this.mState;
    }

    public final long getTargetProgress() {
        return this.zzhnp;
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
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getMilestoneId(), false);
        zzd.zza(parcel, 2, getCurrentProgress());
        zzd.zza(parcel, 3, getTargetProgress());
        zzd.zza(parcel, 4, getCompletionRewardData(), false);
        zzd.zzc(parcel, 5, getState());
        zzd.zza(parcel, 6, getEventId(), false);
        zzd.zzai(parcel, zze);
    }
}
