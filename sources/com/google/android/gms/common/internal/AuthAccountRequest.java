package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.IAccountAccessor.Stub;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;
import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;
import javax.annotation.Nullable;

@Class(creator = "AuthAccountRequestCreator")
public class AuthAccountRequest extends AbstractSafeParcelable {
    public static final Creator<AuthAccountRequest> CREATOR = new zaa();
    @VersionField(mo13996id = 1)
    private final int zale;
    @Field(mo13990id = 2)
    @Deprecated
    private final IBinder zanw;
    @Field(mo13990id = 3)
    private final Scope[] zanx;
    @Field(mo13990id = 4)
    private Integer zany;
    @Field(mo13990id = 5)
    private Integer zanz;
    @Field(mo13990id = 6, type = "android.accounts.Account")
    private Account zax;

    @Constructor
    AuthAccountRequest(@Param(mo13993id = 1) int i, @Param(mo13993id = 2) IBinder iBinder, @Param(mo13993id = 3) Scope[] scopeArr, @Param(mo13993id = 4) Integer num, @Param(mo13993id = 5) Integer num2, @Param(mo13993id = 6) Account account) {
        this.zale = i;
        this.zanw = iBinder;
        this.zanx = scopeArr;
        this.zany = num;
        this.zanz = num2;
        this.zax = account;
    }

    public AuthAccountRequest(Account account, Set<Scope> set) {
        this(3, null, (Scope[]) set.toArray(new Scope[set.size()]), null, null, (Account) Preconditions.checkNotNull(account));
    }

    @Deprecated
    public AuthAccountRequest(IAccountAccessor iAccountAccessor, Set<Scope> set) {
        this(3, iAccountAccessor.asBinder(), (Scope[]) set.toArray(new Scope[set.size()]), null, null, null);
    }

    public Account getAccount() {
        if (this.zax != null) {
            return this.zax;
        }
        if (this.zanw != null) {
            return AccountAccessor.getAccountBinderSafe(Stub.asInterface(this.zanw));
        }
        return null;
    }

    @Nullable
    public Integer getOauthPolicy() {
        return this.zany;
    }

    @Nullable
    public Integer getPolicyAction() {
        return this.zanz;
    }

    public Set<Scope> getScopes() {
        return new HashSet(Arrays.asList(this.zanx));
    }

    public AuthAccountRequest setOauthPolicy(@Nullable Integer num) {
        this.zany = num;
        return this;
    }

    public AuthAccountRequest setPolicyAction(@Nullable Integer num) {
        this.zanz = num;
        return this;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.zale);
        SafeParcelWriter.writeIBinder(parcel, 2, this.zanw, false);
        SafeParcelWriter.writeTypedArray(parcel, 3, this.zanx, i, false);
        SafeParcelWriter.writeIntegerObject(parcel, 4, this.zany, false);
        SafeParcelWriter.writeIntegerObject(parcel, 5, this.zanz, false);
        SafeParcelWriter.writeParcelable(parcel, 6, this.zax, i, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
