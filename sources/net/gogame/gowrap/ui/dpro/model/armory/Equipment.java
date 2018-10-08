package net.gogame.gowrap.ui.dpro.model.armory;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.StringUtils;

public class Equipment extends BaseJsonObject {
    private static final String KEY_BASE = "base";
    private static final String KEY_EXCEED = "exceed";
    private static final String KEY_SKILL_ITEMS = "skillItems";
    private UserStats base;
    private UserStats exceed;
    private List<SkillItem> skillItems;

    public Equipment(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    protected boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, KEY_BASE)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            }
            this.base = new UserStats(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_EXCEED)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            }
            this.exceed = new UserStats(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, KEY_SKILL_ITEMS)) {
            return false;
        } else {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            } else if (jsonReader.peek() == JsonToken.BEGIN_ARRAY) {
                jsonReader.beginArray();
                this.skillItems = new ArrayList();
                while (jsonReader.hasNext()) {
                    if (jsonReader.peek() == JsonToken.NULL) {
                        jsonReader.nextNull();
                        this.skillItems.add(null);
                    } else {
                        this.skillItems.add(new SkillItem(jsonReader));
                    }
                }
                jsonReader.endArray();
                return true;
            } else {
                throw new IllegalArgumentException("array or null expected");
            }
        }
    }

    public UserStats getBase() {
        return this.base;
    }

    public void setBase(UserStats userStats) {
        this.base = userStats;
    }

    public UserStats getExceed() {
        return this.exceed;
    }

    public void setExceed(UserStats userStats) {
        this.exceed = userStats;
    }

    public List<SkillItem> getSkillItems() {
        return this.skillItems;
    }

    public void setSkillItems(List<SkillItem> list) {
        this.skillItems = list;
    }
}
