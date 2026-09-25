.class Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;
.super Landroid/database/sqlite/SQLiteOpenHelper;
.source "SupportKeyValueDBStorageHelper.java"


# static fields
.field private static final DATABASE_NAME:Ljava/lang/String; = "__hs__db_support_key_values"

.field private static final DATABASE_VERSION:Ljava/lang/Integer;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const/4 v0, 0x1

    .line 11
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;->DATABASE_VERSION:Ljava/lang/Integer;

    return-void
.end method

.method constructor <init>(Landroid/content/Context;)V
    .locals 3

    const-string v0, "__hs__db_support_key_values"

    .line 14
    sget-object v1, Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;->DATABASE_VERSION:Ljava/lang/Integer;

    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v1

    const/4 v2, 0x0

    invoke-direct {p0, p1, v0, v2, v1}, Landroid/database/sqlite/SQLiteOpenHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Landroid/database/sqlite/SQLiteDatabase$CursorFactory;I)V

    return-void
.end method

.method private dropTable(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 1

    const-string v0, "DROP TABLE IF EXISTS key_value_store;"

    .line 38
    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public onCreate(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 1

    const-string v0, "CREATE TABLE key_value_store(key text primary key,value blob not null);"

    .line 19
    invoke-virtual {p1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;)V

    return-void
.end method

.method public onDowngrade(Landroid/database/sqlite/SQLiteDatabase;II)V
    .locals 0

    .line 33
    invoke-direct {p0, p1}, Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;->dropTable(Landroid/database/sqlite/SQLiteDatabase;)V

    .line 34
    invoke-virtual {p0, p1}, Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;->onCreate(Landroid/database/sqlite/SQLiteDatabase;)V

    return-void
.end method

.method public onUpgrade(Landroid/database/sqlite/SQLiteDatabase;II)V
    .locals 0

    .line 27
    invoke-direct {p0, p1}, Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;->dropTable(Landroid/database/sqlite/SQLiteDatabase;)V

    .line 28
    invoke-virtual {p0, p1}, Lcom/helpshift/support/storage/SupportKeyValueDBStorageHelper;->onCreate(Landroid/database/sqlite/SQLiteDatabase;)V

    return-void
.end method
