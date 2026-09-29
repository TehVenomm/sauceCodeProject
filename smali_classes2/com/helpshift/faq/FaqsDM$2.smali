.class Lcom/helpshift/faq/FaqsDM$2;
.super Lcom/helpshift/common/domain/F;
.source "FaqsDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/faq/FaqsDM;->fetchQuestion(Lcom/helpshift/common/FetchDataFromThread;Ljava/lang/String;Ljava/lang/String;Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/faq/FaqsDM;

.field final synthetic val$callback:Lcom/helpshift/common/FetchDataFromThread;

.field final synthetic val$faqLanguage:Ljava/lang/String;

.field final synthetic val$isAdminSuggested:Z

.field final synthetic val$publishId:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/faq/FaqsDM;Ljava/lang/String;ZLjava/lang/String;Lcom/helpshift/common/FetchDataFromThread;)V
    .locals 0

    .line 84
    iput-object p1, p0, Lcom/helpshift/faq/FaqsDM$2;->this$0:Lcom/helpshift/faq/FaqsDM;

    iput-object p2, p0, Lcom/helpshift/faq/FaqsDM$2;->val$faqLanguage:Ljava/lang/String;

    iput-boolean p3, p0, Lcom/helpshift/faq/FaqsDM$2;->val$isAdminSuggested:Z

    iput-object p4, p0, Lcom/helpshift/faq/FaqsDM$2;->val$publishId:Ljava/lang/String;

    iput-object p5, p0, Lcom/helpshift/faq/FaqsDM$2;->val$callback:Lcom/helpshift/common/FetchDataFromThread;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 88
    :try_start_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 91
    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM$2;->val$faqLanguage:Ljava/lang/String;

    .line 92
    iget-boolean v2, p0, Lcom/helpshift/faq/FaqsDM$2;->val$isAdminSuggested:Z

    if-eqz v2, :cond_0

    const/4 v2, 0x1

    goto :goto_0

    .line 96
    :cond_0
    iget-object v2, p0, Lcom/helpshift/faq/FaqsDM$2;->this$0:Lcom/helpshift/faq/FaqsDM;

    iget-object v2, v2, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v2}, Lcom/helpshift/common/domain/Domain;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object v2

    const-string v3, "defaultFallbackLanguageEnable"

    invoke-virtual {v2, v3}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v2

    :goto_0
    const-string v3, "edfl"

    .line 98
    invoke-static {v2}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v3, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 99
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "/faqs/"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lcom/helpshift/faq/FaqsDM$2;->val$publishId:Ljava/lang/String;

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "/"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 101
    new-instance v3, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {v3, v0}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    .line 102
    iget-object v0, p0, Lcom/helpshift/faq/FaqsDM$2;->this$0:Lcom/helpshift/faq/FaqsDM;

    invoke-virtual {v0, v3, v1}, Lcom/helpshift/faq/FaqsDM;->setFaqsCustomHeaders(Lcom/helpshift/common/platform/network/RequestData;Ljava/lang/String;)V

    .line 104
    iget-object v0, p0, Lcom/helpshift/faq/FaqsDM$2;->this$0:Lcom/helpshift/faq/FaqsDM;

    invoke-virtual {v0, v2}, Lcom/helpshift/faq/FaqsDM;->getFaqsNetwork(Ljava/lang/String;)Lcom/helpshift/common/domain/network/Network;

    move-result-object v0

    .line 106
    invoke-interface {v0, v3}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object v0

    .line 107
    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM$2;->this$0:Lcom/helpshift/faq/FaqsDM;

    iget-object v1, v1, Lcom/helpshift/faq/FaqsDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;

    move-result-object v1

    iget-object v0, v0, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    invoke-interface {v1, v0}, Lcom/helpshift/common/platform/network/ResponseParser;->parseSingleFAQ(Ljava/lang/String;)Lcom/helpshift/faq/FaqCore;

    move-result-object v0

    .line 108
    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM$2;->val$callback:Lcom/helpshift/common/FetchDataFromThread;

    invoke-interface {v1, v0}, Lcom/helpshift/common/FetchDataFromThread;->onDataFetched(Ljava/lang/Object;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v0

    .line 112
    iget-object v1, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v2, Lcom/helpshift/common/exception/NetworkException;->CONTENT_UNCHANGED:Lcom/helpshift/common/exception/NetworkException;

    if-eq v1, v2, :cond_4

    .line 113
    invoke-virtual {v0}, Lcom/helpshift/common/exception/RootAPIException;->getServerStatusCode()I

    move-result v0

    .line 114
    sget-object v1, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->FORBIDDEN_ACCESS:Ljava/lang/Integer;

    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v1

    if-eq v0, v1, :cond_1

    sget-object v1, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->CONTENT_NOT_FOUND:Ljava/lang/Integer;

    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v1

    if-ne v0, v1, :cond_3

    .line 126
    :cond_1
    iget-boolean v1, p0, Lcom/helpshift/faq/FaqsDM$2;->val$isAdminSuggested:Z

    if-eqz v1, :cond_2

    .line 127
    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM$2;->this$0:Lcom/helpshift/faq/FaqsDM;

    iget-object v1, v1, Lcom/helpshift/faq/FaqsDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getFAQSuggestionsDAO()Lcom/helpshift/conversation/dao/FAQSuggestionsDAO;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/faq/FaqsDM$2;->val$publishId:Ljava/lang/String;

    iget-object v3, p0, Lcom/helpshift/faq/FaqsDM$2;->val$faqLanguage:Ljava/lang/String;

    invoke-interface {v1, v2, v3}, Lcom/helpshift/conversation/dao/FAQSuggestionsDAO;->removeFAQ(Ljava/lang/String;Ljava/lang/String;)V

    .line 129
    :cond_2
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "/faqs/"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/faq/FaqsDM$2;->val$publishId:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "/"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 130
    iget-object v2, p0, Lcom/helpshift/faq/FaqsDM$2;->this$0:Lcom/helpshift/faq/FaqsDM;

    iget-object v2, v2, Lcom/helpshift/faq/FaqsDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object v2

    const-string v3, ""

    invoke-interface {v2, v1, v3}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->storeETag(Ljava/lang/String;Ljava/lang/String;)V

    .line 132
    :cond_3
    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM$2;->val$callback:Lcom/helpshift/common/FetchDataFromThread;

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-interface {v1, v0}, Lcom/helpshift/common/FetchDataFromThread;->onFailure(Ljava/lang/Object;)V

    :cond_4
    :goto_1
    return-void
.end method
