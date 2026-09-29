.class public Lnet/gogame/chat/zopim/ZopimChatContext;
.super Lnet/gogame/chat/AbstractChatContext;
.source "ZopimChatContext.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/chat/zopim/ZopimChatContext$ChatTimeoutReceiver;,
        Lnet/gogame/chat/zopim/ZopimChatContext$ZopimOption;
    }
.end annotation


# static fields
.field private static hasSession:Z = false


# instance fields
.field private final activity:Landroidx/fragment/app/FragmentActivity;

.field private agentNick:Ljava/lang/String;

.field private final agentTypingEntry:Lnet/gogame/chat/AgentTypingEntry;

.field private agents:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/Agent;",
            ">;"
        }
    .end annotation
.end field

.field private final agentsObserver:Lcom/zopim/android/sdk/data/observers/AgentsObserver;

.field private final broadcastReceiver:Landroid/content/BroadcastReceiver;

.field private chat:Lcom/zopim/android/sdk/api/ChatApi;

.field private chatLogList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ">;"
        }
    .end annotation
.end field

.field private final chatLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

.field private final connectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

.field private profileIconToShowList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/Boolean;",
            ">;"
        }
    .end annotation
.end field

.field private final queuedFiles:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/io/File;",
            ">;"
        }
    .end annotation
.end field

.field private final sessionConfig:Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

.field private final uiContext:Lnet/gogame/chat/UIContext;

.field private final viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>(Landroidx/fragment/app/FragmentActivity;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;Lnet/gogame/chat/ChatAdapterViewFactory;Lnet/gogame/chat/UIContext;)V
    .locals 2

    .line 83
    invoke-direct {p0}, Lnet/gogame/chat/AbstractChatContext;-><init>()V

    .line 48
    new-instance v0, Lnet/gogame/chat/zopim/ZopimChatContext$ChatTimeoutReceiver;

    invoke-direct {v0, p0}, Lnet/gogame/chat/zopim/ZopimChatContext$ChatTimeoutReceiver;-><init>(Lnet/gogame/chat/zopim/ZopimChatContext;)V

    iput-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->broadcastReceiver:Landroid/content/BroadcastReceiver;

    .line 51
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chatLogList:Ljava/util/List;

    .line 52
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->profileIconToShowList:Ljava/util/List;

    const-string v0, ""

    .line 54
    iput-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agentNick:Ljava/lang/String;

    .line 55
    new-instance v0, Lnet/gogame/chat/AgentTypingEntry;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Lnet/gogame/chat/AgentTypingEntry;-><init>(Z)V

    iput-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agentTypingEntry:Lnet/gogame/chat/AgentTypingEntry;

    .line 56
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->queuedFiles:Ljava/util/List;

    .line 58
    new-instance v0, Lnet/gogame/chat/zopim/ZopimChatContext$1;

    invoke-direct {v0, p0}, Lnet/gogame/chat/zopim/ZopimChatContext$1;-><init>(Lnet/gogame/chat/zopim/ZopimChatContext;)V

    iput-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->connectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    .line 67
    new-instance v0, Lnet/gogame/chat/zopim/ZopimChatContext$2;

    invoke-direct {v0, p0}, Lnet/gogame/chat/zopim/ZopimChatContext$2;-><init>(Lnet/gogame/chat/zopim/ZopimChatContext;)V

    iput-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agentsObserver:Lcom/zopim/android/sdk/data/observers/AgentsObserver;

    .line 74
    new-instance v0, Lnet/gogame/chat/zopim/ZopimChatContext$3;

    invoke-direct {v0, p0}, Lnet/gogame/chat/zopim/ZopimChatContext$3;-><init>(Lnet/gogame/chat/zopim/ZopimChatContext;)V

    iput-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chatLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

    .line 85
    iput-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->activity:Landroidx/fragment/app/FragmentActivity;

    .line 86
    iput-object p2, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->sessionConfig:Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    .line 87
    iput-object p3, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 88
    iput-object p4, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->uiContext:Lnet/gogame/chat/UIContext;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/chat/zopim/ZopimChatContext;)V
    .locals 0

    .line 41
    invoke-direct {p0}, Lnet/gogame/chat/zopim/ZopimChatContext;->doNotifyDataSetChanged()V

    return-void
.end method

.method static synthetic access$102(Lnet/gogame/chat/zopim/ZopimChatContext;Ljava/util/List;)Ljava/util/List;
    .locals 0

    .line 41
    iput-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chatLogList:Ljava/util/List;

    return-object p1
.end method

.method static synthetic access$202(Lnet/gogame/chat/zopim/ZopimChatContext;Ljava/util/List;)Ljava/util/List;
    .locals 0

    .line 41
    iput-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->profileIconToShowList:Ljava/util/List;

    return-object p1
.end method

.method static synthetic access$302(Lnet/gogame/chat/zopim/ZopimChatContext;Ljava/util/Map;)Ljava/util/Map;
    .locals 0

    .line 41
    iput-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agents:Ljava/util/Map;

    return-object p1
.end method

.method private doNotifyDataSetChanged()V
    .locals 2

    .line 92
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->activity:Landroidx/fragment/app/FragmentActivity;

    new-instance v1, Lnet/gogame/chat/zopim/ZopimChatContext$4;

    invoke-direct {v1, p0}, Lnet/gogame/chat/zopim/ZopimChatContext$4;-><init>(Lnet/gogame/chat/zopim/ZopimChatContext;)V

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentActivity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method private log(Lcom/zopim/android/sdk/model/ChatLog;)V
    .locals 0

    return-void
.end method

.method private onOptionClickListener(Ljava/lang/String;)Landroid/view/View$OnClickListener;
    .locals 1

    .line 370
    new-instance v0, Lnet/gogame/chat/zopim/ZopimChatContext$7;

    invoke-direct {v0, p0, p1}, Lnet/gogame/chat/zopim/ZopimChatContext$7;-><init>(Lnet/gogame/chat/zopim/ZopimChatContext;Ljava/lang/String;)V

    return-object v0
.end method

.method private toOptions([Lcom/zopim/android/sdk/model/ChatLog$Option;)Ljava/util/List;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([",
            "Lcom/zopim/android/sdk/model/ChatLog$Option;",
            ")",
            "Ljava/util/List<",
            "Lnet/gogame/chat/ChatAdapterViewFactory$Option;",
            ">;"
        }
    .end annotation

    .line 318
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 319
    array-length v1, p1

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_0

    aget-object v3, p1, v2

    .line 320
    new-instance v4, Lnet/gogame/chat/zopim/ZopimChatContext$ZopimOption;

    invoke-direct {v4, v3}, Lnet/gogame/chat/zopim/ZopimChatContext$ZopimOption;-><init>(Lcom/zopim/android/sdk/model/ChatLog$Option;)V

    invoke-interface {v0, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_0
    return-object v0
.end method

.method private toRating(Lnet/gogame/chat/ChatContext$Rating;)Lcom/zopim/android/sdk/model/ChatLog$Rating;
    .locals 1

    if-nez p1, :cond_0

    .line 343
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNRATED:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    return-object p1

    .line 345
    :cond_0
    sget-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$net$gogame$chat$ChatContext$Rating:[I

    invoke-virtual {p1}, Lnet/gogame/chat/ChatContext$Rating;->ordinal()I

    move-result p1

    aget p1, v0, p1

    packed-switch p1, :pswitch_data_0

    .line 353
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNRATED:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    return-object p1

    .line 351
    :pswitch_0
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNRATED:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    return-object p1

    .line 349
    :pswitch_1
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$Rating;->BAD:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    return-object p1

    .line 347
    :pswitch_2
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$Rating;->GOOD:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    return-object p1

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private toRating(Lcom/zopim/android/sdk/model/ChatLog$Rating;)Lnet/gogame/chat/ChatContext$Rating;
    .locals 1

    if-nez p1, :cond_0

    .line 327
    sget-object p1, Lnet/gogame/chat/ChatContext$Rating;->UNRATED:Lnet/gogame/chat/ChatContext$Rating;

    return-object p1

    .line 329
    :cond_0
    sget-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Rating:[I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->ordinal()I

    move-result p1

    aget p1, v0, p1

    packed-switch p1, :pswitch_data_0

    .line 337
    sget-object p1, Lnet/gogame/chat/ChatContext$Rating;->UNRATED:Lnet/gogame/chat/ChatContext$Rating;

    return-object p1

    .line 335
    :pswitch_0
    sget-object p1, Lnet/gogame/chat/ChatContext$Rating;->UNRATED:Lnet/gogame/chat/ChatContext$Rating;

    return-object p1

    .line 333
    :pswitch_1
    sget-object p1, Lnet/gogame/chat/ChatContext$Rating;->BAD:Lnet/gogame/chat/ChatContext$Rating;

    return-object p1

    .line 331
    :pswitch_2
    sget-object p1, Lnet/gogame/chat/ChatContext$Rating;->GOOD:Lnet/gogame/chat/ChatContext$Rating;

    return-object p1

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public getAgentTypingEntry()Lnet/gogame/chat/AgentTypingEntry;
    .locals 2

    .line 178
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agents:Ljava/util/Map;

    if-eqz v0, :cond_0

    .line 179
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agents:Ljava/util/Map;

    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agentNick:Ljava/lang/String;

    invoke-interface {v0, v1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 180
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agents:Ljava/util/Map;

    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agentNick:Ljava/lang/String;

    invoke-interface {v0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/zopim/android/sdk/model/Agent;

    if-eqz v0, :cond_0

    .line 181
    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/Agent;->isTyping()Ljava/lang/Boolean;

    move-result-object v1

    if-eqz v1, :cond_0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/Agent;->isTyping()Ljava/lang/Boolean;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 186
    :goto_0
    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agentTypingEntry:Lnet/gogame/chat/AgentTypingEntry;

    invoke-virtual {v1, v0}, Lnet/gogame/chat/AgentTypingEntry;->setTyping(Z)V

    .line 187
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agentTypingEntry:Lnet/gogame/chat/AgentTypingEntry;

    return-object v0
.end method

.method public getChatEntry(I)Ljava/lang/Object;
    .locals 1

    .line 169
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chatLogList:Ljava/util/List;

    if-eqz v0, :cond_1

    if-ltz p1, :cond_1

    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chatLogList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-lt p1, v0, :cond_0

    goto :goto_0

    .line 172
    :cond_0
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chatLogList:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return-object p1
.end method

.method public getChatEntryCount()I
    .locals 1

    .line 161
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chatLogList:Ljava/util/List;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    .line 164
    :cond_0
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chatLogList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public getView(Ljava/lang/Object;ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 11

    .line 218
    instance-of v0, p1, Lcom/zopim/android/sdk/model/ChatLog;

    const/4 v1, 0x0

    if-eqz v0, :cond_7

    .line 219
    check-cast p1, Lcom/zopim/android/sdk/model/ChatLog;

    .line 220
    sget-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v2

    invoke-virtual {v2}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v2

    aget v0, v0, v2

    const/4 v2, 0x0

    const/4 v3, 0x1

    packed-switch v0, :pswitch_data_0

    .line 309
    invoke-direct {p0, p1}, Lnet/gogame/chat/zopim/ZopimChatContext;->log(Lcom/zopim/android/sdk/model/ChatLog;)V

    .line 310
    iget-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-virtual {p1, p3, p4}, Lnet/gogame/chat/ChatAdapterViewFactory;->getEmptyView(Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 305
    :pswitch_0
    invoke-direct {p0, p1}, Lnet/gogame/chat/zopim/ZopimChatContext;->log(Lcom/zopim/android/sdk/model/ChatLog;)V

    .line 306
    iget-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-virtual {p1, p3, p4}, Lnet/gogame/chat/ChatAdapterViewFactory;->getEmptyView(Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 294
    :pswitch_1
    iget-object p2, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 295
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getRating()Lcom/zopim/android/sdk/model/ChatLog$Rating;

    move-result-object p1

    invoke-direct {p0, p1}, Lnet/gogame/chat/zopim/ZopimChatContext;->toRating(Lcom/zopim/android/sdk/model/ChatLog$Rating;)Lnet/gogame/chat/ChatContext$Rating;

    move-result-object p1

    new-instance v0, Lnet/gogame/chat/zopim/ZopimChatContext$6;

    invoke-direct {v0, p0}, Lnet/gogame/chat/zopim/ZopimChatContext$6;-><init>(Lnet/gogame/chat/zopim/ZopimChatContext;)V

    .line 294
    invoke-virtual {p2, p3, p4, p1, v0}, Lnet/gogame/chat/ChatAdapterViewFactory;->getRatingView(Landroid/view/View;Landroid/view/ViewGroup;Lnet/gogame/chat/ChatContext$Rating;Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 286
    :pswitch_2
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getFileName()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p0, p2}, Lnet/gogame/chat/zopim/ZopimChatContext;->getImageUri(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p2

    if-eqz p2, :cond_0

    .line 288
    iget-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-virtual {p1, p3, p4, p2}, Lnet/gogame/chat/ChatAdapterViewFactory;->getVisitorAttachmentView(Landroid/view/View;Landroid/view/ViewGroup;Landroid/net/Uri;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 290
    :cond_0
    iget-object p2, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    const-string v0, "%s sent"

    new-array v1, v3, [Ljava/lang/Object;

    .line 291
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getFileName()Ljava/lang/String;

    move-result-object p1

    aput-object p1, v1, v2

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    .line 290
    invoke-virtual {p2, p3, p4, p1}, Lnet/gogame/chat/ChatAdapterViewFactory;->getVisitorMessageView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 283
    :pswitch_3
    iget-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    const-string p2, "System offline"

    invoke-virtual {p1, p3, p4, p2}, Lnet/gogame/chat/ChatAdapterViewFactory;->getNotificationView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 279
    :pswitch_4
    iget-object p2, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    const-string v0, "%s left the chat"

    new-array v1, v3, [Ljava/lang/Object;

    .line 280
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getDisplayName()Ljava/lang/String;

    move-result-object p1

    aput-object p1, v1, v2

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    .line 279
    invoke-virtual {p2, p3, p4, p1}, Lnet/gogame/chat/ChatAdapterViewFactory;->getNotificationView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 273
    :pswitch_5
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getNick()Ljava/lang/String;

    move-result-object p2

    if-eqz p2, :cond_1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getNick()Ljava/lang/String;

    move-result-object p2

    const-string v0, "agent"

    invoke-virtual {p2, v0}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result p2

    if-eqz p2, :cond_1

    .line 274
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getNick()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agentNick:Ljava/lang/String;

    .line 276
    :cond_1
    iget-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-virtual {p1, p3, p4}, Lnet/gogame/chat/ChatAdapterViewFactory;->getEmptyView(Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 269
    :pswitch_6
    iget-object p2, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 270
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object p1

    .line 269
    invoke-virtual {p2, p3, p4, p1}, Lnet/gogame/chat/ChatAdapterViewFactory;->getNotificationView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 265
    :pswitch_7
    iget-object p2, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 266
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object p1

    .line 265
    invoke-virtual {p2, p3, p4, p1}, Lnet/gogame/chat/ChatAdapterViewFactory;->getNotificationView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 261
    :pswitch_8
    iget-object p2, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 262
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object p1

    .line 261
    invoke-virtual {p2, p3, p4, p1}, Lnet/gogame/chat/ChatAdapterViewFactory;->getVisitorMessageView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 222
    :pswitch_9
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->profileIconToShowList:Ljava/util/List;

    invoke-interface {v0, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Ljava/lang/Boolean;

    invoke-virtual {p2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v5

    .line 223
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getDisplayName()Ljava/lang/String;

    move-result-object v6

    .line 225
    iget-object p2, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agents:Ljava/util/Map;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getNick()Ljava/lang/String;

    move-result-object v0

    invoke-interface {p2, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lcom/zopim/android/sdk/model/Agent;

    if-eqz p2, :cond_2

    .line 227
    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Agent;->getAvatarUri()Ljava/lang/String;

    move-result-object v1

    :cond_2
    move-object v7, v1

    .line 230
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object p2

    if-eqz p2, :cond_4

    .line 231
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object p2

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Attachment;->getUrl()Ljava/net/URL;

    move-result-object p2

    if-eqz p2, :cond_3

    .line 232
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object p2

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Attachment;->getThumbnail()Ljava/net/URL;

    move-result-object p2

    if-eqz p2, :cond_3

    .line 233
    iget-object v2, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 235
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object p2

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Attachment;->getUrl()Ljava/net/URL;

    move-result-object p2

    invoke-virtual {p2}, Ljava/net/URL;->toString()Ljava/lang/String;

    move-result-object v8

    .line 236
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/Attachment;->getThumbnail()Ljava/net/URL;

    move-result-object p1

    invoke-virtual {p1}, Ljava/net/URL;->toString()Ljava/lang/String;

    move-result-object v9

    move-object v3, p3

    move-object v4, p4

    .line 233
    invoke-virtual/range {v2 .. v9}, Lnet/gogame/chat/ChatAdapterViewFactory;->getAgentAttachmentView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 238
    :cond_3
    iget-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-virtual {p1, p3, p4}, Lnet/gogame/chat/ChatAdapterViewFactory;->getEmptyView(Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 240
    :cond_4
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getOptions()[Lcom/zopim/android/sdk/model/ChatLog$Option;

    move-result-object p2

    if-eqz p2, :cond_5

    .line 241
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getOptions()[Lcom/zopim/android/sdk/model/ChatLog$Option;

    move-result-object p2

    array-length p2, p2

    if-lez p2, :cond_5

    .line 242
    iget-object v2, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 244
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object v8

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getOptions()[Lcom/zopim/android/sdk/model/ChatLog$Option;

    move-result-object p1

    invoke-direct {p0, p1}, Lnet/gogame/chat/zopim/ZopimChatContext;->toOptions([Lcom/zopim/android/sdk/model/ChatLog$Option;)Ljava/util/List;

    move-result-object v9

    new-instance v10, Lnet/gogame/chat/zopim/ZopimChatContext$5;

    invoke-direct {v10, p0}, Lnet/gogame/chat/zopim/ZopimChatContext$5;-><init>(Lnet/gogame/chat/zopim/ZopimChatContext;)V

    move-object v3, p3

    move-object v4, p4

    .line 242
    invoke-virtual/range {v2 .. v10}, Lnet/gogame/chat/ChatAdapterViewFactory;->getAgentOptionsView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/List;Lnet/gogame/chat/ChatAdapterViewFactory$OptionListener;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 252
    :cond_5
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object p2

    if-eqz p2, :cond_6

    .line 253
    iget-object v2, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 255
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object v8

    move-object v3, p3

    move-object v4, p4

    .line 253
    invoke-virtual/range {v2 .. v8}, Lnet/gogame/chat/ChatAdapterViewFactory;->getAgentMessageView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 257
    :cond_6
    iget-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-virtual {p1, p3, p4}, Lnet/gogame/chat/ChatAdapterViewFactory;->getEmptyView(Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1

    :cond_7
    return-object v1

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_9
        :pswitch_8
        :pswitch_7
        :pswitch_6
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public isAttachmentSupported()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method

.method public send(Ljava/io/File;)V
    .locals 1

    if-eqz p1, :cond_1

    .line 201
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chat:Lcom/zopim/android/sdk/api/ChatApi;

    if-eqz v0, :cond_0

    .line 202
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chat:Lcom/zopim/android/sdk/api/ChatApi;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/ChatApi;->send(Ljava/io/File;)V

    goto :goto_0

    .line 204
    :cond_0
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->queuedFiles:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_1
    :goto_0
    return-void
.end method

.method public send(Ljava/lang/String;)V
    .locals 1

    .line 192
    invoke-static {p1}, Lorg/apache/commons/lang3/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 194
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chat:Lcom/zopim/android/sdk/api/ChatApi;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/ChatApi;->send(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public send(Lnet/gogame/chat/ChatContext$Rating;)V
    .locals 1

    if-eqz p1, :cond_0

    .line 212
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chat:Lcom/zopim/android/sdk/api/ChatApi;

    invoke-direct {p0, p1}, Lnet/gogame/chat/zopim/ZopimChatContext;->toRating(Lnet/gogame/chat/ChatContext$Rating;)Lcom/zopim/android/sdk/model/ChatLog$Rating;

    move-result-object p1

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/ChatApi;->sendChatRating(Lcom/zopim/android/sdk/model/ChatLog$Rating;)V

    :cond_0
    return-void
.end method

.method public start()V
    .locals 4

    .line 123
    sget-boolean v0, Lnet/gogame/chat/zopim/ZopimChatContext;->hasSession:Z

    if-nez v0, :cond_0

    .line 124
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->sessionConfig:Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->activity:Landroidx/fragment/app/FragmentActivity;

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->build(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chat:Lcom/zopim/android/sdk/api/ChatApi;

    const/4 v0, 0x1

    .line 125
    sput-boolean v0, Lnet/gogame/chat/zopim/ZopimChatContext;->hasSession:Z

    goto :goto_0

    .line 127
    :cond_0
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->activity:Landroidx/fragment/app/FragmentActivity;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->resume(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chat:Lcom/zopim/android/sdk/api/ChatApi;

    .line 129
    :goto_0
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->uiContext:Lnet/gogame/chat/UIContext;

    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->broadcastReceiver:Landroid/content/BroadcastReceiver;

    new-instance v2, Landroid/content/IntentFilter;

    const-string v3, "chat.action.TIMEOUT"

    invoke-direct {v2, v3}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    invoke-interface {v0, v1, v2}, Lnet/gogame/chat/UIContext;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)V

    .line 131
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->connectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->addConnectionObserver(Ljava/util/Observer;)V

    .line 132
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agentsObserver:Lcom/zopim/android/sdk/data/observers/AgentsObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->addAgentsObserver(Ljava/util/Observer;)V

    .line 133
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chatLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->addChatLogObserver(Ljava/util/Observer;)V

    .line 134
    invoke-direct {p0}, Lnet/gogame/chat/zopim/ZopimChatContext;->doNotifyDataSetChanged()V

    .line 136
    new-instance v0, Ljava/util/ArrayList;

    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->queuedFiles:Ljava/util/List;

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 137
    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->queuedFiles:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->clear()V

    .line 138
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/io/File;

    .line 139
    invoke-virtual {p0, v1}, Lnet/gogame/chat/zopim/ZopimChatContext;->send(Ljava/io/File;)V

    goto :goto_1

    :cond_1
    return-void
.end method

.method public stop()V
    .locals 2

    .line 145
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->uiContext:Lnet/gogame/chat/UIContext;

    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->broadcastReceiver:Landroid/content/BroadcastReceiver;

    invoke-interface {v0, v1}, Lnet/gogame/chat/UIContext;->unregisterReceiver(Landroid/content/BroadcastReceiver;)V

    .line 146
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->connectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteConnectionObserver(Ljava/util/Observer;)V

    .line 147
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->agentsObserver:Lcom/zopim/android/sdk/data/observers/AgentsObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteAgentsObserver(Ljava/util/Observer;)V

    .line 148
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chatLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteChatLogObserver(Ljava/util/Observer;)V

    .line 149
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chat:Lcom/zopim/android/sdk/api/ChatApi;

    if-eqz v0, :cond_0

    const/4 v0, 0x0

    .line 150
    iput-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext;->chat:Lcom/zopim/android/sdk/api/ChatApi;

    :cond_0
    return-void
.end method
