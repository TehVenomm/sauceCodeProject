.class Lcom/helpshift/support/storage/IMAppSessionStorage$LazyHolder;
.super Ljava/lang/Object;
.source "IMAppSessionStorage.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/storage/IMAppSessionStorage;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "LazyHolder"
.end annotation


# static fields
.field static final INSTANCE:Lcom/helpshift/support/storage/IMAppSessionStorage;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 107
    new-instance v0, Lcom/helpshift/support/storage/IMAppSessionStorage;

    invoke-direct {v0}, Lcom/helpshift/support/storage/IMAppSessionStorage;-><init>()V

    sput-object v0, Lcom/helpshift/support/storage/IMAppSessionStorage$LazyHolder;->INSTANCE:Lcom/helpshift/support/storage/IMAppSessionStorage;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 106
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
