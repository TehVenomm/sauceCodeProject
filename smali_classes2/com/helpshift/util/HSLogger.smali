.class public Lcom/helpshift/util/HSLogger;
.super Ljava/lang/Object;
.source "HSLogger.java"


# static fields
.field public static final LOG_STORE_DB_NAME:Ljava/lang/String; = "__hs_log_store"

.field public static final OLD_ERROR_REPORTING_DATABASE_NAME:Ljava/lang/String; = "__hs__db_error_reports"

.field private static logger:Lcom/helpshift/logger/ILogger;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 12
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static d(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x0

    .line 61
    invoke-static {p0, p1, v0, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public static d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    .locals 1

    const/4 v0, 0x0

    .line 73
    invoke-static {p0, p1, p2, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public static varargs d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 2

    const/4 v0, 0x1

    .line 89
    new-array v0, v0, [Ljava/lang/Throwable;

    const/4 v1, 0x0

    aput-object p2, v0, v1

    const/4 p2, 0x2

    invoke-static {p2, p0, p1, v0, p3}, Lcom/helpshift/util/HSLogger;->logMessage(ILjava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public static varargs d(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 1

    const/4 v0, 0x0

    .line 85
    invoke-static {p0, p1, v0, p2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public static deleteAll()V
    .locals 1

    .line 164
    sget-object v0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    if-nez v0, :cond_0

    return-void

    .line 168
    :cond_0
    sget-object v0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    invoke-interface {v0}, Lcom/helpshift/logger/ILogger;->deleteAllCachedLogs()V

    return-void
.end method

.method public static e(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    const/4 v0, 0x0

    .line 69
    move-object v1, v0

    check-cast v1, [Ljava/lang/Throwable;

    invoke-static {p0, p1, v1, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public static e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    .locals 2

    const/4 v0, 0x1

    .line 81
    new-array v0, v0, [Ljava/lang/Throwable;

    const/4 v1, 0x0

    aput-object p2, v0, v1

    const/4 p2, 0x0

    invoke-static {p0, p1, v0, p2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public static varargs e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 2

    const/4 v0, 0x1

    .line 97
    new-array v0, v0, [Ljava/lang/Throwable;

    const/4 v1, 0x0

    aput-object p2, v0, v1

    const/16 p2, 0x8

    invoke-static {p2, p0, p1, v0, p3}, Lcom/helpshift/util/HSLogger;->logMessage(ILjava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public static varargs e(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 1

    const/16 v0, 0x8

    .line 101
    invoke-static {v0, p0, p1, p2, p3}, Lcom/helpshift/util/HSLogger;->logMessage(ILjava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public static enableLogging(ZZ)V
    .locals 1

    .line 55
    sget-object v0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    if-eqz v0, :cond_0

    .line 56
    sget-object v0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    invoke-interface {v0, p0, p1}, Lcom/helpshift/logger/ILogger;->enableLogging(ZZ)V

    :cond_0
    return-void
.end method

.method public static varargs f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 2

    const/4 v0, 0x1

    .line 105
    new-array v0, v0, [Ljava/lang/Throwable;

    const/4 v1, 0x0

    aput-object p2, v0, v1

    const/16 p2, 0x10

    invoke-static {p2, p0, p1, v0, p3}, Lcom/helpshift/util/HSLogger;->logMessage(ILjava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public static varargs f(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 1

    const/16 v0, 0x10

    .line 109
    invoke-static {v0, p0, p1, p2, p3}, Lcom/helpshift/util/HSLogger;->logMessage(ILjava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public static getAll()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/logger/model/LogModel;",
            ">;"
        }
    .end annotation

    .line 153
    sget-object v0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 157
    :cond_0
    sget-object v0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    invoke-interface {v0}, Lcom/helpshift/logger/ILogger;->getAll()Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method public static getFatalLogsCount()I
    .locals 2

    .line 178
    sget-object v0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    .line 182
    :cond_0
    sget-object v0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    const/16 v1, 0x10

    invoke-interface {v0, v1}, Lcom/helpshift/logger/ILogger;->getCount(I)I

    move-result v0

    return v0
.end method

.method public static initialize(Lcom/helpshift/logger/ILogger;I)V
    .locals 0

    .line 34
    sput-object p0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    .line 35
    sget-object p0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    invoke-interface {p0, p1}, Lcom/helpshift/logger/ILogger;->setLoggingLevel(I)V

    return-void
.end method

.method private static varargs logMessage(ILjava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 1

    .line 122
    sget-object v0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    if-nez v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x2

    if-eq p0, v0, :cond_4

    const/4 v0, 0x4

    if-eq p0, v0, :cond_3

    const/16 v0, 0x8

    if-eq p0, v0, :cond_2

    const/16 v0, 0x10

    if-eq p0, v0, :cond_1

    goto :goto_0

    .line 140
    :cond_1
    sget-object p0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    invoke-interface {p0, p1, p2, p3, p4}, Lcom/helpshift/logger/ILogger;->f(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    goto :goto_0

    .line 128
    :cond_2
    sget-object p0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    invoke-interface {p0, p1, p2, p3, p4}, Lcom/helpshift/logger/ILogger;->e(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    goto :goto_0

    .line 132
    :cond_3
    sget-object p0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    invoke-interface {p0, p1, p2, p3, p4}, Lcom/helpshift/logger/ILogger;->w(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    goto :goto_0

    .line 136
    :cond_4
    sget-object p0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    invoke-interface {p0, p1, p2, p3, p4}, Lcom/helpshift/logger/ILogger;->d(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    :goto_0
    return-void
.end method

.method public static updateTimeStampDelta(F)V
    .locals 5

    .line 45
    sget-object v0, Lcom/helpshift/util/HSLogger;->logger:Lcom/helpshift/logger/ILogger;

    float-to-long v1, p0

    const-wide/16 v3, 0x3e8

    mul-long v1, v1, v3

    invoke-interface {v0, v1, v2}, Lcom/helpshift/logger/ILogger;->setTimestampDelta(J)V

    return-void
.end method

.method public static w(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x0

    .line 65
    invoke-static {p0, p1, v0, v0}, Lcom/helpshift/util/HSLogger;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public static w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    .locals 1

    const/4 v0, 0x0

    .line 77
    invoke-static {p0, p1, p2, v0}, Lcom/helpshift/util/HSLogger;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method

.method public static varargs w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    .locals 2

    const/4 v0, 0x1

    .line 93
    new-array v0, v0, [Ljava/lang/Throwable;

    const/4 v1, 0x0

    aput-object p2, v0, v1

    const/4 p2, 0x4

    invoke-static {p2, p0, p1, v0, p3}, Lcom/helpshift/util/HSLogger;->logMessage(ILjava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    return-void
.end method
