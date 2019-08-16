package com.google.android.gms.drive.query;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.drive.DriveSpace;
import com.google.android.gms.drive.query.internal.zzr;
import com.google.android.gms.drive.query.internal.zzt;
import com.google.android.gms.drive.query.internal.zzx;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Locale;
import java.util.Set;

@Class(creator = "QueryCreator")
@Reserved({1000})
public class Query extends AbstractSafeParcelable {
    public static final Creator<Query> CREATOR = new zzb();
    @Field(mo13990id = 7)
    private final List<DriveSpace> zzbw;
    @Field(mo13990id = 1)
    private final zzr zzkw;
    @Field(mo13990id = 3)
    private final String zzkx;
    @Nullable
    @Field(mo13990id = 4)
    private final SortOrder zzky;
    @Field(mo13990id = 5)
    final List<String> zzkz;
    @Field(mo13990id = 6)
    final boolean zzla;
    @Field(mo13990id = 8)
    final boolean zzlb;

    @VisibleForTesting
    public static class Builder {
        private String zzkx;
        private SortOrder zzky;
        private List<String> zzkz = Collections.emptyList();
        private boolean zzla;
        private boolean zzlb;
        private final List<Filter> zzlc = new ArrayList();
        private Set<DriveSpace> zzld = Collections.emptySet();

        public Builder() {
        }

        public Builder(Query query) {
            this.zzlc.add(query.getFilter());
            this.zzkx = query.getPageToken();
            this.zzky = query.getSortOrder();
            this.zzkz = query.zzkz;
            this.zzla = query.zzla;
            query.zzba();
            this.zzld = query.zzba();
            this.zzlb = query.zzlb;
        }

        public Builder addFilter(@NonNull Filter filter) {
            Preconditions.checkNotNull(filter, "Filter may not be null.");
            if (!(filter instanceof zzt)) {
                this.zzlc.add(filter);
            }
            return this;
        }

        public Query build() {
            return new Query(new zzr(zzx.zzmf, (Iterable<Filter>) this.zzlc), this.zzkx, this.zzky, this.zzkz, this.zzla, this.zzld, this.zzlb);
        }

        @Deprecated
        public Builder setPageToken(String str) {
            this.zzkx = str;
            return this;
        }

        public Builder setSortOrder(SortOrder sortOrder) {
            this.zzky = sortOrder;
            return this;
        }
    }

    @Constructor
    Query(@Param(mo13993id = 1) zzr zzr, @Param(mo13993id = 3) String str, @Nullable @Param(mo13993id = 4) SortOrder sortOrder, @Param(mo13993id = 5) @NonNull List<String> list, @Param(mo13993id = 6) boolean z, @Param(mo13993id = 7) @NonNull List<DriveSpace> list2, @Param(mo13993id = 8) boolean z2) {
        this.zzkw = zzr;
        this.zzkx = str;
        this.zzky = sortOrder;
        this.zzkz = list;
        this.zzla = z;
        this.zzbw = list2;
        this.zzlb = z2;
    }

    private Query(zzr zzr, String str, SortOrder sortOrder, @NonNull List<String> list, boolean z, @NonNull Set<DriveSpace> set, boolean z2) {
        this(zzr, str, sortOrder, list, z, (List<DriveSpace>) new ArrayList<DriveSpace>(set), z2);
    }

    public Filter getFilter() {
        return this.zzkw;
    }

    @Deprecated
    public String getPageToken() {
        return this.zzkx;
    }

    @Nullable
    public SortOrder getSortOrder() {
        return this.zzky;
    }

    public String toString() {
        return String.format(Locale.US, "Query[%s,%s,PageToken=%s,Spaces=%s]", new Object[]{this.zzkw, this.zzky, this.zzkx, this.zzbw});
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeParcelable(parcel, 1, this.zzkw, i, false);
        SafeParcelWriter.writeString(parcel, 3, this.zzkx, false);
        SafeParcelWriter.writeParcelable(parcel, 4, this.zzky, i, false);
        SafeParcelWriter.writeStringList(parcel, 5, this.zzkz, false);
        SafeParcelWriter.writeBoolean(parcel, 6, this.zzla);
        SafeParcelWriter.writeTypedList(parcel, 7, this.zzbw, false);
        SafeParcelWriter.writeBoolean(parcel, 8, this.zzlb);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }

    public final Set<DriveSpace> zzba() {
        return new HashSet(this.zzbw);
    }
}
