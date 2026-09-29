.class Lcom/crashlytics/android/core/ReportUploader;
.super Ljava/lang/Object;
.source "ReportUploader.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/crashlytics/android/core/ReportUploader$Worker;
    }
.end annotation


# static fields
.field private static final CLS_FILE_EXT:Ljava/lang/String; = ".cls"

.field static final HEADER_INVALID_CLS_FILE:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private static final RETRY_INTERVALS:[S

.field private static final crashFileFilter:Ljava/io/FilenameFilter;


# instance fields
.field private final createReportCall:Lcom/crashlytics/android/core/CreateReportSpiCall;

.field private final fileAccessLock:Ljava/lang/Object;

.field private uploadThread:Ljava/lang/Thread;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 25
    new-instance v0, Lcom/crashlytics/android/core/ReportUploader$1;

    invoke-direct {v0}, Lcom/crashlytics/android/core/ReportUploader$1;-><init>()V

    sput-object v0, Lcom/crashlytics/android/core/ReportUploader;->crashFileFilter:Ljava/io/FilenameFilter;

    const-string v0, "X-CRASHLYTICS-INVALID-SESSION"

    const-string v1, "1"

    .line 32
    invoke-static {v0, v1}, Ljava/util/Collections;->singletonMap(Ljava/lang/Object;Ljava/lang/Object;)Ljava/util/Map;

    move-result-object v0

    sput-object v0, Lcom/crashlytics/android/core/ReportUploader;->HEADER_INVALID_CLS_FILE:Ljava/util/Map;

    const/4 v0, 0x6

    .line 35
    new-array v0, v0, [S

    fill-array-data v0, :array_0

    sput-object v0, Lcom/crashlytics/android/core/ReportUploader;->RETRY_INTERVALS:[S

    return-void

    :array_0
    .array-data 2
        0xas
        0x14s
        0x1es
        0x3cs
        0x78s
        0x12cs
    .end array-data
.end method

.method public constructor <init>(Lcom/crashlytics/android/core/CreateReportSpiCall;)V
    .locals 1

    .line 42
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 38
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    iput-object v0, p0, Lcom/crashlytics/android/core/ReportUploader;->fileAccessLock:Ljava/lang/Object;

    if-eqz p1, :cond_0

    .line 46
    iput-object p1, p0, Lcom/crashlytics/android/core/ReportUploader;->createReportCall:Lcom/crashlytics/android/core/CreateReportSpiCall;

    return-void

    .line 44
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "createReportCall must not be null."

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method static synthetic access$002(Lcom/crashlytics/android/core/ReportUploader;Ljava/lang/Thread;)Ljava/lang/Thread;
    .locals 0

    .line 23
    iput-object p1, p0, Lcom/crashlytics/android/core/ReportUploader;->uploadThread:Ljava/lang/Thread;

    return-object p1
.end method

.method static synthetic access$100()[S
    .locals 1

    .line 23
    sget-object v0, Lcom/crashlytics/android/core/ReportUploader;->RETRY_INTERVALS:[S

    return-object v0
.end method


# virtual methods
.method findReports()Ljava/util/List;
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/crashlytics/android/core/Report;",
            ">;"
        }
    .end annotation

    .line 101
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v1, "Fabric"

    const-string v2, "Checking for crash reports..."

    invoke-interface {v0, v1, v2}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 104
    iget-object v0, p0, Lcom/crashlytics/android/core/ReportUploader;->fileAccessLock:Ljava/lang/Object;

    monitor-enter v0

    .line 105
    :try_start_0
    invoke-static {}, Lcom/crashlytics/android/core/CrashlyticsCore;->getInstance()Lcom/crashlytics/android/core/CrashlyticsCore;

    move-result-object v1

    invoke-virtual {v1}, Lcom/crashlytics/android/core/CrashlyticsCore;->getSdkDirectory()Ljava/io/File;

    move-result-object v1

    sget-object v2, Lcom/crashlytics/android/core/ReportUploader;->crashFileFilter:Ljava/io/FilenameFilter;

    invoke-virtual {v1, v2}, Ljava/io/File;->listFiles(Ljava/io/FilenameFilter;)[Ljava/io/File;

    move-result-object v1

    .line 106
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 108
    new-instance v0, Ljava/util/LinkedList;

    invoke-direct {v0}, Ljava/util/LinkedList;-><init>()V

    .line 109
    array-length v2, v1

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v2, :cond_0

    aget-object v4, v1, v3

    .line 110
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v5

    const-string v6, "Fabric"

    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "Found crash report "

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/io/File;->getPath()Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-interface {v5, v6, v7}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 111
    new-instance v5, Lcom/crashlytics/android/core/SessionReport;

    invoke-direct {v5, v4}, Lcom/crashlytics/android/core/SessionReport;-><init>(Ljava/io/File;)V

    invoke-interface {v0, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 114
    :cond_0
    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 115
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v1

    const-string v2, "Fabric"

    const-string v3, "No reports found."

    invoke-interface {v1, v2, v3}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    :cond_1
    return-object v0

    :catchall_0
    move-exception v1

    .line 106
    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1
.end method

.method forceUpload(Lcom/crashlytics/android/core/Report;)Z
    .locals 7

    .line 75
    iget-object v0, p0, Lcom/crashlytics/android/core/ReportUploader;->fileAccessLock:Ljava/lang/Object;

    monitor-enter v0

    const/4 v1, 0x0

    .line 77
    :try_start_0
    invoke-static {}, Lcom/crashlytics/android/core/CrashlyticsCore;->getInstance()Lcom/crashlytics/android/core/CrashlyticsCore;

    move-result-object v2

    invoke-virtual {v2}, Lcom/crashlytics/android/core/CrashlyticsCore;->getContext()Landroid/content/Context;

    move-result-object v2

    .line 79
    new-instance v3, Lcom/crashlytics/android/core/CreateReportRequest;

    new-instance v4, Lio/fabric/sdk/android/services/common/ApiKey;

    invoke-direct {v4}, Lio/fabric/sdk/android/services/common/ApiKey;-><init>()V

    invoke-virtual {v4, v2}, Lio/fabric/sdk/android/services/common/ApiKey;->getValue(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v2

    invoke-direct {v3, v2, p1}, Lcom/crashlytics/android/core/CreateReportRequest;-><init>(Ljava/lang/String;Lcom/crashlytics/android/core/Report;)V

    .line 81
    iget-object v2, p0, Lcom/crashlytics/android/core/ReportUploader;->createReportCall:Lcom/crashlytics/android/core/CreateReportSpiCall;

    invoke-interface {v2, v3}, Lcom/crashlytics/android/core/CreateReportSpiCall;->invoke(Lcom/crashlytics/android/core/CreateReportRequest;)Z

    move-result v2

    .line 83
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v3

    const-string v4, "Fabric"

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Crashlytics report upload "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    if-eqz v2, :cond_0

    const-string v6, "complete: "

    goto :goto_0

    :cond_0
    const-string v6, "FAILED: "

    :goto_0
    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-interface {p1}, Lcom/crashlytics/android/core/Report;->getFileName()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-interface {v3, v4, v5}, Lio/fabric/sdk/android/Logger;->i(Ljava/lang/String;Ljava/lang/String;)V

    if-eqz v2, :cond_1

    .line 87
    invoke-interface {p1}, Lcom/crashlytics/android/core/Report;->remove()Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const/4 p1, 0x1

    const/4 v1, 0x1

    goto :goto_1

    :catchall_0
    move-exception p1

    goto :goto_2

    :catch_0
    move-exception v2

    .line 93
    :try_start_1
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v3

    const-string v4, "Fabric"

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Error occurred sending report "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v3, v4, p1, v2}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 96
    :cond_1
    :goto_1
    monitor-exit v0

    return v1

    :goto_2
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw p1
.end method

.method isUploading()Z
    .locals 1

    .line 62
    iget-object v0, p0, Lcom/crashlytics/android/core/ReportUploader;->uploadThread:Ljava/lang/Thread;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public uploadReports()V
    .locals 1

    const/4 v0, 0x0

    .line 50
    invoke-virtual {p0, v0}, Lcom/crashlytics/android/core/ReportUploader;->uploadReports(F)V

    return-void
.end method

.method public declared-synchronized uploadReports(F)V
    .locals 2

    monitor-enter p0

    .line 54
    :try_start_0
    iget-object v0, p0, Lcom/crashlytics/android/core/ReportUploader;->uploadThread:Ljava/lang/Thread;

    if-nez v0, :cond_0

    .line 55
    new-instance v0, Ljava/lang/Thread;

    new-instance v1, Lcom/crashlytics/android/core/ReportUploader$Worker;

    invoke-direct {v1, p0, p1}, Lcom/crashlytics/android/core/ReportUploader$Worker;-><init>(Lcom/crashlytics/android/core/ReportUploader;F)V

    const-string p1, "Crashlytics Report Uploader"

    invoke-direct {v0, v1, p1}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;Ljava/lang/String;)V

    iput-object v0, p0, Lcom/crashlytics/android/core/ReportUploader;->uploadThread:Ljava/lang/Thread;

    .line 56
    iget-object p1, p0, Lcom/crashlytics/android/core/ReportUploader;->uploadThread:Ljava/lang/Thread;

    invoke-virtual {p1}, Ljava/lang/Thread;->start()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 58
    :cond_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 53
    monitor-exit p0

    throw p1
.end method
