package jp.colopl.network;

import android.content.Context;
import android.location.Location;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import jp.colopl.util.LocationUtil;
import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

public class PostLocationAsyncTask extends PostLocationAsynTaskBase {
    PostLocationAsyncTaskDelegate mDelegate;

    public PostLocationAsyncTask(Context context, String str, HashMap<String, Location> hashMap) {
        super(context, str, hashMap);
    }

    protected void after(Context context, String str) {
        this.mDelegate.onPostLocation(str);
    }

    public PostLocationAsyncTaskDelegate getDelegate() {
        return this.mDelegate;
    }

    protected List<NameValuePair> getPostData() {
        List arrayList = new ArrayList();
        arrayList.add(LocationUtil.getMostAccurateLocation(getLocations()));
        String encryptedLocations = LocationUtil.getEncryptedLocations(arrayList);
        List<NameValuePair> arrayList2 = new ArrayList(1);
        arrayList2.add(new BasicNameValuePair("location", encryptedLocations));
        return arrayList2;
    }

    protected void handleError(Context context, Exception exception) {
        this.mDelegate.onPostLocation(null);
    }

    public void setDelegate(PostLocationAsyncTaskDelegate postLocationAsyncTaskDelegate) {
        this.mDelegate = postLocationAsyncTaskDelegate;
    }
}
