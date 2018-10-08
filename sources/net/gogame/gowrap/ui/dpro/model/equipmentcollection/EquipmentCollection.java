package net.gogame.gowrap.ui.dpro.model.equipmentcollection;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

public class EquipmentCollection extends BaseJsonObject {
    private static final String KEY_EQUIPMENT_LIST = "equipmentList";
    private static final String KEY_FORGED_EQUIPMENT_COUNT = "forgedEquipmentCount";
    private static final String KEY_TOTAL_EQUIPMENT_COUNT = "totalEquipmentCount";
    private List<Equipment> equipmentList;
    private Integer forgedEquipmentCount;
    private Integer totalEquipmentCount;

    public EquipmentCollection(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    protected boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, KEY_EQUIPMENT_LIST)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            } else if (jsonReader.peek() == JsonToken.BEGIN_ARRAY) {
                jsonReader.beginArray();
                this.equipmentList = new ArrayList();
                while (jsonReader.hasNext()) {
                    if (jsonReader.peek() == JsonToken.NULL) {
                        this.equipmentList.add(null);
                        jsonReader.nextNull();
                    } else {
                        this.equipmentList.add(new Equipment(jsonReader));
                    }
                }
                jsonReader.endArray();
                return true;
            } else {
                throw new IllegalArgumentException("array or null expected");
            }
        } else if (StringUtils.isEquals(str, KEY_FORGED_EQUIPMENT_COUNT)) {
            this.forgedEquipmentCount = JSONUtils.optInt(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, KEY_TOTAL_EQUIPMENT_COUNT)) {
            return false;
        } else {
            this.totalEquipmentCount = JSONUtils.optInt(jsonReader);
            return true;
        }
    }

    public List<Equipment> getEquipmentList() {
        return this.equipmentList;
    }

    public void setEquipmentList(List<Equipment> list) {
        this.equipmentList = list;
    }

    public Integer getForgedEquipmentCount() {
        return this.forgedEquipmentCount;
    }

    public void setForgedEquipmentCount(Integer num) {
        this.forgedEquipmentCount = num;
    }

    public Integer getTotalEquipmentCount() {
        return this.totalEquipmentCount;
    }

    public void setTotalEquipmentCount(Integer num) {
        this.totalEquipmentCount = num;
    }
}
