.class public Lcom/helpshift/common/conversation/ConversationDB;
.super Ljava/lang/Object;
.source "ConversationDB.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;,
        Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_ConverDB"

.field private static instance:Lcom/helpshift/common/conversation/ConversationDB;


# instance fields
.field private final KEY_BOT_ACTION_TYPE:Ljava/lang/String;

.field private final KEY_BOT_ENDED_REASON:Ljava/lang/String;

.field private final KEY_CHATBOT_INFO:Ljava/lang/String;

.field private final KEY_CONTENT_TYPE:Ljava/lang/String;

.field private final KEY_CONVERSATION_ENDED_DELEGATE_SENT:Ljava/lang/String;

.field private final KEY_CSAT_FEEDBACK:Ljava/lang/String;

.field private final KEY_CSAT_RATING:Ljava/lang/String;

.field private final KEY_CSAT_STATE:Ljava/lang/String;

.field private final KEY_DATE_TIME:Ljava/lang/String;

.field private final KEY_FAQS:Ljava/lang/String;

.field private final KEY_FAQ_LANGUAGE:Ljava/lang/String;

.field private final KEY_FAQ_PUBLISH_ID:Ljava/lang/String;

.field private final KEY_FAQ_TITLE:Ljava/lang/String;

.field private final KEY_FILE_NAME:Ljava/lang/String;

.field private final KEY_FILE_PATH:Ljava/lang/String;

.field private final KEY_FOLLOW_UP_REJECTED_OPEN_CONVERSATION:Ljava/lang/String;

.field private final KEY_FOLLOW_UP_REJECTED_REASON:Ljava/lang/String;

.field private final KEY_HAS_NEXT_BOT:Ljava/lang/String;

.field private final KEY_IMAGE_ATTACHMENT_COMPRESSION_COPYING_DONE:Ljava/lang/String;

.field private final KEY_IMAGE_ATTACHMENT_DRAFT_FILE_PATH:Ljava/lang/String;

.field private final KEY_IMAGE_ATTACHMENT_DRAFT_ORIGINAL_NAME:Ljava/lang/String;

.field private final KEY_IMAGE_ATTACHMENT_DRAFT_ORIGINAL_SIZE:Ljava/lang/String;

.field private final KEY_INCREMENT_MESSAGE_COUNT:Ljava/lang/String;

.field private final KEY_INPUT_KEYBOARD:Ljava/lang/String;

.field private final KEY_INPUT_LABEL:Ljava/lang/String;

.field private final KEY_INPUT_OPTIONS:Ljava/lang/String;

.field private final KEY_INPUT_PLACEHOLDER:Ljava/lang/String;

.field private final KEY_INPUT_REQUIRED:Ljava/lang/String;

.field private final KEY_INPUT_SKIP_LABEL:Ljava/lang/String;

.field private final KEY_IS_ANSWERED:Ljava/lang/String;

.field private final KEY_IS_AUTO_FILLED_PREISSUE:Ljava/lang/String;

.field private final KEY_IS_MESSAGE_EMPTY:Ljava/lang/String;

.field private final KEY_IS_RESPONSE_SKIPPED:Ljava/lang/String;

.field private final KEY_IS_SUGGESTION_READ_EVENT_SENT:Ljava/lang/String;

.field private final KEY_MESSAGE_SYNC_STATUS:Ljava/lang/String;

.field private final KEY_OPTION_DATA:Ljava/lang/String;

.field private final KEY_OPTION_TITLE:Ljava/lang/String;

.field private final KEY_OPTION_TYPE:Ljava/lang/String;

.field private final KEY_READ_AT:Ljava/lang/String;

.field private final KEY_REFERRED_MESSAGE_ID:Ljava/lang/String;

.field private final KEY_REFERRED_MESSAGE_TYPE:Ljava/lang/String;

.field private final KEY_SECURE_ATTACHMENT:Ljava/lang/String;

.field private final KEY_SEEN_AT_MESSAGE_CURSOR:Ljava/lang/String;

.field private final KEY_SEEN_SYNC_STATUS:Ljava/lang/String;

.field private final KEY_SELECTED_OPTION_DATA:Ljava/lang/String;

.field private final KEY_SIZE:Ljava/lang/String;

.field private final KEY_SUGGESTION_READ_FAQ_PUBLISH_ID:Ljava/lang/String;

.field private final KEY_THUMBNAIL_FILE_PATH:Ljava/lang/String;

.field private final KEY_THUMBNAIL_URL:Ljava/lang/String;

.field private final KEY_TIMEZONE_ID:Ljava/lang/String;

.field private final KEY_URL:Ljava/lang/String;

.field private final dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

.field private final dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;


# direct methods
.method private constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 133
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "csat_rating"

    .line 76
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_CSAT_RATING:Ljava/lang/String;

    const-string v0, "csat_state"

    .line 77
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_CSAT_STATE:Ljava/lang/String;

    const-string v0, "csat_feedback"

    .line 78
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_CSAT_FEEDBACK:Ljava/lang/String;

    const-string v0, "increment_message_count"

    .line 79
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_INCREMENT_MESSAGE_COUNT:Ljava/lang/String;

    const-string v0, "ended_delegate_sent"

    .line 80
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_CONVERSATION_ENDED_DELEGATE_SENT:Ljava/lang/String;

    const-string v0, "image_draft_orig_name"

    .line 81
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_IMAGE_ATTACHMENT_DRAFT_ORIGINAL_NAME:Ljava/lang/String;

    const-string v0, "image_draft_orig_size"

    .line 82
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_IMAGE_ATTACHMENT_DRAFT_ORIGINAL_SIZE:Ljava/lang/String;

    const-string v0, "image_draft_file_path"

    .line 83
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_IMAGE_ATTACHMENT_DRAFT_FILE_PATH:Ljava/lang/String;

    const-string v0, "image_copy_done"

    .line 84
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_IMAGE_ATTACHMENT_COMPRESSION_COPYING_DONE:Ljava/lang/String;

    const-string v0, "is_autofilled_preissue"

    .line 85
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_IS_AUTO_FILLED_PREISSUE:Ljava/lang/String;

    const-string v0, "referredMessageId"

    .line 88
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_REFERRED_MESSAGE_ID:Ljava/lang/String;

    const-string v0, "rejected_reason"

    .line 89
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_FOLLOW_UP_REJECTED_REASON:Ljava/lang/String;

    const-string v0, "rejected_conv_id"

    .line 90
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_FOLLOW_UP_REJECTED_OPEN_CONVERSATION:Ljava/lang/String;

    const-string v0, "is_answered"

    .line 91
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_IS_ANSWERED:Ljava/lang/String;

    const-string v0, "content_type"

    .line 92
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_CONTENT_TYPE:Ljava/lang/String;

    const-string v0, "file_name"

    .line 93
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_FILE_NAME:Ljava/lang/String;

    const-string v0, "url"

    .line 94
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_URL:Ljava/lang/String;

    const-string v0, "size"

    .line 95
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_SIZE:Ljava/lang/String;

    const-string v0, "thumbnail_url"

    .line 96
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_THUMBNAIL_URL:Ljava/lang/String;

    const-string v0, "thumbnailFilePath"

    .line 97
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_THUMBNAIL_FILE_PATH:Ljava/lang/String;

    const-string v0, "filePath"

    .line 98
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_FILE_PATH:Ljava/lang/String;

    const-string v0, "seen_cursor"

    .line 99
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_SEEN_AT_MESSAGE_CURSOR:Ljava/lang/String;

    const-string v0, "seen_sync_status"

    .line 100
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_SEEN_SYNC_STATUS:Ljava/lang/String;

    const-string v0, "read_at"

    .line 101
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_READ_AT:Ljava/lang/String;

    const-string v0, "input_keyboard"

    .line 102
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_INPUT_KEYBOARD:Ljava/lang/String;

    const-string v0, "input_required"

    .line 103
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_INPUT_REQUIRED:Ljava/lang/String;

    const-string v0, "input_skip_label"

    .line 104
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_INPUT_SKIP_LABEL:Ljava/lang/String;

    const-string v0, "input_placeholder"

    .line 105
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_INPUT_PLACEHOLDER:Ljava/lang/String;

    const-string v0, "input_label"

    .line 106
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_INPUT_LABEL:Ljava/lang/String;

    const-string v0, "input_options"

    .line 107
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_INPUT_OPTIONS:Ljava/lang/String;

    const-string v0, "option_type"

    .line 108
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_OPTION_TYPE:Ljava/lang/String;

    const-string v0, "option_title"

    .line 109
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_OPTION_TITLE:Ljava/lang/String;

    const-string v0, "option_data"

    .line 110
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_OPTION_DATA:Ljava/lang/String;

    const-string v0, "chatbot_info"

    .line 111
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_CHATBOT_INFO:Ljava/lang/String;

    const-string v0, "has_next_bot"

    .line 112
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_HAS_NEXT_BOT:Ljava/lang/String;

    const-string v0, "faqs"

    .line 113
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_FAQS:Ljava/lang/String;

    const-string v0, "faq_title"

    .line 114
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_FAQ_TITLE:Ljava/lang/String;

    const-string v0, "faq_publish_id"

    .line 115
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_FAQ_PUBLISH_ID:Ljava/lang/String;

    const-string v0, "faq_language"

    .line 116
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_FAQ_LANGUAGE:Ljava/lang/String;

    const-string v0, "is_response_skipped"

    .line 117
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_IS_RESPONSE_SKIPPED:Ljava/lang/String;

    const-string v0, "selected_option_data"

    .line 118
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_SELECTED_OPTION_DATA:Ljava/lang/String;

    const-string v0, "referred_message_type"

    .line 119
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_REFERRED_MESSAGE_TYPE:Ljava/lang/String;

    const-string v0, "bot_action_type"

    .line 120
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_BOT_ACTION_TYPE:Ljava/lang/String;

    const-string v0, "bot_ended_reason"

    .line 121
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_BOT_ENDED_REASON:Ljava/lang/String;

    const-string v0, "message_sync_status"

    .line 122
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_MESSAGE_SYNC_STATUS:Ljava/lang/String;

    const-string v0, "is_secure"

    .line 123
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_SECURE_ATTACHMENT:Ljava/lang/String;

    const-string v0, "is_message_empty"

    .line 124
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_IS_MESSAGE_EMPTY:Ljava/lang/String;

    const-string v0, "is_suggestion_read_event_sent"

    .line 125
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_IS_SUGGESTION_READ_EVENT_SENT:Ljava/lang/String;

    const-string v0, "suggestion_read_faq_publish_id"

    .line 126
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_SUGGESTION_READ_FAQ_PUBLISH_ID:Ljava/lang/String;

    const-string v0, "dt"

    .line 127
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_DATE_TIME:Ljava/lang/String;

    const-string v0, "timezone_id"

    .line 128
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->KEY_TIMEZONE_ID:Ljava/lang/String;

    .line 134
    new-instance v0, Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-direct {v0}, Lcom/helpshift/common/conversation/ConversationDBInfo;-><init>()V

    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    .line 135
    new-instance v0, Lcom/helpshift/platform/db/ConversationDBHelper;

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-direct {v0, p1, v1}, Lcom/helpshift/platform/db/ConversationDBHelper;-><init>(Landroid/content/Context;Lcom/helpshift/common/conversation/ConversationDBInfo;)V

    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    return-void
.end method

.method private buildJsonObjectForAttachmentMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "content_type"

    .line 1651
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->contentType:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "file_name"

    .line 1652
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->fileName:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "filePath"

    .line 1653
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->filePath:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "url"

    .line 1654
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->attachmentUrl:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "size"

    .line 1655
    iget v1, p2, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->size:I

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    const-string v0, "is_secure"

    .line 1656
    iget-boolean p2, p2, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->isSecureAttachment:Z

    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForAdminBotControlMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "bot_action_type"

    .line 1540
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->actionType:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "has_next_bot"

    .line 1541
    iget-boolean p2, p2, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->hasNextBot:Z

    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForAutoRetriableMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "message_sync_status"

    .line 1680
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->getSyncStatus()I

    move-result p2

    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForBotInfo(Lorg/json/JSONObject;Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "chatbot_info"

    .line 1593
    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForDateTime(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 1586
    iget v0, p2, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->keyboard:I

    const/4 v1, 0x4

    if-ne v0, v1, :cond_0

    const-string v0, "dt"

    .line 1587
    iget-wide v1, p2, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->dateInMillis:J

    invoke-virtual {p1, v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;J)Lorg/json/JSONObject;

    const-string v0, "timezone_id"

    .line 1588
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->timeZoneId:Ljava/lang/String;

    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    :cond_0
    return-void
.end method

.method private buildMetaForFAQList(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;)V
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 1554
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->faqs:Ljava/util/List;

    if-eqz v0, :cond_1

    .line 1555
    new-instance v0, Lorg/json/JSONArray;

    invoke-direct {v0}, Lorg/json/JSONArray;-><init>()V

    .line 1556
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->faqs:Ljava/util/List;

    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;

    .line 1557
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2}, Lorg/json/JSONObject;-><init>()V

    const-string v3, "faq_title"

    .line 1558
    iget-object v4, v1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;->title:Ljava/lang/String;

    invoke-virtual {v2, v3, v4}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v3, "faq_publish_id"

    .line 1559
    iget-object v4, v1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;->publishId:Ljava/lang/String;

    invoke-virtual {v2, v3, v4}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v3, "faq_language"

    .line 1560
    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;->language:Ljava/lang/String;

    invoke-virtual {v2, v3, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 1561
    invoke-virtual {v0, v2}, Lorg/json/JSONArray;->put(Ljava/lang/Object;)Lorg/json/JSONArray;

    goto :goto_0

    :cond_0
    const-string p2, "faqs"

    .line 1563
    invoke-virtual {p1, p2, v0}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    :cond_1
    return-void
.end method

.method private buildMetaForFollowUpRejected(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "referredMessageId"

    .line 1639
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;->referredMessageId:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "rejected_reason"

    .line 1640
    iget v1, p2, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;->reason:I

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    const-string v0, "rejected_conv_id"

    .line 1641
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;->openConversationId:Ljava/lang/String;

    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForImageAttachmentMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/ImageAttachmentMessageDM;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 1671
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->buildJsonObjectForAttachmentMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V

    const-string v0, "thumbnail_url"

    .line 1672
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/ImageAttachmentMessageDM;->thumbnailUrl:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "thumbnailFilePath"

    .line 1673
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/ImageAttachmentMessageDM;->thumbnailFilePath:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "is_secure"

    .line 1674
    iget-boolean p2, p2, Lcom/helpshift/conversation/activeconversation/message/ImageAttachmentMessageDM;->isSecureAttachment:Z

    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForInput(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;)V
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 1607
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->botInfo:Ljava/lang/String;

    invoke-direct {p0, p1, v0}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForBotInfo(Lorg/json/JSONObject;Ljava/lang/String;)V

    const-string v0, "input_required"

    .line 1608
    iget-boolean v1, p2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->required:Z

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    const-string v0, "input_label"

    .line 1609
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->inputLabel:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "input_skip_label"

    .line 1610
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->skipLabel:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 1611
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->options:Ljava/util/List;

    if-eqz v0, :cond_1

    .line 1612
    new-instance v0, Lorg/json/JSONArray;

    invoke-direct {v0}, Lorg/json/JSONArray;-><init>()V

    .line 1613
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->options:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    .line 1614
    new-instance v3, Lorg/json/JSONObject;

    invoke-direct {v3}, Lorg/json/JSONObject;-><init>()V

    const-string v4, "option_title"

    .line 1615
    iget-object v5, v2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->title:Ljava/lang/String;

    invoke-virtual {v3, v4, v5}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v4, "option_data"

    .line 1616
    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->jsonData:Ljava/lang/String;

    invoke-virtual {v3, v4, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 1617
    invoke-virtual {v0, v3}, Lorg/json/JSONArray;->put(Ljava/lang/Object;)Lorg/json/JSONArray;

    goto :goto_0

    :cond_0
    const-string v1, "input_options"

    .line 1619
    invoke-virtual {p1, v1, v0}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    :cond_1
    const-string v0, "option_type"

    .line 1621
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->type:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForInput(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/input/TextInput;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 1597
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->botInfo:Ljava/lang/String;

    invoke-direct {p0, p1, v0}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForBotInfo(Lorg/json/JSONObject;Ljava/lang/String;)V

    const-string v0, "input_required"

    .line 1598
    iget-boolean v1, p2, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->required:Z

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    const-string v0, "input_skip_label"

    .line 1599
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->skipLabel:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "input_label"

    .line 1600
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->inputLabel:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "input_placeholder"

    .line 1601
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->placeholder:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 1602
    iget p2, p2, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->keyboard:I

    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForInputKeyboard(Lorg/json/JSONObject;I)V

    return-void
.end method

.method private buildMetaForInputKeyboard(Lorg/json/JSONObject;I)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "input_keyboard"

    .line 1581
    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForIsAnswered(Lorg/json/JSONObject;Z)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "is_answered"

    .line 1646
    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForIsMessageEmpty(Lorg/json/JSONObject;Z)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "is_message_empty"

    .line 1535
    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForIsResponseSkipped(Lorg/json/JSONObject;Z)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "is_response_skipped"

    .line 1577
    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForIsSuggestionsReadEvent(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "is_suggestion_read_event_sent"

    .line 1530
    iget-boolean v1, p2, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->isSuggestionsReadEventSent:Z

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    const-string v0, "suggestion_read_faq_publish_id"

    .line 1531
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->suggestionsReadFAQPublishId:Ljava/lang/String;

    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForMessageSeenData(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "seen_cursor"

    .line 1626
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->seenAtMessageCursor:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "seen_sync_status"

    .line 1627
    iget-boolean v1, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isMessageSeenSynced:Z

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    const-string v0, "read_at"

    .line 1628
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->readAt:Ljava/lang/String;

    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForReferredMessageId(Lorg/json/JSONObject;Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "referredMessageId"

    .line 1633
    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForReferredMessageType(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "referred_message_type"

    .line 1569
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->getValue()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForScreenshotAttachmentMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 1662
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->buildJsonObjectForAttachmentMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V

    const-string v0, "thumbnail_url"

    .line 1663
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->thumbnailUrl:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "referredMessageId"

    .line 1664
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->refersMessageId:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "is_secure"

    .line 1665
    iget-boolean p2, p2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->isSecureAttachment:Z

    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForSelectedOptionData(Lorg/json/JSONObject;Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "selected_option_data"

    .line 1573
    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    return-void
.end method

.method private buildMetaForUserBotControlMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "bot_action_type"

    .line 1546
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->actionType:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "chatbot_info"

    .line 1547
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->botInfo:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "bot_ended_reason"

    .line 1548
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->reason:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "referredMessageId"

    .line 1550
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->refersMessageId:Ljava/lang/String;

    invoke-virtual {p1, v0, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    return-void
.end method

.method private conversationInboxRecordToContentValues(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Landroid/content/ContentValues;
    .locals 4

    .line 776
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    .line 777
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "user_local_id"

    iget-wide v2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->userLocalId:J

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    .line 778
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "form_name"

    iget-object v2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->formName:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 779
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "form_email"

    iget-object v2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->formEmail:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 780
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "description_draft"

    iget-object v2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->description:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 781
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "description_draft_timestamp"

    iget-wide v2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->descriptionTimeStamp:J

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    .line 782
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "description_type"

    iget v2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->descriptionType:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    .line 783
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "archival_text"

    iget-object v2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->archivalText:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 784
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "reply_text"

    iget-object v2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->replyText:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 785
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "persist_message_box"

    iget-boolean v2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->persistMessageBox:Z

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    .line 786
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "since"

    iget-object v2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->lastSyncTimestamp:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 788
    iget-object v1, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->hasOlderMessages:Ljava/lang/Boolean;

    if-eqz v1, :cond_0

    .line 789
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "has_older_messages"

    iget-object v2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->hasOlderMessages:Ljava/lang/Boolean;

    invoke-virtual {v2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    .line 791
    :cond_0
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "last_conv_redaction_time"

    iget-object v2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->lastConversationsRedactionTime:Ljava/lang/Long;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    .line 794
    :try_start_0
    iget-object p1, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->imageAttachmentDraft:Lcom/helpshift/conversation/dto/ImagePickerFile;

    invoke-direct {p0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->getImageAttachmentDraftMeta(Lcom/helpshift/conversation/dto/ImagePickerFile;)Ljava/lang/String;

    move-result-object p1

    .line 795
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "attachment_draft"

    invoke-virtual {v0, v1, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in generating meta string for image attachment"

    .line 798
    invoke-static {v1, v2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_0
    return-object v0
.end method

.method private cursorToConversationInboxRecord(Landroid/database/Cursor;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
    .locals 19

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    .line 746
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "user_local_id"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v4

    .line 747
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "form_name"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v6

    .line 748
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "form_email"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v7

    .line 749
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "description_draft"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v8

    .line 750
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "description_draft_timestamp"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v9

    .line 751
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "attachment_draft"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v2

    .line 752
    invoke-direct {v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->parseAndGetImageAttachmentDraft(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ImagePickerFile;

    move-result-object v11

    .line 753
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "description_type"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getInt(I)I

    move-result v12

    .line 754
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "archival_text"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v13

    .line 755
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "reply_text"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v14

    .line 756
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "persist_message_box"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getInt(I)I

    move-result v2

    const/4 v3, 0x1

    if-ne v2, v3, :cond_0

    const/4 v15, 0x1

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    const/4 v15, 0x0

    .line 757
    :goto_0
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "since"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v16

    .line 758
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "has_older_messages"

    invoke-static {v1, v2}, Lcom/helpshift/util/DatabaseUtils;->parseBooleanColumnSafe(Landroid/database/Cursor;Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object v17

    .line 759
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "last_conv_redaction_time"

    const-class v3, Ljava/lang/Long;

    invoke-static {v1, v2, v3}, Lcom/helpshift/util/DatabaseUtils;->parseColumnSafe(Landroid/database/Cursor;Ljava/lang/String;Ljava/lang/Class;)Ljava/lang/Object;

    move-result-object v1

    move-object/from16 v18, v1

    check-cast v18, Ljava/lang/Long;

    .line 760
    new-instance v1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-object v3, v1

    invoke-direct/range {v3 .. v18}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;-><init>(JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;JLcom/helpshift/conversation/dto/ImagePickerFile;ILjava/lang/String;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/Boolean;Ljava/lang/Long;)V

    return-object v1
.end method

.method private cursorToFaq(Landroid/database/Cursor;)Lcom/helpshift/support/Faq;
    .locals 14

    .line 1753
    new-instance v13, Lcom/helpshift/support/Faq;

    const-string v0, "_id"

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v1

    const-string v0, "question_id"

    .line 1754
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v3

    const-string v0, "publish_id"

    .line 1755
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v4

    const-string v0, "language"

    .line 1756
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v5

    const-string v0, "section_id"

    .line 1757
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v6

    const-string v0, "title"

    .line 1758
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v7

    const-string v0, "body"

    .line 1759
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v8

    const-string v0, "helpful"

    .line 1760
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result v9

    const-string v0, "rtl"

    .line 1761
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result v0

    const/4 v10, 0x1

    if-ne v0, v10, :cond_0

    goto :goto_0

    :cond_0
    const/4 v10, 0x0

    :goto_0
    invoke-static {v10}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v10

    const-string v0, "tags"

    .line 1762
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/util/HSJSONUtils;->jsonToStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v11

    const-string v0, "c_tags"

    .line 1764
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/util/HSJSONUtils;->jsonToStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v12

    move-object v0, v13

    invoke-direct/range {v0 .. v12}, Lcom/helpshift/support/Faq;-><init>(JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILjava/lang/Boolean;Ljava/util/List;Ljava/util/List;)V

    return-object v13
.end method

.method private cursorToMessageDM(Landroid/database/Cursor;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 29
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    .line 973
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "_id"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v2

    .line 974
    iget-object v4, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "conversation_id"

    invoke-interface {v1, v4}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v4

    invoke-interface {v1, v4}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v4

    .line 975
    iget-object v6, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v6, "server_id"

    invoke-interface {v1, v6}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v6

    invoke-interface {v1, v6}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v8

    .line 976
    iget-object v6, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v6, "body"

    invoke-interface {v1, v6}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v6

    invoke-interface {v1, v6}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v10

    .line 977
    iget-object v6, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v6, "author_name"

    invoke-interface {v1, v6}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v6

    invoke-interface {v1, v6}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v14

    .line 978
    iget-object v6, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v6, "meta"

    invoke-interface {v1, v6}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v6

    invoke-interface {v1, v6}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v6

    .line 979
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "type"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v7

    .line 980
    iget-object v9, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v9}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v9, "created_at"

    invoke-interface {v1, v9}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v9

    invoke-interface {v1, v9}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v11

    .line 981
    invoke-static {v11}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v12

    .line 982
    iget-object v9, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v9}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v9, "md_state"

    invoke-interface {v1, v9}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v9

    invoke-interface {v1, v9}, Landroid/database/Cursor;->getInt(I)I

    move-result v15

    .line 983
    iget-object v9, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v9}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v9, "is_redacted"

    move/from16 v22, v15

    const/4 v15, 0x0

    invoke-static {v1, v9, v15}, Lcom/helpshift/util/DatabaseUtils;->parseBooleanColumnSafe(Landroid/database/Cursor;Ljava/lang/String;Z)Z

    move-result v1

    .line 984
    invoke-static {v7}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->fromValue(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-result-object v7

    .line 986
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->jsonify(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v6

    .line 987
    sget-object v9, Lcom/helpshift/common/conversation/ConversationDB$1;->$SwitchMap$com$helpshift$conversation$activeconversation$message$MessageType:[I

    invoke-virtual {v7}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ordinal()I

    move-result v7

    aget v7, v9, v7

    packed-switch v7, :pswitch_data_0

    const/4 v1, 0x0

    return-object v1

    .line 1189
    :pswitch_0
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseBotActionTypeFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v15

    .line 1190
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseBotInfoFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v17

    .line 1191
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseBotEndedReasonFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v16

    .line 1192
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseReferredMessageIdFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v18

    .line 1193
    invoke-direct {v0, v8, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseAndGetMessageSyncState(Ljava/lang/String;Lorg/json/JSONObject;)I

    move-result v19

    .line 1195
    new-instance v7, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;

    move-object v9, v7

    move/from16 v23, v1

    move/from16 v1, v22

    invoke-direct/range {v9 .. v19}, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V

    .line 1197
    iput-object v8, v7, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    move/from16 v24, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move-object v1, v7

    goto/16 :goto_2

    :pswitch_1
    move/from16 v23, v1

    move/from16 v1, v22

    .line 1179
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseBotActionTypeFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v15

    .line 1180
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseBotInfoFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v16

    .line 1181
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseHasNextBotFromMeta(Lorg/json/JSONObject;)Ljava/lang/Boolean;

    move-result-object v17

    .line 1182
    new-instance v9, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;

    move-object v7, v9

    move/from16 v24, v1

    move-object v1, v9

    move-object v9, v10

    move-object v10, v11

    move-wide v11, v12

    move-object v13, v14

    move-object v14, v15

    move-object/from16 v15, v16

    invoke-direct/range {v7 .. v15}, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 1185
    invoke-virtual/range {v17 .. v17}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v7

    iput-boolean v7, v1, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->hasNextBot:Z

    goto :goto_0

    :pswitch_2
    move/from16 v23, v1

    move/from16 v24, v22

    .line 1169
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;

    move-object v7, v1

    move-object v9, v10

    move-object v10, v11

    move-wide v11, v12

    move-object v13, v14

    invoke-direct/range {v7 .. v13}, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;)V

    .line 1175
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseIsAnsweredFromMeta(Lorg/json/JSONObject;)Z

    move-result v7

    invoke-virtual {v1, v7}, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;->setAnswered(Z)V

    :goto_0
    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    goto/16 :goto_2

    :pswitch_3
    move/from16 v23, v1

    move/from16 v24, v22

    .line 1150
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseImageAttachmentInfoFromMeta(Lorg/json/JSONObject;)Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;

    move-result-object v1

    .line 1151
    new-instance v15, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    iget-object v9, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->url:Ljava/lang/String;

    iget-object v7, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->fileName:Ljava/lang/String;

    move-wide/from16 v25, v2

    iget-object v2, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->thumbnailUrl:Ljava/lang/String;

    iget-object v3, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->contentType:Ljava/lang/String;

    move-wide/from16 v27, v4

    iget-boolean v4, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->isSecure:Z

    iget v5, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->size:I

    move-object/from16 v16, v7

    move-object v7, v15

    move-object/from16 v17, v9

    move-object v9, v10

    move-object v10, v11

    move-wide v11, v12

    move-object v13, v14

    move-object/from16 v14, v17

    move-object v0, v15

    move-object/from16 v15, v16

    move-object/from16 v16, v2

    move-object/from16 v17, v3

    move/from16 v18, v4

    move/from16 v19, v5

    invoke-direct/range {v7 .. v19}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZI)V

    .line 1163
    iget-object v2, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->filePath:Ljava/lang/String;

    iput-object v2, v0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->filePath:Ljava/lang/String;

    .line 1164
    iget-object v1, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->thumbnailFilePath:Ljava/lang/String;

    iput-object v1, v0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->thumbnailFilePath:Ljava/lang/String;

    .line 1165
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->updateState()V

    move-object v1, v0

    move-object/from16 v0, p0

    goto/16 :goto_2

    :pswitch_4
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1134
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseAttachmentInfoFromMeta(Lorg/json/JSONObject;)Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;

    move-result-object v1

    .line 1135
    new-instance v2, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;

    iget v3, v1, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->size:I

    iget-object v15, v1, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->contentType:Ljava/lang/String;

    iget-object v4, v1, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->url:Ljava/lang/String;

    iget-object v5, v1, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->fileName:Ljava/lang/String;

    iget-boolean v9, v1, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->isSecure:Z

    move-object v7, v2

    move/from16 v18, v9

    move-object v9, v10

    move-object v10, v11

    move-wide v11, v12

    move-object v13, v14

    move v14, v3

    move-object/from16 v16, v4

    move-object/from16 v17, v5

    invoke-direct/range {v7 .. v18}, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)V

    .line 1145
    iget-object v1, v1, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->filePath:Ljava/lang/String;

    iput-object v1, v2, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;->filePath:Ljava/lang/String;

    .line 1146
    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;->updateState()V

    goto :goto_1

    :pswitch_5
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1126
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    .line 1131
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseIsAnsweredFromMeta(Lorg/json/JSONObject;)Z

    move-result v2

    move-object v7, v1

    move-object v9, v10

    move-object v10, v11

    move-wide v11, v12

    move-object v13, v14

    move v14, v2

    invoke-direct/range {v7 .. v14}, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Z)V

    goto/16 :goto_2

    :pswitch_6
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1109
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseImageAttachmentInfoFromMeta(Lorg/json/JSONObject;)Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;

    move-result-object v1

    .line 1110
    new-instance v2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    iget-object v15, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->contentType:Ljava/lang/String;

    iget-object v3, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->thumbnailUrl:Ljava/lang/String;

    iget-object v4, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->fileName:Ljava/lang/String;

    iget-object v5, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->url:Ljava/lang/String;

    iget v7, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->size:I

    iget-boolean v9, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->isSecure:Z

    move/from16 v20, v9

    move-object v9, v2

    move-object/from16 v16, v3

    move-object/from16 v17, v4

    move-object/from16 v18, v5

    move/from16 v19, v7

    invoke-direct/range {v9 .. v20}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;IZ)V

    .line 1120
    iget-object v1, v1, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->filePath:Ljava/lang/String;

    iput-object v1, v2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->filePath:Ljava/lang/String;

    .line 1121
    iput-object v8, v2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->serverId:Ljava/lang/String;

    .line 1122
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseReferredMessageIdFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v2, v1}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->setRefersMessageId(Ljava/lang/String;)V

    :goto_1
    move-object v1, v2

    goto/16 :goto_2

    :pswitch_7
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1101
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;

    .line 1105
    invoke-direct {v0, v8, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseAndGetMessageSyncState(Ljava/lang/String;Lorg/json/JSONObject;)I

    move-result v15

    move-object v9, v1

    invoke-direct/range {v9 .. v15}, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;I)V

    .line 1106
    iput-object v8, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    goto/16 :goto_2

    :pswitch_8
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1093
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;

    .line 1097
    invoke-direct {v0, v8, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseAndGetMessageSyncState(Ljava/lang/String;Lorg/json/JSONObject;)I

    move-result v15

    move-object v9, v1

    invoke-direct/range {v9 .. v15}, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;I)V

    .line 1098
    iput-object v8, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    goto/16 :goto_2

    :pswitch_9
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1083
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;

    .line 1087
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseReferredMessageIdFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v15

    .line 1088
    invoke-direct {v0, v8, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseAndGetMessageSyncState(Ljava/lang/String;Lorg/json/JSONObject;)I

    move-result v16

    move-object v9, v1

    invoke-direct/range {v9 .. v16}, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;I)V

    .line 1089
    iput-object v8, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    .line 1090
    move-object v2, v1

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;

    invoke-direct {v0, v2, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseAndSetFollowUpRejectedDataFromMeta(Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;Lorg/json/JSONObject;)V

    goto/16 :goto_2

    :pswitch_a
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1074
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;

    .line 1078
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseReferredMessageIdFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v15

    .line 1079
    invoke-direct {v0, v8, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseAndGetMessageSyncState(Ljava/lang/String;Lorg/json/JSONObject;)I

    move-result v16

    move-object v9, v1

    invoke-direct/range {v9 .. v16}, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;I)V

    .line 1080
    iput-object v8, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    goto/16 :goto_2

    :pswitch_b
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1066
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;

    .line 1071
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseIsAnsweredFromMeta(Lorg/json/JSONObject;)Z

    move-result v2

    move-object v7, v1

    move-object v9, v10

    move-object v10, v11

    move-wide v11, v12

    move-object v13, v14

    move v14, v2

    invoke-direct/range {v7 .. v14}, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Z)V

    goto/16 :goto_2

    :pswitch_c
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1057
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;

    .line 1061
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseReferredMessageIdFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v15

    .line 1062
    invoke-direct {v0, v8, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseAndGetMessageSyncState(Ljava/lang/String;Lorg/json/JSONObject;)I

    move-result v16

    move-object v9, v1

    invoke-direct/range {v9 .. v16}, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;I)V

    .line 1063
    iput-object v8, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    goto/16 :goto_2

    :pswitch_d
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1046
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;

    .line 1047
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseFAQListFromMeta(Lorg/json/JSONObject;)Ljava/util/List;

    move-result-object v2

    .line 1048
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseBotInfoFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v15

    .line 1049
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputRequiredFromMeta(Lorg/json/JSONObject;)Z

    move-result v16

    .line 1050
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputLabelFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v17

    .line 1051
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputSkipLabelFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v18

    .line 1052
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputOptionsFromMeta(Lorg/json/JSONObject;)Ljava/util/List;

    move-result-object v19

    .line 1053
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseIsSuggestionsReadEventSent(Lorg/json/JSONObject;)Z

    move-result v20

    .line 1054
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseSuggestionReadFAQPublishId(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v21

    move-object v7, v1

    move-object v9, v10

    move-object v10, v11

    move-wide v11, v12

    move-object v13, v14

    move-object v14, v2

    invoke-direct/range {v7 .. v21}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/util/List;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Ljava/util/List;ZLjava/lang/String;)V

    goto/16 :goto_2

    :pswitch_e
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1040
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    .line 1041
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseFAQListFromMeta(Lorg/json/JSONObject;)Ljava/util/List;

    move-result-object v2

    .line 1042
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseIsSuggestionsReadEventSent(Lorg/json/JSONObject;)Z

    move-result v15

    .line 1043
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseSuggestionReadFAQPublishId(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v16

    move-object v7, v1

    move-object v9, v10

    move-object v10, v11

    move-wide v11, v12

    move-object v13, v14

    move-object v14, v2

    invoke-direct/range {v7 .. v16}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/util/List;ZLjava/lang/String;)V

    goto/16 :goto_2

    :pswitch_f
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1029
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputOptionsFromMeta(Lorg/json/JSONObject;)Ljava/util/List;

    move-result-object v18

    .line 1030
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;

    .line 1032
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseBotInfoFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v2

    .line 1033
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputRequiredFromMeta(Lorg/json/JSONObject;)Z

    move-result v15

    .line 1034
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputLabelFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v16

    .line 1035
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputSkipLabelFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v17

    .line 1037
    invoke-interface/range {v18 .. v18}, Ljava/util/List;->size()I

    move-result v3

    invoke-direct {v0, v6, v3}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputOptionTypeFromMeta(Lorg/json/JSONObject;I)Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    move-result-object v19

    move-object v7, v1

    move-object v9, v10

    move-object v10, v11

    move-wide v11, v12

    move-object v13, v14

    move-object v14, v2

    invoke-direct/range {v7 .. v19}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Ljava/util/List;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;)V

    goto/16 :goto_2

    :pswitch_10
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1019
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    .line 1020
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseBotInfoFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v2

    .line 1021
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputPlaceholderFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v15

    .line 1022
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputRequiredFromMeta(Lorg/json/JSONObject;)Z

    move-result v16

    .line 1023
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputLabelFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v17

    .line 1024
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputSkipLabelFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v18

    .line 1025
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputKeyboardFromMeta(Lorg/json/JSONObject;)I

    move-result v19

    .line 1026
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseIsMessageEmptyFromMeta(Lorg/json/JSONObject;)Z

    move-result v20

    move-object v7, v1

    move-object v9, v10

    move-object v10, v11

    move-wide v11, v12

    move-object v13, v14

    move-object v14, v2

    invoke-direct/range {v7 .. v20}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;IZ)V

    goto/16 :goto_2

    :pswitch_11
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1016
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;

    move-object v7, v1

    move-object v9, v10

    move-object v10, v11

    move-wide v11, v12

    move-object v13, v14

    invoke-direct/range {v7 .. v13}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;)V

    goto :goto_2

    :pswitch_12
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 1007
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;

    .line 1008
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseBotInfoFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v15

    .line 1009
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseIsResponseSkippedFromMeta(Lorg/json/JSONObject;)Z

    move-result v16

    .line 1010
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseSelectedOptionDataFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v17

    .line 1011
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseReferredMessageIdFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v18

    .line 1012
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseReferredMessageTypeFromMeta(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-result-object v19

    move-object v9, v1

    invoke-direct/range {v9 .. v19}, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 1013
    iput-object v8, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    goto :goto_2

    :pswitch_13
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 993
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;

    .line 995
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseInputKeyboardFromMeta(Lorg/json/JSONObject;)I

    move-result v15

    .line 996
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseBotInfoFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v16

    .line 997
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseIsResponseSkippedFromMeta(Lorg/json/JSONObject;)Z

    move-result v17

    .line 998
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseReferredMessageIdFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v18

    .line 999
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseIsMessageEmptyFromMeta(Lorg/json/JSONObject;)Z

    move-result v19

    move-object v9, v1

    invoke-direct/range {v9 .. v19}, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ILjava/lang/String;ZLjava/lang/String;Z)V

    .line 1001
    iput-object v8, v1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->serverId:Ljava/lang/String;

    .line 1002
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseDateTimeFromMeta(Lorg/json/JSONObject;)J

    move-result-wide v2

    iput-wide v2, v1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->dateInMillis:J

    .line 1003
    invoke-direct {v0, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseTimeZoneIdFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v2

    iput-object v2, v1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->timeZoneId:Ljava/lang/String;

    goto :goto_2

    :pswitch_14
    move/from16 v23, v1

    move-wide/from16 v25, v2

    move-wide/from16 v27, v4

    move/from16 v24, v22

    .line 989
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    move-object v9, v1

    invoke-direct/range {v9 .. v14}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;)V

    .line 990
    iput-object v8, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    .line 1206
    :goto_2
    invoke-static/range {v27 .. v28}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    iput-object v2, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 1207
    invoke-static/range {v25 .. v26}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    iput-object v2, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    move/from16 v2, v24

    .line 1208
    iput v2, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->deliveryState:I

    move/from16 v2, v23

    .line 1209
    iput-boolean v2, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isRedacted:Z

    .line 1210
    invoke-direct {v0, v1, v6}, Lcom/helpshift/common/conversation/ConversationDB;->parseAndSetMessageSeenData(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V

    return-object v1

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_14
        :pswitch_13
        :pswitch_12
        :pswitch_11
        :pswitch_10
        :pswitch_f
        :pswitch_e
        :pswitch_d
        :pswitch_c
        :pswitch_b
        :pswitch_a
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

.method private cursorToReadableConversation(Landroid/database/Cursor;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 32

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    .line 861
    iget-object v2, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "_id"

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v1, v2}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    .line 862
    iget-object v3, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "user_local_id"

    invoke-interface {v1, v3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v3

    invoke-interface {v1, v3}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v3

    .line 863
    iget-object v5, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v5}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v5, "server_id"

    invoke-interface {v1, v5}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v5

    invoke-interface {v1, v5}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v5

    .line 864
    iget-object v6, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v6, "publish_id"

    invoke-interface {v1, v6}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v6

    invoke-interface {v1, v6}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v14

    .line 865
    iget-object v6, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v6, "uuid"

    invoke-interface {v1, v6}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v6

    invoke-interface {v1, v6}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v6

    .line 866
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "title"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v8

    .line 867
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "show_agent_name"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getInt(I)I

    move-result v7

    const/4 v10, 0x1

    if-ne v7, v10, :cond_0

    const/16 v16, 0x1

    goto :goto_0

    :cond_0
    const/16 v16, 0x0

    .line 869
    :goto_0
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "message_cursor"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v15

    .line 870
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "start_new_conversation_action"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getInt(I)I

    move-result v7

    if-ne v7, v10, :cond_1

    const/4 v13, 0x1

    goto :goto_1

    :cond_1
    const/4 v13, 0x0

    .line 872
    :goto_1
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "meta"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v11

    .line 873
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "created_at"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v10

    .line 874
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "epoch_time_created_at"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v17

    .line 875
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "updated_at"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v19

    .line 876
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "pre_conv_server_id"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v12

    .line 877
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "last_user_activity_time"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    move-object/from16 v20, v10

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v9

    .line 878
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "issue_type"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v21

    .line 879
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "full_privacy_enabled"

    move-wide/from16 v22, v9

    const/4 v9, 0x0

    invoke-static {v1, v7, v9}, Lcom/helpshift/util/DatabaseUtils;->parseBooleanColumnSafe(Landroid/database/Cursor;Ljava/lang/String;Z)Z

    move-result v10

    .line 880
    iget-object v7, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v7, "state"

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    invoke-interface {v1, v7}, Landroid/database/Cursor;->getInt(I)I

    move-result v7

    .line 881
    invoke-static {v7}, Lcom/helpshift/conversation/dto/IssueState;->fromInt(I)Lcom/helpshift/conversation/dto/IssueState;

    move-result-object v7

    .line 882
    iget-object v9, v0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v9}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v9, "is_redacted"

    move-object/from16 v24, v7

    const/4 v7, 0x0

    invoke-static {v1, v9, v7}, Lcom/helpshift/util/DatabaseUtils;->parseBooleanColumnSafe(Landroid/database/Cursor;Ljava/lang/String;Z)Z

    move-result v1

    .line 884
    new-instance v9, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-object/from16 v25, v24

    move-object v7, v9

    move/from16 v26, v1

    move-object v0, v9

    move-wide/from16 v27, v22

    move-object/from16 v9, v25

    move v1, v10

    move-object/from16 v10, v20

    move/from16 v30, v1

    move-object/from16 v29, v11

    move-object v1, v12

    move-wide/from16 v11, v17

    move/from16 v31, v13

    move-object/from16 v13, v19

    move-object/from16 v17, v21

    invoke-direct/range {v7 .. v17}, Lcom/helpshift/conversation/activeconversation/model/Conversation;-><init>(Ljava/lang/String;Lcom/helpshift/conversation/dto/IssueState;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;ZLjava/lang/String;)V

    .line 893
    iput-object v5, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    .line 894
    iput-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 895
    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setLocalId(J)V

    .line 896
    iput-object v6, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    move-object/from16 v1, v25

    .line 897
    iput-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 898
    iput-wide v3, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    move/from16 v9, v31

    .line 899
    iput-boolean v9, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isStartNewConversationClicked:Z

    move-wide/from16 v1, v27

    .line 900
    iput-wide v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->lastUserActivityTime:J

    move/from16 v1, v30

    .line 901
    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->wasFullPrivacyEnabledAtCreation:Z

    move/from16 v1, v26

    .line 902
    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isRedacted:Z

    move-object/from16 v2, v29

    move-object/from16 v1, p0

    .line 903
    invoke-direct {v1, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->parseAndSetMetaData(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;)V

    return-object v0
.end method

.method private exists(Landroid/database/sqlite/SQLiteDatabase;Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)Z
    .locals 2

    .line 740
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "SELECT COUNT(*) FROM "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, " WHERE "

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, " LIMIT 1"

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2, p4}, Landroid/database/DatabaseUtils;->longForQuery(Landroid/database/sqlite/SQLiteDatabase;Ljava/lang/String;[Ljava/lang/String;)J

    move-result-wide p1

    const-wide/16 p3, 0x0

    cmp-long v0, p1, p3

    if-lez v0, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method private static faqToContentValues(Lcom/helpshift/support/Faq;)Landroid/content/ContentValues;
    .locals 4

    .line 146
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    const-string v1, "question_id"

    .line 147
    invoke-virtual {p0}, Lcom/helpshift/support/Faq;->getId()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "publish_id"

    .line 148
    iget-object v2, p0, Lcom/helpshift/support/Faq;->publish_id:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "language"

    .line 149
    iget-object v2, p0, Lcom/helpshift/support/Faq;->language:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "section_id"

    .line 150
    iget-object v2, p0, Lcom/helpshift/support/Faq;->section_publish_id:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "title"

    .line 151
    iget-object v2, p0, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "body"

    .line 152
    iget-object v2, p0, Lcom/helpshift/support/Faq;->body:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "helpful"

    .line 153
    iget v2, p0, Lcom/helpshift/support/Faq;->is_helpful:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v1, "rtl"

    .line 154
    iget-object v2, p0, Lcom/helpshift/support/Faq;->is_rtl:Ljava/lang/Boolean;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Boolean;)V

    const-string v1, "tags"

    .line 155
    new-instance v2, Lorg/json/JSONArray;

    invoke-virtual {p0}, Lcom/helpshift/support/Faq;->getTags()Ljava/util/List;

    move-result-object v3

    invoke-direct {v2, v3}, Lorg/json/JSONArray;-><init>(Ljava/util/Collection;)V

    invoke-static {v2}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "c_tags"

    .line 156
    new-instance v2, Lorg/json/JSONArray;

    invoke-virtual {p0}, Lcom/helpshift/support/Faq;->getCategoryTags()Ljava/util/List;

    move-result-object p0

    invoke-direct {v2, p0}, Lorg/json/JSONArray;-><init>(Ljava/util/Collection;)V

    invoke-static {v2}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, v1, p0}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    return-object v0
.end method

.method private getBooleanFromJson(Lorg/json/JSONObject;Ljava/lang/String;Z)Z
    .locals 0
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1281
    invoke-virtual {p1, p2, p3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result p1

    return p1
.end method

.method private getConversationMeta(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/lang/String;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 847
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatState:Lcom/helpshift/conversation/states/ConversationCSATState;

    .line 848
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1}, Lorg/json/JSONObject;-><init>()V

    .line 849
    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatFeedback:Ljava/lang/String;

    .line 850
    iget v3, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatRating:I

    const-string v4, "csat_feedback"

    .line 851
    invoke-virtual {v1, v4, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v2, "csat_rating"

    .line 852
    invoke-virtual {v1, v2, v3}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    const-string v2, "csat_state"

    .line 853
    invoke-virtual {v0}, Lcom/helpshift/conversation/states/ConversationCSATState;->getValue()I

    move-result v0

    invoke-virtual {v1, v2, v0}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    const-string v0, "increment_message_count"

    .line 854
    iget-boolean v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->shouldIncrementMessageCount:Z

    invoke-virtual {v1, v0, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    const-string v0, "ended_delegate_sent"

    .line 855
    iget-boolean v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isConversationEndedDelegateSent:Z

    invoke-virtual {v1, v0, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    const-string v0, "is_autofilled_preissue"

    .line 856
    iget-boolean p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isAutoFilledPreIssue:Z

    invoke-virtual {v1, v0, p1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    .line 857
    invoke-virtual {v1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private getImageAttachmentDraftMeta(Lcom/helpshift/conversation/dto/ImagePickerFile;)Ljava/lang/String;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 838
    :cond_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    const-string v1, "image_draft_orig_name"

    .line 839
    iget-object v2, p1, Lcom/helpshift/conversation/dto/ImagePickerFile;->originalFileName:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "image_draft_orig_size"

    .line 840
    iget-object v2, p1, Lcom/helpshift/conversation/dto/ImagePickerFile;->originalFileSize:Ljava/lang/Long;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "image_draft_file_path"

    .line 841
    iget-object v2, p1, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "image_copy_done"

    .line 842
    iget-boolean p1, p1, Lcom/helpshift/conversation/dto/ImagePickerFile;->isFileCompressionAndCopyingDone:Z

    invoke-virtual {v0, v1, p1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    .line 843
    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public static declared-synchronized getInstance(Landroid/content/Context;)Lcom/helpshift/common/conversation/ConversationDB;
    .locals 2

    const-class v0, Lcom/helpshift/common/conversation/ConversationDB;

    monitor-enter v0

    .line 139
    :try_start_0
    sget-object v1, Lcom/helpshift/common/conversation/ConversationDB;->instance:Lcom/helpshift/common/conversation/ConversationDB;

    if-nez v1, :cond_0

    .line 140
    new-instance v1, Lcom/helpshift/common/conversation/ConversationDB;

    invoke-direct {v1, p0}, Lcom/helpshift/common/conversation/ConversationDB;-><init>(Landroid/content/Context;)V

    sput-object v1, Lcom/helpshift/common/conversation/ConversationDB;->instance:Lcom/helpshift/common/conversation/ConversationDB;

    .line 142
    :cond_0
    sget-object p0, Lcom/helpshift/common/conversation/ConversationDB;->instance:Lcom/helpshift/common/conversation/ConversationDB;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object p0

    :catchall_0
    move-exception p0

    .line 138
    monitor-exit v0

    throw p0
.end method

.method private getIntFromJson(Lorg/json/JSONObject;Ljava/lang/String;I)I
    .locals 0
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1273
    invoke-virtual {p1, p2, p3}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result p1

    return p1
.end method

.method private getMessageMeta(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Ljava/lang/String;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 1398
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    .line 1400
    sget-object v1, Lcom/helpshift/common/conversation/ConversationDB$1;->$SwitchMap$com$helpshift$conversation$activeconversation$message$MessageType:[I

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ordinal()I

    move-result v0

    aget v0, v1, v0

    const/4 v1, 0x0

    packed-switch v0, :pswitch_data_0

    move-object v0, v1

    goto/16 :goto_0

    .line 1507
    :pswitch_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1508
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;

    .line 1509
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForUserBotControlMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;)V

    .line 1510
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForAutoRetriableMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;)V

    goto/16 :goto_0

    .line 1503
    :pswitch_1
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1504
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;

    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForAdminBotControlMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;)V

    goto/16 :goto_0

    .line 1464
    :pswitch_2
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1465
    move-object v2, p1

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;

    .line 1466
    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;->isAnswered()Z

    move-result v2

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForIsAnswered(Lorg/json/JSONObject;Z)V

    .line 1467
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForMessageSeenData(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    goto/16 :goto_0

    .line 1494
    :pswitch_3
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1495
    move-object v2, p1

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/ImageAttachmentMessageDM;

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForImageAttachmentMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/ImageAttachmentMessageDM;)V

    .line 1496
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForMessageSeenData(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    goto/16 :goto_0

    .line 1488
    :pswitch_4
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1489
    move-object v2, p1

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

    .line 1490
    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildJsonObjectForAttachmentMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V

    .line 1491
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForMessageSeenData(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    goto/16 :goto_0

    .line 1458
    :pswitch_5
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1459
    move-object v2, p1

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    .line 1460
    iget-boolean v2, v2, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->isAnswered:Z

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForIsAnswered(Lorg/json/JSONObject;Z)V

    .line 1461
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForMessageSeenData(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    goto/16 :goto_0

    .line 1499
    :pswitch_6
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1500
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForScreenshotAttachmentMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V

    goto/16 :goto_0

    .line 1518
    :pswitch_7
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1519
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;

    .line 1520
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForAutoRetriableMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;)V

    goto/16 :goto_0

    .line 1513
    :pswitch_8
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1514
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;

    .line 1515
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForAutoRetriableMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;)V

    goto/16 :goto_0

    .line 1476
    :pswitch_9
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1477
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;

    .line 1478
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForFollowUpRejected(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;)V

    .line 1479
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForAutoRetriableMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;)V

    goto/16 :goto_0

    .line 1470
    :pswitch_a
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1471
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;

    .line 1472
    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;->referredMessageId:Ljava/lang/String;

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForReferredMessageId(Lorg/json/JSONObject;Ljava/lang/String;)V

    .line 1473
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForAutoRetriableMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;)V

    goto/16 :goto_0

    .line 1482
    :pswitch_b
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1483
    move-object v2, p1

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;

    .line 1484
    iget-boolean v2, v2, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;->isAnswered:Z

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForIsAnswered(Lorg/json/JSONObject;Z)V

    .line 1485
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForMessageSeenData(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    goto/16 :goto_0

    .line 1452
    :pswitch_c
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1453
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;

    .line 1454
    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->referredMessageId:Ljava/lang/String;

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForReferredMessageId(Lorg/json/JSONObject;Ljava/lang/String;)V

    .line 1455
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForAutoRetriableMessage(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;)V

    goto/16 :goto_0

    .line 1445
    :pswitch_d
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1446
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForMessageSeenData(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 1447
    move-object v2, p1

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForFAQList(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;)V

    .line 1448
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForInput(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;)V

    .line 1449
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForIsSuggestionsReadEvent(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;)V

    goto/16 :goto_0

    .line 1439
    :pswitch_e
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1440
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForMessageSeenData(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 1441
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForFAQList(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;)V

    .line 1442
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForIsSuggestionsReadEvent(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;)V

    goto/16 :goto_0

    .line 1434
    :pswitch_f
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1435
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForMessageSeenData(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 1436
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForInput(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;)V

    goto :goto_0

    .line 1427
    :pswitch_10
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1428
    move-object v2, p1

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    .line 1429
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForMessageSeenData(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 1430
    iget-object p1, v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/TextInput;

    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForInput(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/input/TextInput;)V

    .line 1431
    iget-boolean p1, v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->isMessageEmpty:Z

    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForIsMessageEmpty(Lorg/json/JSONObject;Z)V

    goto :goto_0

    .line 1423
    :pswitch_11
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1424
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForMessageSeenData(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    goto :goto_0

    .line 1413
    :pswitch_12
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1414
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;

    .line 1416
    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->botInfo:Ljava/lang/String;

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForBotInfo(Lorg/json/JSONObject;Ljava/lang/String;)V

    .line 1417
    iget-boolean v2, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->skipped:Z

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForIsResponseSkipped(Lorg/json/JSONObject;Z)V

    .line 1418
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->getReferredMessageId()Ljava/lang/String;

    move-result-object v2

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForReferredMessageId(Lorg/json/JSONObject;Ljava/lang/String;)V

    .line 1419
    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForReferredMessageType(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 1420
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->optionData:Ljava/lang/String;

    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForSelectedOptionData(Lorg/json/JSONObject;Ljava/lang/String;)V

    goto :goto_0

    .line 1402
    :pswitch_13
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1403
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;

    .line 1405
    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->botInfo:Ljava/lang/String;

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForBotInfo(Lorg/json/JSONObject;Ljava/lang/String;)V

    .line 1406
    iget v2, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->keyboard:I

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForInputKeyboard(Lorg/json/JSONObject;I)V

    .line 1407
    iget-boolean v2, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->skipped:Z

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForIsResponseSkipped(Lorg/json/JSONObject;Z)V

    .line 1408
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->getReferredMessageId()Ljava/lang/String;

    move-result-object v2

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForReferredMessageId(Lorg/json/JSONObject;Ljava/lang/String;)V

    .line 1409
    iget-boolean v2, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->isMessageEmpty:Z

    invoke-direct {p0, v0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForIsMessageEmpty(Lorg/json/JSONObject;Z)V

    .line 1410
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->buildMetaForDateTime(Lorg/json/JSONObject;Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;)V

    :goto_0
    if-nez v0, :cond_0

    return-object v1

    .line 1526
    :cond_0
    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1

    :pswitch_data_0
    .packed-switch 0x2
        :pswitch_13
        :pswitch_12
        :pswitch_11
        :pswitch_10
        :pswitch_f
        :pswitch_e
        :pswitch_d
        :pswitch_c
        :pswitch_b
        :pswitch_a
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

.method private getStringFromJson(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 0
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1277
    invoke-virtual {p1, p2, p3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private jsonify(Ljava/lang/String;)Lorg/json/JSONObject;
    .locals 3

    .line 1355
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 1357
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    return-object v0

    .line 1362
    :cond_0
    :try_start_0
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    move-object v0, v1

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Exception in jsonify"

    .line 1365
    invoke-static {v1, v2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_0
    return-object v0
.end method

.method private parseAndGetImageAttachmentDraft(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ImagePickerFile;
    .locals 10

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return-object v0

    .line 957
    :cond_0
    :try_start_0
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p1, "image_draft_orig_name"

    .line 958
    invoke-virtual {v1, p1, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    const-string v2, "image_draft_orig_size"

    const-wide/16 v3, -0x1

    .line 959
    invoke-virtual {v1, v2, v3, v4}, Lorg/json/JSONObject;->optLong(Ljava/lang/String;J)J

    move-result-wide v5

    invoke-static {v5, v6}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    const-string v5, "image_draft_file_path"

    .line 960
    invoke-virtual {v1, v5, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v6, "image_copy_done"

    const/4 v7, 0x0

    .line 961
    invoke-virtual {v1, v6, v7}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v1

    .line 962
    new-instance v6, Lcom/helpshift/conversation/dto/ImagePickerFile;

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v7

    cmp-long v9, v7, v3

    if-nez v9, :cond_1

    move-object v2, v0

    :cond_1
    invoke-direct {v6, v5, p1, v2}, Lcom/helpshift/conversation/dto/ImagePickerFile;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Long;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    .line 963
    :try_start_1
    iput-boolean v1, v6, Lcom/helpshift/conversation/dto/ImagePickerFile;->isFileCompressionAndCopyingDone:Z
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_1

    :catch_0
    move-exception p1

    goto :goto_0

    :catch_1
    move-exception p1

    move-object v6, v0

    :goto_0
    const-string v0, "Helpshift_ConverDB"

    const-string v1, "Error in parseAndGetImageAttachmentDraft"

    .line 966
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_1
    return-object v6
.end method

.method private parseAndGetMessageSyncState(Ljava/lang/String;Lorg/json/JSONObject;)I
    .locals 1
    .param p2    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1336
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_0

    const/4 p1, 0x2

    return p1

    :cond_0
    const-string p1, "message_sync_status"

    const/4 v0, 0x1

    .line 1340
    invoke-virtual {p2, p1, v0}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result p1

    return p1
.end method

.method private parseAndSetFollowUpRejectedDataFromMeta(Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;Lorg/json/JSONObject;)V
    .locals 3
    .param p2    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const-string v0, "rejected_reason"

    .line 1389
    invoke-virtual {p2, v0}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;)I

    move-result v0

    const-string v1, "rejected_conv_id"

    const/4 v2, 0x0

    .line 1390
    invoke-virtual {p2, v1, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    .line 1392
    iput v0, p1, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;->reason:I

    .line 1393
    iput-object p2, p1, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;->openConversationId:Ljava/lang/String;

    return-void
.end method

.method private parseAndSetMessageSeenData(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V
    .locals 4
    .param p2    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const-string v0, "read_at"

    const-string v1, ""

    .line 1345
    invoke-virtual {p2, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "seen_cursor"

    const/4 v2, 0x0

    .line 1346
    invoke-virtual {p2, v1, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "seen_sync_status"

    const/4 v3, 0x0

    .line 1347
    invoke-virtual {p2, v2, v3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result p2

    .line 1348
    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->seenAtMessageCursor:Ljava/lang/String;

    .line 1349
    iput-boolean p2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isMessageSeenSynced:Z

    .line 1350
    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->readAt:Ljava/lang/String;

    return-void
.end method

.method private parseAndSetMetaData(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;)V
    .locals 5

    if-nez p2, :cond_0

    return-void

    .line 932
    :cond_0
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p2}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p2, "csat_rating"

    const/4 v1, 0x0

    .line 933
    invoke-virtual {v0, p2, v1}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result p2

    const-string v2, "csat_state"

    .line 934
    sget-object v3, Lcom/helpshift/conversation/states/ConversationCSATState;->NONE:Lcom/helpshift/conversation/states/ConversationCSATState;

    invoke-virtual {v3}, Lcom/helpshift/conversation/states/ConversationCSATState;->getValue()I

    move-result v3

    invoke-virtual {v0, v2, v3}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result v2

    const-string v3, "csat_feedback"

    const/4 v4, 0x0

    .line 935
    invoke-virtual {v0, v3, v4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 936
    iput p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatRating:I

    .line 937
    invoke-static {v2}, Lcom/helpshift/conversation/states/ConversationCSATState;->fromInt(I)Lcom/helpshift/conversation/states/ConversationCSATState;

    move-result-object p2

    iput-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatState:Lcom/helpshift/conversation/states/ConversationCSATState;

    .line 938
    iput-object v3, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatFeedback:Ljava/lang/String;

    const-string p2, "increment_message_count"

    .line 940
    invoke-virtual {v0, p2, v1}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result p2

    iput-boolean p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->shouldIncrementMessageCount:Z

    const-string p2, "ended_delegate_sent"

    .line 942
    invoke-virtual {v0, p2, v1}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result p2

    iput-boolean p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isConversationEndedDelegateSent:Z

    const-string p2, "is_autofilled_preissue"

    .line 944
    invoke-virtual {v0, p2, v1}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result p2

    iput-boolean p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isAutoFilledPreIssue:Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "Helpshift_ConverDB"

    const-string v0, "Error in parseAndSetMetaData"

    .line 947
    invoke-static {p2, v0, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_0
    return-void
.end method

.method private parseAttachmentInfoFromMeta(Lorg/json/JSONObject;)Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;
    .locals 1
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1379
    new-instance v0, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;-><init>(Lcom/helpshift/common/conversation/ConversationDB;Lorg/json/JSONObject;)V

    return-object v0
.end method

.method private parseBotActionTypeFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 2

    const-string v0, "bot_action_type"

    const-string v1, ""

    .line 1223
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private parseBotEndedReasonFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 2

    const-string v0, "bot_ended_reason"

    const-string v1, ""

    .line 1228
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private parseBotInfoFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 2
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const-string v0, "{}"

    const-string v1, "chatbot_info"

    .line 1319
    invoke-virtual {p1, v1, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private parseDateTimeFromMeta(Lorg/json/JSONObject;)J
    .locals 3
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const-string v0, "dt"

    const-wide/16 v1, 0x0

    .line 1324
    invoke-virtual {p1, v0, v1, v2}, Lorg/json/JSONObject;->optLong(Ljava/lang/String;J)J

    move-result-wide v0

    return-wide v0
.end method

.method private parseFAQListFromMeta(Lorg/json/JSONObject;)Ljava/util/List;
    .locals 7
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lorg/json/JSONObject;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;",
            ">;"
        }
    .end annotation

    .line 1240
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    :try_start_0
    const-string v1, "faqs"

    .line 1242
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object p1

    const/4 v1, 0x0

    .line 1243
    :goto_0
    invoke-virtual {p1}, Lorg/json/JSONArray;->length()I

    move-result v2

    if-ge v1, v2, :cond_0

    .line 1244
    invoke-virtual {p1, v1}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v2

    .line 1245
    new-instance v3, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;

    const-string v4, "faq_title"

    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v5, "faq_publish_id"

    .line 1246
    invoke-virtual {v2, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v6, "faq_language"

    .line 1247
    invoke-virtual {v2, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-direct {v3, v4, v5, v2}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 1245
    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    return-object v0

    :catch_0
    return-object v0
.end method

.method private parseHasNextBotFromMeta(Lorg/json/JSONObject;)Ljava/lang/Boolean;
    .locals 2
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const-string v0, "has_next_bot"

    const/4 v1, 0x0

    .line 1332
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result p1

    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    return-object p1
.end method

.method private parseImageAttachmentInfoFromMeta(Lorg/json/JSONObject;)Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;
    .locals 1
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 1383
    new-instance v0, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;-><init>(Lcom/helpshift/common/conversation/ConversationDB;Lorg/json/JSONObject;)V

    return-object v0
.end method

.method private parseInputKeyboardFromMeta(Lorg/json/JSONObject;)I
    .locals 2

    const-string v0, "input_keyboard"

    const/4 v1, 0x1

    .line 1297
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getIntFromJson(Lorg/json/JSONObject;Ljava/lang/String;I)I

    move-result p1

    return p1
.end method

.method private parseInputLabelFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 2

    const-string v0, "input_label"

    const-string v1, ""

    .line 1305
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getStringFromJson(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private parseInputOptionTypeFromMeta(Lorg/json/JSONObject;I)Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;
    .locals 2

    const-string v0, "option_type"

    const-string v1, ""

    .line 1215
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getStringFromJson(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1, p2}, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;->getType(Ljava/lang/String;I)Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    move-result-object p1

    return-object p1
.end method

.method private parseInputOptionsFromMeta(Lorg/json/JSONObject;)Ljava/util/List;
    .locals 6
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lorg/json/JSONObject;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;",
            ">;"
        }
    .end annotation

    .line 1257
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    :try_start_0
    const-string v1, "input_options"

    .line 1259
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object p1

    const/4 v1, 0x0

    .line 1260
    :goto_0
    invoke-virtual {p1}, Lorg/json/JSONArray;->length()I

    move-result v2

    if-ge v1, v2, :cond_0

    .line 1261
    invoke-virtual {p1, v1}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v2

    .line 1262
    new-instance v3, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    const-string v4, "option_title"

    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v5, "option_data"

    .line 1263
    invoke-virtual {v2, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-direct {v3, v4, v2}, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    .line 1262
    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    return-object v0

    :catch_0
    return-object v0
.end method

.method private parseInputPlaceholderFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 2

    const-string v0, "input_placeholder"

    const-string v1, ""

    .line 1313
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getStringFromJson(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private parseInputRequiredFromMeta(Lorg/json/JSONObject;)Z
    .locals 2

    const-string v0, "input_required"

    const/4 v1, 0x0

    .line 1309
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getBooleanFromJson(Lorg/json/JSONObject;Ljava/lang/String;Z)Z

    move-result p1

    return p1
.end method

.method private parseInputSkipLabelFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 2

    const-string v0, "input_skip_label"

    const-string v1, ""

    .line 1301
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getStringFromJson(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private parseIsAnsweredFromMeta(Lorg/json/JSONObject;)Z
    .locals 2
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const-string v0, "is_answered"

    const/4 v1, 0x0

    .line 1375
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result p1

    return p1
.end method

.method private parseIsMessageEmptyFromMeta(Lorg/json/JSONObject;)Z
    .locals 2

    const-string v0, "is_message_empty"

    const/4 v1, 0x0

    .line 1219
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getBooleanFromJson(Lorg/json/JSONObject;Ljava/lang/String;Z)Z

    move-result p1

    return p1
.end method

.method private parseIsResponseSkippedFromMeta(Lorg/json/JSONObject;)Z
    .locals 2

    const-string v0, "is_response_skipped"

    const/4 v1, 0x0

    .line 1293
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getBooleanFromJson(Lorg/json/JSONObject;Ljava/lang/String;Z)Z

    move-result p1

    return p1
.end method

.method private parseIsSuggestionsReadEventSent(Lorg/json/JSONObject;)Z
    .locals 2

    const-string v0, "is_suggestion_read_event_sent"

    const/4 v1, 0x0

    .line 1236
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getBooleanFromJson(Lorg/json/JSONObject;Ljava/lang/String;Z)Z

    move-result p1

    return p1
.end method

.method private parseReferredMessageIdFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 2
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const-string v0, "referredMessageId"

    const/4 v1, 0x0

    .line 1371
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private parseReferredMessageTypeFromMeta(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/MessageType;
    .locals 2

    const-string v0, "referred_message_type"

    const-string v1, ""

    .line 1285
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getStringFromJson(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->fromValue(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-result-object p1

    return-object p1
.end method

.method private parseSelectedOptionDataFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 2

    const-string v0, "selected_option_data"

    const-string v1, "{}"

    .line 1289
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getStringFromJson(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private parseSuggestionReadFAQPublishId(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 2

    const-string v0, "suggestion_read_faq_publish_id"

    const-string v1, ""

    .line 1232
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getStringFromJson(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private parseTimeZoneIdFromMeta(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 1
    .param p1    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const-string v0, "timezone_id"

    .line 1328
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private declared-synchronized readConversation(Ljava/lang/String;[Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 10

    monitor-enter p0

    const/4 v0, 0x0

    .line 164
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v1}, Lcom/helpshift/platform/db/ConversationDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    .line 165
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "issues"

    const/4 v4, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    move-object v5, p1

    move-object v6, p2

    invoke-virtual/range {v2 .. v9}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 172
    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result p2

    if-eqz p2, :cond_0

    .line 173
    invoke-direct {p0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->cursorToReadableConversation(Landroid/database/Cursor;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p2
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    move-object v0, p2

    :cond_0
    if-eqz p1, :cond_1

    .line 181
    :goto_0
    :try_start_2
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    goto :goto_2

    :catch_0
    move-exception p2

    goto :goto_1

    :catchall_0
    move-exception p2

    goto :goto_3

    :catch_1
    move-exception p2

    move-object p1, v0

    :goto_1
    :try_start_3
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in read conversations with localId"

    .line 177
    invoke-static {v1, v2, p2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    if-eqz p1, :cond_1

    goto :goto_0

    .line 184
    :cond_1
    :goto_2
    monitor-exit p0

    return-object v0

    :catchall_1
    move-exception p2

    move-object v0, p1

    :goto_3
    if-eqz v0, :cond_2

    .line 181
    :try_start_4
    invoke-interface {v0}, Landroid/database/Cursor;->close()V

    goto :goto_4

    :catchall_2
    move-exception p1

    goto :goto_5

    .line 183
    :cond_2
    :goto_4
    throw p2
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    .line 160
    :goto_5
    monitor-exit p0

    throw p1
.end method

.method private readMessages(Ljava/lang/String;[Ljava/lang/String;)Ljava/util/List;
    .locals 11
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "[",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 644
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const/4 v1, 0x0

    .line 647
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v2}, Lcom/helpshift/platform/db/ConversationDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v3

    .line 648
    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "messages"

    const/4 v5, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    move-object v6, p1

    move-object v7, p2

    invoke-virtual/range {v3 .. v10}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 656
    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result p2

    if-eqz p2, :cond_2

    .line 658
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->cursorToMessageDM(Landroid/database/Cursor;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p2

    if-eqz p2, :cond_1

    .line 662
    invoke-interface {v0, p2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 664
    :cond_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToNext()Z

    move-result p2
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-nez p2, :cond_0

    :cond_2
    if-eqz p1, :cond_3

    .line 672
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    goto :goto_1

    :catchall_0
    move-exception p2

    move-object v1, p1

    goto :goto_2

    :catch_0
    move-exception p2

    move-object v1, p1

    goto :goto_0

    :catchall_1
    move-exception p2

    goto :goto_2

    :catch_1
    move-exception p2

    :goto_0
    :try_start_2
    const-string p1, "Helpshift_ConverDB"

    const-string v2, "Error in read messages"

    .line 668
    invoke-static {p1, v2, p2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz v1, :cond_3

    .line 672
    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    :cond_3
    :goto_1
    return-object v0

    :goto_2
    if-eqz v1, :cond_4

    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    .line 674
    :cond_4
    throw p2
.end method

.method private readableConversationToContentValues(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Landroid/content/ContentValues;
    .locals 4

    .line 804
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    .line 805
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "user_local_id"

    iget-wide v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    .line 806
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "server_id"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 807
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "pre_conv_server_id"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 808
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "publish_id"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->publishId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 809
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "uuid"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 810
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "title"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->title:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 811
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "message_cursor"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageCursor:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 812
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "show_agent_name"

    iget-boolean v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    .line 813
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "start_new_conversation_action"

    iget-boolean v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isStartNewConversationClicked:Z

    .line 814
    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    .line 813
    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    .line 815
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "created_at"

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getCreatedAt()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 816
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "updated_at"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->updatedAt:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 817
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "epoch_time_created_at"

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getEpochCreatedAtTime()J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    .line 818
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "last_user_activity_time"

    iget-wide v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->lastUserActivityTime:J

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    .line 819
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "issue_type"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 820
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "full_privacy_enabled"

    iget-boolean v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->wasFullPrivacyEnabledAtCreation:Z

    .line 821
    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    .line 820
    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    .line 822
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "state"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    if-nez v2, :cond_0

    const/4 v2, -0x1

    goto :goto_0

    :cond_0
    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {v2}, Lcom/helpshift/conversation/dto/IssueState;->getValue()I

    move-result v2

    :goto_0
    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    .line 823
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "is_redacted"

    iget-boolean v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isRedacted:Z

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    .line 825
    :try_start_0
    invoke-direct {p0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->getConversationMeta(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/lang/String;

    move-result-object p1

    .line 826
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "meta"

    invoke-virtual {v0, v1, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception p1

    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in generating meta string for conversation"

    .line 829
    invoke-static {v1, v2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_1
    return-object v0
.end method

.method private readableMessageToContentValues(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Landroid/content/ContentValues;
    .locals 4

    .line 908
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    .line 909
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "server_id"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 910
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "conversation_id"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    .line 911
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "body"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->body:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 912
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "author_name"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->authorName:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 913
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "created_at"

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getCreatedAt()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 914
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "epoch_time_created_at"

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    .line 915
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "type"

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->getValue()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    .line 916
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "md_state"

    iget v2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->deliveryState:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    .line 917
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "is_redacted"

    iget-boolean v2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isRedacted:Z

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    .line 919
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "meta"

    invoke-direct {p0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->getMessageMeta(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, v1, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in generating meta string for message"

    .line 922
    invoke-static {v1, v2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_0
    return-object v0
.end method


# virtual methods
.method public declared-synchronized deleteConversationInboxData(J)V
    .locals 4

    monitor-enter p0

    .line 1784
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "delete from "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "conversation_inbox"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " where "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "user_local_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ?"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 1787
    :try_start_1
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v1}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    const/4 v2, 0x1

    .line 1788
    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v2, v3

    invoke-virtual {v1, v0, v2}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :catch_0
    move-exception p1

    :try_start_2
    const-string p2, "Helpshift_ConverDB"

    const-string v0, "Error in delete conversationInboxData with UserLocalId"

    .line 1791
    invoke-static {p2, v0, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 1793
    :goto_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 1783
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized deleteConversationWithLocalId(J)V
    .locals 5

    monitor-enter p0

    .line 227
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ?"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    .line 228
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v3

    aput-object v3, v1, v2

    .line 229
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "conversation_id"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, " = ?"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 v3, 0x0

    .line 233
    :try_start_1
    iget-object v4, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v4}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v4
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_2
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 234
    :try_start_2
    invoke-virtual {v4}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 235
    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "issues"

    invoke-virtual {v4, v3, v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    .line 236
    iget-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "messages"

    invoke-virtual {v4, v0, v2, v1}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    .line 237
    invoke-virtual {v4}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_1
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    if-eqz v4, :cond_0

    .line 245
    :try_start_3
    invoke-virtual {v4}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_2

    :catch_0
    move-exception v0

    :try_start_4
    const-string v1, "Helpshift_ConverDB"

    .line 249
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Exception in ending transaction deleteConversationWithLocalId : "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1, p2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    :goto_0
    invoke-static {v1, p1, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    goto :goto_2

    :catchall_0
    move-exception v0

    goto :goto_3

    :catch_1
    move-exception v0

    move-object v3, v4

    goto :goto_1

    :catchall_1
    move-exception v0

    move-object v4, v3

    goto :goto_3

    :catch_2
    move-exception v0

    :goto_1
    :try_start_5
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in delete conversation with localId"

    .line 240
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_1

    if-eqz v3, :cond_0

    .line 245
    :try_start_6
    invoke-virtual {v3}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_6
    .catch Ljava/lang/Exception; {:try_start_6 .. :try_end_6} :catch_3
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    goto :goto_2

    :catch_3
    move-exception v0

    :try_start_7
    const-string v1, "Helpshift_ConverDB"

    .line 249
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Exception in ending transaction deleteConversationWithLocalId : "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1, p2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1
    :try_end_7
    .catchall {:try_start_7 .. :try_end_7} :catchall_2

    goto :goto_0

    .line 252
    :cond_0
    :goto_2
    monitor-exit p0

    return-void

    :goto_3
    if-eqz v4, :cond_1

    .line 245
    :try_start_8
    invoke-virtual {v4}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_8
    .catch Ljava/lang/Exception; {:try_start_8 .. :try_end_8} :catch_4
    .catchall {:try_start_8 .. :try_end_8} :catchall_2

    goto :goto_4

    :catch_4
    move-exception v1

    :try_start_9
    const-string v2, "Helpshift_ConverDB"

    .line 249
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Exception in ending transaction deleteConversationWithLocalId : "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, p1, p2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v2, p1, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 251
    :cond_1
    :goto_4
    throw v0
    :try_end_9
    .catchall {:try_start_9 .. :try_end_9} :catchall_2

    :catchall_2
    move-exception p1

    .line 226
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized deleteConversations(J)V
    .locals 7

    monitor-enter p0

    .line 1796
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "issues"

    .line 1797
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "messages"

    .line 1798
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "."

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "_id"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 1799
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "."

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "user_local_id"

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 1800
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "."

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "conversation_id"

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 1802
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "select "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " from  "

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "issues"

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "  where "

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " = ?"

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 1804
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "delete from "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "messages"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, " where "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " IN  ( "

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " )"

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 1806
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "delete from "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "issues"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " where "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "user_local_id"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " = ?"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 v2, 0x0

    .line 1810
    :try_start_1
    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v3}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v3
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 1811
    :try_start_2
    invoke-virtual {v3}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    const/4 v2, 0x1

    .line 1812
    new-array v4, v2, [Ljava/lang/String;

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v5

    const/4 v6, 0x0

    aput-object v5, v4, v6

    invoke-virtual {v3, v0, v4}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 1813
    new-array v0, v2, [Ljava/lang/String;

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v0, v6

    invoke-virtual {v3, v1, v0}, Landroid/database/sqlite/SQLiteDatabase;->execSQL(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 1814
    invoke-virtual {v3}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    if-eqz v3, :cond_0

    .line 1821
    :try_start_3
    invoke-virtual {v3}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_1

    :catchall_0
    move-exception p1

    goto :goto_2

    :catch_0
    move-exception p1

    move-object v2, v3

    goto :goto_0

    :catchall_1
    move-exception p1

    move-object v3, v2

    goto :goto_2

    :catch_1
    move-exception p1

    :goto_0
    :try_start_4
    const-string p2, "Helpshift_ConverDB"

    const-string v0, "Error in delete conversations with UserLocalId"

    .line 1817
    invoke-static {p2, v0, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v2, :cond_0

    .line 1821
    :try_start_5
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    .line 1824
    :cond_0
    :goto_1
    monitor-exit p0

    return-void

    :goto_2
    if-eqz v3, :cond_1

    .line 1821
    :try_start_6
    invoke-virtual {v3}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V

    .line 1823
    :cond_1
    throw p1
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    :catchall_2
    move-exception p1

    .line 1795
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized deleteMessagesForConversation(J)Z
    .locals 6

    monitor-enter p0

    .line 725
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "conversation_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "= ? "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    .line 726
    new-array v2, v1, [Ljava/lang/String;

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 729
    :try_start_1
    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v3}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v3

    .line 730
    iget-object v5, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v5}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v5, "messages"

    invoke-virtual {v3, v5, v0, v2}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 731
    monitor-exit p0

    return v1

    :catch_0
    move-exception v0

    :try_start_2
    const-string v1, "Helpshift_ConverDB"

    .line 734
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Error deleting messages for : "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1, p2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v1, p1, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 735
    monitor-exit p0

    return v4

    :catchall_0
    move-exception p1

    .line 724
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized dropAndCreateDatabase()V
    .locals 2

    monitor-enter p0

    .line 1698
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v1}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/platform/db/ConversationDBHelper;->dropAndCreateDatabase(Landroid/database/sqlite/SQLiteDatabase;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 1699
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 1697
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getAdminFAQSuggestion(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/support/Faq;
    .locals 10

    monitor-enter p0

    .line 1702
    :try_start_0
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_4

    invoke-static {p2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    if-eqz v0, :cond_0

    goto :goto_4

    .line 1709
    :cond_0
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v0}, Lcom/helpshift/platform/db/ConversationDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    const-string v3, "faq_suggestions"

    const/4 v4, 0x0

    const-string v5, "publish_id = ? AND language = ?"

    const/4 v0, 0x2

    .line 1710
    new-array v6, v0, [Ljava/lang/String;

    const/4 v0, 0x0

    aput-object p1, v6, v0

    const/4 p1, 0x1

    aput-object p2, v6, p1

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    invoke-virtual/range {v2 .. v9}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 1716
    :try_start_2
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result p2

    if-eqz p2, :cond_1

    .line 1717
    invoke-direct {p0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->cursorToFaq(Landroid/database/Cursor;)Lcom/helpshift/support/Faq;

    move-result-object p2
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    move-object v1, p2

    :cond_1
    if-eqz p1, :cond_2

    .line 1725
    :goto_0
    :try_start_3
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_2

    :catch_0
    move-exception p2

    goto :goto_1

    :catchall_0
    move-exception p2

    goto :goto_3

    :catch_1
    move-exception p2

    move-object p1, v1

    :goto_1
    :try_start_4
    const-string v0, "Helpshift_ConverDB"

    const-string v2, "Error in getAdminFAQSuggestion"

    .line 1721
    invoke-static {v0, v2, p2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz p1, :cond_2

    goto :goto_0

    .line 1728
    :cond_2
    :goto_2
    monitor-exit p0

    return-object v1

    :catchall_1
    move-exception p2

    move-object v1, p1

    :goto_3
    if-eqz v1, :cond_3

    .line 1725
    :try_start_5
    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    .line 1727
    :cond_3
    throw p2
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    .line 1703
    :cond_4
    :goto_4
    monitor-exit p0

    return-object v1

    :catchall_2
    move-exception p1

    .line 1701
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized getMessagesCountForConversations(Ljava/util/List;[Ljava/lang/String;)Ljava/util/Map;
    .locals 13
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/Long;",
            ">;[",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/Long;",
            "Ljava/lang/Integer;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 551
    :try_start_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 552
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    const/4 v3, 0x0

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/Long;

    .line 554
    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-interface {v0, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_7

    goto :goto_0

    :cond_0
    const/16 v1, 0x384

    const/4 v2, 0x0

    .line 560
    :try_start_1
    new-instance v4, Ljava/util/ArrayList;

    invoke-direct {v4, p1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    invoke-static {v1, v4}, Lcom/helpshift/util/DatabaseUtils;->createBatches(ILjava/util/List;)Ljava/util/List;

    move-result-object p1

    .line 562
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v1}, Lcom/helpshift/platform/db/ConversationDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_3
    .catchall {:try_start_1 .. :try_end_1} :catchall_3

    .line 563
    :try_start_2
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 564
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_5

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/util/List;

    .line 565
    invoke-interface {v4}, Ljava/util/List;->size()I

    move-result v5

    invoke-static {v5}, Lcom/helpshift/util/DatabaseUtils;->makePlaceholders(I)Ljava/lang/String;

    move-result-object v5

    .line 566
    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    .line 568
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v8, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v8}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v8, "conversation_id"

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v8, " IN ("

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, ")"

    invoke-virtual {v7, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    .line 569
    invoke-virtual {v6, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 571
    new-instance v5, Ljava/util/ArrayList;

    invoke-direct {v5}, Ljava/util/ArrayList;-><init>()V

    .line 573
    invoke-interface {v4}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :goto_2
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_1

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/lang/Long;

    .line 574
    invoke-static {v7}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v7

    invoke-interface {v5, v7}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    :cond_1
    if-eqz p2, :cond_2

    .line 578
    array-length v4, p2

    invoke-static {v4}, Lcom/helpshift/util/DatabaseUtils;->makePlaceholders(I)Ljava/lang/String;

    move-result-object v4

    .line 579
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v8, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v8}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v8, "type"

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v8, " IN ("

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, ")"

    invoke-virtual {v7, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    const-string v7, " AND "

    .line 581
    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 582
    invoke-virtual {v6, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 584
    invoke-static {p2}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v4

    invoke-interface {v5, v4}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    .line 588
    :cond_2
    invoke-interface {v5}, Ljava/util/List;->size()I

    move-result v4

    new-array v8, v4, [Ljava/lang/String;

    .line 589
    invoke-interface {v5, v8}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    .line 591
    iget-object v4, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v5, "messages"

    const/4 v4, 0x2

    new-array v7, v4, [Ljava/lang/String;

    const-string v4, "COUNT(*) AS COUNT"

    aput-object v4, v7, v3

    const/4 v4, 0x1

    iget-object v9, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v9}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v9, "conversation_id"

    aput-object v9, v7, v4

    .line 594
    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v9

    iget-object v4, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v10, "conversation_id"

    const/4 v11, 0x0

    const/4 v12, 0x0

    move-object v4, v1

    move-object v6, v7

    move-object v7, v9

    move-object v9, v10

    move-object v10, v11

    move-object v11, v12

    .line 591
    invoke-virtual/range {v4 .. v11}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v4
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    .line 600
    :try_start_3
    invoke-interface {v4}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v2

    if-eqz v2, :cond_4

    .line 602
    :cond_3
    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "conversation_id"

    invoke-interface {v4, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v4, v2}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v5

    const-string v2, "COUNT"

    .line 603
    invoke-interface {v4, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v4, v2}, Landroid/database/Cursor;->getInt(I)I

    move-result v2

    .line 604
    invoke-static {v5, v6}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v5

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v0, v5, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 605
    invoke-interface {v4}, Landroid/database/Cursor;->moveToNext()Z

    move-result v2
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    if-nez v2, :cond_3

    :cond_4
    move-object v2, v4

    goto/16 :goto_1

    :catchall_0
    move-exception p1

    goto/16 :goto_c

    :catch_0
    move-exception p1

    goto :goto_6

    .line 608
    :cond_5
    :try_start_4
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_2
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    if-eqz v1, :cond_7

    .line 615
    :try_start_5
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result p1

    if-eqz p1, :cond_7

    .line 616
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_1
    .catchall {:try_start_5 .. :try_end_5} :catchall_1

    goto :goto_5

    :catchall_1
    move-exception p1

    goto :goto_4

    :catch_1
    move-exception p1

    :try_start_6
    const-string p2, "Helpshift_ConverDB"

    const-string v1, "Error in get messages count inside finally block, "

    .line 620
    invoke-static {p2, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_1

    if-eqz v2, :cond_a

    .line 624
    :goto_3
    :try_start_7
    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    goto :goto_b

    :goto_4
    if-eqz v2, :cond_6

    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    .line 626
    :cond_6
    throw p1
    :try_end_7
    .catchall {:try_start_7 .. :try_end_7} :catchall_7

    :cond_7
    :goto_5
    if-eqz v2, :cond_a

    goto :goto_3

    :catchall_2
    move-exception p1

    goto :goto_d

    :catch_2
    move-exception p1

    move-object v4, v2

    :goto_6
    move-object v2, v1

    goto :goto_7

    :catchall_3
    move-exception p1

    move-object v1, v2

    goto :goto_d

    :catch_3
    move-exception p1

    move-object v4, v2

    :goto_7
    :try_start_8
    const-string p2, "Helpshift_ConverDB"

    const-string v1, "Error in get messages count"

    .line 611
    invoke-static {p2, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_5

    if-eqz v2, :cond_9

    .line 615
    :try_start_9
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result p1

    if-eqz p1, :cond_9

    .line 616
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_9
    .catch Ljava/lang/Exception; {:try_start_9 .. :try_end_9} :catch_4
    .catchall {:try_start_9 .. :try_end_9} :catchall_4

    goto :goto_a

    :catchall_4
    move-exception p1

    goto :goto_9

    :catch_4
    move-exception p1

    :try_start_a
    const-string p2, "Helpshift_ConverDB"

    const-string v1, "Error in get messages count inside finally block, "

    .line 620
    invoke-static {p2, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_a
    .catchall {:try_start_a .. :try_end_a} :catchall_4

    if-eqz v4, :cond_a

    .line 624
    :goto_8
    :try_start_b
    invoke-interface {v4}, Landroid/database/Cursor;->close()V

    goto :goto_b

    :goto_9
    if-eqz v4, :cond_8

    invoke-interface {v4}, Landroid/database/Cursor;->close()V

    .line 626
    :cond_8
    throw p1
    :try_end_b
    .catchall {:try_start_b .. :try_end_b} :catchall_7

    :cond_9
    :goto_a
    if-eqz v4, :cond_a

    goto :goto_8

    .line 628
    :cond_a
    :goto_b
    monitor-exit p0

    return-object v0

    :catchall_5
    move-exception p1

    move-object v1, v2

    :goto_c
    move-object v2, v4

    :goto_d
    if-eqz v1, :cond_c

    .line 615
    :try_start_c
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result p2

    if-eqz p2, :cond_c

    .line 616
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_c
    .catch Ljava/lang/Exception; {:try_start_c .. :try_end_c} :catch_5
    .catchall {:try_start_c .. :try_end_c} :catchall_6

    goto :goto_10

    :catchall_6
    move-exception p1

    goto :goto_f

    :catch_5
    move-exception p2

    :try_start_d
    const-string v0, "Helpshift_ConverDB"

    const-string v1, "Error in get messages count inside finally block, "

    .line 620
    invoke-static {v0, v1, p2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_d
    .catchall {:try_start_d .. :try_end_d} :catchall_6

    if-eqz v2, :cond_d

    .line 624
    :goto_e
    :try_start_e
    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    goto :goto_11

    :goto_f
    if-eqz v2, :cond_b

    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    .line 626
    :cond_b
    throw p1

    :cond_c
    :goto_10
    if-eqz v2, :cond_d

    goto :goto_e

    .line 627
    :cond_d
    :goto_11
    throw p1
    :try_end_e
    .catchall {:try_start_e .. :try_end_e} :catchall_7

    :catchall_7
    move-exception p1

    .line 550
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized getOldestConversationEpochCreatedAtTime(J)Ljava/lang/Long;
    .locals 12

    monitor-enter p0

    .line 1867
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "user_local_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ?"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    const/4 v0, 0x1

    .line 1868
    new-array v6, v0, [Ljava/lang/String;

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    const/4 p2, 0x0

    aput-object p1, v6, p2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 p1, 0x0

    .line 1870
    :try_start_1
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v1}, Lcom/helpshift/platform/db/ConversationDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    .line 1871
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "issues"

    new-array v4, v0, [Ljava/lang/String;

    iget-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "epoch_time_created_at"

    aput-object v0, v4, p2

    const/4 v7, 0x0

    const/4 v8, 0x0

    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "epoch_time_created_at"

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " ASC"

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v9

    const-string v10, "1"

    invoke-virtual/range {v2 .. v10}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p2
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 1879
    :try_start_2
    invoke-interface {p2}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 1880
    iget-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "epoch_time_created_at"

    const-class v1, Ljava/lang/Long;

    invoke-static {p2, v0, v1}, Lcom/helpshift/util/DatabaseUtils;->parseColumnSafe(Landroid/database/Cursor;Ljava/lang/String;Ljava/lang/Class;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Long;
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    move-object p1, v0

    :cond_0
    if-eqz p2, :cond_1

    .line 1888
    :goto_0
    :try_start_3
    invoke-interface {p2}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_2

    :catch_0
    move-exception v0

    goto :goto_1

    :catchall_0
    move-exception p2

    goto :goto_3

    :catch_1
    move-exception v0

    move-object p2, p1

    :goto_1
    :try_start_4
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in getting latest conversation created_at time"

    .line 1884
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz p2, :cond_1

    goto :goto_0

    .line 1891
    :cond_1
    :goto_2
    monitor-exit p0

    return-object p1

    :catchall_1
    move-exception p1

    move-object v11, p2

    move-object p2, p1

    move-object p1, v11

    :goto_3
    if-eqz p1, :cond_2

    .line 1888
    :try_start_5
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    .line 1890
    :cond_2
    throw p2
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    :catchall_2
    move-exception p1

    .line 1864
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized getOldestMessageCursor(J)Ljava/lang/String;
    .locals 9

    monitor-enter p0

    :try_start_0
    const-string v0, "message_create_at"

    .line 1828
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "issues"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "."

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "user_local_id"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 1829
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "issues"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "."

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "_id"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 1830
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v4, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "messages"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, "."

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v4, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "conversation_id"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    .line 1831
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v5, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v5}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v5, "messages"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "."

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v5, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v5}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v5, "created_at"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    .line 1832
    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v6, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v6, "messages"

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v6, "."

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v6, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v6, "epoch_time_created_at"

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    .line 1833
    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "SELECT "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, " AS "

    invoke-virtual {v6, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, " FROM "

    invoke-virtual {v6, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v4, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "issues"

    invoke-virtual {v6, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, " INNER JOIN "

    invoke-virtual {v6, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v4, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "messages"

    invoke-virtual {v6, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, " ON "

    invoke-virtual {v6, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " = "

    invoke-virtual {v6, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " WHERE "

    invoke-virtual {v6, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ? ORDER BY "

    invoke-virtual {v6, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "  ASC LIMIT 1"

    invoke-virtual {v6, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x1

    .line 1843
    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v2, v3
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 p1, 0x0

    .line 1845
    :try_start_1
    iget-object p2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {p2}, Lcom/helpshift/platform/db/ConversationDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object p2

    .line 1846
    invoke-virtual {p2, v1, v2}, Landroid/database/sqlite/SQLiteDatabase;->rawQuery(Ljava/lang/String;[Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p2
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 1848
    :try_start_2
    invoke-interface {p2}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 1849
    invoke-interface {p2, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p2, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v0
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    move-object p1, v0

    :cond_0
    if-eqz p2, :cond_1

    .line 1857
    :goto_0
    :try_start_3
    invoke-interface {p2}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_2

    :catch_0
    move-exception v0

    goto :goto_1

    :catchall_0
    move-exception p2

    move-object v8, p2

    move-object p2, p1

    move-object p1, v8

    goto :goto_3

    :catch_1
    move-exception v0

    move-object p2, p1

    :goto_1
    :try_start_4
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in read messages"

    .line 1853
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz p2, :cond_1

    goto :goto_0

    .line 1861
    :cond_1
    :goto_2
    monitor-exit p0

    return-object p1

    :catchall_1
    move-exception p1

    :goto_3
    if-eqz p2, :cond_2

    .line 1857
    :try_start_5
    invoke-interface {p2}, Landroid/database/Cursor;->close()V

    .line 1859
    :cond_2
    throw p1
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    :catchall_2
    move-exception p1

    .line 1826
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized insertConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)J
    .locals 5

    monitor-enter p0

    .line 267
    :try_start_0
    invoke-direct {p0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->readableConversationToContentValues(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Landroid/content/ContentValues;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const-wide/16 v0, -0x1

    .line 270
    :try_start_1
    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v2}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    .line 271
    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "issues"

    const/4 v4, 0x0

    invoke-virtual {v2, v3, v4, p1}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    move-result-wide v2
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    move-wide v0, v2

    goto :goto_0

    :catch_0
    move-exception p1

    :try_start_2
    const-string v2, "Helpshift_ConverDB"

    const-string v3, "Error in insert conversation"

    .line 274
    invoke-static {v2, v3, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 276
    :goto_0
    monitor-exit p0

    return-wide v0

    :catchall_0
    move-exception p1

    .line 266
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized insertConversations(Ljava/util/List;)Ljava/util/List;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)",
            "Ljava/util/List<",
            "Ljava/lang/Long;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 280
    :try_start_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 281
    monitor-exit p0

    return-object v1

    .line 284
    :cond_0
    :try_start_1
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 285
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 286
    invoke-direct {p0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->readableConversationToContentValues(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Landroid/content/ContentValues;

    move-result-object v2

    .line 287
    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 290
    :cond_1
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_2

    .line 292
    :try_start_2
    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v2}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    .line 293
    :try_start_3
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 294
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/content/ContentValues;

    .line 295
    iget-object v4, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "issues"

    invoke-virtual {v2, v4, v1, v3}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    move-result-wide v3

    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    .line 296
    invoke-interface {p1, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 298
    :cond_2
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_1
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    if-eqz v2, :cond_3

    .line 306
    :try_start_4
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_0
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    goto :goto_4

    :catch_0
    move-exception v0

    :try_start_5
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in insert conversations inside finally block"

    .line 309
    :goto_2
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    goto :goto_4

    :catchall_0
    move-exception p1

    goto :goto_5

    :catch_1
    move-exception v0

    move-object v1, v2

    goto :goto_3

    :catchall_1
    move-exception p1

    move-object v2, v1

    goto :goto_5

    :catch_2
    move-exception v0

    :goto_3
    :try_start_6
    const-string v2, "Helpshift_ConverDB"

    const-string v3, "Error in insert conversations"

    .line 301
    invoke-static {v2, v3, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_1

    if-eqz v1, :cond_3

    .line 306
    :try_start_7
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_7
    .catch Ljava/lang/Exception; {:try_start_7 .. :try_end_7} :catch_3
    .catchall {:try_start_7 .. :try_end_7} :catchall_2

    goto :goto_4

    :catch_3
    move-exception v0

    :try_start_8
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in insert conversations inside finally block"
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_2

    goto :goto_2

    .line 313
    :cond_3
    :goto_4
    monitor-exit p0

    return-object p1

    :goto_5
    if-eqz v2, :cond_4

    .line 306
    :try_start_9
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_9
    .catch Ljava/lang/Exception; {:try_start_9 .. :try_end_9} :catch_4
    .catchall {:try_start_9 .. :try_end_9} :catchall_2

    goto :goto_6

    :catch_4
    move-exception v0

    :try_start_a
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in insert conversations inside finally block"

    .line 309
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 312
    :cond_4
    :goto_6
    throw p1
    :try_end_a
    .catchall {:try_start_a .. :try_end_a} :catchall_2

    :catchall_2
    move-exception p1

    .line 279
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized insertMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)J
    .locals 5

    monitor-enter p0

    .line 436
    :try_start_0
    invoke-direct {p0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->readableMessageToContentValues(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Landroid/content/ContentValues;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const-wide/16 v0, -0x1

    .line 439
    :try_start_1
    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v2}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    .line 440
    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "messages"

    const/4 v4, 0x0

    invoke-virtual {v2, v3, v4, p1}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    move-result-wide v2
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    move-wide v0, v2

    goto :goto_0

    :catch_0
    move-exception p1

    :try_start_2
    const-string v2, "Helpshift_ConverDB"

    const-string v3, "Error in insert message"

    .line 443
    invoke-static {v2, v3, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 445
    :goto_0
    monitor-exit p0

    return-wide v0

    :catchall_0
    move-exception p1

    .line 435
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized insertMessages(Ljava/util/List;)Ljava/util/List;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)",
            "Ljava/util/List<",
            "Ljava/lang/Long;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 449
    :try_start_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 450
    monitor-exit p0

    return-object v1

    .line 453
    :cond_0
    :try_start_1
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 454
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 455
    invoke-direct {p0, v2}, Lcom/helpshift/common/conversation/ConversationDB;->readableMessageToContentValues(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Landroid/content/ContentValues;

    move-result-object v2

    .line 456
    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 460
    :cond_1
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_2

    .line 462
    :try_start_2
    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v2}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    .line 463
    :try_start_3
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 464
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/content/ContentValues;

    .line 465
    iget-object v4, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "messages"

    invoke-virtual {v2, v4, v1, v3}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    move-result-wide v3

    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    .line 466
    invoke-interface {p1, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 468
    :cond_2
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_1
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    if-eqz v2, :cond_3

    .line 476
    :try_start_4
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_0
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    goto :goto_4

    :catch_0
    move-exception v0

    :try_start_5
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in insert messages inside finally block"

    .line 479
    :goto_2
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    goto :goto_4

    :catchall_0
    move-exception p1

    goto :goto_5

    :catch_1
    move-exception v0

    move-object v1, v2

    goto :goto_3

    :catchall_1
    move-exception p1

    move-object v2, v1

    goto :goto_5

    :catch_2
    move-exception v0

    :goto_3
    :try_start_6
    const-string v2, "Helpshift_ConverDB"

    const-string v3, "Error in insert messages"

    .line 471
    invoke-static {v2, v3, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_1

    if-eqz v1, :cond_3

    .line 476
    :try_start_7
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_7
    .catch Ljava/lang/Exception; {:try_start_7 .. :try_end_7} :catch_3
    .catchall {:try_start_7 .. :try_end_7} :catchall_2

    goto :goto_4

    :catch_3
    move-exception v0

    :try_start_8
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in insert messages inside finally block"
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_2

    goto :goto_2

    .line 483
    :cond_3
    :goto_4
    monitor-exit p0

    return-object p1

    :goto_5
    if-eqz v2, :cond_4

    .line 476
    :try_start_9
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_9
    .catch Ljava/lang/Exception; {:try_start_9 .. :try_end_9} :catch_4
    .catchall {:try_start_9 .. :try_end_9} :catchall_2

    goto :goto_6

    :catch_4
    move-exception v0

    :try_start_a
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in insert messages inside finally block"

    .line 479
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 482
    :cond_4
    :goto_6
    throw p1
    :try_end_a
    .catchall {:try_start_a .. :try_end_a} :catchall_2

    :catchall_2
    move-exception p1

    .line 448
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized insertOrUpdateAdminFAQSuggestion(Lcom/helpshift/support/Faq;)V
    .locals 5

    monitor-enter p0

    .line 1732
    :try_start_0
    invoke-static {p1}, Lcom/helpshift/common/conversation/ConversationDB;->faqToContentValues(Lcom/helpshift/support/Faq;)Landroid/content/ContentValues;

    move-result-object v0

    const-string v1, "publish_id = ? AND language = ?"

    const/4 v2, 0x2

    .line 1736
    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    iget-object v4, p1, Lcom/helpshift/support/Faq;->publish_id:Ljava/lang/String;

    aput-object v4, v2, v3

    const/4 v3, 0x1

    iget-object p1, p1, Lcom/helpshift/support/Faq;->language:Ljava/lang/String;

    aput-object p1, v2, v3
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 1738
    :try_start_1
    iget-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {p1}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object p1

    const-string v3, "faq_suggestions"

    .line 1739
    invoke-direct {p0, p1, v3, v1, v2}, Lcom/helpshift/common/conversation/ConversationDB;->exists(Landroid/database/sqlite/SQLiteDatabase;Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_0

    const-string v1, "faq_suggestions"

    const/4 v2, 0x0

    .line 1741
    invoke-virtual {p1, v1, v2, v0}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    goto :goto_0

    :cond_0
    const-string v3, "faq_suggestions"

    .line 1744
    invoke-virtual {p1, v3, v0, v1, v2}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :catch_0
    move-exception p1

    :try_start_2
    const-string v0, "Helpshift_ConverDB"

    const-string v1, "Error in insertOrUpdateAdminFAQSuggestion"

    .line 1748
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 1750
    :goto_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 1731
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized readConversationInboxRecord(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
    .locals 11

    monitor-enter p0

    .line 409
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "user_local_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ?"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    const/4 v0, 0x1

    .line 410
    new-array v6, v0, [Ljava/lang/String;

    const/4 v0, 0x0

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v6, v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 p1, 0x0

    .line 412
    :try_start_1
    iget-object p2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {p2}, Lcom/helpshift/platform/db/ConversationDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    .line 413
    iget-object p2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {p2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "conversation_inbox"

    const/4 v4, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    invoke-virtual/range {v2 .. v9}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p2
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 420
    :try_start_2
    invoke-interface {p2}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 421
    invoke-direct {p0, p2}, Lcom/helpshift/common/conversation/ConversationDB;->cursorToConversationInboxRecord(Landroid/database/Cursor;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object v0
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    move-object p1, v0

    :cond_0
    if-eqz p2, :cond_1

    .line 429
    :goto_0
    :try_start_3
    invoke-interface {p2}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_2

    :catch_0
    move-exception v0

    goto :goto_1

    :catchall_0
    move-exception p2

    goto :goto_3

    :catch_1
    move-exception v0

    move-object p2, p1

    :goto_1
    :try_start_4
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in read conversation inbox record"

    .line 425
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz p2, :cond_1

    goto :goto_0

    .line 432
    :cond_1
    :goto_2
    monitor-exit p0

    return-object p1

    :catchall_1
    move-exception p1

    move-object v10, p2

    move-object p2, p1

    move-object p1, v10

    :goto_3
    if-eqz p1, :cond_2

    .line 429
    :try_start_5
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    .line 431
    :cond_2
    throw p2
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    :catchall_2
    move-exception p1

    .line 406
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized readConversationWithLocalId(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 3

    monitor-enter p0

    .line 221
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ?"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    .line 222
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v1, v2

    .line 223
    invoke-direct {p0, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->readConversation(Ljava/lang/String;[Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 220
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized readConversationWithServerId(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 3

    monitor-enter p0

    .line 255
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "server_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ?"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    .line 256
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v1, v2

    .line 257
    invoke-direct {p0, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->readConversation(Ljava/lang/String;[Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 254
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized readConversationsWithLocalId(J)Ljava/util/List;
    .locals 12
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(J)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 188
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const/4 v1, 0x0

    .line 190
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v3, "user_local_id"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, " = ?"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    const/4 v2, 0x1

    .line 191
    new-array v8, v2, [Ljava/lang/String;

    const/4 v2, 0x0

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v8, v2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    .line 193
    :try_start_1
    iget-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {p1}, Lcom/helpshift/platform/db/ConversationDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v4

    .line 194
    iget-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v5, "issues"

    const/4 v6, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    const/4 v11, 0x0

    invoke-virtual/range {v4 .. v11}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 202
    :try_start_2
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result p2

    if-eqz p2, :cond_1

    .line 204
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->cursorToReadableConversation(Landroid/database/Cursor;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p2

    .line 205
    invoke-interface {v0, p2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 206
    invoke-interface {p1}, Landroid/database/Cursor;->moveToNext()Z

    move-result p2
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    if-nez p2, :cond_0

    :cond_1
    if-eqz p1, :cond_2

    .line 214
    :try_start_3
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_1

    :catchall_0
    move-exception p2

    move-object v1, p1

    goto :goto_2

    :catch_0
    move-exception p2

    move-object v1, p1

    goto :goto_0

    :catchall_1
    move-exception p2

    goto :goto_2

    :catch_1
    move-exception p2

    :goto_0
    :try_start_4
    const-string p1, "Helpshift_ConverDB"

    const-string v2, "Error in read conversations with localId"

    .line 210
    invoke-static {p1, v2, p2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v1, :cond_2

    .line 214
    :try_start_5
    invoke-interface {v1}, Landroid/database/Cursor;->close()V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    .line 217
    :cond_2
    :goto_1
    monitor-exit p0

    return-object v0

    :goto_2
    if-eqz v1, :cond_3

    .line 214
    :try_start_6
    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    .line 216
    :cond_3
    throw p2
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    :catchall_2
    move-exception p1

    .line 187
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized readMessageWithLocalId(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 3

    monitor-enter p0

    .line 1691
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ?"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    .line 1692
    new-array v1, v1, [Ljava/lang/String;

    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    const/4 v2, 0x0

    aput-object p1, v1, v2

    .line 1693
    invoke-direct {p0, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->readMessages(Ljava/lang/String;[Ljava/lang/String;)Ljava/util/List;

    move-result-object p1

    .line 1694
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 p1, 0x0

    goto :goto_0

    :cond_0
    invoke-interface {p1, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :goto_0
    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 1690
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized readMessageWithServerId(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 3

    monitor-enter p0

    .line 1684
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "server_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ?"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    .line 1685
    new-array v1, v1, [Ljava/lang/String;

    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    const/4 v2, 0x0

    aput-object p1, v1, v2

    .line 1686
    invoke-direct {p0, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->readMessages(Ljava/lang/String;[Ljava/lang/String;)Ljava/util/List;

    move-result-object p1

    .line 1687
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 p1, 0x0

    goto :goto_0

    :cond_0
    invoke-interface {p1, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :goto_0
    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 1683
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized readMessages(J)Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(J)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 632
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "conversation_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ?"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    .line 633
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v1, v2

    .line 634
    invoke-direct {p0, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->readMessages(Ljava/lang/String;[Ljava/lang/String;)Ljava/util/List;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 631
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized readMessages(JLcom/helpshift/conversation/activeconversation/message/MessageType;)Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(J",
            "Lcom/helpshift/conversation/activeconversation/message/MessageType;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 638
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "conversation_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ? AND "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "type"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ?"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x2

    .line 639
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v1, v2

    const/4 p1, 0x1

    invoke-virtual {p3}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->getValue()Ljava/lang/String;

    move-result-object p2

    aput-object p2, v1, p1

    .line 640
    invoke-direct {p0, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->readMessages(Ljava/lang/String;[Ljava/lang/String;)Ljava/util/List;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 637
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized readMessagesForConversations(Ljava/util/Collection;)Ljava/util/List;
    .locals 11
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "Ljava/lang/Long;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 487
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_7

    const/16 v1, 0x384

    const/4 v2, 0x0

    .line 492
    :try_start_1
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3, p1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    invoke-static {v1, v3}, Lcom/helpshift/util/DatabaseUtils;->createBatches(ILjava/util/List;)Ljava/util/List;

    move-result-object p1

    .line 494
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v1}, Lcom/helpshift/platform/db/ConversationDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_3
    .catchall {:try_start_1 .. :try_end_1} :catchall_3

    .line 495
    :try_start_2
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 497
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_4

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/util/List;

    .line 498
    invoke-interface {v3}, Ljava/util/List;->size()I

    move-result v4

    invoke-static {v4}, Lcom/helpshift/util/DatabaseUtils;->makePlaceholders(I)Ljava/lang/String;

    move-result-object v4

    .line 499
    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v6, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v6, "conversation_id"

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v6, " IN ("

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, ")"

    invoke-virtual {v5, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    .line 500
    invoke-interface {v3}, Ljava/util/List;->size()I

    move-result v4

    new-array v7, v4, [Ljava/lang/String;

    const/4 v4, 0x0

    .line 501
    :goto_1
    invoke-interface {v3}, Ljava/util/List;->size()I

    move-result v5

    if-ge v4, v5, :cond_0

    .line 502
    invoke-interface {v3, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    invoke-static {v5}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v5

    aput-object v5, v7, v4

    add-int/lit8 v4, v4, 0x1

    goto :goto_1

    .line 505
    :cond_0
    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "messages"

    const/4 v5, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    move-object v3, v1

    invoke-virtual/range {v3 .. v10}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v3
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    .line 513
    :try_start_3
    invoke-interface {v3}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v2

    if-eqz v2, :cond_3

    .line 515
    :cond_1
    invoke-direct {p0, v3}, Lcom/helpshift/common/conversation/ConversationDB;->cursorToMessageDM(Landroid/database/Cursor;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v2

    if-eqz v2, :cond_2

    .line 521
    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 523
    :cond_2
    invoke-interface {v3}, Landroid/database/Cursor;->moveToNext()Z

    move-result v2
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    if-nez v2, :cond_1

    :cond_3
    move-object v2, v3

    goto :goto_0

    :catchall_0
    move-exception p1

    goto/16 :goto_b

    :catch_0
    move-exception p1

    goto :goto_5

    .line 526
    :cond_4
    :try_start_4
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_2
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    if-eqz v1, :cond_6

    .line 533
    :try_start_5
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result p1

    if-eqz p1, :cond_6

    .line 534
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_1
    .catchall {:try_start_5 .. :try_end_5} :catchall_1

    goto :goto_4

    :catchall_1
    move-exception p1

    goto :goto_3

    :catch_1
    move-exception p1

    :try_start_6
    const-string v1, "Helpshift_ConverDB"

    const-string v3, "Error in read messages inside finally block, "

    .line 538
    invoke-static {v1, v3, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_1

    if-eqz v2, :cond_9

    .line 542
    :goto_2
    :try_start_7
    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    goto :goto_a

    :goto_3
    if-eqz v2, :cond_5

    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    .line 544
    :cond_5
    throw p1
    :try_end_7
    .catchall {:try_start_7 .. :try_end_7} :catchall_7

    :cond_6
    :goto_4
    if-eqz v2, :cond_9

    goto :goto_2

    :catchall_2
    move-exception p1

    goto :goto_c

    :catch_2
    move-exception p1

    move-object v3, v2

    :goto_5
    move-object v2, v1

    goto :goto_6

    :catchall_3
    move-exception p1

    move-object v1, v2

    goto :goto_c

    :catch_3
    move-exception p1

    move-object v3, v2

    :goto_6
    :try_start_8
    const-string v1, "Helpshift_ConverDB"

    const-string v4, "Error in read messages"

    .line 529
    invoke-static {v1, v4, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_5

    if-eqz v2, :cond_8

    .line 533
    :try_start_9
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result p1

    if-eqz p1, :cond_8

    .line 534
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_9
    .catch Ljava/lang/Exception; {:try_start_9 .. :try_end_9} :catch_4
    .catchall {:try_start_9 .. :try_end_9} :catchall_4

    goto :goto_9

    :catchall_4
    move-exception p1

    goto :goto_8

    :catch_4
    move-exception p1

    :try_start_a
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in read messages inside finally block, "

    .line 538
    invoke-static {v1, v2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_a
    .catchall {:try_start_a .. :try_end_a} :catchall_4

    if-eqz v3, :cond_9

    .line 542
    :goto_7
    :try_start_b
    invoke-interface {v3}, Landroid/database/Cursor;->close()V

    goto :goto_a

    :goto_8
    if-eqz v3, :cond_7

    invoke-interface {v3}, Landroid/database/Cursor;->close()V

    .line 544
    :cond_7
    throw p1
    :try_end_b
    .catchall {:try_start_b .. :try_end_b} :catchall_7

    :cond_8
    :goto_9
    if-eqz v3, :cond_9

    goto :goto_7

    .line 546
    :cond_9
    :goto_a
    monitor-exit p0

    return-object v0

    :catchall_5
    move-exception p1

    move-object v1, v2

    :goto_b
    move-object v2, v3

    :goto_c
    if-eqz v1, :cond_b

    .line 533
    :try_start_c
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v0

    if-eqz v0, :cond_b

    .line 534
    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_c
    .catch Ljava/lang/Exception; {:try_start_c .. :try_end_c} :catch_5
    .catchall {:try_start_c .. :try_end_c} :catchall_6

    goto :goto_f

    :catchall_6
    move-exception p1

    goto :goto_e

    :catch_5
    move-exception v0

    :try_start_d
    const-string v1, "Helpshift_ConverDB"

    const-string v3, "Error in read messages inside finally block, "

    .line 538
    invoke-static {v1, v3, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_d
    .catchall {:try_start_d .. :try_end_d} :catchall_6

    if-eqz v2, :cond_c

    .line 542
    :goto_d
    :try_start_e
    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    goto :goto_10

    :goto_e
    if-eqz v2, :cond_a

    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    .line 544
    :cond_a
    throw p1

    :cond_b
    :goto_f
    if-eqz v2, :cond_c

    goto :goto_d

    .line 545
    :cond_c
    :goto_10
    throw p1
    :try_end_e
    .catchall {:try_start_e .. :try_end_e} :catchall_7

    :catchall_7
    move-exception p1

    .line 486
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized readPreConversationWithServerId(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 3

    monitor-enter p0

    .line 261
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "pre_conv_server_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ?"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    .line 262
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v1, v2

    .line 263
    invoke-direct {p0, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->readConversation(Ljava/lang/String;[Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 260
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized removeAdminFAQSuggestion(Ljava/lang/String;Ljava/lang/String;)V
    .locals 3

    monitor-enter p0

    .line 1768
    :try_start_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    invoke-static {p2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v0, :cond_0

    :try_start_1
    const-string v0, "publish_id = ? AND language = ?"

    const/4 v1, 0x2

    .line 1773
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    aput-object p1, v1, v2

    const/4 p1, 0x1

    aput-object p2, v1, p1

    .line 1774
    iget-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {p1}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object p1

    const-string p2, "faq_suggestions"

    .line 1775
    invoke-virtual {p1, p2, v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :catch_0
    move-exception p1

    :try_start_2
    const-string p2, "Helpshift_ConverDB"

    const-string v0, "Error in removeAdminFAQSuggestion"

    .line 1778
    invoke-static {p2, v0, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 1781
    :cond_0
    :goto_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 1767
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized storeConversationInboxRecord(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
    .locals 5

    monitor-enter p0

    .line 384
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "user_local_id"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " = ?"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    .line 385
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    iget-wide v3, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->userLocalId:J

    invoke-static {v3, v4}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v3

    aput-object v3, v1, v2

    .line 386
    invoke-direct {p0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->conversationInboxRecordToContentValues(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Landroid/content/ContentValues;

    move-result-object v2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 388
    :try_start_1
    iget-object v3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v3}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v3

    .line 389
    iget-object v4, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "conversation_inbox"

    invoke-direct {p0, v3, v4, v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->exists(Landroid/database/sqlite/SQLiteDatabase;Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_0

    .line 391
    iget-object v4, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v4, "conversation_inbox"

    invoke-virtual {v3, v4, v2, v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I

    goto :goto_0

    .line 397
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v0, "conversation_inbox"

    const/4 v1, 0x0

    invoke-virtual {v3, v0, v1, v2}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :catch_0
    move-exception v0

    :try_start_2
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in store conversation inbox record"

    .line 401
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 403
    :goto_0
    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 383
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized updateConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 1

    monitor-enter p0

    .line 317
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 318
    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 319
    invoke-virtual {p0, v0}, Lcom/helpshift/common/conversation/ConversationDB;->updateConversations(Ljava/util/List;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 320
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 316
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized updateConversations(Ljava/util/List;)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)V"
        }
    .end annotation

    monitor-enter p0

    .line 341
    :try_start_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    if-nez v0, :cond_0

    .line 342
    monitor-exit p0

    return-void

    .line 344
    :cond_0
    :try_start_1
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 345
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 346
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    const/4 v4, 0x0

    if-eqz v3, :cond_1

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 347
    invoke-direct {p0, v3}, Lcom/helpshift/common/conversation/ConversationDB;->readableConversationToContentValues(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Landroid/content/ContentValues;

    move-result-object v5

    .line 348
    invoke-interface {v0, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const/4 v5, 0x1

    .line 349
    new-array v5, v5, [Ljava/lang/String;

    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-static {v3}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    aput-object v3, v5, v4

    .line 350
    invoke-interface {v1, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    const/4 v2, 0x0

    .line 353
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v5, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v5}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v5, "_id"

    invoke-virtual {v3, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, " = ?"

    invoke-virtual {v3, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_2

    .line 355
    :try_start_2
    iget-object v5, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v5}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v5
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    .line 356
    :try_start_3
    invoke-virtual {v5}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 357
    :goto_1
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v2

    if-ge v4, v2, :cond_2

    .line 358
    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "issues"

    .line 359
    invoke-interface {v0, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroid/content/ContentValues;

    .line 361
    invoke-interface {v1, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v7

    check-cast v7, [Ljava/lang/String;

    .line 358
    invoke-virtual {v5, v2, v6, v3, v7}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I

    add-int/lit8 v4, v4, 0x1

    goto :goto_1

    .line 363
    :cond_2
    invoke-virtual {v5}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_1
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    if-eqz v5, :cond_3

    .line 371
    :try_start_4
    invoke-virtual {v5}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_0
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    goto :goto_4

    :catch_0
    move-exception p1

    :try_start_5
    const-string v0, "Helpshift_ConverDB"

    const-string v1, "Error in update conversations inside finally block"

    .line 374
    :goto_2
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    goto :goto_4

    :catchall_0
    move-exception p1

    goto :goto_5

    :catch_1
    move-exception p1

    move-object v2, v5

    goto :goto_3

    :catchall_1
    move-exception p1

    move-object v5, v2

    goto :goto_5

    :catch_2
    move-exception p1

    :goto_3
    :try_start_6
    const-string v0, "Helpshift_ConverDB"

    const-string v1, "Error in update conversations"

    .line 366
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_1

    if-eqz v2, :cond_3

    .line 371
    :try_start_7
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_7
    .catch Ljava/lang/Exception; {:try_start_7 .. :try_end_7} :catch_3
    .catchall {:try_start_7 .. :try_end_7} :catchall_2

    goto :goto_4

    :catch_3
    move-exception p1

    :try_start_8
    const-string v0, "Helpshift_ConverDB"

    const-string v1, "Error in update conversations inside finally block"
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_2

    goto :goto_2

    .line 378
    :cond_3
    :goto_4
    monitor-exit p0

    return-void

    :goto_5
    if-eqz v5, :cond_4

    .line 371
    :try_start_9
    invoke-virtual {v5}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_9
    .catch Ljava/lang/Exception; {:try_start_9 .. :try_end_9} :catch_4
    .catchall {:try_start_9 .. :try_end_9} :catchall_2

    goto :goto_6

    :catch_4
    move-exception v0

    :try_start_a
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in update conversations inside finally block"

    .line 374
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 377
    :cond_4
    :goto_6
    throw p1
    :try_end_a
    .catchall {:try_start_a .. :try_end_a} :catchall_2

    :catchall_2
    move-exception p1

    .line 340
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized updateLastUserActivityTimeInConversation(Ljava/lang/Long;J)V
    .locals 2

    monitor-enter p0

    .line 323
    :try_start_0
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    .line 324
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "last_user_activity_time"

    invoke-static {p2, p3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p2

    invoke-virtual {v0, v1, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    .line 326
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    iget-object p3, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {p3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string p3, "_id"

    invoke-virtual {p2, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p3, " = ?"

    invoke-virtual {p2, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    const/4 p3, 0x1

    .line 327
    new-array p3, p3, [Ljava/lang/String;

    const/4 v1, 0x0

    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    aput-object p1, p3, v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 329
    :try_start_1
    iget-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {p1}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object p1

    .line 330
    iget-object v1, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v1, "issues"

    invoke-virtual {p1, v1, v0, p2, p3}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :catch_0
    move-exception p1

    :try_start_2
    const-string p2, "Helpshift_ConverDB"

    const-string p3, "Error in updateLastUserActivityTimeInConversation"

    .line 336
    invoke-static {p2, p3, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 338
    :goto_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 322
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized updateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    monitor-enter p0

    .line 679
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 680
    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 681
    invoke-virtual {p0, v0}, Lcom/helpshift/common/conversation/ConversationDB;->updateMessages(Ljava/util/List;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 682
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 678
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized updateMessages(Ljava/util/List;)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    monitor-enter p0

    .line 685
    :try_start_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    if-nez v0, :cond_0

    .line 686
    monitor-exit p0

    return-void

    .line 688
    :cond_0
    :try_start_1
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 689
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 690
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    const/4 v4, 0x0

    if-eqz v3, :cond_1

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 691
    invoke-direct {p0, v3}, Lcom/helpshift/common/conversation/ConversationDB;->readableMessageToContentValues(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Landroid/content/ContentValues;

    move-result-object v5

    .line 692
    invoke-interface {v0, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const/4 v5, 0x1

    .line 693
    new-array v5, v5, [Ljava/lang/String;

    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    invoke-static {v3}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    aput-object v3, v5, v4

    .line 694
    invoke-interface {v1, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    const/4 v2, 0x0

    .line 697
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v5, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v5}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v5, "_id"

    invoke-virtual {v3, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, " = ?"

    invoke-virtual {v3, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_2

    .line 699
    :try_start_2
    iget-object v5, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbHelper:Lcom/helpshift/platform/db/ConversationDBHelper;

    invoke-virtual {v5}, Lcom/helpshift/platform/db/ConversationDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v5
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    .line 700
    :try_start_3
    invoke-virtual {v5}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    .line 701
    :goto_1
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v2

    if-ge v4, v2, :cond_2

    .line 702
    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDB;->dbInfo:Lcom/helpshift/common/conversation/ConversationDBInfo;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const-string v2, "messages"

    .line 703
    invoke-interface {v0, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroid/content/ContentValues;

    .line 705
    invoke-interface {v1, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v7

    check-cast v7, [Ljava/lang/String;

    .line 702
    invoke-virtual {v5, v2, v6, v3, v7}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I

    add-int/lit8 v4, v4, 0x1

    goto :goto_1

    .line 707
    :cond_2
    invoke-virtual {v5}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_1
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    if-eqz v5, :cond_3

    .line 715
    :try_start_4
    invoke-virtual {v5}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_0
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    goto :goto_4

    :catch_0
    move-exception p1

    :try_start_5
    const-string v0, "Helpshift_ConverDB"

    const-string v1, "Error in update messages"

    .line 718
    :goto_2
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    goto :goto_4

    :catchall_0
    move-exception p1

    goto :goto_5

    :catch_1
    move-exception p1

    move-object v2, v5

    goto :goto_3

    :catchall_1
    move-exception p1

    move-object v5, v2

    goto :goto_5

    :catch_2
    move-exception p1

    :goto_3
    :try_start_6
    const-string v0, "Helpshift_ConverDB"

    const-string v1, "Error in update messages"

    .line 710
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_1

    if-eqz v2, :cond_3

    .line 715
    :try_start_7
    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_7
    .catch Ljava/lang/Exception; {:try_start_7 .. :try_end_7} :catch_3
    .catchall {:try_start_7 .. :try_end_7} :catchall_2

    goto :goto_4

    :catch_3
    move-exception p1

    :try_start_8
    const-string v0, "Helpshift_ConverDB"

    const-string v1, "Error in update messages"
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_2

    goto :goto_2

    .line 722
    :cond_3
    :goto_4
    monitor-exit p0

    return-void

    :goto_5
    if-eqz v5, :cond_4

    .line 715
    :try_start_9
    invoke-virtual {v5}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_9
    .catch Ljava/lang/Exception; {:try_start_9 .. :try_end_9} :catch_4
    .catchall {:try_start_9 .. :try_end_9} :catchall_2

    goto :goto_6

    :catch_4
    move-exception v0

    :try_start_a
    const-string v1, "Helpshift_ConverDB"

    const-string v2, "Error in update messages"

    .line 718
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 721
    :cond_4
    :goto_6
    throw p1
    :try_end_a
    .catchall {:try_start_a .. :try_end_a} :catchall_2

    :catchall_2
    move-exception p1

    .line 684
    monitor-exit p0

    throw p1
.end method
