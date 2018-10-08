package com.google.android.gms.drive.query;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveSpace;
import com.google.android.gms.drive.query.internal.zzr;
import com.google.android.gms.drive.query.internal.zzt;
import com.google.android.gms.drive.query.internal.zzx;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Locale;
import java.util.Set;

public class Query extends zza {
    public static final Creator<Query> CREATOR = new zzb();
    private List<DriveSpace> zzgew;
    private final Set<DriveSpace> zzgex;
    private zzr zzgmu;
    private String zzgmv;
    private SortOrder zzgmw;
    final List<String> zzgmx;
    final boolean zzgmy;
    final boolean zzgmz;

    public static class Builder {
        private Set<DriveSpace> zzgex;
        private String zzgmv;
        private SortOrder zzgmw;
        private List<String> zzgmx;
        private boolean zzgmy;
        private boolean zzgmz;
        private final List<Filter> zzgna = new ArrayList();

        public Builder(Query query) {
            this.zzgna.add(query.getFilter());
            this.zzgmv = query.getPageToken();
            this.zzgmw = query.getSortOrder();
            this.zzgmx = query.zzgmx;
            this.zzgmy = query.zzgmy;
            this.zzgex = query.zzanx();
            this.zzgmz = query.zzgmz;
        }

        public Builder addFilter(Filter filter) {
            if (!(filter instanceof zzt)) {
                this.zzgna.add(filter);
            }
            return this;
        }

        public Query build() {
            return new Query(new zzr(zzx.zzgoc, this.zzgna), this.zzgmv, this.zzgmw, this.zzgmx, this.zzgmy, this.zzgex, this.zzgmz);
        }

        @Deprecated
        public Builder setPageToken(String str) {
            this.zzgmv = str;
            return this;
        }

        public Builder setSortOrder(SortOrder sortOrder) {
            this.zzgmw = sortOrder;
            return this;
        }
    }

    private Query(zzr zzr, String str, SortOrder sortOrder, List<String> list, boolean z, List<DriveSpace> list2, Set<DriveSpace> set, boolean z2) {
        this.zzgmu = zzr;
        this.zzgmv = str;
        this.zzgmw = sortOrder;
        this.zzgmx = list;
        this.zzgmy = z;
        this.zzgew = list2;
        this.zzgex = set;
        this.zzgmz = z2;
    }

    Query(zzr zzr, String str, SortOrder sortOrder, List<String> list, boolean z, List<DriveSpace> list2, boolean z2) {
        this(zzr, str, sortOrder, (List) list, z, (List) list2, list2 == null ? null : new HashSet(list2), z2);
    }

    private Query(zzr zzr, String str, SortOrder sortOrder, List<String> list, boolean z, Set<DriveSpace> set, boolean z2) {
        this(zzr, str, sortOrder, (List) list, z, set == null ? null : new ArrayList(set), (Set) set, z2);
    }

    public Filter getFilter() {
        return this.zzgmu;
    }

    @Deprecated
    public String getPageToken() {
        return this.zzgmv;
    }

    public SortOrder getSortOrder() {
        return this.zzgmw;
    }

    public String toString() {
        return String.format(Locale.US, "Query[%s,%s,PageToken=%s,Spaces=%s]", new Object[]{this.zzgmu, this.zzgmw, this.zzgmv, this.zzgew});
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzgmu, i, false);
        zzd.zza(parcel, 3, this.zzgmv, false);
        zzd.zza(parcel, 4, this.zzgmw, i, false);
        zzd.zzb(parcel, 5, this.zzgmx, false);
        zzd.zza(parcel, 6, this.zzgmy);
        zzd.zzc(parcel, 7, this.zzgew, false);
        zzd.zza(parcel, 8, this.zzgmz);
        zzd.zzai(parcel, zze);
    }

    public final Set<DriveSpace> zzanx() {
        return this.zzgex;
    }
}
