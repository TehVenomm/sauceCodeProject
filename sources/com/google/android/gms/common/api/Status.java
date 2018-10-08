package com.google.android.gms.common.api;

import android.app.Activity;
import android.app.PendingIntent;
import android.content.IntentSender.SendIntentException;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class Status extends zza implements Result, ReflectedParcelable {
    public static final Creator<Status> CREATOR = new zzh();
    public static final Status zzfhp = new Status(0);
    public static final Status zzfhq = new Status(14);
    public static final Status zzfhr = new Status(8);
    public static final Status zzfhs = new Status(15);
    public static final Status zzfht = new Status(16);
    private static Status zzfhu = new Status(17);
    private static Status zzfhv = new Status(18);
    private final PendingIntent mPendingIntent;
    private int zzdxt;
    private final int zzezx;
    private final String zzffa;

    public Status(int i) {
        this(i, null);
    }

    Status(int i, int i2, String str, PendingIntent pendingIntent) {
        this.zzdxt = i;
        this.zzezx = i2;
        this.zzffa = str;
        this.mPendingIntent = pendingIntent;
    }

    public Status(int i, String str) {
        this(1, i, str, null);
    }

    public Status(int i, String str, PendingIntent pendingIntent) {
        this(1, i, str, pendingIntent);
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof Status)) {
            return false;
        }
        Status status = (Status) obj;
        return this.zzdxt == status.zzdxt && this.zzezx == status.zzezx && zzbf.equal(this.zzffa, status.zzffa) && zzbf.equal(this.mPendingIntent, status.mPendingIntent);
    }

    public final PendingIntent getResolution() {
        return this.mPendingIntent;
    }

    public final Status getStatus() {
        return this;
    }

    public final int getStatusCode() {
        return this.zzezx;
    }

    @Nullable
    public final String getStatusMessage() {
        return this.zzffa;
    }

    public final boolean hasResolution() {
        return this.mPendingIntent != null;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.zzdxt), Integer.valueOf(this.zzezx), this.zzffa, this.mPendingIntent});
    }

    public final boolean isCanceled() {
        return this.zzezx == 16;
    }

    public final boolean isInterrupted() {
        return this.zzezx == 14;
    }

    public final boolean isSuccess() {
        return this.zzezx <= 0;
    }

    public final void startResolutionForResult(Activity activity, int i) throws SendIntentException {
        if (hasResolution()) {
            activity.startIntentSenderForResult(this.mPendingIntent.getIntentSender(), i, null, 0, 0, 0);
        }
    }

    public final String toString() {
        return zzbf.zzt(this).zzg("statusCode", zzaft()).zzg("resolution", this.mPendingIntent).toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, getStatusCode());
        zzd.zza(parcel, 2, getStatusMessage(), false);
        zzd.zza(parcel, 3, this.mPendingIntent, i, false);
        zzd.zzc(parcel, 1000, this.zzdxt);
        zzd.zzai(parcel, zze);
    }

    public final String zzaft() {
        return this.zzffa != null ? this.zzffa : CommonStatusCodes.getStatusCodeString(this.zzezx);
    }
}
