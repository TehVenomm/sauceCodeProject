package com.facebook;

import android.net.Uri;
import android.os.Bundle;
import com.facebook.share.internal.OpenGraphJSONUtility;
import com.facebook.share.internal.OpenGraphJSONUtility.PhotoJSONProcessor;
import com.facebook.share.model.ShareOpenGraphObject;
import com.facebook.share.model.SharePhoto;
import java.util.Locale;
import org.json.JSONException;
import org.json.JSONObject;

public class ShareGraphRequest {
    public static GraphRequest createOpenGraphObject(ShareOpenGraphObject shareOpenGraphObject) throws FacebookException {
        String string = shareOpenGraphObject.getString("type");
        String str = string == null ? shareOpenGraphObject.getString("og:type") : string;
        if (str == null) {
            throw new FacebookException("Open graph object type cannot be null");
        }
        try {
            JSONObject jSONObject = (JSONObject) OpenGraphJSONUtility.toJSONValue(shareOpenGraphObject, new PhotoJSONProcessor() {
                public JSONObject toJSONObject(SharePhoto sharePhoto) {
                    Uri imageUrl = sharePhoto.getImageUrl();
                    JSONObject jSONObject = new JSONObject();
                    try {
                        jSONObject.put("url", imageUrl.toString());
                        return jSONObject;
                    } catch (Exception e) {
                        throw new FacebookException("Unable to attach images", (Throwable) e);
                    }
                }
            });
            Bundle bundle = new Bundle();
            bundle.putString("object", jSONObject.toString());
            return new GraphRequest(AccessToken.getCurrentAccessToken(), String.format(Locale.ROOT, "%s/%s", new Object[]{"me", "objects/" + str}), bundle, HttpMethod.POST);
        } catch (JSONException e) {
            throw new FacebookException(e.getMessage());
        }
    }
}
