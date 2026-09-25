.class Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2$4;
.super Ljava/lang/Object;
.source "NewsFeedFragment.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->onDownloadFailed()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;)V
    .locals 0

    .line 149
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2$4;->this$1:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 153
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2$4;->this$1:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)V

    .line 154
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2$4;->this$1:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->access$500(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)Lnet/gogame/gowrap/ui/UIContext;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;

    invoke-direct {v1}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;-><init>()V

    invoke-interface {v0, v1}, Lnet/gogame/gowrap/ui/UIContext;->pushFragment(Landroid/app/Fragment;)V

    return-void
.end method
