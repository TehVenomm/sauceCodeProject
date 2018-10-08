package jp.colopl.network;

import android.content.Context;
import com.github.droidfu.concurrent.BetterAsyncTask;
import java.util.List;
import jp.colopl.config.Config;
import jp.colopl.util.HTTP;
import org.apache.http.NameValuePair;

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

    protected void after(Context context, String str) {
        if (this.listener != null) {
            this.listener.onReceiveResponse(this, str);
        }
    }

    protected String doCheckedInBackground(Context context, Void... voidArr) throws Exception {
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

    protected void handleError(Context context, Exception exception) {
        if (this.listener != null) {
            this.listener.onReceiveError(this, exception);
        }
    }

    public void setListener(HttpRequestListener httpRequestListener) {
        this.listener = httpRequestListener;
    }

    public void setTag(int i) {
        this.tag = i;
    }
}
