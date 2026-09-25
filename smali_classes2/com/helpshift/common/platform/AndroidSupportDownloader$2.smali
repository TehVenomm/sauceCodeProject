.class Lcom/helpshift/common/platform/AndroidSupportDownloader$2;
.super Ljava/lang/Object;
.source "AndroidSupportDownloader.java"

# interfaces
.implements Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;


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

    .line 103
    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader$2;->this$0:Lcom/helpshift/common/platform/AndroidSupportDownloader;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onProgressChanged(Ljava/lang/String;I)V
    .locals 1

    .line 106
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader$2;->this$0:Lcom/helpshift/common/platform/AndroidSupportDownloader;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/platform/AndroidSupportDownloader;->handleProgressChange(Ljava/lang/String;I)V

    return-void
.end method
