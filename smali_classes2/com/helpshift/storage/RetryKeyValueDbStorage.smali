.class public Lcom/helpshift/storage/RetryKeyValueDbStorage;
.super Lcom/helpshift/storage/BaseRetryKeyValueStorage;
.source "RetryKeyValueDbStorage.java"


# static fields
.field private static final backupFileName:Ljava/lang/String; = "__hs__kv_backup"


# instance fields
.field private final context:Landroid/content/Context;

.field private sqLiteOpenHelper:Landroid/database/sqlite/SQLiteOpenHelper;


# direct methods
.method constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 16
    invoke-direct {p0}, Lcom/helpshift/storage/BaseRetryKeyValueStorage;-><init>()V

    .line 17
    iput-object p1, p0, Lcom/helpshift/storage/RetryKeyValueDbStorage;->context:Landroid/content/Context;

    .line 18
    new-instance v0, Lcom/helpshift/storage/KeyValueDbStorageHelper;

    invoke-direct {v0, p1}, Lcom/helpshift/storage/KeyValueDbStorageHelper;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/storage/RetryKeyValueDbStorage;->sqLiteOpenHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    .line 19
    new-instance p1, Lcom/helpshift/storage/KeyValueDbStorage;

    iget-object v0, p0, Lcom/helpshift/storage/RetryKeyValueDbStorage;->sqLiteOpenHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    const-string v1, "__hs__kv_backup"

    invoke-direct {p1, v0, v1}, Lcom/helpshift/storage/KeyValueDbStorage;-><init>(Landroid/database/sqlite/SQLiteOpenHelper;Ljava/lang/String;)V

    iput-object p1, p0, Lcom/helpshift/storage/RetryKeyValueDbStorage;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    return-void
.end method


# virtual methods
.method protected reInitiateDbInstance()V
    .locals 3

    .line 25
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/storage/RetryKeyValueDbStorage;->sqLiteOpenHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    if-eqz v0, :cond_0

    .line 26
    iget-object v0, p0, Lcom/helpshift/storage/RetryKeyValueDbStorage;->sqLiteOpenHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteOpenHelper;->close()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_RetryKeyValue"

    const-string v2, "Error in closing DB"

    .line 30
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 33
    :cond_0
    :goto_0
    new-instance v0, Lcom/helpshift/storage/KeyValueDbStorageHelper;

    iget-object v1, p0, Lcom/helpshift/storage/RetryKeyValueDbStorage;->context:Landroid/content/Context;

    invoke-direct {v0, v1}, Lcom/helpshift/storage/KeyValueDbStorageHelper;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/storage/RetryKeyValueDbStorage;->sqLiteOpenHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    .line 36
    new-instance v0, Lcom/helpshift/storage/KeyValueDbStorage;

    iget-object v1, p0, Lcom/helpshift/storage/RetryKeyValueDbStorage;->sqLiteOpenHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    const-string v2, "__hs__kv_backup"

    invoke-direct {v0, v1, v2}, Lcom/helpshift/storage/KeyValueDbStorage;-><init>(Landroid/database/sqlite/SQLiteOpenHelper;Ljava/lang/String;)V

    iput-object v0, p0, Lcom/helpshift/storage/RetryKeyValueDbStorage;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    return-void
.end method
