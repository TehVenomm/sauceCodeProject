package com.google.android.gms.games.stats;

import android.os.Bundle;
import android.os.Parcel;
import com.google.android.gms.common.data.DataHolder;

public final class zzc extends com.google.android.gms.common.data.zzc implements PlayerStats {
    private Bundle zzhpf;

    zzc(DataHolder dataHolder, int i) {
        super(dataHolder, i);
    }

    public final int describeContents() {
        return 0;
    }

    public final boolean equals(Object obj) {
        return zza.zza(this, obj);
    }

    public final /* synthetic */ Object freeze() {
        return new zza(this);
    }

    public final float getAverageSessionLength() {
        return getFloat("ave_session_length_minutes");
    }

    public final float getChurnProbability() {
        return getFloat("churn_probability");
    }

    public final int getDaysSinceLastPlayed() {
        return getInteger("days_since_last_played");
    }

    public final float getHighSpenderProbability() {
        return !zzft("high_spender_probability") ? -1.0f : getFloat("high_spender_probability");
    }

    public final int getNumberOfPurchases() {
        return getInteger("num_purchases");
    }

    public final int getNumberOfSessions() {
        return getInteger("num_sessions");
    }

    public final float getSessionPercentile() {
        return getFloat("num_sessions_percentile");
    }

    public final float getSpendPercentile() {
        return getFloat("spend_percentile");
    }

    public final float getSpendProbability() {
        return !zzft("spend_probability") ? -1.0f : getFloat("spend_probability");
    }

    public final float getTotalSpendNext28Days() {
        return !zzft("total_spend_next_28_days") ? -1.0f : getFloat("total_spend_next_28_days");
    }

    public final int hashCode() {
        return zza.zza(this);
    }

    public final String toString() {
        return zza.zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        ((zza) ((PlayerStats) freeze())).writeToParcel(parcel, i);
    }

    public final Bundle zzarz() {
        int i = 0;
        if (this.zzhpf != null) {
            return this.zzhpf;
        }
        this.zzhpf = new Bundle();
        String string = getString("unknown_raw_keys");
        String string2 = getString("unknown_raw_values");
        if (!(string == null || string2 == null)) {
            String[] split = string.split(",");
            String[] split2 = string2.split(",");
            if ((split.length <= split2.length ? 1 : 0) == 0) {
                throw new IllegalStateException(String.valueOf("Invalid raw arguments!"));
            }
            while (i < split.length) {
                this.zzhpf.putString(split[i], split2[i]);
                i++;
            }
        }
        return this.zzhpf;
    }
}
