package p018jp.colopl.network;

/* renamed from: jp.colopl.network.HttpRequestListener */
public interface HttpRequestListener {
    void onReceiveError(HttpPostAsyncTask httpPostAsyncTask, Exception exc);

    void onReceiveResponse(HttpPostAsyncTask httpPostAsyncTask, String str);
}
