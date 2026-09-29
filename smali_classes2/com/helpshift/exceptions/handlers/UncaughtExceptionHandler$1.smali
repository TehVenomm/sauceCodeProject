.class final Lcom/helpshift/exceptions/handlers/UncaughtExceptionHandler$1;
.super Ljava/lang/Object;
.source "UncaughtExceptionHandler.java"

# interfaces
.implements Ljava/lang/Thread$UncaughtExceptionHandler;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/exceptions/handlers/UncaughtExceptionHandler;->init(Landroid/content/Context;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$context:Landroid/content/Context;

.field final synthetic val$defaultUncaughtExceptionHandler:Ljava/lang/Thread$UncaughtExceptionHandler;


# direct methods
.method constructor <init>(Landroid/content/Context;Ljava/lang/Thread$UncaughtExceptionHandler;)V
    .locals 0

    .line 30
    iput-object p1, p0, Lcom/helpshift/exceptions/handlers/UncaughtExceptionHandler$1;->val$context:Landroid/content/Context;

    iput-object p2, p0, Lcom/helpshift/exceptions/handlers/UncaughtExceptionHandler$1;->val$defaultUncaughtExceptionHandler:Ljava/lang/Thread$UncaughtExceptionHandler;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public uncaughtException(Ljava/lang/Thread;Ljava/lang/Throwable;)V
    .locals 3

    .line 35
    invoke-static {p2}, Lcom/helpshift/exceptions/handlers/UncaughtExceptionHandler;->isCausedByHelpshift(Ljava/lang/Throwable;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 36
    iget-object v0, p0, Lcom/helpshift/exceptions/handlers/UncaughtExceptionHandler$1;->val$context:Landroid/content/Context;

    .line 37
    invoke-static {v0, p1}, Lcom/helpshift/util/ErrorReportProvider;->getErrorReportExtras(Landroid/content/Context;Ljava/lang/Thread;)Ljava/util/List;

    move-result-object v0

    const/4 v1, 0x0

    new-array v1, v1, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-interface {v0, v1}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v1, "UncaughtExceptionHandler"

    const-string v2, "UNCAUGHT EXCEPTION "

    .line 39
    invoke-static {v1, v2, p2, v0}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 43
    :cond_0
    iget-object v0, p0, Lcom/helpshift/exceptions/handlers/UncaughtExceptionHandler$1;->val$defaultUncaughtExceptionHandler:Ljava/lang/Thread$UncaughtExceptionHandler;

    if-eqz v0, :cond_1

    .line 44
    iget-object v0, p0, Lcom/helpshift/exceptions/handlers/UncaughtExceptionHandler$1;->val$defaultUncaughtExceptionHandler:Ljava/lang/Thread$UncaughtExceptionHandler;

    invoke-interface {v0, p1, p2}, Ljava/lang/Thread$UncaughtExceptionHandler;->uncaughtException(Ljava/lang/Thread;Ljava/lang/Throwable;)V

    :cond_1
    return-void
.end method
