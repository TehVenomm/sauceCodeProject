.class Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$6;
.super Ljava/lang/Object;
.source "SupportFormFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)V
    .locals 0

    .line 205
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 3

    .line 209
    new-instance p1, Landroid/content/Intent;

    invoke-direct {p1}, Landroid/content/Intent;-><init>()V

    const-string v0, "image/*"

    .line 210
    invoke-virtual {p1, v0}, Landroid/content/Intent;->setType(Ljava/lang/String;)Landroid/content/Intent;

    const-string v0, "android.intent.action.GET_CONTENT"

    .line 211
    invoke-virtual {p1, v0}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    .line 212
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    sget v2, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_form_select_picture_prompt:I

    .line 213
    invoke-virtual {v1, v2}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v1

    .line 212
    invoke-static {p1, v1}, Landroid/content/Intent;->createChooser(Landroid/content/Intent;Ljava/lang/CharSequence;)Landroid/content/Intent;

    move-result-object p1

    const/16 v1, 0x1389

    invoke-virtual {v0, p1, v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->startActivityForResult(Landroid/content/Intent;I)V

    return-void
.end method
