.class Lorg/onepf/oms/OpenIabHelper$6;
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

    .line 299
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$6;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public get()Lorg/onepf/oms/Appstore;
    .locals 2
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 303
    new-instance v0, Lorg/onepf/oms/appstore/SkubitAppstore;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$6;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v1}, Lorg/onepf/oms/OpenIabHelper;->access$000(Lorg/onepf/oms/OpenIabHelper;)Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1}, Lorg/onepf/oms/appstore/SkubitAppstore;-><init>(Landroid/content/Context;)V

    return-object v0
.end method
