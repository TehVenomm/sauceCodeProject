package com.google.android.gms.auth;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;
import java.util.List;

@Class(creator = "AccountChangeEventsResponseCreator")
public class AccountChangeEventsResponse extends AbstractSafeParcelable {
    public static final Creator<AccountChangeEventsResponse> CREATOR = new zzc();
    @VersionField(mo13996id = 1)
    private final int zze;
    @Field(mo13990id = 2)
    private final List<AccountChangeEvent> zzl;

    @Constructor
    AccountChangeEventsResponse(@Param(mo13993id = 1) int i, @Param(mo13993id = 2) List<AccountChangeEvent> list) {
        this.zze = i;
        this.zzl = (List) Preconditions.checkNotNull(list);
    }

    public AccountChangeEventsResponse(List<AccountChangeEvent> list) {
        this.zze = 1;
        this.zzl = (List) Preconditions.checkNotNull(list);
    }

    public List<AccountChangeEvent> getEvents() {
        return this.zzl;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.zze);
        SafeParcelWriter.writeTypedList(parcel, 2, this.zzl, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
