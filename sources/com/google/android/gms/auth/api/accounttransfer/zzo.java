package com.google.android.gms.auth.api.accounttransfer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import android.support.p000v4.util.ArrayMap;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;
import com.google.android.gms.common.server.response.FastJsonResponse.Field;
import com.google.android.gms.internal.auth.zzaz;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

@Class(creator = "AccountTransferProgressCreator")
public class zzo extends zzaz {
    public static final Creator<zzo> CREATOR = new zzp();
    private static final ArrayMap<String, Field<?, ?>> zzbe;
    @SafeParcelable.Field(getter = "getRegisteredAccountTypes", mo13990id = 2)
    private List<String> zzbf;
    @SafeParcelable.Field(getter = "getInProgressAccountTypes", mo13990id = 3)
    private List<String> zzbg;
    @SafeParcelable.Field(getter = "getSuccessAccountTypes", mo13990id = 4)
    private List<String> zzbh;
    @SafeParcelable.Field(getter = "getFailedAccountTypes", mo13990id = 5)
    private List<String> zzbi;
    @SafeParcelable.Field(getter = "getEscrowedAccountTypes", mo13990id = 6)
    private List<String> zzbj;
    @VersionField(mo13996id = 1)
    private final int zzv;

    static {
        ArrayMap<String, Field<?, ?>> arrayMap = new ArrayMap<>();
        zzbe = arrayMap;
        arrayMap.put("registered", Field.forStrings("registered", 2));
        zzbe.put("in_progress", Field.forStrings("in_progress", 3));
        zzbe.put("success", Field.forStrings("success", 4));
        zzbe.put("failed", Field.forStrings("failed", 5));
        zzbe.put("escrowed", Field.forStrings("escrowed", 6));
    }

    public zzo() {
        this.zzv = 1;
    }

    @Constructor
    zzo(@Param(mo13993id = 1) int i, @Nullable @Param(mo13993id = 2) List<String> list, @Nullable @Param(mo13993id = 3) List<String> list2, @Nullable @Param(mo13993id = 4) List<String> list3, @Nullable @Param(mo13993id = 5) List<String> list4, @Nullable @Param(mo13993id = 6) List<String> list5) {
        this.zzv = i;
        this.zzbf = list;
        this.zzbg = list2;
        this.zzbh = list3;
        this.zzbi = list4;
        this.zzbj = list5;
    }

    public Map<String, Field<?, ?>> getFieldMappings() {
        return zzbe;
    }

    /* access modifiers changed from: protected */
    public Object getFieldValue(Field field) {
        switch (field.getSafeParcelableFieldId()) {
            case 1:
                return Integer.valueOf(this.zzv);
            case 2:
                return this.zzbf;
            case 3:
                return this.zzbg;
            case 4:
                return this.zzbh;
            case 5:
                return this.zzbi;
            case 6:
                return this.zzbj;
            default:
                throw new IllegalStateException("Unknown SafeParcelable id=" + field.getSafeParcelableFieldId());
        }
    }

    /* access modifiers changed from: protected */
    public boolean isFieldSet(Field field) {
        return true;
    }

    /* access modifiers changed from: protected */
    public void setStringsInternal(Field<?, ?> field, String str, ArrayList<String> arrayList) {
        int safeParcelableFieldId = field.getSafeParcelableFieldId();
        switch (safeParcelableFieldId) {
            case 2:
                this.zzbf = arrayList;
                return;
            case 3:
                this.zzbg = arrayList;
                return;
            case 4:
                this.zzbh = arrayList;
                return;
            case 5:
                this.zzbi = arrayList;
                return;
            case 6:
                this.zzbj = arrayList;
                return;
            default:
                throw new IllegalArgumentException(String.format("Field with id=%d is not known to be a string list.", new Object[]{Integer.valueOf(safeParcelableFieldId)}));
        }
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.zzv);
        SafeParcelWriter.writeStringList(parcel, 2, this.zzbf, false);
        SafeParcelWriter.writeStringList(parcel, 3, this.zzbg, false);
        SafeParcelWriter.writeStringList(parcel, 4, this.zzbh, false);
        SafeParcelWriter.writeStringList(parcel, 5, this.zzbi, false);
        SafeParcelWriter.writeStringList(parcel, 6, this.zzbj, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
