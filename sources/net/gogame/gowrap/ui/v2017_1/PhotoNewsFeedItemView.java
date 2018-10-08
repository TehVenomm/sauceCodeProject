package net.gogame.gowrap.ui.v2017_1;

import android.annotation.TargetApi;
import android.content.Context;
import android.graphics.drawable.Drawable;
import android.util.AttributeSet;
import android.util.Log;
import android.widget.ImageView;
import net.gogame.gowrap.C1426R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.support.DownloadManager.DownloadResult;
import net.gogame.gowrap.support.DownloadManager.Target;
import net.gogame.gowrap.ui.download.DownloadResultSource;
import net.gogame.gowrap.ui.utils.ImageUtils;

public class PhotoNewsFeedItemView extends AbstractNewsFeedItemView implements Target {
    private DownloadResult downloadResult;
    private ImageView imageView;

    public PhotoNewsFeedItemView(Context context) {
        super(context);
    }

    public PhotoNewsFeedItemView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
    }

    public PhotoNewsFeedItemView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
    }

    @TargetApi(21)
    public PhotoNewsFeedItemView(Context context, AttributeSet attributeSet, int i, int i2) {
        super(context, attributeSet, i, i2);
    }

    protected void customInit(Context context) {
        this.imageView = (ImageView) findViewById(C1426R.id.net_gogame_gowrap_newsfeed_item_photo);
    }

    protected int getViewResourceId() {
        return C1426R.layout.net_gogame_gowrap_newsfeed_photo_item;
    }

    protected Integer getResizingViewResourceId() {
        return Integer.valueOf(C1426R.id.net_gogame_gowrap_newsfeed_item_photo);
    }

    protected int[] getClickResourceIds() {
        return new int[]{C1426R.id.net_gogame_gowrap_newsfeed_item_media, C1426R.id.net_gogame_gowrap_newsfeed_item_content};
    }

    protected int[] getButtonClickResourceIds() {
        return new int[]{C1426R.id.net_gogame_gowrap_newsfeed_item_button};
    }

    protected void onLayoutCompleted() {
        update();
    }

    private void update() {
        if (isLayoutCompleted() && this.downloadResult != null) {
            try {
                this.imageView.setImageDrawable(ImageUtils.getSampledBitmapDrawable(this.imageView.getContext(), new DownloadResultSource(this.downloadResult), Integer.valueOf(this.imageView.getWidth()), Integer.valueOf(this.imageView.getHeight())));
            } catch (Throwable th) {
                Log.e(Constants.TAG, "Exception", th);
            }
        }
    }

    public void setImage(Drawable drawable) {
        this.imageView.setImageDrawable(drawable);
    }

    public void onDownloadStarted(Drawable drawable) {
        if (drawable != null) {
            this.imageView.setImageDrawable(drawable);
        }
    }

    public void onDownloadSucceeded(DownloadResult downloadResult) {
        this.downloadResult = downloadResult;
        update();
    }

    public void onDownloadFailed(Drawable drawable) {
        this.imageView.setImageDrawable(drawable);
    }
}
