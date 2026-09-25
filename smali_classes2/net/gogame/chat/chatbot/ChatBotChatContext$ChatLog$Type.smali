.class final enum Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;
.super Ljava/lang/Enum;
.source "ChatBotChatContext.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4018
    name = "Type"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

.field public static final enum CHAT_MSG_AGENT:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

.field public static final enum CHAT_MSG_SYSTEM:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

.field public static final enum CHAT_MSG_VISITOR:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 211
    new-instance v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    const-string v1, "CHAT_MSG_VISITOR"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_VISITOR:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    .line 212
    new-instance v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    const-string v1, "CHAT_MSG_AGENT"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_AGENT:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    .line 213
    new-instance v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    const-string v1, "CHAT_MSG_SYSTEM"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_SYSTEM:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    const/4 v0, 0x3

    .line 210
    new-array v0, v0, [Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    sget-object v1, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_VISITOR:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    aput-object v1, v0, v2

    sget-object v1, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_AGENT:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    aput-object v1, v0, v3

    sget-object v1, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_SYSTEM:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    aput-object v1, v0, v4

    sput-object v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->$VALUES:[Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 210
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;
    .locals 1

    .line 210
    const-class v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    return-object p0
.end method

.method public static values()[Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;
    .locals 1

    .line 210
    sget-object v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->$VALUES:[Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    invoke-virtual {v0}, [Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    return-object v0
.end method
