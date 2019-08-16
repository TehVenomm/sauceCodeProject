package net.gogame.gowrap.model.news;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

public class Banner extends BaseJsonObject {
    private static final String KEY_END_DATE_TIME = "endDateTime";
    private static final String KEY_ID = "id";
    private static final String KEY_IMAGE_URL = "imageUrl";
    private static final String KEY_LINK = "link";
    private static final String KEY_START_DATE_TIME = "startDateTime";
    private Long endDateTime;

    /* renamed from: id */
    private long f1409id;
    private String imageUrl;
    private String link;
    private Long startDateTime;

    public Banner() {
    }

    public Banner(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, "id")) {
            this.f1409id = jsonReader.nextLong();
            return true;
        } else if (StringUtils.isEquals(str, KEY_START_DATE_TIME)) {
            this.startDateTime = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_END_DATE_TIME)) {
            this.endDateTime = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, "link")) {
            this.link = JSONUtils.optString(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, KEY_IMAGE_URL)) {
            return false;
        } else {
            this.imageUrl = JSONUtils.optString(jsonReader);
            return true;
        }
    }

    public long getId() {
        return this.f1409id;
    }

    public void setId(long j) {
        this.f1409id = j;
    }

    public Long getStartDateTime() {
        return this.startDateTime;
    }

    public void setStartDateTime(Long l) {
        this.startDateTime = l;
    }

    public Long getEndDateTime() {
        return this.endDateTime;
    }

    public void setEndDateTime(Long l) {
        this.endDateTime = l;
    }

    public String getLink() {
        return this.link;
    }

    public void setLink(String str) {
        this.link = str;
    }

    public String getImageUrl() {
        return this.imageUrl;
    }

    public void setImageUrl(String str) {
        this.imageUrl = str;
    }
}
