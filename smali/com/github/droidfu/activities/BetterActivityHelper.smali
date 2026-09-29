.class public Lcom/github/droidfu/activities/BetterActivityHelper;
.super Ljava/lang/Object;
.source "BetterActivityHelper.java"


# static fields
.field public static final ERROR_DIALOG_TITLE_RESOURCE:Ljava/lang/String; = "droidfu_error_dialog_title"

.field private static final PROGRESS_DIALOG_MESSAGE_RESOURCE:Ljava/lang/String; = "droidfu_progress_dialog_message"

.field private static final PROGRESS_DIALOG_TITLE_RESOURCE:Ljava/lang/String; = "droidfu_progress_dialog_title"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 42
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static createProgressDialog(Landroid/app/Activity;II)Landroid/app/ProgressDialog;
    .locals 4

    .line 81
    new-instance v0, Landroid/app/ProgressDialog;

    invoke-direct {v0, p0}, Landroid/app/ProgressDialog;-><init>(Landroid/content/Context;)V

    if-gtz p1, :cond_0

    .line 83
    invoke-virtual {p0}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string v1, "droidfu_progress_dialog_title"

    const-string v2, "string"

    .line 84
    invoke-virtual {p0}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 83
    invoke-virtual {p1, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    .line 86
    :cond_0
    invoke-virtual {v0, p1}, Landroid/app/ProgressDialog;->setTitle(I)V

    if-gtz p2, :cond_1

    .line 88
    invoke-virtual {p0}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string p2, "droidfu_progress_dialog_message"

    const-string v1, "string"

    .line 89
    invoke-virtual {p0}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    .line 88
    invoke-virtual {p1, p2, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    .line 91
    :cond_1
    invoke-virtual {p0, p2}, Landroid/app/Activity;->getString(I)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Landroid/app/ProgressDialog;->setMessage(Ljava/lang/CharSequence;)V

    const/4 p1, 0x1

    .line 92
    invoke-virtual {v0, p1}, Landroid/app/ProgressDialog;->setIndeterminate(Z)V

    .line 93
    new-instance p1, Lcom/github/droidfu/activities/BetterActivityHelper$1;

    invoke-direct {p1, p0}, Lcom/github/droidfu/activities/BetterActivityHelper$1;-><init>(Landroid/app/Activity;)V

    invoke-virtual {v0, p1}, Landroid/app/ProgressDialog;->setOnKeyListener(Landroid/content/DialogInterface$OnKeyListener;)V

    return-object v0
.end method

.method public static getWindowFeatures(Landroid/app/Activity;)I
    .locals 1

    .line 52
    invoke-virtual {p0}, Landroid/app/Activity;->getWindow()Landroid/view/Window;

    move-result-object p0

    const/4 v0, 0x0

    if-nez p0, :cond_0

    return v0

    :cond_0
    return v0
.end method

.method static handleApplicationClosing(Landroid/content/Context;I)V
    .locals 2

    const/4 v0, 0x4

    if-ne p1, v0, :cond_0

    const-string p1, "activity"

    .line 297
    invoke-virtual {p0, p1}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    .line 296
    check-cast p1, Landroid/app/ActivityManager;

    const/4 v0, 0x2

    .line 298
    invoke-virtual {p1, v0}, Landroid/app/ActivityManager;->getRunningTasks(I)Ljava/util/List;

    move-result-object p1

    const/4 v0, 0x0

    .line 300
    invoke-interface {p1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/app/ActivityManager$RunningTaskInfo;

    const/4 v1, 0x1

    .line 301
    invoke-interface {p1, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/app/ActivityManager$RunningTaskInfo;

    .line 306
    iget-object v1, v0, Landroid/app/ActivityManager$RunningTaskInfo;->topActivity:Landroid/content/ComponentName;

    iget-object v0, v0, Landroid/app/ActivityManager$RunningTaskInfo;->baseActivity:Landroid/content/ComponentName;

    invoke-virtual {v1, v0}, Landroid/content/ComponentName;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 307
    iget-object p1, p1, Landroid/app/ActivityManager$RunningTaskInfo;->baseActivity:Landroid/content/ComponentName;

    invoke-virtual {p1}, Landroid/content/ComponentName;->getPackageName()Ljava/lang/String;

    move-result-object p1

    const-string v0, "com.android.launcher"

    invoke-virtual {p1, v0}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 309
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    .line 308
    check-cast p0, Lcom/github/droidfu/DroidFuApplication;

    .line 310
    invoke-virtual {p0}, Lcom/github/droidfu/DroidFuApplication;->onClose()V

    :cond_0
    return-void
.end method

.method public static isApplicationBroughtToBackground(Landroid/content/Context;)Z
    .locals 4

    const-string v0, "activity"

    .line 270
    invoke-virtual {p0, v0}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/app/ActivityManager;

    const/4 v1, 0x1

    .line 271
    invoke-virtual {v0, v1}, Landroid/app/ActivityManager;->getRunningTasks(I)Ljava/util/List;

    move-result-object v0

    .line 272
    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v2

    const/4 v3, 0x0

    if-nez v2, :cond_0

    .line 273
    invoke-interface {v0, v3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/app/ActivityManager$RunningTaskInfo;

    iget-object v0, v0, Landroid/app/ActivityManager$RunningTaskInfo;->topActivity:Landroid/content/ComponentName;

    .line 274
    invoke-virtual {v0}, Landroid/content/ComponentName;->getPackageName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p0

    if-nez p0, :cond_0

    return v1

    :cond_0
    return v3
.end method

.method public static newErrorHandlerDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/Exception;)Landroid/app/AlertDialog;
    .locals 6

    .line 169
    instance-of v0, p2, Lcom/github/droidfu/exception/ResourceMessageException;

    if-eqz v0, :cond_0

    .line 170
    move-object v0, p2

    check-cast v0, Lcom/github/droidfu/exception/ResourceMessageException;

    .line 171
    invoke-interface {v0}, Lcom/github/droidfu/exception/ResourceMessageException;->getClientMessageResourceId()I

    move-result v0

    .line 170
    invoke-virtual {p0, v0}, Landroid/app/Activity;->getString(I)Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    .line 173
    :cond_0
    invoke-virtual {p2}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v0

    .line 176
    :goto_0
    new-instance v1, Landroid/app/AlertDialog$Builder;

    invoke-direct {v1, p0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    .line 177
    invoke-virtual {v1, p1}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 178
    invoke-virtual {v1, v0}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    const p1, 0x1080027

    .line 179
    invoke-virtual {v1, p1}, Landroid/app/AlertDialog$Builder;->setIcon(I)Landroid/app/AlertDialog$Builder;

    const/4 p1, 0x0

    .line 180
    invoke-virtual {v1, p1}, Landroid/app/AlertDialog$Builder;->setCancelable(Z)Landroid/app/AlertDialog$Builder;

    const p1, 0x104000a

    .line 181
    invoke-virtual {p0, p1}, Landroid/app/Activity;->getString(I)Ljava/lang/String;

    move-result-object p1

    new-instance v0, Lcom/github/droidfu/activities/BetterActivityHelper$3;

    invoke-direct {v0}, Lcom/github/droidfu/activities/BetterActivityHelper$3;-><init>()V

    invoke-virtual {v1, p1, v0}, Landroid/app/AlertDialog$Builder;->setPositiveButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    const-string p1, "android.intent.action.SEND"

    const-string v0, "message/rfc822"

    .line 188
    invoke-static {p0, p1, v0}, Lcom/github/droidfu/support/IntentSupport;->isIntentAvailable(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_1

    .line 190
    invoke-virtual {p0}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string v0, "droidfu_dialog_button_send_error_report"

    const-string v2, "string"

    .line 191
    invoke-virtual {p0}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 190
    invoke-virtual {p1, v0, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    .line 192
    invoke-virtual {p0, p1}, Landroid/app/Activity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 193
    invoke-virtual {p0}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v2, "droidfu_error_report_email_address"

    const-string v3, "string"

    .line 194
    invoke-virtual {p0}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    .line 193
    invoke-virtual {v0, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 195
    invoke-virtual {p0, v0}, Landroid/app/Activity;->getString(I)Ljava/lang/String;

    move-result-object v0

    .line 196
    invoke-virtual {p0}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    const-string v3, "droidfu_error_report_email_subject"

    const-string v4, "string"

    .line 197
    invoke-virtual {p0}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v5

    .line 196
    invoke-virtual {v2, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    .line 198
    invoke-virtual {p0, v2}, Landroid/app/Activity;->getString(I)Ljava/lang/String;

    move-result-object v2

    .line 199
    invoke-static {p0, p2}, Lcom/github/droidfu/support/DiagnosticSupport;->createDiagnosis(Landroid/app/Activity;Ljava/lang/Exception;)Ljava/lang/String;

    move-result-object p2

    .line 200
    invoke-static {p0, v0, v2, p2}, Lcom/github/droidfu/support/IntentSupport;->newEmailIntent(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p2

    .line 202
    new-instance v0, Lcom/github/droidfu/activities/BetterActivityHelper$4;

    invoke-direct {v0, p0, p2}, Lcom/github/droidfu/activities/BetterActivityHelper$4;-><init>(Landroid/app/Activity;Landroid/content/Intent;)V

    invoke-virtual {v1, p1, v0}, Landroid/app/AlertDialog$Builder;->setNegativeButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 211
    :cond_1
    invoke-virtual {v1}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object p0

    return-object p0
.end method

.method public static newListDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Lcom/github/droidfu/dialogs/DialogClickListener;Z)Landroid/app/Dialog;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "TT;>;",
            "Lcom/github/droidfu/dialogs/DialogClickListener<",
            "TT;>;Z)",
            "Landroid/app/Dialog;"
        }
    .end annotation

    const/4 v5, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move v4, p4

    .line 235
    invoke-static/range {v0 .. v5}, Lcom/github/droidfu/activities/BetterActivityHelper;->newListDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Lcom/github/droidfu/dialogs/DialogClickListener;ZI)Landroid/app/Dialog;

    move-result-object p0

    return-object p0
.end method

.method public static newListDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Lcom/github/droidfu/dialogs/DialogClickListener;ZI)Landroid/app/Dialog;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(",
            "Landroid/app/Activity;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "TT;>;",
            "Lcom/github/droidfu/dialogs/DialogClickListener<",
            "TT;>;ZI)",
            "Landroid/app/Dialog;"
        }
    .end annotation

    .line 241
    invoke-interface {p2}, Ljava/util/List;->size()I

    move-result v0

    .line 242
    new-array v1, v0, [Ljava/lang/String;

    const/4 v2, 0x0

    :goto_0
    if-lt v2, v0, :cond_1

    .line 247
    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    if-eqz p1, :cond_0

    .line 249
    invoke-virtual {v0, p1}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 251
    :cond_0
    new-instance p0, Lcom/github/droidfu/activities/BetterActivityHelper$5;

    invoke-direct {p0, p4, p3, p2}, Lcom/github/droidfu/activities/BetterActivityHelper$5;-><init>(ZLcom/github/droidfu/dialogs/DialogClickListener;Ljava/util/List;)V

    invoke-virtual {v0, v1, p5, p0}, Landroid/app/AlertDialog$Builder;->setSingleChoiceItems([Ljava/lang/CharSequence;ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 260
    invoke-virtual {v0}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object p0

    return-object p0

    .line 244
    :cond_1
    invoke-interface {p2, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v1, v2

    add-int/lit8 v2, v2, 0x1

    goto :goto_0
.end method

.method public static newMessageDialog(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;I)Landroid/app/AlertDialog;
    .locals 2

    .line 138
    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    const/4 p0, 0x0

    .line 139
    invoke-virtual {v0, p0}, Landroid/app/AlertDialog$Builder;->setCancelable(Z)Landroid/app/AlertDialog$Builder;

    const-string p0, "Okay"

    .line 140
    new-instance v1, Lcom/github/droidfu/activities/BetterActivityHelper$2;

    invoke-direct {v1}, Lcom/github/droidfu/activities/BetterActivityHelper$2;-><init>()V

    invoke-virtual {v0, p0, v1}, Landroid/app/AlertDialog$Builder;->setPositiveButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 147
    invoke-virtual {v0, p1}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 148
    invoke-virtual {v0, p2}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 149
    invoke-virtual {v0, p3}, Landroid/app/AlertDialog$Builder;->setIcon(I)Landroid/app/AlertDialog$Builder;

    .line 151
    invoke-virtual {v0}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object p0

    return-object p0
.end method

.method public static newYesNoDialog(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog;
    .locals 1

    .line 115
    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    const/4 p0, 0x0

    .line 116
    invoke-virtual {v0, p0}, Landroid/app/AlertDialog$Builder;->setCancelable(Z)Landroid/app/AlertDialog$Builder;

    const p0, 0x1040013

    .line 117
    invoke-virtual {v0, p0, p4}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    const p0, 0x1040009

    .line 118
    invoke-virtual {v0, p0, p4}, Landroid/app/AlertDialog$Builder;->setNegativeButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 120
    invoke-virtual {v0, p1}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 121
    invoke-virtual {v0, p2}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 122
    invoke-virtual {v0, p3}, Landroid/app/AlertDialog$Builder;->setIcon(I)Landroid/app/AlertDialog$Builder;

    .line 124
    invoke-virtual {v0}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object p0

    return-object p0
.end method
