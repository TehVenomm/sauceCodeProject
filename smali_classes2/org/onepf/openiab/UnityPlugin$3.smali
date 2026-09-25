.class Lorg/onepf/openiab/UnityPlugin$3;
.super Ljava/lang/Object;
.source "UnityPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/openiab/UnityPlugin;->queryInventory([Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/openiab/UnityPlugin;

.field final synthetic val$itemSkus:[Ljava/lang/String;


# direct methods
.method constructor <init>(Lorg/onepf/openiab/UnityPlugin;[Ljava/lang/String;)V
    .locals 0

    .line 153
    iput-object p1, p0, Lorg/onepf/openiab/UnityPlugin$3;->this$0:Lorg/onepf/openiab/UnityPlugin;

    iput-object p2, p0, Lorg/onepf/openiab/UnityPlugin$3;->val$itemSkus:[Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 156
    iget-object v0, p0, Lorg/onepf/openiab/UnityPlugin$3;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-static {v0}, Lorg/onepf/openiab/UnityPlugin;->access$000(Lorg/onepf/openiab/UnityPlugin;)Lorg/onepf/oms/OpenIabHelper;

    move-result-object v0

    iget-object v1, p0, Lorg/onepf/openiab/UnityPlugin$3;->val$itemSkus:[Ljava/lang/String;

    invoke-static {v1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v1

    iget-object v2, p0, Lorg/onepf/openiab/UnityPlugin$3;->this$0:Lorg/onepf/openiab/UnityPlugin;

    iget-object v2, v2, Lorg/onepf/openiab/UnityPlugin;->_queryInventoryListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;

    const/4 v3, 0x1

    invoke-virtual {v0, v3, v1, v2}, Lorg/onepf/oms/OpenIabHelper;->queryInventoryAsync(ZLjava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method
