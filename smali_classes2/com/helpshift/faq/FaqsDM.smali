.class public Lcom/helpshift/faq/FaqsDM;
.super Ljava/lang/Object;
.source "FaqsDM.java"

# interfaces
.implements Lcom/helpshift/common/AutoRetriableDM;


# instance fields
.field final domain:Lcom/helpshift/common/domain/Domain;

.field final faqEventDAO:Lcom/helpshift/faq/dao/FaqEventDAO;

.field final platform:Lcom/helpshift/common/platform/Platform;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V
    .locals 0

    .line 43
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 44
    iput-object p1, p0, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 45
    iput-object p2, p0, Lcom/helpshift/faq/FaqsDM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 46
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getFaqEventDAO()Lcom/helpshift/faq/dao/FaqEventDAO;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/faq/FaqsDM;->faqEventDAO:Lcom/helpshift/faq/dao/FaqEventDAO;

    .line 47
    iget-object p1, p0, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object p1

    sget-object p2, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->FAQ:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {p1, p2, p0}, Lcom/helpshift/common/AutoRetryFailedEventDM;->register(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;Lcom/helpshift/common/AutoRetriableDM;)V

    return-void
.end method


# virtual methods
.method public fetchFaqs(Lcom/helpshift/common/FetchDataFromThread;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/FetchDataFromThread<",
            "Lcom/helpshift/faq/FaqsResponse;",
            "Lcom/helpshift/common/exception/ExceptionType;",
            ">;)V"
        }
    .end annotation

    if-nez p1, :cond_0

    return-void

    .line 144
    :cond_0
    iget-object v0, p0, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/faq/FaqsDM$3;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/faq/FaqsDM$3;-><init>(Lcom/helpshift/faq/FaqsDM;Lcom/helpshift/common/FetchDataFromThread;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public fetchQuestion(Lcom/helpshift/common/FetchDataFromThread;Ljava/lang/String;Ljava/lang/String;Z)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/FetchDataFromThread<",
            "Lcom/helpshift/faq/FaqCore;",
            "Ljava/lang/Integer;",
            ">;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Z)V"
        }
    .end annotation

    .line 84
    iget-object v0, p0, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v7, Lcom/helpshift/faq/FaqsDM$2;

    move-object v1, v7

    move-object v2, p0

    move-object v3, p3

    move v4, p4

    move-object v5, p2

    move-object v6, p1

    invoke-direct/range {v1 .. v6}, Lcom/helpshift/faq/FaqsDM$2;-><init>(Lcom/helpshift/faq/FaqsDM;Ljava/lang/String;ZLjava/lang/String;Lcom/helpshift/common/FetchDataFromThread;)V

    invoke-virtual {v0, v7}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method getFaqsNetwork(Ljava/lang/String;)Lcom/helpshift/common/domain/network/Network;
    .locals 3

    .line 248
    new-instance v0, Lcom/helpshift/common/domain/network/GETNetwork;

    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/faq/FaqsDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, p1, v1, v2}, Lcom/helpshift/common/domain/network/GETNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 249
    new-instance v1, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;

    invoke-direct {v1, v0}, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 250
    new-instance v0, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v2, p0, Lcom/helpshift/faq/FaqsDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 251
    new-instance v1, Lcom/helpshift/common/domain/network/ContentUnchangedNetwork;

    invoke-direct {v1, v0}, Lcom/helpshift/common/domain/network/ContentUnchangedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 252
    new-instance v0, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v0, v1}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 253
    new-instance v1, Lcom/helpshift/common/domain/network/ETagNetwork;

    iget-object v2, p0, Lcom/helpshift/faq/FaqsDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v1, v0, v2, p1}, Lcom/helpshift/common/domain/network/ETagNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;Ljava/lang/String;)V

    return-object v1
.end method

.method public markHelpful(Ljava/lang/String;Z)V
    .locals 2

    .line 52
    iget-object v0, p0, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/faq/FaqsDM$1;

    invoke-direct {v1, p0, p1, p2}, Lcom/helpshift/faq/FaqsDM$1;-><init>(Lcom/helpshift/faq/FaqsDM;Ljava/lang/String;Z)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    if-eqz p2, :cond_0

    .line 74
    sget-object p2, Lcom/helpshift/analytics/AnalyticsEventType;->MARKED_HELPFUL:Lcom/helpshift/analytics/AnalyticsEventType;

    goto :goto_0

    .line 77
    :cond_0
    sget-object p2, Lcom/helpshift/analytics/AnalyticsEventType;->MARKED_UNHELPFUL:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 79
    :goto_0
    iget-object v0, p0, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object v0

    invoke-virtual {v0, p2, p1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/lang/String;)V

    return-void
.end method

.method send(Ljava/lang/String;Z)V
    .locals 2

    if-eqz p2, :cond_0

    .line 219
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "/faqs/"

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "/helpful/"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    .line 222
    :cond_0
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "/faqs/"

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "/unhelpful/"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 224
    :goto_0
    new-instance p2, Lcom/helpshift/common/domain/network/POSTNetwork;

    iget-object v0, p0, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {p2, p1, v0, v1}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 225
    new-instance p1, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;

    invoke-direct {p1, p2}, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 226
    new-instance p2, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v0, p0, Lcom/helpshift/faq/FaqsDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {p2, p1, v0}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 227
    new-instance p1, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {p1, p2}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 229
    new-instance p2, Lcom/helpshift/common/platform/network/RequestData;

    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-direct {p2, v0}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    .line 230
    invoke-interface {p1, p2}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    return-void
.end method

.method public sendFailedApiCalls(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)V
    .locals 5

    .line 192
    sget-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->FAQ:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    if-eq p1, v0, :cond_0

    return-void

    .line 195
    :cond_0
    iget-object p1, p0, Lcom/helpshift/faq/FaqsDM;->faqEventDAO:Lcom/helpshift/faq/dao/FaqEventDAO;

    invoke-interface {p1}, Lcom/helpshift/faq/dao/FaqEventDAO;->getUnSentFaqMarkHelpfulEvents()Ljava/util/Map;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 197
    invoke-interface {p1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 198
    invoke-interface {p1, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/Boolean;

    .line 200
    :try_start_0
    invoke-virtual {v2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v2

    invoke-virtual {p0, v1, v2}, Lcom/helpshift/faq/FaqsDM;->send(Ljava/lang/String;Z)V

    .line 201
    iget-object v2, p0, Lcom/helpshift/faq/FaqsDM;->faqEventDAO:Lcom/helpshift/faq/dao/FaqEventDAO;

    invoke-interface {v2, v1}, Lcom/helpshift/faq/dao/FaqEventDAO;->removeFaqMarkHelpfulEvent(Ljava/lang/String;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 204
    iget-object v3, v2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v4, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-ne v3, v4, :cond_1

    .line 205
    iget-object v2, p0, Lcom/helpshift/faq/FaqsDM;->faqEventDAO:Lcom/helpshift/faq/dao/FaqEventDAO;

    invoke-interface {v2, v1}, Lcom/helpshift/faq/dao/FaqEventDAO;->removeFaqMarkHelpfulEvent(Ljava/lang/String;)V

    goto :goto_0

    .line 208
    :cond_1
    throw v2

    :cond_2
    return-void
.end method

.method setFaqsCustomHeaders(Lcom/helpshift/common/platform/network/RequestData;Ljava/lang/String;)V
    .locals 6

    .line 234
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 236
    invoke-static {p2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 237
    iget-object p2, p0, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object p2

    invoke-virtual {p2}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getSDKLanguage()Ljava/lang/String;

    move-result-object p2

    .line 238
    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getDefaultLanguage()Ljava/lang/String;

    move-result-object v1

    .line 240
    invoke-static {p2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    move-object p2, v1

    :cond_0
    const-string v1, "Accept-Language"

    .line 243
    sget-object v2, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    const-string v3, "%s;q=1.0"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    aput-object p2, v4, v5

    invoke-static {v2, v3, v4}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    invoke-interface {v0, v1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 244
    invoke-virtual {p1, v0}, Lcom/helpshift/common/platform/network/RequestData;->setCustomHeaders(Ljava/util/Map;)V

    return-void
.end method
