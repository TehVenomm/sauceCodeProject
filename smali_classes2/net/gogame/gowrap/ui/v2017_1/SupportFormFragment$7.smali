.class Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$7;
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

    .line 219
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 223
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    const/4 v0, 0x0

    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$202(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Landroid/net/Uri;)Landroid/net/Uri;

    .line 224
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$400(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/view/View;

    move-result-object p1

    const/16 v0, 0x8

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    .line 225
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$500(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/widget/TextView;

    move-result-object p1

    sget v0, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_form_attachment_no_file_caption:I

    invoke-virtual {p1, v0}, Landroid/widget/TextView;->setText(I)V

    return-void
.end method
