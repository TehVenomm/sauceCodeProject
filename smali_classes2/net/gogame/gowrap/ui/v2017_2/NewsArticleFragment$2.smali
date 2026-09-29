.class Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$2;
.super Ljava/lang/Object;
.source "NewsArticleFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->populateButton(Lnet/gogame/gowrap/model/news/MarkupElement;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

.field final synthetic val$url:Ljava/lang/String;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;Ljava/lang/String;)V
    .locals 0

    .line 281
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$2;->val$url:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 285
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$2;->val$url:Ljava/lang/String;

    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->access$100(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;Ljava/lang/String;)V

    return-void
.end method
