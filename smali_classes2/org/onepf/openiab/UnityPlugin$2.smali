.class Lorg/onepf/openiab/UnityPlugin$2;
.super Ljava/lang/Object;
.source "UnityPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/openiab/UnityPlugin;->queryInventory()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/openiab/UnityPlugin;


# direct methods
.method constructor <init>(Lorg/onepf/openiab/UnityPlugin;)V
    .locals 0

    .line 144
    iput-object p1, p0, Lorg/onepf/openiab/UnityPlugin$2;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 147
    iget-object v0, p0, Lorg/onepf/openiab/UnityPlugin$2;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-static {v0}, Lorg/onepf/openiab/UnityPlugin;->access$000(Lorg/onepf/openiab/UnityPlugin;)Lorg/onepf/oms/OpenIabHelper;

    move-result-object v0

    iget-object v1, p0, Lorg/onepf/openiab/UnityPlugin$2;->this$0:Lorg/onepf/openiab/UnityPlugin;

    iget-object v1, v1, Lorg/onepf/openiab/UnityPlugin;->_queryInventoryListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;

    invoke-virtual {v0, v1}, Lorg/onepf/oms/OpenIabHelper;->queryInventoryAsync(Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method
