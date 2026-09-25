.class public Lcom/helpshift/support/storage/FaqsDBHelper;
.super Landroid/database/sqlite/SQLiteOpenHelper;
.source "FaqsDBHelper.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/storage/FaqsDBHelper$LazyHolder;
    }
.end annotation


# static fields
.field private static final CUR_DATABASE_VERSION:I = 0x3

.field private static final DATABASE_NAME:Ljava/lang/String; = "__hs__db_faq"


# direct methods
.method constructor <init>(Landroid/content/Context;)V
    .locals 3

    const-string v0, "__hs__db_faq"

    const/4 v1, 0x0

    const/4 v2, 0x3

    .line 22
    invoke-direct {p0, p1, v0, v1, v2}, Landroid/database/sqlite/SQLiteOpenHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Landroid/database/sqlite/SQLiteDatabase$CursorFactory;I)V

    return-void
.end method

.method private createTables(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 1

    const-string v0, "CREATE TABLE faqs (_id INTEGER PRIMARY KEY AUTOINCREMENT,question_id TEXT NOT NULL,publish_id TEXT NOT NULL,language TEXT NOT NULL,section_id TEXT NOT NULL,title TEXT NOT NULL,body TEXT NOT NULL,helpful INTEGER,rtl INTEGER,tags TEXT,c_tags TEXT,FOREIGN KEY(section_id) REFERENCES sections (_id));"

    .line 47
    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const-string v0, "CREATE TABLE sections (_id INTEGER PRIMARY KEY AUTOINCREMENT,section_id TEXT NOT NULL,publish_id INTEGER NOT NULL,title TEXT NOT NULL);"

    .line 62
    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method

.method private dropTables(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 1

    const-string v0, "DROP TABLE IF EXISTS faqs"

    .line 71
    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    const-string v0, "DROP TABLE IF EXISTS sections"

    .line 72
    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method

.method public static getInstance()Lcom/helpshift/support/storage/FaqsDBHelper;
    .locals 1

    .line 26
    sget-object v0, Lcom/helpshift/support/storage/FaqsDBHelper$LazyHolder;->INSTANCE:Lcom/helpshift/support/storage/FaqsDBHelper;

    return-object v0
.end method


# virtual methods
.method public clearDatabase(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 0

    .line 76
    invoke-direct {p0, p1}, Lcom/helpshift/support/storage/FaqsDBHelper;->dropTables(Landroid/database/sqlite/SQLiteDatabase;)V

    .line 77
    invoke-direct {p0, p1}, Lcom/helpshift/support/storage/FaqsDBHelper;->createTables(Landroid/database/sqlite/SQLiteDatabase;)V

    return-void
.end method

.method public onCreate(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 0

    .line 31
    invoke-direct {p0, p1}, Lcom/helpshift/support/storage/FaqsDBHelper;->createTables(Landroid/database/sqlite/SQLiteDatabase;)V

    return-void
.end method

.method public onDowngrade(Landroid/database/sqlite/SQLiteDatabase;II)V
    .locals 1

    .line 42
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object p2

    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object p2

    const-string p3, "/faqs/"

    const/4 v0, 0x0

    invoke-interface {p2, p3, v0}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->storeETag(Ljava/lang/String;Ljava/lang/String;)V

    .line 43
    invoke-virtual {p0, p1}, Lcom/helpshift/support/storage/FaqsDBHelper;->clearDatabase(Landroid/database/sqlite/SQLiteDatabase;)V

    return-void
.end method

.method public onUpgrade(Landroid/database/sqlite/SQLiteDatabase;II)V
    .locals 1

    .line 36
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object p2

    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object p2

    const-string p3, "/faqs/"

    const/4 v0, 0x0

    invoke-interface {p2, p3, v0}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->storeETag(Ljava/lang/String;Ljava/lang/String;)V

    .line 37
    invoke-virtual {p0, p1}, Lcom/helpshift/support/storage/FaqsDBHelper;->clearDatabase(Landroid/database/sqlite/SQLiteDatabase;)V

    return-void
.end method
