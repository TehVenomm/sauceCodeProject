.class public Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;
.super Ljava/lang/Object;
.source "ConversationInfoVM.java"

# interfaces
.implements Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;


# instance fields
.field private final domain:Lcom/helpshift/common/domain/Domain;

.field private renderer:Lcom/helpshift/conversation/activeconversation/ConversationInfoRenderer;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/conversation/activeconversation/ConversationInfoRenderer;)V
    .locals 0

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 15
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 16
    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationInfoRenderer;

    .line 17
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object p1

    invoke-virtual {p1, p0}, Lcom/helpshift/account/AuthenticationFailureDM;->registerListener(Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;)V

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;)Lcom/helpshift/conversation/activeconversation/ConversationInfoRenderer;
    .locals 0

    .line 9
    iget-object p0, p0, Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationInfoRenderer;

    return-object p0
.end method


# virtual methods
.method public onAuthenticationFailure()V
    .locals 2

    .line 22
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/ConversationInfoVM$1;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/ConversationInfoVM$1;-><init>(Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public unregisterRenderer()V
    .locals 1

    const/4 v0, 0x0

    .line 33
    iput-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationInfoRenderer;

    .line 34
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/account/AuthenticationFailureDM;->unregisterListener(Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;)V

    return-void
.end method
