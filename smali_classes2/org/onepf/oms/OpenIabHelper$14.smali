.class Lorg/onepf/oms/OpenIabHelper$14;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Landroid/content/ServiceConnection;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper;->discoverOpenStores(Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;Ljava/util/Queue;Ljava/util/List;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/OpenIabHelper;

.field final synthetic val$appstores:Ljava/util/List;

.field final synthetic val$bindServiceIntents:Ljava/util/Queue;

.field final synthetic val$listener:Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/List;Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;Ljava/util/Queue;)V
    .locals 0

    .line 989
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$14;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$14;->val$appstores:Ljava/util/List;

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$14;->val$listener:Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;

    iput-object p4, p0, Lorg/onepf/oms/OpenIabHelper$14;->val$bindServiceIntents:Ljava/util/Queue;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 2

    .line 994
    :try_start_0
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$14;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v0, p1, p2, p0}, Lorg/onepf/oms/OpenIabHelper;->access$600(Lorg/onepf/oms/OpenIabHelper;Landroid/content/ComponentName;Landroid/os/IBinder;Landroid/content/ServiceConnection;)Lorg/onepf/oms/appstore/OpenAppstore;

    move-result-object p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "onServiceConnected() Error creating appsotre: "

    .line 996
    invoke-static {p2, p1}, Lorg/onepf/oms/util/Logger;->w(Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p1, 0x0

    :goto_0
    if-eqz p1, :cond_0

    .line 999
    iget-object p2, p0, Lorg/onepf/oms/OpenIabHelper$14;->val$appstores:Ljava/util/List;

    invoke-interface {p2, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 1001
    :cond_0
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$14;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iget-object p2, p0, Lorg/onepf/oms/OpenIabHelper$14;->val$listener:Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$14;->val$bindServiceIntents:Ljava/util/Queue;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$14;->val$appstores:Ljava/util/List;

    invoke-static {p1, p2, v0, v1}, Lorg/onepf/oms/OpenIabHelper;->access$1900(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;Ljava/util/Queue;Ljava/util/List;)V

    return-void
.end method

.method public onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 0

    return-void
.end method
