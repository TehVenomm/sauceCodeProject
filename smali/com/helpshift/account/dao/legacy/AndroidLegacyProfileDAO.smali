.class public Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;
.super Landroid/database/sqlite/SQLiteOpenHelper;
.source "AndroidLegacyProfileDAO.java"

# interfaces
.implements Lcom/helpshift/migration/legacyUser/LegacyProfileDAO;


# static fields
.field private static final ALTER_PROFILES_TABLE_ADD_DEVICE_ID:Ljava/lang/String; = "ALTER TABLE profiles ADD did TEXT"

.field private static final ALTER_PROFILES_TABLE_ADD_PUSH_TOKEN_SYNC_STATUS:Ljava/lang/String; = "ALTER TABLE profiles ADD push_token_sync INTEGER"

.field private static final ALTER_PROFILES_TABLE_ADD_USER_ID:Ljava/lang/String; = "ALTER TABLE profiles ADD uid TEXT"

.field private static final COLUMN_DID:Ljava/lang/String; = "did"

.field private static final COLUMN_EMAIL:Ljava/lang/String; = "email"

.field private static final COLUMN_ID:Ljava/lang/String; = "_id"

.field private static final COLUMN_IDENTIFIER:Ljava/lang/String; = "IDENTIFIER"

.field private static final COLUMN_NAME:Ljava/lang/String; = "name"

.field private static final COLUMN_PROFILE_ID:Ljava/lang/String; = "profile_id"

.field private static final COLUMN_PUSH_TOKEN_SYNC_STATUS:Ljava/lang/String; = "push_token_sync"

.field private static final COLUMN_SALT:Ljava/lang/String; = "salt"

.field private static final COLUMN_UID:Ljava/lang/String; = "uid"

.field private static final DATABASE_CREATE:Ljava/lang/String; = "CREATE TABLE profiles(_id INTEGER PRIMARY KEY AUTOINCREMENT, IDENTIFIER TEXT NOT NULL UNIQUE, profile_id TEXT UNIQUE, name TEXT, email TEXT, salt TEXT, uid TEXT, did TEXT, push_token_sync INTEGER );"

.field private static final DATABASE_NAME:Ljava/lang/String; = "__hs__db_profiles"

.field private static final DATABASE_VERSION:I = 0x3

.field private static final TABLE_PROFILES:Ljava/lang/String; = "profiles"

.field private static final TAG:Ljava/lang/String; = "Helpshift_ALProfileDAO"

.field private static instance:Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;


# direct methods
.method private constructor <init>(Landroid/content/Context;)V
    .locals 3

    const-string v0, "__hs__db_profiles"

    const/4 v1, 0x0

    const/4 v2, 0x3

    .line 53
    invoke-direct {p0, p1, v0, v1, v2}, Landroid/database/sqlite/SQLiteOpenHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Landroid/database/sqlite/SQLiteDatabase$CursorFactory;I)V

    return-void
.end method

.method private cursorToProfile(Landroid/database/Cursor;)Lcom/helpshift/account/dao/ProfileDTO;
    .locals 11

    .line 135
    new-instance v10, Lcom/helpshift/account/dao/ProfileDTO;

    const-string v0, "_id"

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    .line 136
    invoke-static {p1}, Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;->getColumnIndexForIdentifier(Landroid/database/Cursor;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v2

    const-string v0, "profile_id"

    .line 137
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v3

    const-string v0, "name"

    .line 138
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v4

    const-string v0, "email"

    .line 139
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v5

    const-string v0, "salt"

    .line 140
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v6

    const-string v0, "uid"

    .line 141
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v7

    const-string v0, "did"

    .line 142
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v8

    const-string v0, "push_token_sync"

    .line 143
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result p1

    const/4 v0, 0x1

    if-ne p1, v0, :cond_0

    const/4 v9, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    const/4 v9, 0x0

    :goto_0
    move-object v0, v10

    invoke-direct/range {v0 .. v9}, Lcom/helpshift/account/dao/ProfileDTO;-><init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)V

    return-object v10
.end method

.method private static getColumnIndexForIdentifier(Landroid/database/Cursor;)I
    .locals 2

    const-string v0, "IDENTIFIER"

    .line 68
    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    const/4 v1, -0x1

    if-ne v0, v1, :cond_0

    const-string v0, "IDENTIFIER"

    .line 70
    invoke-virtual {v0}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v0

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    :cond_0
    return v0
.end method

.method public static declared-synchronized getInstance(Landroid/content/Context;)Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;
    .locals 2

    const-class v0, Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;

    monitor-enter v0

    .line 57
    :try_start_0
    sget-object v1, Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;->instance:Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;

    if-nez v1, :cond_0

    .line 58
    new-instance v1, Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;

    invoke-direct {v1, p0}, Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;-><init>(Landroid/content/Context;)V

    sput-object v1, Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;->instance:Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;

    .line 60
    :cond_0
    sget-object p0, Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;->instance:Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object p0

    :catchall_0
    move-exception p0

    .line 56
    monitor-exit v0

    throw p0
.end method


# virtual methods
.method public deleteProfiles()V
    .locals 2

    .line 102
    invoke-virtual {p0}, Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    if-eqz v0, :cond_0

    const-string v1, "DROP TABLE IF EXISTS profiles"

    .line 104
    invoke-virtual {v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public fetchProfiles()Ljava/util/List;
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/account/dao/ProfileDTO;",
            ">;"
        }
    .end annotation

    const/4 v0, 0x0

    .line 80
    :try_start_0
    invoke-virtual {p0}, Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    const-string v2, "profiles"

    const/4 v3, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    .line 81
    invoke-virtual/range {v1 .. v8}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 82
    :try_start_1
    invoke-interface {v1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 83
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 85
    :cond_0
    :try_start_2
    invoke-direct {p0, v1}, Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;->cursorToProfile(Landroid/database/Cursor;)Lcom/helpshift/account/dao/ProfileDTO;

    move-result-object v0

    invoke-interface {v2, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 86
    invoke-interface {v1}, Landroid/database/Cursor;->moveToNext()Z

    move-result v0
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-nez v0, :cond_0

    move-object v0, v2

    goto :goto_0

    :catch_0
    move-exception v0

    goto :goto_1

    :cond_1
    :goto_0
    if-eqz v1, :cond_3

    .line 94
    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    goto :goto_2

    :catch_1
    move-exception v2

    move-object v9, v2

    move-object v2, v0

    move-object v0, v9

    goto :goto_1

    :catchall_0
    move-exception v1

    move-object v9, v1

    move-object v1, v0

    move-object v0, v9

    goto :goto_3

    :catch_2
    move-exception v1

    move-object v2, v0

    move-object v0, v1

    move-object v1, v2

    :goto_1
    :try_start_3
    const-string v3, "Helpshift_ALProfileDAO"

    const-string v4, "Error in fetchProfiles"

    .line 90
    invoke-static {v3, v4, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    if-eqz v1, :cond_2

    .line 94
    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    :cond_2
    move-object v0, v2

    :cond_3
    :goto_2
    return-object v0

    :catchall_1
    move-exception v0

    :goto_3
    if-eqz v1, :cond_4

    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    .line 96
    :cond_4
    throw v0
.end method

.method public onCreate(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 1

    const-string v0, "CREATE TABLE profiles(_id INTEGER PRIMARY KEY AUTOINCREMENT, IDENTIFIER TEXT NOT NULL UNIQUE, profile_id TEXT UNIQUE, name TEXT, email TEXT, salt TEXT, uid TEXT, did TEXT, push_token_sync INTEGER );"

    .line 110
    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method

.method public onDowngrade(Landroid/database/sqlite/SQLiteDatabase;II)V
    .locals 0

    .line 128
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->isOpen()Z

    move-result p2

    if-eqz p2, :cond_0

    const-string p2, "DROP TABLE IF EXISTS profiles"

    .line 129
    invoke-virtual {p1, p2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    .line 130
    invoke-virtual {p0, p1}, Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;->onCreate(Landroid/database/sqlite/SQLiteDatabase;)V

    :cond_0
    return-void
.end method

.method public onUpgrade(Landroid/database/sqlite/SQLiteDatabase;II)V
    .locals 0

    .line 115
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->isOpen()Z

    move-result p3

    if-eqz p3, :cond_1

    const/4 p3, 0x2

    if-ge p2, p3, :cond_0

    const-string p3, "ALTER TABLE profiles ADD uid TEXT"

    .line 117
    invoke-virtual {p1, p3}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const-string p3, "ALTER TABLE profiles ADD did TEXT"

    .line 118
    invoke-virtual {p1, p3}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    :cond_0
    const/4 p3, 0x3

    if-ge p2, p3, :cond_1

    const-string p2, "ALTER TABLE profiles ADD push_token_sync INTEGER"

    .line 121
    invoke-virtual {p1, p2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    :cond_1
    return-void
.end method
