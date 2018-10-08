package com.google.android.gms.plus.model.moments;

import com.google.android.gms.common.data.Freezable;
import com.google.android.gms.plus.internal.model.moments.ItemScopeEntity;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public interface ItemScope extends Freezable<ItemScope> {

    public static class Builder {
        private String mName;
        private String zzAV;
        private String zzAX;
        private String zzGM;
        private String zzaAa;
        private String zzaAb;
        private String zzaAc;
        private ItemScopeEntity zzaAd;
        private String zzaAe;
        private String zzaAf;
        private String zzaAg;
        private ItemScopeEntity zzaAh;
        private ItemScopeEntity zzaAi;
        private ItemScopeEntity zzaAj;
        private List<ItemScopeEntity> zzaAk;
        private String zzaAl;
        private String zzaAm;
        private String zzaAn;
        private String zzaAo;
        private ItemScopeEntity zzaAp;
        private String zzaAq;
        private String zzaAr;
        private String zzaAs;
        private ItemScopeEntity zzaAt;
        private String zzaAu;
        private String zzaAv;
        private String zzaAw;
        private String zzaAx;
        private String zzadH;
        private double zzapL;
        private double zzapM;
        private final Set<Integer> zzazD = new HashSet();
        private ItemScopeEntity zzazE;
        private List<String> zzazF;
        private ItemScopeEntity zzazG;
        private String zzazH;
        private String zzazI;
        private String zzazJ;
        private List<ItemScopeEntity> zzazK;
        private int zzazL;
        private List<ItemScopeEntity> zzazM;
        private ItemScopeEntity zzazN;
        private List<ItemScopeEntity> zzazO;
        private String zzazP;
        private String zzazQ;
        private ItemScopeEntity zzazR;
        private String zzazS;
        private String zzazT;
        private List<ItemScopeEntity> zzazU;
        private String zzazV;
        private String zzazW;
        private String zzazX;
        private String zzazY;
        private String zzazZ;
        private String zzql;

        public ItemScope build() {
            return new ItemScopeEntity(this.zzazD, this.zzazE, this.zzazF, this.zzazG, this.zzazH, this.zzazI, this.zzazJ, this.zzazK, this.zzazL, this.zzazM, this.zzazN, this.zzazO, this.zzazP, this.zzazQ, this.zzazR, this.zzazS, this.zzazT, this.zzql, this.zzazU, this.zzazV, this.zzazW, this.zzazX, this.zzadH, this.zzazY, this.zzazZ, this.zzaAa, this.zzaAb, this.zzaAc, this.zzaAd, this.zzaAe, this.zzaAf, this.zzGM, this.zzaAg, this.zzaAh, this.zzapL, this.zzaAi, this.zzapM, this.mName, this.zzaAj, this.zzaAk, this.zzaAl, this.zzaAm, this.zzaAn, this.zzaAo, this.zzaAp, this.zzaAq, this.zzaAr, this.zzaAs, this.zzaAt, this.zzaAu, this.zzaAv, this.zzAV, this.zzAX, this.zzaAw, this.zzaAx);
        }

        public Builder setAbout(ItemScope itemScope) {
            this.zzazE = (ItemScopeEntity) itemScope;
            this.zzazD.add(Integer.valueOf(2));
            return this;
        }

        public Builder setAdditionalName(List<String> list) {
            this.zzazF = list;
            this.zzazD.add(Integer.valueOf(3));
            return this;
        }

        public Builder setAddress(ItemScope itemScope) {
            this.zzazG = (ItemScopeEntity) itemScope;
            this.zzazD.add(Integer.valueOf(4));
            return this;
        }

        public Builder setAddressCountry(String str) {
            this.zzazH = str;
            this.zzazD.add(Integer.valueOf(5));
            return this;
        }

        public Builder setAddressLocality(String str) {
            this.zzazI = str;
            this.zzazD.add(Integer.valueOf(6));
            return this;
        }

        public Builder setAddressRegion(String str) {
            this.zzazJ = str;
            this.zzazD.add(Integer.valueOf(7));
            return this;
        }

        public Builder setAssociated_media(List<ItemScope> list) {
            this.zzazK = list;
            this.zzazD.add(Integer.valueOf(8));
            return this;
        }

        public Builder setAttendeeCount(int i) {
            this.zzazL = i;
            this.zzazD.add(Integer.valueOf(9));
            return this;
        }

        public Builder setAttendees(List<ItemScope> list) {
            this.zzazM = list;
            this.zzazD.add(Integer.valueOf(10));
            return this;
        }

        public Builder setAudio(ItemScope itemScope) {
            this.zzazN = (ItemScopeEntity) itemScope;
            this.zzazD.add(Integer.valueOf(11));
            return this;
        }

        public Builder setAuthor(List<ItemScope> list) {
            this.zzazO = list;
            this.zzazD.add(Integer.valueOf(12));
            return this;
        }

        public Builder setBestRating(String str) {
            this.zzazP = str;
            this.zzazD.add(Integer.valueOf(13));
            return this;
        }

        public Builder setBirthDate(String str) {
            this.zzazQ = str;
            this.zzazD.add(Integer.valueOf(14));
            return this;
        }

        public Builder setByArtist(ItemScope itemScope) {
            this.zzazR = (ItemScopeEntity) itemScope;
            this.zzazD.add(Integer.valueOf(15));
            return this;
        }

        public Builder setCaption(String str) {
            this.zzazS = str;
            this.zzazD.add(Integer.valueOf(16));
            return this;
        }

        public Builder setContentSize(String str) {
            this.zzazT = str;
            this.zzazD.add(Integer.valueOf(17));
            return this;
        }

        public Builder setContentUrl(String str) {
            this.zzql = str;
            this.zzazD.add(Integer.valueOf(18));
            return this;
        }

        public Builder setContributor(List<ItemScope> list) {
            this.zzazU = list;
            this.zzazD.add(Integer.valueOf(19));
            return this;
        }

        public Builder setDateCreated(String str) {
            this.zzazV = str;
            this.zzazD.add(Integer.valueOf(20));
            return this;
        }

        public Builder setDateModified(String str) {
            this.zzazW = str;
            this.zzazD.add(Integer.valueOf(21));
            return this;
        }

        public Builder setDatePublished(String str) {
            this.zzazX = str;
            this.zzazD.add(Integer.valueOf(22));
            return this;
        }

        public Builder setDescription(String str) {
            this.zzadH = str;
            this.zzazD.add(Integer.valueOf(23));
            return this;
        }

        public Builder setDuration(String str) {
            this.zzazY = str;
            this.zzazD.add(Integer.valueOf(24));
            return this;
        }

        public Builder setEmbedUrl(String str) {
            this.zzazZ = str;
            this.zzazD.add(Integer.valueOf(25));
            return this;
        }

        public Builder setEndDate(String str) {
            this.zzaAa = str;
            this.zzazD.add(Integer.valueOf(26));
            return this;
        }

        public Builder setFamilyName(String str) {
            this.zzaAb = str;
            this.zzazD.add(Integer.valueOf(27));
            return this;
        }

        public Builder setGender(String str) {
            this.zzaAc = str;
            this.zzazD.add(Integer.valueOf(28));
            return this;
        }

        public Builder setGeo(ItemScope itemScope) {
            this.zzaAd = (ItemScopeEntity) itemScope;
            this.zzazD.add(Integer.valueOf(29));
            return this;
        }

        public Builder setGivenName(String str) {
            this.zzaAe = str;
            this.zzazD.add(Integer.valueOf(30));
            return this;
        }

        public Builder setHeight(String str) {
            this.zzaAf = str;
            this.zzazD.add(Integer.valueOf(31));
            return this;
        }

        public Builder setId(String str) {
            this.zzGM = str;
            this.zzazD.add(Integer.valueOf(32));
            return this;
        }

        public Builder setImage(String str) {
            this.zzaAg = str;
            this.zzazD.add(Integer.valueOf(33));
            return this;
        }

        public Builder setInAlbum(ItemScope itemScope) {
            this.zzaAh = (ItemScopeEntity) itemScope;
            this.zzazD.add(Integer.valueOf(34));
            return this;
        }

        public Builder setLatitude(double d) {
            this.zzapL = d;
            this.zzazD.add(Integer.valueOf(36));
            return this;
        }

        public Builder setLocation(ItemScope itemScope) {
            this.zzaAi = (ItemScopeEntity) itemScope;
            this.zzazD.add(Integer.valueOf(37));
            return this;
        }

        public Builder setLongitude(double d) {
            this.zzapM = d;
            this.zzazD.add(Integer.valueOf(38));
            return this;
        }

        public Builder setName(String str) {
            this.mName = str;
            this.zzazD.add(Integer.valueOf(39));
            return this;
        }

        public Builder setPartOfTVSeries(ItemScope itemScope) {
            this.zzaAj = (ItemScopeEntity) itemScope;
            this.zzazD.add(Integer.valueOf(40));
            return this;
        }

        public Builder setPerformers(List<ItemScope> list) {
            this.zzaAk = list;
            this.zzazD.add(Integer.valueOf(41));
            return this;
        }

        public Builder setPlayerType(String str) {
            this.zzaAl = str;
            this.zzazD.add(Integer.valueOf(42));
            return this;
        }

        public Builder setPostOfficeBoxNumber(String str) {
            this.zzaAm = str;
            this.zzazD.add(Integer.valueOf(43));
            return this;
        }

        public Builder setPostalCode(String str) {
            this.zzaAn = str;
            this.zzazD.add(Integer.valueOf(44));
            return this;
        }

        public Builder setRatingValue(String str) {
            this.zzaAo = str;
            this.zzazD.add(Integer.valueOf(45));
            return this;
        }

        public Builder setReviewRating(ItemScope itemScope) {
            this.zzaAp = (ItemScopeEntity) itemScope;
            this.zzazD.add(Integer.valueOf(46));
            return this;
        }

        public Builder setStartDate(String str) {
            this.zzaAq = str;
            this.zzazD.add(Integer.valueOf(47));
            return this;
        }

        public Builder setStreetAddress(String str) {
            this.zzaAr = str;
            this.zzazD.add(Integer.valueOf(48));
            return this;
        }

        public Builder setText(String str) {
            this.zzaAs = str;
            this.zzazD.add(Integer.valueOf(49));
            return this;
        }

        public Builder setThumbnail(ItemScope itemScope) {
            this.zzaAt = (ItemScopeEntity) itemScope;
            this.zzazD.add(Integer.valueOf(50));
            return this;
        }

        public Builder setThumbnailUrl(String str) {
            this.zzaAu = str;
            this.zzazD.add(Integer.valueOf(51));
            return this;
        }

        public Builder setTickerSymbol(String str) {
            this.zzaAv = str;
            this.zzazD.add(Integer.valueOf(52));
            return this;
        }

        public Builder setType(String str) {
            this.zzAV = str;
            this.zzazD.add(Integer.valueOf(53));
            return this;
        }

        public Builder setUrl(String str) {
            this.zzAX = str;
            this.zzazD.add(Integer.valueOf(54));
            return this;
        }

        public Builder setWidth(String str) {
            this.zzaAw = str;
            this.zzazD.add(Integer.valueOf(55));
            return this;
        }

        public Builder setWorstRating(String str) {
            this.zzaAx = str;
            this.zzazD.add(Integer.valueOf(56));
            return this;
        }
    }

    ItemScope getAbout();

    List<String> getAdditionalName();

    ItemScope getAddress();

    String getAddressCountry();

    String getAddressLocality();

    String getAddressRegion();

    List<ItemScope> getAssociated_media();

    int getAttendeeCount();

    List<ItemScope> getAttendees();

    ItemScope getAudio();

    List<ItemScope> getAuthor();

    String getBestRating();

    String getBirthDate();

    ItemScope getByArtist();

    String getCaption();

    String getContentSize();

    String getContentUrl();

    List<ItemScope> getContributor();

    String getDateCreated();

    String getDateModified();

    String getDatePublished();

    String getDescription();

    String getDuration();

    String getEmbedUrl();

    String getEndDate();

    String getFamilyName();

    String getGender();

    ItemScope getGeo();

    String getGivenName();

    String getHeight();

    String getId();

    String getImage();

    ItemScope getInAlbum();

    double getLatitude();

    ItemScope getLocation();

    double getLongitude();

    String getName();

    ItemScope getPartOfTVSeries();

    List<ItemScope> getPerformers();

    String getPlayerType();

    String getPostOfficeBoxNumber();

    String getPostalCode();

    String getRatingValue();

    ItemScope getReviewRating();

    String getStartDate();

    String getStreetAddress();

    String getText();

    ItemScope getThumbnail();

    String getThumbnailUrl();

    String getTickerSymbol();

    String getType();

    String getUrl();

    String getWidth();

    String getWorstRating();

    boolean hasAbout();

    boolean hasAdditionalName();

    boolean hasAddress();

    boolean hasAddressCountry();

    boolean hasAddressLocality();

    boolean hasAddressRegion();

    boolean hasAssociated_media();

    boolean hasAttendeeCount();

    boolean hasAttendees();

    boolean hasAudio();

    boolean hasAuthor();

    boolean hasBestRating();

    boolean hasBirthDate();

    boolean hasByArtist();

    boolean hasCaption();

    boolean hasContentSize();

    boolean hasContentUrl();

    boolean hasContributor();

    boolean hasDateCreated();

    boolean hasDateModified();

    boolean hasDatePublished();

    boolean hasDescription();

    boolean hasDuration();

    boolean hasEmbedUrl();

    boolean hasEndDate();

    boolean hasFamilyName();

    boolean hasGender();

    boolean hasGeo();

    boolean hasGivenName();

    boolean hasHeight();

    boolean hasId();

    boolean hasImage();

    boolean hasInAlbum();

    boolean hasLatitude();

    boolean hasLocation();

    boolean hasLongitude();

    boolean hasName();

    boolean hasPartOfTVSeries();

    boolean hasPerformers();

    boolean hasPlayerType();

    boolean hasPostOfficeBoxNumber();

    boolean hasPostalCode();

    boolean hasRatingValue();

    boolean hasReviewRating();

    boolean hasStartDate();

    boolean hasStreetAddress();

    boolean hasText();

    boolean hasThumbnail();

    boolean hasThumbnailUrl();

    boolean hasTickerSymbol();

    boolean hasType();

    boolean hasUrl();

    boolean hasWidth();

    boolean hasWorstRating();
}
