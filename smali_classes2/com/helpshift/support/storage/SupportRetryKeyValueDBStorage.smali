.class Lcom/helpshift/support/storage/SupportRetryKeyValueDBStorage;
.super Lcom/helpshift/storage/BaseRetryKeyValueStorage;
.source "SupportRetryKeyValueDBStorage.java"


# instance fields
.field private final context:Landroid/content/Context;

.field private sqLiteOpenHelper:Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;


# direct methods
.method constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 13
    invoke-direct {p0}, Lcom/helpshift/storage/BaseRetryKeyValueStorage;-><init>()V

    .line 14
    iput-object p1, p0, Lcom/helpshift/support/storage/SupportRetryKeyValueDBStorage;->context:Landroid/content/Context;

    .line 15
    new-instance v0, Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;

    invoke-direct {v0, p1}, Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/support/storage/SupportRetryKeyValueDBStorage;->sqLiteOpenHelper:Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;

    .line 16
    new-instance p1, Lcom/helpshift/storage/KeyValueDbStorage;

    iget-object v0, p0, Lcom/helpshift/support/storage/SupportRetryKeyValueDBStorage;->sqLiteOpenHelper:Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;

    const/4 v1, 0x0

    invoke-direct {p1, v0, v1}, Lcom/helpshift/storage/KeyValueDbStorage;-><init>(Landroid/database/sqlite/SQLiteOpenHelper;Ljava/lang/String;)V

    iput-object p1, p0, Lcom/helpshift/support/storage/SupportRetryKeyValueDBStorage;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    return-void
.end method


# virtual methods
.method protected reInitiateDbInstance()V
    .locals 3

    .line 23
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/support/storage/SupportRetryKeyValueDBStorage;->sqLiteOpenHelper:Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;

    if-eqz v0, :cond_0

    .line 24
    iget-object v0, p0, Lcom/helpshift/support/storage/SupportRetryKeyValueDBStorage;->sqLiteOpenHelper:Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;

    invoke-virtual {v0}, Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;->close()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_RetryKeyValue"

    const-string v2, "Error in closing DB"

    .line 28
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 31
    :cond_0
    :goto_0
    new-instance v0, Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;

    iget-object v1, p0, Lcom/helpshift/support/storage/SupportRetryKeyValueDBStorage;->context:Landroid/content/Context;

    invoke-direct {v0, v1}, Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/support/storage/SupportRetryKeyValueDBStorage;->sqLiteOpenHelper:Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;

    .line 32
    new-instance v0, Lcom/helpshift/storage/KeyValueDbStorage;

    iget-object v1, p0, Lcom/helpshift/support/storage/SupportRetryKeyValueDBStorage;->sqLiteOpenHelper:Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/storage/KeyValueDbStorage;-><init>(Landroid/database/sqlite/SQLiteOpenHelper;Ljava/lang/String;)V

    iput-object v0, p0, Lcom/helpshift/support/storage/SupportRetryKeyValueDBStorage;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    return-void
.end method
