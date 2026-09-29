.class Lcom/helpshift/support/compositions/FaqFragment$Failure;
.super Landroid/os/Handler;
.source "FaqFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/compositions/FaqFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "Failure"
.end annotation


# instance fields
.field private final faqFragmentWeakReference:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Lcom/helpshift/support/compositions/FaqFragment;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Lcom/helpshift/support/compositions/FaqFragment;)V
    .locals 1

    .line 286
    invoke-direct {p0}, Landroid/os/Handler;-><init>()V

    .line 287
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/helpshift/support/compositions/FaqFragment$Failure;->faqFragmentWeakReference:Ljava/lang/ref/WeakReference;

    return-void
.end method


# virtual methods
.method public handleMessage(Landroid/os/Message;)V
    .locals 4

    .line 292
    iget-object v0, p0, Lcom/helpshift/support/compositions/FaqFragment$Failure;->faqFragmentWeakReference:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/compositions/FaqFragment;

    if-eqz v0, :cond_4

    .line 293
    invoke-virtual {v0}, Lcom/helpshift/support/compositions/FaqFragment;->getHost()Ljava/lang/Object;

    move-result-object v1

    if-eqz v1, :cond_4

    invoke-virtual {v0}, Lcom/helpshift/support/compositions/FaqFragment;->isDetached()Z

    move-result v1

    if-eqz v1, :cond_0

    goto :goto_1

    :cond_0
    const/4 v1, 0x0

    .line 298
    iget v2, p1, Landroid/os/Message;->what:I

    .line 299
    iget-object v3, p1, Landroid/os/Message;->obj:Ljava/lang/Object;

    instance-of v3, v3, Lcom/helpshift/common/exception/ExceptionType;

    if-eqz v3, :cond_1

    .line 300
    iget-object p1, p1, Landroid/os/Message;->obj:Ljava/lang/Object;

    move-object v1, p1

    check-cast v1, Lcom/helpshift/common/exception/ExceptionType;

    .line 303
    :cond_1
    iget p1, v0, Lcom/helpshift/support/compositions/FaqFragment;->sectionsSize:I

    if-nez p1, :cond_3

    .line 304
    sget p1, Lcom/helpshift/support/constants/GetSectionsCallBackStatus;->API_FAILURE_CONTENT_UNCHANGED:I

    if-ne v2, p1, :cond_2

    const/4 p1, 0x2

    .line 305
    invoke-virtual {v0, p1}, Lcom/helpshift/support/compositions/FaqFragment;->updateFaqLoadingUI(I)V

    goto :goto_0

    :cond_2
    const/4 p1, 0x3

    .line 308
    invoke-virtual {v0, p1}, Lcom/helpshift/support/compositions/FaqFragment;->updateFaqLoadingUI(I)V

    .line 311
    invoke-virtual {v0}, Lcom/helpshift/support/compositions/FaqFragment;->getView()Landroid/view/View;

    move-result-object p1

    invoke-static {v1, p1}, Lcom/helpshift/support/util/SnackbarUtil;->showSnackbar(Lcom/helpshift/common/exception/ExceptionType;Landroid/view/View;)V

    goto :goto_0

    :cond_3
    const/4 p1, 0x1

    .line 316
    invoke-virtual {v0, p1}, Lcom/helpshift/support/compositions/FaqFragment;->updateFaqLoadingUI(I)V

    :goto_0
    return-void

    :cond_4
    :goto_1
    return-void
.end method
