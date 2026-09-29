.class Lcom/helpshift/support/fragments/SearchFragment$3;
.super Ljava/lang/Object;
.source "SearchFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/fragments/SearchFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/fragments/SearchFragment;


# direct methods
.method constructor <init>(Lcom/helpshift/support/fragments/SearchFragment;)V
    .locals 0

    .line 109
    iput-object p1, p0, Lcom/helpshift/support/fragments/SearchFragment$3;->this$0:Lcom/helpshift/support/fragments/SearchFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 112
    iget-object p1, p0, Lcom/helpshift/support/fragments/SearchFragment$3;->this$0:Lcom/helpshift/support/fragments/SearchFragment;

    invoke-virtual {p1}, Lcom/helpshift/support/fragments/SearchFragment;->getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;

    move-result-object p1

    iget-object v0, p0, Lcom/helpshift/support/fragments/SearchFragment$3;->this$0:Lcom/helpshift/support/fragments/SearchFragment;

    iget-object v0, v0, Lcom/helpshift/support/fragments/SearchFragment;->currentQuery:Ljava/lang/String;

    invoke-interface {p1, v0}, Lcom/helpshift/support/contracts/FaqFragmentListener;->onContactUsClicked(Ljava/lang/String;)V

    return-void
.end method
