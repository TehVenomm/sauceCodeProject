package net.gogame.gowrap.support;

import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;
import io.fabric.sdk.android.services.network.HttpRequest;
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
import net.gogame.gowrap.io.utils.FileUtils;
import net.gogame.gowrap.io.utils.IOUtils;

public final class DownloadUtils {

    public interface Callback {
        void onDownloadFailed();

        void onDownloadSucceeded();
    }

    public interface Target extends Closeable {
        OutputStream getOutputStream() throws IOException;

        void setEtag(String str) throws IOException;
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

        public DownloadAsyncTask(Context context, URL url, Target target, boolean z, Callback callback) {
            this.context = context;
            this.url = url;
            this.target = target;
            this.checkEtag = z;
            this.callback = callback;
        }

        protected Void doInBackground(Void... voidArr) {
            Throwable th;
            HttpURLConnection httpURLConnection;
            Throwable th2;
            OutputStream outputStream;
            try {
                HttpURLConnection httpURLConnection2;
                String str = "Etag/" + this.url.toString();
                if (this.checkEtag) {
                    Log.d(Constants.TAG, String.format("Checking if we need to download %s", new Object[]{this.url}));
                    String preference = PreferenceUtils.getPreference(this.context, str);
                    if (preference != null) {
                        try {
                            httpURLConnection2 = (HttpURLConnection) this.url.openConnection();
                            try {
                                httpURLConnection2.setRequestMethod(HttpRequest.METHOD_HEAD);
                                httpURLConnection2.setConnectTimeout(10000);
                                httpURLConnection2.setReadTimeout(10000);
                                HttpUtils.drainQuietly(httpURLConnection2);
                                if (httpURLConnection2.getResponseCode() != 200) {
                                    Log.w(Constants.TAG, String.format("%s: %d %s", new Object[]{this.url, Integer.valueOf(httpURLConnection2.getResponseCode()), httpURLConnection2.getResponseMessage()}));
                                    if (httpURLConnection2 != null) {
                                        httpURLConnection2.disconnect();
                                    }
                                    return null;
                                }
                                Object obj;
                                Map headerFields = httpURLConnection2.getHeaderFields();
                                if (headerFields == null || headerFields.get(ETAG_HEADER_NAME) == null || ((List) headerFields.get(ETAG_HEADER_NAME)).isEmpty()) {
                                    obj = null;
                                } else {
                                    obj = (String) ((List) headerFields.get(ETAG_HEADER_NAME)).get(0);
                                }
                                if (obj == null) {
                                    Log.w(Constants.TAG, String.format("Etag not found for %s", new Object[]{this.url}));
                                } else if (preference.equals(obj)) {
                                    Log.d(Constants.TAG, String.format("Local file is the same as the remote file: %s", new Object[]{this.url}));
                                    if (this.callback != null) {
                                        this.callback.onDownloadSucceeded();
                                    }
                                    if (httpURLConnection2 != null) {
                                        httpURLConnection2.disconnect();
                                    }
                                    return null;
                                }
                                if (httpURLConnection2 != null) {
                                    httpURLConnection2.disconnect();
                                }
                            } catch (Throwable e) {
                                Log.e(Constants.TAG, "Exception", e);
                            } catch (Throwable e2) {
                                th = e2;
                                httpURLConnection = httpURLConnection2;
                                th2 = th;
                                if (httpURLConnection != null) {
                                    httpURLConnection.disconnect();
                                }
                                throw th2;
                            }
                        } catch (Throwable th3) {
                            th2 = th3;
                            httpURLConnection = null;
                            if (httpURLConnection != null) {
                                httpURLConnection.disconnect();
                            }
                            throw th2;
                        }
                    }
                }
                Log.d(Constants.TAG, String.format("Downloading %s", new Object[]{this.url}));
                try {
                    httpURLConnection2 = (HttpURLConnection) this.url.openConnection();
                    try {
                        httpURLConnection2.setRequestMethod(HttpRequest.METHOD_GET);
                        httpURLConnection2.setConnectTimeout(10000);
                        httpURLConnection2.setReadTimeout(10000);
                        if (httpURLConnection2.getResponseCode() != 200) {
                            Log.w(Constants.TAG, String.format("%s: %d %s", new Object[]{this.url, Integer.valueOf(httpURLConnection2.getResponseCode()), httpURLConnection2.getResponseMessage()}));
                            HttpUtils.drainQuietly(httpURLConnection2);
                            if (this.callback != null) {
                                this.callback.onDownloadFailed();
                            }
                            if (httpURLConnection2 != null) {
                                httpURLConnection2.disconnect();
                            }
                            return null;
                        }
                        String str2;
                        Map headerFields2 = httpURLConnection2.getHeaderFields();
                        if (headerFields2 == null || headerFields2.get(ETAG_HEADER_NAME) == null || ((List) headerFields2.get(ETAG_HEADER_NAME)).isEmpty()) {
                            str2 = null;
                        } else {
                            str2 = (String) ((List) headerFields2.get(ETAG_HEADER_NAME)).get(0);
                        }
                        if (str2 != null) {
                            this.target.setEtag(str2);
                        }
                        InputStream inputStream = httpURLConnection2.getInputStream();
                        try {
                            outputStream = this.target.getOutputStream();
                            IOUtils.copy(inputStream, outputStream);
                            IOUtils.closeQuietly(outputStream);
                            this.target.close();
                            Log.d(Constants.TAG, String.format("Downloaded %s", new Object[]{this.url}));
                            if (this.checkEtag) {
                                PreferenceUtils.setPreference(this.context, str, str2);
                            }
                            if (this.callback != null) {
                                try {
                                    this.callback.onDownloadSucceeded();
                                } catch (Throwable e22) {
                                    Log.e(Constants.TAG, "Exception", e22);
                                }
                            }
                            IOUtils.closeQuietly(inputStream);
                            if (httpURLConnection2 != null) {
                                httpURLConnection2.disconnect();
                            }
                            return null;
                        } catch (Throwable th4) {
                            IOUtils.closeQuietly(inputStream);
                        }
                    } catch (Throwable e222) {
                        Log.e(Constants.TAG, "Exception", e222);
                    } catch (Throwable e2222) {
                        th = e2222;
                        httpURLConnection = httpURLConnection2;
                        th2 = th;
                        if (httpURLConnection != null) {
                            httpURLConnection.disconnect();
                        }
                        throw th2;
                    }
                } catch (Throwable th5) {
                    th2 = th5;
                    httpURLConnection = null;
                    if (httpURLConnection != null) {
                        httpURLConnection.disconnect();
                    }
                    throw th2;
                }
            } catch (Throwable th22) {
                Log.e(Constants.TAG, "Exception", th22);
                if (this.callback != null) {
                    try {
                        this.callback.onDownloadFailed();
                    } catch (Throwable th222) {
                        Log.e(Constants.TAG, "Exception", th222);
                    }
                }
            }
        }
    }

    public static class FileTarget implements Target {
        private final File file;
        private File tempFile;

        public FileTarget(File file) {
            this.file = file;
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

    private DownloadUtils() {
    }

    public static void download(Context context, URL url, Target target, boolean z, Callback callback) {
        new DownloadAsyncTask(context, url, target, z, callback).execute(new Void[0]);
    }
}
