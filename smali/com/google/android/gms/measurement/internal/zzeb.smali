.class public final Lcom/google/android/gms/measurement/internal/zzeb;
.super Lcom/google/android/gms/measurement/internal/zzg;


# instance fields
.field private final zzjv:Lcom/google/android/gms/measurement/internal/zzea;

.field private zzjw:Z


# direct methods
.method constructor <init>(Lcom/google/android/gms/measurement/internal/zzfj;)V
    .locals 2

    .line 1
    invoke-direct {p0, p1}, Lcom/google/android/gms/measurement/internal/zzg;-><init>(Lcom/google/android/gms/measurement/internal/zzfj;)V

    .line 2
    new-instance p1, Lcom/google/android/gms/measurement/internal/zzea;

    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->getContext()Landroid/content/Context;

    move-result-object v0

    const-string v1, "google_app_measurement_local.db"

    .line 4
    invoke-direct {p1, p0, v0, v1}, Lcom/google/android/gms/measurement/internal/zzea;-><init>(Lcom/google/android/gms/measurement/internal/zzeb;Landroid/content/Context;Ljava/lang/String;)V

    iput-object p1, p0, Lcom/google/android/gms/measurement/internal/zzeb;->zzjv:Lcom/google/android/gms/measurement/internal/zzea;

    return-void
.end method

.method private final getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;
    .locals 2
    .annotation build Landroidx/annotation/WorkerThread;
    .end annotation

    .annotation build Lcom/google/android/gms/common/util/VisibleForTesting;
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/database/sqlite/SQLiteException;
        }
    .end annotation

    .line 308
    iget-boolean v0, p0, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    .line 310
    :cond_0
    iget-object v0, p0, Lcom/google/android/gms/measurement/internal/zzeb;->zzjv:Lcom/google/android/gms/measurement/internal/zzea;

    invoke-virtual {v0}, Lcom/google/android/gms/measurement/internal/zzea;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    if-nez v0, :cond_1

    const/4 v0, 0x1

    .line 312
    iput-boolean v0, p0, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z

    return-object v1

    :cond_1
    return-object v0
.end method

.method private static zza(Landroid/database/sqlite/SQLiteDatabase;)J
    .locals 11

    const/4 v0, 0x0

    :try_start_0
    const-string v2, "messages"

    const-string v1, "rowid"

    .line 294
    filled-new-array {v1}, [Ljava/lang/String;

    move-result-object v3

    const-string v4, "type=?"

    const-string v1, "3"

    filled-new-array {v1}, [Ljava/lang/String;

    move-result-object v5

    const/4 v6, 0x0

    const/4 v7, 0x0

    const-string v8, "rowid desc"

    const-string v9, "1"

    move-object v1, p0

    .line 296
    invoke-virtual/range {v1 .. v9}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 297
    :try_start_1
    invoke-interface {p0}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v0

    if-eqz v0, :cond_1

    const/4 v0, 0x0

    .line 298
    invoke-interface {p0, v0}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz p0, :cond_0

    .line 300
    invoke-interface {p0}, Landroid/database/Cursor;->close()V

    :cond_0
    return-wide v0

    :cond_1
    if-eqz p0, :cond_2

    .line 304
    invoke-interface {p0}, Landroid/database/Cursor;->close()V

    :cond_2
    const-wide/16 v0, -0x1

    return-wide v0

    :catchall_0
    move-exception v0

    move-object v10, v0

    move-object v0, p0

    move-object p0, v10

    goto :goto_0

    :catchall_1
    move-exception p0

    :goto_0
    if-eqz v0, :cond_3

    .line 307
    invoke-interface {v0}, Landroid/database/Cursor;->close()V

    :cond_3
    throw p0
.end method

.method private final zza(I[B)Z
    .locals 17
    .annotation build Landroidx/annotation/WorkerThread;
    .end annotation

    move-object/from16 v1, p0

    .line 17
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzm()V

    .line 18
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzo()V

    .line 19
    iget-boolean v0, v1, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z

    const/4 v2, 0x0

    if-eqz v0, :cond_0

    return v2

    .line 21
    :cond_0
    new-instance v3, Landroid/content/ContentValues;

    invoke-direct {v3}, Landroid/content/ContentValues;-><init>()V

    const-string v0, "type"

    .line 22
    invoke-static/range {p1 .. p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    invoke-virtual {v3, v0, v4}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v0, "entry"

    move-object/from16 v4, p2

    .line 23
    invoke-virtual {v3, v0, v4}, Landroid/content/ContentValues;->put(Ljava/lang/String;[B)V

    const/4 v4, 0x5

    const/4 v5, 0x0

    const/4 v6, 0x5

    :goto_0
    if-ge v5, v4, :cond_e

    const/4 v7, 0x0

    const/4 v8, 0x1

    .line 28
    :try_start_0
    invoke-direct/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzeb;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v9
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_0 .. :try_end_0} :catch_8
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_0 .. :try_end_0} :catch_6
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_5
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    if-nez v9, :cond_2

    .line 30
    :try_start_1
    iput-boolean v8, v1, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_1 .. :try_end_1} :catch_7
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_3

    if-eqz v9, :cond_1

    .line 32
    invoke-virtual {v9}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    :cond_1
    return v2

    :catch_0
    move-exception v0

    move-object v12, v7

    :goto_1
    move-object v7, v9

    goto/16 :goto_3

    :catch_1
    move-exception v0

    goto/16 :goto_6

    .line 34
    :cond_2
    :try_start_2
    invoke-virtual {v9}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    const-wide/16 v10, 0x0

    const-string v0, "select count(1) from messages"

    .line 36
    invoke-virtual {v9, v0, v7}, Landroid/database/sqlite/SQLiteDatabase;->rawQuery(Ljava/lang/String;[Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v12
    :try_end_2
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_2 .. :try_end_2} :catch_1
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_2 .. :try_end_2} :catch_7
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_3

    if-eqz v12, :cond_3

    .line 37
    :try_start_3
    invoke-interface {v12}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v0

    if-eqz v0, :cond_3

    .line 38
    invoke-interface {v12, v2}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v10

    goto :goto_2

    :catchall_0
    move-exception v0

    goto/16 :goto_8

    :catch_2
    move-exception v0

    goto :goto_1

    :catch_3
    move-exception v0

    move-object v7, v12

    goto/16 :goto_6

    :cond_3
    :goto_2
    const-wide/32 v13, 0x186a0

    cmp-long v0, v10, v13

    if-ltz v0, :cond_4

    .line 40
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v0

    const-string v15, "Data loss, local db full"

    invoke-virtual {v0, v15}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V

    const/4 v0, 0x0

    sub-long/2addr v13, v10

    const-wide/16 v10, 0x1

    add-long/2addr v13, v10

    const-string v0, "messages"

    const-string v10, "rowid in (select rowid from messages order by rowid asc limit ?)"

    .line 42
    new-array v11, v8, [Ljava/lang/String;

    .line 43
    invoke-static {v13, v14}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v15

    aput-object v15, v11, v2

    .line 44
    invoke-virtual {v9, v0, v10, v11}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    move-result v0

    int-to-long v10, v0

    cmp-long v0, v10, v13

    if-eqz v0, :cond_4

    .line 46
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v0

    .line 47
    invoke-virtual {v0}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v0

    const-string v15, "Different delete count than expected in local db. expected, received, difference"

    .line 48
    invoke-static {v13, v14}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v4

    .line 49
    invoke-static {v10, v11}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    const/16 v16, 0x0

    sub-long/2addr v13, v10

    .line 50
    invoke-static {v13, v14}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v10

    .line 51
    invoke-virtual {v0, v15, v4, v2, v10}, Lcom/google/android/gms/measurement/internal/zzeh;->zza(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/Object;)V

    :cond_4
    const-string v0, "messages"

    .line 52
    invoke-virtual {v9, v0, v7, v3}, Landroid/database/sqlite/SQLiteDatabase;->insertOrThrow(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    .line 53
    invoke-virtual {v9}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V

    .line 54
    invoke-virtual {v9}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_3
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_3 .. :try_end_3} :catch_3
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_3 .. :try_end_3} :catch_4
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_3 .. :try_end_3} :catch_2
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    if-eqz v12, :cond_5

    .line 56
    invoke-interface {v12}, Landroid/database/Cursor;->close()V

    :cond_5
    if-eqz v9, :cond_6

    .line 58
    invoke-virtual {v9}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    :cond_6
    return v8

    :catch_4
    move-object v7, v12

    goto :goto_5

    :catchall_1
    move-exception v0

    move-object v9, v7

    move-object v12, v9

    goto :goto_8

    :catch_5
    move-exception v0

    move-object v12, v7

    :goto_3
    if-eqz v7, :cond_7

    .line 75
    :try_start_4
    invoke-virtual {v7}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v2

    if-eqz v2, :cond_7

    .line 76
    invoke-virtual {v7}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V

    goto :goto_4

    :catchall_2
    move-exception v0

    move-object v9, v7

    goto :goto_8

    .line 77
    :cond_7
    :goto_4
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v2

    invoke-virtual {v2}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v2

    const-string v4, "Error writing entry to local database"

    invoke-virtual {v2, v4, v0}, Lcom/google/android/gms/measurement/internal/zzeh;->zza(Ljava/lang/String;Ljava/lang/Object;)V

    .line 78
    iput-boolean v8, v1, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    if-eqz v12, :cond_8

    .line 80
    invoke-interface {v12}, Landroid/database/Cursor;->close()V

    :cond_8
    if-eqz v7, :cond_b

    .line 82
    invoke-virtual {v7}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    goto :goto_7

    :catch_6
    move-object v9, v7

    :catch_7
    :goto_5
    int-to-long v10, v6

    .line 68
    :try_start_5
    invoke-static {v10, v11}, Landroid/os/SystemClock;->sleep(J)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_3

    add-int/lit8 v6, v6, 0x14

    if-eqz v7, :cond_9

    .line 71
    invoke-interface {v7}, Landroid/database/Cursor;->close()V

    :cond_9
    if-eqz v9, :cond_b

    .line 73
    invoke-virtual {v9}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    goto :goto_7

    :catch_8
    move-exception v0

    move-object v9, v7

    .line 61
    :goto_6
    :try_start_6
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v2

    invoke-virtual {v2}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v2

    const-string v4, "Error writing entry to local database"

    invoke-virtual {v2, v4, v0}, Lcom/google/android/gms/measurement/internal/zzeh;->zza(Ljava/lang/String;Ljava/lang/Object;)V

    .line 62
    iput-boolean v8, v1, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_3

    if-eqz v7, :cond_a

    .line 64
    invoke-interface {v7}, Landroid/database/Cursor;->close()V

    :cond_a
    if-eqz v9, :cond_b

    .line 66
    invoke-virtual {v9}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    :cond_b
    :goto_7
    add-int/lit8 v5, v5, 0x1

    const/4 v2, 0x0

    const/4 v4, 0x5

    goto/16 :goto_0

    :catchall_3
    move-exception v0

    move-object v12, v7

    :goto_8
    if-eqz v12, :cond_c

    .line 84
    invoke-interface {v12}, Landroid/database/Cursor;->close()V

    :cond_c
    if-eqz v9, :cond_d

    .line 86
    invoke-virtual {v9}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    :cond_d
    throw v0

    .line 88
    :cond_e
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/measurement/internal/zzef;->zzgn()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v0

    const-string v2, "Failed to write entry to local database"

    invoke-virtual {v0, v2}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V

    const/4 v2, 0x0

    return v2
.end method

.method private final zzcg()Z
    .locals 2
    .annotation build Lcom/google/android/gms/common/util/VisibleForTesting;
    .end annotation

    .line 315
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->getContext()Landroid/content/Context;

    move-result-object v0

    const-string v1, "google_app_measurement_local.db"

    .line 317
    invoke-virtual {v0, v1}, Landroid/content/Context;->getDatabasePath(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    invoke-virtual {v0}, Ljava/io/File;->exists()Z

    move-result v0

    return v0
.end method


# virtual methods
.method public final bridge synthetic getContext()Landroid/content/Context;
    .locals 1

    .line 331
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->getContext()Landroid/content/Context;

    move-result-object v0

    return-object v0
.end method

.method public final resetAnalyticsData()V
    .locals 3
    .annotation build Landroidx/annotation/WorkerThread;
    .end annotation

    .line 7
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzm()V

    .line 8
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzo()V

    .line 9
    :try_start_0
    invoke-direct {p0}, Lcom/google/android/gms/measurement/internal/zzeb;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    const-string v1, "messages"

    const/4 v2, 0x0

    .line 10
    invoke-virtual {v0, v1, v2, v2}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    move-result v0

    add-int/lit8 v0, v0, 0x0

    if-lez v0, :cond_0

    .line 12
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v1

    invoke-virtual {v1}, Lcom/google/android/gms/measurement/internal/zzef;->zzgs()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v1

    const-string v2, "Reset local analytics data. records"

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-virtual {v1, v2, v0}, Lcom/google/android/gms/measurement/internal/zzeh;->zza(Ljava/lang/String;Ljava/lang/Object;)V
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_0

    :cond_0
    return-void

    :catch_0
    move-exception v0

    .line 15
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v1

    invoke-virtual {v1}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v1

    const-string v2, "Error resetting local analytics data. error"

    invoke-virtual {v1, v2, v0}, Lcom/google/android/gms/measurement/internal/zzeh;->zza(Ljava/lang/String;Ljava/lang/Object;)V

    return-void
.end method

.method public final zza(Lcom/google/android/gms/measurement/internal/zzai;)Z
    .locals 3

    .line 90
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    const/4 v1, 0x0

    .line 91
    invoke-virtual {p1, v0, v1}, Lcom/google/android/gms/measurement/internal/zzai;->writeToParcel(Landroid/os/Parcel;I)V

    .line 92
    invoke-virtual {v0}, Landroid/os/Parcel;->marshall()[B

    move-result-object p1

    .line 93
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 94
    array-length v0, p1

    const/high16 v2, 0x20000

    if-le v0, v2, :cond_0

    .line 95
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object p1

    .line 96
    invoke-virtual {p1}, Lcom/google/android/gms/measurement/internal/zzef;->zzgn()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object p1

    const-string v0, "Event is too long for local database. Sending event directly to service"

    .line 97
    invoke-virtual {p1, v0}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V

    return v1

    .line 99
    :cond_0
    invoke-direct {p0, v1, p1}, Lcom/google/android/gms/measurement/internal/zzeb;->zza(I[B)Z

    move-result p1

    return p1
.end method

.method public final zza(Lcom/google/android/gms/measurement/internal/zzjn;)Z
    .locals 3

    .line 100
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    const/4 v1, 0x0

    .line 101
    invoke-virtual {p1, v0, v1}, Lcom/google/android/gms/measurement/internal/zzjn;->writeToParcel(Landroid/os/Parcel;I)V

    .line 102
    invoke-virtual {v0}, Landroid/os/Parcel;->marshall()[B

    move-result-object p1

    .line 103
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 104
    array-length v0, p1

    const/high16 v2, 0x20000

    if-le v0, v2, :cond_0

    .line 105
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object p1

    .line 106
    invoke-virtual {p1}, Lcom/google/android/gms/measurement/internal/zzef;->zzgn()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object p1

    const-string v0, "User property too long for local database. Sending directly to service"

    .line 107
    invoke-virtual {p1, v0}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V

    return v1

    :cond_0
    const/4 v0, 0x1

    .line 109
    invoke-direct {p0, v0, p1}, Lcom/google/android/gms/measurement/internal/zzeb;->zza(I[B)Z

    move-result p1

    return p1
.end method

.method public final bridge synthetic zzaa()Lcom/google/android/gms/measurement/internal/zzfc;
    .locals 1

    .line 334
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzaa()Lcom/google/android/gms/measurement/internal/zzfc;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzab()Lcom/google/android/gms/measurement/internal/zzef;
    .locals 1

    .line 335
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzac()Lcom/google/android/gms/measurement/internal/zzeo;
    .locals 1

    .line 336
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzac()Lcom/google/android/gms/measurement/internal/zzeo;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzad()Lcom/google/android/gms/measurement/internal/zzs;
    .locals 1

    .line 337
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzad()Lcom/google/android/gms/measurement/internal/zzs;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzae()Lcom/google/android/gms/measurement/internal/zzr;
    .locals 1

    .line 338
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzae()Lcom/google/android/gms/measurement/internal/zzr;

    move-result-object v0

    return-object v0
.end method

.method protected final zzbk()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public final zzc(I)Ljava/util/List;
    .locals 21
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)",
            "Ljava/util/List<",
            "Lcom/google/android/gms/common/internal/safeparcel/AbstractSafeParcelable;",
            ">;"
        }
    .end annotation

    move-object/from16 v1, p0

    .line 117
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzo()V

    .line 118
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzm()V

    .line 119
    iget-boolean v0, v1, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z

    const/4 v2, 0x0

    if-eqz v0, :cond_0

    return-object v2

    .line 121
    :cond_0
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    .line 122
    invoke-direct/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzeb;->zzcg()Z

    move-result v0

    if-nez v0, :cond_1

    return-object v3

    :cond_1
    const/4 v4, 0x5

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x5

    :goto_0
    if-ge v6, v4, :cond_16

    const/4 v8, 0x1

    .line 128
    :try_start_0
    invoke-direct/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzeb;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v15
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_0 .. :try_end_0} :catch_19
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_0 .. :try_end_0} :catch_17
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_16
    .catchall {:try_start_0 .. :try_end_0} :catchall_9

    if-nez v15, :cond_3

    .line 130
    :try_start_1
    iput-boolean v8, v1, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz v15, :cond_2

    .line 132
    invoke-virtual {v15}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    :cond_2
    return-object v2

    :catchall_0
    move-exception v0

    move-object v9, v2

    move-object/from16 v20, v15

    goto/16 :goto_14

    :catch_0
    move-exception v0

    move-object v9, v2

    move-object/from16 v20, v15

    goto/16 :goto_f

    :catch_1
    move-object v12, v15

    goto/16 :goto_d

    :catch_2
    move-exception v0

    move-object v9, v2

    move-object/from16 v20, v15

    goto/16 :goto_12

    .line 134
    :cond_3
    :try_start_2
    invoke-virtual {v15}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 135
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzad()Lcom/google/android/gms/measurement/internal/zzs;

    move-result-object v0

    sget-object v9, Lcom/google/android/gms/measurement/internal/zzak;->zzjd:Lcom/google/android/gms/measurement/internal/zzdu;

    invoke-virtual {v0, v9}, Lcom/google/android/gms/measurement/internal/zzs;->zza(Lcom/google/android/gms/measurement/internal/zzdu;)Z

    move-result v0
    :try_end_2
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_2 .. :try_end_2} :catch_15
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_2 .. :try_end_2} :catch_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_2 .. :try_end_2} :catch_14
    .catchall {:try_start_2 .. :try_end_2} :catchall_8

    const/16 v9, 0x64

    const-wide/16 v18, -0x1

    if-eqz v0, :cond_5

    .line 136
    :try_start_3
    invoke-static {v15}, Lcom/google/android/gms/measurement/internal/zzeb;->zza(Landroid/database/sqlite/SQLiteDatabase;)J

    move-result-wide v10
    :try_end_3
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_3 .. :try_end_3} :catch_6
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_3 .. :try_end_3} :catch_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_3 .. :try_end_3} :catch_5
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    cmp-long v0, v10, v18

    if-eqz v0, :cond_4

    :try_start_4
    const-string v0, "rowid<?"

    .line 141
    new-array v12, v8, [Ljava/lang/String;

    invoke-static {v10, v11}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v10

    aput-object v10, v12, v5
    :try_end_4
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_4 .. :try_end_4} :catch_2
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_4 .. :try_end_4} :catch_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_4 .. :try_end_4} :catch_0
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    move-object v13, v12

    move-object v12, v0

    goto :goto_1

    :cond_4
    move-object v12, v2

    move-object v13, v12

    :goto_1
    :try_start_5
    const-string v10, "messages"

    const-string v0, "rowid"

    const-string v11, "type"

    const-string v14, "entry"

    .line 142
    filled-new-array {v0, v11, v14}, [Ljava/lang/String;

    move-result-object v11

    const/4 v14, 0x0

    const/4 v0, 0x0

    const-string v16, "rowid asc"

    .line 143
    invoke-static {v9}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v17
    :try_end_5
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_5 .. :try_end_5} :catch_6
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_5 .. :try_end_5} :catch_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_5 .. :try_end_5} :catch_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    move-object v9, v15

    move-object/from16 v20, v15

    move-object v15, v0

    .line 144
    :try_start_6
    invoke-virtual/range {v9 .. v17}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v0
    :try_end_6
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_6 .. :try_end_6} :catch_4
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_6 .. :try_end_6} :catch_12
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_6 .. :try_end_6} :catch_3
    .catchall {:try_start_6 .. :try_end_6} :catchall_1

    goto :goto_5

    :catchall_1
    move-exception v0

    goto :goto_2

    :catch_3
    move-exception v0

    goto :goto_3

    :catch_4
    move-exception v0

    goto :goto_4

    :catchall_2
    move-exception v0

    move-object/from16 v20, v15

    :goto_2
    move-object v9, v2

    goto/16 :goto_14

    :catch_5
    move-exception v0

    move-object/from16 v20, v15

    :goto_3
    move-object v9, v2

    goto/16 :goto_f

    :catch_6
    move-exception v0

    move-object/from16 v20, v15

    :goto_4
    move-object v9, v2

    goto/16 :goto_12

    :cond_5
    move-object/from16 v20, v15

    :try_start_7
    const-string v10, "messages"

    const-string v0, "rowid"

    const-string v11, "type"

    const-string v12, "entry"

    .line 146
    filled-new-array {v0, v11, v12}, [Ljava/lang/String;

    move-result-object v11

    const/4 v12, 0x0

    const/4 v13, 0x0

    const/4 v14, 0x0

    const/4 v15, 0x0

    const-string v16, "rowid asc"

    .line 147
    invoke-static {v9}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v17

    move-object/from16 v9, v20

    .line 148
    invoke-virtual/range {v9 .. v17}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v0
    :try_end_7
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_7 .. :try_end_7} :catch_13
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_7 .. :try_end_7} :catch_12
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_7 .. :try_end_7} :catch_11
    .catchall {:try_start_7 .. :try_end_7} :catchall_7

    :goto_5
    move-object v9, v0

    .line 150
    :cond_6
    :goto_6
    :try_start_8
    invoke-interface {v9}, Landroid/database/Cursor;->moveToNext()Z

    move-result v0
    :try_end_8
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_8 .. :try_end_8} :catch_10
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_8 .. :try_end_8} :catch_f
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_8 .. :try_end_8} :catch_e
    .catchall {:try_start_8 .. :try_end_8} :catchall_6

    if-eqz v0, :cond_b

    .line 151
    :try_start_9
    invoke-interface {v9, v5}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v18

    .line 152
    invoke-interface {v9, v8}, Landroid/database/Cursor;->getInt(I)I

    move-result v0

    const/4 v10, 0x2

    .line 153
    invoke-interface {v9, v10}, Landroid/database/Cursor;->getBlob(I)[B

    move-result-object v11

    if-nez v0, :cond_7

    .line 155
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v10
    :try_end_9
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_9 .. :try_end_9} :catch_b
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_9 .. :try_end_9} :catch_f
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_9 .. :try_end_9} :catch_a
    .catchall {:try_start_9 .. :try_end_9} :catchall_b

    .line 156
    :try_start_a
    array-length v0, v11

    invoke-virtual {v10, v11, v5, v0}, Landroid/os/Parcel;->unmarshall([BII)V

    .line 157
    invoke-virtual {v10, v5}, Landroid/os/Parcel;->setDataPosition(I)V

    .line 158
    sget-object v0, Lcom/google/android/gms/measurement/internal/zzai;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v0, v10}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/google/android/gms/measurement/internal/zzai;
    :try_end_a
    .catch Lcom/google/android/gms/common/internal/safeparcel/SafeParcelReader$ParseException; {:try_start_a .. :try_end_a} :catch_7
    .catchall {:try_start_a .. :try_end_a} :catchall_3

    .line 159
    :try_start_b
    invoke-virtual {v10}, Landroid/os/Parcel;->recycle()V

    if-eqz v0, :cond_6

    .line 165
    invoke-interface {v3, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_b
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_b .. :try_end_b} :catch_b
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_b .. :try_end_b} :catch_f
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_b .. :try_end_b} :catch_a
    .catchall {:try_start_b .. :try_end_b} :catchall_b

    goto :goto_6

    :catchall_3
    move-exception v0

    goto :goto_7

    .line 162
    :catch_7
    :try_start_c
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v0

    const-string v11, "Failed to load event from local database"

    invoke-virtual {v0, v11}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V
    :try_end_c
    .catchall {:try_start_c .. :try_end_c} :catchall_3

    .line 163
    :try_start_d
    invoke-virtual {v10}, Landroid/os/Parcel;->recycle()V

    goto :goto_6

    :goto_7
    invoke-virtual {v10}, Landroid/os/Parcel;->recycle()V

    throw v0

    :cond_7
    if-ne v0, v8, :cond_8

    .line 167
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v10
    :try_end_d
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_d .. :try_end_d} :catch_b
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_d .. :try_end_d} :catch_f
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_d .. :try_end_d} :catch_a
    .catchall {:try_start_d .. :try_end_d} :catchall_b

    .line 169
    :try_start_e
    array-length v0, v11

    invoke-virtual {v10, v11, v5, v0}, Landroid/os/Parcel;->unmarshall([BII)V

    .line 170
    invoke-virtual {v10, v5}, Landroid/os/Parcel;->setDataPosition(I)V

    .line 171
    sget-object v0, Lcom/google/android/gms/measurement/internal/zzjn;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v0, v10}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/google/android/gms/measurement/internal/zzjn;
    :try_end_e
    .catch Lcom/google/android/gms/common/internal/safeparcel/SafeParcelReader$ParseException; {:try_start_e .. :try_end_e} :catch_8
    .catchall {:try_start_e .. :try_end_e} :catchall_4

    .line 172
    :try_start_f
    invoke-virtual {v10}, Landroid/os/Parcel;->recycle()V
    :try_end_f
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_f .. :try_end_f} :catch_b
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_f .. :try_end_f} :catch_f
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_f .. :try_end_f} :catch_a
    .catchall {:try_start_f .. :try_end_f} :catchall_b

    goto :goto_8

    :catchall_4
    move-exception v0

    goto :goto_9

    .line 175
    :catch_8
    :try_start_10
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v0

    const-string v11, "Failed to load user property from local database"

    invoke-virtual {v0, v11}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V
    :try_end_10
    .catchall {:try_start_10 .. :try_end_10} :catchall_4

    .line 176
    :try_start_11
    invoke-virtual {v10}, Landroid/os/Parcel;->recycle()V

    move-object v0, v2

    :goto_8
    if-eqz v0, :cond_6

    .line 180
    invoke-interface {v3, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_6

    .line 178
    :goto_9
    invoke-virtual {v10}, Landroid/os/Parcel;->recycle()V

    throw v0

    :cond_8
    if-ne v0, v10, :cond_9

    .line 182
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v10
    :try_end_11
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_11 .. :try_end_11} :catch_b
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_11 .. :try_end_11} :catch_f
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_11 .. :try_end_11} :catch_a
    .catchall {:try_start_11 .. :try_end_11} :catchall_b

    .line 184
    :try_start_12
    array-length v0, v11

    invoke-virtual {v10, v11, v5, v0}, Landroid/os/Parcel;->unmarshall([BII)V

    .line 185
    invoke-virtual {v10, v5}, Landroid/os/Parcel;->setDataPosition(I)V

    .line 186
    sget-object v0, Lcom/google/android/gms/measurement/internal/zzq;->CREATOR:Landroid/os/Parcelable$Creator;

    .line 187
    invoke-interface {v0, v10}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/google/android/gms/measurement/internal/zzq;
    :try_end_12
    .catch Lcom/google/android/gms/common/internal/safeparcel/SafeParcelReader$ParseException; {:try_start_12 .. :try_end_12} :catch_9
    .catchall {:try_start_12 .. :try_end_12} :catchall_5

    .line 188
    :try_start_13
    invoke-virtual {v10}, Landroid/os/Parcel;->recycle()V
    :try_end_13
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_13 .. :try_end_13} :catch_b
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_13 .. :try_end_13} :catch_f
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_13 .. :try_end_13} :catch_a
    .catchall {:try_start_13 .. :try_end_13} :catchall_b

    goto :goto_a

    :catchall_5
    move-exception v0

    goto :goto_b

    .line 191
    :catch_9
    :try_start_14
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v0

    const-string v11, "Failed to load user property from local database"

    invoke-virtual {v0, v11}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V
    :try_end_14
    .catchall {:try_start_14 .. :try_end_14} :catchall_5

    .line 192
    :try_start_15
    invoke-virtual {v10}, Landroid/os/Parcel;->recycle()V

    move-object v0, v2

    :goto_a
    if-eqz v0, :cond_6

    .line 196
    invoke-interface {v3, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto/16 :goto_6

    .line 194
    :goto_b
    invoke-virtual {v10}, Landroid/os/Parcel;->recycle()V

    throw v0

    :cond_9
    const/4 v10, 0x3

    if-ne v0, v10, :cond_a

    .line 198
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/measurement/internal/zzef;->zzgn()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v0

    const-string v10, "Skipping app launch break"

    invoke-virtual {v0, v10}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V

    goto/16 :goto_6

    .line 199
    :cond_a
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v0

    const-string v10, "Unknown record type in local database"

    invoke-virtual {v0, v10}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V
    :try_end_15
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_15 .. :try_end_15} :catch_b
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_15 .. :try_end_15} :catch_f
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_15 .. :try_end_15} :catch_a
    .catchall {:try_start_15 .. :try_end_15} :catchall_b

    goto/16 :goto_6

    :catch_a
    move-exception v0

    goto/16 :goto_f

    :catch_b
    move-exception v0

    goto/16 :goto_12

    :cond_b
    :try_start_16
    const-string v0, "messages"

    const-string v10, "rowid <= ?"

    .line 201
    new-array v11, v8, [Ljava/lang/String;

    .line 202
    invoke-static/range {v18 .. v19}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v12

    aput-object v12, v11, v5
    :try_end_16
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_16 .. :try_end_16} :catch_10
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_16 .. :try_end_16} :catch_f
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_16 .. :try_end_16} :catch_e
    .catchall {:try_start_16 .. :try_end_16} :catchall_6

    move-object/from16 v12, v20

    .line 203
    :try_start_17
    invoke-virtual {v12, v0, v10, v11}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    move-result v0

    .line 204
    invoke-interface {v3}, Ljava/util/List;->size()I

    move-result v10

    if-ge v0, v10, :cond_c

    .line 205
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v0

    const-string v10, "Fewer entries removed from local database than expected"

    invoke-virtual {v0, v10}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V

    .line 206
    :cond_c
    invoke-virtual {v12}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V

    .line 207
    invoke-virtual {v12}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_17
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_17 .. :try_end_17} :catch_d
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_17 .. :try_end_17} :catch_18
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_17 .. :try_end_17} :catch_c
    .catchall {:try_start_17 .. :try_end_17} :catchall_a

    if-eqz v9, :cond_d

    .line 210
    invoke-interface {v9}, Landroid/database/Cursor;->close()V

    :cond_d
    if-eqz v12, :cond_e

    .line 212
    invoke-virtual {v12}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    :cond_e
    return-object v3

    :catch_c
    move-exception v0

    goto :goto_c

    :catch_d
    move-exception v0

    goto :goto_e

    :catchall_6
    move-exception v0

    move-object/from16 v12, v20

    goto/16 :goto_14

    :catch_e
    move-exception v0

    move-object/from16 v12, v20

    goto :goto_f

    :catch_f
    move-object/from16 v12, v20

    goto/16 :goto_10

    :catch_10
    move-exception v0

    move-object/from16 v12, v20

    goto/16 :goto_12

    :catchall_7
    move-exception v0

    move-object/from16 v12, v20

    goto/16 :goto_2

    :catch_11
    move-exception v0

    move-object/from16 v12, v20

    goto/16 :goto_3

    :catch_12
    move-object/from16 v12, v20

    goto :goto_d

    :catch_13
    move-exception v0

    move-object/from16 v12, v20

    goto/16 :goto_4

    :catchall_8
    move-exception v0

    move-object v12, v15

    move-object v9, v2

    goto :goto_11

    :catch_14
    move-exception v0

    move-object v12, v15

    move-object v9, v2

    :goto_c
    move-object/from16 v20, v12

    goto :goto_f

    :goto_d
    move-object v9, v2

    goto :goto_10

    :catch_15
    move-exception v0

    move-object v12, v15

    move-object v9, v2

    :goto_e
    move-object/from16 v20, v12

    goto :goto_12

    :catchall_9
    move-exception v0

    move-object v9, v2

    move-object/from16 v20, v9

    goto :goto_14

    :catch_16
    move-exception v0

    move-object v9, v2

    move-object/from16 v20, v9

    :goto_f
    if-eqz v20, :cond_f

    .line 229
    :try_start_18
    invoke-virtual/range {v20 .. v20}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v10

    if-eqz v10, :cond_f

    .line 230
    invoke-virtual/range {v20 .. v20}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V

    .line 231
    :cond_f
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v10

    invoke-virtual {v10}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v10

    const-string v11, "Error reading entries from local database"

    invoke-virtual {v10, v11, v0}, Lcom/google/android/gms/measurement/internal/zzeh;->zza(Ljava/lang/String;Ljava/lang/Object;)V

    .line 232
    iput-boolean v8, v1, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z
    :try_end_18
    .catchall {:try_start_18 .. :try_end_18} :catchall_b

    if-eqz v9, :cond_10

    .line 234
    invoke-interface {v9}, Landroid/database/Cursor;->close()V

    :cond_10
    if-eqz v20, :cond_13

    .line 236
    invoke-virtual/range {v20 .. v20}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    goto :goto_13

    :catch_17
    move-object v9, v2

    move-object v12, v9

    :catch_18
    :goto_10
    int-to-long v10, v7

    .line 222
    :try_start_19
    invoke-static {v10, v11}, Landroid/os/SystemClock;->sleep(J)V
    :try_end_19
    .catchall {:try_start_19 .. :try_end_19} :catchall_a

    add-int/lit8 v7, v7, 0x14

    if-eqz v9, :cond_11

    .line 225
    invoke-interface {v9}, Landroid/database/Cursor;->close()V

    :cond_11
    if-eqz v12, :cond_13

    .line 227
    invoke-virtual {v12}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    goto :goto_13

    :catchall_a
    move-exception v0

    :goto_11
    move-object/from16 v20, v12

    goto :goto_14

    :catch_19
    move-exception v0

    move-object v9, v2

    move-object/from16 v20, v9

    .line 215
    :goto_12
    :try_start_1a
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v10

    invoke-virtual {v10}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v10

    const-string v11, "Error reading entries from local database"

    invoke-virtual {v10, v11, v0}, Lcom/google/android/gms/measurement/internal/zzeh;->zza(Ljava/lang/String;Ljava/lang/Object;)V

    .line 216
    iput-boolean v8, v1, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z
    :try_end_1a
    .catchall {:try_start_1a .. :try_end_1a} :catchall_b

    if-eqz v9, :cond_12

    .line 218
    invoke-interface {v9}, Landroid/database/Cursor;->close()V

    :cond_12
    if-eqz v20, :cond_13

    .line 220
    invoke-virtual/range {v20 .. v20}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    :cond_13
    :goto_13
    add-int/lit8 v6, v6, 0x1

    goto/16 :goto_0

    :catchall_b
    move-exception v0

    :goto_14
    if-eqz v9, :cond_14

    .line 238
    invoke-interface {v9}, Landroid/database/Cursor;->close()V

    :cond_14
    if-eqz v20, :cond_15

    .line 240
    invoke-virtual/range {v20 .. v20}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    :cond_15
    throw v0

    .line 242
    :cond_16
    invoke-virtual/range {p0 .. p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/measurement/internal/zzef;->zzgn()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v0

    const-string v3, "Failed to read events from database in reasonable time"

    invoke-virtual {v0, v3}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V

    return-object v2
.end method

.method public final zzc(Lcom/google/android/gms/measurement/internal/zzq;)Z
    .locals 2

    .line 110
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzz()Lcom/google/android/gms/measurement/internal/zzjs;

    invoke-static {p1}, Lcom/google/android/gms/measurement/internal/zzjs;->zza(Landroid/os/Parcelable;)[B

    move-result-object p1

    .line 111
    array-length v0, p1

    const/high16 v1, 0x20000

    if-le v0, v1, :cond_0

    .line 112
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object p1

    .line 113
    invoke-virtual {p1}, Lcom/google/android/gms/measurement/internal/zzef;->zzgn()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object p1

    const-string v0, "Conditional user property too long for local database. Sending directly to service"

    .line 114
    invoke-virtual {p1, v0}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V

    const/4 p1, 0x0

    return p1

    :cond_0
    const/4 v0, 0x2

    .line 116
    invoke-direct {p0, v0, p1}, Lcom/google/android/gms/measurement/internal/zzeb;->zza(I[B)Z

    move-result p1

    return p1
.end method

.method public final zzgh()Z
    .locals 2
    .annotation build Landroidx/annotation/WorkerThread;
    .end annotation

    const/4 v0, 0x0

    .line 244
    new-array v0, v0, [B

    const/4 v1, 0x3

    invoke-direct {p0, v1, v0}, Lcom/google/android/gms/measurement/internal/zzeb;->zza(I[B)Z

    move-result v0

    return v0
.end method

.method public final zzgi()Z
    .locals 11
    .annotation build Landroidx/annotation/WorkerThread;
    .end annotation

    .line 245
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzo()V

    .line 246
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzm()V

    .line 247
    iget-boolean v0, p0, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    .line 249
    :cond_0
    invoke-direct {p0}, Lcom/google/android/gms/measurement/internal/zzeb;->zzcg()Z

    move-result v0

    if-nez v0, :cond_1

    return v1

    :cond_1
    const/4 v0, 0x5

    const/4 v2, 0x0

    const/4 v3, 0x5

    :goto_0
    if-ge v2, v0, :cond_8

    const/4 v4, 0x0

    const/4 v5, 0x1

    .line 254
    :try_start_0
    invoke-direct {p0}, Lcom/google/android/gms/measurement/internal/zzeb;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v6
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_0 .. :try_end_0} :catch_5
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_0 .. :try_end_0} :catch_4
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_3
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v6, :cond_3

    .line 256
    :try_start_1
    iput-boolean v5, p0, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    if-eqz v6, :cond_2

    .line 258
    invoke-virtual {v6}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    :cond_2
    return v1

    :catch_0
    move-exception v4

    goto :goto_1

    :catch_1
    move-exception v4

    goto :goto_3

    .line 260
    :cond_3
    :try_start_2
    invoke-virtual {v6}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    const-string v4, "messages"

    const-string v7, "type == ?"

    .line 261
    new-array v8, v5, [Ljava/lang/String;

    const/4 v9, 0x3

    .line 262
    invoke-static {v9}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v9

    aput-object v9, v8, v1

    .line 263
    invoke-virtual {v6, v4, v7, v8}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    .line 264
    invoke-virtual {v6}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V

    .line 265
    invoke-virtual {v6}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_2
    .catch Landroid/database/sqlite/SQLiteFullException; {:try_start_2 .. :try_end_2} :catch_1
    .catch Landroid/database/sqlite/SQLiteDatabaseLockedException; {:try_start_2 .. :try_end_2} :catch_2
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz v6, :cond_4

    .line 267
    invoke-virtual {v6}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    :cond_4
    return v5

    :catch_2
    move-object v4, v6

    goto :goto_2

    :catchall_0
    move-exception v0

    move-object v6, v4

    goto :goto_5

    :catch_3
    move-exception v6

    move-object v10, v6

    move-object v6, v4

    move-object v4, v10

    :goto_1
    if-eqz v6, :cond_5

    .line 280
    :try_start_3
    invoke-virtual {v6}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v7

    if-eqz v7, :cond_5

    .line 281
    invoke-virtual {v6}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V

    .line 282
    :cond_5
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v7

    invoke-virtual {v7}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v7

    const-string v8, "Error deleting app launch break from local database"

    invoke-virtual {v7, v8, v4}, Lcom/google/android/gms/measurement/internal/zzeh;->zza(Ljava/lang/String;Ljava/lang/Object;)V

    .line 283
    iput-boolean v5, p0, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    if-eqz v6, :cond_6

    .line 285
    invoke-virtual {v6}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    goto :goto_4

    :catch_4
    :goto_2
    int-to-long v5, v3

    .line 275
    :try_start_4
    invoke-static {v5, v6}, Landroid/os/SystemClock;->sleep(J)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    add-int/lit8 v3, v3, 0x14

    if-eqz v4, :cond_6

    .line 278
    invoke-virtual {v4}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    goto :goto_4

    :catch_5
    move-exception v6

    move-object v10, v6

    move-object v6, v4

    move-object v4, v10

    .line 270
    :goto_3
    :try_start_5
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v7

    invoke-virtual {v7}, Lcom/google/android/gms/measurement/internal/zzef;->zzgk()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v7

    const-string v8, "Error deleting app launch break from local database"

    invoke-virtual {v7, v8, v4}, Lcom/google/android/gms/measurement/internal/zzeh;->zza(Ljava/lang/String;Ljava/lang/Object;)V

    .line 271
    iput-boolean v5, p0, Lcom/google/android/gms/measurement/internal/zzeb;->zzjw:Z
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_1

    if-eqz v6, :cond_6

    .line 273
    invoke-virtual {v6}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    :cond_6
    :goto_4
    add-int/lit8 v2, v2, 0x1

    goto/16 :goto_0

    :catchall_1
    move-exception v0

    :goto_5
    if-eqz v6, :cond_7

    .line 287
    invoke-virtual {v6}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    :cond_7
    throw v0

    .line 289
    :cond_8
    invoke-virtual {p0}, Lcom/google/android/gms/measurement/internal/zzgf;->zzab()Lcom/google/android/gms/measurement/internal/zzef;

    move-result-object v0

    .line 290
    invoke-virtual {v0}, Lcom/google/android/gms/measurement/internal/zzef;->zzgn()Lcom/google/android/gms/measurement/internal/zzeh;

    move-result-object v0

    const-string v2, "Error deleting app launch break from local database in reasonable time"

    .line 291
    invoke-virtual {v0, v2}, Lcom/google/android/gms/measurement/internal/zzeh;->zzao(Ljava/lang/String;)V

    return v1
.end method

.method public final bridge synthetic zzl()V
    .locals 0

    .line 318
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzl()V

    return-void
.end method

.method public final bridge synthetic zzm()V
    .locals 0

    .line 319
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzm()V

    return-void
.end method

.method public final bridge synthetic zzn()V
    .locals 0

    .line 320
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzn()V

    return-void
.end method

.method public final bridge synthetic zzo()V
    .locals 0

    .line 321
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzo()V

    return-void
.end method

.method public final bridge synthetic zzp()Lcom/google/android/gms/measurement/internal/zza;
    .locals 1

    .line 322
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzp()Lcom/google/android/gms/measurement/internal/zza;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzq()Lcom/google/android/gms/measurement/internal/zzgp;
    .locals 1

    .line 323
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzq()Lcom/google/android/gms/measurement/internal/zzgp;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzr()Lcom/google/android/gms/measurement/internal/zzdy;
    .locals 1

    .line 324
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzr()Lcom/google/android/gms/measurement/internal/zzdy;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzs()Lcom/google/android/gms/measurement/internal/zzhv;
    .locals 1

    .line 325
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzs()Lcom/google/android/gms/measurement/internal/zzhv;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzt()Lcom/google/android/gms/measurement/internal/zzhq;
    .locals 1

    .line 326
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzt()Lcom/google/android/gms/measurement/internal/zzhq;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzu()Lcom/google/android/gms/measurement/internal/zzeb;
    .locals 1

    .line 327
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzu()Lcom/google/android/gms/measurement/internal/zzeb;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzv()Lcom/google/android/gms/measurement/internal/zziw;
    .locals 1

    .line 328
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzv()Lcom/google/android/gms/measurement/internal/zziw;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzw()Lcom/google/android/gms/measurement/internal/zzac;
    .locals 1

    .line 329
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzw()Lcom/google/android/gms/measurement/internal/zzac;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzx()Lcom/google/android/gms/common/util/Clock;
    .locals 1

    .line 330
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzx()Lcom/google/android/gms/common/util/Clock;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzy()Lcom/google/android/gms/measurement/internal/zzed;
    .locals 1

    .line 332
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzy()Lcom/google/android/gms/measurement/internal/zzed;

    move-result-object v0

    return-object v0
.end method

.method public final bridge synthetic zzz()Lcom/google/android/gms/measurement/internal/zzjs;
    .locals 1

    .line 333
    invoke-super {p0}, Lcom/google/android/gms/measurement/internal/zzg;->zzz()Lcom/google/android/gms/measurement/internal/zzjs;

    move-result-object v0

    return-object v0
.end method
