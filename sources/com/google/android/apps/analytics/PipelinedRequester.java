package com.google.android.apps.analytics;

import android.util.Log;
import java.io.IOException;
import java.net.Socket;
import org.apache.http.Header;
import org.apache.http.HttpConnectionMetrics;
import org.apache.http.HttpEntityEnclosingRequest;
import org.apache.http.HttpException;
import org.apache.http.HttpHost;
import org.apache.http.HttpResponse;
import org.apache.http.HttpVersion;
import org.apache.http.conn.scheme.PlainSocketFactory;
import org.apache.http.conn.scheme.SocketFactory;
import org.apache.http.impl.DefaultHttpClientConnection;
import org.apache.http.params.BasicHttpParams;

class PipelinedRequester {
    private static final int RECEIVE_BUFFER_SIZE = 8192;
    Callbacks callbacks;
    boolean canPipeline;
    DefaultHttpClientConnection connection;
    HttpHost host;
    int lastStatusCode;
    SocketFactory socketFactory;

    interface Callbacks {
        void pipelineModeChanged(boolean z);

        void requestSent();

        void serverError(int i);
    }

    public PipelinedRequester(HttpHost httpHost) {
        this(httpHost, new PlainSocketFactory());
    }

    public PipelinedRequester(HttpHost httpHost, SocketFactory socketFactory2) {
        this.connection = new DefaultHttpClientConnection();
        this.canPipeline = true;
        this.host = httpHost;
        this.socketFactory = socketFactory2;
    }

    private void closeConnection() {
        if (this.connection != null && this.connection.isOpen()) {
            try {
                this.connection.close();
            } catch (IOException e) {
            }
        }
    }

    private void maybeOpenConnection() throws IOException {
        if (this.connection == null || !this.connection.isOpen()) {
            BasicHttpParams basicHttpParams = new BasicHttpParams();
            Socket connectSocket = this.socketFactory.connectSocket(this.socketFactory.createSocket(), this.host.getHostName(), this.host.getPort(), null, 0, basicHttpParams);
            connectSocket.setReceiveBufferSize(8192);
            this.connection.bind(connectSocket, basicHttpParams);
        }
    }

    public void addRequest(HttpEntityEnclosingRequest httpEntityEnclosingRequest) throws HttpException, IOException {
        maybeOpenConnection();
        this.connection.sendRequestHeader(httpEntityEnclosingRequest);
        this.connection.sendRequestEntity(httpEntityEnclosingRequest);
    }

    public void finishedCurrentRequests() {
        closeConnection();
    }

    public void installCallbacks(Callbacks callbacks2) {
        this.callbacks = callbacks2;
    }

    public void sendRequests() throws IOException, HttpException {
        this.connection.flush();
        HttpConnectionMetrics metrics = this.connection.getMetrics();
        while (metrics.getResponseCount() < metrics.getRequestCount()) {
            HttpResponse receiveResponseHeader = this.connection.receiveResponseHeader();
            if (!receiveResponseHeader.getStatusLine().getProtocolVersion().greaterEquals(HttpVersion.HTTP_1_1)) {
                this.callbacks.pipelineModeChanged(false);
                this.canPipeline = false;
            }
            Header[] headers = receiveResponseHeader.getHeaders("Connection");
            if (headers != null) {
                for (Header value : headers) {
                    if ("close".equalsIgnoreCase(value.getValue())) {
                        this.callbacks.pipelineModeChanged(false);
                        this.canPipeline = false;
                    }
                }
            }
            this.lastStatusCode = receiveResponseHeader.getStatusLine().getStatusCode();
            if (this.lastStatusCode != 200) {
                this.callbacks.serverError(this.lastStatusCode);
                closeConnection();
                return;
            }
            this.connection.receiveResponseEntity(receiveResponseHeader);
            receiveResponseHeader.getEntity().consumeContent();
            this.callbacks.requestSent();
            if (GoogleAnalyticsTracker.getInstance().getDebug()) {
                Log.v(GoogleAnalyticsTracker.LOG_TAG, "HTTP Response Code: " + receiveResponseHeader.getStatusLine().getStatusCode());
            }
            if (!this.canPipeline) {
                closeConnection();
                return;
            }
        }
    }
}
