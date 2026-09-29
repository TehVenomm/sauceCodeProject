.class public Lnet/gogame/chat/ChatAdapter;
.super Landroid/widget/BaseAdapter;
.source "ChatAdapter.java"


# instance fields
.field private final chatContext:Lnet/gogame/chat/ChatContext;

.field private final context:Landroid/content/Context;

.field private final dataSetObserver:Landroid/database/DataSetObserver;

.field private final uiContext:Lnet/gogame/chat/UIContext;

.field private final viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;


# direct methods
.method public constructor <init>(Landroid/content/Context;Lnet/gogame/chat/UIContext;Lnet/gogame/chat/ChatAdapterViewFactory;Lnet/gogame/chat/ChatContext;)V
    .locals 1

    .line 27
    invoke-direct {p0}, Landroid/widget/BaseAdapter;-><init>()V

    .line 17
    new-instance v0, Lnet/gogame/chat/ChatAdapter$1;

    invoke-direct {v0, p0}, Lnet/gogame/chat/ChatAdapter$1;-><init>(Lnet/gogame/chat/ChatAdapter;)V

    iput-object v0, p0, Lnet/gogame/chat/ChatAdapter;->dataSetObserver:Landroid/database/DataSetObserver;

    .line 29
    iput-object p1, p0, Lnet/gogame/chat/ChatAdapter;->context:Landroid/content/Context;

    .line 30
    iput-object p2, p0, Lnet/gogame/chat/ChatAdapter;->uiContext:Lnet/gogame/chat/UIContext;

    .line 31
    iput-object p3, p0, Lnet/gogame/chat/ChatAdapter;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    .line 32
    iput-object p4, p0, Lnet/gogame/chat/ChatAdapter;->chatContext:Lnet/gogame/chat/ChatContext;

    return-void
.end method


# virtual methods
.method public getChatContext()Lnet/gogame/chat/ChatContext;
    .locals 1

    .line 36
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapter;->chatContext:Lnet/gogame/chat/ChatContext;

    return-object v0
.end method

.method public getCount()I
    .locals 1

    .line 49
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapter;->chatContext:Lnet/gogame/chat/ChatContext;

    invoke-interface {v0}, Lnet/gogame/chat/ChatContext;->getChatEntryCount()I

    move-result v0

    add-int/lit8 v0, v0, 0x1

    return v0
.end method

.method public getItem(I)Ljava/lang/Object;
    .locals 2

    const/4 v0, 0x0

    if-gez p1, :cond_0

    return-object v0

    .line 56
    :cond_0
    iget-object v1, p0, Lnet/gogame/chat/ChatAdapter;->chatContext:Lnet/gogame/chat/ChatContext;

    invoke-interface {v1}, Lnet/gogame/chat/ChatContext;->getChatEntryCount()I

    move-result v1

    if-ge p1, v1, :cond_1

    .line 57
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapter;->chatContext:Lnet/gogame/chat/ChatContext;

    invoke-interface {v0, p1}, Lnet/gogame/chat/ChatContext;->getChatEntry(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 58
    :cond_1
    iget-object v1, p0, Lnet/gogame/chat/ChatAdapter;->chatContext:Lnet/gogame/chat/ChatContext;

    invoke-interface {v1}, Lnet/gogame/chat/ChatContext;->getChatEntryCount()I

    move-result v1

    if-ne p1, v1, :cond_2

    .line 59
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapter;->chatContext:Lnet/gogame/chat/ChatContext;

    invoke-interface {p1}, Lnet/gogame/chat/ChatContext;->getAgentTypingEntry()Lnet/gogame/chat/AgentTypingEntry;

    move-result-object p1

    return-object p1

    :cond_2
    return-object v0
.end method

.method public getItemId(I)J
    .locals 2

    add-int/lit16 p1, p1, 0x2710

    int-to-long v0, p1

    return-wide v0
.end method

.method public getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 2

    .line 72
    invoke-virtual {p0, p1}, Lnet/gogame/chat/ChatAdapter;->getItem(I)Ljava/lang/Object;

    move-result-object v0

    .line 73
    instance-of v1, v0, Lnet/gogame/chat/AgentTypingEntry;

    if-eqz v1, :cond_1

    .line 74
    check-cast v0, Lnet/gogame/chat/AgentTypingEntry;

    .line 75
    invoke-virtual {v0}, Lnet/gogame/chat/AgentTypingEntry;->isTyping()Z

    move-result p1

    if-eqz p1, :cond_0

    .line 76
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapter;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    iget-object v0, p0, Lnet/gogame/chat/ChatAdapter;->context:Landroid/content/Context;

    .line 77
    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$string;->net_gogame_chat_agent_typing_message:I

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v0

    .line 76
    invoke-virtual {p1, p2, p3, v0}, Lnet/gogame/chat/ChatAdapterViewFactory;->getNotificationView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 80
    :cond_0
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapter;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    const/4 v0, 0x0

    const/4 v1, 0x1

    invoke-virtual {p1, p2, p3, v0, v1}, Lnet/gogame/chat/ChatAdapterViewFactory;->getNotificationView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;Z)Landroid/view/View;

    move-result-object p1

    return-object p1

    .line 83
    :cond_1
    iget-object v1, p0, Lnet/gogame/chat/ChatAdapter;->chatContext:Lnet/gogame/chat/ChatContext;

    invoke-interface {v1, v0, p1, p2, p3}, Lnet/gogame/chat/ChatContext;->getView(Ljava/lang/Object;ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    if-nez p1, :cond_2

    .line 85
    iget-object p2, p0, Lnet/gogame/chat/ChatAdapter;->viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-virtual {p2, p1, p3}, Lnet/gogame/chat/ChatAdapterViewFactory;->getEmptyView(Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    :cond_2
    return-object p1
.end method

.method public start()V
    .locals 2

    .line 40
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapter;->chatContext:Lnet/gogame/chat/ChatContext;

    iget-object v1, p0, Lnet/gogame/chat/ChatAdapter;->dataSetObserver:Landroid/database/DataSetObserver;

    invoke-interface {v0, v1}, Lnet/gogame/chat/ChatContext;->registerDataSetObserver(Landroid/database/DataSetObserver;)V

    return-void
.end method

.method public stop()V
    .locals 2

    .line 44
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapter;->chatContext:Lnet/gogame/chat/ChatContext;

    iget-object v1, p0, Lnet/gogame/chat/ChatAdapter;->dataSetObserver:Landroid/database/DataSetObserver;

    invoke-interface {v0, v1}, Lnet/gogame/chat/ChatContext;->unregisterDataSetObserver(Landroid/database/DataSetObserver;)V

    return-void
.end method
