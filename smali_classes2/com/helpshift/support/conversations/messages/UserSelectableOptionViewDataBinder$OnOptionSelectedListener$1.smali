.class Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener$1;
.super Ljava/lang/Object;
.source "UserSelectableOptionViewDataBinder.java"

# interfaces
.implements Landroid/view/animation/Animation$AnimationListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;->onClick(Landroid/view/View;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;

.field final synthetic val$clickedView:Landroid/widget/TextView;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;Landroid/widget/TextView;)V
    .locals 0

    .line 149
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener$1;->this$1:Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener$1;->val$clickedView:Landroid/widget/TextView;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onAnimationEnd(Landroid/view/animation/Animation;)V
    .locals 3

    .line 156
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener$1;->this$1:Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;->viewHolder:Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsMessageView:Landroid/widget/LinearLayout;

    const/16 v0, 0x8

    invoke-virtual {p1, v0}, Landroid/widget/LinearLayout;->setVisibility(I)V

    .line 157
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener$1;->this$1:Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    if-eqz p1, :cond_0

    .line 159
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener$1;->this$1:Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener$1;->this$1:Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;

    iget-object v0, v0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;->message:Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    iget-object v1, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener$1;->val$clickedView:Landroid/widget/TextView;

    invoke-virtual {v1}, Landroid/widget/TextView;->getTag()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    iget-object v2, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener$1;->this$1:Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;

    iget-boolean v2, v2, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;->isSkip:Z

    invoke-interface {p1, v0, v1, v2}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;->handleOptionSelected(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V

    :cond_0
    return-void
.end method

.method public onAnimationRepeat(Landroid/view/animation/Animation;)V
    .locals 0

    return-void
.end method

.method public onAnimationStart(Landroid/view/animation/Animation;)V
    .locals 0

    return-void
.end method
