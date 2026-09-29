.class public Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;
.super Ljava/util/Observable;
.source "SDKConfigurationDM.java"


# static fields
.field public static final ALLOW_USER_ATTACHMENTS:Ljava/lang/String; = "allowUserAttachments"

.field public static final API_KEY:Ljava/lang/String; = "apiKey"

.field public static final APP_REVIEWED:Ljava/lang/String; = "app_reviewed"

.field public static final AUTO_FILL_FIRST_PREISSUE_MESSAGE:Ljava/lang/String; = "autoFillFirstPreIssueMessage"

.field public static final BREADCRUMB_LIMIT:Ljava/lang/String; = "breadcrumbLimit"

.field public static final CONVERSATIONAL_ISSUE_FILING:Ljava/lang/String; = "conversationalIssueFiling"

.field public static final CONVERSATION_GREETING_MESSAGE:Ljava/lang/String; = "conversationGreetingMessage"

.field public static final CONVERSATION_PRE_FILL_TEXT:Ljava/lang/String; = "conversationPrefillText"

.field public static final CUSTOMER_SATISFACTION_SURVEY:Ljava/lang/String; = "customerSatisfactionSurvey"

.field public static final DEBUG_LOG_LIMIT:Ljava/lang/String; = "debugLogLimit"

.field public static final DEFAULT_FALLBACK_LANGUAGE_ENABLE:Ljava/lang/String; = "defaultFallbackLanguageEnable"

.field public static final DISABLE_ANIMATION:Ljava/lang/String; = "disableAnimations"

.field public static final DISABLE_APP_LAUNCH_EVENT:Ljava/lang/String; = "disableAppLaunchEvent"

.field public static final DISABLE_ERROR_LOGGING:Ljava/lang/String; = "disableErrorLogging"

.field public static final DISABLE_IN_APP_CONVERSATION:Ljava/lang/String; = "disableInAppConversation"

.field public static final DOMAIN_NAME:Ljava/lang/String; = "domainName"

.field public static final ENABLE_CONTACT_US:Ljava/lang/String; = "enableContactUs"

.field public static final ENABLE_DEFAULT_CONVERSATIONAL_FILING:Ljava/lang/String; = "enableDefaultConversationalFiling"

.field public static final ENABLE_FULL_PRIVACY:Ljava/lang/String; = "fullPrivacy"

.field public static final ENABLE_IN_APP_NOTIFICATION:Ljava/lang/String; = "enableInAppNotification"

.field public static final ENABLE_TYPING_INDICATOR:Ljava/lang/String; = "enableTypingIndicator"

.field public static final ENABLE_TYPING_INDICATOR_AGENT:Ljava/lang/String; = "enableTypingIndicatorAgent"

.field public static final FONT_PATH:Ljava/lang/String; = "fontPath"

.field public static final GOTO_CONVERSATION_AFTER_CONTACT_US:Ljava/lang/String; = "gotoConversationAfterContactUs"

.field public static final HELPSHIFT_BRANDING_DISABLE_AGENT:Ljava/lang/String; = "disableHelpshiftBrandingAgent"

.field public static final HELPSHIFT_BRANDING_DISABLE_INSTALL:Ljava/lang/String; = "disableHelpshiftBranding"

.field public static final HIDE_NAME_AND_EMAIL:Ljava/lang/String; = "hideNameAndEmail"

.field public static final INBOX_POLLING_ENABLE:Ljava/lang/String; = "inboxPollingEnable"

.field public static final INITIAL_USER_MESSAGE_TO_AUTOSEND_IN_PREISSUE:Ljava/lang/String; = "initialUserMessageToAutoSendInPreissue"

.field public static final LAST_SUCCESSFUL_CONFIG_FETCH_TIME:Ljava/lang/String; = "lastSuccessfulConfigFetchTime"

.field private static final MINIMUM_PERIODIC_FETCH_INTERVAL:Ljava/lang/Long;

.field private static final MINIMUM_PREISSUE_RESET_INTERVAL:Ljava/lang/Long;

.field public static final NOTIFICATION_ICON_ID:Ljava/lang/String; = "notificationIconId"

.field public static final NOTIFICATION_LARGE_ICON_ID:Ljava/lang/String; = "notificationLargeIconId"

.field public static final NOTIFICATION_MUTE_ENABLE:Ljava/lang/String; = "notificationMute"

.field public static final NOTIFICATION_SOUND_ID:Ljava/lang/String; = "notificationSoundId"

.field public static final PERIODIC_FETCH_INTERVAL:Ljava/lang/String; = "periodicFetchInterval"

.field public static final PERIODIC_REVIEW_ENABLED:Ljava/lang/String; = "periodicReviewEnabled"

.field public static final PERIODIC_REVIEW_INTERVAL:Ljava/lang/String; = "periodicReviewInterval"

.field public static final PERIODIC_REVIEW_TYPE:Ljava/lang/String; = "periodicReviewType"

.field public static final PLATFORM_ID:Ljava/lang/String; = "platformId"

.field public static final PLUGIN_VERSION:Ljava/lang/String; = "pluginVersion"

.field public static final PREISSUE_RESET_INTERVAL:Ljava/lang/String; = "preissueResetInterval"

.field public static final PROFILE_FORM_ENABLE:Ljava/lang/String; = "profileFormEnable"

.field public static final REQUIRE_EMAIL:Ljava/lang/String; = "requireEmail"

.field public static final REQUIRE_NAME_AND_EMAIL:Ljava/lang/String; = "requireNameAndEmail"

.field public static final REVIEW_URL:Ljava/lang/String; = "reviewUrl"

.field public static final RUNTIME_VERSION:Ljava/lang/String; = "runtimeVersion"

.field public static final SDK_LANGUAGE:Ljava/lang/String; = "sdkLanguage"

.field public static final SDK_TYPE:Ljava/lang/String; = "sdkType"

.field public static final SHOULD_SHOW_CONVERSATION_HISTORY_AGENT:Ljava/lang/String; = "showConversationHistoryAgent"

.field public static final SHOW_AGENT_NAME:Ljava/lang/String; = "showAgentName"

.field public static final SHOW_CONVERSATION_INFO_SCREEN:Ljava/lang/String; = "showConversationInfoScreen"

.field public static final SHOW_CONVERSATION_RESOLUTION_QUESTION_AGENT:Ljava/lang/String; = "showConversationResolutionQuestionAgent"

.field public static final SHOW_CONVERSATION_RESOLUTION_QUESTION_API:Ljava/lang/String; = "showConversationResolutionQuestion"

.field public static final SHOW_SEARCH_ON_NEW_CONVERSATION:Ljava/lang/String; = "showSearchOnNewConversation"

.field public static final SUPPORT_NOTIFICATION_CHANNEL_ID:Ljava/lang/String; = "supportNotificationChannelId"

.field private static final TAG:Ljava/lang/String; = "Helpshift_SDKConfigDM"


# instance fields
.field private final domain:Lcom/helpshift/common/domain/Domain;

.field private final kvStore:Lcom/helpshift/common/platform/KVStore;

.field private final platform:Lcom/helpshift/common/platform/Platform;

.field private final responseParser:Lcom/helpshift/common/platform/network/ResponseParser;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    const-wide/16 v0, 0x3c

    .line 96
    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    sput-object v0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->MINIMUM_PERIODIC_FETCH_INTERVAL:Ljava/lang/Long;

    const-wide/32 v0, 0xa8c0

    .line 97
    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    sput-object v0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->MINIMUM_PREISSUE_RESET_INTERVAL:Ljava/lang/Long;

    return-void
.end method

.method public constructor <init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V
    .locals 0

    .line 106
    invoke-direct {p0}, Ljava/util/Observable;-><init>()V

    .line 107
    iput-object p1, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 108
    iput-object p2, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 109
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->responseParser:Lcom/helpshift/common/platform/network/ResponseParser;

    .line 110
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getKVStore()Lcom/helpshift/common/platform/KVStore;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    return-void
.end method

.method private removeNullValues(Ljava/util/Map;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)V"
        }
    .end annotation

    .line 381
    invoke-interface {p1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    .line 382
    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 383
    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map$Entry;

    .line 384
    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v0

    if-nez v0, :cond_0

    .line 385
    invoke-interface {p1}, Ljava/util/Iterator;->remove()V

    goto :goto_0

    :cond_1
    return-void
.end method

.method private updateLastSuccessfulConfigFetchTime()V
    .locals 6

    .line 207
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "lastSuccessfulConfigFetchTime"

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v2

    const-wide/16 v4, 0x3e8

    div-long/2addr v2, v4

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/platform/KVStore;->setLong(Ljava/lang/String;Ljava/lang/Long;)V

    return-void
.end method

.method private updateUserConfig(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/configuration/response/RootServerConfig;Lcom/helpshift/account/domainmodel/UserManagerDM;)V
    .locals 0

    .line 167
    iget-boolean p2, p2, Lcom/helpshift/configuration/response/RootServerConfig;->issueExists:Z

    invoke-virtual {p3, p1, p2}, Lcom/helpshift/account/domainmodel/UserManagerDM;->updateIssueExists(Lcom/helpshift/account/domainmodel/UserDM;Z)V

    return-void
.end method


# virtual methods
.method public fetchServerConfig(Lcom/helpshift/account/domainmodel/UserManagerDM;)Lcom/helpshift/configuration/response/RootServerConfig;
    .locals 5

    .line 124
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    .line 125
    sget-object v1, Lcom/helpshift/common/domain/network/NetworkConstants;->SUPPORT_CONFIG_ROUTE:Ljava/lang/String;

    .line 126
    new-instance v2, Lcom/helpshift/common/domain/network/GETNetwork;

    iget-object v3, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v4, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v2, v1, v3, v4}, Lcom/helpshift/common/domain/network/GETNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 127
    new-instance v3, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v4, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v3, v2, v4}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 128
    new-instance v2, Lcom/helpshift/common/domain/network/ETagNetwork;

    iget-object v4, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v2, v3, v4, v1}, Lcom/helpshift/common/domain/network/ETagNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;Ljava/lang/String;)V

    .line 129
    new-instance v1, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v1, v2}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 130
    new-instance v2, Lcom/helpshift/common/platform/network/RequestData;

    invoke-static {p1}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserManagerDM;)Ljava/util/HashMap;

    move-result-object v3

    invoke-direct {v2, v3}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    .line 132
    :try_start_0
    invoke-interface {v1, v2}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object v1

    .line 134
    iget-object v2, v1, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    if-nez v2, :cond_0

    const-string p1, "Helpshift_SDKConfigDM"

    const-string v0, "SDK config data fetched but nothing to update."

    .line 135
    invoke-static {p1, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 136
    invoke-direct {p0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->updateLastSuccessfulConfigFetchTime()V

    const/4 p1, 0x0

    return-object p1

    :cond_0
    const-string v2, "Helpshift_SDKConfigDM"

    const-string v3, "SDK config data updated successfully"

    .line 140
    invoke-static {v2, v3}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 141
    iget-object v2, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->responseParser:Lcom/helpshift/common/platform/network/ResponseParser;

    iget-object v1, v1, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    invoke-interface {v2, v1}, Lcom/helpshift/common/platform/network/ResponseParser;->parseConfigResponse(Ljava/lang/String;)Lcom/helpshift/configuration/response/RootServerConfig;

    move-result-object v1

    .line 143
    invoke-virtual {p0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->updateServerConfig(Lcom/helpshift/configuration/response/RootServerConfig;)V

    .line 144
    invoke-direct {p0, v0, v1, p1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->updateUserConfig(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/configuration/response/RootServerConfig;Lcom/helpshift/account/domainmodel/UserManagerDM;)V

    .line 145
    invoke-direct {p0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->updateLastSuccessfulConfigFetchTime()V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v1

    :catch_0
    move-exception p1

    .line 151
    iget-object v0, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    instance-of v0, v0, Lcom/helpshift/common/exception/NetworkException;

    if-eqz v0, :cond_1

    iget-object v0, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    check-cast v0, Lcom/helpshift/common/exception/NetworkException;

    iget v0, v0, Lcom/helpshift/common/exception/NetworkException;->serverStatusCode:I

    sget-object v1, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->CONTENT_UNCHANGED:Ljava/lang/Integer;

    .line 152
    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v1

    if-ne v0, v1, :cond_1

    .line 153
    invoke-direct {p0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->updateLastSuccessfulConfigFetchTime()V

    .line 155
    :cond_1
    throw p1
.end method

.method public getBoolean(Ljava/lang/String;)Z
    .locals 3

    .line 216
    invoke-virtual {p1}, Ljava/lang/String;->hashCode()I

    move-result v0

    const/4 v1, 0x1

    const/4 v2, 0x0

    sparse-switch v0, :sswitch_data_0

    goto :goto_0

    :sswitch_0
    const-string v0, "showAgentName"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_1

    :sswitch_1
    const-string v0, "allowUserAttachments"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x6

    goto :goto_1

    :sswitch_2
    const-string v0, "defaultFallbackLanguageEnable"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x3

    goto :goto_1

    :sswitch_3
    const-string v0, "enableInAppNotification"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x2

    goto :goto_1

    :sswitch_4
    const-string v0, "enableTypingIndicatorAgent"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x5

    goto :goto_1

    :sswitch_5
    const-string v0, "showConversationResolutionQuestionAgent"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x4

    goto :goto_1

    :sswitch_6
    const-string v0, "profileFormEnable"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x0

    goto :goto_1

    :sswitch_7
    const-string v0, "conversationalIssueFiling"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x7

    goto :goto_1

    :cond_0
    :goto_0
    const/4 v0, -0x1

    :goto_1
    packed-switch v0, :pswitch_data_0

    const/4 v1, 0x0

    goto :goto_2

    .line 227
    :pswitch_0
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "enableDefaultConversationalFiling"

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/platform/KVStore;->getBoolean(Ljava/lang/String;Ljava/lang/Boolean;)Ljava/lang/Boolean;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v1

    .line 233
    :goto_2
    :pswitch_1
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-interface {v0, p1, v1}, Lcom/helpshift/common/platform/KVStore;->getBoolean(Ljava/lang/String;Ljava/lang/Boolean;)Ljava/lang/Boolean;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    return p1

    nop

    :sswitch_data_0
    .sparse-switch
        -0x6583db5c -> :sswitch_7
        -0x23465e10 -> :sswitch_6
        -0x193ffecd -> :sswitch_5
        -0x15d84a50 -> :sswitch_4
        -0x142b457c -> :sswitch_3
        0x4b466e1e -> :sswitch_2
        0x54dac45c -> :sswitch_1
        0x745f78b3 -> :sswitch_0
    .end sparse-switch

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_1
        :pswitch_1
        :pswitch_1
        :pswitch_1
        :pswitch_1
        :pswitch_1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public getEnableContactUs()Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;
    .locals 3

    .line 354
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "enableContactUs"

    const/4 v2, 0x0

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/platform/KVStore;->getInt(Ljava/lang/String;Ljava/lang/Integer;)Ljava/lang/Integer;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    invoke-static {v0}, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->fromInt(I)Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    move-result-object v0

    return-object v0
.end method

.method public getInt(Ljava/lang/String;)Ljava/lang/Integer;
    .locals 2

    .line 238
    invoke-virtual {p1}, Ljava/lang/String;->hashCode()I

    move-result v0

    const v1, -0x444e5b6

    if-eq v0, v1, :cond_1

    const v1, 0x5285b578

    if-eq v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const-string v0, "breadcrumbLimit"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_2

    const/4 v0, 0x1

    goto :goto_1

    :cond_1
    const-string v0, "debugLogLimit"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_2

    const/4 v0, 0x0

    goto :goto_1

    :cond_2
    :goto_0
    const/4 v0, -0x1

    :goto_1
    packed-switch v0, :pswitch_data_0

    const/4 v0, 0x0

    goto :goto_2

    :pswitch_0
    const/16 v0, 0x64

    .line 241
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    .line 247
    :goto_2
    iget-object v1, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    invoke-interface {v1, p1, v0}, Lcom/helpshift/common/platform/KVStore;->getInt(Ljava/lang/String;Ljava/lang/Integer;)Ljava/lang/Integer;

    move-result-object p1

    return-object p1

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method public getLastSuccessfulConfigFetchTime()Ljava/lang/Long;
    .locals 4

    .line 211
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "lastSuccessfulConfigFetchTime"

    const-wide/16 v2, 0x0

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/platform/KVStore;->getLong(Ljava/lang/String;Ljava/lang/Long;)Ljava/lang/Long;

    move-result-object v0

    return-object v0
.end method

.method public getMinimumConversationDescriptionLength()I
    .locals 1

    .line 377
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getMinimumConversationDescriptionLength()I

    move-result v0

    return v0
.end method

.method public getPeriodicFetchInterval()J
    .locals 4

    .line 416
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "periodicFetchInterval"

    const-wide/16 v2, 0x0

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/platform/KVStore;->getLong(Ljava/lang/String;Ljava/lang/Long;)Ljava/lang/Long;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    .line 417
    sget-object v2, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->MINIMUM_PERIODIC_FETCH_INTERVAL:Ljava/lang/Long;

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-static {v0, v1, v2, v3}, Ljava/lang/Math;->max(JJ)J

    move-result-wide v0

    return-wide v0
.end method

.method public getPeriodicReview()Lcom/helpshift/configuration/response/PeriodicReview;
    .locals 5

    .line 268
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "periodicReviewEnabled"

    const/4 v2, 0x0

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    invoke-interface {v0, v1, v3}, Lcom/helpshift/common/platform/KVStore;->getBoolean(Ljava/lang/String;Ljava/lang/Boolean;)Ljava/lang/Boolean;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    .line 269
    iget-object v1, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v3, "periodicReviewInterval"

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v1, v3, v2}, Lcom/helpshift/common/platform/KVStore;->getInt(Ljava/lang/String;Ljava/lang/Integer;)Ljava/lang/Integer;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v1

    .line 270
    iget-object v2, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v3, "periodicReviewType"

    const-string v4, ""

    invoke-interface {v2, v3, v4}, Lcom/helpshift/common/platform/KVStore;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 271
    new-instance v3, Lcom/helpshift/configuration/response/PeriodicReview;

    invoke-direct {v3, v0, v1, v2}, Lcom/helpshift/configuration/response/PeriodicReview;-><init>(ZILjava/lang/String;)V

    return-object v3
.end method

.method public getPreissueResetInterval()J
    .locals 4

    .line 427
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "preissueResetInterval"

    const-wide/16 v2, 0x0

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/platform/KVStore;->getLong(Ljava/lang/String;Ljava/lang/Long;)Ljava/lang/Long;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    .line 428
    sget-object v2, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->MINIMUM_PREISSUE_RESET_INTERVAL:Ljava/lang/Long;

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-static {v0, v1, v2, v3}, Ljava/lang/Math;->max(JJ)J

    move-result-wide v0

    return-wide v0
.end method

.method public getString(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    .line 252
    invoke-virtual {p1}, Ljava/lang/String;->hashCode()I

    move-result v0

    const v1, -0x144c264e

    if-eq v0, v1, :cond_2

    const v1, 0x1d62f6f7

    if-eq v0, v1, :cond_1

    const v1, 0x741d1294

    if-eq v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const-string v0, "sdkType"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_3

    const/4 v0, 0x2

    goto :goto_1

    :cond_1
    const-string v0, "reviewUrl"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_3

    const/4 v0, 0x0

    goto :goto_1

    :cond_2
    const-string v0, "sdkLanguage"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_3

    const/4 v0, 0x1

    goto :goto_1

    :cond_3
    :goto_0
    const/4 v0, -0x1

    :goto_1
    packed-switch v0, :pswitch_data_0

    const/4 v0, 0x0

    goto :goto_2

    :pswitch_0
    const-string v0, "android"

    goto :goto_2

    :pswitch_1
    const-string v0, ""

    .line 264
    :goto_2
    iget-object v1, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    invoke-interface {v1, p1, v0}, Lcom/helpshift/common/platform/KVStore;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public isHelpshiftBrandingDisabled()Z
    .locals 4

    .line 309
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "disableHelpshiftBranding"

    const/4 v2, 0x0

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    invoke-interface {v0, v1, v3}, Lcom/helpshift/common/platform/KVStore;->getBoolean(Ljava/lang/String;Ljava/lang/Boolean;)Ljava/lang/Boolean;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "disableHelpshiftBrandingAgent"

    .line 310
    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    invoke-interface {v0, v1, v3}, Lcom/helpshift/common/platform/KVStore;->getBoolean(Ljava/lang/String;Ljava/lang/Boolean;)Ljava/lang/Boolean;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-eqz v0, :cond_1

    :cond_0
    const/4 v2, 0x1

    :cond_1
    return v2
.end method

.method public setAppReviewed(Z)V
    .locals 2

    .line 358
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "app_reviewed"

    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    invoke-interface {v0, v1, p1}, Lcom/helpshift/common/platform/KVStore;->setBoolean(Ljava/lang/String;Ljava/lang/Boolean;)V

    return-void
.end method

.method public setSdkLanguage(Ljava/lang/String;)V
    .locals 2

    .line 362
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "sdkLanguage"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/common/platform/KVStore;->setString(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public shouldAutoFillPreissueFirstMessage()Z
    .locals 3

    .line 432
    iget-object v0, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "autoFillFirstPreIssueMessage"

    const/4 v2, 0x0

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/platform/KVStore;->getBoolean(Ljava/lang/String;Ljava/lang/Boolean;)Ljava/lang/Boolean;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    return v0
.end method

.method public shouldCreateConversationAnonymously()Z
    .locals 1

    const-string v0, "fullPrivacy"

    .line 370
    invoke-virtual {p0, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_2

    const-string v0, "requireNameAndEmail"

    .line 371
    invoke-virtual {p0, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "hideNameAndEmail"

    .line 372
    invoke-virtual {p0, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_1

    :cond_0
    const-string v0, "profileFormEnable"

    .line 373
    invoke-virtual {p0, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_1

    goto :goto_0

    :cond_1
    const/4 v0, 0x0

    goto :goto_1

    :cond_2
    :goto_0
    const/4 v0, 0x1

    :goto_1
    return v0
.end method

.method public shouldEnableTypingIndicator()Z
    .locals 1

    const-string v0, "enableTypingIndicatorAgent"

    .line 366
    invoke-virtual {p0, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_1

    const-string v0, "enableTypingIndicator"

    invoke-virtual {p0, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v0, 0x1

    :goto_1
    return v0
.end method

.method public shouldShowConversationHistory()Z
    .locals 1

    const-string v0, "showConversationHistoryAgent"

    .line 399
    invoke-virtual {p0, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "conversationalIssueFiling"

    .line 401
    invoke-virtual {p0, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "fullPrivacy"

    .line 403
    invoke-virtual {p0, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    xor-int/lit8 v0, v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public shouldShowConversationResolutionQuestion()Z
    .locals 1

    const-string v0, "showConversationResolutionQuestionAgent"

    .line 314
    invoke-virtual {p0, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_1

    const-string v0, "showConversationResolutionQuestion"

    .line 315
    invoke-virtual {p0, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v0, 0x1

    :goto_1
    return v0
.end method

.method public updateApiConfig(Lcom/helpshift/configuration/dto/RootApiConfig;)V
    .locals 4

    .line 323
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "conversationPrefillText"

    .line 324
    iget-object v2, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->conversationPrefillText:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "initialUserMessageToAutoSendInPreissue"

    .line 325
    iget-object v2, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->initialUserMessageToAutoSend:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 330
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    const-string v2, "fullPrivacy"

    .line 331
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->enableFullPrivacy:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "hideNameAndEmail"

    .line 332
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->hideNameAndEmail:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "requireEmail"

    .line 333
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->requireEmail:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "showSearchOnNewConversation"

    .line 334
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->showSearchOnNewConversation:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "gotoConversationAfterContactUs"

    .line 335
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->gotoConversationAfterContactUs:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "showConversationResolutionQuestion"

    .line 336
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->showConversationResolutionQuestion:Ljava/lang/Boolean;

    .line 337
    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "showConversationInfoScreen"

    .line 338
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->showConversationInfoScreen:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "enableTypingIndicator"

    .line 339
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->enableTypingIndicator:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 341
    iget-object v2, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->enableContactUs:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    if-eqz v2, :cond_0

    const-string v2, "enableContactUs"

    .line 342
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->enableContactUs:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    invoke-virtual {v3}, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->getValue()I

    move-result v3

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    const-string v2, "enableDefaultConversationalFiling"

    .line 345
    iget-object p1, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->enableDefaultConversationalFiling:Ljava/lang/Boolean;

    invoke-interface {v1, v2, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 347
    invoke-direct {p0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->removeNullValues(Ljava/util/Map;)V

    .line 349
    invoke-interface {v1, v0}, Ljava/util/Map;->putAll(Ljava/util/Map;)V

    .line 350
    iget-object p1, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    invoke-interface {p1, v1}, Lcom/helpshift/common/platform/KVStore;->setKeyValues(Ljava/util/Map;)V

    return-void
.end method

.method public updateInstallConfig(Lcom/helpshift/configuration/dto/RootInstallConfig;)V
    .locals 4

    .line 279
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "supportNotificationChannelId"

    .line 280
    iget-object v2, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->supportNotificationChannelId:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "fontPath"

    .line 281
    iget-object v2, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->fontPath:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 286
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    const-string v2, "enableInAppNotification"

    .line 287
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->enableInAppNotification:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "defaultFallbackLanguageEnable"

    .line 288
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->enableDefaultFallbackLanguage:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "inboxPollingEnable"

    .line 289
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->enableInboxPolling:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "notificationMute"

    .line 290
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->enableNotificationMute:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "disableAnimations"

    .line 291
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->disableAnimations:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "disableHelpshiftBranding"

    .line 292
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->disableHelpshiftBranding:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "disableErrorLogging"

    .line 293
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->disableErrorLogging:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "disableAppLaunchEvent"

    .line 294
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->disableAppLaunchEvent:Ljava/lang/Boolean;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "notificationSoundId"

    .line 295
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->notificationSound:Ljava/lang/Integer;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "notificationIconId"

    .line 296
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->notificationIcon:Ljava/lang/Integer;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "notificationLargeIconId"

    .line 297
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->largeNotificationIcon:Ljava/lang/Integer;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "sdkType"

    .line 298
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->sdkType:Ljava/lang/String;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "pluginVersion"

    .line 299
    iget-object v3, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->pluginVersion:Ljava/lang/String;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "runtimeVersion"

    .line 300
    iget-object p1, p1, Lcom/helpshift/configuration/dto/RootInstallConfig;->runtimeVersion:Ljava/lang/String;

    invoke-interface {v1, v2, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 302
    invoke-direct {p0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->removeNullValues(Ljava/util/Map;)V

    .line 304
    invoke-interface {v1, v0}, Ljava/util/Map;->putAll(Ljava/util/Map;)V

    .line 305
    iget-object p1, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    invoke-interface {p1, v1}, Lcom/helpshift/common/platform/KVStore;->setKeyValues(Ljava/util/Map;)V

    return-void
.end method

.method public updateServerConfig(Lcom/helpshift/configuration/response/RootServerConfig;)V
    .locals 4

    .line 171
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "requireNameAndEmail"

    .line 172
    iget-boolean v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->requireNameAndEmail:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "profileFormEnable"

    .line 173
    iget-boolean v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->profileFormEnable:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "showAgentName"

    .line 174
    iget-boolean v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->showAgentName:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "customerSatisfactionSurvey"

    .line 175
    iget-boolean v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->customerSatisfactionSurvey:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "disableInAppConversation"

    .line 176
    iget-boolean v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->disableInAppConversation:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "disableHelpshiftBrandingAgent"

    .line 177
    iget-boolean v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->disableHelpshiftBranding:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "debugLogLimit"

    .line 178
    iget v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->debugLogLimit:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "breadcrumbLimit"

    .line 179
    iget v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->breadcrumbLimit:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "reviewUrl"

    .line 180
    iget-object v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->reviewUrl:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 182
    iget-object v1, p1, Lcom/helpshift/configuration/response/RootServerConfig;->periodicReview:Lcom/helpshift/configuration/response/PeriodicReview;

    if-nez v1, :cond_0

    .line 184
    new-instance v1, Lcom/helpshift/configuration/response/PeriodicReview;

    const/4 v2, 0x0

    const/4 v3, 0x0

    invoke-direct {v1, v3, v3, v2}, Lcom/helpshift/configuration/response/PeriodicReview;-><init>(ZILjava/lang/String;)V

    :cond_0
    const-string v2, "periodicReviewEnabled"

    .line 187
    iget-boolean v3, v1, Lcom/helpshift/configuration/response/PeriodicReview;->isEnabled:Z

    invoke-static {v3}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    invoke-interface {v0, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "periodicReviewInterval"

    .line 188
    iget v3, v1, Lcom/helpshift/configuration/response/PeriodicReview;->interval:I

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-interface {v0, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "periodicReviewType"

    .line 189
    iget-object v1, v1, Lcom/helpshift/configuration/response/PeriodicReview;->type:Ljava/lang/String;

    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "conversationGreetingMessage"

    .line 190
    iget-object v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->conversationGreetingMessage:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "conversationalIssueFiling"

    .line 191
    iget-boolean v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->conversationalIssueFiling:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "enableTypingIndicatorAgent"

    .line 192
    iget-boolean v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->enableTypingIndicator:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "showConversationResolutionQuestionAgent"

    .line 193
    iget-boolean v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->showConversationResolutionQuestion:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "showConversationHistoryAgent"

    .line 194
    iget-boolean v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->shouldShowConversationHistory:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "allowUserAttachments"

    .line 195
    iget-boolean v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->allowUserAttachments:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "periodicFetchInterval"

    .line 196
    iget-wide v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->periodicFetchInterval:J

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "preissueResetInterval"

    .line 197
    iget-wide v2, p1, Lcom/helpshift/configuration/response/RootServerConfig;->preissueResetInterval:J

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "autoFillFirstPreIssueMessage"

    .line 198
    iget-boolean p1, p1, Lcom/helpshift/configuration/response/RootServerConfig;->autoFillFirstPreissueMessage:Z

    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    invoke-interface {v0, v1, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 200
    iget-object p1, p0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->kvStore:Lcom/helpshift/common/platform/KVStore;

    invoke-interface {p1, v0}, Lcom/helpshift/common/platform/KVStore;->setKeyValues(Ljava/util/Map;)V

    .line 202
    invoke-virtual {p0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->setChanged()V

    .line 203
    invoke-virtual {p0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->notifyObservers()V

    return-void
.end method
