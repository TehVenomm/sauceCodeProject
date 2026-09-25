.class public Lcom/helpshift/util/HelpshiftUtil;
.super Ljava/lang/Object;
.source "HelpshiftUtil.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_InitUtil"

.field private static isHelpshiftInitSuccessful:Z


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 13
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static installWithCachedCreds(Landroid/content/Context;)Z
    .locals 6

    .line 21
    sget-boolean v0, Lcom/helpshift/util/HelpshiftUtil;->isHelpshiftInitSuccessful:Z

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    return v1

    .line 27
    :cond_0
    :try_start_0
    invoke-static {p0}, Lcom/helpshift/util/HelpshiftContext;->setApplicationContext(Landroid/content/Context;)V

    .line 28
    invoke-static {p0}, Lcom/helpshift/storage/HelpshiftUnityStorage;->getInstance(Landroid/content/Context;)Lcom/helpshift/storage/HelpshiftUnityStorage;

    move-result-object v0

    const-string v2, "apiKey"

    .line 29
    invoke-virtual {v0, v2}, Lcom/helpshift/storage/HelpshiftUnityStorage;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "domainName"

    .line 30
    invoke-virtual {v0, v3}, Lcom/helpshift/storage/HelpshiftUnityStorage;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v4, "appId"

    .line 31
    invoke-virtual {v0, v4}, Lcom/helpshift/storage/HelpshiftUnityStorage;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v5, "installConfig"

    .line 32
    invoke-virtual {v0, v5}, Lcom/helpshift/storage/HelpshiftUnityStorage;->getMap(Ljava/lang/String;)Ljava/util/HashMap;

    move-result-object v0

    if-nez v0, :cond_1

    .line 35
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    check-cast p0, Landroid/app/Application;

    invoke-static {p0, v2, v3, v4}, Lcom/helpshift/HelpshiftUnityAPI;->install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 38
    :cond_1
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    check-cast p0, Landroid/app/Application;

    invoke-static {p0, v2, v3, v4, v0}, Lcom/helpshift/HelpshiftUnityAPI;->install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    .line 41
    :goto_0
    sput-boolean v1, Lcom/helpshift/util/HelpshiftUtil;->isHelpshiftInitSuccessful:Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return v1

    :catch_0
    move-exception p0

    const-string v0, "Helpshift_InitUtil"

    const-string v1, "Error initializing Helpshift : "

    .line 45
    invoke-static {v0, v1, p0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p0, 0x0

    return p0
.end method
