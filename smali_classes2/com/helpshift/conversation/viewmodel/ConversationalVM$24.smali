.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$24;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sendScreenShot(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

.field final synthetic val$imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

.field final synthetic val$referId:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V
    .locals 0

    .line 1607
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$24;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$24;->val$imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iput-object p3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$24;->val$referId:Ljava/lang/String;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 1610
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$24;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$24;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 1611
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$24;->val$imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$24;->val$referId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2, v3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendScreenshot(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V

    return-void
.end method
