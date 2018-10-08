package net.gogame.gowrap.support;

import android.content.Context;
import android.util.JsonReader;
import android.util.JsonToken;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.net.URL;
import net.gogame.gowrap.io.utils.IOUtils;
import org.json.JSONException;
import org.json.JSONObject;

public final class JSONUtils {
    private JSONUtils() {
    }

    public static JSONObject read(InputStream inputStream) throws IOException, JSONException {
        return new JSONObject(IOUtils.toString(inputStream, "UTF-8"));
    }

    public static JSONObject read(File file) throws IOException, JSONException {
        InputStream newInputStream = IOUtils.newInputStream(file);
        try {
            JSONObject read = read(newInputStream);
            return read;
        } finally {
            IOUtils.closeQuietly(newInputStream);
        }
    }

    public static JSONObject assetRead(Context context, String str) throws IOException, JSONException {
        InputStream newInputStream = IOUtils.newInputStream(context, str);
        try {
            JSONObject read = read(newInputStream);
            return read;
        } finally {
            IOUtils.closeQuietly(newInputStream);
        }
    }

    public static String optString(JSONObject jSONObject, String str) {
        return optString(jSONObject, str, null);
    }

    public static String optString(JSONObject jSONObject, String str, String str2) {
        if (!jSONObject.has(str)) {
            return str2;
        }
        if (str2 != null) {
            return jSONObject.optString(str, str2);
        }
        return jSONObject.optString(str);
    }

    public static Boolean optBoolean(JSONObject jSONObject, String str) {
        return optBoolean(jSONObject, str, null);
    }

    public static Boolean optBoolean(JSONObject jSONObject, String str, Boolean bool) {
        if (!jSONObject.has(str)) {
            return bool;
        }
        if (bool != null) {
            return Boolean.valueOf(jSONObject.optBoolean(str, bool.booleanValue()));
        }
        return Boolean.valueOf(jSONObject.optBoolean(str));
    }

    public static String optUrl(JSONObject jSONObject, String str) {
        return optUrl(jSONObject, str, null);
    }

    public static String optUrl(JSONObject jSONObject, String str, String str2) {
        if (!jSONObject.has(str)) {
            return str2;
        }
        String optString = jSONObject.optString(str);
        if (optString == null) {
            return str2;
        }
        optString = StringUtils.trimToNull(optString);
        try {
            URL url = new URL(optString);
            return optString;
        } catch (MalformedURLException e) {
            return str2;
        }
    }

    public static String optString(JsonReader jsonReader) throws IOException {
        if (jsonReader.peek() == JsonToken.STRING) {
            return jsonReader.nextString();
        }
        if (jsonReader.peek() == JsonToken.NULL) {
            jsonReader.nextNull();
            return null;
        }
        throw new IllegalArgumentException("string or null expected");
    }

    public static Long optLong(JsonReader jsonReader) throws IOException {
        if (jsonReader.peek() == JsonToken.NUMBER) {
            return Long.valueOf(jsonReader.nextLong());
        }
        if (jsonReader.peek() == JsonToken.NULL) {
            jsonReader.nextNull();
            return null;
        }
        throw new IllegalArgumentException("long or null expected");
    }

    public static Integer optInt(JsonReader jsonReader) throws IOException {
        if (jsonReader.peek() == JsonToken.NUMBER) {
            return Integer.valueOf(jsonReader.nextInt());
        }
        if (jsonReader.peek() == JsonToken.NULL) {
            jsonReader.nextNull();
            return null;
        }
        throw new IllegalArgumentException("int or null expected");
    }

    public static void closeQuietly(JsonReader jsonReader) {
        if (jsonReader != null) {
            try {
                jsonReader.close();
            } catch (IOException e) {
            }
        }
    }
}
