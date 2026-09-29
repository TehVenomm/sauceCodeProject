.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$23;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showListPicker(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

.field final synthetic val$optionInputMessageDM:Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V
    .locals 0

    .line 1511
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$23;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$23;->val$optionInputMessageDM:Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 1514
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$23;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$23;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-static {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->access$400(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ListPickerVM;->getAllOptions()Ljava/util/List;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$23;->val$optionInputMessageDM:Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->inputLabel:Ljava/lang/String;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$23;->val$optionInputMessageDM:Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-boolean v3, v3, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->required:Z

    iget-object v4, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$23;->val$optionInputMessageDM:Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    iget-object v4, v4, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v4, v4, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->skipLabel:Ljava/lang/String;

    invoke-interface {v0, v1, v2, v3, v4}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->showListPicker(Ljava/util/List;Ljava/lang/String;ZLjava/lang/String;)V

    return-void
.end method
