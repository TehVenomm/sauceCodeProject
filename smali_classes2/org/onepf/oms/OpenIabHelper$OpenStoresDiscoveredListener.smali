.class public interface abstract Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/OpenIabHelper;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "OpenStoresDiscoveredListener"
.end annotation


# virtual methods
.method public abstract openStoresDiscovered(Ljava/util/List;)V
    .param p1    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lorg/onepf/oms/Appstore;",
            ">;)V"
        }
    .end annotation
.end method
