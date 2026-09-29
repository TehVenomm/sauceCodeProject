.class public Lcom/helpshift/conversation/CreatePreIssueDM;
.super Lcom/helpshift/common/domain/F;
.source "CreatePreIssueDM.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_CrtePreIsue"


# instance fields
.field private final conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

.field private final conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

.field private final greetingMessage:Ljava/lang/String;

.field private listener:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;",
            ">;"
        }
    .end annotation
.end field

.field private final userMessage:Ljava/lang/String;

.field private final viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;


# direct methods
.method public constructor <init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/ViewableConversation;Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 37
    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    .line 38
    iput-object p3, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 39
    iput-object p1, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    .line 40
    iput-object p2, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    .line 41
    new-instance p1, Ljava/lang/ref/WeakReference;

    invoke-direct {p1, p4}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object p1, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->listener:Ljava/lang/ref/WeakReference;

    .line 42
    iput-object p5, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->greetingMessage:Ljava/lang/String;

    .line 43
    iput-object p6, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->userMessage:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 48
    iget-object v0, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 51
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->isSynced(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v1

    if-eqz v1, :cond_0

    return-void

    :cond_0
    const-string v1, "Helpshift_CrtePreIsue"

    const-string v2, "Filing preissue with backend."

    .line 55
    invoke-static {v1, v2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 57
    iget-object v1, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v2, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->greetingMessage:Ljava/lang/String;

    iget-object v3, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->userMessage:Ljava/lang/String;

    invoke-virtual {v1, v0, v2, v3}, Lcom/helpshift/conversation/domainmodel/ConversationController;->createPreIssueNetwork(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;Ljava/lang/String;)V

    .line 60
    iget-object v1, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v1, v1, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v2

    invoke-virtual {v1, v0, v2, v3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateLastUserActivityTime(Lcom/helpshift/conversation/activeconversation/model/Conversation;J)V

    .line 62
    iget-object v1, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->listener:Ljava/lang/ref/WeakReference;

    invoke-virtual {v1}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 63
    iget-object v1, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->listener:Ljava/lang/ref/WeakReference;

    invoke-virtual {v1}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;

    iget-object v2, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-interface {v1, v2, v3}, Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;->onCreateConversationSuccess(J)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "Helpshift_CrtePreIsue"

    const-string v3, "Error filing a pre-issue"

    .line 68
    invoke-static {v2, v3, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 71
    iget-object v2, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->listener:Ljava/lang/ref/WeakReference;

    invoke-virtual {v2}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v2

    if-eqz v2, :cond_1

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getPreIssueId()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 72
    iget-object v0, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->listener:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;

    invoke-interface {v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;->onCreateConversationFailure(Ljava/lang/Exception;)V

    :cond_1
    :goto_0
    return-void
.end method

.method public setListener(Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;)V
    .locals 1

    .line 78
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/helpshift/conversation/CreatePreIssueDM;->listener:Ljava/lang/ref/WeakReference;

    return-void
.end method
