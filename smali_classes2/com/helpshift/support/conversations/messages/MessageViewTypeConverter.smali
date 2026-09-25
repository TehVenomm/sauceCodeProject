.class public Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;
.super Ljava/lang/Object;
.source "MessageViewTypeConverter.java"


# instance fields
.field private agentTypingMessageDataBinder:Lcom/helpshift/support/conversations/messages/AgentTypingMessageDataBinder;

.field private conversationFooterViewBinder:Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder;

.field private historyLoadingViewBinder:Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;

.field private viewTypeToDataBinderMap:Landroid/util/SparseArray;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/util/SparseArray<",
            "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 3

    .line 47
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 48
    new-instance v0, Landroid/util/SparseArray;

    invoke-direct {v0}, Landroid/util/SparseArray;-><init>()V

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    .line 49
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->ADMIN_TEXT_MESSAGE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 50
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->USER_TEXT_MESSAGE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 51
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->USER_SCREENSHOT_ATTACHMENT:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 52
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->ADMIN_ATTACHMENT_IMAGE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 53
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->ADMIN_ATTACHMENT_GENERIC:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 54
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->REQUESTED_APP_REVIEW:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 55
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->CONFIRMATION_REJECTED:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 56
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->ADMIN_REQUEST_ATTACHMENT:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 57
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->REQUEST_FOR_REOPEN:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 58
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->ADMIN_SUGGESTIONS_LIST:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 59
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->USER_SELECTABLE_OPTION:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 60
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->SYSTEM_DATE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 61
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->SYSTEM_DIVIDER:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 62
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->SYSTEM_PUBLISH_ID:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 63
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->ADMIN_REDACTED_MESSAGE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/AdminRedactedMessageDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/AdminRedactedMessageDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 64
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->USER_REDACTED_MESSAGE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 65
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    sget-object v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->SYSTEM_CONVERSATION_REDACTED_MESSAGE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget v1, v1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    new-instance v2, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder;

    invoke-direct {v2, p1}, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder;-><init>(Landroid/content/Context;)V

    .line 66
    invoke-virtual {v0, v1, v2}, Landroid/util/SparseArray;->put(ILjava/lang/Object;)V

    .line 67
    new-instance v0, Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder;

    invoke-direct {v0, p1}, Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->conversationFooterViewBinder:Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder;

    .line 68
    new-instance v0, Lcom/helpshift/support/conversations/messages/AgentTypingMessageDataBinder;

    invoke-direct {v0, p1}, Lcom/helpshift/support/conversations/messages/AgentTypingMessageDataBinder;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->agentTypingMessageDataBinder:Lcom/helpshift/support/conversations/messages/AgentTypingMessageDataBinder;

    .line 69
    new-instance v0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;

    invoke-direct {v0, p1}, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->historyLoadingViewBinder:Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;

    return-void
.end method


# virtual methods
.method public getAgentTypingMessageDataBinder()Lcom/helpshift/support/conversations/messages/AgentTypingMessageDataBinder;
    .locals 1

    .line 138
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->agentTypingMessageDataBinder:Lcom/helpshift/support/conversations/messages/AgentTypingMessageDataBinder;

    return-object v0
.end method

.method public getConversationFooterViewBinder()Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder;
    .locals 1

    .line 134
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->conversationFooterViewBinder:Lcom/helpshift/support/conversations/messages/ConversationFooterViewBinder;

    return-object v0
.end method

.method public getHistoryLoadingViewBinder()Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;
    .locals 1

    .line 142
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->historyLoadingViewBinder:Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;

    return-object v0
.end method

.method public messageToViewType(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)I
    .locals 1

    .line 73
    iget-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isRedacted:Z

    if-eqz v0, :cond_1

    .line 74
    iget-boolean p1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isAdminMessage:Z

    if-eqz p1, :cond_0

    .line 75
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->ADMIN_REDACTED_MESSAGE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 78
    :cond_0
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->USER_REDACTED_MESSAGE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 81
    :cond_1
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    if-eqz v0, :cond_2

    .line 82
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->ADMIN_SUGGESTIONS_LIST:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 84
    :cond_2
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    if-eqz v0, :cond_3

    .line 85
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->USER_SELECTABLE_OPTION:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 87
    :cond_3
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;

    if-eqz v0, :cond_4

    .line 88
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->ADMIN_TEXT_MESSAGE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 90
    :cond_4
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    if-eqz v0, :cond_5

    .line 91
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->USER_TEXT_MESSAGE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 93
    :cond_5
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    if-eqz v0, :cond_6

    .line 94
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->USER_SCREENSHOT_ATTACHMENT:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 96
    :cond_6
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    if-eqz v0, :cond_7

    .line 97
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->ADMIN_ATTACHMENT_IMAGE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 99
    :cond_7
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;

    if-eqz v0, :cond_8

    .line 100
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->ADMIN_ATTACHMENT_GENERIC:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 102
    :cond_8
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;

    if-eqz v0, :cond_9

    .line 103
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->REQUESTED_APP_REVIEW:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 105
    :cond_9
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;

    if-eqz v0, :cond_a

    .line 106
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->CONFIRMATION_REJECTED:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 108
    :cond_a
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    if-eqz v0, :cond_b

    .line 109
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->ADMIN_REQUEST_ATTACHMENT:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 111
    :cond_b
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;

    if-eqz v0, :cond_c

    .line 112
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->REQUEST_FOR_REOPEN:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 114
    :cond_c
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;

    if-eqz v0, :cond_d

    .line 115
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->SYSTEM_DATE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 117
    :cond_d
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;

    if-eqz v0, :cond_e

    .line 118
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->SYSTEM_DIVIDER:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 120
    :cond_e
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;

    if-eqz v0, :cond_f

    .line 121
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->SYSTEM_PUBLISH_ID:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    .line 123
    :cond_f
    instance-of p1, p1, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;

    if-eqz p1, :cond_10

    .line 124
    sget-object p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->SYSTEM_CONVERSATION_REDACTED_MESSAGE:Lcom/helpshift/support/conversations/messages/MessageViewType;

    iget p1, p1, Lcom/helpshift/support/conversations/messages/MessageViewType;->key:I

    return p1

    :cond_10
    const/4 p1, -0x1

    return p1
.end method

.method public viewTypeToDataBinder(I)Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
    .locals 1

    .line 130
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/MessageViewTypeConverter;->viewTypeToDataBinderMap:Landroid/util/SparseArray;

    invoke-virtual {v0, p1}, Landroid/util/SparseArray;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;

    return-object p1
.end method
