.class Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$1;
.super Ljava/lang/Object;
.source "AdminImageAttachmentMessageDM.java"

# interfaces
.implements Lcom/helpshift/downloader/SupportDownloadStateChangeListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->downloadThumbnailImage(Lcom/helpshift/common/platform/Platform;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

.field final synthetic val$platform:Lcom/helpshift/common/platform/Platform;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;Lcom/helpshift/common/platform/Platform;)V
    .locals 0

    .line 116
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$1;->val$platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onFailure(Ljava/lang/String;)V
    .locals 1

    .line 119
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;->DOWNLOAD_NOT_STARTED:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;

    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;)V

    return-void
.end method

.method public onProgressChange(Ljava/lang/String;I)V
    .locals 0

    return-void
.end method

.method public onSuccess(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 124
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    iput-object p2, p1, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->thumbnailFilePath:Ljava/lang/String;

    .line 125
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$1;->val$platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p1

    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    .line 126
    invoke-interface {p1, p2}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 127
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    sget-object p2, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;->THUMBNAIL_DOWNLOADED:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;)V

    return-void
.end method
