.class Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$1;
.super Ljava/lang/Object;
.source "AdminAttachmentMessageDataBinder.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;->bind(Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;

.field final synthetic val$message:Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;)V
    .locals 0

    .line 73
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 76
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    if-eqz p1, :cond_0

    .line 77
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;

    invoke-interface {p1, v0}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;->handleGenericAttachmentMessageClick(Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;)V

    :cond_0
    return-void
.end method
