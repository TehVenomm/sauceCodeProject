.class public Lcom/helpshift/common/conversation/migration/ConversationMigrationFactory;
.super Ljava/lang/Object;
.source "ConversationMigrationFactory.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 6
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getMigrationForDbVersion(ILandroid/database/sqlite/SQLiteDatabase;)Lcom/helpshift/common/migrator/Migrator;
    .locals 1

    const/4 v0, 0x6

    if-ne p0, v0, :cond_0

    .line 10
    new-instance p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;

    invoke-direct {p0, p1}, Lcom/helpshift/common/conversation/migration/MigrationFromDb_6_to_7;-><init>(Landroid/database/sqlite/SQLiteDatabase;)V

    return-object p0

    :cond_0
    const/4 v0, 0x7

    if-ne p0, v0, :cond_1

    .line 13
    new-instance p0, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;

    invoke-direct {p0, p1}, Lcom/helpshift/common/conversation/migration/MigrationFromDb_7_to_8;-><init>(Landroid/database/sqlite/SQLiteDatabase;)V

    return-object p0

    .line 16
    :cond_1
    new-instance p0, Ljava/lang/IllegalStateException;

    const-string p1, "Unsupported version for database migration"

    invoke-direct {p0, p1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p0
.end method
