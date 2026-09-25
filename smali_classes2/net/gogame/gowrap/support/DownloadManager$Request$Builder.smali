.class public Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;
.super Ljava/lang/Object;
.source "DownloadManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/support/DownloadManager$Request;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Builder"
.end annotation


# instance fields
.field private final request:Lnet/gogame/gowrap/support/DownloadManager$Request;


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 1

    .line 85
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 82
    new-instance v0, Lnet/gogame/gowrap/support/DownloadManager$Request;

    invoke-direct {v0}, Lnet/gogame/gowrap/support/DownloadManager$Request;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->request:Lnet/gogame/gowrap/support/DownloadManager$Request;

    .line 87
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->request:Lnet/gogame/gowrap/support/DownloadManager$Request;

    invoke-static {p1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p1

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/support/DownloadManager$Request;->setUri(Landroid/net/Uri;)V

    return-void
.end method

.method public static newBuilder(Ljava/lang/String;)Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;
    .locals 1

    .line 91
    new-instance v0, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;-><init>(Ljava/lang/String;)V

    return-object v0
.end method


# virtual methods
.method public error(I)Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;
    .locals 1

    .line 100
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->request:Lnet/gogame/gowrap/support/DownloadManager$Request;

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/support/DownloadManager$Request;->setErrorResourceId(Ljava/lang/Integer;)V

    return-object p0
.end method

.method public into(Lnet/gogame/gowrap/support/DownloadManager$Target;)Lnet/gogame/gowrap/support/DownloadManager$Request;
    .locals 1

    .line 105
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->request:Lnet/gogame/gowrap/support/DownloadManager$Request;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/support/DownloadManager$Request;->setTarget(Lnet/gogame/gowrap/support/DownloadManager$Target;)V

    .line 106
    iget-object p1, p0, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->request:Lnet/gogame/gowrap/support/DownloadManager$Request;

    return-object p1
.end method

.method public placeHolder(I)Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;
    .locals 1

    .line 95
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->request:Lnet/gogame/gowrap/support/DownloadManager$Request;

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/support/DownloadManager$Request;->setPlaceholderResourceId(Ljava/lang/Integer;)V

    return-object p0
.end method
