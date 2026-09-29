.class public Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/ImageAttachmentMessageDM;
.source "ScreenshotMessageDM.java"


# instance fields
.field public refersMessageId:Ljava/lang/String;

.field public state:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;IZ)V
    .locals 14

    .line 44
    sget-object v13, Lcom/helpshift/conversation/activeconversation/message/MessageType;->SCREENSHOT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    const/4 v11, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object/from16 v2, p2

    move-wide/from16 v3, p3

    move-object/from16 v5, p5

    move-object/from16 v6, p9

    move-object/from16 v7, p8

    move-object/from16 v8, p7

    move-object/from16 v9, p6

    move/from16 v10, p10

    move/from16 v12, p11

    invoke-direct/range {v0 .. v13}, Lcom/helpshift/conversation/activeconversation/message/ImageAttachmentMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;IZZLcom/helpshift/conversation/activeconversation/message/MessageType;)V

    return-void
.end method

.method static synthetic access$003(Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    .line 36
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/ImageAttachmentMessageDM;->filePath:Ljava/lang/String;

    return-object p1
.end method


# virtual methods
.method public checkAndReDownloadImageIfNotExist(Lcom/helpshift/common/platform/Platform;)V
    .locals 6

    .line 160
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->state:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENT:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    if-ne v0, v1, :cond_0

    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->getFilePath()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/common/util/FileUtil;->doesFilePathExistAndCanRead(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 161
    new-instance v0, Lcom/helpshift/downloader/AdminFileInfo;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->attachmentUrl:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->fileName:Ljava/lang/String;

    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->contentType:Ljava/lang/String;

    iget-boolean v4, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->isSecureAttachment:Z

    invoke-direct {v0, v1, v2, v3, v4}, Lcom/helpshift/downloader/AdminFileInfo;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)V

    .line 162
    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getDownloader()Lcom/helpshift/downloader/SupportDownloader;

    move-result-object v1

    sget-object v2, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;->INTERNAL_ONLY:Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    new-instance v3, Lcom/helpshift/common/domain/network/AuthDataProvider;

    iget-object v4, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v5, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->attachmentUrl:Ljava/lang/String;

    invoke-direct {v3, v4, p1, v5}, Lcom/helpshift/common/domain/network/AuthDataProvider;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;Ljava/lang/String;)V

    new-instance v4, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM$1;

    invoke-direct {v4, p0, p1}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM$1;-><init>(Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;Lcom/helpshift/common/platform/Platform;)V

    .line 163
    invoke-interface {v1, v0, v2, v3, v4}, Lcom/helpshift/downloader/SupportDownloader;->startDownload(Lcom/helpshift/downloader/AdminFileInfo;Lcom/helpshift/downloader/SupportDownloader$StorageDirType;Lcom/helpshift/common/domain/network/AuthDataProvider;Lcom/helpshift/downloader/SupportDownloadStateChangeListener;)V

    :cond_0
    return-void
.end method

.method public getFilePath()Ljava/lang/String;
    .locals 1

    .line 153
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->filePath:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/util/FileUtil;->doesFilePathExistAndCanRead(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    .line 154
    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->filePath:Ljava/lang/String;

    .line 156
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->filePath:Ljava/lang/String;

    return-object v0
.end method

.method public handleClick(Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;)V
    .locals 2

    .line 56
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->state:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENT:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    if-ne v0, v1, :cond_0

    if-eqz p1, :cond_0

    .line 57
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->getFilePath()Ljava/lang/String;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->contentType:Ljava/lang/String;

    invoke-interface {p1, v0, v1}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->launchScreenshotAttachment(Ljava/lang/String;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public isUISupportedMessage()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method

.method public setRefersMessageId(Ljava/lang/String;)V
    .locals 1

    if-nez p1, :cond_0

    .line 50
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "localRscMessage_"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 52
    :cond_0
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->refersMessageId:Ljava/lang/String;

    return-void
.end method

.method public setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V
    .locals 0

    .line 131
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->state:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    .line 132
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->notifyUpdated()V

    return-void
.end method

.method public updateState(Z)V
    .locals 2

    .line 136
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->serverId:Ljava/lang/String;

    if-nez v0, :cond_2

    .line 137
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->state:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENDING:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    if-ne v0, v1, :cond_0

    return-void

    :cond_0
    if-eqz p1, :cond_1

    .line 141
    sget-object p1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->UNSENT_RETRYABLE:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    goto :goto_0

    .line 144
    :cond_1
    sget-object p1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->UNSENT_NOT_RETRYABLE:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    goto :goto_0

    .line 148
    :cond_2
    sget-object p1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENT:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    :goto_0
    return-void
.end method

.method public uploadImage(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;Z)V
    .locals 9

    .line 64
    invoke-interface {p2}, Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;->getIssueId()Ljava/lang/String;

    move-result-object v0

    .line 65
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_5

    .line 69
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->getFilePath()Ljava/lang/String;

    move-result-object v1

    if-nez v1, :cond_0

    return-void

    :cond_0
    if-eqz p3, :cond_1

    .line 74
    iget-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->getFilePath()Ljava/lang/String;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->refersMessageId:Ljava/lang/String;

    invoke-interface {p3, v1, v2}, Lcom/helpshift/common/platform/Platform;->compressAndStoreScreenshot(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p3

    iput-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->filePath:Ljava/lang/String;

    .line 75
    iget-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p3}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p3

    invoke-interface {p3, p0}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 77
    :cond_1
    sget-object p3, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENDING:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {p0, p3}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    .line 81
    invoke-static {p1}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object p3

    const-string v1, "body"

    const-string v2, "Screenshot sent"

    .line 82
    invoke-interface {p3, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "type"

    const-string v2, "sc"

    .line 83
    invoke-interface {p3, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "refers"

    .line 84
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->refersMessageId:Ljava/lang/String;

    invoke-interface {p3, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "screenshot"

    .line 85
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->getFilePath()Ljava/lang/String;

    move-result-object v2

    invoke-interface {p3, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "originalFileName"

    .line 86
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->fileName:Ljava/lang/String;

    invoke-interface {p3, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 89
    :try_start_0
    invoke-virtual {p0, p2}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->getIssueSendMessageRoute(Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)Ljava/lang/String;

    move-result-object v7

    .line 90
    new-instance v4, Lcom/helpshift/common/domain/network/UploadNetwork;

    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v4, v7, p2, v1}, Lcom/helpshift/common/domain/network/UploadNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 91
    new-instance p2, Lcom/helpshift/common/domain/network/IdempotentNetwork;

    iget-object v5, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->getIdempotentPolicy()Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;

    move-result-object v6

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->localId:Ljava/lang/Long;

    invoke-static {v1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v8

    move-object v3, p2

    invoke-direct/range {v3 .. v8}, Lcom/helpshift/common/domain/network/IdempotentNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;Ljava/lang/String;Ljava/lang/String;)V

    .line 92
    new-instance v1, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;

    invoke-direct {v1, p2}, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 93
    new-instance p2, Lcom/helpshift/common/domain/network/GuardAgainstConversationArchivalNetwork;

    invoke-direct {p2, v1}, Lcom/helpshift/common/domain/network/GuardAgainstConversationArchivalNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 94
    new-instance v1, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {v1, p3}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    .line 95
    new-instance p3, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {p3, p2}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 96
    invoke-virtual {p3, v1}, Lcom/helpshift/common/domain/network/GuardOKNetwork;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p2

    .line 98
    iget-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 99
    invoke-interface {p3}, Lcom/helpshift/common/platform/Platform;->getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;

    move-result-object p3

    iget-object p2, p2, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    invoke-interface {p3, p2}, Lcom/helpshift/common/platform/network/ResponseParser;->parseScreenshotMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    move-result-object p2

    .line 100
    iget-object p3, p2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->serverId:Ljava/lang/String;

    iput-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->serverId:Ljava/lang/String;

    .line 101
    iget-object p3, p2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->authorId:Ljava/lang/String;

    iput-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->authorId:Ljava/lang/String;

    .line 102
    invoke-virtual {p0, p2}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 103
    sget-object p3, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENT:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {p0, p3}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    .line 104
    iget-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p3}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p3

    invoke-interface {p3, p0}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 105
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->notifyUpdated()V

    .line 108
    new-instance p3, Ljava/util/HashMap;

    invoke-direct {p3}, Ljava/util/HashMap;-><init>()V

    const-string v1, "id"

    .line 109
    invoke-interface {p3, v1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "body"

    .line 110
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->attachmentUrl:Ljava/lang/String;

    invoke-interface {p3, v0, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "type"

    const-string v0, "url"

    .line 111
    invoke-interface {p3, p2, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 112
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object p2

    sget-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->MESSAGE_ADDED:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {p2, v0, p3}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V

    .line 113
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object p2

    const-string p3, "User sent a screenshot"

    invoke-virtual {p2, p3}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->userRepliedToConversation(Ljava/lang/String;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p2

    .line 116
    iget-object p3, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v0, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-eq p3, v0, :cond_2

    iget-object p3, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v0, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-ne p3, v0, :cond_3

    .line 118
    :cond_2
    iget-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p3}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object p3

    iget-object v0, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-virtual {p3, p1, v0}, Lcom/helpshift/account/AuthenticationFailureDM;->notifyAuthenticationFailure(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/common/exception/ExceptionType;)V

    .line 123
    :cond_3
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->serverId:Ljava/lang/String;

    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_4

    .line 124
    sget-object p1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->UNSENT_RETRYABLE:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    .line 126
    :cond_4
    invoke-static {p2}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    .line 66
    :cond_5
    new-instance p1, Ljava/lang/UnsupportedOperationException;

    const-string p2, "ScreenshotMessageDM send called with conversation in pre issue mode."

    invoke-direct {p1, p2}, Ljava/lang/UnsupportedOperationException;-><init>(Ljava/lang/String;)V

    throw p1
.end method
