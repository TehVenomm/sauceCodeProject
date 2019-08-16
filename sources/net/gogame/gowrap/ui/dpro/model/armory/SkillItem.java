package net.gogame.gowrap.p019ui.dpro.model.armory;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.dpro.model.armory.SkillItem */
public class SkillItem extends BaseJsonObject {
    private static final String KEY_BASE = "base";
    private UserStats base;

    public SkillItem() {
    }

    public SkillItem(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public boolean doParse(JsonReader jsonReader, String str) throws IOException {
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
