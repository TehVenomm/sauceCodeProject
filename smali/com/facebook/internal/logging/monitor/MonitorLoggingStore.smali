.class public Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;
.super Ljava/lang/Object;
.source "MonitorLoggingStore.java"

# interfaces
.implements Lcom/facebook/internal/logging/LoggingStore;


# static fields
.field public static final PERSISTED_LOGS_FILENAME:Ljava/lang/String; = "facebooksdk.monitoring.persistedlogs"

.field private static monitorLoggingStore:Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 52
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static declared-synchronized getInstance()Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;
    .locals 2

    const-class v0, Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;

    monitor-enter v0

    .line 56
    :try_start_0
    sget-object v1, Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;->monitorLoggingStore:Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;

    if-nez v1, :cond_0

    .line 57
    new-instance v1, Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;

    invoke-direct {v1}, Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;-><init>()V

    sput-object v1, Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;->monitorLoggingStore:Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;

    .line 59
    :cond_0
    sget-object v1, Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;->monitorLoggingStore:Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object v1

    :catchall_0
    move-exception v1

    .line 55
    monitor-exit v0

    throw v1
.end method


# virtual methods
.method public readAndClearStore()Ljava/util/List;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/facebook/internal/logging/ExternalLog;",
            ">;"
        }
    .end annotation

    .line 69
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 70
    invoke-static {}, Lcom/facebook/FacebookSdk;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    const/4 v2, 0x0

    :try_start_0
    const-string v3, "facebooksdk.monitoring.persistedlogs"

    .line 73
    invoke-virtual {v1, v3}, Landroid/content/Context;->openFileInput(Ljava/lang/String;)Ljava/io/FileInputStream;

    move-result-object v3

    .line 74
    new-instance v4, Ljava/io/ObjectInputStream;

    new-instance v5, Ljava/io/BufferedInputStream;

    invoke-direct {v5, v3}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;)V

    invoke-direct {v4, v5}, Ljava/io/ObjectInputStream;-><init>(Ljava/io/InputStream;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_3
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 75
    :try_start_1
    invoke-virtual {v4}, Ljava/io/ObjectInputStream;->readObject()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/List;
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 79
    invoke-static {v4}, Lcom/facebook/internal/Utility;->closeQuietly(Ljava/io/Closeable;)V

    :try_start_2
    const-string v0, "facebooksdk.monitoring.persistedlogs"

    .line 82
    invoke-virtual {v1, v0}, Landroid/content/Context;->getFileStreamPath(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    invoke-virtual {v0}, Ljava/io/File;->delete()Z
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0

    :catch_0
    move-object v0, v2

    goto :goto_2

    :catchall_0
    move-exception v0

    move-object v2, v4

    goto :goto_0

    :catch_1
    move-object v2, v4

    goto :goto_1

    :catchall_1
    move-exception v0

    .line 79
    :goto_0
    invoke-static {v2}, Lcom/facebook/internal/Utility;->closeQuietly(Ljava/io/Closeable;)V

    :try_start_3
    const-string v2, "facebooksdk.monitoring.persistedlogs"

    .line 82
    invoke-virtual {v1, v2}, Landroid/content/Context;->getFileStreamPath(Ljava/lang/String;)Ljava/io/File;

    move-result-object v1

    invoke-virtual {v1}, Ljava/io/File;->delete()Z
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_2

    .line 86
    :catch_2
    throw v0

    .line 79
    :catch_3
    :goto_1
    invoke-static {v2}, Lcom/facebook/internal/Utility;->closeQuietly(Ljava/io/Closeable;)V

    :try_start_4
    const-string v2, "facebooksdk.monitoring.persistedlogs"

    .line 82
    invoke-virtual {v1, v2}, Landroid/content/Context;->getFileStreamPath(Ljava/lang/String;)Ljava/io/File;

    move-result-object v1

    invoke-virtual {v1}, Ljava/io/File;->delete()Z
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_4

    :catch_4
    :goto_2
    return-object v0
.end method

.method public saveLogsToDisk(Ljava/util/List;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/facebook/internal/logging/ExternalLog;",
            ">;)V"
        }
    .end annotation

    .line 97
    invoke-static {}, Lcom/facebook/FacebookSdk;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    const/4 v1, 0x0

    .line 100
    :try_start_0
    new-instance v2, Ljava/io/ObjectOutputStream;

    new-instance v3, Ljava/io/BufferedOutputStream;

    const-string v4, "facebooksdk.monitoring.persistedlogs"

    const/4 v5, 0x0

    .line 102
    invoke-virtual {v0, v4, v5}, Landroid/content/Context;->openFileOutput(Ljava/lang/String;I)Ljava/io/FileOutputStream;

    move-result-object v4

    invoke-direct {v3, v4}, Ljava/io/BufferedOutputStream;-><init>(Ljava/io/OutputStream;)V

    invoke-direct {v2, v3}, Ljava/io/ObjectOutputStream;-><init>(Ljava/io/OutputStream;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 103
    :try_start_1
    invoke-virtual {v2, p1}, Ljava/io/ObjectOutputStream;->writeObject(Ljava/lang/Object;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 112
    invoke-static {v2}, Lcom/facebook/internal/Utility;->closeQuietly(Ljava/io/Closeable;)V

    goto :goto_3

    :catchall_0
    move-exception p1

    move-object v1, v2

    goto :goto_1

    :catch_0
    move-object v1, v2

    goto :goto_0

    :catchall_1
    move-exception p1

    goto :goto_1

    :catch_1
    :goto_0
    :try_start_2
    const-string p1, "facebooksdk.monitoring.persistedlogs"

    .line 107
    invoke-virtual {v0, p1}, Landroid/content/Context;->getFileStreamPath(Ljava/lang/String;)Ljava/io/File;

    move-result-object p1

    invoke-virtual {p1}, Ljava/io/File;->delete()Z
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    goto :goto_2

    .line 112
    :goto_1
    invoke-static {v1}, Lcom/facebook/internal/Utility;->closeQuietly(Ljava/io/Closeable;)V

    .line 113
    throw p1

    .line 112
    :catch_2
    :goto_2
    invoke-static {v1}, Lcom/facebook/internal/Utility;->closeQuietly(Ljava/io/Closeable;)V

    :goto_3
    return-void
.end method
