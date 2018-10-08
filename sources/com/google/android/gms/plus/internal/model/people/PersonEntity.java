package com.google.android.gms.plus.internal.model.people;

import android.os.Parcel;
import android.support.v4.view.MotionEventCompat;
import com.facebook.internal.FacebookRequestErrorClassification;
import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.server.converter.StringToIntConverter;
import com.google.android.gms.common.server.response.FastJsonResponse.Field;
import com.google.android.gms.common.server.response.FastSafeParcelableJsonResponse;
import com.google.android.gms.plus.PlusShare;
import com.google.android.gms.plus.model.people.Person;
import com.google.android.gms.plus.model.people.Person.AgeRange;
import com.google.android.gms.plus.model.people.Person.Cover;
import com.google.android.gms.plus.model.people.Person.Cover.CoverInfo;
import com.google.android.gms.plus.model.people.Person.Cover.CoverPhoto;
import com.google.android.gms.plus.model.people.Person.Image;
import com.google.android.gms.plus.model.people.Person.Name;
import com.google.android.gms.plus.model.people.Person.Organizations;
import com.google.android.gms.plus.model.people.Person.PlacesLived;
import com.google.android.gms.plus.model.people.Person.Urls;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import io.fabric.sdk.android.services.settings.SettingsJsonConstants;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import net.gogame.gowrap.InternalConstants;

public final class PersonEntity extends FastSafeParcelableJsonResponse implements Person {
    public static final zza CREATOR = new zza();
    private static final HashMap<String, Field<?, ?>> zzazC = new HashMap();
    String zzAX;
    String zzGM;
    String zzMf;
    String zzWF;
    String zzaAB;
    AgeRangeEntity zzaAC;
    String zzaAD;
    String zzaAE;
    int zzaAF;
    CoverEntity zzaAG;
    String zzaAH;
    ImageEntity zzaAI;
    boolean zzaAJ;
    NameEntity zzaAK;
    String zzaAL;
    int zzaAM;
    List<OrganizationsEntity> zzaAN;
    List<PlacesLivedEntity> zzaAO;
    int zzaAP;
    int zzaAQ;
    String zzaAR;
    List<UrlsEntity> zzaAS;
    boolean zzaAT;
    final Set<Integer> zzazD;
    int zzqm;
    final int zzzH;

    public static final class AgeRangeEntity extends FastSafeParcelableJsonResponse implements AgeRange {
        public static final zzb CREATOR = new zzb();
        private static final HashMap<String, Field<?, ?>> zzazC = new HashMap();
        int zzaAU;
        int zzaAV;
        final Set<Integer> zzazD;
        final int zzzH;

        static {
            zzazC.put("max", Field.zzh("max", 2));
            zzazC.put("min", Field.zzh("min", 3));
        }

        public AgeRangeEntity() {
            this.zzzH = 1;
            this.zzazD = new HashSet();
        }

        AgeRangeEntity(Set<Integer> set, int i, int i2, int i3) {
            this.zzazD = set;
            this.zzzH = i;
            this.zzaAU = i2;
            this.zzaAV = i3;
        }

        public int describeContents() {
            zzb zzb = CREATOR;
            return 0;
        }

        public boolean equals(Object obj) {
            if (!(obj instanceof AgeRangeEntity)) {
                return false;
            }
            if (this == obj) {
                return true;
            }
            AgeRangeEntity ageRangeEntity = (AgeRangeEntity) obj;
            for (Field field : zzazC.values()) {
                if (zza(field)) {
                    if (!ageRangeEntity.zza(field)) {
                        return false;
                    }
                    if (!zzb(field).equals(ageRangeEntity.zzb(field))) {
                        return false;
                    }
                } else if (ageRangeEntity.zza(field)) {
                    return false;
                }
            }
            return true;
        }

        public /* synthetic */ Object freeze() {
            return zzvQ();
        }

        public int getMax() {
            return this.zzaAU;
        }

        public int getMin() {
            return this.zzaAV;
        }

        public boolean hasMax() {
            return this.zzazD.contains(Integer.valueOf(2));
        }

        public boolean hasMin() {
            return this.zzazD.contains(Integer.valueOf(3));
        }

        public int hashCode() {
            int i = 0;
            for (Field field : zzazC.values()) {
                int hashCode;
                if (zza(field)) {
                    hashCode = zzb(field).hashCode() + (i + field.zzmF());
                } else {
                    hashCode = i;
                }
                i = hashCode;
            }
            return i;
        }

        public boolean isDataValid() {
            return true;
        }

        public void writeToParcel(Parcel parcel, int i) {
            zzb zzb = CREATOR;
            zzb.zza(this, parcel, i);
        }

        protected boolean zza(Field field) {
            return this.zzazD.contains(Integer.valueOf(field.zzmF()));
        }

        protected Object zzb(Field field) {
            switch (field.zzmF()) {
                case 2:
                    return Integer.valueOf(this.zzaAU);
                case 3:
                    return Integer.valueOf(this.zzaAV);
                default:
                    throw new IllegalStateException("Unknown safe parcelable id=" + field.zzmF());
            }
        }

        public /* synthetic */ Map zzmy() {
            return zzvL();
        }

        public HashMap<String, Field<?, ?>> zzvL() {
            return zzazC;
        }

        public AgeRangeEntity zzvQ() {
            return this;
        }
    }

    public static final class CoverEntity extends FastSafeParcelableJsonResponse implements Cover {
        public static final zzc CREATOR = new zzc();
        private static final HashMap<String, Field<?, ?>> zzazC = new HashMap();
        CoverInfoEntity zzaAW;
        CoverPhotoEntity zzaAX;
        int zzaAY;
        final Set<Integer> zzazD;
        final int zzzH;

        public static final class CoverInfoEntity extends FastSafeParcelableJsonResponse implements CoverInfo {
            public static final zzd CREATOR = new zzd();
            private static final HashMap<String, Field<?, ?>> zzazC = new HashMap();
            int zzaAZ;
            int zzaBa;
            final Set<Integer> zzazD;
            final int zzzH;

            static {
                zzazC.put("leftImageOffset", Field.zzh("leftImageOffset", 2));
                zzazC.put("topImageOffset", Field.zzh("topImageOffset", 3));
            }

            public CoverInfoEntity() {
                this.zzzH = 1;
                this.zzazD = new HashSet();
            }

            CoverInfoEntity(Set<Integer> set, int i, int i2, int i3) {
                this.zzazD = set;
                this.zzzH = i;
                this.zzaAZ = i2;
                this.zzaBa = i3;
            }

            public int describeContents() {
                zzd zzd = CREATOR;
                return 0;
            }

            public boolean equals(Object obj) {
                if (!(obj instanceof CoverInfoEntity)) {
                    return false;
                }
                if (this == obj) {
                    return true;
                }
                CoverInfoEntity coverInfoEntity = (CoverInfoEntity) obj;
                for (Field field : zzazC.values()) {
                    if (zza(field)) {
                        if (!coverInfoEntity.zza(field)) {
                            return false;
                        }
                        if (!zzb(field).equals(coverInfoEntity.zzb(field))) {
                            return false;
                        }
                    } else if (coverInfoEntity.zza(field)) {
                        return false;
                    }
                }
                return true;
            }

            public /* synthetic */ Object freeze() {
                return zzvS();
            }

            public int getLeftImageOffset() {
                return this.zzaAZ;
            }

            public int getTopImageOffset() {
                return this.zzaBa;
            }

            public boolean hasLeftImageOffset() {
                return this.zzazD.contains(Integer.valueOf(2));
            }

            public boolean hasTopImageOffset() {
                return this.zzazD.contains(Integer.valueOf(3));
            }

            public int hashCode() {
                int i = 0;
                for (Field field : zzazC.values()) {
                    int hashCode;
                    if (zza(field)) {
                        hashCode = zzb(field).hashCode() + (i + field.zzmF());
                    } else {
                        hashCode = i;
                    }
                    i = hashCode;
                }
                return i;
            }

            public boolean isDataValid() {
                return true;
            }

            public void writeToParcel(Parcel parcel, int i) {
                zzd zzd = CREATOR;
                zzd.zza(this, parcel, i);
            }

            protected boolean zza(Field field) {
                return this.zzazD.contains(Integer.valueOf(field.zzmF()));
            }

            protected Object zzb(Field field) {
                switch (field.zzmF()) {
                    case 2:
                        return Integer.valueOf(this.zzaAZ);
                    case 3:
                        return Integer.valueOf(this.zzaBa);
                    default:
                        throw new IllegalStateException("Unknown safe parcelable id=" + field.zzmF());
                }
            }

            public /* synthetic */ Map zzmy() {
                return zzvL();
            }

            public HashMap<String, Field<?, ?>> zzvL() {
                return zzazC;
            }

            public CoverInfoEntity zzvS() {
                return this;
            }
        }

        public static final class CoverPhotoEntity extends FastSafeParcelableJsonResponse implements CoverPhoto {
            public static final zze CREATOR = new zze();
            private static final HashMap<String, Field<?, ?>> zzazC = new HashMap();
            String zzAX;
            final Set<Integer> zzazD;
            int zzma;
            int zzmb;
            final int zzzH;

            static {
                zzazC.put(SettingsJsonConstants.ICON_HEIGHT_KEY, Field.zzh(SettingsJsonConstants.ICON_HEIGHT_KEY, 2));
                zzazC.put("url", Field.zzk("url", 3));
                zzazC.put(SettingsJsonConstants.ICON_WIDTH_KEY, Field.zzh(SettingsJsonConstants.ICON_WIDTH_KEY, 4));
            }

            public CoverPhotoEntity() {
                this.zzzH = 1;
                this.zzazD = new HashSet();
            }

            CoverPhotoEntity(Set<Integer> set, int i, int i2, String str, int i3) {
                this.zzazD = set;
                this.zzzH = i;
                this.zzmb = i2;
                this.zzAX = str;
                this.zzma = i3;
            }

            public int describeContents() {
                zze zze = CREATOR;
                return 0;
            }

            public boolean equals(Object obj) {
                if (!(obj instanceof CoverPhotoEntity)) {
                    return false;
                }
                if (this == obj) {
                    return true;
                }
                CoverPhotoEntity coverPhotoEntity = (CoverPhotoEntity) obj;
                for (Field field : zzazC.values()) {
                    if (zza(field)) {
                        if (!coverPhotoEntity.zza(field)) {
                            return false;
                        }
                        if (!zzb(field).equals(coverPhotoEntity.zzb(field))) {
                            return false;
                        }
                    } else if (coverPhotoEntity.zza(field)) {
                        return false;
                    }
                }
                return true;
            }

            public /* synthetic */ Object freeze() {
                return zzvT();
            }

            public int getHeight() {
                return this.zzmb;
            }

            public String getUrl() {
                return this.zzAX;
            }

            public int getWidth() {
                return this.zzma;
            }

            public boolean hasHeight() {
                return this.zzazD.contains(Integer.valueOf(2));
            }

            public boolean hasUrl() {
                return this.zzazD.contains(Integer.valueOf(3));
            }

            public boolean hasWidth() {
                return this.zzazD.contains(Integer.valueOf(4));
            }

            public int hashCode() {
                int i = 0;
                for (Field field : zzazC.values()) {
                    int hashCode;
                    if (zza(field)) {
                        hashCode = zzb(field).hashCode() + (i + field.zzmF());
                    } else {
                        hashCode = i;
                    }
                    i = hashCode;
                }
                return i;
            }

            public boolean isDataValid() {
                return true;
            }

            public void writeToParcel(Parcel parcel, int i) {
                zze zze = CREATOR;
                zze.zza(this, parcel, i);
            }

            protected boolean zza(Field field) {
                return this.zzazD.contains(Integer.valueOf(field.zzmF()));
            }

            protected Object zzb(Field field) {
                switch (field.zzmF()) {
                    case 2:
                        return Integer.valueOf(this.zzmb);
                    case 3:
                        return this.zzAX;
                    case 4:
                        return Integer.valueOf(this.zzma);
                    default:
                        throw new IllegalStateException("Unknown safe parcelable id=" + field.zzmF());
                }
            }

            public /* synthetic */ Map zzmy() {
                return zzvL();
            }

            public HashMap<String, Field<?, ?>> zzvL() {
                return zzazC;
            }

            public CoverPhotoEntity zzvT() {
                return this;
            }
        }

        static {
            zzazC.put("coverInfo", Field.zza("coverInfo", 2, CoverInfoEntity.class));
            zzazC.put("coverPhoto", Field.zza("coverPhoto", 3, CoverPhotoEntity.class));
            zzazC.put("layout", Field.zza("layout", 4, new StringToIntConverter().zzg("banner", 0), false));
        }

        public CoverEntity() {
            this.zzzH = 1;
            this.zzazD = new HashSet();
        }

        CoverEntity(Set<Integer> set, int i, CoverInfoEntity coverInfoEntity, CoverPhotoEntity coverPhotoEntity, int i2) {
            this.zzazD = set;
            this.zzzH = i;
            this.zzaAW = coverInfoEntity;
            this.zzaAX = coverPhotoEntity;
            this.zzaAY = i2;
        }

        public int describeContents() {
            zzc zzc = CREATOR;
            return 0;
        }

        public boolean equals(Object obj) {
            if (!(obj instanceof CoverEntity)) {
                return false;
            }
            if (this == obj) {
                return true;
            }
            CoverEntity coverEntity = (CoverEntity) obj;
            for (Field field : zzazC.values()) {
                if (zza(field)) {
                    if (!coverEntity.zza(field)) {
                        return false;
                    }
                    if (!zzb(field).equals(coverEntity.zzb(field))) {
                        return false;
                    }
                } else if (coverEntity.zza(field)) {
                    return false;
                }
            }
            return true;
        }

        public /* synthetic */ Object freeze() {
            return zzvR();
        }

        public CoverInfo getCoverInfo() {
            return this.zzaAW;
        }

        public CoverPhoto getCoverPhoto() {
            return this.zzaAX;
        }

        public int getLayout() {
            return this.zzaAY;
        }

        public boolean hasCoverInfo() {
            return this.zzazD.contains(Integer.valueOf(2));
        }

        public boolean hasCoverPhoto() {
            return this.zzazD.contains(Integer.valueOf(3));
        }

        public boolean hasLayout() {
            return this.zzazD.contains(Integer.valueOf(4));
        }

        public int hashCode() {
            int i = 0;
            for (Field field : zzazC.values()) {
                int hashCode;
                if (zza(field)) {
                    hashCode = zzb(field).hashCode() + (i + field.zzmF());
                } else {
                    hashCode = i;
                }
                i = hashCode;
            }
            return i;
        }

        public boolean isDataValid() {
            return true;
        }

        public void writeToParcel(Parcel parcel, int i) {
            zzc zzc = CREATOR;
            zzc.zza(this, parcel, i);
        }

        protected boolean zza(Field field) {
            return this.zzazD.contains(Integer.valueOf(field.zzmF()));
        }

        protected Object zzb(Field field) {
            switch (field.zzmF()) {
                case 2:
                    return this.zzaAW;
                case 3:
                    return this.zzaAX;
                case 4:
                    return Integer.valueOf(this.zzaAY);
                default:
                    throw new IllegalStateException("Unknown safe parcelable id=" + field.zzmF());
            }
        }

        public /* synthetic */ Map zzmy() {
            return zzvL();
        }

        public HashMap<String, Field<?, ?>> zzvL() {
            return zzazC;
        }

        public CoverEntity zzvR() {
            return this;
        }
    }

    public static final class ImageEntity extends FastSafeParcelableJsonResponse implements Image {
        public static final zzf CREATOR = new zzf();
        private static final HashMap<String, Field<?, ?>> zzazC = new HashMap();
        String zzAX;
        final Set<Integer> zzazD;
        final int zzzH;

        static {
            zzazC.put("url", Field.zzk("url", 2));
        }

        public ImageEntity() {
            this.zzzH = 1;
            this.zzazD = new HashSet();
        }

        public ImageEntity(String str) {
            this.zzazD = new HashSet();
            this.zzzH = 1;
            this.zzAX = str;
            this.zzazD.add(Integer.valueOf(2));
        }

        ImageEntity(Set<Integer> set, int i, String str) {
            this.zzazD = set;
            this.zzzH = i;
            this.zzAX = str;
        }

        public int describeContents() {
            zzf zzf = CREATOR;
            return 0;
        }

        public boolean equals(Object obj) {
            if (!(obj instanceof ImageEntity)) {
                return false;
            }
            if (this == obj) {
                return true;
            }
            ImageEntity imageEntity = (ImageEntity) obj;
            for (Field field : zzazC.values()) {
                if (zza(field)) {
                    if (!imageEntity.zza(field)) {
                        return false;
                    }
                    if (!zzb(field).equals(imageEntity.zzb(field))) {
                        return false;
                    }
                } else if (imageEntity.zza(field)) {
                    return false;
                }
            }
            return true;
        }

        public /* synthetic */ Object freeze() {
            return zzvU();
        }

        public String getUrl() {
            return this.zzAX;
        }

        public boolean hasUrl() {
            return this.zzazD.contains(Integer.valueOf(2));
        }

        public int hashCode() {
            int i = 0;
            for (Field field : zzazC.values()) {
                int hashCode;
                if (zza(field)) {
                    hashCode = zzb(field).hashCode() + (i + field.zzmF());
                } else {
                    hashCode = i;
                }
                i = hashCode;
            }
            return i;
        }

        public boolean isDataValid() {
            return true;
        }

        public void writeToParcel(Parcel parcel, int i) {
            zzf zzf = CREATOR;
            zzf.zza(this, parcel, i);
        }

        protected boolean zza(Field field) {
            return this.zzazD.contains(Integer.valueOf(field.zzmF()));
        }

        protected Object zzb(Field field) {
            switch (field.zzmF()) {
                case 2:
                    return this.zzAX;
                default:
                    throw new IllegalStateException("Unknown safe parcelable id=" + field.zzmF());
            }
        }

        public /* synthetic */ Map zzmy() {
            return zzvL();
        }

        public HashMap<String, Field<?, ?>> zzvL() {
            return zzazC;
        }

        public ImageEntity zzvU() {
            return this;
        }
    }

    public static final class NameEntity extends FastSafeParcelableJsonResponse implements Name {
        public static final zzg CREATOR = new zzg();
        private static final HashMap<String, Field<?, ?>> zzazC = new HashMap();
        String zzaAb;
        String zzaAe;
        String zzaBb;
        String zzaBc;
        String zzaBd;
        String zzaBe;
        final Set<Integer> zzazD;
        final int zzzH;

        static {
            zzazC.put("familyName", Field.zzk("familyName", 2));
            zzazC.put("formatted", Field.zzk("formatted", 3));
            zzazC.put("givenName", Field.zzk("givenName", 4));
            zzazC.put("honorificPrefix", Field.zzk("honorificPrefix", 5));
            zzazC.put("honorificSuffix", Field.zzk("honorificSuffix", 6));
            zzazC.put("middleName", Field.zzk("middleName", 7));
        }

        public NameEntity() {
            this.zzzH = 1;
            this.zzazD = new HashSet();
        }

        NameEntity(Set<Integer> set, int i, String str, String str2, String str3, String str4, String str5, String str6) {
            this.zzazD = set;
            this.zzzH = i;
            this.zzaAb = str;
            this.zzaBb = str2;
            this.zzaAe = str3;
            this.zzaBc = str4;
            this.zzaBd = str5;
            this.zzaBe = str6;
        }

        public int describeContents() {
            zzg zzg = CREATOR;
            return 0;
        }

        public boolean equals(Object obj) {
            if (!(obj instanceof NameEntity)) {
                return false;
            }
            if (this == obj) {
                return true;
            }
            NameEntity nameEntity = (NameEntity) obj;
            for (Field field : zzazC.values()) {
                if (zza(field)) {
                    if (!nameEntity.zza(field)) {
                        return false;
                    }
                    if (!zzb(field).equals(nameEntity.zzb(field))) {
                        return false;
                    }
                } else if (nameEntity.zza(field)) {
                    return false;
                }
            }
            return true;
        }

        public /* synthetic */ Object freeze() {
            return zzvV();
        }

        public String getFamilyName() {
            return this.zzaAb;
        }

        public String getFormatted() {
            return this.zzaBb;
        }

        public String getGivenName() {
            return this.zzaAe;
        }

        public String getHonorificPrefix() {
            return this.zzaBc;
        }

        public String getHonorificSuffix() {
            return this.zzaBd;
        }

        public String getMiddleName() {
            return this.zzaBe;
        }

        public boolean hasFamilyName() {
            return this.zzazD.contains(Integer.valueOf(2));
        }

        public boolean hasFormatted() {
            return this.zzazD.contains(Integer.valueOf(3));
        }

        public boolean hasGivenName() {
            return this.zzazD.contains(Integer.valueOf(4));
        }

        public boolean hasHonorificPrefix() {
            return this.zzazD.contains(Integer.valueOf(5));
        }

        public boolean hasHonorificSuffix() {
            return this.zzazD.contains(Integer.valueOf(6));
        }

        public boolean hasMiddleName() {
            return this.zzazD.contains(Integer.valueOf(7));
        }

        public int hashCode() {
            int i = 0;
            for (Field field : zzazC.values()) {
                int hashCode;
                if (zza(field)) {
                    hashCode = zzb(field).hashCode() + (i + field.zzmF());
                } else {
                    hashCode = i;
                }
                i = hashCode;
            }
            return i;
        }

        public boolean isDataValid() {
            return true;
        }

        public void writeToParcel(Parcel parcel, int i) {
            zzg zzg = CREATOR;
            zzg.zza(this, parcel, i);
        }

        protected boolean zza(Field field) {
            return this.zzazD.contains(Integer.valueOf(field.zzmF()));
        }

        protected Object zzb(Field field) {
            switch (field.zzmF()) {
                case 2:
                    return this.zzaAb;
                case 3:
                    return this.zzaBb;
                case 4:
                    return this.zzaAe;
                case 5:
                    return this.zzaBc;
                case 6:
                    return this.zzaBd;
                case 7:
                    return this.zzaBe;
                default:
                    throw new IllegalStateException("Unknown safe parcelable id=" + field.zzmF());
            }
        }

        public /* synthetic */ Map zzmy() {
            return zzvL();
        }

        public HashMap<String, Field<?, ?>> zzvL() {
            return zzazC;
        }

        public NameEntity zzvV() {
            return this;
        }
    }

    public static final class OrganizationsEntity extends FastSafeParcelableJsonResponse implements Organizations {
        public static final zzh CREATOR = new zzh();
        private static final HashMap<String, Field<?, ?>> zzazC = new HashMap();
        String mName;
        int zzMG;
        String zzWn;
        String zzaAa;
        String zzaAq;
        String zzaBf;
        String zzaBg;
        boolean zzaBh;
        String zzadH;
        final Set<Integer> zzazD;
        final int zzzH;

        static {
            zzazC.put("department", Field.zzk("department", 2));
            zzazC.put("description", Field.zzk("description", 3));
            zzazC.put("endDate", Field.zzk("endDate", 4));
            zzazC.put("location", Field.zzk("location", 5));
            zzazC.put("name", Field.zzk("name", 6));
            zzazC.put("primary", Field.zzj("primary", 7));
            zzazC.put("startDate", Field.zzk("startDate", 8));
            zzazC.put("title", Field.zzk("title", 9));
            zzazC.put(ShareConstants.MEDIA_TYPE, Field.zza(ShareConstants.MEDIA_TYPE, 10, new StringToIntConverter().zzg("work", 0).zzg("school", 1), false));
        }

        public OrganizationsEntity() {
            this.zzzH = 1;
            this.zzazD = new HashSet();
        }

        OrganizationsEntity(Set<Integer> set, int i, String str, String str2, String str3, String str4, String str5, boolean z, String str6, String str7, int i2) {
            this.zzazD = set;
            this.zzzH = i;
            this.zzaBf = str;
            this.zzadH = str2;
            this.zzaAa = str3;
            this.zzaBg = str4;
            this.mName = str5;
            this.zzaBh = z;
            this.zzaAq = str6;
            this.zzWn = str7;
            this.zzMG = i2;
        }

        public int describeContents() {
            zzh zzh = CREATOR;
            return 0;
        }

        public boolean equals(Object obj) {
            if (!(obj instanceof OrganizationsEntity)) {
                return false;
            }
            if (this == obj) {
                return true;
            }
            OrganizationsEntity organizationsEntity = (OrganizationsEntity) obj;
            for (Field field : zzazC.values()) {
                if (zza(field)) {
                    if (!organizationsEntity.zza(field)) {
                        return false;
                    }
                    if (!zzb(field).equals(organizationsEntity.zzb(field))) {
                        return false;
                    }
                } else if (organizationsEntity.zza(field)) {
                    return false;
                }
            }
            return true;
        }

        public /* synthetic */ Object freeze() {
            return zzvW();
        }

        public String getDepartment() {
            return this.zzaBf;
        }

        public String getDescription() {
            return this.zzadH;
        }

        public String getEndDate() {
            return this.zzaAa;
        }

        public String getLocation() {
            return this.zzaBg;
        }

        public String getName() {
            return this.mName;
        }

        public String getStartDate() {
            return this.zzaAq;
        }

        public String getTitle() {
            return this.zzWn;
        }

        public int getType() {
            return this.zzMG;
        }

        public boolean hasDepartment() {
            return this.zzazD.contains(Integer.valueOf(2));
        }

        public boolean hasDescription() {
            return this.zzazD.contains(Integer.valueOf(3));
        }

        public boolean hasEndDate() {
            return this.zzazD.contains(Integer.valueOf(4));
        }

        public boolean hasLocation() {
            return this.zzazD.contains(Integer.valueOf(5));
        }

        public boolean hasName() {
            return this.zzazD.contains(Integer.valueOf(6));
        }

        public boolean hasPrimary() {
            return this.zzazD.contains(Integer.valueOf(7));
        }

        public boolean hasStartDate() {
            return this.zzazD.contains(Integer.valueOf(8));
        }

        public boolean hasTitle() {
            return this.zzazD.contains(Integer.valueOf(9));
        }

        public boolean hasType() {
            return this.zzazD.contains(Integer.valueOf(10));
        }

        public int hashCode() {
            int i = 0;
            for (Field field : zzazC.values()) {
                int hashCode;
                if (zza(field)) {
                    hashCode = zzb(field).hashCode() + (i + field.zzmF());
                } else {
                    hashCode = i;
                }
                i = hashCode;
            }
            return i;
        }

        public boolean isDataValid() {
            return true;
        }

        public boolean isPrimary() {
            return this.zzaBh;
        }

        public void writeToParcel(Parcel parcel, int i) {
            zzh zzh = CREATOR;
            zzh.zza(this, parcel, i);
        }

        protected boolean zza(Field field) {
            return this.zzazD.contains(Integer.valueOf(field.zzmF()));
        }

        protected Object zzb(Field field) {
            switch (field.zzmF()) {
                case 2:
                    return this.zzaBf;
                case 3:
                    return this.zzadH;
                case 4:
                    return this.zzaAa;
                case 5:
                    return this.zzaBg;
                case 6:
                    return this.mName;
                case 7:
                    return Boolean.valueOf(this.zzaBh);
                case 8:
                    return this.zzaAq;
                case 9:
                    return this.zzWn;
                case 10:
                    return Integer.valueOf(this.zzMG);
                default:
                    throw new IllegalStateException("Unknown safe parcelable id=" + field.zzmF());
            }
        }

        public /* synthetic */ Map zzmy() {
            return zzvL();
        }

        public HashMap<String, Field<?, ?>> zzvL() {
            return zzazC;
        }

        public OrganizationsEntity zzvW() {
            return this;
        }
    }

    public static final class PlacesLivedEntity extends FastSafeParcelableJsonResponse implements PlacesLived {
        public static final zzi CREATOR = new zzi();
        private static final HashMap<String, Field<?, ?>> zzazC = new HashMap();
        String mValue;
        boolean zzaBh;
        final Set<Integer> zzazD;
        final int zzzH;

        static {
            zzazC.put("primary", Field.zzj("primary", 2));
            zzazC.put(Param.VALUE, Field.zzk(Param.VALUE, 3));
        }

        public PlacesLivedEntity() {
            this.zzzH = 1;
            this.zzazD = new HashSet();
        }

        PlacesLivedEntity(Set<Integer> set, int i, boolean z, String str) {
            this.zzazD = set;
            this.zzzH = i;
            this.zzaBh = z;
            this.mValue = str;
        }

        public int describeContents() {
            zzi zzi = CREATOR;
            return 0;
        }

        public boolean equals(Object obj) {
            if (!(obj instanceof PlacesLivedEntity)) {
                return false;
            }
            if (this == obj) {
                return true;
            }
            PlacesLivedEntity placesLivedEntity = (PlacesLivedEntity) obj;
            for (Field field : zzazC.values()) {
                if (zza(field)) {
                    if (!placesLivedEntity.zza(field)) {
                        return false;
                    }
                    if (!zzb(field).equals(placesLivedEntity.zzb(field))) {
                        return false;
                    }
                } else if (placesLivedEntity.zza(field)) {
                    return false;
                }
            }
            return true;
        }

        public /* synthetic */ Object freeze() {
            return zzvX();
        }

        public String getValue() {
            return this.mValue;
        }

        public boolean hasPrimary() {
            return this.zzazD.contains(Integer.valueOf(2));
        }

        public boolean hasValue() {
            return this.zzazD.contains(Integer.valueOf(3));
        }

        public int hashCode() {
            int i = 0;
            for (Field field : zzazC.values()) {
                int hashCode;
                if (zza(field)) {
                    hashCode = zzb(field).hashCode() + (i + field.zzmF());
                } else {
                    hashCode = i;
                }
                i = hashCode;
            }
            return i;
        }

        public boolean isDataValid() {
            return true;
        }

        public boolean isPrimary() {
            return this.zzaBh;
        }

        public void writeToParcel(Parcel parcel, int i) {
            zzi zzi = CREATOR;
            zzi.zza(this, parcel, i);
        }

        protected boolean zza(Field field) {
            return this.zzazD.contains(Integer.valueOf(field.zzmF()));
        }

        protected Object zzb(Field field) {
            switch (field.zzmF()) {
                case 2:
                    return Boolean.valueOf(this.zzaBh);
                case 3:
                    return this.mValue;
                default:
                    throw new IllegalStateException("Unknown safe parcelable id=" + field.zzmF());
            }
        }

        public /* synthetic */ Map zzmy() {
            return zzvL();
        }

        public HashMap<String, Field<?, ?>> zzvL() {
            return zzazC;
        }

        public PlacesLivedEntity zzvX() {
            return this;
        }
    }

    public static final class UrlsEntity extends FastSafeParcelableJsonResponse implements Urls {
        public static final zzj CREATOR = new zzj();
        private static final HashMap<String, Field<?, ?>> zzazC = new HashMap();
        String mValue;
        int zzMG;
        private final int zzaBi;
        String zzawK;
        final Set<Integer> zzazD;
        final int zzzH;

        static {
            zzazC.put(PlusShare.KEY_CALL_TO_ACTION_LABEL, Field.zzk(PlusShare.KEY_CALL_TO_ACTION_LABEL, 5));
            zzazC.put(ShareConstants.MEDIA_TYPE, Field.zza(ShareConstants.MEDIA_TYPE, 6, new StringToIntConverter().zzg(InternalConstants.COMMUNITY_HOME, 0).zzg("work", 1).zzg("blog", 2).zzg(Scopes.PROFILE, 3).zzg(FacebookRequestErrorClassification.KEY_OTHER, 4).zzg("otherProfile", 5).zzg("contributor", 6).zzg("website", 7), false));
            zzazC.put(Param.VALUE, Field.zzk(Param.VALUE, 4));
        }

        public UrlsEntity() {
            this.zzaBi = 4;
            this.zzzH = 1;
            this.zzazD = new HashSet();
        }

        UrlsEntity(Set<Integer> set, int i, String str, int i2, String str2, int i3) {
            this.zzaBi = 4;
            this.zzazD = set;
            this.zzzH = i;
            this.zzawK = str;
            this.zzMG = i2;
            this.mValue = str2;
        }

        public int describeContents() {
            zzj zzj = CREATOR;
            return 0;
        }

        public boolean equals(Object obj) {
            if (!(obj instanceof UrlsEntity)) {
                return false;
            }
            if (this == obj) {
                return true;
            }
            UrlsEntity urlsEntity = (UrlsEntity) obj;
            for (Field field : zzazC.values()) {
                if (zza(field)) {
                    if (!urlsEntity.zza(field)) {
                        return false;
                    }
                    if (!zzb(field).equals(urlsEntity.zzb(field))) {
                        return false;
                    }
                } else if (urlsEntity.zza(field)) {
                    return false;
                }
            }
            return true;
        }

        public /* synthetic */ Object freeze() {
            return zzvZ();
        }

        public String getLabel() {
            return this.zzawK;
        }

        public int getType() {
            return this.zzMG;
        }

        public String getValue() {
            return this.mValue;
        }

        public boolean hasLabel() {
            return this.zzazD.contains(Integer.valueOf(5));
        }

        public boolean hasType() {
            return this.zzazD.contains(Integer.valueOf(6));
        }

        public boolean hasValue() {
            return this.zzazD.contains(Integer.valueOf(4));
        }

        public int hashCode() {
            int i = 0;
            for (Field field : zzazC.values()) {
                int hashCode;
                if (zza(field)) {
                    hashCode = zzb(field).hashCode() + (i + field.zzmF());
                } else {
                    hashCode = i;
                }
                i = hashCode;
            }
            return i;
        }

        public boolean isDataValid() {
            return true;
        }

        public void writeToParcel(Parcel parcel, int i) {
            zzj zzj = CREATOR;
            zzj.zza(this, parcel, i);
        }

        protected boolean zza(Field field) {
            return this.zzazD.contains(Integer.valueOf(field.zzmF()));
        }

        protected Object zzb(Field field) {
            switch (field.zzmF()) {
                case 4:
                    return this.mValue;
                case 5:
                    return this.zzawK;
                case 6:
                    return Integer.valueOf(this.zzMG);
                default:
                    throw new IllegalStateException("Unknown safe parcelable id=" + field.zzmF());
            }
        }

        public /* synthetic */ Map zzmy() {
            return zzvL();
        }

        public HashMap<String, Field<?, ?>> zzvL() {
            return zzazC;
        }

        @Deprecated
        public int zzvY() {
            return 4;
        }

        public UrlsEntity zzvZ() {
            return this;
        }
    }

    public static class zza {
        public static int zzdq(String str) {
            if (str.equals("person")) {
                return 0;
            }
            if (str.equals("page")) {
                return 1;
            }
            throw new IllegalArgumentException("Unknown objectType string: " + str);
        }
    }

    static {
        zzazC.put("aboutMe", Field.zzk("aboutMe", 2));
        zzazC.put("ageRange", Field.zza("ageRange", 3, AgeRangeEntity.class));
        zzazC.put("birthday", Field.zzk("birthday", 4));
        zzazC.put("braggingRights", Field.zzk("braggingRights", 5));
        zzazC.put("circledByCount", Field.zzh("circledByCount", 6));
        zzazC.put("cover", Field.zza("cover", 7, CoverEntity.class));
        zzazC.put("currentLocation", Field.zzk("currentLocation", 8));
        zzazC.put("displayName", Field.zzk("displayName", 9));
        zzazC.put("gender", Field.zza("gender", 12, new StringToIntConverter().zzg("male", 0).zzg("female", 1).zzg(FacebookRequestErrorClassification.KEY_OTHER, 2), false));
        zzazC.put("id", Field.zzk("id", 14));
        zzazC.put("image", Field.zza("image", 15, ImageEntity.class));
        zzazC.put("isPlusUser", Field.zzj("isPlusUser", 16));
        zzazC.put("language", Field.zzk("language", 18));
        zzazC.put("name", Field.zza("name", 19, NameEntity.class));
        zzazC.put("nickname", Field.zzk("nickname", 20));
        zzazC.put("objectType", Field.zza("objectType", 21, new StringToIntConverter().zzg("person", 0).zzg("page", 1), false));
        zzazC.put("organizations", Field.zzb("organizations", 22, OrganizationsEntity.class));
        zzazC.put("placesLived", Field.zzb("placesLived", 23, PlacesLivedEntity.class));
        zzazC.put("plusOneCount", Field.zzh("plusOneCount", 24));
        zzazC.put("relationshipStatus", Field.zza("relationshipStatus", 25, new StringToIntConverter().zzg("single", 0).zzg("in_a_relationship", 1).zzg("engaged", 2).zzg("married", 3).zzg("its_complicated", 4).zzg("open_relationship", 5).zzg("widowed", 6).zzg("in_domestic_partnership", 7).zzg("in_civil_union", 8), false));
        zzazC.put("tagline", Field.zzk("tagline", 26));
        zzazC.put("url", Field.zzk("url", 27));
        zzazC.put("urls", Field.zzb("urls", 28, UrlsEntity.class));
        zzazC.put("verified", Field.zzj("verified", 29));
    }

    public PersonEntity() {
        this.zzzH = 1;
        this.zzazD = new HashSet();
    }

    public PersonEntity(String str, String str2, ImageEntity imageEntity, int i, String str3) {
        this.zzzH = 1;
        this.zzazD = new HashSet();
        this.zzWF = str;
        this.zzazD.add(Integer.valueOf(9));
        this.zzGM = str2;
        this.zzazD.add(Integer.valueOf(14));
        this.zzaAI = imageEntity;
        this.zzazD.add(Integer.valueOf(15));
        this.zzaAM = i;
        this.zzazD.add(Integer.valueOf(21));
        this.zzAX = str3;
        this.zzazD.add(Integer.valueOf(27));
    }

    PersonEntity(Set<Integer> set, int i, String str, AgeRangeEntity ageRangeEntity, String str2, String str3, int i2, CoverEntity coverEntity, String str4, String str5, int i3, String str6, ImageEntity imageEntity, boolean z, String str7, NameEntity nameEntity, String str8, int i4, List<OrganizationsEntity> list, List<PlacesLivedEntity> list2, int i5, int i6, String str9, String str10, List<UrlsEntity> list3, boolean z2) {
        this.zzazD = set;
        this.zzzH = i;
        this.zzaAB = str;
        this.zzaAC = ageRangeEntity;
        this.zzaAD = str2;
        this.zzaAE = str3;
        this.zzaAF = i2;
        this.zzaAG = coverEntity;
        this.zzaAH = str4;
        this.zzWF = str5;
        this.zzqm = i3;
        this.zzGM = str6;
        this.zzaAI = imageEntity;
        this.zzaAJ = z;
        this.zzMf = str7;
        this.zzaAK = nameEntity;
        this.zzaAL = str8;
        this.zzaAM = i4;
        this.zzaAN = list;
        this.zzaAO = list2;
        this.zzaAP = i5;
        this.zzaAQ = i6;
        this.zzaAR = str9;
        this.zzAX = str10;
        this.zzaAS = list3;
        this.zzaAT = z2;
    }

    public static PersonEntity zzl(byte[] bArr) {
        Parcel obtain = Parcel.obtain();
        obtain.unmarshall(bArr, 0, bArr.length);
        obtain.setDataPosition(0);
        PersonEntity zzeU = CREATOR.zzeU(obtain);
        obtain.recycle();
        return zzeU;
    }

    public int describeContents() {
        zza zza = CREATOR;
        return 0;
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof PersonEntity)) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        PersonEntity personEntity = (PersonEntity) obj;
        for (Field field : zzazC.values()) {
            if (zza(field)) {
                if (!personEntity.zza(field)) {
                    return false;
                }
                if (!zzb(field).equals(personEntity.zzb(field))) {
                    return false;
                }
            } else if (personEntity.zza(field)) {
                return false;
            }
        }
        return true;
    }

    public /* synthetic */ Object freeze() {
        return zzvP();
    }

    public String getAboutMe() {
        return this.zzaAB;
    }

    public AgeRange getAgeRange() {
        return this.zzaAC;
    }

    public String getBirthday() {
        return this.zzaAD;
    }

    public String getBraggingRights() {
        return this.zzaAE;
    }

    public int getCircledByCount() {
        return this.zzaAF;
    }

    public Cover getCover() {
        return this.zzaAG;
    }

    public String getCurrentLocation() {
        return this.zzaAH;
    }

    public String getDisplayName() {
        return this.zzWF;
    }

    public int getGender() {
        return this.zzqm;
    }

    public String getId() {
        return this.zzGM;
    }

    public Image getImage() {
        return this.zzaAI;
    }

    public String getLanguage() {
        return this.zzMf;
    }

    public Name getName() {
        return this.zzaAK;
    }

    public String getNickname() {
        return this.zzaAL;
    }

    public int getObjectType() {
        return this.zzaAM;
    }

    public List<Organizations> getOrganizations() {
        return (ArrayList) this.zzaAN;
    }

    public List<PlacesLived> getPlacesLived() {
        return (ArrayList) this.zzaAO;
    }

    public int getPlusOneCount() {
        return this.zzaAP;
    }

    public int getRelationshipStatus() {
        return this.zzaAQ;
    }

    public String getTagline() {
        return this.zzaAR;
    }

    public String getUrl() {
        return this.zzAX;
    }

    public List<Urls> getUrls() {
        return (ArrayList) this.zzaAS;
    }

    public boolean hasAboutMe() {
        return this.zzazD.contains(Integer.valueOf(2));
    }

    public boolean hasAgeRange() {
        return this.zzazD.contains(Integer.valueOf(3));
    }

    public boolean hasBirthday() {
        return this.zzazD.contains(Integer.valueOf(4));
    }

    public boolean hasBraggingRights() {
        return this.zzazD.contains(Integer.valueOf(5));
    }

    public boolean hasCircledByCount() {
        return this.zzazD.contains(Integer.valueOf(6));
    }

    public boolean hasCover() {
        return this.zzazD.contains(Integer.valueOf(7));
    }

    public boolean hasCurrentLocation() {
        return this.zzazD.contains(Integer.valueOf(8));
    }

    public boolean hasDisplayName() {
        return this.zzazD.contains(Integer.valueOf(9));
    }

    public boolean hasGender() {
        return this.zzazD.contains(Integer.valueOf(12));
    }

    public boolean hasId() {
        return this.zzazD.contains(Integer.valueOf(14));
    }

    public boolean hasImage() {
        return this.zzazD.contains(Integer.valueOf(15));
    }

    public boolean hasIsPlusUser() {
        return this.zzazD.contains(Integer.valueOf(16));
    }

    public boolean hasLanguage() {
        return this.zzazD.contains(Integer.valueOf(18));
    }

    public boolean hasName() {
        return this.zzazD.contains(Integer.valueOf(19));
    }

    public boolean hasNickname() {
        return this.zzazD.contains(Integer.valueOf(20));
    }

    public boolean hasObjectType() {
        return this.zzazD.contains(Integer.valueOf(21));
    }

    public boolean hasOrganizations() {
        return this.zzazD.contains(Integer.valueOf(22));
    }

    public boolean hasPlacesLived() {
        return this.zzazD.contains(Integer.valueOf(23));
    }

    public boolean hasPlusOneCount() {
        return this.zzazD.contains(Integer.valueOf(24));
    }

    public boolean hasRelationshipStatus() {
        return this.zzazD.contains(Integer.valueOf(25));
    }

    public boolean hasTagline() {
        return this.zzazD.contains(Integer.valueOf(26));
    }

    public boolean hasUrl() {
        return this.zzazD.contains(Integer.valueOf(27));
    }

    public boolean hasUrls() {
        return this.zzazD.contains(Integer.valueOf(28));
    }

    public boolean hasVerified() {
        return this.zzazD.contains(Integer.valueOf(29));
    }

    public int hashCode() {
        int i = 0;
        for (Field field : zzazC.values()) {
            int hashCode;
            if (zza(field)) {
                hashCode = zzb(field).hashCode() + (i + field.zzmF());
            } else {
                hashCode = i;
            }
            i = hashCode;
        }
        return i;
    }

    public boolean isDataValid() {
        return true;
    }

    public boolean isPlusUser() {
        return this.zzaAJ;
    }

    public boolean isVerified() {
        return this.zzaAT;
    }

    public void writeToParcel(Parcel parcel, int i) {
        zza zza = CREATOR;
        zza.zza(this, parcel, i);
    }

    protected boolean zza(Field field) {
        return this.zzazD.contains(Integer.valueOf(field.zzmF()));
    }

    protected Object zzb(Field field) {
        switch (field.zzmF()) {
            case 2:
                return this.zzaAB;
            case 3:
                return this.zzaAC;
            case 4:
                return this.zzaAD;
            case 5:
                return this.zzaAE;
            case 6:
                return Integer.valueOf(this.zzaAF);
            case 7:
                return this.zzaAG;
            case 8:
                return this.zzaAH;
            case 9:
                return this.zzWF;
            case 12:
                return Integer.valueOf(this.zzqm);
            case 14:
                return this.zzGM;
            case 15:
                return this.zzaAI;
            case 16:
                return Boolean.valueOf(this.zzaAJ);
            case 18:
                return this.zzMf;
            case 19:
                return this.zzaAK;
            case 20:
                return this.zzaAL;
            case 21:
                return Integer.valueOf(this.zzaAM);
            case 22:
                return this.zzaAN;
            case 23:
                return this.zzaAO;
            case MotionEventCompat.AXIS_DISTANCE /*24*/:
                return Integer.valueOf(this.zzaAP);
            case 25:
                return Integer.valueOf(this.zzaAQ);
            case 26:
                return this.zzaAR;
            case MotionEventCompat.AXIS_RELATIVE_X /*27*/:
                return this.zzAX;
            case MotionEventCompat.AXIS_RELATIVE_Y /*28*/:
                return this.zzaAS;
            case 29:
                return Boolean.valueOf(this.zzaAT);
            default:
                throw new IllegalStateException("Unknown safe parcelable id=" + field.zzmF());
        }
    }

    public /* synthetic */ Map zzmy() {
        return zzvL();
    }

    public HashMap<String, Field<?, ?>> zzvL() {
        return zzazC;
    }

    public PersonEntity zzvP() {
        return this;
    }
}
