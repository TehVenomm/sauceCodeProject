package net.gogame.gowrap.p019ui.dpro.model.armory;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.dpro.model.armory.Armory */
public class Armory extends BaseJsonObject {
    private static final String KEY_CURRENT_EQUIP_SET_NO = "currentEquipSetNo";
    private static final String KEY_EQUIPMENT_SETS = "equipmentSets";
    private Integer currentEquipSetNo;
    private List<EquipmentSet> equipmentSets;

    public Armory() {
    }

    public Armory(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, KEY_CURRENT_EQUIP_SET_NO)) {
            this.currentEquipSetNo = JSONUtils.optInt(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, KEY_EQUIPMENT_SETS)) {
            return false;
        } else {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            } else if (jsonReader.peek() == JsonToken.BEGIN_ARRAY) {
                jsonReader.beginArray();
                this.equipmentSets = new ArrayList();
                while (jsonReader.hasNext()) {
                    if (jsonReader.peek() == JsonToken.NULL) {
                        jsonReader.nextNull();
                        this.equipmentSets.add(null);
                    } else {
                        this.equipmentSets.add(new EquipmentSet(jsonReader));
                    }
                }
                jsonReader.endArray();
                return true;
            } else {
                throw new IllegalArgumentException("array or null expected");
            }
        }
    }

    public Integer getCurrentEquipSetNo() {
        return this.currentEquipSetNo;
    }

    public void setCurrentEquipSetNo(Integer num) {
        this.currentEquipSetNo = num;
    }

    public List<EquipmentSet> getEquipmentSets() {
        return this.equipmentSets;
    }

    public void setEquipmentSets(List<EquipmentSet> list) {
        this.equipmentSets = list;
    }
}
