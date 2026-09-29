.class public Lcom/helpshift/UnityPluginAPIEventsBridge;
.super Ljava/lang/Object;
.source "UnityPluginAPIEventsBridge.java"

# interfaces
.implements Lcom/helpshift/PluginEventBridge$PluginEventsAPI;


# instance fields
.field private storage:Lcom/helpshift/storage/HelpshiftUnityStorage;


# direct methods
.method public constructor <init>(Lcom/helpshift/storage/HelpshiftUnityStorage;)V
    .locals 0

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 15
    iput-object p1, p0, Lcom/helpshift/UnityPluginAPIEventsBridge;->storage:Lcom/helpshift/storage/HelpshiftUnityStorage;

    return-void
.end method


# virtual methods
.method public getPendingIntentForNotification(Landroid/content/Context;Landroid/app/PendingIntent;)Landroid/app/PendingIntent;
    .locals 0

    .line 26
    invoke-static {p1, p2}, Lcom/helpshift/util/IntentProvider;->wrapPendingIntentWithUnityDelegateActivity(Landroid/content/Context;Landroid/app/PendingIntent;)Landroid/app/PendingIntent;

    move-result-object p1

    return-object p1
.end method

.method public sendMessage(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 20
    iget-object v0, p0, Lcom/helpshift/UnityPluginAPIEventsBridge;->storage:Lcom/helpshift/storage/HelpshiftUnityStorage;

    const-string v1, "unityMessageHandler"

    invoke-virtual {v0, v1}, Lcom/helpshift/storage/HelpshiftUnityStorage;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 21
    invoke-static {v0, p1, p2}, Lcom/helpshift/util/UnityUtils;->sendUnityMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public shouldCallFirstForegroundEvent()Z
    .locals 1

    .line 31
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method
