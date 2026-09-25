.class public Lnet/gogame/gowrap/ui/utils/ShareBroadcastReceiver;
.super Landroid/content/BroadcastReceiver;
.source "ShareBroadcastReceiver.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 7
    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 0

    .line 11
    invoke-virtual {p2}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object p2

    invoke-virtual {p2}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Lnet/gogame/gowrap/ui/utils/ShareHelper;->share(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method
