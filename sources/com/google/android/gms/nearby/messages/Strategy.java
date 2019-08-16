package com.google.android.gms.nearby.messages;

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
import java.util.ArrayList;
import javax.annotation.concurrent.Immutable;

@Immutable
@Class(creator = "StrategyCreator")
public class Strategy extends AbstractSafeParcelable {
    public static final Strategy BLE_ONLY;
    public static final Creator<Strategy> CREATOR = new zzg();
    public static final Strategy DEFAULT = new Builder().build();
    public static final int DISCOVERY_MODE_BROADCAST = 1;
    public static final int DISCOVERY_MODE_DEFAULT = 3;
    public static final int DISCOVERY_MODE_SCAN = 2;
    public static final int DISTANCE_TYPE_DEFAULT = 0;
    public static final int DISTANCE_TYPE_EARSHOT = 1;
    public static final int TTL_SECONDS_DEFAULT = 300;
    public static final int TTL_SECONDS_INFINITE = Integer.MAX_VALUE;
    public static final int TTL_SECONDS_MAX = 86400;
    @Deprecated
    private static final Strategy zzfm;
    @VersionField(mo13996id = 1000)
    private final int zzex;
    @Field(mo13990id = 1)
    @Deprecated
    private final int zzfn;
    @Field(mo13990id = 2)
    private final int zzfo;
    @Field(mo13990id = 3)
    private final int zzfp;
    @Field(mo13990id = 4)
    @Deprecated
    private final boolean zzfq;
    @Field(getter = "getDiscoveryMedium", mo13990id = 5)
    private final int zzfr;
    @Field(getter = "getDiscoveryMode", mo13990id = 6)
    private final int zzfs;
    @Field(getter = "getBackgroundScanMode", mo13990id = 7)
    private final int zzft;

    public static class Builder {
        private int zzfu = 3;
        private int zzfv = Strategy.TTL_SECONDS_DEFAULT;
        private int zzfw = 0;
        private int zzfx = -1;
        private int zzfy = 0;

        public Strategy build() {
            if (this.zzfx != 2 || this.zzfw != 1) {
                return new Strategy(2, 0, this.zzfv, this.zzfw, false, this.zzfx, this.zzfu, 0);
            }
            throw new IllegalStateException("Cannot set EARSHOT with BLE only mode.");
        }

        public Builder setDiscoveryMode(int i) {
            this.zzfu = i;
            return this;
        }

        public Builder setDistanceType(int i) {
            this.zzfw = i;
            return this;
        }

        public Builder setTtlSeconds(int i) {
            Preconditions.checkArgument(i == Integer.MAX_VALUE || (i > 0 && i <= 86400), "mTtlSeconds(%d) must either be TTL_SECONDS_INFINITE, or it must be between 1 and TTL_SECONDS_MAX(%d) inclusive", Integer.valueOf(i), Integer.valueOf(Strategy.TTL_SECONDS_MAX));
            this.zzfv = i;
            return this;
        }

        public final Builder zze(int i) {
            this.zzfx = 2;
            return this;
        }
    }

    static {
        Strategy build = new Builder().zze(2).setTtlSeconds(Integer.MAX_VALUE).build();
        BLE_ONLY = build;
        zzfm = build;
    }

    @Constructor
    Strategy(@Param(mo13993id = 1000) int i, @Param(mo13993id = 1) int i2, @Param(mo13993id = 2) int i3, @Param(mo13993id = 3) int i4, @Param(mo13993id = 4) boolean z, @Param(mo13993id = 5) int i5, @Param(mo13993id = 6) int i6, @Param(mo13993id = 7) int i7) {
        this.zzex = i;
        this.zzfn = i2;
        if (i2 != 0) {
            switch (i2) {
                case 2:
                    this.zzfs = 1;
                    break;
                case 3:
                    this.zzfs = 2;
                    break;
                default:
                    this.zzfs = 3;
                    break;
            }
        } else {
            this.zzfs = i6;
        }
        this.zzfp = i4;
        this.zzfq = z;
        if (!z) {
            this.zzfo = i3;
            switch (i5) {
                case -1:
                case 0:
                case 1:
                case 6:
                    this.zzfr = -1;
                    break;
                default:
                    this.zzfr = i5;
                    break;
            }
        } else {
            this.zzfr = 2;
            this.zzfo = Integer.MAX_VALUE;
        }
        this.zzft = i7;
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof Strategy)) {
                return false;
            }
            Strategy strategy = (Strategy) obj;
            if (!(this.zzex == strategy.zzex && this.zzfs == strategy.zzfs && this.zzfo == strategy.zzfo && this.zzfp == strategy.zzfp && this.zzfr == strategy.zzfr && this.zzft == strategy.zzft)) {
                return false;
            }
        }
        return true;
    }

    public int hashCode() {
        return (((((((((this.zzex * 31) + this.zzfs) * 31) + this.zzfo) * 31) + this.zzfp) * 31) + this.zzfr) * 31) + this.zzft;
    }

    public String toString() {
        String str;
        String obj;
        String obj2;
        String str2;
        int i = this.zzfo;
        int i2 = this.zzfp;
        switch (i2) {
            case 0:
                str = "DEFAULT";
                break;
            case 1:
                str = "EARSHOT";
                break;
            default:
                str = "UNKNOWN:" + i2;
                break;
        }
        int i3 = this.zzfr;
        if (i3 == -1) {
            obj = "DEFAULT";
        } else {
            ArrayList arrayList = new ArrayList();
            if ((i3 & 4) > 0) {
                arrayList.add("ULTRASOUND");
            }
            if ((i3 & 2) > 0) {
                arrayList.add("BLE");
            }
            obj = arrayList.isEmpty() ? "UNKNOWN:" + i3 : arrayList.toString();
        }
        int i4 = this.zzfs;
        if (i4 == 3) {
            obj2 = "DEFAULT";
        } else {
            ArrayList arrayList2 = new ArrayList();
            if ((i4 & 1) > 0) {
                arrayList2.add("BROADCAST");
            }
            if ((i4 & 2) > 0) {
                arrayList2.add("SCAN");
            }
            obj2 = arrayList2.isEmpty() ? "UNKNOWN:" + i4 : arrayList2.toString();
        }
        int i5 = this.zzft;
        switch (i5) {
            case 0:
                str2 = "DEFAULT";
                break;
            case 1:
                str2 = "ALWAYS_ON";
                break;
            default:
                str2 = "UNKNOWN: " + i5;
                break;
        }
        return new StringBuilder(String.valueOf(str).length() + 102 + String.valueOf(obj).length() + String.valueOf(obj2).length() + String.valueOf(str2).length()).append("Strategy{ttlSeconds=").append(i).append(", distanceType=").append(str).append(", discoveryMedium=").append(obj).append(", discoveryMode=").append(obj2).append(", backgroundScanMode=").append(str2).append('}').toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.zzfn);
        SafeParcelWriter.writeInt(parcel, 2, this.zzfo);
        SafeParcelWriter.writeInt(parcel, 3, this.zzfp);
        SafeParcelWriter.writeBoolean(parcel, 4, this.zzfq);
        SafeParcelWriter.writeInt(parcel, 5, this.zzfr);
        SafeParcelWriter.writeInt(parcel, 6, this.zzfs);
        SafeParcelWriter.writeInt(parcel, 7, this.zzft);
        SafeParcelWriter.writeInt(parcel, 1000, this.zzex);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }

    public final int zzae() {
        return this.zzft;
    }
}
