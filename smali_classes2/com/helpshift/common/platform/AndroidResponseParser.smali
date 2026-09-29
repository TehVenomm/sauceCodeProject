.class Lcom/helpshift/common/platform/AndroidResponseParser;
.super Ljava/lang/Object;
.source "AndroidResponseParser.java"

# interfaces
.implements Lcom/helpshift/common/platform/network/ResponseParser;


# static fields
.field private static final OPTIONS_MAX_LIMIT:I = 0x1f4

.field private static final TAG:Ljava/lang/String; = "Helpshift_AResponseParser"


# direct methods
.method constructor <init>()V
    .locals 0

    .line 69
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private convertDeliveryStateToInt(Ljava/lang/String;)I
    .locals 5

    .line 1116
    invoke-virtual {p1}, Ljava/lang/String;->hashCode()I

    move-result v0

    const v1, -0x32158c51

    const/4 v2, 0x2

    const/4 v3, 0x1

    const/4 v4, 0x0

    if-eq v0, v1, :cond_2

    const v1, 0x355996

    if-eq v0, v1, :cond_1

    const v1, 0x35cf98

    if-eq v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const-string v0, "sent"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_3

    const/4 p1, 0x2

    goto :goto_1

    :cond_1
    const-string v0, "read"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_3

    const/4 p1, 0x1

    goto :goto_1

    :cond_2
    const-string v0, "unread"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_3

    const/4 p1, 0x0

    goto :goto_1

    :cond_3
    :goto_0
    const/4 p1, -0x1

    :goto_1
    packed-switch p1, :pswitch_data_0

    return v4

    :pswitch_0
    return v2

    :pswitch_1
    return v3

    :pswitch_2
    return v4

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private parseAdminAttachmentEntities(Lorg/json/JSONObject;Lorg/json/JSONArray;)Ljava/util/List;
    .locals 22
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lorg/json/JSONObject;",
            "Lorg/json/JSONArray;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    move-object/from16 v0, p1

    .line 977
    :try_start_0
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    const/4 v2, 0x0

    const/4 v3, 0x0

    .line 978
    :goto_0
    invoke-virtual/range {p2 .. p2}, Lorg/json/JSONArray;->length()I

    move-result v4

    if-ge v3, v4, :cond_4

    move-object/from16 v4, p2

    .line 979
    invoke-virtual {v4, v3}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v5

    .line 980
    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "id"

    invoke-virtual {v0, v7}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v7, "_"

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v9

    const-string v6, "created_at"

    .line 981
    invoke-virtual {v0, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    .line 982
    sget-object v7, Lcom/helpshift/common/util/HSDateFormatSpec;->STORAGE_TIME_FORMAT:Lcom/helpshift/common/util/HSSimpleDateFormat;

    add-int/lit8 v3, v3, 0x1

    invoke-static {v7, v6, v3}, Lcom/helpshift/common/util/HSDateFormatSpec;->addMilliSeconds(Lcom/helpshift/common/util/HSSimpleDateFormat;Ljava/lang/String;I)Ljava/lang/String;

    move-result-object v11

    .line 985
    invoke-static {v11}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v12

    const-string v6, "body"

    .line 987
    invoke-virtual {v5, v6}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v6

    if-eqz v6, :cond_0

    const-string v6, "body"

    .line 988
    invoke-virtual {v5, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    goto :goto_1

    :cond_0
    const-string v6, "body"

    invoke-virtual {v0, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    :goto_1
    move-object v10, v6

    const-string v6, "author"

    .line 989
    invoke-virtual {v0, v6}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v6

    const-string v7, "name"

    invoke-virtual {v6, v7}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v14

    const-string v6, "url"

    .line 990
    invoke-virtual {v5, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v17

    const-string v6, "content-type"

    .line 991
    invoke-virtual {v5, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v7, "size"

    .line 992
    invoke-virtual {v5, v7}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v7

    const-string v8, "file-name"

    .line 993
    invoke-virtual {v5, v8}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v18

    const-string v8, "secure?"

    .line 994
    invoke-virtual {v5, v8, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v19

    const-string v8, "redacted"

    .line 995
    invoke-virtual {v0, v8, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v8

    if-nez v8, :cond_2

    const-string v8, "redacted"

    invoke-virtual {v5, v8, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v8

    if-eqz v8, :cond_1

    goto :goto_2

    :cond_1
    const/4 v15, 0x0

    goto :goto_3

    :cond_2
    :goto_2
    const/4 v8, 0x1

    const/4 v15, 0x1

    :goto_3
    const-string v8, "md_state"

    const-string v2, ""

    .line 997
    invoke-virtual {v0, v8, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    move-object/from16 v8, p0

    invoke-direct {v8, v2}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v2

    const-string v0, "image"

    .line 999
    invoke-virtual {v5, v0}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_3

    .line 1000
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    move/from16 v21, v3

    const-string v3, "thumbnail"

    .line 1003
    invoke-virtual {v5, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    move-object v8, v0

    move v5, v15

    move-object/from16 v15, v17

    move-object/from16 v16, v18

    move-object/from16 v17, v3

    move-object/from16 v18, v6

    move/from16 v20, v7

    invoke-direct/range {v8 .. v20}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZI)V

    goto :goto_4

    :cond_3
    move/from16 v21, v3

    move v5, v15

    .line 1007
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;

    move-object v8, v0

    move v15, v7

    move-object/from16 v16, v6

    invoke-direct/range {v8 .. v19}, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)V

    .line 1011
    :goto_4
    iput v2, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->deliveryState:I

    .line 1012
    iput-boolean v5, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isRedacted:Z

    .line 1013
    invoke-interface {v1, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    move/from16 v3, v21

    move-object/from16 v0, p1

    const/4 v2, 0x0

    goto/16 :goto_0

    :cond_4
    return-object v1

    :catch_0
    move-exception v0

    .line 1018
    sget-object v1, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v2, "Parsing exception while reading admin attachment message"

    .line 1019
    invoke-static {v0, v1, v2}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0
.end method

.method private parseAdminEmptyMessageWithTextInputDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;
    .locals 17

    move-object/from16 v0, p1

    :try_start_0
    const-string v1, "input"

    .line 754
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v2, "created_at"

    .line 755
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    .line 756
    invoke-static {v6}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v7

    .line 757
    new-instance v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    const-string v3, "id"

    .line 758
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v5, ""

    const-string v3, "author"

    .line 761
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v3

    const-string v9, "name"

    invoke-virtual {v3, v9}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    const-string v3, "chatbot_info"

    .line 762
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v3

    invoke-virtual {v3}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v10

    const-string v3, "placeholder"

    .line 763
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v11

    const-string v3, "required"

    .line 764
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v12

    const-string v3, "label"

    .line 765
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    const-string v3, "skip_label"

    .line 766
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v14

    const/4 v15, 0x1

    const/16 v16, 0x1

    move-object v3, v2

    invoke-direct/range {v3 .. v16}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;IZ)V

    const-string v1, "md_state"

    const-string v3, ""

    .line 769
    invoke-virtual {v0, v1, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    move-object/from16 v3, p0

    :try_start_1
    invoke-direct {v3, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v1

    iput v1, v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->deliveryState:I

    const-string v1, "redacted"

    const/4 v4, 0x0

    .line 770
    invoke-virtual {v0, v1, v4}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v0

    iput-boolean v0, v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->isRedacted:Z
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    return-object v2

    :catch_0
    move-exception v0

    goto :goto_0

    :catch_1
    move-exception v0

    move-object/from16 v3, p0

    .line 774
    :goto_0
    sget-object v1, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v2, "Parsing exception while reading admin empty message with text input"

    invoke-static {v0, v1, v2}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0
.end method

.method private parseAdminMessage(Ljava/lang/String;Lorg/json/JSONObject;Ljava/util/List;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Lorg/json/JSONObject;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    const/4 v0, -0x1

    .line 400
    :try_start_0
    invoke-virtual {p1}, Ljava/lang/String;->hashCode()I

    move-result v1

    const/4 v2, 0x4

    const/4 v3, 0x3

    const/4 v4, 0x2

    const/4 v5, 0x1

    sparse-switch v1, :sswitch_data_0

    goto/16 :goto_0

    :sswitch_0
    const-string v1, "txt_msg_with_numeric_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x3

    goto/16 :goto_0

    :sswitch_1
    const-string v1, "bot_ended"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0xd

    goto/16 :goto_0

    :sswitch_2
    const-string v1, "empty_msg_with_txt_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x5

    goto/16 :goto_0

    :sswitch_3
    const-string v1, "bot_started"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0xc

    goto/16 :goto_0

    :sswitch_4
    const-string v1, "faq_list"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0xa

    goto :goto_0

    :sswitch_5
    const-string v1, "txt_msg_with_txt_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :sswitch_6
    const-string v1, "txt_msg_with_option_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x6

    goto :goto_0

    :sswitch_7
    const-string v1, "txt_msg_with_dt_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x4

    goto :goto_0

    :sswitch_8
    const-string v1, "txt"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x0

    goto :goto_0

    :sswitch_9
    const-string v1, "rsc"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0x8

    goto :goto_0

    :sswitch_a
    const-string v1, "rfr"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0x9

    goto :goto_0

    :sswitch_b
    const-string v1, "rar"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x7

    goto :goto_0

    :sswitch_c
    const-string v1, "txt_msg_with_email_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x2

    goto :goto_0

    :sswitch_d
    const-string v1, "faq_list_msg_with_option_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0xb

    :cond_0
    :goto_0
    packed-switch v0, :pswitch_data_0

    const-string p1, "input"

    goto/16 :goto_1

    .line 439
    :pswitch_0
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1, v5}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseBotControlMessage(Ljava/lang/String;Z)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto/16 :goto_2

    .line 435
    :pswitch_1
    invoke-direct {p0, p2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseFAQListMessageWitOptionInputDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto/16 :goto_2

    .line 432
    :pswitch_2
    invoke-direct {p0, p2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseFAQListMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto/16 :goto_2

    .line 429
    :pswitch_3
    invoke-direct {p0, p2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseRequestForReopenMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 426
    :pswitch_4
    invoke-direct {p0, p2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseRequestScreenshotMessageDM(Lorg/json/JSONObject;)Ljava/util/List;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    goto :goto_2

    .line 423
    :pswitch_5
    invoke-direct {p0, p2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseRequestAppReviewMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 420
    :pswitch_6
    invoke-direct {p0, p2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAdminMessageWithOptionInputDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 417
    :pswitch_7
    invoke-direct {p0, p2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAdminEmptyMessageWithTextInputDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 414
    :pswitch_8
    invoke-direct {p0, p2, v2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAdminMessageWithTextInputDM(Lorg/json/JSONObject;I)Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 411
    :pswitch_9
    invoke-direct {p0, p2, v3}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAdminMessageWithTextInputDM(Lorg/json/JSONObject;I)Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 408
    :pswitch_a
    invoke-direct {p0, p2, v4}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAdminMessageWithTextInputDM(Lorg/json/JSONObject;I)Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 405
    :pswitch_b
    invoke-direct {p0, p2, v5}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAdminMessageWithTextInputDM(Lorg/json/JSONObject;I)Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 402
    :pswitch_c
    invoke-direct {p0, p2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAdminMessageDM(Lorg/json/JSONObject;)Ljava/util/List;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    goto :goto_2

    .line 442
    :goto_1
    invoke-virtual {p2, p1}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_1

    .line 443
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseUnsupportedAdminMessageWithInput(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UnsupportedAdminMessageWithInputDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_2

    :catch_0
    move-exception p1

    const-string p2, "Helpshift_AResponseParser"

    const-string p3, "Exception while parsing messages: "

    .line 448
    invoke-static {p2, p3, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_1
    :goto_2
    return-void

    :sswitch_data_0
    .sparse-switch
        -0x3ebff4fd -> :sswitch_d
        -0xb0d3d25 -> :sswitch_c
        0x1b823 -> :sswitch_b
        0x1b8be -> :sswitch_a
        0x1ba42 -> :sswitch_9
        0x1c270 -> :sswitch_8
        0x7f3ce67 -> :sswitch_7
        0x1640a48c -> :sswitch_6
        0x1fdc97af -> :sswitch_5
        0x35355c27 -> :sswitch_4
        0x35e1ae09 -> :sswitch_3
        0x5d46a5b2 -> :sswitch_2
        0x6d06fa42 -> :sswitch_1
        0x7e0aec8c -> :sswitch_0
    .end sparse-switch

    :pswitch_data_0
    .packed-switch 0x0
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
        :pswitch_0
    .end packed-switch
.end method

.method private parseAdminMessageDM(Lorg/json/JSONObject;)Ljava/util/List;
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lorg/json/JSONObject;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 807
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const-string v1, "meta"

    .line 808
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v2, "attachments"

    invoke-virtual {v1, v2}, Lorg/json/JSONObject;->optJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v1

    const-string v2, "created_at"

    .line 809
    invoke-virtual {p1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    .line 810
    invoke-static {v6}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v7

    .line 812
    new-instance v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;

    const-string v3, "id"

    invoke-virtual {p1, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v3, "body"

    .line 813
    invoke-virtual {p1, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v3, "author"

    .line 816
    invoke-virtual {p1, v3}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v3

    const-string v9, "name"

    .line 817
    invoke-virtual {v3, v9}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    move-object v3, v2

    invoke-direct/range {v3 .. v9}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;)V

    const-string v3, "md_state"

    const-string v4, ""

    .line 818
    invoke-virtual {p1, v3, v4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-direct {p0, v3}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v3

    iput v3, v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->deliveryState:I

    const-string v3, "redacted"

    const/4 v4, 0x0

    .line 819
    invoke-virtual {p1, v3, v4}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v3

    iput-boolean v3, v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->isRedacted:Z

    .line 820
    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    if-eqz v1, :cond_0

    .line 822
    invoke-direct {p0, p1, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAdminAttachmentEntities(Lorg/json/JSONObject;Lorg/json/JSONArray;)Ljava/util/List;

    move-result-object p1

    invoke-interface {v0, p1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    :cond_0
    return-object v0

    :catch_0
    move-exception p1

    .line 827
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading admin text message"

    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method private parseAdminMessageWithOptionInputDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 17

    move-object/from16 v1, p0

    move-object/from16 v0, p1

    :try_start_0
    const-string v2, "input"

    .line 684
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    const-string v3, "created_at"

    .line 685
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    .line 686
    invoke-static {v7}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v8

    .line 687
    new-instance v3, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;

    const-string v4, "id"

    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v4, "body"

    .line 688
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v4, "author"

    .line 690
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v4

    const-string v10, "name"

    .line 691
    invoke-virtual {v4, v10}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v10

    const-string v4, "chatbot_info"

    .line 692
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v4

    invoke-virtual {v4}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v11

    const-string v4, "required"

    .line 693
    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v12

    const-string v4, "label"

    .line 694
    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    const-string v4, "skip_label"

    .line 695
    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v14

    .line 696
    invoke-direct {v1, v2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseOptions(Lorg/json/JSONObject;)Ljava/util/List;

    move-result-object v15

    invoke-direct {v1, v2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseOptionType(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    move-result-object v16

    move-object v4, v3

    invoke-direct/range {v4 .. v16}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Ljava/util/List;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;)V

    const-string v2, "md_state"

    const-string v4, ""

    .line 697
    invoke-virtual {v0, v2, v4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v2

    iput v2, v3, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->deliveryState:I

    const-string v2, "redacted"

    const/4 v4, 0x0

    .line 699
    invoke-virtual {v0, v2, v4}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v0

    iput-boolean v0, v3, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->isRedacted:Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v3

    :catch_0
    move-exception v0

    .line 703
    sget-object v2, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v3, "Parsing exception while reading admin text message with option input"

    .line 704
    invoke-static {v0, v2, v3}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0
.end method

.method private parseAdminMessageWithTextInputDM(Lorg/json/JSONObject;I)Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;
    .locals 17

    move-object/from16 v0, p1

    :try_start_0
    const-string v1, "input"

    .line 782
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v2, "created_at"

    .line 783
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    .line 784
    invoke-static {v6}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v7

    .line 785
    new-instance v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    const-string v3, "id"

    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v3, "body"

    .line 786
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v3, "author"

    .line 788
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v3

    const-string v9, "name"

    invoke-virtual {v3, v9}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    const-string v3, "chatbot_info"

    .line 789
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v3

    invoke-virtual {v3}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v10

    const-string v3, "placeholder"

    .line 790
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v11

    const-string v3, "required"

    .line 791
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v12

    const-string v3, "label"

    .line 792
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    const-string v3, "skip_label"

    .line 793
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v14

    const/16 v16, 0x0

    move-object v3, v2

    move/from16 v15, p2

    invoke-direct/range {v3 .. v16}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;IZ)V

    const-string v1, "md_state"

    const-string v3, ""

    .line 795
    invoke-virtual {v0, v1, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    move-object/from16 v3, p0

    :try_start_1
    invoke-direct {v3, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v1

    iput v1, v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->deliveryState:I

    const-string v1, "redacted"

    const/4 v4, 0x0

    .line 796
    invoke-virtual {v0, v1, v4}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v0

    iput-boolean v0, v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->isRedacted:Z
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    return-object v2

    :catch_0
    move-exception v0

    goto :goto_0

    :catch_1
    move-exception v0

    move-object/from16 v3, p0

    .line 800
    :goto_0
    sget-object v1, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v2, "Parsing exception while reading admin message with text input"

    invoke-static {v0, v1, v2}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0
.end method

.method private parseAndSetDataForUserSentMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "author"

    .line 1131
    invoke-virtual {p2, v0}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    const-string v1, "id"

    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->authorId:Ljava/lang/String;

    const-string v0, "request_id"

    .line 1132
    invoke-virtual {p2, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->createdRequestId:Ljava/lang/String;

    return-void
.end method

.method private parseDisableHelpshiftBrandingValue(Lorg/json/JSONObject;)Z
    .locals 2

    if-eqz p1, :cond_0

    const-string v0, "hl"

    const-string v1, "true"

    .line 365
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    const-string v0, "true"

    .line 366
    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    xor-int/lit8 p1, p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method private parseFAQList(Lorg/json/JSONArray;)Ljava/util/List;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lorg/json/JSONArray;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;",
            ">;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 541
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const/4 v1, 0x0

    .line 542
    :goto_0
    invoke-virtual {p1}, Lorg/json/JSONArray;->length()I

    move-result v2

    if-ge v1, v2, :cond_0

    .line 543
    invoke-virtual {p1, v1}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v2

    const-string v3, "data"

    .line 544
    invoke-virtual {v2, v3}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v3

    .line 546
    new-instance v4, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;

    const-string v5, "title"

    invoke-virtual {v2, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v5, "publish_id"

    .line 547
    invoke-virtual {v3, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v6, "language"

    .line 548
    invoke-virtual {v3, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-direct {v4, v2, v5, v3}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 546
    invoke-interface {v0, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    return-object v0
.end method

.method private parseFAQListMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;
    .locals 9

    :try_start_0
    const-string v0, "created_at"

    .line 583
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    .line 584
    invoke-static {v4}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v5

    .line 585
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    const-string v1, "id"

    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v1, "body"

    .line 586
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v1, "author"

    .line 588
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v7, "name"

    invoke-virtual {v1, v7}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    const-string v1, "faqs"

    .line 589
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v1

    invoke-direct {p0, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseFAQList(Lorg/json/JSONArray;)Ljava/util/List;

    move-result-object v8

    move-object v1, v0

    invoke-direct/range {v1 .. v8}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/util/List;)V

    const-string v1, "md_state"

    const-string v2, ""

    .line 590
    invoke-virtual {p1, v1, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v1

    iput v1, v0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->deliveryState:I

    const-string v1, "redacted"

    const/4 v2, 0x0

    .line 592
    invoke-virtual {p1, v1, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result p1

    iput-boolean p1, v0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->isRedacted:Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p1

    .line 596
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading faq list message"

    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method private parseFAQListMessageWitOptionInputDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 17

    move-object/from16 v1, p0

    move-object/from16 v0, p1

    :try_start_0
    const-string v2, "input"

    .line 556
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    const-string v3, "created_at"

    .line 557
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    .line 558
    invoke-static {v7}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v8

    .line 559
    new-instance v3, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;

    const-string v4, "id"

    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v4, "body"

    .line 560
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v4, "author"

    .line 562
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v4

    const-string v10, "name"

    .line 563
    invoke-virtual {v4, v10}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v10

    const-string v4, "faqs"

    .line 564
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v4

    invoke-direct {v1, v4}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseFAQList(Lorg/json/JSONArray;)Ljava/util/List;

    move-result-object v11

    const-string v4, "chatbot_info"

    .line 565
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v4

    invoke-virtual {v4}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v12

    const-string v4, "required"

    .line 566
    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v13

    const-string v4, "label"

    .line 567
    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v14

    const-string v4, "skip_label"

    .line 568
    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v15

    .line 569
    invoke-direct {v1, v2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseOptions(Lorg/json/JSONObject;)Ljava/util/List;

    move-result-object v16

    move-object v4, v3

    invoke-direct/range {v4 .. v16}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/util/List;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Ljava/util/List;)V

    const-string v2, "md_state"

    const-string v4, ""

    .line 570
    invoke-virtual {v0, v2, v4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v2

    iput v2, v3, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->deliveryState:I

    const-string v2, "redacted"

    const/4 v4, 0x0

    .line 571
    invoke-virtual {v0, v2, v4}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v0

    iput-boolean v0, v3, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->isRedacted:Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v3

    :catch_0
    move-exception v0

    .line 575
    sget-object v2, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v3, "Parsing exception while reading list message with option input"

    invoke-static {v0, v2, v3}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0
.end method

.method private parseFollowupAcceptedMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;
    .locals 9

    :try_start_0
    const-string v0, "created_at"

    .line 626
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 627
    invoke-static {v3}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v4

    .line 628
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;

    const-string v1, "body"

    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v1, "author"

    .line 630
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v6, "name"

    .line 631
    invoke-virtual {v1, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v1, "meta"

    .line 632
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v7, "refers"

    .line 633
    invoke-virtual {v1, v7}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    const/4 v8, 0x2

    move-object v1, v0

    invoke-direct/range {v1 .. v8}, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;I)V

    const-string v1, "id"

    .line 635
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    iput-object v1, v0, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;->serverId:Ljava/lang/String;

    const-string v1, "md_state"

    const-string v2, ""

    .line 636
    invoke-virtual {p1, v1, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v1

    iput v1, v0, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;->deliveryState:I

    const-string v1, "redacted"

    const/4 v2, 0x0

    .line 637
    invoke-virtual {p1, v1, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v1

    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;->isRedacted:Z

    .line 638
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAndSetDataForUserSentMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p1

    .line 642
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading follow-up accepted message"

    .line 643
    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method private parseFollowupRejectedMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;
    .locals 9

    :try_start_0
    const-string v0, "created_at"

    .line 603
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 604
    invoke-static {v3}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v4

    .line 605
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;

    const-string v1, "body"

    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v1, "author"

    .line 607
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v6, "name"

    .line 608
    invoke-virtual {v1, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v1, "meta"

    .line 609
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v7, "refers"

    .line 610
    invoke-virtual {v1, v7}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    const/4 v8, 0x2

    move-object v1, v0

    invoke-direct/range {v1 .. v8}, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;I)V

    const-string v1, "id"

    .line 612
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    iput-object v1, v0, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;->serverId:Ljava/lang/String;

    const-string v1, "md_state"

    const-string v2, ""

    .line 613
    invoke-virtual {p1, v1, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v1

    iput v1, v0, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;->deliveryState:I

    const-string v1, "redacted"

    const/4 v2, 0x0

    .line 614
    invoke-virtual {p1, v1, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v1

    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;->isRedacted:Z

    .line 615
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAndSetDataForUserSentMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p1

    .line 619
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading follow-up rejected message"

    .line 620
    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method private parseMessageDMs(Lorg/json/JSONArray;)Ljava/util/List;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lorg/json/JSONArray;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 372
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 374
    invoke-virtual {p1}, Lorg/json/JSONArray;->length()I

    move-result v1

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_2

    .line 377
    :try_start_0
    invoke-virtual {p1, v2}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v3

    const-string v4, "type"

    .line 378
    invoke-virtual {v3, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v5, "origin"

    .line 379
    invoke-virtual {v3, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v6, "admin"

    .line 381
    invoke-virtual {v6, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_0

    .line 382
    invoke-direct {p0, v4, v3, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAdminMessage(Ljava/lang/String;Lorg/json/JSONObject;Ljava/util/List;)V

    goto :goto_1

    :cond_0
    const-string v6, "mobile"

    .line 384
    invoke-virtual {v6, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_1

    .line 385
    invoke-direct {p0, v4, v3, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseMobileMessage(Ljava/lang/String;Lorg/json/JSONObject;Ljava/util/List;)V

    goto :goto_1

    :cond_1
    const-string v3, "Helpshift_AResponseParser"

    const-string v4, "Unknown message type received."

    .line 388
    invoke-static {v3, v4}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v3

    const-string v4, "Helpshift_AResponseParser"

    const-string v5, "Exception while parsing messages: "

    .line 392
    invoke-static {v4, v5, v3}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_1
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_2
    return-object v0
.end method

.method private parseMobileMessage(Ljava/lang/String;Lorg/json/JSONObject;Ljava/util/List;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Lorg/json/JSONObject;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    const/4 v0, -0x1

    .line 454
    :try_start_0
    invoke-virtual {p1}, Ljava/lang/String;->hashCode()I

    move-result v1

    const/4 v2, 0x0

    sparse-switch v1, :sswitch_data_0

    goto/16 :goto_0

    :sswitch_0
    const-string v1, "rsp_txt_msg_with_dt_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0xb

    goto/16 :goto_0

    :sswitch_1
    const-string v1, "rsp_txt_msg_with_option_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0xc

    goto/16 :goto_0

    :sswitch_2
    const-string v1, "rsp_empty_msg_with_txt_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x7

    goto/16 :goto_0

    :sswitch_3
    const-string v1, "rsp_txt_msg_with_txt_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0x8

    goto/16 :goto_0

    :sswitch_4
    const-string v1, "txt"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x0

    goto/16 :goto_0

    :sswitch_5
    const-string v1, "ncr"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x2

    goto :goto_0

    :sswitch_6
    const-string v1, "sc"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x4

    goto :goto_0

    :sswitch_7
    const-string v1, "rj"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x6

    goto :goto_0

    :sswitch_8
    const-string v1, "ra"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x5

    goto :goto_0

    :sswitch_9
    const-string v1, "ca"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x3

    goto :goto_0

    :sswitch_a
    const-string v1, "ar"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :sswitch_b
    const-string v1, "rsp_txt_msg_with_numeric_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0xa

    goto :goto_0

    :sswitch_c
    const-string v1, "bot_cancelled"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0xe

    goto :goto_0

    :sswitch_d
    const-string v1, "rsp_faq_list_msg_with_option_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0xd

    goto :goto_0

    :sswitch_e
    const-string v1, "rsp_txt_msg_with_email_input"

    invoke-virtual {p1, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 v0, 0x9

    :cond_0
    :goto_0
    packed-switch v0, :pswitch_data_0

    goto/16 :goto_1

    .line 488
    :pswitch_0
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1, v2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseBotControlMessage(Ljava/lang/String;Z)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 485
    :pswitch_1
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseResponseMessageForOptionInput(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 481
    :pswitch_2
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseResponseMessageForTextInput(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 474
    :pswitch_3
    invoke-direct {p0, p2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseFollowupRejectedMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 471
    :pswitch_4
    invoke-direct {p0, p2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseFollowupAcceptedMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 468
    :pswitch_5
    invoke-direct {p0, p2}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseScreenshotMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 465
    :pswitch_6
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseConfirmationAcceptedMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 462
    :pswitch_7
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseConfirmationRejectedMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 459
    :pswitch_8
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAcceptedAppReviewMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 456
    :pswitch_9
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseReadableUserMessage(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    move-result-object p1

    invoke-interface {p3, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception p1

    const-string p2, "Helpshift_AResponseParser"

    const-string p3, "Exception while parsing messages: "

    .line 493
    invoke-static {p2, p3, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_1
    return-void

    :sswitch_data_0
    .sparse-switch
        -0x318c7d35 -> :sswitch_e
        -0x2732e90d -> :sswitch_d
        -0x2086ab27 -> :sswitch_c
        -0x5a48f84 -> :sswitch_b
        0xc31 -> :sswitch_a
        0xc5e -> :sswitch_9
        0xe2f -> :sswitch_8
        0xe38 -> :sswitch_7
        0xe50 -> :sswitch_6
        0x1a95d -> :sswitch_5
        0x1c270 -> :sswitch_4
        0x1d6c939f -> :sswitch_3
        0x36c765a2 -> :sswitch_2
        0x6cd7e29c -> :sswitch_1
        0x7b7c9477 -> :sswitch_0
    .end sparse-switch

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_9
        :pswitch_8
        :pswitch_7
        :pswitch_6
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_2
        :pswitch_2
        :pswitch_2
        :pswitch_2
        :pswitch_1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private parseOptionType(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "type"

    .line 678
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "options"

    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object p1

    invoke-virtual {p1}, Lorg/json/JSONArray;->length()I

    move-result p1

    invoke-static {v0, p1}, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;->getType(Ljava/lang/String;I)Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    move-result-object p1

    return-object p1
.end method

.method private parseOptions(Lorg/json/JSONObject;)Ljava/util/List;
    .locals 7
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

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 666
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const-string v1, "options"

    .line 667
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object p1

    .line 668
    invoke-virtual {p1}, Lorg/json/JSONArray;->length()I

    move-result v1

    const/16 v2, 0x1f4

    invoke-static {v1, v2}, Ljava/lang/Math;->min(II)I

    move-result v1

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_0

    .line 670
    invoke-virtual {p1, v2}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v3

    .line 671
    new-instance v4, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    const-string v5, "title"

    invoke-virtual {v3, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v6, "data"

    .line 672
    invoke-virtual {v3, v6}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v3

    invoke-virtual {v3}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v4, v5, v3}, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    .line 671
    invoke-interface {v0, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_0
    return-object v0
.end method

.method private parsePeriodicReview(Lorg/json/JSONObject;)Lcom/helpshift/configuration/response/PeriodicReview;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 1110
    new-instance v0, Lcom/helpshift/configuration/response/PeriodicReview;

    const-string v1, "s"

    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;)Z

    move-result v1

    const-string v2, "i"

    .line 1111
    invoke-virtual {p1, v2}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;)I

    move-result v2

    const-string v3, "t"

    const-string v4, ""

    .line 1112
    invoke-virtual {p1, v3, v4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, v1, v2, p1}, Lcom/helpshift/configuration/response/PeriodicReview;-><init>(ZILjava/lang/String;)V

    return-object v0
.end method

.method private parseRequestAppReviewMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;
    .locals 10

    :try_start_0
    const-string v0, "meta"

    .line 1026
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    const-string v1, "response"

    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    const-string v2, "state"

    .line 1028
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;)Z

    move-result v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    const-string v2, "invisible"

    .line 1030
    invoke-virtual {p1, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_2

    if-eqz v0, :cond_1

    goto :goto_1

    :cond_1
    const/4 v9, 0x0

    goto :goto_2

    :cond_2
    :goto_1
    const/4 v0, 0x1

    const/4 v9, 0x1

    :goto_2
    const-string v0, "created_at"

    .line 1031
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    .line 1032
    invoke-static {v5}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v6

    .line 1033
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;

    const-string v2, "id"

    invoke-virtual {p1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v2, "body"

    .line 1034
    invoke-virtual {p1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v2, "author"

    .line 1036
    invoke-virtual {p1, v2}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    const-string v8, "name"

    .line 1037
    invoke-virtual {v2, v8}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    move-object v2, v0

    invoke-direct/range {v2 .. v9}, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Z)V

    const-string v2, "md_state"

    const-string v3, ""

    .line 1039
    invoke-virtual {p1, v2, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-direct {p0, v2}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v2

    iput v2, v0, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;->deliveryState:I

    const-string v2, "redacted"

    .line 1040
    invoke-virtual {p1, v2, v1}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result p1

    iput-boolean p1, v0, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;->isRedacted:Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p1

    .line 1044
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading request review message"

    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method private parseRequestForReopenMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;
    .locals 8

    :try_start_0
    const-string v0, "created_at"

    .line 649
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    .line 650
    invoke-static {v4}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v5

    .line 651
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;

    const-string v1, "id"

    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v1, "body"

    .line 652
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v1, "author"

    .line 654
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v7, "name"

    .line 655
    invoke-virtual {v1, v7}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    move-object v1, v0

    invoke-direct/range {v1 .. v7}, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;)V

    const-string v1, "md_state"

    const-string v2, ""

    .line 656
    invoke-virtual {p1, v1, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v1

    iput v1, v0, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;->deliveryState:I

    const-string v1, "redacted"

    const/4 v2, 0x0

    .line 657
    invoke-virtual {p1, v1, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result p1

    iput-boolean p1, v0, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;->isRedacted:Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p1

    .line 661
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading reopen message"

    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method private parseRequestScreenshotMessageDM(Lorg/json/JSONObject;)Ljava/util/List;
    .locals 12
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lorg/json/JSONObject;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 1050
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const-string v1, "meta"

    .line 1052
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v2, "response"

    .line 1053
    invoke-virtual {v1, v2}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    const-string v3, "attachments"

    .line 1054
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->optJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v1

    const/4 v3, 0x0

    if-eqz v2, :cond_0

    const-string v4, "state"

    .line 1056
    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v2

    move v11, v2

    goto :goto_0

    :cond_0
    const/4 v11, 0x0

    :goto_0
    const-string v2, "created_at"

    .line 1058
    invoke-virtual {p1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    .line 1059
    invoke-static {v7}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v8

    .line 1060
    new-instance v2, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    const-string v4, "id"

    invoke-virtual {p1, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v4, "body"

    .line 1061
    invoke-virtual {p1, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v4, "author"

    .line 1063
    invoke-virtual {p1, v4}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v4

    const-string v10, "name"

    .line 1064
    invoke-virtual {v4, v10}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v10

    move-object v4, v2

    invoke-direct/range {v4 .. v11}, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Z)V

    const-string v4, "md_state"

    const-string v5, ""

    .line 1066
    invoke-virtual {p1, v4, v5}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-direct {p0, v4}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v4

    iput v4, v2, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->deliveryState:I

    const-string v4, "redacted"

    .line 1067
    invoke-virtual {p1, v4, v3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v3

    iput-boolean v3, v2, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->isRedacted:Z

    .line 1068
    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    if-eqz v1, :cond_1

    .line 1070
    invoke-direct {p0, p1, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAdminAttachmentEntities(Lorg/json/JSONObject;Lorg/json/JSONArray;)Ljava/util/List;

    move-result-object p1

    invoke-interface {v0, p1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    :cond_1
    return-object v0

    :catch_0
    move-exception p1

    .line 1075
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading request screenshot message"

    .line 1076
    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method private parseScreenshotMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;
    .locals 17

    move-object/from16 v1, p0

    move-object/from16 v0, p1

    :try_start_0
    const-string v2, "meta"

    .line 1082
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    const-string v3, "attachments"

    .line 1083
    invoke-virtual {v2, v3}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v2

    const/4 v3, 0x0

    invoke-virtual {v2, v3}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v2

    const-string v4, "created_at"

    .line 1084
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    .line 1085
    invoke-static {v7}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v8

    const-string v4, "body"

    .line 1086
    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_0

    const-string v4, "body"

    .line 1087
    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    goto :goto_0

    :cond_0
    const-string v4, "body"

    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    :goto_0
    move-object v6, v4

    .line 1088
    new-instance v4, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    const-string v5, "author"

    .line 1090
    invoke-virtual {v0, v5}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v5

    const-string v10, "name"

    .line 1091
    invoke-virtual {v5, v10}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v10

    const-string v5, "content-type"

    .line 1092
    invoke-virtual {v2, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v11

    const-string v5, "thumbnail"

    const-string v12, ""

    .line 1093
    invoke-virtual {v2, v5, v12}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v12

    const-string v5, "file-name"

    .line 1094
    invoke-virtual {v2, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    const-string v5, "url"

    .line 1095
    invoke-virtual {v2, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v14

    const-string v5, "size"

    .line 1096
    invoke-virtual {v2, v5}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v15

    const-string v5, "secure?"

    .line 1097
    invoke-virtual {v2, v5, v3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v16

    move-object v5, v4

    invoke-direct/range {v5 .. v16}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;IZ)V

    const-string v5, "id"

    .line 1098
    invoke-virtual {v0, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    iput-object v5, v4, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->serverId:Ljava/lang/String;

    const-string v5, "md_state"

    const-string v6, ""

    .line 1099
    invoke-virtual {v0, v5, v6}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-direct {v1, v5}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v5

    iput v5, v4, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->deliveryState:I

    const-string v5, "redacted"

    .line 1100
    invoke-virtual {v0, v5, v3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v5

    if-nez v5, :cond_1

    const-string v5, "redacted"

    invoke-virtual {v2, v5, v3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v2

    if-eqz v2, :cond_2

    :cond_1
    const/4 v3, 0x1

    :cond_2
    iput-boolean v3, v4, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->isRedacted:Z

    .line 1101
    invoke-direct {v1, v4, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAndSetDataForUserSentMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v4

    :catch_0
    move-exception v0

    .line 1105
    sget-object v2, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v3, "Parsing exception while reading screenshot message"

    invoke-static {v0, v2, v3}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0
.end method

.method private parseUnsupportedAdminMessageWithInput(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UnsupportedAdminMessageWithInputDM;
    .locals 11

    .line 834
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p1, "created_at"

    .line 835
    invoke-virtual {v0, p1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    .line 836
    invoke-static {v4}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v5

    .line 837
    new-instance p1, Lcom/helpshift/conversation/activeconversation/message/UnsupportedAdminMessageWithInputDM;

    const-string v1, "id"

    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v1, "body"

    .line 838
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v1, "author"

    .line 840
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v7, "name"

    .line 841
    invoke-virtual {v1, v7}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    const-string v1, "type"

    .line 842
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    const-string v1, "chatbot_info"

    .line 843
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    invoke-virtual {v1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v9

    const-string v1, "input"

    .line 844
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v10

    move-object v1, p1

    invoke-direct/range {v1 .. v10}, Lcom/helpshift/conversation/activeconversation/message/UnsupportedAdminMessageWithInputDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 847
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading unsupported admin message with input"

    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method


# virtual methods
.method public parseAcceptedAppReviewMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;
    .locals 9

    .line 167
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p1, "created_at"

    .line 168
    invoke-virtual {v0, p1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 169
    invoke-static {v3}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v4

    .line 170
    new-instance p1, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;

    const-string v1, "body"

    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v1, "author"

    .line 172
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v6, "name"

    .line 173
    invoke-virtual {v1, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v1, "meta"

    .line 174
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v7, "refers"

    .line 175
    invoke-virtual {v1, v7}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    const/4 v8, 0x2

    move-object v1, p1

    invoke-direct/range {v1 .. v8}, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;I)V

    const-string v1, "id"

    .line 177
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->serverId:Ljava/lang/String;

    const-string v1, "md_state"

    const-string v2, ""

    .line 178
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v1

    iput v1, p1, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->deliveryState:I

    const-string v1, "redacted"

    const/4 v2, 0x0

    .line 179
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v1

    iput-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->isRedacted:Z

    .line 180
    invoke-direct {p0, p1, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAndSetDataForUserSentMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 184
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading accepted review message"

    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method public parseAuthToken(Ljava/lang/String;)Lcom/helpshift/auth/dto/WebSocketAuthData;
    .locals 2

    .line 306
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p1, "token"

    .line 307
    invoke-virtual {v0, p1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    const-string v1, "endpoint"

    .line 308
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 309
    new-instance v1, Lcom/helpshift/auth/dto/WebSocketAuthData;

    invoke-direct {v1, p1, v0}, Lcom/helpshift/auth/dto/WebSocketAuthData;-><init>(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "Helpshift_AResponseParser"

    const-string v1, "Exception in parsing auth token"

    .line 312
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 v1, 0x0

    :goto_0
    return-object v1
.end method

.method public parseBotControlMessage(Ljava/lang/String;Z)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 16

    .line 500
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    move-object/from16 v1, p1

    invoke-direct {v0, v1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v1, "type"

    .line 501
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "id"

    .line 502
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    const-string v2, "chatbot_info"

    .line 503
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    invoke-virtual {v2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v10

    const-string v2, "redacted"

    const/4 v11, 0x0

    .line 504
    invoke-virtual {v0, v2, v11}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v14

    const-string v2, "created_at"

    .line 505
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    .line 506
    invoke-static {v5}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v6

    if-eqz p2, :cond_0

    .line 508
    new-instance v12, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;

    const-string v2, "body"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v2, "author"

    .line 511
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    const-string v3, "name"

    .line 512
    invoke-virtual {v2, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    move-object v2, v12

    move-object v3, v13

    move-object v9, v1

    invoke-direct/range {v2 .. v10}, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "has_next_bot"

    .line 514
    invoke-virtual {v0, v1, v11}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v0

    iput-boolean v0, v12, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->hasNextBot:Z

    .line 515
    iput-boolean v14, v12, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->isRedacted:Z

    return-object v12

    :cond_0
    const-string v2, "meta"

    .line 519
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    const-string v3, "chatbot_cancelled_reason"

    .line 520
    invoke-virtual {v2, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    const-string v3, "refers"

    .line 521
    invoke-virtual {v2, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v11

    .line 522
    new-instance v15, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;

    const-string v2, "body"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v2, "author"

    .line 524
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    const-string v4, "name"

    .line 525
    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    const/4 v12, 0x2

    move-object v2, v15

    move-object v4, v5

    move-wide v5, v6

    move-object v7, v8

    move-object v8, v1

    invoke-direct/range {v2 .. v12}, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V

    .line 529
    iput-object v13, v15, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->serverId:Ljava/lang/String;

    .line 530
    iput-boolean v14, v15, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->isRedacted:Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    move-object/from16 v1, p0

    .line 531
    :try_start_1
    invoke-direct {v1, v15, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAndSetDataForUserSentMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    return-object v15

    :catch_0
    move-exception v0

    goto :goto_0

    :catch_1
    move-exception v0

    move-object/from16 v1, p0

    .line 536
    :goto_0
    sget-object v2, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v3, "Parsing exception while reading bot control messages."

    invoke-static {v0, v2, v3}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0
.end method

.method public parseConfigResponse(Ljava/lang/String;)Lcom/helpshift/configuration/response/RootServerConfig;
    .locals 29

    move-object/from16 v1, p0

    .line 855
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    move-object/from16 v2, p1

    invoke-direct {v0, v2}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v2, "last_redaction_at"

    .line 857
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    const/4 v3, 0x0

    if-eqz v2, :cond_0

    const-string v2, "last_redaction_at"

    .line 858
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getLong(Ljava/lang/String;)J

    move-result-wide v4

    invoke-static {v4, v5}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    move-object/from16 v21, v2

    goto :goto_0

    :cond_0
    move-object/from16 v21, v3

    :goto_0
    const-string v2, "profile_created_at"

    .line 861
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_1

    const-string v2, "profile_created_at"

    .line 862
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getLong(Ljava/lang/String;)J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    :cond_1
    move-object/from16 v22, v3

    const-string v2, "pfi"

    const-wide/16 v3, 0x0

    .line 866
    invoke-virtual {v0, v2, v3, v4}, Lorg/json/JSONObject;->optLong(Ljava/lang/String;J)J

    move-result-wide v5

    const-wide/16 v7, 0x3e8

    div-long v24, v5, v7

    const-string v2, "pri"

    .line 869
    invoke-virtual {v0, v2, v3, v4}, Lorg/json/JSONObject;->optLong(Ljava/lang/String;J)J

    move-result-wide v2

    div-long v26, v2, v7

    const-string v2, "afp"

    const/4 v3, 0x0

    .line 872
    invoke-virtual {v0, v2, v3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v28

    .line 874
    new-instance v2, Lcom/helpshift/configuration/response/RootServerConfig;

    const-string v4, "rne"

    invoke-virtual {v0, v4, v3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v5

    const-string v4, "pfe"

    const/4 v6, 0x1

    .line 875
    invoke-virtual {v0, v4, v6}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v7

    const-string v4, "san"

    .line 876
    invoke-virtual {v0, v4, v6}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v8

    const-string v4, "csat"

    .line 877
    invoke-virtual {v0, v4, v3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v9

    const-string v4, "dia"

    .line 878
    invoke-virtual {v0, v4, v3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v10

    const-string v4, "t"

    .line 879
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v4

    invoke-direct {v1, v4}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseDisableHelpshiftBrandingValue(Lorg/json/JSONObject;)Z

    move-result v11

    const-string v4, "issue_exists"

    .line 880
    invoke-virtual {v0, v4, v6}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v12

    const-string v4, "dbgl"

    const/16 v13, 0x64

    .line 881
    invoke-virtual {v0, v4, v13}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result v14

    const-string v4, "bcl"

    .line 882
    invoke-virtual {v0, v4, v13}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result v13

    const-string v4, "rurl"

    const-string v15, ""

    .line 883
    invoke-virtual {v0, v4, v15}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v15

    const-string v4, "pr"

    .line 884
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v4

    invoke-direct {v1, v4}, Lcom/helpshift/common/platform/AndroidResponseParser;->parsePeriodicReview(Lorg/json/JSONObject;)Lcom/helpshift/configuration/response/PeriodicReview;

    move-result-object v16

    const-string v4, "ic"

    .line 885
    invoke-virtual {v0, v4, v3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v17

    const-string v4, "gm"

    const-string v3, ""

    .line 886
    invoke-virtual {v0, v4, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v4, "tyi"

    .line 887
    invoke-virtual {v0, v4, v6}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v18

    const-string v4, "rq"

    .line 888
    invoke-virtual {v0, v4, v6}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v19

    const-string v4, "conversation_history_enabled"

    const/4 v6, 0x0

    .line 889
    invoke-virtual {v0, v4, v6}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v20

    const-string v4, "allow_user_attachments"

    const/4 v6, 0x1

    .line 892
    invoke-virtual {v0, v4, v6}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v23

    move-object v4, v2

    move v6, v7

    move v7, v8

    move v8, v9

    move v9, v10

    move v10, v11

    move v11, v12

    move v12, v14

    move-object v14, v15

    move-object/from16 v15, v16

    move/from16 v16, v17

    move-object/from16 v17, v3

    invoke-direct/range {v4 .. v28}, Lcom/helpshift/configuration/response/RootServerConfig;-><init>(ZZZZZZZIILjava/lang/String;Lcom/helpshift/configuration/response/PeriodicReview;ZLjava/lang/String;ZZZLjava/lang/Long;Ljava/lang/Long;ZJJZ)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v2

    :catch_0
    move-exception v0

    .line 898
    sget-object v2, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v3, "Parsing exception while fetching config"

    invoke-static {v0, v2, v3}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0
.end method

.method public parseConfirmationAcceptedMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;
    .locals 8

    .line 191
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p1, "created_at"

    .line 192
    invoke-virtual {v0, p1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 193
    invoke-static {v3}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v4

    .line 194
    new-instance p1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;

    const-string v1, "body"

    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v1, "author"

    .line 196
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v6, "name"

    .line 197
    invoke-virtual {v1, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const/4 v7, 0x2

    move-object v1, p1

    invoke-direct/range {v1 .. v7}, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;I)V

    const-string v1, "id"

    .line 199
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;->serverId:Ljava/lang/String;

    const-string v1, "md_state"

    const-string v2, ""

    .line 200
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v1

    iput v1, p1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;->deliveryState:I

    const-string v1, "redacted"

    const/4 v2, 0x0

    .line 201
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v1

    iput-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;->isRedacted:Z

    .line 202
    invoke-direct {p0, p1, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAndSetDataForUserSentMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 206
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading confirmation accepted message"

    .line 207
    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method public parseConfirmationRejectedMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;
    .locals 8

    .line 214
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p1, "created_at"

    .line 215
    invoke-virtual {v0, p1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 216
    invoke-static {v3}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v4

    .line 217
    new-instance p1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;

    const-string v1, "body"

    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v1, "author"

    .line 219
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v6, "name"

    .line 220
    invoke-virtual {v1, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const/4 v7, 0x2

    move-object v1, p1

    invoke-direct/range {v1 .. v7}, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;I)V

    const-string v1, "id"

    .line 222
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;->serverId:Ljava/lang/String;

    const-string v1, "md_state"

    const-string v2, ""

    .line 223
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v1

    iput v1, p1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;->deliveryState:I

    const-string v1, "redacted"

    const/4 v2, 0x0

    .line 224
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v1

    iput-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;->isRedacted:Z

    .line 225
    invoke-direct {p0, p1, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAndSetDataForUserSentMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 229
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading confirmation rejected message"

    .line 230
    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method public parseConversationHistory(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ConversationHistory;
    .locals 4

    .line 263
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 264
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    const-string v1, "issues"

    .line 265
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v1

    const/4 v2, 0x0

    .line 266
    :goto_0
    invoke-virtual {v1}, Lorg/json/JSONArray;->length()I

    move-result v3

    if-ge v2, v3, :cond_0

    .line 267
    invoke-virtual {v1, v2}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v3

    invoke-virtual {v3}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p0, v3}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseReadableConversation(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v3

    invoke-interface {p1, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_0
    const-string v1, "has_older_messages"

    .line 270
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    .line 271
    new-instance v1, Lcom/helpshift/conversation/dto/ConversationHistory;

    invoke-direct {v1, p1, v0}, Lcom/helpshift/conversation/dto/ConversationHistory;-><init>(Ljava/util/List;Z)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v1

    :catch_0
    move-exception p1

    .line 274
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading conversation history"

    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method public parseConversationInbox(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ConversationInbox;
    .locals 4

    .line 237
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 238
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    const-string v1, "issues"

    .line 239
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v1

    const/4 v2, 0x0

    .line 240
    :goto_0
    invoke-virtual {v1}, Lorg/json/JSONArray;->length()I

    move-result v3

    if-ge v2, v3, :cond_0

    .line 241
    invoke-virtual {v1, v2}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v3

    invoke-virtual {v3}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p0, v3}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseReadableConversation(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v3

    invoke-interface {p1, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    const-string v2, "has_older_messages"

    .line 246
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_1

    const-string v1, "has_older_messages"

    .line 247
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v1

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    :cond_1
    const-string v2, "cursor"

    .line 250
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "issue_exists"

    .line 252
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    .line 253
    new-instance v3, Lcom/helpshift/conversation/dto/ConversationInbox;

    invoke-direct {v3, v2, p1, v0, v1}, Lcom/helpshift/conversation/dto/ConversationInbox;-><init>(Ljava/lang/String;Ljava/util/List;ZLjava/lang/Boolean;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v3

    :catch_0
    move-exception p1

    .line 256
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading conversation inbox"

    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method public parseFollowupAcceptedMessage(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;
    .locals 2

    .line 293
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 294
    invoke-direct {p0, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseFollowupAcceptedMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;

    move-result-object p1
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 297
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading follow-up accepted message"

    .line 298
    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method public parseFollowupRejectedMessage(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;
    .locals 2

    .line 281
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 282
    invoke-direct {p0, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseFollowupRejectedMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;

    move-result-object p1
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 285
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading follow-up rejected message"

    .line 286
    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method public parseReadableConversation(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 17

    .line 905
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    move-object/from16 v1, p1

    invoke-direct {v0, v1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v1, "messages"

    .line 906
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v1
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    move-object/from16 v2, p0

    :try_start_1
    invoke-direct {v2, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseMessageDMs(Lorg/json/JSONArray;)Ljava/util/List;

    move-result-object v1

    .line 909
    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v3

    const/4 v4, 0x1

    sub-int/2addr v3, v4

    :goto_0
    if-ltz v3, :cond_1

    .line 910
    invoke-interface {v1, v3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 914
    instance-of v7, v6, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;

    if-nez v7, :cond_0

    instance-of v7, v6, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    if-nez v7, :cond_0

    .line 916
    invoke-virtual {v6}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getCreatedAt()Ljava/lang/String;

    move-result-object v3

    move-object v14, v3

    goto :goto_1

    :cond_0
    add-int/lit8 v3, v3, -0x1

    goto :goto_0

    :cond_1
    const/4 v14, 0x0

    :goto_1
    const-string v3, "state"

    .line 921
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v3

    invoke-static {v3}, Lcom/helpshift/conversation/dto/IssueState;->fromInt(I)Lcom/helpshift/conversation/dto/IssueState;

    move-result-object v8

    const-string v3, "created_at"

    .line 922
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    .line 923
    invoke-static {v9}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v10

    const-string v3, "type"

    .line 924
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 925
    new-instance v15, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    const-string v6, "title"

    const-string v7, ""

    invoke-virtual {v0, v6, v7}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    const-string v6, "updated_at"

    .line 929
    invoke-virtual {v0, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v12

    const-string v6, "publish_id"

    .line 930
    invoke-virtual {v0, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    const-string v6, "show-agent-name"

    .line 932
    invoke-virtual {v0, v6, v4}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v4

    move-object v6, v15

    move-object v5, v15

    move v15, v4

    move-object/from16 v16, v3

    invoke-direct/range {v6 .. v16}, Lcom/helpshift/conversation/activeconversation/model/Conversation;-><init>(Ljava/lang/String;Lcom/helpshift/conversation/dto/IssueState;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;ZLjava/lang/String;)V

    const-string v4, "redacted"

    const/4 v6, 0x0

    .line 934
    invoke-virtual {v0, v4, v6}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v4

    iput-boolean v4, v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isRedacted:Z

    const-string v4, "issue_id"

    .line 935
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->isNull(Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_2

    const/4 v4, 0x0

    goto :goto_2

    :cond_2
    const-string v4, "issue_id"

    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    :goto_2
    iput-object v4, v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    const-string v4, "preissue_id"

    .line 936
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->isNull(Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_3

    const/4 v4, 0x0

    goto :goto_3

    :cond_3
    const-string v4, "preissue_id"

    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    :goto_3
    iput-object v4, v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 937
    iput-object v3, v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    const-string v4, "request_id"

    .line 938
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    iput-object v4, v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;->createdRequestId:Ljava/lang/String;

    const-string v4, "issue"

    .line 940
    invoke-virtual {v4, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_5

    const-string v3, "csat_received"

    .line 943
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_4

    sget-object v0, Lcom/helpshift/conversation/states/ConversationCSATState;->SUBMITTED_SYNCED:Lcom/helpshift/conversation/states/ConversationCSATState;

    goto :goto_4

    :cond_4
    sget-object v0, Lcom/helpshift/conversation/states/ConversationCSATState;->NONE:Lcom/helpshift/conversation/states/ConversationCSATState;

    :goto_4
    iput-object v0, v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatState:Lcom/helpshift/conversation/states/ConversationCSATState;

    .line 945
    :cond_5
    invoke-virtual {v5, v1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setMessageDMs(Ljava/util/List;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    return-object v5

    :catch_0
    move-exception v0

    goto :goto_5

    :catch_1
    move-exception v0

    move-object/from16 v2, p0

    .line 949
    :goto_5
    sget-object v1, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v3, "Parsing exception in reading conversation"

    invoke-static {v0, v1, v3}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0
.end method

.method public parseReadableUserMessage(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;
    .locals 7

    .line 77
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p1, "created_at"

    .line 78
    invoke-virtual {v0, p1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 79
    invoke-static {v3}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v4

    .line 80
    new-instance p1, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    const-string v1, "body"

    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v1, "author"

    .line 82
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v6, "name"

    .line 83
    invoke-virtual {v1, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    move-object v1, p1

    invoke-direct/range {v1 .. v6}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;)V

    const-string v1, "id"

    .line 84
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->serverId:Ljava/lang/String;

    const-string v1, "md_state"

    const-string v2, ""

    .line 85
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v1}, Lcom/helpshift/common/platform/AndroidResponseParser;->convertDeliveryStateToInt(Ljava/lang/String;)I

    move-result v1

    iput v1, p1, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->deliveryState:I

    const-string v1, "redacted"

    const/4 v2, 0x0

    .line 86
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v1

    iput-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->isRedacted:Z

    .line 87
    invoke-direct {p0, p1, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAndSetDataForUserSentMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 91
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading user text message"

    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method public parseResponseMessageForOptionInput(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;
    .locals 17

    .line 712
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    move-object/from16 v1, p1

    invoke-direct {v0, v1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v1, "type"

    .line 713
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const/4 v2, -0x1

    .line 717
    invoke-virtual {v1}, Ljava/lang/String;->hashCode()I

    move-result v3

    const v4, -0x2732e90d

    const/4 v5, 0x0

    if-eq v3, v4, :cond_1

    const v4, 0x6cd7e29c

    if-eq v3, v4, :cond_0

    goto :goto_0

    :cond_0
    const-string v3, "rsp_txt_msg_with_option_input"

    invoke-virtual {v1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_2

    const/4 v2, 0x0

    goto :goto_0

    :cond_1
    const-string v3, "rsp_faq_list_msg_with_option_input"

    invoke-virtual {v1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_2

    const/4 v2, 0x1

    :cond_2
    :goto_0
    packed-switch v2, :pswitch_data_0

    move-object/from16 v2, p0

    const/4 v0, 0x0

    return-object v0

    .line 722
    :pswitch_0
    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->FAQ_LIST_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    goto :goto_1

    .line 719
    :pswitch_1
    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_TEXT_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    :goto_1
    move-object/from16 v16, v1

    const-string v1, "skipped"

    .line 728
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v13

    if-eqz v13, :cond_3

    const-string v1, "{}"

    :goto_2
    move-object v14, v1

    goto :goto_3

    :cond_3
    const-string v1, "option_data"

    .line 729
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    invoke-virtual {v1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v1

    goto :goto_2

    :goto_3
    const-string v1, "created_at"

    .line 730
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    .line 731
    invoke-static {v8}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v9

    .line 732
    new-instance v1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;

    const-string v2, "body"

    .line 733
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    const-string v2, "author"

    .line 735
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    const-string v3, "name"

    invoke-virtual {v2, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v11

    const-string v2, "chatbot_info"

    .line 736
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    invoke-virtual {v2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v12

    const-string v2, "meta"

    .line 739
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    const-string v3, "refers"

    invoke-virtual {v2, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v15

    move-object v6, v1

    invoke-direct/range {v6 .. v16}, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    const-string v2, "id"

    .line 741
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    iput-object v2, v1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->serverId:Ljava/lang/String;

    const-string v2, "redacted"

    .line 742
    invoke-virtual {v0, v2, v5}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v2

    iput-boolean v2, v1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->isRedacted:Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    move-object/from16 v2, p0

    .line 743
    :try_start_1
    invoke-direct {v2, v1, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAndSetDataForUserSentMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    return-object v1

    :catch_0
    move-exception v0

    goto :goto_4

    :catch_1
    move-exception v0

    move-object/from16 v2, p0

    .line 747
    :goto_4
    sget-object v1, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v3, "Parsing exception while reading user response for option input"

    invoke-static {v0, v1, v3}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public parseResponseMessageForTextInput(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;
    .locals 20

    .line 98
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    move-object/from16 v1, p1

    invoke-direct {v0, v1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v1, "type"

    .line 99
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const/4 v2, -0x1

    .line 103
    invoke-virtual {v1}, Ljava/lang/String;->hashCode()I

    move-result v3

    const/4 v4, 0x3

    const/4 v5, 0x2

    const/4 v6, 0x4

    const/4 v7, 0x0

    const/4 v8, 0x1

    sparse-switch v3, :sswitch_data_0

    goto :goto_0

    :sswitch_0
    const-string v3, "rsp_txt_msg_with_dt_input"

    invoke-virtual {v1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v2, 0x4

    goto :goto_0

    :sswitch_1
    const-string v3, "rsp_empty_msg_with_txt_input"

    invoke-virtual {v1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v2, 0x0

    goto :goto_0

    :sswitch_2
    const-string v3, "rsp_txt_msg_with_txt_input"

    invoke-virtual {v1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v2, 0x1

    goto :goto_0

    :sswitch_3
    const-string v3, "rsp_txt_msg_with_numeric_input"

    invoke-virtual {v1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v2, 0x3

    goto :goto_0

    :sswitch_4
    const-string v3, "rsp_txt_msg_with_email_input"

    invoke-virtual {v1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v2, 0x2

    :cond_0
    :goto_0
    packed-switch v2, :pswitch_data_0

    move-object/from16 v1, p0

    const/4 v0, 0x0

    return-object v0

    :pswitch_0
    const/4 v4, 0x4

    goto :goto_1

    :pswitch_1
    const/4 v4, 0x2

    goto :goto_1

    :pswitch_2
    const/4 v4, 0x1

    :goto_1
    :pswitch_3
    const/16 v19, 0x0

    goto :goto_2

    :pswitch_4
    const/4 v4, 0x1

    const/16 v19, 0x1

    :goto_2
    if-nez v19, :cond_1

    const-string v1, "skipped"

    .line 123
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_1

    goto :goto_3

    :cond_1
    const/4 v8, 0x0

    :goto_3
    const-string v1, "meta"

    .line 124
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    const-string v2, "created_at"

    .line 125
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v11

    .line 126
    invoke-static {v11}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v12

    .line 127
    new-instance v2, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;

    const-string v3, "body"

    .line 128
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v10

    const-string v3, "author"

    .line 130
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v3

    const-string v5, "name"

    invoke-virtual {v3, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v14

    const-string v3, "chatbot_info"

    .line 132
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v3

    invoke-virtual {v3}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v16

    const-string v3, "refers"

    .line 134
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v18

    move-object v9, v2

    move v15, v4

    move/from16 v17, v8

    invoke-direct/range {v9 .. v19}, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ILjava/lang/String;ZLjava/lang/String;Z)V

    if-ne v4, v6, :cond_2

    if-nez v8, :cond_2

    const-string v3, "dt"

    .line 138
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->getLong(Ljava/lang/String;)J

    move-result-wide v3

    iput-wide v3, v2, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->dateInMillis:J

    const-string v3, "timezone"

    .line 139
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    iput-object v1, v2, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->timeZoneId:Ljava/lang/String;

    :cond_2
    const-string v1, "id"

    .line 142
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    iput-object v1, v2, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->serverId:Ljava/lang/String;

    const-string v1, "redacted"

    .line 143
    invoke-virtual {v0, v1, v7}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v1

    iput-boolean v1, v2, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->isRedacted:Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    move-object/from16 v1, p0

    .line 144
    :try_start_1
    invoke-direct {v1, v2, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseAndSetDataForUserSentMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lorg/json/JSONObject;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    return-object v2

    :catch_0
    move-exception v0

    goto :goto_4

    :catch_1
    move-exception v0

    move-object/from16 v1, p0

    .line 148
    :goto_4
    sget-object v2, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v3, "Parsing exception while reading user response for text input"

    invoke-static {v0, v2, v3}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0

    nop

    :sswitch_data_0
    .sparse-switch
        -0x318c7d35 -> :sswitch_4
        -0x5a48f84 -> :sswitch_3
        0x1d6c939f -> :sswitch_2
        0x36c765a2 -> :sswitch_1
        0x7b7c9477 -> :sswitch_0
    .end sparse-switch

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_4
        :pswitch_2
        :pswitch_1
        :pswitch_3
        :pswitch_0
    .end packed-switch
.end method

.method public parseScreenshotMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;
    .locals 2

    .line 156
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 157
    invoke-direct {p0, v0}, Lcom/helpshift/common/platform/AndroidResponseParser;->parseScreenshotMessageDM(Lorg/json/JSONObject;)Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    move-result-object p1
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 160
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading reopen message"

    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method public parseSingleFAQ(Ljava/lang/String;)Lcom/helpshift/faq/FaqCore;
    .locals 12

    .line 956
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 957
    new-instance p1, Lcom/helpshift/faq/FaqCore;

    const-string v1, "id"

    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v1, "publish_id"

    .line 958
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v1, "language"

    .line 959
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v1, "section_id"

    .line 960
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v1, "title"

    .line 961
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v1, "body"

    .line 962
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    const/4 v8, 0x0

    const-string v1, "is_rtl"

    .line 964
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v9, "true"

    invoke-virtual {v1, v9}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v9

    const-string v1, "stags"

    .line 965
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    const-string v1, "stags"

    .line 966
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/helpshift/util/HSJSONUtils;->jsonToStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v1

    goto :goto_0

    :cond_0
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    :goto_0
    move-object v10, v1

    const-string v1, "issue_tags"

    .line 967
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_1

    const-string v1, "issue_tags"

    .line 968
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 967
    invoke-static {v0}, Lcom/helpshift/util/HSJSONUtils;->jsonToStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v0

    goto :goto_1

    :cond_1
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    :goto_1
    move-object v11, v0

    move-object v1, p1

    invoke-direct/range {v1 .. v11}, Lcom/helpshift/faq/FaqCore;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILjava/lang/Boolean;Ljava/util/List;Ljava/util/List;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 971
    sget-object v0, Lcom/helpshift/common/exception/ParseException;->GENERIC:Lcom/helpshift/common/exception/ParseException;

    const-string v1, "Parsing exception while reading single faq"

    invoke-static {p1, v0, v1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method public parseWebSocketMessage(Ljava/lang/String;)Lcom/helpshift/conversation/dto/WebSocketMessage;
    .locals 7

    const/4 v0, 0x0

    .line 328
    :try_start_0
    new-instance v1, Lorg/json/JSONArray;

    invoke-direct {v1, p1}, Lorg/json/JSONArray;-><init>(Ljava/lang/String;)V

    const/4 p1, 0x0

    .line 329
    invoke-virtual {v1, p1}, Lorg/json/JSONArray;->getInt(I)I

    move-result v2

    const/16 v3, 0x64

    const/4 v4, 0x1

    if-eq v2, v3, :cond_1

    const/16 p1, 0x6b

    if-eq v2, p1, :cond_0

    move-object p1, v0

    goto/16 :goto_3

    .line 332
    :cond_0
    sget-object p1, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-virtual {v1, v4}, Lorg/json/JSONArray;->getLong(I)J

    move-result-wide v1

    invoke-virtual {p1, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v1

    .line 333
    new-instance p1, Lcom/helpshift/conversation/dto/WSPingMessage;

    invoke-direct {p1, v1, v2}, Lcom/helpshift/conversation/dto/WSPingMessage;-><init>(J)V

    goto :goto_3

    :cond_1
    const/4 v2, 0x2

    .line 336
    invoke-virtual {v1, v2}, Lorg/json/JSONArray;->getJSONArray(I)Lorg/json/JSONArray;

    move-result-object v1
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    move-object v2, v0

    const/4 v0, 0x0

    .line 338
    :goto_0
    :try_start_1
    invoke-virtual {v1}, Lorg/json/JSONArray;->length()I

    move-result v3

    if-ge v0, v3, :cond_4

    .line 339
    invoke-virtual {v1, v0}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v3

    .line 340
    new-instance v5, Lorg/json/JSONObject;

    const-string v6, "m"

    invoke-virtual {v3, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-direct {v5, v3}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v3, "stream"

    .line 341
    invoke-virtual {v5, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v6, "agent_type_activity"

    .line 342
    invoke-virtual {v6, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_3

    const-string v3, "action"

    .line 343
    invoke-virtual {v5, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v6, "start"

    .line 344
    invoke-virtual {v6, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_2

    .line 345
    sget-object v3, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-string v6, "ttl"

    invoke-virtual {v5, v6}, Lorg/json/JSONObject;->getLong(Ljava/lang/String;)J

    move-result-wide v5

    invoke-virtual {v3, v5, v6}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v5

    .line 346
    new-instance v3, Lcom/helpshift/conversation/dto/WSTypingActionMessage;

    invoke-direct {v3, v4, v5, v6}, Lcom/helpshift/conversation/dto/WSTypingActionMessage;-><init>(ZJ)V

    goto :goto_1

    :cond_2
    const-string v5, "stop"

    .line 348
    invoke-virtual {v5, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_3

    .line 349
    new-instance v3, Lcom/helpshift/conversation/dto/WSTypingActionMessage;

    const-wide/16 v5, 0x0

    invoke-direct {v3, p1, v5, v6}, Lcom/helpshift/conversation/dto/WSTypingActionMessage;-><init>(ZJ)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    :goto_1
    move-object v2, v3

    :cond_3
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :catch_0
    move-exception p1

    goto :goto_2

    :catch_1
    move-exception p1

    move-object v2, v0

    :goto_2
    const-string v0, "Helpshift_AResponseParser"

    const-string v1, "Exception in parsing web-socket message"

    .line 357
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_4
    move-object p1, v2

    :goto_3
    return-object p1
.end method
