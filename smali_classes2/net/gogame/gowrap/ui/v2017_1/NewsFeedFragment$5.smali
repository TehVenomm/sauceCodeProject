.class Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;
.super Ljava/lang/Object;
.source "NewsFeedFragment.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->initializeNewsFeed()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

.field final synthetic val$childView:Landroid/view/View;

.field final synthetic val$scrollX:I

.field final synthetic val$scrollY:I


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Landroid/view/View;II)V
    .locals 0

    .line 279
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->val$childView:Landroid/view/View;

    iput p3, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->val$scrollY:I

    iput p4, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->val$scrollX:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 283
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->val$childView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getHeight()I

    move-result v0

    iget v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->val$scrollY:I

    if-lt v0, v1, :cond_0

    .line 284
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->access$800(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)Landroid/widget/ScrollView;

    move-result-object v0

    iget v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->val$scrollX:I

    iget v2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->val$scrollY:I

    invoke-virtual {v0, v1, v2}, Landroid/widget/ScrollView;->scrollTo(II)V

    goto :goto_0

    .line 286
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->access$800(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)Landroid/widget/ScrollView;

    move-result-object v0

    iget v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->val$scrollX:I

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->val$childView:Landroid/view/View;

    invoke-virtual {v2}, Landroid/view/View;->getHeight()I

    move-result v2

    invoke-virtual {v0, v1, v2}, Landroid/widget/ScrollView;->scrollTo(II)V

    .line 287
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->access$800(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)Landroid/widget/ScrollView;

    move-result-object v0

    const-wide/16 v1, 0x3e8

    invoke-virtual {v0, p0, v1, v2}, Landroid/widget/ScrollView;->postDelayed(Ljava/lang/Runnable;J)Z

    :goto_0
    return-void
.end method
