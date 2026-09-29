.class Lorg/onepf/oms/OpenIabHelper$4;
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

    .line 281
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$4;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public get()Lorg/onepf/oms/Appstore;
    .locals 3
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 285
    new-instance v0, Lorg/onepf/oms/appstore/SamsungApps;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$4;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v1}, Lorg/onepf/oms/OpenIabHelper;->access$200(Lorg/onepf/oms/OpenIabHelper;)Landroid/app/Activity;

    move-result-object v1

    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$4;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v2}, Lorg/onepf/oms/OpenIabHelper;->access$100(Lorg/onepf/oms/OpenIabHelper;)Lorg/onepf/oms/OpenIabHelper$Options;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Lorg/onepf/oms/appstore/SamsungApps;-><init>(Landroid/app/Activity;Lorg/onepf/oms/OpenIabHelper$Options;)V

    return-object v0
.end method
