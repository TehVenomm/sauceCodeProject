.class Ljp/colopl/drapro/StartActivity$16;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/StartActivity;->showConsumeDialog(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/drapro/StartActivity;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/StartActivity;)V
    .locals 0

    .line 1128
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$16;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 2

    .line 1133
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$16;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string p2, "url_terms"

    const-string v0, "string"

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$16;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, p2, v0, v1}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    .line 1134
    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$16;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p2, p1}, Ljp/colopl/drapro/StartActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p1

    .line 1135
    new-instance p2, Landroid/content/Intent;

    const-string v0, "android.intent.action.VIEW"

    invoke-direct {p2, v0, p1}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    .line 1136
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$16;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/StartActivity;->startActivity(Landroid/content/Intent;)V

    .line 1137
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$16;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    return-void
.end method
