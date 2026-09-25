.class synthetic Lnet/gogame/chat/chatbot/ChatBotChatContext$2;
.super Ljava/lang/Object;
.source "ChatBotChatContext.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/chat/chatbot/ChatBotChatContext;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$net$gogame$chat$chatbot$ChatBotChatContext$ChatLog$Type:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 122
    invoke-static {}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->values()[Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$2;->$SwitchMap$net$gogame$chat$chatbot$ChatBotChatContext$ChatLog$Type:[I

    :try_start_0
    sget-object v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$2;->$SwitchMap$net$gogame$chat$chatbot$ChatBotChatContext$ChatLog$Type:[I

    sget-object v1, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_VISITOR:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    invoke-virtual {v1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$2;->$SwitchMap$net$gogame$chat$chatbot$ChatBotChatContext$ChatLog$Type:[I

    sget-object v1, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_AGENT:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    invoke-virtual {v1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    :try_start_2
    sget-object v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$2;->$SwitchMap$net$gogame$chat$chatbot$ChatBotChatContext$ChatLog$Type:[I

    sget-object v1, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_SYSTEM:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    invoke-virtual {v1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->ordinal()I

    move-result v1

    const/4 v2, 0x3

    aput v2, v0, v1
    :try_end_2
    .catch Ljava/lang/NoSuchFieldError; {:try_start_2 .. :try_end_2} :catch_2

    :catch_2
    return-void
.end method
