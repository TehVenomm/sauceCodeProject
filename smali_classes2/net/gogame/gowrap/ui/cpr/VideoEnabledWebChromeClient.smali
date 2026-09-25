.class public Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;
.super Landroid/webkit/WebChromeClient;
.source "VideoEnabledWebChromeClient.java"

# interfaces
.implements Landroid/media/MediaPlayer$OnPreparedListener;
.implements Landroid/media/MediaPlayer$OnCompletionListener;
.implements Landroid/media/MediaPlayer$OnErrorListener;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient$ToggledFullscreenCallback;
    }
.end annotation


# instance fields
.field private activityNonVideoView:Landroid/view/View;

.field private activityVideoView:Landroid/view/ViewGroup;

.field private isVideoFullscreen:Z

.field private loadingView:Landroid/view/View;

.field private toggledFullscreenCallback:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient$ToggledFullscreenCallback;

.field private videoViewCallback:Landroid/webkit/WebChromeClient$CustomViewCallback;

.field private videoViewContainer:Landroid/widget/FrameLayout;

.field private webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 42
    invoke-direct {p0}, Landroid/webkit/WebChromeClient;-><init>()V

    return-void
.end method

.method public constructor <init>(Landroid/view/View;Landroid/view/ViewGroup;)V
    .locals 0

    .line 52
    invoke-direct {p0}, Landroid/webkit/WebChromeClient;-><init>()V

    .line 53
    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->activityNonVideoView:Landroid/view/View;

    .line 54
    iput-object p2, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->activityVideoView:Landroid/view/ViewGroup;

    const/4 p1, 0x0

    .line 55
    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->loadingView:Landroid/view/View;

    .line 56
    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    const/4 p1, 0x0

    .line 57
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->isVideoFullscreen:Z

    return-void
.end method

.method public constructor <init>(Landroid/view/View;Landroid/view/ViewGroup;Landroid/view/View;)V
    .locals 0

    .line 68
    invoke-direct {p0}, Landroid/webkit/WebChromeClient;-><init>()V

    .line 69
    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->activityNonVideoView:Landroid/view/View;

    .line 70
    iput-object p2, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->activityVideoView:Landroid/view/ViewGroup;

    .line 71
    iput-object p3, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->loadingView:Landroid/view/View;

    const/4 p1, 0x0

    .line 72
    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    const/4 p1, 0x0

    .line 73
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->isVideoFullscreen:Z

    return-void
.end method

.method public constructor <init>(Landroid/view/View;Landroid/view/ViewGroup;Landroid/view/View;Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;)V
    .locals 0

    .line 86
    invoke-direct {p0}, Landroid/webkit/WebChromeClient;-><init>()V

    .line 87
    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->activityNonVideoView:Landroid/view/View;

    .line 88
    iput-object p2, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->activityVideoView:Landroid/view/ViewGroup;

    .line 89
    iput-object p3, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->loadingView:Landroid/view/View;

    .line 90
    iput-object p4, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    const/4 p1, 0x0

    .line 91
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->isVideoFullscreen:Z

    return-void
.end method


# virtual methods
.method public getVideoLoadingProgressView()Landroid/view/View;
    .locals 2

    .line 211
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->loadingView:Landroid/view/View;

    if-eqz v0, :cond_0

    .line 212
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->loadingView:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 213
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->loadingView:Landroid/view/View;

    return-object v0

    .line 215
    :cond_0
    invoke-super {p0}, Landroid/webkit/WebChromeClient;->getVideoLoadingProgressView()Landroid/view/View;

    move-result-object v0

    return-object v0
.end method

.method public isVideoFullscreen()Z
    .locals 1

    .line 100
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->isVideoFullscreen:Z

    return v0
.end method

.method public onBackPressed()Z
    .locals 1

    .line 247
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->isVideoFullscreen:Z

    if-eqz v0, :cond_0

    .line 248
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->onHideCustomView()V

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public onCompletion(Landroid/media/MediaPlayer;)V
    .locals 0

    .line 230
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->onHideCustomView()V

    return-void
.end method

.method public onError(Landroid/media/MediaPlayer;II)Z
    .locals 0

    const/4 p1, 0x0

    return p1
.end method

.method public onHideCustomView()V
    .locals 3

    .line 185
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->isVideoFullscreen:Z

    if-eqz v0, :cond_1

    .line 187
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->activityVideoView:Landroid/view/ViewGroup;

    const/4 v1, 0x4

    invoke-virtual {v0, v1}, Landroid/view/ViewGroup;->setVisibility(I)V

    .line 188
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->activityVideoView:Landroid/view/ViewGroup;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->videoViewContainer:Landroid/widget/FrameLayout;

    invoke-virtual {v0, v1}, Landroid/view/ViewGroup;->removeView(Landroid/view/View;)V

    .line 189
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->activityNonVideoView:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 192
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->videoViewCallback:Landroid/webkit/WebChromeClient$CustomViewCallback;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->videoViewCallback:Landroid/webkit/WebChromeClient$CustomViewCallback;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    const-string v2, ".chromium."

    invoke-virtual {v0, v2}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 193
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->videoViewCallback:Landroid/webkit/WebChromeClient$CustomViewCallback;

    invoke-interface {v0}, Landroid/webkit/WebChromeClient$CustomViewCallback;->onCustomViewHidden()V

    .line 197
    :cond_0
    iput-boolean v1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->isVideoFullscreen:Z

    const/4 v0, 0x0

    .line 198
    iput-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->videoViewContainer:Landroid/widget/FrameLayout;

    .line 199
    iput-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->videoViewCallback:Landroid/webkit/WebChromeClient$CustomViewCallback;

    .line 202
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->toggledFullscreenCallback:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient$ToggledFullscreenCallback;

    if-eqz v0, :cond_1

    .line 203
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->toggledFullscreenCallback:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient$ToggledFullscreenCallback;

    invoke-interface {v0, v1}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient$ToggledFullscreenCallback;->toggledFullscreen(Z)V

    :cond_1
    return-void
.end method

.method public onPrepared(Landroid/media/MediaPlayer;)V
    .locals 1

    .line 222
    iget-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->loadingView:Landroid/view/View;

    if-eqz p1, :cond_0

    .line 223
    iget-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->loadingView:Landroid/view/View;

    const/16 v0, 0x8

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    :cond_0
    return-void
.end method

.method public onShowCustomView(Landroid/view/View;ILandroid/webkit/WebChromeClient$CustomViewCallback;)V
    .locals 0

    .line 177
    invoke-virtual {p0, p1, p3}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->onShowCustomView(Landroid/view/View;Landroid/webkit/WebChromeClient$CustomViewCallback;)V

    return-void
.end method

.method public onShowCustomView(Landroid/view/View;Landroid/webkit/WebChromeClient$CustomViewCallback;)V
    .locals 4

    .line 115
    instance-of v0, p1, Landroid/widget/FrameLayout;

    if-eqz v0, :cond_2

    .line 117
    check-cast p1, Landroid/widget/FrameLayout;

    .line 118
    invoke-virtual {p1}, Landroid/widget/FrameLayout;->getFocusedChild()Landroid/view/View;

    move-result-object v0

    const/4 v1, 0x1

    .line 121
    iput-boolean v1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->isVideoFullscreen:Z

    .line 122
    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->videoViewContainer:Landroid/widget/FrameLayout;

    .line 123
    iput-object p2, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->videoViewCallback:Landroid/webkit/WebChromeClient$CustomViewCallback;

    .line 126
    iget-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->activityNonVideoView:Landroid/view/View;

    const/4 p2, 0x4

    invoke-virtual {p1, p2}, Landroid/view/View;->setVisibility(I)V

    .line 127
    iget-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->activityVideoView:Landroid/view/ViewGroup;

    iget-object p2, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->videoViewContainer:Landroid/widget/FrameLayout;

    new-instance v2, Landroid/view/ViewGroup$LayoutParams;

    const/4 v3, -0x1

    invoke-direct {v2, v3, v3}, Landroid/view/ViewGroup$LayoutParams;-><init>(II)V

    invoke-virtual {p1, p2, v2}, Landroid/view/ViewGroup;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    .line 128
    iget-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->activityVideoView:Landroid/view/ViewGroup;

    const/4 p2, 0x0

    invoke-virtual {p1, p2}, Landroid/view/ViewGroup;->setVisibility(I)V

    .line 130
    instance-of p1, v0, Landroid/widget/VideoView;

    if-eqz p1, :cond_0

    .line 132
    check-cast v0, Landroid/widget/VideoView;

    .line 135
    invoke-virtual {v0, p0}, Landroid/widget/VideoView;->setOnPreparedListener(Landroid/media/MediaPlayer$OnPreparedListener;)V

    .line 136
    invoke-virtual {v0, p0}, Landroid/widget/VideoView;->setOnCompletionListener(Landroid/media/MediaPlayer$OnCompletionListener;)V

    .line 137
    invoke-virtual {v0, p0}, Landroid/widget/VideoView;->setOnErrorListener(Landroid/media/MediaPlayer$OnErrorListener;)V

    goto/16 :goto_0

    .line 146
    :cond_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    if-eqz p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object p1

    invoke-virtual {p1}, Landroid/webkit/WebSettings;->getJavaScriptEnabled()Z

    move-result p1

    if-eqz p1, :cond_1

    instance-of p1, v0, Landroid/view/SurfaceView;

    if-eqz p1, :cond_1

    const-string p1, "javascript:"

    .line 149
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "var _ytrp_html5_video_last;"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 150
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "var _ytrp_html5_video = document.getElementsByTagName(\'video\')[0];"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 151
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "if (_ytrp_html5_video != undefined && _ytrp_html5_video != _ytrp_html5_video_last) {"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 153
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "_ytrp_html5_video_last = _ytrp_html5_video;"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 154
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "function _ytrp_html5_video_ended() {"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 156
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "_VideoEnabledWebView.notifyVideoEnd();"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 158
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "}"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 159
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "_ytrp_html5_video.addEventListener(\'ended\', _ytrp_html5_video_ended);"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 161
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "}"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 162
    iget-object p2, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {p2, p1}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->loadUrl(Ljava/lang/String;)V

    .line 167
    :cond_1
    :goto_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->toggledFullscreenCallback:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient$ToggledFullscreenCallback;

    if-eqz p1, :cond_2

    .line 168
    iget-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->toggledFullscreenCallback:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient$ToggledFullscreenCallback;

    invoke-interface {p1, v1}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient$ToggledFullscreenCallback;->toggledFullscreen(Z)V

    :cond_2
    return-void
.end method

.method public setOnToggledFullscreen(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient$ToggledFullscreenCallback;)V
    .locals 0

    .line 110
    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->toggledFullscreenCallback:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient$ToggledFullscreenCallback;

    return-void
.end method
