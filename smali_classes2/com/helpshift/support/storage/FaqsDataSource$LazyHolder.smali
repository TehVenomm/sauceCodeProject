.class final Lcom/helpshift/support/storage/FaqsDataSource$LazyHolder;
.super Ljava/lang/Object;
.source "FaqsDataSource.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/storage/FaqsDataSource;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1a
    name = "LazyHolder"
.end annotation


# static fields
.field static final INSTANCE:Lcom/helpshift/support/storage/FaqsDataSource;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 368
    new-instance v0, Lcom/helpshift/support/storage/FaqsDataSource;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Lcom/helpshift/support/storage/FaqsDataSource;-><init>(Lcom/helpshift/support/storage/FaqsDataSource$1;)V

    sput-object v0, Lcom/helpshift/support/storage/FaqsDataSource$LazyHolder;->INSTANCE:Lcom/helpshift/support/storage/FaqsDataSource;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 367
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
