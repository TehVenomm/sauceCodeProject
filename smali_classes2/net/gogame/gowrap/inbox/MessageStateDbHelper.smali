.class public Lnet/gogame/gowrap/inbox/MessageStateDbHelper;
.super Landroid/database/sqlite/SQLiteOpenHelper;
.source "MessageStateDbHelper.java"


# static fields
.field private static final DATABASE_NAME:Ljava/lang/String; = "goWrap-messages"

.field private static final DATABASE_VERSION:I = 0x1

.field private static instance:Lnet/gogame/gowrap/inbox/MessageStateDbHelper;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 3

    const-string v0, "goWrap-messages"

    const/4 v1, 0x0

    const/4 v2, 0x1

    .line 15
    invoke-direct {p0, p1, v0, v1, v2}, Landroid/database/sqlite/SQLiteOpenHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Landroid/database/sqlite/SQLiteDatabase$CursorFactory;I)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/database/sqlite/SQLiteDatabase$CursorFactory;)V
    .locals 2

    const-string v0, "goWrap-messages"

    const/4 v1, 0x1

    .line 19
    invoke-direct {p0, p1, v0, p2, v1}, Landroid/database/sqlite/SQLiteOpenHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Landroid/database/sqlite/SQLiteDatabase$CursorFactory;I)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/database/sqlite/SQLiteDatabase$CursorFactory;Landroid/database/DatabaseErrorHandler;)V
    .locals 6

    const-string v2, "goWrap-messages"

    const/4 v4, 0x1

    move-object v0, p0

    move-object v1, p1

    move-object v3, p2

    move-object v5, p3

    .line 24
    invoke-direct/range {v0 .. v5}, Landroid/database/sqlite/SQLiteOpenHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Landroid/database/sqlite/SQLiteDatabase$CursorFactory;ILandroid/database/DatabaseErrorHandler;)V

    return-void
.end method

.method public static declared-synchronized getInstance(Landroid/content/Context;)Lnet/gogame/gowrap/inbox/MessageStateDbHelper;
    .locals 2

    const-class v0, Lnet/gogame/gowrap/inbox/MessageStateDbHelper;

    monitor-enter v0

    .line 28
    :try_start_0
    sget-object v1, Lnet/gogame/gowrap/inbox/MessageStateDbHelper;->instance:Lnet/gogame/gowrap/inbox/MessageStateDbHelper;

    if-nez v1, :cond_0

    .line 29
    new-instance v1, Lnet/gogame/gowrap/inbox/MessageStateDbHelper;

    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/inbox/MessageStateDbHelper;-><init>(Landroid/content/Context;)V

    sput-object v1, Lnet/gogame/gowrap/inbox/MessageStateDbHelper;->instance:Lnet/gogame/gowrap/inbox/MessageStateDbHelper;

    .line 31
    :cond_0
    sget-object p0, Lnet/gogame/gowrap/inbox/MessageStateDbHelper;->instance:Lnet/gogame/gowrap/inbox/MessageStateDbHelper;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object p0

    :catchall_0
    move-exception p0

    .line 27
    monitor-exit v0

    throw p0
.end method


# virtual methods
.method public onCreate(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 1

    const-string v0, "CREATE TABLE IF NOT EXISTS message_status (    message_type VARCHAR(255),    message_id BIGINT,    message_timestamp BIGINT,    message_read TINYINT,    UNIQUE(message_type, message_id) ON CONFLICT REPLACE);"

    .line 36
    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method

.method public onUpgrade(Landroid/database/sqlite/SQLiteDatabase;II)V
    .locals 0

    if-eq p2, p3, :cond_0

    const-string p2, "DROP TABLE IF EXISTS message_status"

    .line 49
    invoke-virtual {p1, p2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    .line 50
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/inbox/MessageStateDbHelper;->onCreate(Landroid/database/sqlite/SQLiteDatabase;)V

    :cond_0
    return-void
.end method
