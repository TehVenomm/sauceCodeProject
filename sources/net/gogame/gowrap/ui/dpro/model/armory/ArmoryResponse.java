package net.gogame.gowrap.ui.dpro.model.armory;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.support.StringUtils;
import net.gogame.gowrap.ui.dpro.model.BaseResponse;

public class ArmoryResponse extends BaseResponse {
    private static final String KEY_ARMORY = "armory";
    private Armory armory;

    public ArmoryResponse(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    protected boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (!StringUtils.isEquals(str, KEY_ARMORY)) {
            return super.doParse(jsonReader, str);
        }
        this.armory = new Armory(jsonReader);
        return true;
    }

    public Armory getArmory() {
        return this.armory;
    }

    public void setArmory(Armory armory) {
        this.armory = armory;
    }
}
