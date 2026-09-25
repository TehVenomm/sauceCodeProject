.class Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$6;
.super Ljava/lang/Object;
.source "SupportFragment.java"

# interfaces
.implements Landroid/widget/TextView$OnEditorActionListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)V
    .locals 0

    .line 214
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onEditorAction(Landroid/widget/TextView;ILandroid/view/KeyEvent;)Z
    .locals 0

    .line 218
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$400(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)V

    .line 219
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {p2}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Landroid/widget/EditText;

    move-result-object p2

    invoke-virtual {p2}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object p2

    invoke-virtual {p2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$100(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;Ljava/lang/String;)V

    const/4 p1, 0x1

    return p1
.end method
