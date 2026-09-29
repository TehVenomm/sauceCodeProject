.class Lcom/helpshift/support/fragments/SearchResultFragment$1;
.super Ljava/lang/Object;
.source "SearchResultFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/fragments/SearchResultFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/fragments/SearchResultFragment;


# direct methods
.method constructor <init>(Lcom/helpshift/support/fragments/SearchResultFragment;)V
    .locals 0

    .line 50
    iput-object p1, p0, Lcom/helpshift/support/fragments/SearchResultFragment$1;->this$0:Lcom/helpshift/support/fragments/SearchResultFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    .line 53
    invoke-virtual {p1}, Landroid/view/View;->getTag()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    .line 55
    iget-object v0, p0, Lcom/helpshift/support/fragments/SearchResultFragment$1;->this$0:Lcom/helpshift/support/fragments/SearchResultFragment;

    iget-object v0, v0, Lcom/helpshift/support/fragments/SearchResultFragment;->searchResultList:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/RecyclerView;->getAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/adapters/SearchResultAdapter;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/adapters/SearchResultAdapter;->getFaq(Ljava/lang/String;)Lcom/helpshift/support/Faq;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 57
    iget-object v0, v0, Lcom/helpshift/support/Faq;->searchTerms:Ljava/util/ArrayList;

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 59
    :goto_0
    iget-object v1, p0, Lcom/helpshift/support/fragments/SearchResultFragment$1;->this$0:Lcom/helpshift/support/fragments/SearchResultFragment;

    iget-object v1, v1, Lcom/helpshift/support/fragments/SearchResultFragment;->searchResultListener:Lcom/helpshift/support/contracts/SearchResultListener;

    invoke-interface {v1, p1, v0}, Lcom/helpshift/support/contracts/SearchResultListener;->onQuestionSelected(Ljava/lang/String;Ljava/util/ArrayList;)V

    return-void
.end method
