.class Ljp/colopl/drapro/AppHelper$2$2;
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

    .line 184
    iput-object p1, p0, Ljp/colopl/drapro/AppHelper$2$2;->this$0:Ljp/colopl/drapro/AppHelper$2;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 4
    .annotation build Landroid/annotation/SuppressLint;
        value = {
            "NewApi"
        }
    .end annotation

    .line 188
    sget p1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 p2, 0xb

    if-lt p1, p2, :cond_0

    .line 190
    sget-object p1, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    const-string p2, "clipboard"

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/StartActivity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/content/ClipboardManager;

    const-string p2, "InvitationCode"

    .line 191
    iget-object v0, p0, Ljp/colopl/drapro/AppHelper$2$2;->this$0:Ljp/colopl/drapro/AppHelper$2;

    iget-object v0, v0, Ljp/colopl/drapro/AppHelper$2;->val$ShowText:Ljava/lang/String;

    invoke-static {p2, v0}, Landroid/content/ClipData;->newPlainText(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Landroid/content/ClipData;

    move-result-object p2

    .line 192
    invoke-virtual {p1, p2}, Landroid/content/ClipboardManager;->setPrimaryClip(Landroid/content/ClipData;)V

    goto :goto_0

    .line 194
    :cond_0
    sget-object p1, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    const-string p2, "clipboard"

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/StartActivity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/text/ClipboardManager;

    .line 195
    iget-object p2, p0, Ljp/colopl/drapro/AppHelper$2$2;->this$0:Ljp/colopl/drapro/AppHelper$2;

    iget-object p2, p2, Ljp/colopl/drapro/AppHelper$2;->val$ShowText:Ljava/lang/String;

    invoke-virtual {p1, p2}, Landroid/text/ClipboardManager;->setText(Ljava/lang/CharSequence;)V

    :goto_0
    const/4 p1, 0x0

    .line 197
    invoke-static {p1}, Ljp/colopl/drapro/AppHelper;->access$002(Z)Z

    .line 198
    sget-object p2, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object p2

    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "copy_to_clipboard"

    const-string v2, "string"

    sget-object v3, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    invoke-static {p2, v0, p1}, Landroid/widget/Toast;->makeText(Landroid/content/Context;II)Landroid/widget/Toast;

    move-result-object p1

    invoke-virtual {p1}, Landroid/widget/Toast;->show()V

    return-void
.end method
