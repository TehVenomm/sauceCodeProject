package com.facebook.share.internal;

import android.support.annotation.Nullable;
import com.facebook.share.model.ShareOpenGraphAction;
import com.facebook.share.model.ShareOpenGraphObject;
import com.facebook.share.model.SharePhoto;
import java.util.List;
import org.json.JSONException;
import org.json.JSONObject;

public final class OpenGraphJSONUtility {

    public interface PhotoJSONProcessor {
        JSONObject toJSONObject(SharePhoto sharePhoto);
    }

    private OpenGraphJSONUtility() {
    }

    /* JADX WARNING: Incorrect type for immutable var: ssa=java.util.List, code=java.util.List<java.lang.Object>, for r3v0, types: [java.util.List, java.util.List<java.lang.Object>] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static org.json.JSONArray toJSONArray(java.util.List<java.lang.Object> r3, com.facebook.share.internal.OpenGraphJSONUtility.PhotoJSONProcessor r4) throws org.json.JSONException {
        /*
            org.json.JSONArray r0 = new org.json.JSONArray
            r0.<init>()
            java.util.Iterator r1 = r3.iterator()
        L_0x0009:
            boolean r2 = r1.hasNext()
            if (r2 == 0) goto L_0x001b
            java.lang.Object r2 = r1.next()
            java.lang.Object r2 = toJSONValue(r2, r4)
            r0.put(r2)
            goto L_0x0009
        L_0x001b:
            return r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.share.internal.OpenGraphJSONUtility.toJSONArray(java.util.List, com.facebook.share.internal.OpenGraphJSONUtility$PhotoJSONProcessor):org.json.JSONArray");
    }

    public static JSONObject toJSONObject(ShareOpenGraphAction shareOpenGraphAction, PhotoJSONProcessor photoJSONProcessor) throws JSONException {
        JSONObject jSONObject = new JSONObject();
        for (String str : shareOpenGraphAction.keySet()) {
            jSONObject.put(str, toJSONValue(shareOpenGraphAction.get(str), photoJSONProcessor));
        }
        return jSONObject;
    }

    private static JSONObject toJSONObject(ShareOpenGraphObject shareOpenGraphObject, PhotoJSONProcessor photoJSONProcessor) throws JSONException {
        JSONObject jSONObject = new JSONObject();
        for (String str : shareOpenGraphObject.keySet()) {
            jSONObject.put(str, toJSONValue(shareOpenGraphObject.get(str), photoJSONProcessor));
        }
        return jSONObject;
    }

    public static Object toJSONValue(@Nullable Object obj, PhotoJSONProcessor photoJSONProcessor) throws JSONException {
        if (obj == null) {
            return JSONObject.NULL;
        }
        if ((obj instanceof String) || (obj instanceof Boolean) || (obj instanceof Double) || (obj instanceof Float) || (obj instanceof Integer) || (obj instanceof Long)) {
            return obj;
        }
        if (obj instanceof SharePhoto) {
            if (photoJSONProcessor != null) {
                return photoJSONProcessor.toJSONObject((SharePhoto) obj);
            }
            return null;
        } else if (obj instanceof ShareOpenGraphObject) {
            return toJSONObject((ShareOpenGraphObject) obj, photoJSONProcessor);
        } else {
            if (obj instanceof List) {
                return toJSONArray((List) obj, photoJSONProcessor);
            }
            throw new IllegalArgumentException("Invalid object found for JSON serialization: " + obj.toString());
        }
    }
}
