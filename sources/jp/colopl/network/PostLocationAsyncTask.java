package p018jp.colopl.network;

import android.content.Context;
import android.location.Location;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import p018jp.colopl.util.LocationUtil;

/* renamed from: jp.colopl.network.PostLocationAsyncTask */
public class PostLocationAsyncTask extends PostLocationAsynTaskBase {
    PostLocationAsyncTaskDelegate mDelegate;

    public PostLocationAsyncTask(Context context, String str, HashMap<String, Location> hashMap) {
        super(context, str, hashMap);
    }

    /* access modifiers changed from: protected */
    public void after(Context context, String str) {
        this.mDelegate.onPostLocation(str);
    }

    public PostLocationAsyncTaskDelegate getDelegate() {
        return this.mDelegate;
    }

    /* access modifiers changed from: protected */
    public List<NameValuePair> getPostData() {
        ArrayList arrayList = new ArrayList();
        arrayList.add(LocationUtil.getMostAccurateLocation(getLocations()));
        String encryptedLocations = LocationUtil.getEncryptedLocations(arrayList);
        ArrayList arrayList2 = new ArrayList(1);
        arrayList2.add(new BasicNameValuePair("location", encryptedLocations));
        return arrayList2;
    }

    /* access modifiers changed from: protected */
    public void handleError(Context context, Exception exc) {
        this.mDelegate.onPostLocation(null);
    }

    public void setDelegate(PostLocationAsyncTaskDelegate postLocationAsyncTaskDelegate) {
        this.mDelegate = postLocationAsyncTaskDelegate;
    }
}
