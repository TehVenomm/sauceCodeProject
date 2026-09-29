.class public Lcom/helpshift/support/conversations/AuthenticationFailureFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "AuthenticationFailureFragment.java"


# static fields
.field public static final FRAGMENT_TAG:Ljava/lang/String; = "HSAuthenticationFailureFragment"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 13
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    return-void
.end method

.method public static newInstance()Lcom/helpshift/support/conversations/AuthenticationFailureFragment;
    .locals 1

    .line 18
    new-instance v0, Lcom/helpshift/support/conversations/AuthenticationFailureFragment;

    invoke-direct {v0}, Lcom/helpshift/support/conversations/AuthenticationFailureFragment;-><init>()V

    return-object v0
.end method


# virtual methods
.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1

    .line 24
    sget p3, Lcom/helpshift/R$layout;->hs__authentication_failure_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onResume()V
    .locals 1

    .line 37
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onResume()V

    .line 39
    sget v0, Lcom/helpshift/R$string;->hs__conversation_header:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/conversations/AuthenticationFailureFragment;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/conversations/AuthenticationFailureFragment;->setToolbarTitle(Ljava/lang/String;)V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 3
    .param p2    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 30
    sget v0, Lcom/helpshift/R$id;->info_icon:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    .line 31
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/AuthenticationFailureFragment;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v0}, Landroid/widget/ImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    const v2, 0x1010036

    invoke-static {v1, v0, v2}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 32
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method
