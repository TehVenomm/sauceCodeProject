.class Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;
.super Ljava/lang/Object;
.source "ConversationalFragmentRenderer.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->registerListeners()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V
    .locals 0

    .line 599
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    .line 602
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p1, p1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerHeaderSearchView:Landroid/widget/EditText;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/widget/EditText;->setVisibility(I)V

    .line 603
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p1, p1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerExpandedHeaderText:Landroid/widget/TextView;

    const/16 v1, 0x8

    invoke-virtual {p1, v1}, Landroid/widget/TextView;->setVisibility(I)V

    .line 604
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p1, p1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerSearchView:Landroid/widget/ImageView;

    invoke-virtual {p1, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 605
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p1, p1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerHeaderSearchView:Landroid/widget/EditText;

    invoke-virtual {p1}, Landroid/widget/EditText;->requestFocus()Z

    .line 606
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p1, p1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    invoke-virtual {p1, v0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->setDraggable(Z)V

    .line 607
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p1, p1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapseView:Landroid/widget/ImageView;

    invoke-virtual {p1, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 608
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p1, p1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBackView:Landroid/widget/ImageView;

    invoke-virtual {p1, v0}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 609
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p1, p1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object v1, v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerHeaderSearchView:Landroid/widget/EditText;

    invoke-static {p1, v1}, Lcom/helpshift/support/util/KeyboardUtil;->showKeyboard(Landroid/content/Context;Landroid/view/View;)V

    .line 610
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p1, p1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    invoke-virtual {p1, v0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->setDraggable(Z)V

    return-void
.end method
