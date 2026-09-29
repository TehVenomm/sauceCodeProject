.class public interface abstract Lnet/gogame/gowrap/support/DownloadManager$Target;
.super Ljava/lang/Object;
.source "DownloadManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/support/DownloadManager;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "Target"
.end annotation


# virtual methods
.method public abstract onDownloadFailed(Landroid/graphics/drawable/Drawable;)V
.end method

.method public abstract onDownloadStarted(Landroid/graphics/drawable/Drawable;)V
.end method

.method public abstract onDownloadSucceeded(Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;)V
.end method
