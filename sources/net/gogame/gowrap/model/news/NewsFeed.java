package net.gogame.gowrap.model.news;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.StringUtils;

public class NewsFeed extends BaseJsonObject {
    private static final String KEY_ARTICLES = "articles";
    private static final String KEY_BANNERS = "banners";
    private List<Article> articles;
    private List<Banner> banners;

    public NewsFeed() {
    }

    public NewsFeed(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, KEY_BANNERS)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            } else if (jsonReader.peek() == JsonToken.BEGIN_ARRAY) {
                jsonReader.beginArray();
                this.banners = new ArrayList();
                while (jsonReader.hasNext()) {
                    if (jsonReader.peek() == JsonToken.NULL) {
                        jsonReader.nextNull();
                        this.banners.add(null);
                    } else {
                        this.banners.add(new Banner(jsonReader));
                    }
                }
                jsonReader.endArray();
                return true;
            } else {
                throw new IllegalArgumentException("array or null expected");
            }
        } else if (!StringUtils.isEquals(str, KEY_ARTICLES)) {
            return false;
        } else {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            } else if (jsonReader.peek() == JsonToken.BEGIN_ARRAY) {
                jsonReader.beginArray();
                this.articles = new ArrayList();
                while (jsonReader.hasNext()) {
                    if (jsonReader.peek() == JsonToken.NULL) {
                        jsonReader.nextNull();
                        this.articles.add(null);
                    } else {
                        this.articles.add(new Article(jsonReader));
                    }
                }
                jsonReader.endArray();
                return true;
            } else {
                throw new IllegalArgumentException("array or null expected");
            }
        }
    }

    public List<Banner> getBanners() {
        return this.banners;
    }

    public void setBanners(List<Banner> list) {
        this.banners = list;
    }

    public List<Article> getArticles() {
        return this.articles;
    }

    public void setArticles(List<Article> list) {
        this.articles = list;
    }
}
