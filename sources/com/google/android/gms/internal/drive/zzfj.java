package com.google.android.gms.internal.drive;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.drive.events.ChangeEvent;
import com.google.android.gms.drive.events.CompletionEvent;
import com.google.android.gms.drive.events.DriveEvent;
import com.google.android.gms.drive.events.zzb;
import com.google.android.gms.drive.events.zzo;
import com.google.android.gms.drive.events.zzr;
import com.google.android.gms.drive.events.zzv;

@Class(creator = "OnEventResponseCreator")
@Reserved({1, 4, 8})
public final class zzfj extends AbstractSafeParcelable {
    public static final Creator<zzfj> CREATOR = new zzfk();
    @Field(mo13990id = 2)
    private final int zzcy;
    @Field(mo13990id = 3)
    private final ChangeEvent zzhl;
    @Field(mo13990id = 5)
    private final CompletionEvent zzhm;
    @Field(mo13990id = 6)
    private final zzo zzhn;
    @Field(mo13990id = 7)
    private final zzb zzho;
    @Field(mo13990id = 9)
    private final zzv zzhp;
    @Field(mo13990id = 10)
    private final zzr zzhq;

    @Constructor
    zzfj(@Param(mo13993id = 2) int i, @Param(mo13993id = 3) ChangeEvent changeEvent, @Param(mo13993id = 5) CompletionEvent completionEvent, @Param(mo13993id = 6) zzo zzo, @Param(mo13993id = 7) zzb zzb, @Param(mo13993id = 9) zzv zzv, @Param(mo13993id = 10) zzr zzr) {
        this.zzcy = i;
        this.zzhl = changeEvent;
        this.zzhm = completionEvent;
        this.zzhn = zzo;
        this.zzho = zzb;
        this.zzhp = zzv;
        this.zzhq = zzr;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 2, this.zzcy);
        SafeParcelWriter.writeParcelable(parcel, 3, this.zzhl, i, false);
        SafeParcelWriter.writeParcelable(parcel, 5, this.zzhm, i, false);
        SafeParcelWriter.writeParcelable(parcel, 6, this.zzhn, i, false);
        SafeParcelWriter.writeParcelable(parcel, 7, this.zzho, i, false);
        SafeParcelWriter.writeParcelable(parcel, 9, this.zzhp, i, false);
        SafeParcelWriter.writeParcelable(parcel, 10, this.zzhq, i, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }

    public final DriveEvent zzak() {
        switch (this.zzcy) {
            case 1:
                return this.zzhl;
            case 2:
                return this.zzhm;
            case 3:
                return this.zzhn;
            case 4:
                return this.zzho;
            case 7:
                return this.zzhp;
            case 8:
                return this.zzhq;
            default:
                throw new IllegalStateException("Unexpected event type " + this.zzcy);
        }
    }
}
