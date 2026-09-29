.class final Ljp/colopl/drapro/InAppBillingHelper$5;
.super Ljava/lang/Object;
.source "InAppBillingHelper.java"

# interfaces
.implements Ljp/colopl/iab/IabHelper$OnConsumeMultiFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/InAppBillingHelper;->ConsumePromotionItems()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    .line 405
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onConsumeMultiFinished(Ljava/util/List;Ljava/util/List;)V
    .locals 3
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

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 409
    :goto_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v2

    if-ge v0, v2, :cond_1

    .line 410
    invoke-interface {p2, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljp/colopl/iab/IabResult;

    invoke-virtual {v2}, Ljp/colopl/iab/IabResult;->isSuccess()Z

    move-result v2

    if-eqz v2, :cond_0

    .line 411
    invoke-static {}, Ljp/colopl/drapro/InAppBillingHelper;->access$000()Ljava/util/ArrayList;

    move-result-object v1

    invoke-interface {p1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/util/ArrayList;->remove(Ljava/lang/Object;)Z

    const/4 v1, 0x1

    :cond_0
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 415
    :cond_1
    sget-object p1, Ljp/colopl/drapro/InAppBillingHelper;->consumeList:Ljava/util/ArrayList;

    invoke-virtual {p1}, Ljava/util/ArrayList;->clear()V

    .line 416
    invoke-static {v1}, Ljp/colopl/drapro/InAppBillingHelper;->FinishCheckPromotion(Z)V

    return-void
.end method
