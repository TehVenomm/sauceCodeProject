.class Lorg/onepf/openiab/UnityProxyActivity$1;
.super Landroid/content/BroadcastReceiver;
.source "UnityProxyActivity.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/openiab/UnityProxyActivity;->onCreate(Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/openiab/UnityProxyActivity;


# direct methods
.method constructor <init>(Lorg/onepf/openiab/UnityProxyActivity;)V
    .locals 0

    .line 41
    iput-object p1, p0, Lorg/onepf/openiab/UnityProxyActivity$1;->this$0:Lorg/onepf/openiab/UnityProxyActivity;

    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 0

    const-string p1, "OpenIAB-UnityPlugin"

    const-string p2, "Finish broadcast was received"

    .line 44
    invoke-static {p1, p2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 45
    iget-object p1, p0, Lorg/onepf/openiab/UnityProxyActivity$1;->this$0:Lorg/onepf/openiab/UnityProxyActivity;

    invoke-virtual {p1}, Lorg/onepf/openiab/UnityProxyActivity;->isFinishing()Z

    move-result p1

    if-nez p1, :cond_0

    .line 46
    iget-object p1, p0, Lorg/onepf/openiab/UnityProxyActivity$1;->this$0:Lorg/onepf/openiab/UnityProxyActivity;

    invoke-virtual {p1}, Lorg/onepf/openiab/UnityProxyActivity;->finish()V

    :cond_0
    return-void
.end method
