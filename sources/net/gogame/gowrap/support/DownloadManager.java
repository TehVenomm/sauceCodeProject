package net.gogame.gowrap.support;

import android.graphics.drawable.Drawable;
import android.net.Uri;
import java.io.Closeable;
import java.io.IOException;
import java.io.InputStream;

public interface DownloadManager {

    public interface DownloadResult extends Closeable {
        InputStream getInputStream() throws IOException;
    }

    public interface Listener {
        void onDownloadsFinished();

        void onDownloadsStarted();
    }

    public static class Request {
        private Integer errorResourceId;
        private Integer placeholderResourceId;
        private Target target;
        private Uri uri;

        public static class Builder {
            private final Request request = new Request();

            public Builder(String str) {
                this.request.setUri(Uri.parse(str));
            }

            public static Builder newBuilder(String str) {
                return new Builder(str);
            }

            public Builder placeHolder(int i) {
                this.request.setPlaceholderResourceId(Integer.valueOf(i));
                return this;
            }

            public Builder error(int i) {
                this.request.setErrorResourceId(Integer.valueOf(i));
                return this;
            }

            public Request into(Target target) {
                this.request.setTarget(target);
                return this.request;
            }
        }

        public Uri getUri() {
            return this.uri;
        }

        public void setUri(Uri uri2) {
            this.uri = uri2;
        }

        public Integer getPlaceholderResourceId() {
            return this.placeholderResourceId;
        }

        public void setPlaceholderResourceId(Integer num) {
            this.placeholderResourceId = num;
        }

        public Integer getErrorResourceId() {
            return this.errorResourceId;
        }

        public void setErrorResourceId(Integer num) {
            this.errorResourceId = num;
        }

        public Target getTarget() {
            return this.target;
        }

        public void setTarget(Target target2) {
            this.target = target2;
        }
    }

    public interface Target {
        void onDownloadFailed(Drawable drawable);

        void onDownloadStarted(Drawable drawable);

        void onDownloadSucceeded(DownloadResult downloadResult);
    }

    void addListener(Listener listener);

    void download(Request request);

    boolean isDownloading();

    void removeListener(Listener listener);
}
