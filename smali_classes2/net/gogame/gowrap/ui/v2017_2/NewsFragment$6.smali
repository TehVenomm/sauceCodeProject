.class Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;
.super Ljava/lang/Object;
.source "NewsFragment.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->doShowBanner(Lnet/gogame/gowrap/model/news/Banner;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

.field final synthetic val$banner:Lnet/gogame/gowrap/model/news/Banner;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Lnet/gogame/gowrap/model/news/Banner;)V
    .locals 0

    .line 387
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->val$banner:Lnet/gogame/gowrap/model/news/Banner;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 10

    .line 391
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->val$banner:Lnet/gogame/gowrap/model/news/Banner;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/Banner;->getStartDateTime()Ljava/lang/Long;

    move-result-object v0

    const/4 v1, 0x1

    const/4 v2, 0x0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->val$banner:Lnet/gogame/gowrap/model/news/Banner;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/Banner;->getEndDateTime()Ljava/lang/Long;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 392
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$1300(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/TextView;

    move-result-object v0

    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    sget v4, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_news_banner_time_period_format:I

    const/4 v5, 0x2

    new-array v5, v5, [Ljava/lang/Object;

    iget-object v6, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    .line 394
    invoke-static {v6}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$1200(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Ljava/text/DateFormat;

    move-result-object v6

    new-instance v7, Ljava/util/Date;

    iget-object v8, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->val$banner:Lnet/gogame/gowrap/model/news/Banner;

    invoke-virtual {v8}, Lnet/gogame/gowrap/model/news/Banner;->getStartDateTime()Ljava/lang/Long;

    move-result-object v8

    invoke-virtual {v8}, Ljava/lang/Long;->longValue()J

    move-result-wide v8

    invoke-direct {v7, v8, v9}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v6, v7}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v2

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    .line 395
    invoke-static {v2}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$1200(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Ljava/text/DateFormat;

    move-result-object v2

    new-instance v6, Ljava/util/Date;

    iget-object v7, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->val$banner:Lnet/gogame/gowrap/model/news/Banner;

    invoke-virtual {v7}, Lnet/gogame/gowrap/model/news/Banner;->getEndDateTime()Ljava/lang/Long;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/Long;->longValue()J

    move-result-wide v7

    invoke-direct {v6, v7, v8}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v2, v6}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v2

    aput-object v2, v5, v1

    .line 392
    invoke-virtual {v3, v4, v5}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    goto/16 :goto_0

    .line 396
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->val$banner:Lnet/gogame/gowrap/model/news/Banner;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/Banner;->getStartDateTime()Ljava/lang/Long;

    move-result-object v0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->val$banner:Lnet/gogame/gowrap/model/news/Banner;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/Banner;->getEndDateTime()Ljava/lang/Long;

    move-result-object v0

    if-nez v0, :cond_1

    .line 397
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$1300(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/TextView;

    move-result-object v0

    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    sget v4, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_news_banner_time_period_from_format:I

    new-array v1, v1, [Ljava/lang/Object;

    iget-object v5, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    .line 399
    invoke-static {v5}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$1200(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Ljava/text/DateFormat;

    move-result-object v5

    new-instance v6, Ljava/util/Date;

    iget-object v7, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->val$banner:Lnet/gogame/gowrap/model/news/Banner;

    invoke-virtual {v7}, Lnet/gogame/gowrap/model/news/Banner;->getStartDateTime()Ljava/lang/Long;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/Long;->longValue()J

    move-result-wide v7

    invoke-direct {v6, v7, v8}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v5, v6}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v5

    aput-object v5, v1, v2

    .line 397
    invoke-virtual {v3, v4, v1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    goto :goto_0

    .line 400
    :cond_1
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->val$banner:Lnet/gogame/gowrap/model/news/Banner;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/Banner;->getStartDateTime()Ljava/lang/Long;

    move-result-object v0

    if-nez v0, :cond_2

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->val$banner:Lnet/gogame/gowrap/model/news/Banner;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/Banner;->getEndDateTime()Ljava/lang/Long;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 401
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$1300(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/TextView;

    move-result-object v0

    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    sget v4, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_news_banner_time_period_until_format:I

    new-array v1, v1, [Ljava/lang/Object;

    iget-object v5, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    .line 403
    invoke-static {v5}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$1200(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Ljava/text/DateFormat;

    move-result-object v5

    new-instance v6, Ljava/util/Date;

    iget-object v7, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->val$banner:Lnet/gogame/gowrap/model/news/Banner;

    invoke-virtual {v7}, Lnet/gogame/gowrap/model/news/Banner;->getEndDateTime()Ljava/lang/Long;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/Long;->longValue()J

    move-result-wide v7

    invoke-direct {v6, v7, v8}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v5, v6}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v5

    aput-object v5, v1, v2

    .line 401
    invoke-virtual {v3, v4, v1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    goto :goto_0

    .line 405
    :cond_2
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$1300(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/TextView;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    :goto_0
    return-void
.end method
