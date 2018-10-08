package net.gogame.gowrap.ui.download;

import android.graphics.drawable.Drawable;
import android.util.Log;
import android.widget.ImageView;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.support.DownloadManager.DownloadResult;
import net.gogame.gowrap.support.DownloadManager.Target;
import net.gogame.gowrap.ui.utils.ImageUtils;

public class ImageViewTarget implements Target {
    private boolean cancelled;
    private ImageView imageView;

    public ImageViewTarget(ImageView imageView) {
        this.imageView = imageView;
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
