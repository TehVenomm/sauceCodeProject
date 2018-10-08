package com.google.android.gms.plus.internal.model.moments;

import android.os.Parcel;
import android.support.v4.view.MotionEventCompat;
import com.facebook.GraphRequest;
import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.common.server.response.FastJsonResponse.Field;
import com.google.android.gms.common.server.response.FastSafeParcelableJsonResponse;
import com.google.android.gms.plus.PlusShare;
import com.google.android.gms.plus.model.moments.ItemScope;
import io.fabric.sdk.android.services.settings.SettingsJsonConstants;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;

public final class ItemScopeEntity extends FastSafeParcelableJsonResponse implements ItemScope {
    public static final zza CREATOR = new zza();
    private static final HashMap<String, Field<?, ?>> zzazC = new HashMap();
    String mName;
    String zzAV;
    String zzAX;
    String zzGM;
    String zzaAa;
    String zzaAb;
    String zzaAc;
    ItemScopeEntity zzaAd;
    String zzaAe;
    String zzaAf;
    String zzaAg;
    ItemScopeEntity zzaAh;
    ItemScopeEntity zzaAi;
    ItemScopeEntity zzaAj;
    List<ItemScopeEntity> zzaAk;
    String zzaAl;
    String zzaAm;
    String zzaAn;
    String zzaAo;
    ItemScopeEntity zzaAp;
    String zzaAq;
    String zzaAr;
    String zzaAs;
    ItemScopeEntity zzaAt;
    String zzaAu;
    String zzaAv;
    String zzaAw;
    String zzaAx;
    String zzadH;
    double zzapL;
    double zzapM;
    final Set<Integer> zzazD;
    ItemScopeEntity zzazE;
    List<String> zzazF;
    ItemScopeEntity zzazG;
    String zzazH;
    String zzazI;
    String zzazJ;
    List<ItemScopeEntity> zzazK;
    int zzazL;
    List<ItemScopeEntity> zzazM;
    ItemScopeEntity zzazN;
    List<ItemScopeEntity> zzazO;
    String zzazP;
    String zzazQ;
    ItemScopeEntity zzazR;
    String zzazS;
    String zzazT;
    List<ItemScopeEntity> zzazU;
    String zzazV;
    String zzazW;
    String zzazX;
    String zzazY;
    String zzazZ;
    String zzql;
    final int zzzH;

    static {
        zzazC.put("about", Field.zza("about", 2, ItemScopeEntity.class));
        zzazC.put("additionalName", Field.zzl("additionalName", 3));
        zzazC.put("address", Field.zza("address", 4, ItemScopeEntity.class));
        zzazC.put("addressCountry", Field.zzk("addressCountry", 5));
        zzazC.put("addressLocality", Field.zzk("addressLocality", 6));
        zzazC.put("addressRegion", Field.zzk("addressRegion", 7));
        zzazC.put("associated_media", Field.zzb("associated_media", 8, ItemScopeEntity.class));
        zzazC.put("attendeeCount", Field.zzh("attendeeCount", 9));
        zzazC.put("attendees", Field.zzb("attendees", 10, ItemScopeEntity.class));
        zzazC.put("audio", Field.zza("audio", 11, ItemScopeEntity.class));
        zzazC.put("author", Field.zzb("author", 12, ItemScopeEntity.class));
        zzazC.put("bestRating", Field.zzk("bestRating", 13));
        zzazC.put("birthDate", Field.zzk("birthDate", 14));
        zzazC.put("byArtist", Field.zza("byArtist", 15, ItemScopeEntity.class));
        zzazC.put(ShareConstants.FEED_CAPTION_PARAM, Field.zzk(ShareConstants.FEED_CAPTION_PARAM, 16));
        zzazC.put("contentSize", Field.zzk("contentSize", 17));
        zzazC.put("contentUrl", Field.zzk("contentUrl", 18));
        zzazC.put("contributor", Field.zzb("contributor", 19, ItemScopeEntity.class));
        zzazC.put("dateCreated", Field.zzk("dateCreated", 20));
        zzazC.put("dateModified", Field.zzk("dateModified", 21));
        zzazC.put("datePublished", Field.zzk("datePublished", 22));
        zzazC.put("description", Field.zzk("description", 23));
        zzazC.put("duration", Field.zzk("duration", 24));
        zzazC.put("embedUrl", Field.zzk("embedUrl", 25));
        zzazC.put("endDate", Field.zzk("endDate", 26));
        zzazC.put("familyName", Field.zzk("familyName", 27));
        zzazC.put("gender", Field.zzk("gender", 28));
        zzazC.put("geo", Field.zza("geo", 29, ItemScopeEntity.class));
        zzazC.put("givenName", Field.zzk("givenName", 30));
        zzazC.put(SettingsJsonConstants.ICON_HEIGHT_KEY, Field.zzk(SettingsJsonConstants.ICON_HEIGHT_KEY, 31));
        zzazC.put("id", Field.zzk("id", 32));
        zzazC.put("image", Field.zzk("image", 33));
        zzazC.put("inAlbum", Field.zza("inAlbum", 34, ItemScopeEntity.class));
        zzazC.put("latitude", Field.zzi("latitude", 36));
        zzazC.put("location", Field.zza("location", 37, ItemScopeEntity.class));
        zzazC.put("longitude", Field.zzi("longitude", 38));
        zzazC.put("name", Field.zzk("name", 39));
        zzazC.put("partOfTVSeries", Field.zza("partOfTVSeries", 40, ItemScopeEntity.class));
        zzazC.put("performers", Field.zzb("performers", 41, ItemScopeEntity.class));
        zzazC.put("playerType", Field.zzk("playerType", 42));
        zzazC.put("postOfficeBoxNumber", Field.zzk("postOfficeBoxNumber", 43));
        zzazC.put("postalCode", Field.zzk("postalCode", 44));
        zzazC.put("ratingValue", Field.zzk("ratingValue", 45));
        zzazC.put("reviewRating", Field.zza("reviewRating", 46, ItemScopeEntity.class));
        zzazC.put("startDate", Field.zzk("startDate", 47));
        zzazC.put("streetAddress", Field.zzk("streetAddress", 48));
        zzazC.put("text", Field.zzk("text", 49));
        zzazC.put("thumbnail", Field.zza("thumbnail", 50, ItemScopeEntity.class));
        zzazC.put(PlusShare.KEY_CONTENT_DEEP_LINK_METADATA_THUMBNAIL_URL, Field.zzk(PlusShare.KEY_CONTENT_DEEP_LINK_METADATA_THUMBNAIL_URL, 51));
        zzazC.put("tickerSymbol", Field.zzk("tickerSymbol", 52));
        zzazC.put(ShareConstants.MEDIA_TYPE, Field.zzk(ShareConstants.MEDIA_TYPE, 53));
        zzazC.put("url", Field.zzk("url", 54));
        zzazC.put(SettingsJsonConstants.ICON_WIDTH_KEY, Field.zzk(SettingsJsonConstants.ICON_WIDTH_KEY, 55));
        zzazC.put("worstRating", Field.zzk("worstRating", 56));
    }

    public ItemScopeEntity() {
        this.zzzH = 1;
        this.zzazD = new HashSet();
    }

    ItemScopeEntity(Set<Integer> set, int i, ItemScopeEntity itemScopeEntity, List<String> list, ItemScopeEntity itemScopeEntity2, String str, String str2, String str3, List<ItemScopeEntity> list2, int i2, List<ItemScopeEntity> list3, ItemScopeEntity itemScopeEntity3, List<ItemScopeEntity> list4, String str4, String str5, ItemScopeEntity itemScopeEntity4, String str6, String str7, String str8, List<ItemScopeEntity> list5, String str9, String str10, String str11, String str12, String str13, String str14, String str15, String str16, String str17, ItemScopeEntity itemScopeEntity5, String str18, String str19, String str20, String str21, ItemScopeEntity itemScopeEntity6, double d, ItemScopeEntity itemScopeEntity7, double d2, String str22, ItemScopeEntity itemScopeEntity8, List<ItemScopeEntity> list6, String str23, String str24, String str25, String str26, ItemScopeEntity itemScopeEntity9, String str27, String str28, String str29, ItemScopeEntity itemScopeEntity10, String str30, String str31, String str32, String str33, String str34, String str35) {
        this.zzazD = set;
        this.zzzH = i;
        this.zzazE = itemScopeEntity;
        this.zzazF = list;
        this.zzazG = itemScopeEntity2;
        this.zzazH = str;
        this.zzazI = str2;
        this.zzazJ = str3;
        this.zzazK = list2;
        this.zzazL = i2;
        this.zzazM = list3;
        this.zzazN = itemScopeEntity3;
        this.zzazO = list4;
        this.zzazP = str4;
        this.zzazQ = str5;
        this.zzazR = itemScopeEntity4;
        this.zzazS = str6;
        this.zzazT = str7;
        this.zzql = str8;
        this.zzazU = list5;
        this.zzazV = str9;
        this.zzazW = str10;
        this.zzazX = str11;
        this.zzadH = str12;
        this.zzazY = str13;
        this.zzazZ = str14;
        this.zzaAa = str15;
        this.zzaAb = str16;
        this.zzaAc = str17;
        this.zzaAd = itemScopeEntity5;
        this.zzaAe = str18;
        this.zzaAf = str19;
        this.zzGM = str20;
        this.zzaAg = str21;
        this.zzaAh = itemScopeEntity6;
        this.zzapL = d;
        this.zzaAi = itemScopeEntity7;
        this.zzapM = d2;
        this.mName = str22;
        this.zzaAj = itemScopeEntity8;
        this.zzaAk = list6;
        this.zzaAl = str23;
        this.zzaAm = str24;
        this.zzaAn = str25;
        this.zzaAo = str26;
        this.zzaAp = itemScopeEntity9;
        this.zzaAq = str27;
        this.zzaAr = str28;
        this.zzaAs = str29;
        this.zzaAt = itemScopeEntity10;
        this.zzaAu = str30;
        this.zzaAv = str31;
        this.zzAV = str32;
        this.zzAX = str33;
        this.zzaAw = str34;
        this.zzaAx = str35;
    }

    public ItemScopeEntity(Set<Integer> set, ItemScopeEntity itemScopeEntity, List<String> list, ItemScopeEntity itemScopeEntity2, String str, String str2, String str3, List<ItemScopeEntity> list2, int i, List<ItemScopeEntity> list3, ItemScopeEntity itemScopeEntity3, List<ItemScopeEntity> list4, String str4, String str5, ItemScopeEntity itemScopeEntity4, String str6, String str7, String str8, List<ItemScopeEntity> list5, String str9, String str10, String str11, String str12, String str13, String str14, String str15, String str16, String str17, ItemScopeEntity itemScopeEntity5, String str18, String str19, String str20, String str21, ItemScopeEntity itemScopeEntity6, double d, ItemScopeEntity itemScopeEntity7, double d2, String str22, ItemScopeEntity itemScopeEntity8, List<ItemScopeEntity> list6, String str23, String str24, String str25, String str26, ItemScopeEntity itemScopeEntity9, String str27, String str28, String str29, ItemScopeEntity itemScopeEntity10, String str30, String str31, String str32, String str33, String str34, String str35) {
        this.zzazD = set;
        this.zzzH = 1;
        this.zzazE = itemScopeEntity;
        this.zzazF = list;
        this.zzazG = itemScopeEntity2;
        this.zzazH = str;
        this.zzazI = str2;
        this.zzazJ = str3;
        this.zzazK = list2;
        this.zzazL = i;
        this.zzazM = list3;
        this.zzazN = itemScopeEntity3;
        this.zzazO = list4;
        this.zzazP = str4;
        this.zzazQ = str5;
        this.zzazR = itemScopeEntity4;
        this.zzazS = str6;
        this.zzazT = str7;
        this.zzql = str8;
        this.zzazU = list5;
        this.zzazV = str9;
        this.zzazW = str10;
        this.zzazX = str11;
        this.zzadH = str12;
        this.zzazY = str13;
        this.zzazZ = str14;
        this.zzaAa = str15;
        this.zzaAb = str16;
        this.zzaAc = str17;
        this.zzaAd = itemScopeEntity5;
        this.zzaAe = str18;
        this.zzaAf = str19;
        this.zzGM = str20;
        this.zzaAg = str21;
        this.zzaAh = itemScopeEntity6;
        this.zzapL = d;
        this.zzaAi = itemScopeEntity7;
        this.zzapM = d2;
        this.mName = str22;
        this.zzaAj = itemScopeEntity8;
        this.zzaAk = list6;
        this.zzaAl = str23;
        this.zzaAm = str24;
        this.zzaAn = str25;
        this.zzaAo = str26;
        this.zzaAp = itemScopeEntity9;
        this.zzaAq = str27;
        this.zzaAr = str28;
        this.zzaAs = str29;
        this.zzaAt = itemScopeEntity10;
        this.zzaAu = str30;
        this.zzaAv = str31;
        this.zzAV = str32;
        this.zzAX = str33;
        this.zzaAw = str34;
        this.zzaAx = str35;
    }

    public int describeContents() {
        zza zza = CREATOR;
        return 0;
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof ItemScopeEntity)) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        ItemScopeEntity itemScopeEntity = (ItemScopeEntity) obj;
        for (Field field : zzazC.values()) {
            if (zza(field)) {
                if (!itemScopeEntity.zza(field)) {
                    return false;
                }
                if (!zzb(field).equals(itemScopeEntity.zzb(field))) {
                    return false;
                }
            } else if (itemScopeEntity.zza(field)) {
                return false;
            }
        }
        return true;
    }

    public /* synthetic */ Object freeze() {
        return zzvM();
    }

    public ItemScope getAbout() {
        return this.zzazE;
    }

    public List<String> getAdditionalName() {
        return this.zzazF;
    }

    public ItemScope getAddress() {
        return this.zzazG;
    }

    public String getAddressCountry() {
        return this.zzazH;
    }

    public String getAddressLocality() {
        return this.zzazI;
    }

    public String getAddressRegion() {
        return this.zzazJ;
    }

    public List<ItemScope> getAssociated_media() {
        return (ArrayList) this.zzazK;
    }

    public int getAttendeeCount() {
        return this.zzazL;
    }

    public List<ItemScope> getAttendees() {
        return (ArrayList) this.zzazM;
    }

    public ItemScope getAudio() {
        return this.zzazN;
    }

    public List<ItemScope> getAuthor() {
        return (ArrayList) this.zzazO;
    }

    public String getBestRating() {
        return this.zzazP;
    }

    public String getBirthDate() {
        return this.zzazQ;
    }

    public ItemScope getByArtist() {
        return this.zzazR;
    }

    public String getCaption() {
        return this.zzazS;
    }

    public String getContentSize() {
        return this.zzazT;
    }

    public String getContentUrl() {
        return this.zzql;
    }

    public List<ItemScope> getContributor() {
        return (ArrayList) this.zzazU;
    }

    public String getDateCreated() {
        return this.zzazV;
    }

    public String getDateModified() {
        return this.zzazW;
    }

    public String getDatePublished() {
        return this.zzazX;
    }

    public String getDescription() {
        return this.zzadH;
    }

    public String getDuration() {
        return this.zzazY;
    }

    public String getEmbedUrl() {
        return this.zzazZ;
    }

    public String getEndDate() {
        return this.zzaAa;
    }

    public String getFamilyName() {
        return this.zzaAb;
    }

    public String getGender() {
        return this.zzaAc;
    }

    public ItemScope getGeo() {
        return this.zzaAd;
    }

    public String getGivenName() {
        return this.zzaAe;
    }

    public String getHeight() {
        return this.zzaAf;
    }

    public String getId() {
        return this.zzGM;
    }

    public String getImage() {
        return this.zzaAg;
    }

    public ItemScope getInAlbum() {
        return this.zzaAh;
    }

    public double getLatitude() {
        return this.zzapL;
    }

    public ItemScope getLocation() {
        return this.zzaAi;
    }

    public double getLongitude() {
        return this.zzapM;
    }

    public String getName() {
        return this.mName;
    }

    public ItemScope getPartOfTVSeries() {
        return this.zzaAj;
    }

    public List<ItemScope> getPerformers() {
        return (ArrayList) this.zzaAk;
    }

    public String getPlayerType() {
        return this.zzaAl;
    }

    public String getPostOfficeBoxNumber() {
        return this.zzaAm;
    }

    public String getPostalCode() {
        return this.zzaAn;
    }

    public String getRatingValue() {
        return this.zzaAo;
    }

    public ItemScope getReviewRating() {
        return this.zzaAp;
    }

    public String getStartDate() {
        return this.zzaAq;
    }

    public String getStreetAddress() {
        return this.zzaAr;
    }

    public String getText() {
        return this.zzaAs;
    }

    public ItemScope getThumbnail() {
        return this.zzaAt;
    }

    public String getThumbnailUrl() {
        return this.zzaAu;
    }

    public String getTickerSymbol() {
        return this.zzaAv;
    }

    public String getType() {
        return this.zzAV;
    }

    public String getUrl() {
        return this.zzAX;
    }

    public String getWidth() {
        return this.zzaAw;
    }

    public String getWorstRating() {
        return this.zzaAx;
    }

    public boolean hasAbout() {
        return this.zzazD.contains(Integer.valueOf(2));
    }

    public boolean hasAdditionalName() {
        return this.zzazD.contains(Integer.valueOf(3));
    }

    public boolean hasAddress() {
        return this.zzazD.contains(Integer.valueOf(4));
    }

    public boolean hasAddressCountry() {
        return this.zzazD.contains(Integer.valueOf(5));
    }

    public boolean hasAddressLocality() {
        return this.zzazD.contains(Integer.valueOf(6));
    }

    public boolean hasAddressRegion() {
        return this.zzazD.contains(Integer.valueOf(7));
    }

    public boolean hasAssociated_media() {
        return this.zzazD.contains(Integer.valueOf(8));
    }

    public boolean hasAttendeeCount() {
        return this.zzazD.contains(Integer.valueOf(9));
    }

    public boolean hasAttendees() {
        return this.zzazD.contains(Integer.valueOf(10));
    }

    public boolean hasAudio() {
        return this.zzazD.contains(Integer.valueOf(11));
    }

    public boolean hasAuthor() {
        return this.zzazD.contains(Integer.valueOf(12));
    }

    public boolean hasBestRating() {
        return this.zzazD.contains(Integer.valueOf(13));
    }

    public boolean hasBirthDate() {
        return this.zzazD.contains(Integer.valueOf(14));
    }

    public boolean hasByArtist() {
        return this.zzazD.contains(Integer.valueOf(15));
    }

    public boolean hasCaption() {
        return this.zzazD.contains(Integer.valueOf(16));
    }

    public boolean hasContentSize() {
        return this.zzazD.contains(Integer.valueOf(17));
    }

    public boolean hasContentUrl() {
        return this.zzazD.contains(Integer.valueOf(18));
    }

    public boolean hasContributor() {
        return this.zzazD.contains(Integer.valueOf(19));
    }

    public boolean hasDateCreated() {
        return this.zzazD.contains(Integer.valueOf(20));
    }

    public boolean hasDateModified() {
        return this.zzazD.contains(Integer.valueOf(21));
    }

    public boolean hasDatePublished() {
        return this.zzazD.contains(Integer.valueOf(22));
    }

    public boolean hasDescription() {
        return this.zzazD.contains(Integer.valueOf(23));
    }

    public boolean hasDuration() {
        return this.zzazD.contains(Integer.valueOf(24));
    }

    public boolean hasEmbedUrl() {
        return this.zzazD.contains(Integer.valueOf(25));
    }

    public boolean hasEndDate() {
        return this.zzazD.contains(Integer.valueOf(26));
    }

    public boolean hasFamilyName() {
        return this.zzazD.contains(Integer.valueOf(27));
    }

    public boolean hasGender() {
        return this.zzazD.contains(Integer.valueOf(28));
    }

    public boolean hasGeo() {
        return this.zzazD.contains(Integer.valueOf(29));
    }

    public boolean hasGivenName() {
        return this.zzazD.contains(Integer.valueOf(30));
    }

    public boolean hasHeight() {
        return this.zzazD.contains(Integer.valueOf(31));
    }

    public boolean hasId() {
        return this.zzazD.contains(Integer.valueOf(32));
    }

    public boolean hasImage() {
        return this.zzazD.contains(Integer.valueOf(33));
    }

    public boolean hasInAlbum() {
        return this.zzazD.contains(Integer.valueOf(34));
    }

    public boolean hasLatitude() {
        return this.zzazD.contains(Integer.valueOf(36));
    }

    public boolean hasLocation() {
        return this.zzazD.contains(Integer.valueOf(37));
    }

    public boolean hasLongitude() {
        return this.zzazD.contains(Integer.valueOf(38));
    }

    public boolean hasName() {
        return this.zzazD.contains(Integer.valueOf(39));
    }

    public boolean hasPartOfTVSeries() {
        return this.zzazD.contains(Integer.valueOf(40));
    }

    public boolean hasPerformers() {
        return this.zzazD.contains(Integer.valueOf(41));
    }

    public boolean hasPlayerType() {
        return this.zzazD.contains(Integer.valueOf(42));
    }

    public boolean hasPostOfficeBoxNumber() {
        return this.zzazD.contains(Integer.valueOf(43));
    }

    public boolean hasPostalCode() {
        return this.zzazD.contains(Integer.valueOf(44));
    }

    public boolean hasRatingValue() {
        return this.zzazD.contains(Integer.valueOf(45));
    }

    public boolean hasReviewRating() {
        return this.zzazD.contains(Integer.valueOf(46));
    }

    public boolean hasStartDate() {
        return this.zzazD.contains(Integer.valueOf(47));
    }

    public boolean hasStreetAddress() {
        return this.zzazD.contains(Integer.valueOf(48));
    }

    public boolean hasText() {
        return this.zzazD.contains(Integer.valueOf(49));
    }

    public boolean hasThumbnail() {
        return this.zzazD.contains(Integer.valueOf(50));
    }

    public boolean hasThumbnailUrl() {
        return this.zzazD.contains(Integer.valueOf(51));
    }

    public boolean hasTickerSymbol() {
        return this.zzazD.contains(Integer.valueOf(52));
    }

    public boolean hasType() {
        return this.zzazD.contains(Integer.valueOf(53));
    }

    public boolean hasUrl() {
        return this.zzazD.contains(Integer.valueOf(54));
    }

    public boolean hasWidth() {
        return this.zzazD.contains(Integer.valueOf(55));
    }

    public boolean hasWorstRating() {
        return this.zzazD.contains(Integer.valueOf(56));
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
        zza zza = CREATOR;
        zza.zza(this, parcel, i);
    }

    protected boolean zza(Field field) {
        return this.zzazD.contains(Integer.valueOf(field.zzmF()));
    }

    protected Object zzb(Field field) {
        switch (field.zzmF()) {
            case 2:
                return this.zzazE;
            case 3:
                return this.zzazF;
            case 4:
                return this.zzazG;
            case 5:
                return this.zzazH;
            case 6:
                return this.zzazI;
            case 7:
                return this.zzazJ;
            case 8:
                return this.zzazK;
            case 9:
                return Integer.valueOf(this.zzazL);
            case 10:
                return this.zzazM;
            case 11:
                return this.zzazN;
            case 12:
                return this.zzazO;
            case 13:
                return this.zzazP;
            case 14:
                return this.zzazQ;
            case 15:
                return this.zzazR;
            case 16:
                return this.zzazS;
            case 17:
                return this.zzazT;
            case 18:
                return this.zzql;
            case 19:
                return this.zzazU;
            case 20:
                return this.zzazV;
            case 21:
                return this.zzazW;
            case 22:
                return this.zzazX;
            case 23:
                return this.zzadH;
            case MotionEventCompat.AXIS_DISTANCE /*24*/:
                return this.zzazY;
            case 25:
                return this.zzazZ;
            case 26:
                return this.zzaAa;
            case MotionEventCompat.AXIS_RELATIVE_X /*27*/:
                return this.zzaAb;
            case MotionEventCompat.AXIS_RELATIVE_Y /*28*/:
                return this.zzaAc;
            case 29:
                return this.zzaAd;
            case 30:
                return this.zzaAe;
            case 31:
                return this.zzaAf;
            case 32:
                return this.zzGM;
            case 33:
                return this.zzaAg;
            case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                return this.zzaAh;
            case MotionEventCompat.AXIS_GENERIC_5 /*36*/:
                return Double.valueOf(this.zzapL);
            case MotionEventCompat.AXIS_GENERIC_6 /*37*/:
                return this.zzaAi;
            case MotionEventCompat.AXIS_GENERIC_7 /*38*/:
                return Double.valueOf(this.zzapM);
            case MotionEventCompat.AXIS_GENERIC_8 /*39*/:
                return this.mName;
            case 40:
                return this.zzaAj;
            case MotionEventCompat.AXIS_GENERIC_10 /*41*/:
                return this.zzaAk;
            case MotionEventCompat.AXIS_GENERIC_11 /*42*/:
                return this.zzaAl;
            case MotionEventCompat.AXIS_GENERIC_12 /*43*/:
                return this.zzaAm;
            case MotionEventCompat.AXIS_GENERIC_13 /*44*/:
                return this.zzaAn;
            case MotionEventCompat.AXIS_GENERIC_14 /*45*/:
                return this.zzaAo;
            case MotionEventCompat.AXIS_GENERIC_15 /*46*/:
                return this.zzaAp;
            case MotionEventCompat.AXIS_GENERIC_16 /*47*/:
                return this.zzaAq;
            case 48:
                return this.zzaAr;
            case 49:
                return this.zzaAs;
            case GraphRequest.MAXIMUM_BATCH_SIZE /*50*/:
                return this.zzaAt;
            case 51:
                return this.zzaAu;
            case 52:
                return this.zzaAv;
            case 53:
                return this.zzAV;
            case 54:
                return this.zzAX;
            case 55:
                return this.zzaAw;
            case 56:
                return this.zzaAx;
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

    public ItemScopeEntity zzvM() {
        return this;
    }
}
