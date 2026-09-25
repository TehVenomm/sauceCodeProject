.class public Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;
.super Ljava/lang/Object;
.source "ChatBotChatContext.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/chat/chatbot/ChatBotChatContext;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "ChatLog"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;
    }
.end annotation


# instance fields
.field private agentAvatarUri:Ljava/lang/String;

.field private agentDisplayName:Ljava/lang/String;

.field private agentId:Ljava/lang/String;

.field private message:Ljava/lang/String;

.field private timestamp:J

.field private type:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 153
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getAgentAvatarUri()Ljava/lang/String;
    .locals 1

    .line 195
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->agentAvatarUri:Ljava/lang/String;

    return-object v0
.end method

.method public getAgentDisplayName()Ljava/lang/String;
    .locals 1

    .line 187
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->agentDisplayName:Ljava/lang/String;

    return-object v0
.end method

.method public getAgentId()Ljava/lang/String;
    .locals 1

    .line 179
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->agentId:Ljava/lang/String;

    return-object v0
.end method

.method public getMessage()Ljava/lang/String;
    .locals 1

    .line 203
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->message:Ljava/lang/String;

    return-object v0
.end method

.method public getTimestamp()J
    .locals 2

    .line 163
    iget-wide v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->timestamp:J

    return-wide v0
.end method

.method public getType()Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;
    .locals 1

    .line 171
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->type:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    return-object v0
.end method

.method public setAgentAvatarUri(Ljava/lang/String;)V
    .locals 0

    .line 199
    iput-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->agentAvatarUri:Ljava/lang/String;

    return-void
.end method

.method public setAgentDisplayName(Ljava/lang/String;)V
    .locals 0

    .line 191
    iput-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->agentDisplayName:Ljava/lang/String;

    return-void
.end method

.method public setAgentId(Ljava/lang/String;)V
    .locals 0

    .line 183
    iput-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->agentId:Ljava/lang/String;

    return-void
.end method

.method public setMessage(Ljava/lang/String;)V
    .locals 0

    .line 207
    iput-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->message:Ljava/lang/String;

    return-void
.end method

.method public setTimestamp(J)V
    .locals 0

    .line 167
    iput-wide p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->timestamp:J

    return-void
.end method

.method public setType(Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;)V
    .locals 0

    .line 175
    iput-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->type:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    return-void
.end method
