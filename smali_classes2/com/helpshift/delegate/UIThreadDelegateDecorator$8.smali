.class Lcom/helpshift/delegate/UIThreadDelegateDecorator$8;
.super Lcom/helpshift/common/domain/F;
.source "UIThreadDelegateDecorator.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/delegate/UIThreadDelegateDecorator;->didReceiveNotification(I)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

.field final synthetic val$newMessagesCount:I


# direct methods
.method constructor <init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;I)V
    .locals 0

    .line 101
    iput-object p1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$8;->this$0:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    iput p2, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$8;->val$newMessagesCount:I

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 104
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$8;->this$0:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    invoke-static {v0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->access$000(Lcom/helpshift/delegate/UIThreadDelegateDecorator;)Lcom/helpshift/delegate/RootDelegate;

    move-result-object v0

    iget v1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$8;->val$newMessagesCount:I

    invoke-interface {v0, v1}, Lcom/helpshift/delegate/RootDelegate;->didReceiveNotification(I)V

    return-void
.end method
