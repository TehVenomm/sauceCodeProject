.class Lorg/onepf/oms/OpenIabHelper$2;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Lorg/onepf/oms/OpenIabHelper$AppstoreFactory;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/OpenIabHelper;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/OpenIabHelper;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper;)V
    .locals 0

    .line 260
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$2;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public get()Lorg/onepf/oms/Appstore;
    .locals 3
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 264
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$2;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v0}, Lorg/onepf/oms/OpenIabHelper;->access$100(Lorg/onepf/oms/OpenIabHelper;)Lorg/onepf/oms/OpenIabHelper$Options;

    move-result-object v0

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getVerifyMode()I

    move-result v0

    const/4 v1, 0x1

    if-eq v0, v1, :cond_0

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$2;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v0}, Lorg/onepf/oms/OpenIabHelper;->access$100(Lorg/onepf/oms/OpenIabHelper;)Lorg/onepf/oms/OpenIabHelper$Options;

    move-result-object v0

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getStoreKeys()Ljava/util/Map;

    move-result-object v0

    const-string v1, "com.google.play"

    invoke-interface {v0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 267
    :goto_0
    new-instance v1, Lorg/onepf/oms/appstore/GooglePlay;

    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$2;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v2}, Lorg/onepf/oms/OpenIabHelper;->access$000(Lorg/onepf/oms/OpenIabHelper;)Landroid/content/Context;

    move-result-object v2

    invoke-direct {v1, v2, v0}, Lorg/onepf/oms/appstore/GooglePlay;-><init>(Landroid/content/Context;Ljava/lang/String;)V

    return-object v1
.end method
