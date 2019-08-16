package net.gogame.gowrap.model.faq;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

public class Section {
    private static final String KEY_ARTICLES = "articles";
    private static final String KEY_NAME = "name";
    private List<Article> articles;
    private String name;

    public Section() {
    }

    public Section(String str, List<Article> list) {
        this.name = str;
        this.articles = list;
    }

    public Section(JsonReader jsonReader) throws IOException {
        if (jsonReader.peek() == JsonToken.BEGIN_OBJECT) {
            jsonReader.beginObject();
            while (jsonReader.hasNext()) {
                String nextName = jsonReader.nextName();
                if (StringUtils.isEquals(nextName, "name")) {
                    this.name = JSONUtils.optString(jsonReader);
                } else if (!StringUtils.isEquals(nextName, KEY_ARTICLES)) {
                    jsonReader.skipValue();
                } else if (jsonReader.peek() == JsonToken.BEGIN_ARRAY) {
                    this.articles = new ArrayList();
                    jsonReader.beginArray();
                    while (jsonReader.hasNext()) {
                        this.articles.add(new Article(jsonReader));
                    }
                    jsonReader.endArray();
                } else if (jsonReader.peek() == JsonToken.NULL) {
                    jsonReader.nextNull();
                    this.articles = null;
                } else {
                    throw new IllegalArgumentException("array or null expected");
                }
            }
            jsonReader.endObject();
            return;
        }
        throw new IllegalArgumentException("object expected");
    }

    public String getName() {
        return this.name;
    }

    public void setName(String str) {
        this.name = str;
    }

    public List<Article> getArticles() {
        return this.articles;
    }

    public void setArticles(List<Article> list) {
        this.articles = list;
    }
}
