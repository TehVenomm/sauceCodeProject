.class public final Lcom/helpshift/support/HSReview;
.super Landroidx/fragment/app/FragmentActivity;
.source "HSReview.java"


# instance fields
.field private flowList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 18
    invoke-direct {p0}, Landroidx/fragment/app/FragmentActivity;-><init>()V

    return-void
.end method


# virtual methods
.method protected attachBaseContext(Landroid/content/Context;)V
    .locals 0

    .line 52
    invoke-static {p1}, Lcom/helpshift/util/ApplicationUtil;->getContextWithUpdatedLocaleLegacy(Landroid/content/Context;)Landroid/content/Context;

    move-result-object p1

    invoke-super {p0, p1}, Landroidx/fragment/app/FragmentActivity;->attachBaseContext(Landroid/content/Context;)V

    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 2

    .line 24
    invoke-super {p0, p1}, Landroidx/fragment/app/FragmentActivity;->onCreate(Landroid/os/Bundle;)V

    .line 26
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object p1

    iget-object p1, p1, Lcom/helpshift/model/InfoModelFactory;->sdkInfoModel:Lcom/helpshift/model/SdkInfoModel;

    invoke-virtual {p1}, Lcom/helpshift/model/SdkInfoModel;->getTheme()Ljava/lang/Integer;

    move-result-object p1

    .line 28
    invoke-static {p0, p1}, Lcom/helpshift/util/AssetsUtil;->resourceExists(Landroid/content/Context;Ljava/lang/Integer;)Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    goto :goto_0

    :cond_0
    sget p1, Lcom/helpshift/R$style;->Helpshift_Theme_Base:I

    .line 29
    :goto_0
    invoke-virtual {p0, p1}, Lcom/helpshift/support/HSReview;->setTheme(I)V

    .line 31
    new-instance p1, Landroid/view/View;

    invoke-direct {p1, p0}, Landroid/view/View;-><init>(Landroid/content/Context;)V

    invoke-virtual {p0, p1}, Lcom/helpshift/support/HSReview;->setContentView(Landroid/view/View;)V

    .line 35
    invoke-static {}, Lcom/helpshift/support/flows/CustomContactUsFlowListHolder;->getFlowList()Ljava/util/List;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/HSReview;->flowList:Ljava/util/List;

    const/4 p1, 0x0

    .line 36
    invoke-static {p1}, Lcom/helpshift/support/flows/CustomContactUsFlowListHolder;->setFlowList(Ljava/util/List;)V

    .line 38
    invoke-virtual {p0}, Lcom/helpshift/support/HSReview;->getSupportFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object p1

    .line 39
    new-instance v0, Lcom/helpshift/support/HSReviewFragment;

    invoke-direct {v0}, Lcom/helpshift/support/HSReviewFragment;-><init>()V

    const-string v1, "hs__review_dialog"

    .line 40
    invoke-virtual {v0, p1, v1}, Lcom/helpshift/support/HSReviewFragment;->show(Landroidx/fragment/app/FragmentManager;Ljava/lang/String;)V

    return-void
.end method

.method protected onDestroy()V
    .locals 1

    .line 45
    invoke-super {p0}, Landroidx/fragment/app/FragmentActivity;->onDestroy()V

    .line 46
    iget-object v0, p0, Lcom/helpshift/support/HSReview;->flowList:Ljava/util/List;

    invoke-static {v0}, Lcom/helpshift/support/flows/CustomContactUsFlowListHolder;->setFlowList(Ljava/util/List;)V

    .line 47
    invoke-static {}, Lcom/helpshift/util/ApplicationUtil;->restoreApplicationLocale()V

    return-void
.end method
