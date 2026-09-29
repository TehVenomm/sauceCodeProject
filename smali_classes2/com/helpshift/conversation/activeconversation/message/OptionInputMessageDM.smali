.class public Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;
.source "OptionInputMessageDM.java"


# instance fields
.field public final input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

.field public final referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;


# direct methods
.method public constructor <init>(Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;)V
    .locals 7

    .line 22
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->body:Ljava/lang/String;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->getCreatedAt()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->getEpochCreatedAtTime()J

    move-result-wide v3

    iget-object v5, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->authorName:Ljava/lang/String;

    sget-object v6, Lcom/helpshift/conversation/activeconversation/message/MessageType;->OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 24
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->serverId:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->serverId:Ljava/lang/String;

    .line 25
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    .line 26
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    .line 27
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->conversationLocalId:Ljava/lang/Long;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->conversationLocalId:Ljava/lang/Long;

    return-void
.end method

.method public constructor <init>(Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;)V
    .locals 7

    .line 13
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->body:Ljava/lang/String;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->getCreatedAt()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->getEpochCreatedAtTime()J

    move-result-wide v3

    iget-object v5, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->authorName:Ljava/lang/String;

    sget-object v6, Lcom/helpshift/conversation/activeconversation/message/MessageType;->OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 15
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->serverId:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->serverId:Ljava/lang/String;

    .line 16
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    .line 17
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    .line 18
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->conversationLocalId:Ljava/lang/Long;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->conversationLocalId:Ljava/lang/Long;

    return-void
.end method
