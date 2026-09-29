.class public Lcom/helpshift/HelpshiftUnity;
.super Ljava/lang/Object;
.source "HelpshiftUnity.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_UnityJava"

.field private static final UNITY_PLUGIN_VERSION:Ljava/lang/String; = "5.2.0"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 18
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static handlePush(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 0

    .line 53
    invoke-static {p0}, Lcom/helpshift/util/HelpshiftUtil;->installWithCachedCreds(Landroid/content/Context;)Z

    .line 54
    invoke-static {p0, p1}, Lcom/helpshift/HelpshiftUnityAPI;->handlePush(Landroid/content/Context;Landroid/content/Intent;)V

    return-void
.end method

.method public static handlePush(Landroid/content/Context;Landroid/os/Bundle;)V
    .locals 0

    .line 64
    invoke-static {p0}, Lcom/helpshift/util/HelpshiftUtil;->installWithCachedCreds(Landroid/content/Context;)Z

    .line 65
    invoke-static {p0, p1}, Lcom/helpshift/HelpshiftUnityAPI;->handlePush(Landroid/content/Context;Landroid/os/Bundle;)V

    return-void
.end method

.method public static handlePush(Landroid/content/Context;Ljava/util/Map;)V
    .locals 0
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

    .line 75
    invoke-static {p0}, Lcom/helpshift/util/HelpshiftUtil;->installWithCachedCreds(Landroid/content/Context;)Z

    .line 76
    invoke-static {p0, p1}, Lcom/helpshift/HelpshiftUnityAPI;->handlePush(Landroid/content/Context;Ljava/util/Map;)V

    return-void
.end method

.method public static install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Application;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    if-nez p4, :cond_0

    .line 38
    new-instance p4, Ljava/util/HashMap;

    invoke-direct {p4}, Ljava/util/HashMap;-><init>()V

    :cond_0
    const-string v0, "sdkType"

    const-string v1, "unity"

    .line 41
    invoke-interface {p4, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "pluginVersion"

    const-string v1, "5.2.0"

    .line 42
    invoke-interface {p4, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 43
    invoke-static {p0, p1, p2, p3, p4}, Lcom/helpshift/HelpshiftUnityAPI;->install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public static registerDeviceToken(Landroid/content/Context;Ljava/lang/String;)V
    .locals 0

    .line 89
    invoke-static {p0, p1}, Lcom/helpshift/Core;->registerDeviceToken(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method

.method public static setInboxPushNotificationDelegate(Lcom/helpshift/campaigns/delegates/InboxPushNotificationDelegate;)V
    .locals 1

    .line 85
    invoke-static {}, Lcom/helpshift/campaigns/Inbox;->getInstance()Lcom/helpshift/campaigns/Inbox;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/campaigns/Inbox;->setInboxPushNotificationDelegate(Lcom/helpshift/campaigns/delegates/InboxPushNotificationDelegate;)V

    return-void
.end method
