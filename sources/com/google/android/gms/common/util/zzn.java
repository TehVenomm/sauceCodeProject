package com.google.android.gms.common.util;

import android.support.v4.view.MotionEventCompat;
import android.text.TextUtils;
import java.util.Iterator;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public final class zzn {
    private static final Pattern zzfyx = Pattern.compile("\\\\.");
    private static final Pattern zzfyy = Pattern.compile("[\\\\\"/\b\f\n\r\t]");

    public static boolean zzc(Object obj, Object obj2) {
        if (obj == null && obj2 == null) {
            return true;
        }
        if (!(obj == null || obj2 == null)) {
            if ((obj instanceof JSONObject) && (obj2 instanceof JSONObject)) {
                JSONObject jSONObject = (JSONObject) obj;
                JSONObject jSONObject2 = (JSONObject) obj2;
                if (jSONObject.length() == jSONObject2.length()) {
                    Iterator keys = jSONObject.keys();
                    while (keys.hasNext()) {
                        String str = (String) keys.next();
                        if (jSONObject2.has(str)) {
                            try {
                                if (!zzc(jSONObject.get(str), jSONObject2.get(str))) {
                                    return false;
                                }
                            } catch (JSONException e) {
                                return false;
                            }
                        }
                    }
                    return true;
                }
            } else if (!(obj instanceof JSONArray) || !(obj2 instanceof JSONArray)) {
                return obj.equals(obj2);
            } else {
                JSONArray jSONArray = (JSONArray) obj;
                JSONArray jSONArray2 = (JSONArray) obj2;
                if (jSONArray.length() == jSONArray2.length()) {
                    int i = 0;
                    while (i < jSONArray.length()) {
                        try {
                            if (zzc(jSONArray.get(i), jSONArray2.get(i))) {
                                i++;
                            }
                        } catch (JSONException e2) {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
        return false;
    }

    public static String zzgk(String str) {
        if (TextUtils.isEmpty(str)) {
            return str;
        }
        Matcher matcher = zzfyy.matcher(str);
        StringBuffer stringBuffer = null;
        while (matcher.find()) {
            if (stringBuffer == null) {
                stringBuffer = new StringBuffer();
            }
            switch (matcher.group().charAt(0)) {
                case '\b':
                    matcher.appendReplacement(stringBuffer, "\\\\b");
                    break;
                case '\t':
                    matcher.appendReplacement(stringBuffer, "\\\\t");
                    break;
                case '\n':
                    matcher.appendReplacement(stringBuffer, "\\\\n");
                    break;
                case '\f':
                    matcher.appendReplacement(stringBuffer, "\\\\f");
                    break;
                case '\r':
                    matcher.appendReplacement(stringBuffer, "\\\\r");
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    matcher.appendReplacement(stringBuffer, "\\\\\\\"");
                    break;
                case MotionEventCompat.AXIS_GENERIC_16 /*47*/:
                    matcher.appendReplacement(stringBuffer, "\\\\/");
                    break;
                case '\\':
                    matcher.appendReplacement(stringBuffer, "\\\\\\\\");
                    break;
                default:
                    break;
            }
        }
        if (stringBuffer == null) {
            return str;
        }
        matcher.appendTail(stringBuffer);
        return stringBuffer.toString();
    }
}
