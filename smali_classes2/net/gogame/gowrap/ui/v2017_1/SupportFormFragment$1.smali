.class Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$1;
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

    .line 64
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 0

    .line 68
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Lnet/gogame/gowrap/ui/UIContext;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 69
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Lnet/gogame/gowrap/ui/UIContext;

    move-result-object p1

    invoke-interface {p1}, Lnet/gogame/gowrap/ui/UIContext;->goBack()Z

    :cond_0
    return-void
.end method
