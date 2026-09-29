.class public Lcom/helpshift/logger/database/LogSQLiteStorage;
.super Ljava/lang/Object;
.source "LogSQLiteStorage.java"

# interfaces
.implements Lcom/helpshift/logger/database/LogStorage;


# static fields
.field private static final MAX_ROWS:I = 0x64

.field private static final TAG:Ljava/lang/String; = "LogSqliteStorage"

.field private static final syncLock:Ljava/lang/Object;


# instance fields
.field private logStorageSQLiteHelper:Lcom/helpshift/logger/database/LogStorageSQLiteHelper;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 25
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/helpshift/logger/database/LogSQLiteStorage;->syncLock:Ljava/lang/Object;

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;)V
    .locals 1

    .line 41
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 42
    new-instance v0, Lcom/helpshift/logger/database/LogStorageSQLiteHelper;

    invoke-direct {v0, p1, p2}, Lcom/helpshift/logger/database/LogStorageSQLiteHelper;-><init>(Landroid/content/Context;Ljava/lang/String;)V

    iput-object v0, p0, Lcom/helpshift/logger/database/LogSQLiteStorage;->logStorageSQLiteHelper:Lcom/helpshift/logger/database/LogStorageSQLiteHelper;

    return-void
.end method

.method private makeINQueryPlaceHolders(I)Ljava/lang/String;
    .locals 3

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 180
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "("

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    const/4 v1, 0x0

    :goto_0
    add-int/lit8 v2, p1, -0x1

    if-ge v1, v2, :cond_1

    const-string v2, "?,"

    .line 183
    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    const-string p1, "?)"

    .line 186
    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 188
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method


# virtual methods
.method public deleteAll()V
    .locals 4

    .line 129
    sget-object v0, Lcom/helpshift/logger/database/LogSQLiteStorage;->syncLock:Ljava/lang/Object;

    monitor-enter v0

    .line 131
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/logger/database/LogSQLiteStorage;->logStorageSQLiteHelper:Lcom/helpshift/logger/database/LogStorageSQLiteHelper;

    invoke-virtual {v1}, Lcom/helpshift/logger/database/LogStorageSQLiteHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    const-string v2, "DELETE FROM LOG_MESSAGES"

    .line 132
    invoke-virtual {v1, v2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception v1

    goto :goto_1

    :catch_0
    move-exception v1

    :try_start_1
    const-string v2, "LogSqliteStorage"

    const-string v3, "Error deleting all logs from db"

    .line 135
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 137
    :goto_0
    monitor-exit v0

    return-void

    :goto_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1
.end method

.method public getAll()Ljava/util/List;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/logger/model/LogModel;",
            ">;"
        }
    .end annotation

    .line 103
    sget-object v0, Lcom/helpshift/logger/database/LogSQLiteStorage;->syncLock:Ljava/lang/Object;

    monitor-enter v0

    const/4 v1, 0x0

    .line 106
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/logger/database/LogSQLiteStorage;->logStorageSQLiteHelper:Lcom/helpshift/logger/database/LogStorageSQLiteHelper;

    invoke-virtual {v2}, Lcom/helpshift/logger/database/LogStorageSQLiteHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    const-string v3, "SELECT * FROM LOG_MESSAGES"

    .line 109
    invoke-virtual {v2, v3, v1}, Landroid/database/sqlite/SQLiteDatabase;->rawQuery(Ljava/lang/String;[Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v2
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 112
    :try_start_1
    invoke-static {v2}, Lcom/helpshift/logger/adapters/LogStorageModelAdapter;->fromCursor(Landroid/database/Cursor;)Ljava/util/List;

    move-result-object v3
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    if-eqz v2, :cond_0

    .line 119
    :try_start_2
    invoke-interface {v2}, Landroid/database/Cursor;->close()V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    :cond_0
    move-object v1, v3

    goto :goto_1

    :catch_0
    move-exception v3

    goto :goto_0

    :catchall_0
    move-exception v2

    move-object v6, v2

    move-object v2, v1

    move-object v1, v6

    goto :goto_2

    :catch_1
    move-exception v3

    move-object v2, v1

    :goto_0
    :try_start_3
    const-string v4, "LogSqliteStorage"

    const-string v5, "Error getting all log messages : "

    .line 115
    invoke-static {v4, v5, v3}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    if-eqz v2, :cond_1

    .line 119
    :try_start_4
    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    .line 122
    :cond_1
    :goto_1
    monitor-exit v0

    return-object v1

    :catchall_1
    move-exception v1

    :goto_2
    if-eqz v2, :cond_2

    .line 119
    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    goto :goto_3

    :catchall_2
    move-exception v1

    goto :goto_4

    .line 121
    :cond_2
    :goto_3
    throw v1

    .line 122
    :goto_4
    monitor-exit v0
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    throw v1
.end method

.method public getCount(Ljava/util/List;)I
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)I"
        }
    .end annotation

    const/4 v0, 0x0

    if-eqz p1, :cond_4

    .line 143
    invoke-interface {p1}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    goto/16 :goto_4

    .line 148
    :cond_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v1

    invoke-direct {p0, v1}, Lcom/helpshift/logger/database/LogSQLiteStorage;->makeINQueryPlaceHolders(I)Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x0

    .line 152
    :try_start_0
    iget-object v3, p0, Lcom/helpshift/logger/database/LogSQLiteStorage;->logStorageSQLiteHelper:Lcom/helpshift/logger/database/LogStorageSQLiteHelper;

    invoke-virtual {v3}, Lcom/helpshift/logger/database/LogStorageSQLiteHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v3

    .line 153
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "SELECT COUNT(*) FROM LOG_MESSAGES WHERE LEVEL IN "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    new-array v4, v0, [Ljava/lang/String;

    .line 155
    invoke-interface {p1, v4}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object p1

    check-cast p1, [Ljava/lang/String;

    .line 153
    invoke-virtual {v3, v1, p1}, Landroid/database/sqlite/SQLiteDatabase;->rawQuery(Ljava/lang/String;[Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    if-eqz p1, :cond_1

    .line 157
    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 158
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result v1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    move v0, v1

    goto :goto_0

    :catchall_0
    move-exception v0

    move-object v2, p1

    goto :goto_3

    :catch_0
    move-exception v1

    move-object v2, p1

    goto :goto_1

    :cond_1
    :goto_0
    if-eqz p1, :cond_2

    .line 167
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    goto :goto_2

    :catchall_1
    move-exception v0

    goto :goto_3

    :catch_1
    move-exception v1

    :goto_1
    :try_start_2
    const-string p1, "LogSqliteStorage"

    .line 163
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Error getting logs count : "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {p1, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz v2, :cond_2

    .line 167
    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    :cond_2
    :goto_2
    return v0

    :goto_3
    if-eqz v2, :cond_3

    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    .line 169
    :cond_3
    throw v0

    :cond_4
    :goto_4
    return v0
.end method

.method public insert(Lcom/helpshift/logger/model/LogModel;)V
    .locals 9

    .line 50
    sget-object v0, Lcom/helpshift/logger/database/LogSQLiteStorage;->syncLock:Ljava/lang/Object;

    monitor-enter v0

    const/4 v1, 0x0

    .line 52
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/logger/database/LogSQLiteStorage;->logStorageSQLiteHelper:Lcom/helpshift/logger/database/LogStorageSQLiteHelper;

    invoke-virtual {v2}, Lcom/helpshift/logger/database/LogStorageSQLiteHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_5
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    .line 54
    :try_start_1
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_4
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    :try_start_2
    const-string v3, "SELECT rowid FROM LOG_MESSAGES"

    .line 58
    invoke-virtual {v2, v3, v1}, Landroid/database/sqlite/SQLiteDatabase;->rawQuery(Ljava/lang/String;[Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v3
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_1
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz v3, :cond_0

    .line 61
    :try_start_3
    invoke-interface {v3}, Landroid/database/Cursor;->getCount()I

    move-result v4

    const/16 v5, 0x64

    if-lt v4, v5, :cond_0

    .line 63
    invoke-interface {v3}, Landroid/database/Cursor;->moveToFirst()Z

    const/4 v4, 0x0

    .line 64
    invoke-interface {v3, v4}, Landroid/database/Cursor;->getInt(I)I

    move-result v5

    const-string v6, "LOG_MESSAGES"

    const-string v7, "rowid = ?"

    const/4 v8, 0x1

    .line 65
    new-array v8, v8, [Ljava/lang/String;

    invoke-static {v5}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v5

    aput-object v5, v8, v4

    invoke-virtual {v2, v6, v7, v8}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_1

    :catch_0
    move-exception v4

    goto :goto_0

    :catch_1
    move-exception v4

    move-object v3, v1

    :goto_0
    :try_start_4
    const-string v5, "LogSqliteStorage"

    .line 69
    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "Error in rotation of logs + "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v6, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v5, v4}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    const-string v4, "LOG_MESSAGES"

    .line 71
    invoke-virtual {v2, v4, v1, v1}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    :cond_0
    :goto_1
    const-string v4, "LOG_MESSAGES"

    .line 74
    invoke-static {p1}, Lcom/helpshift/logger/adapters/LogStorageModelAdapter;->toContentValues(Lcom/helpshift/logger/model/LogModel;)Landroid/content/ContentValues;

    move-result-object p1

    invoke-virtual {v2, v4, v1, p1}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    .line 75
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_3
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    if-eqz v2, :cond_1

    .line 83
    :try_start_5
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_2
    .catchall {:try_start_5 .. :try_end_5} :catchall_4

    goto :goto_2

    :catch_2
    move-exception p1

    :try_start_6
    const-string v1, "LogSqliteStorage"

    const-string v2, "Error inserting log inside finally block: "

    .line 87
    invoke-static {v1, v2, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_1
    :goto_2
    if-eqz v3, :cond_3

    .line 91
    :goto_3
    invoke-interface {v3}, Landroid/database/Cursor;->close()V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_4

    goto :goto_7

    :catchall_0
    move-exception p1

    goto :goto_8

    :catch_3
    move-exception p1

    goto :goto_4

    :catchall_1
    move-exception p1

    move-object v3, v1

    goto :goto_8

    :catch_4
    move-exception p1

    move-object v3, v1

    :goto_4
    move-object v1, v2

    goto :goto_5

    :catchall_2
    move-exception p1

    move-object v2, v1

    move-object v3, v2

    goto :goto_8

    :catch_5
    move-exception p1

    move-object v3, v1

    :goto_5
    :try_start_7
    const-string v2, "LogSqliteStorage"

    const-string v4, "Error inserting log : "

    .line 78
    invoke-static {v2, v4, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I
    :try_end_7
    .catchall {:try_start_7 .. :try_end_7} :catchall_3

    if-eqz v1, :cond_2

    .line 83
    :try_start_8
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_8
    .catch Ljava/lang/Exception; {:try_start_8 .. :try_end_8} :catch_6
    .catchall {:try_start_8 .. :try_end_8} :catchall_4

    goto :goto_6

    :catch_6
    move-exception p1

    :try_start_9
    const-string v1, "LogSqliteStorage"

    const-string v2, "Error inserting log inside finally block: "

    .line 87
    invoke-static {v1, v2, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_2
    :goto_6
    if-eqz v3, :cond_3

    goto :goto_3

    .line 94
    :cond_3
    :goto_7
    monitor-exit v0
    :try_end_9
    .catchall {:try_start_9 .. :try_end_9} :catchall_4

    return-void

    :catchall_3
    move-exception p1

    move-object v2, v1

    :goto_8
    if-eqz v2, :cond_4

    .line 83
    :try_start_a
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_a
    .catch Ljava/lang/Exception; {:try_start_a .. :try_end_a} :catch_7
    .catchall {:try_start_a .. :try_end_a} :catchall_4

    goto :goto_9

    :catchall_4
    move-exception p1

    goto :goto_a

    :catch_7
    move-exception v1

    :try_start_b
    const-string v2, "LogSqliteStorage"

    const-string v4, "Error inserting log inside finally block: "

    .line 87
    invoke-static {v2, v4, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_4
    :goto_9
    if-eqz v3, :cond_5

    .line 91
    invoke-interface {v3}, Landroid/database/Cursor;->close()V

    .line 93
    :cond_5
    throw p1

    .line 94
    :goto_a
    monitor-exit v0
    :try_end_b
    .catchall {:try_start_b .. :try_end_b} :catchall_4

    throw p1
.end method
