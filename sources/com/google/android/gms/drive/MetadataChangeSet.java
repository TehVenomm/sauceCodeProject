package com.google.android.gms.drive;

import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.metadata.CustomPropertyKey;
import com.google.android.gms.drive.metadata.MetadataField;
import com.google.android.gms.drive.metadata.internal.AppVisibleCustomProperties;
import com.google.android.gms.drive.metadata.internal.AppVisibleCustomProperties.zza;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;
import com.google.android.gms.internal.zzbnr;
import com.google.android.gms.internal.zzboe;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.util.Collections;
import java.util.Date;
import java.util.Map;

public final class MetadataChangeSet {
    public static final int CUSTOM_PROPERTY_SIZE_LIMIT_BYTES = 124;
    public static final int INDEXABLE_TEXT_SIZE_LIMIT_BYTES = 131072;
    public static final int MAX_PRIVATE_PROPERTIES_PER_RESOURCE_PER_APP = 30;
    public static final int MAX_PUBLIC_PROPERTIES_PER_RESOURCE = 30;
    public static final int MAX_TOTAL_PROPERTIES_PER_RESOURCE = 100;
    public static final MetadataChangeSet zzgeb = new MetadataChangeSet(MetadataBundle.zzant());
    private final MetadataBundle zzgec;

    public static class Builder {
        private final MetadataBundle zzgec = MetadataBundle.zzant();
        private zza zzged;

        private final zza zzanb() {
            if (this.zzged == null) {
                this.zzged = new zza();
            }
            return this.zzged;
        }

        private static int zzgp(String str) {
            return str == null ? 0 : str.getBytes().length;
        }

        private static void zzi(String str, int i, int i2) {
            zzbp.zzb(i2 <= i, String.format("%s must be no more than %d bytes, but is %d bytes.", new Object[]{str, Integer.valueOf(i), Integer.valueOf(i2)}));
        }

        public MetadataChangeSet build() {
            if (this.zzged != null) {
                this.zzgec.zzc(zzbnr.zzgkv, this.zzged.zzanq());
            }
            return new MetadataChangeSet(this.zzgec);
        }

        public Builder deleteCustomProperty(CustomPropertyKey customPropertyKey) {
            zzbp.zzb((Object) customPropertyKey, (Object) "key");
            zzanb().zza(customPropertyKey, null);
            return this;
        }

        public Builder setCustomProperty(CustomPropertyKey customPropertyKey, String str) {
            zzbp.zzb((Object) customPropertyKey, (Object) "key");
            zzbp.zzb((Object) str, Param.VALUE);
            zzi("The total size of key string and value string of a custom property", MetadataChangeSet.CUSTOM_PROPERTY_SIZE_LIMIT_BYTES, zzgp(customPropertyKey.getKey()) + zzgp(str));
            zzanb().zza(customPropertyKey, str);
            return this;
        }

        public Builder setDescription(String str) {
            this.zzgec.zzc(zzbnr.zzgkw, str);
            return this;
        }

        public Builder setIndexableText(String str) {
            zzi("Indexable text size", 131072, zzgp(str));
            this.zzgec.zzc(zzbnr.zzglc, str);
            return this;
        }

        public Builder setLastViewedByMeDate(Date date) {
            this.zzgec.zzc(zzboe.zzgmm, date);
            return this;
        }

        public Builder setMimeType(String str) {
            this.zzgec.zzc(zzbnr.zzglq, str);
            return this;
        }

        public Builder setPinned(boolean z) {
            this.zzgec.zzc(zzbnr.zzgli, Boolean.valueOf(z));
            return this;
        }

        public Builder setStarred(boolean z) {
            this.zzgec.zzc(zzbnr.zzglx, Boolean.valueOf(z));
            return this;
        }

        public Builder setTitle(String str) {
            this.zzgec.zzc(zzbnr.zzglz, str);
            return this;
        }

        public Builder setViewed(boolean z) {
            this.zzgec.zzc(zzbnr.zzglp, Boolean.valueOf(z));
            return this;
        }
    }

    public MetadataChangeSet(MetadataBundle metadataBundle) {
        this.zzgec = metadataBundle.zzanu();
    }

    public final Map<CustomPropertyKey, String> getCustomPropertyChangeMap() {
        AppVisibleCustomProperties appVisibleCustomProperties = (AppVisibleCustomProperties) this.zzgec.zza(zzbnr.zzgkv);
        return appVisibleCustomProperties == null ? Collections.emptyMap() : appVisibleCustomProperties.zzanp();
    }

    public final String getDescription() {
        return (String) this.zzgec.zza(zzbnr.zzgkw);
    }

    public final String getIndexableText() {
        return (String) this.zzgec.zza(zzbnr.zzglc);
    }

    public final Date getLastViewedByMeDate() {
        return (Date) this.zzgec.zza(zzboe.zzgmm);
    }

    public final String getMimeType() {
        return (String) this.zzgec.zza(zzbnr.zzglq);
    }

    public final String getTitle() {
        return (String) this.zzgec.zza(zzbnr.zzglz);
    }

    public final Boolean isPinned() {
        return (Boolean) this.zzgec.zza(zzbnr.zzgli);
    }

    public final Boolean isStarred() {
        return (Boolean) this.zzgec.zza(zzbnr.zzglx);
    }

    public final Boolean isViewed() {
        return (Boolean) this.zzgec.zza(zzbnr.zzglp);
    }

    public final <T> MetadataChangeSet zza(MetadataField<T> metadataField, T t) {
        MetadataChangeSet metadataChangeSet = new MetadataChangeSet(this.zzgec);
        metadataChangeSet.zzgec.zzc(metadataField, t);
        return metadataChangeSet;
    }

    public final MetadataBundle zzana() {
        return this.zzgec;
    }
}
