package com.google.android.gms.drive.query;

import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.metadata.SearchableCollectionMetadataField;
import com.google.android.gms.drive.metadata.SearchableMetadataField;
import com.google.android.gms.drive.metadata.SearchableOrderedMetadataField;
import com.google.android.gms.drive.metadata.internal.AppVisibleCustomProperties;
import com.google.android.gms.internal.zzbnr;
import com.google.android.gms.internal.zzboe;
import java.util.Date;

public class SearchableField {
    public static final SearchableMetadataField<Boolean> IS_PINNED = zzbnr.zzgli;
    public static final SearchableOrderedMetadataField<Date> LAST_VIEWED_BY_ME = zzboe.zzgmm;
    public static final SearchableMetadataField<String> MIME_TYPE = zzbnr.zzglq;
    public static final SearchableOrderedMetadataField<Date> MODIFIED_DATE = zzboe.zzgmn;
    public static final SearchableCollectionMetadataField<DriveId> PARENTS = zzbnr.zzglv;
    public static final SearchableMetadataField<Boolean> STARRED = zzbnr.zzglx;
    public static final SearchableMetadataField<String> TITLE = zzbnr.zzglz;
    public static final SearchableMetadataField<Boolean> TRASHED = zzbnr.zzgma;
    public static final SearchableOrderedMetadataField<Date> zzgnb = zzboe.zzgmp;
    public static final SearchableMetadataField<AppVisibleCustomProperties> zzgnc = zzbnr.zzgkv;
}
