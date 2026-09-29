.class public Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/SystemMessageDM;
.source "SystemPublishIdMessageDM.java"


# instance fields
.field public isFirstMessageInList:Z


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;JZ)V
    .locals 6

    .line 8
    sget-object v5, Lcom/helpshift/conversation/activeconversation/message/MessageType;->SYSTEM_PUBLISH_ID:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-wide v3, p3

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/activeconversation/message/SystemMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 9
    iput-boolean p5, p0, Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;->isFirstMessageInList:Z

    return-void
.end method
