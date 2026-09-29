.class public Lcom/helpshift/android/commons/downloader/DownloadConfig;
.super Ljava/lang/Object;
.source "DownloadConfig.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;
    }
.end annotation


# instance fields
.field downloadDirType:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

.field externalStorageDirectoryPath:Ljava/lang/String;

.field isNoMedia:Z

.field useCache:Z

.field writeToFile:Z


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 10
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
