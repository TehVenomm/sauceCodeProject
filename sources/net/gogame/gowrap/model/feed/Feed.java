package net.gogame.gowrap.model.feed;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

public class Feed implements Serializable {
    private static final String KEY_ITEMS = "items";
    private List<Item> items;

    public static class Item implements Serializable {
        private static final String KEY_ARTICLE_LINK = "articleLink";
        private static final String KEY_CREATED_TIME = "createdTime";
        private static final String KEY_ID = "id";
        private static final String KEY_LINK = "link";
        private static final String KEY_MEDIA_HEIGHT = "mediaHeight";
        private static final String KEY_MEDIA_PREVIEW = "mediaPreview";
        private static final String KEY_MEDIA_SOURCE = "mediaSource";
        private static final String KEY_MEDIA_WIDTH = "mediaWidth";
        private static final String KEY_MESSAGE = "message";
        private static final String KEY_TYPE = "type";
        private static final String KEY_UPDATED_TIME = "updatedTime";
        private String articleLink;
        private Long createdTime;
        private String id;
        private String link;
        private Integer mediaHeight;
        private String mediaPreview;
        private String mediaSource;
        private Integer mediaWidth;
        private String message;
        private String type;
        private Long updatedTime;

        public Item(JsonReader jsonReader) throws IOException {
            if (jsonReader.peek() == JsonToken.BEGIN_OBJECT) {
                jsonReader.beginObject();
                while (jsonReader.hasNext()) {
                    String nextName = jsonReader.nextName();
                    if (StringUtils.isEquals(nextName, "id")) {
                        this.id = JSONUtils.optString(jsonReader);
                    } else if (StringUtils.isEquals(nextName, KEY_CREATED_TIME)) {
                        this.createdTime = JSONUtils.optLong(jsonReader);
                    } else if (StringUtils.isEquals(nextName, KEY_UPDATED_TIME)) {
                        this.updatedTime = JSONUtils.optLong(jsonReader);
                    } else if (StringUtils.isEquals(nextName, "type")) {
                        this.type = JSONUtils.optString(jsonReader);
                    } else if (StringUtils.isEquals(nextName, KEY_MEDIA_WIDTH)) {
                        this.mediaWidth = JSONUtils.optInt(jsonReader);
                    } else if (StringUtils.isEquals(nextName, KEY_MEDIA_HEIGHT)) {
                        this.mediaHeight = JSONUtils.optInt(jsonReader);
                    } else if (StringUtils.isEquals(nextName, KEY_MEDIA_PREVIEW)) {
                        this.mediaPreview = JSONUtils.optString(jsonReader);
                    } else if (StringUtils.isEquals(nextName, KEY_MEDIA_SOURCE)) {
                        this.mediaSource = JSONUtils.optString(jsonReader);
                    } else if (StringUtils.isEquals(nextName, "link")) {
                        this.link = JSONUtils.optString(jsonReader);
                    } else if (StringUtils.isEquals(nextName, KEY_ARTICLE_LINK)) {
                        this.articleLink = JSONUtils.optString(jsonReader);
                    } else if (StringUtils.isEquals(nextName, "message")) {
                        this.message = JSONUtils.optString(jsonReader);
                    } else {
                        jsonReader.skipValue();
                    }
                }
                jsonReader.endObject();
                return;
            }
            throw new IllegalArgumentException("Invalid news feed");
        }

        public String getId() {
            return this.id;
        }

        public void setId(String str) {
            this.id = str;
        }

        public Long getCreatedTime() {
            return this.createdTime;
        }

        public void setCreatedTime(Long l) {
            this.createdTime = l;
        }

        public Long getUpdatedTime() {
            return this.updatedTime;
        }

        public void setUpdatedTime(Long l) {
            this.updatedTime = l;
        }

        public String getType() {
            return this.type;
        }

        public void setType(String str) {
            this.type = str;
        }

        public Integer getMediaWidth() {
            return this.mediaWidth;
        }

        public void setMediaWidth(Integer num) {
            this.mediaWidth = num;
        }

        public Integer getMediaHeight() {
            return this.mediaHeight;
        }

        public void setMediaHeight(Integer num) {
            this.mediaHeight = num;
        }

        public String getMediaPreview() {
            return this.mediaPreview;
        }

        public void setMediaPreview(String str) {
            this.mediaPreview = str;
        }

        public String getMediaSource() {
            return this.mediaSource;
        }

        public void setMediaSource(String str) {
            this.mediaSource = str;
        }

        public String getLink() {
            return this.link;
        }

        public void setLink(String str) {
            this.link = str;
        }

        public String getArticleLink() {
            return this.articleLink;
        }

        public void setArticleLink(String str) {
            this.articleLink = str;
        }

        public String getMessage() {
            return this.message;
        }

        public void setMessage(String str) {
            this.message = str;
        }
    }

    public Feed(JsonReader jsonReader) throws IOException {
        if (jsonReader.peek() == JsonToken.BEGIN_OBJECT) {
            jsonReader.beginObject();
            while (jsonReader.hasNext()) {
                if (!StringUtils.isEquals(jsonReader.nextName(), KEY_ITEMS)) {
                    jsonReader.skipValue();
                } else if (jsonReader.peek() == JsonToken.BEGIN_ARRAY) {
                    this.items = new ArrayList();
                    jsonReader.beginArray();
                    while (jsonReader.hasNext()) {
                        this.items.add(new Item(jsonReader));
                    }
                    jsonReader.endArray();
                } else if (jsonReader.peek() == JsonToken.NULL) {
                    jsonReader.nextNull();
                    this.items = null;
                } else {
                    throw new IllegalArgumentException("Invalid news feed");
                }
            }
            jsonReader.endObject();
            return;
        }
        throw new IllegalArgumentException("Invalid news feed");
    }

    public List<Item> getItems() {
        return this.items;
    }

    public void setItems(List<Item> list) {
        this.items = list;
    }
}
