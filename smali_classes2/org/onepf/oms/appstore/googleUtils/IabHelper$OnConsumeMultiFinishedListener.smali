.class public interface abstract Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;
.super Ljava/lang/Object;
.source "IabHelper.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/appstore/googleUtils/IabHelper;
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
