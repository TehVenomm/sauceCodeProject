.class Lcom/helpshift/common/platform/AndroidSupportDownloader$3;
.super Ljava/lang/Object;
.source "AndroidSupportDownloader.java"

# interfaces
.implements Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/platform/AndroidSupportDownloader;->startDownload(Lcom/helpshift/downloader/AdminFileInfo;Lcom/helpshift/downloader/SupportDownloader$StorageDirType;Lcom/helpshift/common/domain/network/AuthDataProvider;Lcom/helpshift/downloader/SupportDownloadStateChangeListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/platform/AndroidSupportDownloader;


# direct methods
.method constructor <init>(Lcom/helpshift/common/platform/AndroidSupportDownloader;)V
    .locals 0

    .line 109
    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader$3;->this$0:Lcom/helpshift/common/platform/AndroidSupportDownloader;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onDownloadFinish(ZLjava/lang/String;Ljava/lang/Object;)V
    .locals 0

    if-eqz p1, :cond_0

    .line 113
    invoke-virtual {p3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    .line 114
    iget-object p3, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader$3;->this$0:Lcom/helpshift/common/platform/AndroidSupportDownloader;

    invoke-virtual {p3, p2, p1}, Lcom/helpshift/common/platform/AndroidSupportDownloader;->handleDownloadSuccess(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 117
    :cond_0
    iget-object p1, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader$3;->this$0:Lcom/helpshift/common/platform/AndroidSupportDownloader;

    invoke-virtual {p1, p2}, Lcom/helpshift/common/platform/AndroidSupportDownloader;->handleDownloadFailure(Ljava/lang/String;)V

    :goto_0
    return-void
.end method
