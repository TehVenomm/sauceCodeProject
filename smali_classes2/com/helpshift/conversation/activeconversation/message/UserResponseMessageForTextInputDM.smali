.class public Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;
.super Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;
.source "UserResponseMessageForTextInputDM.java"


# instance fields
.field public botInfo:Ljava/lang/String;

.field public dateInMillis:J

.field public final isMessageEmpty:Z

.field public keyboard:I

.field private referredMessageId:Ljava/lang/String;

.field public skipped:Z

.field public timeZoneId:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ILjava/lang/String;ZLjava/lang/String;Z)V
    .locals 8

    move-object v7, p0

    .line 34
    sget-object v6, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_RESP_FOR_TEXT_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-wide v3, p3

    move-object v5, p5

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    move v0, p6

    .line 35
    iput v0, v7, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->keyboard:I

    move-object v0, p7

    .line 36
    iput-object v0, v7, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->botInfo:Ljava/lang/String;

    move/from16 v0, p8

    .line 37
    iput-boolean v0, v7, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->skipped:Z

    move-object/from16 v0, p9

    .line 38
    iput-object v0, v7, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->referredMessageId:Ljava/lang/String;

    move/from16 v0, p10

    .line 39
    iput-boolean v0, v7, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->isMessageEmpty:Z

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;Z)V
    .locals 7

    .line 44
    sget-object v6, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_RESP_FOR_TEXT_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-wide v3, p3

    move-object v5, p5

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 45
    iget-object p1, p6, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/TextInput;

    iget p1, p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->keyboard:I

    iput p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->keyboard:I

    .line 46
    iget-object p1, p6, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/TextInput;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->botInfo:Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->botInfo:Ljava/lang/String;

    .line 47
    iput-boolean p7, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->skipped:Z

    .line 48
    iget-object p1, p6, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->serverId:Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->referredMessageId:Ljava/lang/String;

    .line 49
    iget-boolean p1, p6, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->isMessageEmpty:Z

    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->isMessageEmpty:Z

    return-void
.end method


# virtual methods
.method protected getData()Ljava/util/Map;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/text/ParseException;
        }
    .end annotation

    .line 95
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "chatbot_info"

    .line 96
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->botInfo:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "skipped"

    .line 97
    iget-boolean v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->skipped:Z

    invoke-static {v2}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 101
    iget v1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->keyboard:I

    const/4 v2, 0x4

    if-ne v1, v2, :cond_0

    iget-boolean v1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->skipped:Z

    if-nez v1, :cond_0

    .line 102
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getCurrentLocale()Ljava/util/Locale;

    move-result-object v1

    const-string v2, "EEEE, MMMM dd, yyyy"

    .line 103
    invoke-static {v2, v1}, Lcom/helpshift/common/util/HSDateFormatSpec;->getDateFormatter(Ljava/lang/String;Ljava/util/Locale;)Lcom/helpshift/common/util/HSSimpleDateFormat;

    move-result-object v1

    .line 104
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->body:Ljava/lang/String;

    invoke-virtual {v2}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Lcom/helpshift/common/util/HSSimpleDateFormat;->parse(Ljava/lang/String;)Ljava/util/Date;

    move-result-object v1

    .line 106
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    .line 107
    invoke-virtual {v1}, Ljava/util/Date;->getTime()J

    move-result-wide v3

    iput-wide v3, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->dateInMillis:J

    .line 108
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/common/platform/Device;->getTimeZoneId()Ljava/lang/String;

    move-result-object v1

    iput-object v1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->timeZoneId:Ljava/lang/String;

    const-string v1, "dt"

    .line 109
    iget-wide v3, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->dateInMillis:J

    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    invoke-interface {v2, v1, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "timezone"

    .line 110
    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->timeZoneId:Ljava/lang/String;

    invoke-interface {v2, v1, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 112
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getJsonifier()Lcom/helpshift/common/platform/Jsonifier;

    move-result-object v1

    invoke-interface {v1, v2}, Lcom/helpshift/common/platform/Jsonifier;->jsonify(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "message_meta"

    .line 113
    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    return-object v0
.end method

.method protected getMessageTypeForRequest()Ljava/lang/String;
    .locals 1

    .line 69
    iget v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->keyboard:I

    packed-switch v0, :pswitch_data_0

    .line 84
    invoke-super {p0}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->getMessageTypeForRequest()Ljava/lang/String;

    move-result-object v0

    return-object v0

    :pswitch_0
    const-string v0, "rsp_txt_msg_with_dt_input"

    return-object v0

    :pswitch_1
    const-string v0, "rsp_txt_msg_with_numeric_input"

    return-object v0

    :pswitch_2
    const-string v0, "rsp_txt_msg_with_email_input"

    return-object v0

    .line 71
    :pswitch_3
    iget-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->isMessageEmpty:Z

    if-eqz v0, :cond_0

    const-string v0, "rsp_empty_msg_with_txt_input"

    return-object v0

    :cond_0
    const-string v0, "rsp_txt_msg_with_txt_input"

    return-object v0

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public getReferredMessageId()Ljava/lang/String;
    .locals 1

    .line 90
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->referredMessageId:Ljava/lang/String;

    return-object v0
.end method

.method public merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 2

    .line 54
    invoke-super {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 55
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;

    if-eqz v0, :cond_0

    .line 56
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;

    .line 57
    iget v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->keyboard:I

    iput v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->keyboard:I

    .line 58
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->botInfo:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->botInfo:Ljava/lang/String;

    .line 59
    iget-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->skipped:Z

    iput-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->skipped:Z

    .line 60
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->referredMessageId:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->referredMessageId:Ljava/lang/String;

    .line 61
    iget-wide v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->dateInMillis:J

    iput-wide v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->dateInMillis:J

    .line 62
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->timeZoneId:Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->timeZoneId:Ljava/lang/String;

    :cond_0
    return-void
.end method

.method protected parseResponse(Lcom/helpshift/common/platform/network/Response;)Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;
    .locals 1

    .line 121
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;

    move-result-object v0

    .line 122
    iget-object p1, p1, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    invoke-interface {v0, p1}, Lcom/helpshift/common/platform/network/ResponseParser;->parseResponseMessageForTextInput(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;

    move-result-object p1

    return-object p1
.end method
