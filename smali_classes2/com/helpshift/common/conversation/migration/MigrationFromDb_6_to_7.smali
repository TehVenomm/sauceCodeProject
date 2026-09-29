.class public Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;
.super Lcom/helpshift/common/migrator/Migrator;
.source "MigrationFromDb_6_to_7.java"


# instance fields
.field private ADD_EPOCH_TIME_CREATE_AT_COLUMN_INTO_CONVERSATIONS_TABLE:Ljava/lang/String;

.field private ADD_EPOCH_TIME_CREATE_AT_COLUMN_INTO_MESSAGES_TABLE:Ljava/lang/String;

.field private ADD_FULL_PRIVACY_ENABLED_COLUMN_INTO_CONVERSATIONS_TABLE:Ljava/lang/String;

.field private ADD_HAS_OLDER_MESSAGES_COLUMN_INTO_INBOX_TABLE:Ljava/lang/String;

.field private ADD_IS_REDACTED_COLUMN_INTO_CONVERSATIONS_TABLE:Ljava/lang/String;

.field private ADD_IS_REDACTED_COLUMN_INTO_MESSAGES_TABLE:Ljava/lang/String;

.field private ADD_LAST_CONVERSATIONS_REDACTED_TIME_COLUMN_INTO_INBOX_TABLE:Ljava/lang/String;

.field private dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;


# direct methods
.method constructor <init>(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 1

    .line 43
    invoke-direct {p0, p1}, Lcom/helpshift/common/migrator/Migrator;-><init>(Landroid/database/sqlite/SQLiteDatabase;)V

    .line 16
    new-instance p1, Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-direct {p1}, Lcom/helpshift/common/conversation/ConversationDBInfo;-><init>()V

    iput-object p1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    .line 18
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "ALTER TABLE "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "conversation_inbox"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " ADD COLUMN "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "has_older_messages"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " INT ;"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_HAS_OLDER_MESSAGES_COLUMN_INTO_INBOX_TABLE:Ljava/lang/String;

    .line 21
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "ALTER TABLE "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "conversation_inbox"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " ADD COLUMN "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "last_conv_redaction_time"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " INT ;"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_LAST_CONVERSATIONS_REDACTED_TIME_COLUMN_INTO_INBOX_TABLE:Ljava/lang/String;

    .line 25
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "ALTER TABLE "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "issues"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " ADD COLUMN "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "full_privacy_enabled"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " INTEGER ;"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_FULL_PRIVACY_ENABLED_COLUMN_INTO_CONVERSATIONS_TABLE:Ljava/lang/String;

    .line 28
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "ALTER TABLE "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "issues"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " ADD COLUMN "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "is_redacted"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " INTEGER ;"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_IS_REDACTED_COLUMN_INTO_CONVERSATIONS_TABLE:Ljava/lang/String;

    .line 31
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "ALTER TABLE "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "issues"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " ADD COLUMN "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "epoch_time_created_at"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " INTEGER NOT NULL DEFAULT 0 ;"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_EPOCH_TIME_CREATE_AT_COLUMN_INTO_CONVERSATIONS_TABLE:Ljava/lang/String;

    .line 35
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "ALTER TABLE "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "messages"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " ADD COLUMN "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "is_redacted"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " INTEGER ;"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_IS_REDACTED_COLUMN_INTO_MESSAGES_TABLE:Ljava/lang/String;

    .line 38
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "ALTER TABLE "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "messages"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " ADD COLUMN "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "epoch_time_created_at"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " INTEGER NOT NULL DEFAULT 0 ;"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_EPOCH_TIME_CREATE_AT_COLUMN_INTO_MESSAGES_TABLE:Ljava/lang/String;

    return-void
.end method

.method private migrateData()V
    .locals 11

    .line 63
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 64
    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "_id"

    iget-object v2, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "created_at"

    filled-new-array {v1, v2}, [Ljava/lang/String;

    move-result-object v1

    .line 67
    iget-object v3, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v2, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "issues"

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    move-object v5, v1

    invoke-virtual/range {v3 .. v10}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v2

    .line 69
    invoke-interface {v2}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v3

    if-eqz v3, :cond_1

    .line 71
    :cond_0
    iget-object v3, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "_id"

    invoke-interface {v2, v3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v3

    invoke-interface {v2, v3}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v3

    .line 72
    iget-object v5, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v5}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v5, "created_at"

    invoke-interface {v2, v5}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v5

    invoke-interface {v2, v5}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v5

    .line 73
    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    invoke-interface {v0, v3, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 74
    invoke-interface {v2}, Landroid/database/Cursor;->moveToNext()Z

    move-result v3

    if-nez v3, :cond_0

    .line 76
    :cond_1
    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    .line 79
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    .line 80
    iget-object v3, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v4, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "messages"

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    move-object v5, v1

    invoke-virtual/range {v3 .. v10}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v1

    .line 82
    invoke-interface {v1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v3

    if-eqz v3, :cond_3

    .line 84
    :cond_2
    iget-object v3, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "_id"

    invoke-interface {v1, v3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v3

    invoke-interface {v1, v3}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v3

    .line 85
    iget-object v5, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v5}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v5, "created_at"

    invoke-interface {v1, v5}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v5

    invoke-interface {v1, v5}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v5

    .line 86
    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    invoke-interface {v2, v3, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 87
    invoke-interface {v1}, Landroid/database/Cursor;->moveToNext()Z

    move-result v3

    if-nez v3, :cond_2

    .line 89
    :cond_3
    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    .line 92
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 93
    invoke-interface {v0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_4

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/util/Map$Entry;

    .line 94
    invoke-interface {v3}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    .line 95
    invoke-static {v4}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v4

    .line 96
    invoke-interface {v3}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    invoke-static {v4, v5}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v4

    invoke-interface {v1, v3, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 99
    :cond_4
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 100
    invoke-interface {v2}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_1
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_5

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/util/Map$Entry;

    .line 101
    invoke-interface {v3}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    .line 102
    invoke-static {v4}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v4

    .line 103
    invoke-interface {v3}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    invoke-static {v4, v5}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v4

    invoke-interface {v0, v3, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_1

    .line 107
    :cond_5
    invoke-interface {v1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_2
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    const/4 v3, 0x0

    const/4 v4, 0x1

    if-eqz v2, :cond_6

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    .line 108
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/Long;

    .line 109
    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/Long;

    .line 110
    new-instance v6, Landroid/content/ContentValues;

    invoke-direct {v6}, Landroid/content/ContentValues;-><init>()V

    .line 111
    iget-object v7, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "epoch_time_created_at"

    invoke-virtual {v6, v7, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    .line 113
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v7, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "_id"

    invoke-virtual {v2, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v7, " = ?"

    invoke-virtual {v2, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 114
    new-array v4, v4, [Ljava/lang/String;

    invoke-static {v5}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v3

    .line 115
    iget-object v3, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v5, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v5}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v5, "issues"

    invoke-virtual {v3, v5, v6, v2, v4}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I

    goto :goto_2

    .line 121
    :cond_6
    invoke-interface {v0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_3
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_7

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 122
    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/Long;

    .line 123
    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/Long;

    .line 124
    new-instance v5, Landroid/content/ContentValues;

    invoke-direct {v5}, Landroid/content/ContentValues;-><init>()V

    .line 125
    iget-object v6, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v6, "epoch_time_created_at"

    invoke-virtual {v5, v6, v1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    .line 127
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v6, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v6, "_id"

    invoke-virtual {v1, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v6, " = ?"

    invoke-virtual {v1, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 128
    new-array v6, v4, [Ljava/lang/String;

    invoke-static {v2}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    aput-object v2, v6, v3

    .line 129
    iget-object v2, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v7, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "messages"

    invoke-virtual {v2, v7, v5, v1, v6}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I

    goto :goto_3

    :cond_7
    return-void
.end method

.method private migrateTable()V
    .locals 2

    .line 53
    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_HAS_OLDER_MESSAGES_COLUMN_INTO_INBOX_TABLE:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    .line 54
    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_LAST_CONVERSATIONS_REDACTED_TIME_COLUMN_INTO_INBOX_TABLE:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    .line 55
    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_FULL_PRIVACY_ENABLED_COLUMN_INTO_CONVERSATIONS_TABLE:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    .line 56
    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_IS_REDACTED_COLUMN_INTO_CONVERSATIONS_TABLE:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    .line 57
    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_EPOCH_TIME_CREATE_AT_COLUMN_INTO_MESSAGES_TABLE:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    .line 58
    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_IS_REDACTED_COLUMN_INTO_MESSAGES_TABLE:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    .line 59
    iget-object v0, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->db:Landroid/database/sqlite/SQLiteDatabase;

    iget-object v1, p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->ADD_EPOCH_TIME_CREATE_AT_COLUMN_INTO_CONVERSATIONS_TABLE:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public migrate()V
    .locals 0

    .line 48
    invoke-direct {p0}, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->migrateTable()V

    .line 49
    invoke-direct {p0}, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;->migrateData()V

    return-void
.end method
