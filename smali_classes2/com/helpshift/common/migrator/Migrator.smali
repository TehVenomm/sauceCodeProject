.class public abstract Lcom/helpshift/common/migrator/Migrator;
.super Ljava/lang/Object;
.source "Migrator.java"


# instance fields
.field protected db:Landroid/database/sqlite/SQLiteDatabase;


# direct methods
.method public constructor <init>(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 0

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 9
    iput-object p1, p0, Lcom/helpshift/common/migrator/Migrator;->db:Landroid/database/sqlite/SQLiteDatabase;

    return-void
.end method


# virtual methods
.method public abstract migrate()V
.end method
