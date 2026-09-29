.class public Lcom/helpshift/conversation/viewmodel/ConversationalVM;
.super Ljava/lang/Object;
.source "ConversationalVM.java"

# interfaces
.implements Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;
.implements Lcom/helpshift/conversation/viewmodel/ListPickerVMCallback;
.implements Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;
.implements Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;
.implements Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;
.implements Ljava/util/Observer;


# static fields
.field public static final CREATE_NEW_PRE_ISSUE:Ljava/lang/String; = "create_new_pre_issue"

.field public static final NO_NETWORK_ERROR:I = 0x1

.field public static final POLL_FAILURE_ERROR:I = 0x2

.field private static final TAG:Ljava/lang/String; = "Helpshift_ConvsatnlVM"


# instance fields
.field attachImageButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

.field awaitingUserInputForBotStep:Z

.field private botMessageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

.field confirmationBoxViewState:Lcom/helpshift/widget/MutableBaseViewState;

.field final conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

.field conversationFooterViewState:Lcom/helpshift/widget/MutableConversationFooterViewState;

.field conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

.field domain:Lcom/helpshift/common/domain/Domain;

.field historyLoadingViewState:Lcom/helpshift/widget/MutableHistoryLoadingViewState;

.field protected isConversationRejected:Z

.field isInBetweenBotExecution:Z

.field isNetworkAvailable:Z

.field private isScreenCurrentlyVisible:Z

.field isShowingPollFailureError:Z

.field isUserReplyDraftClearedForBotChange:Z

.field private listPickerVM:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

.field messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

.field platform:Lcom/helpshift/common/platform/Platform;

.field renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

.field replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

.field replyButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

.field replyFieldViewState:Lcom/helpshift/widget/MutableReplyFieldViewState;

.field private retainMessageBoxOnUI:Z

.field scrollJumperViewState:Lcom/helpshift/widget/MutableScrollJumperViewState;

.field final sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

.field private showConversationHistory:Z

.field public final viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

.field widgetGateway:Lcom/helpshift/widget/WidgetGateway;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/conversation/activeconversation/ViewableConversation;Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;ZZ)V
    .locals 1

    .line 152
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x1

    .line 103
    iput-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isNetworkAvailable:Z

    .line 153
    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 154
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 155
    iput-object p3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    .line 156
    iput-object p4, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 157
    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    .line 158
    iput-boolean p7, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->retainMessageBoxOnUI:Z

    .line 159
    iget-object p1, p3, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    .line 160
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-virtual {p1, p0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->addObserver(Ljava/util/Observer;)V

    .line 161
    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object p1

    invoke-virtual {p1, p0}, Lcom/helpshift/account/AuthenticationFailureDM;->registerListener(Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;)V

    .line 163
    new-instance p1, Lcom/helpshift/widget/WidgetGateway;

    iget-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-direct {p1, p2, p3}, Lcom/helpshift/widget/WidgetGateway;-><init>(Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;Lcom/helpshift/conversation/domainmodel/ConversationController;)V

    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->widgetGateway:Lcom/helpshift/widget/WidgetGateway;

    .line 166
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->widgetGateway:Lcom/helpshift/widget/WidgetGateway;

    invoke-virtual {p1}, Lcom/helpshift/widget/WidgetGateway;->makeReplyFieldViewState()Lcom/helpshift/widget/MutableReplyFieldViewState;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyFieldViewState:Lcom/helpshift/widget/MutableReplyFieldViewState;

    .line 167
    new-instance p1, Lcom/helpshift/widget/MutableHistoryLoadingViewState;

    invoke-direct {p1}, Lcom/helpshift/widget/MutableHistoryLoadingViewState;-><init>()V

    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->historyLoadingViewState:Lcom/helpshift/widget/MutableHistoryLoadingViewState;

    .line 168
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->widgetGateway:Lcom/helpshift/widget/WidgetGateway;

    invoke-virtual {p1}, Lcom/helpshift/widget/WidgetGateway;->makeScrollJumperViewState()Lcom/helpshift/widget/MutableScrollJumperViewState;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->scrollJumperViewState:Lcom/helpshift/widget/MutableScrollJumperViewState;

    .line 169
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->shouldShowReplyBoxOnConversationRejected()Z

    move-result p1

    .line 170
    invoke-virtual {p4}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p2

    .line 171
    iget-object p7, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    .line 172
    invoke-virtual {p7, p2, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->setEnableMessageClickOnResolutionRejected(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    .line 173
    iget-object p7, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->widgetGateway:Lcom/helpshift/widget/WidgetGateway;

    .line 174
    invoke-virtual {p7, p2, p1}, Lcom/helpshift/widget/WidgetGateway;->makeConversationFooterViewState(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)Lcom/helpshift/widget/MutableConversationFooterViewState;

    move-result-object p7

    iput-object p7, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationFooterViewState:Lcom/helpshift/widget/MutableConversationFooterViewState;

    .line 178
    iget-object p7, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->widgetGateway:Lcom/helpshift/widget/WidgetGateway;

    invoke-virtual {p4}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    invoke-virtual {p7, v0}, Lcom/helpshift/widget/WidgetGateway;->makeAttachImageButtonViewState(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Lcom/helpshift/widget/MutableBaseViewState;

    move-result-object p7

    iput-object p7, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->attachImageButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    .line 180
    new-instance p7, Lcom/helpshift/widget/MutableBaseViewState;

    invoke-direct {p7}, Lcom/helpshift/widget/MutableBaseViewState;-><init>()V

    iput-object p7, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    .line 182
    iget-object p7, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->widgetGateway:Lcom/helpshift/widget/WidgetGateway;

    .line 183
    invoke-virtual {p7, p2, p1}, Lcom/helpshift/widget/WidgetGateway;->makeReplyBoxViewState(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)Lcom/helpshift/widget/MutableReplyBoxViewState;

    move-result-object p7

    iput-object p7, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    .line 185
    iget-object p7, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->widgetGateway:Lcom/helpshift/widget/WidgetGateway;

    invoke-virtual {p7, p2}, Lcom/helpshift/widget/WidgetGateway;->makeConfirmationBoxViewState(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Lcom/helpshift/widget/MutableBaseViewState;

    move-result-object p7

    iput-object p7, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->confirmationBoxViewState:Lcom/helpshift/widget/MutableBaseViewState;

    .line 188
    iget-object p7, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    invoke-virtual {p7}, Lcom/helpshift/widget/MutableReplyBoxViewState;->isVisible()Z

    move-result p7

    if-eqz p7, :cond_0

    const/4 p7, 0x2

    goto :goto_0

    :cond_0
    const/4 p7, -0x1

    .line 189
    :goto_0
    invoke-virtual {p3, p7}, Lcom/helpshift/conversation/domainmodel/ConversationController;->setConversationViewState(I)V

    if-nez p1, :cond_1

    .line 191
    iget-object p1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object p3, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p1, p3, :cond_1

    .line 193
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->handleConversationEnded(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 197
    :cond_1
    invoke-virtual {p4, p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->setConversationVMCallback(Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;)V

    .line 200
    iput-object p5, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    .line 201
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->initMessagesList()V

    .line 202
    iput-boolean p6, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showConversationHistory:Z

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/common/exception/RootAPIException;)V
    .locals 0

    .line 95
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showErrorForNoNetwork(Lcom/helpshift/common/exception/RootAPIException;)V

    return-void
.end method

.method static synthetic access$100(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V
    .locals 0

    .line 95
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateUserInputState()V

    return-void
.end method

.method static synthetic access$200(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 0

    .line 95
    iget-object p0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->botMessageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    return-object p0
.end method

.method static synthetic access$300(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V
    .locals 0

    .line 95
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showOptions(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V

    return-void
.end method

.method static synthetic access$400(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)Lcom/helpshift/conversation/viewmodel/ListPickerVM;
    .locals 0

    .line 95
    iget-object p0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->listPickerVM:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    return-object p0
.end method

.method private clearNotifications()V
    .locals 2

    .line 1744
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 1745
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->clearNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 1746
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->resetPushNotificationCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    return-void
.end method

.method private createOptionsBotMessage(Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;)Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;
    .locals 2

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 1426
    :cond_0
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    invoke-direct {v0, p1}, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;-><init>(Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;)V

    .line 1427
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v0, p1, v1}, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    return-object v0
.end method

.method private createOptionsBotMessage(Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;)Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;
    .locals 2

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 1417
    :cond_0
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    invoke-direct {v0, p1}, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;-><init>(Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;)V

    .line 1418
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v0, p1, v1}, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    return-object v0
.end method

.method private disableUserInputOptions()V
    .locals 2

    .line 1033
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-eqz v0, :cond_0

    .line 1034
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->hideKeyboard()V

    .line 1038
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->attachImageButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 1041
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->disableUserTextInput()V

    return-void
.end method

.method private disableUserTextInput()V
    .locals 2

    .line 1105
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableReplyBoxViewState;->setVisible(Z)V

    return-void
.end method

.method private evaluateBotMessages(Ljava/util/Collection;)Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "+",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 1052
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 1053
    iget-boolean v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    .line 1054
    iget-boolean v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    invoke-direct {p0, p1, v2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->processMessagesForBots(Ljava/util/Collection;Z)Ljava/util/List;

    move-result-object p1

    .line 1056
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v2

    if-nez v2, :cond_1

    if-eqz v1, :cond_0

    .line 1059
    iget-boolean v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    if-nez v2, :cond_0

    .line 1061
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    .line 1063
    invoke-virtual {v2, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldEnableMessagesClick(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v2

    .line 1061
    invoke-virtual {v1, v0, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessagesClickOnBotSwitch(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    .line 1064
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->removeOptionsMessageFromUI()V

    .line 1069
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableReplyBoxViewState;->setStandardTextInput()V

    .line 1071
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$15;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$15;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    goto :goto_0

    .line 1084
    :cond_0
    iget-boolean v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    if-eqz v2, :cond_1

    if-nez v1, :cond_1

    .line 1091
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    const/4 v2, 0x0

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessagesClickOnBotSwitch(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    .line 1096
    :cond_1
    :goto_0
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateUserInputState()V

    return-object p1
.end method

.method private generateSystemRedactedConversationMessageDM(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;
    .locals 5

    .line 384
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;

    .line 385
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getCreatedAt()Ljava/lang/String;

    move-result-object v1

    .line 386
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getEpochCreatedAtTime()J

    move-result-wide v2

    const/4 v4, 0x1

    invoke-direct {v0, v1, v2, v3, v4}, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;-><init>(Ljava/lang/String;JI)V

    .line 388
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 389
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object p1, v0, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;->conversationLocalId:Ljava/lang/Long;

    return-object v0
.end method

.method private getUIMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 394
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 399
    iget-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isRedacted:Z

    if-eqz v1, :cond_0

    .line 400
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->generateSystemRedactedConversationMessageDM(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;

    move-result-object p1

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 403
    :cond_0
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->buildUIMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/util/List;

    move-result-object p1

    invoke-interface {v0, p1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    :goto_0
    return-object v0
.end method

.method private getUIMessagesForHistory(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 1548
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 1553
    iget-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isRedacted:Z

    if-eqz v1, :cond_0

    .line 1554
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->generateSystemRedactedConversationMessageDM(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;

    move-result-object p1

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 1557
    :cond_0
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-interface {v0, p1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    :goto_0
    return-object v0
.end method

.method private hideListPicker(Z)V
    .locals 2

    .line 1014
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$14;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$14;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Z)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method private incrementCreatedAt(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 5

    .line 1433
    new-instance v0, Ljava/util/Date;

    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v1

    const-wide/16 v3, 0x1

    add-long/2addr v1, v3

    invoke-direct {v0, v1, v2}, Ljava/util/Date;-><init>(J)V

    .line 1434
    sget-object p2, Lcom/helpshift/common/util/HSDateFormatSpec;->STORAGE_TIME_FORMAT:Lcom/helpshift/common/util/HSSimpleDateFormat;

    invoke-virtual {p2, v0}, Lcom/helpshift/common/util/HSSimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object p2

    .line 1435
    invoke-static {p2}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v0

    .line 1436
    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->setCreatedAt(Ljava/lang/String;)V

    .line 1437
    invoke-virtual {p1, v0, v1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->setEpochCreatedAtTime(J)V

    return-void
.end method

.method private loadHistoryMessagesInternal()V
    .locals 2

    .line 1937
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->historyLoadingViewState:Lcom/helpshift/widget/MutableHistoryLoadingViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableHistoryLoadingViewState;->getState()Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    move-result-object v0

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->LOADING:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    if-ne v0, v1, :cond_0

    return-void

    .line 1942
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$27;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$27;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method private markMessagesAsSeenOnEntry()V
    .locals 3

    .line 270
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 271
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->isSynced(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v1

    if-nez v1, :cond_0

    return-void

    .line 279
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v2, Lcom/helpshift/conversation/viewmodel/ConversationalVM$2;

    invoke-direct {v2, p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$2;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    invoke-virtual {v1, v2}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method private markMessagesAsSeenOnExit()V
    .locals 3

    .line 242
    new-instance v0, Ljava/util/ArrayList;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getAllConversations()Ljava/util/List;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 245
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    .line 246
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v2, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->isSynced(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v2

    if-nez v2, :cond_0

    .line 247
    invoke-interface {v0, v1}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    .line 253
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v2, Lcom/helpshift/conversation/viewmodel/ConversationalVM$1;

    invoke-direct {v2, p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$1;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Ljava/util/List;)V

    invoke-virtual {v1, v2}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method private notifyRendererForScrollToBottom()V
    .locals 2

    .line 1975
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$28;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$28;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method private processMessagesForBots(Ljava/util/Collection;Z)Ljava/util/List;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "+",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;Z)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 425
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0, p1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 427
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1

    .line 429
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    .line 430
    invoke-virtual {v1, v0, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->evaluateBotExecutionState(Ljava/util/List;Z)Z

    move-result p2

    iput-boolean p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    .line 432
    iget-boolean p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    const/4 v1, 0x0

    if-eqz p2, :cond_7

    .line 437
    iget-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p2, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->getLatestUnansweredBotMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p1

    .line 438
    iget-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->botMessageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    const/4 v2, 0x1

    if-eqz p2, :cond_0

    if-eqz p1, :cond_0

    .line 443
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    iget-object v3, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-virtual {p2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p2

    if-eqz p2, :cond_0

    .line 444
    iput-boolean v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->awaitingUserInputForBotStep:Z

    return-object v0

    :cond_0
    if-eqz p1, :cond_4

    .line 449
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v3, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_TEXT_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-eq p2, v3, :cond_1

    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v3, Lcom/helpshift/conversation/activeconversation/message/MessageType;->FAQ_LIST_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne p2, v3, :cond_4

    .line 455
    :cond_1
    invoke-interface {v0, p1}, Ljava/util/List;->indexOf(Ljava/lang/Object;)I

    move-result p2

    const/4 v3, -0x1

    if-eq p2, v3, :cond_5

    .line 461
    iget-object v3, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v4, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_TEXT_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v3, v4, :cond_2

    .line 462
    move-object v3, p1

    check-cast v3, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;

    invoke-direct {p0, v3}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->createOptionsBotMessage(Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;)Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    move-result-object v3

    goto :goto_0

    .line 465
    :cond_2
    move-object v3, p1

    check-cast v3, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;

    invoke-direct {p0, v3}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->createOptionsBotMessage(Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;)Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    move-result-object v3

    .line 469
    :goto_0
    invoke-direct {p0, v3, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->incrementCreatedAt(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 473
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v4, v4, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->type:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    sget-object v5, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;->PILL:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    if-ne v4, v5, :cond_3

    add-int/2addr p2, v2

    .line 474
    invoke-interface {v0, p2, v3}, Ljava/util/List;->add(ILjava/lang/Object;)V

    .line 476
    :cond_3
    iput-object v3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->botMessageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    goto :goto_1

    .line 480
    :cond_4
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->botMessageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    :cond_5
    :goto_1
    if-eqz p1, :cond_6

    .line 485
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->removeOptionsMessageFromUI()V

    .line 486
    iput-boolean v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->awaitingUserInputForBotStep:Z

    goto :goto_2

    .line 489
    :cond_6
    iput-boolean v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->awaitingUserInputForBotStep:Z

    goto :goto_2

    .line 493
    :cond_7
    iput-boolean v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->awaitingUserInputForBotStep:Z

    :goto_2
    return-object v0
.end method

.method private removeOptionsMessageFromUI()V
    .locals 5

    .line 501
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    if-nez v0, :cond_0

    return-void

    .line 505
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->copyOfUIMessageDMs()Ljava/util/List;

    move-result-object v0

    .line 506
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 507
    invoke-static {v0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v2

    if-nez v2, :cond_3

    .line 508
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 509
    iget-object v3, v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v4, Lcom/helpshift/conversation/activeconversation/message/MessageType;->OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v3, v4, :cond_1

    .line 510
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 513
    :cond_2
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->remove(Ljava/util/List;)V

    :cond_3
    const/4 v0, 0x0

    .line 520
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->hideListPicker(Z)V

    return-void
.end method

.method private resetIncrementMessageCountFlag()V
    .locals 4

    .line 1750
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    const/4 v2, 0x0

    const/4 v3, 0x1

    invoke-virtual {v0, v1, v2, v3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->setShouldIncrementMessageCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;ZZ)V

    return-void
.end method

.method private sendNormalTextMessage(Ljava/lang/String;)V
    .locals 2

    .line 718
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->clearReply()V

    .line 719
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$7;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$7;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method private setScreenVisibility(Z)V
    .locals 0

    .line 232
    iput-boolean p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isScreenCurrentlyVisible:Z

    return-void
.end method

.method private setUserCanReadMessages(Z)V
    .locals 1

    .line 236
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->setUserCanReadMessages(Z)V

    .line 237
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isAgentTyping()Z

    move-result p1

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onAgentTypingUpdate(Z)V

    return-void
.end method

.method private shouldShowReplyBoxOnConversationRejected()Z
    .locals 1

    .line 1715
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getUserReplyText()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    .line 1716
    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->shouldPersistMessageBox()Z

    move-result v0

    if-nez v0, :cond_1

    iget-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->retainMessageBoxOnUI:Z

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

.method private showConfirmationBox()V
    .locals 2

    .line 1997
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableReplyBoxViewState;->setVisible(Z)V

    .line 1998
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateAttachmentButtonViewState()V

    .line 1999
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->confirmationBoxViewState:Lcom/helpshift/widget/MutableBaseViewState;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 2000
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationFooterViewState:Lcom/helpshift/widget/MutableConversationFooterViewState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->NONE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableConversationFooterViewState;->setState(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    return-void
.end method

.method private showErrorForNoNetwork(Lcom/helpshift/common/exception/RootAPIException;)V
    .locals 1

    .line 793
    iget-object p1, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    instance-of p1, p1, Lcom/helpshift/common/exception/NetworkException;

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->isOnline()Z

    move-result p1

    if-nez p1, :cond_0

    .line 794
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$9;

    invoke-direct {v0, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$9;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {p1, v0}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method private showListPicker(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V
    .locals 2

    .line 1510
    new-instance v0, Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-direct {v0, v1, p1, p0}, Lcom/helpshift/conversation/viewmodel/ListPickerVM;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/viewmodel/ListPickerVMCallback;)V

    iput-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->listPickerVM:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    .line 1511
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$23;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$23;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method private showMessageBox()V
    .locals 2

    .line 1990
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableReplyBoxViewState;->setVisible(Z)V

    .line 1991
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateAttachmentButtonViewState()V

    .line 1992
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->confirmationBoxViewState:Lcom/helpshift/widget/MutableBaseViewState;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 1993
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationFooterViewState:Lcom/helpshift/widget/MutableConversationFooterViewState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->NONE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableConversationFooterViewState;->setState(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    return-void
.end method

.method private showOptions(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V
    .locals 2

    .line 1485
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->type:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;->PILL:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    if-ne v0, v1, :cond_0

    .line 1486
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    invoke-interface {v0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->showOptionInput(Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;)V

    goto :goto_0

    .line 1489
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showListPicker(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V

    :goto_0
    return-void
.end method

.method private showUnreadMessagesIndicator()V
    .locals 2

    .line 1859
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->scrollJumperViewState:Lcom/helpshift/widget/MutableScrollJumperViewState;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableScrollJumperViewState;->setShouldShowUnreadMessagesIndicator(Z)V

    return-void
.end method

.method private updateAttachmentButtonViewState()V
    .locals 2

    .line 2004
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->resetDefaultMenuItemsVisibility()V

    .line 2006
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->attachImageButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableBaseViewState;->isVisible()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 2007
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->attachImageButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    iget-boolean v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isConversationRejected:Z

    if-nez v1, :cond_0

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    invoke-virtual {v1}, Lcom/helpshift/widget/MutableReplyBoxViewState;->isVisible()Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v1, 0x1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    :cond_1
    return-void
.end method

.method private updateReplyBoxVisibility()V
    .locals 2

    .line 1448
    iget-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    if-eqz v0, :cond_2

    iget-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isConversationRejected:Z

    if-nez v0, :cond_2

    .line 1450
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->botMessageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    if-nez v0, :cond_0

    .line 1451
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableReplyBoxViewState;->setVisible(Z)V

    return-void

    .line 1456
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->botMessageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_TEXT_WITH_TEXT_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v0, v1, :cond_1

    .line 1457
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->botMessageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    .line 1458
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/TextInput;

    invoke-virtual {v1, v0}, Lcom/helpshift/widget/MutableReplyBoxViewState;->setInput(Lcom/helpshift/conversation/activeconversation/message/input/Input;)V

    goto :goto_0

    .line 1460
    :cond_1
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->botMessageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v0, v1, :cond_3

    .line 1462
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$22;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$22;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    goto :goto_0

    .line 1478
    :cond_2
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableReplyBoxViewState;->isVisible()Z

    move-result v0

    if-eqz v0, :cond_3

    .line 1479
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableReplyBoxViewState;->setStandardTextInput()V

    :cond_3
    :goto_0
    return-void
.end method

.method private updateUserInputState()V
    .locals 5

    .line 1113
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 1115
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 1116
    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    const/4 v3, 0x1

    const/4 v4, 0x0

    if-ne v1, v2, :cond_0

    .line 1119
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->disableUserInputOptions()V

    goto :goto_2

    .line 1121
    :cond_0
    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v1, v2, :cond_8

    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v1, v2, :cond_8

    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->COMPLETED_ISSUE_CREATED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v1, v2, :cond_1

    goto :goto_2

    .line 1126
    :cond_1
    iget-boolean v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    if-eqz v1, :cond_7

    .line 1129
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->attachImageButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {v1, v4}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 1131
    iget-boolean v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->awaitingUserInputForBotStep:Z

    if-eqz v1, :cond_2

    goto :goto_2

    .line 1137
    :cond_2
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->disableUserInputOptions()V

    .line 1143
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    if-eqz v1, :cond_6

    .line 1145
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v1}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v1

    if-lez v1, :cond_5

    .line 1147
    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    sub-int/2addr v1, v3

    invoke-virtual {v0, v1}, Lcom/helpshift/common/util/HSObservableList;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1148
    instance-of v1, v0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;

    if-nez v1, :cond_3

    instance-of v1, v0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;

    if-eqz v1, :cond_5

    .line 1150
    :cond_3
    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    .line 1151
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->getState()Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    move-result-object v0

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENT:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    if-ne v0, v1, :cond_4

    goto :goto_0

    :cond_4
    const/4 v3, 0x0

    :cond_5
    :goto_0
    move v4, v3

    goto :goto_2

    :cond_6
    :goto_1
    const/4 v4, 0x1

    goto :goto_2

    .line 1157
    :cond_7
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v1

    if-eqz v1, :cond_8

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_8

    .line 1160
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->disableUserInputOptions()V

    goto :goto_1

    .line 1167
    :cond_8
    :goto_2
    invoke-virtual {p0, v4}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showFakeTypingIndicator(Z)V

    return-void
.end method


# virtual methods
.method public add(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 1824
    invoke-static {p1}, Ljava/util/Collections;->singletonList(Ljava/lang/Object;)Ljava/util/List;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->addAll(Ljava/util/Collection;)V

    return-void
.end method

.method public bridge synthetic add(Ljava/lang/Object;)V
    .locals 0

    .line 95
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->add(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    return-void
.end method

.method public addAll(Ljava/util/Collection;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "+",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    const-string v0, "Helpshift_ConvsatnlVM"

    .line 913
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "addAll called : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-interface {p1}, Ljava/util/Collection;->size()I

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 914
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 917
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    .line 918
    invoke-virtual {v1, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->hasBotSwitchedToAnotherBotInPollerResponse(Ljava/util/Collection;)Z

    move-result v1

    const/4 v2, 0x0

    if-eqz v1, :cond_0

    .line 921
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessagesClickOnBotSwitch(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    .line 925
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->evaluateBotMessages(Ljava/util/Collection;)Ljava/util/List;

    move-result-object p1

    .line 928
    iget-boolean v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    if-eqz v1, :cond_1

    .line 932
    iget-boolean v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isUserReplyDraftClearedForBotChange:Z

    if-nez v1, :cond_2

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    .line 933
    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->containsAtleastOneUserMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 934
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->clearUserReplyDraft()V

    const/4 v0, 0x1

    .line 935
    iput-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isUserReplyDraftClearedForBotChange:Z

    goto :goto_0

    .line 939
    :cond_1
    iput-boolean v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isUserReplyDraftClearedForBotChange:Z

    .line 942
    :cond_2
    :goto_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    if-eqz v0, :cond_3

    .line 943
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->addMessages(Ljava/util/Collection;)V

    :cond_3
    return-void
.end method

.method public appendMessages(II)V
    .locals 1

    .line 1835
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-eqz v0, :cond_0

    .line 1836
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->appendMessages(II)V

    :cond_0
    return-void
.end method

.method protected buildUIMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 409
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 410
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v1, v2}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 415
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldOpen(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 416
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->processMessagesForBots(Ljava/util/Collection;Z)Ljava/util/List;

    move-result-object p1

    return-object p1

    .line 419
    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-direct {v0, p1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    return-object v0
.end method

.method clearReply()V
    .locals 2

    .line 707
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$6;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$6;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public clearUserReplyDraft()V
    .locals 2

    .line 318
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    const-string v1, ""

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveUserReplyText(Ljava/lang/String;)V

    .line 319
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyFieldViewState:Lcom/helpshift/widget/MutableReplyFieldViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableReplyFieldViewState;->clearReplyText()V

    return-void
.end method

.method createPreIssue(Ljava/lang/String;Z)V
    .locals 3

    const-string v0, "Helpshift_ConvsatnlVM"

    .line 599
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Trigger preissue creation. Retrying ? "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 602
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateLastUserActivityTime()V

    .line 605
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->clearUserReplyDraft()V

    .line 608
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->disableUserInputOptions()V

    .line 610
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v1, "conversationGreetingMessage"

    invoke-virtual {v0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-nez p2, :cond_0

    .line 613
    iget-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    invoke-virtual {p2, v1, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->addPreissueFirstUserMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;)V

    .line 616
    :cond_0
    iget-boolean p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isNetworkAvailable:Z

    if-nez p2, :cond_1

    .line 617
    new-instance p1, Ljava/lang/Exception;

    const-string p2, "No internet connection."

    invoke-direct {p1, p2}, Ljava/lang/Exception;-><init>(Ljava/lang/String;)V

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onCreateConversationFailure(Ljava/lang/Exception;)V

    return-void

    .line 622
    :cond_1
    iget-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 623
    invoke-virtual {p2, v1, v0, p1, p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->createPreIssue(Lcom/helpshift/conversation/activeconversation/ViewableConversation;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;)V

    return-void
.end method

.method public forceClickOnNewConversationButton()V
    .locals 1

    .line 1737
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    iget-boolean v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isStartNewConversationClicked:Z

    if-eqz v0, :cond_0

    .line 1738
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onNewConversationButtonClicked()V

    :cond_0
    return-void
.end method

.method public getAttachImageButtonViewState()Lcom/helpshift/widget/BaseViewState;
    .locals 1

    .line 2049
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->attachImageButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    return-object v0
.end method

.method public getConfirmationBoxViewState()Lcom/helpshift/widget/BaseViewState;
    .locals 1

    .line 2057
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->confirmationBoxViewState:Lcom/helpshift/widget/MutableBaseViewState;

    return-object v0
.end method

.method public getConversationFooterViewState()Lcom/helpshift/widget/ConversationFooterViewState;
    .locals 1

    .line 2045
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationFooterViewState:Lcom/helpshift/widget/MutableConversationFooterViewState;

    return-object v0
.end method

.method public getHistoryLoadingViewState()Lcom/helpshift/widget/HistoryLoadingViewState;
    .locals 1

    .line 2037
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->historyLoadingViewState:Lcom/helpshift/widget/MutableHistoryLoadingViewState;

    return-object v0
.end method

.method public getReplyBoxViewState()Lcom/helpshift/widget/ReplyBoxViewState;
    .locals 1

    .line 2053
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    return-object v0
.end method

.method public getReplyButtonViewState()Lcom/helpshift/widget/BaseViewState;
    .locals 1

    .line 2061
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    return-object v0
.end method

.method public getReplyFieldViewState()Lcom/helpshift/widget/ReplyFieldViewState;
    .locals 1

    .line 2033
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyFieldViewState:Lcom/helpshift/widget/MutableReplyFieldViewState;

    return-object v0
.end method

.method public getScrollJumperViewState()Lcom/helpshift/widget/ScrollJumperViewState;
    .locals 1

    .line 2041
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->scrollJumperViewState:Lcom/helpshift/widget/MutableScrollJumperViewState;

    return-object v0
.end method

.method public handleAdminAttachmentMessageClick(Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V
    .locals 1

    .line 1617
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->onAdminAttachmentMessageClicked(Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V

    return-void
.end method

.method public handleAdminSuggestedQuestionRead(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Ljava/lang/String;Ljava/lang/String;)V
    .locals 7

    .line 1343
    invoke-static {p3}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 1344
    iget-object v3, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 1345
    iget-object v4, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    .line 1346
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;

    move-object v1, v0

    move-object v2, p0

    move-object v5, p2

    move-object v6, p3

    invoke-direct/range {v1 .. v6}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {p1, v0}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method public handleAppReviewRequestClick(Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V
    .locals 3

    .line 832
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v1, "reviewUrl"

    invoke-virtual {v0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v0

    .line 833
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    .line 834
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const/4 v2, 0x1

    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->setAppReviewed(Z)V

    .line 835
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-eqz v1, :cond_0

    .line 836
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->openAppReviewStore(Ljava/lang/String;)V

    .line 839
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    invoke-virtual {v0, v1, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->handleAppReviewRequestClick(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V

    return-void
.end method

.method handleConversationRejectedState()V
    .locals 3

    .line 1696
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 1697
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    const-string v2, ""

    invoke-virtual {v1, v2}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveUserReplyText(Ljava/lang/String;)V

    .line 1700
    iget-boolean v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isRedacted:Z

    if-eqz v0, :cond_0

    .line 1701
    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->REDACTED_STATE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    goto :goto_0

    .line 1704
    :cond_0
    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->REJECTED_MESSAGE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    .line 1706
    :goto_0
    invoke-virtual {p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showStartNewConversation(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    const/4 v0, 0x1

    .line 1707
    iput-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isConversationRejected:Z

    return-void
.end method

.method public handleIdempotentPreIssueCreationSuccess()V
    .locals 2

    .line 1304
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$19;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$19;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public handleOptionSelected(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V
    .locals 4

    .line 964
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    if-nez v0, :cond_0

    return-void

    .line 970
    :cond_0
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->type:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;->PILL:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    const/4 v2, 0x1

    if-ne v0, v1, :cond_1

    .line 974
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getUiMessageDMs()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0, p1}, Ljava/util/List;->indexOf(Ljava/lang/Object;)I

    move-result v0

    .line 975
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-static {p1}, Ljava/util/Collections;->singletonList(Ljava/lang/Object;)Ljava/util/List;

    move-result-object v3

    invoke-virtual {v1, v3}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->remove(Ljava/util/List;)V

    .line 979
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    sub-int/2addr v0, v2

    invoke-interface {v1, v0, v2}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->updateMessages(II)V

    .line 983
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateLastUserActivityTime()V

    .line 985
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->type:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;->PILL:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    if-ne v0, v1, :cond_2

    .line 987
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->disableUserInputOptions()V

    goto :goto_0

    .line 989
    :cond_2
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->type:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;->PICKER:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    if-ne v0, v1, :cond_3

    .line 990
    invoke-direct {p0, v2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->hideListPicker(Z)V

    .line 992
    :cond_3
    :goto_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;

    invoke-direct {v1, p0, p1, p2, p3}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public handleOptionSelectedForPicker(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V
    .locals 1

    const/4 v0, 0x0

    .line 1500
    iput-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->listPickerVM:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    .line 1501
    invoke-virtual {p0, p1, p2, p3}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleOptionSelected(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V

    return-void
.end method

.method public handleOptionSelectedForPicker(Lcom/helpshift/conversation/viewmodel/OptionUIModel;Z)V
    .locals 1

    .line 1529
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->listPickerVM:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    if-eqz v0, :cond_0

    .line 1530
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->listPickerVM:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/conversation/viewmodel/ListPickerVM;->handleOptionSelectedForPicker(Lcom/helpshift/conversation/viewmodel/OptionUIModel;Z)V

    :cond_0
    return-void
.end method

.method public handlePreIssueCreationSuccess()V
    .locals 2

    .line 1263
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$18;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$18;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public handleScreenshotMessageClick(Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V
    .locals 1

    .line 828
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->onScreenshotMessageClicked(Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V

    return-void
.end method

.method public handleStateChangeForIssueMode(Lcom/helpshift/conversation/dto/IssueState;)V
    .locals 6

    const-string v0, "Helpshift_ConvsatnlVM"

    .line 1640
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Changing conversation status to: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 1641
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 1642
    invoke-static {p1}, Lcom/helpshift/conversation/ConversationUtil;->isInProgressState(Lcom/helpshift/conversation/dto/IssueState;)Z

    move-result v1

    const/4 v2, 0x2

    const/4 v3, 0x1

    const/4 v4, 0x0

    const/4 v5, -0x1

    if-eqz v1, :cond_0

    .line 1644
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showMessageBox()V

    const/4 p1, 0x0

    :goto_0
    const/4 v0, 0x0

    const/4 v5, 0x2

    goto/16 :goto_2

    .line 1648
    :cond_0
    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p1, v1, :cond_3

    .line 1649
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-virtual {p1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->shouldShowConversationResolutionQuestion()Z

    move-result p1

    if-eqz p1, :cond_1

    .line 1650
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showConfirmationBox()V

    .line 1654
    :cond_1
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->scrollJumperViewState:Lcom/helpshift/widget/MutableScrollJumperViewState;

    invoke-virtual {p1}, Lcom/helpshift/widget/MutableScrollJumperViewState;->isVisible()Z

    move-result p1

    if-nez p1, :cond_2

    .line 1655
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->notifyRendererForScrollToBottom()V

    :cond_2
    const/4 p1, 0x1

    const/4 v0, 0x0

    const/4 v3, 0x0

    goto :goto_2

    .line 1658
    :cond_3
    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p1, v1, :cond_4

    .line 1660
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleConversationRejectedState()V

    const/4 p1, 0x1

    const/4 v0, 0x1

    goto :goto_2

    .line 1662
    :cond_4
    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p1, v1, :cond_6

    .line 1663
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    const-string v1, ""

    invoke-virtual {p1, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveUserReplyText(Ljava/lang/String;)V

    .line 1664
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldShowCSATInFooter(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result p1

    if-eqz p1, :cond_5

    .line 1665
    sget-object p1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->CSAT_RATING:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showStartNewConversation(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    goto :goto_1

    .line 1668
    :cond_5
    sget-object p1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->START_NEW_CONVERSATION:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showStartNewConversation(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    goto :goto_1

    .line 1671
    :cond_6
    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p1, v1, :cond_7

    .line 1672
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {p1, v4}, Lcom/helpshift/conversation/domainmodel/ConversationController;->setPersistMessageBox(Z)V

    .line 1673
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showMessageBox()V

    .line 1674
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p1, v0, v3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->setEnableMessageClickOnResolutionRejected(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    const/4 p1, 0x1

    goto :goto_0

    .line 1677
    :cond_7
    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->ARCHIVED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p1, v0, :cond_8

    .line 1678
    sget-object p1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->ARCHIVAL_MESSAGE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showStartNewConversation(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    goto :goto_1

    .line 1680
    :cond_8
    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->AUTHOR_MISMATCH:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p1, v0, :cond_9

    .line 1681
    sget-object p1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->AUTHOR_MISMATCH:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showStartNewConversation(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    :cond_9
    :goto_1
    const/4 p1, 0x1

    const/4 v0, 0x0

    :goto_2
    if-eqz v3, :cond_a

    .line 1686
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateUIOnNewMessageReceived()V

    :cond_a
    if-eqz p1, :cond_b

    .line 1689
    invoke-virtual {p0, v4}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onAgentTypingUpdate(Z)V

    .line 1691
    :cond_b
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {p1, v5}, Lcom/helpshift/conversation/domainmodel/ConversationController;->setConversationViewState(I)V

    .line 1692
    iput-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isConversationRejected:Z

    return-void
.end method

.method protected hideAllFooterWidgets()V
    .locals 2

    .line 2012
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableReplyBoxViewState;->setVisible(Z)V

    .line 2013
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateAttachmentButtonViewState()V

    .line 2014
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->confirmationBoxViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 2015
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationFooterViewState:Lcom/helpshift/widget/MutableConversationFooterViewState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->NONE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableConversationFooterViewState;->setState(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    return-void
.end method

.method public hidePickerClearButton()V
    .locals 1

    .line 1541
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->hidePickerClearButton()V

    return-void
.end method

.method protected initMessagesList()V
    .locals 6

    .line 327
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    if-eqz v0, :cond_0

    .line 328
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->unregisterMessageListVMCallback()V

    .line 331
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 337
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->initializeConversationsForUI()V

    .line 338
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->initializeIssueStatusForUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 339
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->hasMoreMessages()Z

    move-result v1

    .line 340
    new-instance v2, Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v4, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-direct {v2, v3, v4}, Lcom/helpshift/conversation/viewmodel/MessageListVM;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;)V

    iput-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    .line 341
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getUIConversations()Ljava/util/List;

    move-result-object v2

    .line 344
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    .line 345
    iget-object v4, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v4}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getAllConversations()Ljava/util/List;

    move-result-object v4

    .line 346
    invoke-interface {v4}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :goto_0
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_1

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 347
    invoke-direct {p0, v5}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getUIMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/util/List;

    move-result-object v5

    invoke-interface {v3, v5}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    goto :goto_0

    .line 349
    :cond_1
    iget-object v4, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v4, v2, v3, v1, p0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->initializeMessageList(Ljava/util/List;Ljava/util/List;ZLcom/helpshift/conversation/viewmodel/MessageListVMCallback;)V

    .line 351
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getUiMessageDMs()Ljava/util/List;

    move-result-object v2

    invoke-interface {v1, v2}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->initializeMessages(Ljava/util/List;)V

    .line 353
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1, p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->registerMessagesObserver(Lcom/helpshift/common/util/HSListObserver;)V

    .line 354
    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, v1, :cond_2

    const/4 v0, 0x1

    goto :goto_1

    :cond_2
    const/4 v0, 0x0

    :goto_1
    iput-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isConversationRejected:Z

    .line 355
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->prefillReplyBox()V

    return-void
.end method

.method public isMessageBoxVisible()Z
    .locals 1

    .line 850
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableReplyBoxViewState;->isVisible()Z

    move-result v0

    return v0
.end method

.method public isVisibleOnUI()Z
    .locals 1

    .line 855
    iget-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isScreenCurrentlyVisible:Z

    return v0
.end method

.method public launchAttachment(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 1622
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->launchAttachment(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public launchScreenshotAttachment(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 845
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->launchScreenshotAttachment(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public markConversationResolutionStatus(Z)V
    .locals 3

    const-string v0, "Helpshift_ConvsatnlVM"

    .line 1626
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Sending resolution event : Accepted? "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 1629
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 1630
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v1, v2, :cond_0

    .line 1631
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, v0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->markConversationResolutionStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    :cond_0
    return-void
.end method

.method public newAdminMessagesAdded()V
    .locals 0

    .line 1843
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateUIOnNewMessageReceived()V

    return-void
.end method

.method public newUserMessagesAdded()V
    .locals 0

    .line 1849
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->notifyRendererForScrollToBottom()V

    return-void
.end method

.method public onAdminMessageLinkClicked(Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 5

    const/4 v0, 0x0

    .line 1761
    :try_start_0
    invoke-static {p1}, Ljava/net/URI;->create(Ljava/lang/String;)Ljava/net/URI;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 1764
    invoke-virtual {v1}, Ljava/net/URI;->getScheme()Ljava/lang/String;

    move-result-object v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    :cond_0
    move-object v1, v0

    .line 1769
    :goto_0
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 1770
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getAllConversations()Ljava/util/List;

    move-result-object v2

    .line 1772
    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :cond_1
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_2

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1773
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v4, p2}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_1

    move-object v0, v3

    .line 1778
    :cond_2
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p2

    if-nez p2, :cond_5

    .line 1779
    new-instance p2, Ljava/util/HashMap;

    invoke-direct {p2}, Ljava/util/HashMap;-><init>()V

    if-eqz v0, :cond_4

    .line 1781
    iget-object v2, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_3

    const-string v2, "preissue_id"

    .line 1782
    iget-object v3, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-interface {p2, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1784
    :cond_3
    iget-object v2, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_4

    const-string v2, "issue_id"

    .line 1785
    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-interface {p2, v2, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_4
    const-string v0, "p"

    .line 1788
    invoke-interface {p2, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "u"

    .line 1789
    invoke-interface {p2, v0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1790
    sget-object p1, Lcom/helpshift/analytics/AnalyticsEventType;->ADMIN_MESSAGE_DEEPLINK_CLICKED:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->pushAnalyticsEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V

    :cond_5
    return-void
.end method

.method public onAgentTypingUpdate(Z)V
    .locals 2

    .line 860
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$11;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$11;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Z)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public onAuthenticationFailure()V
    .locals 2

    .line 1897
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$26;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$26;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public onCSATSurveySubmitted(ILjava/lang/String;)V
    .locals 4

    .line 1721
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-eqz v0, :cond_0

    .line 1722
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->showCSATSubmittedView()V

    .line 1724
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 1725
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v1

    if-nez v1, :cond_1

    .line 1726
    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->START_NEW_CONVERSATION:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    invoke-virtual {p0, v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showStartNewConversation(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    :cond_1
    const-string v1, "Helpshift_ConvsatnlVM"

    .line 1728
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Sending CSAT rating : "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v3, ", feedback: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 1729
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, v0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendCSATSurvey(Lcom/helpshift/conversation/activeconversation/model/Conversation;ILjava/lang/String;)V

    return-void
.end method

.method public onConversationInboxPollFailure()V
    .locals 2

    const-string v0, "Helpshift_ConvsatnlVM"

    const-string v1, "On conversation inbox poll failure"

    .line 628
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 631
    invoke-virtual {p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showFakeTypingIndicator(Z)V

    .line 636
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->isOnline()Z

    move-result v0

    if-eqz v0, :cond_1

    iget-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->awaitingUserInputForBotStep:Z

    if-nez v0, :cond_1

    iget-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 637
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v0

    if-eqz v0, :cond_1

    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 638
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 640
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$3;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$3;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    const/4 v0, 0x1

    .line 648
    iput-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isShowingPollFailureError:Z

    :cond_1
    return-void
.end method

.method public onConversationInboxPollSuccess()V
    .locals 2

    .line 654
    iget-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isShowingPollFailureError:Z

    if-eqz v0, :cond_0

    .line 655
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$4;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$4;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    const/4 v0, 0x0

    .line 663
    iput-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isShowingPollFailureError:Z

    :cond_0
    return-void
.end method

.method public onCreateConversationFailure(Ljava/lang/Exception;)V
    .locals 2

    const-string v0, "Helpshift_ConvsatnlVM"

    const-string v1, "Error filing a pre-issue"

    .line 1321
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 1322
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$20;

    invoke-direct {v0, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$20;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {p1, v0}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public onCreateConversationSuccess(J)V
    .locals 0

    .line 1258
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handlePreIssueCreationSuccess()V

    return-void
.end method

.method public onDestroy()V
    .locals 1

    .line 2065
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->resetLastNotificationCountFetchTime()V

    return-void
.end method

.method public onHistoryLoadingError()V
    .locals 2

    .line 1598
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->historyLoadingViewState:Lcom/helpshift/widget/MutableHistoryLoadingViewState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->ERROR:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableHistoryLoadingViewState;->setState(Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;)V

    return-void
.end method

.method public onHistoryLoadingStarted()V
    .locals 2

    .line 1603
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->historyLoadingViewState:Lcom/helpshift/widget/MutableHistoryLoadingViewState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->LOADING:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableHistoryLoadingViewState;->setState(Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;)V

    return-void
.end method

.method public onHistoryLoadingSuccess()V
    .locals 2

    .line 1593
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->historyLoadingViewState:Lcom/helpshift/widget/MutableHistoryLoadingViewState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->NONE:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableHistoryLoadingViewState;->setState(Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;)V

    return-void
.end method

.method public onImageAttachmentButtonClick()V
    .locals 2

    .line 1711
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->setPersistMessageBox(Z)V

    return-void
.end method

.method public onIssueStatusChange(Lcom/helpshift/conversation/dto/IssueState;)V
    .locals 2

    .line 1221
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_1

    .line 1222
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleStateChangeForIssueMode(Lcom/helpshift/conversation/dto/IssueState;)V

    .line 1226
    iget-boolean p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    if-eqz p1, :cond_0

    .line 1227
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->attachImageButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {p1, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    :cond_0
    return-void

    .line 1232
    :cond_1
    sget-object v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$29;->$SwitchMap$com$helpshift$conversation$dto$IssueState:[I

    invoke-virtual {p1}, Lcom/helpshift/conversation/dto/IssueState;->ordinal()I

    move-result p1

    aget p1, v0, p1

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 1242
    :pswitch_0
    iput-boolean v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->awaitingUserInputForBotStep:Z

    .line 1243
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->removeOptionsMessageFromUI()V

    .line 1245
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleConversationRejectedState()V

    .line 1248
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateUIOnNewMessageReceived()V

    goto :goto_0

    .line 1235
    :pswitch_1
    iput-boolean v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->awaitingUserInputForBotStep:Z

    .line 1236
    sget-object p1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->START_NEW_CONVERSATION:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showStartNewConversation(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    .line 1238
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateUIOnNewMessageReceived()V

    .line 1253
    :goto_0
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateUserInputState()V

    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public onListPickerSearchQueryChange(Ljava/lang/String;)V
    .locals 1

    .line 1523
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->listPickerVM:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    if-eqz v0, :cond_0

    .line 1524
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->listPickerVM:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/viewmodel/ListPickerVM;->onListPickerSearchQueryChange(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public onNetworkAvailable()V
    .locals 2

    .line 1172
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$16;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$16;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public onNetworkUnAvailable()V
    .locals 2

    .line 1191
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$17;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$17;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public onNewConversationButtonClicked()V
    .locals 5

    .line 1368
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->stopLiveUpdates()V

    .line 1369
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 1370
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->setStartNewConversationButtonClicked(Lcom/helpshift/conversation/activeconversation/model/Conversation;ZZ)V

    .line 1371
    iget-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showConversationHistory:Z

    if-eqz v0, :cond_1

    .line 1374
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->hideAllFooterWidgets()V

    .line 1377
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getOpenConversationWithMessages()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    if-nez v0, :cond_0

    .line 1382
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->createLocalPreIssueConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 1386
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->onNewConversationStarted(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 1388
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->refreshVM()V

    .line 1389
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderMenuItems()V

    .line 1390
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->initMessagesList()V

    .line 1392
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->notifyRefreshList()V

    goto :goto_1

    .line 1406
    :cond_1
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "create_new_pre_issue"

    .line 1407
    iget-boolean v3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showConversationHistory:Z

    iget-object v4, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-virtual {v4}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->shouldShowConversationHistory()Z

    move-result v4

    if-eq v3, v4, :cond_2

    goto :goto_0

    :cond_2
    const/4 v2, 0x0

    :goto_0
    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1408
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->openFreshConversationScreen(Ljava/util/Map;)V

    :goto_1
    return-void
.end method

.method public onPause()V
    .locals 1

    const/4 v0, 0x0

    .line 215
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->setScreenVisibility(Z)V

    .line 216
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->setUserCanReadMessages(Z)V

    .line 217
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->markMessagesAsSeenOnExit()V

    .line 218
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->clearNotifications()V

    .line 219
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->resetIncrementMessageCountFlag()V

    .line 221
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->getReply()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->saveReplyText(Ljava/lang/String;)V

    return-void
.end method

.method public onResume()V
    .locals 1

    .line 206
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->refreshVM()V

    .line 207
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderMenuItems()V

    const/4 v0, 0x1

    .line 208
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->setScreenVisibility(Z)V

    .line 209
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->setUserCanReadMessages(Z)V

    .line 210
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->markMessagesAsSeenOnEntry()V

    .line 211
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->clearNotifications()V

    return-void
.end method

.method public onScrollJumperViewClicked()V
    .locals 0

    .line 1908
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->notifyRendererForScrollToBottom()V

    return-void
.end method

.method public onScrolledToBottom()V
    .locals 2

    .line 1912
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->scrollJumperViewState:Lcom/helpshift/widget/MutableScrollJumperViewState;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableScrollJumperViewState;->setVisible(Z)V

    .line 1914
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->scrollJumperViewState:Lcom/helpshift/widget/MutableScrollJumperViewState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableScrollJumperViewState;->setShouldShowUnreadMessagesIndicator(Z)V

    return-void
.end method

.method public onScrolledToTop()V
    .locals 2

    .line 1923
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->historyLoadingViewState:Lcom/helpshift/widget/MutableHistoryLoadingViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableHistoryLoadingViewState;->getState()Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    move-result-object v0

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->NONE:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    if-ne v0, v1, :cond_0

    .line 1924
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->loadHistoryMessagesInternal()V

    :cond_0
    return-void
.end method

.method public onScrolling()V
    .locals 2

    .line 1918
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->scrollJumperViewState:Lcom/helpshift/widget/MutableScrollJumperViewState;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableScrollJumperViewState;->setVisible(Z)V

    return-void
.end method

.method public onSkipClick()V
    .locals 3

    .line 877
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateLastUserActivityTime()V

    .line 879
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->botMessageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 880
    instance-of v1, v0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    if-eqz v1, :cond_0

    .line 884
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->clearUserReplyDraft()V

    .line 887
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->disableUserInputOptions()V

    .line 889
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v2, Lcom/helpshift/conversation/viewmodel/ConversationalVM$12;

    invoke-direct {v2, p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$12;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    invoke-virtual {v1, v2}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    .line 908
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->hideSkipButton()V

    return-void
.end method

.method public onUIMessageListUpdated()V
    .locals 0

    .line 1442
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateReplyBoxVisibility()V

    return-void
.end method

.method protected prefillReplyBox()V
    .locals 3

    .line 360
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getUserReplyText()Ljava/lang/String;

    move-result-object v0

    .line 364
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    .line 365
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v2, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->containsAtleastOneUserMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v1

    if-nez v1, :cond_0

    .line 366
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getConversationArchivalPrefillText()Ljava/lang/String;

    move-result-object v0

    .line 368
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 369
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v1, "conversationPrefillText"

    invoke-virtual {v0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    :cond_0
    if-eqz v0, :cond_1

    .line 378
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyFieldViewState:Lcom/helpshift/widget/MutableReplyFieldViewState;

    invoke-virtual {v1, v0}, Lcom/helpshift/widget/MutableReplyFieldViewState;->setReplyText(Ljava/lang/String;)V

    :cond_1
    return-void
.end method

.method public prependConversations(Ljava/util/List;Z)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;Z)V"
        }
    .end annotation

    .line 1565
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_1

    if-nez p2, :cond_0

    .line 1572
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    new-instance p2, Ljava/util/ArrayList;

    invoke-direct {p2}, Ljava/util/ArrayList;-><init>()V

    const/4 v0, 0x0

    invoke-virtual {p1, p2, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->prependMessages(Ljava/util/List;Z)V

    :cond_0
    return-void

    .line 1577
    :cond_1
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getUIConversations()Ljava/util/List;

    move-result-object v0

    .line 1580
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 1581
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1582
    invoke-direct {p0, v2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getUIMessagesForHistory(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/util/List;

    move-result-object v2

    invoke-interface {v1, v2}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    goto :goto_0

    .line 1585
    :cond_2
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    if-eqz p1, :cond_3

    .line 1586
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->updateUIConversationOrder(Ljava/util/List;)V

    .line 1587
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {p1, v1, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->prependMessages(Ljava/util/List;Z)V

    :cond_3
    return-void
.end method

.method public pushAnalyticsEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/analytics/AnalyticsEventType;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 1754
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object v0

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V

    return-void
.end method

.method public refreshAll()V
    .locals 1

    .line 1890
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-eqz v0, :cond_0

    .line 1891
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->notifyRefreshList()V

    :cond_0
    return-void
.end method

.method public refreshVM()V
    .locals 6

    .line 528
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->shouldShowReplyBoxOnConversationRejected()Z

    move-result v0

    .line 529
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    .line 530
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->widgetGateway:Lcom/helpshift/widget/WidgetGateway;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    invoke-virtual {v2, v3, v1, v0}, Lcom/helpshift/widget/WidgetGateway;->updateReplyBoxWidget(Lcom/helpshift/widget/MutableReplyBoxViewState;Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    .line 532
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->widgetGateway:Lcom/helpshift/widget/WidgetGateway;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->confirmationBoxViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {v2, v3, v1}, Lcom/helpshift/widget/WidgetGateway;->updateConfirmationBoxViewState(Lcom/helpshift/widget/MutableBaseViewState;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 533
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->widgetGateway:Lcom/helpshift/widget/WidgetGateway;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationFooterViewState:Lcom/helpshift/widget/MutableConversationFooterViewState;

    invoke-virtual {v2, v3, v1, v0}, Lcom/helpshift/widget/WidgetGateway;->updateConversationFooterViewState(Lcom/helpshift/widget/MutableConversationFooterViewState;Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    .line 537
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableReplyBoxViewState;->isVisible()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x2

    goto :goto_0

    :cond_0
    const/4 v0, -0x1

    .line 538
    :goto_0
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v2, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->setConversationViewState(I)V

    .line 541
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0, p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->registerMessagesObserver(Lcom/helpshift/common/util/HSListObserver;)V

    .line 544
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0, p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->setConversationVMCallback(Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;)V

    .line 548
    iget-object v0, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    const/4 v2, 0x1

    if-nez v0, :cond_1

    iget-object v0, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    if-nez v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 549
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getAllConversations()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-le v0, v2, :cond_2

    .line 550
    :cond_1
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/ConversationInboxPoller;->startChatPoller()V

    .line 554
    :cond_2
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->isSynced(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v0

    const/4 v3, 0x0

    if-nez v0, :cond_5

    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    .line 555
    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->containsAtleastOneUserMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v0

    if-eqz v0, :cond_5

    .line 556
    iget-object v0, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    iget-object v4, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v4}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v4

    sub-int/2addr v4, v2

    invoke-virtual {v0, v4}, Lcom/helpshift/common/util/HSObservableList;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 557
    instance-of v2, v0, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    if-eqz v2, :cond_4

    .line 558
    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    .line 559
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->getState()Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    move-result-object v2

    sget-object v4, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENT:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    if-eq v2, v4, :cond_3

    .line 560
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    invoke-virtual {v2, v3}, Lcom/helpshift/widget/MutableReplyBoxViewState;->setVisible(Z)V

    .line 565
    :cond_3
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    invoke-virtual {v2, v3, v4}, Lcom/helpshift/conversation/domainmodel/ConversationController;->isPreissueCreationInProgress(J)Z

    move-result v1

    if-eqz v1, :cond_4

    .line 566
    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENDING:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    :cond_4
    return-void

    .line 573
    :cond_5
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->isSynced(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v0

    if-nez v0, :cond_6

    .line 574
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-virtual {v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->shouldAutoFillPreissueFirstMessage()Z

    move-result v0

    if-eqz v0, :cond_6

    .line 576
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v4, "initialUserMessageToAutoSendInPreissue"

    invoke-virtual {v0, v4}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 577
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_6

    const-string v4, "Helpshift_ConvsatnlVM"

    const-string v5, "Auto-filing preissue with client set user message."

    .line 578
    invoke-static {v4, v5}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 579
    iget-object v4, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v4, v1, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateIsAutoFilledPreissueFlag(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    .line 580
    invoke-virtual {p0, v0, v3}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->createPreIssue(Ljava/lang/String;Z)V

    return-void

    .line 586
    :cond_6
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->isSynced(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v0

    if-eqz v0, :cond_7

    .line 588
    iget-object v0, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-direct {p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->evaluateBotMessages(Ljava/util/Collection;)Ljava/util/List;

    .line 594
    :cond_7
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateReplyBoxVisibility()V

    return-void
.end method

.method public renderMenuItems()V
    .locals 2

    .line 226
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyFieldViewState:Lcom/helpshift/widget/MutableReplyFieldViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableReplyFieldViewState;->getReplyText()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    xor-int/lit8 v0, v0, 0x1

    .line 227
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {v1, v0}, Lcom/helpshift/widget/MutableBaseViewState;->setEnabled(Z)V

    .line 228
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateAttachmentButtonViewState()V

    return-void
.end method

.method protected resetDefaultMenuItemsVisibility()V
    .locals 3

    .line 304
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->attachImageButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->widgetGateway:Lcom/helpshift/widget/WidgetGateway;

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 305
    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v2

    invoke-virtual {v1, v2}, Lcom/helpshift/widget/WidgetGateway;->getDefaultVisibilityForAttachImageButton(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v1

    .line 304
    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    return-void
.end method

.method public retryHistoryLoadingMessages()V
    .locals 2

    .line 1930
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->historyLoadingViewState:Lcom/helpshift/widget/MutableHistoryLoadingViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableHistoryLoadingViewState;->getState()Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    move-result-object v0

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->ERROR:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    if-ne v0, v1, :cond_0

    .line 1931
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->loadHistoryMessagesInternal()V

    :cond_0
    return-void
.end method

.method public retryMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 2

    .line 807
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public saveReplyText(Ljava/lang/String;)V
    .locals 1

    .line 309
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyFieldViewState:Lcom/helpshift/widget/MutableReplyFieldViewState;

    invoke-virtual {v0, p1}, Lcom/helpshift/widget/MutableReplyFieldViewState;->setReplyText(Ljava/lang/String;)V

    .line 310
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveUserReplyText(Ljava/lang/String;)V

    return-void
.end method

.method public sendScreenShot(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V
    .locals 2

    .line 1607
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$24;

    invoke-direct {v1, p0, p1, p2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$24;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public sendTextMessage()V
    .locals 3

    .line 696
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->getReply()Ljava/lang/String;

    move-result-object v0

    .line 698
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    return-void

    .line 702
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    const/4 v2, 0x1

    invoke-virtual {v1, v2}, Lcom/helpshift/conversation/domainmodel/ConversationController;->setPersistMessageBox(Z)V

    .line 703
    invoke-virtual {v0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sendTextMessage(Ljava/lang/String;)V

    return-void
.end method

.method protected sendTextMessage(Ljava/lang/String;)V
    .locals 3

    .line 730
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateLastUserActivityTime()V

    .line 732
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 733
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->containsAtleastOneUserMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v1

    if-nez v1, :cond_1

    .line 735
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->userVisibleCharacterCount(Ljava/lang/String;)I

    move-result v1

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-virtual {v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getMinimumConversationDescriptionLength()I

    move-result v2

    if-ge v1, v2, :cond_0

    .line 736
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    const/4 v0, 0x1

    invoke-interface {p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->showReplyValidationFailedError(I)V

    return-void

    .line 742
    :cond_0
    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 743
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->clearReply()V

    const/4 v0, 0x0

    .line 744
    invoke-virtual {p0, p1, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->createPreIssue(Ljava/lang/String;Z)V

    return-void

    .line 749
    :cond_1
    iget-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    if-nez v0, :cond_2

    .line 750
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sendNormalTextMessage(Ljava/lang/String;)V

    return-void

    .line 755
    :cond_2
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->botMessageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 756
    instance-of v1, v0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    if-nez v1, :cond_3

    .line 757
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sendNormalTextMessage(Ljava/lang/String;)V

    return-void

    .line 761
    :cond_3
    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    .line 762
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/TextInput;

    .line 763
    iget-object v2, v0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/TextInput;

    invoke-virtual {v2, p1}, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->validate(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_4

    .line 766
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    iget v0, v1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->keyboard:I

    invoke-interface {p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->showReplyValidationFailedError(I)V

    return-void

    .line 770
    :cond_4
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v1}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->hideReplyValidationFailedError()V

    .line 773
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->disableUserInputOptions()V

    .line 774
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->clearReply()V

    .line 776
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v2, Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;

    invoke-direct {v2, p0, p1, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;)V

    invoke-virtual {v1, v2}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public setConversationViewState(I)V
    .locals 1

    .line 1733
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->setConversationViewState(I)V

    return-void
.end method

.method public shouldShowUnreadMessagesIndicator()Z
    .locals 1

    .line 314
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->scrollJumperViewState:Lcom/helpshift/widget/MutableScrollJumperViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableScrollJumperViewState;->shouldShowUnreadMessagesIndicator()Z

    move-result v0

    return v0
.end method

.method public showEmptyListPickerView()V
    .locals 1

    .line 1506
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->showEmptyListPickerView()V

    return-void
.end method

.method showFakeTypingIndicator(Z)V
    .locals 2

    .line 673
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM$5;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$5;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Z)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public showPickerClearButton()V
    .locals 1

    .line 1536
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->showPickerClearButton()V

    return-void
.end method

.method protected showStartNewConversation(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V
    .locals 2

    .line 2019
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableReplyBoxViewState;->setVisible(Z)V

    .line 2020
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateAttachmentButtonViewState()V

    .line 2021
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->confirmationBoxViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 2022
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationFooterViewState:Lcom/helpshift/widget/MutableConversationFooterViewState;

    invoke-virtual {v0, p1}, Lcom/helpshift/widget/MutableConversationFooterViewState;->setState(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    return-void
.end method

.method public startLiveUpdates()V
    .locals 1

    .line 1795
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->startLiveUpdates()V

    return-void
.end method

.method public stopLiveUpdates()V
    .locals 1

    .line 1799
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->stopLiveUpdates()V

    return-void
.end method

.method public toggleReplySendButton(Z)V
    .locals 1

    .line 2026
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {v0, p1}, Lcom/helpshift/widget/MutableBaseViewState;->setEnabled(Z)V

    return-void
.end method

.method public unregisterRenderer()V
    .locals 2

    .line 290
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->unregisterConversationVMCallback()V

    .line 292
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 293
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->unregisterMessageListVMCallback()V

    .line 294
    iput-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    .line 297
    :cond_0
    iput-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    .line 299
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-virtual {v0, p0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->deleteObserver(Ljava/util/Observer;)V

    .line 300
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/account/AuthenticationFailureDM;->unregisterListener(Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;)V

    return-void
.end method

.method public update(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 3

    const-string v0, "Helpshift_ConvsatnlVM"

    .line 949
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "update called : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 950
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateUserInputState()V

    .line 951
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    if-eqz v0, :cond_0

    .line 952
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    :cond_0
    return-void
.end method

.method public bridge synthetic update(Ljava/lang/Object;)V
    .locals 0

    .line 95
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->update(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    return-void
.end method

.method public update(Ljava/util/Observable;Ljava/lang/Object;)V
    .locals 1

    .line 1804
    iget-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$25;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM$25;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Ljava/util/Observable;)V

    invoke-virtual {p2, v0}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public updateLastUserActivityTime()V
    .locals 4

    .line 1828
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 1829
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    .line 1830
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v2

    .line 1829
    invoke-virtual {v0, v1, v2, v3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateLastUserActivityTime(Lcom/helpshift/conversation/activeconversation/model/Conversation;J)V

    return-void
.end method

.method public updateListPickerOptions(Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/OptionUIModel;",
            ">;)V"
        }
    .end annotation

    .line 1495
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->updateListPickerOptions(Ljava/util/List;)V

    return-void
.end method

.method public updateMessages(II)V
    .locals 1

    .line 1883
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-eqz v0, :cond_0

    .line 1884
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->updateMessages(II)V

    :cond_0
    return-void
.end method

.method protected updateTypingIndicatorStatus(Z)V
    .locals 0

    if-eqz p1, :cond_0

    .line 1960
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {p1}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->showAgentTypingIndicator()V

    .line 1962
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->scrollJumperViewState:Lcom/helpshift/widget/MutableScrollJumperViewState;

    invoke-virtual {p1}, Lcom/helpshift/widget/MutableScrollJumperViewState;->isVisible()Z

    move-result p1

    xor-int/lit8 p1, p1, 0x1

    goto :goto_0

    .line 1965
    :cond_0
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {p1}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->hideAgentTypingIndicator()V

    const/4 p1, 0x0

    :goto_0
    if-eqz p1, :cond_1

    .line 1969
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->notifyRendererForScrollToBottom()V

    :cond_1
    return-void
.end method

.method protected updateUIOnNewMessageReceived()V
    .locals 1

    .line 1872
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->scrollJumperViewState:Lcom/helpshift/widget/MutableScrollJumperViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableScrollJumperViewState;->isVisible()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 1874
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showUnreadMessagesIndicator()V

    goto :goto_0

    .line 1877
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->notifyRendererForScrollToBottom()V

    :goto_0
    return-void
.end method

.method public updateUnreadMessageCountIndicator(Z)V
    .locals 1

    .line 1986
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->scrollJumperViewState:Lcom/helpshift/widget/MutableScrollJumperViewState;

    invoke-virtual {v0, p1}, Lcom/helpshift/widget/MutableScrollJumperViewState;->setShouldShowUnreadMessagesIndicator(Z)V

    return-void
.end method
