.class Lcom/helpshift/faq/FaqsDM$3;
.super Lcom/helpshift/common/domain/F;
.source "FaqsDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/faq/FaqsDM;->fetchFaqs(Lcom/helpshift/common/FetchDataFromThread;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/faq/FaqsDM;

.field final synthetic val$callback:Lcom/helpshift/common/FetchDataFromThread;


# direct methods
.method constructor <init>(Lcom/helpshift/faq/FaqsDM;Lcom/helpshift/common/FetchDataFromThread;)V
    .locals 0

    .line 144
    iput-object p1, p0, Lcom/helpshift/faq/FaqsDM$3;->this$0:Lcom/helpshift/faq/FaqsDM;

    iput-object p2, p0, Lcom/helpshift/faq/FaqsDM$3;->val$callback:Lcom/helpshift/common/FetchDataFromThread;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 10

    .line 148
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/faq/FaqsDM$3;->this$0:Lcom/helpshift/faq/FaqsDM;

    const-string v1, "/faqs/"

    invoke-virtual {v0, v1}, Lcom/helpshift/faq/FaqsDM;->getFaqsNetwork(Ljava/lang/String;)Lcom/helpshift/common/domain/network/Network;

    move-result-object v0

    .line 150
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 152
    iget-object v2, p0, Lcom/helpshift/faq/FaqsDM$3;->this$0:Lcom/helpshift/faq/FaqsDM;

    iget-object v2, v2, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v2}, Lcom/helpshift/common/domain/Domain;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object v2

    const-string v3, "defaultFallbackLanguageEnable"

    invoke-virtual {v2, v3}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v2

    const-string v3, "edfl"

    .line 153
    invoke-static {v2}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v3, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 155
    new-instance v2, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {v2, v1}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    .line 157
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 159
    iget-object v3, p0, Lcom/helpshift/faq/FaqsDM$3;->this$0:Lcom/helpshift/faq/FaqsDM;

    iget-object v3, v3, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v3}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object v3

    invoke-virtual {v3}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getSDKLanguage()Ljava/lang/String;

    move-result-object v3

    .line 160
    iget-object v4, p0, Lcom/helpshift/faq/FaqsDM$3;->this$0:Lcom/helpshift/faq/FaqsDM;

    iget-object v4, v4, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v4}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object v4

    invoke-virtual {v4}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getDefaultLanguage()Ljava/lang/String;

    move-result-object v4

    .line 162
    invoke-static {v3}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_0

    move-object v3, v4

    :cond_0
    const-string v4, "Accept-Language"

    .line 163
    sget-object v5, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    const-string v6, "%s;q=1.0"

    const/4 v7, 0x1

    new-array v8, v7, [Ljava/lang/Object;

    const/4 v9, 0x0

    aput-object v3, v8, v9

    invoke-static {v5, v6, v8}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v4, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 164
    invoke-virtual {v2, v1}, Lcom/helpshift/common/platform/network/RequestData;->setCustomHeaders(Ljava/util/Map;)V

    .line 165
    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM$3;->this$0:Lcom/helpshift/faq/FaqsDM;

    const/4 v3, 0x0

    invoke-virtual {v1, v2, v3}, Lcom/helpshift/faq/FaqsDM;->setFaqsCustomHeaders(Lcom/helpshift/common/platform/network/RequestData;Ljava/lang/String;)V

    .line 167
    invoke-interface {v0, v2}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object v0

    .line 169
    iget-object v0, v0, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    if-eqz v0, :cond_1

    .line 173
    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM$3;->this$0:Lcom/helpshift/faq/FaqsDM;

    iget-object v1, v1, Lcom/helpshift/faq/FaqsDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getJsonifier()Lcom/helpshift/common/platform/Jsonifier;

    move-result-object v1

    invoke-interface {v1, v0}, Lcom/helpshift/common/platform/Jsonifier;->jsonifyToArray(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v3

    goto :goto_0

    :cond_1
    const/4 v7, 0x2

    .line 179
    :goto_0
    new-instance v0, Lcom/helpshift/faq/FaqsResponse;

    invoke-direct {v0, v3, v7}, Lcom/helpshift/faq/FaqsResponse;-><init>(Ljava/lang/Object;I)V

    .line 180
    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM$3;->val$callback:Lcom/helpshift/common/FetchDataFromThread;

    invoke-interface {v1, v0}, Lcom/helpshift/common/FetchDataFromThread;->onDataFetched(Ljava/lang/Object;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v0

    .line 183
    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM$3;->val$callback:Lcom/helpshift/common/FetchDataFromThread;

    iget-object v0, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-interface {v1, v0}, Lcom/helpshift/common/FetchDataFromThread;->onFailure(Ljava/lang/Object;)V

    :goto_1
    return-void
.end method
