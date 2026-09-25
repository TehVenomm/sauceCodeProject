.class public Lcom/helpshift/account/dao/UserDBInfo;
.super Ljava/lang/Object;
.source "UserDBInfo.java"


# static fields
.field static final ACTIVE:Ljava/lang/String; = "active"

.field static final ANALYTICS_EVENT_ID:Ljava/lang/String; = "analytics_event_id"

.field static final ANONOYMOUS:Ljava/lang/String; = "anonymous"

.field static final AUTH_TOKEN:Ljava/lang/String; = "auth_token"

.field static final COLUMN_ID:Ljava/lang/String; = "_id"

.field static final CREATE_CLEARED_USER_TABLE:Ljava/lang/String; = "CREATE TABLE cleared_user_table ( _id INTEGER PRIMARY KEY AUTOINCREMENT, identifier TEXT, name TEXT, email TEXT, deviceid TEXT, auth_token TEXT, sync_state INTEGER );"

.field static final CREATE_LEGACY_ANALYTICS_EVENT_IDS_TABLE:Ljava/lang/String; = "CREATE TABLE legacy_analytics_event_id_table ( identifier TEXT, analytics_event_id TEXT );"

.field static final CREATE_LEGACY_PROFILE_TABLE:Ljava/lang/String; = "CREATE TABLE legacy_profile_table ( identifier TEXT PRIMARY KEY, name TEXT, email TEXT, serverid TEXT, migration_state INTEGER );"

.field static final CREATE_REDACTION_INFO_TABLE:Ljava/lang/String; = "CREATE TABLE redaction_info_table ( user_local_id INTEGER PRIMARY KEY, redaction_state INTEGER , redaction_type INTEGER );"

.field static final CREATE_USER_TABLE:Ljava/lang/String; = "CREATE TABLE user_table(_id INTEGER PRIMARY KEY AUTOINCREMENT, identifier TEXT, name TEXT, email TEXT, deviceid TEXT, auth_token TEXT, active INTEGER DEFAULT 0, anonymous INTEGER DEFAULT 0, issue_exists INTEGER DEFAULT 1, initial_state_synced INTEGER DEFAULT 0, push_token_synced INTEGER DEFAULT 0 );"

.field static final DATABASE_NAME:Ljava/lang/String; = "__hs_db_helpshift_users"

.field static final DATABASE_VERSION:Ljava/lang/Integer;

.field static final DEVICE_ID:Ljava/lang/String; = "deviceid"

.field static final EMAIL:Ljava/lang/String; = "email"

.field static final IDENTIFIER:Ljava/lang/String; = "identifier"

.field static final INITIAL_STATE_SYNCED:Ljava/lang/String; = "initial_state_synced"

.field static final INT_TRUE:Ljava/lang/Integer;

.field static final ISSUE_EXISTS:Ljava/lang/String; = "issue_exists"

.field static final MIGRATION_STATE:Ljava/lang/String; = "migration_state"

.field static final NAME:Ljava/lang/String; = "name"

.field static final PUSH_TOKEN_SYNCED:Ljava/lang/String; = "push_token_synced"

.field static final REDACTION_STATE:Ljava/lang/String; = "redaction_state"

.field static final REDACTION_TYPE:Ljava/lang/String; = "redaction_type"

.field static final SERVER_ID:Ljava/lang/String; = "serverid"

.field static final SYNC_STATE:Ljava/lang/String; = "sync_state"

.field static final TABLE_CLEARED_USERS:Ljava/lang/String; = "cleared_user_table"

.field static final TABLE_LEGACY_ANALYTICS_EVENT_IDS:Ljava/lang/String; = "legacy_analytics_event_id_table"

.field static final TABLE_LEGACY_PROFILES:Ljava/lang/String; = "legacy_profile_table"

.field static final TABLE_REDACTION_INFO:Ljava/lang/String; = "redaction_info_table"

.field static final TABLE_USERS:Ljava/lang/String; = "user_table"

.field static final USER_LOCAL_ID:Ljava/lang/String; = "user_local_id"

.field static final WHERE_LOCAL_ID_IS:Ljava/lang/String; = "_id = ?"


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const/4 v0, 0x2

    .line 18
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    sput-object v0, Lcom/helpshift/account/dao/UserDBInfo;->DATABASE_VERSION:Ljava/lang/Integer;

    const/4 v0, 0x1

    .line 106
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    sput-object v0, Lcom/helpshift/account/dao/UserDBInfo;->INT_TRUE:Ljava/lang/Integer;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getQueriesForDropAndCreate()Ljava/util/List;
    .locals 2
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 129
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const-string v1, "DROP TABLE IF EXISTS user_table"

    .line 130
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "DROP TABLE IF EXISTS legacy_profile_table"

    .line 131
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "DROP TABLE IF EXISTS cleared_user_table"

    .line 132
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "DROP TABLE IF EXISTS legacy_analytics_event_id_table"

    .line 133
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "DROP TABLE IF EXISTS redaction_info_table"

    .line 134
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 137
    invoke-virtual {p0}, Lcom/helpshift/account/dao/UserDBInfo;->getQueriesForOnCreate()Ljava/util/List;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    return-object v0
.end method

.method public getQueriesForOnCreate()Ljava/util/List;
    .locals 6
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 110
    new-instance v0, Ljava/util/ArrayList;

    const-string v1, "CREATE TABLE user_table(_id INTEGER PRIMARY KEY AUTOINCREMENT, identifier TEXT, name TEXT, email TEXT, deviceid TEXT, auth_token TEXT, active INTEGER DEFAULT 0, anonymous INTEGER DEFAULT 0, issue_exists INTEGER DEFAULT 1, initial_state_synced INTEGER DEFAULT 0, push_token_synced INTEGER DEFAULT 0 );"

    const-string v2, "CREATE TABLE cleared_user_table ( _id INTEGER PRIMARY KEY AUTOINCREMENT, identifier TEXT, name TEXT, email TEXT, deviceid TEXT, auth_token TEXT, sync_state INTEGER );"

    const-string v3, "CREATE TABLE legacy_profile_table ( identifier TEXT PRIMARY KEY, name TEXT, email TEXT, serverid TEXT, migration_state INTEGER );"

    const-string v4, "CREATE TABLE legacy_analytics_event_id_table ( identifier TEXT, analytics_event_id TEXT );"

    const-string v5, "CREATE TABLE redaction_info_table ( user_local_id INTEGER PRIMARY KEY, redaction_state INTEGER , redaction_type INTEGER );"

    filled-new-array {v1, v2, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    return-object v0
.end method

.method public getQueriesForOnUpgrade(I)Ljava/util/List;
    .locals 2
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 119
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const/4 v1, 0x1

    if-ne p1, v1, :cond_0

    const-string p1, "CREATE TABLE redaction_info_table ( user_local_id INTEGER PRIMARY KEY, redaction_state INTEGER , redaction_type INTEGER );"

    .line 122
    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_0
    return-object v0
.end method
