package com.github.droidfu.imageloader;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.os.Handler;
import android.os.Message;
import android.widget.ImageView;

public class ImageLoaderHandler extends Handler {
    private Drawable errorDrawable;
    private String imageUrl;
    private ImageView imageView;

    public ImageLoaderHandler(ImageView imageView, String str) {
        this.imageView = imageView;
        this.imageUrl = str;
    }

    public ImageLoaderHandler(ImageView imageView, String str, Drawable drawable) {
        this(imageView, str);
        this.errorDrawable = drawable;
    }

    public String getImageUrl() {
        return this.imageUrl;
    }

    public ImageView getImageView() {
        return this.imageView;
    }

    protected boolean handleImageLoaded(Bitmap bitmap, Message message) {
        if (!this.imageUrl.equals((String) this.imageView.getTag())) {
            return false;
        }
        if (bitmap == null && this.errorDrawable != null) {
            bitmap = ((BitmapDrawable) this.errorDrawable).getBitmap();
        }
        if (bitmap != null) {
            this.imageView.setImageBitmap(bitmap);
        }
        return true;
    }

    protected final void handleImageLoadedMessage(Message message) {
        handleImageLoaded((Bitmap) message.getData().getParcelable(ImageLoader.BITMAP_EXTRA), message);
    }

    public final void handleMessage(Message message) {
        if (message.what == 0) {
            handleImageLoadedMessage(message);
        }
    }

    public void setImageUrl(String str) {
        this.imageUrl = str;
    }

    public void setImageView(ImageView imageView) {
        this.imageView = imageView;
    }
}
