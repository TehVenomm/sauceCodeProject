.class public Lnet/gogame/chat/chatbot/ChatBotChatContext;
.super Lnet/gogame/chat/AbstractChatContext;
.source "ChatBotChatContext.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;,
        Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;
    }
.end annotation


# static fields
.field private static final DEBUG:Z = false

.field private static final DEFAULT_AGENT_AVATAR_URI:Ljava/lang/String; = null

.field private static final DEFAULT_AGENT_DISPLAY_NAME:Ljava/lang/String; = "Sarah"

.field private static final DEFAULT_AGENT_ID:Ljava/lang/String; = "default"

.field private static final SERVICE_URL:Ljava/lang/String; = "https://gw-chat.gogame.net/webchat/receive/"


# instance fields
.field private final activity:Landroid/app/Activity;

.field private final agentTypingEntry:Lnet/gogame/chat/AgentTypingEntry;

.field private final chatBotConfig:Lnet/gogame/chat/chatbot/ChatBotConfig;

.field private final chatLogs:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;",
            ">;"
        }
    .end annotation
.end field

.field private final guid:Ljava/lang/String;

.field private final viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>(Landroid/app/Activity;Lnet/gogame/chat/chatbot/ChatBotConfig;Lnet/gogame/chat/ChatAdapterViewFactory;)V
    .locals 2

    .line 49
    invoke-direct {p0}, Lnet/gogame/chat/AbstractChatContext;-><init>()V

    .line 44
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->chatLogs:Ljava/util/List;

    .line 45
    new-instance v0, Lnet/gogame/chat/AgentTypingEntry;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Lnet/gogame/chat/AgentTypingEntry;-><init>(Z)V

    iput-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->agentTypingEntry:Lnet/gogame/chat/AgentTypingEntry;

    .line 51
    iput-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->activity:Landroid/app/Activity;

    .line 52
    iput-object p2, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->chatBotConfig:Lnet/gogame/chat/chatbot/ChatBotConfig;

    .line 53
    iput-object p3, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 55
    invoke-virtual {p2}, Lnet/gogame/chat/chatbot/ChatBotConfig;->getGuid()Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_0

    invoke-virtual {p2}, Lnet/gogame/chat/chatbot/ChatBotConfig;->getGuid()Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    :cond_0
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    .line 56
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object p2

    invoke-virtual {p2}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, "_"

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide p2

    invoke-virtual {p1, p2, p3}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    :goto_0
    iput-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->guid:Ljava/lang/String;

    return-void
.end method

.method static synthetic access$100(Lnet/gogame/chat/chatbot/ChatBotChatContext;)Lnet/gogame/chat/chatbot/ChatBotConfig;
    .locals 0

    .line 32
    iget-object p0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->chatBotConfig:Lnet/gogame/chat/chatbot/ChatBotConfig;

    return-object p0
.end method

.method static synthetic access$200(Lnet/gogame/chat/chatbot/ChatBotChatContext;)Ljava/lang/String;
    .locals 0

    .line 32
    iget-object p0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->guid:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$300()Ljava/lang/String;
    .locals 1

    .line 32
    sget-object v0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->DEFAULT_AGENT_AVATAR_URI:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$400(Lnet/gogame/chat/chatbot/ChatBotChatContext;)Ljava/util/List;
    .locals 0

    .line 32
    iget-object p0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->chatLogs:Ljava/util/List;

    return-object p0
.end method

.method static synthetic access$500(Lnet/gogame/chat/chatbot/ChatBotChatContext;)Landroid/app/Activity;
    .locals 0

    .line 32
    iget-object p0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->activity:Landroid/app/Activity;

    return-object p0
.end method

.method static synthetic access$600(Lnet/gogame/chat/chatbot/ChatBotChatContext;Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;)V
    .locals 0

    .line 32
    invoke-direct {p0, p1}, Lnet/gogame/chat/chatbot/ChatBotChatContext;->addChatLog(Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;)V

    return-void
.end method

.method private addChatLog(Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;)V
    .locals 2

    .line 322
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->activity:Landroid/app/Activity;

    new-instance v1, Lnet/gogame/chat/chatbot/ChatBotChatContext$1;

    invoke-direct {v1, p0, p1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$1;-><init>(Lnet/gogame/chat/chatbot/ChatBotChatContext;Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method


# virtual methods
.method public getAgentTypingEntry()Lnet/gogame/chat/AgentTypingEntry;
    .locals 1

    .line 89
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->agentTypingEntry:Lnet/gogame/chat/AgentTypingEntry;

    return-object v0
.end method

.method public getChatEntry(I)Ljava/lang/Object;
    .locals 1

    if-ltz p1, :cond_0

    .line 81
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->chatLogs:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-ge p1, v0, :cond_0

    .line 82
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->chatLogs:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    :cond_0
    const/4 p1, 0x0

    return-object p1
.end method

.method public getChatEntryCount()I
    .locals 1

    .line 76
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->chatLogs:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public getView(Ljava/lang/Object;ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 10

    .line 117
    instance-of v0, p1, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;

    if-eqz v0, :cond_3

    .line 118
    check-cast p1, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;

    .line 119
    invoke-virtual {p1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->getType()Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    move-result-object v0

    if-nez v0, :cond_0

    .line 120
    iget-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-virtual {p1, p3, p4}, Lnet/gogame/chat/ChatAdapterViewFactory;->getEmptyView(Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 122
    :cond_0
    sget-object v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$2;->$SwitchMap$net$gogame$chat$chatbot$ChatBotChatContext$ChatLog$Type:[I

    invoke-virtual {p1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->getType()Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    move-result-object v1

    invoke-virtual {v1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    .line 146
    iget-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-virtual {p1, p3, p4}, Lnet/gogame/chat/ChatAdapterViewFactory;->getEmptyView(Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 142
    :pswitch_0
    iget-object p2, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 143
    invoke-virtual {p1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->getMessage()Ljava/lang/String;

    move-result-object p1

    .line 142
    invoke-virtual {p2, p3, p4, p1}, Lnet/gogame/chat/ChatAdapterViewFactory;->getNotificationView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    return-object p1

    :pswitch_1
    const/4 v0, 0x0

    const/4 v1, 0x1

    if-nez p2, :cond_1

    :goto_0
    const/4 v6, 0x1

    goto :goto_1

    .line 132
    :cond_1
    iget-object v2, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->chatLogs:Ljava/util/List;

    sub-int/2addr p2, v1

    invoke-interface {v2, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;

    .line 133
    invoke-virtual {p2}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->getType()Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    move-result-object p2

    sget-object v2, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_AGENT:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    if-eq p2, v2, :cond_2

    goto :goto_0

    :cond_2
    const/4 v6, 0x0

    .line 137
    :goto_1
    iget-object v3, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 138
    invoke-virtual {p1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->getAgentDisplayName()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {p1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->getAgentAvatarUri()Ljava/lang/String;

    move-result-object v8

    .line 139
    invoke-virtual {p1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->getMessage()Ljava/lang/String;

    move-result-object v9

    move-object v4, p3

    move-object v5, p4

    .line 137
    invoke-virtual/range {v3 .. v9}, Lnet/gogame/chat/ChatAdapterViewFactory;->getAgentMessageView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 124
    :pswitch_2
    iget-object p2, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 125
    invoke-virtual {p1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->getMessage()Ljava/lang/String;

    move-result-object p1

    .line 124
    invoke-virtual {p2, p3, p4, p1}, Lnet/gogame/chat/ChatAdapterViewFactory;->getVisitorMessageView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    return-object p1

    :cond_3
    const/4 p1, 0x0

    return-object p1

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public isAttachmentSupported()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public send(Ljava/io/File;)V
    .locals 0

    return-void
.end method

.method public send(Ljava/lang/String;)V
    .locals 3

    .line 94
    invoke-static {p1}, Lorg/apache/commons/lang3/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 96
    new-instance v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;

    invoke-direct {v0}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;-><init>()V

    .line 97
    sget-object v1, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_VISITOR:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    invoke-virtual {v0, v1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->setType(Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;)V

    .line 98
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->setTimestamp(J)V

    .line 99
    invoke-virtual {v0, p1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->setMessage(Ljava/lang/String;)V

    .line 100
    invoke-direct {p0, v0}, Lnet/gogame/chat/chatbot/ChatBotChatContext;->addChatLog(Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;)V

    .line 101
    new-instance v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;

    const/4 v1, 0x0

    invoke-direct {v0, p0, v1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;-><init>(Lnet/gogame/chat/chatbot/ChatBotChatContext;Lnet/gogame/chat/chatbot/ChatBotChatContext$1;)V

    const/4 v1, 0x1

    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    aput-object p1, v1, v2

    invoke-virtual {v0, v1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    :cond_0
    return-void
.end method

.method public send(Lnet/gogame/chat/ChatContext$Rating;)V
    .locals 0

    return-void
.end method

.method public start()V
    .locals 4

    .line 61
    new-instance v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;

    const/4 v1, 0x0

    invoke-direct {v0, p0, v1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;-><init>(Lnet/gogame/chat/chatbot/ChatBotChatContext;Lnet/gogame/chat/chatbot/ChatBotChatContext$1;)V

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    check-cast v1, Ljava/lang/String;

    const/4 v3, 0x0

    aput-object v1, v2, v3

    invoke-virtual {v0, v2}, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    return-void
.end method

.method public stop()V
    .locals 0

    return-void
.end method
