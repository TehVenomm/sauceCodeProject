.class Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$3;
.super Ljava/lang/Object;
.source "SupportFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


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

.field final synthetic val$uiContext:Lnet/gogame/gowrap/ui/UIContext;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;Lnet/gogame/gowrap/ui/UIContext;)V
    .locals 0

    .line 156
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$3;->val$uiContext:Lnet/gogame/gowrap/ui/UIContext;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 160
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$3;->val$uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-eqz p1, :cond_0

    .line 161
    new-instance p1, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-direct {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;-><init>()V

    .line 162
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$3;->val$uiContext:Lnet/gogame/gowrap/ui/UIContext;

    invoke-interface {v0, p1}, Lnet/gogame/gowrap/ui/UIContext;->pushFragment(Landroid/app/Fragment;)V

    :cond_0
    return-void
.end method
