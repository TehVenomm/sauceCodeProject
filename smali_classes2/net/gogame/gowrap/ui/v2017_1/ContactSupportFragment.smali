.class public Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment;
.super Landroid/app/Fragment;
.source "ContactSupportFragment.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/VipListener;


# instance fields
.field private chatButton:Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 17
    invoke-direct {p0}, Landroid/app/Fragment;-><init>()V

    return-void
.end method

.method private updateChatButton(ZZ)V
    .locals 1

    .line 69
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment;->chatButton:Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;

    if-nez p1, :cond_0

    if-nez p2, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->setMasked(Z)V

    return-void
.end method


# virtual methods
.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 2

    .line 24
    sget p3, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_fragment_contact_support:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 27
    invoke-virtual {p2}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object p2

    .line 29
    instance-of p3, p2, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz p3, :cond_0

    .line 30
    move-object p3, p2

    check-cast p3, Lnet/gogame/gowrap/ui/UIContext;

    goto :goto_0

    :cond_0
    const/4 p3, 0x0

    .line 34
    :goto_0
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_form_button:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    .line 35
    new-instance v1, Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment$1;

    invoke-direct {v1, p0, p3}, Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment$1;-><init>(Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment;Lnet/gogame/gowrap/ui/UIContext;)V

    invoke-virtual {v0, v1}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 45
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_chat_button:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment;->chatButton:Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;

    .line 47
    invoke-interface {p3}, Lnet/gogame/gowrap/ui/UIContext;->isVipChatEnabled()Z

    move-result v0

    sget-object v1, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isChatBotEnabled()Z

    move-result v1

    invoke-direct {p0, v0, v1}, Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment;->updateChatButton(ZZ)V

    .line 48
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment;->chatButton:Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment$2;

    invoke-direct {v1, p0, p3, p2}, Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment$2;-><init>(Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment;Lnet/gogame/gowrap/ui/UIContext;Landroid/content/Context;)V

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-object p1
.end method

.method public onDisableVipChat()V
    .locals 2

    .line 79
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isChatBotEnabled()Z

    move-result v0

    const/4 v1, 0x0

    invoke-direct {p0, v1, v0}, Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment;->updateChatButton(ZZ)V

    return-void
.end method

.method public onEnableVipChat()V
    .locals 2

    .line 74
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isChatBotEnabled()Z

    move-result v0

    const/4 v1, 0x1

    invoke-direct {p0, v1, v0}, Lnet/gogame/gowrap/ui/v2017_1/ContactSupportFragment;->updateChatButton(ZZ)V

    return-void
.end method
