.class public Lcom/helpshift/common/platform/AndroidBackupDAO;
.super Ljava/lang/Object;
.source "AndroidBackupDAO.java"

# interfaces
.implements Lcom/helpshift/common/dao/BackupDAO;


# instance fields
.field private final BACKUP_FILE_NAME:Ljava/lang/String;

.field private externalDirectoryPath:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 21
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "__hs__backup_dao_storage"

    .line 23
    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidBackupDAO;->BACKUP_FILE_NAME:Ljava/lang/String;

    return-void
.end method

.method private backupExists()Z
    .locals 3

    .line 158
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/common/platform/AndroidBackupDAO;->getExternalDirectoryPath()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Landroid/os/Environment;->getExternalStoragePublicDirectory(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    .line 159
    new-instance v1, Ljava/io/File;

    const-string v2, "__hs__backup_dao_storage"

    invoke-direct {v1, v0, v2}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 160
    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return v0

    :catch_0
    const/4 v0, 0x0

    return v0
.end method

.method private backupHashMap(Ljava/util/HashMap;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)V"
        }
    .end annotation

    if-nez p1, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x0

    .line 100
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/common/platform/AndroidBackupDAO;->getExternalDirectoryPath()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Landroid/os/Environment;->getExternalStoragePublicDirectory(Ljava/lang/String;)Ljava/io/File;

    move-result-object v1

    .line 101
    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result v2

    if-nez v2, :cond_1

    .line 103
    invoke-virtual {v1}, Ljava/io/File;->mkdirs()Z

    .line 106
    :cond_1
    invoke-virtual {v1}, Ljava/io/File;->canWrite()Z

    move-result v2

    if-eqz v2, :cond_2

    .line 107
    new-instance v2, Ljava/io/File;

    const-string v3, "__hs__backup_dao_storage"

    invoke-direct {v2, v1, v3}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 108
    new-instance v1, Ljava/io/ObjectOutputStream;

    new-instance v3, Ljava/io/FileOutputStream;

    invoke-direct {v3, v2}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V

    invoke-direct {v1, v3}, Ljava/io/ObjectOutputStream;-><init>(Ljava/io/OutputStream;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 109
    :try_start_1
    invoke-virtual {v1, p1}, Ljava/io/ObjectOutputStream;->writeObject(Ljava/lang/Object;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    move-object v0, v1

    goto :goto_0

    :catchall_0
    move-exception p1

    move-object v0, v1

    goto :goto_2

    :catch_0
    move-object v0, v1

    goto :goto_3

    :cond_2
    :goto_0
    if-eqz v0, :cond_4

    .line 117
    :goto_1
    :try_start_2
    invoke-virtual {v0}, Ljava/io/ObjectOutputStream;->close()V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_3

    goto :goto_4

    :catchall_1
    move-exception p1

    :goto_2
    if-eqz v0, :cond_3

    :try_start_3
    invoke-virtual {v0}, Ljava/io/ObjectOutputStream;->close()V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_1

    .line 122
    :catch_1
    :cond_3
    throw p1

    :catch_2
    :goto_3
    if-eqz v0, :cond_4

    goto :goto_1

    :catch_3
    :cond_4
    :goto_4
    return-void
.end method

.method private getExternalDirectoryPath()Ljava/lang/String;
    .locals 2

    .line 170
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidBackupDAO;->externalDirectoryPath:Ljava/lang/String;

    if-nez v0, :cond_0

    .line 171
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, ".backups/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 172
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "/helpshift/databases/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidBackupDAO;->externalDirectoryPath:Ljava/lang/String;

    .line 175
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidBackupDAO;->externalDirectoryPath:Ljava/lang/String;

    return-object v0
.end method

.method private restoreHashMap()Ljava/util/HashMap;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;"
        }
    .end annotation

    .line 126
    invoke-direct {p0}, Lcom/helpshift/common/platform/AndroidBackupDAO;->backupExists()Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return-object v1

    .line 133
    :cond_0
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/common/platform/AndroidBackupDAO;->getExternalDirectoryPath()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Landroid/os/Environment;->getExternalStoragePublicDirectory(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    .line 135
    invoke-virtual {v0}, Ljava/io/File;->canRead()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 136
    new-instance v2, Ljava/io/File;

    const-string v3, "__hs__backup_dao_storage"

    invoke-direct {v2, v0, v3}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 137
    new-instance v0, Ljava/io/ObjectInputStream;

    new-instance v3, Ljava/io/FileInputStream;

    invoke-direct {v3, v2}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    invoke-direct {v0, v3}, Ljava/io/ObjectInputStream;-><init>(Ljava/io/InputStream;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 139
    :try_start_1
    invoke-virtual {v0}, Ljava/io/ObjectInputStream;->readObject()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/HashMap;
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_2
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    move-object v1, v2

    goto :goto_0

    :catchall_0
    move-exception v1

    move-object v4, v1

    move-object v1, v0

    move-object v0, v4

    goto :goto_2

    :cond_1
    move-object v0, v1

    :goto_0
    if-eqz v0, :cond_3

    .line 147
    :goto_1
    :try_start_2
    invoke-virtual {v0}, Ljava/io/ObjectInputStream;->close()V
    :try_end_2
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_3

    goto :goto_3

    :catchall_1
    move-exception v0

    :goto_2
    if-eqz v1, :cond_2

    :try_start_3
    invoke-virtual {v1}, Ljava/io/ObjectInputStream;->close()V
    :try_end_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_0

    .line 152
    :catch_0
    :cond_2
    throw v0

    :catch_1
    move-object v0, v1

    :catch_2
    if-eqz v0, :cond_3

    goto :goto_1

    :catch_3
    :cond_3
    :goto_3
    return-object v1
.end method


# virtual methods
.method public declared-synchronized delete()V
    .locals 3

    monitor-enter p0

    .line 75
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/common/platform/AndroidBackupDAO;->backupExists()Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v0, :cond_0

    .line 76
    monitor-exit p0

    return-void

    .line 80
    :cond_0
    :try_start_1
    invoke-direct {p0}, Lcom/helpshift/common/platform/AndroidBackupDAO;->getExternalDirectoryPath()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Landroid/os/Environment;->getExternalStoragePublicDirectory(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 81
    invoke-virtual {v0}, Ljava/io/File;->exists()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 82
    new-instance v1, Ljava/io/File;

    const-string v2, "__hs__backup_dao_storage"

    invoke-direct {v1, v0, v2}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 83
    invoke-virtual {v1}, Ljava/io/File;->canWrite()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 84
    invoke-virtual {v1}, Ljava/io/File;->delete()Z
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 91
    :catch_0
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 74
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getValue(Ljava/lang/String;)Ljava/io/Serializable;
    .locals 2

    monitor-enter p0

    .line 48
    :try_start_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 49
    monitor-exit p0

    return-object v1

    .line 52
    :cond_0
    :try_start_1
    invoke-direct {p0}, Lcom/helpshift/common/platform/AndroidBackupDAO;->restoreHashMap()Ljava/util/HashMap;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 54
    invoke-virtual {v0, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/io/Serializable;
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    monitor-exit p0

    return-object p1

    .line 57
    :cond_1
    monitor-exit p0

    return-object v1

    :catchall_0
    move-exception p1

    .line 47
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized removeKey(Ljava/lang/String;)V
    .locals 2

    monitor-enter p0

    .line 62
    :try_start_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v0, :cond_0

    .line 63
    monitor-exit p0

    return-void

    .line 66
    :cond_0
    :try_start_1
    invoke-direct {p0}, Lcom/helpshift/common/platform/AndroidBackupDAO;->restoreHashMap()Ljava/util/HashMap;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 67
    invoke-virtual {v0, p1}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 68
    invoke-virtual {v0, p1}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 69
    invoke-direct {p0, v0}, Lcom/helpshift/common/platform/AndroidBackupDAO;->backupHashMap(Ljava/util/HashMap;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 71
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 61
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized storeValue(Ljava/lang/String;Ljava/io/Serializable;)V
    .locals 2

    monitor-enter p0

    .line 28
    :try_start_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_3

    if-nez p2, :cond_0

    goto :goto_0

    .line 32
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/common/platform/AndroidBackupDAO;->restoreHashMap()Ljava/util/HashMap;

    move-result-object v0

    if-nez v0, :cond_1

    .line 34
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 38
    :cond_1
    invoke-virtual {v0, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    invoke-virtual {p2, v1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v1, :cond_2

    .line 39
    monitor-exit p0

    return-void

    .line 42
    :cond_2
    :try_start_1
    invoke-virtual {v0, p1, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 43
    invoke-direct {p0, v0}, Lcom/helpshift/common/platform/AndroidBackupDAO;->backupHashMap(Ljava/util/HashMap;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 44
    monitor-exit p0

    return-void

    .line 29
    :cond_3
    :goto_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 27
    monitor-exit p0

    throw p1
.end method
