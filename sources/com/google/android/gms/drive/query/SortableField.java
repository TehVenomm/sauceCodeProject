package com.google.android.gms.drive.query;

import com.google.android.gms.drive.metadata.SortableMetadataField;
import com.google.android.gms.internal.zzbnr;
import com.google.android.gms.internal.zzboe;
import java.util.Date;

public class SortableField {
    public static final SortableMetadataField<Date> CREATED_DATE = zzboe.zzgml;
    public static final SortableMetadataField<Date> LAST_VIEWED_BY_ME = zzboe.zzgmm;
    public static final SortableMetadataField<Date> MODIFIED_BY_ME_DATE = zzboe.zzgmo;
    public static final SortableMetadataField<Date> MODIFIED_DATE = zzboe.zzgmn;
    public static final SortableMetadataField<Long> QUOTA_USED = zzbnr.zzglw;
    public static final SortableMetadataField<Date> SHARED_WITH_ME_DATE = zzboe.zzgmp;
    public static final SortableMetadataField<String> TITLE = zzbnr.zzglz;
    private static SortableMetadataField<Date> zzgnf = zzboe.zzgmq;
}
