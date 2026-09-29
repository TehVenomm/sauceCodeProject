.class public interface abstract Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;
.super Ljava/lang/Object;
.source "DownloadManager.java"

# interfaces
.implements Ljava/io/Closeable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/support/DownloadManager;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "DownloadResult"
.end annotation


# virtual methods
.method public abstract getInputStream()Ljava/io/InputStream;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation
.end method
