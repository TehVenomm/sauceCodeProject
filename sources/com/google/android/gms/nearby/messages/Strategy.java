package com.google.android.gms.nearby.messages;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import java.util.ArrayList;
import java.util.List;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

public class Strategy extends zza {
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
    private static Strategy zzjeg;
    private int zzdxt;
    @Deprecated
    private int zzjeh;
    private int zzjei;
    private int zzjej;
    @Deprecated
    private boolean zzjek;
    private int zzjel;
    private int zzjem;
    private final int zzjen;

    public static class Builder {
        private int zzjeo = 3;
        private int zzjep = Strategy.TTL_SECONDS_DEFAULT;
        private int zzjeq = 0;
        private int zzjer = -1;
        private int zzjes = 0;

        public Strategy build() {
            if (this.zzjer == 2 && this.zzjeq == 1) {
                throw new IllegalStateException("Cannot set EARSHOT with BLE only mode.");
            }
            return new Strategy(2, 0, this.zzjep, this.zzjeq, false, this.zzjer, this.zzjeo, 0);
        }

        public Builder setDiscoveryMode(int i) {
            this.zzjeo = i;
            return this;
        }

        public Builder setDistanceType(int i) {
            this.zzjeq = i;
            return this;
        }

        public Builder setTtlSeconds(int i) {
            boolean z = i == Strategy.TTL_SECONDS_INFINITE || (i > 0 && i <= Strategy.TTL_SECONDS_MAX);
            zzbp.zzb(z, "mTtlSeconds(%d) must either be TTL_SECONDS_INFINITE, or it must be between 1 and TTL_SECONDS_MAX(%d) inclusive", Integer.valueOf(i), Integer.valueOf(Strategy.TTL_SECONDS_MAX));
            this.zzjep = i;
            return this;
        }

        public final Builder zzdx(int i) {
            this.zzjer = 2;
            return this;
        }
    }

    static {
        Strategy build = new Builder().zzdx(2).setTtlSeconds(TTL_SECONDS_INFINITE).build();
        BLE_ONLY = build;
        zzjeg = build;
    }

    Strategy(int i, int i2, int i3, int i4, boolean z, int i5, int i6, int i7) {
        this.zzdxt = i;
        this.zzjeh = i2;
        if (i2 != 0) {
            switch (i2) {
                case 2:
                    this.zzjem = 1;
                    break;
                case 3:
                    this.zzjem = 2;
                    break;
                default:
                    this.zzjem = 3;
                    break;
            }
        }
        this.zzjem = i6;
        this.zzjej = i4;
        this.zzjek = z;
        if (!z) {
            this.zzjei = i3;
            switch (i5) {
                case -1:
                case 0:
                case 1:
                case 6:
                    this.zzjel = -1;
                    break;
                default:
                    this.zzjel = i5;
                    break;
            }
        }
        this.zzjel = 2;
        this.zzjei = TTL_SECONDS_INFINITE;
        this.zzjen = i7;
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof Strategy)) {
                return false;
            }
            Strategy strategy = (Strategy) obj;
            if (this.zzdxt != strategy.zzdxt || this.zzjem != strategy.zzjem || this.zzjei != strategy.zzjei || this.zzjej != strategy.zzjej) {
                return false;
            }
            if (this.zzjel != strategy.zzjel) {
                return false;
            }
        }
        return true;
    }

    public int hashCode() {
        return (((((((this.zzdxt * 31) + this.zzjem) * 31) + this.zzjei) * 31) + this.zzjej) * 31) + this.zzjel;
    }

    public String toString() {
        String str;
        String str2;
        String str3;
        String str4;
        int i = this.zzjei;
        int i2 = this.zzjej;
        switch (i2) {
            case 0:
                str = AbstractIntegrationSupport.DEFAULT_REWARD_ID;
                break;
            case 1:
                str = "EARSHOT";
                break;
            default:
                str = "UNKNOWN:" + i2;
                break;
        }
        int i3 = this.zzjel;
        if (i3 == -1) {
            str2 = AbstractIntegrationSupport.DEFAULT_REWARD_ID;
        } else {
            List arrayList = new ArrayList();
            if ((i3 & 4) > 0) {
                arrayList.add("ULTRASOUND");
            }
            if ((i3 & 2) > 0) {
                arrayList.add("BLE");
            }
            str2 = arrayList.isEmpty() ? "UNKNOWN:" + i3 : arrayList.toString();
        }
        int i4 = this.zzjem;
        if (i4 == 3) {
            str3 = AbstractIntegrationSupport.DEFAULT_REWARD_ID;
        } else {
            List arrayList2 = new ArrayList();
            if ((i4 & 1) > 0) {
                arrayList2.add("BROADCAST");
            }
            if ((i4 & 2) > 0) {
                arrayList2.add("SCAN");
            }
            str3 = arrayList2.isEmpty() ? "UNKNOWN:" + i4 : arrayList2.toString();
        }
        int i5 = this.zzjen;
        switch (i5) {
            case 0:
                str4 = AbstractIntegrationSupport.DEFAULT_REWARD_ID;
                break;
            case 1:
                str4 = "ALWAYS_ON";
                break;
            default:
                str4 = "UNKNOWN: " + i5;
                break;
        }
        return new StringBuilder((((String.valueOf(str).length() + 102) + String.valueOf(str2).length()) + String.valueOf(str3).length()) + String.valueOf(str4).length()).append("Strategy{ttlSeconds=").append(i).append(", distanceType=").append(str).append(", discoveryMedium=").append(str2).append(", discoveryMode=").append(str3).append(", backgroundScanMode=").append(str4).append("}").toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzjeh);
        zzd.zzc(parcel, 2, this.zzjei);
        zzd.zzc(parcel, 3, this.zzjej);
        zzd.zza(parcel, 4, this.zzjek);
        zzd.zzc(parcel, 5, this.zzjel);
        zzd.zzc(parcel, 6, this.zzjem);
        zzd.zzc(parcel, 7, this.zzjen);
        zzd.zzc(parcel, 1000, this.zzdxt);
        zzd.zzai(parcel, zze);
    }

    public final int zzbay() {
        return this.zzjen;
    }
}
