.class Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$2;
.super Ljava/lang/Object;
.source "AdminImageAttachmentMessageDM.java"

# interfaces
.implements Lcom/helpshift/downloader/SupportDownloadStateChangeListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->handleClick(Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;)V
    .locals 0

    .line 152
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onFailure(Ljava/lang/String;)V
    .locals 1

    .line 155
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;->THUMBNAIL_DOWNLOADED:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;

    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;)V

    return-void
.end method

.method public onProgressChange(Ljava/lang/String;I)V
    .locals 0

    .line 168
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    invoke-static {p1, p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->access$002(Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;I)I

    .line 169
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->notifyUpdated()V

    return-void
.end method

.method public onSuccess(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 160
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    iput-object p2, p1, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->filePath:Ljava/lang/String;

    .line 161
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p1

    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    .line 162
    invoke-interface {p1, p2}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 163
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    sget-object p2, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;->IMAGE_DOWNLOADED:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;)V

    return-void
.end method
