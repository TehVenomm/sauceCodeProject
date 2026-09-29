.class public interface abstract Lcom/helpshift/CoreApi;
.super Ljava/lang/Object;
.source "CoreApi.java"


# virtual methods
.method public abstract clearAnonymousUser()Z
.end method

.method public abstract fetchServerConfig()V
.end method

.method public abstract getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;
.end method

.method public abstract getActiveConversationOrPreIssue()Lcom/helpshift/conversation/activeconversation/model/Conversation;
.end method

.method public abstract getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;
.end method

.method public abstract getAttachmentFileManagerDM()Lcom/helpshift/common/domain/AttachmentFileManagerDM;
.end method

.method public abstract getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;
.end method

.method public abstract getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;
.end method

.method public abstract getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;
.end method

.method public abstract getConversationInfoViewModel(Lcom/helpshift/conversation/activeconversation/ConversationInfoRenderer;)Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;
.end method

.method public abstract getConversationalViewModel(ZLjava/lang/Long;Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;Z)Lcom/helpshift/conversation/viewmodel/ConversationalVM;
.end method

.method public abstract getCryptoDM()Lcom/helpshift/crypto/CryptoDM;
.end method

.method public abstract getCustomIssueFieldDM()Lcom/helpshift/cif/CustomIssueFieldDM;
.end method

.method public abstract getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;
.end method

.method public abstract getDomain()Lcom/helpshift/common/domain/Domain;
.end method

.method public abstract getErrorReportsDM()Lcom/helpshift/logger/ErrorReportsDM;
.end method

.method public abstract getFaqDM()Lcom/helpshift/faq/FaqsDM;
.end method

.method public abstract getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;
.end method

.method public abstract getMetaDataDM()Lcom/helpshift/meta/MetaDataDM;
.end method

.method public abstract getNewConversationViewModel(Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;)Lcom/helpshift/conversation/viewmodel/NewConversationVM;
.end method

.method public abstract getNotificationCountAsync(Lcom/helpshift/common/FetchDataFromThread;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/FetchDataFromThread<",
            "Lcom/helpshift/util/ValuePair<",
            "Ljava/lang/Integer;",
            "Ljava/lang/Boolean;",
            ">;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract getNotificationCountSync()I
.end method

.method public abstract getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;
.end method

.method public abstract getScreenshotPreviewModel(Lcom/helpshift/conversation/activeconversation/ScreenshotPreviewRenderer;)Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;
.end method

.method public abstract getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;
.end method

.method public abstract getUserSetupVM(Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;)Lcom/helpshift/conversation/usersetup/UserSetupVM;
.end method

.method public abstract handlePushNotification(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract isActiveConversationActionable()Z
.end method

.method public abstract isSDKSessionActive()Z
.end method

.method public abstract login(Lcom/helpshift/HelpshiftUser;)Z
.end method

.method public abstract logout()Z
.end method

.method public abstract onSDKSessionEnded()V
.end method

.method public abstract onSDKSessionStarted()V
.end method

.method public abstract refreshPoller()V
.end method

.method public abstract resetPreIssueConversations()V
.end method

.method public abstract resetUsersSyncStatusAndStartSetupForActiveUser()V
.end method

.method public abstract sendAnalyticsEvent()V
.end method

.method public abstract sendAppStartEvent()V
.end method

.method public abstract sendFailedApiCalls()V
.end method

.method public abstract sendRequestIdsForSuccessfulApiCalls()V
.end method

.method public abstract setDelegateListener(Lcom/helpshift/delegate/RootDelegate;)V
.end method

.method public abstract setNameAndEmail(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract setPushToken(Ljava/lang/String;)V
.end method

.method public abstract updateApiConfig(Lcom/helpshift/configuration/dto/RootApiConfig;)V
.end method

.method public abstract updateInstallConfig(Lcom/helpshift/configuration/dto/RootInstallConfig;)V
.end method
