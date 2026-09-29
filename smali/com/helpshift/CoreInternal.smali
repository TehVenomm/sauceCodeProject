.class public Lcom/helpshift/CoreInternal;
.super Ljava/lang/Object;
.source "CoreInternal.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_CoreInternal"

.field static apiProvider:Lcom/helpshift/Core$ApiProvider;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 34
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static clearAnonymousUser()V
    .locals 2

    .line 206
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 209
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 210
    new-instance v1, Lcom/helpshift/CoreInternal$8;

    invoke-direct {v1}, Lcom/helpshift/CoreInternal$8;-><init>()V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static getActionExecutor()Lcom/helpshift/executors/ActionExecutor;
    .locals 1

    .line 276
    sget-object v0, Lcom/helpshift/CoreInternal;->apiProvider:Lcom/helpshift/Core$ApiProvider;

    invoke-interface {v0}, Lcom/helpshift/Core$ApiProvider;->_getActionExecutor()Lcom/helpshift/executors/ActionExecutor;

    move-result-object v0

    return-object v0
.end method

.method public static handlePush(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 2

    .line 136
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 139
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 140
    new-instance v1, Lcom/helpshift/CoreInternal$5;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/CoreInternal$5;-><init>(Landroid/content/Context;Landroid/content/Intent;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static handlePush(Landroid/content/Context;Landroid/os/Bundle;)V
    .locals 1

    .line 150
    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    .line 151
    invoke-virtual {v0, p1}, Landroid/content/Intent;->putExtras(Landroid/os/Bundle;)Landroid/content/Intent;

    .line 152
    invoke-static {p0, v0}, Lcom/helpshift/CoreInternal;->handlePush(Landroid/content/Context;Landroid/content/Intent;)V

    return-void
.end method

.method public static handlePush(Landroid/content/Context;Ljava/util/Map;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    if-eqz p1, :cond_2

    .line 157
    invoke-interface {p1}, Ljava/util/Map;->size()I

    move-result v0

    if-nez v0, :cond_0

    goto :goto_1

    .line 161
    :cond_0
    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    .line 162
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    .line 163
    invoke-interface {p1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    .line 164
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-virtual {v1, v3, v2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 166
    :cond_1
    invoke-virtual {v0, v1}, Landroid/content/Intent;->putExtras(Landroid/os/Bundle;)Landroid/content/Intent;

    .line 167
    invoke-static {p0, v0}, Lcom/helpshift/CoreInternal;->handlePush(Landroid/content/Context;Landroid/content/Intent;)V

    return-void

    :cond_2
    :goto_1
    return-void
.end method

.method public static init(Lcom/helpshift/Core$ApiProvider;)V
    .locals 0

    .line 41
    sput-object p0, Lcom/helpshift/CoreInternal;->apiProvider:Lcom/helpshift/Core$ApiProvider;

    return-void
.end method

.method static initLogging(Landroid/content/Context;Ljava/util/Map;)V
    .locals 5

    const-string v0, "enableLogging"

    .line 282
    invoke-interface {p1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    .line 283
    instance-of v1, v0, Ljava/lang/Boolean;

    const/4 v2, 0x0

    const/4 v3, 0x1

    if-eqz v1, :cond_0

    check-cast v0, Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    const-string v1, "disableErrorLogging"

    .line 286
    invoke-interface {p1, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    if-nez v1, :cond_1

    const-string v1, "disableErrorReporting"

    .line 288
    invoke-interface {p1, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    :cond_1
    if-nez v1, :cond_2

    .line 292
    invoke-static {v3}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    .line 294
    :cond_2
    instance-of p1, v1, Ljava/lang/Boolean;

    if-eqz p1, :cond_3

    check-cast v1, Ljava/lang/Boolean;

    .line 295
    invoke-virtual {v1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    if-eqz p1, :cond_3

    const/4 v2, 0x1

    .line 299
    :cond_3
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->getServerTimeDelta()F

    move-result p1

    .line 302
    invoke-static {p0}, Lcom/helpshift/util/ApplicationUtil;->isApplicationDebuggable(Landroid/content/Context;)Z

    move-result v1

    if-eqz v1, :cond_4

    const/4 v1, 0x2

    goto :goto_1

    :cond_4
    const/4 v1, 0x4

    :goto_1
    const-string v3, "__hs_log_store"

    const-string v4, "7.6.3"

    .line 304
    invoke-static {p0, v3, v4}, Lcom/helpshift/logger/LoggerProvider;->getLoggerInstance(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/ILogger;

    move-result-object v3

    invoke-static {v3, v1}, Lcom/helpshift/util/HSLogger;->initialize(Lcom/helpshift/logger/ILogger;I)V

    .line 306
    new-instance v1, Lcom/helpshift/logger/logmodels/LogExtrasModelFactory;

    invoke-direct {v1}, Lcom/helpshift/logger/logmodels/LogExtrasModelFactory;-><init>()V

    invoke-static {v1}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->initialize(Lcom/helpshift/logger/logmodels/ILogExtrasModelFactory;)V

    .line 307
    invoke-static {p1}, Lcom/helpshift/util/HSLogger;->updateTimeStampDelta(F)V

    xor-int/lit8 p1, v2, 0x1

    .line 310
    invoke-static {v0, p1}, Lcom/helpshift/util/HSLogger;->enableLogging(ZZ)V

    xor-int/lit8 p1, v2, 0x1

    .line 313
    invoke-static {p1}, Lcom/helpshift/static_classes/ErrorReporting;->shouldEnable(Z)V

    if-nez v2, :cond_5

    .line 317
    invoke-static {p0}, Lcom/helpshift/exceptions/handlers/UncaughtExceptionHandler;->init(Landroid/content/Context;)V

    .line 322
    :cond_5
    invoke-static {}, Lcom/helpshift/util/HSLogger;->getFatalLogsCount()I

    move-result p0

    if-nez p0, :cond_6

    .line 323
    invoke-static {}, Lcom/helpshift/util/HSLogger;->deleteAll()V

    :cond_6
    return-void
.end method

.method public static install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/exceptions/InstallException;
        }
    .end annotation

    .line 65
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, p1, p2, p3, v0}, Lcom/helpshift/CoreInternal;->install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public static install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .locals 9
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/exceptions/InstallException;
        }
    .end annotation

    .line 71
    invoke-static {}, Lcom/helpshift/CoreInternal;->verifyInit()V

    .line 72
    invoke-static {p1}, Lcom/helpshift/util/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    .line 73
    :cond_0
    invoke-static {p2}, Lcom/helpshift/util/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_1

    invoke-virtual {p2}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v0

    move-object v6, v0

    goto :goto_0

    :cond_1
    move-object v6, p2

    .line 74
    :goto_0
    invoke-static {p3}, Lcom/helpshift/util/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_2

    invoke-virtual {p3}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p3

    .line 76
    :cond_2
    invoke-static {p1, v6, p3}, Lcom/helpshift/util/SchemaUtil;->validateInstallCredentials(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    .line 77
    sget-object v0, Lcom/helpshift/util/HelpshiftContext;->installAPICalled:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    .line 78
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v7

    .line 79
    new-instance v8, Lcom/helpshift/CoreInternal$2;

    move-object v0, v8

    move-object v1, p0

    move-object v2, p1

    move-object v3, v6

    move-object v4, p3

    move-object v5, p4

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/CoreInternal$2;-><init>(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    invoke-interface {v7, v8}, Lcom/helpshift/util/concurrent/ApiExecutor;->runSync(Ljava/lang/Runnable;)V

    .line 90
    new-instance v8, Lcom/helpshift/CoreInternal$3;

    move-object v0, v8

    move-object v2, p4

    move-object v3, p2

    move-object v4, p1

    move-object v5, v6

    move-object v6, p3

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/CoreInternal$3;-><init>(Landroid/app/Application;Ljava/util/Map;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v7, v8}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static login(Lcom/helpshift/HelpshiftUser;)V
    .locals 2

    .line 171
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 174
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 175
    new-instance v1, Lcom/helpshift/CoreInternal$6;

    invoke-direct {v1, p0}, Lcom/helpshift/CoreInternal$6;-><init>(Lcom/helpshift/HelpshiftUser;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static logout()V
    .locals 2

    .line 192
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 195
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 196
    new-instance v1, Lcom/helpshift/CoreInternal$7;

    invoke-direct {v1}, Lcom/helpshift/CoreInternal$7;-><init>()V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static onAppBackground()V
    .locals 2

    .line 262
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 265
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 266
    new-instance v1, Lcom/helpshift/CoreInternal$12;

    invoke-direct {v1}, Lcom/helpshift/CoreInternal$12;-><init>()V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static onAppForeground()V
    .locals 2

    .line 248
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 251
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 252
    new-instance v1, Lcom/helpshift/CoreInternal$11;

    invoke-direct {v1}, Lcom/helpshift/CoreInternal$11;-><init>()V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static registerDeviceToken(Landroid/content/Context;Ljava/lang/String;)V
    .locals 2

    .line 122
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 125
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 126
    new-instance v1, Lcom/helpshift/CoreInternal$4;

    invoke-direct {v1, p1, p0}, Lcom/helpshift/CoreInternal$4;-><init>(Ljava/lang/String;Landroid/content/Context;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static setNameAndEmail(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 51
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 54
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 55
    new-instance v1, Lcom/helpshift/CoreInternal$1;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/CoreInternal$1;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static setSDKLanguage(Ljava/lang/String;)V
    .locals 2

    .line 220
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 223
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 224
    new-instance v1, Lcom/helpshift/CoreInternal$9;

    invoke-direct {v1, p0}, Lcom/helpshift/CoreInternal$9;-><init>(Ljava/lang/String;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static setTheme(I)V
    .locals 2

    .line 234
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->verifyInstall()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 237
    :cond_0
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 238
    new-instance v1, Lcom/helpshift/CoreInternal$10;

    invoke-direct {v1, p0}, Lcom/helpshift/CoreInternal$10;-><init>(I)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method protected static verifyInit()V
    .locals 2

    .line 45
    sget-object v0, Lcom/helpshift/CoreInternal;->apiProvider:Lcom/helpshift/Core$ApiProvider;

    if-eqz v0, :cond_0

    return-void

    .line 46
    :cond_0
    new-instance v0, Lcom/helpshift/exceptions/HelpshiftInitializationException;

    const-string v1, "com.helpshift.Core.init() method not called with valid arguments"

    invoke-direct {v0, v1}, Lcom/helpshift/exceptions/HelpshiftInitializationException;-><init>(Ljava/lang/String;)V

    throw v0
.end method
