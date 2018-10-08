package net.gogame.chat;

import android.graphics.Bitmap;
import android.graphics.drawable.Drawable;
import com.squareup.picasso.Picasso.LoadedFrom;
import com.squareup.picasso.Target;

public class ZoomableImageViewTarget implements Target {
    private final ZoomableImageView imageView;

    public ZoomableImageViewTarget(ZoomableImageView zoomableImageView) {
        this.imageView = zoomableImageView;
    }

    public void onBitmapLoaded(Bitmap bitmap, LoadedFrom loadedFrom) {
        this.imageView.setImageBitmap(bitmap);
    }

    public void onBitmapFailed(Drawable drawable) {
        this.imageView.setImageDrawable(drawable);
    }

    public void onPrepareLoad(Drawable drawable) {
        this.imageView.setImageDrawable(drawable);
    }
}
