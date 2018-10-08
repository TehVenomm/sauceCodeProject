package com.zopim.android.sdk.api;

import android.support.annotation.NonNull;
import android.util.Log;
import android.util.Patterns;
import com.zopim.android.sdk.api.C0809l.C0808a;
import com.zopim.android.sdk.api.ErrorResponse.Kind;
import com.zopim.android.sdk.api.HttpRequest.ProgressListener;
import com.zopim.android.sdk.api.HttpRequest.Status;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.URL;
import java.net.URLConnection;
import javax.net.ssl.HttpsURLConnection;

/* renamed from: com.zopim.android.sdk.api.s */
final class C0816s implements HttpRequest {
    /* renamed from: b */
    private static final String f666b = C0816s.class.getSimpleName();
    /* renamed from: d */
    private static final String f667d = Long.toHexString(System.currentTimeMillis());
    /* renamed from: c */
    private String f668c = HttpRequest.METHOD_POST;
    /* renamed from: e */
    private C0800u<Void> f669e;
    /* renamed from: f */
    private ProgressListener f670f;

    C0816s() {
    }

    /* renamed from: a */
    private void m622a(int i) {
        if (this.f670f != null) {
            this.f670f.onProgressUpdate(i);
        }
    }

    /* renamed from: b */
    private void m623b(@NonNull File file, @NonNull URL url) {
        PrintWriter printWriter;
        InputStream bufferedInputStream;
        OutputStream outputStream;
        Throwable th;
        Throwable th2;
        ErrorResponse a;
        HttpsURLConnection httpsURLConnection;
        HttpsURLConnection httpsURLConnection2 = null;
        OutputStream outputStream2;
        try {
            long length;
            HttpsURLConnection httpsURLConnection3 = (HttpsURLConnection) url.openConnection();
            try {
                httpsURLConnection3.setRequestMethod(this.f668c);
                httpsURLConnection3.setDoOutput(true);
                httpsURLConnection3.setRequestProperty("User-Agent", System.getProperty("http.agent"));
                httpsURLConnection3.setRequestProperty(HttpRequest.HEADER_ACCEPT_CHARSET, "UTF-8");
                httpsURLConnection3.setRequestProperty(HttpRequest.HEADER_CONTENT_TYPE, "multipart/form-data; boundary=" + f667d);
                httpsURLConnection3.setInstanceFollowRedirects(false);
                httpsURLConnection3.setReadTimeout((int) a);
                outputStream2 = httpsURLConnection3.getOutputStream();
                try {
                    printWriter = new PrintWriter(new OutputStreamWriter(outputStream2, "UTF-8"), true);
                    try {
                        printWriter.append("--" + f667d).append("\r\n");
                        printWriter.append("Content-Disposition: form-data; name=\"binaryFile\"; filename=\"" + file.getName() + "\"").append("\r\n");
                        printWriter.append("Content-Type: " + URLConnection.guessContentTypeFromName(file.getName())).append("\r\n");
                        printWriter.append("Content-Length: " + file.length());
                        printWriter.append("Content-Transfer-Encoding: binary").append("\r\n");
                        printWriter.append("\r\n").flush();
                        length = file.length();
                        m622a(1);
                        bufferedInputStream = new BufferedInputStream(new FileInputStream(file));
                    } catch (Throwable e) {
                        bufferedInputStream = null;
                        outputStream = outputStream2;
                        th = e;
                        httpsURLConnection2 = httpsURLConnection3;
                        th2 = th;
                        try {
                            Log.e(f666b, "Error uploading file to " + url, th2);
                            a = new C0808a().m596a(Kind.UNEXPECTED).m597a(th2.getMessage()).m599b(url.toExternalForm()).m598a();
                            if (this.f669e != null) {
                                this.f669e.m576b(a);
                            }
                            if (httpsURLConnection2 != null) {
                                Logger.m564v(f666b, "Disconnecting url connection");
                                httpsURLConnection2.disconnect();
                            }
                            if (printWriter != null) {
                                try {
                                    Logger.m564v(f666b, "Closing print writer");
                                    printWriter.close();
                                } catch (Throwable th22) {
                                    Log.w(f666b, "Failed to close writer", th22);
                                }
                            }
                            if (outputStream != null) {
                                try {
                                    Logger.m564v(f666b, "Closing output stream");
                                    outputStream.close();
                                } catch (Throwable th222) {
                                    Log.w(f666b, "Failed to close output stream", th222);
                                }
                            }
                            if (bufferedInputStream != null) {
                                try {
                                    Logger.m564v(f666b, "Closing file input stream");
                                    bufferedInputStream.close();
                                } catch (Throwable th2222) {
                                    Log.w(f666b, "Failed to close file input stream", th2222);
                                    return;
                                }
                            }
                        } catch (Throwable th3) {
                            th2222 = th3;
                            outputStream2 = outputStream;
                            if (httpsURLConnection2 != null) {
                                Logger.m564v(f666b, "Disconnecting url connection");
                                httpsURLConnection2.disconnect();
                            }
                            if (printWriter != null) {
                                try {
                                    Logger.m564v(f666b, "Closing print writer");
                                    printWriter.close();
                                } catch (Throwable e2) {
                                    Log.w(f666b, "Failed to close writer", e2);
                                }
                            }
                            if (outputStream2 != null) {
                                try {
                                    Logger.m564v(f666b, "Closing output stream");
                                    outputStream2.close();
                                } catch (Throwable e22) {
                                    Log.w(f666b, "Failed to close output stream", e22);
                                }
                            }
                            if (bufferedInputStream != null) {
                                try {
                                    Logger.m564v(f666b, "Closing file input stream");
                                    bufferedInputStream.close();
                                } catch (Throwable e222) {
                                    Log.w(f666b, "Failed to close file input stream", e222);
                                }
                            }
                            throw th2222;
                        }
                    } catch (Throwable e2222) {
                        bufferedInputStream = null;
                        httpsURLConnection = httpsURLConnection3;
                        th2222 = e2222;
                        httpsURLConnection2 = httpsURLConnection;
                        if (httpsURLConnection2 != null) {
                            Logger.m564v(f666b, "Disconnecting url connection");
                            httpsURLConnection2.disconnect();
                        }
                        if (printWriter != null) {
                            Logger.m564v(f666b, "Closing print writer");
                            printWriter.close();
                        }
                        if (outputStream2 != null) {
                            Logger.m564v(f666b, "Closing output stream");
                            outputStream2.close();
                        }
                        if (bufferedInputStream != null) {
                            Logger.m564v(f666b, "Closing file input stream");
                            bufferedInputStream.close();
                        }
                        throw th2222;
                    }
                } catch (Throwable e22222) {
                    printWriter = null;
                    bufferedInputStream = null;
                    outputStream = outputStream2;
                    httpsURLConnection = httpsURLConnection3;
                    th2222 = e22222;
                    httpsURLConnection2 = httpsURLConnection;
                    Log.e(f666b, "Error uploading file to " + url, th2222);
                    a = new C0808a().m596a(Kind.UNEXPECTED).m597a(th2222.getMessage()).m599b(url.toExternalForm()).m598a();
                    if (this.f669e != null) {
                        this.f669e.m576b(a);
                    }
                    if (httpsURLConnection2 != null) {
                        Logger.m564v(f666b, "Disconnecting url connection");
                        httpsURLConnection2.disconnect();
                    }
                    if (printWriter != null) {
                        Logger.m564v(f666b, "Closing print writer");
                        printWriter.close();
                    }
                    if (outputStream != null) {
                        Logger.m564v(f666b, "Closing output stream");
                        outputStream.close();
                    }
                    if (bufferedInputStream != null) {
                        Logger.m564v(f666b, "Closing file input stream");
                        bufferedInputStream.close();
                    }
                } catch (Throwable e222222) {
                    printWriter = null;
                    bufferedInputStream = null;
                    th = e222222;
                    httpsURLConnection2 = httpsURLConnection3;
                    th2222 = th;
                    if (httpsURLConnection2 != null) {
                        Logger.m564v(f666b, "Disconnecting url connection");
                        httpsURLConnection2.disconnect();
                    }
                    if (printWriter != null) {
                        Logger.m564v(f666b, "Closing print writer");
                        printWriter.close();
                    }
                    if (outputStream2 != null) {
                        Logger.m564v(f666b, "Closing output stream");
                        outputStream2.close();
                    }
                    if (bufferedInputStream != null) {
                        Logger.m564v(f666b, "Closing file input stream");
                        bufferedInputStream.close();
                    }
                    throw th2222;
                }
            } catch (Throwable e2222222) {
                th = e2222222;
                httpsURLConnection2 = httpsURLConnection3;
                th2222 = th;
                bufferedInputStream = null;
                outputStream = null;
                printWriter = null;
                Log.e(f666b, "Error uploading file to " + url, th2222);
                a = new C0808a().m596a(Kind.UNEXPECTED).m597a(th2222.getMessage()).m599b(url.toExternalForm()).m598a();
                if (this.f669e != null) {
                    this.f669e.m576b(a);
                }
                if (httpsURLConnection2 != null) {
                    Logger.m564v(f666b, "Disconnecting url connection");
                    httpsURLConnection2.disconnect();
                }
                if (printWriter != null) {
                    Logger.m564v(f666b, "Closing print writer");
                    printWriter.close();
                }
                if (outputStream != null) {
                    Logger.m564v(f666b, "Closing output stream");
                    outputStream.close();
                }
                if (bufferedInputStream != null) {
                    Logger.m564v(f666b, "Closing file input stream");
                    bufferedInputStream.close();
                }
            } catch (Throwable e22222222) {
                outputStream2 = null;
                printWriter = null;
                bufferedInputStream = null;
                httpsURLConnection = httpsURLConnection3;
                th2222 = e22222222;
                httpsURLConnection2 = httpsURLConnection;
                if (httpsURLConnection2 != null) {
                    Logger.m564v(f666b, "Disconnecting url connection");
                    httpsURLConnection2.disconnect();
                }
                if (printWriter != null) {
                    Logger.m564v(f666b, "Closing print writer");
                    printWriter.close();
                }
                if (outputStream2 != null) {
                    Logger.m564v(f666b, "Closing output stream");
                    outputStream2.close();
                }
                if (bufferedInputStream != null) {
                    Logger.m564v(f666b, "Closing file input stream");
                    bufferedInputStream.close();
                }
                throw th2222;
            }
            try {
                int min = Math.min(bufferedInputStream.available(), 4096);
                byte[] bArr = new byte[min];
                min = bufferedInputStream.read(bArr, 0, min);
                Log.v(f666b, "Reading bytes from fis");
                int i = min;
                while (i > 0) {
                    outputStream2.write(bArr, 0, i);
                    m622a(Math.round((float) (((long) (99 * min)) / length)));
                    i = bufferedInputStream.read(bArr, 0, Math.min(bufferedInputStream.available(), 4096));
                    min += i;
                }
                Logger.m564v(f666b, "Finished write to output stream. Closing file input stream");
                bufferedInputStream.close();
                outputStream2.flush();
                printWriter.append("\r\n").flush();
                printWriter.append("--" + f667d + "--").append("\r\n").flush();
                printWriter.close();
                outputStream2.close();
                min = httpsURLConnection3.getResponseCode();
                switch (C0817t.f671a[Status.getStatus(min).ordinal()]) {
                    case 1:
                        Logger.m562i(f666b, "Request completed. Status " + min);
                        if (this.f669e != null) {
                            this.f669e.m577b(null);
                            break;
                        }
                        break;
                    case 2:
                    case 3:
                    case 4:
                        ErrorResponse a2 = new C0808a().m596a(Kind.HTTP).m595a(min).m599b(url.toExternalForm()).m600c(httpsURLConnection3.getResponseMessage()).m598a();
                        if (this.f669e != null) {
                            this.f669e.m576b(a2);
                            break;
                        }
                        break;
                }
                if (httpsURLConnection3 != null) {
                    Logger.m564v(f666b, "Disconnecting url connection");
                    httpsURLConnection3.disconnect();
                }
                if (printWriter != null) {
                    try {
                        Logger.m564v(f666b, "Closing print writer");
                        printWriter.close();
                    } catch (Throwable th22222) {
                        Log.w(f666b, "Failed to close writer", th22222);
                    }
                }
                if (outputStream2 != null) {
                    try {
                        Logger.m564v(f666b, "Closing output stream");
                        outputStream2.close();
                    } catch (Throwable th222222) {
                        Log.w(f666b, "Failed to close output stream", th222222);
                    }
                }
                if (bufferedInputStream != null) {
                    try {
                        Logger.m564v(f666b, "Closing file input stream");
                        bufferedInputStream.close();
                    } catch (Throwable th2222222) {
                        Log.w(f666b, "Failed to close file input stream", th2222222);
                    }
                }
            } catch (Throwable e222222222) {
                outputStream = outputStream2;
                httpsURLConnection = httpsURLConnection3;
                th2222222 = e222222222;
                httpsURLConnection2 = httpsURLConnection;
                Log.e(f666b, "Error uploading file to " + url, th2222222);
                a = new C0808a().m596a(Kind.UNEXPECTED).m597a(th2222222.getMessage()).m599b(url.toExternalForm()).m598a();
                if (this.f669e != null) {
                    this.f669e.m576b(a);
                }
                if (httpsURLConnection2 != null) {
                    Logger.m564v(f666b, "Disconnecting url connection");
                    httpsURLConnection2.disconnect();
                }
                if (printWriter != null) {
                    Logger.m564v(f666b, "Closing print writer");
                    printWriter.close();
                }
                if (outputStream != null) {
                    Logger.m564v(f666b, "Closing output stream");
                    outputStream.close();
                }
                if (bufferedInputStream != null) {
                    Logger.m564v(f666b, "Closing file input stream");
                    bufferedInputStream.close();
                }
            } catch (Throwable e2222222222) {
                th = e2222222222;
                httpsURLConnection2 = httpsURLConnection3;
                th2222222 = th;
                if (httpsURLConnection2 != null) {
                    Logger.m564v(f666b, "Disconnecting url connection");
                    httpsURLConnection2.disconnect();
                }
                if (printWriter != null) {
                    Logger.m564v(f666b, "Closing print writer");
                    printWriter.close();
                }
                if (outputStream2 != null) {
                    Logger.m564v(f666b, "Closing output stream");
                    outputStream2.close();
                }
                if (bufferedInputStream != null) {
                    Logger.m564v(f666b, "Closing file input stream");
                    bufferedInputStream.close();
                }
                throw th2222222;
            }
        } catch (Exception e3) {
            th2222222 = e3;
            bufferedInputStream = null;
            outputStream = null;
            printWriter = null;
            Log.e(f666b, "Error uploading file to " + url, th2222222);
            a = new C0808a().m596a(Kind.UNEXPECTED).m597a(th2222222.getMessage()).m599b(url.toExternalForm()).m598a();
            if (this.f669e != null) {
                this.f669e.m576b(a);
            }
            if (httpsURLConnection2 != null) {
                Logger.m564v(f666b, "Disconnecting url connection");
                httpsURLConnection2.disconnect();
            }
            if (printWriter != null) {
                Logger.m564v(f666b, "Closing print writer");
                printWriter.close();
            }
            if (outputStream != null) {
                Logger.m564v(f666b, "Closing output stream");
                outputStream.close();
            }
            if (bufferedInputStream != null) {
                Logger.m564v(f666b, "Closing file input stream");
                bufferedInputStream.close();
            }
        } catch (Throwable th4) {
            th2222222 = th4;
            outputStream2 = null;
            printWriter = null;
            bufferedInputStream = null;
            if (httpsURLConnection2 != null) {
                Logger.m564v(f666b, "Disconnecting url connection");
                httpsURLConnection2.disconnect();
            }
            if (printWriter != null) {
                Logger.m564v(f666b, "Closing print writer");
                printWriter.close();
            }
            if (outputStream2 != null) {
                Logger.m564v(f666b, "Closing output stream");
                outputStream2.close();
            }
            if (bufferedInputStream != null) {
                Logger.m564v(f666b, "Closing file input stream");
                bufferedInputStream.close();
            }
            throw th2222222;
        }
    }

    /* renamed from: a */
    public void m624a(ProgressListener progressListener) {
        this.f670f = progressListener;
    }

    /* renamed from: a */
    public void m625a(C0800u<Void> c0800u) {
        this.f669e = c0800u;
    }

    /* renamed from: a */
    public void m626a(File file, URL url) {
        if (file == null || file.getName() == null || file.getName().isEmpty() || !file.exists()) {
            Log.e(f666b, "File validation failed. Upload aborted.");
        } else if (url == null || !Patterns.WEB_URL.matcher(url.toString()).matches()) {
            Log.e(f666b, "URL validation failed. Upload aborted.");
        } else {
            Logger.m564v(f666b, "Start of upload.");
            m623b(file, url);
            Logger.m564v(f666b, "End of upload.");
        }
    }
}
