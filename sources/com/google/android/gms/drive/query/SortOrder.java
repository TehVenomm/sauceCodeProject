package com.google.android.gms.drive.query;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.text.TextUtils;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.metadata.SortableMetadataField;
import com.google.android.gms.drive.query.internal.zzf;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

public class SortOrder extends zza {
    public static final Creator<SortOrder> CREATOR = new zzc();
    private List<zzf> zzgnd;
    private boolean zzgne;

    public static class Builder {
        private final List<zzf> zzgnd = new ArrayList();
        private boolean zzgne = false;

        public Builder addSortAscending(SortableMetadataField sortableMetadataField) {
            this.zzgnd.add(new zzf(sortableMetadataField.getName(), true));
            return this;
        }

        public Builder addSortDescending(SortableMetadataField sortableMetadataField) {
            this.zzgnd.add(new zzf(sortableMetadataField.getName(), false));
            return this;
        }

        public SortOrder build() {
            return new SortOrder(this.zzgnd, false);
        }
    }

    SortOrder(List<zzf> list, boolean z) {
        this.zzgnd = list;
        this.zzgne = z;
    }

    public String toString() {
        return String.format(Locale.US, "SortOrder[%s, %s]", new Object[]{TextUtils.join(",", this.zzgnd), Boolean.valueOf(this.zzgne)});
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzgnd, false);
        zzd.zza(parcel, 2, this.zzgne);
        zzd.zzai(parcel, zze);
    }
}
