.class public Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;
.super Ljava/lang/Object;
.source "DownloadConfig.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/android/commons/downloader/DownloadConfig;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Builder"
.end annotation


# instance fields
.field private downloadDirType:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

.field private externalStorageDirectoryPath:Ljava/lang/String;

.field private isNoMedia:Z

.field private useCache:Z

.field private writeToFile:Z


# direct methods
.method public constructor <init>()V
    .locals 2

    .line 37
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x1

    .line 38
    iput-boolean v0, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->useCache:Z

    const/4 v1, 0x0

    .line 39
    iput-boolean v1, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->isNoMedia:Z

    .line 40
    iput-boolean v0, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->writeToFile:Z

    .line 41
    sget-object v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->INTERNAL_ONLY:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    iput-object v0, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->downloadDirType:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    const-string v0, ""

    .line 42
    iput-object v0, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->externalStorageDirectoryPath:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public create()Lcom/helpshift/android/commons/downloader/DownloadConfig;
    .locals 2

    .line 109
    new-instance v0, Lcom/helpshift/android/commons/downloader/DownloadConfig;

    invoke-direct {v0}, Lcom/helpshift/android/commons/downloader/DownloadConfig;-><init>()V

    .line 110
    iget-boolean v1, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->useCache:Z

    iput-boolean v1, v0, Lcom/helpshift/android/commons/downloader/DownloadConfig;->useCache:Z

    .line 111
    iget-boolean v1, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->isNoMedia:Z

    iput-boolean v1, v0, Lcom/helpshift/android/commons/downloader/DownloadConfig;->isNoMedia:Z

    .line 112
    iget-boolean v1, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->writeToFile:Z

    iput-boolean v1, v0, Lcom/helpshift/android/commons/downloader/DownloadConfig;->writeToFile:Z

    .line 113
    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->externalStorageDirectoryPath:Ljava/lang/String;

    iput-object v1, v0, Lcom/helpshift/android/commons/downloader/DownloadConfig;->externalStorageDirectoryPath:Ljava/lang/String;

    .line 114
    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->downloadDirType:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    iput-object v1, v0, Lcom/helpshift/android/commons/downloader/DownloadConfig;->downloadDirType:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    return-object v0
.end method

.method public setDownloadDirType(Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;)Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;
    .locals 0

    .line 99
    iput-object p1, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->downloadDirType:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    return-object p0
.end method

.method public setExternalStorageDirectoryPath(Ljava/lang/String;)Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;
    .locals 0

    .line 87
    iput-object p1, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->externalStorageDirectoryPath:Ljava/lang/String;

    return-object p0
.end method

.method public setIsNoMedia(Z)Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;
    .locals 0

    .line 63
    iput-boolean p1, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->isNoMedia:Z

    return-object p0
.end method

.method public setUseCache(Z)Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;
    .locals 0

    .line 52
    iput-boolean p1, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->useCache:Z

    return-object p0
.end method

.method public setWriteToFile(Z)Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;
    .locals 0

    .line 75
    iput-boolean p1, p0, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->writeToFile:Z

    return-object p0
.end method
