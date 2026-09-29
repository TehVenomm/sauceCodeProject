.class public Lcom/helpshift/storage/KeyValueDbStorage;
.super Ljava/lang/Object;
.source "KeyValueDbStorage.java"

# interfaces
.implements Lcom/helpshift/storage/KeyValueStorage;


# static fields
.field private static final TAG:Ljava/lang/String; = "HS_KeyValueDB"


# instance fields
.field private backupData:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;"
        }
    .end annotation
.end field

.field private backupFileName:Ljava/lang/String;

.field private helper:Landroid/database/sqlite/SQLiteOpenHelper;


# direct methods
.method public constructor <init>(Landroid/database/sqlite/SQLiteOpenHelper;Ljava/lang/String;)V
    .locals 0

    .line 27
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 28
    iput-object p1, p0, Lcom/helpshift/storage/KeyValueDbStorage;->helper:Landroid/database/sqlite/SQLiteOpenHelper;

    .line 29
    iput-object p2, p0, Lcom/helpshift/storage/KeyValueDbStorage;->backupFileName:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public get(Ljava/lang/String;)Ljava/lang/Object;
    .locals 10

    .line 122
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    .line 129
    :cond_0
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/storage/KeyValueDbStorage;->helper:Landroid/database/sqlite/SQLiteOpenHelper;

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteOpenHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    const-string v5, "key=?"

    const/4 v0, 0x1

    .line 131
    new-array v6, v0, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v6, v3

    const-string v3, "key_value_store"

    const/4 v4, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    .line 132
    invoke-virtual/range {v2 .. v9}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 133
    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v2
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz v2, :cond_1

    .line 135
    :try_start_2
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getBlob(I)[B

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/util/ByteArrayUtil;->toObject([B)Ljava/lang/Object;

    move-result-object v0
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    move-object v1, v0

    :catch_0
    :cond_1
    if-eqz p1, :cond_2

    .line 144
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_2
    return-object v1

    :catchall_0
    move-exception v0

    move-object v1, p1

    goto :goto_0

    :catchall_1
    move-exception v0

    :goto_0
    if-eqz v1, :cond_3

    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    .line 146
    :cond_3
    throw v0
.end method

.method public removeAllKeys()V
    .locals 3

    .line 164
    iget-object v0, p0, Lcom/helpshift/storage/KeyValueDbStorage;->helper:Landroid/database/sqlite/SQLiteOpenHelper;

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteOpenHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    const-string v1, "key_value_store"

    const/4 v2, 0x0

    .line 165
    invoke-virtual {v0, v1, v2, v2}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    return-void
.end method

.method public removeKey(Ljava/lang/String;)V
    .locals 4

    .line 152
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 155
    :cond_0
    iget-object v0, p0, Lcom/helpshift/storage/KeyValueDbStorage;->helper:Landroid/database/sqlite/SQLiteOpenHelper;

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteOpenHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    const-string v1, "key=?"

    const/4 v2, 0x1

    .line 158
    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    const-string p1, "key_value_store"

    .line 159
    invoke-virtual {v0, p1, v1, v2}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    return-void
.end method

.method public set(Ljava/lang/String;Ljava/io/Serializable;)Z
    .locals 7

    .line 34
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_3

    if-nez p2, :cond_0

    goto :goto_1

    .line 38
    :cond_0
    iget-object v0, p0, Lcom/helpshift/storage/KeyValueDbStorage;->helper:Landroid/database/sqlite/SQLiteOpenHelper;

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteOpenHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    const-string v2, "key=?"

    const/4 v3, 0x1

    .line 41
    new-array v4, v3, [Ljava/lang/String;

    aput-object p1, v4, v1

    .line 42
    new-instance v5, Landroid/content/ContentValues;

    invoke-direct {v5}, Landroid/content/ContentValues;-><init>()V

    const-string v6, "key"

    .line 43
    invoke-virtual {v5, v6, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    :try_start_0
    const-string p1, "value"

    .line 45
    invoke-static {p2}, Lcom/helpshift/util/ByteArrayUtil;->toByteArray(Ljava/lang/Object;)[B

    move-result-object p2

    invoke-virtual {v5, p1, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;[B)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    const-string p1, "key_value_store"

    .line 52
    invoke-static {v0, p1, v2, v4}, Lcom/helpshift/util/DatabaseUtils;->exists(Landroid/database/sqlite/SQLiteDatabase;Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_1

    const-string p1, "key_value_store"

    .line 53
    invoke-virtual {v0, p1, v5, v2, v4}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I

    goto :goto_0

    :cond_1
    const-string p1, "key_value_store"

    const/4 p2, 0x0

    .line 56
    invoke-virtual {v0, p1, p2, v5}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    move-result-wide p1

    const-wide/16 v0, -0x1

    cmp-long v2, p1, v0

    if-eqz v2, :cond_2

    :goto_0
    return v3

    .line 58
    :cond_2
    new-instance p1, Landroid/database/sqlite/SQLiteException;

    const-string p2, "DB insert failed and return -1"

    invoke-direct {p1, p2}, Landroid/database/sqlite/SQLiteException;-><init>(Ljava/lang/String;)V

    throw p1

    :catch_0
    move-exception p1

    const-string p2, "HS_KeyValueDB"

    const-string v0, "Error in serializing value"

    .line 48
    invoke-static {p2, v0, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    return v1

    :cond_3
    :goto_1
    return v1
.end method

.method public setKeyValues(Ljava/util/Map;)Z
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)Z"
        }
    .end annotation

    const/4 v0, 0x0

    if-eqz p1, :cond_4

    .line 67
    invoke-interface {p1}, Ljava/util/Map;->size()I

    move-result v1

    if-nez v1, :cond_0

    goto/16 :goto_1

    .line 71
    :cond_0
    iget-object v1, p0, Lcom/helpshift/storage/KeyValueDbStorage;->helper:Landroid/database/sqlite/SQLiteOpenHelper;

    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteOpenHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    const-string v2, "key=?"

    const-string v3, "INSERT INTO key_value_store VALUES (?,?)"

    .line 73
    invoke-virtual {v1, v3}, Landroid/database/sqlite/SQLiteDatabase;->compileStatement(Ljava/lang/String;)Landroid/database/sqlite/SQLiteStatement;

    move-result-object v3

    const-string v4, "UPDATE key_value_store SET value = ? WHERE key = ?"

    .line 77
    invoke-virtual {v1, v4}, Landroid/database/sqlite/SQLiteDatabase;->compileStatement(Ljava/lang/String;)Landroid/database/sqlite/SQLiteStatement;

    move-result-object v4

    .line 82
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 83
    invoke-interface {p1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_1
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    const/4 v6, 0x1

    if-eqz v5, :cond_3

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/util/Map$Entry;

    .line 84
    invoke-interface {v5}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/lang/CharSequence;

    invoke-static {v7}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v7

    if-nez v7, :cond_1

    invoke-interface {v5}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v7

    if-eqz v7, :cond_1

    .line 86
    :try_start_0
    new-array v7, v6, [Ljava/lang/String;

    invoke-interface {v5}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Ljava/lang/String;

    aput-object v8, v7, v0

    .line 87
    invoke-interface {v5}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v8

    invoke-static {v8}, Lcom/helpshift/util/ByteArrayUtil;->toByteArray(Ljava/lang/Object;)[B

    move-result-object v8

    const-string v9, "key_value_store"

    .line 89
    invoke-static {v1, v9, v2, v7}, Lcom/helpshift/util/DatabaseUtils;->exists(Landroid/database/sqlite/SQLiteDatabase;Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)Z

    move-result v7

    const/4 v9, 0x2

    if-eqz v7, :cond_2

    .line 90
    invoke-interface {v5}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    invoke-virtual {v4, v9, v5}, Landroid/database/sqlite/SQLiteStatement;->bindString(ILjava/lang/String;)V

    .line 91
    invoke-virtual {v4, v6, v8}, Landroid/database/sqlite/SQLiteStatement;->bindBlob(I[B)V

    .line 92
    invoke-virtual {v4}, Landroid/database/sqlite/SQLiteStatement;->execute()V

    .line 93
    invoke-virtual {v4}, Landroid/database/sqlite/SQLiteStatement;->clearBindings()V

    goto :goto_0

    .line 96
    :cond_2
    invoke-interface {v5}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    invoke-virtual {v3, v6, v5}, Landroid/database/sqlite/SQLiteStatement;->bindString(ILjava/lang/String;)V

    .line 97
    invoke-virtual {v3, v9, v8}, Landroid/database/sqlite/SQLiteStatement;->bindBlob(I[B)V

    .line 98
    invoke-virtual {v3}, Landroid/database/sqlite/SQLiteStatement;->execute()V

    .line 99
    invoke-virtual {v3}, Landroid/database/sqlite/SQLiteStatement;->clearBindings()V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v5

    const-string v6, "HS_KeyValueDB"

    const-string v7, "Error in serializing value"

    .line 103
    invoke-static {v6, v7, v5}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0

    .line 109
    :cond_3
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V

    .line 110
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V

    return v6

    :cond_4
    :goto_1
    return v0
.end method
