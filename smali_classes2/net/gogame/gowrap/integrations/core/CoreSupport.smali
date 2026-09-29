.class public Lnet/gogame/gowrap/integrations/core/CoreSupport;
.super Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;
.source "CoreSupport.java"


# static fields
.field public static final CONFIG_APP_ID:Ljava/lang/String; = "appId"

.field public static final CONFIG_CUSTOM_MAIN_ACTIVITY:Ljava/lang/String; = "customMainActivity"

.field public static final CONFIG_DISABLE_FAB:Ljava/lang/String; = "disableFab"

.field public static final CONFIG_FORCE_ENABLE_CHAT:Ljava/lang/String; = "forceEnableChat"

.field public static final CONFIG_VARIANT_ID:Ljava/lang/String; = "variantId"

.field public static final INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;


# instance fields
.field private appId:Ljava/lang/String;

.field private customMainActivity:Ljava/lang/String;

.field private disableFab:Z

.field private forceEnableChat:Z

.field private variantId:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 16
    new-instance v0, Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-direct {v0}, Lnet/gogame/gowrap/integrations/core/CoreSupport;-><init>()V

    sput-object v0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    return-void
.end method

.method private constructor <init>()V
    .locals 1

    const-string v0, "core"

    .line 30
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;-><init>(Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 25
    iput-boolean v0, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->disableFab:Z

    .line 26
    iput-boolean v0, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->forceEnableChat:Z

    return-void
.end method

.method private shouldDisplayFab(Landroid/app/Activity;)Z
    .locals 2

    .line 80
    iget-boolean v0, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->disableFab:Z

    if-nez v0, :cond_0

    .line 81
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    const-string v1, "net.gogame.gowrap."

    invoke-virtual {v0, v1}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 82
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    const-string v1, "net.gogame.gopay."

    invoke-virtual {v0, v1}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 83
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    const-string v1, "net.gogame.zopim."

    invoke-virtual {v0, v1}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 84
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    const-string v0, "jp.noahapps.sdk."

    invoke-virtual {p1, v0}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method


# virtual methods
.method protected doInit(Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;)V
    .locals 2

    const-string v0, "appId"

    .line 52
    invoke-virtual {p2, v0}, Lnet/gogame/gowrap/integrations/Config;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->appId:Ljava/lang/String;

    const-string v0, "variantId"

    .line 53
    invoke-virtual {p2, v0}, Lnet/gogame/gowrap/integrations/Config;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->variantId:Ljava/lang/String;

    const-string v0, "disableFab"

    const/4 v1, 0x0

    .line 54
    invoke-virtual {p2, v0, v1}, Lnet/gogame/gowrap/integrations/Config;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    iput-boolean v0, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->disableFab:Z

    const-string v0, "forceEnableChat"

    .line 55
    invoke-virtual {p2, v0, v1}, Lnet/gogame/gowrap/integrations/Config;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    iput-boolean v0, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->forceEnableChat:Z

    const-string v0, "customMainActivity"

    .line 56
    invoke-virtual {p2, v0}, Lnet/gogame/gowrap/integrations/Config;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->customMainActivity:Ljava/lang/String;

    .line 58
    iget-object p2, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->customMainActivity:Ljava/lang/String;

    if-eqz p2, :cond_0

    .line 60
    :try_start_0
    iget-object p2, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->customMainActivity:Ljava/lang/String;

    invoke-static {p2}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;

    move-result-object p2

    .line 61
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0, p2}, Lnet/gogame/gowrap/GoWrapImpl;->setMainActivity(Ljava/lang/Class;)V
    :try_end_0
    .catch Ljava/lang/ClassNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p2

    const-string v0, "Exception"

    .line 63
    invoke-static {v0, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 67
    :cond_0
    :goto_0
    invoke-virtual {p1}, Landroid/app/Activity;->getApplication()Landroid/app/Application;

    move-result-object p2

    sget-object v0, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {p2, v0}, Landroid/app/Application;->registerActivityLifecycleCallbacks(Landroid/app/Application$ActivityLifecycleCallbacks;)V

    .line 69
    sget-object p2, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {p2, p1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->setup(Landroid/content/Context;)V

    .line 71
    new-instance p2, Lnet/gogame/gowrap/integrations/Config;

    invoke-direct {p2}, Lnet/gogame/gowrap/integrations/Config;-><init>()V

    .line 72
    sget-object v0, Lnet/gogame/gowrap/integrations/zendesk/ZendeskSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/zendesk/ZendeskSupport;

    invoke-virtual {v0, p1, p2, p3}, Lnet/gogame/gowrap/integrations/zendesk/ZendeskSupport;->init(Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;)V

    .line 74
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->shouldDisplayFab(Landroid/app/Activity;)Z

    move-result p2

    if-eqz p2, :cond_1

    .line 75
    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/FabManager;->onCreate(Landroid/app/Activity;)V

    :cond_1
    return-void
.end method

.method public getAppId()Ljava/lang/String;
    .locals 1

    .line 34
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->appId:Ljava/lang/String;

    return-object v0
.end method

.method public getVariantId()Ljava/lang/String;
    .locals 1

    .line 38
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->variantId:Ljava/lang/String;

    return-object v0
.end method

.method public isForceEnableChat()Z
    .locals 1

    .line 42
    iget-boolean v0, p0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->forceEnableChat:Z

    return v0
.end method

.method public isIntegrated()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method

.method public onActivityCreated(Landroid/app/Activity;)V
    .locals 1

    .line 89
    invoke-super {p0, p1}, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;->onActivityCreated(Landroid/app/Activity;)V

    .line 90
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->shouldDisplayFab(Landroid/app/Activity;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 91
    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/FabManager;->onCreate(Landroid/app/Activity;)V

    :cond_0
    return-void
.end method

.method public onActivityDestroyed(Landroid/app/Activity;)V
    .locals 1

    .line 113
    invoke-super {p0, p1}, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;->onActivityDestroyed(Landroid/app/Activity;)V

    .line 114
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->shouldDisplayFab(Landroid/app/Activity;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 115
    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/FabManager;->onDestroy(Landroid/app/Activity;)V

    :cond_0
    return-void
.end method

.method public onActivityPaused(Landroid/app/Activity;)V
    .locals 1

    .line 105
    invoke-super {p0, p1}, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;->onActivityPaused(Landroid/app/Activity;)V

    .line 106
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->shouldDisplayFab(Landroid/app/Activity;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 107
    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/FabManager;->onPause(Landroid/app/Activity;)V

    :cond_0
    return-void
.end method

.method public onActivityResumed(Landroid/app/Activity;)V
    .locals 1

    .line 97
    invoke-super {p0, p1}, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;->onActivityResumed(Landroid/app/Activity;)V

    .line 98
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->shouldDisplayFab(Landroid/app/Activity;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 99
    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/FabManager;->onResume(Landroid/app/Activity;)V

    :cond_0
    return-void
.end method
