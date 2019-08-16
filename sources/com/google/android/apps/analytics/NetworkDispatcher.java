package com.google.android.apps.analytics;

import android.os.Build;
import android.os.Build.VERSION;
import android.os.Handler;
import android.os.HandlerThread;
import android.util.Log;
import com.google.android.apps.analytics.Dispatcher.Callbacks;
import java.io.IOException;
import java.util.Collections;
import java.util.LinkedList;
import java.util.Locale;
import org.apache.commons.lang3.StringUtils;
import org.apache.http.Header;
import org.apache.http.HttpException;
import org.apache.http.HttpHost;
import org.apache.http.ParseException;
import org.apache.http.entity.StringEntity;
import org.apache.http.message.BasicHttpEntityEnclosingRequest;
import p017io.fabric.sdk.android.services.network.HttpRequest;

class NetworkDispatcher implements Dispatcher {
    private static final String GOOGLE_ANALYTICS_HOST_NAME = "www.google-analytics.com";
    private static final int GOOGLE_ANALYTICS_HOST_PORT = 80;
    private static final int MAX_EVENTS_PER_PIPELINE = 30;
    private static final int MAX_GET_LENGTH = 2036;
    private static final int MAX_POST_LENGTH = 8192;
    private static final int MAX_SEQUENTIAL_REQUESTS = 5;
    private static final long MIN_RETRY_INTERVAL = 2;
    private static final String USER_AGENT_TEMPLATE = "%s/%s (Linux; U; Android %s; %s-%s; %s Build/%s)";
    private DispatcherThread dispatcherThread;
    private boolean dryRun;
    /* access modifiers changed from: private */
    public final HttpHost googleAnalyticsHost;
    private final String userAgent;

    private static class DispatcherThread extends HandlerThread {
        /* access modifiers changed from: private */
        public final Callbacks callbacks;
        /* access modifiers changed from: private */
        public AsyncDispatchTask currentTask;
        volatile Handler handlerExecuteOnDispatcherThread;
        /* access modifiers changed from: private */
        public int lastStatusCode;
        /* access modifiers changed from: private */
        public int maxEventsPerRequest;
        /* access modifiers changed from: private */
        public final NetworkDispatcher parent;
        /* access modifiers changed from: private */
        public final PipelinedRequester pipelinedRequester;
        /* access modifiers changed from: private */
        public final RequesterCallbacks requesterCallBacks;
        /* access modifiers changed from: private */
        public long retryInterval;
        /* access modifiers changed from: private */
        public final String userAgent;

        private class AsyncDispatchTask implements Runnable {
            private final LinkedList<Hit> hits = new LinkedList<>();

            public AsyncDispatchTask(Hit[] hitArr) {
                Collections.addAll(this.hits, hitArr);
            }

            private void dispatchSomePendingHits(boolean z) throws IOException, ParseException, HttpException {
                String str;
                String str2;
                BasicHttpEntityEnclosingRequest basicHttpEntityEnclosingRequest;
                if (GoogleAnalyticsTracker.getInstance().getDebug() && z) {
                    Log.v(GoogleAnalyticsTracker.LOG_TAG, "dispatching hits in dry run mode");
                }
                int i = 0;
                while (i < this.hits.size() && i < DispatcherThread.this.maxEventsPerRequest) {
                    String addQueueTimeParameter = Utils.addQueueTimeParameter(((Hit) this.hits.get(i)).hitString, System.currentTimeMillis());
                    int indexOf = addQueueTimeParameter.indexOf(63);
                    if (indexOf < 0) {
                        str2 = "";
                        str = addQueueTimeParameter;
                    } else {
                        str = indexOf > 0 ? addQueueTimeParameter.substring(0, indexOf) : "";
                        str2 = indexOf < addQueueTimeParameter.length() + -2 ? addQueueTimeParameter.substring(indexOf + 1) : "";
                    }
                    if (str2.length() < NetworkDispatcher.MAX_GET_LENGTH) {
                        basicHttpEntityEnclosingRequest = new BasicHttpEntityEnclosingRequest(HttpRequest.METHOD_GET, addQueueTimeParameter);
                    } else {
                        BasicHttpEntityEnclosingRequest basicHttpEntityEnclosingRequest2 = new BasicHttpEntityEnclosingRequest(HttpRequest.METHOD_POST, "/p" + str);
                        basicHttpEntityEnclosingRequest2.addHeader(HttpRequest.HEADER_CONTENT_LENGTH, Integer.toString(str2.length()));
                        basicHttpEntityEnclosingRequest2.addHeader(HttpRequest.HEADER_CONTENT_TYPE, "text/plain");
                        basicHttpEntityEnclosingRequest2.setEntity(new StringEntity(str2));
                        basicHttpEntityEnclosingRequest = basicHttpEntityEnclosingRequest2;
                    }
                    String hostName = DispatcherThread.this.parent.googleAnalyticsHost.getHostName();
                    if (DispatcherThread.this.parent.googleAnalyticsHost.getPort() != 80) {
                        hostName = hostName + ":" + DispatcherThread.this.parent.googleAnalyticsHost.getPort();
                    }
                    basicHttpEntityEnclosingRequest.addHeader("Host", hostName);
                    basicHttpEntityEnclosingRequest.addHeader("User-Agent", DispatcherThread.this.userAgent);
                    if (GoogleAnalyticsTracker.getInstance().getDebug()) {
                        StringBuffer stringBuffer = new StringBuffer();
                        for (Header obj : basicHttpEntityEnclosingRequest.getAllHeaders()) {
                            stringBuffer.append(obj.toString()).append(StringUtils.f1199LF);
                        }
                        stringBuffer.append(basicHttpEntityEnclosingRequest.getRequestLine().toString()).append(StringUtils.f1199LF);
                        Log.i(GoogleAnalyticsTracker.LOG_TAG, stringBuffer.toString());
                    }
                    if (str2.length() > 8192) {
                        Log.w(GoogleAnalyticsTracker.LOG_TAG, "Hit too long (> 8192 bytes)--not sent");
                        DispatcherThread.this.requesterCallBacks.requestSent();
                    } else if (z) {
                        DispatcherThread.this.requesterCallBacks.requestSent();
                    } else {
                        DispatcherThread.this.pipelinedRequester.addRequest(basicHttpEntityEnclosingRequest);
                    }
                    i++;
                }
                if (!z) {
                    DispatcherThread.this.pipelinedRequester.sendRequests();
                }
            }

            public Hit removeNextHit() {
                return (Hit) this.hits.poll();
            }

            public void run() {
                DispatcherThread.this.currentTask = this;
                int i = 0;
                while (i < 5 && this.hits.size() > 0) {
                    long j = 0;
                    try {
                        if (DispatcherThread.this.lastStatusCode == 500 || DispatcherThread.this.lastStatusCode == 503) {
                            j = (long) (Math.random() * ((double) DispatcherThread.this.retryInterval));
                            if (DispatcherThread.this.retryInterval < 256) {
                                DispatcherThread.access$630(DispatcherThread.this, 2);
                            }
                        } else {
                            DispatcherThread.this.retryInterval = 2;
                        }
                        Thread.sleep(j * 1000);
                        dispatchSomePendingHits(DispatcherThread.this.parent.isDryRun());
                        i++;
                    } catch (InterruptedException e) {
                        Log.w(GoogleAnalyticsTracker.LOG_TAG, "Couldn't sleep.", e);
                    } catch (IOException e2) {
                        Log.w(GoogleAnalyticsTracker.LOG_TAG, "Problem with socket or streams.", e2);
                    } catch (HttpException e3) {
                        Log.w(GoogleAnalyticsTracker.LOG_TAG, "Problem with http streams.", e3);
                    }
                }
                DispatcherThread.this.pipelinedRequester.finishedCurrentRequests();
                DispatcherThread.this.callbacks.dispatchFinished();
                DispatcherThread.this.currentTask = null;
            }
        }

        private class RequesterCallbacks implements Callbacks {
            private RequesterCallbacks() {
            }

            public void pipelineModeChanged(boolean z) {
                if (z) {
                    DispatcherThread.this.maxEventsPerRequest = 30;
                } else {
                    DispatcherThread.this.maxEventsPerRequest = 1;
                }
            }

            public void requestSent() {
                if (DispatcherThread.this.currentTask != null) {
                    Hit removeNextHit = DispatcherThread.this.currentTask.removeNextHit();
                    if (removeNextHit != null) {
                        DispatcherThread.this.callbacks.hitDispatched(removeNextHit.hitId);
                    }
                }
            }

            public void serverError(int i) {
                DispatcherThread.this.lastStatusCode = i;
            }
        }

        private DispatcherThread(Callbacks callbacks2, PipelinedRequester pipelinedRequester2, String str, NetworkDispatcher networkDispatcher) {
            super("DispatcherThread");
            this.maxEventsPerRequest = 30;
            this.currentTask = null;
            this.callbacks = callbacks2;
            this.userAgent = str;
            this.pipelinedRequester = pipelinedRequester2;
            this.requesterCallBacks = new RequesterCallbacks();
            this.pipelinedRequester.installCallbacks(this.requesterCallBacks);
            this.parent = networkDispatcher;
        }

        private DispatcherThread(Callbacks callbacks2, String str, NetworkDispatcher networkDispatcher) {
            this(callbacks2, new PipelinedRequester(networkDispatcher.googleAnalyticsHost), str, networkDispatcher);
        }

        static /* synthetic */ long access$630(DispatcherThread dispatcherThread, long j) {
            long j2 = dispatcherThread.retryInterval * j;
            dispatcherThread.retryInterval = j2;
            return j2;
        }

        public void dispatchHits(Hit[] hitArr) {
            if (this.handlerExecuteOnDispatcherThread != null) {
                this.handlerExecuteOnDispatcherThread.post(new AsyncDispatchTask(hitArr));
            }
        }

        /* access modifiers changed from: protected */
        public void onLooperPrepared() {
            this.handlerExecuteOnDispatcherThread = new Handler();
        }
    }

    public NetworkDispatcher() {
        this(GoogleAnalyticsTracker.PRODUCT, GoogleAnalyticsTracker.VERSION);
    }

    public NetworkDispatcher(String str, String str2) {
        this(str, str2, GOOGLE_ANALYTICS_HOST_NAME, 80);
    }

    NetworkDispatcher(String str, String str2, String str3, int i) {
        this.dryRun = false;
        this.googleAnalyticsHost = new HttpHost(str3, i);
        Locale locale = Locale.getDefault();
        this.userAgent = String.format(USER_AGENT_TEMPLATE, new Object[]{str, str2, VERSION.RELEASE, locale.getLanguage() != null ? locale.getLanguage().toLowerCase() : "en", locale.getCountry() != null ? locale.getCountry().toLowerCase() : "", Build.MODEL, Build.ID});
    }

    public void dispatchHits(Hit[] hitArr) {
        if (this.dispatcherThread != null) {
            this.dispatcherThread.dispatchHits(hitArr);
        }
    }

    /* access modifiers changed from: 0000 */
    public String getUserAgent() {
        return this.userAgent;
    }

    public void init(Callbacks callbacks) {
        stop();
        this.dispatcherThread = new DispatcherThread(callbacks, this.userAgent, this);
        this.dispatcherThread.start();
    }

    public void init(Callbacks callbacks, PipelinedRequester pipelinedRequester, HitStore hitStore) {
        stop();
        this.dispatcherThread = new DispatcherThread(callbacks, pipelinedRequester, this.userAgent, this);
        this.dispatcherThread.start();
    }

    public boolean isDryRun() {
        return this.dryRun;
    }

    public void setDryRun(boolean z) {
        this.dryRun = z;
    }

    public void stop() {
        if (this.dispatcherThread != null && this.dispatcherThread.getLooper() != null) {
            this.dispatcherThread.getLooper().quit();
            this.dispatcherThread = null;
        }
    }

    /* access modifiers changed from: 0000 */
    public void waitForThreadLooper() {
        this.dispatcherThread.getLooper();
        while (this.dispatcherThread.handlerExecuteOnDispatcherThread == null) {
            Thread.yield();
        }
    }
}
