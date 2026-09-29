.class public interface abstract Lcom/helpshift/PluginEventBridge$PluginEventsAPI;
.super Ljava/lang/Object;
.source "PluginEventBridge.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/PluginEventBridge;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "PluginEventsAPI"
.end annotation


# virtual methods
.method public abstract getPendingIntentForNotification(Landroid/content/Context;Landroid/app/PendingIntent;)Landroid/app/PendingIntent;
.end method

.method public abstract sendMessage(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract shouldCallFirstForegroundEvent()Z
.end method
