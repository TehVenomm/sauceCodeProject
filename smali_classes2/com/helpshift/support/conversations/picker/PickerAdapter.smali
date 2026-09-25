.class public Lcom/helpshift/support/conversations/picker/PickerAdapter;
.super Landroidx/recyclerview/widget/RecyclerView$Adapter;
.source "PickerAdapter.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/recyclerview/widget/RecyclerView$Adapter<",
        "Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;",
        ">;"
    }
.end annotation


# instance fields
.field conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

.field private options:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/OptionUIModel;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Ljava/util/List;Lcom/helpshift/support/conversations/ConversationalFragmentRouter;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/OptionUIModel;",
            ">;",
            "Lcom/helpshift/support/conversations/ConversationalFragmentRouter;",
            ")V"
        }
    .end annotation

    .line 27
    invoke-direct {p0}, Landroidx/recyclerview/widget/RecyclerView$Adapter;-><init>()V

    .line 28
    iput-object p1, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter;->options:Ljava/util/List;

    .line 29
    iput-object p2, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    return-void
.end method

.method static synthetic access$200(Lcom/helpshift/support/conversations/picker/PickerAdapter;)Ljava/util/List;
    .locals 0

    .line 23
    iget-object p0, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter;->options:Ljava/util/List;

    return-object p0
.end method


# virtual methods
.method public dispatchUpdates(Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/OptionUIModel;",
            ">;)V"
        }
    .end annotation

    .line 73
    iget-object v0, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter;->options:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->clear()V

    .line 74
    iget-object v0, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter;->options:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    .line 75
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/picker/PickerAdapter;->notifyDataSetChanged()V

    return-void
.end method

.method public getItemCount()I
    .locals 1

    .line 69
    iget-object v0, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter;->options:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public bridge synthetic onBindViewHolder(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V
    .locals 0
    .param p1    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 23
    check-cast p1, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/picker/PickerAdapter;->onBindViewHolder(Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;I)V

    return-void
.end method

.method public onBindViewHolder(Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;I)V
    .locals 7
    .param p1    # Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 42
    iget-object v0, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter;->options:Ljava/util/List;

    invoke-interface {v0, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lcom/helpshift/conversation/viewmodel/OptionUIModel;

    .line 43
    iget-object v0, p2, Lcom/helpshift/conversation/viewmodel/OptionUIModel;->option:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->title:Ljava/lang/String;

    .line 45
    iget-object v1, p2, Lcom/helpshift/conversation/viewmodel/OptionUIModel;->titleHighlightInfo:Ljava/util/List;

    invoke-static {v1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 47
    invoke-static {p1}, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->access$000(Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;)Landroid/widget/TextView;

    move-result-object p2

    invoke-virtual {p2, v0}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    goto :goto_1

    .line 51
    :cond_0
    invoke-static {p1}, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->access$000(Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;)Landroid/widget/TextView;

    move-result-object v1

    invoke-virtual {v1}, Landroid/widget/TextView;->getContext()Landroid/content/Context;

    move-result-object v1

    sget v2, Lcom/helpshift/R$attr;->hs__searchHighlightColor:I

    invoke-static {v1, v2}, Lcom/helpshift/util/Styles;->getColor(Landroid/content/Context;I)I

    move-result v1

    .line 53
    new-instance v2, Landroid/text/SpannableString;

    invoke-direct {v2, v0}, Landroid/text/SpannableString;-><init>(Ljava/lang/CharSequence;)V

    .line 54
    iget-object p2, p2, Lcom/helpshift/conversation/viewmodel/OptionUIModel;->titleHighlightInfo:Ljava/util/List;

    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/viewmodel/HSRange;

    .line 55
    new-instance v4, Landroid/text/style/BackgroundColorSpan;

    invoke-direct {v4, v1}, Landroid/text/style/BackgroundColorSpan;-><init>(I)V

    iget v5, v3, Lcom/helpshift/conversation/viewmodel/HSRange;->index:I

    iget v6, v3, Lcom/helpshift/conversation/viewmodel/HSRange;->index:I

    iget v3, v3, Lcom/helpshift/conversation/viewmodel/HSRange;->length:I

    add-int/2addr v6, v3

    const/16 v3, 0x21

    invoke-interface {v2, v4, v5, v6, v3}, Landroid/text/Spannable;->setSpan(Ljava/lang/Object;III)V

    goto :goto_0

    .line 60
    :cond_1
    invoke-static {p1}, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->access$000(Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;)Landroid/widget/TextView;

    move-result-object p2

    invoke-virtual {p2, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 62
    :goto_1
    invoke-static {p1}, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->access$000(Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;)Landroid/widget/TextView;

    move-result-object p2

    invoke-virtual {p2}, Landroid/widget/TextView;->getContext()Landroid/content/Context;

    move-result-object p2

    .line 63
    sget v1, Lcom/helpshift/R$string;->hs__picker_option_list_item_voice_over:I

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object v0, v2, v3

    invoke-virtual {p2, v1, v2}, Landroid/content/Context;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    .line 64
    invoke-static {p1}, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->access$100(Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;)Landroid/view/View;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/view/View;->setContentDescription(Ljava/lang/CharSequence;)V

    return-void
.end method

.method public bridge synthetic onCreateViewHolder(Landroid/view/ViewGroup;I)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0
    .param p1    # Landroid/view/ViewGroup;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .line 23
    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/picker/PickerAdapter;->onCreateViewHolder(Landroid/view/ViewGroup;I)Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public onCreateViewHolder(Landroid/view/ViewGroup;I)Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;
    .locals 2
    .param p1    # Landroid/view/ViewGroup;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .line 35
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object p2

    invoke-static {p2}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p2

    sget v0, Lcom/helpshift/R$layout;->hs__picker_option:I

    const/4 v1, 0x0

    .line 36
    invoke-virtual {p2, v0, p1, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 37
    new-instance p2, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;

    invoke-direct {p2, p0, p1}, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;-><init>(Lcom/helpshift/support/conversations/picker/PickerAdapter;Landroid/view/View;)V

    return-object p2
.end method
