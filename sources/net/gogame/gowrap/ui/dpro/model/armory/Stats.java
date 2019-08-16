package net.gogame.gowrap.p019ui.dpro.model.armory;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.dpro.model.armory.Stats */
public class Stats extends BaseJsonObject {
    private static final String KEY_ATK = "atk";
    private static final String KEY_DARK_ATK = "darkAtk";
    private static final String KEY_DARK_DEF = "darkDef";
    private static final String KEY_DEF = "def";
    private static final String KEY_EARTH_ATK = "earthAtk";
    private static final String KEY_EARTH_DEF = "earthDef";
    private static final String KEY_FIRE_ATK = "fireAtk";
    private static final String KEY_FIRE_DEF = "fireDef";
    private static final String KEY_HP = "hp";
    private static final String KEY_ICE_ATK = "iceAtk";
    private static final String KEY_ICE_DEF = "iceDef";
    private static final String KEY_LIGHT_ATK = "lightAtk";
    private static final String KEY_LIGHT_DEF = "lightDef";
    private static final String KEY_WIND_ATK = "windAtk";
    private static final String KEY_WIND_DEF = "windDef";
    private Long atk;
    private Long darkAtk;
    private Long darkDef;
    private Long def;
    private Long earthAtk;
    private Long earthDef;
    private Long fireAtk;
    private Long fireDef;

    /* renamed from: hp */
    private Long f1418hp;
    private Long iceAtk;
    private Long iceDef;
    private Long lightAtk;
    private Long lightDef;
    private Long windAtk;
    private Long windDef;

    public Stats() {
    }

    public Stats(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, KEY_ATK)) {
            this.atk = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_DEF)) {
            this.def = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_HP)) {
            this.f1418hp = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_FIRE_ATK)) {
            this.fireAtk = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_FIRE_DEF)) {
            this.fireDef = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_ICE_ATK)) {
            this.iceAtk = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_ICE_DEF)) {
            this.iceDef = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_WIND_ATK)) {
            this.windAtk = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_WIND_DEF)) {
            this.windDef = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_EARTH_ATK)) {
            this.earthAtk = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_EARTH_DEF)) {
            this.earthDef = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_LIGHT_ATK)) {
            this.lightAtk = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_LIGHT_DEF)) {
            this.lightDef = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_DARK_ATK)) {
            this.darkAtk = JSONUtils.optLong(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, KEY_DARK_DEF)) {
            return false;
        } else {
            this.darkDef = JSONUtils.optLong(jsonReader);
            return true;
        }
    }

    public Long getAtk() {
        return this.atk;
    }

    public void setAtk(Long l) {
        this.atk = l;
    }

    public Long getDef() {
        return this.def;
    }

    public void setDef(Long l) {
        this.def = l;
    }

    public Long getHp() {
        return this.f1418hp;
    }

    public void setHp(Long l) {
        this.f1418hp = l;
    }

    public Long getFireAtk() {
        return this.fireAtk;
    }

    public void setFireAtk(Long l) {
        this.fireAtk = l;
    }

    public Long getFireDef() {
        return this.fireDef;
    }

    public void setFireDef(Long l) {
        this.fireDef = l;
    }

    public Long getIceAtk() {
        return this.iceAtk;
    }

    public void setIceAtk(Long l) {
        this.iceAtk = l;
    }

    public Long getIceDef() {
        return this.iceDef;
    }

    public void setIceDef(Long l) {
        this.iceDef = l;
    }

    public Long getWindAtk() {
        return this.windAtk;
    }

    public void setWindAtk(Long l) {
        this.windAtk = l;
    }

    public Long getWindDef() {
        return this.windDef;
    }

    public void setWindDef(Long l) {
        this.windDef = l;
    }

    public Long getEarthAtk() {
        return this.earthAtk;
    }

    public void setEarthAtk(Long l) {
        this.earthAtk = l;
    }

    public Long getEarthDef() {
        return this.earthDef;
    }

    public void setEarthDef(Long l) {
        this.earthDef = l;
    }

    public Long getLightAtk() {
        return this.lightAtk;
    }

    public void setLightAtk(Long l) {
        this.lightAtk = l;
    }

    public Long getLightDef() {
        return this.lightDef;
    }

    public void setLightDef(Long l) {
        this.lightDef = l;
    }

    public Long getDarkAtk() {
        return this.darkAtk;
    }

    public void setDarkAtk(Long l) {
        this.darkAtk = l;
    }

    public Long getDarkDef() {
        return this.darkDef;
    }

    public void setDarkDef(Long l) {
        this.darkDef = l;
    }
}
