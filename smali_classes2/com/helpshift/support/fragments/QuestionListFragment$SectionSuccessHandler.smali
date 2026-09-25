.class Lcom/helpshift/support/fragments/QuestionListFragment$SectionSuccessHandler;
.super Landroid/os/Handler;
.source "QuestionListFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/fragments/QuestionListFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "SectionSuccessHandler"
.end annotation


# instance fields
.field private final questionListFragmentWeakReference:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Lcom/helpshift/support/fragments/QuestionListFragment;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Lcom/helpshift/support/fragments/QuestionListFragment;)V
    .locals 1

    .line 232
    invoke-direct {p0}, Landroid/os/Handler;-><init>()V

    .line 233
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/helpshift/support/fragments/QuestionListFragment$SectionSuccessHandler;->questionListFragmentWeakReference:Ljava/lang/ref/WeakReference;

    return-void
.end method


# virtual methods
.method public handleMessage(Landroid/os/Message;)V
    .locals 3

    .line 238
    iget-object v0, p0, Lcom/helpshift/support/fragments/QuestionListFragment$SectionSuccessHandler;->questionListFragmentWeakReference:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/QuestionListFragment;

    if-eqz v0, :cond_4

    .line 239
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/QuestionListFragment;->isDetached()Z

    move-result v1

    if-eqz v1, :cond_0

    goto :goto_1

    .line 243
    :cond_0
    iget-object v1, p1, Landroid/os/Message;->obj:Ljava/lang/Object;

    if-eqz v1, :cond_1

    .line 244
    iget-object p1, p1, Landroid/os/Message;->obj:Ljava/lang/Object;

    check-cast p1, Lcom/helpshift/support/Section;

    .line 245
    invoke-virtual {v0, p1}, Lcom/helpshift/support/fragments/QuestionListFragment;->updateSectionData(Lcom/helpshift/support/Section;)V

    const-string v0, "Helpshift_QstnListFrag"

    .line 246
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "FAQ section loaded : SectionSuccessHandler : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lcom/helpshift/support/Section;->getTitle()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 249
    :cond_1
    invoke-static {v0}, Lcom/helpshift/support/fragments/QuestionListFragment;->access$000(Lcom/helpshift/support/fragments/QuestionListFragment;)Landroidx/recyclerview/widget/RecyclerView;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 250
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->getAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object v1

    if-eqz v1, :cond_2

    .line 251
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->getAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object p1

    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->getItemCount()I

    move-result p1

    if-nez p1, :cond_3

    :cond_2
    const/16 p1, 0x67

    .line 252
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/QuestionListFragment;->getView()Landroid/view/View;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/helpshift/support/util/SnackbarUtil;->showErrorSnackbar(ILandroid/view/View;)V

    :cond_3
    :goto_0
    return-void

    :cond_4
    :goto_1
    return-void
.end method
