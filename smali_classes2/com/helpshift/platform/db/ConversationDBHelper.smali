.class public Lcom/helpshift/platform/db/ConversationDBHelper;
.super Landroid/database/sqlite/SQLiteOpenHelper;
.source "ConversationDBHelper.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_ConversationDB"


# instance fields
.field private final dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;


# direct methods
.method public constructor <init>(Landroid/content/Context;Lcom/helpshift/common/conversation/ConversationDBInfo;)V
    .locals 3

    const-string v0, "__hs__db_issues"

    .line 22
    sget-object v1, Lcom/helpshift/common/conversation/ConversationDBInfo;->DATABASE_VERSION:Ljava/lang/Integer;

    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v1

    const/4 v2, 0x0

    invoke-direct {p0, p1, v0, v2, v1}, Landroid/database/sqlite/SQLiteOpenHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Landroid/database/sqlite/SQLiteDatabase$CursorFactory;I)V

    .line 23
    iput-object p2, p0, Lcom/helpshift/platform/db/ConversationDBHelper;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    return-void
.end method


# virtual methods
.method public dropAndCreateDatabase(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 5

    .line 97
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->isOpen()Z

    move-result v0

    if-eqz v0, :cond_2

    const/4 v0, 0x0

    .line 99
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/platform/db/ConversationDBHelper;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Lcom/helpshift/common/conversation/ConversationDBInfo;->getQueriesForDropAndCreate()Ljava/util/List;

    move-result-object v1

    .line 100
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 101
    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 102
    invoke-virtual {p1, v2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    goto :goto_0

    .line 104
    :cond_0
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 112
    :try_start_1
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v1

    if-eqz v1, :cond_2

    .line 113
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_3

    :catch_0
    move-exception p1

    const-string v1, "Helpshift_ConversationDB"

    const-string v2, "Error in dropAndCreateDatabase inside finally block, "

    .line 117
    new-array v0, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-static {v1, v2, p1, v0}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    goto :goto_3

    :catchall_0
    move-exception v1

    goto :goto_1

    :catch_1
    move-exception v1

    :try_start_2
    const-string v2, "Helpshift_ConversationDB"

    .line 107
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Exception while upgrading tables, version: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v4, Lcom/helpshift/common/conversation/ConversationDBInfo;->DATABASE_VERSION:Ljava/lang/Integer;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    new-array v4, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-static {v2, v3, v1, v4}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 112
    :try_start_3
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v1

    if-eqz v1, :cond_2

    .line 113
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_0

    goto :goto_3

    .line 112
    :goto_1
    :try_start_4
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 113
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_2

    goto :goto_2

    :catch_2
    move-exception p1

    .line 117
    new-array v0, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v2, "Helpshift_ConversationDB"

    const-string v3, "Error in dropAndCreateDatabase inside finally block, "

    invoke-static {v2, v3, p1, v0}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 119
    :cond_1
    :goto_2
    throw v1

    :cond_2
    :goto_3
    return-void
.end method

.method public onCreate(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 5

    .line 28
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->isOpen()Z

    move-result v0

    if-eqz v0, :cond_2

    const/4 v0, 0x0

    .line 30
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/platform/db/ConversationDBHelper;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Lcom/helpshift/common/conversation/ConversationDBInfo;->getQueriesForOnCreate()Ljava/util/List;

    move-result-object v1

    .line 31
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 32
    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 33
    invoke-virtual {p1, v2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    goto :goto_0

    .line 35
    :cond_0
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 43
    :try_start_1
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v1

    if-eqz v1, :cond_2

    .line 44
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_3

    :catch_0
    move-exception p1

    const-string v1, "Helpshift_ConversationDB"

    const-string v2, "Error in onCreate inside finally block, "

    .line 48
    new-array v0, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-static {v1, v2, p1, v0}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    goto :goto_3

    :catchall_0
    move-exception v1

    goto :goto_1

    :catch_1
    move-exception v1

    :try_start_2
    const-string v2, "Helpshift_ConversationDB"

    .line 38
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Exception while creating tables: version: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v4, Lcom/helpshift/common/conversation/ConversationDBInfo;->DATABASE_VERSION:Ljava/lang/Integer;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    new-array v4, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-static {v2, v3, v1, v4}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 43
    :try_start_3
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v1

    if-eqz v1, :cond_2

    .line 44
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_0

    goto :goto_3

    .line 43
    :goto_1
    :try_start_4
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 44
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_2

    goto :goto_2

    :catch_2
    move-exception p1

    .line 48
    new-array v0, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v2, "Helpshift_ConversationDB"

    const-string v3, "Error in onCreate inside finally block, "

    invoke-static {v2, v3, p1, v0}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 50
    :cond_1
    :goto_2
    throw v1

    :cond_2
    :goto_3
    return-void
.end method

.method public onUpgrade(Landroid/database/sqlite/SQLiteDatabase;II)V
    .locals 5

    .line 56
    iget-object v0, p0, Lcom/helpshift/platform/db/ConversationDBHelper;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Lcom/helpshift/common/conversation/ConversationDBInfo;->getMinimumDBVersionSupportedForMigration()I

    move-result v0

    if-ge p2, v0, :cond_0

    .line 57
    invoke-virtual {p0, p1}, Lcom/helpshift/platform/db/ConversationDBHelper;->dropAndCreateDatabase(Landroid/database/sqlite/SQLiteDatabase;)V

    goto/16 :goto_5

    .line 60
    :cond_0
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->isOpen()Z

    move-result v0

    if-eqz v0, :cond_5

    const/4 v0, 0x0

    .line 63
    :try_start_0
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    move v1, p2

    :goto_0
    if-ge v1, p3, :cond_1

    .line 65
    invoke-static {v1, p1}, Lcom/helpshift/common/conversation/migration/ConversationMigrationFactory;->getMigrationForDbVersion(ILandroid/database/sqlite/SQLiteDatabase;)Lcom/helpshift/common/migrator/Migrator;

    move-result-object v2

    .line 66
    invoke-virtual {v2}, Lcom/helpshift/common/migrator/Migrator;->migrate()V

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 68
    :cond_1
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 78
    :try_start_1
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result p2

    if-eqz p2, :cond_3

    .line 79
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_2

    :catch_0
    move-exception p2

    const-string p3, "Helpshift_ConversationDB"

    const-string v1, "Exception while migrating conversationDB inside finally block, "

    .line 83
    new-array v2, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-static {p3, v1, p2, v2}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    goto :goto_2

    :catchall_0
    move-exception p2

    goto :goto_3

    :catch_1
    move-exception v1

    :try_start_2
    const-string v2, "Helpshift_ConversationDB"

    .line 73
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Exception while migrating conversationDB, old: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, p2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string p2, ", new: "

    invoke-virtual {v3, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, p3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    new-array p3, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-static {v2, p2, v1, p3}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 78
    :try_start_3
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result p2

    if-eqz p2, :cond_2

    .line 79
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_2

    goto :goto_1

    :catch_2
    move-exception p2

    const-string p3, "Helpshift_ConversationDB"

    const-string v1, "Exception while migrating conversationDB inside finally block, "

    .line 83
    new-array v0, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-static {p3, v1, p2, v0}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    :cond_2
    :goto_1
    const/4 v0, 0x1

    :cond_3
    :goto_2
    if-eqz v0, :cond_5

    .line 87
    invoke-virtual {p0, p1}, Lcom/helpshift/platform/db/ConversationDBHelper;->dropAndCreateDatabase(Landroid/database/sqlite/SQLiteDatabase;)V

    .line 88
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->resetUsersSyncStatusAndStartSetupForActiveUser()V

    goto :goto_5

    .line 78
    :goto_3
    :try_start_4
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result p3

    if-eqz p3, :cond_4

    .line 79
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_3

    goto :goto_4

    :catch_3
    move-exception p1

    .line 83
    new-array p3, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v0, "Helpshift_ConversationDB"

    const-string v1, "Exception while migrating conversationDB inside finally block, "

    invoke-static {v0, v1, p1, p3}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 85
    :cond_4
    :goto_4
    throw p2

    :cond_5
    :goto_5
    return-void
.end method
