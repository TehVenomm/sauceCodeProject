package com.google.android.gms.drive;

import com.google.android.gms.common.data.Freezable;
import com.google.android.gms.drive.metadata.CustomPropertyKey;
import com.google.android.gms.drive.metadata.MetadataField;
import com.google.android.gms.drive.metadata.internal.AppVisibleCustomProperties;
import com.google.android.gms.internal.zzbnr;
import com.google.android.gms.internal.zzboe;
import com.google.android.gms.internal.zzbom;
import java.util.Collections;
import java.util.Date;
import java.util.Map;

public abstract class Metadata implements Freezable<Metadata> {
    public static final int CONTENT_AVAILABLE_LOCALLY = 1;
    public static final int CONTENT_NOT_AVAILABLE_LOCALLY = 0;

    public String getAlternateLink() {
        return (String) zza(zzbnr.zzgku);
    }

    public int getContentAvailability() {
        Integer num = (Integer) zza(zzbom.zzgms);
        return num == null ? 0 : num.intValue();
    }

    public Date getCreatedDate() {
        return (Date) zza(zzboe.zzgml);
    }

    public Map<CustomPropertyKey, String> getCustomProperties() {
        AppVisibleCustomProperties appVisibleCustomProperties = (AppVisibleCustomProperties) zza(zzbnr.zzgkv);
        return appVisibleCustomProperties == null ? Collections.emptyMap() : appVisibleCustomProperties.zzanp();
    }

    public String getDescription() {
        return (String) zza(zzbnr.zzgkw);
    }

    public DriveId getDriveId() {
        return (DriveId) zza(zzbnr.zzgkt);
    }

    public String getEmbedLink() {
        return (String) zza(zzbnr.zzgkx);
    }

    public String getFileExtension() {
        return (String) zza(zzbnr.zzgky);
    }

    public long getFileSize() {
        return ((Long) zza(zzbnr.zzgkz)).longValue();
    }

    public Date getLastViewedByMeDate() {
        return (Date) zza(zzboe.zzgmm);
    }

    public String getMimeType() {
        return (String) zza(zzbnr.zzglq);
    }

    public Date getModifiedByMeDate() {
        return (Date) zza(zzboe.zzgmo);
    }

    public Date getModifiedDate() {
        return (Date) zza(zzboe.zzgmn);
    }

    public String getOriginalFilename() {
        return (String) zza(zzbnr.zzglr);
    }

    public long getQuotaBytesUsed() {
        return ((Long) zza(zzbnr.zzglw)).longValue();
    }

    public Date getSharedWithMeDate() {
        return (Date) zza(zzboe.zzgmp);
    }

    public String getTitle() {
        return (String) zza(zzbnr.zzglz);
    }

    public String getWebContentLink() {
        return (String) zza(zzbnr.zzgmb);
    }

    public String getWebViewLink() {
        return (String) zza(zzbnr.zzgmc);
    }

    public boolean isEditable() {
        Boolean bool = (Boolean) zza(zzbnr.zzglf);
        return bool == null ? false : bool.booleanValue();
    }

    public boolean isExplicitlyTrashed() {
        Boolean bool = (Boolean) zza(zzbnr.zzglg);
        return bool == null ? false : bool.booleanValue();
    }

    public boolean isFolder() {
        return DriveFolder.MIME_TYPE.equals(getMimeType());
    }

    public boolean isInAppFolder() {
        Boolean bool = (Boolean) zza(zzbnr.zzgld);
        return bool == null ? false : bool.booleanValue();
    }

    public boolean isPinnable() {
        Boolean bool = (Boolean) zza(zzbom.zzgmt);
        return bool == null ? false : bool.booleanValue();
    }

    public boolean isPinned() {
        Boolean bool = (Boolean) zza(zzbnr.zzgli);
        return bool == null ? false : bool.booleanValue();
    }

    public boolean isRestricted() {
        Boolean bool = (Boolean) zza(zzbnr.zzglk);
        return bool == null ? false : bool.booleanValue();
    }

    public boolean isShared() {
        Boolean bool = (Boolean) zza(zzbnr.zzgll);
        return bool == null ? false : bool.booleanValue();
    }

    public boolean isStarred() {
        Boolean bool = (Boolean) zza(zzbnr.zzglx);
        return bool == null ? false : bool.booleanValue();
    }

    public boolean isTrashable() {
        Boolean bool = (Boolean) zza(zzbnr.zzglo);
        return bool == null ? true : bool.booleanValue();
    }

    public boolean isTrashed() {
        Boolean bool = (Boolean) zza(zzbnr.zzgma);
        return bool == null ? false : bool.booleanValue();
    }

    public boolean isViewed() {
        Boolean bool = (Boolean) zza(zzbnr.zzglp);
        return bool == null ? false : bool.booleanValue();
    }

    public abstract <T> T zza(MetadataField<T> metadataField);
}
