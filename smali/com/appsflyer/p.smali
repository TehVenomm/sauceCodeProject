.class final Lcom/appsflyer/p;
.super Ljava/lang/Object;
.source ""


# static fields
.field private static ॱ:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method constructor <init>()V
    .locals 0

    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static declared-synchronized ˋ(Ljava/lang/ref/WeakReference;)Ljava/lang/String;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/ref/WeakReference<",
            "Landroid/content/Context;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    const-class v0, Lcom/appsflyer/p;

    monitor-enter v0

    .line 30
    :try_start_0
    invoke-virtual {p0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v1

    if-nez v1, :cond_0

    .line 31
    sget-object p0, Lcom/appsflyer/p;->ॱ:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object p0

    .line 33
    :cond_0
    :try_start_1
    sget-object v1, Lcom/appsflyer/p;->ॱ:Ljava/lang/String;

    if-nez v1, :cond_6

    .line 4118
    invoke-virtual {p0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v1

    const/4 v2, 0x0

    const/4 v3, 0x0

    if-nez v1, :cond_1

    goto :goto_0

    .line 4121
    :cond_1
    invoke-virtual {p0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/content/Context;

    const-string v4, "appsflyer-data"

    invoke-virtual {v1, v4, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v1

    const-string v4, "AF_INSTALLATION"

    .line 4122
    invoke-interface {v1, v4, v3}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    :goto_0
    if-eqz v3, :cond_2

    .line 36
    sput-object v3, Lcom/appsflyer/p;->ॱ:Ljava/lang/String;
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto/16 :goto_3

    .line 39
    :cond_2
    :try_start_2
    new-instance v1, Ljava/io/File;

    invoke-virtual {p0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/content/Context;

    invoke-virtual {v3}, Landroid/content/Context;->getFilesDir()Ljava/io/File;

    move-result-object v3

    const-string v4, "AF_INSTALLATION"

    invoke-direct {v1, v3, v4}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 40
    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result v3

    const/16 v4, 0x9

    if-nez v3, :cond_4

    .line 6103
    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v1, v4, :cond_3

    .line 6104
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v5

    .line 6114
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, v5, v6}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string v3, "-"

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    new-instance v3, Ljava/security/SecureRandom;

    invoke-direct {v3}, Ljava/security/SecureRandom;-><init>()V

    invoke-virtual {v3}, Ljava/util/Random;->nextLong()J

    move-result-wide v5

    invoke-static {v5, v6}, Ljava/lang/Math;->abs(J)J

    move-result-wide v5

    invoke-virtual {v1, v5, v6}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    goto :goto_1

    .line 7107
    :cond_3
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    .line 41
    :goto_1
    sput-object v1, Lcom/appsflyer/p;->ॱ:Ljava/lang/String;

    goto :goto_2

    .line 43
    :cond_4
    invoke-static {v1}, Lcom/appsflyer/p;->ˏ(Ljava/io/File;)Ljava/lang/String;

    move-result-object v3

    sput-object v3, Lcom/appsflyer/p;->ॱ:Ljava/lang/String;

    .line 44
    invoke-virtual {v1}, Ljava/io/File;->delete()Z

    .line 46
    :goto_2
    sget-object v1, Lcom/appsflyer/p;->ॱ:Ljava/lang/String;

    .line 7131
    invoke-virtual {p0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Landroid/content/Context;

    const-string v3, "appsflyer-data"

    invoke-virtual {p0, v3, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p0

    .line 7132
    invoke-interface {p0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    const-string v2, "AF_INSTALLATION"

    .line 7133
    invoke-interface {p0, v2, v1}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 7134
    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v1, v4, :cond_5

    .line 7135
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->apply()V

    goto :goto_3

    .line 7137
    :cond_5
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    goto :goto_3

    :catch_0
    move-exception p0

    :try_start_3
    const-string v1, "Error getting AF unique ID"

    .line 48
    invoke-static {v1, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 51
    :goto_3
    sget-object p0, Lcom/appsflyer/p;->ॱ:Ljava/lang/String;

    if-eqz p0, :cond_6

    .line 52
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object p0

    const-string v1, "uid"

    sget-object v2, Lcom/appsflyer/p;->ॱ:Ljava/lang/String;

    invoke-virtual {p0, v1, v2}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    .line 55
    :cond_6
    sget-object p0, Lcom/appsflyer/p;->ॱ:Ljava/lang/String;
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    monitor-exit v0

    return-object p0

    :catchall_0
    move-exception p0

    .line 29
    monitor-exit v0

    throw p0
.end method

.method private static ˏ(Ljava/io/File;)Ljava/lang/String;
    .locals 5

    const/4 v0, 0x0

    .line 62
    :try_start_0
    new-instance v1, Ljava/io/RandomAccessFile;

    const-string v2, "r"

    invoke-direct {v1, p0, v2}, Ljava/io/RandomAccessFile;-><init>(Ljava/io/File;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_3
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 63
    :try_start_1
    invoke-virtual {v1}, Ljava/io/RandomAccessFile;->length()J

    move-result-wide v2

    long-to-int p0, v2

    new-array p0, p0, [B
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_2
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 64
    :try_start_2
    invoke-virtual {v1, p0}, Ljava/io/RandomAccessFile;->readFully([B)V

    .line 65
    invoke-virtual {v1}, Ljava/io/RandomAccessFile;->close()V
    :try_end_2
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_1
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 71
    :try_start_3
    invoke-virtual {v1}, Ljava/io/RandomAccessFile;->close()V
    :try_end_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_0

    goto :goto_3

    :catch_0
    move-exception v0

    const-string v1, "Exception while trying to close the InstallationFile"

    .line 74
    invoke-static {v1, v0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_3

    :catch_1
    move-exception v0

    move-object v4, v1

    move-object v1, p0

    move-object p0, v0

    goto :goto_0

    :catchall_0
    move-exception p0

    move-object v0, v1

    goto :goto_5

    :catch_2
    move-exception p0

    move-object v4, v1

    move-object v1, v0

    :goto_0
    move-object v0, v4

    goto :goto_1

    :catchall_1
    move-exception p0

    goto :goto_5

    :catch_3
    move-exception p0

    move-object v1, v0

    :goto_1
    :try_start_4
    const-string v2, "Exception while reading InstallationFile: "

    .line 67
    invoke-static {v2, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v0, :cond_0

    .line 71
    :try_start_5
    invoke-virtual {v0}, Ljava/io/RandomAccessFile;->close()V
    :try_end_5
    .catch Ljava/io/IOException; {:try_start_5 .. :try_end_5} :catch_4

    goto :goto_2

    :catch_4
    move-exception p0

    const-string v0, "Exception while trying to close the InstallationFile"

    .line 74
    invoke-static {v0, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    :goto_2
    move-object p0, v1

    .line 77
    :goto_3
    new-instance v0, Ljava/lang/String;

    if-eqz p0, :cond_1

    goto :goto_4

    :cond_1
    const/4 p0, 0x0

    new-array p0, p0, [B

    :goto_4
    invoke-direct {v0, p0}, Ljava/lang/String;-><init>([B)V

    return-object v0

    :goto_5
    if-eqz v0, :cond_2

    .line 71
    :try_start_6
    invoke-virtual {v0}, Ljava/io/RandomAccessFile;->close()V
    :try_end_6
    .catch Ljava/io/IOException; {:try_start_6 .. :try_end_6} :catch_5

    goto :goto_6

    :catch_5
    move-exception v0

    const-string v1, "Exception while trying to close the InstallationFile"

    .line 74
    invoke-static {v1, v0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 75
    :cond_2
    :goto_6
    throw p0
.end method
