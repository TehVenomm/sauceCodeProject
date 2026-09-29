.class public Lcom/helpshift/delegate/UIThreadDelegateDecorator;
.super Ljava/lang/Object;
.source "UIThreadDelegateDecorator.java"


# instance fields
.field private authenticationFailedCalledList:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Boolean;",
            ">;"
        }
    .end annotation
.end field

.field private delegate:Lcom/helpshift/delegate/RootDelegate;

.field private domain:Lcom/helpshift/common/domain/Domain;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/Domain;)V
    .locals 0

    .line 17
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 18
    iput-object p1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->domain:Lcom/helpshift/common/domain/Domain;

    .line 19
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1}, Ljava/util/HashMap;-><init>()V

    iput-object p1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->authenticationFailedCalledList:Ljava/util/Map;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/delegate/UIThreadDelegateDecorator;)Lcom/helpshift/delegate/RootDelegate;
    .locals 0

    .line 12
    iget-object p0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    return-object p0
.end method


# virtual methods
.method public authenticationFailed(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/delegate/AuthenticationFailureReason;)V
    .locals 3

    .line 112
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    if-eqz v0, :cond_2

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->isActiveUser()Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 116
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, "_"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getAuthToken()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 120
    iget-object v1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->authenticationFailedCalledList:Ljava/util/Map;

    invoke-interface {v1, v0}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->authenticationFailedCalledList:Ljava/util/Map;

    invoke-interface {v1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/Boolean;

    invoke-virtual {v1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v1

    if-eqz v1, :cond_1

    return-void

    .line 125
    :cond_1
    iget-object v1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->authenticationFailedCalledList:Ljava/util/Map;

    const/4 v2, 0x1

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v1, v0, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 126
    new-instance v0, Lcom/helpshift/HelpshiftUser$Builder;

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Lcom/helpshift/HelpshiftUser$Builder;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    .line 127
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/HelpshiftUser$Builder;->setName(Ljava/lang/String;)Lcom/helpshift/HelpshiftUser$Builder;

    move-result-object v0

    .line 128
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getAuthToken()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lcom/helpshift/HelpshiftUser$Builder;->setAuthToken(Ljava/lang/String;)Lcom/helpshift/HelpshiftUser$Builder;

    move-result-object p1

    .line 129
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser$Builder;->build()Lcom/helpshift/HelpshiftUser;

    move-result-object p1

    .line 130
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/delegate/UIThreadDelegateDecorator$9;

    invoke-direct {v1, p0, p1, p2}, Lcom/helpshift/delegate/UIThreadDelegateDecorator$9;-><init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;Lcom/helpshift/HelpshiftUser;Lcom/helpshift/delegate/AuthenticationFailureReason;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void

    :cond_2
    :goto_0
    return-void
.end method

.method public conversationEnded()V
    .locals 2

    .line 56
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    if-eqz v0, :cond_0

    .line 57
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/delegate/UIThreadDelegateDecorator$4;

    invoke-direct {v1, p0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator$4;-><init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method public didReceiveNotification(I)V
    .locals 2

    .line 100
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    if-eqz v0, :cond_0

    .line 101
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/delegate/UIThreadDelegateDecorator$8;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/delegate/UIThreadDelegateDecorator$8;-><init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;I)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method public displayAttachmentFile(Ljava/io/File;)V
    .locals 2

    .line 89
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    if-eqz v0, :cond_0

    .line 90
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/delegate/UIThreadDelegateDecorator$7;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/delegate/UIThreadDelegateDecorator$7;-><init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;Ljava/io/File;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method public getDelegate()Lcom/helpshift/delegate/RootDelegate;
    .locals 1

    .line 143
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    return-object v0
.end method

.method public isDelegateRegistered()Z
    .locals 1

    .line 139
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public newConversationStarted(Ljava/lang/String;)V
    .locals 2

    .line 45
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    if-eqz v0, :cond_0

    .line 46
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/delegate/UIThreadDelegateDecorator$3;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/delegate/UIThreadDelegateDecorator$3;-><init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method public sessionBegan()V
    .locals 2

    .line 23
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    if-eqz v0, :cond_0

    .line 24
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/delegate/UIThreadDelegateDecorator$1;

    invoke-direct {v1, p0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator$1;-><init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method public sessionEnded()V
    .locals 2

    .line 34
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    if-eqz v0, :cond_0

    .line 35
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/delegate/UIThreadDelegateDecorator$2;

    invoke-direct {v1, p0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator$2;-><init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method public setDelegate(Lcom/helpshift/delegate/RootDelegate;)V
    .locals 0

    .line 147
    iput-object p1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    return-void
.end method

.method public userCompletedCustomerSatisfactionSurvey(ILjava/lang/String;)V
    .locals 2

    .line 78
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    if-eqz v0, :cond_0

    .line 79
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/delegate/UIThreadDelegateDecorator$6;

    invoke-direct {v1, p0, p1, p2}, Lcom/helpshift/delegate/UIThreadDelegateDecorator$6;-><init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;ILjava/lang/String;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method public userRepliedToConversation(Ljava/lang/String;)V
    .locals 2

    .line 67
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->delegate:Lcom/helpshift/delegate/RootDelegate;

    if-eqz v0, :cond_0

    .line 68
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/delegate/UIThreadDelegateDecorator$5;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/delegate/UIThreadDelegateDecorator$5;-><init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method
