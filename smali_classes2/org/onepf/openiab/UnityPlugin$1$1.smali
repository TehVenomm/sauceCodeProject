.class Lorg/onepf/openiab/UnityPlugin$1$1;
.super Ljava/lang/Object;
.source "UnityPlugin.java"

# interfaces
.implements Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/openiab/UnityPlugin$1;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lorg/onepf/openiab/UnityPlugin$1;


# direct methods
.method constructor <init>(Lorg/onepf/openiab/UnityPlugin$1;)V
    .locals 0

    .line 111
    iput-object p1, p0, Lorg/onepf/openiab/UnityPlugin$1$1;->this$1:Lorg/onepf/openiab/UnityPlugin$1;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V
    .locals 3

    const-string v0, "OpenIAB-UnityPlugin"

    const-string v1, "Setup finished."

    .line 113
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 115
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->isFailure()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "OpenIAB-UnityPlugin"

    .line 117
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Problem setting up in-app billing: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const-string v0, "OpenIABEventManager"

    const-string v1, "OnBillingNotSupported"

    .line 118
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, v1, p1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :cond_0
    const-string p1, "OpenIAB-UnityPlugin"

    const-string v0, "Setup successful."

    .line 123
    invoke-static {p1, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    const-string p1, "OpenIABEventManager"

    const-string v0, "OnBillingSupported"

    const-string v1, ""

    .line 124
    invoke-static {p1, v0, v1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
