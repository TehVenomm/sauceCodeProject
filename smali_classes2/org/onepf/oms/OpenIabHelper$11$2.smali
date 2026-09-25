.class Lorg/onepf/oms/OpenIabHelper$11$2;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper$11;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lorg/onepf/oms/OpenIabHelper$11;

.field final synthetic val$foundAppstore:Lorg/onepf/oms/Appstore;

.field final synthetic val$listenerWrapper:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper$11;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V
    .locals 0

    .line 800
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$11$2;->this$1:Lorg/onepf/oms/OpenIabHelper$11;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$11$2;->val$listenerWrapper:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$11$2;->val$foundAppstore:Lorg/onepf/oms/Appstore;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 803
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$11$2;->this$1:Lorg/onepf/oms/OpenIabHelper$11;

    iget-object v0, v0, Lorg/onepf/oms/OpenIabHelper$11;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$11$2;->val$listenerWrapper:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$11$2;->val$foundAppstore:Lorg/onepf/oms/Appstore;

    invoke-static {v0, v1, v2}, Lorg/onepf/oms/OpenIabHelper;->access$1700(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V

    return-void
.end method
