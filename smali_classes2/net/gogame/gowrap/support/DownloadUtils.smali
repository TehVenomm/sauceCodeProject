.class public final Lnet/gogame/gowrap/support/DownloadUtils;
.super Ljava/lang/Object;
.source "DownloadUtils.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;,
        Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;,
        Lnet/gogame/gowrap/support/DownloadUtils$Target;,
        Lnet/gogame/gowrap/support/DownloadUtils$Callback;
    }
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static download(Landroid/content/Context;Ljava/net/URL;Lnet/gogame/gowrap/support/DownloadUtils$Target;ZLnet/gogame/gowrap/support/DownloadUtils$Callback;)V
    .locals 7

    .line 30
    new-instance v6, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;

    move-object v0, v6

    move-object v1, p0

    move-object v2, p1

    move-object v3, p2

    move v4, p3

    move-object v5, p4

    invoke-direct/range {v0 .. v5}, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;-><init>(Landroid/content/Context;Ljava/net/URL;Lnet/gogame/gowrap/support/DownloadUtils$Target;ZLnet/gogame/gowrap/support/DownloadUtils$Callback;)V

    const/4 p0, 0x0

    new-array p0, p0, [Ljava/lang/Void;

    invoke-virtual {v6, p0}, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    return-void
.end method
