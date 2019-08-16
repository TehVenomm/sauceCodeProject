package com.unity3d.player;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.JarURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.SocketTimeoutException;
import java.net.URL;
import java.net.URLConnection;
import java.net.UnknownHostException;
import java.nio.ByteBuffer;
import java.nio.channels.Channels;
import java.nio.channels.ReadableByteChannel;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import javax.net.ssl.HttpsURLConnection;
import javax.net.ssl.SSLHandshakeException;
import javax.net.ssl.SSLSocketFactory;

class UnityWebRequest implements Runnable {

    /* renamed from: a */
    private long f540a;

    /* renamed from: b */
    private String f541b;

    /* renamed from: c */
    private String f542c;

    /* renamed from: d */
    private Map f543d;

    /* renamed from: e */
    private int f544e;

    /* renamed from: f */
    private long f545f;

    UnityWebRequest(long j, String str, Map map, String str2, int i) {
        this.f540a = j;
        this.f541b = str2;
        this.f542c = str;
        this.f543d = map;
        this.f544e = i;
    }

    private static native void contentLengthCallback(long j, int i);

    private static native boolean downloadCallback(long j, ByteBuffer byteBuffer, int i);

    private static native void errorCallback(long j, int i, String str);

    private boolean hasTimedOut() {
        return this.f544e > 0 && System.currentTimeMillis() - this.f545f >= ((long) this.f544e);
    }

    private static native void headerCallback(long j, String str, String str2);

    private static native void responseCodeCallback(long j, int i);

    private static native int uploadCallback(long j, ByteBuffer byteBuffer);

    /* access modifiers changed from: protected */
    public void badProtocolCallback(String str) {
        errorCallback(this.f540a, 4, str);
    }

    /* access modifiers changed from: protected */
    public void contentLengthCallback(int i) {
        contentLengthCallback(this.f540a, i);
    }

    /* access modifiers changed from: protected */
    public boolean downloadCallback(ByteBuffer byteBuffer, int i) {
        return downloadCallback(this.f540a, byteBuffer, i);
    }

    /* access modifiers changed from: protected */
    public void errorCallback(String str) {
        errorCallback(this.f540a, 2, str);
    }

    /* access modifiers changed from: protected */
    public void headerCallback(String str, String str2) {
        headerCallback(this.f540a, str, str2);
    }

    /* access modifiers changed from: protected */
    public void headerCallback(Map map) {
        if (map != null && map.size() != 0) {
            for (Entry entry : map.entrySet()) {
                String str = (String) entry.getKey();
                if (str == null) {
                    str = "Status";
                }
                for (String headerCallback : (List) entry.getValue()) {
                    headerCallback(str, headerCallback);
                }
            }
        }
    }

    /* access modifiers changed from: protected */
    public void malformattedUrlCallback(String str) {
        errorCallback(this.f540a, 5, str);
    }

    /* access modifiers changed from: protected */
    public void responseCodeCallback(int i) {
        responseCodeCallback(this.f540a, i);
    }

    public void run() {
        InputStream inputStream;
        this.f545f = System.currentTimeMillis();
        try {
            URL url = new URL(this.f541b);
            URLConnection openConnection = url.openConnection();
            openConnection.setConnectTimeout(this.f544e);
            openConnection.setReadTimeout(this.f544e);
            if (openConnection instanceof HttpsURLConnection) {
                SSLSocketFactory a = C1095a.m511a();
                if (a != null) {
                    ((HttpsURLConnection) openConnection).setSSLSocketFactory(a);
                }
            }
            if (url.getProtocol().equalsIgnoreCase("file") && !url.getHost().isEmpty()) {
                malformattedUrlCallback("file:// must use an absolute path");
            } else if (openConnection instanceof JarURLConnection) {
                badProtocolCallback("A URL Connection to a Java ARchive (JAR) file or an entry in a JAR file is not supported");
            } else {
                if (openConnection instanceof HttpURLConnection) {
                    try {
                        HttpURLConnection httpURLConnection = (HttpURLConnection) openConnection;
                        httpURLConnection.setRequestMethod(this.f542c);
                        httpURLConnection.setInstanceFollowRedirects(false);
                    } catch (ProtocolException e) {
                        badProtocolCallback(e.toString());
                        return;
                    }
                }
                if (this.f543d != null) {
                    for (Entry entry : this.f543d.entrySet()) {
                        openConnection.addRequestProperty((String) entry.getKey(), (String) entry.getValue());
                    }
                }
                ByteBuffer allocateDirect = ByteBuffer.allocateDirect(131072);
                if (uploadCallback(null) > 0) {
                    openConnection.setDoOutput(true);
                    try {
                        OutputStream outputStream = openConnection.getOutputStream();
                        int uploadCallback = uploadCallback(allocateDirect);
                        while (uploadCallback > 0) {
                            if (hasTimedOut()) {
                                outputStream.close();
                                errorCallback(this.f540a, 14, "WebRequest timed out.");
                                return;
                            }
                            outputStream.write(allocateDirect.array(), allocateDirect.arrayOffset(), uploadCallback);
                            uploadCallback = uploadCallback(allocateDirect);
                        }
                    } catch (Exception e2) {
                        errorCallback(e2.toString());
                        return;
                    }
                }
                if (openConnection instanceof HttpURLConnection) {
                    try {
                        responseCodeCallback(((HttpURLConnection) openConnection).getResponseCode());
                    } catch (UnknownHostException e3) {
                        unknownHostCallback(e3.toString());
                    } catch (SocketTimeoutException e4) {
                        errorCallback(this.f540a, 14, e4.toString());
                        return;
                    } catch (IOException e5) {
                        errorCallback(e5.toString());
                        return;
                    }
                }
                Map headerFields = openConnection.getHeaderFields();
                headerCallback(headerFields);
                if ((headerFields == null || !headerFields.containsKey("content-length")) && openConnection.getContentLength() != -1) {
                    headerCallback("content-length", String.valueOf(openConnection.getContentLength()));
                }
                if ((headerFields == null || !headerFields.containsKey("content-type")) && openConnection.getContentType() != null) {
                    headerCallback("content-type", openConnection.getContentType());
                }
                contentLengthCallback(openConnection.getContentLength());
                try {
                    if (openConnection instanceof HttpURLConnection) {
                        HttpURLConnection httpURLConnection2 = (HttpURLConnection) openConnection;
                        responseCodeCallback(httpURLConnection2.getResponseCode());
                        inputStream = httpURLConnection2.getErrorStream();
                    } else {
                        inputStream = null;
                    }
                    if (inputStream == null) {
                        inputStream = openConnection.getInputStream();
                    }
                    ReadableByteChannel newChannel = Channels.newChannel(inputStream);
                    int read = newChannel.read(allocateDirect);
                    while (read != -1) {
                        if (!hasTimedOut()) {
                            if (!downloadCallback(allocateDirect, read)) {
                                break;
                            }
                            allocateDirect.clear();
                            read = newChannel.read(allocateDirect);
                        } else {
                            newChannel.close();
                            errorCallback(this.f540a, 14, "WebRequest timed out.");
                            return;
                        }
                    }
                    newChannel.close();
                } catch (UnknownHostException e6) {
                    unknownHostCallback(e6.toString());
                } catch (SSLHandshakeException e7) {
                    sslCannotConnectCallback(e7.toString());
                } catch (SocketTimeoutException e8) {
                    errorCallback(this.f540a, 14, e8.toString());
                } catch (IOException e9) {
                    errorCallback(this.f540a, 14, e9.toString());
                } catch (Exception e10) {
                    errorCallback(e10.toString());
                }
            }
        } catch (MalformedURLException e11) {
            malformattedUrlCallback(e11.toString());
        } catch (IOException e12) {
            errorCallback(e12.toString());
        }
    }

    /* access modifiers changed from: protected */
    public void sslCannotConnectCallback(String str) {
        errorCallback(this.f540a, 16, str);
    }

    /* access modifiers changed from: protected */
    public void unknownHostCallback(String str) {
        errorCallback(this.f540a, 7, str);
    }

    /* access modifiers changed from: protected */
    public int uploadCallback(ByteBuffer byteBuffer) {
        return uploadCallback(this.f540a, byteBuffer);
    }
}
