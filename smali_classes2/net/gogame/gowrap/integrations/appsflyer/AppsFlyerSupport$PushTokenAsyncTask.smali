.class Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport$PushTokenAsyncTask;
.super Lnet/gogame/gowrap/support/AbstractPushTokenAsyncTask;
.source "AppsFlyerSupport.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "PushTokenAsyncTask"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 145
    invoke-direct {p0}, Lnet/gogame/gowrap/support/AbstractPushTokenAsyncTask;-><init>()V

    return-void
.end method

.method synthetic constructor <init>(Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport$1;)V
    .locals 0

    .line 145
    invoke-direct {p0}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport$PushTokenAsyncTask;-><init>()V

    return-void
.end method


# virtual methods
.method protected onPushTokenReceived(Landroid/content/Context;Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 151
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object v0

    invoke-virtual {v0, p1, p2}, Lcom/appsflyer/AppsFlyerLib;->updateServerUninstallToken(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method
