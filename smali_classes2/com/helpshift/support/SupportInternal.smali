.class public final Lcom/helpshift/support/SupportInternal;
.super Ljava/lang/Object;
.source "SupportInternal.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/SupportInternal$EnableContactUs;,
        Lcom/helpshift/support/SupportInternal$RateAlert;
    }
.end annotation


# static fields
.field public static final CONVERSATION_FLOW:Ljava/lang/String; = "conversationFlow"

.field public static final CustomMetadataKey:Ljava/lang/String; = "hs-custom-metadata"

.field public static final DYNAMIC_FORM_FLOW:Ljava/lang/String; = "dynamicFormFlow"

.field public static final FAQS_FLOW:Ljava/lang/String; = "faqsFlow"

.field public static final FAQ_SECTION_FLOW:Ljava/lang/String; = "faqSectionFlow"

.field public static final SINGLE_FAQ_FLOW:Ljava/lang/String; = "singleFaqFlow"

.field public static final TAG:Ljava/lang/String; = "Helpshift_SupportInter"

.field private static context:Landroid/content/Context;

.field private static data:Lcom/helpshift/support/HSApiData;

.field private static storage:Lcom/helpshift/support/HSStorage;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 90
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Landroid/os/Bundle;"
        }
    .end annotation

    .line 1001
    new-instance v0, Ljava/util/HashMap;

    invoke-static {}, Lcom/helpshift/support/util/ConfigUtil;->getDefaultApiConfig()Ljava/util/Map;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 1002
    invoke-virtual {v0, p0}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 1004
    invoke-static {v0}, Lcom/helpshift/support/ContactUsFilter;->setConfig(Ljava/util/HashMap;)V

    .line 1005
    new-instance p0, Landroid/os/Bundle;

    invoke-direct {p0}, Landroid/os/Bundle;-><init>()V

    .line 1006
    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->createMetadataCallback(Ljava/util/HashMap;)V

    .line 1007
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, v0}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 1009
    new-instance v2, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;

    invoke-direct {v2}, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;-><init>()V

    invoke-virtual {v2, v0}, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->applyMap(Ljava/util/Map;)Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;

    move-result-object v2

    invoke-virtual {v2}, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->build()Lcom/helpshift/configuration/dto/RootApiConfig;

    move-result-object v2

    .line 1010
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v3

    invoke-interface {v3, v2}, Lcom/helpshift/CoreApi;->updateApiConfig(Lcom/helpshift/configuration/dto/RootApiConfig;)V

    .line 1013
    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->updateCustomIssueFieldData(Ljava/util/Map;)V

    :try_start_0
    const-string v2, "conversationPrefillText"

    .line 1019
    invoke-virtual {v1, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    const-string v2, "conversationPrefillText"

    .line 1022
    invoke-virtual {v1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "null"

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_0

    const-string v2, "hs-custom-metadata"

    .line 1026
    invoke-virtual {v1, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    const-string v2, "dropMeta"

    const/4 v3, 0x1

    .line 1027
    invoke-virtual {p0, v2, v3}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    :cond_0
    const-string v2, "toolbarId"

    .line 1032
    invoke-virtual {v1, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_1

    const-string v2, "toolbarId"

    const-string v3, "toolbarId"

    .line 1033
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v3

    invoke-virtual {p0, v2, v3}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    const-string v3, "Helpshift_SupportInter"

    const-string v4, "JSON exception while parsing config : "

    .line 1037
    invoke-static {v3, v4, v2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_1
    :goto_0
    const-string v2, "showSearchOnNewConversation"

    const-string v3, "showSearchOnNewConversation"

    const/4 v4, 0x0

    .line 1041
    invoke-virtual {v1, v3, v4}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v1

    .line 1040
    invoke-virtual {p0, v2, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v1, "withTagsMatching"

    const-string v2, "withTagsMatching"

    .line 1044
    invoke-virtual {v0, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    invoke-static {v2}, Lcom/helpshift/support/SupportInternal;->cleanFaqTagFilter(Ljava/lang/Object;)Lcom/helpshift/support/FaqTagFilter;

    move-result-object v2

    invoke-virtual {p0, v1, v2}, Landroid/os/Bundle;->putSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    const-string v1, "customContactUsFlows"

    .line 1050
    invoke-virtual {v0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/List;

    .line 1051
    invoke-static {v0}, Lcom/helpshift/support/flows/CustomContactUsFlowListHolder;->setFlowList(Ljava/util/List;)V

    return-object p0
.end method

.method private static cleanFaqTagFilter(Ljava/lang/Object;)Lcom/helpshift/support/FaqTagFilter;
    .locals 3

    const/4 v0, 0x0

    if-nez p0, :cond_0

    return-object v0

    .line 1078
    :cond_0
    :try_start_0
    check-cast p0, Ljava/util/Map;

    const-string v1, "operator"

    .line 1080
    invoke-interface {p0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 1081
    invoke-static {v1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v2

    if-nez v2, :cond_3

    .line 1082
    invoke-virtual {v1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v1

    sget-object v2, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-virtual {v1, v2}, Ljava/lang/String;->toLowerCase(Ljava/util/Locale;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "tags"

    .line 1083
    invoke-interface {p0, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, [Ljava/lang/String;

    check-cast p0, [Ljava/lang/String;

    if-eqz p0, :cond_3

    .line 1085
    array-length v2, p0

    if-lez v2, :cond_3

    const-string v2, "and"

    .line 1086
    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_1

    .line 1087
    new-instance v1, Lcom/helpshift/support/FaqTagFilter;

    const-string v2, "and"

    invoke-direct {v1, v2, p0}, Lcom/helpshift/support/FaqTagFilter;-><init>(Ljava/lang/String;[Ljava/lang/String;)V

    return-object v1

    :cond_1
    const-string v2, "or"

    .line 1089
    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_2

    .line 1090
    new-instance v1, Lcom/helpshift/support/FaqTagFilter;

    const-string v2, "or"

    invoke-direct {v1, v2, p0}, Lcom/helpshift/support/FaqTagFilter;-><init>(Ljava/lang/String;[Ljava/lang/String;)V

    return-object v1

    :cond_2
    const-string v2, "not"

    .line 1092
    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_3

    .line 1093
    new-instance v1, Lcom/helpshift/support/FaqTagFilter;

    const-string v2, "not"

    invoke-direct {v1, v2, p0}, Lcom/helpshift/support/FaqTagFilter;-><init>(Ljava/lang/String;[Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/ClassCastException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v1

    :catch_0
    move-exception p0

    const-string v1, "Helpshift_SupportInter"

    const-string v2, "Invalid FaqTagFilter object in config"

    .line 1099
    invoke-static {v1, v2, p0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_3
    return-object v0
.end method

.method public static clearAnonymousUser()Z
    .locals 1

    .line 1280
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->clearAnonymousUser()Z

    move-result v0

    return v0
.end method

.method public static clearBreadCrumbs()V
    .locals 1

    .line 485
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getMetaDataDM()Lcom/helpshift/meta/MetaDataDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/meta/MetaDataDM;->clearBreadCrumbs()V

    return-void
.end method

.method private static createMetadataCallback(Ljava/util/HashMap;)V
    .locals 1

    const-string v0, "hs-custom-metadata"

    .line 848
    invoke-virtual {p0, v0}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 849
    new-instance v0, Lcom/helpshift/support/SupportInternal$3;

    invoke-direct {v0, p0}, Lcom/helpshift/support/SupportInternal$3;-><init>(Ljava/util/HashMap;)V

    .line 860
    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->setMetadataCallback(Lcom/helpshift/support/Callable;)V

    :cond_0
    return-void
.end method

.method public static getConversationFragment(Landroid/app/Activity;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .line 1324
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p1}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    const-string p1, "Helpshift_SupportInter"

    const-string v1, "Get Conversation fragment : "

    const/4 v2, 0x1

    .line 1326
    new-array v3, v2, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v4, "Config"

    invoke-static {v4, v0}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromMap(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v4

    const/4 v5, 0x0

    aput-object v4, v3, v5

    invoke-static {p1, v1, v3}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 1327
    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->removeShowConversationUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object p1

    const-string v0, "showInFullScreen"

    .line 1328
    invoke-static {p0}, Lcom/helpshift/util/ActivityUtil;->isFullScreen(Landroid/app/Activity;)Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v0, "support_mode"

    .line 1329
    invoke-virtual {p1, v0, v2}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v0, "decomp"

    .line 1330
    invoke-virtual {p1, v0, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v0, "showInFullScreen"

    .line 1331
    invoke-static {p0}, Lcom/helpshift/util/ActivityUtil;->isFullScreen(Landroid/app/Activity;)Ljava/lang/Boolean;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p0

    invoke-virtual {p1, v0, p0}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string p0, "isRoot"

    .line 1332
    invoke-virtual {p1, p0, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string p0, "search_performed"

    .line 1333
    invoke-virtual {p1, p0, v5}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 1334
    invoke-static {p1}, Lcom/helpshift/support/fragments/SupportFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getDynamicFormFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .line 1370
    invoke-static {p2}, Lcom/helpshift/support/flows/DynamicFormFlowListHolder;->setFlowList(Ljava/util/List;)V

    .line 1371
    new-instance p2, Ljava/util/HashMap;

    invoke-direct {p2, p3}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    const-string p3, "Helpshift_SupportInter"

    const-string v0, "Get dynamic flow fragment : "

    const/4 v1, 0x1

    .line 1373
    new-array v2, v1, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v3, "Config"

    invoke-static {v3, p2}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromMap(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-static {p3, v0, v2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 1374
    invoke-static {p2}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object p2

    const-string p3, "support_mode"

    const/4 v0, 0x4

    .line 1375
    invoke-virtual {p2, p3, v0}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string p3, "showInFullScreen"

    .line 1376
    invoke-static {p0}, Lcom/helpshift/util/ActivityUtil;->isFullScreen(Landroid/app/Activity;)Ljava/lang/Boolean;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p0

    invoke-virtual {p2, p3, p0}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string p0, "flow_title"

    .line 1377
    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p0, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p0, "decomp"

    .line 1378
    invoke-virtual {p2, p0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 1379
    invoke-static {p2}, Lcom/helpshift/support/fragments/SupportFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getFAQSectionFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .line 1339
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    const-string p2, "Helpshift_SupportInter"

    .line 1341
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Get FAQ section fragment : Publish Id : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x1

    new-array v3, v2, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v4, "Config"

    .line 1342
    invoke-static {v4, v0}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromMap(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v4

    const/4 v5, 0x0

    aput-object v4, v3, v5

    .line 1341
    invoke-static {p2, v1, v3}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 1343
    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->removeFAQFlowUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;

    move-result-object p2

    invoke-static {p2}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object p2

    const-string v0, "support_mode"

    const/4 v1, 0x2

    .line 1344
    invoke-virtual {p2, v0, v1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v0, "sectionPublishId"

    .line 1345
    invoke-virtual {p2, v0, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "showInFullScreen"

    .line 1346
    invoke-static {p0}, Lcom/helpshift/util/ActivityUtil;->isFullScreen(Landroid/app/Activity;)Ljava/lang/Boolean;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p0

    invoke-virtual {p2, p1, p0}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string p0, "decomp"

    .line 1347
    invoke-virtual {p2, p0, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string p0, "isRoot"

    .line 1348
    invoke-virtual {p2, p0, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 1349
    invoke-static {p2}, Lcom/helpshift/support/fragments/SupportFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static getFAQsFragment(Landroid/app/Activity;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .line 1315
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p1}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    const-string p1, "Helpshift_SupportInter"

    const-string v1, "Get FAQ fragment : "

    const/4 v2, 0x1

    .line 1317
    new-array v2, v2, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v3, "Config"

    invoke-static {v3, v0}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromMap(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-static {p1, v1, v2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 1318
    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->removeFAQFlowUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object p1

    const-string v0, "showInFullScreen"

    .line 1319
    invoke-static {p0}, Lcom/helpshift/util/ActivityUtil;->isFullScreen(Landroid/app/Activity;)Ljava/lang/Boolean;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p0

    invoke-virtual {p1, v0, p0}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 1320
    invoke-static {p1}, Lcom/helpshift/support/fragments/SupportFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method private static getIssueId(Landroid/content/Intent;Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    const/4 v0, 0x0

    if-eqz p0, :cond_3

    .line 1170
    invoke-virtual {p0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v1

    if-nez v1, :cond_0

    goto :goto_1

    .line 1175
    :cond_0
    invoke-virtual {p0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object p0

    const-string v1, "issue"

    .line 1176
    invoke-virtual {v1, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    const-string p1, "issue_id"

    .line 1177
    invoke-virtual {p0, p1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    :cond_1
    const-string v1, "preissue"

    .line 1179
    invoke-virtual {v1, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_2

    const-string p1, "preissue_id"

    .line 1180
    invoke-virtual {p0, p1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    :cond_2
    :goto_0
    return-object v0

    :cond_3
    :goto_1
    return-object v0
.end method

.method private static getIssueType(Landroid/content/Intent;)Ljava/lang/String;
    .locals 1

    if-eqz p0, :cond_1

    .line 1187
    invoke-virtual {p0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 1191
    :cond_0
    invoke-virtual {p0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object p0

    const-string v0, "issue_type"

    invoke-virtual {p0, v0}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0

    :cond_1
    :goto_0
    const/4 p0, 0x0

    return-object p0
.end method

.method public static getNotificationCount()Ljava/lang/Integer;
    .locals 1

    .line 295
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getNotificationCountSync()I

    move-result v0

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    return-object v0
.end method

.method public static getNotificationCount(Landroid/os/Handler;Landroid/os/Handler;)V
    .locals 2

    if-nez p0, :cond_0

    return-void

    .line 327
    :cond_0
    sget-object v0, Lcom/helpshift/support/SupportInternal;->data:Lcom/helpshift/support/HSApiData;

    if-eqz v0, :cond_1

    sget-object v0, Lcom/helpshift/support/SupportInternal;->storage:Lcom/helpshift/support/HSStorage;

    if-nez v0, :cond_3

    .line 328
    :cond_1
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    if-nez v0, :cond_2

    return-void

    .line 331
    :cond_2
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->init(Landroid/content/Context;)V

    .line 334
    :cond_3
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    new-instance v1, Lcom/helpshift/support/SupportInternal$1;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/support/SupportInternal$1;-><init>(Landroid/os/Handler;Landroid/os/Handler;)V

    .line 335
    invoke-interface {v0, v1}, Lcom/helpshift/CoreApi;->getNotificationCountAsync(Lcom/helpshift/common/FetchDataFromThread;)V

    return-void
.end method

.method public static getSingleFAQFragment(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/support/fragments/SupportFragment;"
        }
    .end annotation

    .line 1354
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    const-string p2, "Helpshift_SupportInter"

    .line 1356
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Get single FAQ fragment : Publish Id : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x1

    new-array v3, v2, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v4, "Config"

    .line 1357
    invoke-static {v4, v0}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromMap(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v4

    const/4 v5, 0x0

    aput-object v4, v3, v5

    .line 1356
    invoke-static {p2, v1, v3}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 1358
    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->removeFAQFlowUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;

    move-result-object p2

    invoke-static {p2}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object p2

    const-string v0, "support_mode"

    const/4 v1, 0x3

    .line 1359
    invoke-virtual {p2, v0, v1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v0, "questionPublishId"

    .line 1360
    invoke-virtual {p2, v0, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "showInFullScreen"

    .line 1361
    invoke-static {p0}, Lcom/helpshift/util/ActivityUtil;->isFullScreen(Landroid/app/Activity;)Ljava/lang/Boolean;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p0

    invoke-virtual {p2, p1, p0}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string p0, "decomp"

    .line 1362
    invoke-virtual {p2, p0, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string p0, "isRoot"

    .line 1363
    invoke-virtual {p2, p0, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 1364
    invoke-static {p2}, Lcom/helpshift/support/fragments/SupportFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p0

    return-object p0
.end method

.method public static handlePush(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 3

    .line 1151
    invoke-static {p0}, Lcom/helpshift/support/SupportInternal;->init(Landroid/content/Context;)V

    .line 1153
    invoke-static {p1}, Lcom/helpshift/support/SupportInternal;->getIssueType(Landroid/content/Intent;)Ljava/lang/String;

    move-result-object p0

    .line 1154
    invoke-static {p1, p0}, Lcom/helpshift/support/SupportInternal;->getIssueId(Landroid/content/Intent;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    const-string p0, "Helpshift_SupportInter"

    const-string p1, "Unknown issuetype/issueId in push payload"

    .line 1157
    invoke-static {p0, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :cond_0
    const/4 v1, 0x0

    .line 1162
    invoke-virtual {p1}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object p1

    if-eqz p1, :cond_1

    const-string v2, "app_name"

    .line 1163
    invoke-virtual {p1, v2}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_1

    const-string v1, "app_name"

    .line 1164
    invoke-virtual {p1, v1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 1166
    :cond_1
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1, p0, v0, v1}, Lcom/helpshift/CoreApi;->handlePushNotification(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method private static init(Landroid/app/Application;)V
    .locals 0

    .line 95
    invoke-virtual {p0}, Landroid/app/Application;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    invoke-static {p0}, Lcom/helpshift/support/SupportInternal;->initialize(Landroid/content/Context;)V

    return-void
.end method

.method private static init(Landroid/content/Context;)V
    .locals 0

    .line 99
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    invoke-static {p0}, Lcom/helpshift/support/SupportInternal;->initialize(Landroid/content/Context;)V

    return-void
.end method

.method private static initialize(Landroid/content/Context;)V
    .locals 1

    .line 103
    sget-object v0, Lcom/helpshift/support/SupportInternal;->context:Landroid/content/Context;

    if-nez v0, :cond_0

    .line 104
    new-instance v0, Lcom/helpshift/support/HSApiData;

    invoke-direct {v0, p0}, Lcom/helpshift/support/HSApiData;-><init>(Landroid/content/Context;)V

    sput-object v0, Lcom/helpshift/support/SupportInternal;->data:Lcom/helpshift/support/HSApiData;

    .line 105
    sget-object v0, Lcom/helpshift/support/SupportInternal;->data:Lcom/helpshift/support/HSApiData;

    iget-object v0, v0, Lcom/helpshift/support/HSApiData;->storage:Lcom/helpshift/support/HSStorage;

    sput-object v0, Lcom/helpshift/support/SupportInternal;->storage:Lcom/helpshift/support/HSStorage;

    .line 106
    invoke-static {p0}, Lcom/helpshift/support/ContactUsFilter;->init(Landroid/content/Context;)V

    .line 107
    sput-object p0, Lcom/helpshift/support/SupportInternal;->context:Landroid/content/Context;

    :cond_0
    return-void
.end method

.method public static install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 158
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, p1, p2, p3, v0}, Lcom/helpshift/support/SupportInternal;->install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public static install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .locals 4
    .annotation build Landroid/annotation/TargetApi;
        value = 0xe
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Application;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 215
    invoke-static {p0}, Lcom/helpshift/support/SupportInternal;->init(Landroid/app/Application;)V

    .line 218
    new-instance p1, Lcom/helpshift/support/providers/SupportDataProvider;

    invoke-direct {p1}, Lcom/helpshift/support/providers/SupportDataProvider;-><init>()V

    invoke-static {p1}, Lcom/helpshift/providers/CrossModuleDataProvider;->setSupportDataProvider(Lcom/helpshift/providers/ISupportDataProvider;)V

    .line 220
    invoke-static {}, Lcom/helpshift/support/util/ConfigUtil;->getDefaultInstallConfig()Ljava/util/Map;

    move-result-object p1

    check-cast p1, Ljava/util/HashMap;

    if-eqz p4, :cond_0

    .line 222
    invoke-virtual {p1, p4}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 225
    :cond_0
    invoke-virtual {p0}, Landroid/app/Application;->getPackageName()Ljava/lang/String;

    move-result-object p2

    .line 226
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object p3

    iget-object p3, p3, Lcom/helpshift/model/InfoModelFactory;->appInfoModel:Lcom/helpshift/model/AppInfoModel;

    const-string p4, "notificationIcon"

    .line 227
    invoke-virtual {p1, p4}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p4

    .line 228
    instance-of v0, p4, Ljava/lang/String;

    if-eqz v0, :cond_1

    .line 229
    check-cast p4, Ljava/lang/String;

    const-string v0, "drawable"

    invoke-static {p0, p4, v0, p2}, Lcom/helpshift/util/ApplicationUtil;->getResourceIdFromName(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p4

    const-string v0, "notificationIcon"

    .line 230
    invoke-static {p4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p4

    invoke-virtual {p1, v0, p4}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    const-string p4, "notificationSound"

    .line 233
    invoke-virtual {p1, p4}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p4

    .line 234
    instance-of v0, p4, Ljava/lang/String;

    if-eqz v0, :cond_2

    .line 235
    check-cast p4, Ljava/lang/String;

    const-string v0, "raw"

    invoke-static {p0, p4, v0, p2}, Lcom/helpshift/util/ApplicationUtil;->getResourceIdFromName(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    const-string p4, "notificationSound"

    .line 236
    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    invoke-virtual {p1, p4, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 239
    :cond_2
    new-instance p2, Lcom/helpshift/configuration/dto/RootInstallConfig$RootInstallConfigBuilder;

    invoke-direct {p2}, Lcom/helpshift/configuration/dto/RootInstallConfig$RootInstallConfigBuilder;-><init>()V

    invoke-virtual {p2, p1}, Lcom/helpshift/configuration/dto/RootInstallConfig$RootInstallConfigBuilder;->applyMap(Ljava/util/Map;)Lcom/helpshift/configuration/dto/RootInstallConfig$RootInstallConfigBuilder;

    move-result-object p2

    invoke-virtual {p2}, Lcom/helpshift/configuration/dto/RootInstallConfig$RootInstallConfigBuilder;->build()Lcom/helpshift/configuration/dto/RootInstallConfig;

    move-result-object p2

    .line 242
    sget-object p4, Lcom/helpshift/support/SupportInternal;->context:Landroid/content/Context;

    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object v0

    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->getDomain()Lcom/helpshift/common/domain/Domain;

    move-result-object v1

    sget-object v2, Lcom/helpshift/support/SupportInternal;->data:Lcom/helpshift/support/HSApiData;

    sget-object v3, Lcom/helpshift/support/SupportInternal;->storage:Lcom/helpshift/support/HSStorage;

    invoke-static {p4, v0, v1, v2, v3}, Lcom/helpshift/support/SupportMigrator;->migrate(Landroid/content/Context;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/support/HSApiData;Lcom/helpshift/support/HSStorage;)V

    .line 244
    iget-object p4, p2, Lcom/helpshift/configuration/dto/RootInstallConfig;->fontPath:Ljava/lang/String;

    invoke-virtual {p3, p4}, Lcom/helpshift/model/AppInfoModel;->setFontPath(Ljava/lang/String;)V

    const-string p4, "screenOrientation"

    .line 246
    const-class v0, Ljava/lang/Integer;

    const/4 v1, 0x0

    invoke-static {p1, p4, v0, v1}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Integer;

    if-nez p1, :cond_3

    const/4 p1, -0x1

    goto :goto_0

    .line 247
    :cond_3
    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    :goto_0
    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    .line 248
    invoke-virtual {p3, p1}, Lcom/helpshift/model/AppInfoModel;->setScreenOrientation(Ljava/lang/Integer;)V

    .line 250
    iget-object p1, p2, Lcom/helpshift/configuration/dto/RootInstallConfig;->disableAnimations:Ljava/lang/Boolean;

    const/4 p4, 0x0

    if-nez p1, :cond_4

    const/4 p1, 0x0

    goto :goto_1

    .line 251
    :cond_4
    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    :goto_1
    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    .line 252
    invoke-virtual {p3, p1}, Lcom/helpshift/model/AppInfoModel;->setDisableAnimations(Ljava/lang/Boolean;)V

    .line 254
    sget-object p1, Lcom/helpshift/support/SupportInternal;->context:Landroid/content/Context;

    invoke-static {p1}, Lcom/helpshift/util/ApplicationUtil;->getApplicationVersion(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p1

    .line 256
    sget-object p3, Lcom/helpshift/support/SupportInternal;->storage:Lcom/helpshift/support/HSStorage;

    invoke-virtual {p3}, Lcom/helpshift/support/HSStorage;->getApplicationVersion()Ljava/lang/String;

    move-result-object p3

    invoke-virtual {p3, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p3

    if-nez p3, :cond_5

    .line 257
    sget-object p3, Lcom/helpshift/support/SupportInternal;->data:Lcom/helpshift/support/HSApiData;

    invoke-virtual {p3}, Lcom/helpshift/support/HSApiData;->resetReviewCounter()V

    .line 258
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p3

    invoke-interface {p3}, Lcom/helpshift/CoreApi;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object p3

    invoke-virtual {p3, p4}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->setAppReviewed(Z)V

    .line 259
    sget-object p3, Lcom/helpshift/support/SupportInternal;->storage:Lcom/helpshift/support/HSStorage;

    invoke-virtual {p3, p1}, Lcom/helpshift/support/HSStorage;->setApplicationVersion(Ljava/lang/String;)V

    .line 262
    :cond_5
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1, p2}, Lcom/helpshift/CoreApi;->updateInstallConfig(Lcom/helpshift/configuration/dto/RootInstallConfig;)V

    const-string p1, "__hs__db_error_reports"

    .line 264
    invoke-virtual {p0, p1}, Landroid/app/Application;->deleteDatabase(Ljava/lang/String;)Z

    .line 266
    new-instance p1, Lcom/helpshift/notifications/NotificationChannelsManager;

    invoke-direct {p1, p0}, Lcom/helpshift/notifications/NotificationChannelsManager;-><init>(Landroid/content/Context;)V

    .line 267
    invoke-virtual {p1}, Lcom/helpshift/notifications/NotificationChannelsManager;->checkAndUpdateDefaultChannelInfo()V

    return-void
.end method

.method public static isConversationActive()Z
    .locals 1

    .line 282
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->isActiveConversationActionable()Z

    move-result v0

    return v0
.end method

.method private static isValidPublishId(Ljava/lang/String;)Z
    .locals 1

    if-eqz p0, :cond_0

    .line 997
    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_0

    const-string v0, "\\d+"

    invoke-virtual {p0, v0}, Ljava/lang/String;->matches(Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_0

    const/4 p0, 0x1

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    :goto_0
    return p0
.end method

.method public static leaveBreadCrumb(Ljava/lang/String;)V
    .locals 1

    if-eqz p0, :cond_0

    .line 467
    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 468
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getMetaDataDM()Lcom/helpshift/meta/MetaDataDM;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/meta/MetaDataDM;->pushBreadCrumb(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public static login(Lcom/helpshift/HelpshiftUser;)Z
    .locals 1

    .line 1267
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0, p0}, Lcom/helpshift/CoreApi;->login(Lcom/helpshift/HelpshiftUser;)Z

    move-result p0

    return p0
.end method

.method public static logout()Z
    .locals 1

    .line 1276
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->logout()Z

    move-result v0

    return v0
.end method

.method public static preInstall(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .locals 1

    .line 125
    invoke-virtual {p0}, Landroid/app/Application;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/util/HelpshiftContext;->setApplicationContext(Landroid/content/Context;)V

    .line 126
    invoke-static {p1, p2, p3}, Lcom/helpshift/util/HelpshiftContext;->initializeCore(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    if-eqz p4, :cond_0

    const-string p1, "manualLifecycleTracking"

    .line 130
    invoke-interface {p4, p1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const-string p1, "manualLifecycleTracking"

    .line 131
    invoke-interface {p4, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    .line 134
    :goto_0
    new-instance p2, Lcom/helpshift/support/SupportAppLifeCycleListener;

    invoke-direct {p2}, Lcom/helpshift/support/SupportAppLifeCycleListener;-><init>()V

    .line 135
    invoke-static {}, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->getInstance()Lcom/helpshift/applifecycle/HSAppLifeCycleController;

    move-result-object p3

    .line 136
    invoke-virtual {p3, p0, p1}, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->init(Landroid/app/Application;Z)V

    .line 137
    invoke-virtual {p3, p2}, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->registerAppLifeCycleListener(Lcom/helpshift/applifecycle/HSAppLifeCycleListener;)V

    return-void
.end method

.method public static registerDeviceToken(Landroid/content/Context;Ljava/lang/String;)V
    .locals 0

    .line 445
    invoke-static {p0}, Lcom/helpshift/support/SupportInternal;->init(Landroid/content/Context;)V

    if-eqz p1, :cond_0

    .line 447
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p0

    invoke-interface {p0, p1}, Lcom/helpshift/CoreApi;->setPushToken(Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    const-string p0, "Helpshift_SupportInter"

    const-string p1, "Device Token is null"

    .line 450
    invoke-static {p0, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method public static removeFAQFlowUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 983
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p0}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    const-string p0, "conversationPrefillText"

    .line 984
    invoke-virtual {v0, p0}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    return-object v0
.end method

.method public static removeShowConversationUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 990
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p0}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    const-string p0, "enableContactUs"

    .line 991
    invoke-virtual {v0, p0}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    const-string p0, "customContactUsFlows"

    .line 992
    invoke-virtual {v0, p0}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    return-object v0
.end method

.method public static setDelegate(Lcom/helpshift/delegate/RootDelegate;)V
    .locals 1

    .line 1260
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0, p0}, Lcom/helpshift/CoreApi;->setDelegateListener(Lcom/helpshift/delegate/RootDelegate;)V

    return-void
.end method

.method public static setMetadataCallback(Lcom/helpshift/support/Callable;)V
    .locals 1

    .line 830
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getMetaDataDM()Lcom/helpshift/meta/MetaDataDM;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/meta/MetaDataDM;->setCustomMetaDataCallable(Lcom/helpshift/meta/RootMetaDataCallable;)V

    return-void
.end method

.method public static setMetadataCallback(Lcom/helpshift/support/MetadataCallable;)V
    .locals 1

    .line 834
    new-instance v0, Lcom/helpshift/support/SupportInternal$2;

    invoke-direct {v0, p0}, Lcom/helpshift/support/SupportInternal$2;-><init>(Lcom/helpshift/support/MetadataCallable;)V

    .line 844
    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->setMetadataCallback(Lcom/helpshift/support/Callable;)V

    return-void
.end method

.method public static setNameAndEmail(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    if-eqz p0, :cond_1

    .line 381
    invoke-static {p0}, Lcom/helpshift/util/HSPattern;->hasOnlySpecialCharacters(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 385
    :cond_0
    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p0

    goto :goto_1

    :cond_1
    :goto_0
    const-string p0, ""

    .line 388
    :goto_1
    invoke-static {p1}, Lcom/helpshift/util/HSPattern;->isValidEmail(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_2

    const-string p1, ""

    goto :goto_2

    .line 392
    :cond_2
    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    .line 395
    :goto_2
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0, p0, p1}, Lcom/helpshift/CoreApi;->setNameAndEmail(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public static setSDKLanguage(Ljava/lang/String;)V
    .locals 1

    .line 1307
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->setSdkLanguage(Ljava/lang/String;)V

    return-void
.end method

.method public static setTheme(I)V
    .locals 1

    .line 1311
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/model/InfoModelFactory;->sdkInfoModel:Lcom/helpshift/model/SdkInfoModel;

    invoke-virtual {v0, p0}, Lcom/helpshift/model/SdkInfoModel;->setTheme(I)V

    return-void
.end method

.method public static setUserIdentifier(Ljava/lang/String;)V
    .locals 1

    if-eqz p0, :cond_0

    .line 410
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->setUserMetaIdentifier(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public static showAlertToRateApp(Ljava/lang/String;Lcom/helpshift/support/AlertToRateAppListener;)V
    .locals 2

    .line 1231
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.VIEW"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 1232
    invoke-static {p0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-nez v1, :cond_0

    .line 1233
    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setData(Landroid/net/Uri;)Landroid/content/Intent;

    .line 1236
    :cond_0
    invoke-static {p0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-nez v1, :cond_2

    sget-object v1, Lcom/helpshift/support/SupportInternal;->context:Landroid/content/Context;

    .line 1237
    invoke-virtual {v1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/content/Intent;->resolveActivity(Landroid/content/pm/PackageManager;)Landroid/content/ComponentName;

    move-result-object v0

    if-nez v0, :cond_1

    goto :goto_0

    .line 1244
    :cond_1
    invoke-static {p1}, Lcom/helpshift/support/HSReviewFragment;->setAlertToRateAppListener(Lcom/helpshift/support/AlertToRateAppListener;)V

    .line 1246
    new-instance p1, Landroid/content/Intent;

    sget-object v0, Lcom/helpshift/support/SupportInternal;->context:Landroid/content/Context;

    const-class v1, Lcom/helpshift/support/HSReview;

    invoke-direct {p1, v0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v0, "disableReview"

    const/4 v1, 0x0

    .line 1247
    invoke-virtual {p1, v0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    const-string v0, "rurl"

    .line 1248
    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {p1, v0, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const/high16 p0, 0x10000000

    .line 1249
    invoke-virtual {p1, p0}, Landroid/content/Intent;->setFlags(I)Landroid/content/Intent;

    .line 1250
    sget-object p0, Lcom/helpshift/support/SupportInternal;->context:Landroid/content/Context;

    invoke-virtual {p0, p1}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    return-void

    :cond_2
    :goto_0
    if-eqz p1, :cond_3

    const/4 p0, 0x3

    .line 1239
    invoke-interface {p1, p0}, Lcom/helpshift/support/AlertToRateAppListener;->onAction(I)V

    :cond_3
    return-void
.end method

.method public static showConversation(Landroid/app/Activity;Ljava/util/Map;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    const-string v0, "updateMetaData"

    const-string v1, ""

    .line 562
    invoke-static {v0, v1}, Lcom/helpshift/PluginEventBridge;->sendMessage(Ljava/lang/String;Ljava/lang/String;)V

    .line 564
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p1}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    const-string p1, "Helpshift_SupportInter"

    const-string v1, "Show conversation : "

    const/4 v2, 0x1

    .line 566
    new-array v3, v2, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v4, "Config"

    invoke-static {v4, v0}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromMap(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v4

    const/4 v5, 0x0

    aput-object v4, v3, v5

    invoke-static {p1, v1, v3}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 567
    new-instance p1, Landroid/content/Intent;

    const-class v1, Lcom/helpshift/support/activities/ParentActivity;

    invoke-direct {p1, p0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "support_mode"

    .line 568
    invoke-virtual {p1, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    const-string v1, "decomp"

    .line 569
    invoke-virtual {p1, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    .line 570
    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->removeShowConversationUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/content/Intent;->putExtras(Landroid/os/Bundle;)Landroid/content/Intent;

    const-string v0, "showInFullScreen"

    .line 571
    invoke-static {p0}, Lcom/helpshift/util/ActivityUtil;->isFullScreen(Landroid/app/Activity;)Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {p1, v0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    const-string v0, "isRoot"

    .line 572
    invoke-virtual {p1, v0, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    const-string v0, "search_performed"

    .line 573
    invoke-virtual {p1, v0, v5}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    .line 574
    invoke-virtual {p0, p1}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static showDynamicForm(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;)V"
        }
    .end annotation

    const-string v0, "Helpshift_SupportInter"

    const-string v1, "Show dynamic form"

    .line 969
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "updateMetaData"

    const-string v1, ""

    .line 971
    invoke-static {v0, v1}, Lcom/helpshift/PluginEventBridge;->sendMessage(Ljava/lang/String;Ljava/lang/String;)V

    .line 973
    new-instance v0, Landroid/content/Intent;

    const-class v1, Lcom/helpshift/support/activities/ParentActivity;

    invoke-direct {v0, p0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    .line 974
    invoke-static {p2}, Lcom/helpshift/support/flows/DynamicFormFlowListHolder;->setFlowList(Ljava/util/List;)V

    const-string p2, "showInFullScreen"

    .line 975
    invoke-static {p0}, Lcom/helpshift/util/ActivityUtil;->isFullScreen(Landroid/app/Activity;)Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {v0, p2, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    const-string p2, "support_mode"

    const/4 v1, 0x4

    .line 976
    invoke-virtual {v0, p2, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    const-string p2, "decomp"

    const/4 v1, 0x1

    .line 977
    invoke-virtual {v0, p2, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    const-string p2, "flow_title"

    .line 978
    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p2, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 979
    invoke-virtual {p0, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static showFAQSection(Landroid/app/Activity;Ljava/lang/String;)V
    .locals 1

    .line 598
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, p1, v0}, Lcom/helpshift/support/SupportInternal;->showFAQSection(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public static showFAQSection(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 697
    invoke-static {p1}, Lcom/helpshift/support/SupportInternal;->isValidPublishId(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 p1, 0x0

    .line 700
    :cond_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    const-string p2, "Helpshift_SupportInter"

    .line 702
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Show FAQ section : Publish Id : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x1

    new-array v3, v2, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const/4 v4, 0x0

    const-string v5, "Config"

    .line 703
    invoke-static {v5, v0}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromMap(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v5

    aput-object v5, v3, v4

    .line 702
    invoke-static {p2, v1, v3}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    const-string p2, "updateMetaData"

    const-string v1, ""

    .line 705
    invoke-static {p2, v1}, Lcom/helpshift/PluginEventBridge;->sendMessage(Ljava/lang/String;Ljava/lang/String;)V

    .line 707
    new-instance p2, Landroid/content/Intent;

    const-class v1, Lcom/helpshift/support/activities/ParentActivity;

    invoke-direct {p2, p0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "support_mode"

    const/4 v3, 0x2

    .line 708
    invoke-virtual {p2, v1, v3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    .line 709
    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->removeFAQFlowUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object v0

    invoke-virtual {p2, v0}, Landroid/content/Intent;->putExtras(Landroid/os/Bundle;)Landroid/content/Intent;

    const-string v0, "sectionPublishId"

    .line 710
    invoke-virtual {p2, v0, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p1, "showInFullScreen"

    .line 711
    invoke-static {p0}, Lcom/helpshift/util/ActivityUtil;->isFullScreen(Landroid/app/Activity;)Ljava/lang/Boolean;

    move-result-object v0

    invoke-virtual {p2, p1, v0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    const-string p1, "decomp"

    .line 712
    invoke-virtual {p2, p1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    const-string p1, "isRoot"

    .line 713
    invoke-virtual {p2, p1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    .line 714
    invoke-virtual {p0, p2}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static showFAQs(Landroid/app/Activity;Ljava/util/Map;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 954
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p1}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    const-string p1, "Helpshift_SupportInter"

    const-string v1, "Show FAQs : "

    const/4 v2, 0x1

    .line 956
    new-array v3, v2, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v4, "Config"

    invoke-static {v4, v0}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromMap(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v4

    const/4 v5, 0x0

    aput-object v4, v3, v5

    invoke-static {p1, v1, v3}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    const-string p1, "updateMetaData"

    const-string v1, ""

    .line 958
    invoke-static {p1, v1}, Lcom/helpshift/PluginEventBridge;->sendMessage(Ljava/lang/String;Ljava/lang/String;)V

    .line 960
    new-instance p1, Landroid/content/Intent;

    const-class v1, Lcom/helpshift/support/activities/ParentActivity;

    invoke-direct {p1, p0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    .line 961
    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->removeFAQFlowUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/content/Intent;->putExtras(Landroid/os/Bundle;)Landroid/content/Intent;

    const-string v0, "showInFullScreen"

    .line 962
    invoke-static {p0}, Lcom/helpshift/util/ActivityUtil;->isFullScreen(Landroid/app/Activity;)Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {p1, v0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    const-string v0, "decomp"

    .line 963
    invoke-virtual {p1, v0, v5}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    const-string v0, "isRoot"

    .line 964
    invoke-virtual {p1, v0, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    .line 965
    invoke-virtual {p0, p1}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static showSingleFAQ(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 785
    invoke-static {p1}, Lcom/helpshift/support/SupportInternal;->isValidPublishId(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 p1, 0x0

    .line 789
    :cond_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    const-string p2, "Helpshift_SupportInter"

    .line 791
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Show single FAQ : Publish Id : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x1

    new-array v3, v2, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const/4 v4, 0x0

    const-string v5, "Config"

    .line 792
    invoke-static {v5, v0}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromMap(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v5

    aput-object v5, v3, v4

    .line 791
    invoke-static {p2, v1, v3}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    const-string p2, "updateMetaData"

    const-string v1, ""

    .line 794
    invoke-static {p2, v1}, Lcom/helpshift/PluginEventBridge;->sendMessage(Ljava/lang/String;Ljava/lang/String;)V

    .line 796
    new-instance p2, Landroid/content/Intent;

    const-class v1, Lcom/helpshift/support/activities/ParentActivity;

    invoke-direct {p2, p0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "support_mode"

    const/4 v3, 0x3

    .line 797
    invoke-virtual {p2, v1, v3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    .line 798
    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->removeFAQFlowUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object v0

    invoke-virtual {p2, v0}, Landroid/content/Intent;->putExtras(Landroid/os/Bundle;)Landroid/content/Intent;

    const-string v0, "questionPublishId"

    .line 799
    invoke-virtual {p2, v0, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p1, "showInFullScreen"

    .line 800
    invoke-static {p0}, Lcom/helpshift/util/ActivityUtil;->isFullScreen(Landroid/app/Activity;)Ljava/lang/Boolean;

    move-result-object v0

    invoke-virtual {p2, p1, v0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    const-string p1, "decomp"

    .line 801
    invoke-virtual {p2, p1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    const-string p1, "isRoot"

    .line 802
    invoke-virtual {p2, p1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    .line 803
    invoke-virtual {p0, p2}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method private static updateCustomIssueFieldData(Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    const-string v0, "hs-custom-issue-field"

    .line 1058
    invoke-interface {p0, v0}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "hs-custom-issue-field"

    .line 1059
    invoke-interface {p0, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    .line 1060
    instance-of v0, p0, Ljava/util/Map;

    if-eqz v0, :cond_0

    .line 1062
    :try_start_0
    check-cast p0, Ljava/util/Map;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    const-string v0, "Helpshift_SupportInter"

    const-string v1, "Exception while parsing CIF data : "

    .line 1065
    invoke-static {v0, v1, p0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    const/4 p0, 0x0

    .line 1069
    :goto_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getCustomIssueFieldDM()Lcom/helpshift/cif/CustomIssueFieldDM;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/cif/CustomIssueFieldDM;->setCustomIssueFieldData(Ljava/util/Map;)V

    return-void
.end method
