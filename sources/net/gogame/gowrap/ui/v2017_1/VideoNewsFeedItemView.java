package net.gogame.gowrap.p019ui.v2017_1;

import android.annotation.TargetApi;
import android.content.Context;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.util.AttributeSet;
import android.util.Log;
import android.widget.MediaController;
import android.widget.VideoView;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.p019ui.download.DownloadResultSource;
import net.gogame.gowrap.p019ui.utils.ImageUtils;
import net.gogame.gowrap.support.DownloadManager.DownloadResult;
import net.gogame.gowrap.support.DownloadManager.Target;

/* renamed from: net.gogame.gowrap.ui.v2017_1.VideoNewsFeedItemView */
public class VideoNewsFeedItemView extends AbstractNewsFeedItemView implements Target {
    private DownloadResult downloadResult;
    private Uri videoUri;
    private VideoView videoView;

    public VideoNewsFeedItemView(Context context) {
        super(context);
    }

    public VideoNewsFeedItemView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
    }

    public VideoNewsFeedItemView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
    }

    @TargetApi(21)
    public VideoNewsFeedItemView(Context context, AttributeSet attributeSet, int i, int i2) {
        super(context, attributeSet, i, i2);
        customInit(context);
    }

    /* access modifiers changed from: protected */
    public void customInit(Context context) {
        this.videoView = (VideoView) findViewById(C1423R.C1424id.net_gogame_gowrap_newsfeed_item_video);
        MediaController mediaController = new MediaController(context);
        mediaController.setAnchorView(this);
        this.videoView.setMediaController(mediaController);
    }

    /* access modifiers changed from: protected */
    public int getViewResourceId() {
        return C1423R.C1425layout.net_gogame_gowrap_newsfeed_video_item;
    }

    /* access modifiers changed from: protected */
    public Integer getResizingViewResourceId() {
        return Integer.valueOf(C1423R.C1424id.net_gogame_gowrap_newsfeed_item_video);
    }

    /* access modifiers changed from: protected */
    public int[] getClickResourceIds() {
        return new int[]{C1423R.C1424id.net_gogame_gowrap_newsfeed_item_content};
    }

    /* access modifiers changed from: protected */
    public int[] getButtonClickResourceIds() {
        return new int[]{C1423R.C1424id.net_gogame_gowrap_newsfeed_item_button};
    }

    /* access modifiers changed from: protected */
    public void onLayoutCompleted() {
        update();
    }

    private void update() {
        if (isLayoutCompleted() && this.downloadResult != null) {
            try {
                this.videoView.setBackgroundDrawable(ImageUtils.getSampledBitmapDrawable(this.videoView.getContext(), new DownloadResultSource(this.downloadResult), Integer.valueOf(this.videoView.getWidth()), Integer.valueOf(this.videoView.getHeight())));
            } catch (Throwable th) {
                Log.e(Constants.TAG, "Exception", th);
            }
        }
    }

    public void setPreviewImage(Drawable drawable) {
        this.videoView.setBackgroundDrawable(drawable);
    }

    public void setVideo(Uri uri) {
        this.videoUri = uri;
    }

    public void onDownloadStarted(Drawable drawable) {
        if (drawable != null) {
            this.videoView.setBackgroundDrawable(drawable);
        }
    }

    public void onDownloadSucceeded(DownloadResult downloadResult2) {
        this.downloadResult = downloadResult2;
        update();
    }

    public void onDownloadFailed(Drawable drawable) {
        this.videoView.setBackgroundDrawable(drawable);
    }
}
