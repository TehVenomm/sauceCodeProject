.class Lcom/helpshift/delegate/UIThreadDelegateDecorator$6;
.super Lcom/helpshift/common/domain/F;
.source "UIThreadDelegateDecorator.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/delegate/UIThreadDelegateDecorator;->userCompletedCustomerSatisfactionSurvey(ILjava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

.field final synthetic val$feedback:Ljava/lang/String;

.field final synthetic val$rating:I


# direct methods
.method constructor <init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;ILjava/lang/String;)V
    .locals 0

    .line 79
    iput-object p1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$6;->this$0:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    iput p2, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$6;->val$rating:I

    iput-object p3, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$6;->val$feedback:Ljava/lang/String;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 82
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$6;->this$0:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    invoke-static {v0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->access$000(Lcom/helpshift/delegate/UIThreadDelegateDecorator;)Lcom/helpshift/delegate/RootDelegate;

    move-result-object v0

    iget v1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$6;->val$rating:I

    iget-object v2, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$6;->val$feedback:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Lcom/helpshift/delegate/RootDelegate;->userCompletedCustomerSatisfactionSurvey(ILjava/lang/String;)V

    return-void
.end method
