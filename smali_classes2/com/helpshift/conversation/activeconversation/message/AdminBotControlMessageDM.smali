.class public Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;
.source "AdminBotControlMessageDM.java"


# instance fields
.field public actionType:Ljava/lang/String;

.field public botInfo:Ljava/lang/String;

.field public hasNextBot:Z


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 9

    move-object v8, p0

    .line 26
    sget-object v7, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_BOT_CONTROL:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-wide v4, p4

    move-object v6, p6

    invoke-direct/range {v0 .. v7}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    move-object/from16 v0, p7

    .line 27
    iput-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->actionType:Ljava/lang/String;

    move-object/from16 v0, p8

    .line 28
    iput-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->botInfo:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public isUISupportedMessage()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 33
    invoke-super {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 34
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;

    if-eqz v0, :cond_0

    .line 35
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;

    .line 36
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->actionType:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->actionType:Ljava/lang/String;

    .line 37
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->botInfo:Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->botInfo:Ljava/lang/String;

    :cond_0
    return-void
.end method
