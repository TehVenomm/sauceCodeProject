package jp.colopl.util;

import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.util.List;
import org.apache.http.NameValuePair;

public class HttpMultipartRequest {
    private static final String BOUNDARY = "----------V2ymHFg03ehbqgZCaKO6jy";
    private static final int COUNT_UNIT_BYTES = 4096;
    private static final String TAG = "HttpMultipartRequest";
    private int connectionTimeout;
    private List<String> cookies;
    private File file;
    private ProgressCallback mCallback;
    private List<NameValuePair> postData;
    private int readingTimeout;
    private String url;
    private String userAgent;

    public interface ProgressCallback {
        void progress(int i, int i2);
    }

    public HttpMultipartRequest(String str, List<NameValuePair> list, List<String> list2, File file, String str2, ProgressCallback progressCallback) {
        this.connectionTimeout = 900000;
        this.readingTimeout = 900000;
        if (str == null || file == null) {
            throw new IllegalArgumentException();
        }
        this.url = str;
        this.postData = list;
        this.cookies = list2;
        this.file = file;
        this.userAgent = str2;
        this.mCallback = progressCallback;
    }

    public HttpMultipartRequest(String str, List<NameValuePair> list, List<String> list2, String str2, String str3, ProgressCallback progressCallback) {
        this(str, (List) list, (List) list2, new File(str2), str3, progressCallback);
    }

    private String getBoundaryMessage(List<NameValuePair> list, String str) {
        StringBuffer append = new StringBuffer("--").append(BOUNDARY).append("\r\n");
        if (list != null) {
            for (NameValuePair nameValuePair : list) {
                append.append("Content-Disposition: form-data; name=\"").append(nameValuePair.getName()).append("\"\r\n").append("\r\n").append(nameValuePair.getValue()).append("\r\n").append("--").append(BOUNDARY).append("\r\n");
            }
        }
        String[] split = str.split("\\.");
        append.append("Content-Disposition: form-data; name=\"file\"").append("; filename=\"").append(str).append("\"\r\n").append("Content-Type: ").append("image/" + split[split.length - 1]).append("\r\n\r\n");
        return append.toString();
    }

    private byte[] getImageBytes(File file) {
        FileInputStream fileInputStream;
        FileNotFoundException e;
        IOException e2;
        Throwable th;
        FileInputStream fileInputStream2 = null;
        byte[] bArr = new byte[10];
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        try {
            fileInputStream = new FileInputStream(file);
            while (fileInputStream.read(bArr) > 0) {
                try {
                    byteArrayOutputStream.write(bArr);
                } catch (FileNotFoundException e3) {
                    e = e3;
                } catch (IOException e4) {
                    e2 = e4;
                } catch (Throwable th2) {
                    th = th2;
                }
            }
            try {
                byteArrayOutputStream.close();
            } catch (IOException e5) {
            }
            if (fileInputStream != null) {
                try {
                    fileInputStream.close();
                } catch (IOException e6) {
                }
            }
        } catch (FileNotFoundException e7) {
            e = e7;
            fileInputStream = null;
            try {
                e.printStackTrace();
                try {
                    byteArrayOutputStream.close();
                } catch (IOException e8) {
                }
                if (fileInputStream != null) {
                    try {
                        fileInputStream.close();
                    } catch (IOException e9) {
                    }
                }
                return byteArrayOutputStream.toByteArray();
            } catch (Throwable th3) {
                th = th3;
                fileInputStream2 = fileInputStream;
                fileInputStream = fileInputStream2;
                try {
                    byteArrayOutputStream.close();
                } catch (IOException e10) {
                }
                if (fileInputStream != null) {
                    try {
                        fileInputStream.close();
                    } catch (IOException e11) {
                    }
                }
                throw th;
            }
        } catch (IOException e12) {
            e2 = e12;
            fileInputStream = null;
            e2.printStackTrace();
            try {
                byteArrayOutputStream.close();
            } catch (IOException e13) {
            }
            if (fileInputStream != null) {
                try {
                    fileInputStream.close();
                } catch (IOException e14) {
                }
            }
            return byteArrayOutputStream.toByteArray();
        } catch (Throwable th4) {
            th = th4;
            fileInputStream = fileInputStream2;
            byteArrayOutputStream.close();
            if (fileInputStream != null) {
                fileInputStream.close();
            }
            throw th;
        }
        return byteArrayOutputStream.toByteArray();
    }

    public String send() {
        URLConnection uRLConnection;
        IOException iOException;
        Throwable th;
        try {
            URLConnection openConnection = new URL(this.url).openConnection();
            try {
                openConnection.setRequestProperty(HttpRequest.HEADER_CONTENT_TYPE, "multipart/form-data; boundary=----------V2ymHFg03ehbqgZCaKO6jy");
                openConnection.setRequestProperty("User-Agent", this.userAgent);
                openConnection.setConnectTimeout(this.connectionTimeout);
                openConnection.setReadTimeout(this.readingTimeout);
                ((HttpURLConnection) openConnection).setRequestMethod(HttpRequest.METHOD_POST);
                openConnection.setDoOutput(true);
                if (this.cookies != null) {
                    StringBuffer stringBuffer = new StringBuffer();
                    for (String append : this.cookies) {
                        stringBuffer.append(append).append("; ");
                    }
                    openConnection.setRequestProperty("Cookie", stringBuffer.toString());
                }
                openConnection.connect();
                OutputStream outputStream = openConnection.getOutputStream();
                outputStream.write(getBoundaryMessage(this.postData, this.file.getName()).getBytes());
                byte[] imageBytes = getImageBytes(this.file);
                int length = imageBytes.length;
                int i = 0;
                while (i < length) {
                    int i2 = 4096;
                    if (i + 4096 > length) {
                        i2 = length - i;
                    }
                    outputStream.write(imageBytes, i, i2);
                    i2 += i;
                    if (this.mCallback != null) {
                        this.mCallback.progress(i2, length);
                        i = i2;
                    } else {
                        i = i2;
                    }
                }
                outputStream.write("\r\n------------V2ymHFg03ehbqgZCaKO6jy--\r\n".getBytes());
                outputStream.close();
                String append2 = ((HttpURLConnection) openConnection).getResponseCode() == 200 ? StringUtil.convertToString(new DoneHandlerInputStream(openConnection.getInputStream())) : null;
                if (openConnection == null) {
                    return append2;
                }
                ((HttpURLConnection) openConnection).disconnect();
                return append2;
            } catch (IOException e) {
                IOException iOException2 = e;
                uRLConnection = openConnection;
                iOException = iOException2;
                try {
                    Util.dLog("", iOException.getMessage());
                    if (uRLConnection != null) {
                        return null;
                    }
                    ((HttpURLConnection) uRLConnection).disconnect();
                    return null;
                } catch (Throwable th2) {
                    th = th2;
                    if (uRLConnection != null) {
                        ((HttpURLConnection) uRLConnection).disconnect();
                    }
                    throw th;
                }
            } catch (Throwable th3) {
                Throwable th4 = th3;
                uRLConnection = openConnection;
                th = th4;
                if (uRLConnection != null) {
                    ((HttpURLConnection) uRLConnection).disconnect();
                }
                throw th;
            }
        } catch (IOException e2) {
            iOException = e2;
            uRLConnection = null;
            Util.dLog("", iOException.getMessage());
            if (uRLConnection != null) {
                return null;
            }
            ((HttpURLConnection) uRLConnection).disconnect();
            return null;
        } catch (Throwable th32) {
            th = th32;
            uRLConnection = null;
            if (uRLConnection != null) {
                ((HttpURLConnection) uRLConnection).disconnect();
            }
            throw th;
        }
    }

    public void setConnectionTimeout(int i) {
        this.connectionTimeout = i;
    }

    public void setReadingTimeout(int i) {
        this.readingTimeout = i;
    }
}
