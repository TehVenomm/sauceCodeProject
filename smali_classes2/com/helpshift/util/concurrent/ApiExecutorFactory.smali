.class public Lcom/helpshift/util/concurrent/ApiExecutorFactory;
.super Ljava/lang/Object;
.source "ApiExecutorFactory.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/util/concurrent/ApiExecutorFactory$LazyHolder;
    }
.end annotation


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 3
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;
    .locals 1

    .line 5
    sget-object v0, Lcom/helpshift/util/concurrent/ApiExecutorFactory$LazyHolder;->HANDLER_EXECUTOR:Lcom/helpshift/util/concurrent/ApiExecutor;

    return-object v0
.end method
