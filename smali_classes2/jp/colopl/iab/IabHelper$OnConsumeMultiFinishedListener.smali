.class public interface abstract Ljp/colopl/iab/IabHelper$OnConsumeMultiFinishedListener;
.super Ljava/lang/Object;
.source "IabHelper.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Ljp/colopl/iab/IabHelper;
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
            "Ljp/colopl/iab/Purchase;",
            ">;",
            "Ljava/util/List<",
            "Ljp/colopl/iab/IabResult;",
            ">;)V"
        }
    .end annotation
.end method
