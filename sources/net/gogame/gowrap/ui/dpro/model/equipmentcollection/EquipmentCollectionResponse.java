package net.gogame.gowrap.p019ui.dpro.model.equipmentcollection;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.p019ui.dpro.model.BaseResponse;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.dpro.model.equipmentcollection.EquipmentCollectionResponse */
public class EquipmentCollectionResponse extends BaseResponse {
    private static final String KEY_EQUIPMENT_COLLECTION = "equipmentCollection";
    private EquipmentCollection equipmentCollection;

    public EquipmentCollectionResponse() {
    }

    public EquipmentCollectionResponse(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (!StringUtils.isEquals(str, KEY_EQUIPMENT_COLLECTION)) {
            return super.doParse(jsonReader, str);
        }
        this.equipmentCollection = new EquipmentCollection(jsonReader);
        return true;
    }

    public EquipmentCollection getEquipmentCollection() {
        return this.equipmentCollection;
    }

    public void setEquipmentCollection(EquipmentCollection equipmentCollection2) {
        this.equipmentCollection = equipmentCollection2;
    }
}
