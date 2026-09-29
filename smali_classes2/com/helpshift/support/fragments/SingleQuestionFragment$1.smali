.class Lcom/helpshift/support/fragments/SingleQuestionFragment$1;
.super Lcom/helpshift/common/domain/F;
.source "SingleQuestionFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/fragments/SingleQuestionFragment;->highlightAndReloadQuestion()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/fragments/SingleQuestionFragment;

.field final synthetic val$searchTerms:Ljava/util/ArrayList;


# direct methods
.method constructor <init>(Lcom/helpshift/support/fragments/SingleQuestionFragment;Ljava/util/ArrayList;)V
    .locals 0

    .line 451
    iput-object p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment$1;->this$0:Lcom/helpshift/support/fragments/SingleQuestionFragment;

    iput-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment$1;->val$searchTerms:Ljava/util/ArrayList;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 454
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment$1;->this$0:Lcom/helpshift/support/fragments/SingleQuestionFragment;

    iget-object v1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment$1;->this$0:Lcom/helpshift/support/fragments/SingleQuestionFragment;

    .line 455
    invoke-virtual {v1}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getContext()Landroid/content/Context;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment$1;->this$0:Lcom/helpshift/support/fragments/SingleQuestionFragment;

    invoke-static {v2}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->access$100(Lcom/helpshift/support/fragments/SingleQuestionFragment;)Lcom/helpshift/support/Faq;

    move-result-object v2

    iget-object v3, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment$1;->val$searchTerms:Ljava/util/ArrayList;

    invoke-static {v1, v2, v3}, Lcom/helpshift/support/util/Styles;->getQuestionWithHighlightedSearchTerms(Landroid/content/Context;Lcom/helpshift/support/Faq;Ljava/util/ArrayList;)Lcom/helpshift/support/Faq;

    move-result-object v1

    .line 454
    invoke-static {v0, v1}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->access$002(Lcom/helpshift/support/fragments/SingleQuestionFragment;Lcom/helpshift/support/Faq;)Lcom/helpshift/support/Faq;

    .line 456
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getDomain()Lcom/helpshift/common/domain/Domain;

    move-result-object v0

    new-instance v1, Lcom/helpshift/support/fragments/SingleQuestionFragment$1$1;

    invoke-direct {v1, p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment$1$1;-><init>(Lcom/helpshift/support/fragments/SingleQuestionFragment$1;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method
