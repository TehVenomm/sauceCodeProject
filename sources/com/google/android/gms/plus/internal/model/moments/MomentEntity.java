package com.google.android.gms.plus.internal.model.moments;

import android.os.Parcel;
import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.common.server.response.FastJsonResponse.Field;
import com.google.android.gms.common.server.response.FastSafeParcelableJsonResponse;
import com.google.android.gms.plus.model.moments.ItemScope;
import com.google.android.gms.plus.model.moments.Moment;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public final class MomentEntity extends FastSafeParcelableJsonResponse implements Moment {
    public static final zzb CREATOR = new zzb();
    private static final HashMap<String, Field<?, ?>> zzazC = new HashMap();
    String zzAV;
    String zzGM;
    String zzaAq;
    ItemScopeEntity zzaAy;
    ItemScopeEntity zzaAz;
    final Set<Integer> zzazD;
    final int zzzH;

    static {
        zzazC.put("id", Field.zzk("id", 2));
        zzazC.put("result", Field.zza("result", 4, ItemScopeEntity.class));
        zzazC.put("startDate", Field.zzk("startDate", 5));
        zzazC.put("target", Field.zza("target", 6, ItemScopeEntity.class));
        zzazC.put(ShareConstants.MEDIA_TYPE, Field.zzk(ShareConstants.MEDIA_TYPE, 7));
    }

    public MomentEntity() {
        this.zzzH = 1;
        this.zzazD = new HashSet();
    }

    MomentEntity(Set<Integer> set, int i, String str, ItemScopeEntity itemScopeEntity, String str2, ItemScopeEntity itemScopeEntity2, String str3) {
        this.zzazD = set;
        this.zzzH = i;
        this.zzGM = str;
        this.zzaAy = itemScopeEntity;
        this.zzaAq = str2;
        this.zzaAz = itemScopeEntity2;
        this.zzAV = str3;
    }

    public MomentEntity(Set<Integer> set, String str, ItemScopeEntity itemScopeEntity, String str2, ItemScopeEntity itemScopeEntity2, String str3) {
        this.zzazD = set;
        this.zzzH = 1;
        this.zzGM = str;
        this.zzaAy = itemScopeEntity;
        this.zzaAq = str2;
        this.zzaAz = itemScopeEntity2;
        this.zzAV = str3;
    }

    public int describeContents() {
        zzb zzb = CREATOR;
        return 0;
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof MomentEntity)) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        MomentEntity momentEntity = (MomentEntity) obj;
        for (Field field : zzazC.values()) {
            if (zza(field)) {
                if (!momentEntity.zza(field)) {
                    return false;
                }
                if (!zzb(field).equals(momentEntity.zzb(field))) {
                    return false;
                }
            } else if (momentEntity.zza(field)) {
                return false;
            }
        }
        return true;
    }

    public /* synthetic */ Object freeze() {
        return zzvN();
    }

    public String getId() {
        return this.zzGM;
    }

    public ItemScope getResult() {
        return this.zzaAy;
    }

    public String getStartDate() {
        return this.zzaAq;
    }

    public ItemScope getTarget() {
        return this.zzaAz;
    }

    public String getType() {
        return this.zzAV;
    }

    public boolean hasId() {
        return this.zzazD.contains(Integer.valueOf(2));
    }

    public boolean hasResult() {
        return this.zzazD.contains(Integer.valueOf(4));
    }

    public boolean hasStartDate() {
        return this.zzazD.contains(Integer.valueOf(5));
    }

    public boolean hasTarget() {
        return this.zzazD.contains(Integer.valueOf(6));
    }

    public boolean hasType() {
        return this.zzazD.contains(Integer.valueOf(7));
    }

    public int hashCode() {
        int i = 0;
        for (Field field : zzazC.values()) {
            int hashCode;
            if (zza(field)) {
                hashCode = zzb(field).hashCode() + (i + field.zzmF());
            } else {
                hashCode = i;
            }
            i = hashCode;
        }
        return i;
    }

    public boolean isDataValid() {
        return true;
    }

    public void writeToParcel(Parcel parcel, int i) {
        zzb zzb = CREATOR;
        zzb.zza(this, parcel, i);
    }

    protected boolean zza(Field field) {
        return this.zzazD.contains(Integer.valueOf(field.zzmF()));
    }

    protected Object zzb(Field field) {
        switch (field.zzmF()) {
            case 2:
                return this.zzGM;
            case 4:
                return this.zzaAy;
            case 5:
                return this.zzaAq;
            case 6:
                return this.zzaAz;
            case 7:
                return this.zzAV;
            default:
                throw new IllegalStateException("Unknown safe parcelable id=" + field.zzmF());
        }
    }

    public /* synthetic */ Map zzmy() {
        return zzvL();
    }

    public HashMap<String, Field<?, ?>> zzvL() {
        return zzazC;
    }

    public MomentEntity zzvN() {
        return this;
    }
}
