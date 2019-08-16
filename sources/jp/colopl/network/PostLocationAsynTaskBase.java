package p018jp.colopl.network;

import android.content.Context;
import android.location.Location;
import com.github.droidfu.concurrent.BetterAsyncTask;
import java.util.HashMap;
import java.util.List;
import org.apache.http.NameValuePair;
import p018jp.colopl.config.Config;
import p018jp.colopl.util.HTTP;

/* renamed from: jp.colopl.network.PostLocationAsynTaskBase */
public abstract class PostLocationAsynTaskBase extends BetterAsyncTask<Void, Void, String> {
    private Config mConfig;
    private HashMap<String, Location> mLocations;
    private String mPostUrl;

    public PostLocationAsynTaskBase(Context context, String str, HashMap<String, Location> hashMap) {
        super(context);
        this.mPostUrl = str;
        this.mLocations = hashMap;
        this.mConfig = new Config(context);
    }

    /* access modifiers changed from: protected */
    public abstract void after(Context context, String str);

    /* access modifiers changed from: protected */
    public String doCheckedInBackground(Context context, Void... voidArr) throws Exception {
        super.doCheckedInBackground(context, voidArr);
        return handleResponseInBackground(HTTP.post(this.mPostUrl, getPostData(), HTTP.createCookies(this.mConfig)));
    }

    /* access modifiers changed from: protected */
    public Config getConfig() {
        return this.mConfig;
    }

    /* access modifiers changed from: protected */
    public HashMap<String, Location> getLocations() {
        return this.mLocations;
    }

    /* access modifiers changed from: protected */
    public abstract List<NameValuePair> getPostData();

    /* access modifiers changed from: protected */
    public String getPostUrl() {
        return this.mPostUrl;
    }

    /* access modifiers changed from: protected */
    public abstract void handleError(Context context, Exception exc);

    /* access modifiers changed from: protected */
    public String handleResponseInBackground(String str) {
        return str;
    }
}
