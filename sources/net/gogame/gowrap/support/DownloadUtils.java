package net.gogame.gowrap.support;

import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;
import java.io.Closeable;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.List;
import java.util.Map;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.p021io.utils.FileUtils;
import net.gogame.gowrap.p021io.utils.IOUtils;
import p017io.fabric.sdk.android.services.network.HttpRequest;

public final class DownloadUtils {

    public interface Callback {
        void onDownloadFailed();

        void onDownloadSucceeded();
    }

    public static class DownloadAsyncTask extends AsyncTask<Void, Void, Void> {
        private static final int CONNECT_TIMEOUT = 10000;
        private static final String ETAG_HEADER_NAME = "Etag";
        private static final int READ_TIMEOUT = 10000;
        private final Callback callback;
        private final boolean checkEtag;
        private final Context context;
        private final Target target;
        private final URL url;

        public DownloadAsyncTask(Context context2, URL url2, Target target2, boolean z, Callback callback2) {
            this.context = context2;
            this.url = url2;
            this.target = target2;
            this.checkEtag = z;
            this.callback = callback2;
        }

        /* access modifiers changed from: protected */
        public Void doInBackground(Void... voidArr) {
            Throwable th;
            HttpURLConnection httpURLConnection;
            String str;
            OutputStream outputStream;
            Throwable th2;
            HttpURLConnection httpURLConnection2;
            String str2;
            try {
                String str3 = "Etag/" + this.url.toString();
                if (this.checkEtag) {
                    Log.d(Constants.TAG, String.format("Checking if we need to download %s", new Object[]{this.url}));
                    String preference = PreferenceUtils.getPreference(this.context, str3);
                    if (preference != null) {
                        try {
                            HttpURLConnection httpURLConnection3 = (HttpURLConnection) this.url.openConnection();
                            try {
                                httpURLConnection3.setRequestMethod(HttpRequest.METHOD_HEAD);
                                httpURLConnection3.setConnectTimeout(10000);
                                httpURLConnection3.setReadTimeout(10000);
                                HttpUtils.drainQuietly(httpURLConnection3);
                                if (httpURLConnection3.getResponseCode() != 200) {
                                    Log.w(Constants.TAG, String.format("%s: %d %s", new Object[]{this.url, Integer.valueOf(httpURLConnection3.getResponseCode()), httpURLConnection3.getResponseMessage()}));
                                    if (httpURLConnection3 != null) {
                                        httpURLConnection3.disconnect();
                                    }
                                    return null;
                                }
                                Map headerFields = httpURLConnection3.getHeaderFields();
                                if (headerFields == null || headerFields.get(ETAG_HEADER_NAME) == null || ((List) headerFields.get(ETAG_HEADER_NAME)).isEmpty()) {
                                    str2 = null;
                                } else {
                                    str2 = (String) ((List) headerFields.get(ETAG_HEADER_NAME)).get(0);
                                }
                                if (str2 == null) {
                                    Log.w(Constants.TAG, String.format("Etag not found for %s", new Object[]{this.url}));
                                } else if (preference.equals(str2)) {
                                    Log.d(Constants.TAG, String.format("Local file is the same as the remote file: %s", new Object[]{this.url}));
                                    if (this.callback != null) {
                                        this.callback.onDownloadSucceeded();
                                    }
                                    if (httpURLConnection3 != null) {
                                        httpURLConnection3.disconnect();
                                    }
                                    return null;
                                }
                                if (httpURLConnection3 != null) {
                                    httpURLConnection3.disconnect();
                                }
                            } catch (Exception e) {
                                Log.e(Constants.TAG, "Exception", e);
                            } catch (Throwable th3) {
                                th2 = th3;
                                httpURLConnection2 = httpURLConnection3;
                            }
                        } catch (Throwable th4) {
                            th2 = th4;
                            httpURLConnection2 = null;
                            if (httpURLConnection2 != null) {
                                httpURLConnection2.disconnect();
                            }
                            throw th2;
                        }
                    }
                }
                Log.d(Constants.TAG, String.format("Downloading %s", new Object[]{this.url}));
                try {
                    HttpURLConnection httpURLConnection4 = (HttpURLConnection) this.url.openConnection();
                    try {
                        httpURLConnection4.setRequestMethod(HttpRequest.METHOD_GET);
                        httpURLConnection4.setConnectTimeout(10000);
                        httpURLConnection4.setReadTimeout(10000);
                        if (httpURLConnection4.getResponseCode() != 200) {
                            Log.w(Constants.TAG, String.format("%s: %d %s", new Object[]{this.url, Integer.valueOf(httpURLConnection4.getResponseCode()), httpURLConnection4.getResponseMessage()}));
                            HttpUtils.drainQuietly(httpURLConnection4);
                            if (this.callback != null) {
                                this.callback.onDownloadFailed();
                            }
                            if (httpURLConnection4 != null) {
                                httpURLConnection4.disconnect();
                            }
                            return null;
                        }
                        Map headerFields2 = httpURLConnection4.getHeaderFields();
                        if (headerFields2 == null || headerFields2.get(ETAG_HEADER_NAME) == null || ((List) headerFields2.get(ETAG_HEADER_NAME)).isEmpty()) {
                            str = null;
                        } else {
                            str = (String) ((List) headerFields2.get(ETAG_HEADER_NAME)).get(0);
                        }
                        if (str != null) {
                            this.target.setEtag(str);
                        }
                        InputStream inputStream = httpURLConnection4.getInputStream();
                        try {
                            outputStream = this.target.getOutputStream();
                            IOUtils.copy(inputStream, outputStream);
                            IOUtils.closeQuietly(outputStream);
                            this.target.close();
                            Log.d(Constants.TAG, String.format("Downloaded %s", new Object[]{this.url}));
                            if (this.checkEtag) {
                                PreferenceUtils.setPreference(this.context, str3, str);
                            }
                            if (this.callback != null) {
                                try {
                                    this.callback.onDownloadSucceeded();
                                } catch (Exception e2) {
                                    Log.e(Constants.TAG, "Exception", e2);
                                }
                            }
                            IOUtils.closeQuietly(inputStream);
                            if (httpURLConnection4 != null) {
                                httpURLConnection4.disconnect();
                            }
                            return null;
                        } catch (Throwable th5) {
                            IOUtils.closeQuietly(inputStream);
                            throw th5;
                        }
                    } catch (Exception e3) {
                        Log.e(Constants.TAG, "Exception", e3);
                    } catch (Throwable th6) {
                        th = th6;
                        httpURLConnection = httpURLConnection4;
                    }
                } catch (Throwable th7) {
                    th = th7;
                    httpURLConnection = null;
                    if (httpURLConnection != null) {
                        httpURLConnection.disconnect();
                    }
                    throw th;
                }
            } catch (Exception e4) {
                Log.e(Constants.TAG, "Exception", e4);
                if (this.callback != null) {
                    try {
                        this.callback.onDownloadFailed();
                    } catch (Exception e5) {
                        Log.e(Constants.TAG, "Exception", e5);
                    }
                }
            }
        }
    }

    public static class FileTarget implements Target {
        private final File file;
        private File tempFile;

        public FileTarget(File file2) {
            this.file = file2;
        }

        public void setEtag(String str) throws IOException {
        }

        public OutputStream getOutputStream() throws IOException {
            if (this.tempFile == null) {
                this.tempFile = File.createTempFile("download-", ".tmp");
                this.tempFile.deleteOnExit();
                return new FileOutputStream(this.tempFile);
            }
            throw new IllegalStateException("Output stream already opened");
        }

        public void close() throws IOException {
            if (this.tempFile != null) {
                FileUtils.copy(this.tempFile, this.file);
                this.tempFile.delete();
                this.tempFile = null;
            }
        }
    }

    public interface Target extends Closeable {
        OutputStream getOutputStream() throws IOException;

        void setEtag(String str) throws IOException;
    }

    private DownloadUtils() {
    }

    public static void download(Context context, URL url, Target target, boolean z, Callback callback) {
        new DownloadAsyncTask(context, url, target, z, callback).execute(new Void[0]);
    }
}
