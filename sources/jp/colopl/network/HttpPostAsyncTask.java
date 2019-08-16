package p018jp.colopl.network;

import android.content.Context;
import com.github.droidfu.concurrent.BetterAsyncTask;
import java.util.List;
import org.apache.http.NameValuePair;
import p018jp.colopl.config.Config;
import p018jp.colopl.util.HTTP;

/* renamed from: jp.colopl.network.HttpPostAsyncTask */
public class HttpPostAsyncTask extends BetterAsyncTask<Void, Void, String> {
    private List<String> cookies;
    private HttpRequestListener listener;
    private List<NameValuePair> postData;
    private int tag;
    private String url;

    public HttpPostAsyncTask(Context context, String str, List<NameValuePair> list) {
        super(context);
        this.url = str;
        this.postData = list;
        this.cookies = HTTP.createCookies(new Config(context));
    }

    public HttpPostAsyncTask(Context context, String str, List<NameValuePair> list, List<String> list2) {
        super(context);
        this.url = str;
        this.postData = list;
        this.cookies = list2;
    }

    /* access modifiers changed from: protected */
    public void after(Context context, String str) {
        if (this.listener != null) {
            this.listener.onReceiveResponse(this, str);
        }
    }

    /* access modifiers changed from: protected */
    public String doCheckedInBackground(Context context, Void... voidArr) throws Exception {
        super.doCheckedInBackground(context, voidArr);
        String post = HTTP.post(this.url, this.postData, this.cookies);
        if (post != null) {
            return post;
        }
        throw new Exception();
    }

    public int getTag() {
        return this.tag;
    }

    /* access modifiers changed from: protected */
    public void handleError(Context context, Exception exc) {
        if (this.listener != null) {
            this.listener.onReceiveError(this, exc);
        }
    }

    public void setListener(HttpRequestListener httpRequestListener) {
        this.listener = httpRequestListener;
    }

    public void setTag(int i) {
        this.tag = i;
    }
}
