.class public Lcom/helpshift/support/controllers/SupportController;
.super Ljava/lang/Object;
.source "SupportController.java"

# interfaces
.implements Lcom/helpshift/support/contracts/SearchResultListener;
.implements Lcom/helpshift/support/contracts/ScreenshotPreviewListener;


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_SupportContr"


# instance fields
.field private final KEY_CONVERSATION_ADD_TO_BACK_STACK:Ljava/lang/String;

.field private final KEY_CONVERSATION_BUNDLE:Ljava/lang/String;

.field private final KEY_SUPPORT_CONTROLLER_STARTED_STATE:Ljava/lang/String;

.field private final bundle:Landroid/os/Bundle;

.field private final context:Landroid/content/Context;

.field private conversationAddToBackStack:Z

.field private conversationBundle:Landroid/os/Bundle;

.field private fragmentManager:Landroidx/fragment/app/FragmentManager;

.field private isControllerStarted:Z

.field private searchPerformed:Z

.field private sourceSearchQuery:Ljava/lang/String;

.field private supportMode:I

.field private final supportScreenView:Lcom/helpshift/support/contracts/SupportScreenView;


# direct methods
.method public constructor <init>(Landroid/content/Context;Lcom/helpshift/support/contracts/SupportScreenView;Landroidx/fragment/app/FragmentManager;Landroid/os/Bundle;)V
    .locals 1

    .line 85
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "key_support_controller_started"

    .line 70
    iput-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->KEY_SUPPORT_CONTROLLER_STARTED_STATE:Ljava/lang/String;

    const-string v0, "key_conversation_bundle"

    .line 71
    iput-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->KEY_CONVERSATION_BUNDLE:Ljava/lang/String;

    const-string v0, "key_conversation_add_to_back_stack"

    .line 72
    iput-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->KEY_CONVERSATION_ADD_TO_BACK_STACK:Ljava/lang/String;

    const/4 v0, 0x0

    .line 80
    iput-boolean v0, p0, Lcom/helpshift/support/controllers/SupportController;->searchPerformed:Z

    .line 86
    iput-object p1, p0, Lcom/helpshift/support/controllers/SupportController;->context:Landroid/content/Context;

    .line 87
    iput-object p2, p0, Lcom/helpshift/support/controllers/SupportController;->supportScreenView:Lcom/helpshift/support/contracts/SupportScreenView;

    .line 88
    iput-object p3, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    .line 89
    iput-object p4, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    return-void
.end method

.method private clearConversationStack()V
    .locals 5

    .line 649
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->getFragments()Ljava/util/List;

    move-result-object v0

    .line 652
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v1

    const/4 v2, 0x1

    sub-int/2addr v1, v2

    :goto_0
    if-ltz v1, :cond_3

    .line 653
    invoke-interface {v0, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroidx/fragment/app/Fragment;

    .line 656
    instance-of v4, v3, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    if-nez v4, :cond_0

    instance-of v4, v3, Lcom/helpshift/support/conversations/BaseConversationFragment;

    if-nez v4, :cond_0

    instance-of v4, v3, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;

    if-nez v4, :cond_0

    instance-of v4, v3, Lcom/helpshift/support/conversations/AuthenticationFailureFragment;

    if-eqz v4, :cond_2

    :cond_0
    if-nez v1, :cond_1

    .line 662
    iget-object v4, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-static {v4, v3}, Lcom/helpshift/support/util/FragmentUtil;->removeFragment(Landroidx/fragment/app/FragmentManager;Landroidx/fragment/app/Fragment;)V

    .line 667
    iget-object v4, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-virtual {v4}, Landroidx/fragment/app/FragmentManager;->getFragments()Ljava/util/List;

    move-result-object v4

    if-eqz v4, :cond_2

    .line 668
    invoke-interface {v4}, Ljava/util/List;->size()I

    move-result v4

    if-lez v4, :cond_2

    .line 669
    iget-object v4, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-static {v4, v3}, Lcom/helpshift/support/util/FragmentUtil;->popBackStack(Landroidx/fragment/app/FragmentManager;Ljava/lang/String;)V

    goto :goto_1

    .line 677
    :cond_1
    iget-object v4, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-static {v4, v3}, Lcom/helpshift/support/util/FragmentUtil;->popBackStack(Landroidx/fragment/app/FragmentManager;Ljava/lang/String;)V

    :cond_2
    :goto_1
    add-int/lit8 v1, v1, -0x1

    goto :goto_0

    .line 684
    :cond_3
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-string v1, "HSConversationFragment"

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    if-eqz v0, :cond_4

    .line 686
    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v0}, Lcom/helpshift/support/util/FragmentUtil;->popBackStackImmediate(Landroidx/fragment/app/FragmentManager;Ljava/lang/String;)V

    const/4 v0, 0x1

    goto :goto_2

    :cond_4
    const/4 v0, 0x0

    :goto_2
    if-nez v0, :cond_5

    .line 690
    iput-boolean v2, p0, Lcom/helpshift/support/controllers/SupportController;->conversationAddToBackStack:Z

    :cond_5
    return-void
.end method

.method private handleCustomContactUsFlows()Z
    .locals 2

    .line 499
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    if-nez v0, :cond_0

    .line 501
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-static {v0}, Lcom/helpshift/support/util/FragmentUtil;->getFaqFlowFragment(Landroidx/fragment/app/FragmentManager;)Lcom/helpshift/support/fragments/FaqFlowFragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 503
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getCustomContactUsFlows()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 504
    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_0

    const/4 v1, 0x1

    .line 505
    invoke-virtual {p0, v0, v1}, Lcom/helpshift/support/controllers/SupportController;->startDynamicForm(Ljava/util/List;Z)V

    return v1

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method private isDuplicateFAQScreenAlreadyOpen(Landroid/os/Bundle;)Z
    .locals 4

    .line 337
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-static {v0}, Lcom/helpshift/support/util/FragmentUtil;->getTopMostFragment(Landroidx/fragment/app/FragmentManager;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    .line 338
    instance-of v1, v0, Lcom/helpshift/support/fragments/FaqFlowFragment;

    const/4 v2, 0x0

    if-eqz v1, :cond_2

    .line 339
    check-cast v0, Lcom/helpshift/support/fragments/FaqFlowFragment;

    .line 340
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getFaqFlowController()Lcom/helpshift/support/controllers/FaqFlowController;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 342
    invoke-virtual {v0}, Lcom/helpshift/support/controllers/FaqFlowController;->getTopMostFaqFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    .line 343
    instance-of v1, v0, Lcom/helpshift/support/fragments/SingleQuestionFragment;

    const/4 v3, 0x1

    if-eqz v1, :cond_1

    .line 344
    check-cast v0, Lcom/helpshift/support/fragments/SingleQuestionFragment;

    const-string v1, "questionPublishId"

    .line 345
    invoke-virtual {p1, v1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 346
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getQuestionPublishId()Ljava/lang/String;

    move-result-object v0

    if-eqz p1, :cond_0

    .line 350
    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 v2, 0x1

    :cond_0
    return v2

    :cond_1
    return v3

    :cond_2
    return v2
.end method

.method private replaceConversationFlow(Landroid/os/Bundle;)V
    .locals 4

    const-string v0, "conversationIdInPush"

    .line 126
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getLong(Ljava/lang/String;)J

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    .line 127
    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    if-eqz v1, :cond_0

    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    const-string v2, "issueId"

    .line 128
    invoke-virtual {v1, v2}, Landroid/os/Bundle;->getLong(Ljava/lang/String;)J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    .line 131
    :goto_0
    invoke-virtual {v0, v1}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x1

    xor-int/2addr v0, v1

    const/4 v2, 0x0

    .line 134
    iget-object v3, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-virtual {v3}, Landroidx/fragment/app/FragmentManager;->getFragments()Ljava/util/List;

    move-result-object v3

    if-eqz v0, :cond_1

    .line 140
    invoke-direct {p0}, Lcom/helpshift/support/controllers/SupportController;->clearConversationStack()V

    goto :goto_1

    .line 146
    :cond_1
    invoke-interface {v3}, Ljava/util/List;->size()I

    move-result v0

    if-lez v0, :cond_4

    .line 151
    invoke-interface {v3}, Ljava/util/List;->size()I

    move-result v0

    sub-int/2addr v0, v1

    invoke-interface {v3, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/fragment/app/Fragment;

    .line 154
    instance-of v3, v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    if-eqz v3, :cond_2

    return-void

    .line 158
    :cond_2
    instance-of v0, v0, Lcom/helpshift/support/conversations/BaseConversationFragment;

    if-nez v0, :cond_3

    goto :goto_1

    :cond_3
    const/4 v1, 0x0

    :cond_4
    :goto_1
    if-eqz v1, :cond_5

    .line 168
    iput-object p1, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    .line 169
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/SupportController;->startConversationFlow()V

    :cond_5
    return-void
.end method

.method private sendTicketAvoidedEvent()V
    .locals 5

    .line 532
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-static {v0}, Lcom/helpshift/support/util/FragmentUtil;->getSingleQuestionFragment(Landroidx/fragment/app/FragmentManager;)Lcom/helpshift/support/fragments/SingleQuestionFragment;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 534
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getQuestionId()Ljava/lang/String;

    move-result-object v0

    .line 535
    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-nez v1, :cond_1

    .line 536
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    const-string v2, "id"

    .line 537
    invoke-interface {v1, v2, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 538
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v0

    .line 540
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object v2

    invoke-interface {v2}, Lcom/helpshift/common/platform/Platform;->getConversationInboxDAO()Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    move-result-object v2

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    invoke-interface {v2, v3, v4}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getDescriptionDetail(J)Lcom/helpshift/conversation/dto/ConversationDetailDTO;

    move-result-object v0

    if-eqz v0, :cond_0

    const-string v2, "str"

    .line 542
    iget-object v0, v0, Lcom/helpshift/conversation/dto/ConversationDetailDTO;->title:Ljava/lang/String;

    invoke-interface {v1, v2, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 544
    :cond_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object v0

    sget-object v2, Lcom/helpshift/analytics/AnalyticsEventType;->TICKET_AVOIDED:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {v0, v2, v1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V

    :cond_1
    return-void
.end method

.method private showConversationFragment(ZLjava/lang/Long;Ljava/util/Map;)V
    .locals 7
    .param p2    # Ljava/lang/Long;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(Z",
            "Ljava/lang/Long;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Boolean;",
            ">;)V"
        }
    .end annotation

    const-string v0, "Helpshift_SupportContr"

    .line 386
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Starting conversation fragment: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    if-nez p1, :cond_1

    if-nez p2, :cond_0

    return-void

    .line 394
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    const-string v1, "issueId"

    invoke-virtual {p2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-virtual {v0, v1, v2, v3}, Landroid/os/Bundle;->putLong(Ljava/lang/String;J)V

    .line 396
    :cond_1
    iget-object p2, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    const-string v0, "show_conv_history"

    invoke-virtual {p2, v0, p1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 397
    invoke-interface {p3}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Ljava/lang/String;

    .line 398
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    invoke-interface {p3, p2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/Boolean;

    invoke-virtual {v1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v1

    invoke-virtual {v0, p2, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    goto :goto_0

    .line 401
    :cond_2
    iget-object p1, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    invoke-static {p1}, Lcom/helpshift/support/conversations/ConversationalFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/conversations/ConversationalFragment;

    move-result-object v2

    const/4 p1, 0x0

    .line 403
    iget-boolean p2, p0, Lcom/helpshift/support/controllers/SupportController;->conversationAddToBackStack:Z

    if-eqz p2, :cond_3

    .line 404
    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    .line 407
    invoke-direct {p0}, Lcom/helpshift/support/controllers/SupportController;->clearConversationStack()V

    :cond_3
    move-object v4, p1

    .line 409
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget v1, Lcom/helpshift/R$id;->flow_fragment_container:I

    const-string v3, "HSConversationFragment"

    const/4 v5, 0x0

    const/4 v6, 0x0

    invoke-static/range {v0 .. v6}, Lcom/helpshift/support/util/FragmentUtil;->startFragment(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Ljava/lang/String;ZZ)V

    return-void
.end method

.method private showNewConversationFragment()V
    .locals 8

    const-string v0, "Helpshift_SupportContr"

    const-string v1, "Starting new conversation fragment"

    .line 363
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 364
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    const-string v1, "search_performed"

    iget-boolean v2, p0, Lcom/helpshift/support/controllers/SupportController;->searchPerformed:Z

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 365
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    const-string v1, "source_search_query"

    iget-object v2, p0, Lcom/helpshift/support/controllers/SupportController;->sourceSearchQuery:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 366
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    invoke-static {v0}, Lcom/helpshift/support/conversations/NewConversationFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/conversations/NewConversationFragment;

    move-result-object v3

    .line 368
    iget-boolean v0, p0, Lcom/helpshift/support/controllers/SupportController;->conversationAddToBackStack:Z

    if-eqz v0, :cond_0

    .line 369
    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    .line 372
    invoke-direct {p0}, Lcom/helpshift/support/controllers/SupportController;->clearConversationStack()V

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    move-object v5, v0

    .line 374
    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget v2, Lcom/helpshift/R$id;->flow_fragment_container:I

    const-string v4, "HSNewConversationFragment"

    const/4 v6, 0x0

    const/4 v7, 0x0

    invoke-static/range {v1 .. v7}, Lcom/helpshift/support/util/FragmentUtil;->startFragment(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Ljava/lang/String;ZZ)V

    return-void
.end method

.method private startConversationFlowInternal(Ljava/util/Map;)V
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Boolean;",
            ">;)V"
        }
    .end annotation

    .line 215
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    if-nez v0, :cond_0

    .line 216
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    iput-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    .line 219
    :cond_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object v0

    const-string v1, "disableInAppConversation"

    invoke-virtual {v0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    .line 220
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->shouldShowConversationHistory()Z

    move-result v1

    const/4 v2, 0x0

    const/4 v3, 0x1

    if-eqz v1, :cond_1

    if-nez v0, :cond_1

    .line 224
    invoke-direct {p0, v3, v2, p1}, Lcom/helpshift/support/controllers/SupportController;->showConversationFragment(ZLjava/lang/Long;Ljava/util/Map;)V

    return-void

    .line 228
    :cond_1
    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    const-string v4, "conversationIdInPush"

    const-wide/16 v5, 0x0

    invoke-virtual {v1, v4, v5, v6}, Landroid/os/Bundle;->getLong(Ljava/lang/String;J)J

    move-result-wide v7

    cmp-long v1, v7, v5

    const/4 v4, 0x0

    if-eqz v1, :cond_2

    .line 230
    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    const-string v5, "conversationIdInPush"

    invoke-virtual {v1, v5}, Landroid/os/Bundle;->remove(Ljava/lang/String;)V

    .line 231
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v1

    .line 232
    invoke-virtual {v1, v7, v8}, Lcom/helpshift/conversation/domainmodel/ConversationController;->shouldOpenConversationFromNotification(J)Z

    move-result v1

    if-eqz v1, :cond_2

    .line 235
    invoke-static {v7, v8}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    invoke-direct {p0, v4, v0, p1}, Lcom/helpshift/support/controllers/SupportController;->showConversationFragment(ZLjava/lang/Long;Ljava/util/Map;)V

    return-void

    :cond_2
    if-nez v0, :cond_3

    .line 242
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getActiveConversationOrPreIssue()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    if-eqz v0, :cond_3

    .line 244
    iget-object v2, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    :cond_3
    if-nez v2, :cond_7

    .line 249
    invoke-static {}, Lcom/helpshift/support/flows/CustomContactUsFlowListHolder;->getFlowList()Ljava/util/List;

    move-result-object p1

    if-eqz p1, :cond_6

    .line 250
    invoke-interface {p1}, Ljava/util/List;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_4

    goto :goto_0

    .line 259
    :cond_4
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->getBackStackEntryCount()I

    move-result v0

    sub-int/2addr v0, v3

    .line 260
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/SupportController;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    invoke-virtual {v1, v0}, Landroidx/fragment/app/FragmentManager;->getBackStackEntryAt(I)Landroidx/fragment/app/FragmentManager$BackStackEntry;

    move-result-object v0

    if-eqz v0, :cond_5

    .line 262
    invoke-interface {v0}, Landroidx/fragment/app/FragmentManager$BackStackEntry;->getName()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_5

    .line 263
    const-class v1, Lcom/helpshift/support/conversations/ConversationalFragment;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_5

    .line 264
    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-static {v1, v0}, Lcom/helpshift/support/util/FragmentUtil;->popBackStackImmediate(Landroidx/fragment/app/FragmentManager;Ljava/lang/String;)V

    .line 267
    :cond_5
    invoke-virtual {p0, p1, v3}, Lcom/helpshift/support/controllers/SupportController;->startDynamicForm(Ljava/util/List;Z)V

    goto :goto_1

    .line 251
    :cond_6
    :goto_0
    invoke-direct {p0}, Lcom/helpshift/support/controllers/SupportController;->showNewConversationFragment()V

    goto :goto_1

    .line 271
    :cond_7
    invoke-direct {p0, v4, v2, p1}, Lcom/helpshift/support/controllers/SupportController;->showConversationFragment(ZLjava/lang/Long;Ljava/util/Map;)V

    :goto_1
    return-void
.end method


# virtual methods
.method public actionDone()V
    .locals 6

    .line 514
    invoke-direct {p0}, Lcom/helpshift/support/controllers/SupportController;->sendTicketAvoidedEvent()V

    .line 516
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v0

    .line 517
    new-instance v1, Lcom/helpshift/conversation/dto/ConversationDetailDTO;

    const-string v2, ""

    invoke-static {}, Ljava/lang/System;->nanoTime()J

    move-result-wide v3

    const/4 v5, 0x0

    invoke-direct {v1, v2, v3, v4, v5}, Lcom/helpshift/conversation/dto/ConversationDetailDTO;-><init>(Ljava/lang/String;JI)V

    .line 519
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object v2

    invoke-interface {v2}, Lcom/helpshift/common/platform/Platform;->getConversationInboxDAO()Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    move-result-object v2

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    invoke-interface {v2, v3, v4, v1}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveDescriptionDetail(JLcom/helpshift/conversation/dto/ConversationDetailDTO;)V

    .line 521
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getConversationInboxDAO()Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    move-result-object v1

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    const/4 v0, 0x0

    invoke-interface {v1, v2, v3, v0}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveImageAttachment(JLcom/helpshift/conversation/dto/ImagePickerFile;)V

    .line 522
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/SupportController;->getSupportMode()I

    move-result v0

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    .line 523
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->supportScreenView:Lcom/helpshift/support/contracts/SupportScreenView;

    invoke-interface {v0}, Lcom/helpshift/support/contracts/SupportScreenView;->exitSdkSession()V

    goto :goto_0

    .line 526
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/SupportController;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    const-class v1, Lcom/helpshift/support/conversations/NewConversationFragment;

    .line 527
    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    .line 526
    invoke-static {v0, v1}, Lcom/helpshift/support/util/FragmentUtil;->popBackStackImmediate(Landroidx/fragment/app/FragmentManager;Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method public addScreenshot(Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 2

    .line 589
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-class v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/support/util/FragmentUtil;->popBackStack(Landroidx/fragment/app/FragmentManager;Ljava/lang/String;)V

    .line 590
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-string v1, "HSNewConversationFragment"

    .line 591
    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/conversations/NewConversationFragment;

    if-eqz v0, :cond_0

    .line 593
    sget-object v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->ADD:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    invoke-virtual {v0, v1, p1}, Lcom/helpshift/support/conversations/NewConversationFragment;->handleScreenshotAction(Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;Lcom/helpshift/conversation/dto/ImagePickerFile;)Z

    :cond_0
    return-void
.end method

.method public changeScreenshot(Landroid/os/Bundle;)V
    .locals 2

    .line 619
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->supportScreenView:Lcom/helpshift/support/contracts/SupportScreenView;

    const/4 v1, 0x1

    invoke-interface {v0, v1, p1}, Lcom/helpshift/support/contracts/SupportScreenView;->launchImagePicker(ZLandroid/os/Bundle;)V

    return-void
.end method

.method public getFragmentManager()Landroidx/fragment/app/FragmentManager;
    .locals 1

    .line 478
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    return-object v0
.end method

.method public getSupportMode()I
    .locals 1

    .line 474
    iget v0, p0, Lcom/helpshift/support/controllers/SupportController;->supportMode:I

    return v0
.end method

.method public onAdminSuggestedQuestionSelected(Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;)V
    .locals 3

    .line 576
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->context:Landroid/content/Context;

    invoke-static {v0}, Lcom/helpshift/support/util/Styles;->isTablet(Landroid/content/Context;)Z

    move-result v0

    .line 577
    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    const-string v2, "questionPublishId"

    invoke-virtual {v1, v2, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 578
    iget-object p1, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    const-string v1, "questionLanguage"

    invoke-virtual {p1, v1, p2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 579
    iget-object p1, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget p2, Lcom/helpshift/R$id;->flow_fragment_container:I

    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    const/4 v2, 0x3

    .line 582
    invoke-static {v1, v2, v0, p3}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->newInstance(Landroid/os/Bundle;IZLcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;)Lcom/helpshift/support/fragments/SingleQuestionFragment;

    move-result-object p3

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 579
    invoke-static {p1, p2, p3, v0, v1}, Lcom/helpshift/support/util/FragmentUtil;->startFragmentWithBackStack(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Z)V

    return-void
.end method

.method public onAuthenticationFailure()V
    .locals 0

    .line 276
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/SupportController;->showAuthenticationFailureFragment()V

    return-void
.end method

.method public onContactUsClicked(Ljava/lang/String;)V
    .locals 1

    .line 482
    invoke-direct {p0}, Lcom/helpshift/support/controllers/SupportController;->handleCustomContactUsFlows()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 486
    :cond_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 487
    iput-object p1, p0, Lcom/helpshift/support/controllers/SupportController;->sourceSearchQuery:Ljava/lang/String;

    .line 490
    :cond_1
    iget-object p1, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    const/4 v0, 0x1

    invoke-virtual {p0, p1, v0}, Lcom/helpshift/support/controllers/SupportController;->startConversationFlow(Landroid/os/Bundle;Z)V

    return-void
.end method

.method public onFragmentManagerUpdate(Landroidx/fragment/app/FragmentManager;)V
    .locals 0

    .line 93
    iput-object p1, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    return-void
.end method

.method public onNewIntent(Landroid/os/Bundle;)V
    .locals 3

    const-string v0, "support_mode"

    .line 628
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result v0

    const/4 v1, 0x1

    if-eq v0, v1, :cond_1

    const/4 v2, 0x4

    if-eq v0, v2, :cond_0

    .line 638
    invoke-static {}, Lcom/helpshift/support/flows/CustomContactUsFlowListHolder;->getFlowList()Ljava/util/List;

    move-result-object v0

    invoke-virtual {p0, p1, v1, v0}, Lcom/helpshift/support/controllers/SupportController;->startFaqFlow(Landroid/os/Bundle;ZLjava/util/List;)V

    goto :goto_0

    :cond_0
    const-string v0, "flow_title"

    .line 635
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-static {}, Lcom/helpshift/support/flows/DynamicFormFlowListHolder;->getFlowList()Ljava/util/List;

    move-result-object v0

    invoke-virtual {p0, p1, v0, v1}, Lcom/helpshift/support/controllers/SupportController;->startDynamicForm(Ljava/lang/String;Ljava/util/List;Z)V

    goto :goto_0

    .line 632
    :cond_1
    invoke-direct {p0, p1}, Lcom/helpshift/support/controllers/SupportController;->replaceConversationFlow(Landroid/os/Bundle;)V

    :goto_0
    return-void
.end method

.method public onQuestionSelected(Ljava/lang/String;Ljava/util/ArrayList;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 551
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->context:Landroid/content/Context;

    invoke-static {v0}, Lcom/helpshift/support/util/Styles;->isTablet(Landroid/content/Context;)Z

    move-result v0

    .line 552
    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    const-string v2, "questionPublishId"

    invoke-virtual {v1, v2, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    if-eqz p2, :cond_0

    .line 554
    iget-object p1, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    const-string v1, "searchTerms"

    invoke-virtual {p1, v1, p2}, Landroid/os/Bundle;->putStringArrayList(Ljava/lang/String;Ljava/util/ArrayList;)V

    .line 556
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget p2, Lcom/helpshift/R$id;->flow_fragment_container:I

    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    const/4 v2, 0x2

    const/4 v3, 0x0

    .line 558
    invoke-static {v1, v2, v0, v3}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->newInstance(Landroid/os/Bundle;IZLcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;)Lcom/helpshift/support/fragments/SingleQuestionFragment;

    move-result-object v0

    const/4 v1, 0x0

    .line 556
    invoke-static {p1, p2, v0, v3, v1}, Lcom/helpshift/support/util/FragmentUtil;->startFragmentWithBackStack(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Z)V

    return-void
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 2

    const-string v0, "key_support_controller_started"

    .line 695
    iget-boolean v1, p0, Lcom/helpshift/support/controllers/SupportController;->isControllerStarted:Z

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v0, "key_conversation_bundle"

    .line 696
    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    const-string v0, "key_conversation_add_to_back_stack"

    .line 697
    iget-boolean v1, p0, Lcom/helpshift/support/controllers/SupportController;->conversationAddToBackStack:Z

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    return-void
.end method

.method public onUserSetupSyncCompleted()V
    .locals 1

    .line 738
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-direct {p0, v0}, Lcom/helpshift/support/controllers/SupportController;->startConversationFlowInternal(Ljava/util/Map;)V

    return-void
.end method

.method public onViewStateRestored(Landroid/os/Bundle;)V
    .locals 3

    .line 701
    iget-boolean v0, p0, Lcom/helpshift/support/controllers/SupportController;->isControllerStarted:Z

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const-string v0, "key_support_controller_started"

    .line 705
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_3

    const-string v0, "key_support_controller_started"

    .line 706
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v0

    iput-boolean v0, p0, Lcom/helpshift/support/controllers/SupportController;->isControllerStarted:Z

    .line 707
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    const-string v1, "support_mode"

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v0

    iput v0, p0, Lcom/helpshift/support/controllers/SupportController;->supportMode:I

    .line 709
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    if-eqz v0, :cond_3

    .line 710
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-string v1, "ScreenshotPreviewFragment"

    .line 711
    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    if-eqz v0, :cond_1

    .line 713
    invoke-virtual {v0, p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->setScreenshotPreviewListener(Lcom/helpshift/support/contracts/ScreenshotPreviewListener;)V

    .line 716
    :cond_1
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-string v1, "HSSearchResultFragment"

    .line 717
    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/SearchResultFragment;

    if-eqz v0, :cond_2

    .line 719
    invoke-virtual {v0, p0}, Lcom/helpshift/support/fragments/SearchResultFragment;->setSearchResultListener(Lcom/helpshift/support/contracts/SearchResultListener;)V

    .line 722
    :cond_2
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-string v1, "HSDynamicFormFragment"

    .line 723
    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/DynamicFormFragment;

    if-eqz v0, :cond_3

    .line 725
    invoke-virtual {v0, p0}, Lcom/helpshift/support/fragments/DynamicFormFragment;->setSupportController(Lcom/helpshift/support/controllers/SupportController;)V

    :cond_3
    const-string v0, "key_conversation_bundle"

    .line 730
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_4

    const-string v0, "key_conversation_add_to_back_stack"

    .line 731
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_4

    const-string v0, "key_conversation_bundle"

    .line 732
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getBundle(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    const-string v0, "key_conversation_add_to_back_stack"

    .line 733
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;)Z

    move-result p1

    iput-boolean p1, p0, Lcom/helpshift/support/controllers/SupportController;->conversationAddToBackStack:Z

    :cond_4
    return-void
.end method

.method public removeScreenshot()V
    .locals 3

    .line 609
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-class v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/support/util/FragmentUtil;->popBackStack(Landroidx/fragment/app/FragmentManager;Ljava/lang/String;)V

    .line 610
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-string v1, "HSNewConversationFragment"

    .line 611
    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/conversations/NewConversationFragment;

    if-eqz v0, :cond_0

    .line 613
    sget-object v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->REMOVE:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/support/conversations/NewConversationFragment;->handleScreenshotAction(Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;Lcom/helpshift/conversation/dto/ImagePickerFile;)Z

    :cond_0
    return-void
.end method

.method public removeScreenshotPreviewFragment()V
    .locals 2

    .line 624
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-class v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/support/util/FragmentUtil;->popBackStack(Landroidx/fragment/app/FragmentManager;Ljava/lang/String;)V

    return-void
.end method

.method public sendAnyway()V
    .locals 2

    .line 565
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object v0

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->TICKET_AVOIDANCE_FAILED:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {v0, v1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;)V

    .line 566
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/SupportController;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    const-class v1, Lcom/helpshift/support/fragments/SearchResultFragment;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/support/util/FragmentUtil;->popBackStackImmediate(Landroidx/fragment/app/FragmentManager;Ljava/lang/String;)V

    .line 567
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-string v1, "HSNewConversationFragment"

    .line 568
    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/conversations/NewConversationFragment;

    if-eqz v0, :cond_0

    .line 570
    invoke-virtual {v0}, Lcom/helpshift/support/conversations/NewConversationFragment;->startNewConversation()V

    :cond_0
    return-void
.end method

.method public sendScreenshot(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V
    .locals 2
    .param p2    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 599
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-class v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/support/util/FragmentUtil;->popBackStack(Landroidx/fragment/app/FragmentManager;Ljava/lang/String;)V

    .line 600
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-string v1, "HSConversationFragment"

    .line 601
    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/conversations/ConversationalFragment;

    if-eqz v0, :cond_0

    .line 603
    sget-object v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->SEND:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    invoke-virtual {v0, v1, p1, p2}, Lcom/helpshift/support/conversations/ConversationalFragment;->handleScreenshotAction(Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)Z

    :cond_0
    return-void
.end method

.method public setSearchPerformed(Z)V
    .locals 0

    .line 97
    iput-boolean p1, p0, Lcom/helpshift/support/controllers/SupportController;->searchPerformed:Z

    return-void
.end method

.method public showAuthenticationFailureFragment()V
    .locals 9

    const-string v0, "Helpshift_SupportContr"

    const-string v1, "Starting authentication failure fragment"

    .line 281
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 283
    invoke-static {}, Lcom/helpshift/support/conversations/AuthenticationFailureFragment;->newInstance()Lcom/helpshift/support/conversations/AuthenticationFailureFragment;

    move-result-object v4

    .line 285
    iget-boolean v0, p0, Lcom/helpshift/support/controllers/SupportController;->conversationAddToBackStack:Z

    if-eqz v0, :cond_0

    .line 286
    invoke-virtual {v4}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    move-object v6, v0

    .line 289
    invoke-direct {p0}, Lcom/helpshift/support/controllers/SupportController;->clearConversationStack()V

    .line 290
    iget-object v2, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget v3, Lcom/helpshift/R$id;->flow_fragment_container:I

    const-string v5, "HSAuthenticationFailureFragment"

    const/4 v7, 0x0

    const/4 v8, 0x0

    invoke-static/range {v2 .. v8}, Lcom/helpshift/support/util/FragmentUtil;->startFragment(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Ljava/lang/String;ZZ)V

    return-void
.end method

.method public showConversationSearchResultFragment(Landroid/os/Bundle;)V
    .locals 4

    .line 419
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget v1, Lcom/helpshift/R$id;->flow_fragment_container:I

    .line 421
    invoke-static {p1, p0}, Lcom/helpshift/support/fragments/SearchResultFragment;->newInstance(Landroid/os/Bundle;Lcom/helpshift/support/contracts/SearchResultListener;)Lcom/helpshift/support/fragments/SearchResultFragment;

    move-result-object p1

    const-string v2, "HSSearchResultFragment"

    const/4 v3, 0x0

    .line 419
    invoke-static {v0, v1, p1, v2, v3}, Lcom/helpshift/support/util/FragmentUtil;->startFragmentWithBackStack(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Z)V

    return-void
.end method

.method public showUserSetupFragment()V
    .locals 7

    .line 300
    invoke-static {}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->newInstance()Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;

    move-result-object v2

    .line 302
    iget-boolean v0, p0, Lcom/helpshift/support/controllers/SupportController;->conversationAddToBackStack:Z

    if-eqz v0, :cond_0

    .line 303
    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    .line 306
    invoke-direct {p0}, Lcom/helpshift/support/controllers/SupportController;->clearConversationStack()V

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    move-object v4, v0

    .line 308
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget v1, Lcom/helpshift/R$id;->flow_fragment_container:I

    const-string v3, "HSUserSetupFragment"

    const/4 v5, 0x0

    const/4 v6, 0x0

    invoke-static/range {v0 .. v6}, Lcom/helpshift/support/util/FragmentUtil;->startFragment(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Ljava/lang/String;ZZ)V

    return-void
.end method

.method public start()V
    .locals 4

    .line 101
    iget-boolean v0, p0, Lcom/helpshift/support/controllers/SupportController;->isControllerStarted:Z

    const/4 v1, 0x1

    if-nez v0, :cond_2

    .line 102
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    const-string v2, "support_mode"

    const/4 v3, 0x0

    invoke-virtual {v0, v2, v3}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v0

    iput v0, p0, Lcom/helpshift/support/controllers/SupportController;->supportMode:I

    .line 103
    iget v0, p0, Lcom/helpshift/support/controllers/SupportController;->supportMode:I

    if-eq v0, v1, :cond_1

    const/4 v2, 0x4

    if-eq v0, v2, :cond_0

    .line 111
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    invoke-static {}, Lcom/helpshift/support/flows/CustomContactUsFlowListHolder;->getFlowList()Ljava/util/List;

    move-result-object v2

    invoke-virtual {p0, v0, v3, v2}, Lcom/helpshift/support/controllers/SupportController;->startFaqFlow(Landroid/os/Bundle;ZLjava/util/List;)V

    goto :goto_0

    .line 108
    :cond_0
    invoke-static {}, Lcom/helpshift/support/flows/DynamicFormFlowListHolder;->getFlowList()Ljava/util/List;

    move-result-object v0

    invoke-virtual {p0, v0, v3}, Lcom/helpshift/support/controllers/SupportController;->startDynamicForm(Ljava/util/List;Z)V

    goto :goto_0

    .line 105
    :cond_1
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    invoke-virtual {p0, v0, v3}, Lcom/helpshift/support/controllers/SupportController;->startConversationFlow(Landroid/os/Bundle;Z)V

    .line 115
    :cond_2
    :goto_0
    iput-boolean v1, p0, Lcom/helpshift/support/controllers/SupportController;->isControllerStarted:Z

    return-void
.end method

.method public startConversationFlow()V
    .locals 1

    .line 185
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-virtual {p0, v0}, Lcom/helpshift/support/controllers/SupportController;->startConversationFlow(Ljava/util/Map;)V

    return-void
.end method

.method public startConversationFlow(Landroid/os/Bundle;Z)V
    .locals 0

    .line 179
    iput-boolean p2, p0, Lcom/helpshift/support/controllers/SupportController;->conversationAddToBackStack:Z

    .line 180
    iput-object p1, p0, Lcom/helpshift/support/controllers/SupportController;->conversationBundle:Landroid/os/Bundle;

    .line 181
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/SupportController;->startConversationFlow()V

    return-void
.end method

.method public startConversationFlow(Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Boolean;",
            ">;)V"
        }
    .end annotation

    .line 193
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUserSetupDM()Lcom/helpshift/account/domainmodel/UserSetupDM;

    move-result-object v0

    .line 194
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserSetupDM;->getState()Lcom/helpshift/account/domainmodel/UserSetupState;

    move-result-object v0

    .line 199
    sget-object v1, Lcom/helpshift/support/controllers/SupportController$1;->$SwitchMap$com$helpshift$account$domainmodel$UserSetupState:[I

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserSetupState;->ordinal()I

    move-result v0

    aget v0, v1, v0

    packed-switch v0, :pswitch_data_0

    goto :goto_0

    .line 206
    :pswitch_0
    invoke-direct {p0, p1}, Lcom/helpshift/support/controllers/SupportController;->startConversationFlowInternal(Ljava/util/Map;)V

    goto :goto_0

    .line 203
    :pswitch_1
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/SupportController;->showUserSetupFragment()V

    :goto_0
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public startDynamicForm(ILjava/util/List;Z)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;Z)V"
        }
    .end annotation

    .line 466
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    if-eqz v0, :cond_0

    if-eqz p1, :cond_0

    .line 467
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    const-string v1, "flow_title"

    iget-object v2, p0, Lcom/helpshift/support/controllers/SupportController;->context:Landroid/content/Context;

    invoke-virtual {v2}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    invoke-virtual {v2, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, v1, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 469
    :cond_0
    invoke-virtual {p0, p2, p3}, Lcom/helpshift/support/controllers/SupportController;->startDynamicForm(Ljava/util/List;Z)V

    return-void
.end method

.method public startDynamicForm(Ljava/lang/String;Ljava/util/List;Z)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;Z)V"
        }
    .end annotation

    .line 458
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    if-eqz v0, :cond_0

    .line 459
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    const-string v1, "flow_title"

    invoke-virtual {v0, v1, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 461
    :cond_0
    invoke-virtual {p0, p2, p3}, Lcom/helpshift/support/controllers/SupportController;->startDynamicForm(Ljava/util/List;Z)V

    return-void
.end method

.method public startDynamicForm(Ljava/util/List;Z)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;Z)V"
        }
    .end annotation

    .line 442
    iget-object v0, p0, Lcom/helpshift/support/controllers/SupportController;->bundle:Landroid/os/Bundle;

    invoke-static {v0, p1, p0}, Lcom/helpshift/support/fragments/DynamicFormFragment;->newInstance(Landroid/os/Bundle;Ljava/util/List;Lcom/helpshift/support/controllers/SupportController;)Lcom/helpshift/support/fragments/DynamicFormFragment;

    move-result-object v3

    if-eqz p2, :cond_0

    .line 445
    const-class p1, Lcom/helpshift/support/fragments/DynamicFormFragment;

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    move-object v5, p1

    .line 447
    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget v2, Lcom/helpshift/R$id;->flow_fragment_container:I

    const-string v4, "HSDynamicFormFragment"

    const/4 v6, 0x0

    const/4 v7, 0x0

    invoke-static/range {v1 .. v7}, Lcom/helpshift/support/util/FragmentUtil;->startFragment(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Ljava/lang/String;ZZ)V

    return-void
.end method

.method public startFaqFlow(Landroid/os/Bundle;ZLjava/util/List;)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/os/Bundle;",
            "Z",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;)V"
        }
    .end annotation

    .line 319
    invoke-direct {p0, p1}, Lcom/helpshift/support/controllers/SupportController;->isDuplicateFAQScreenAlreadyOpen(Landroid/os/Bundle;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 322
    :cond_0
    invoke-static {p1, p3}, Lcom/helpshift/support/fragments/FaqFlowFragment;->newInstance(Landroid/os/Bundle;Ljava/util/List;)Lcom/helpshift/support/fragments/FaqFlowFragment;

    move-result-object v3

    const/4 p1, 0x0

    if-eqz p2, :cond_1

    .line 325
    const-class p1, Lcom/helpshift/support/fragments/FaqFlowFragment;

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    :cond_1
    move-object v5, p1

    .line 327
    iget-object v1, p0, Lcom/helpshift/support/controllers/SupportController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget v2, Lcom/helpshift/R$id;->flow_fragment_container:I

    const-string v4, "Helpshift_FaqFlowFrag"

    const/4 v6, 0x0

    const/4 v7, 0x0

    invoke-static/range {v1 .. v7}, Lcom/helpshift/support/util/FragmentUtil;->startFragment(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Ljava/lang/String;ZZ)V

    return-void
.end method

.method public startScreenshotPreviewFragment(Lcom/helpshift/conversation/dto/ImagePickerFile;Landroid/os/Bundle;Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;)V
    .locals 5

    .line 429
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/SupportController;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/util/FragmentUtil;->getScreenshotPreviewFragment(Landroidx/fragment/app/FragmentManager;)Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    move-result-object v0

    if-nez v0, :cond_0

    .line 431
    invoke-static {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->newInstance(Lcom/helpshift/support/contracts/ScreenshotPreviewListener;)Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    move-result-object v0

    .line 432
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/SupportController;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    sget v2, Lcom/helpshift/R$id;->flow_fragment_container:I

    const-string v3, "ScreenshotPreviewFragment"

    const/4 v4, 0x0

    invoke-static {v1, v2, v0, v3, v4}, Lcom/helpshift/support/util/FragmentUtil;->startFragmentWithBackStack(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Z)V

    .line 438
    :cond_0
    invoke-virtual {v0, p2, p1, p3}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->setParams(Landroid/os/Bundle;Lcom/helpshift/conversation/dto/ImagePickerFile;Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;)V

    return-void
.end method
