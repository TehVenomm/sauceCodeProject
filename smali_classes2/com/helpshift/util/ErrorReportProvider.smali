.class public Lcom/helpshift/util/ErrorReportProvider;
.super Ljava/lang/Object;
.source "ErrorReportProvider.java"


# static fields
.field public static final BATCH_TIME:J = 0x5265c00L

.field public static final KEY_ACTIVE_CONVERSATION_ID:Ljava/lang/String; = "actconvid"

.field public static final KEY_APP_ID:Ljava/lang/String; = "appId"

.field public static final KEY_FUNNEL:Ljava/lang/String; = "funnel"

.field public static final KEY_NETWORK_TYPE:Ljava/lang/String; = "nt"

.field public static final KEY_THREAD_INFO:Ljava/lang/String; = "thread"

.field private static TAG:Ljava/lang/String; = "HS_ErrorReport"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 12
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getErrorReportExtras(Landroid/content/Context;Ljava/lang/Thread;)Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/Thread;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/logger/logmodels/ILogExtrasModel;",
            ">;"
        }
    .end annotation

    .line 35
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    :try_start_0
    const-string v1, "appId"

    .line 38
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "nt"

    .line 42
    invoke-static {p0}, Lcom/helpshift/util/HelpshiftConnectionUtil;->getNetworkType(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p0

    invoke-static {v1, p0}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object p0

    invoke-interface {v0, p0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 45
    invoke-static {}, Lcom/helpshift/providers/CrossModuleDataProvider;->getSupportDataProvider()Lcom/helpshift/providers/ISupportDataProvider;

    move-result-object p0

    if-nez p0, :cond_0

    const-string v1, ""

    goto :goto_0

    .line 47
    :cond_0
    invoke-interface {p0}, Lcom/helpshift/providers/ISupportDataProvider;->getActionEvents()Ljava/lang/String;

    move-result-object v1

    :goto_0
    if-eqz v1, :cond_1

    const-string v2, "funnel"

    .line 50
    invoke-static {v2, v1}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_1
    if-nez p0, :cond_2

    const-string p0, ""

    goto :goto_1

    .line 53
    :cond_2
    invoke-interface {p0}, Lcom/helpshift/providers/ISupportDataProvider;->getActiveConversationId()Ljava/lang/String;

    move-result-object p0

    .line 54
    :goto_1
    invoke-static {p0}, Lcom/helpshift/util/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-nez v1, :cond_3

    const-string v1, "actconvid"

    .line 55
    invoke-static {v1, p0}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object p0

    invoke-interface {v0, p0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_3
    const-string p0, "Unknown"

    if-eqz p1, :cond_4

    .line 61
    invoke-virtual {p1}, Ljava/lang/Thread;->toString()Ljava/lang/String;

    move-result-object p0

    :cond_4
    const-string p1, "thread"

    .line 63
    invoke-static {p1, p0}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object p0

    invoke-interface {v0, p0}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_2

    :catch_0
    move-exception p0

    .line 66
    sget-object p1, Lcom/helpshift/util/ErrorReportProvider;->TAG:Ljava/lang/String;

    const-string v1, "Error creating error report"

    invoke-static {p1, v1, p0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_2
    return-object v0
.end method
