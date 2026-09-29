.class public Lcom/helpshift/account/AuthenticationFailureDM;
.super Ljava/lang/Object;
.source "AuthenticationFailureDM.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;
    }
.end annotation


# instance fields
.field domain:Lcom/helpshift/common/domain/Domain;

.field observerList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/Domain;)V
    .locals 1

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 19
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/account/AuthenticationFailureDM;->observerList:Ljava/util/List;

    .line 23
    iput-object p1, p0, Lcom/helpshift/account/AuthenticationFailureDM;->domain:Lcom/helpshift/common/domain/Domain;

    return-void
.end method

.method private notifyObservers()V
    .locals 2

    .line 64
    iget-object v0, p0, Lcom/helpshift/account/AuthenticationFailureDM;->observerList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;

    if-eqz v1, :cond_0

    .line 66
    invoke-interface {v1}, Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;->onAuthenticationFailure()V

    goto :goto_0

    :cond_1
    return-void
.end method


# virtual methods
.method public notifyAuthenticationFailure(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/common/exception/ExceptionType;)V
    .locals 2

    .line 39
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->isActiveUser()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x0

    .line 44
    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-ne p2, v1, :cond_1

    .line 45
    sget-object v0, Lcom/helpshift/delegate/AuthenticationFailureReason;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/delegate/AuthenticationFailureReason;

    goto :goto_0

    .line 47
    :cond_1
    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-ne p2, v1, :cond_2

    .line 48
    sget-object v0, Lcom/helpshift/delegate/AuthenticationFailureReason;->INVALID_AUTH_TOKEN:Lcom/helpshift/delegate/AuthenticationFailureReason;

    :cond_2
    :goto_0
    if-nez v0, :cond_3

    return-void

    .line 57
    :cond_3
    invoke-direct {p0}, Lcom/helpshift/account/AuthenticationFailureDM;->notifyObservers()V

    .line 60
    iget-object p2, p0, Lcom/helpshift/account/AuthenticationFailureDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object p2

    invoke-virtual {p2, p1, v0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->authenticationFailed(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/delegate/AuthenticationFailureReason;)V

    return-void
.end method

.method public registerListener(Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;)V
    .locals 1

    if-eqz p1, :cond_0

    .line 28
    iget-object v0, p0, Lcom/helpshift/account/AuthenticationFailureDM;->observerList:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_0
    return-void
.end method

.method public unregisterListener(Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;)V
    .locals 1

    if-eqz p1, :cond_0

    .line 34
    iget-object v0, p0, Lcom/helpshift/account/AuthenticationFailureDM;->observerList:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    :cond_0
    return-void
.end method
