package net.gogame.gowrap.ui.dpro.model.equipmentcollection;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.support.StringUtils;
import net.gogame.gowrap.ui.dpro.model.BaseResponse;

public class EquipmentCollectionResponse extends BaseResponse {
    private static final String KEY_EQUIPMENT_COLLECTION = "equipmentCollection";
    private EquipmentCollection equipmentCollection;

    public EquipmentCollectionResponse(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    protected boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (!StringUtils.isEquals(str, KEY_EQUIPMENT_COLLECTION)) {
            return super.doParse(jsonReader, str);
        }
        this.equipmentCollection = new EquipmentCollection(jsonReader);
        return true;
    }

    public EquipmentCollection getEquipmentCollection() {
        return this.equipmentCollection;
    }

    public void setEquipmentCollection(EquipmentCollection equipmentCollection) {
        this.equipmentCollection = equipmentCollection;
    }
}
