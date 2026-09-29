.class public Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/SystemMessageDM;
.source "SystemDividerMessageDM.java"


# instance fields
.field public final showDividerText:Z


# direct methods
.method public constructor <init>(Ljava/lang/String;JZ)V
    .locals 6

    const-string v1, ""

    .line 8
    sget-object v5, Lcom/helpshift/conversation/activeconversation/message/MessageType;->SYSTEM_DIVIDER:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    move-object v2, p1

    move-wide v3, p2

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/activeconversation/message/SystemMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 9
    iput-boolean p4, p0, Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;->showDividerText:Z

    return-void
.end method
