.class Lcom/helpshift/logger/Logger;
.super Ljava/lang/Object;
.source "Logger.java"

# interfaces
.implements Lcom/helpshift/logger/ILogger;


# static fields
.field private static final ERROR:Ljava/lang/String; = "ERROR"

.field private static final FATAL:Ljava/lang/String; = "FATAL"

.field static final MAX_EXTRAS_COUNT:I = 0x14

.field static final MAX_LOG_SIZE:I = 0x1388

.field private static final WARN:Ljava/lang/String; = "WARN"


# instance fields
.field private final TAG:Ljava/lang/String;

.field private enableConsoleLogging:Z

.field private enableLogCaching:Z

.field private logStorage:Lcom/helpshift/logger/database/LogStorage;

.field private loggingLevel:I

.field private final sdkVersion:Ljava/lang/String;

.field private final simpleDateFormat:Ljava/text/SimpleDateFormat;

.field private threadPoolExecutor:Ljava/util/concurrent/ThreadPoolExecutor;

.field private timestampDelta:J


# direct methods
.method constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V
    .locals 3

    .line 100
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x4

    .line 46
    iput v0, p0, Lcom/helpshift/logger/Logger;->loggingLevel:I

    .line 51
    const-class v0, Lcom/helpshift/logger/Logger;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/logger/Logger;->TAG:Ljava/lang/String;

    .line 89
    new-instance v0, Ljava/text/SimpleDateFormat;

    const-string v1, "dd/MM/yyyy HH:mm:ss"

    sget-object v2, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct {v0, v1, v2}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    iput-object v0, p0, Lcom/helpshift/logger/Logger;->simpleDateFormat:Ljava/text/SimpleDateFormat;

    .line 101
    new-instance v0, Lcom/helpshift/logger/database/LogSQLiteStorage;

    invoke-direct {v0, p1, p2}, Lcom/helpshift/logger/database/LogSQLiteStorage;-><init>(Landroid/content/Context;Ljava/lang/String;)V

    iput-object v0, p0, Lcom/helpshift/logger/Logger;->logStorage:Lcom/helpshift/logger/database/LogStorage;

    .line 102
    iput-object p3, p0, Lcom/helpshift/logger/Logger;->sdkVersion:Ljava/lang/String;

    return-void
.end method

.method private containsUnknownHostException([Ljava/lang/Throwable;)Z
    .locals 3

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    :cond_0
    const/4 v1, 0x0

    .line 332
    :goto_0
    array-length v2, p1

    if-ge v1, v2, :cond_2

    .line 333
    aget-object v2, p1, v1

    instance-of v2, v2, Ljava/net/UnknownHostException;

    if-eqz v2, :cond_1

    const/4 v0, 0x1

    goto :goto_1

    :cond_1
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_2
    :goto_1
    return v0
.end method

.method private convertMaskToLogLevel(I)Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 391
    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    and-int/lit8 v1, p1, 0x8

    if-eqz v1, :cond_1

    const-string v1, "ERROR"

    .line 393
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_1
    and-int/lit8 v1, p1, 0x4

    if-eqz v1, :cond_2

    const-string v1, "WARN"

    .line 397
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_2
    and-int/lit8 p1, p1, 0x10

    if-eqz p1, :cond_3

    const-string p1, "FATAL"

    .line 401
    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_3
    return-object v0
.end method

.method private getExtrasForConsoleLogging([Lcom/helpshift/logger/logmodels/ILogExtrasModel;)Ljava/lang/String;
    .locals 4

    const-string v0, " "

    if-eqz p1, :cond_2

    .line 350
    array-length v1, p1

    if-lez v1, :cond_2

    .line 351
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, " "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    .line 352
    array-length v1, p1

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    aget-object v3, p1, v2

    if-eqz v3, :cond_0

    .line 354
    invoke-interface {v3}, Lcom/helpshift/logger/logmodels/ILogExtrasModel;->getConsoleLoggingMessage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, " "

    .line 355
    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 358
    :cond_1
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    :cond_2
    return-object v0
.end method

.method private getStackTraceString([Ljava/lang/Throwable;)Ljava/lang/String;
    .locals 3

    const-string v0, ""

    if-eqz p1, :cond_2

    .line 367
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    .line 368
    invoke-direct {p0, p1}, Lcom/helpshift/logger/Logger;->containsUnknownHostException([Ljava/lang/Throwable;)Z

    move-result v1

    if-eqz v1, :cond_0

    const-string v0, "UnknownHostException"

    goto :goto_1

    :cond_0
    const/4 v1, 0x0

    .line 375
    :goto_0
    array-length v2, p1

    if-ge v1, v2, :cond_1

    .line 376
    aget-object v2, p1, v1

    invoke-static {v2}, Landroid/util/Log;->getStackTraceString(Ljava/lang/Throwable;)Ljava/lang/String;

    move-result-object v2

    .line 377
    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 379
    :cond_1
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    :cond_2
    :goto_1
    return-object v0
.end method

.method private isConsoleLoggingEnabled()Z
    .locals 1

    .line 289
    iget-boolean v0, p0, Lcom/helpshift/logger/Logger;->enableConsoleLogging:Z

    return v0
.end method

.method private isLogCachingEnabled()Z
    .locals 1

    .line 293
    iget-boolean v0, p0, Lcom/helpshift/logger/Logger;->enableLogCaching:Z

    return v0
.end method

.method private logMessageToDatabase(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)Ljava/util/concurrent/Future;
    .locals 3

    .line 304
    new-instance v0, Lcom/helpshift/logger/LogMessage;

    invoke-direct {v0}, Lcom/helpshift/logger/LogMessage;-><init>()V

    .line 305
    iput-object p1, v0, Lcom/helpshift/logger/LogMessage;->level:Ljava/lang/String;

    .line 306
    iput-object p4, v0, Lcom/helpshift/logger/LogMessage;->extras:[Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    .line 307
    iput-object p2, v0, Lcom/helpshift/logger/LogMessage;->message:Ljava/lang/String;

    .line 308
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide p1

    iget-wide v1, p0, Lcom/helpshift/logger/Logger;->timestampDelta:J

    add-long/2addr p1, v1

    iput-wide p1, v0, Lcom/helpshift/logger/LogMessage;->timeStamp:J

    .line 309
    iput-object p3, v0, Lcom/helpshift/logger/LogMessage;->stacktrace:Ljava/lang/String;

    .line 310
    iget-object p1, p0, Lcom/helpshift/logger/Logger;->sdkVersion:Ljava/lang/String;

    iput-object p1, v0, Lcom/helpshift/logger/LogMessage;->sdkVersion:Ljava/lang/String;

    .line 315
    :try_start_0
    iget-object p1, p0, Lcom/helpshift/logger/Logger;->threadPoolExecutor:Ljava/util/concurrent/ThreadPoolExecutor;

    new-instance p2, Lcom/helpshift/logger/WorkerThread;

    iget-object p3, p0, Lcom/helpshift/logger/Logger;->logStorage:Lcom/helpshift/logger/database/LogStorage;

    iget-object p4, p0, Lcom/helpshift/logger/Logger;->simpleDateFormat:Ljava/text/SimpleDateFormat;

    invoke-direct {p2, v0, p3, p4}, Lcom/helpshift/logger/WorkerThread;-><init>(Lcom/helpshift/logger/LogMessage;Lcom/helpshift/logger/database/LogStorage;Ljava/text/SimpleDateFormat;)V

    invoke-virtual {p1, p2}, Ljava/util/concurrent/ThreadPoolExecutor;->submit(Ljava/lang/Runnable;)Ljava/util/concurrent/Future;

    move-result-object p1
    :try_end_0
    .catch Ljava/util/concurrent/RejectedExecutionException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 321
    iget-object p2, p0, Lcom/helpshift/logger/Logger;->TAG:Ljava/lang/String;

    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    const-string p4, "Rejected execution of log message : "

    invoke-virtual {p3, p4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object p4, v0, Lcom/helpshift/logger/LogMessage;->message:Ljava/lang/String;

    invoke-virtual {p3, p4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p3

    invoke-static {p2, p3, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    const/4 p1, 0x0

    return-object p1
.end method


# virtual methods
.method public d(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x0

    .line 151
    invoke-virtual {p0, p1, p2, v0, v0}, Lcom/helpshift/logger/Logger;->d(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public varargs d(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 1

    const/4 v0, 0x0

    .line 186
    invoke-virtual {p0, p1, p2, v0, p3}, Lcom/helpshift/logger/Logger;->d(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public d(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;)V
    .locals 1

    const/4 v0, 0x0

    .line 166
    invoke-virtual {p0, p1, p2, p3, v0}, Lcom/helpshift/logger/Logger;->d(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public varargs d(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 2

    .line 201
    invoke-direct {p0}, Lcom/helpshift/logger/Logger;->isConsoleLoggingEnabled()Z

    move-result v0

    if-eqz v0, :cond_0

    iget v0, p0, Lcom/helpshift/logger/Logger;->loggingLevel:I

    const/4 v1, 0x2

    if-gt v0, v1, :cond_0

    .line 202
    invoke-direct {p0, p3}, Lcom/helpshift/logger/Logger;->getStackTraceString([Ljava/lang/Throwable;)Ljava/lang/String;

    move-result-object p3

    .line 203
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-direct {p0, p4}, Lcom/helpshift/logger/Logger;->getExtrasForConsoleLogging([Lcom/helpshift/logger/logmodels/ILogExtrasModel;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :cond_0
    return-void
.end method

.method public deleteAllCachedLogs()V
    .locals 1

    .line 280
    iget-object v0, p0, Lcom/helpshift/logger/Logger;->logStorage:Lcom/helpshift/logger/database/LogStorage;

    invoke-interface {v0}, Lcom/helpshift/logger/database/LogStorage;->deleteAll()V

    return-void
.end method

.method public e(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x0

    .line 161
    invoke-virtual {p0, p1, p2, v0, v0}, Lcom/helpshift/logger/Logger;->e(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public varargs e(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 1

    const/4 v0, 0x0

    .line 196
    invoke-virtual {p0, p1, p2, v0, p3}, Lcom/helpshift/logger/Logger;->e(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public e(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;)V
    .locals 1

    const/4 v0, 0x0

    .line 176
    invoke-virtual {p0, p1, p2, p3, v0}, Lcom/helpshift/logger/Logger;->e(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public varargs e(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 3

    .line 228
    invoke-direct {p0}, Lcom/helpshift/logger/Logger;->isConsoleLoggingEnabled()Z

    move-result v0

    if-eqz v0, :cond_0

    iget v0, p0, Lcom/helpshift/logger/Logger;->loggingLevel:I

    const/16 v1, 0x8

    if-gt v0, v1, :cond_0

    .line 229
    invoke-direct {p0, p3}, Lcom/helpshift/logger/Logger;->getStackTraceString([Ljava/lang/Throwable;)Ljava/lang/String;

    move-result-object v0

    .line 230
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-direct {p0, p4}, Lcom/helpshift/logger/Logger;->getExtrasForConsoleLogging([Lcom/helpshift/logger/logmodels/ILogExtrasModel;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {p1, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 237
    :goto_0
    invoke-direct {p0}, Lcom/helpshift/logger/Logger;->isLogCachingEnabled()Z

    move-result p1

    if-eqz p1, :cond_2

    invoke-direct {p0, p3}, Lcom/helpshift/logger/Logger;->containsUnknownHostException([Ljava/lang/Throwable;)Z

    move-result p1

    if-nez p1, :cond_2

    if-nez v0, :cond_1

    .line 239
    invoke-direct {p0, p3}, Lcom/helpshift/logger/Logger;->getStackTraceString([Ljava/lang/Throwable;)Ljava/lang/String;

    move-result-object v0

    :cond_1
    const-string p1, "ERROR"

    .line 241
    invoke-direct {p0, p1, p2, v0, p4}, Lcom/helpshift/logger/Logger;->logMessageToDatabase(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)Ljava/util/concurrent/Future;

    :cond_2
    return-void
.end method

.method public enableLogging(ZZ)V
    .locals 8

    .line 108
    iput-boolean p1, p0, Lcom/helpshift/logger/Logger;->enableConsoleLogging:Z

    .line 110
    iget-boolean p1, p0, Lcom/helpshift/logger/Logger;->enableLogCaching:Z

    if-ne p1, p2, :cond_0

    return-void

    .line 114
    :cond_0
    iput-boolean p2, p0, Lcom/helpshift/logger/Logger;->enableLogCaching:Z

    .line 115
    iget-boolean p1, p0, Lcom/helpshift/logger/Logger;->enableLogCaching:Z

    if-eqz p1, :cond_1

    .line 118
    new-instance v6, Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-direct {v6}, Ljava/util/concurrent/LinkedBlockingQueue;-><init>()V

    .line 123
    new-instance p1, Ljava/util/concurrent/ThreadPoolExecutor;

    const/4 v1, 0x1

    const/4 v2, 0x1

    const-wide/16 v3, 0x3c

    sget-object v5, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    new-instance v7, Lcom/helpshift/logger/Logger$1;

    invoke-direct {v7, p0}, Lcom/helpshift/logger/Logger$1;-><init>(Lcom/helpshift/logger/Logger;)V

    move-object v0, p1

    invoke-direct/range {v0 .. v7}, Ljava/util/concurrent/ThreadPoolExecutor;-><init>(IIJLjava/util/concurrent/TimeUnit;Ljava/util/concurrent/BlockingQueue;Ljava/util/concurrent/ThreadFactory;)V

    iput-object p1, p0, Lcom/helpshift/logger/Logger;->threadPoolExecutor:Ljava/util/concurrent/ThreadPoolExecutor;

    goto :goto_0

    .line 133
    :cond_1
    iget-object p1, p0, Lcom/helpshift/logger/Logger;->threadPoolExecutor:Ljava/util/concurrent/ThreadPoolExecutor;

    if-eqz p1, :cond_2

    .line 134
    iget-object p1, p0, Lcom/helpshift/logger/Logger;->threadPoolExecutor:Ljava/util/concurrent/ThreadPoolExecutor;

    invoke-virtual {p1}, Ljava/util/concurrent/ThreadPoolExecutor;->shutdown()V

    :cond_2
    :goto_0
    return-void
.end method

.method public f(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;)V
    .locals 1

    const/4 v0, 0x0

    .line 181
    invoke-virtual {p0, p1, p2, p3, v0}, Lcom/helpshift/logger/Logger;->f(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public varargs f(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 3

    .line 250
    invoke-direct {p0}, Lcom/helpshift/logger/Logger;->isConsoleLoggingEnabled()Z

    move-result v0

    if-eqz v0, :cond_0

    iget v0, p0, Lcom/helpshift/logger/Logger;->loggingLevel:I

    const/16 v1, 0x10

    if-gt v0, v1, :cond_0

    .line 251
    invoke-direct {p0, p3}, Lcom/helpshift/logger/Logger;->getStackTraceString([Ljava/lang/Throwable;)Ljava/lang/String;

    move-result-object v0

    .line 252
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-direct {p0, p4}, Lcom/helpshift/logger/Logger;->getExtrasForConsoleLogging([Lcom/helpshift/logger/logmodels/ILogExtrasModel;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {p1, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 255
    :goto_0
    invoke-direct {p0}, Lcom/helpshift/logger/Logger;->isLogCachingEnabled()Z

    move-result p1

    if-eqz p1, :cond_2

    if-nez v0, :cond_1

    .line 257
    invoke-direct {p0, p3}, Lcom/helpshift/logger/Logger;->getStackTraceString([Ljava/lang/Throwable;)Ljava/lang/String;

    move-result-object v0

    :cond_1
    const-string p1, "FATAL"

    .line 260
    invoke-direct {p0, p1, p2, v0, p4}, Lcom/helpshift/logger/Logger;->logMessageToDatabase(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)Ljava/util/concurrent/Future;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 264
    :try_start_0
    invoke-interface {p1}, Ljava/util/concurrent/Future;->get()Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception p1

    .line 268
    iget-object p2, p0, Lcom/helpshift/logger/Logger;->TAG:Ljava/lang/String;

    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    const-string p4, "Error logging fatal log : "

    invoke-virtual {p3, p4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :cond_2
    :goto_1
    return-void
.end method

.method public getAll()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/logger/model/LogModel;",
            ">;"
        }
    .end annotation

    .line 275
    iget-object v0, p0, Lcom/helpshift/logger/Logger;->logStorage:Lcom/helpshift/logger/database/LogStorage;

    invoke-interface {v0}, Lcom/helpshift/logger/database/LogStorage;->getAll()Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method public getCount(I)I
    .locals 1

    .line 285
    iget-object v0, p0, Lcom/helpshift/logger/Logger;->logStorage:Lcom/helpshift/logger/database/LogStorage;

    invoke-direct {p0, p1}, Lcom/helpshift/logger/Logger;->convertMaskToLogLevel(I)Ljava/util/List;

    move-result-object p1

    invoke-interface {v0, p1}, Lcom/helpshift/logger/database/LogStorage;->getCount(Ljava/util/List;)I

    move-result p1

    return p1
.end method

.method public setLoggingLevel(I)V
    .locals 0

    .line 141
    iput p1, p0, Lcom/helpshift/logger/Logger;->loggingLevel:I

    return-void
.end method

.method public setTimestampDelta(J)V
    .locals 0

    .line 146
    iput-wide p1, p0, Lcom/helpshift/logger/Logger;->timestampDelta:J

    return-void
.end method

.method public w(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x0

    .line 156
    invoke-virtual {p0, p1, p2, v0, v0}, Lcom/helpshift/logger/Logger;->w(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public varargs w(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 1

    const/4 v0, 0x0

    .line 191
    invoke-virtual {p0, p1, p2, v0, p3}, Lcom/helpshift/logger/Logger;->w(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public w(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;)V
    .locals 1

    const/4 v0, 0x0

    .line 171
    invoke-virtual {p0, p1, p2, p3, v0}, Lcom/helpshift/logger/Logger;->w(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public varargs w(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 3

    .line 211
    invoke-direct {p0}, Lcom/helpshift/logger/Logger;->isConsoleLoggingEnabled()Z

    move-result v0

    if-eqz v0, :cond_0

    iget v0, p0, Lcom/helpshift/logger/Logger;->loggingLevel:I

    const/4 v1, 0x4

    if-gt v0, v1, :cond_0

    .line 212
    invoke-direct {p0, p3}, Lcom/helpshift/logger/Logger;->getStackTraceString([Ljava/lang/Throwable;)Ljava/lang/String;

    move-result-object v0

    .line 213
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-direct {p0, p4}, Lcom/helpshift/logger/Logger;->getExtrasForConsoleLogging([Lcom/helpshift/logger/logmodels/ILogExtrasModel;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {p1, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 216
    :goto_0
    invoke-direct {p0}, Lcom/helpshift/logger/Logger;->isLogCachingEnabled()Z

    move-result p1

    if-eqz p1, :cond_2

    if-nez v0, :cond_1

    .line 218
    invoke-direct {p0, p3}, Lcom/helpshift/logger/Logger;->getStackTraceString([Ljava/lang/Throwable;)Ljava/lang/String;

    move-result-object v0

    :cond_1
    const-string p1, "WARN"

    .line 220
    invoke-direct {p0, p1, p2, v0, p4}, Lcom/helpshift/logger/Logger;->logMessageToDatabase(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)Ljava/util/concurrent/Future;

    :cond_2
    return-void
.end method
