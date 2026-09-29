.class public Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;
.super Landroid/widget/LinearLayout;


# static fields
.field private static final LOG_TAG:Ljava/lang/String; = "TypingIndicatorView"

.field private static final TYPING_INDICATOR_MAX_DELAY:J


# instance fields
.field private mAnimations:[Landroid/graphics/drawable/AnimationDrawable;

.field private mTransitionDelay:J


# direct methods
.method static constructor <clinit>()V
    .locals 3

    sget-object v0, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v1, 0x2

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v0

    sput-wide v0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->TYPING_INDICATOR_MAX_DELAY:J

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    invoke-direct {p0, p1}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    sget-wide v0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->TYPING_INDICATOR_MAX_DELAY:J

    iput-wide v0, p0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->mTransitionDelay:J

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 0

    invoke-direct {p0, p1, p2}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    sget-wide p1, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->TYPING_INDICATOR_MAX_DELAY:J

    iput-wide p1, p0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->mTransitionDelay:J

    return-void
.end method

.method private prepareAnimations()[Landroid/graphics/drawable/AnimationDrawable;
    .locals 11
    .annotation build Landroid/annotation/TargetApi;
        value = 0x10
    .end annotation

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->getChildCount()I

    move-result v0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$integer;->typing_dot_duration:I

    invoke-virtual {v1, v2}, Landroid/content/res/Resources;->getInteger(I)I

    move-result v1

    int-to-long v2, v1

    sget-wide v4, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->TYPING_INDICATOR_MAX_DELAY:J

    cmp-long v6, v2, v4

    if-lez v6, :cond_0

    sget-wide v2, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->TYPING_INDICATOR_MAX_DELAY:J

    :cond_0
    iput-wide v2, p0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->mTransitionDelay:J

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lcom/zopim/android/sdk/R$drawable;->ic_typing_dot_secondary:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v2

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    sget v4, Lcom/zopim/android/sdk/R$drawable;->ic_typing_dot_primary:I

    invoke-virtual {v3, v4}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v3

    new-array v4, v0, [Landroid/graphics/drawable/AnimationDrawable;

    const/4 v5, 0x0

    const/4 v6, 0x0

    :goto_0
    invoke-virtual {p0, v6}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->getChildAt(I)Landroid/view/View;

    move-result-object v7

    instance-of v7, v7, Landroid/widget/ImageView;

    if-eqz v7, :cond_2

    invoke-virtual {p0, v6}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->getChildAt(I)Landroid/view/View;

    move-result-object v7

    check-cast v7, Landroid/widget/ImageView;

    new-instance v8, Landroid/graphics/drawable/AnimationDrawable;

    invoke-direct {v8}, Landroid/graphics/drawable/AnimationDrawable;-><init>()V

    add-int/lit8 v9, v0, -0x1

    mul-int v9, v9, v1

    invoke-virtual {v8, v2, v9}, Landroid/graphics/drawable/AnimationDrawable;->addFrame(Landroid/graphics/drawable/Drawable;I)V

    invoke-virtual {v8, v3, v1}, Landroid/graphics/drawable/AnimationDrawable;->addFrame(Landroid/graphics/drawable/Drawable;I)V

    invoke-virtual {v8, v5}, Landroid/graphics/drawable/AnimationDrawable;->setOneShot(Z)V

    sget v9, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v10, 0x10

    if-ge v9, v10, :cond_1

    invoke-virtual {v7, v8}, Landroid/widget/ImageView;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    goto :goto_1

    :cond_1
    invoke-virtual {v7, v8}, Landroid/widget/ImageView;->setBackground(Landroid/graphics/drawable/Drawable;)V

    :goto_1
    add-int/lit8 v7, v6, 0x1

    aput-object v8, v4, v6

    move v6, v7

    goto :goto_0

    :cond_2
    return-object v4
.end method


# virtual methods
.method public start()V
    .locals 5

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->prepareAnimations()[Landroid/graphics/drawable/AnimationDrawable;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->mAnimations:[Landroid/graphics/drawable/AnimationDrawable;

    const-wide/16 v0, 0x0

    const/4 v2, 0x0

    :goto_0
    iget-object v3, p0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->mAnimations:[Landroid/graphics/drawable/AnimationDrawable;

    array-length v3, v3

    if-ge v2, v3, :cond_0

    iget-object v3, p0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->mAnimations:[Landroid/graphics/drawable/AnimationDrawable;

    aget-object v3, v3, v2

    new-instance v4, Lcom/zopim/android/sdk/chatlog/view/a;

    invoke-direct {v4, p0, v3}, Lcom/zopim/android/sdk/chatlog/view/a;-><init>(Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;Landroid/graphics/drawable/AnimationDrawable;)V

    invoke-virtual {p0, v4, v0, v1}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->postDelayed(Ljava/lang/Runnable;J)Z

    iget-wide v3, p0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->mTransitionDelay:J

    add-long/2addr v0, v3

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_0
    return-void
.end method

.method public stop()V
    .locals 3

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->mAnimations:[Landroid/graphics/drawable/AnimationDrawable;

    if-nez v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Animations are not initialized. Aborting stop."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    const/4 v0, 0x0

    const/4 v1, 0x0

    :goto_0
    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->mAnimations:[Landroid/graphics/drawable/AnimationDrawable;

    array-length v2, v2

    if-ge v1, v2, :cond_1

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->mAnimations:[Landroid/graphics/drawable/AnimationDrawable;

    aget-object v2, v2, v1

    invoke-virtual {v2, v0}, Landroid/graphics/drawable/AnimationDrawable;->selectDrawable(I)Z

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->mAnimations:[Landroid/graphics/drawable/AnimationDrawable;

    aget-object v2, v2, v1

    invoke-virtual {v2}, Landroid/graphics/drawable/AnimationDrawable;->stop()V

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    return-void
.end method
