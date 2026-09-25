.class Lnet/gogame/chat/chatbot/ChatBotChatContext$1;
.super Ljava/lang/Object;
.source "ChatBotChatContext.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/chatbot/ChatBotChatContext;->addChatLog(Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/chatbot/ChatBotChatContext;

.field final synthetic val$chatLog:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;


# direct methods
.method constructor <init>(Lnet/gogame/chat/chatbot/ChatBotChatContext;Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;)V
    .locals 0

    .line 322
    iput-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$1;->this$0:Lnet/gogame/chat/chatbot/ChatBotChatContext;

    iput-object p2, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$1;->val$chatLog:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 326
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$1;->this$0:Lnet/gogame/chat/chatbot/ChatBotChatContext;

    invoke-static {v0}, Lnet/gogame/chat/chatbot/ChatBotChatContext;->access$400(Lnet/gogame/chat/chatbot/ChatBotChatContext;)Ljava/util/List;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$1;->val$chatLog:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 327
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$1;->this$0:Lnet/gogame/chat/chatbot/ChatBotChatContext;

    invoke-virtual {v0}, Lnet/gogame/chat/chatbot/ChatBotChatContext;->notifyDataSetChanged()V

    return-void
.end method
