.class public Lcom/github/droidfu/activities/BetterExpandableListActivity;
.super Landroid/app/ExpandableListActivity;
.source "BetterExpandableListActivity.java"

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

    .line 31
    invoke-direct {p0}, Landroid/app/ExpandableListActivity;-><init>()V

    return-void
.end method


# virtual methods
.method public getCurrentIntent()Landroid/content/Intent;
    .locals 1

    .line 110
    iget-object v0, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->currentIntent:Landroid/content/Intent;

    return-object v0
.end method

.method public getWindowFeatures()I
    .locals 1

    .line 90
    invoke-static {p0}, Lcom/github/droidfu/activities/BetterActivityHelper;->getWindowFeatures(Landroid/app/Activity;)I

    move-result v0

    return v0
.end method

.method public isApplicationBroughtToBackground()Z
    .locals 1

    .line 106
    invoke-static {p0}, Lcom/github/droidfu/activities/BetterActivityHelper;->isApplicationBroughtToBackground(Landroid/content/Context;)Z

    move-result v0

    return v0
.end method

.method public isLandscapeMode()Z
    .locals 2

    .line 114
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->getWindowManager()Landroid/view/WindowManager;

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

    .line 102
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->wasInterrupted:Z

    if-nez v0, :cond_0

    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->wasCreated:Z

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public isPortraitMode()Z
    .locals 1

    .line 118
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->isLandscapeMode()Z

    move-result v0

    xor-int/lit8 v0, v0, 0x1

    return v0
.end method

.method public isRestoring()Z
    .locals 1

    .line 94
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->wasInterrupted:Z

    return v0
.end method

.method public isResuming()Z
    .locals 1

    .line 98
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->wasCreated:Z

    xor-int/lit8 v0, v0, 0x1

    return v0
.end method

.method public newAlertDialog(II)Landroid/app/AlertDialog;
    .locals 1

    .line 133
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 134
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    const v0, 0x1080027

    .line 133
    invoke-static {p0, p1, p2, v0}, Lcom/github/droidfu/activities/BetterActivityHelper;->newMessageDialog(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;I)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newErrorHandlerDialog(ILjava/lang/Exception;)Landroid/app/AlertDialog;
    .locals 0

    .line 138
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    invoke-static {p0, p1, p2}, Lcom/github/droidfu/activities/BetterActivityHelper;->newErrorHandlerDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/Exception;)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newErrorHandlerDialog(Ljava/lang/Exception;)Landroid/app/AlertDialog;
    .locals 4

    .line 142
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "droidfu_error_dialog_title"

    const-string v2, "string"

    .line 143
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 142
    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    invoke-virtual {p0, v0, p1}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->newErrorHandlerDialog(ILjava/lang/Exception;)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newInfoDialog(II)Landroid/app/AlertDialog;
    .locals 1

    .line 128
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 129
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    const v0, 0x108009b

    .line 128
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

    .line 149
    invoke-static {p0, p1, p2, p3, p4}, Lcom/github/droidfu/activities/BetterActivityHelper;->newListDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Lcom/github/droidfu/dialogs/DialogClickListener;Z)Landroid/app/Dialog;

    move-result-object p1

    return-object p1
.end method

.method public newYesNoDialog(IILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog;
    .locals 1

    .line 123
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 124
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    const v0, 0x108009b

    .line 123
    invoke-static {p0, p1, p2, v0, p3}, Lcom/github/droidfu/activities/BetterActivityHelper;->newYesNoDialog(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method protected onCreate(Landroid/os/Bundle;)V
    .locals 1

    .line 43
    invoke-super {p0, p1}, Landroid/app/ExpandableListActivity;->onCreate(Landroid/os/Bundle;)V

    const/4 p1, 0x1

    .line 45
    iput-boolean p1, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->wasCreated:Z

    .line 46
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->getIntent()Landroid/content/Intent;

    move-result-object p1

    iput-object p1, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->currentIntent:Landroid/content/Intent;

    .line 48
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterExpandableListActivity;->getApplication()Landroid/app/Application;

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

    .line 77
    iget p1, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->progressDialogTitleId:I

    .line 78
    iget v0, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->progressDialogMsgId:I

    .line 77
    invoke-static {p0, p1, v0}, Lcom/github/droidfu/activities/BetterActivityHelper;->createProgressDialog(Landroid/app/Activity;II)Landroid/app/ProgressDialog;

    move-result-object p1

    return-object p1
.end method

.method public onKeyDown(ILandroid/view/KeyEvent;)Z
    .locals 0

    .line 154
    invoke-static {p0, p1}, Lcom/github/droidfu/activities/BetterActivityHelper;->handleApplicationClosing(Landroid/content/Context;I)V

    .line 155
    invoke-super {p0, p1, p2}, Landroid/app/ExpandableListActivity;->onKeyDown(ILandroid/view/KeyEvent;)Z

    move-result p1

    return p1
.end method

.method protected onNewIntent(Landroid/content/Intent;)V
    .locals 0

    .line 71
    invoke-super {p0, p1}, Landroid/app/ExpandableListActivity;->onNewIntent(Landroid/content/Intent;)V

    .line 72
    iput-object p1, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->currentIntent:Landroid/content/Intent;

    return-void
.end method

.method protected onPause()V
    .locals 1

    .line 65
    invoke-super {p0}, Landroid/app/ExpandableListActivity;->onPause()V

    const/4 v0, 0x0

    .line 66
    iput-boolean v0, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->wasInterrupted:Z

    iput-boolean v0, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->wasCreated:Z

    return-void
.end method

.method protected onRestoreInstanceState(Landroid/os/Bundle;)V
    .locals 0

    .line 59
    invoke-super {p0, p1}, Landroid/app/ExpandableListActivity;->onRestoreInstanceState(Landroid/os/Bundle;)V

    const/4 p1, 0x1

    .line 60
    iput-boolean p1, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->wasInterrupted:Z

    return-void
.end method

.method protected onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 0

    .line 54
    invoke-super {p0, p1}, Landroid/app/ExpandableListActivity;->onSaveInstanceState(Landroid/os/Bundle;)V

    return-void
.end method

.method public setProgressDialogMsgId(I)V
    .locals 0

    .line 86
    iput p1, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->progressDialogMsgId:I

    return-void
.end method

.method public setProgressDialogTitleId(I)V
    .locals 0

    .line 82
    iput p1, p0, Lcom/github/droidfu/activities/BetterExpandableListActivity;->progressDialogTitleId:I

    return-void
.end method
