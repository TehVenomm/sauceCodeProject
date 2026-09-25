.class Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$1;
.super Ljava/lang/Object;
.source "AdminSuggestionsMessageViewDataBinder.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->bind(Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;

.field final synthetic val$faq:Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;

.field final synthetic val$message:Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;)V
    .locals 0

    .line 90
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    iput-object p3, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$1;->val$faq:Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 3

    .line 93
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    iget-object v1, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$1;->val$faq:Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;->publishId:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$1;->val$faq:Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;->language:Ljava/lang/String;

    .line 94
    invoke-interface {p1, v0, v1, v2}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;->onAdminSuggestedQuestionSelected(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
