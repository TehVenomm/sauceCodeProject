.class Lcom/helpshift/support/search/storage/SearchTokenDaoImpl$LazyHolder;
.super Ljava/lang/Object;
.source "SearchTokenDaoImpl.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "LazyHolder"
.end annotation


# static fields
.field static final INSTANCE:Lcom/helpshift/support/search/SearchTokenDao;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 161
    new-instance v0, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;

    invoke-direct {v0}, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;-><init>()V

    sput-object v0, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl$LazyHolder;->INSTANCE:Lcom/helpshift/support/search/SearchTokenDao;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 160
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
