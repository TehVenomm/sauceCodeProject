.class final Lcom/helpshift/support/storage/SectionsDataSource$LazyHolder;
.super Ljava/lang/Object;
.source "SectionsDataSource.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/storage/SectionsDataSource;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1a
    name = "LazyHolder"
.end annotation


# static fields
.field static final INSTANCE:Lcom/helpshift/support/storage/SectionsDataSource;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 178
    new-instance v0, Lcom/helpshift/support/storage/SectionsDataSource;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Lcom/helpshift/support/storage/SectionsDataSource;-><init>(Lcom/helpshift/support/storage/SectionsDataSource$1;)V

    sput-object v0, Lcom/helpshift/support/storage/SectionsDataSource$LazyHolder;->INSTANCE:Lcom/helpshift/support/storage/SectionsDataSource;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 177
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
