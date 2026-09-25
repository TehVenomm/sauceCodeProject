.class public Lcom/helpshift/downloader/AdminFileInfo;
.super Ljava/lang/Object;
.source "AdminFileInfo.java"


# instance fields
.field public final contentType:Ljava/lang/String;

.field public final fileName:Ljava/lang/String;

.field public final isSecureAttachment:Z

.field public final url:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)V
    .locals 0

    .line 10
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 11
    iput-object p1, p0, Lcom/helpshift/downloader/AdminFileInfo;->url:Ljava/lang/String;

    .line 12
    iput-object p2, p0, Lcom/helpshift/downloader/AdminFileInfo;->fileName:Ljava/lang/String;

    .line 13
    iput-object p3, p0, Lcom/helpshift/downloader/AdminFileInfo;->contentType:Ljava/lang/String;

    .line 14
    iput-boolean p4, p0, Lcom/helpshift/downloader/AdminFileInfo;->isSecureAttachment:Z

    return-void
.end method
