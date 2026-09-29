.class Lcom/helpshift/support/conversations/usersetup/UserSetupFragment$1;
.super Ljava/lang/Object;
.source "UserSetupFragment.java"

# interfaces
.implements Lcom/helpshift/widget/HSObserver;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->addViewStateObservers()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;)V
    .locals 0

    .line 72
    iput-object p1, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment$1;->this$0:Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onChanged(Ljava/lang/Object;)V
    .locals 0

    .line 75
    check-cast p1, Lcom/helpshift/widget/BaseViewState;

    .line 76
    invoke-virtual {p1}, Lcom/helpshift/widget/BaseViewState;->isVisible()Z

    move-result p1

    if-eqz p1, :cond_0

    .line 77
    iget-object p1, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment$1;->this$0:Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;

    invoke-virtual {p1}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->showProgressBar()V

    goto :goto_0

    .line 80
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment$1;->this$0:Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;

    invoke-virtual {p1}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->hideProgressBar()V

    :goto_0
    return-void
.end method
