package net.gogame.gowrap.model.faq;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

public class Article {
    private static final String KEY_BODY = "body";
    private static final String KEY_TITLE = "title";
    private String body;
    private String title;

    public Article() {
    }

    public Article(String str, String str2) {
        this.title = str;
        this.body = str2;
    }

    public Article(JsonReader jsonReader) throws IOException {
        if (jsonReader.peek() == JsonToken.BEGIN_OBJECT) {
            jsonReader.beginObject();
            while (jsonReader.hasNext()) {
                String nextName = jsonReader.nextName();
                if (StringUtils.isEquals(nextName, "title")) {
                    this.title = JSONUtils.optString(jsonReader);
                } else if (StringUtils.isEquals(nextName, "body")) {
                    this.body = JSONUtils.optString(jsonReader);
                } else {
                    jsonReader.skipValue();
                }
            }
            jsonReader.endObject();
            return;
        }
        throw new IllegalArgumentException("object expected");
    }

    public String getTitle() {
        return this.title;
    }

    public void setTitle(String str) {
        this.title = str;
    }

    public String getBody() {
        return this.body;
    }

    public void setBody(String str) {
        this.body = str;
    }
}
