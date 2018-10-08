package jp.colopl.network;

public interface HttpRequestListener {
    void onReceiveError(HttpPostAsyncTask httpPostAsyncTask, Exception exception);

    void onReceiveResponse(HttpPostAsyncTask httpPostAsyncTask, String str);
}
