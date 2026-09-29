.class public Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;
.super Landroidx/fragment/app/Fragment;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;
    }
.end annotation


# static fields
.field private static final LOG_TAG:Ljava/lang/String; = "ConnectionToastFragment"

.field private static final STATE_SHOW_TOAST:Ljava/lang/String; = "SHOW_TOAST"


# instance fields
.field mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

.field private mHandler:Landroid/os/Handler;

.field private mMessageView:Landroid/widget/TextView;

.field private mToastListener:Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;

.field private mToastView:Landroid/view/View;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 2

    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mHandler:Landroid/os/Handler;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/x;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/x;-><init>(Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    return-void
.end method

.method static synthetic access$000(Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;Lcom/zopim/android/sdk/model/Connection;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->updateToastView(Lcom/zopim/android/sdk/model/Connection;)V

    return-void
.end method

.method static synthetic access$100(Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;)Landroid/os/Handler;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mHandler:Landroid/os/Handler;

    return-object p0
.end method

.method private hideToast()V
    .locals 2
    .annotation build Landroid/annotation/TargetApi;
        value = 0xb
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastView:Landroid/view/View;

    if-nez v0, :cond_0

    return-void

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getVisibility()I

    move-result v0

    if-nez v0, :cond_2

    sget-object v0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Hide no network toast"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->getView()Landroid/view/View;

    move-result-object v0

    invoke-virtual {v0}, Landroid/view/View;->bringToFront()V

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0xb

    if-lt v0, v1, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastView:Landroid/view/View;

    sget-object v1, Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;->BOTTOM:Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/anim/AnimatorPack;->slideOut(Landroid/view/View;Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;)Landroid/animation/Animator;

    move-result-object v0

    invoke-virtual {v0}, Landroid/animation/Animator;->start()V

    goto :goto_0

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastView:Landroid/view/View;

    const/4 v1, 0x4

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    :goto_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastListener:Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastListener:Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;

    invoke-interface {v0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;->onHideToast()V

    :cond_2
    return-void
.end method

.method private showToast()V
    .locals 2
    .annotation build Landroid/annotation/TargetApi;
        value = 0xb
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastView:Landroid/view/View;

    if-nez v0, :cond_0

    return-void

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getVisibility()I

    move-result v0

    if-eqz v0, :cond_2

    sget-object v0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Show no network toast"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->getView()Landroid/view/View;

    move-result-object v0

    invoke-virtual {v0}, Landroid/view/View;->bringToFront()V

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0xb

    if-lt v0, v1, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastView:Landroid/view/View;

    sget-object v1, Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;->BOTTOM:Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/anim/AnimatorPack;->slideIn(Landroid/view/View;Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;)Landroid/animation/Animator;

    move-result-object v0

    invoke-virtual {v0}, Landroid/animation/Animator;->start()V

    goto :goto_0

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastView:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    :goto_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastListener:Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastListener:Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;

    invoke-interface {v0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;->onShowToast()V

    :cond_2
    return-void
.end method

.method private updateToastView(Lcom/zopim/android/sdk/model/Connection;)V
    .locals 2

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Connection must not be null. Can not update visibility."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/chatlog/z;->a:[I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/Connection;->getStatus()Lcom/zopim/android/sdk/model/Connection$Status;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/Connection$Status;->ordinal()I

    move-result p1

    aget p1, v0, p1

    packed-switch p1, :pswitch_data_0

    goto :goto_1

    :pswitch_0
    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->hideToast()V

    goto :goto_1

    :pswitch_1
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mMessageView:Landroid/widget/TextView;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$string;->reconnecting_toast_message:I

    goto :goto_0

    :pswitch_2
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mMessageView:Landroid/widget/TextView;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$string;->no_connectivity_toast_message:I

    :goto_0
    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->showToast()V

    :goto_1
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_2
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public onAttach(Landroid/app/Activity;)V
    .locals 1

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onAttach(Landroid/app/Activity;)V

    instance-of v0, p1, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;

    if-eqz v0, :cond_0

    check-cast p1, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastListener:Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;

    :cond_0
    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object p1

    instance-of p1, p1, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;

    if-eqz p1, :cond_1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastListener:Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;

    :cond_1
    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 2

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onCreate(Landroid/os/Bundle;)V

    if-nez p1, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->getChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object p1

    invoke-virtual {p1}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object p1

    new-instance v0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;

    invoke-direct {v0}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;-><init>()V

    const-class v1, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v0, v1}, Landroidx/fragment/app/FragmentTransaction;->add(Landroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {p1}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    :cond_0
    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .param p3    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    sget p3, Lcom/zopim/android/sdk/R$layout;->zopim_toast_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroy()V
    .locals 2

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onDestroy()V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mHandler:Landroid/os/Handler;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacksAndMessages(Ljava/lang/Object;)V

    return-void
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 2

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onSaveInstanceState(Landroid/os/Bundle;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getVisibility()I

    move-result v0

    if-nez v0, :cond_0

    const-string v0, "SHOW_TOAST"

    const/4 v1, 0x1

    :goto_0
    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    goto :goto_1

    :cond_0
    const-string v0, "SHOW_TOAST"

    const/4 v1, 0x0

    goto :goto_0

    :goto_1
    return-void
.end method

.method public onStart()V
    .locals 2

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStart()V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/data/DataSource;->getConnection()Lcom/zopim/android/sdk/model/Connection;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->updateToastView(Lcom/zopim/android/sdk/model/Connection;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->addConnectionObserver(Ljava/util/Observer;)V

    return-void
.end method

.method public onStop()V
    .locals 2

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStop()V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteConnectionObserver(Ljava/util/Observer;)V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 0
    .param p2    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1, p2}, Landroidx/fragment/app/Fragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    sget p2, Lcom/zopim/android/sdk/R$id;->network_no_connectivity:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mToastView:Landroid/view/View;

    sget p2, Lcom/zopim/android/sdk/R$id;->message_text:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->mMessageView:Landroid/widget/TextView;

    return-void
.end method

.method public onViewStateRestored(Landroid/os/Bundle;)V
    .locals 2
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onViewStateRestored(Landroid/os/Bundle;)V

    if-eqz p1, :cond_1

    const-string v0, "SHOW_TOAST"

    const/4 v1, 0x0

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result p1

    if-eqz p1, :cond_0

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->showToast()V

    goto :goto_0

    :cond_0
    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->hideToast()V

    :cond_1
    :goto_0
    return-void
.end method
