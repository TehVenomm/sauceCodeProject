.class public Lcom/zopim/android/sdk/widget/ChatWidgetService;
.super Landroid/app/Service;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/widget/ChatWidgetService$a;,
        Lcom/zopim/android/sdk/widget/ChatWidgetService$b;,
        Lcom/zopim/android/sdk/widget/ChatWidgetService$LocalBinder;
    }
.end annotation


# static fields
.field private static final ANIMATION_FRAME_RATE:I = 0x1e

.field private static final DEFAULT_WIDGET_HEIGHT_DP:I = 0x32

.field private static final DEFAULT_WIDGET_WIDTH_DP:I = 0x32

.field private static final LOG_TAG:Ljava/lang/String; = "ChatWidgetService"

.field private static final WIDGET_INIT_DELAY:I = 0x96


# instance fields
.field mAgentsObserver:Lcom/zopim/android/sdk/data/observers/AgentsObserver;

.field private mAnimationHandler:Landroid/os/Handler;

.field private final mBinder:Landroid/os/IBinder;

.field mChannelLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

.field private mCrossfadeAnimator:Landroid/animation/AnimatorSet;

.field private mHorizontalMargin:I

.field private mInitialAgentMessageCount:I

.field private mOffsetX:D

.field private mOffsetY:D

.field private mRootLayoutParams:Landroid/view/WindowManager$LayoutParams;

.field private mTimer:Ljava/util/Timer;

.field private mTypingIndicatorView:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

.field private mUnreadCount:I

.field private mUnreadNotificationView:Landroid/widget/TextView;

.field private mVerticalMargin:I

.field private mWidgetAnimatorTask:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

.field private mWidgetBackground:Landroid/widget/ImageView;

.field private mWidgetView:Lcom/zopim/android/sdk/widget/view/WidgetView;

.field private mWindowManager:Landroid/view/WindowManager;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 2

    invoke-direct {p0}, Landroid/app/Service;-><init>()V

    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mAnimationHandler:Landroid/os/Handler;

    new-instance v0, Lcom/zopim/android/sdk/widget/ChatWidgetService$LocalBinder;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService$LocalBinder;-><init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mBinder:Landroid/os/IBinder;

    new-instance v0, Lcom/zopim/android/sdk/widget/c;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/widget/c;-><init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mAgentsObserver:Lcom/zopim/android/sdk/data/observers/AgentsObserver;

    new-instance v0, Lcom/zopim/android/sdk/widget/f;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/widget/f;-><init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mChannelLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

    return-void
.end method

.method static synthetic access$000(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mTypingIndicatorView:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    return-object p0
.end method

.method static synthetic access$1002(Lcom/zopim/android/sdk/widget/ChatWidgetService;D)D
    .locals 0

    iput-wide p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mOffsetX:D

    return-wide p1
.end method

.method static synthetic access$1102(Lcom/zopim/android/sdk/widget/ChatWidgetService;D)D
    .locals 0

    iput-wide p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mOffsetY:D

    return-wide p1
.end method

.method static synthetic access$1200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/os/Handler;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mAnimationHandler:Landroid/os/Handler;

    return-object p0
.end method

.method static synthetic access$1300(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/widget/TextView;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mUnreadNotificationView:Landroid/widget/TextView;

    return-object p0
.end method

.method static synthetic access$1400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/animation/AnimatorSet;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mCrossfadeAnimator:Landroid/animation/AnimatorSet;

    return-object p0
.end method

.method static synthetic access$1500(Lcom/zopim/android/sdk/widget/ChatWidgetService;)I
    .locals 0

    iget p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mUnreadCount:I

    return p0
.end method

.method static synthetic access$1502(Lcom/zopim/android/sdk/widget/ChatWidgetService;I)I
    .locals 0

    iput p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mUnreadCount:I

    return p1
.end method

.method static synthetic access$1600(Lcom/zopim/android/sdk/widget/ChatWidgetService;)I
    .locals 0

    iget p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mInitialAgentMessageCount:I

    return p0
.end method

.method static synthetic access$1700(Lcom/zopim/android/sdk/widget/ChatWidgetService;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->showUnreadNotification()V

    return-void
.end method

.method static synthetic access$200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/view/WidgetView;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetView:Lcom/zopim/android/sdk/widget/view/WidgetView;

    return-object p0
.end method

.method static synthetic access$300()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->LOG_TAG:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mRootLayoutParams:Landroid/view/WindowManager$LayoutParams;

    return-object p0
.end method

.method static synthetic access$500(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWindowManager:Landroid/view/WindowManager;

    return-object p0
.end method

.method static synthetic access$600(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/ChatWidgetService$a;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetAnimatorTask:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    return-object p0
.end method

.method static synthetic access$602(Lcom/zopim/android/sdk/widget/ChatWidgetService;Lcom/zopim/android/sdk/widget/ChatWidgetService$a;)Lcom/zopim/android/sdk/widget/ChatWidgetService$a;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetAnimatorTask:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    return-object p1
.end method

.method static synthetic access$700(Lcom/zopim/android/sdk/widget/ChatWidgetService;)I
    .locals 0

    iget p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mHorizontalMargin:I

    return p0
.end method

.method static synthetic access$800(Lcom/zopim/android/sdk/widget/ChatWidgetService;)I
    .locals 0

    iget p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mVerticalMargin:I

    return p0
.end method

.method static synthetic access$900(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Ljava/util/Timer;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mTimer:Ljava/util/Timer;

    return-object p0
.end method

.method static synthetic access$902(Lcom/zopim/android/sdk/widget/ChatWidgetService;Ljava/util/Timer;)Ljava/util/Timer;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mTimer:Ljava/util/Timer;

    return-object p1
.end method

.method private hasSystemAlertWindowPermission(Landroid/content/Context;)Z
    .locals 4
    .annotation build Landroid/annotation/TargetApi;
        value = 0x13
    .end annotation

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/4 v1, 0x0

    const/4 v2, 0x1

    const/16 v3, 0x13

    if-lt v0, v3, :cond_1

    const-string v0, "android.permission.SYSTEM_ALERT_WINDOW"

    invoke-virtual {p1, v0}, Landroid/content/Context;->checkCallingOrSelfPermission(Ljava/lang/String;)I

    move-result p1

    if-nez p1, :cond_0

    const/4 v1, 0x1

    :cond_0
    return v1

    :cond_1
    const-string v0, "android.permission.SYSTEM_ALERT_WINDOW"

    invoke-virtual {p1, v0}, Landroid/content/Context;->checkCallingOrSelfPermission(Ljava/lang/String;)I

    move-result p1

    if-nez p1, :cond_2

    const/4 v1, 0x1

    :cond_2
    return v1
.end method

.method private showUnreadNotification()V
    .locals 2
    .annotation build Landroid/annotation/TargetApi;
        value = 0xb
    .end annotation

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0xb

    if-lt v0, v1, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mCrossfadeAnimator:Landroid/animation/AnimatorSet;

    invoke-virtual {v0}, Landroid/animation/AnimatorSet;->start()V

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mTypingIndicatorView:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    const/4 v1, 0x4

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mUnreadNotificationView:Landroid/widget/TextView;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setVisibility(I)V

    :goto_0
    return-void
.end method


# virtual methods
.method public onBind(Landroid/content/Intent;)Landroid/os/IBinder;
    .locals 0

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mBinder:Landroid/os/IBinder;

    return-object p1
.end method

.method public onConfigurationChanged(Landroid/content/res/Configuration;)V
    .locals 8

    invoke-super {p0, p1}, Landroid/app/Service;->onConfigurationChanged(Landroid/content/res/Configuration;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    invoke-virtual {p1}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object p1

    iget p1, p1, Landroid/util/DisplayMetrics;->widthPixels:I

    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v0

    iget v0, v0, Landroid/util/DisplayMetrics;->heightPixels:I

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mRootLayoutParams:Landroid/view/WindowManager$LayoutParams;

    iget-wide v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mOffsetX:D

    const-wide/high16 v4, 0x4059000000000000L    # 100.0

    div-double/2addr v2, v4

    int-to-double v6, p1

    invoke-static {v6, v7}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v2, v2, v6

    double-to-int p1, v2

    iput p1, v1, Landroid/view/WindowManager$LayoutParams;->x:I

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mRootLayoutParams:Landroid/view/WindowManager$LayoutParams;

    iget-wide v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mOffsetY:D

    div-double/2addr v1, v4

    int-to-double v3, v0

    invoke-static {v3, v4}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v1, v1, v3

    double-to-int v0, v1

    iput v0, p1, Landroid/view/WindowManager$LayoutParams;->y:I

    new-instance p1, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mHorizontalMargin:I

    iget v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mVerticalMargin:I

    invoke-direct {p1, p0, v0, v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;-><init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;II)V

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetAnimatorTask:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    new-instance p1, Ljava/util/Timer;

    invoke-direct {p1}, Ljava/util/Timer;-><init>()V

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mTimer:Ljava/util/Timer;

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mTimer:Ljava/util/Timer;

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetAnimatorTask:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    const-wide/16 v2, 0x0

    const-wide/16 v4, 0x1e

    invoke-virtual/range {v0 .. v5}, Ljava/util/Timer;->schedule(Ljava/util/TimerTask;JJ)V

    return-void
.end method

.method public onCreate()V
    .locals 14
    .annotation build Landroid/annotation/TargetApi;
        value = 0xb
    .end annotation

    invoke-direct {p0, p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->hasSystemAlertWindowPermission(Landroid/content/Context;)Z

    move-result v0

    if-nez v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Not presenting chat widget. Missing permission SYSTEM_ALERT_WINDOW"

    invoke-static {v0, v1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->stopSelf()V

    return-void

    :cond_0
    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v0

    iget v0, v0, Landroid/util/DisplayMetrics;->heightPixels:I

    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v1

    iget v1, v1, Landroid/util/DisplayMetrics;->widthPixels:I

    :try_start_0
    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lcom/zopim/android/sdk/R$dimen;->widget_horizontal_margin:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v2

    iput v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mHorizontalMargin:I

    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lcom/zopim/android/sdk/R$dimen;->widget_vertical_margin:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v2

    iput v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mVerticalMargin:I
    :try_end_0
    .catch Landroid/content/res/Resources$NotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    sget-object v3, Lcom/zopim/android/sdk/widget/ChatWidgetService;->LOG_TAG:Ljava/lang/String;

    const-string v4, "Could not find margin resources. Will use zero margin"

    invoke-static {v3, v4, v2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    const/4 v2, 0x0

    iput v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mHorizontalMargin:I

    iput v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mVerticalMargin:I

    :goto_0
    const-string v2, "window"

    invoke-virtual {p0, v2}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/view/WindowManager;

    iput-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWindowManager:Landroid/view/WindowManager;

    invoke-static {p0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v2

    sget v3, Lcom/zopim/android/sdk/R$layout;->chat_widget:I

    const/4 v4, 0x0

    invoke-virtual {v2, v3, v4}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;)Landroid/view/View;

    move-result-object v2

    check-cast v2, Lcom/zopim/android/sdk/widget/view/WidgetView;

    iput-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetView:Lcom/zopim/android/sdk/widget/view/WidgetView;

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetView:Lcom/zopim/android/sdk/widget/view/WidgetView;

    sget v3, Lcom/zopim/android/sdk/R$id;->typing_indicator:I

    invoke-virtual {v2, v3}, Lcom/zopim/android/sdk/widget/view/WidgetView;->findViewById(I)Landroid/view/View;

    move-result-object v2

    check-cast v2, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    iput-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mTypingIndicatorView:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetView:Lcom/zopim/android/sdk/widget/view/WidgetView;

    sget v3, Lcom/zopim/android/sdk/R$id;->unread_notification:I

    invoke-virtual {v2, v3}, Lcom/zopim/android/sdk/widget/view/WidgetView;->findViewById(I)Landroid/view/View;

    move-result-object v2

    check-cast v2, Landroid/widget/TextView;

    iput-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mUnreadNotificationView:Landroid/widget/TextView;

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetView:Lcom/zopim/android/sdk/widget/view/WidgetView;

    sget v3, Lcom/zopim/android/sdk/R$id;->background:I

    invoke-virtual {v2, v3}, Lcom/zopim/android/sdk/widget/view/WidgetView;->findViewById(I)Landroid/view/View;

    move-result-object v2

    check-cast v2, Landroid/widget/ImageView;

    iput-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetBackground:Landroid/widget/ImageView;

    sget v2, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v3, 0xb

    if-lt v2, v3, :cond_1

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetBackground:Landroid/widget/ImageView;

    iget-object v3, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetBackground:Landroid/widget/ImageView;

    invoke-static {v2, v3}, Lcom/zopim/android/sdk/anim/AnimatorPack;->crossfade(Landroid/view/View;Landroid/view/View;)Landroid/animation/AnimatorSet;

    move-result-object v2

    iget-object v3, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mTypingIndicatorView:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    iget-object v5, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mUnreadNotificationView:Landroid/widget/TextView;

    invoke-static {v3, v5}, Lcom/zopim/android/sdk/anim/AnimatorPack;->crossfade(Landroid/view/View;Landroid/view/View;)Landroid/animation/AnimatorSet;

    move-result-object v3

    new-instance v5, Landroid/animation/AnimatorSet;

    invoke-direct {v5}, Landroid/animation/AnimatorSet;-><init>()V

    iput-object v5, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mCrossfadeAnimator:Landroid/animation/AnimatorSet;

    iget-object v5, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mCrossfadeAnimator:Landroid/animation/AnimatorSet;

    invoke-virtual {v5, v2}, Landroid/animation/AnimatorSet;->play(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    move-result-object v2

    invoke-virtual {v2, v3}, Landroid/animation/AnimatorSet$Builder;->with(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mCrossfadeAnimator:Landroid/animation/AnimatorSet;

    new-instance v3, Lcom/zopim/android/sdk/widget/a;

    invoke-direct {v3, p0}, Lcom/zopim/android/sdk/widget/a;-><init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;)V

    invoke-virtual {v2, v3}, Landroid/animation/AnimatorSet;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lcom/zopim/android/sdk/R$integer;->crossfade_duration:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getInteger(I)I

    move-result v2

    int-to-long v2, v2

    const-wide/16 v5, 0x0

    cmp-long v7, v2, v5

    if-lez v7, :cond_1

    iget-object v5, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mCrossfadeAnimator:Landroid/animation/AnimatorSet;

    invoke-virtual {v5, v2, v3}, Landroid/animation/AnimatorSet;->setDuration(J)Landroid/animation/AnimatorSet;

    :cond_1
    iget-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetView:Lcom/zopim/android/sdk/widget/view/WidgetView;

    new-instance v3, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;

    invoke-direct {v3, p0, v4}, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;-><init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;Lcom/zopim/android/sdk/widget/a;)V

    invoke-virtual {v2, v3}, Lcom/zopim/android/sdk/widget/view/WidgetView;->setOnTouchListener(Landroid/view/View$OnTouchListener;)V

    const/high16 v2, 0x42480000    # 50.0f

    invoke-static {p0, v2}, Lcom/zopim/android/sdk/util/Dimensions;->convertDpToPixel(Landroid/content/Context;F)I

    move-result v3

    invoke-static {p0, v2}, Lcom/zopim/android/sdk/util/Dimensions;->convertDpToPixel(Landroid/content/Context;F)I

    move-result v2

    :try_start_1
    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    sget v5, Lcom/zopim/android/sdk/R$dimen;->widget_width:I

    invoke-virtual {v4, v5}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v4
    :try_end_1
    .catch Landroid/content/res/Resources$NotFoundException; {:try_start_1 .. :try_end_1} :catch_2

    :try_start_2
    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    sget v5, Lcom/zopim/android/sdk/R$dimen;->widget_height:I

    invoke-virtual {v3, v5}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v3
    :try_end_2
    .catch Landroid/content/res/Resources$NotFoundException; {:try_start_2 .. :try_end_2} :catch_1

    move v9, v3

    goto :goto_2

    :catch_1
    move-exception v3

    goto :goto_1

    :catch_2
    move-exception v4

    move-object v13, v4

    move v4, v3

    move-object v3, v13

    :goto_1
    sget-object v5, Lcom/zopim/android/sdk/widget/ChatWidgetService;->LOG_TAG:Ljava/lang/String;

    const-string v6, "Could not find widget size resources. Will use default size."

    invoke-static {v5, v6, v3}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    move v9, v2

    :goto_2
    move v8, v4

    new-instance v2, Landroid/view/WindowManager$LayoutParams;

    const/16 v10, 0x7d2

    const/16 v11, 0x208

    const/4 v12, -0x3

    move-object v7, v2

    invoke-direct/range {v7 .. v12}, Landroid/view/WindowManager$LayoutParams;-><init>(IIIII)V

    iput-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mRootLayoutParams:Landroid/view/WindowManager$LayoutParams;

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mRootLayoutParams:Landroid/view/WindowManager$LayoutParams;

    const/16 v3, 0x33

    iput v3, v2, Landroid/view/WindowManager$LayoutParams;->gravity:I

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWindowManager:Landroid/view/WindowManager;

    iget-object v3, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetView:Lcom/zopim/android/sdk/widget/view/WidgetView;

    iget-object v4, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mRootLayoutParams:Landroid/view/WindowManager$LayoutParams;

    invoke-interface {v2, v3, v4}, Landroid/view/WindowManager;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetView:Lcom/zopim/android/sdk/widget/view/WidgetView;

    new-instance v3, Lcom/zopim/android/sdk/widget/b;

    invoke-direct {v3, p0, v1, v0}, Lcom/zopim/android/sdk/widget/b;-><init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;II)V

    const-wide/16 v0, 0x96

    invoke-virtual {v2, v3, v0, v1}, Lcom/zopim/android/sdk/widget/view/WidgetView;->postDelayed(Ljava/lang/Runnable;J)Z

    return-void
.end method

.method public onDestroy()V
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Destroying Widget UI"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetView:Lcom/zopim/android/sdk/widget/view/WidgetView;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWindowManager:Landroid/view/WindowManager;

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mWidgetView:Lcom/zopim/android/sdk/widget/view/WidgetView;

    invoke-interface {v0, v1}, Landroid/view/WindowManager;->removeView(Landroid/view/View;)V

    :cond_0
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mAgentsObserver:Lcom/zopim/android/sdk/data/observers/AgentsObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteAgentsObserver(Ljava/util/Observer;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mChannelLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteChatLogObserver(Ljava/util/Observer;)V

    return-void
.end method

.method public onStartCommand(Landroid/content/Intent;II)I
    .locals 2

    sget-object p2, Lcom/zopim/android/sdk/widget/ChatWidgetService;->LOG_TAG:Ljava/lang/String;

    const-string p3, "Starting Widget UI"

    invoke-static {p2, p3}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    const-string p2, "STOP_WIDGET_SERVICE"

    invoke-virtual {p1}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    const/4 p2, 0x2

    if-eqz p1, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->stopSelf()V

    return p2

    :cond_0
    const/4 p1, 0x0

    iput p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mUnreadCount:I

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    move-result-object p3

    const/4 v0, 0x1

    new-array v0, v0, [Lcom/zopim/android/sdk/model/ChatLog$Type;

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_AGENT:Lcom/zopim/android/sdk/model/ChatLog$Type;

    aput-object v1, v0, p1

    invoke-virtual {p3, v0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->countMessages([Lcom/zopim/android/sdk/model/ChatLog$Type;)I

    move-result p1

    iput p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mInitialAgentMessageCount:I

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object p1

    iget-object p3, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mAgentsObserver:Lcom/zopim/android/sdk/data/observers/AgentsObserver;

    invoke-interface {p1, p3}, Lcom/zopim/android/sdk/data/DataSource;->addAgentsObserver(Ljava/util/Observer;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object p1

    iget-object p3, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService;->mChannelLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

    invoke-interface {p1, p3}, Lcom/zopim/android/sdk/data/DataSource;->addChatLogObserver(Ljava/util/Observer;)V

    return p2
.end method
