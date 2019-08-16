package net.gogame.gowrap.p019ui.dpro.model.armory;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.p019ui.dpro.model.BaseResponse;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.dpro.model.armory.ArmoryResponse */
public class ArmoryResponse extends BaseResponse {
    private static final String KEY_ARMORY = "armory";
    private Armory armory;

    public ArmoryResponse() {
    }

    public ArmoryResponse(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (!StringUtils.isEquals(str, KEY_ARMORY)) {
            return super.doParse(jsonReader, str);
        }
        this.armory = new Armory(jsonReader);
        return true;
    }

    public Armory getArmory() {
        return this.armory;
    }

    public void setArmory(Armory armory2) {
        this.armory = armory2;
    }
}
