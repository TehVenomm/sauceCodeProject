.class Lorg/onepf/oms/OpenIabHelper$9;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Landroid/content/ServiceConnection;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper;->setupForPackage(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/lang/String;Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/OpenIabHelper;

.field final synthetic val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

.field final synthetic val$withFallback:Z


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper;ZLorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 0

    .line 617
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$9;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iput-boolean p2, p0, Lorg/onepf/oms/OpenIabHelper$9;->val$withFallback:Z

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$9;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 2

    const/4 v0, 0x0

    .line 622
    :try_start_0
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$9;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v1, p1, p2, p0}, Lorg/onepf/oms/OpenIabHelper;->access$600(Lorg/onepf/oms/OpenIabHelper;Landroid/content/ComponentName;Landroid/os/IBinder;Landroid/content/ServiceConnection;)Lorg/onepf/oms/appstore/OpenAppstore;

    move-result-object p1

    if-eqz p1, :cond_1

    .line 625
    invoke-interface {p1}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object p2

    .line 626
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$9;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v1}, Lorg/onepf/oms/OpenIabHelper;->access$300(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    :goto_0
    move-object v0, p1

    goto :goto_1

    .line 630
    :cond_0
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$9;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {p1, p2}, Lorg/onepf/oms/OpenIabHelper;->access$700(Lorg/onepf/oms/OpenIabHelper;Ljava/lang/String;)Lorg/onepf/oms/Appstore;

    move-result-object p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "setupForPackage() Error binding to open store service : "

    .line 634
    invoke-static {p2, p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_1
    :goto_1
    if-nez v0, :cond_2

    .line 636
    iget-boolean p1, p0, Lorg/onepf/oms/OpenIabHelper$9;->val$withFallback:Z

    if-eqz p1, :cond_2

    .line 637
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$9;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iget-object p2, p0, Lorg/onepf/oms/OpenIabHelper$9;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-static {p1, p2}, Lorg/onepf/oms/OpenIabHelper;->access$800(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    goto :goto_2

    .line 639
    :cond_2
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$9;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iget-object p2, p0, Lorg/onepf/oms/OpenIabHelper$9;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-static {p1, p2, v0}, Lorg/onepf/oms/OpenIabHelper;->access$900(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V

    :goto_2
    return-void
.end method

.method public onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 0

    return-void
.end method
