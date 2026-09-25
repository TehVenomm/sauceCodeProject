.class Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$7;
.super Ljava/lang/Object;
.source "NewsFeedFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->appendItems(I)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

.field final synthetic val$item:Lnet/gogame/gowrap/model/feed/Feed$Item;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Lnet/gogame/gowrap/model/feed/Feed$Item;)V
    .locals 0

    .line 393
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$7;->val$item:Lnet/gogame/gowrap/model/feed/Feed$Item;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 397
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$7;->val$item:Lnet/gogame/gowrap/model/feed/Feed$Item;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getLink()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/utils/ShareHelper;->share(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method
