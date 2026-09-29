.class public interface abstract Lnet/gogame/gowrap/support/DownloadManager;
.super Ljava/lang/Object;
.source "DownloadManager.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/support/DownloadManager$Request;,
        Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;,
        Lnet/gogame/gowrap/support/DownloadManager$Target;,
        Lnet/gogame/gowrap/support/DownloadManager$Listener;
    }
.end annotation


# virtual methods
.method public abstract addListener(Lnet/gogame/gowrap/support/DownloadManager$Listener;)V
.end method

.method public abstract download(Lnet/gogame/gowrap/support/DownloadManager$Request;)V
.end method

.method public abstract isDownloading()Z
.end method

.method public abstract removeListener(Lnet/gogame/gowrap/support/DownloadManager$Listener;)V
.end method
