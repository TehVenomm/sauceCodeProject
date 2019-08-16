package net.gogame.gowrap.support;

import android.content.Context;
import android.net.Uri;
import android.os.Handler;
import android.util.Log;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.net.URL;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.HashSet;
import java.util.Set;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.support.DiskLruCache.Editor;
import net.gogame.gowrap.support.DiskLruCache.Snapshot;
import net.gogame.gowrap.support.DownloadManager.DownloadResult;
import net.gogame.gowrap.support.DownloadManager.Listener;
import net.gogame.gowrap.support.DownloadManager.Request;
import net.gogame.gowrap.support.DownloadManager.Target;
import net.gogame.gowrap.support.DownloadUtils.Callback;
import p017io.fabric.sdk.android.services.common.CommonUtils;
import p017io.fabric.sdk.android.services.events.EventsFilesManager;

public class DefaultDownloadManager implements DownloadManager {
    /* access modifiers changed from: private */
    public final Context context;
    /* access modifiers changed from: private */
    public final DiskLruCache diskLruCache;
    private int downloadCount = 0;
    /* access modifiers changed from: private */
    public final Handler handler = new Handler();
    private final Set<Listener> listeners = new HashSet();

    private class DefaultDownloadResult implements DownloadResult {
        private final String key;
        private Snapshot snapshot;

        public DefaultDownloadResult(String str) {
            this.key = str;
        }

        public InputStream getInputStream() throws IOException {
            if (this.snapshot == null) {
                this.snapshot = DefaultDownloadManager.this.diskLruCache.get(this.key);
                if (this.snapshot == null) {
                    return null;
                }
                return this.snapshot.getInputStream(0);
            }
            throw new IllegalStateException("Snapshot already open");
        }

        public void close() throws IOException {
            if (this.snapshot != null) {
                this.snapshot.close();
                this.snapshot = null;
            }
        }
    }

    public DefaultDownloadManager(Context context2, DiskLruCache diskLruCache2) {
        this.context = context2;
        this.diskLruCache = diskLruCache2;
    }

    private static String toKey(Uri uri) {
        try {
            return computeDigest(uri.toString(), CommonUtils.SHA1_INSTANCE) + EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR + computeDigest(uri.toString(), "MD5");
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

    private static String computeDigest(String str, String str2) throws NoSuchAlgorithmException, UnsupportedEncodingException {
        MessageDigest instance = MessageDigest.getInstance(str2);
        instance.update(str.getBytes("UTF-8"));
        byte[] digest = instance.digest();
        StringBuilder sb = new StringBuilder();
        for (byte b : digest) {
            sb.append(String.format("%02x", new Object[]{Integer.valueOf(b & 255)}));
        }
        return sb.toString();
    }

    private synchronized void onDownloadStarted() {
        this.downloadCount++;
        fireDownloadsStarted();
    }

    /* access modifiers changed from: private */
    public synchronized void onDownloadFinished() {
        this.downloadCount--;
        if (this.downloadCount < 0) {
            this.downloadCount = 0;
        }
        if (this.downloadCount == 0) {
            this.handler.post(new Runnable() {
                public void run() {
                    DefaultDownloadManager.this.fireDownloadsFinished();
                }
            });
        }
    }

    public void download(final Request request) {
        if (request.getUri() != null && request.getTarget() != null) {
            final Target target = request.getTarget();
            try {
                final String key = toKey(request.getUri());
                Snapshot snapshot = this.diskLruCache.get(key);
                if (snapshot != null) {
                    snapshot.close();
                    this.handler.post(new Runnable() {
                        public void run() {
                            try {
                                target.onDownloadSucceeded(new DefaultDownloadResult(key));
                            } catch (Exception e) {
                                Log.e(Constants.TAG, "Exception", e);
                            }
                        }
                    });
                    return;
                }
                if (request.getPlaceholderResourceId() != null) {
                    target.onDownloadStarted(this.context.getResources().getDrawable(request.getPlaceholderResourceId().intValue()));
                } else {
                    target.onDownloadStarted(null);
                }
                onDownloadStarted();
                DownloadUtils.download(this.context, new URL(request.getUri().toString()), new DownloadUtils.Target() {
                    private Editor editor;

                    public void close() throws IOException {
                        if (this.editor != null) {
                            this.editor.commit();
                        }
                    }

                    public void setEtag(String str) throws IOException {
                        if (this.editor == null) {
                            this.editor = DefaultDownloadManager.this.diskLruCache.edit(key);
                        }
                        this.editor.set(1, str);
                    }

                    public OutputStream getOutputStream() throws IOException {
                        if (this.editor == null) {
                            this.editor = DefaultDownloadManager.this.diskLruCache.edit(key);
                        }
                        return this.editor.newOutputStream(0);
                    }
                }, false, new Callback() {
                    public void onDownloadSucceeded() {
                        DefaultDownloadManager.this.onDownloadFinished();
                        DefaultDownloadManager.this.handler.post(new Runnable() {
                            public void run() {
                                try {
                                    target.onDownloadSucceeded(new DefaultDownloadResult(key));
                                } catch (Exception e) {
                                    Log.e(Constants.TAG, "Exception", e);
                                }
                            }
                        });
                    }

                    public void onDownloadFailed() {
                        DefaultDownloadManager.this.onDownloadFinished();
                        DefaultDownloadManager.this.handler.post(new Runnable() {
                            public void run() {
                                try {
                                    if (request.getErrorResourceId() != null) {
                                        target.onDownloadFailed(DefaultDownloadManager.this.context.getResources().getDrawable(request.getErrorResourceId().intValue()));
                                    } else {
                                        target.onDownloadFailed(null);
                                    }
                                } catch (Exception e) {
                                    Log.e(Constants.TAG, "Exception", e);
                                }
                            }
                        });
                    }
                });
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public boolean isDownloading() {
        return this.downloadCount > 0;
    }

    public void addListener(Listener listener) {
        if (listener != null) {
            this.listeners.add(listener);
        }
    }

    public void removeListener(Listener listener) {
        if (listener != null) {
            this.listeners.remove(listener);
        }
    }

    private void fireDownloadsStarted() {
        for (Listener onDownloadsStarted : this.listeners) {
            try {
                onDownloadsStarted.onDownloadsStarted();
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    /* access modifiers changed from: private */
    public void fireDownloadsFinished() {
        for (Listener onDownloadsFinished : this.listeners) {
            try {
                onDownloadsFinished.onDownloadsFinished();
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }
}
