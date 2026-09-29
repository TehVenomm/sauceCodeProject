.class Lorg/onepf/openiab/UnityPlugin$1;
.super Ljava/lang/Object;
.source "UnityPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/openiab/UnityPlugin;->initWithOptions(Lorg/onepf/oms/OpenIabHelper$Options;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/openiab/UnityPlugin;

.field final synthetic val$options:Lorg/onepf/oms/OpenIabHelper$Options;


# direct methods
.method constructor <init>(Lorg/onepf/openiab/UnityPlugin;Lorg/onepf/oms/OpenIabHelper$Options;)V
    .locals 0

    .line 102
    iput-object p1, p0, Lorg/onepf/openiab/UnityPlugin$1;->this$0:Lorg/onepf/openiab/UnityPlugin;

    iput-object p2, p0, Lorg/onepf/openiab/UnityPlugin$1;->val$options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 105
    iget-object v0, p0, Lorg/onepf/openiab/UnityPlugin$1;->this$0:Lorg/onepf/openiab/UnityPlugin;

    new-instance v1, Lorg/onepf/oms/OpenIabHelper;

    sget-object v2, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    iget-object v3, p0, Lorg/onepf/openiab/UnityPlugin$1;->val$options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-direct {v1, v2, v3}, Lorg/onepf/oms/OpenIabHelper;-><init>(Landroid/content/Context;Lorg/onepf/oms/OpenIabHelper$Options;)V

    invoke-static {v0, v1}, Lorg/onepf/openiab/UnityPlugin;->access$002(Lorg/onepf/openiab/UnityPlugin;Lorg/onepf/oms/OpenIabHelper;)Lorg/onepf/oms/OpenIabHelper;

    .line 106
    iget-object v0, p0, Lorg/onepf/openiab/UnityPlugin$1;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-static {v0}, Lorg/onepf/openiab/UnityPlugin;->access$100(Lorg/onepf/openiab/UnityPlugin;)V

    const-string v0, "OpenIAB-UnityPlugin"

    const-string v1, "Starting setup."

    .line 110
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 111
    iget-object v0, p0, Lorg/onepf/openiab/UnityPlugin$1;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-static {v0}, Lorg/onepf/openiab/UnityPlugin;->access$000(Lorg/onepf/openiab/UnityPlugin;)Lorg/onepf/oms/OpenIabHelper;

    move-result-object v0

    new-instance v1, Lorg/onepf/openiab/UnityPlugin$1$1;

    invoke-direct {v1, p0}, Lorg/onepf/openiab/UnityPlugin$1$1;-><init>(Lorg/onepf/openiab/UnityPlugin$1;)V

    invoke-virtual {v0, v1}, Lorg/onepf/oms/OpenIabHelper;->startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    return-void
.end method
