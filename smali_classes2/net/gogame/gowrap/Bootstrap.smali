.class public Lnet/gogame/gowrap/Bootstrap;
.super Lnet/gogame/gowrap/bootstrap/AbstractBootstrap;
.source "Bootstrap.java"


# static fields
.field private static final INSTANCE:Lnet/gogame/gowrap/Bootstrap;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 9
    new-instance v0, Lnet/gogame/gowrap/Bootstrap;

    invoke-direct {v0}, Lnet/gogame/gowrap/Bootstrap;-><init>()V

    sput-object v0, Lnet/gogame/gowrap/Bootstrap;->INSTANCE:Lnet/gogame/gowrap/Bootstrap;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 7
    invoke-direct {p0}, Lnet/gogame/gowrap/bootstrap/AbstractBootstrap;-><init>()V

    return-void
.end method

.method public static init(Landroid/app/Activity;)V
    .locals 1

    .line 77
    sget-object v0, Lnet/gogame/gowrap/Bootstrap;->INSTANCE:Lnet/gogame/gowrap/Bootstrap;

    invoke-virtual {v0, p0}, Lnet/gogame/gowrap/Bootstrap;->doInit(Landroid/app/Activity;)V

    return-void
.end method

.method public static init(Landroid/app/Activity;Z)V
    .locals 1

    .line 73
    sget-object v0, Lnet/gogame/gowrap/Bootstrap;->INSTANCE:Lnet/gogame/gowrap/Bootstrap;

    invoke-virtual {v0, p0, p1}, Lnet/gogame/gowrap/Bootstrap;->doInit(Landroid/app/Activity;Z)V

    return-void
.end method

.method public static unityInit()V
    .locals 1

    .line 81
    sget-object v0, Lnet/gogame/gowrap/Bootstrap;->INSTANCE:Lnet/gogame/gowrap/Bootstrap;

    invoke-virtual {v0}, Lnet/gogame/gowrap/Bootstrap;->doUnityInit()V

    return-void
.end method


# virtual methods
.method public doCustomInit(Landroid/app/Activity;)V
    .locals 5

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 14
    :try_start_0
    sget-object v2, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    const-class v3, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;

    invoke-virtual {v2, v3}, Lnet/gogame/gowrap/GoWrapImpl;->setMainActivity(Ljava/lang/Class;)V

    .line 16
    new-instance v2, Lnet/gogame/gowrap/integrations/Config;

    invoke-direct {v2}, Lnet/gogame/gowrap/integrations/Config;-><init>()V

    const-string v3, "appId"

    const-string v4, "DragonProject"

    .line 18
    invoke-virtual {v2, v3, v4}, Lnet/gogame/gowrap/integrations/Config;->putString(Ljava/lang/String;Ljava/lang/Object;)V

    const-string v3, "disableFab"

    const/4 v4, 0x1

    .line 20
    invoke-virtual {v2, v3, v4}, Lnet/gogame/gowrap/integrations/Config;->putBoolean(Ljava/lang/String;Z)V

    const-string v3, "forceEnableChat"

    .line 21
    invoke-virtual {v2, v3, v1}, Lnet/gogame/gowrap/integrations/Config;->putBoolean(Ljava/lang/String;Z)V

    const-string v3, "customMainActivity"

    .line 23
    invoke-virtual {v2, v3, v0}, Lnet/gogame/gowrap/integrations/Config;->putString(Ljava/lang/String;Ljava/lang/Object;)V

    .line 25
    sget-object v3, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    sget-object v4, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {v3, v4, p1, v2}, Lnet/gogame/gowrap/GoWrapImpl;->register(Lnet/gogame/gowrap/integrations/IntegrationSupport;Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    const-string v3, "goWrap"

    const-string v4, "Exception"

    .line 28
    invoke-static {v3, v4, v2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 31
    :goto_0
    :try_start_1
    new-instance v2, Lnet/gogame/gowrap/integrations/Config;

    invoke-direct {v2}, Lnet/gogame/gowrap/integrations/Config;-><init>()V

    const-string v3, "accountKey"

    const-string v4, "3NVMS9MbGUuR3KVE1V3mBiaPkWsaJgyr"

    .line 33
    invoke-virtual {v2, v3, v4}, Lnet/gogame/gowrap/integrations/Config;->putString(Ljava/lang/String;Ljava/lang/Object;)V

    .line 36
    sget-object v3, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    new-instance v4, Lnet/gogame/gowrap/integrations/zopim/CustomZopimSupport;

    invoke-direct {v4}, Lnet/gogame/gowrap/integrations/zopim/CustomZopimSupport;-><init>()V

    invoke-virtual {v3, v4, p1, v2}, Lnet/gogame/gowrap/GoWrapImpl;->register(Lnet/gogame/gowrap/integrations/IntegrationSupport;Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_1

    :catch_1
    move-exception v2

    const-string v3, "goWrap"

    const-string v4, "Exception"

    .line 39
    invoke-static {v3, v4, v2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 42
    :goto_1
    :try_start_2
    new-instance v2, Lnet/gogame/gowrap/integrations/Config;

    invoke-direct {v2}, Lnet/gogame/gowrap/integrations/Config;-><init>()V

    const-string v3, "devKey"

    const-string v4, "BdP724hHJFraJaxKXvNex7"

    .line 44
    invoke-virtual {v2, v3, v4}, Lnet/gogame/gowrap/integrations/Config;->putString(Ljava/lang/String;Ljava/lang/Object;)V

    const-string v3, "senderId"

    .line 47
    invoke-virtual {v2, v3, v0}, Lnet/gogame/gowrap/integrations/Config;->putString(Ljava/lang/String;Ljava/lang/Object;)V

    .line 49
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    new-instance v3, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;

    invoke-direct {v3}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;-><init>()V

    invoke-virtual {v0, v3, p1, v2}, Lnet/gogame/gowrap/GoWrapImpl;->register(Lnet/gogame/gowrap/integrations/IntegrationSupport;Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;)V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2

    goto :goto_2

    :catch_2
    move-exception v0

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 52
    invoke-static {v2, v3, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 55
    :goto_2
    :try_start_3
    new-instance v0, Lnet/gogame/gowrap/integrations/Config;

    invoke-direct {v0}, Lnet/gogame/gowrap/integrations/Config;-><init>()V

    const-string v2, "appId"

    const-string v3, "424419412411496365097632"

    .line 57
    invoke-virtual {v0, v2, v3}, Lnet/gogame/gowrap/integrations/Config;->putString(Ljava/lang/String;Ljava/lang/Object;)V

    const-string v2, "secret"

    const-string v3, "bvhp6z9myz91cs83ejhnrwnqmqfvjy9p"

    .line 60
    invoke-virtual {v0, v2, v3}, Lnet/gogame/gowrap/integrations/Config;->putString(Ljava/lang/String;Ljava/lang/Object;)V

    const-string v2, "gameManagedVipStatus"

    .line 63
    invoke-virtual {v0, v2, v1}, Lnet/gogame/gowrap/integrations/Config;->putBoolean(Ljava/lang/String;Z)V

    .line 65
    sget-object v1, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    new-instance v2, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;

    invoke-direct {v2}, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;-><init>()V

    invoke-virtual {v1, v2, p1, v0}, Lnet/gogame/gowrap/GoWrapImpl;->register(Lnet/gogame/gowrap/integrations/IntegrationSupport;Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;)V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_3

    goto :goto_3

    :catch_3
    move-exception p1

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 68
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_3
    return-void
.end method
