.class public Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/MessageDM;
.source "AdminMessageDM.java"


# direct methods
.method public constructor <init>(Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;)V
    .locals 8

    .line 19
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->body:Ljava/lang/String;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->getCreatedAt()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->getEpochCreatedAtTime()J

    move-result-wide v3

    iget-object v5, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->authorName:Ljava/lang/String;

    sget-object v7, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_TEXT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    const/4 v6, 0x1

    move-object v0, p0

    invoke-direct/range {v0 .. v7}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 21
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->serverId:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->serverId:Ljava/lang/String;

    .line 22
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->conversationLocalId:Ljava/lang/Long;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 23
    iget-boolean p1, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->shouldShowAgentNameForConversation:Z

    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->shouldShowAgentNameForConversation:Z

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;)V
    .locals 8

    .line 8
    sget-object v7, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_TEXT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    const/4 v6, 0x1

    move-object v0, p0

    move-object v1, p2

    move-object v2, p3

    move-wide v3, p4

    move-object v5, p6

    invoke-direct/range {v0 .. v7}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 9
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->serverId:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V
    .locals 8

    const/4 v6, 0x1

    move-object v0, p0

    move-object v1, p2

    move-object v2, p3

    move-wide v3, p4

    move-object v5, p6

    move-object v7, p7

    .line 14
    invoke-direct/range {v0 .. v7}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 15
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->serverId:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public isUISupportedMessage()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method
