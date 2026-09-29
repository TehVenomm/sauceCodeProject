.class Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$1;
.super Ljava/lang/Object;
.source "AdminMessageViewDataBinder.java"

# interfaces
.implements Lcom/helpshift/util/HSLinkify$LinkClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;->bind(Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;

.field final synthetic val$message:Lcom/helpshift/conversation/activeconversation/message/MessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 51
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onLinkClicked(Ljava/lang/String;)V
    .locals 2

    .line 54
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;

    iget-object v0, v0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    if-eqz v0, :cond_0

    .line 55
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;

    iget-object v0, v0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    iget-object v1, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-interface {v0, p1, v1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;->onAdminMessageLinkClicked(Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    :cond_0
    return-void
.end method
