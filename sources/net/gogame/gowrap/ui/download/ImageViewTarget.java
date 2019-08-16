package net.gogame.gowrap.p019ui.download;

import android.graphics.drawable.Drawable;
import android.util.Log;
import android.widget.ImageView;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.p019ui.utils.ImageUtils;
import net.gogame.gowrap.support.DownloadManager.DownloadResult;
import net.gogame.gowrap.support.DownloadManager.Target;

/* renamed from: net.gogame.gowrap.ui.download.ImageViewTarget */
public class ImageViewTarget implements Target {
    private boolean cancelled;
    private ImageView imageView;

    public ImageViewTarget(ImageView imageView2) {
        this.imageView = imageView2;
    }

    public boolean isCancelled() {
        return this.cancelled;
    }

    public void setCancelled(boolean z) {
        this.cancelled = z;
    }

    public void onDownloadStarted(Drawable drawable) {
    }

    public void onDownloadSucceeded(DownloadResult downloadResult) {
        if (!this.cancelled) {
            try {
                this.imageView.setImageDrawable(ImageUtils.getSampledBitmapDrawable(this.imageView.getContext(), new DownloadResultSource(downloadResult), Integer.valueOf(this.imageView.getWidth()), Integer.valueOf(this.imageView.getHeight())));
            } catch (Throwable th) {
                Log.e(Constants.TAG, "Exception", th);
            }
        }
    }

    public void onDownloadFailed(Drawable drawable) {
    }
}
