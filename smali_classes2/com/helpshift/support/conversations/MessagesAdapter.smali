.class public Lcom/helpshift/support/conversations/MessagesAdapter;
.super Landroidx/recyclerview/widget/RecyclerView$Adapter;
.source "MessagesAdapter.java"

# interfaces
.implements Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;
.implements Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder$ConversationFooterClickListener;
.implements Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$HistoryLoadingClickListener;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/recyclerview/widget/RecyclerView$Adapter<",
        "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
        ">;",
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;",
        "Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder$ConversationFooterClickListener;",
        "Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$HistoryLoadingClickListener;"
    }
.end annotation


# instance fields
.field private conversationFooterState:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

.field private historyLoadingState:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

.field private isAgentTypingIndicatorVisible:Z

.field private messageViewTypeConverter:Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;

.field private messages:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation
.end field

.field private messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/util/List;Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;",
            "Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;",
            ")V"
        }
    .end annotation

    .line 44
    invoke-direct {p0}, Landroidx/recyclerview/widget/RecyclerView$Adapter;-><init>()V

    .line 39
    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->NONE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    iput-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->conversationFooterState:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    const/4 v0, 0x0

    .line 40
    iput-boolean v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->isAgentTypingIndicatorVisible:Z

    .line 41
    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->NONE:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    iput-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->historyLoadingState:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    .line 45
    new-instance v0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;

    invoke-direct {v0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messageViewTypeConverter:Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;

    .line 46
    iput-object p2, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messages:Ljava/util/List;

    .line 47
    iput-object p3, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    return-void
.end method

.method private getFooterCount()I
    .locals 3

    .line 153
    iget-boolean v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->isAgentTypingIndicatorVisible:Z

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 156
    :goto_0
    iget-object v1, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->conversationFooterState:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    sget-object v2, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->NONE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    if-eq v1, v2, :cond_1

    add-int/lit8 v0, v0, 0x1

    :cond_1
    return v0
.end method

.method private getFooterViewType(I)I
    .locals 2

    .line 304
    invoke-direct {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getHeaderCount()I

    move-result v0

    invoke-virtual {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getMessageCount()I

    move-result v1

    add-int/2addr v0, v1

    sub-int/2addr p1, v0

    .line 306
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->conversationFooterState:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->NONE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    if-eq v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    packed-switch p1, :pswitch_data_0

    goto :goto_1

    :pswitch_0
    if-eqz v0, :cond_2

    .line 318
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->CONVERSATION_FOOTER:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    goto :goto_2

    .line 309
    :pswitch_1
    iget-boolean p1, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->isAgentTypingIndicatorVisible:Z

    if-eqz p1, :cond_1

    .line 310
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->AGENT_TYPING_FOOTER:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    goto :goto_2

    :cond_1
    if-eqz v0, :cond_2

    .line 313
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->CONVERSATION_FOOTER:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    goto :goto_2

    :cond_2
    :goto_1
    const/4 p1, -0x1

    :goto_2
    return p1

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private getHeaderCount()I
    .locals 2

    .line 142
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->historyLoadingState:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->NONE:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    if-eq v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method private getHeaderViewType()I
    .locals 1

    .line 289
    sget-object v0, Lcom/helpshift/support/conversations/messages/MessageViewType;->HISTORY_LOADING_VIEW:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v0, v0, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return v0
.end method

.method private getMessageFromUIMessageList(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 1

    .line 173
    invoke-direct {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getHeaderCount()I

    move-result v0

    sub-int/2addr p1, v0

    .line 174
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messages:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    return-object p1
.end method


# virtual methods
.method public getItemCount()I
    .locals 2

    .line 117
    invoke-direct {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getHeaderCount()I

    move-result v0

    invoke-virtual {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getMessageCount()I

    move-result v1

    add-int/2addr v0, v1

    invoke-direct {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getFooterCount()I

    move-result v1

    add-int/2addr v0, v1

    return v0
.end method

.method public getItemViewType(I)I
    .locals 2

    .line 103
    invoke-direct {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getHeaderCount()I

    move-result v0

    if-ge p1, v0, :cond_0

    .line 104
    invoke-direct {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getHeaderViewType()I

    move-result p1

    return p1

    .line 106
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getHeaderCount()I

    move-result v0

    invoke-virtual {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getMessageCount()I

    move-result v1

    add-int/2addr v0, v1

    if-ge p1, v0, :cond_1

    .line 107
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messageViewTypeConverter:Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;

    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/MessagesAdapter;->getMessageFromUIMessageList(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p1

    invoke-virtual {v0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->messageToViewType(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)I

    move-result p1

    return p1

    .line 110
    :cond_1
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/MessagesAdapter;->getFooterViewType(I)I

    move-result p1

    return p1
.end method

.method public getMessageCount()I
    .locals 1

    .line 362
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messages:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public handleAdminImageAttachmentMessageClick(Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;)V
    .locals 1

    .line 230
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 231
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0, p1}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->handleAdminImageAttachmentMessageClick(Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;)V

    :cond_0
    return-void
.end method

.method public handleGenericAttachmentMessageClick(Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;)V
    .locals 1

    .line 223
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 224
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0, p1}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->handleGenericAttachmentMessageClick(Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;)V

    :cond_0
    return-void
.end method

.method public handleOptionSelected(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V
    .locals 1

    .line 237
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 238
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0, p1, p2, p3}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->handleOptionSelected(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V

    :cond_0
    return-void
.end method

.method public handleReplyReviewButtonClick(Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V
    .locals 1

    .line 209
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 210
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0, p1}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->handleReplyReviewButtonClick(Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V

    :cond_0
    return-void
.end method

.method public launchImagePicker(Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;)V
    .locals 1

    .line 202
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 203
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0, p1}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->launchImagePicker(Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;)V

    :cond_0
    return-void
.end method

.method public onAdminMessageLinkClicked(Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 179
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 180
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->onAdminMessageLinkClicked(Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    :cond_0
    return-void
.end method

.method public onAdminSuggestedQuestionSelected(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 244
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 245
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0, p1, p2, p3}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->onAdminSuggestedQuestionSelected(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Ljava/lang/String;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public onBindViewHolder(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V
    .locals 2

    .line 75
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->getItemViewType()I

    move-result v0

    .line 76
    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->HISTORY_LOADING_VIEW:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    if-ne v0, v1, :cond_0

    .line 77
    iget-object p2, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messageViewTypeConverter:Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;

    invoke-virtual {p2}, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->getHistoryLoadingViewBinder()Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;

    move-result-object p2

    check-cast p1, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;

    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->historyLoadingState:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    .line 78
    invoke-virtual {p2, p1, v0}, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;->bind(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;)V

    goto :goto_0

    .line 80
    :cond_0
    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->CONVERSATION_FOOTER:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    if-ne v0, v1, :cond_1

    .line 81
    iget-object p2, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messageViewTypeConverter:Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;

    invoke-virtual {p2}, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->getConversationFooterViewBinder()Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder;

    move-result-object p2

    check-cast p1, Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder$ViewHolder;

    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->conversationFooterState:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    .line 82
    invoke-virtual {p2, p1, v0}, Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder;->bind(Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    goto :goto_0

    .line 85
    :cond_1
    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->AGENT_TYPING_FOOTER:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    if-ne v0, v1, :cond_2

    goto :goto_0

    .line 89
    :cond_2
    invoke-direct {p0, p2}, Lcom/helpshift/support/conversations/MessagesAdapter;->getMessageFromUIMessageList(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p2

    .line 90
    iget-object v1, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messageViewTypeConverter:Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;

    invoke-virtual {v1, v0}, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinder(I)Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;

    move-result-object v0

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;->bind(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    :goto_0
    return-void
.end method

.method public onCSATSurveySubmitted(ILjava/lang/String;)V
    .locals 1

    .line 341
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 342
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->onCSATSurveySubmitted(ILjava/lang/String;)V

    :cond_0
    return-void
.end method

.method public onCreateContextMenu(Landroid/view/ContextMenu;Ljava/lang/String;)V
    .locals 1

    .line 186
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 187
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->onCreateContextMenu(Landroid/view/ContextMenu;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public onCreateViewHolder(Landroid/view/ViewGroup;I)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 1

    .line 52
    sget-object v0, Lcom/helpshift/support/conversations/messages/MessageViewType;->HISTORY_LOADING_VIEW:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v0, v0, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    if-ne p2, v0, :cond_0

    .line 53
    iget-object p2, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messageViewTypeConverter:Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;

    invoke-virtual {p2}, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->getHistoryLoadingViewBinder()Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;

    move-result-object p2

    .line 54
    invoke-virtual {p2, p0}, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;->setHistoryLoadingClickListener(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$HistoryLoadingClickListener;)V

    .line 55
    invoke-virtual {p2, p1}, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;

    move-result-object p1

    return-object p1

    .line 57
    :cond_0
    sget-object v0, Lcom/helpshift/support/conversations/messages/MessageViewType;->CONVERSATION_FOOTER:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v0, v0, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    if-ne p2, v0, :cond_1

    .line 58
    iget-object p2, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messageViewTypeConverter:Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;

    .line 59
    invoke-virtual {p2}, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->getConversationFooterViewBinder()Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder;

    move-result-object p2

    .line 60
    invoke-virtual {p2, p0}, Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder;->setConversationFooterClickListener(Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder$ConversationFooterClickListener;)V

    .line 61
    invoke-virtual {p2, p1}, Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder$ViewHolder;

    move-result-object p1

    return-object p1

    .line 63
    :cond_1
    sget-object v0, Lcom/helpshift/support/conversations/messages/MessageViewType;->AGENT_TYPING_FOOTER:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v0, v0, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    if-ne p2, v0, :cond_2

    .line 64
    iget-object p2, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messageViewTypeConverter:Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;

    invoke-virtual {p2}, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->getAgentTypingMessageDataBinder()Lcom/helpshift/support/conversations/messages/AgentTypingMessageDataBinder;

    move-result-object p2

    invoke-virtual {p2, p1}, Lcom/helpshift/support/conversations/messages/AgentTypingMessageDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    move-result-object p1

    return-object p1

    .line 67
    :cond_2
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messageViewTypeConverter:Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;

    invoke-virtual {v0, p2}, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinder(I)Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;

    move-result-object p2

    .line 68
    invoke-virtual {p2, p0}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;->setMessageItemClickListener(Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;)V

    .line 69
    invoke-virtual {p2, p1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public onHistoryLoadingRetryClicked()V
    .locals 1

    .line 367
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 368
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->onHistoryLoadingRetryClicked()V

    :cond_0
    return-void
.end method

.method public onItemRangeChanged(II)V
    .locals 1

    .line 129
    invoke-direct {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getHeaderCount()I

    move-result v0

    add-int/2addr p1, v0

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/MessagesAdapter;->notifyItemRangeChanged(II)V

    return-void
.end method

.method public onItemRangeInserted(II)V
    .locals 1

    .line 125
    invoke-direct {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getHeaderCount()I

    move-result v0

    add-int/2addr p1, v0

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/MessagesAdapter;->notifyItemRangeInserted(II)V

    return-void
.end method

.method public onItemRangeRemoved(II)V
    .locals 1

    .line 133
    invoke-direct {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getHeaderCount()I

    move-result v0

    add-int/2addr p1, v0

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/MessagesAdapter;->notifyItemRangeRemoved(II)V

    return-void
.end method

.method public onScreenshotMessageClicked(Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V
    .locals 1

    .line 216
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 217
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0, p1}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->onScreenshotMessageClicked(Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V

    :cond_0
    return-void
.end method

.method public onStartNewConversationButtonClick()V
    .locals 1

    .line 334
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 335
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->onStartNewConversationButtonClick()V

    :cond_0
    return-void
.end method

.method public retryMessage(I)V
    .locals 1

    .line 194
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    if-eqz v0, :cond_0

    .line 195
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/MessagesAdapter;->getMessageFromUIMessageList(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p1

    .line 196
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    invoke-interface {v0, p1}, Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;->retryMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    :cond_0
    return-void
.end method

.method public setAgentTypingIndicatorVisibility(Z)V
    .locals 1

    .line 250
    iget-boolean v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->isAgentTypingIndicatorVisible:Z

    if-eq v0, p1, :cond_1

    .line 251
    iput-boolean p1, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->isAgentTypingIndicatorVisible:Z

    const/4 v0, 0x1

    if-eqz p1, :cond_0

    .line 253
    iget-object p1, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messages:Ljava/util/List;

    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result p1

    invoke-virtual {p0, p1, v0}, Lcom/helpshift/support/conversations/MessagesAdapter;->notifyItemRangeInserted(II)V

    goto :goto_0

    .line 256
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messages:Ljava/util/List;

    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result p1

    invoke-virtual {p0, p1, v0}, Lcom/helpshift/support/conversations/MessagesAdapter;->notifyItemRangeRemoved(II)V

    :cond_1
    :goto_0
    return-void
.end method

.method public setConversationFooterState(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V
    .locals 0

    if-nez p1, :cond_0

    .line 326
    sget-object p1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->NONE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    .line 328
    :cond_0
    iput-object p1, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->conversationFooterState:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    .line 329
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/MessagesAdapter;->notifyDataSetChanged()V

    return-void
.end method

.method public setHistoryLoadingState(Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;)V
    .locals 3

    if-eqz p1, :cond_3

    .line 263
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->historyLoadingState:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    if-ne v0, p1, :cond_0

    goto :goto_1

    .line 274
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->historyLoadingState:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->NONE:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    const/4 v2, 0x0

    if-ne v0, v1, :cond_1

    .line 275
    iput-object p1, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->historyLoadingState:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    .line 276
    invoke-virtual {p0, v2}, Lcom/helpshift/support/conversations/MessagesAdapter;->notifyItemInserted(I)V

    goto :goto_0

    .line 278
    :cond_1
    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->NONE:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    if-ne p1, v0, :cond_2

    .line 279
    iput-object p1, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->historyLoadingState:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    .line 280
    invoke-virtual {p0, v2}, Lcom/helpshift/support/conversations/MessagesAdapter;->notifyItemRemoved(I)V

    goto :goto_0

    .line 283
    :cond_2
    iput-object p1, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->historyLoadingState:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    .line 284
    invoke-virtual {p0, v2}, Lcom/helpshift/support/conversations/MessagesAdapter;->notifyItemChanged(I)V

    :goto_0
    return-void

    :cond_3
    :goto_1
    return-void
.end method

.method public unregisterAdapterClickListener()V
    .locals 1

    const/4 v0, 0x0

    .line 351
    iput-object v0, p0, Lcom/helpshift/support/conversations/MessagesAdapter;->messagesAdapterClickListener:Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;

    return-void
.end method
