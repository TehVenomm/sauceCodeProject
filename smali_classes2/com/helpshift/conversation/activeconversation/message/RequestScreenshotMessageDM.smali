.class public Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/MessageDM;
.source "RequestScreenshotMessageDM.java"


# instance fields
.field public isAnswered:Z

.field private isAttachmentButtonClickable:Z


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Z)V
    .locals 8

    .line 14
    sget-object v7, Lcom/helpshift/conversation/activeconversation/message/MessageType;->REQUESTED_SCREENSHOT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    const/4 v6, 0x1

    move-object v0, p0

    move-object v1, p2

    move-object v2, p3

    move-wide v3, p4

    move-object v5, p6

    invoke-direct/range {v0 .. v7}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 15
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->serverId:Ljava/lang/String;

    .line 16
    iput-boolean p7, p0, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->isAnswered:Z

    const/4 p1, 0x1

    .line 17
    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->isAttachmentButtonClickable:Z

    return-void
.end method


# virtual methods
.method public isAttachmentButtonClickable()Z
    .locals 1

    .line 21
    iget-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->isAnswered:Z

    if-nez v0, :cond_0

    iget-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->isAttachmentButtonClickable:Z

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isUISupportedMessage()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method

.method public merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 37
    invoke-super {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 38
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    if-eqz v0, :cond_0

    .line 39
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    .line 40
    iget-boolean p1, p1, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->isAnswered:Z

    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->isAnswered:Z

    :cond_0
    return-void
.end method

.method public setAttachmentButtonClickable(Z)V
    .locals 0

    .line 25
    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->isAttachmentButtonClickable:Z

    .line 26
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->notifyUpdated()V

    return-void
.end method

.method public setIsAnswered(Lcom/helpshift/common/platform/Platform;Z)V
    .locals 0

    .line 30
    iput-boolean p2, p0, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->isAnswered:Z

    .line 31
    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p1

    invoke-interface {p1, p0}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 32
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->notifyUpdated()V

    return-void
.end method
