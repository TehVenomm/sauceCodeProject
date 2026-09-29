.class Lorg/onepf/openiab/UnityPlugin$9;
.super Landroid/content/BroadcastReceiver;
.source "UnityPlugin.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/openiab/UnityPlugin;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "YandexBillingReceiver"


# instance fields
.field final synthetic this$0:Lorg/onepf/openiab/UnityPlugin;


# direct methods
.method constructor <init>(Lorg/onepf/openiab/UnityPlugin;)V
    .locals 0

    .line 383
    iput-object p1, p0, Lorg/onepf/openiab/UnityPlugin$9;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method

.method private purchaseStateChanged(Landroid/content/Intent;)V
    .locals 3

    const-string v0, "YandexBillingReceiver"

    .line 397
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "purchaseStateChanged intent: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 398
    iget-object v0, p0, Lorg/onepf/openiab/UnityPlugin$9;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-static {v0}, Lorg/onepf/openiab/UnityPlugin;->access$000(Lorg/onepf/openiab/UnityPlugin;)Lorg/onepf/oms/OpenIabHelper;

    move-result-object v0

    const/16 v1, 0x2711

    const/4 v2, -0x1

    invoke-virtual {v0, v1, v2, p1}, Lorg/onepf/oms/OpenIabHelper;->handleActivityResult(IILandroid/content/Intent;)Z

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 3

    .line 388
    invoke-virtual {p2}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object p1

    const-string v0, "YandexBillingReceiver"

    .line 389
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "onReceive intent: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    const-string v0, "com.yandex.store.service.PURCHASE_STATE_CHANGED"

    .line 391
    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 392
    invoke-direct {p0, p2}, Lorg/onepf/openiab/UnityPlugin$9;->purchaseStateChanged(Landroid/content/Intent;)V

    :cond_0
    return-void
.end method
