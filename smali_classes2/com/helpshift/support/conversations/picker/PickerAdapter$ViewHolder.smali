.class Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "PickerAdapter.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/picker/PickerAdapter;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "ViewHolder"
.end annotation


# instance fields
.field private final layoutView:Landroid/view/View;

.field private final optionView:Landroid/widget/TextView;

.field final synthetic this$0:Lcom/helpshift/support/conversations/picker/PickerAdapter;


# direct methods
.method public constructor <init>(Lcom/helpshift/support/conversations/picker/PickerAdapter;Landroid/view/View;)V
    .locals 0

    .line 82
    iput-object p1, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->this$0:Lcom/helpshift/support/conversations/picker/PickerAdapter;

    .line 83
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 84
    iget-object p1, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->itemView:Landroid/view/View;

    sget p2, Lcom/helpshift/R$id;->hs__option:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->optionView:Landroid/widget/TextView;

    .line 85
    iget-object p1, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->itemView:Landroid/view/View;

    sget p2, Lcom/helpshift/R$id;->option_list_item_layout:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->layoutView:Landroid/view/View;

    .line 86
    iget-object p1, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->layoutView:Landroid/view/View;

    invoke-virtual {p1, p0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;)Landroid/widget/TextView;
    .locals 0

    .line 78
    iget-object p0, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->optionView:Landroid/widget/TextView;

    return-object p0
.end method

.method static synthetic access$100(Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;)Landroid/view/View;
    .locals 0

    .line 78
    iget-object p0, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->layoutView:Landroid/view/View;

    return-object p0
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    .line 91
    iget-object p1, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->this$0:Lcom/helpshift/support/conversations/picker/PickerAdapter;

    iget-object p1, p1, Lcom/helpshift/support/conversations/picker/PickerAdapter;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    if-eqz p1, :cond_0

    .line 92
    iget-object p1, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->this$0:Lcom/helpshift/support/conversations/picker/PickerAdapter;

    iget-object p1, p1, Lcom/helpshift/support/conversations/picker/PickerAdapter;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    iget-object v0, p0, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->this$0:Lcom/helpshift/support/conversations/picker/PickerAdapter;

    invoke-static {v0}, Lcom/helpshift/support/conversations/picker/PickerAdapter;->access$200(Lcom/helpshift/support/conversations/picker/PickerAdapter;)Ljava/util/List;

    move-result-object v0

    invoke-virtual {p0}, Lcom/helpshift/support/conversations/picker/PickerAdapter$ViewHolder;->getAdapterPosition()I

    move-result v1

    invoke-interface {v0, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/viewmodel/OptionUIModel;

    const/4 v1, 0x0

    invoke-interface {p1, v0, v1}, Lcom/helpshift/support/conversations/ConversationalFragmentRouter;->handleOptionSelectedForPicker(Lcom/helpshift/conversation/viewmodel/OptionUIModel;Z)V

    :cond_0
    return-void
.end method
