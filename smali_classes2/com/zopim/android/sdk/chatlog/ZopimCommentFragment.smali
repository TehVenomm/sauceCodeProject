.class public Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;
.super Landroidx/fragment/app/Fragment;

# interfaces
.implements Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;


# static fields
.field private static final EXTRA_COMMENT:Ljava/lang/String; = "COMMENT"

.field private static final LOG_TAG:Ljava/lang/String; = "ZopimCommentFragment"

.field private static final STATE_MENU_ITEM_ENABLED:Ljava/lang/String; = "MENU_ITEM_ENABLED"

.field private static final STATE_NO_CONNECTION:Ljava/lang/String; = "NO_CONNECTION"


# instance fields
.field private mChat:Lcom/zopim/android/sdk/api/Chat;

.field private mCommentEditor:Landroid/widget/EditText;

.field private mMenu:Landroid/view/Menu;

.field private mNoConnection:Z

.field private mStateMenuItemEnabled:Z


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mStateMenuItemEnabled:Z

    return-void
.end method

.method public static newInstance(Ljava/lang/String;)Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;
    .locals 3

    new-instance v0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;

    invoke-direct {v0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;-><init>()V

    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "COMMENT"

    invoke-virtual {v1, v2, p0}, Landroid/os/Bundle;->putSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->setArguments(Landroid/os/Bundle;)V

    return-object v0
.end method


# virtual methods
.method public onActivityCreated(Landroid/os/Bundle;)V
    .locals 1
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onActivityCreated(Landroid/os/Bundle;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    if-eqz p1, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    const-string v0, "COMMENT"

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    const-string v0, "COMMENT"

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mCommentEditor:Landroid/widget/EditText;

    invoke-virtual {v0, p1}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    :cond_0
    return-void
.end method

.method public onConnected()V
    .locals 2

    const/4 v0, 0x0

    iput-boolean v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mNoConnection:Z

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mMenu:Landroid/view/Menu;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mMenu:Landroid/view/Menu;

    sget v1, Lcom/zopim/android/sdk/R$id;->send_comment:I

    invoke-interface {v0, v1}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object v0

    if-eqz v0, :cond_0

    invoke-interface {v0}, Landroid/view/MenuItem;->isEnabled()Z

    move-result v1

    if-nez v1, :cond_0

    const/4 v1, 0x1

    invoke-interface {v0, v1}, Landroid/view/MenuItem;->setEnabled(Z)Landroid/view/MenuItem;

    :cond_0
    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 4
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onCreate(Landroid/os/Bundle;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->resume(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    if-nez p1, :cond_0

    new-instance p1, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;

    invoke-direct {p1}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;-><init>()V

    new-instance v0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;

    invoke-direct {v0}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;-><init>()V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->getChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    invoke-virtual {v1}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$id;->toast_fragment_container:I

    const-class v3, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v2, p1, v3}, Landroidx/fragment/app/FragmentTransaction;->add(ILandroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    const-class p1, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, v0, p1}, Landroidx/fragment/app/FragmentTransaction;->add(Landroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {v1}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    :cond_0
    return-void
.end method

.method public onCreateOptionsMenu(Landroid/view/Menu;Landroid/view/MenuInflater;)V
    .locals 1

    invoke-super {p0, p1, p2}, Landroidx/fragment/app/Fragment;->onCreateOptionsMenu(Landroid/view/Menu;Landroid/view/MenuInflater;)V

    sget v0, Lcom/zopim/android/sdk/R$menu;->chat_comment_menu:I

    invoke-virtual {p2, v0, p1}, Landroid/view/MenuInflater;->inflate(ILandroid/view/Menu;)V

    sget p2, Lcom/zopim/android/sdk/R$id;->send_comment:I

    invoke-interface {p1, p2}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object p2

    iget-boolean v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mStateMenuItemEnabled:Z

    invoke-interface {p2, v0}, Landroid/view/MenuItem;->setEnabled(Z)Landroid/view/MenuItem;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mMenu:Landroid/view/Menu;

    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1

    sget p3, Lcom/zopim/android/sdk/R$layout;->zopim_comment_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDisconnected()V
    .locals 2

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mNoConnection:Z

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mMenu:Landroid/view/Menu;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mMenu:Landroid/view/Menu;

    sget v1, Lcom/zopim/android/sdk/R$id;->send_comment:I

    invoke-interface {v0, v1}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object v0

    if-eqz v0, :cond_0

    invoke-interface {v0}, Landroid/view/MenuItem;->isEnabled()Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v1, 0x0

    invoke-interface {v0, v1}, Landroid/view/MenuItem;->setEnabled(Z)Landroid/view/MenuItem;

    :cond_0
    return-void
.end method

.method public onOptionsItemSelected(Landroid/view/MenuItem;)Z
    .locals 2

    invoke-interface {p1}, Landroid/view/MenuItem;->getItemId()I

    move-result v0

    sget v1, Lcom/zopim/android/sdk/R$id;->send_comment:I

    if-ne v1, v0, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {p1}, Lcom/zopim/android/sdk/api/Chat;->hasEnded()Z

    move-result p1

    if-nez p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mCommentEditor:Landroid/widget/EditText;

    invoke-virtual {p1}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->sendChatComment(Ljava/lang/String;)V

    :cond_0
    const/4 p1, 0x1

    return p1

    :cond_1
    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onOptionsItemSelected(Landroid/view/MenuItem;)Z

    move-result p1

    return p1
.end method

.method public onPause()V
    .locals 5

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onPause()V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->hasEnded()Z

    move-result v0

    const/4 v1, 0x1

    xor-int/2addr v0, v1

    sget v2, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v3, 0xb

    const/4 v4, 0x0

    if-lt v2, v3, :cond_1

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    goto :goto_0

    :cond_1
    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->isFinishing()Z

    move-result v0

    if-eqz v0, :cond_0

    :goto_0
    if-eqz v1, :cond_2

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    new-instance v1, Landroid/content/Intent;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v2

    const-class v3, Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-direct {v1, v2, v3}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentActivity;->startService(Landroid/content/Intent;)Landroid/content/ComponentName;

    :cond_2
    return-void
.end method

.method public onResume()V
    .locals 4

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onResume()V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    new-instance v1, Landroid/content/Intent;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v2

    const-class v3, Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-direct {v1, v2, v3}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentActivity;->stopService(Landroid/content/Intent;)Z

    return-void
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 2

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onSaveInstanceState(Landroid/os/Bundle;)V

    const-string v0, "NO_CONNECTION"

    iget-boolean v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mNoConnection:Z

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mMenu:Landroid/view/Menu;

    sget v1, Lcom/zopim/android/sdk/R$id;->send_comment:I

    invoke-interface {v0, v1}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object v0

    const-string v1, "MENU_ITEM_ENABLED"

    invoke-interface {v0}, Landroid/view/MenuItem;->isEnabled()Z

    move-result v0

    invoke-virtual {p1, v1, v0}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 0
    .param p2    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1, p2}, Landroidx/fragment/app/Fragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    const/4 p2, 0x1

    invoke-virtual {p0, p2}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->setHasOptionsMenu(Z)V

    sget p2, Lcom/zopim/android/sdk/R$id;->comment_editor:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/EditText;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mCommentEditor:Landroid/widget/EditText;

    return-void
.end method

.method public onViewStateRestored(Landroid/os/Bundle;)V
    .locals 2
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onViewStateRestored(Landroid/os/Bundle;)V

    if-eqz p1, :cond_0

    const-string v0, "NO_CONNECTION"

    const/4 v1, 0x0

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    iput-boolean v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mNoConnection:Z

    const-string v0, "MENU_ITEM_ENABLED"

    const/4 v1, 0x1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result p1

    iput-boolean p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->mStateMenuItemEnabled:Z

    :cond_0
    return-void
.end method
