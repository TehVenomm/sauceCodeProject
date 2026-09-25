.class public Lnet/gogame/gowrap/support/DownloadManager$Request;
.super Ljava/lang/Object;
.source "DownloadManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/support/DownloadManager;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Request"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;
    }
.end annotation


# instance fields
.field private errorResourceId:Ljava/lang/Integer;

.field private placeholderResourceId:Ljava/lang/Integer;

.field private target:Lnet/gogame/gowrap/support/DownloadManager$Target;

.field private uri:Landroid/net/Uri;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 41
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getErrorResourceId()Ljava/lang/Integer;
    .locals 1

    .line 65
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadManager$Request;->errorResourceId:Ljava/lang/Integer;

    return-object v0
.end method

.method public getPlaceholderResourceId()Ljava/lang/Integer;
    .locals 1

    .line 57
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadManager$Request;->placeholderResourceId:Ljava/lang/Integer;

    return-object v0
.end method

.method public getTarget()Lnet/gogame/gowrap/support/DownloadManager$Target;
    .locals 1

    .line 73
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadManager$Request;->target:Lnet/gogame/gowrap/support/DownloadManager$Target;

    return-object v0
.end method

.method public getUri()Landroid/net/Uri;
    .locals 1

    .line 49
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadManager$Request;->uri:Landroid/net/Uri;

    return-object v0
.end method

.method public setErrorResourceId(Ljava/lang/Integer;)V
    .locals 0

    .line 69
    iput-object p1, p0, Lnet/gogame/gowrap/support/DownloadManager$Request;->errorResourceId:Ljava/lang/Integer;

    return-void
.end method

.method public setPlaceholderResourceId(Ljava/lang/Integer;)V
    .locals 0

    .line 61
    iput-object p1, p0, Lnet/gogame/gowrap/support/DownloadManager$Request;->placeholderResourceId:Ljava/lang/Integer;

    return-void
.end method

.method public setTarget(Lnet/gogame/gowrap/support/DownloadManager$Target;)V
    .locals 0

    .line 77
    iput-object p1, p0, Lnet/gogame/gowrap/support/DownloadManager$Request;->target:Lnet/gogame/gowrap/support/DownloadManager$Target;

    return-void
.end method

.method public setUri(Landroid/net/Uri;)V
    .locals 0

    .line 53
    iput-object p1, p0, Lnet/gogame/gowrap/support/DownloadManager$Request;->uri:Landroid/net/Uri;

    return-void
.end method
