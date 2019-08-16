package com.google.android.gms.common.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.internal.IAccountAccessor.Stub;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;

@Class(creator = "ResolveAccountResponseCreator")
public class ResolveAccountResponse extends AbstractSafeParcelable {
    public static final Creator<ResolveAccountResponse> CREATOR = new zan();
    @Field(getter = "getConnectionResult", mo13990id = 3)
    private ConnectionResult zadh;
    @Field(getter = "getSaveDefaultAccount", mo13990id = 4)
    private boolean zagf;
    @VersionField(mo13996id = 1)
    private final int zale;
    @Field(mo13990id = 2)
    private IBinder zanw;
    @Field(getter = "isFromCrossClientAuth", mo13990id = 5)
    private boolean zapb;

    public ResolveAccountResponse(int i) {
        this(new ConnectionResult(i, null));
    }

    @Constructor
    ResolveAccountResponse(@Param(mo13993id = 1) int i, @Param(mo13993id = 2) IBinder iBinder, @Param(mo13993id = 3) ConnectionResult connectionResult, @Param(mo13993id = 4) boolean z, @Param(mo13993id = 5) boolean z2) {
        this.zale = i;
        this.zanw = iBinder;
        this.zadh = connectionResult;
        this.zagf = z;
        this.zapb = z2;
    }

    public ResolveAccountResponse(ConnectionResult connectionResult) {
        this(1, null, connectionResult, false, false);
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof ResolveAccountResponse)) {
                return false;
            }
            ResolveAccountResponse resolveAccountResponse = (ResolveAccountResponse) obj;
            if (!this.zadh.equals(resolveAccountResponse.zadh) || !getAccountAccessor().equals(resolveAccountResponse.getAccountAccessor())) {
                return false;
            }
        }
        return true;
    }

    public IAccountAccessor getAccountAccessor() {
        return Stub.asInterface(this.zanw);
    }

    public ConnectionResult getConnectionResult() {
        return this.zadh;
    }

    public boolean getSaveDefaultAccount() {
        return this.zagf;
    }

    public boolean isFromCrossClientAuth() {
        return this.zapb;
    }

    public ResolveAccountResponse setAccountAccessor(IAccountAccessor iAccountAccessor) {
        this.zanw = iAccountAccessor == null ? null : iAccountAccessor.asBinder();
        return this;
    }

    public ResolveAccountResponse setIsFromCrossClientAuth(boolean z) {
        this.zapb = z;
        return this;
    }

    public ResolveAccountResponse setSaveDefaultAccount(boolean z) {
        this.zagf = z;
        return this;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.zale);
        SafeParcelWriter.writeIBinder(parcel, 2, this.zanw, false);
        SafeParcelWriter.writeParcelable(parcel, 3, getConnectionResult(), i, false);
        SafeParcelWriter.writeBoolean(parcel, 4, getSaveDefaultAccount());
        SafeParcelWriter.writeBoolean(parcel, 5, isFromCrossClientAuth());
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
