package p018jp.colopl.network;

import android.content.Context;
import android.location.Location;
import android.text.TextUtils;
import com.appsflyer.AppsFlyerLib;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONException;
import org.json.JSONObject;
import p018jp.colopl.util.Crypto;
import p018jp.colopl.util.LocationUtil;

/* renamed from: jp.colopl.network.PostLocationAsGuestAsyncTask */
public class PostLocationAsGuestAsyncTask extends PostLocationAsynTaskBase {
    String mAid;
    PostLocationAsGuestAsyncTaskDelegate mDelegate;
    String mGuid;
    String mHash;
    String mTime;

    public PostLocationAsGuestAsyncTask(Context context, String str, HashMap<String, Location> hashMap, String str2, String str3, String str4, String str5) {
        super(context, str, hashMap);
        this.mAid = str2;
        this.mGuid = str3;
        this.mTime = str4;
        this.mHash = str5;
    }

    /* access modifiers changed from: protected */
    public void after(Context context, String str) {
        if (this.mDelegate != null && !isCancelled()) {
            this.mDelegate.onPostLocationAsGuest(str);
        }
    }

    /* access modifiers changed from: protected */
    public List<NameValuePair> getPostData() {
        ArrayList arrayList = new ArrayList();
        arrayList.add(LocationUtil.getMostAccurateLocation(getLocations()));
        String encryptedLocations = LocationUtil.getEncryptedLocations(arrayList);
        ArrayList arrayList2 = new ArrayList(5);
        arrayList2.add(new BasicNameValuePair(AppsFlyerLib.ATTRIBUTION_ID_COLUMN_NAME, this.mAid));
        arrayList2.add(new BasicNameValuePair("puid", this.mGuid));
        arrayList2.add(new BasicNameValuePair("t", this.mTime));
        arrayList2.add(new BasicNameValuePair("sk", this.mHash));
        arrayList2.add(new BasicNameValuePair("location", encryptedLocations));
        return arrayList2;
    }

    /* access modifiers changed from: protected */
    public void handleError(Context context, Exception exc) {
        if (this.mDelegate != null && !isCancelled()) {
            this.mDelegate.onPostLocationAsGuest(null);
        }
    }

    /* access modifiers changed from: protected */
    public String handleResponseInBackground(String str) {
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        if (str.charAt(str.length() - 1) == 10) {
            str = str.substring(0, str.length() - 1);
        }
        try {
            String decrypt = Crypto.decrypt(str);
            if (decrypt != null) {
                try {
                    try {
                        if (new JSONObject(decrypt).getJSONObject("status").getInt("code") != 100) {
                            return null;
                        }
                    } catch (JSONException e) {
                        e = e;
                        e.printStackTrace();
                        return null;
                    }
                } catch (JSONException e2) {
                    e = e2;
                    e.printStackTrace();
                    return null;
                }
            }
            return decrypt;
        } catch (Exception e3) {
            e3.printStackTrace();
            return null;
        }
    }

    public void setDelegate(PostLocationAsGuestAsyncTaskDelegate postLocationAsGuestAsyncTaskDelegate) {
        this.mDelegate = postLocationAsGuestAsyncTaskDelegate;
    }
}
