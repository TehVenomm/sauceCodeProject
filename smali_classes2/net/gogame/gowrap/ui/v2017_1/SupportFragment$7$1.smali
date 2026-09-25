.class Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7$1;
.super Ljava/lang/Object;
.source "SupportFragment.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;->onFocusChange(Landroid/view/View;Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;

.field final synthetic val$distance:I


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;I)V
    .locals 0

    .line 233
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7$1;->this$1:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;

    iput p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7$1;->val$distance:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 237
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7$1;->this$1:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$500(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Landroid/widget/ExpandableListView;

    move-result-object v0

    iget v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7$1;->val$distance:I

    const/16 v2, 0x64

    invoke-virtual {v0, v1, v2}, Landroid/widget/ExpandableListView;->smoothScrollBy(II)V

    return-void
.end method
