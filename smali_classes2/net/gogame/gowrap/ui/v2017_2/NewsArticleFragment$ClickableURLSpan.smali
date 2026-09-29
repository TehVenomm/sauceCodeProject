.class Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$ClickableURLSpan;
.super Landroid/text/style/ClickableSpan;
.source "NewsArticleFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "ClickableURLSpan"
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

.field private final url:Ljava/lang/String;


# direct methods
.method public constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;Ljava/lang/String;)V
    .locals 0

    .line 309
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$ClickableURLSpan;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

    .line 310
    invoke-direct {p0}, Landroid/text/style/ClickableSpan;-><init>()V

    .line 312
    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$ClickableURLSpan;->url:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 317
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$ClickableURLSpan;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$ClickableURLSpan;->url:Ljava/lang/String;

    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->access$100(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;Ljava/lang/String;)V

    return-void
.end method
