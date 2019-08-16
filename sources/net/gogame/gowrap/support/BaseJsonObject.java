package net.gogame.gowrap.support;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import java.io.Serializable;

public abstract class BaseJsonObject implements Serializable {
    /* access modifiers changed from: protected */
    public abstract boolean doParse(JsonReader jsonReader, String str) throws IOException;

    public BaseJsonObject() {
    }

    public BaseJsonObject(JsonReader jsonReader) throws IOException {
        if (jsonReader.peek() == JsonToken.BEGIN_OBJECT) {
            jsonReader.beginObject();
            while (jsonReader.hasNext()) {
                if (!doParse(jsonReader, jsonReader.nextName())) {
                    jsonReader.skipValue();
                }
            }
            jsonReader.endObject();
            return;
        }
        throw new IllegalArgumentException("Invalid object");
    }
}
