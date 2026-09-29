.class Lcom/helpshift/support/fragments/SingleQuestionFragment$Failure;
.super Landroid/os/Handler;
.source "SingleQuestionFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/fragments/SingleQuestionFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "Failure"
.end annotation


# instance fields
.field private singleQuestionFragmentWeakReference:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Lcom/helpshift/support/fragments/SingleQuestionFragment;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Lcom/helpshift/support/fragments/SingleQuestionFragment;)V
    .locals 1

    .line 609
    invoke-direct {p0}, Landroid/os/Handler;-><init>()V

    .line 610
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment$Failure;->singleQuestionFragmentWeakReference:Ljava/lang/ref/WeakReference;

    return-void
.end method


# virtual methods
.method public handleMessage(Landroid/os/Message;)V
    .locals 1

    .line 615
    invoke-super {p0, p1}, Landroid/os/Handler;->handleMessage(Landroid/os/Message;)V

    .line 616
    iget-object p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment$Failure;->singleQuestionFragmentWeakReference:Ljava/lang/ref/WeakReference;

    invoke-virtual {p1}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/support/fragments/SingleQuestionFragment;

    if-eqz p1, :cond_2

    .line 617
    invoke-virtual {p1}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->isDetached()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 621
    :cond_0
    invoke-static {p1}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->access$100(Lcom/helpshift/support/fragments/SingleQuestionFragment;)Lcom/helpshift/support/Faq;

    move-result-object v0

    if-nez v0, :cond_1

    const/16 v0, 0x66

    .line 623
    invoke-virtual {p1}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getView()Landroid/view/View;

    move-result-object p1

    .line 622
    invoke-static {v0, p1}, Lcom/helpshift/support/util/SnackbarUtil;->showErrorSnackbar(ILandroid/view/View;)V

    :cond_1
    return-void

    :cond_2
    :goto_0
    return-void
.end method
