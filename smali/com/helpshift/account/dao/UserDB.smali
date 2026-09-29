.class public Lcom/helpshift/account/dao/UserDB;
.super Ljava/lang/Object;
.source "UserDB.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_UserDB"

.field private static instance:Lcom/helpshift/account/dao/UserDB;


# instance fields
.field private final userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

.field private final userDBInfo:Lcom/helpshift/account/dao/UserDBInfo;


# direct methods
.method private constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 55
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 56
    new-instance v0, Lcom/helpshift/account/dao/UserDBInfo;

    invoke-direct {v0}, Lcom/helpshift/account/dao/UserDBInfo;-><init>()V

    iput-object v0, p0, Lcom/helpshift/account/dao/UserDB;->userDBInfo:Lcom/helpshift/account/dao/UserDBInfo;

    .line 57
    new-instance v0, Lcom/helpshift/account/dao/UserDBHelper;

    iget-object v1, p0, Lcom/helpshift/account/dao/UserDB;->userDBInfo:Lcom/helpshift/account/dao/UserDBInfo;

    invoke-direct {v0, p1, v1}, Lcom/helpshift/account/dao/UserDBHelper;-><init>(Landroid/content/Context;Lcom/helpshift/account/dao/UserDBInfo;)V

    iput-object v0, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    return-void
.end method

.method private clearedUserDMtoContentValues(Lcom/helpshift/account/domainmodel/ClearedUserDM;)Landroid/content/ContentValues;
    .locals 3

    .line 356
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    const-string v1, "_id"

    .line 357
    iget-object v2, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->localId:Ljava/lang/Long;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string v1, "identifier"

    .line 358
    iget-object v2, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->identifier:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "email"

    .line 359
    iget-object v2, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->email:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "auth_token"

    .line 360
    iget-object v2, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->authToken:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "deviceid"

    .line 361
    iget-object v2, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->deviceId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "sync_state"

    .line 362
    iget-object p1, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->syncState:Lcom/helpshift/account/dao/ClearedUserSyncState;

    invoke-virtual {p1}, Lcom/helpshift/account/dao/ClearedUserSyncState;->ordinal()I

    move-result p1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-virtual {v0, v1, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    return-object v0
.end method

.method private cursorToClearedUserDM(Landroid/database/Cursor;)Lcom/helpshift/account/domainmodel/ClearedUserDM;
    .locals 9

    const-string v0, "_id"

    .line 323
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    const-string v0, "identifier"

    .line 324
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v4

    const-string v0, "email"

    .line 325
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v5

    const-string v0, "deviceid"

    .line 326
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v7

    const-string v0, "auth_token"

    .line 327
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v6

    const-string v0, "sync_state"

    .line 329
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result p1

    invoke-direct {p0, p1}, Lcom/helpshift/account/dao/UserDB;->intToClearedUserSyncState(I)Lcom/helpshift/account/dao/ClearedUserSyncState;

    move-result-object v8

    .line 331
    new-instance p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;

    move-object v2, p1

    invoke-direct/range {v2 .. v8}, Lcom/helpshift/account/domainmodel/ClearedUserDM;-><init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/account/dao/ClearedUserSyncState;)V

    return-object p1
.end method

.method private cursorToLegacyProfile(Landroid/database/Cursor;)Lcom/helpshift/migration/legacyUser/LegacyProfile;
    .locals 7

    const-string v0, "identifier"

    .line 341
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v2

    const-string v0, "email"

    .line 342
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v3

    const-string v0, "name"

    .line 343
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v4

    const-string v0, "serverid"

    .line 344
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v5

    const-string v0, "migration_state"

    .line 346
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result p1

    invoke-direct {p0, p1}, Lcom/helpshift/account/dao/UserDB;->intToMigrationState(I)Lcom/helpshift/migration/MigrationState;

    move-result-object v6

    .line 348
    new-instance p1, Lcom/helpshift/migration/legacyUser/LegacyProfile;

    move-object v1, p1

    invoke-direct/range {v1 .. v6}, Lcom/helpshift/migration/legacyUser/LegacyProfile;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/migration/MigrationState;)V

    return-object p1
.end method

.method private cursorToRedactionDetail(Landroid/database/Cursor;)Lcom/helpshift/redaction/RedactionDetail;
    .locals 4

    const-string v0, "user_local_id"

    .line 757
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v0

    const-string v2, "redaction_state"

    .line 759
    invoke-interface {p1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {p1, v2}, Landroid/database/Cursor;->getInt(I)I

    move-result v2

    invoke-direct {p0, v2}, Lcom/helpshift/account/dao/UserDB;->intToRedactionState(I)Lcom/helpshift/redaction/RedactionState;

    move-result-object v2

    const-string v3, "redaction_type"

    .line 761
    invoke-interface {p1, v3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v3

    invoke-interface {p1, v3}, Landroid/database/Cursor;->getInt(I)I

    move-result p1

    invoke-direct {p0, p1}, Lcom/helpshift/account/dao/UserDB;->intToRedactionType(I)Lcom/helpshift/redaction/RedactionType;

    move-result-object p1

    .line 762
    new-instance v3, Lcom/helpshift/redaction/RedactionDetail;

    invoke-direct {v3, v0, v1, v2, p1}, Lcom/helpshift/redaction/RedactionDetail;-><init>(JLcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionType;)V

    return-object v3
.end method

.method private cursorToUserDM(Landroid/database/Cursor;)Lcom/helpshift/account/domainmodel/UserDM;
    .locals 14

    const-string v0, "_id"

    .line 292
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    const-string v0, "identifier"

    .line 293
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v4

    const-string v0, "name"

    .line 294
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v6

    const-string v0, "email"

    .line 295
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v5

    const-string v0, "deviceid"

    .line 296
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v7

    const-string v0, "auth_token"

    .line 297
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v11

    const-string v0, "active"

    .line 298
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result v0

    .line 299
    sget-object v1, Lcom/helpshift/account/dao/UserDBInfo;->INT_TRUE:Ljava/lang/Integer;

    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v1

    const/4 v2, 0x0

    const/4 v8, 0x1

    if-ne v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    const-string v1, "anonymous"

    .line 300
    invoke-interface {p1, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getInt(I)I

    move-result v1

    .line 301
    sget-object v9, Lcom/helpshift/account/dao/UserDBInfo;->INT_TRUE:Ljava/lang/Integer;

    invoke-virtual {v9}, Ljava/lang/Integer;->intValue()I

    move-result v9

    if-ne v1, v9, :cond_1

    const/4 v9, 0x1

    goto :goto_1

    :cond_1
    const/4 v9, 0x0

    :goto_1
    const-string v1, "issue_exists"

    .line 302
    invoke-interface {p1, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getInt(I)I

    move-result v1

    .line 303
    sget-object v10, Lcom/helpshift/account/dao/UserDBInfo;->INT_TRUE:Ljava/lang/Integer;

    invoke-virtual {v10}, Ljava/lang/Integer;->intValue()I

    move-result v10

    if-ne v1, v10, :cond_2

    const/4 v12, 0x1

    goto :goto_2

    :cond_2
    const/4 v12, 0x0

    :goto_2
    const-string v1, "push_token_synced"

    .line 304
    invoke-interface {p1, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getInt(I)I

    move-result v1

    .line 305
    sget-object v10, Lcom/helpshift/account/dao/UserDBInfo;->INT_TRUE:Ljava/lang/Integer;

    invoke-virtual {v10}, Ljava/lang/Integer;->intValue()I

    move-result v10

    if-ne v1, v10, :cond_3

    const/4 v10, 0x1

    goto :goto_3

    :cond_3
    const/4 v10, 0x0

    :goto_3
    const-string v1, "initial_state_synced"

    .line 307
    invoke-interface {p1, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getInt(I)I

    move-result p1

    invoke-direct {p0, p1}, Lcom/helpshift/account/dao/UserDB;->intToUserSyncStatus(I)Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-result-object v13

    .line 309
    new-instance p1, Lcom/helpshift/account/domainmodel/UserDM;

    move-object v2, p1

    move v8, v0

    invoke-direct/range {v2 .. v13}, Lcom/helpshift/account/domainmodel/UserDM;-><init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZZLjava/lang/String;ZLcom/helpshift/account/domainmodel/UserSyncStatus;)V

    return-object p1
.end method

.method private getClearUserDMWithLocalId(Lcom/helpshift/account/domainmodel/ClearedUserDM;J)Lcom/helpshift/account/domainmodel/ClearedUserDM;
    .locals 8

    .line 440
    new-instance v7, Lcom/helpshift/account/domainmodel/ClearedUserDM;

    invoke-static {p2, p3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    iget-object v2, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->identifier:Ljava/lang/String;

    iget-object v3, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->email:Ljava/lang/String;

    iget-object v4, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->authToken:Ljava/lang/String;

    iget-object v5, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->deviceId:Ljava/lang/String;

    iget-object v6, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->syncState:Lcom/helpshift/account/dao/ClearedUserSyncState;

    move-object v0, v7

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/account/domainmodel/ClearedUserDM;-><init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/account/dao/ClearedUserSyncState;)V

    return-object v7
.end method

.method public static declared-synchronized getInstance(Landroid/content/Context;)Lcom/helpshift/account/dao/UserDB;
    .locals 2

    const-class v0, Lcom/helpshift/account/dao/UserDB;

    monitor-enter v0

    .line 61
    :try_start_0
    sget-object v1, Lcom/helpshift/account/dao/UserDB;->instance:Lcom/helpshift/account/dao/UserDB;

    if-nez v1, :cond_0

    .line 62
    new-instance v1, Lcom/helpshift/account/dao/UserDB;

    invoke-direct {v1, p0}, Lcom/helpshift/account/dao/UserDB;-><init>(Landroid/content/Context;)V

    sput-object v1, Lcom/helpshift/account/dao/UserDB;->instance:Lcom/helpshift/account/dao/UserDB;

    .line 64
    :cond_0
    sget-object p0, Lcom/helpshift/account/dao/UserDB;->instance:Lcom/helpshift/account/dao/UserDB;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object p0

    :catchall_0
    move-exception p0

    .line 60
    monitor-exit v0

    throw p0
.end method

.method private declared-synchronized getUser(Ljava/lang/String;[Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM;
    .locals 10

    monitor-enter p0

    const/4 v0, 0x0

    .line 117
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v1}, Lcom/helpshift/account/dao/UserDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    const-string v3, "user_table"

    const/4 v4, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    move-object v5, p1

    move-object v6, p2

    .line 118
    invoke-virtual/range {v2 .. v9}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 126
    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result p2

    if-eqz p2, :cond_0

    .line 127
    invoke-direct {p0, p1}, Lcom/helpshift/account/dao/UserDB;->cursorToUserDM(Landroid/database/Cursor;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p2
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    move-object v0, p2

    :cond_0
    if-eqz p1, :cond_1

    .line 135
    :goto_0
    :try_start_2
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    goto :goto_2

    :catch_0
    move-exception p2

    goto :goto_1

    :catchall_0
    move-exception p2

    move-object p1, v0

    goto :goto_3

    :catch_1
    move-exception p2

    move-object p1, v0

    :goto_1
    :try_start_3
    const-string v1, "Helpshift_UserDB"

    const-string v2, "Error in reading user"

    .line 131
    invoke-static {v1, v2, p2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    if-eqz p1, :cond_1

    goto :goto_0

    .line 139
    :cond_1
    :goto_2
    monitor-exit p0

    return-object v0

    :catchall_1
    move-exception p2

    :goto_3
    if-eqz p1, :cond_2

    .line 135
    :try_start_4
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    goto :goto_4

    :catchall_2
    move-exception p1

    goto :goto_5

    .line 137
    :cond_2
    :goto_4
    throw p2
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    .line 113
    :goto_5
    monitor-exit p0

    throw p1
.end method

.method private getUserDMWithLocalId(Lcom/helpshift/account/domainmodel/UserDM;J)Lcom/helpshift/account/domainmodel/UserDM;
    .locals 13

    .line 433
    new-instance v12, Lcom/helpshift/account/domainmodel/UserDM;

    invoke-static/range {p2 .. p3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getName()Ljava/lang/String;

    move-result-object v4

    .line 434
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getDeviceId()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->isActiveUser()Z

    move-result v6

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->isAnonymousUser()Z

    move-result v7

    .line 435
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->isPushTokenSynced()Z

    move-result v8

    .line 436
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getAuthToken()Ljava/lang/String;

    move-result-object v9

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->issueExists()Z

    move-result v10

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getSyncState()Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-result-object v11

    move-object v0, v12

    invoke-direct/range {v0 .. v11}, Lcom/helpshift/account/domainmodel/UserDM;-><init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZZLjava/lang/String;ZLcom/helpshift/account/domainmodel/UserSyncStatus;)V

    return-object v12
.end method

.method private intToClearedUserSyncState(I)Lcom/helpshift/account/dao/ClearedUserSyncState;
    .locals 1

    if-ltz p1, :cond_0

    const/4 v0, 0x3

    if-le p1, v0, :cond_1

    :cond_0
    const/4 p1, 0x0

    .line 716
    :cond_1
    invoke-static {}, Lcom/helpshift/account/dao/ClearedUserSyncState;->values()[Lcom/helpshift/account/dao/ClearedUserSyncState;

    move-result-object v0

    aget-object p1, v0, p1

    return-object p1
.end method

.method private intToMigrationState(I)Lcom/helpshift/migration/MigrationState;
    .locals 1

    if-ltz p1, :cond_0

    const/4 v0, 0x3

    if-le p1, v0, :cond_1

    :cond_0
    const/4 p1, 0x0

    .line 709
    :cond_1
    invoke-static {}, Lcom/helpshift/migration/MigrationState;->values()[Lcom/helpshift/migration/MigrationState;

    move-result-object v0

    aget-object p1, v0, p1

    return-object p1
.end method

.method private intToRedactionState(I)Lcom/helpshift/redaction/RedactionState;
    .locals 2

    .line 723
    invoke-static {}, Lcom/helpshift/redaction/RedactionState;->values()[Lcom/helpshift/redaction/RedactionState;

    move-result-object v0

    if-ltz p1, :cond_0

    .line 724
    array-length v1, v0

    if-le p1, v1, :cond_1

    :cond_0
    const/4 p1, 0x0

    .line 728
    :cond_1
    aget-object p1, v0, p1

    return-object p1
.end method

.method private intToRedactionType(I)Lcom/helpshift/redaction/RedactionType;
    .locals 2

    .line 735
    invoke-static {}, Lcom/helpshift/redaction/RedactionType;->values()[Lcom/helpshift/redaction/RedactionType;

    move-result-object v0

    if-ltz p1, :cond_0

    .line 736
    array-length v1, v0

    if-le p1, v1, :cond_1

    :cond_0
    const/4 p1, 0x0

    .line 739
    :cond_1
    aget-object p1, v0, p1

    return-object p1
.end method

.method private intToUserSyncStatus(I)Lcom/helpshift/account/domainmodel/UserSyncStatus;
    .locals 1

    if-ltz p1, :cond_0

    const/4 v0, 0x3

    if-le p1, v0, :cond_1

    :cond_0
    const/4 p1, 0x0

    .line 702
    :cond_1
    invoke-static {}, Lcom/helpshift/account/domainmodel/UserSyncStatus;->values()[Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-result-object v0

    aget-object p1, v0, p1

    return-object p1
.end method

.method private legacyAnalyticsIDPairToContentValues(Lcom/helpshift/common/platform/network/KeyValuePair;)Landroid/content/ContentValues;
    .locals 3

    .line 426
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    const-string v1, "identifier"

    .line 427
    iget-object v2, p1, Lcom/helpshift/common/platform/network/KeyValuePair;->key:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "analytics_event_id"

    .line 428
    iget-object p1, p1, Lcom/helpshift/common/platform/network/KeyValuePair;->value:Ljava/lang/String;

    invoke-virtual {v0, v1, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    return-object v0
.end method

.method private legacyProfileToContentValues(Lcom/helpshift/migration/legacyUser/LegacyProfile;)Landroid/content/ContentValues;
    .locals 3

    .line 416
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    const-string v1, "identifier"

    .line 417
    iget-object v2, p1, Lcom/helpshift/migration/legacyUser/LegacyProfile;->identifier:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "name"

    .line 418
    iget-object v2, p1, Lcom/helpshift/migration/legacyUser/LegacyProfile;->name:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "email"

    .line 419
    iget-object v2, p1, Lcom/helpshift/migration/legacyUser/LegacyProfile;->email:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "serverid"

    .line 420
    iget-object v2, p1, Lcom/helpshift/migration/legacyUser/LegacyProfile;->serverId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "migration_state"

    .line 421
    iget-object p1, p1, Lcom/helpshift/migration/legacyUser/LegacyProfile;->migrationState:Lcom/helpshift/migration/MigrationState;

    invoke-virtual {p1}, Lcom/helpshift/migration/MigrationState;->ordinal()I

    move-result p1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-virtual {v0, v1, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    return-object v0
.end method

.method private redactionDetailToContentValues(Lcom/helpshift/redaction/RedactionDetail;)Landroid/content/ContentValues;
    .locals 4

    .line 746
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    const-string v1, "user_local_id"

    .line 747
    iget-wide v2, p1, Lcom/helpshift/redaction/RedactionDetail;->userLocalId:J

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string v1, "redaction_state"

    .line 748
    iget-object v2, p1, Lcom/helpshift/redaction/RedactionDetail;->redactionState:Lcom/helpshift/redaction/RedactionState;

    invoke-virtual {v2}, Lcom/helpshift/redaction/RedactionState;->ordinal()I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v1, "redaction_type"

    .line 749
    iget-object p1, p1, Lcom/helpshift/redaction/RedactionDetail;->redactionType:Lcom/helpshift/redaction/RedactionType;

    invoke-virtual {p1}, Lcom/helpshift/redaction/RedactionType;->ordinal()I

    move-result p1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-virtual {v0, v1, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    return-object v0
.end method

.method private userDMToContentValues(Lcom/helpshift/account/domainmodel/UserDM;)Landroid/content/ContentValues;
    .locals 3

    .line 367
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    .line 368
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    if-eqz v1, :cond_0

    const-string v1, "_id"

    .line 369
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    .line 372
    :cond_0
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_1

    const-string v1, "identifier"

    .line 373
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_1
    const-string v1, "identifier"

    const-string v2, ""

    .line 376
    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 379
    :goto_0
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getName()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_2

    const-string v1, "name"

    .line 380
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_1

    :cond_2
    const-string v1, "name"

    const-string v2, ""

    .line 383
    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 386
    :goto_1
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_3

    const-string v1, "email"

    .line 387
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_2

    :cond_3
    const-string v1, "email"

    const-string v2, ""

    .line 390
    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 393
    :goto_2
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getDeviceId()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_4

    const-string v1, "deviceid"

    .line 394
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getDeviceId()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_3

    :cond_4
    const-string v1, "deviceid"

    const-string v2, ""

    .line 397
    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 400
    :goto_3
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getAuthToken()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_5

    const-string v1, "auth_token"

    .line 401
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getAuthToken()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_4

    :cond_5
    const-string v1, "auth_token"

    const-string v2, ""

    .line 404
    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    :goto_4
    const-string v1, "active"

    .line 407
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->isActiveUser()Z

    move-result v2

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Boolean;)V

    const-string v1, "anonymous"

    .line 408
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->isAnonymousUser()Z

    move-result v2

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Boolean;)V

    const-string v1, "issue_exists"

    .line 409
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->issueExists()Z

    move-result v2

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Boolean;)V

    const-string v1, "push_token_synced"

    .line 410
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->isPushTokenSynced()Z

    move-result v2

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Boolean;)V

    const-string v1, "initial_state_synced"

    .line 411
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getSyncState()Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserSyncStatus;->ordinal()I

    move-result p1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-virtual {v0, v1, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    return-object v0
.end method


# virtual methods
.method declared-synchronized activateUser(Ljava/lang/Long;)Z
    .locals 9

    monitor-enter p0

    const/4 v0, 0x0

    if-nez p1, :cond_0

    .line 222
    monitor-exit p0

    return v0

    :cond_0
    const/4 v1, 0x0

    const/4 v2, 0x1

    .line 229
    :try_start_0
    iget-object v3, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v3}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v3
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 230
    :try_start_1
    new-instance v1, Landroid/content/ContentValues;

    invoke-direct {v1}, Landroid/content/ContentValues;-><init>()V

    const-string v4, "active"

    .line 231
    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v5

    invoke-virtual {v1, v4, v5}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Boolean;)V

    .line 232
    new-instance v4, Landroid/content/ContentValues;

    invoke-direct {v4}, Landroid/content/ContentValues;-><init>()V

    const-string v5, "active"

    .line 233
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v6

    invoke-virtual {v4, v5, v6}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Boolean;)V

    .line 235
    invoke-virtual {v3}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    const-string v5, "user_table"

    const-string v6, "_id = ?"

    .line 237
    new-array v7, v2, [Ljava/lang/String;

    .line 240
    invoke-virtual {p1}, Ljava/lang/Long;->toString()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v7, v0

    .line 237
    invoke-virtual {v3, v5, v1, v6, v7}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I

    move-result v1

    if-lez v1, :cond_1

    const-string v1, "user_table"

    const-string v5, "_id != ?"

    .line 245
    new-array v6, v2, [Ljava/lang/String;

    .line 248
    invoke-virtual {p1}, Ljava/lang/Long;->toString()Ljava/lang/String;

    move-result-object p1

    aput-object p1, v6, v0

    .line 245
    invoke-virtual {v3, v1, v4, v5, v6}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I

    .line 250
    :cond_1
    invoke-virtual {v3}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz v3, :cond_2

    .line 259
    :try_start_2
    invoke-virtual {v3}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    goto :goto_0

    :catch_0
    move-exception p1

    :try_start_3
    const-string v0, "Helpshift_UserDB"

    const-string v1, "Error in activating user in finally block"

    .line 263
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    :cond_2
    :goto_0
    const/4 v0, 0x1

    goto :goto_2

    :catchall_0
    move-exception p1

    goto :goto_3

    :catch_1
    move-exception p1

    move-object v1, v3

    goto :goto_1

    :catchall_1
    move-exception p1

    move-object v3, v1

    goto :goto_3

    :catch_2
    move-exception p1

    :goto_1
    :try_start_4
    const-string v2, "Helpshift_UserDB"

    const-string v3, "Error in activating user"

    .line 254
    invoke-static {v2, v3, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v1, :cond_3

    .line 259
    :try_start_5
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_3
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    goto :goto_2

    :catch_3
    move-exception p1

    :try_start_6
    const-string v1, "Helpshift_UserDB"

    const-string v2, "Error in activating user in finally block"

    .line 263
    invoke-static {v1, v2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    .line 267
    :cond_3
    :goto_2
    monitor-exit p0

    return v0

    :goto_3
    if-eqz v3, :cond_4

    .line 259
    :try_start_7
    invoke-virtual {v3}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_7
    .catch Ljava/lang/Exception; {:try_start_7 .. :try_end_7} :catch_4
    .catchall {:try_start_7 .. :try_end_7} :catchall_2

    goto :goto_4

    :catchall_2
    move-exception p1

    goto :goto_5

    :catch_4
    move-exception v0

    :try_start_8
    const-string v1, "Helpshift_UserDB"

    const-string v2, "Error in activating user in finally block"

    .line 263
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 265
    :cond_4
    :goto_4
    throw p1
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_2

    .line 220
    :goto_5
    monitor-exit p0

    throw p1
.end method

.method createUser(Lcom/helpshift/account/domainmodel/UserDM;)Lcom/helpshift/account/domainmodel/UserDM;
    .locals 4

    const/4 v0, 0x0

    .line 72
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v1}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    .line 73
    invoke-direct {p0, p1}, Lcom/helpshift/account/dao/UserDB;->userDMToContentValues(Lcom/helpshift/account/domainmodel/UserDM;)Landroid/content/ContentValues;

    move-result-object v2

    const-string v3, "user_table"

    .line 74
    invoke-virtual {v1, v3, v0, v2}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "Helpshift_UserDB"

    const-string v3, "Error in creating user"

    .line 77
    invoke-static {v2, v3, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    move-object v1, v0

    :goto_0
    if-nez v1, :cond_0

    return-object v0

    .line 83
    :cond_0
    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/account/dao/UserDB;->getUserDMWithLocalId(Lcom/helpshift/account/domainmodel/UserDM;J)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p1

    return-object p1
.end method

.method declared-synchronized deleteClearedUser(Ljava/lang/Long;)Z
    .locals 8

    monitor-enter p0

    const/4 v0, 0x0

    const/4 v1, 0x1

    const-wide/16 v2, 0x0

    .line 527
    :try_start_0
    iget-object v4, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v4}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v4

    const-string v5, "user_table"

    const-string v6, "_id = ?"

    .line 528
    new-array v7, v1, [Ljava/lang/String;

    .line 530
    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v7, v0

    .line 528
    invoke-virtual {v4, v5, v6, v7}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    move-result p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    int-to-long v4, p1

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_1
    const-string v4, "Helpshift_UserDB"

    const-string v5, "Error in deleting cleared user"

    .line 533
    invoke-static {v4, v5, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    move-wide v4, v2

    :goto_0
    cmp-long p1, v4, v2

    if-lez p1, :cond_0

    const/4 v0, 0x1

    .line 535
    :cond_0
    monitor-exit p0

    return v0

    .line 523
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized deleteLegacyProfile(Ljava/lang/String;)V
    .locals 5

    monitor-enter p0

    .line 573
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v0}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    const-string v1, "legacy_profile_table"

    const-string v2, "identifier = ?"

    const/4 v3, 0x1

    .line 574
    new-array v3, v3, [Ljava/lang/String;

    const/4 v4, 0x0

    aput-object p1, v3, v4

    invoke-virtual {v0, v1, v2, v3}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_1
    const-string v0, "Helpshift_UserDB"

    const-string v1, "Error in deleting legacy profile"

    .line 579
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 581
    :goto_0
    monitor-exit p0

    return-void

    .line 572
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized deleteRedactionDetail(J)V
    .locals 5

    monitor-enter p0

    .line 843
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v0}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    const-string v1, "redaction_info_table"

    const-string v2, "user_local_id = ?"

    const/4 v3, 0x1

    .line 844
    new-array v3, v3, [Ljava/lang/String;

    const/4 v4, 0x0

    .line 846
    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v3, v4

    .line 844
    invoke-virtual {v0, v1, v2, v3}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_1
    const-string p2, "Helpshift_UserDB"

    const-string v0, "Error in deleting redaction detail"

    .line 849
    invoke-static {p2, v0, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 851
    :goto_0
    monitor-exit p0

    return-void

    .line 842
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized deleteUser(Ljava/lang/Long;)Z
    .locals 8

    monitor-enter p0

    const/4 v0, 0x0

    if-nez p1, :cond_0

    .line 273
    monitor-exit p0

    return v0

    :cond_0
    const/4 v1, 0x1

    const-wide/16 v2, 0x0

    .line 280
    :try_start_0
    iget-object v4, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v4}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v4

    const-string v5, "user_table"

    const-string v6, "_id = ?"

    .line 281
    new-array v7, v1, [Ljava/lang/String;

    .line 283
    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v7, v0

    .line 281
    invoke-virtual {v4, v5, v6, v7}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    move-result p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    int-to-long v4, p1

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_1
    const-string v4, "Helpshift_UserDB"

    const-string v5, "Error in deleting user"

    .line 286
    invoke-static {v4, v5, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    move-wide v4, v2

    :goto_0
    cmp-long p1, v4, v2

    if-lez p1, :cond_1

    const/4 v0, 0x1

    .line 288
    :cond_1
    monitor-exit p0

    return v0

    .line 271
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized fetchClearedUsers()Ljava/util/List;
    .locals 12
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/account/domainmodel/ClearedUserDM;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 467
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 v1, 0x0

    .line 471
    :try_start_1
    iget-object v2, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v2}, Lcom/helpshift/account/dao/UserDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v3

    const-string v4, "cleared_user_table"

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    .line 472
    invoke-virtual/range {v3 .. v10}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v2
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 480
    :try_start_2
    invoke-interface {v2}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 482
    :cond_0
    invoke-direct {p0, v2}, Lcom/helpshift/account/dao/UserDB;->cursorToClearedUserDM(Landroid/database/Cursor;)Lcom/helpshift/account/domainmodel/ClearedUserDM;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 483
    invoke-interface {v2}, Landroid/database/Cursor;->moveToNext()Z

    move-result v1
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-nez v1, :cond_0

    :cond_1
    if-eqz v2, :cond_2

    .line 491
    :goto_0
    :try_start_3
    invoke-interface {v2}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_2

    :catch_0
    move-exception v1

    goto :goto_1

    :catchall_0
    move-exception v0

    move-object v2, v1

    goto :goto_3

    :catch_1
    move-exception v2

    move-object v11, v2

    move-object v2, v1

    move-object v1, v11

    :goto_1
    :try_start_4
    const-string v3, "Helpshift_UserDB"

    const-string v4, "Error in reading all cleared users"

    .line 487
    invoke-static {v3, v4, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v2, :cond_2

    goto :goto_0

    .line 495
    :cond_2
    :goto_2
    monitor-exit p0

    return-object v0

    :catchall_1
    move-exception v0

    :goto_3
    if-eqz v2, :cond_3

    .line 491
    :try_start_5
    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    .line 493
    :cond_3
    throw v0
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    :catchall_2
    move-exception v0

    .line 466
    monitor-exit p0

    throw v0
.end method

.method declared-synchronized fetchLegacyAnalyticsEventId(Ljava/lang/String;)Ljava/lang/String;
    .locals 10

    monitor-enter p0

    const/4 v0, 0x1

    .line 671
    :try_start_0
    new-array v5, v0, [Ljava/lang/String;

    const/4 v0, 0x0

    aput-object p1, v5, v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 p1, 0x0

    .line 673
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v0}, Lcom/helpshift/account/dao/UserDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    const-string v2, "legacy_analytics_event_id_table"

    const/4 v3, 0x0

    const-string v4, "identifier = ?"

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    .line 674
    invoke-virtual/range {v1 .. v8}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v0
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 682
    :try_start_2
    invoke-interface {v0}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v1

    if-eqz v1, :cond_0

    const-string v1, "analytics_event_id"

    .line 683
    invoke-interface {v0, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {v0, v1}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v1
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    move-object p1, v1

    :cond_0
    if-eqz v0, :cond_1

    .line 691
    :goto_0
    :try_start_3
    invoke-interface {v0}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_2

    :catch_0
    move-exception v1

    goto :goto_1

    :catchall_0
    move-exception v0

    move-object v9, v0

    move-object v0, p1

    move-object p1, v9

    goto :goto_3

    :catch_1
    move-exception v1

    move-object v0, p1

    :goto_1
    :try_start_4
    const-string v2, "Helpshift_UserDB"

    const-string v3, "Error in reading legacy analytics eventID with identifier"

    .line 687
    invoke-static {v2, v3, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v0, :cond_1

    goto :goto_0

    .line 695
    :cond_1
    :goto_2
    monitor-exit p0

    return-object p1

    :catchall_1
    move-exception p1

    :goto_3
    if-eqz v0, :cond_2

    .line 691
    :try_start_5
    invoke-interface {v0}, Landroid/database/Cursor;->close()V

    .line 693
    :cond_2
    throw p1
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    :catchall_2
    move-exception p1

    .line 668
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized fetchLegacyProfile(Ljava/lang/String;)Lcom/helpshift/migration/legacyUser/LegacyProfile;
    .locals 10

    monitor-enter p0

    const/4 v0, 0x1

    .line 588
    :try_start_0
    new-array v5, v0, [Ljava/lang/String;

    const/4 v0, 0x0

    aput-object p1, v5, v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 p1, 0x0

    .line 590
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v0}, Lcom/helpshift/account/dao/UserDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    const-string v2, "legacy_profile_table"

    const/4 v3, 0x0

    const-string v4, "identifier = ?"

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    .line 591
    invoke-virtual/range {v1 .. v8}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v0
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 599
    :try_start_2
    invoke-interface {v0}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 600
    invoke-direct {p0, v0}, Lcom/helpshift/account/dao/UserDB;->cursorToLegacyProfile(Landroid/database/Cursor;)Lcom/helpshift/migration/legacyUser/LegacyProfile;

    move-result-object v1
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    move-object p1, v1

    :cond_0
    if-eqz v0, :cond_1

    .line 608
    :goto_0
    :try_start_3
    invoke-interface {v0}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_2

    :catch_0
    move-exception v1

    goto :goto_1

    :catchall_0
    move-exception v0

    move-object v9, v0

    move-object v0, p1

    move-object p1, v9

    goto :goto_3

    :catch_1
    move-exception v1

    move-object v0, p1

    :goto_1
    :try_start_4
    const-string v2, "Helpshift_UserDB"

    const-string v3, "Error in reading legacy profile with identifier"

    .line 604
    invoke-static {v2, v3, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v0, :cond_1

    goto :goto_0

    .line 612
    :cond_1
    :goto_2
    monitor-exit p0

    return-object p1

    :catchall_1
    move-exception p1

    :goto_3
    if-eqz v0, :cond_2

    .line 608
    :try_start_5
    invoke-interface {v0}, Landroid/database/Cursor;->close()V

    .line 610
    :cond_2
    throw p1
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    :catchall_2
    move-exception p1

    .line 585
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized fetchRedactionDetail(J)Lcom/helpshift/redaction/RedactionDetail;
    .locals 10

    monitor-enter p0

    const/4 v0, 0x1

    .line 798
    :try_start_0
    new-array v5, v0, [Ljava/lang/String;

    const/4 v0, 0x0

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v5, v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 p1, 0x0

    .line 800
    :try_start_1
    iget-object p2, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {p2}, Lcom/helpshift/account/dao/UserDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    const-string v2, "redaction_info_table"

    const/4 v3, 0x0

    const-string v4, "user_local_id = ?"

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    .line 801
    invoke-virtual/range {v1 .. v8}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p2
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 809
    :try_start_2
    invoke-interface {p2}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 810
    invoke-direct {p0, p2}, Lcom/helpshift/account/dao/UserDB;->cursorToRedactionDetail(Landroid/database/Cursor;)Lcom/helpshift/redaction/RedactionDetail;

    move-result-object v0
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    move-object p1, v0

    :cond_0
    if-eqz p2, :cond_1

    .line 818
    :goto_0
    :try_start_3
    invoke-interface {p2}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_2

    :catch_0
    move-exception v0

    goto :goto_1

    :catchall_0
    move-exception p2

    move-object v9, p2

    move-object p2, p1

    move-object p1, v9

    goto :goto_3

    :catch_1
    move-exception v0

    move-object p2, p1

    :goto_1
    :try_start_4
    const-string v1, "Helpshift_UserDB"

    const-string v2, "Error in reading redaction detail of the user"

    .line 814
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz p2, :cond_1

    goto :goto_0

    .line 821
    :cond_1
    :goto_2
    monitor-exit p0

    return-object p1

    :catchall_1
    move-exception p1

    :goto_3
    if-eqz p2, :cond_2

    .line 818
    :try_start_5
    invoke-interface {p2}, Landroid/database/Cursor;->close()V

    .line 820
    :cond_2
    throw p1
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    :catchall_2
    move-exception p1

    .line 795
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized fetchUser(Ljava/lang/Long;)Lcom/helpshift/account/domainmodel/UserDM;
    .locals 2

    monitor-enter p0

    if-nez p1, :cond_0

    const/4 p1, 0x0

    .line 144
    monitor-exit p0

    return-object p1

    :cond_0
    const/4 v0, 0x1

    .line 146
    :try_start_0
    new-array v0, v0, [Ljava/lang/String;

    const/4 v1, 0x0

    invoke-virtual {p1}, Ljava/lang/Long;->toString()Ljava/lang/String;

    move-result-object p1

    aput-object p1, v0, v1

    const-string p1, "_id = ?"

    .line 147
    invoke-direct {p0, p1, v0}, Lcom/helpshift/account/dao/UserDB;->getUser(Ljava/lang/String;[Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 142
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized fetchUser(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM;
    .locals 3

    monitor-enter p0

    if-nez p1, :cond_0

    if-nez p2, :cond_0

    const/4 p1, 0x0

    .line 153
    monitor-exit p0

    return-object p1

    :cond_0
    if-nez p1, :cond_1

    :try_start_0
    const-string p1, ""

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :cond_1
    :goto_0
    if-nez p2, :cond_2

    const-string p2, ""

    :cond_2
    const-string v0, "identifier = ? AND email = ?"

    const/4 v1, 0x2

    .line 165
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    aput-object p1, v1, v2

    const/4 p1, 0x1

    aput-object p2, v1, p1

    .line 166
    invoke-direct {p0, v0, v1}, Lcom/helpshift/account/dao/UserDB;->getUser(Ljava/lang/String;[Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    .line 151
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized fetchUsers()Ljava/util/List;
    .locals 12
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/account/domainmodel/UserDM;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 188
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 v1, 0x0

    .line 192
    :try_start_1
    iget-object v2, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v2}, Lcom/helpshift/account/dao/UserDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v3

    const-string v4, "user_table"

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    .line 193
    invoke-virtual/range {v3 .. v10}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v2
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 201
    :try_start_2
    invoke-interface {v2}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 203
    :cond_0
    invoke-direct {p0, v2}, Lcom/helpshift/account/dao/UserDB;->cursorToUserDM(Landroid/database/Cursor;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 204
    invoke-interface {v2}, Landroid/database/Cursor;->moveToNext()Z

    move-result v1
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-nez v1, :cond_0

    :cond_1
    if-eqz v2, :cond_2

    .line 212
    :goto_0
    :try_start_3
    invoke-interface {v2}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_2

    :catch_0
    move-exception v1

    goto :goto_1

    :catchall_0
    move-exception v0

    move-object v2, v1

    goto :goto_3

    :catch_1
    move-exception v2

    move-object v11, v2

    move-object v2, v1

    move-object v1, v11

    :goto_1
    :try_start_4
    const-string v3, "Helpshift_UserDB"

    const-string v4, "Error in reading all users"

    .line 208
    invoke-static {v3, v4, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v2, :cond_2

    goto :goto_0

    .line 216
    :cond_2
    :goto_2
    monitor-exit p0

    return-object v0

    :catchall_1
    move-exception v0

    :goto_3
    if-eqz v2, :cond_3

    .line 212
    :try_start_5
    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    .line 214
    :cond_3
    throw v0
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    :catchall_2
    move-exception v0

    .line 187
    monitor-exit p0

    throw v0
.end method

.method declared-synchronized getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;
    .locals 4

    monitor-enter p0

    :try_start_0
    const-string v0, "active = ?"

    const/4 v1, 0x1

    .line 172
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    sget-object v3, Lcom/helpshift/account/dao/UserDBInfo;->INT_TRUE:Ljava/lang/Integer;

    invoke-virtual {v3}, Ljava/lang/Integer;->toString()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v1, v2

    .line 174
    invoke-direct {p0, v0, v1}, Lcom/helpshift/account/dao/UserDB;->getUser(Ljava/lang/String;[Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 170
    monitor-exit p0

    throw v0
.end method

.method declared-synchronized getAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;
    .locals 4

    monitor-enter p0

    :try_start_0
    const-string v0, "anonymous = ?"

    const/4 v1, 0x1

    .line 180
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    sget-object v3, Lcom/helpshift/account/dao/UserDBInfo;->INT_TRUE:Ljava/lang/Integer;

    invoke-virtual {v3}, Ljava/lang/Integer;->toString()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v1, v2

    .line 182
    invoke-direct {p0, v0, v1}, Lcom/helpshift/account/dao/UserDB;->getUser(Ljava/lang/String;[Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 178
    monitor-exit p0

    throw v0
.end method

.method declared-synchronized insertClearedUser(Lcom/helpshift/account/domainmodel/ClearedUserDM;)Lcom/helpshift/account/domainmodel/ClearedUserDM;
    .locals 4

    monitor-enter p0

    const/4 v0, 0x0

    .line 450
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v1}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    .line 451
    invoke-direct {p0, p1}, Lcom/helpshift/account/dao/UserDB;->clearedUserDMtoContentValues(Lcom/helpshift/account/domainmodel/ClearedUserDM;)Landroid/content/ContentValues;

    move-result-object v2

    const-string v3, "cleared_user_table"

    .line 452
    invoke-virtual {v1, v3, v0, v2}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception v1

    :try_start_1
    const-string v2, "Helpshift_UserDB"

    const-string v3, "Error in creating cleared user"

    .line 455
    invoke-static {v2, v3, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    move-object v1, v0

    :goto_0
    if-nez v1, :cond_0

    .line 459
    monitor-exit p0

    return-object v0

    .line 461
    :cond_0
    :try_start_2
    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/account/dao/UserDB;->getClearUserDMWithLocalId(Lcom/helpshift/account/domainmodel/ClearedUserDM;J)Lcom/helpshift/account/domainmodel/ClearedUserDM;

    move-result-object p1
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    monitor-exit p0

    return-object p1

    .line 446
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized insertRedactionDetail(Lcom/helpshift/redaction/RedactionDetail;)V
    .locals 3

    monitor-enter p0

    .line 770
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v0}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    .line 771
    invoke-direct {p0, p1}, Lcom/helpshift/account/dao/UserDB;->redactionDetailToContentValues(Lcom/helpshift/redaction/RedactionDetail;)Landroid/content/ContentValues;

    move-result-object p1

    const-string v1, "redaction_info_table"

    const/4 v2, 0x0

    .line 772
    invoke-virtual {v0, v1, v2, p1}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_1
    const-string v0, "Helpshift_UserDB"

    const-string v1, "Error in inserting redaction info of the user"

    .line 775
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 777
    :goto_0
    monitor-exit p0

    return-void

    .line 769
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized storeLegacyAnalyticsEventIds(Ljava/util/List;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/common/platform/network/KeyValuePair;",
            ">;)V"
        }
    .end annotation

    monitor-enter p0

    const/4 v0, 0x0

    .line 642
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v1}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 644
    :try_start_1
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 645
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/common/platform/network/KeyValuePair;

    .line 646
    invoke-direct {p0, v2}, Lcom/helpshift/account/dao/UserDB;->legacyAnalyticsIDPairToContentValues(Lcom/helpshift/common/platform/network/KeyValuePair;)Landroid/content/ContentValues;

    move-result-object v2

    const-string v3, "legacy_analytics_event_id_table"

    .line 647
    invoke-virtual {v1, v3, v0, v2}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    goto :goto_0

    .line 649
    :cond_0
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz v1, :cond_1

    .line 657
    :try_start_2
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    goto :goto_3

    :catch_0
    move-exception p1

    :try_start_3
    const-string v0, "Helpshift_UserDB"

    const-string v1, "Error in storing legacy analytics events in finally block"

    .line 661
    :goto_1
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_3

    :catchall_0
    move-exception p1

    goto :goto_4

    :catch_1
    move-exception p1

    move-object v0, v1

    goto :goto_2

    :catchall_1
    move-exception p1

    move-object v1, v0

    goto :goto_4

    :catch_2
    move-exception p1

    :goto_2
    :try_start_4
    const-string v1, "Helpshift_UserDB"

    const-string v2, "Error in storing legacy analytics events"

    .line 652
    invoke-static {v1, v2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v0, :cond_1

    .line 657
    :try_start_5
    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_3
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    goto :goto_3

    :catch_3
    move-exception p1

    :try_start_6
    const-string v0, "Helpshift_UserDB"

    const-string v1, "Error in storing legacy analytics events in finally block"
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    goto :goto_1

    .line 664
    :cond_1
    :goto_3
    monitor-exit p0

    return-void

    :goto_4
    if-eqz v1, :cond_2

    .line 657
    :try_start_7
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_7
    .catch Ljava/lang/Exception; {:try_start_7 .. :try_end_7} :catch_4
    .catchall {:try_start_7 .. :try_end_7} :catchall_2

    goto :goto_5

    :catchall_2
    move-exception p1

    goto :goto_6

    :catch_4
    move-exception v0

    :try_start_8
    const-string v1, "Helpshift_UserDB"

    const-string v2, "Error in storing legacy analytics events in finally block"

    .line 661
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 663
    :cond_2
    :goto_5
    throw p1
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_2

    .line 639
    :goto_6
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized storeLegacyProfiles(Ljava/util/List;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/migration/legacyUser/LegacyProfile;",
            ">;)V"
        }
    .end annotation

    monitor-enter p0

    const/4 v0, 0x0

    .line 542
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v1}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 544
    :try_start_1
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 546
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/migration/legacyUser/LegacyProfile;

    .line 547
    invoke-direct {p0, v2}, Lcom/helpshift/account/dao/UserDB;->legacyProfileToContentValues(Lcom/helpshift/migration/legacyUser/LegacyProfile;)Landroid/content/ContentValues;

    move-result-object v2

    const-string v3, "legacy_profile_table"

    .line 548
    invoke-virtual {v1, v3, v0, v2}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    goto :goto_0

    .line 552
    :cond_0
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz v1, :cond_1

    .line 560
    :try_start_2
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    goto :goto_3

    :catch_0
    move-exception p1

    :try_start_3
    const-string v0, "Helpshift_UserDB"

    const-string v1, "Error in storing legacy profiles in finally block"

    .line 564
    :goto_1
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_3

    :catchall_0
    move-exception p1

    goto :goto_4

    :catch_1
    move-exception p1

    move-object v0, v1

    goto :goto_2

    :catchall_1
    move-exception p1

    move-object v1, v0

    goto :goto_4

    :catch_2
    move-exception p1

    :goto_2
    :try_start_4
    const-string v1, "Helpshift_UserDB"

    const-string v2, "Error in storing legacy profiles"

    .line 555
    invoke-static {v1, v2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v0, :cond_1

    .line 560
    :try_start_5
    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_3
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    goto :goto_3

    :catch_3
    move-exception p1

    :try_start_6
    const-string v0, "Helpshift_UserDB"

    const-string v1, "Error in storing legacy profiles in finally block"
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    goto :goto_1

    .line 567
    :cond_1
    :goto_3
    monitor-exit p0

    return-void

    :goto_4
    if-eqz v1, :cond_2

    .line 560
    :try_start_7
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_7
    .catch Ljava/lang/Exception; {:try_start_7 .. :try_end_7} :catch_4
    .catchall {:try_start_7 .. :try_end_7} :catchall_2

    goto :goto_5

    :catchall_2
    move-exception p1

    goto :goto_6

    :catch_4
    move-exception v0

    :try_start_8
    const-string v1, "Helpshift_UserDB"

    const-string v2, "Error in storing legacy profiles in finally block"

    .line 564
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 566
    :cond_2
    :goto_5
    throw p1
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_2

    .line 539
    :goto_6
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized updateClearedUserSyncState(Ljava/lang/Long;Lcom/helpshift/account/dao/ClearedUserSyncState;)Z
    .locals 6

    monitor-enter p0

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 504
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v2}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    .line 506
    new-instance v3, Landroid/content/ContentValues;

    invoke-direct {v3}, Landroid/content/ContentValues;-><init>()V

    const-string v4, "sync_state"

    .line 507
    invoke-virtual {p2}, Lcom/helpshift/account/dao/ClearedUserSyncState;->ordinal()I

    move-result p2

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    invoke-virtual {v3, v4, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string p2, "cleared_user_table"

    const-string v4, "_id = ?"

    .line 508
    new-array v5, v0, [Ljava/lang/String;

    .line 511
    invoke-virtual {p1}, Ljava/lang/Long;->toString()Ljava/lang/String;

    move-result-object p1

    aput-object p1, v5, v1

    .line 508
    invoke-virtual {v2, p2, v3, v4, v5}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_1
    const-string p2, "Helpshift_UserDB"

    const-string v0, "Error in updating cleared user sync status"

    .line 515
    invoke-static {p2, v0, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    const/4 v0, 0x0

    .line 518
    :goto_0
    monitor-exit p0

    return v0

    .line 499
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized updateRedactionDetail(Lcom/helpshift/redaction/RedactionDetail;)V
    .locals 8

    monitor-enter p0

    .line 782
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v0}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    .line 783
    invoke-direct {p0, p1}, Lcom/helpshift/account/dao/UserDB;->redactionDetailToContentValues(Lcom/helpshift/redaction/RedactionDetail;)Landroid/content/ContentValues;

    move-result-object v1

    const-string v2, "redaction_info_table"

    const-string v3, "user_local_id = ?"

    const/4 v4, 0x1

    .line 784
    new-array v4, v4, [Ljava/lang/String;

    const/4 v5, 0x0

    iget-wide v6, p1, Lcom/helpshift/redaction/RedactionDetail;->userLocalId:J

    .line 787
    invoke-static {v6, v7}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v4, v5

    .line 784
    invoke-virtual {v0, v2, v1, v3, v4}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_1
    const-string v0, "Helpshift_UserDB"

    const-string v1, "Error in updating redaction detail"

    .line 790
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 792
    :goto_0
    monitor-exit p0

    return-void

    .line 781
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized updateRedactionState(JLcom/helpshift/redaction/RedactionState;)V
    .locals 5

    monitor-enter p0

    .line 827
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v0}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    .line 828
    new-instance v1, Landroid/content/ContentValues;

    invoke-direct {v1}, Landroid/content/ContentValues;-><init>()V

    const-string v2, "redaction_state"

    .line 829
    invoke-virtual {p3}, Lcom/helpshift/redaction/RedactionState;->ordinal()I

    move-result p3

    invoke-static {p3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p3

    invoke-virtual {v1, v2, p3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string p3, "redaction_info_table"

    const-string v2, "user_local_id = ?"

    const/4 v3, 0x1

    .line 830
    new-array v3, v3, [Ljava/lang/String;

    const/4 v4, 0x0

    .line 833
    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v3, v4

    .line 830
    invoke-virtual {v0, p3, v1, v2, v3}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_1
    const-string p2, "Helpshift_UserDB"

    const-string p3, "Error in updating redaction status"

    .line 836
    invoke-static {p2, p3, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 838
    :goto_0
    monitor-exit p0

    return-void

    .line 826
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized updateUser(Lcom/helpshift/account/domainmodel/UserDM;)Z
    .locals 7

    monitor-enter p0

    .line 88
    :try_start_0
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 89
    monitor-exit p0

    return v1

    :cond_0
    const/4 v0, 0x1

    .line 96
    :try_start_1
    iget-object v2, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v2}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    .line 98
    invoke-direct {p0, p1}, Lcom/helpshift/account/dao/UserDB;->userDMToContentValues(Lcom/helpshift/account/domainmodel/UserDM;)Landroid/content/ContentValues;

    move-result-object v3

    const-string v4, "user_table"

    const-string v5, "_id = ?"

    .line 99
    new-array v6, v0, [Ljava/lang/String;

    .line 102
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object p1

    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v6, v1

    .line 99
    invoke-virtual {v2, v4, v3, v5, v6}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :catch_0
    move-exception p1

    :try_start_2
    const-string v0, "Helpshift_UserDB"

    const-string v2, "Error in updating user"

    .line 106
    invoke-static {v0, v2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    const/4 v0, 0x0

    .line 109
    :goto_0
    monitor-exit p0

    return v0

    :catchall_0
    move-exception p1

    .line 87
    monitor-exit p0

    throw p1
.end method

.method declared-synchronized updateUserMigrationState(Ljava/lang/String;Lcom/helpshift/migration/MigrationState;)Z
    .locals 6

    monitor-enter p0

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 621
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/account/dao/UserDB;->userDBHelper:Lcom/helpshift/account/dao/UserDBHelper;

    invoke-virtual {v2}, Lcom/helpshift/account/dao/UserDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    .line 623
    new-instance v3, Landroid/content/ContentValues;

    invoke-direct {v3}, Landroid/content/ContentValues;-><init>()V

    const-string v4, "migration_state"

    .line 624
    invoke-virtual {p2}, Lcom/helpshift/migration/MigrationState;->ordinal()I

    move-result p2

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    invoke-virtual {v3, v4, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string p2, "legacy_profile_table"

    const-string v4, "identifier = ?"

    .line 625
    new-array v5, v0, [Ljava/lang/String;

    aput-object p1, v5, v1

    invoke-virtual {v2, p2, v3, v4, v5}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_1
    const-string p2, "Helpshift_UserDB"

    const-string v0, "Error in updating user migration sync status"

    .line 632
    invoke-static {p2, v0, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    const/4 v0, 0x0

    .line 635
    :goto_0
    monitor-exit p0

    return v0

    .line 616
    :goto_1
    monitor-exit p0

    throw p1
.end method
