.class Ljp/colopl/drapro/AppHelper$2$3;
.super Ljava/lang/Object;
.source "AppHelper.java"

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/AppHelper$2;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/drapro/AppHelper$2;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/AppHelper$2;)V
    .locals 0

    .line 204
    iput-object p1, p0, Ljp/colopl/drapro/AppHelper$2$3;->this$0:Ljp/colopl/drapro/AppHelper$2;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 3

    .line 208
    sget-object p1, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string p2, "invite_mail_title"

    const-string v0, "string"

    sget-object v1, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, p2, v0, v1}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    .line 209
    sget-object p2, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "invite_mail_body"

    const-string v1, "string"

    sget-object v2, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    .line 210
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.SEND"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v1, "plain/text"

    .line 211
    invoke-virtual {v0, v1}, Landroid/content/Intent;->setType(Ljava/lang/String;)Landroid/content/Intent;

    const-string v1, "android.intent.extra.SUBJECT"

    .line 212
    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    .line 213
    sget-object p1, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    .line 214
    invoke-virtual {p1, p2}, Ljp/colopl/drapro/StartActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    const/4 p2, 0x1

    new-array p2, p2, [Ljava/lang/Object;

    iget-object v1, p0, Ljp/colopl/drapro/AppHelper$2$3;->this$0:Ljp/colopl/drapro/AppHelper$2;

    iget-object v1, v1, Ljp/colopl/drapro/AppHelper$2;->val$ShowText:Ljava/lang/String;

    const/4 v2, 0x0

    aput-object v1, p2, v2

    .line 213
    invoke-static {p1, p2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    const-string p2, "android.intent.extra.TEXT"

    .line 217
    invoke-virtual {v0, p2, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 218
    sget-object p1, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    const-string p2, "Mail"

    invoke-static {v0, p2}, Landroid/content/Intent;->createChooser(Landroid/content/Intent;Ljava/lang/CharSequence;)Landroid/content/Intent;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/StartActivity;->startActivity(Landroid/content/Intent;)V

    .line 219
    invoke-static {v2}, Ljp/colopl/drapro/AppHelper;->access$002(Z)Z

    return-void
.end method
