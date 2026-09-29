.class Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$6;
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

    .line 371
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$6;->val$item:Lnet/gogame/gowrap/model/feed/Feed$Item;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    .line 375
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$6;->val$item:Lnet/gogame/gowrap/model/feed/Feed$Item;

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getArticleLink()Ljava/lang/String;

    move-result-object p1

    const/4 v0, 0x0

    if-eqz p1, :cond_0

    .line 380
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->access$500(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)Lnet/gogame/gowrap/ui/UIContext;

    move-result-object p1

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$6;->val$item:Lnet/gogame/gowrap/model/feed/Feed$Item;

    invoke-virtual {v1}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getArticleLink()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p1, v1, v0}, Lnet/gogame/gowrap/ui/UIContext;->loadUrl(Ljava/lang/String;Z)V

    goto :goto_0

    .line 387
    :cond_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->access$500(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)Lnet/gogame/gowrap/ui/UIContext;

    move-result-object p1

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$6;->val$item:Lnet/gogame/gowrap/model/feed/Feed$Item;

    invoke-virtual {v1}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getLink()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p1, v1, v0}, Lnet/gogame/gowrap/ui/UIContext;->loadUrl(Ljava/lang/String;Z)V

    :goto_0
    return-void
.end method
