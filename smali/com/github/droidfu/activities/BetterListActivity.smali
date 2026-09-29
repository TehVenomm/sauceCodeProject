.class public Lcom/github/droidfu/activities/BetterListActivity;
.super Landroid/app/ListActivity;
.source "BetterListActivity.java"

# interfaces
.implements Lcom/github/droidfu/activities/BetterActivity;


# static fields
.field private static final IS_BUSY_EXTRA:Ljava/lang/String; = "is_busy"


# instance fields
.field private currentIntent:Landroid/content/Intent;

.field private progressDialogMsgId:I

.field private progressDialogTitleId:I

.field private wasCreated:Z

.field private wasInterrupted:Z


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 33
    invoke-direct {p0}, Landroid/app/ListActivity;-><init>()V

    return-void
.end method


# virtual methods
.method public getCurrentIntent()Landroid/content/Intent;
    .locals 1

    .line 131
    iget-object v0, p0, Lcom/github/droidfu/activities/BetterListActivity;->currentIntent:Landroid/content/Intent;

    return-object v0
.end method

.method public getWindowFeatures()I
    .locals 1

    .line 111
    invoke-static {p0}, Lcom/github/droidfu/activities/BetterActivityHelper;->getWindowFeatures(Landroid/app/Activity;)I

    move-result v0

    return v0
.end method

.method public isApplicationBroughtToBackground()Z
    .locals 1

    .line 127
    invoke-static {p0}, Lcom/github/droidfu/activities/BetterActivityHelper;->isApplicationBroughtToBackground(Landroid/content/Context;)Z

    move-result v0

    return v0
.end method

.method public isLandscapeMode()Z
    .locals 2

    .line 135
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterListActivity;->getWindowManager()Landroid/view/WindowManager;

    move-result-object v0

    invoke-interface {v0}, Landroid/view/WindowManager;->getDefaultDisplay()Landroid/view/Display;

    move-result-object v0

    invoke-virtual {v0}, Landroid/view/Display;->getOrientation()I

    move-result v0

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return v1

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public isLaunching()Z
    .locals 1

    .line 123
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterListActivity;->wasInterrupted:Z

    if-nez v0, :cond_0

    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterListActivity;->wasCreated:Z

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public isPortraitMode()Z
    .locals 1

    .line 139
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterListActivity;->isLandscapeMode()Z

    move-result v0

    xor-int/lit8 v0, v0, 0x1

    return v0
.end method

.method public isRestoring()Z
    .locals 1

    .line 115
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterListActivity;->wasInterrupted:Z

    return v0
.end method

.method public isResuming()Z
    .locals 1

    .line 119
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterListActivity;->wasCreated:Z

    xor-int/lit8 v0, v0, 0x1

    return v0
.end method

.method public newAlertDialog(II)Landroid/app/AlertDialog;
    .locals 1

    .line 154
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterListActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 155
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterListActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    const v0, 0x1080027

    .line 154
    invoke-static {p0, p1, p2, v0}, Lcom/github/droidfu/activities/BetterActivityHelper;->newMessageDialog(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;I)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newErrorHandlerDialog(ILjava/lang/Exception;)Landroid/app/AlertDialog;
    .locals 0

    .line 159
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterListActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    invoke-static {p0, p1, p2}, Lcom/github/droidfu/activities/BetterActivityHelper;->newErrorHandlerDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/Exception;)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newErrorHandlerDialog(Ljava/lang/Exception;)Landroid/app/AlertDialog;
    .locals 4

    .line 163
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterListActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "droidfu_error_dialog_title"

    const-string v2, "string"

    .line 164
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterListActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 163
    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    invoke-virtual {p0, v0, p1}, Lcom/github/droidfu/activities/BetterListActivity;->newErrorHandlerDialog(ILjava/lang/Exception;)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newInfoDialog(II)Landroid/app/AlertDialog;
    .locals 1

    .line 149
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterListActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 150
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterListActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    const v0, 0x108009b

    .line 149
    invoke-static {p0, p1, p2, v0}, Lcom/github/droidfu/activities/BetterActivityHelper;->newMessageDialog(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;I)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newListDialog(Ljava/lang/String;Ljava/util/List;Lcom/github/droidfu/dialogs/DialogClickListener;Z)Landroid/app/Dialog;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "TT;>;",
            "Lcom/github/droidfu/dialogs/DialogClickListener<",
            "TT;>;Z)",
            "Landroid/app/Dialog;"
        }
    .end annotation

    .line 169
    invoke-static {p0, p1, p2, p3, p4}, Lcom/github/droidfu/activities/BetterActivityHelper;->newListDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Lcom/github/droidfu/dialogs/DialogClickListener;Z)Landroid/app/Dialog;

    move-result-object p1

    return-object p1
.end method

.method public newYesNoDialog(IILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog;
    .locals 1

    .line 144
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterListActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 145
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterListActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    const v0, 0x108009b

    .line 144
    invoke-static {p0, p1, p2, v0, p3}, Lcom/github/droidfu/activities/BetterActivityHelper;->newYesNoDialog(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method protected onCreate(Landroid/os/Bundle;)V
    .locals 1

    .line 47
    invoke-super {p0, p1}, Landroid/app/ListActivity;->onCreate(Landroid/os/Bundle;)V

    const/4 p1, 0x1

    .line 49
    iput-boolean p1, p0, Lcom/github/droidfu/activities/BetterListActivity;->wasCreated:Z

    .line 50
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterListActivity;->getIntent()Landroid/content/Intent;

    move-result-object p1

    iput-object p1, p0, Lcom/github/droidfu/activities/BetterListActivity;->currentIntent:Landroid/content/Intent;

    .line 52
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterListActivity;->getApplication()Landroid/app/Application;

    move-result-object p1

    check-cast p1, Lcom/github/droidfu/DroidFuApplication;

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getCanonicalName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0, p0}, Lcom/github/droidfu/DroidFuApplication;->setActiveContext(Ljava/lang/String;Landroid/content/Context;)V

    return-void
.end method

.method protected onCreateDialog(I)Landroid/app/Dialog;
    .locals 1

    .line 98
    iget p1, p0, Lcom/github/droidfu/activities/BetterListActivity;->progressDialogTitleId:I

    .line 99
    iget v0, p0, Lcom/github/droidfu/activities/BetterListActivity;->progressDialogMsgId:I

    .line 98
    invoke-static {p0, p1, v0}, Lcom/github/droidfu/activities/BetterActivityHelper;->createProgressDialog(Landroid/app/Activity;II)Landroid/app/ProgressDialog;

    move-result-object p1

    return-object p1
.end method

.method protected onDestroy()V
    .locals 0

    .line 58
    invoke-super {p0}, Landroid/app/ListActivity;->onDestroy()V

    return-void
.end method

.method public onKeyDown(ILandroid/view/KeyEvent;)Z
    .locals 0

    .line 174
    invoke-static {p0, p1}, Lcom/github/droidfu/activities/BetterActivityHelper;->handleApplicationClosing(Landroid/content/Context;I)V

    .line 175
    invoke-super {p0, p1, p2}, Landroid/app/ListActivity;->onKeyDown(ILandroid/view/KeyEvent;)Z

    move-result p1

    return p1
.end method

.method protected onNewIntent(Landroid/content/Intent;)V
    .locals 0

    .line 92
    invoke-super {p0, p1}, Landroid/app/ListActivity;->onNewIntent(Landroid/content/Intent;)V

    .line 93
    iput-object p1, p0, Lcom/github/droidfu/activities/BetterListActivity;->currentIntent:Landroid/content/Intent;

    return-void
.end method

.method protected onPause()V
    .locals 1

    .line 86
    invoke-super {p0}, Landroid/app/ListActivity;->onPause()V

    const/4 v0, 0x0

    .line 87
    iput-boolean v0, p0, Lcom/github/droidfu/activities/BetterListActivity;->wasInterrupted:Z

    iput-boolean v0, p0, Lcom/github/droidfu/activities/BetterListActivity;->wasCreated:Z

    return-void
.end method

.method protected onRestoreInstanceState(Landroid/os/Bundle;)V
    .locals 2

    .line 75
    invoke-super {p0, p1}, Landroid/app/ListActivity;->onRestoreInstanceState(Landroid/os/Bundle;)V

    .line 76
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterListActivity;->getListAdapter()Landroid/widget/ListAdapter;

    move-result-object v0

    .line 77
    instance-of v1, v0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;

    if-eqz v1, :cond_0

    const-string v1, "is_busy"

    .line 78
    invoke-virtual {p1, v1}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;)Z

    move-result p1

    .line 79
    check-cast v0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;

    invoke-virtual {v0, p1}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->setIsLoadingData(Z)V

    :cond_0
    const/4 p1, 0x1

    .line 81
    iput-boolean p1, p0, Lcom/github/droidfu/activities/BetterListActivity;->wasInterrupted:Z

    return-void
.end method

.method protected onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 2

    .line 65
    invoke-super {p0, p1}, Landroid/app/ListActivity;->onSaveInstanceState(Landroid/os/Bundle;)V

    .line 66
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterListActivity;->getListAdapter()Landroid/widget/ListAdapter;

    move-result-object v0

    .line 67
    instance-of v1, v0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;

    if-eqz v1, :cond_0

    .line 68
    check-cast v0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;

    invoke-virtual {v0}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->isLoadingData()Z

    move-result v0

    const-string v1, "is_busy"

    .line 69
    invoke-virtual {p1, v1, v0}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    :cond_0
    return-void
.end method

.method public setProgressDialogMsgId(I)V
    .locals 0

    .line 107
    iput p1, p0, Lcom/github/droidfu/activities/BetterListActivity;->progressDialogMsgId:I

    return-void
.end method

.method public setProgressDialogTitleId(I)V
    .locals 0

    .line 103
    iput p1, p0, Lcom/github/droidfu/activities/BetterListActivity;->progressDialogTitleId:I

    return-void
.end method
