.class public Lcom/github/droidfu/http/ConnectionChangedBroadcastReceiver;
.super Landroid/content/BroadcastReceiver;
.source "ConnectionChangedBroadcastReceiver.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 22
    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 0

    .line 26
    invoke-static {p1}, Lcom/github/droidfu/http/BetterHttp;->setContext(Landroid/content/Context;)V

    .line 27
    invoke-static {}, Lcom/github/droidfu/http/BetterHttp;->updateProxySettings()V

    return-void
.end method
