package bolts;

import android.content.Context;
import android.net.Uri;
import android.webkit.JavascriptInterface;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import bolts.AppLink.Target;
import com.facebook.appevents.AppEventsConstants;
import com.google.android.gms.nearby.messages.Strategy;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.Callable;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class WebViewAppLinkResolver implements AppLinkResolver {
    private static final String KEY_AL_VALUE = "value";
    private static final String KEY_ANDROID = "android";
    private static final String KEY_APP_NAME = "app_name";
    private static final String KEY_CLASS = "class";
    private static final String KEY_PACKAGE = "package";
    private static final String KEY_SHOULD_FALLBACK = "should_fallback";
    private static final String KEY_URL = "url";
    private static final String KEY_WEB = "web";
    private static final String KEY_WEB_URL = "url";
    private static final String META_TAG_PREFIX = "al";
    private static final String PREFER_HEADER = "Prefer-Html-Meta-Tags";
    private static final String TAG_EXTRACTION_JAVASCRIPT = "javascript:boltsWebViewAppLinkResolverResult.setValue((function() {  var metaTags = document.getElementsByTagName('meta');  var results = [];  for (var i = 0; i < metaTags.length; i++) {    var property = metaTags[i].getAttribute('property');    if (property && property.substring(0, 'al:'.length) === 'al:') {      var tag = { \"property\": metaTags[i].getAttribute('property') };      if (metaTags[i].hasAttribute('content')) {        tag['content'] = metaTags[i].getAttribute('content');      }      results.push(tag);    }  }  return JSON.stringify(results);})())";
    private final Context context;

    public WebViewAppLinkResolver(Context context) {
        this.context = context;
    }

    private static List<Map<String, Object>> getAlList(Map<String, Object> map, String str) {
        List<Map<String, Object>> list = (List) map.get(str);
        return list == null ? Collections.emptyList() : list;
    }

    private static AppLink makeAppLinkFromAlData(Map<String, Object> map, Uri uri) {
        Map map2;
        Uri uri2;
        List arrayList = new ArrayList();
        List list = (List) map.get("android");
        if (list == null) {
            list = Collections.emptyList();
        }
        for (Map map22 : r0) {
            List alList = getAlList(map22, "url");
            List alList2 = getAlList(map22, KEY_PACKAGE);
            List alList3 = getAlList(map22, KEY_CLASS);
            List alList4 = getAlList(map22, "app_name");
            int max = Math.max(alList.size(), Math.max(alList2.size(), Math.max(alList3.size(), alList4.size())));
            int i = 0;
            while (i < max) {
                arrayList.add(new Target((String) (alList2.size() > i ? ((Map) alList2.get(i)).get("value") : null), (String) (alList3.size() > i ? ((Map) alList3.get(i)).get("value") : null), tryCreateUrl((String) (alList.size() > i ? ((Map) alList.get(i)).get("value") : null)), (String) (alList4.size() > i ? ((Map) alList4.get(i)).get("value") : null)));
                i++;
            }
        }
        list = (List) map.get("web");
        if (list == null || list.size() <= 0) {
            uri2 = uri;
        } else {
            map22 = (Map) list.get(0);
            List list2 = (List) map22.get("url");
            list = (List) map22.get(KEY_SHOULD_FALLBACK);
            if (list != null && list.size() > 0) {
                if (Arrays.asList(new String[]{"no", "false", AppEventsConstants.EVENT_PARAM_VALUE_NO}).contains(((String) ((Map) list.get(0)).get("value")).toLowerCase())) {
                    uri2 = null;
                    if (!(uri2 == null || list2 == null || list2.size() <= 0)) {
                        uri2 = tryCreateUrl((String) ((Map) list2.get(0)).get("value"));
                    }
                }
            }
            uri2 = uri;
            uri2 = tryCreateUrl((String) ((Map) list2.get(0)).get("value"));
        }
        return new AppLink(uri, arrayList, uri2);
    }

    private static Map<String, Object> parseAlData(JSONArray jSONArray) throws JSONException {
        Map hashMap = new HashMap();
        for (int i = 0; i < jSONArray.length(); i++) {
            JSONObject jSONObject = jSONArray.getJSONObject(i);
            String[] split = jSONObject.getString("property").split(":");
            if (split[0].equals(META_TAG_PREFIX)) {
                Map map = hashMap;
                int i2 = 1;
                while (i2 < split.length) {
                    List list;
                    List list2 = (List) map.get(split[i2]);
                    if (list2 == null) {
                        ArrayList arrayList = new ArrayList();
                        map.put(split[i2], arrayList);
                        list = arrayList;
                    } else {
                        list = list2;
                    }
                    Map map2 = list.size() > 0 ? (Map) list.get(list.size() - 1) : null;
                    if (map2 == null || i2 == split.length - 1) {
                        map2 = new HashMap();
                        list.add(map2);
                    }
                    i2++;
                    map = map2;
                }
                if (jSONObject.has(Param.CONTENT)) {
                    if (jSONObject.isNull(Param.CONTENT)) {
                        map.put("value", null);
                    } else {
                        map.put("value", jSONObject.getString(Param.CONTENT));
                    }
                }
            }
        }
        return hashMap;
    }

    private static String readFromConnection(URLConnection uRLConnection) throws IOException {
        InputStream inputStream;
        String str = null;
        if (uRLConnection instanceof HttpURLConnection) {
            HttpURLConnection httpURLConnection = (HttpURLConnection) uRLConnection;
            try {
                inputStream = uRLConnection.getInputStream();
            } catch (Exception e) {
                inputStream = httpURLConnection.getErrorStream();
            }
        } else {
            inputStream = uRLConnection.getInputStream();
        }
        try {
            String substring;
            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            byte[] bArr = new byte[1024];
            while (true) {
                int read = inputStream.read(bArr);
                if (read == -1) {
                    break;
                }
                byteArrayOutputStream.write(bArr, 0, read);
            }
            String contentEncoding = uRLConnection.getContentEncoding();
            if (contentEncoding == null) {
                for (String str2 : uRLConnection.getContentType().split(";")) {
                    str2 = str2.trim();
                    if (str2.startsWith("charset=")) {
                        substring = str2.substring("charset=".length());
                        break;
                    }
                }
                substring = contentEncoding;
                if (substring == null) {
                    substring = "UTF-8";
                }
            } else {
                substring = contentEncoding;
            }
            str2 = new String(byteArrayOutputStream.toByteArray(), substring);
            return str2;
        } finally {
            inputStream.close();
        }
    }

    private static Uri tryCreateUrl(String str) {
        return str == null ? null : Uri.parse(str);
    }

    public Task<AppLink> getAppLinkFromUrlInBackground(final Uri uri) {
        final Capture capture = new Capture();
        final Capture capture2 = new Capture();
        return Task.callInBackground(new Callable<Void>() {
            public Void call() throws Exception {
                Void voidR = null;
                URL url = new URL(uri.toString());
                URLConnection uRLConnection = null;
                while (url != null) {
                    URLConnection openConnection = url.openConnection();
                    if (openConnection instanceof HttpURLConnection) {
                        ((HttpURLConnection) openConnection).setInstanceFollowRedirects(true);
                    }
                    openConnection.setRequestProperty(WebViewAppLinkResolver.PREFER_HEADER, WebViewAppLinkResolver.META_TAG_PREFIX);
                    openConnection.connect();
                    if (openConnection instanceof HttpURLConnection) {
                        HttpURLConnection httpURLConnection = (HttpURLConnection) openConnection;
                        if (httpURLConnection.getResponseCode() < Strategy.TTL_SECONDS_DEFAULT || httpURLConnection.getResponseCode() >= 400) {
                            uRLConnection = openConnection;
                            url = null;
                        } else {
                            URL url2 = new URL(httpURLConnection.getHeaderField("Location"));
                            httpURLConnection.disconnect();
                            uRLConnection = openConnection;
                            url = url2;
                        }
                    } else {
                        uRLConnection = openConnection;
                        url = null;
                    }
                }
                try {
                    capture.set(WebViewAppLinkResolver.readFromConnection(uRLConnection));
                    capture2.set(uRLConnection.getContentType());
                    return voidR;
                } finally {
                    voidR = uRLConnection instanceof HttpURLConnection;
                    if (voidR != null) {
                        ((HttpURLConnection) uRLConnection).disconnect();
                    }
                }
            }
        }).onSuccessTask(new Continuation<Void, Task<JSONArray>>() {

            /* renamed from: bolts.WebViewAppLinkResolver$2$1 */
            class C01811 extends WebViewClient {
                private boolean loaded = false;

                C01811() {
                }

                private void runJavaScript(WebView webView) {
                    if (!this.loaded) {
                        this.loaded = true;
                        webView.loadUrl(WebViewAppLinkResolver.TAG_EXTRACTION_JAVASCRIPT);
                    }
                }

                public void onLoadResource(WebView webView, String str) {
                    super.onLoadResource(webView, str);
                    runJavaScript(webView);
                }

                public void onPageFinished(WebView webView, String str) {
                    super.onPageFinished(webView, str);
                    runJavaScript(webView);
                }
            }

            public Task<JSONArray> then(Task<Void> task) throws Exception {
                final TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
                WebView webView = new WebView(WebViewAppLinkResolver.this.context);
                webView.getSettings().setJavaScriptEnabled(true);
                webView.setNetworkAvailable(false);
                webView.setWebViewClient(new C01811());
                webView.addJavascriptInterface(new Object() {
                    @JavascriptInterface
                    public void setValue(String str) {
                        try {
                            taskCompletionSource.trySetResult(new JSONArray(str));
                        } catch (Exception e) {
                            taskCompletionSource.trySetError(e);
                        }
                    }
                }, "boltsWebViewAppLinkResolverResult");
                webView.loadDataWithBaseURL(uri.toString(), (String) capture.get(), capture2.get() != null ? ((String) capture2.get()).split(";")[0] : null, null, null);
                return taskCompletionSource.getTask();
            }
        }, Task.UI_THREAD_EXECUTOR).onSuccess(new Continuation<JSONArray, AppLink>() {
            public AppLink then(Task<JSONArray> task) throws Exception {
                return WebViewAppLinkResolver.makeAppLinkFromAlData(WebViewAppLinkResolver.parseAlData((JSONArray) task.getResult()), uri);
            }
        });
    }
}
