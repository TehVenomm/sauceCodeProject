.class Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$5;
.super Lnet/gogame/gowrap/ui/view/RightDrawableOnTouchListener;
.source "SupportFragment.java"


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

    .line 202
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-direct {p0}, Lnet/gogame/gowrap/ui/view/RightDrawableOnTouchListener;-><init>()V

    return-void
.end method


# virtual methods
.method public onDrawableTouch(Landroid/view/MotionEvent;)Z
    .locals 1

    .line 206
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Landroid/widget/EditText;

    move-result-object p1

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    .line 207
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Landroid/widget/EditText;

    move-result-object v0

    invoke-virtual {v0}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$100(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;Ljava/lang/String;)V

    const/4 p1, 0x1

    return p1
.end method
