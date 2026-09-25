.class Lcom/helpshift/util/concurrent/ApiExecutorFactory$LazyHolder;
.super Ljava/lang/Object;
.source "ApiExecutorFactory.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/util/concurrent/ApiExecutorFactory;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "LazyHolder"
.end annotation


# static fields
.field static final HANDLER_EXECUTOR:Lcom/helpshift/util/concurrent/ApiExecutor;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 9
    new-instance v0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor;

    const-string v1, "HS-cm-api-exec"

    invoke-direct {v0, v1}, Lcom/helpshift/util/concurrent/HandlerThreadExecutor;-><init>(Ljava/lang/String;)V

    sput-object v0, Lcom/helpshift/util/concurrent/ApiExecutorFactory$LazyHolder;->HANDLER_EXECUTOR:Lcom/helpshift/util/concurrent/ApiExecutor;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
