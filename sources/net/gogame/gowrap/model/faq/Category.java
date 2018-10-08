package net.gogame.gowrap.model.faq;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

public class Category {
    private static final String KEY_DESCRIPTION = "description";
    private static final String KEY_NAME = "name";
    private static final String KEY_SECTIONS = "sections";
    private String description;
    private String name;
    private List<Section> sections;

    public Category(String str, String str2, List<Section> list) {
        this.name = str;
        this.description = str2;
        this.sections = list;
    }

    public Category(JsonReader jsonReader) throws IOException {
        if (jsonReader.peek() == JsonToken.BEGIN_OBJECT) {
            jsonReader.beginObject();
            while (jsonReader.hasNext()) {
                String nextName = jsonReader.nextName();
                if (StringUtils.isEquals(nextName, "name")) {
                    this.name = JSONUtils.optString(jsonReader);
                } else if (StringUtils.isEquals(nextName, "description")) {
                    this.description = JSONUtils.optString(jsonReader);
                } else if (!StringUtils.isEquals(nextName, KEY_SECTIONS)) {
                    jsonReader.skipValue();
                } else if (jsonReader.peek() == JsonToken.BEGIN_ARRAY) {
                    this.sections = new ArrayList();
                    jsonReader.beginArray();
                    while (jsonReader.hasNext()) {
                        this.sections.add(new Section(jsonReader));
                    }
                    jsonReader.endArray();
                } else if (jsonReader.peek() == JsonToken.NULL) {
                    jsonReader.nextNull();
                    this.sections = null;
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

    public String getDescription() {
        return this.description;
    }

    public void setDescription(String str) {
        this.description = str;
    }

    public List<Section> getSections() {
        return this.sections;
    }

    public void setSections(List<Section> list) {
        this.sections = list;
    }
}
