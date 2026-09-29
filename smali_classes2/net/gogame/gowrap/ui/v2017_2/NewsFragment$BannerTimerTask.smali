.class Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$BannerTimerTask;
.super Ljava/util/TimerTask;
.source "NewsFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "BannerTimerTask"
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;


# direct methods
.method private constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V
    .locals 0

    .line 545
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$BannerTimerTask;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-direct {p0}, Ljava/util/TimerTask;-><init>()V

    return-void
.end method

.method synthetic constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$1;)V
    .locals 0

    .line 545
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$BannerTimerTask;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 549
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$BannerTimerTask;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$700(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V

    return-void
.end method
