package net.gogame.gowrap.ui.dpro.model.armory;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

public class UserStats extends BaseJsonObject {
    private static final String KEY_EQUIP_SLOT = "equipSlot";
    private static final String KEY_LEVEL = "level";
    private static final String KEY_MAIN_ITEM_ICON_ID = "mainItemIconId";
    private static final String KEY_MAIN_ITEM_ID = "mainItemId";
    private static final String KEY_MAIN_ITEM_TYPE = "mainItemType";
    private static final String KEY_MAX_LEVEL = "maxLevel";
    private static final String KEY_STATS = "stats";
    private static final String KEY_STATS_TYPE = "statsType";
    private static final String KEY_STAT_ITEM_NAME = "statItemName";
    private static final String KEY_STAT_ITEM_RARITY = "statItemRarity";
    private static final String KEY_SUB_ITEM_ICON_ID = "subItemIconId";
    private static final String KEY_SUB_ITEM_ID = "subItemId";
    private static final String KEY_SUB_ITEM_TYPE = "subItemType";
    private Integer equipSlot;
    private Integer level;
    private Long mainItemIconId;
    private Long mainItemId;
    private Integer mainItemType;
    private Integer maxLevel;
    private String statItemName;
    private Integer statItemRarity;
    private Stats stats;
    private Integer statsType;
    private Long subItemIconId;
    private Long subItemId;
    private Integer subItemType;

    public UserStats(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    protected boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, KEY_EQUIP_SLOT)) {
            this.equipSlot = JSONUtils.optInt(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_STATS_TYPE)) {
            this.statsType = JSONUtils.optInt(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_MAIN_ITEM_ID)) {
            this.mainItemId = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_MAIN_ITEM_TYPE)) {
            this.mainItemType = JSONUtils.optInt(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_MAIN_ITEM_ICON_ID)) {
            this.mainItemIconId = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_SUB_ITEM_ID)) {
            this.subItemId = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_SUB_ITEM_TYPE)) {
            this.subItemType = JSONUtils.optInt(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_SUB_ITEM_ICON_ID)) {
            this.subItemIconId = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_STAT_ITEM_NAME)) {
            this.statItemName = JSONUtils.optString(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_STAT_ITEM_RARITY)) {
            this.statItemRarity = JSONUtils.optInt(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, "level")) {
            this.level = JSONUtils.optInt(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_MAX_LEVEL)) {
            this.maxLevel = JSONUtils.optInt(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, KEY_STATS)) {
            return false;
        } else {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            }
            this.stats = new Stats(jsonReader);
            return true;
        }
    }

    public Integer getEquipSlot() {
        return this.equipSlot;
    }

    public void setEquipSlot(Integer num) {
        this.equipSlot = num;
    }

    public Integer getStatsType() {
        return this.statsType;
    }

    public void setStatsType(Integer num) {
        this.statsType = num;
    }

    public Long getMainItemId() {
        return this.mainItemId;
    }

    public void setMainItemId(Long l) {
        this.mainItemId = l;
    }

    public Integer getMainItemType() {
        return this.mainItemType;
    }

    public void setMainItemType(Integer num) {
        this.mainItemType = num;
    }

    public Long getMainItemIconId() {
        return this.mainItemIconId;
    }

    public void setMainItemIconId(Long l) {
        this.mainItemIconId = l;
    }

    public Long getSubItemId() {
        return this.subItemId;
    }

    public void setSubItemId(Long l) {
        this.subItemId = l;
    }

    public Integer getSubItemType() {
        return this.subItemType;
    }

    public void setSubItemType(Integer num) {
        this.subItemType = num;
    }

    public Long getSubItemIconId() {
        return this.subItemIconId;
    }

    public void setSubItemIconId(Long l) {
        this.subItemIconId = l;
    }

    public String getStatItemName() {
        return this.statItemName;
    }

    public void setStatItemName(String str) {
        this.statItemName = str;
    }

    public Integer getStatItemRarity() {
        return this.statItemRarity;
    }

    public void setStatItemRarity(Integer num) {
        this.statItemRarity = num;
    }

    public Integer getLevel() {
        return this.level;
    }

    public void setLevel(Integer num) {
        this.level = num;
    }

    public Integer getMaxLevel() {
        return this.maxLevel;
    }

    public void setMaxLevel(Integer num) {
        this.maxLevel = num;
    }

    public Stats getStats() {
        return this.stats;
    }

    public void setStats(Stats stats) {
        this.stats = stats;
    }
}
