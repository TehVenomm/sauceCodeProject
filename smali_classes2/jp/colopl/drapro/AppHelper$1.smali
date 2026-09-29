.class final Ljp/colopl/drapro/AppHelper$1;
.super Ljava/lang/Object;
.source "AppHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/AppHelper;->quit()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    .line 102
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 6

    .line 105
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "dialog_title_quit_fullname"

    const-string v2, "string"

    sget-object v3, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 106
    sget-object v1, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "dialog_message_quit"

    const-string v3, "string"

    sget-object v4, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    .line 107
    sget-object v2, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    const-string v3, "dialog_button_quit"

    const-string v4, "string"

    sget-object v5, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v5}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    .line 108
    new-instance v3, Landroid/app/AlertDialog$Builder;

    sget-object v4, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-direct {v3, v4}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    .line 109
    sget-object v4, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4, v0}, Ljp/colopl/drapro/StartActivity;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v3, v0}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 110
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0, v1}, Ljp/colopl/drapro/StartActivity;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v3, v0}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 111
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0, v2}, Ljp/colopl/drapro/StartActivity;->getString(I)Ljava/lang/String;

    move-result-object v0

    new-instance v1, Ljp/colopl/drapro/AppHelper$1$1;

    invoke-direct {v1, p0}, Ljp/colopl/drapro/AppHelper$1$1;-><init>(Ljp/colopl/drapro/AppHelper$1;)V

    invoke-virtual {v3, v0, v1}, Landroid/app/AlertDialog$Builder;->setPositiveButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 120
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "dialog_button_cancel"

    const-string v2, "string"

    sget-object v4, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0, v1, v2, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 121
    sget-object v1, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v1, v0}, Ljp/colopl/drapro/StartActivity;->getString(I)Ljava/lang/String;

    move-result-object v0

    new-instance v1, Ljp/colopl/drapro/AppHelper$1$2;

    invoke-direct {v1, p0}, Ljp/colopl/drapro/AppHelper$1$2;-><init>(Ljp/colopl/drapro/AppHelper$1;)V

    invoke-virtual {v3, v0, v1}, Landroid/app/AlertDialog$Builder;->setNegativeButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 129
    new-instance v0, Ljp/colopl/drapro/AppHelper$1$3;

    invoke-direct {v0, p0}, Ljp/colopl/drapro/AppHelper$1$3;-><init>(Ljp/colopl/drapro/AppHelper$1;)V

    invoke-virtual {v3, v0}, Landroid/app/AlertDialog$Builder;->setOnCancelListener(Landroid/content/DialogInterface$OnCancelListener;)Landroid/app/AlertDialog$Builder;

    .line 137
    invoke-virtual {v3}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object v0

    .line 138
    invoke-virtual {v0}, Landroid/app/AlertDialog;->show()V

    return-void
.end method
