.class public Lcom/helpshift/conversation/activeconversation/message/SystemMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/MessageDM;
.source "SystemMessageDM.java"


# direct methods
.method constructor <init>(Ljava/lang/String;Ljava/lang/String;JLcom/helpshift/conversation/activeconversation/message/MessageType;)V
    .locals 8

    const-string v5, "mobile"

    const/4 v6, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-wide v3, p3

    move-object v7, p5

    .line 10
    invoke-direct/range {v0 .. v7}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/activeconversation/message/MessageType;)V

    return-void
.end method


# virtual methods
.method public isUISupportedMessage()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method
