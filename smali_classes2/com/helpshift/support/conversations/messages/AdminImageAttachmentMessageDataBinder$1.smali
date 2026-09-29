.class Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$1;
.super Ljava/lang/Object;
.source "AdminImageAttachmentMessageDataBinder.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->bind(Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;

.field final synthetic val$message:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;)V
    .locals 0

    .line 103
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 106
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    if-eqz p1, :cond_0

    .line 107
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    invoke-interface {p1, v0}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;->handleAdminImageAttachmentMessageClick(Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;)V

    :cond_0
    return-void
.end method
