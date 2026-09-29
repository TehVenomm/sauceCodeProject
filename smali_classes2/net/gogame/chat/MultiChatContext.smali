.class public Lnet/gogame/chat/MultiChatContext;
.super Lnet/gogame/chat/AbstractChatContext;
.source "MultiChatContext.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/chat/MultiChatContext$Listener;
    }
.end annotation


# instance fields
.field private final agentTypingEntry:Lnet/gogame/chat/AgentTypingEntry;

.field private final chatContexts:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/chat/ChatContext;",
            ">;"
        }
    .end annotation
.end field

.field private currentChatContext:Lnet/gogame/chat/ChatContext;

.field private final dataSetObserver:Landroid/database/DataSetObserver;

.field private final listener:Lnet/gogame/chat/MultiChatContext$Listener;


# direct methods
.method public constructor <init>()V
    .locals 3

    .line 27
    invoke-direct {p0}, Lnet/gogame/chat/AbstractChatContext;-><init>()V

    .line 14
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/chat/MultiChatContext;->chatContexts:Ljava/util/List;

    const/4 v0, 0x0

    .line 15
    iput-object v0, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    .line 16
    new-instance v1, Lnet/gogame/chat/AgentTypingEntry;

    const/4 v2, 0x0

    invoke-direct {v1, v2}, Lnet/gogame/chat/AgentTypingEntry;-><init>(Z)V

    iput-object v1, p0, Lnet/gogame/chat/MultiChatContext;->agentTypingEntry:Lnet/gogame/chat/AgentTypingEntry;

    .line 17
    new-instance v1, Lnet/gogame/chat/MultiChatContext$1;

    invoke-direct {v1, p0}, Lnet/gogame/chat/MultiChatContext$1;-><init>(Lnet/gogame/chat/MultiChatContext;)V

    iput-object v1, p0, Lnet/gogame/chat/MultiChatContext;->dataSetObserver:Landroid/database/DataSetObserver;

    .line 29
    iput-object v0, p0, Lnet/gogame/chat/MultiChatContext;->listener:Lnet/gogame/chat/MultiChatContext$Listener;

    return-void
.end method

.method public constructor <init>(Lnet/gogame/chat/MultiChatContext$Listener;)V
    .locals 2

    .line 33
    invoke-direct {p0}, Lnet/gogame/chat/AbstractChatContext;-><init>()V

    .line 14
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/chat/MultiChatContext;->chatContexts:Ljava/util/List;

    const/4 v0, 0x0

    .line 15
    iput-object v0, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    .line 16
    new-instance v0, Lnet/gogame/chat/AgentTypingEntry;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Lnet/gogame/chat/AgentTypingEntry;-><init>(Z)V

    iput-object v0, p0, Lnet/gogame/chat/MultiChatContext;->agentTypingEntry:Lnet/gogame/chat/AgentTypingEntry;

    .line 17
    new-instance v0, Lnet/gogame/chat/MultiChatContext$1;

    invoke-direct {v0, p0}, Lnet/gogame/chat/MultiChatContext$1;-><init>(Lnet/gogame/chat/MultiChatContext;)V

    iput-object v0, p0, Lnet/gogame/chat/MultiChatContext;->dataSetObserver:Landroid/database/DataSetObserver;

    .line 35
    iput-object p1, p0, Lnet/gogame/chat/MultiChatContext;->listener:Lnet/gogame/chat/MultiChatContext$Listener;

    return-void
.end method


# virtual methods
.method public addChatContext(Lnet/gogame/chat/ChatContext;)V
    .locals 1

    .line 57
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->dataSetObserver:Landroid/database/DataSetObserver;

    invoke-interface {p1, v0}, Lnet/gogame/chat/ChatContext;->registerDataSetObserver(Landroid/database/DataSetObserver;)V

    .line 58
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->chatContexts:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 59
    iput-object p1, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    .line 60
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->listener:Lnet/gogame/chat/MultiChatContext$Listener;

    if-eqz v0, :cond_0

    .line 61
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->listener:Lnet/gogame/chat/MultiChatContext$Listener;

    invoke-interface {v0, p1}, Lnet/gogame/chat/MultiChatContext$Listener;->onChatContextAdded(Lnet/gogame/chat/ChatContext;)V

    :cond_0
    return-void
.end method

.method public getAgentTypingEntry()Lnet/gogame/chat/AgentTypingEntry;
    .locals 1

    .line 97
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    if-eqz v0, :cond_0

    .line 98
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    invoke-interface {v0}, Lnet/gogame/chat/ChatContext;->getAgentTypingEntry()Lnet/gogame/chat/AgentTypingEntry;

    move-result-object v0

    return-object v0

    .line 100
    :cond_0
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->agentTypingEntry:Lnet/gogame/chat/AgentTypingEntry;

    return-object v0
.end method

.method public getChatEntry(I)Ljava/lang/Object;
    .locals 5

    .line 85
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->chatContexts:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    const/4 v1, 0x0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/chat/ChatContext;

    sub-int v3, p1, v1

    .line 87
    invoke-interface {v2}, Lnet/gogame/chat/ChatContext;->getChatEntryCount()I

    move-result v4

    if-ge v3, v4, :cond_0

    .line 88
    invoke-interface {v2, v3}, Lnet/gogame/chat/ChatContext;->getChatEntry(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 90
    :cond_0
    invoke-interface {v2}, Lnet/gogame/chat/ChatContext;->getChatEntryCount()I

    move-result v2

    add-int/2addr v1, v2

    goto :goto_0

    :cond_1
    const/4 p1, 0x0

    return-object p1
.end method

.method public getChatEntryCount()I
    .locals 3

    .line 76
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->chatContexts:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    const/4 v1, 0x0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/chat/ChatContext;

    .line 77
    invoke-interface {v2}, Lnet/gogame/chat/ChatContext;->getChatEntryCount()I

    move-result v2

    add-int/2addr v1, v2

    goto :goto_0

    :cond_0
    return v1
.end method

.method public getCurrentChatContext()Lnet/gogame/chat/ChatContext;
    .locals 1

    .line 39
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    return-object v0
.end method

.method public getView(Ljava/lang/Object;ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 5

    .line 125
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->chatContexts:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    const/4 v1, 0x0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/chat/ChatContext;

    sub-int v3, p2, v1

    .line 127
    invoke-interface {v2}, Lnet/gogame/chat/ChatContext;->getChatEntryCount()I

    move-result v4

    if-ge v3, v4, :cond_0

    .line 128
    invoke-interface {v2, p1, v3, p3, p4}, Lnet/gogame/chat/ChatContext;->getView(Ljava/lang/Object;ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 130
    :cond_0
    invoke-interface {v2}, Lnet/gogame/chat/ChatContext;->getChatEntryCount()I

    move-result v2

    add-int/2addr v1, v2

    goto :goto_0

    :cond_1
    const/4 p1, 0x0

    return-object p1
.end method

.method public isAttachmentSupported()Z
    .locals 1

    .line 67
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    if-eqz v0, :cond_0

    .line 68
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    invoke-interface {v0}, Lnet/gogame/chat/ChatContext;->isAttachmentSupported()Z

    move-result v0

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public send(Ljava/io/File;)V
    .locals 1

    .line 110
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    if-eqz v0, :cond_0

    .line 111
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    invoke-interface {v0, p1}, Lnet/gogame/chat/ChatContext;->send(Ljava/io/File;)V

    :cond_0
    return-void
.end method

.method public send(Ljava/lang/String;)V
    .locals 1

    .line 105
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    invoke-interface {v0, p1}, Lnet/gogame/chat/ChatContext;->send(Ljava/lang/String;)V

    return-void
.end method

.method public send(Lnet/gogame/chat/ChatContext$Rating;)V
    .locals 1

    .line 117
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    if-eqz v0, :cond_0

    .line 118
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->currentChatContext:Lnet/gogame/chat/ChatContext;

    invoke-interface {v0, p1}, Lnet/gogame/chat/ChatContext;->send(Lnet/gogame/chat/ChatContext$Rating;)V

    :cond_0
    return-void
.end method

.method public start()V
    .locals 2

    .line 44
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->chatContexts:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/chat/ChatContext;

    .line 45
    invoke-interface {v1}, Lnet/gogame/chat/ChatContext;->start()V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public stop()V
    .locals 2

    .line 51
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext;->chatContexts:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/chat/ChatContext;

    .line 52
    invoke-interface {v1}, Lnet/gogame/chat/ChatContext;->stop()V

    goto :goto_0

    :cond_0
    return-void
.end method
