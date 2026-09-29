.class final Ljp/colopl/drapro/AppHelper$2;
.super Ljava/lang/Object;
.source "AppHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/AppHelper;->ShowInvitationCodeView(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$Description:Ljava/lang/String;

.field final synthetic val$ShowText:Ljava/lang/String;

.field final synthetic val$Title:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 159
    iput-object p1, p0, Ljp/colopl/drapro/AppHelper$2;->val$ShowText:Ljava/lang/String;

    iput-object p2, p0, Ljp/colopl/drapro/AppHelper$2;->val$Title:Ljava/lang/String;

    iput-object p3, p0, Ljp/colopl/drapro/AppHelper$2;->val$Description:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 5
    .annotation build Landroid/annotation/SuppressLint;
        value = {
            "NewApi"
        }
    .end annotation

    .line 163
    new-instance v0, Landroid/widget/EditText;

    sget-object v1, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-direct {v0, v1}, Landroid/widget/EditText;-><init>(Landroid/content/Context;)V

    .line 164
    iget-object v1, p0, Ljp/colopl/drapro/AppHelper$2;->val$ShowText:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    const/4 v1, 0x0

    .line 165
    invoke-virtual {v0, v1}, Landroid/widget/EditText;->setKeyListener(Landroid/text/method/KeyListener;)V

    .line 167
    new-instance v1, Landroid/app/AlertDialog$Builder;

    sget-object v2, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-direct {v1, v2}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    .line 169
    iget-object v2, p0, Ljp/colopl/drapro/AppHelper$2;->val$Title:Ljava/lang/String;

    invoke-virtual {v1, v2}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 170
    iget-object v2, p0, Ljp/colopl/drapro/AppHelper$2;->val$Description:Ljava/lang/String;

    invoke-virtual {v1, v2}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 171
    invoke-virtual {v1, v0}, Landroid/app/AlertDialog$Builder;->setView(Landroid/view/View;)Landroid/app/AlertDialog$Builder;

    .line 174
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v2, "dialog_button_cancel"

    const-string v3, "string"

    sget-object v4, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 175
    new-instance v2, Ljp/colopl/drapro/AppHelper$2$1;

    invoke-direct {v2, p0}, Ljp/colopl/drapro/AppHelper$2$1;-><init>(Ljp/colopl/drapro/AppHelper$2;)V

    invoke-virtual {v1, v0, v2}, Landroid/app/AlertDialog$Builder;->setNegativeButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 182
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v2, "dialog_button_copy"

    const-string v3, "string"

    sget-object v4, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 183
    new-instance v2, Ljp/colopl/drapro/AppHelper$2$2;

    invoke-direct {v2, p0}, Ljp/colopl/drapro/AppHelper$2$2;-><init>(Ljp/colopl/drapro/AppHelper$2;)V

    invoke-virtual {v1, v0, v2}, Landroid/app/AlertDialog$Builder;->setNeutralButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 202
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v2, "invite_mail_button"

    const-string v3, "string"

    sget-object v4, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 203
    new-instance v2, Ljp/colopl/drapro/AppHelper$2$3;

    invoke-direct {v2, p0}, Ljp/colopl/drapro/AppHelper$2$3;-><init>(Ljp/colopl/drapro/AppHelper$2;)V

    invoke-virtual {v1, v0, v2}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 223
    invoke-virtual {v1}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object v0

    const/4 v1, 0x0

    .line 224
    invoke-virtual {v0, v1}, Landroid/app/AlertDialog;->setCanceledOnTouchOutside(Z)V

    .line 225
    invoke-virtual {v0}, Landroid/app/AlertDialog;->show()V

    return-void
.end method
