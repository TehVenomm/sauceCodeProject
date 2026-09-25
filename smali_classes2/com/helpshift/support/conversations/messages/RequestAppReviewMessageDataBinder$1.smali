.class Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$1;
.super Ljava/lang/Object;
.source "RequestAppReviewMessageDataBinder.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;->bind(Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;

.field final synthetic val$message:Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V
    .locals 0

    .line 47
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 50
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    if-eqz p1, :cond_0

    .line 51
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;

    invoke-interface {p1, v0}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;->handleReplyReviewButtonClick(Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V

    :cond_0
    return-void
.end method
