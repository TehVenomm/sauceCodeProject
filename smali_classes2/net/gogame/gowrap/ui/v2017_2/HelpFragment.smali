.class public Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;
.super Landroid/app/Fragment;
.source "HelpFragment.java"


# instance fields
.field private uiContext:Lnet/gogame/gowrap/ui/UIContext;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 17
    invoke-direct {p0}, Landroid/app/Fragment;-><init>()V

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;)Lnet/gogame/gowrap/ui/UIContext;
    .locals 0

    .line 17
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    return-object p0
.end method


# virtual methods
.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1

    .line 23
    sget p3, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_v2017_2_fragment_help:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 26
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;->getActivity()Landroid/app/Activity;

    move-result-object p2

    instance-of p2, p2, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz p2, :cond_0

    .line 27
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;->getActivity()Landroid/app/Activity;

    move-result-object p2

    check-cast p2, Lnet/gogame/gowrap/ui/UIContext;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    .line 30
    :cond_0
    sget p2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_faq_button:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    .line 31
    new-instance p3, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$1;

    invoke-direct {p3, p0}, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$1;-><init>(Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;)V

    invoke-virtual {p2, p3}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 39
    sget p2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_form_button:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    .line 40
    new-instance p3, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$2;

    invoke-direct {p3, p0}, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$2;-><init>(Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;)V

    invoke-virtual {p2, p3}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 48
    sget p2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_chat_button:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    .line 49
    new-instance p3, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$3;

    invoke-direct {p3, p0}, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$3;-><init>(Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;)V

    invoke-virtual {p2, p3}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-object p1
.end method
