.class public abstract Lcom/helpshift/support/conversations/BaseConversationFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "BaseConversationFragment.java"

# interfaces
.implements Landroid/view/MenuItem$OnMenuItemClickListener;
.implements Lcom/helpshift/support/fragments/IMenuItemEventListener;


# static fields
.field public static final REQUEST_READ_STORAGE_PERMISSION_REQUEST_ID:I = 0x2

.field public static final REQUEST_WRITE_STORAGE_PERMISSION_REQUEST_ID:I = 0x3

.field private static final TAG:Ljava/lang/String; = "Helpshift_BaseConvFrag"


# instance fields
.field private permissionDeniedSnackbar:Lcom/google/android/material/snackbar/Snackbar;

.field private showRationaleSnackbar:Lcom/google/android/material/snackbar/Snackbar;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 34
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    return-void
.end method


# virtual methods
.method protected abstract getScreenshotMode()I
.end method

.method protected getSupportController()Lcom/helpshift/support/controllers/SupportController;
    .locals 1

    .line 89
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SupportFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v0

    return-object v0
.end method

.method protected getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1

    .line 85
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/SupportFragment;

    return-object v0
.end method

.method protected abstract getToolbarTitle()Ljava/lang/String;
.end method

.method protected abstract getViewName()Lcom/helpshift/support/util/AppSessionConstants$Screen;
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 0

    .line 152
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onCreate(Landroid/os/Bundle;)V

    .line 153
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/support/fragments/SupportFragment;->resetNewMessageCount()V

    return-void
.end method

.method public onDestroyView()V
    .locals 1

    .line 170
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getView()Landroid/view/View;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/util/SnackbarUtil;->hideSnackbar(Landroid/view/View;)V

    .line 171
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/support/fragments/SupportFragment;->unRegisterToolbarMenuEventsListener(Lcom/helpshift/support/fragments/IMenuItemEventListener;)V

    .line 172
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onDestroyView()V

    return-void
.end method

.method public onMenuItemClick(Landroid/view/MenuItem;)Z
    .locals 0

    const/4 p1, 0x0

    return p1
.end method

.method public onPause()V
    .locals 1

    .line 53
    iget-object v0, p0, Lcom/helpshift/support/conversations/BaseConversationFragment;->showRationaleSnackbar:Lcom/google/android/material/snackbar/Snackbar;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/conversations/BaseConversationFragment;->showRationaleSnackbar:Lcom/google/android/material/snackbar/Snackbar;

    invoke-virtual {v0}, Lcom/google/android/material/snackbar/Snackbar;->isShown()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 54
    iget-object v0, p0, Lcom/helpshift/support/conversations/BaseConversationFragment;->showRationaleSnackbar:Lcom/google/android/material/snackbar/Snackbar;

    invoke-virtual {v0}, Lcom/google/android/material/snackbar/Snackbar;->dismiss()V

    .line 56
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/conversations/BaseConversationFragment;->permissionDeniedSnackbar:Lcom/google/android/material/snackbar/Snackbar;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/support/conversations/BaseConversationFragment;->permissionDeniedSnackbar:Lcom/google/android/material/snackbar/Snackbar;

    invoke-virtual {v0}, Lcom/google/android/material/snackbar/Snackbar;->isShown()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 57
    iget-object v0, p0, Lcom/helpshift/support/conversations/BaseConversationFragment;->permissionDeniedSnackbar:Lcom/google/android/material/snackbar/Snackbar;

    invoke-virtual {v0}, Lcom/google/android/material/snackbar/Snackbar;->dismiss()V

    .line 59
    :cond_1
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onPause()V

    return-void
.end method

.method protected abstract onPermissionGranted(I)V
.end method

.method public onRequestPermissionsResult(I[Ljava/lang/String;[I)V
    .locals 2
    .param p2    # [Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p3    # [I
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 129
    invoke-super {p0, p1, p2, p3}, Lcom/helpshift/support/fragments/MainFragment;->onRequestPermissionsResult(I[Ljava/lang/String;[I)V

    .line 130
    array-length p2, p3

    const/4 v0, 0x0

    const/4 v1, 0x1

    if-ne p2, v1, :cond_0

    aget p2, p3, v0

    if-nez p2, :cond_0

    const/4 v0, 0x1

    :cond_0
    const-string p2, "Helpshift_BaseConvFrag"

    .line 132
    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "onRequestPermissionsResult : request: "

    invoke-virtual {p3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, ", result: "

    invoke-virtual {p3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3, v0}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p3

    invoke-static {p2, p3}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    if-eqz v0, :cond_1

    .line 134
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/BaseConversationFragment;->onPermissionGranted(I)V

    goto :goto_0

    .line 137
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getView()Landroid/view/View;

    move-result-object p1

    sget p2, Lcom/helpshift/R$string;->hs__permission_denied_message:I

    const/4 p3, -0x1

    invoke-static {p1, p2, p3}, Lcom/helpshift/views/HSSnackbar;->make(Landroid/view/View;II)Lcom/google/android/material/snackbar/Snackbar;

    move-result-object p1

    sget p2, Lcom/helpshift/R$string;->hs__permission_denied_snackbar_action:I

    new-instance p3, Lcom/helpshift/support/conversations/BaseConversationFragment$1;

    invoke-direct {p3, p0}, Lcom/helpshift/support/conversations/BaseConversationFragment$1;-><init>(Lcom/helpshift/support/conversations/BaseConversationFragment;)V

    .line 139
    invoke-virtual {p1, p2, p3}, Lcom/google/android/material/snackbar/Snackbar;->setAction(ILandroid/view/View$OnClickListener;)Lcom/google/android/material/snackbar/Snackbar;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/BaseConversationFragment;->permissionDeniedSnackbar:Lcom/google/android/material/snackbar/Snackbar;

    .line 146
    iget-object p1, p0, Lcom/helpshift/support/conversations/BaseConversationFragment;->permissionDeniedSnackbar:Lcom/google/android/material/snackbar/Snackbar;

    invoke-virtual {p1}, Lcom/google/android/material/snackbar/Snackbar;->show()V

    :goto_0
    return-void
.end method

.method public onResume()V
    .locals 1

    .line 164
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onResume()V

    .line 165
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getToolbarTitle()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->setToolbarTitle(Ljava/lang/String;)V

    return-void
.end method

.method public onStart()V
    .locals 3

    .line 46
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onStart()V

    .line 47
    invoke-static {}, Lcom/helpshift/support/storage/IMAppSessionStorage;->getInstance()Lcom/helpshift/support/storage/IMAppSessionStorage;

    move-result-object v0

    const-string v1, "current_open_screen"

    .line 48
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getViewName()Lcom/helpshift/support/util/AppSessionConstants$Screen;

    move-result-object v2

    .line 47
    invoke-virtual {v0, v1, v2}, Lcom/helpshift/support/storage/IMAppSessionStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public onStop()V
    .locals 2

    .line 64
    invoke-static {}, Lcom/helpshift/support/storage/IMAppSessionStorage;->getInstance()Lcom/helpshift/support/storage/IMAppSessionStorage;

    move-result-object v0

    const-string v1, "current_open_screen"

    .line 65
    invoke-virtual {v0, v1}, Lcom/helpshift/support/storage/IMAppSessionStorage;->get(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;

    if-eqz v0, :cond_0

    .line 66
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getViewName()Lcom/helpshift/support/util/AppSessionConstants$Screen;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/support/util/AppSessionConstants$Screen;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 67
    invoke-static {}, Lcom/helpshift/support/storage/IMAppSessionStorage;->getInstance()Lcom/helpshift/support/storage/IMAppSessionStorage;

    move-result-object v0

    const-string v1, "current_open_screen"

    invoke-virtual {v0, v1}, Lcom/helpshift/support/storage/IMAppSessionStorage;->removeKey(Ljava/lang/String;)V

    .line 69
    :cond_0
    sget v0, Lcom/helpshift/R$string;->hs__help_header:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->setToolbarTitle(Ljava/lang/String;)V

    .line 70
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onStop()V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 0
    .param p2    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 158
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    .line 159
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p1

    invoke-virtual {p1, p0}, Lcom/helpshift/support/fragments/SupportFragment;->registerToolbarMenuEventsListener(Lcom/helpshift/support/fragments/IMenuItemEventListener;)V

    return-void
.end method

.method public requestPermission(ZI)V
    .locals 3

    packed-switch p2, :pswitch_data_0

    const/4 v0, 0x0

    goto :goto_0

    :pswitch_0
    const-string v0, "android.permission.WRITE_EXTERNAL_STORAGE"

    goto :goto_0

    :pswitch_1
    const-string v0, "android.permission.READ_EXTERNAL_STORAGE"

    :goto_0
    if-eqz p1, :cond_0

    if-eqz v0, :cond_0

    .line 111
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getContext()Landroid/content/Context;

    move-result-object p1

    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getView()Landroid/view/View;

    move-result-object v1

    invoke-static {p1, v1}, Lcom/helpshift/support/util/KeyboardUtil;->hideKeyboard(Landroid/content/Context;Landroid/view/View;)V

    .line 113
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object p1

    const/4 v1, 0x1

    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    aput-object v0, v1, v2

    .line 116
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getView()Landroid/view/View;

    move-result-object v0

    .line 113
    invoke-static {p1, v1, p2, v0}, Lcom/helpshift/support/util/PermissionUtil;->requestPermissions(Landroidx/fragment/app/Fragment;[Ljava/lang/String;ILandroid/view/View;)Lcom/google/android/material/snackbar/Snackbar;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/BaseConversationFragment;->showRationaleSnackbar:Lcom/google/android/material/snackbar/Snackbar;

    goto :goto_1

    .line 119
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->isDetached()Z

    move-result p1

    if-nez p1, :cond_1

    .line 120
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getView()Landroid/view/View;

    move-result-object p1

    sget p2, Lcom/helpshift/R$string;->hs__permission_not_granted:I

    const/4 v0, -0x1

    invoke-static {p1, p2, v0}, Lcom/helpshift/support/util/SnackbarUtil;->showSnackbar(Landroid/view/View;II)V

    :cond_1
    :goto_1
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method protected requestWriteExternalStoragePermission(Z)V
    .locals 1

    const/4 v0, 0x3

    .line 97
    invoke-virtual {p0, p1, v0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->requestPermission(ZI)V

    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method
