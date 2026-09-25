.class Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;
.super Landroid/database/sqlite/SQLiteOpenHelper;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/android/apps/analytics/PersistentHitStore;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "DataBaseHelper"
.end annotation


# instance fields
.field private final databaseVersion:I

.field private final store:Lcom/google/android/apps/analytics/PersistentHitStore;


# direct methods
.method public constructor <init>(Landroid/content/Context;Lcom/google/android/apps/analytics/PersistentHitStore;)V
    .locals 2

    const-string v0, "google_analytics.db"

    const/4 v1, 0x5

    invoke-direct {p0, p1, v0, v1, p2}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;-><init>(Landroid/content/Context;Ljava/lang/String;ILcom/google/android/apps/analytics/PersistentHitStore;)V

    return-void
.end method

.method constructor <init>(Landroid/content/Context;Ljava/lang/String;ILcom/google/android/apps/analytics/PersistentHitStore;)V
    .locals 1

    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, v0, p3}, Landroid/database/sqlite/SQLiteOpenHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Landroid/database/sqlite/SQLiteDatabase$CursorFactory;I)V

    iput p3, p0, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->databaseVersion:I

    iput-object p4, p0, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->store:Lcom/google/android/apps/analytics/PersistentHitStore;

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Lcom/google/android/apps/analytics/PersistentHitStore;)V
    .locals 1

    const/4 v0, 0x5

    invoke-direct {p0, p1, p2, v0, p3}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;-><init>(Landroid/content/Context;Ljava/lang/String;ILcom/google/android/apps/analytics/PersistentHitStore;)V

    return-void
.end method

.method private createECommerceTables(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 1

    const-string v0, "DROP TABLE IF EXISTS transaction_events;"

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    invoke-static {}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$400()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const-string v0, "DROP TABLE IF EXISTS item_events;"

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    invoke-static {}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$500()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method

.method private createHitTable(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 1

    const-string v0, "DROP TABLE IF EXISTS hits;"

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    invoke-static {}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$600()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method

.method private createReferrerTable(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 1

    const-string v0, "DROP TABLE IF EXISTS referrer;"

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const-string v0, "CREATE TABLE IF NOT EXISTS referrer (referrer TEXT PRIMARY KEY NOT NULL,timestamp_referrer INTEGER NOT NULL,referrer_visit INTEGER NOT NULL DEFAULT 1,referrer_index INTEGER NOT NULL DEFAULT 1);"

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method

.method private fixReferrerTable(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 14

    const/4 v1, 0x0

    :try_start_0
    const-string v3, "referrer"

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    move-object v2, p1

    invoke-virtual/range {v2 .. v9}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v2
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    :try_start_1
    invoke-interface {v2}, Landroid/database/Cursor;->getColumnNames()[Ljava/lang/String;

    move-result-object v0

    const/4 v3, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    :goto_0
    array-length v6, v0

    const/4 v7, 0x1

    if-ge v3, v6, :cond_2

    aget-object v6, v0, v3

    const-string v8, "referrer_index"

    invoke-virtual {v6, v8}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_0

    const/4 v4, 0x1

    goto :goto_1

    :cond_0
    aget-object v6, v0, v3

    const-string v8, "referrer_visit"

    invoke-virtual {v6, v8}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_1

    const/4 v5, 0x1

    :cond_1
    :goto_1
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :cond_2
    if-eqz v4, :cond_3

    if-nez v5, :cond_8

    :cond_3
    invoke-interface {v2}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v0

    if-eqz v0, :cond_6

    const-string v0, "referrer_visit"

    invoke-interface {v2, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    const-string v3, "referrer_index"

    invoke-interface {v2, v3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v3

    new-instance v4, Lcom/google/android/apps/analytics/Referrer;

    const-string v5, "referrer"

    invoke-interface {v2, v5}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v5

    invoke-interface {v2, v5}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v9

    const-string v5, "timestamp_referrer"

    invoke-interface {v2, v5}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v5

    invoke-interface {v2, v5}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v10

    const/4 v5, -0x1

    if-ne v0, v5, :cond_4

    const/4 v12, 0x1

    goto :goto_2

    :cond_4
    invoke-interface {v2, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result v0

    move v12, v0

    :goto_2
    if-ne v3, v5, :cond_5

    const/4 v13, 0x1

    goto :goto_3

    :cond_5
    invoke-interface {v2, v3}, Landroid/database/Cursor;->getInt(I)I

    move-result v7

    move v13, v7

    :goto_3
    move-object v8, v4

    invoke-direct/range {v8 .. v13}, Lcom/google/android/apps/analytics/Referrer;-><init>(Ljava/lang/String;JII)V

    goto :goto_4

    :cond_6
    move-object v4, v1

    :goto_4
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    invoke-direct {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->createReferrerTable(Landroid/database/sqlite/SQLiteDatabase;)V

    if-eqz v4, :cond_7

    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    const-string v3, "referrer"

    invoke-virtual {v4}, Lcom/google/android/apps/analytics/Referrer;->getReferrerString()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v3, v5}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v3, "timestamp_referrer"

    invoke-virtual {v4}, Lcom/google/android/apps/analytics/Referrer;->getTimeStamp()J

    move-result-wide v5

    invoke-static {v5, v6}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v5

    invoke-virtual {v0, v3, v5}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string v3, "referrer_visit"

    invoke-virtual {v4}, Lcom/google/android/apps/analytics/Referrer;->getVisit()I

    move-result v5

    invoke-static {v5}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v5

    invoke-virtual {v0, v3, v5}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v3, "referrer_index"

    invoke-virtual {v4}, Lcom/google/android/apps/analytics/Referrer;->getIndex()I

    move-result v4

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    invoke-virtual {v0, v3, v4}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v3, "referrer"

    invoke-virtual {p1, v3, v1, v0}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    :cond_7
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :cond_8
    if-eqz v2, :cond_9

    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    :cond_9
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v0

    if-eqz v0, :cond_b

    goto :goto_6

    :catchall_0
    move-exception v0

    goto :goto_7

    :catch_0
    move-exception v0

    move-object v1, v2

    goto :goto_5

    :catchall_1
    move-exception v0

    move-object v2, v1

    goto :goto_7

    :catch_1
    move-exception v0

    :goto_5
    :try_start_2
    const-string v2, "GoogleAnalyticsTracker"

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz v1, :cond_a

    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    :cond_a
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v0

    if-eqz v0, :cond_b

    :goto_6
    invoke-static {p1}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$900(Landroid/database/sqlite/SQLiteDatabase;)Z

    :cond_b
    return-void

    :goto_7
    if-eqz v2, :cond_c

    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    :cond_c
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v1

    if-eqz v1, :cond_d

    invoke-static {p1}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$900(Landroid/database/sqlite/SQLiteDatabase;)Z

    :cond_d
    throw v0
.end method

.method private migrateEventsToHits(Landroid/database/sqlite/SQLiteDatabase;I)V
    .locals 4

    iget-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->store:Lcom/google/android/apps/analytics/PersistentHitStore;

    invoke-virtual {v0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore;->loadExistingSession(Landroid/database/sqlite/SQLiteDatabase;)V

    iget-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->store:Lcom/google/android/apps/analytics/PersistentHitStore;

    iget-object v1, p0, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->store:Lcom/google/android/apps/analytics/PersistentHitStore;

    invoke-virtual {v1, p1}, Lcom/google/android/apps/analytics/PersistentHitStore;->getVisitorVarBuffer(Landroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/CustomVariableBuffer;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$702(Lcom/google/android/apps/analytics/PersistentHitStore;Lcom/google/android/apps/analytics/CustomVariableBuffer;)Lcom/google/android/apps/analytics/CustomVariableBuffer;

    iget-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->store:Lcom/google/android/apps/analytics/PersistentHitStore;

    const/16 v1, 0x3e8

    invoke-virtual {v0, v1, p1, p2}, Lcom/google/android/apps/analytics/PersistentHitStore;->peekEvents(ILandroid/database/sqlite/SQLiteDatabase;I)[Lcom/google/android/apps/analytics/Event;

    move-result-object p2

    const/4 v0, 0x0

    const/4 v1, 0x0

    :goto_0
    array-length v2, p2

    if-ge v1, v2, :cond_0

    iget-object v2, p0, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->store:Lcom/google/android/apps/analytics/PersistentHitStore;

    aget-object v3, p2, v1

    invoke-static {v2, v3, p1, v0}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$800(Lcom/google/android/apps/analytics/PersistentHitStore;Lcom/google/android/apps/analytics/Event;Landroid/database/sqlite/SQLiteDatabase;Z)V

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    const-string p2, "DELETE from events;"

    invoke-virtual {p1, p2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const-string p2, "DELETE from item_events;"

    invoke-virtual {p1, p2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const-string p2, "DELETE from transaction_events;"

    invoke-virtual {p1, p2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const-string p2, "DELETE from custom_variables;"

    invoke-virtual {p1, p2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method

.method private migratePreV4Referrer(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 14

    const/4 v0, 0x0

    :try_start_0
    const-string v2, "install_referrer"

    const-string v1, "referrer"

    filled-new-array {v1}, [Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    move-object v1, p1

    invoke-virtual/range {v1 .. v8}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v1
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const-wide/16 v2, 0x0

    :try_start_1
    invoke-interface {v1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v4

    if-eqz v4, :cond_1

    const/4 v4, 0x0

    invoke-interface {v1, v4}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v5

    const-string v7, "session"

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    const/4 v11, 0x0

    const/4 v12, 0x0

    const/4 v13, 0x0

    move-object v6, p1

    invoke-virtual/range {v6 .. v13}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v6
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    :try_start_2
    invoke-interface {v6}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v7

    if-eqz v7, :cond_0

    invoke-interface {v6, v4}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v2

    :cond_0
    new-instance v4, Landroid/content/ContentValues;

    invoke-direct {v4}, Landroid/content/ContentValues;-><init>()V

    const-string v7, "referrer"

    invoke-virtual {v4, v7, v5}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v5, "timestamp_referrer"

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v4, v5, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string v2, "referrer_visit"

    const/4 v3, 0x1

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v5

    invoke-virtual {v4, v2, v5}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v2, "referrer_index"

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-virtual {v4, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v2, "referrer"

    invoke-virtual {p1, v2, v0, v4}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J
    :try_end_2
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    move-object v0, v6

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_4

    :catch_0
    move-exception p1

    goto :goto_1

    :cond_1
    :goto_0
    if-eqz v1, :cond_2

    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    :cond_2
    if-eqz v0, :cond_4

    invoke-interface {v0}, Landroid/database/Cursor;->close()V

    goto :goto_3

    :catchall_1
    move-exception p1

    move-object v6, v0

    goto :goto_4

    :catch_1
    move-exception p1

    move-object v6, v0

    :goto_1
    move-object v0, v1

    goto :goto_2

    :catchall_2
    move-exception p1

    move-object v1, v0

    move-object v6, v1

    goto :goto_4

    :catch_2
    move-exception p1

    move-object v6, v0

    :goto_2
    :try_start_3
    const-string v1, "GoogleAnalyticsTracker"

    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_3

    if-eqz v0, :cond_3

    invoke-interface {v0}, Landroid/database/Cursor;->close()V

    :cond_3
    if-eqz v6, :cond_4

    invoke-interface {v6}, Landroid/database/Cursor;->close()V

    :cond_4
    :goto_3
    return-void

    :catchall_3
    move-exception p1

    move-object v1, v0

    :goto_4
    if-eqz v1, :cond_5

    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    :cond_5
    if-eqz v6, :cond_6

    invoke-interface {v6}, Landroid/database/Cursor;->close()V

    :cond_6
    throw p1
.end method


# virtual methods
.method createCustomVariableTables(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 4

    const-string v0, "DROP TABLE IF EXISTS custom_variables;"

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    invoke-static {}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$200()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const-string v0, "DROP TABLE IF EXISTS custom_var_cache;"

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    invoke-static {}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$300()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const/4 v0, 0x1

    :goto_0
    const/4 v1, 0x5

    if-gt v0, v1, :cond_0

    new-instance v1, Landroid/content/ContentValues;

    invoke-direct {v1}, Landroid/content/ContentValues;-><init>()V

    const-string v2, "event_id"

    const/4 v3, 0x0

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v2, "cv_index"

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v2, "cv_name"

    const-string v3, ""

    invoke-virtual {v1, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "cv_scope"

    const/4 v3, 0x3

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v2, "cv_value"

    const-string v3, ""

    invoke-virtual {v1, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "custom_var_cache"

    const-string v3, "event_id"

    invoke-virtual {p1, v2, v3, v1}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_0
    return-void
.end method

.method public onCreate(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 2

    const-string v0, "DROP TABLE IF EXISTS events;"

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    invoke-static {}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$000()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const-string v0, "DROP TABLE IF EXISTS install_referrer;"

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const-string v0, "CREATE TABLE install_referrer (referrer TEXT PRIMARY KEY NOT NULL);"

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const-string v0, "DROP TABLE IF EXISTS session;"

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    invoke-static {}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$100()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->databaseVersion:I

    const/4 v1, 0x1

    if-le v0, v1, :cond_0

    invoke-virtual {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->createCustomVariableTables(Landroid/database/sqlite/SQLiteDatabase;)V

    :cond_0
    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->databaseVersion:I

    const/4 v1, 0x2

    if-le v0, v1, :cond_1

    invoke-direct {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->createECommerceTables(Landroid/database/sqlite/SQLiteDatabase;)V

    :cond_1
    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->databaseVersion:I

    const/4 v1, 0x3

    if-le v0, v1, :cond_2

    invoke-direct {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->createHitTable(Landroid/database/sqlite/SQLiteDatabase;)V

    invoke-direct {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->createReferrerTable(Landroid/database/sqlite/SQLiteDatabase;)V

    :cond_2
    return-void
.end method

.method public onDowngrade(Landroid/database/sqlite/SQLiteDatabase;II)V
    .locals 9

    const-string v0, "GoogleAnalyticsTracker"

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Downgrading database version from "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string p2, " to "

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string p2, " not recommended."

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {v0, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    const-string p2, "CREATE TABLE IF NOT EXISTS referrer (referrer TEXT PRIMARY KEY NOT NULL,timestamp_referrer INTEGER NOT NULL,referrer_visit INTEGER NOT NULL DEFAULT 1,referrer_index INTEGER NOT NULL DEFAULT 1);"

    invoke-virtual {p1, p2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    invoke-static {}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$600()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    invoke-static {}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$300()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    invoke-static {}, Lcom/google/android/apps/analytics/PersistentHitStore;->access$100()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    new-instance p2, Ljava/util/HashSet;

    invoke-direct {p2}, Ljava/util/HashSet;-><init>()V

    const-string v1, "custom_var_cache"

    const/4 v2, 0x0

    const/4 v3, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    move-object v0, p1

    invoke-virtual/range {v0 .. v8}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p3

    :goto_0
    :try_start_0
    invoke-interface {p3}, Landroid/database/Cursor;->moveToNext()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "cv_index"

    invoke-interface {p3, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p3, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result v0

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-interface {p2, v0}, Ljava/util/Set;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :cond_0
    :goto_1
    invoke-interface {p3}, Landroid/database/Cursor;->close()V

    goto :goto_2

    :catchall_0
    move-exception p1

    goto/16 :goto_5

    :catch_0
    move-exception v0

    :try_start_1
    const-string v1, "GoogleAnalyticsTracker"

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Error on downgrade: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_1

    :goto_2
    const/4 p3, 0x1

    :goto_3
    const/4 v0, 0x5

    if-gt p3, v0, :cond_2

    :try_start_2
    invoke-static {p3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-interface {p2, v0}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    const-string v1, "event_id"

    const/4 v2, 0x0

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v1, "cv_index"

    invoke-static {p3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v1, "cv_name"

    const-string v2, ""

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "cv_scope"

    const/4 v2, 0x3

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v1, "cv_value"

    const-string v2, ""

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "custom_var_cache"

    const-string v2, "event_id"

    invoke-virtual {p1, v1, v2, v0}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J
    :try_end_2
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_2 .. :try_end_2} :catch_1

    goto :goto_4

    :catch_1
    move-exception v0

    const-string v1, "GoogleAnalyticsTracker"

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Error inserting custom variable on downgrade: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    :goto_4
    add-int/lit8 p3, p3, 0x1

    goto :goto_3

    :cond_2
    return-void

    :goto_5
    invoke-interface {p3}, Landroid/database/Cursor;->close()V

    throw p1
.end method

.method public onOpen(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 1

    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->isReadOnly()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string p1, "GoogleAnalyticsTracker"

    const-string v0, "Warning: Need to update database, but it\'s read only."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    invoke-direct {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->fixReferrerTable(Landroid/database/sqlite/SQLiteDatabase;)V

    return-void
.end method

.method public onUpgrade(Landroid/database/sqlite/SQLiteDatabase;II)V
    .locals 2

    if-le p2, p3, :cond_0

    invoke-virtual {p0, p1, p2, p3}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->onDowngrade(Landroid/database/sqlite/SQLiteDatabase;II)V

    return-void

    :cond_0
    const/4 v0, 0x2

    if-ge p2, v0, :cond_1

    const/4 v1, 0x1

    if-le p3, v1, :cond_1

    invoke-virtual {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->createCustomVariableTables(Landroid/database/sqlite/SQLiteDatabase;)V

    :cond_1
    const/4 v1, 0x3

    if-ge p2, v1, :cond_2

    if-le p3, v0, :cond_2

    invoke-direct {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->createECommerceTables(Landroid/database/sqlite/SQLiteDatabase;)V

    :cond_2
    const/4 v0, 0x4

    if-ge p2, v0, :cond_3

    if-le p3, v1, :cond_3

    invoke-direct {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->createHitTable(Landroid/database/sqlite/SQLiteDatabase;)V

    invoke-direct {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->createReferrerTable(Landroid/database/sqlite/SQLiteDatabase;)V

    invoke-direct {p0, p1, p2}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->migrateEventsToHits(Landroid/database/sqlite/SQLiteDatabase;I)V

    invoke-direct {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->migratePreV4Referrer(Landroid/database/sqlite/SQLiteDatabase;)V

    :cond_3
    return-void
.end method
