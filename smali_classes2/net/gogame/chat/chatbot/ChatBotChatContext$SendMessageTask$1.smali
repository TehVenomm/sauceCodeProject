.class Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;
.super Ljava/lang/Object;
.source "ChatBotChatContext.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;->send(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;

.field final synthetic val$agentAvatarUri:Ljava/lang/String;

.field final synthetic val$agentDisplayName:Ljava/lang/String;

.field final synthetic val$agentId:Ljava/lang/String;

.field final synthetic val$responseMessages:Ljava/util/List;

.field final synthetic val$timestamp:J


# direct methods
.method constructor <init>(Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;Ljava/util/List;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 282
    iput-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->this$1:Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;

    iput-object p2, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->val$responseMessages:Ljava/util/List;

    iput-wide p3, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->val$timestamp:J

    iput-object p5, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->val$agentId:Ljava/lang/String;

    iput-object p6, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->val$agentDisplayName:Ljava/lang/String;

    iput-object p7, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->val$agentAvatarUri:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 5

    .line 286
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->val$responseMessages:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 287
    new-instance v2, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;

    invoke-direct {v2}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;-><init>()V

    .line 288
    sget-object v3, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_AGENT:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    invoke-virtual {v2, v3}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->setType(Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;)V

    .line 289
    iget-wide v3, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->val$timestamp:J

    invoke-virtual {v2, v3, v4}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->setTimestamp(J)V

    .line 290
    iget-object v3, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->val$agentId:Ljava/lang/String;

    invoke-virtual {v2, v3}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->setAgentId(Ljava/lang/String;)V

    .line 291
    iget-object v3, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->val$agentDisplayName:Ljava/lang/String;

    invoke-virtual {v2, v3}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->setAgentDisplayName(Ljava/lang/String;)V

    .line 292
    iget-object v3, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->val$agentAvatarUri:Ljava/lang/String;

    invoke-virtual {v2, v3}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->setAgentAvatarUri(Ljava/lang/String;)V

    .line 293
    invoke-virtual {v2, v1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->setMessage(Ljava/lang/String;)V

    .line 294
    iget-object v1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->this$1:Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;

    iget-object v1, v1, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;->this$0:Lnet/gogame/chat/chatbot/ChatBotChatContext;

    invoke-static {v1}, Lnet/gogame/chat/chatbot/ChatBotChatContext;->access$400(Lnet/gogame/chat/chatbot/ChatBotChatContext;)Ljava/util/List;

    move-result-object v1

    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 296
    :cond_0
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;->this$1:Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;

    iget-object v0, v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;->this$0:Lnet/gogame/chat/chatbot/ChatBotChatContext;

    invoke-virtual {v0}, Lnet/gogame/chat/chatbot/ChatBotChatContext;->notifyDataSetChanged()V

    return-void
.end method
