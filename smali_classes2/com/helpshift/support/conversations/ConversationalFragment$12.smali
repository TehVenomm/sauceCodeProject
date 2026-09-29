.class Lcom/helpshift/support/conversations/ConversationalFragment$12;
.super Ljava/lang/Object;
.source "ConversationalFragment.java"

# interfaces
.implements Lcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/ConversationalFragment;->onAdminSuggestedQuestionSelected(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Ljava/lang/String;Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/ConversationalFragment;

.field final synthetic val$messageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

.field final synthetic val$questionPublishId:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/ConversationalFragment;Lcom/helpshift/conversation/activeconversation/message/MessageDM;Ljava/lang/String;)V
    .locals 0

    .line 421
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment$12;->this$0:Lcom/helpshift/support/conversations/ConversationalFragment;

    iput-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragment$12;->val$messageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    iput-object p3, p0, Lcom/helpshift/support/conversations/ConversationalFragment$12;->val$questionPublishId:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onQuestionRead(Ljava/lang/String;)V
    .locals 3

    .line 424
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment$12;->this$0:Lcom/helpshift/support/conversations/ConversationalFragment;

    iget-object v0, v0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment$12;->val$messageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    iget-object v2, p0, Lcom/helpshift/support/conversations/ConversationalFragment$12;->val$questionPublishId:Ljava/lang/String;

    invoke-virtual {v0, v1, p1, v2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleAdminSuggestedQuestionRead(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
