package com.github.droidfu.widgets;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.drawable.AnimationDrawable;
import android.graphics.drawable.Drawable;
import android.os.Message;
import android.util.AttributeSet;
import android.view.ViewGroup.LayoutParams;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.ProgressBar;
import android.widget.ViewSwitcher;
import com.github.droidfu.DroidFu;
import com.github.droidfu.imageloader.ImageLoader;
import com.github.droidfu.imageloader.ImageLoaderHandler;

public class WebImageView extends ViewSwitcher {
    private Drawable errorDrawable;
    private String imageUrl;
    private ImageView imageView;
    private boolean isLoaded;
    private ProgressBar loadingSpinner;
    private Drawable progressDrawable;
    private ScaleType scaleType = ScaleType.CENTER_CROP;

    private class DefaultImageLoaderHandler extends ImageLoaderHandler {
        public DefaultImageLoaderHandler() {
            super(WebImageView.this.imageView, WebImageView.this.imageUrl, WebImageView.this.errorDrawable);
        }

        protected boolean handleImageLoaded(Bitmap bitmap, Message message) {
            boolean handleImageLoaded = super.handleImageLoaded(bitmap, message);
            if (handleImageLoaded) {
                WebImageView.this.isLoaded = true;
                WebImageView.this.setDisplayedChild(1);
            }
            return handleImageLoaded;
        }
    }

    public WebImageView(Context context, AttributeSet attributeSet) {
        Drawable drawable = null;
        super(context, attributeSet);
        int attributeResourceValue = attributeSet.getAttributeResourceValue(DroidFu.XMLNS, "progressDrawable", 0);
        int attributeResourceValue2 = attributeSet.getAttributeResourceValue(DroidFu.XMLNS, "errorDrawable", 0);
        Drawable drawable2 = attributeResourceValue > 0 ? context.getResources().getDrawable(attributeResourceValue) : null;
        if (attributeResourceValue2 > 0) {
            drawable = context.getResources().getDrawable(attributeResourceValue2);
        }
        initialize(context, attributeSet.getAttributeValue(DroidFu.XMLNS, "imageUrl"), drawable2, drawable, attributeSet.getAttributeBooleanValue(DroidFu.XMLNS, "autoLoad", true));
    }

    public WebImageView(Context context, String str, Drawable drawable, Drawable drawable2, boolean z) {
        super(context);
        initialize(context, str, drawable, drawable2, z);
    }

    public WebImageView(Context context, String str, Drawable drawable, boolean z) {
        super(context);
        initialize(context, str, drawable, null, z);
    }

    public WebImageView(Context context, String str, boolean z) {
        super(context);
        initialize(context, str, null, null, z);
    }

    private void addImageView(Context context) {
        this.imageView = new ImageView(context);
        this.imageView.setScaleType(this.scaleType);
        LayoutParams layoutParams = new FrameLayout.LayoutParams(-2, -2);
        layoutParams.gravity = 17;
        addView(this.imageView, 1, layoutParams);
    }

    private void addLoadingSpinnerView(Context context) {
        this.loadingSpinner = new ProgressBar(context);
        this.loadingSpinner.setIndeterminate(true);
        if (this.progressDrawable == null) {
            this.progressDrawable = this.loadingSpinner.getIndeterminateDrawable();
        } else {
            this.loadingSpinner.setIndeterminateDrawable(this.progressDrawable);
            if (this.progressDrawable instanceof AnimationDrawable) {
                ((AnimationDrawable) this.progressDrawable).start();
            }
        }
        LayoutParams layoutParams = new FrameLayout.LayoutParams(this.progressDrawable.getIntrinsicWidth(), this.progressDrawable.getIntrinsicHeight());
        layoutParams.gravity = 17;
        addView(this.loadingSpinner, 0, layoutParams);
    }

    private void initialize(Context context, String str, Drawable drawable, Drawable drawable2, boolean z) {
        this.imageUrl = str;
        this.progressDrawable = drawable;
        this.errorDrawable = drawable2;
        ImageLoader.initialize(context);
        addLoadingSpinnerView(context);
        addImageView(context);
        if (z && str != null) {
            loadImage();
        }
    }

    public boolean isLoaded() {
        return this.isLoaded;
    }

    public void loadImage() {
        if (this.imageUrl == null) {
            throw new IllegalStateException("image URL is null; did you forget to set it for this view?");
        }
        ImageLoader.start(this.imageUrl, new DefaultImageLoaderHandler());
    }

    public void reset() {
        super.reset();
        setDisplayedChild(0);
    }

    public void setImageUrl(String str) {
        this.imageUrl = str;
    }

    public void setNoImageDrawable(int i) {
        this.imageView.setImageDrawable(getContext().getResources().getDrawable(i));
        setDisplayedChild(1);
    }
}
