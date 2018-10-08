package net.gogame.gowrap.model.news;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

public class Article extends BaseJsonObject {
    private static final String KEY_CATEGORY = "category";
    private static final String KEY_CONTENT = "content";
    private static final String KEY_DATE_TIME = "dateTime";
    private static final String KEY_END_DATE_TIME = "endDateTime";
    private static final String KEY_ID = "id";
    private static final String KEY_START_DATE_TIME = "startDateTime";
    private static final String KEY_TITLE = "title";
    private Category category;
    private MarkupElement content;
    private Long dateTime;
    private Long endDateTime;
    private long id;
    private Long startDateTime;
    private String title;

    public enum Category {
        ADMIN,
        EVENT,
        IMPORTANT,
        NOTICE,
        SUMMON,
        TIPS
    }

    public Article(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    private Category getCategory(String str) {
        if (str == null) {
            return null;
        }
        for (Category category : Category.values()) {
            if (str.equalsIgnoreCase(category.name())) {
                return category;
            }
        }
        return null;
    }

    protected boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, "id")) {
            this.id = jsonReader.nextLong();
            return true;
        } else if (StringUtils.isEquals(str, KEY_START_DATE_TIME)) {
            this.startDateTime = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_END_DATE_TIME)) {
            this.endDateTime = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_DATE_TIME)) {
            this.dateTime = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_CATEGORY)) {
            this.category = getCategory(JSONUtils.optString(jsonReader));
            return true;
        } else if (StringUtils.isEquals(str, "title")) {
            this.title = JSONUtils.optString(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, "content")) {
            return false;
        } else {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                this.content = null;
                return true;
            }
            this.content = new MarkupElement(jsonReader);
            return true;
        }
    }

    public long getId() {
        return this.id;
    }

    public void setId(long j) {
        this.id = j;
    }

    public Long getDateTime() {
        return this.dateTime;
    }

    public void setDateTime(Long l) {
        this.dateTime = l;
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

    public Category getCategory() {
        return this.category;
    }

    public void setCategory(Category category) {
        this.category = category;
    }

    public String getTitle() {
        return this.title;
    }

    public void setTitle(String str) {
        this.title = str;
    }

    public MarkupElement getContent() {
        return this.content;
    }

    public void setContent(MarkupElement markupElement) {
        this.content = markupElement;
    }
}
