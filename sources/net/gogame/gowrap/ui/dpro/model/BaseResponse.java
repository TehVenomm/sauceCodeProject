package net.gogame.gowrap.ui.dpro.model;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

public class BaseResponse extends BaseJsonObject {
    private static final String KEY_ERROR_MESSAGE = "errorMessage";
    private static final String KEY_STATUS_CODE = "statusCode";
    private String errorMessage;
    private Integer statusCode;

    public BaseResponse(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    protected boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, KEY_STATUS_CODE)) {
            this.statusCode = JSONUtils.optInt(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, KEY_ERROR_MESSAGE)) {
            return false;
        } else {
            this.errorMessage = JSONUtils.optString(jsonReader);
            return true;
        }
    }

    public int getStatusCode() {
        return this.statusCode.intValue();
    }

    public void setStatusCode(int i) {
        this.statusCode = Integer.valueOf(i);
    }

    public String getErrorMessage() {
        return this.errorMessage;
    }

    public void setErrorMessage(String str) {
        this.errorMessage = str;
    }
}
