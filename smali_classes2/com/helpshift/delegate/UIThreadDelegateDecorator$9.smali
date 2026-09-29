.class Lcom/helpshift/delegate/UIThreadDelegateDecorator$9;
.super Lcom/helpshift/common/domain/F;
.source "UIThreadDelegateDecorator.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/delegate/UIThreadDelegateDecorator;->authenticationFailed(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/delegate/AuthenticationFailureReason;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

.field final synthetic val$reason:Lcom/helpshift/delegate/AuthenticationFailureReason;

.field final synthetic val$user:Lcom/helpshift/HelpshiftUser;


# direct methods
.method constructor <init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;Lcom/helpshift/HelpshiftUser;Lcom/helpshift/delegate/AuthenticationFailureReason;)V
    .locals 0

    .line 130
    iput-object p1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$9;->this$0:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    iput-object p2, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$9;->val$user:Lcom/helpshift/HelpshiftUser;

    iput-object p3, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$9;->val$reason:Lcom/helpshift/delegate/AuthenticationFailureReason;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 133
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$9;->this$0:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    invoke-static {v0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->access$000(Lcom/helpshift/delegate/UIThreadDelegateDecorator;)Lcom/helpshift/delegate/RootDelegate;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$9;->val$user:Lcom/helpshift/HelpshiftUser;

    iget-object v2, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$9;->val$reason:Lcom/helpshift/delegate/AuthenticationFailureReason;

    invoke-interface {v0, v1, v2}, Lcom/helpshift/delegate/RootDelegate;->authenticationFailed(Lcom/helpshift/HelpshiftUser;Lcom/helpshift/delegate/AuthenticationFailureReason;)V

    return-void
.end method
