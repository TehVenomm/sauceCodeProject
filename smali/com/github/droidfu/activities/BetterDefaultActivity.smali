.class public Lcom/github/droidfu/activities/BetterDefaultActivity;
.super Landroid/app/Activity;
.source "BetterDefaultActivity.java"

# interfaces
.implements Lcom/github/droidfu/activities/BetterActivity;


# instance fields
.field private currentIntent:Landroid/content/Intent;

.field private progressDialogMsgId:I

.field private progressDialogTitleId:I

.field private wasCreated:Z

.field private wasInterrupted:Z


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 32
    invoke-direct {p0}, Landroid/app/Activity;-><init>()V

    return-void
.end method


# virtual methods
.method public getCurrentIntent()Landroid/content/Intent;
    .locals 1

    .line 115
    iget-object v0, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->currentIntent:Landroid/content/Intent;

    return-object v0
.end method

.method public getWindowFeatures()I
    .locals 1

    .line 95
    invoke-static {p0}, Lcom/github/droidfu/activities/BetterActivityHelper;->getWindowFeatures(Landroid/app/Activity;)I

    move-result v0

    return v0
.end method

.method public isApplicationBroughtToBackground()Z
    .locals 1

    .line 111
    invoke-static {p0}, Lcom/github/droidfu/activities/BetterActivityHelper;->isApplicationBroughtToBackground(Landroid/content/Context;)Z

    move-result v0

    return v0
.end method

.method public isLandscapeMode()Z
    .locals 2

    .line 119
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterDefaultActivity;->getWindowManager()Landroid/view/WindowManager;

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

    .line 107
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->wasInterrupted:Z

    if-nez v0, :cond_0

    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->wasCreated:Z

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public isPortraitMode()Z
    .locals 1

    .line 123
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterDefaultActivity;->isLandscapeMode()Z

    move-result v0

    xor-int/lit8 v0, v0, 0x1

    return v0
.end method

.method public isRestoring()Z
    .locals 1

    .line 99
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->wasInterrupted:Z

    return v0
.end method

.method public isResuming()Z
    .locals 1

    .line 103
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->wasCreated:Z

    xor-int/lit8 v0, v0, 0x1

    return v0
.end method

.method public newAlertDialog(II)Landroid/app/AlertDialog;
    .locals 1

    .line 138
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterDefaultActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 139
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterDefaultActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    const v0, 0x1080027

    .line 138
    invoke-static {p0, p1, p2, v0}, Lcom/github/droidfu/activities/BetterActivityHelper;->newMessageDialog(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;I)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newErrorHandlerDialog(ILjava/lang/Exception;)Landroid/app/AlertDialog;
    .locals 0

    .line 143
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterDefaultActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    invoke-static {p0, p1, p2}, Lcom/github/droidfu/activities/BetterActivityHelper;->newErrorHandlerDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/Exception;)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newErrorHandlerDialog(Ljava/lang/Exception;)Landroid/app/AlertDialog;
    .locals 4

    .line 147
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterDefaultActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "droidfu_error_dialog_title"

    const-string v2, "string"

    .line 148
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterDefaultActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 147
    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    invoke-virtual {p0, v0, p1}, Lcom/github/droidfu/activities/BetterDefaultActivity;->newErrorHandlerDialog(ILjava/lang/Exception;)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newInfoDialog(II)Landroid/app/AlertDialog;
    .locals 1

    .line 133
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterDefaultActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 134
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterDefaultActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    const v0, 0x108009b

    .line 133
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

    .line 154
    invoke-static {p0, p1, p2, p3, p4}, Lcom/github/droidfu/activities/BetterActivityHelper;->newListDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Lcom/github/droidfu/dialogs/DialogClickListener;Z)Landroid/app/Dialog;

    move-result-object p1

    return-object p1
.end method

.method public newYesNoDialog(IILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog;
    .locals 1

    .line 128
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterDefaultActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 129
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterDefaultActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    const v0, 0x108009b

    .line 128
    invoke-static {p0, p1, p2, v0, p3}, Lcom/github/droidfu/activities/BetterActivityHelper;->newYesNoDialog(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method protected onCreate(Landroid/os/Bundle;)V
    .locals 1

    .line 44
    invoke-super {p0, p1}, Landroid/app/Activity;->onCreate(Landroid/os/Bundle;)V

    const/4 p1, 0x1

    .line 46
    iput-boolean p1, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->wasCreated:Z

    .line 47
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterDefaultActivity;->getIntent()Landroid/content/Intent;

    move-result-object p1

    iput-object p1, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->currentIntent:Landroid/content/Intent;

    .line 49
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterDefaultActivity;->getApplication()Landroid/app/Application;

    move-result-object p1

    .line 50
    instance-of v0, p1, Lcom/github/droidfu/DroidFuApplication;

    if-eqz v0, :cond_0

    .line 51
    check-cast p1, Lcom/github/droidfu/DroidFuApplication;

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getCanonicalName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0, p0}, Lcom/github/droidfu/DroidFuApplication;->setActiveContext(Ljava/lang/String;Landroid/content/Context;)V

    :cond_0
    return-void
.end method

.method protected onCreateDialog(I)Landroid/app/Dialog;
    .locals 1

    .line 82
    iget p1, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->progressDialogTitleId:I

    .line 83
    iget v0, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->progressDialogMsgId:I

    .line 82
    invoke-static {p0, p1, v0}, Lcom/github/droidfu/activities/BetterActivityHelper;->createProgressDialog(Landroid/app/Activity;II)Landroid/app/ProgressDialog;

    move-result-object p1

    return-object p1
.end method

.method protected onDestroy()V
    .locals 0

    .line 57
    invoke-super {p0}, Landroid/app/Activity;->onDestroy()V

    return-void
.end method

.method public onKeyDown(ILandroid/view/KeyEvent;)Z
    .locals 0

    .line 159
    invoke-static {p0, p1}, Lcom/github/droidfu/activities/BetterActivityHelper;->handleApplicationClosing(Landroid/content/Context;I)V

    .line 160
    invoke-super {p0, p1, p2}, Landroid/app/Activity;->onKeyDown(ILandroid/view/KeyEvent;)Z

    move-result p1

    return p1
.end method

.method protected onNewIntent(Landroid/content/Intent;)V
    .locals 0

    .line 76
    invoke-super {p0, p1}, Landroid/app/Activity;->onNewIntent(Landroid/content/Intent;)V

    .line 77
    iput-object p1, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->currentIntent:Landroid/content/Intent;

    return-void
.end method

.method protected onPause()V
    .locals 1

    .line 70
    invoke-super {p0}, Landroid/app/Activity;->onPause()V

    const/4 v0, 0x0

    .line 71
    iput-boolean v0, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->wasInterrupted:Z

    iput-boolean v0, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->wasCreated:Z

    return-void
.end method

.method protected onRestoreInstanceState(Landroid/os/Bundle;)V
    .locals 0

    .line 64
    invoke-super {p0, p1}, Landroid/app/Activity;->onRestoreInstanceState(Landroid/os/Bundle;)V

    const/4 p1, 0x1

    .line 65
    iput-boolean p1, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->wasInterrupted:Z

    return-void
.end method

.method public setProgressDialogMsgId(I)V
    .locals 0

    .line 91
    iput p1, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->progressDialogMsgId:I

    return-void
.end method

.method public setProgressDialogTitleId(I)V
    .locals 0

    .line 87
    iput p1, p0, Lcom/github/droidfu/activities/BetterDefaultActivity;->progressDialogTitleId:I

    return-void
.end method
