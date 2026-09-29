.class public Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;
.super Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;
.source "UserResponseMessageForOptionInput.java"


# instance fields
.field public botInfo:Ljava/lang/String;

.field public optionData:Ljava/lang/String;

.field private referredMessageId:Ljava/lang/String;

.field public referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

.field public skipped:Z


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Z)V
    .locals 7

    .line 38
    sget-object v6, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_RESP_FOR_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-wide v3, p3

    move-object v5, p5

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 39
    iget-object p1, p6, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->botInfo:Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->botInfo:Ljava/lang/String;

    .line 40
    iput-boolean p7, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->skipped:Z

    .line 41
    iget-object p1, p6, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->options:Ljava/util/List;

    invoke-direct {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->getSelectedOptionData(Ljava/util/List;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->optionData:Ljava/lang/String;

    .line 42
    iget-object p1, p6, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->serverId:Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageId:Ljava/lang/String;

    .line 43
    iget-object p1, p6, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V
    .locals 8

    move-object v7, p0

    .line 28
    sget-object v6, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_RESP_FOR_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-wide v3, p3

    move-object v5, p5

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    move-object v0, p6

    .line 29
    iput-object v0, v7, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->botInfo:Ljava/lang/String;

    move v0, p7

    .line 30
    iput-boolean v0, v7, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->skipped:Z

    move-object/from16 v0, p8

    .line 31
    iput-object v0, v7, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->optionData:Ljava/lang/String;

    move-object/from16 v0, p9

    .line 32
    iput-object v0, v7, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageId:Ljava/lang/String;

    move-object/from16 v0, p10

    .line 33
    iput-object v0, v7, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    return-void
.end method

.method private getSelectedOptionData(Ljava/util/List;)Ljava/lang/String;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .line 98
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    .line 99
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->title:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->body:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 100
    iget-object p1, v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->jsonData:Ljava/lang/String;

    return-object p1

    :cond_1
    const-string p1, "{}"

    return-object p1
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

    .line 61
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "chatbot_info"

    .line 62
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->botInfo:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "skipped"

    .line 63
    iget-boolean v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->skipped:Z

    invoke-static {v2}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 64
    iget-boolean v1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->skipped:Z

    if-nez v1, :cond_0

    const-string v1, "option_data"

    .line 65
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->optionData:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 67
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v2, Lcom/helpshift/conversation/activeconversation/message/MessageType;->FAQ_LIST_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v1, v2, :cond_2

    .line 68
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 69
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->platform:Lcom/helpshift/common/platform/Platform;

    .line 70
    invoke-interface {v2}, Lcom/helpshift/common/platform/Platform;->getKVStore()Lcom/helpshift/common/platform/KVStore;

    move-result-object v2

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "read_faq_"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v4, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageId:Ljava/lang/String;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v2, v3}, Lcom/helpshift/common/platform/KVStore;->getSerializable(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v2

    .line 71
    instance-of v3, v2, Ljava/util/ArrayList;

    if-eqz v3, :cond_1

    .line 72
    move-object v1, v2

    check-cast v1, Ljava/util/List;

    :cond_1
    const-string v2, "read_faqs"

    .line 74
    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v3}, Lcom/helpshift/common/platform/Platform;->getJsonifier()Lcom/helpshift/common/platform/Jsonifier;

    move-result-object v3

    invoke-interface {v3, v1}, Lcom/helpshift/common/platform/Jsonifier;->jsonifyListToJsonArray(Ljava/util/List;)Ljava/lang/Object;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_2
    return-object v0
.end method

.method protected getMessageTypeForRequest()Ljava/lang/String;
    .locals 2

    .line 82
    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput$1;->$SwitchMap$com$helpshift$conversation$activeconversation$message$MessageType:[I

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    .line 88
    invoke-super {p0}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->getMessageTypeForRequest()Ljava/lang/String;

    move-result-object v0

    return-object v0

    :pswitch_0
    const-string v0, "rsp_faq_list_msg_with_option_input"

    return-object v0

    :pswitch_1
    const-string v0, "rsp_txt_msg_with_option_input"

    return-object v0

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public getReferredMessageId()Ljava/lang/String;
    .locals 1

    .line 94
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageId:Ljava/lang/String;

    return-object v0
.end method

.method public merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 48
    invoke-super {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 49
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;

    if-eqz v0, :cond_0

    .line 50
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;

    .line 51
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->botInfo:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->botInfo:Ljava/lang/String;

    .line 52
    iget-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->skipped:Z

    iput-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->skipped:Z

    .line 53
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->optionData:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->optionData:Ljava/lang/String;

    .line 54
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageId:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageId:Ljava/lang/String;

    .line 55
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    :cond_0
    return-void
.end method

.method protected parseResponse(Lcom/helpshift/common/platform/network/Response;)Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;
    .locals 1

    .line 108
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;

    move-result-object v0

    .line 109
    iget-object p1, p1, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    invoke-interface {v0, p1}, Lcom/helpshift/common/platform/network/ResponseParser;->parseResponseMessageForOptionInput(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;

    move-result-object p1

    return-object p1
.end method
