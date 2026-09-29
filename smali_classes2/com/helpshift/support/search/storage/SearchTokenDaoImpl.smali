.class public Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;
.super Ljava/lang/Object;
.source "SearchTokenDaoImpl.java"

# interfaces
.implements Lcom/helpshift/support/search/SearchTokenDao;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/search/storage/SearchTokenDaoImpl$LazyHolder;
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_SearchToknDao"


# instance fields
.field private final dbHelper:Landroid/database/sqlite/SQLiteOpenHelper;

.field private final scoreMapKeyValueStringSeparator:C

.field private final scoreMapStringSeparator:C


# direct methods
.method constructor <init>()V
    .locals 2

    .line 24
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/16 v0, 0x24

    .line 21
    iput-char v0, p0, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->scoreMapStringSeparator:C

    const/16 v0, 0x3a

    .line 22
    iput-char v0, p0, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->scoreMapKeyValueStringSeparator:C

    .line 25
    new-instance v0, Lcom/helpshift/support/search/storage/SearchDBHelper;

    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1}, Lcom/helpshift/support/search/storage/SearchDBHelper;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->dbHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    return-void
.end method

.method private convertScoreMapToScoreString(Ljava/util/Map;)Ljava/lang/String;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/Integer;",
            "Ljava/lang/Double;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .line 123
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    .line 125
    invoke-interface {p1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    const/4 v1, 0x1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    if-eqz v1, :cond_0

    const/4 v1, 0x0

    goto :goto_1

    :cond_0
    const/16 v3, 0x24

    .line 130
    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 132
    :goto_1
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const/16 v3, 0x3a

    .line 133
    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 134
    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    goto :goto_0

    .line 136
    :cond_1
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private convertScoreStringToScoreMap(Ljava/lang/String;)Ljava/util/Map;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/Integer;",
            "Ljava/lang/Double;",
            ">;"
        }
    .end annotation

    .line 140
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    if-nez p1, :cond_0

    return-object v0

    :cond_0
    const-string v1, "[$]"

    .line 144
    invoke-virtual {p1, v1}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p1

    const/4 v1, 0x0

    const/4 v2, 0x0

    .line 145
    :goto_0
    array-length v3, p1

    if-ge v2, v3, :cond_2

    .line 146
    aget-object v3, p1, v2

    if-eqz v3, :cond_1

    .line 147
    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v4

    if-lez v4, :cond_1

    const-string v4, "[:]"

    .line 148
    invoke-virtual {v3, v4}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_1

    .line 149
    array-length v4, v3

    const/4 v5, 0x2

    if-ne v4, v5, :cond_1

    .line 150
    aget-object v4, v3, v1

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(Ljava/lang/String;)Ljava/lang/Integer;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/Integer;->intValue()I

    move-result v4

    const/4 v5, 0x1

    .line 151
    aget-object v3, v3, v5

    invoke-static {v3}, Ljava/lang/Double;->valueOf(Ljava/lang/String;)Ljava/lang/Double;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Double;->doubleValue()D

    move-result-wide v5

    .line 152
    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-static {v5, v6}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v4

    invoke-interface {v0, v3, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_2
    return-object v0
.end method

.method public static getInstance()Lcom/helpshift/support/search/SearchTokenDao;
    .locals 1

    .line 29
    sget-object v0, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl$LazyHolder;->INSTANCE:Lcom/helpshift/support/search/SearchTokenDao;

    return-object v0
.end method


# virtual methods
.method public clear()V
    .locals 4

    .line 111
    iget-object v0, p0, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->dbHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    monitor-enter v0

    .line 113
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->dbHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteOpenHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    const-string v2, "search_token_table"

    const/4 v3, 0x0

    .line 114
    invoke-virtual {v1, v2, v3, v3}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I
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
    const-string v2, "Helpshift_SearchToknDao"

    const-string v3, "Error occurred when calling clear method"

    .line 117
    invoke-static {v2, v3, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 119
    :goto_0
    monitor-exit v0

    return-void

    :goto_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1
.end method

.method public get(Ljava/lang/String;)Lcom/helpshift/support/search/SearchTokenDto;
    .locals 12
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 77
    iget-object v0, p0, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->dbHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    monitor-enter v0

    const/4 v1, 0x0

    .line 79
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->dbHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteOpenHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v3

    const-string v2, "token"

    const-string v4, "type"

    const-string v5, "score"

    .line 80
    filled-new-array {v2, v4, v5}, [Ljava/lang/String;

    move-result-object v5

    const-string v4, "search_token_table"

    const-string v6, "token=?"

    const/4 v2, 0x1

    .line 85
    new-array v7, v2, [Ljava/lang/String;

    const/4 v2, 0x0

    aput-object p1, v7, v2

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    .line 86
    invoke-virtual/range {v3 .. v10}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 88
    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->getCount()I

    move-result v2

    if-lez v2, :cond_0

    .line 89
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    const-string v2, "token"

    .line 90
    invoke-interface {p1, v2}, Landroid/database/Cursor;->getColumnIndexOrThrow(Ljava/lang/String;)I

    move-result v2

    invoke-interface {p1, v2}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v2

    const-string v3, "type"

    .line 91
    invoke-interface {p1, v3}, Landroid/database/Cursor;->getColumnIndexOrThrow(Ljava/lang/String;)I

    move-result v3

    invoke-interface {p1, v3}, Landroid/database/Cursor;->getInt(I)I

    move-result v3

    const-string v4, "score"

    .line 92
    invoke-interface {p1, v4}, Landroid/database/Cursor;->getColumnIndexOrThrow(Ljava/lang/String;)I

    move-result v4

    invoke-interface {p1, v4}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v4

    .line 93
    invoke-direct {p0, v4}, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->convertScoreStringToScoreMap(Ljava/lang/String;)Ljava/util/Map;

    move-result-object v4

    .line 94
    new-instance v5, Lcom/helpshift/support/search/SearchTokenDto;

    invoke-direct {v5, v2, v3, v4}, Lcom/helpshift/support/search/SearchTokenDto;-><init>(Ljava/lang/String;ILjava/util/Map;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    move-object v1, v5

    :cond_0
    if-eqz p1, :cond_1

    .line 102
    :goto_0
    :try_start_2
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    goto :goto_2

    :catch_0
    move-exception v2

    goto :goto_1

    :catchall_0
    move-exception p1

    move-object v11, v1

    move-object v1, p1

    move-object p1, v11

    goto :goto_3

    :catch_1
    move-exception v2

    move-object p1, v1

    :goto_1
    :try_start_3
    const-string v3, "Helpshift_SearchToknDao"

    const-string v4, "Error occurred when calling get method"

    .line 98
    invoke-static {v3, v4, v2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    if-eqz p1, :cond_1

    goto :goto_0

    .line 105
    :cond_1
    :goto_2
    :try_start_4
    monitor-exit v0

    return-object v1

    :catchall_1
    move-exception v1

    :goto_3
    if-eqz p1, :cond_2

    .line 102
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    goto :goto_4

    :catchall_2
    move-exception p1

    goto :goto_5

    .line 104
    :cond_2
    :goto_4
    throw v1

    .line 105
    :goto_5
    monitor-exit v0
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    throw p1
.end method

.method public save(Ljava/util/List;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/support/search/SearchTokenDto;",
            ">;)V"
        }
    .end annotation

    if-nez p1, :cond_0

    return-void

    .line 37
    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 38
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/support/search/SearchTokenDto;

    .line 39
    iget-object v2, v1, Lcom/helpshift/support/search/SearchTokenDto;->scoreMap:Ljava/util/Map;

    invoke-direct {p0, v2}, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->convertScoreMapToScoreString(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v2

    .line 40
    new-instance v3, Landroid/content/ContentValues;

    invoke-direct {v3}, Landroid/content/ContentValues;-><init>()V

    const-string v4, "token"

    .line 41
    iget-object v5, v1, Lcom/helpshift/support/search/SearchTokenDto;->wordValue:Ljava/lang/String;

    invoke-virtual {v3, v4, v5}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v4, "type"

    .line 42
    iget v1, v1, Lcom/helpshift/support/search/SearchTokenDto;->wordType:I

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-virtual {v3, v4, v1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v1, "score"

    .line 43
    invoke-virtual {v3, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 44
    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 47
    :cond_1
    iget-object p1, p0, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->dbHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    monitor-enter p1

    const/4 v1, 0x0

    .line 49
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->dbHelper:Landroid/database/sqlite/SQLiteOpenHelper;

    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteOpenHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 50
    :try_start_1
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 51
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/content/ContentValues;

    const-string v4, "search_token_table"

    .line 52
    invoke-virtual {v2, v4, v1, v3}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    goto :goto_1

    .line 54
    :cond_2
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz v2, :cond_3

    .line 61
    :try_start_2
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v0

    if-eqz v0, :cond_3

    .line 62
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    goto :goto_4

    :catch_0
    move-exception v0

    :try_start_3
    const-string v1, "Helpshift_SearchToknDao"

    const-string v2, "Error occurred when calling save method inside finally block"

    .line 66
    :goto_2
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_4

    :catchall_0
    move-exception v0

    goto :goto_5

    :catch_1
    move-exception v0

    move-object v1, v2

    goto :goto_3

    :catchall_1
    move-exception v0

    move-object v2, v1

    goto :goto_5

    :catch_2
    move-exception v0

    :goto_3
    :try_start_4
    const-string v2, "Helpshift_SearchToknDao"

    const-string v3, "Error occurred when calling save method"

    .line 57
    invoke-static {v2, v3, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v1, :cond_3

    .line 61
    :try_start_5
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v0

    if-eqz v0, :cond_3

    .line 62
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_3
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    goto :goto_4

    :catch_3
    move-exception v0

    :try_start_6
    const-string v1, "Helpshift_SearchToknDao"

    const-string v2, "Error occurred when calling save method inside finally block"

    goto :goto_2

    .line 69
    :cond_3
    :goto_4
    monitor-exit p1
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    return-void

    :goto_5
    if-eqz v2, :cond_4

    .line 61
    :try_start_7
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v1

    if-eqz v1, :cond_4

    .line 62
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_7
    .catch Ljava/lang/Exception; {:try_start_7 .. :try_end_7} :catch_4
    .catchall {:try_start_7 .. :try_end_7} :catchall_2

    goto :goto_6

    :catchall_2
    move-exception v0

    goto :goto_7

    :catch_4
    move-exception v1

    :try_start_8
    const-string v2, "Helpshift_SearchToknDao"

    const-string v3, "Error occurred when calling save method inside finally block"

    .line 66
    invoke-static {v2, v3, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 68
    :cond_4
    :goto_6
    throw v0

    .line 69
    :goto_7
    monitor-exit p1
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_2

    throw v0
.end method
