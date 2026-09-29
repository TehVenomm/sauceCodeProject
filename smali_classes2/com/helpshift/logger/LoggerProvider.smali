.class public Lcom/helpshift/logger/LoggerProvider;
.super Ljava/lang/Object;
.source "LoggerProvider.java"


# static fields
.field private static hsLoggerInstances:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Lcom/helpshift/logger/ILogger;",
            ">;"
        }
    .end annotation
.end field

.field private static final loggerInstanceLock:Ljava/lang/Object;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 15
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lcom/helpshift/logger/LoggerProvider;->hsLoggerInstances:Ljava/util/HashMap;

    .line 20
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/helpshift/logger/LoggerProvider;->loggerInstanceLock:Ljava/lang/Object;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 10
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getLoggerInstance(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/ILogger;
    .locals 2

    if-nez p0, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 39
    :cond_0
    sget-object v0, Lcom/helpshift/logger/LoggerProvider;->loggerInstanceLock:Ljava/lang/Object;

    monitor-enter v0

    .line 41
    :try_start_0
    sget-object v1, Lcom/helpshift/logger/LoggerProvider;->hsLoggerInstances:Ljava/util/HashMap;

    invoke-virtual {v1, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/logger/ILogger;

    if-nez v1, :cond_1

    .line 45
    new-instance v1, Lcom/helpshift/logger/Logger;

    invoke-direct {v1, p0, p1, p2}, Lcom/helpshift/logger/Logger;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    .line 48
    sget-object p0, Lcom/helpshift/logger/LoggerProvider;->hsLoggerInstances:Ljava/util/HashMap;

    invoke-virtual {p0, p1, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 51
    :cond_1
    monitor-exit v0

    return-object v1

    :catchall_0
    move-exception p0

    .line 52
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p0
.end method
