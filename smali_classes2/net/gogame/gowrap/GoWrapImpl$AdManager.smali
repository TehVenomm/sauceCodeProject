.class abstract Lnet/gogame/gowrap/GoWrapImpl$AdManager;
.super Ljava/lang/Object;
.source "GoWrapImpl.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/GoWrapImpl;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x40a
    name = "AdManager"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "<INTEGRATION:",
        "Ljava/lang/Object;",
        "SIZE:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/lang/Object;"
    }
.end annotation


# instance fields
.field private currentIntegration:Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "TINTEGRATION;"
        }
    .end annotation
.end field

.field private index:I

.field private final integrations:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "TINTEGRATION;>;"
        }
    .end annotation
.end field

.field private final name:Ljava/lang/String;


# direct methods
.method private constructor <init>(Ljava/lang/String;Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "TINTEGRATION;>;)V"
        }
    .end annotation

    .line 796
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 792
    iput v0, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->index:I

    const/4 v0, 0x0

    .line 793
    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->currentIntegration:Ljava/lang/Object;

    .line 797
    iput-object p1, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->name:Ljava/lang/String;

    .line 798
    iput-object p2, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->integrations:Ljava/util/List;

    return-void
.end method

.method synthetic constructor <init>(Ljava/lang/String;Ljava/util/List;Lnet/gogame/gowrap/GoWrapImpl$1;)V
    .locals 0

    .line 788
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;-><init>(Ljava/lang/String;Ljava/util/List;)V

    return-void
.end method

.method private incrementAdProviderIndex()V
    .locals 2

    .line 808
    iget v0, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->index:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->index:I

    .line 809
    iget v0, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->index:I

    iget-object v1, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->integrations:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    if-lt v0, v1, :cond_0

    const/4 v0, 0x0

    .line 810
    iput v0, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->index:I

    :cond_0
    return-void
.end method


# virtual methods
.method public hasAds(Ljava/lang/Object;)Z
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TSIZE;)Z"
        }
    .end annotation

    .line 815
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->integrations:Ljava/util/List;

    const/4 v1, 0x1

    const/4 v2, 0x0

    if-eqz v0, :cond_3

    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->integrations:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_2

    :cond_0
    const/4 v0, 0x0

    .line 820
    :goto_0
    iget-object v3, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->integrations:Ljava/util/List;

    invoke-interface {v3}, Ljava/util/List;->size()I

    move-result v3

    if-ge v0, v3, :cond_2

    .line 821
    iget-object v3, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->integrations:Ljava/util/List;

    iget v4, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->index:I

    invoke-interface {v3, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    .line 822
    invoke-static {v3}, Lnet/gogame/gowrap/GoWrapImpl;->access$500(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v4

    .line 824
    :try_start_0
    invoke-virtual {p0, v3, p1}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->hasAds(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v3

    const/4 v5, 0x2

    if-eqz v3, :cond_1

    const-string v3, "goWrap"

    const-string v6, "%s.hasdAds(): %s has ads"

    .line 825
    new-array v5, v5, [Ljava/lang/Object;

    iget-object v7, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->name:Ljava/lang/String;

    aput-object v7, v5, v2

    aput-object v4, v5, v1

    invoke-static {v6, v5}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    return v1

    :cond_1
    const-string v3, "goWrap"

    const-string v6, "%s.hasAds(): %s has no ads"

    .line 828
    new-array v5, v5, [Ljava/lang/Object;

    iget-object v7, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->name:Ljava/lang/String;

    aput-object v7, v5, v2

    aput-object v4, v5, v1

    invoke-static {v6, v5}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v3

    const-string v4, "goWrap"

    const-string v5, "Exception"

    .line 832
    invoke-static {v4, v5, v3}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 834
    :goto_1
    invoke-direct {p0}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->incrementAdProviderIndex()V

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_2
    const-string p1, "goWrap"

    const-string v0, "%s.hasAds()=false"

    .line 836
    new-array v1, v1, [Ljava/lang/Object;

    iget-object v3, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->name:Ljava/lang/String;

    aput-object v3, v1, v2

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    return v2

    :cond_3
    :goto_2
    const-string p1, "goWrap"

    const-string v0, "%s.hasAds()=false, no integrations available"

    .line 816
    new-array v1, v1, [Ljava/lang/Object;

    iget-object v3, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->name:Ljava/lang/String;

    aput-object v3, v1, v2

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    return v2
.end method

.method protected abstract hasAds(Ljava/lang/Object;Ljava/lang/Object;)Z
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TINTEGRATION;TSIZE;)Z"
        }
    .end annotation
.end method

.method public hideAd()V
    .locals 1

    .line 868
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->currentIntegration:Ljava/lang/Object;

    if-nez v0, :cond_0

    return-void

    .line 871
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->currentIntegration:Ljava/lang/Object;

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->hideAd(Ljava/lang/Object;)V

    return-void
.end method

.method protected abstract hideAd(Ljava/lang/Object;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TINTEGRATION;)V"
        }
    .end annotation
.end method

.method public showAd(Ljava/lang/Object;)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TSIZE;)V"
        }
    .end annotation

    .line 841
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->integrations:Ljava/util/List;

    const/4 v1, 0x0

    const/4 v2, 0x1

    if-eqz v0, :cond_3

    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->integrations:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_2

    :cond_0
    const/4 v0, 0x0

    .line 846
    :goto_0
    iget-object v3, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->integrations:Ljava/util/List;

    invoke-interface {v3}, Ljava/util/List;->size()I

    move-result v3

    if-ge v0, v3, :cond_2

    .line 847
    iget-object v3, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->integrations:Ljava/util/List;

    iget v4, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->index:I

    invoke-interface {v3, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    .line 848
    invoke-static {v3}, Lnet/gogame/gowrap/GoWrapImpl;->access$500(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v4

    .line 850
    :try_start_0
    invoke-virtual {p0, v3, p1}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->showAd(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v5

    const/4 v6, 0x2

    if-eqz v5, :cond_1

    .line 851
    iput-object v3, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->currentIntegration:Ljava/lang/Object;

    const-string v3, "goWrap"

    const-string v5, "%s.showAd(): %s shown"

    .line 852
    new-array v6, v6, [Ljava/lang/Object;

    iget-object v7, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->name:Ljava/lang/String;

    aput-object v7, v6, v1

    aput-object v4, v6, v2

    invoke-static {v5, v6}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 853
    invoke-direct {p0}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->incrementAdProviderIndex()V

    return-void

    :cond_1
    const-string v3, "goWrap"

    const-string v5, "%s.showAd(): %s has no ads"

    .line 856
    new-array v6, v6, [Ljava/lang/Object;

    iget-object v7, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->name:Ljava/lang/String;

    aput-object v7, v6, v1

    aput-object v4, v6, v2

    invoke-static {v5, v6}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v3

    const-string v4, "goWrap"

    const-string v5, "Exception"

    .line 860
    invoke-static {v4, v5, v3}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 862
    :goto_1
    invoke-direct {p0}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->incrementAdProviderIndex()V

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_2
    const-string p1, "goWrap"

    const-string v0, "%s.showAd() no ads"

    .line 864
    new-array v2, v2, [Ljava/lang/Object;

    iget-object v3, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->name:Ljava/lang/String;

    aput-object v3, v2, v1

    invoke-static {v0, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_3
    :goto_2
    const-string p1, "goWrap"

    const-string v0, "%s.showAd(): no integrations available"

    .line 842
    new-array v2, v2, [Ljava/lang/Object;

    iget-object v3, p0, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->name:Ljava/lang/String;

    aput-object v3, v2, v1

    invoke-static {v0, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method protected abstract showAd(Ljava/lang/Object;Ljava/lang/Object;)Z
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TINTEGRATION;TSIZE;)Z"
        }
    .end annotation
.end method
