.class public Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;
.super Lcom/helpshift/common/migrator/Migrator;
.source "MigrationFromDb_7_to_8.java"


# instance fields
.field private RENAME_CONVERSATION_TABLE:Ljava/lang/String;

.field private TEMP_TABLE_CONVERSATIONS:Ljava/lang/String;

.field private dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;


# direct methods
.method constructor <init>(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 1

    .line 20
    invoke-direct {p0, p1}, Lcom/helpshift/common/migrator/Migrator;-><init>(Landroid/database/sqlite/SQLiteDatabase;)V

    .line 13
    new-instance p1, Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-direct {p1}, Lcom/helpshift/common/conversation/ConversationDBInfo;-><init>()V

    iput-object p1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    .line 14
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "issues"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "_old"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->TEMP_TABLE_CONVERSATIONS:Ljava/lang/String;

    .line 16
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "ALTER TABLE "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "issues"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " RENAME TO "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->TEMP_TABLE_CONVERSATIONS:Ljava/lang/String;

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->RENAME_CONVERSATION_TABLE:Ljava/lang/String;

    return-void
.end method

.method private dropTemporaryTables()V
    .locals 3

    .line 103
    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->db:Landroid/database/sqlite/SQLiteDatabase;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "DROP TABLE IF EXISTS "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->TEMP_TABLE_CONVERSATIONS:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method

.method private getDuplicacyRecord()I
    .locals 3

    .line 71
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "SELECT Count("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "server_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ") , Count(DISTINCT "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "server_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ") FROM "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->TEMP_TABLE_CONVERSATIONS:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 74
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "SELECT Count("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "pre_conv_server_id"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ") , Count(DISTINCT "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "pre_conv_server_id"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ") FROM "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->TEMP_TABLE_CONVERSATIONS:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 79
    invoke-direct {p0, v0}, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->getDuplicateRecord(Ljava/lang/String;)I

    move-result v0

    .line 80
    invoke-direct {p0, v1}, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->getDuplicateRecord(Ljava/lang/String;)I

    move-result v1

    add-int/2addr v0, v1

    return v0
.end method

.method private getDuplicateRecord(Ljava/lang/String;)I
    .locals 2

    .line 85
    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->db:Landroid/database/sqlite/SQLiteDatabase;

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Landroid/database/sqlite/SQLiteDatabase;->rawQuery(Ljava/lang/String;[Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1

    .line 88
    :try_start_0
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 89
    invoke-interface {p1, v1}, Landroid/database/Cursor;->getInt(I)I

    move-result v0

    const/4 v1, 0x1

    .line 90
    invoke-interface {p1, v1}, Landroid/database/Cursor;->getInt(I)I

    move-result v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    sub-int v1, v0, v1

    :cond_0
    if-eqz p1, :cond_1

    .line 96
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_1
    return v1

    :catchall_0
    move-exception v0

    if-eqz p1, :cond_2

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    .line 98
    :cond_2
    throw v0
.end method

.method private migrateData()V
    .locals 2

    .line 49
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "INSERT INTO "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "issues"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " ("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "server_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "pre_conv_server_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "publish_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "uuid"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "user_local_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "title"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ","

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "issue_type"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "state"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "show_agent_name"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ","

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "message_cursor"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ","

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "start_new_conversation_action"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "is_redacted"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "meta"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ","

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "last_user_activity_time"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "full_privacy_enabled"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "epoch_time_created_at"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "created_at"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "updated_at"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " ) SELECT "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "server_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "pre_conv_server_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "publish_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "uuid"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "user_local_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "title"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ","

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "issue_type"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "state"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "show_agent_name"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ","

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "message_cursor"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ","

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "start_new_conversation_action"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "is_redacted"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "meta"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ","

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "last_user_activity_time"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "full_privacy_enabled"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "epoch_time_created_at"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "created_at"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "updated_at"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " FROM "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->TEMP_TABLE_CONVERSATIONS:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 67
    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->db:Landroid/database/sqlite/SQLiteDatabase;

    invoke-virtual {v1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method

.method private migrateTable()V
    .locals 2

    .line 44
    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->RENAME_CONVERSATION_TABLE:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    .line 45
    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    iget-object v1, v1, Lcom/helpshift/common/conversation/ConversationDBInfo;->CREATE_CONVERSATION_TABLE:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public migrate()V
    .locals 1

    .line 29
    invoke-direct {p0}, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->migrateTable()V

    .line 30
    invoke-direct {p0}, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->getDuplicacyRecord()I

    move-result v0

    if-nez v0, :cond_0

    .line 31
    invoke-direct {p0}, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->migrateData()V

    goto :goto_0

    .line 37
    :cond_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->resetUsersSyncStatusAndStartSetupForActiveUser()V

    .line 40
    :goto_0
    invoke-direct {p0}, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;->dropTemporaryTables()V

    return-void
.end method
