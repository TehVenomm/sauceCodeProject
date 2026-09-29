.class Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView$1;
.super Ljava/lang/Object;
.source "AbstractNewsFeedItemView.java"

# interfaces
.implements Landroid/view/ViewTreeObserver$OnGlobalLayoutListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->init(Landroid/content/Context;Landroid/util/AttributeSet;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;)V
    .locals 0

    .line 86
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onGlobalLayout()V
    .locals 4

    .line 91
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;)Ljava/lang/Integer;

    move-result-object v0

    if-nez v0, :cond_0

    .line 92
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->access$100(Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;)Landroid/view/View;

    move-result-object v1

    invoke-virtual {v1}, Landroid/view/View;->getWidth()I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-static {v0, v1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->access$002(Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;Ljava/lang/Integer;)Ljava/lang/Integer;

    .line 93
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->access$100(Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;)Landroid/view/View;

    move-result-object v1

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;

    invoke-static {v2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;)Ljava/lang/Integer;

    move-result-object v2

    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;

    invoke-static {v3}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->access$200(Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;)Ljava/lang/Double;

    move-result-object v3

    invoke-virtual {v0, v1, v2, v3}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->resizeView(Landroid/view/View;Ljava/lang/Integer;Ljava/lang/Double;)V

    .line 94
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->onLayoutCompleted()V

    :cond_0
    return-void
.end method
