.class Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$1;
.super Ljava/lang/Object;
.source "RequestScreenshotMessageDataBinder.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;->bind(Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;

.field final synthetic val$message:Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;)V
    .locals 0

    .line 45
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 48
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->isAttachmentButtonClickable()Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    if-eqz p1, :cond_0

    .line 49
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    invoke-interface {p1, v0}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;->launchImagePicker(Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;)V

    :cond_0
    return-void
.end method
