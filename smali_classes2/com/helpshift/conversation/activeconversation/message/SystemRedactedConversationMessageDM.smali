.class public Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/SystemMessageDM;
.source "SystemRedactedConversationMessageDM.java"


# instance fields
.field public contiguousRedactedConversationsCount:I


# direct methods
.method public constructor <init>(Ljava/lang/String;JI)V
    .locals 6

    const-string v1, ""

    .line 11
    sget-object v5, Lcom/helpshift/conversation/activeconversation/message/MessageType;->SYSTEM_CONVERSATION_REDACTED:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    move-object v2, p1

    move-wide v3, p2

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/activeconversation/message/SystemMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 12
    iput p4, p0, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;->contiguousRedactedConversationsCount:I

    return-void
.end method
