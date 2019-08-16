package net.gogame.gowrap.p019ui.dpro.model.equipmentcollection;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.dpro.model.equipmentcollection.Equipment */
public class Equipment extends BaseJsonObject {
    private static final String KEY_ELEMENT = "element";
    private static final String KEY_ICON_ID = "iconId";
    private static final String KEY_ID = "id";
    private static final String KEY_NAME = "name";
    private static final String KEY_POINTS = "points";
    private static final String KEY_RARITY = "rarity";
    private Integer element;
    private Long iconId;

    /* renamed from: id */
    private Long f1419id;
    private String name;
    private Long points;
    private Integer rarity;

    public Equipment() {
    }

    public Equipment(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, "id")) {
            this.f1419id = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, "name")) {
            this.name = JSONUtils.optString(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_ICON_ID)) {
            this.iconId = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_ELEMENT)) {
            this.element = JSONUtils.optInt(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_RARITY)) {
            this.rarity = JSONUtils.optInt(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, KEY_POINTS)) {
            return false;
        } else {
            this.points = JSONUtils.optLong(jsonReader);
            return true;
        }
    }

    public Long getId() {
        return this.f1419id;
    }

    public void setId(Long l) {
        this.f1419id = l;
    }

    public String getName() {
        return this.name;
    }

    public void setName(String str) {
        this.name = str;
    }

    public Long getIconId() {
        return this.iconId;
    }

    public void setIconId(Long l) {
        this.iconId = l;
    }

    public Integer getElement() {
        return this.element;
    }

    public void setElement(Integer num) {
        this.element = num;
    }

    public Integer getRarity() {
        return this.rarity;
    }

    public void setRarity(Integer num) {
        this.rarity = num;
    }

    public Long getPoints() {
        return this.points;
    }

    public void setPoints(Long l) {
        this.points = l;
    }
}
