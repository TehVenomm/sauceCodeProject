package net.gogame.gowrap.p019ui.dpro.model.armory;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.dpro.model.armory.EquipmentSet */
public class EquipmentSet extends BaseJsonObject {
    private static final String KEY_ARM = "arm";
    private static final String KEY_ARMOR = "armor";
    private static final String KEY_ATK = "atk";
    private static final String KEY_DEF = "def";
    private static final String KEY_HELM = "helm";
    private static final String KEY_HP = "hp";
    private static final String KEY_LEG = "leg";
    private static final String KEY_NAME = "name";
    private static final String KEY_USER = "user";
    private static final String KEY_WEAPON0 = "weapon0";
    private static final String KEY_WEAPON1 = "weapon1";
    private static final String KEY_WEAPON2 = "weapon2";
    private Equipment arm;
    private Equipment armor;
    private Long atk;
    private Long def;
    private Equipment helm;

    /* renamed from: hp */
    private Long f1417hp;
    private Equipment leg;
    private String name;
    private UserStats user;
    private Equipment weapon0;
    private Equipment weapon1;
    private Equipment weapon2;

    public EquipmentSet() {
    }

    public EquipmentSet(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, "name")) {
            this.name = JSONUtils.optString(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_ATK)) {
            this.atk = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_DEF)) {
            this.def = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_HP)) {
            this.f1417hp = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_USER)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            }
            this.user = new UserStats(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_WEAPON0)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            }
            this.weapon0 = new Equipment(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_WEAPON1)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            }
            this.weapon1 = new Equipment(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_WEAPON2)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            }
            this.weapon2 = new Equipment(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_ARMOR)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            }
            this.armor = new Equipment(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_HELM)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            }
            this.helm = new Equipment(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_ARM)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            }
            this.arm = new Equipment(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, KEY_LEG)) {
            return false;
        } else {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            }
            this.leg = new Equipment(jsonReader);
            return true;
        }
    }

    public String getName() {
        return this.name;
    }

    public void setName(String str) {
        this.name = str;
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
        return this.f1417hp;
    }

    public void setHp(Long l) {
        this.f1417hp = l;
    }

    public UserStats getUser() {
        return this.user;
    }

    public void setUser(UserStats userStats) {
        this.user = userStats;
    }

    public Equipment getWeapon0() {
        return this.weapon0;
    }

    public void setWeapon0(Equipment equipment) {
        this.weapon0 = equipment;
    }

    public Equipment getWeapon1() {
        return this.weapon1;
    }

    public void setWeapon1(Equipment equipment) {
        this.weapon1 = equipment;
    }

    public Equipment getWeapon2() {
        return this.weapon2;
    }

    public void setWeapon2(Equipment equipment) {
        this.weapon2 = equipment;
    }

    public Equipment getArmor() {
        return this.armor;
    }

    public void setArmor(Equipment equipment) {
        this.armor = equipment;
    }

    public Equipment getHelm() {
        return this.helm;
    }

    public void setHelm(Equipment equipment) {
        this.helm = equipment;
    }

    public Equipment getArm() {
        return this.arm;
    }

    public void setArm(Equipment equipment) {
        this.arm = equipment;
    }

    public Equipment getLeg() {
        return this.leg;
    }

    public void setLeg(Equipment equipment) {
        this.leg = equipment;
    }
}
