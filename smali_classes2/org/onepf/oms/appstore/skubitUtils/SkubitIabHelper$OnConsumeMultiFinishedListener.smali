.class public interface abstract Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeMultiFinishedListener;
.super Ljava/lang/Object;
.source "SkubitIabHelper.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "OnConsumeMultiFinishedListener"
.end annotation


# virtual methods
.method public abstract onConsumeMultiFinished(Ljava/util/List;Ljava/util/List;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lorg/onepf/oms/appstore/googleUtils/Purchase;",
            ">;",
            "Ljava/util/List<",
            "Lorg/onepf/oms/appstore/googleUtils/IabResult;",
            ">;)V"
        }
    .end annotation
.end method
