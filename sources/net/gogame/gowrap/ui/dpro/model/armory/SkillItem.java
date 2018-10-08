package net.gogame.gowrap.ui.dpro.model.armory;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.StringUtils;

public class SkillItem extends BaseJsonObject {
    private static final String KEY_BASE = "base";
    private UserStats base;

    public SkillItem(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    protected boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (!StringUtils.isEquals(str, KEY_BASE)) {
            return false;
        }
        if (jsonReader.peek() == JsonToken.NULL) {
            jsonReader.nextNull();
            return true;
        }
        this.base = new UserStats(jsonReader);
        return true;
    }

    public UserStats getBase() {
        return this.base;
    }

    public void setBase(UserStats userStats) {
        this.base = userStats;
    }
}
