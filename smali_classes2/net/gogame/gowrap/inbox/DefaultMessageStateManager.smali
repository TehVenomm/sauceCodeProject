.class public Lnet/gogame/gowrap/inbox/DefaultMessageStateManager;
.super Ljava/lang/Object;
.source "DefaultMessageStateManager.java"

# interfaces
.implements Lnet/gogame/gowrap/inbox/MessageStateManager;


# static fields
.field private static final TABLE_COLUMNS:[Ljava/lang/String;

.field private static final TABLE_NAME:Ljava/lang/String; = "message_status"


# instance fields
.field private final context:Landroid/content/Context;

.field private final dbHelper:Lnet/gogame/gowrap/inbox/MessageStateDbHelper;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    const-string v0, "message_type"

    const-string v1, "message_id"

    const-string v2, "message_timestamp"

    const-string v3, "message_read"

    .line 15
    filled-new-array {v0, v1, v2, v3}, [Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lnet/gogame/gowrap/inbox/DefaultMessageStateManager;->TABLE_COLUMNS:[Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 24
    iput-object p1, p0, Lnet/gogame/gowrap/inbox/DefaultMessageStateManager;->context:Landroid/content/Context;

    .line 25
    invoke-static {p1}, Lnet/gogame/gowrap/inbox/MessageStateDbHelper;->getInstance(Landroid/content/Context;)Lnet/gogame/gowrap/inbox/MessageStateDbHelper;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/inbox/DefaultMessageStateManager;->dbHelper:Lnet/gogame/gowrap/inbox/MessageStateDbHelper;

    return-void
.end method


# virtual methods
.method public deleteMessageStates(J)V
    .locals 6

    .line 87
    iget-object v0, p0, Lnet/gogame/gowrap/inbox/DefaultMessageStateManager;->dbHelper:Lnet/gogame/gowrap/inbox/MessageStateDbHelper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/inbox/MessageStateDbHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    :try_start_0
    const-string v1, "message_status"

    .line 89
    sget-object v2, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    const-string v3, "(message_timestamp < %d)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    .line 92
    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p1

    aput-object p1, v4, v5

    .line 90
    invoke-static {v2, v3, v4}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    const/4 p2, 0x0

    .line 89
    invoke-virtual {v0, v1, p1, p2}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 95
    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    return-void

    :catchall_0
    move-exception p1

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    .line 96
    throw p1
.end method

.method public deleteMessageStates(Ljava/lang/String;J)V
    .locals 6

    .line 73
    iget-object v0, p0, Lnet/gogame/gowrap/inbox/DefaultMessageStateManager;->dbHelper:Lnet/gogame/gowrap/inbox/MessageStateDbHelper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/inbox/MessageStateDbHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    :try_start_0
    const-string v1, "message_status"

    .line 75
    sget-object v2, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    const-string v3, "(message_type = ?) AND (message_timestamp < %d)"

    const/4 v4, 0x1

    new-array v5, v4, [Ljava/lang/Object;

    .line 78
    invoke-static {p2, p3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p2

    const/4 p3, 0x0

    aput-object p2, v5, p3

    .line 76
    invoke-static {v2, v3, v5}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    new-array v2, v4, [Ljava/lang/String;

    aput-object p1, v2, p3

    .line 75
    invoke-virtual {v0, v1, p2, v2}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 81
    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    return-void

    :catchall_0
    move-exception p1

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    .line 82
    throw p1
.end method

.method public getMessageStates(Ljava/lang/String;J)Ljava/util/List;
    .locals 11
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "J)",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/inbox/MessageState;",
            ">;"
        }
    .end annotation

    .line 30
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 31
    iget-object v1, p0, Lnet/gogame/gowrap/inbox/DefaultMessageStateManager;->dbHelper:Lnet/gogame/gowrap/inbox/MessageStateDbHelper;

    invoke-virtual {v1}, Lnet/gogame/gowrap/inbox/MessageStateDbHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    :try_start_0
    const-string v3, "message_status"

    .line 33
    sget-object v4, Lnet/gogame/gowrap/inbox/DefaultMessageStateManager;->TABLE_COLUMNS:[Ljava/lang/String;

    sget-object v2, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    const-string v5, "(message_type = ?) AND (message_timestamp >= %d)"

    const/4 v10, 0x1

    new-array v6, v10, [Ljava/lang/Object;

    .line 36
    invoke-static {p2, p3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p2

    const/4 p3, 0x0

    aput-object p2, v6, p3

    .line 34
    invoke-static {v2, v5, v6}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v5

    new-array v6, v10, [Ljava/lang/String;

    aput-object p1, v6, p3

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    move-object v2, v1

    .line 33
    invoke-virtual/range {v2 .. v9}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 39
    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    :goto_0
    invoke-interface {p1}, Landroid/database/Cursor;->isAfterLast()Z

    move-result p2

    if-nez p2, :cond_1

    .line 40
    new-instance p2, Lnet/gogame/gowrap/inbox/MessageState;

    invoke-direct {p2}, Lnet/gogame/gowrap/inbox/MessageState;-><init>()V

    .line 41
    invoke-interface {p1, p3}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v2}, Lnet/gogame/gowrap/inbox/MessageState;->setType(Ljava/lang/String;)V

    .line 42
    invoke-interface {p1, v10}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v2

    invoke-virtual {p2, v2, v3}, Lnet/gogame/gowrap/inbox/MessageState;->setId(J)V

    const/4 v2, 0x2

    .line 43
    invoke-interface {p1, v2}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v2

    invoke-virtual {p2, v2, v3}, Lnet/gogame/gowrap/inbox/MessageState;->setTimestamp(J)V

    const/4 v2, 0x3

    .line 44
    invoke-interface {p1, v2}, Landroid/database/Cursor;->getShort(I)S

    move-result v2

    if-eqz v2, :cond_0

    const/4 v2, 0x1

    goto :goto_1

    :cond_0
    const/4 v2, 0x0

    :goto_1
    invoke-virtual {p2, v2}, Lnet/gogame/gowrap/inbox/MessageState;->setRead(Z)V

    .line 45
    invoke-interface {v0, p2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 39
    invoke-interface {p1}, Landroid/database/Cursor;->moveToNext()Z
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    .line 48
    :cond_1
    :try_start_2
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    .line 51
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    return-object v0

    :catchall_0
    move-exception p2

    .line 48
    :try_start_3
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    .line 49
    throw p2
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    :catchall_1
    move-exception p1

    .line 51
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    .line 52
    throw p1
.end method

.method public setMessageState(Ljava/lang/String;JJZ)V
    .locals 3

    .line 58
    iget-object v0, p0, Lnet/gogame/gowrap/inbox/DefaultMessageStateManager;->dbHelper:Lnet/gogame/gowrap/inbox/MessageStateDbHelper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/inbox/MessageStateDbHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    .line 60
    :try_start_0
    new-instance v1, Landroid/content/ContentValues;

    invoke-direct {v1}, Landroid/content/ContentValues;-><init>()V

    const-string v2, "message_type"

    .line 61
    invoke-virtual {v1, v2, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "message_id"

    .line 62
    invoke-static {p2, p3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p2

    invoke-virtual {v1, p1, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string p1, "message_timestamp"

    .line 63
    invoke-static {p4, p5}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p2

    invoke-virtual {v1, p1, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string p1, "message_read"

    int-to-short p2, p6

    .line 64
    invoke-static {p2}, Ljava/lang/Short;->valueOf(S)Ljava/lang/Short;

    move-result-object p2

    invoke-virtual {v1, p1, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Short;)V

    const-string p1, "message_status"

    const/4 p2, 0x0

    .line 65
    invoke-virtual {v0, p1, p2, v1}, Landroid/database/sqlite/SQLiteDatabase;->replace(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 67
    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    return-void

    :catchall_0
    move-exception p1

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->close()V

    .line 68
    throw p1
.end method
