package p017io.fabric.sdk.android.services.network;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.BufferedReader;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.Closeable;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.Flushable;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.PrintStream;
import java.io.Reader;
import java.io.UnsupportedEncodingException;
import java.io.Writer;
import java.net.HttpURLConnection;
import java.net.InetSocketAddress;
import java.net.MalformedURLException;
import java.net.Proxy;
import java.net.Proxy.Type;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.URL;
import java.net.URLEncoder;
import java.nio.ByteBuffer;
import java.nio.CharBuffer;
import java.nio.charset.Charset;
import java.nio.charset.CharsetEncoder;
import java.security.AccessController;
import java.security.PrivilegedAction;
import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Map.Entry;
import java.util.concurrent.Callable;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.concurrent.atomic.AtomicReference;
import java.util.zip.GZIPInputStream;

/* renamed from: io.fabric.sdk.android.services.network.HttpRequest */
public class HttpRequest {
    private static final String BOUNDARY = "00content0boundary00";
    public static final String CHARSET_UTF8 = "UTF-8";
    private static ConnectionFactory CONNECTION_FACTORY = ConnectionFactory.DEFAULT;
    public static final String CONTENT_TYPE_FORM = "application/x-www-form-urlencoded";
    public static final String CONTENT_TYPE_JSON = "application/json";
    private static final String CONTENT_TYPE_MULTIPART = "multipart/form-data; boundary=00content0boundary00";
    private static final String CRLF = "\r\n";
    private static final String[] EMPTY_STRINGS = new String[0];
    public static final String ENCODING_GZIP = "gzip";
    public static final String HEADER_ACCEPT = "Accept";
    public static final String HEADER_ACCEPT_CHARSET = "Accept-Charset";
    public static final String HEADER_ACCEPT_ENCODING = "Accept-Encoding";
    public static final String HEADER_AUTHORIZATION = "Authorization";
    public static final String HEADER_CACHE_CONTROL = "Cache-Control";
    public static final String HEADER_CONTENT_ENCODING = "Content-Encoding";
    public static final String HEADER_CONTENT_LENGTH = "Content-Length";
    public static final String HEADER_CONTENT_TYPE = "Content-Type";
    public static final String HEADER_DATE = "Date";
    public static final String HEADER_ETAG = "ETag";
    public static final String HEADER_EXPIRES = "Expires";
    public static final String HEADER_IF_NONE_MATCH = "If-None-Match";
    public static final String HEADER_LAST_MODIFIED = "Last-Modified";
    public static final String HEADER_LOCATION = "Location";
    public static final String HEADER_PROXY_AUTHORIZATION = "Proxy-Authorization";
    public static final String HEADER_REFERER = "Referer";
    public static final String HEADER_SERVER = "Server";
    public static final String HEADER_USER_AGENT = "User-Agent";
    public static final String METHOD_DELETE = "DELETE";
    public static final String METHOD_GET = "GET";
    public static final String METHOD_HEAD = "HEAD";
    public static final String METHOD_OPTIONS = "OPTIONS";
    public static final String METHOD_POST = "POST";
    public static final String METHOD_PUT = "PUT";
    public static final String METHOD_TRACE = "TRACE";
    public static final String PARAM_CHARSET = "charset";
    /* access modifiers changed from: private */
    public int bufferSize = 8192;
    private HttpURLConnection connection = null;
    private boolean form;
    private String httpProxyHost;
    private int httpProxyPort;
    private boolean ignoreCloseExceptions = true;
    private boolean multipart;
    private RequestOutputStream output;
    private final String requestMethod;
    private boolean uncompress = false;
    public final URL url;

    /* renamed from: io.fabric.sdk.android.services.network.HttpRequest$Base64 */
    public static class Base64 {
        private static final byte EQUALS_SIGN = 61;
        private static final String PREFERRED_ENCODING = "US-ASCII";
        private static final byte[] _STANDARD_ALPHABET = {65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 43, 47};

        private Base64() {
        }

        public static String encode(String str) {
            byte[] bytes;
            try {
                bytes = str.getBytes("US-ASCII");
            } catch (UnsupportedEncodingException e) {
                bytes = str.getBytes();
            }
            return encodeBytes(bytes);
        }

        private static byte[] encode3to4(byte[] bArr, int i, int i2, byte[] bArr2, int i3) {
            int i4 = 0;
            byte[] bArr3 = _STANDARD_ALPHABET;
            int i5 = i2 > 0 ? (bArr[i] << 24) >>> 8 : 0;
            int i6 = i2 > 1 ? (bArr[i + 1] << 24) >>> 16 : 0;
            if (i2 > 2) {
                i4 = (bArr[i + 2] << 24) >>> 24;
            }
            int i7 = i4 | i6 | i5;
            switch (i2) {
                case 1:
                    bArr2[i3] = (byte) bArr3[i7 >>> 18];
                    bArr2[i3 + 1] = (byte) bArr3[(i7 >>> 12) & 63];
                    bArr2[i3 + 2] = (byte) 61;
                    bArr2[i3 + 3] = (byte) 61;
                    break;
                case 2:
                    bArr2[i3] = (byte) bArr3[i7 >>> 18];
                    bArr2[i3 + 1] = (byte) bArr3[(i7 >>> 12) & 63];
                    bArr2[i3 + 2] = (byte) bArr3[(i7 >>> 6) & 63];
                    bArr2[i3 + 3] = (byte) 61;
                    break;
                case 3:
                    bArr2[i3] = (byte) bArr3[i7 >>> 18];
                    bArr2[i3 + 1] = (byte) bArr3[(i7 >>> 12) & 63];
                    bArr2[i3 + 2] = (byte) bArr3[(i7 >>> 6) & 63];
                    bArr2[i3 + 3] = (byte) bArr3[i7 & 63];
                    break;
            }
            return bArr2;
        }

        public static String encodeBytes(byte[] bArr) {
            return encodeBytes(bArr, 0, bArr.length);
        }

        public static String encodeBytes(byte[] bArr, int i, int i2) {
            byte[] encodeBytesToBytes = encodeBytesToBytes(bArr, i, i2);
            try {
                return new String(encodeBytesToBytes, "US-ASCII");
            } catch (UnsupportedEncodingException e) {
                return new String(encodeBytesToBytes);
            }
        }

        public static byte[] encodeBytesToBytes(byte[] bArr, int i, int i2) {
            if (bArr == null) {
                throw new NullPointerException("Cannot serialize a null array.");
            } else if (i < 0) {
                throw new IllegalArgumentException("Cannot have negative offset: " + i);
            } else if (i2 < 0) {
                throw new IllegalArgumentException("Cannot have length offset: " + i2);
            } else if (i + i2 > bArr.length) {
                throw new IllegalArgumentException(String.format(Locale.ENGLISH, "Cannot have offset of %d and length of %d with array of length %d", new Object[]{Integer.valueOf(i), Integer.valueOf(i2), Integer.valueOf(bArr.length)}));
            } else {
                byte[] bArr2 = new byte[((i2 % 3 > 0 ? 4 : 0) + ((i2 / 3) * 4))];
                int i3 = 0;
                int i4 = 0;
                while (i3 < i2 - 2) {
                    encode3to4(bArr, i3 + i, 3, bArr2, i4);
                    i3 += 3;
                    i4 += 4;
                }
                if (i3 < i2) {
                    encode3to4(bArr, i3 + i, i2 - i3, bArr2, i4);
                    i4 += 4;
                }
                if (i4 > bArr2.length - 1) {
                    return bArr2;
                }
                byte[] bArr3 = new byte[i4];
                System.arraycopy(bArr2, 0, bArr3, 0, i4);
                return bArr3;
            }
        }
    }

    /* renamed from: io.fabric.sdk.android.services.network.HttpRequest$CloseOperation */
    protected static abstract class CloseOperation<V> extends Operation<V> {
        private final Closeable closeable;
        private final boolean ignoreCloseExceptions;

        protected CloseOperation(Closeable closeable2, boolean z) {
            this.closeable = closeable2;
            this.ignoreCloseExceptions = z;
        }

        /* access modifiers changed from: protected */
        public void done() throws IOException {
            if (this.closeable instanceof Flushable) {
                ((Flushable) this.closeable).flush();
            }
            if (this.ignoreCloseExceptions) {
                try {
                    this.closeable.close();
                } catch (IOException e) {
                }
            } else {
                this.closeable.close();
            }
        }
    }

    /* renamed from: io.fabric.sdk.android.services.network.HttpRequest$ConnectionFactory */
    public interface ConnectionFactory {
        public static final ConnectionFactory DEFAULT = new ConnectionFactory() {
            public HttpURLConnection create(URL url) throws IOException {
                return (HttpURLConnection) url.openConnection();
            }

            public HttpURLConnection create(URL url, Proxy proxy) throws IOException {
                return (HttpURLConnection) url.openConnection(proxy);
            }
        };

        HttpURLConnection create(URL url) throws IOException;

        HttpURLConnection create(URL url, Proxy proxy) throws IOException;
    }

    /* renamed from: io.fabric.sdk.android.services.network.HttpRequest$FlushOperation */
    protected static abstract class FlushOperation<V> extends Operation<V> {
        private final Flushable flushable;

        protected FlushOperation(Flushable flushable2) {
            this.flushable = flushable2;
        }

        /* access modifiers changed from: protected */
        public void done() throws IOException {
            this.flushable.flush();
        }
    }

    /* renamed from: io.fabric.sdk.android.services.network.HttpRequest$HttpRequestException */
    public static class HttpRequestException extends RuntimeException {
        private static final long serialVersionUID = -1170466989781746231L;

        protected HttpRequestException(IOException iOException) {
            super(iOException);
        }

        public IOException getCause() {
            return (IOException) super.getCause();
        }
    }

    /* renamed from: io.fabric.sdk.android.services.network.HttpRequest$Operation */
    protected static abstract class Operation<V> implements Callable<V> {
        protected Operation() {
        }

        public V call() throws HttpRequestException {
            boolean z = true;
            try {
                V run = run();
                try {
                    done();
                    return run;
                } catch (IOException e) {
                    throw new HttpRequestException(e);
                }
            } catch (HttpRequestException e2) {
                throw e2;
            } catch (IOException e3) {
                throw new HttpRequestException(e3);
            } catch (Throwable th) {
                th = th;
            }
            try {
                done();
            } catch (IOException e4) {
                if (!z) {
                    throw new HttpRequestException(e4);
                }
            }
            throw th;
        }

        /* access modifiers changed from: protected */
        public abstract void done() throws IOException;

        /* access modifiers changed from: protected */
        public abstract V run() throws HttpRequestException, IOException;
    }

    /* renamed from: io.fabric.sdk.android.services.network.HttpRequest$RequestOutputStream */
    public static class RequestOutputStream extends BufferedOutputStream {
        /* access modifiers changed from: private */
        public final CharsetEncoder encoder;

        public RequestOutputStream(OutputStream outputStream, String str, int i) {
            super(outputStream, i);
            this.encoder = Charset.forName(HttpRequest.getValidCharset(str)).newEncoder();
        }

        public RequestOutputStream write(String str) throws IOException {
            ByteBuffer encode = this.encoder.encode(CharBuffer.wrap(str));
            super.write(encode.array(), 0, encode.limit());
            return this;
        }
    }

    public HttpRequest(CharSequence charSequence, String str) throws HttpRequestException {
        try {
            this.url = new URL(charSequence.toString());
            this.requestMethod = str;
        } catch (MalformedURLException e) {
            throw new HttpRequestException(e);
        }
    }

    public HttpRequest(URL url2, String str) throws HttpRequestException {
        this.url = url2;
        this.requestMethod = str;
    }

    private static StringBuilder addParamPrefix(String str, StringBuilder sb) {
        int indexOf = str.indexOf(63);
        int length = sb.length() - 1;
        if (indexOf == -1) {
            sb.append('?');
        } else if (indexOf < length && str.charAt(length) != '&') {
            sb.append('&');
        }
        return sb;
    }

    private static StringBuilder addPathSeparator(String str, StringBuilder sb) {
        if (str.indexOf(58) + 2 == str.lastIndexOf(47)) {
            sb.append('/');
        }
        return sb;
    }

    public static String append(CharSequence charSequence, Map<?, ?> map) {
        String charSequence2 = charSequence.toString();
        if (map == null || map.isEmpty()) {
            return charSequence2;
        }
        StringBuilder sb = new StringBuilder(charSequence2);
        addPathSeparator(charSequence2, sb);
        addParamPrefix(charSequence2, sb);
        Iterator it = map.entrySet().iterator();
        Entry entry = (Entry) it.next();
        sb.append(entry.getKey().toString());
        sb.append('=');
        Object value = entry.getValue();
        if (value != null) {
            sb.append(value);
        }
        while (it.hasNext()) {
            sb.append('&');
            Entry entry2 = (Entry) it.next();
            sb.append(entry2.getKey().toString());
            sb.append('=');
            Object value2 = entry2.getValue();
            if (value2 != null) {
                sb.append(value2);
            }
        }
        return sb.toString();
    }

    public static String append(CharSequence charSequence, Object... objArr) {
        String charSequence2 = charSequence.toString();
        if (objArr == null || objArr.length == 0) {
            return charSequence2;
        }
        if (objArr.length % 2 != 0) {
            throw new IllegalArgumentException("Must specify an even number of parameter names/values");
        }
        StringBuilder sb = new StringBuilder(charSequence2);
        addPathSeparator(charSequence2, sb);
        addParamPrefix(charSequence2, sb);
        sb.append(objArr[0]);
        sb.append('=');
        Object obj = objArr[1];
        if (obj != null) {
            sb.append(obj);
        }
        for (int i = 2; i < objArr.length; i += 2) {
            sb.append('&');
            sb.append(objArr[i]);
            sb.append('=');
            Object obj2 = objArr[i + 1];
            if (obj2 != null) {
                sb.append(obj2);
            }
        }
        return sb.toString();
    }

    private HttpURLConnection createConnection() {
        try {
            HttpURLConnection create = this.httpProxyHost != null ? CONNECTION_FACTORY.create(this.url, createProxy()) : CONNECTION_FACTORY.create(this.url);
            create.setRequestMethod(this.requestMethod);
            return create;
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    private Proxy createProxy() {
        return new Proxy(Type.HTTP, new InetSocketAddress(this.httpProxyHost, this.httpProxyPort));
    }

    public static HttpRequest delete(CharSequence charSequence) throws HttpRequestException {
        return new HttpRequest(charSequence, METHOD_DELETE);
    }

    public static HttpRequest delete(CharSequence charSequence, Map<?, ?> map, boolean z) {
        String append = append(charSequence, map);
        if (z) {
            append = encode(append);
        }
        return delete((CharSequence) append);
    }

    public static HttpRequest delete(CharSequence charSequence, boolean z, Object... objArr) {
        String append = append(charSequence, objArr);
        if (z) {
            append = encode(append);
        }
        return delete((CharSequence) append);
    }

    public static HttpRequest delete(URL url2) throws HttpRequestException {
        return new HttpRequest(url2, METHOD_DELETE);
    }

    public static String encode(CharSequence charSequence) throws HttpRequestException {
        try {
            URL url2 = new URL(charSequence.toString());
            String host = url2.getHost();
            int port = url2.getPort();
            if (port != -1) {
                host = host + ':' + Integer.toString(port);
            }
            try {
                String aSCIIString = new URI(url2.getProtocol(), host, url2.getPath(), url2.getQuery(), url2.getRef()).toASCIIString();
                int indexOf = aSCIIString.indexOf(63);
                return (indexOf <= 0 || indexOf + 1 >= aSCIIString.length()) ? aSCIIString : aSCIIString.substring(0, indexOf + 1) + aSCIIString.substring(indexOf + 1).replace("+", "%2B").replace("#", "%23");
            } catch (URISyntaxException e) {
                IOException iOException = new IOException("Parsing URI failed");
                iOException.initCause(e);
                throw new HttpRequestException(iOException);
            }
        } catch (IOException e2) {
            throw new HttpRequestException(e2);
        }
    }

    public static HttpRequest get(CharSequence charSequence) throws HttpRequestException {
        return new HttpRequest(charSequence, METHOD_GET);
    }

    public static HttpRequest get(CharSequence charSequence, Map<?, ?> map, boolean z) {
        String append = append(charSequence, map);
        if (z) {
            append = encode(append);
        }
        return get((CharSequence) append);
    }

    public static HttpRequest get(CharSequence charSequence, boolean z, Object... objArr) {
        String append = append(charSequence, objArr);
        if (z) {
            append = encode(append);
        }
        return get((CharSequence) append);
    }

    public static HttpRequest get(URL url2) throws HttpRequestException {
        return new HttpRequest(url2, METHOD_GET);
    }

    /* access modifiers changed from: private */
    public static String getValidCharset(String str) {
        return (str == null || str.length() <= 0) ? "UTF-8" : str;
    }

    public static HttpRequest head(CharSequence charSequence) throws HttpRequestException {
        return new HttpRequest(charSequence, METHOD_HEAD);
    }

    public static HttpRequest head(CharSequence charSequence, Map<?, ?> map, boolean z) {
        String append = append(charSequence, map);
        if (z) {
            append = encode(append);
        }
        return head((CharSequence) append);
    }

    public static HttpRequest head(CharSequence charSequence, boolean z, Object... objArr) {
        String append = append(charSequence, objArr);
        if (z) {
            append = encode(append);
        }
        return head((CharSequence) append);
    }

    public static HttpRequest head(URL url2) throws HttpRequestException {
        return new HttpRequest(url2, METHOD_HEAD);
    }

    public static void keepAlive(boolean z) {
        setProperty("http.keepAlive", Boolean.toString(z));
    }

    public static void nonProxyHosts(String... strArr) {
        if (strArr == null || strArr.length <= 0) {
            setProperty("http.nonProxyHosts", null);
            return;
        }
        StringBuilder sb = new StringBuilder();
        int length = strArr.length - 1;
        for (int i = 0; i < length; i++) {
            sb.append(strArr[i]).append('|');
        }
        sb.append(strArr[length]);
        setProperty("http.nonProxyHosts", sb.toString());
    }

    public static HttpRequest options(CharSequence charSequence) throws HttpRequestException {
        return new HttpRequest(charSequence, METHOD_OPTIONS);
    }

    public static HttpRequest options(URL url2) throws HttpRequestException {
        return new HttpRequest(url2, METHOD_OPTIONS);
    }

    public static HttpRequest post(CharSequence charSequence) throws HttpRequestException {
        return new HttpRequest(charSequence, METHOD_POST);
    }

    public static HttpRequest post(CharSequence charSequence, Map<?, ?> map, boolean z) {
        String append = append(charSequence, map);
        if (z) {
            append = encode(append);
        }
        return post((CharSequence) append);
    }

    public static HttpRequest post(CharSequence charSequence, boolean z, Object... objArr) {
        String append = append(charSequence, objArr);
        if (z) {
            append = encode(append);
        }
        return post((CharSequence) append);
    }

    public static HttpRequest post(URL url2) throws HttpRequestException {
        return new HttpRequest(url2, METHOD_POST);
    }

    public static void proxyHost(String str) {
        setProperty("http.proxyHost", str);
        setProperty("https.proxyHost", str);
    }

    public static void proxyPort(int i) {
        String num = Integer.toString(i);
        setProperty("http.proxyPort", num);
        setProperty("https.proxyPort", num);
    }

    public static HttpRequest put(CharSequence charSequence) throws HttpRequestException {
        return new HttpRequest(charSequence, METHOD_PUT);
    }

    public static HttpRequest put(CharSequence charSequence, Map<?, ?> map, boolean z) {
        String append = append(charSequence, map);
        if (z) {
            append = encode(append);
        }
        return put((CharSequence) append);
    }

    public static HttpRequest put(CharSequence charSequence, boolean z, Object... objArr) {
        String append = append(charSequence, objArr);
        if (z) {
            append = encode(append);
        }
        return put((CharSequence) append);
    }

    public static HttpRequest put(URL url2) throws HttpRequestException {
        return new HttpRequest(url2, METHOD_PUT);
    }

    public static void setConnectionFactory(ConnectionFactory connectionFactory) {
        if (connectionFactory == null) {
            CONNECTION_FACTORY = ConnectionFactory.DEFAULT;
        } else {
            CONNECTION_FACTORY = connectionFactory;
        }
    }

    private static String setProperty(final String str, final String str2) {
        return (String) AccessController.doPrivileged(str2 != null ? new PrivilegedAction<String>() {
            public String run() {
                return System.setProperty(str, str2);
            }
        } : new PrivilegedAction<String>() {
            public String run() {
                return System.clearProperty(str);
            }
        });
    }

    public static HttpRequest trace(CharSequence charSequence) throws HttpRequestException {
        return new HttpRequest(charSequence, METHOD_TRACE);
    }

    public static HttpRequest trace(URL url2) throws HttpRequestException {
        return new HttpRequest(url2, METHOD_TRACE);
    }

    public HttpRequest accept(String str) {
        return header("Accept", str);
    }

    public HttpRequest acceptCharset(String str) {
        return header(HEADER_ACCEPT_CHARSET, str);
    }

    public HttpRequest acceptEncoding(String str) {
        return header(HEADER_ACCEPT_ENCODING, str);
    }

    public HttpRequest acceptGzipEncoding() {
        return acceptEncoding(ENCODING_GZIP);
    }

    public HttpRequest acceptJson() {
        return accept("application/json");
    }

    public HttpRequest authorization(String str) {
        return header(HEADER_AUTHORIZATION, str);
    }

    public boolean badRequest() throws HttpRequestException {
        return 400 == code();
    }

    public HttpRequest basic(String str, String str2) {
        return authorization("Basic " + Base64.encode(str + ':' + str2));
    }

    public HttpRequest body(AtomicReference<String> atomicReference) throws HttpRequestException {
        atomicReference.set(body());
        return this;
    }

    public HttpRequest body(AtomicReference<String> atomicReference, String str) throws HttpRequestException {
        atomicReference.set(body(str));
        return this;
    }

    public String body() throws HttpRequestException {
        return body(charset());
    }

    public String body(String str) throws HttpRequestException {
        ByteArrayOutputStream byteStream = byteStream();
        try {
            copy((InputStream) buffer(), (OutputStream) byteStream);
            return byteStream.toString(getValidCharset(str));
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    public BufferedInputStream buffer() throws HttpRequestException {
        return new BufferedInputStream(stream(), this.bufferSize);
    }

    public int bufferSize() {
        return this.bufferSize;
    }

    public HttpRequest bufferSize(int i) {
        if (i < 1) {
            throw new IllegalArgumentException("Size must be greater than zero");
        }
        this.bufferSize = i;
        return this;
    }

    public BufferedReader bufferedReader() throws HttpRequestException {
        return bufferedReader(charset());
    }

    public BufferedReader bufferedReader(String str) throws HttpRequestException {
        return new BufferedReader(reader(str), this.bufferSize);
    }

    /* access modifiers changed from: protected */
    public ByteArrayOutputStream byteStream() {
        int contentLength = contentLength();
        return contentLength > 0 ? new ByteArrayOutputStream(contentLength) : new ByteArrayOutputStream();
    }

    public byte[] bytes() throws HttpRequestException {
        ByteArrayOutputStream byteStream = byteStream();
        try {
            copy((InputStream) buffer(), (OutputStream) byteStream);
            return byteStream.toByteArray();
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    public String cacheControl() {
        return header(HEADER_CACHE_CONTROL);
    }

    public String charset() {
        return parameter(HEADER_CONTENT_TYPE, PARAM_CHARSET);
    }

    public HttpRequest chunk(int i) {
        getConnection().setChunkedStreamingMode(i);
        return this;
    }

    /* access modifiers changed from: protected */
    public HttpRequest closeOutput() throws IOException {
        if (this.output != null) {
            if (this.multipart) {
                this.output.write("\r\n--00content0boundary00--\r\n");
            }
            if (this.ignoreCloseExceptions) {
                try {
                    this.output.close();
                } catch (IOException e) {
                }
            } else {
                this.output.close();
            }
            this.output = null;
        }
        return this;
    }

    /* access modifiers changed from: protected */
    public HttpRequest closeOutputQuietly() throws HttpRequestException {
        try {
            return closeOutput();
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    public int code() throws HttpRequestException {
        try {
            closeOutput();
            return getConnection().getResponseCode();
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    public HttpRequest code(AtomicInteger atomicInteger) throws HttpRequestException {
        atomicInteger.set(code());
        return this;
    }

    public HttpRequest connectTimeout(int i) {
        getConnection().setConnectTimeout(i);
        return this;
    }

    public String contentEncoding() {
        return header(HEADER_CONTENT_ENCODING);
    }

    public int contentLength() {
        return intHeader(HEADER_CONTENT_LENGTH);
    }

    public HttpRequest contentLength(int i) {
        getConnection().setFixedLengthStreamingMode(i);
        return this;
    }

    public HttpRequest contentLength(String str) {
        return contentLength(Integer.parseInt(str));
    }

    public HttpRequest contentType(String str) {
        return contentType(str, null);
    }

    public HttpRequest contentType(String str, String str2) {
        return (str2 == null || str2.length() <= 0) ? header(HEADER_CONTENT_TYPE, str) : header(HEADER_CONTENT_TYPE, str + "; charset=" + str2);
    }

    public String contentType() {
        return header(HEADER_CONTENT_TYPE);
    }

    /* access modifiers changed from: protected */
    public HttpRequest copy(InputStream inputStream, OutputStream outputStream) throws IOException {
        final InputStream inputStream2 = inputStream;
        final OutputStream outputStream2 = outputStream;
        return (HttpRequest) new CloseOperation<HttpRequest>(inputStream, this.ignoreCloseExceptions) {
            public HttpRequest run() throws IOException {
                byte[] bArr = new byte[HttpRequest.this.bufferSize];
                while (true) {
                    int read = inputStream2.read(bArr);
                    if (read == -1) {
                        return HttpRequest.this;
                    }
                    outputStream2.write(bArr, 0, read);
                }
            }
        }.call();
    }

    /* access modifiers changed from: protected */
    public HttpRequest copy(Reader reader, Writer writer) throws IOException {
        final Reader reader2 = reader;
        final Writer writer2 = writer;
        return (HttpRequest) new CloseOperation<HttpRequest>(reader, this.ignoreCloseExceptions) {
            public HttpRequest run() throws IOException {
                char[] cArr = new char[HttpRequest.this.bufferSize];
                while (true) {
                    int read = reader2.read(cArr);
                    if (read == -1) {
                        return HttpRequest.this;
                    }
                    writer2.write(cArr, 0, read);
                }
            }
        }.call();
    }

    public boolean created() throws HttpRequestException {
        return 201 == code();
    }

    public long date() {
        return dateHeader(HEADER_DATE);
    }

    public long dateHeader(String str) throws HttpRequestException {
        return dateHeader(str, -1);
    }

    public long dateHeader(String str, long j) throws HttpRequestException {
        closeOutputQuietly();
        return getConnection().getHeaderFieldDate(str, j);
    }

    public HttpRequest disconnect() {
        getConnection().disconnect();
        return this;
    }

    public String eTag() {
        return header(HEADER_ETAG);
    }

    public long expires() {
        return dateHeader(HEADER_EXPIRES);
    }

    public HttpRequest followRedirects(boolean z) {
        getConnection().setInstanceFollowRedirects(z);
        return this;
    }

    public HttpRequest form(Object obj, Object obj2) throws HttpRequestException {
        return form(obj, obj2, "UTF-8");
    }

    public HttpRequest form(Object obj, Object obj2, String str) throws HttpRequestException {
        boolean z = !this.form;
        if (z) {
            contentType(CONTENT_TYPE_FORM, str);
            this.form = true;
        }
        String validCharset = getValidCharset(str);
        try {
            openOutput();
            if (!z) {
                this.output.write(38);
            }
            this.output.write(URLEncoder.encode(obj.toString(), validCharset));
            this.output.write(61);
            if (obj2 != null) {
                this.output.write(URLEncoder.encode(obj2.toString(), validCharset));
            }
            return this;
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    public HttpRequest form(Entry<?, ?> entry) throws HttpRequestException {
        return form(entry, "UTF-8");
    }

    public HttpRequest form(Entry<?, ?> entry, String str) throws HttpRequestException {
        return form(entry.getKey(), entry.getValue(), str);
    }

    public HttpRequest form(Map<?, ?> map) throws HttpRequestException {
        return form(map, "UTF-8");
    }

    public HttpRequest form(Map<?, ?> map, String str) throws HttpRequestException {
        if (!map.isEmpty()) {
            for (Entry form2 : map.entrySet()) {
                form(form2, str);
            }
        }
        return this;
    }

    public HttpURLConnection getConnection() {
        if (this.connection == null) {
            this.connection = createConnection();
        }
        return this.connection;
    }

    /* access modifiers changed from: protected */
    public String getParam(String str, String str2) {
        int i;
        if (str == null || str.length() == 0) {
            return null;
        }
        int length = str.length();
        int indexOf = str.indexOf(59) + 1;
        if (indexOf == 0 || indexOf == length) {
            return null;
        }
        int indexOf2 = str.indexOf(59, indexOf);
        if (indexOf2 == -1) {
            indexOf2 = length;
        }
        while (indexOf < i) {
            int indexOf3 = str.indexOf(61, indexOf);
            if (indexOf3 != -1 && indexOf3 < i && str2.equals(str.substring(indexOf, indexOf3).trim())) {
                String trim = str.substring(indexOf3 + 1, i).trim();
                int length2 = trim.length();
                if (length2 != 0) {
                    return (length2 > 2 && '\"' == trim.charAt(0) && '\"' == trim.charAt(length2 + -1)) ? trim.substring(1, length2 - 1) : trim;
                }
            }
            indexOf = i + 1;
            i = str.indexOf(59, indexOf);
            if (i == -1) {
                i = length;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public Map<String, String> getParams(String str) {
        if (str == null || str.length() == 0) {
            return Collections.emptyMap();
        }
        int length = str.length();
        int indexOf = str.indexOf(59) + 1;
        if (indexOf == 0 || indexOf == length) {
            return Collections.emptyMap();
        }
        int indexOf2 = str.indexOf(59, indexOf);
        if (indexOf2 == -1) {
            indexOf2 = length;
        }
        LinkedHashMap linkedHashMap = new LinkedHashMap();
        int i = indexOf2;
        while (indexOf < i) {
            int indexOf3 = str.indexOf(61, indexOf);
            if (indexOf3 != -1 && indexOf3 < i) {
                String trim = str.substring(indexOf, indexOf3).trim();
                if (trim.length() > 0) {
                    String trim2 = str.substring(indexOf3 + 1, i).trim();
                    int length2 = trim2.length();
                    if (length2 != 0) {
                        if (length2 > 2 && '\"' == trim2.charAt(0) && '\"' == trim2.charAt(length2 - 1)) {
                            linkedHashMap.put(trim, trim2.substring(1, length2 - 1));
                        } else {
                            linkedHashMap.put(trim, trim2);
                        }
                    }
                }
            }
            int i2 = i + 1;
            i = str.indexOf(59, i2);
            if (i == -1) {
                indexOf = i2;
                i = length;
            } else {
                indexOf = i2;
            }
        }
        return linkedHashMap;
    }

    public HttpRequest header(String str, Number number) {
        return header(str, number != null ? number.toString() : null);
    }

    public HttpRequest header(String str, String str2) {
        getConnection().setRequestProperty(str, str2);
        return this;
    }

    public HttpRequest header(Entry<String, String> entry) {
        return header((String) entry.getKey(), (String) entry.getValue());
    }

    public String header(String str) throws HttpRequestException {
        closeOutputQuietly();
        return getConnection().getHeaderField(str);
    }

    public HttpRequest headers(Map<String, String> map) {
        if (!map.isEmpty()) {
            for (Entry header : map.entrySet()) {
                header(header);
            }
        }
        return this;
    }

    public Map<String, List<String>> headers() throws HttpRequestException {
        closeOutputQuietly();
        return getConnection().getHeaderFields();
    }

    public String[] headers(String str) {
        Map headers = headers();
        if (headers == null || headers.isEmpty()) {
            return EMPTY_STRINGS;
        }
        List list = (List) headers.get(str);
        return (list == null || list.isEmpty()) ? EMPTY_STRINGS : (String[]) list.toArray(new String[list.size()]);
    }

    public HttpRequest ifModifiedSince(long j) {
        getConnection().setIfModifiedSince(j);
        return this;
    }

    public HttpRequest ifNoneMatch(String str) {
        return header(HEADER_IF_NONE_MATCH, str);
    }

    public HttpRequest ignoreCloseExceptions(boolean z) {
        this.ignoreCloseExceptions = z;
        return this;
    }

    public boolean ignoreCloseExceptions() {
        return this.ignoreCloseExceptions;
    }

    public int intHeader(String str) throws HttpRequestException {
        return intHeader(str, -1);
    }

    public int intHeader(String str, int i) throws HttpRequestException {
        closeOutputQuietly();
        return getConnection().getHeaderFieldInt(str, i);
    }

    public boolean isBodyEmpty() throws HttpRequestException {
        return contentLength() == 0;
    }

    public long lastModified() {
        return dateHeader(HEADER_LAST_MODIFIED);
    }

    public String location() {
        return header("Location");
    }

    public String message() throws HttpRequestException {
        try {
            closeOutput();
            return getConnection().getResponseMessage();
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    public String method() {
        return getConnection().getRequestMethod();
    }

    public boolean notFound() throws HttpRequestException {
        return 404 == code();
    }

    public boolean notModified() throws HttpRequestException {
        return 304 == code();
    }

    /* renamed from: ok */
    public boolean mo22482ok() throws HttpRequestException {
        return 200 == code();
    }

    /* access modifiers changed from: protected */
    public HttpRequest openOutput() throws IOException {
        if (this.output == null) {
            getConnection().setDoOutput(true);
            this.output = new RequestOutputStream(getConnection().getOutputStream(), getParam(getConnection().getRequestProperty(HEADER_CONTENT_TYPE), PARAM_CHARSET), this.bufferSize);
        }
        return this;
    }

    public String parameter(String str, String str2) {
        return getParam(header(str), str2);
    }

    public Map<String, String> parameters(String str) {
        return getParams(header(str));
    }

    public HttpRequest part(String str, File file) throws HttpRequestException {
        return part(str, (String) null, file);
    }

    public HttpRequest part(String str, InputStream inputStream) throws HttpRequestException {
        return part(str, (String) null, (String) null, inputStream);
    }

    public HttpRequest part(String str, Number number) throws HttpRequestException {
        return part(str, (String) null, number);
    }

    public HttpRequest part(String str, String str2) {
        return part(str, (String) null, str2);
    }

    public HttpRequest part(String str, String str2, File file) throws HttpRequestException {
        return part(str, str2, (String) null, file);
    }

    public HttpRequest part(String str, String str2, Number number) throws HttpRequestException {
        return part(str, str2, number != null ? number.toString() : null);
    }

    public HttpRequest part(String str, String str2, String str3) throws HttpRequestException {
        return part(str, str2, (String) null, str3);
    }

    /* JADX WARNING: Removed duplicated region for block: B:18:0x0022 A[SYNTHETIC, Splitter:B:18:0x0022] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public p017io.fabric.sdk.android.services.network.HttpRequest part(java.lang.String r4, java.lang.String r5, java.lang.String r6, java.io.File r7) throws p017io.fabric.sdk.android.services.network.HttpRequest.HttpRequestException {
        /*
            r3 = this;
            r2 = 0
            java.io.BufferedInputStream r1 = new java.io.BufferedInputStream     // Catch:{ IOException -> 0x0015, all -> 0x002e }
            java.io.FileInputStream r0 = new java.io.FileInputStream     // Catch:{ IOException -> 0x0015, all -> 0x002e }
            r0.<init>(r7)     // Catch:{ IOException -> 0x0015, all -> 0x002e }
            r1.<init>(r0)     // Catch:{ IOException -> 0x0015, all -> 0x002e }
            io.fabric.sdk.android.services.network.HttpRequest r0 = r3.part(r4, r5, r6, r1)     // Catch:{ IOException -> 0x0026, all -> 0x0028 }
            if (r1 == 0) goto L_0x0014
            r1.close()     // Catch:{ IOException -> 0x002a }
        L_0x0014:
            return r0
        L_0x0015:
            r0 = move-exception
            r1 = r2
        L_0x0017:
            io.fabric.sdk.android.services.network.HttpRequest$HttpRequestException r2 = new io.fabric.sdk.android.services.network.HttpRequest$HttpRequestException     // Catch:{ all -> 0x001d }
            r2.<init>(r0)     // Catch:{ all -> 0x001d }
            throw r2     // Catch:{ all -> 0x001d }
        L_0x001d:
            r0 = move-exception
            r2 = r1
        L_0x001f:
            r1 = r2
        L_0x0020:
            if (r1 == 0) goto L_0x0025
            r1.close()     // Catch:{ IOException -> 0x002c }
        L_0x0025:
            throw r0
        L_0x0026:
            r0 = move-exception
            goto L_0x0017
        L_0x0028:
            r0 = move-exception
            goto L_0x0020
        L_0x002a:
            r1 = move-exception
            goto L_0x0014
        L_0x002c:
            r1 = move-exception
            goto L_0x0025
        L_0x002e:
            r0 = move-exception
            goto L_0x001f
        */
        throw new UnsupportedOperationException("Method not decompiled: p017io.fabric.sdk.android.services.network.HttpRequest.part(java.lang.String, java.lang.String, java.lang.String, java.io.File):io.fabric.sdk.android.services.network.HttpRequest");
    }

    public HttpRequest part(String str, String str2, String str3, InputStream inputStream) throws HttpRequestException {
        try {
            startPart();
            writePartHeader(str, str2, str3);
            copy(inputStream, (OutputStream) this.output);
            return this;
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    public HttpRequest part(String str, String str2, String str3, String str4) throws HttpRequestException {
        try {
            startPart();
            writePartHeader(str, str2, str3);
            this.output.write(str4);
            return this;
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    public HttpRequest partHeader(String str, String str2) throws HttpRequestException {
        return send((CharSequence) str).send((CharSequence) ": ").send((CharSequence) str2).send((CharSequence) CRLF);
    }

    public HttpRequest proxyAuthorization(String str) {
        return header(HEADER_PROXY_AUTHORIZATION, str);
    }

    public HttpRequest proxyBasic(String str, String str2) {
        return proxyAuthorization("Basic " + Base64.encode(str + ':' + str2));
    }

    public HttpRequest readTimeout(int i) {
        getConnection().setReadTimeout(i);
        return this;
    }

    public InputStreamReader reader() throws HttpRequestException {
        return reader(charset());
    }

    public InputStreamReader reader(String str) throws HttpRequestException {
        try {
            return new InputStreamReader(stream(), getValidCharset(str));
        } catch (UnsupportedEncodingException e) {
            throw new HttpRequestException(e);
        }
    }

    public HttpRequest receive(File file) throws HttpRequestException {
        try {
            final BufferedOutputStream bufferedOutputStream = new BufferedOutputStream(new FileOutputStream(file), this.bufferSize);
            return (HttpRequest) new CloseOperation<HttpRequest>(this.ignoreCloseExceptions, bufferedOutputStream) {
                /* access modifiers changed from: protected */
                public HttpRequest run() throws HttpRequestException, IOException {
                    return HttpRequest.this.receive(bufferedOutputStream);
                }
            }.call();
        } catch (FileNotFoundException e) {
            throw new HttpRequestException(e);
        }
    }

    public HttpRequest receive(OutputStream outputStream) throws HttpRequestException {
        try {
            return copy((InputStream) buffer(), outputStream);
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    public HttpRequest receive(PrintStream printStream) throws HttpRequestException {
        return receive((OutputStream) printStream);
    }

    public HttpRequest receive(Writer writer) throws HttpRequestException {
        BufferedReader bufferedReader = bufferedReader();
        final BufferedReader bufferedReader2 = bufferedReader;
        final Writer writer2 = writer;
        return (HttpRequest) new CloseOperation<HttpRequest>(bufferedReader, this.ignoreCloseExceptions) {
            public HttpRequest run() throws IOException {
                return HttpRequest.this.copy((Reader) bufferedReader2, writer2);
            }
        }.call();
    }

    public HttpRequest receive(Appendable appendable) throws HttpRequestException {
        BufferedReader bufferedReader = bufferedReader();
        final BufferedReader bufferedReader2 = bufferedReader;
        final Appendable appendable2 = appendable;
        return (HttpRequest) new CloseOperation<HttpRequest>(bufferedReader, this.ignoreCloseExceptions) {
            public HttpRequest run() throws IOException {
                CharBuffer allocate = CharBuffer.allocate(HttpRequest.this.bufferSize);
                while (true) {
                    int read = bufferedReader2.read(allocate);
                    if (read == -1) {
                        return HttpRequest.this;
                    }
                    allocate.rewind();
                    appendable2.append(allocate, 0, read);
                    allocate.rewind();
                }
            }
        }.call();
    }

    public HttpRequest referer(String str) {
        return header(HEADER_REFERER, str);
    }

    public HttpRequest send(File file) throws HttpRequestException {
        try {
            return send((InputStream) new BufferedInputStream(new FileInputStream(file)));
        } catch (FileNotFoundException e) {
            throw new HttpRequestException(e);
        }
    }

    public HttpRequest send(InputStream inputStream) throws HttpRequestException {
        try {
            openOutput();
            copy(inputStream, (OutputStream) this.output);
            return this;
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    public HttpRequest send(final Reader reader) throws HttpRequestException {
        try {
            openOutput();
            final OutputStreamWriter outputStreamWriter = new OutputStreamWriter(this.output, this.output.encoder.charset());
            return (HttpRequest) new FlushOperation<HttpRequest>(outputStreamWriter) {
                /* access modifiers changed from: protected */
                public HttpRequest run() throws IOException {
                    return HttpRequest.this.copy(reader, outputStreamWriter);
                }
            }.call();
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    public HttpRequest send(CharSequence charSequence) throws HttpRequestException {
        try {
            openOutput();
            this.output.write(charSequence.toString());
            return this;
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }

    public HttpRequest send(byte[] bArr) throws HttpRequestException {
        return send((InputStream) new ByteArrayInputStream(bArr));
    }

    public String server() {
        return header(HEADER_SERVER);
    }

    public boolean serverError() throws HttpRequestException {
        return 500 == code();
    }

    /* access modifiers changed from: protected */
    public HttpRequest startPart() throws IOException {
        if (!this.multipart) {
            this.multipart = true;
            contentType(CONTENT_TYPE_MULTIPART).openOutput();
            this.output.write("--00content0boundary00\r\n");
        } else {
            this.output.write("\r\n--00content0boundary00\r\n");
        }
        return this;
    }

    public InputStream stream() throws HttpRequestException {
        InputStream errorStream;
        if (code() < 400) {
            try {
                errorStream = getConnection().getInputStream();
            } catch (IOException e) {
                throw new HttpRequestException(e);
            }
        } else {
            errorStream = getConnection().getErrorStream();
            if (errorStream == null) {
                try {
                    errorStream = getConnection().getInputStream();
                } catch (IOException e2) {
                    throw new HttpRequestException(e2);
                }
            }
        }
        if (!this.uncompress || !ENCODING_GZIP.equals(contentEncoding())) {
            return errorStream;
        }
        try {
            return new GZIPInputStream(errorStream);
        } catch (IOException e3) {
            throw new HttpRequestException(e3);
        }
    }

    public String toString() {
        return method() + ' ' + url();
    }

    public HttpRequest trustAllCerts() throws HttpRequestException {
        return this;
    }

    public HttpRequest trustAllHosts() {
        return this;
    }

    public HttpRequest uncompress(boolean z) {
        this.uncompress = z;
        return this;
    }

    public URL url() {
        return getConnection().getURL();
    }

    public HttpRequest useCaches(boolean z) {
        getConnection().setUseCaches(z);
        return this;
    }

    public HttpRequest useProxy(String str, int i) {
        if (this.connection != null) {
            throw new IllegalStateException("The connection has already been created. This method must be called before reading or writing to the request.");
        }
        this.httpProxyHost = str;
        this.httpProxyPort = i;
        return this;
    }

    public HttpRequest userAgent(String str) {
        return header("User-Agent", str);
    }

    /* access modifiers changed from: protected */
    public HttpRequest writePartHeader(String str, String str2) throws IOException {
        return writePartHeader(str, str2, null);
    }

    /* access modifiers changed from: protected */
    public HttpRequest writePartHeader(String str, String str2, String str3) throws IOException {
        StringBuilder sb = new StringBuilder();
        sb.append("form-data; name=\"").append(str);
        if (str2 != null) {
            sb.append("\"; filename=\"").append(str2);
        }
        sb.append('\"');
        partHeader("Content-Disposition", sb.toString());
        if (str3 != null) {
            partHeader(HEADER_CONTENT_TYPE, str3);
        }
        return send((CharSequence) CRLF);
    }

    public OutputStreamWriter writer() throws HttpRequestException {
        try {
            openOutput();
            return new OutputStreamWriter(this.output, this.output.encoder.charset());
        } catch (IOException e) {
            throw new HttpRequestException(e);
        }
    }
}
