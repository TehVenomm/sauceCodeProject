package jp.colopl.network;

import android.content.Context;
import android.location.Location;
import com.github.droidfu.concurrent.BetterAsyncTask;
import java.util.HashMap;
import java.util.List;
import jp.colopl.config.Config;
import jp.colopl.util.HTTP;
import org.apache.http.NameValuePair;

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

    protected abstract void after(Context context, String str);

    protected String doCheckedInBackground(Context context, Void... voidArr) throws Exception {
        super.doCheckedInBackground(context, voidArr);
        return handleResponseInBackground(HTTP.post(this.mPostUrl, getPostData(), HTTP.createCookies(this.mConfig)));
    }

    protected Config getConfig() {
        return this.mConfig;
    }

    protected HashMap<String, Location> getLocations() {
        return this.mLocations;
    }

    protected abstract List<NameValuePair> getPostData();

    protected String getPostUrl() {
        return this.mPostUrl;
    }

    protected abstract void handleError(Context context, Exception exception);

    protected String handleResponseInBackground(String str) {
        return str;
    }
}
