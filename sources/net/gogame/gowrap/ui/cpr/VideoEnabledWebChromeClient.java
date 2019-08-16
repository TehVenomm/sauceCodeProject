package net.gogame.gowrap.p019ui.cpr;

import android.media.MediaPlayer;
import android.media.MediaPlayer.OnCompletionListener;
import android.media.MediaPlayer.OnErrorListener;
import android.media.MediaPlayer.OnPreparedListener;
import android.view.SurfaceView;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.webkit.WebChromeClient;
import android.webkit.WebChromeClient.CustomViewCallback;
import android.widget.FrameLayout;
import android.widget.VideoView;

/* renamed from: net.gogame.gowrap.ui.cpr.VideoEnabledWebChromeClient */
public class VideoEnabledWebChromeClient extends WebChromeClient implements OnPreparedListener, OnCompletionListener, OnErrorListener {
    private View activityNonVideoView;
    private ViewGroup activityVideoView;
    private boolean isVideoFullscreen;
    private View loadingView;
    private ToggledFullscreenCallback toggledFullscreenCallback;
    private CustomViewCallback videoViewCallback;
    private FrameLayout videoViewContainer;
    private VideoEnabledWebView webView;

    /* renamed from: net.gogame.gowrap.ui.cpr.VideoEnabledWebChromeClient$ToggledFullscreenCallback */
    public interface ToggledFullscreenCallback {
        void toggledFullscreen(boolean z);
    }

    public VideoEnabledWebChromeClient() {
    }

    public VideoEnabledWebChromeClient(View view, ViewGroup viewGroup) {
        this.activityNonVideoView = view;
        this.activityVideoView = viewGroup;
        this.loadingView = null;
        this.webView = null;
        this.isVideoFullscreen = false;
    }

    public VideoEnabledWebChromeClient(View view, ViewGroup viewGroup, View view2) {
        this.activityNonVideoView = view;
        this.activityVideoView = viewGroup;
        this.loadingView = view2;
        this.webView = null;
        this.isVideoFullscreen = false;
    }

    public VideoEnabledWebChromeClient(View view, ViewGroup viewGroup, View view2, VideoEnabledWebView videoEnabledWebView) {
        this.activityNonVideoView = view;
        this.activityVideoView = viewGroup;
        this.loadingView = view2;
        this.webView = videoEnabledWebView;
        this.isVideoFullscreen = false;
    }

    public boolean isVideoFullscreen() {
        return this.isVideoFullscreen;
    }

    public void setOnToggledFullscreen(ToggledFullscreenCallback toggledFullscreenCallback2) {
        this.toggledFullscreenCallback = toggledFullscreenCallback2;
    }

    public void onShowCustomView(View view, CustomViewCallback customViewCallback) {
        if (view instanceof FrameLayout) {
            FrameLayout frameLayout = (FrameLayout) view;
            View focusedChild = frameLayout.getFocusedChild();
            this.isVideoFullscreen = true;
            this.videoViewContainer = frameLayout;
            this.videoViewCallback = customViewCallback;
            this.activityNonVideoView.setVisibility(4);
            this.activityVideoView.addView(this.videoViewContainer, new LayoutParams(-1, -1));
            this.activityVideoView.setVisibility(0);
            if (focusedChild instanceof VideoView) {
                VideoView videoView = (VideoView) focusedChild;
                videoView.setOnPreparedListener(this);
                videoView.setOnCompletionListener(this);
                videoView.setOnErrorListener(this);
            } else if (this.webView != null && this.webView.getSettings().getJavaScriptEnabled() && (focusedChild instanceof SurfaceView)) {
                this.webView.loadUrl((((((((("javascript:" + "var _ytrp_html5_video_last;") + "var _ytrp_html5_video = document.getElementsByTagName('video')[0];") + "if (_ytrp_html5_video != undefined && _ytrp_html5_video != _ytrp_html5_video_last) {") + "_ytrp_html5_video_last = _ytrp_html5_video;") + "function _ytrp_html5_video_ended() {") + "_VideoEnabledWebView.notifyVideoEnd();") + "}") + "_ytrp_html5_video.addEventListener('ended', _ytrp_html5_video_ended);") + "}");
            }
            if (this.toggledFullscreenCallback != null) {
                this.toggledFullscreenCallback.toggledFullscreen(true);
            }
        }
    }

    public void onShowCustomView(View view, int i, CustomViewCallback customViewCallback) {
        onShowCustomView(view, customViewCallback);
    }

    public void onHideCustomView() {
        if (this.isVideoFullscreen) {
            this.activityVideoView.setVisibility(4);
            this.activityVideoView.removeView(this.videoViewContainer);
            this.activityNonVideoView.setVisibility(0);
            if (this.videoViewCallback != null && !this.videoViewCallback.getClass().getName().contains(".chromium.")) {
                this.videoViewCallback.onCustomViewHidden();
            }
            this.isVideoFullscreen = false;
            this.videoViewContainer = null;
            this.videoViewCallback = null;
            if (this.toggledFullscreenCallback != null) {
                this.toggledFullscreenCallback.toggledFullscreen(false);
            }
        }
    }

    public View getVideoLoadingProgressView() {
        if (this.loadingView == null) {
            return super.getVideoLoadingProgressView();
        }
        this.loadingView.setVisibility(0);
        return this.loadingView;
    }

    public void onPrepared(MediaPlayer mediaPlayer) {
        if (this.loadingView != null) {
            this.loadingView.setVisibility(8);
        }
    }

    public void onCompletion(MediaPlayer mediaPlayer) {
        onHideCustomView();
    }

    public boolean onError(MediaPlayer mediaPlayer, int i, int i2) {
        return false;
    }

    public boolean onBackPressed() {
        if (!this.isVideoFullscreen) {
            return false;
        }
        onHideCustomView();
        return true;
    }
}
