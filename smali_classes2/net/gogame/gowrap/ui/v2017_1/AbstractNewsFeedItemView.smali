.class public abstract Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;
.super Landroid/widget/FrameLayout;
.source "AbstractNewsFeedItemView.java"


# static fields
.field public static final DEFAULT_TIMESTAMP_DATE_FORMAT:Ljava/lang/String; = "MMM d, yy h:mm a"


# instance fields
.field private aspectRatio:Ljava/lang/Double;

.field private availableWidth:Ljava/lang/Integer;

.field private contentBackgroundCount:I

.field private resizingView:Landroid/view/View;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1

    .line 32
    invoke-direct {p0, p1}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;)V

    const/4 v0, 0x1

    .line 26
    iput v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->contentBackgroundCount:I

    const/4 v0, 0x0

    .line 33
    invoke-direct {p0, p1, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->init(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 1

    .line 37
    invoke-direct {p0, p1, p2}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    const/4 v0, 0x1

    .line 26
    iput v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->contentBackgroundCount:I

    .line 38
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->init(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 0

    .line 42
    invoke-direct {p0, p1, p2, p3}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    const/4 p3, 0x1

    .line 26
    iput p3, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->contentBackgroundCount:I

    .line 43
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->init(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 0
    .annotation build Landroid/annotation/TargetApi;
        value = 0x15
    .end annotation

    .line 49
    invoke-direct {p0, p1, p2, p3, p4}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    const/4 p3, 0x1

    .line 26
    iput p3, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->contentBackgroundCount:I

    .line 50
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->init(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;)Ljava/lang/Integer;
    .locals 0

    .line 23
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->availableWidth:Ljava/lang/Integer;

    return-object p0
.end method

.method static synthetic access$002(Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;Ljava/lang/Integer;)Ljava/lang/Integer;
    .locals 0

    .line 23
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->availableWidth:Ljava/lang/Integer;

    return-object p1
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;)Landroid/view/View;
    .locals 0

    .line 23
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->resizingView:Landroid/view/View;

    return-object p0
.end method

.method static synthetic access$200(Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;)Ljava/lang/Double;
    .locals 0

    .line 23
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->aspectRatio:Ljava/lang/Double;

    return-object p0
.end method

.method private init(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2

    .line 70
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->getContext()Landroid/content/Context;

    move-result-object p2

    const-string v0, "layout_inflater"

    invoke-virtual {p2, v0}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Landroid/view/LayoutInflater;

    .line 72
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->getViewResourceId()I

    move-result v0

    const/4 v1, 0x0

    invoke-virtual {p2, v0, p0, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p2

    if-eqz p2, :cond_0

    .line 74
    invoke-virtual {p0, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->addView(Landroid/view/View;)V

    .line 77
    :cond_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    sget v0, Lnet/gogame/gowrap/R$integer;->net_gogame_gowrap_newsfeed_item_content_backgrounds:I

    invoke-virtual {p2, v0}, Landroid/content/res/Resources;->getInteger(I)I

    move-result p2

    iput p2, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->contentBackgroundCount:I

    .line 80
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->customInit(Landroid/content/Context;)V

    .line 82
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->getResizingViewResourceId()Ljava/lang/Integer;

    move-result-object p1

    if-eqz p1, :cond_1

    .line 83
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->getResizingViewResourceId()Ljava/lang/Integer;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->resizingView:Landroid/view/View;

    .line 84
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->resizingView:Landroid/view/View;

    if-eqz p1, :cond_1

    .line 85
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->resizingView:Landroid/view/View;

    invoke-virtual {p1}, Landroid/view/View;->getViewTreeObserver()Landroid/view/ViewTreeObserver;

    move-result-object p1

    new-instance p2, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView$1;

    invoke-direct {p2, p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView$1;-><init>(Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;)V

    invoke-virtual {p1, p2}, Landroid/view/ViewTreeObserver;->addOnGlobalLayoutListener(Landroid/view/ViewTreeObserver$OnGlobalLayoutListener;)V

    :cond_1
    return-void
.end method

.method private setOnClickListener([ILandroid/view/View$OnClickListener;)V
    .locals 3

    .line 155
    array-length v0, p1

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_1

    aget v2, p1, v1

    .line 156
    invoke-virtual {p0, v2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->findViewById(I)Landroid/view/View;

    move-result-object v2

    if-eqz v2, :cond_0

    .line 158
    invoke-virtual {v2, p2}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    return-void
.end method


# virtual methods
.method protected abstract customInit(Landroid/content/Context;)V
.end method

.method protected abstract getButtonClickResourceIds()[I
.end method

.method protected abstract getClickResourceIds()[I
.end method

.method public getResizingView()Landroid/view/View;
    .locals 1

    .line 193
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->resizingView:Landroid/view/View;

    return-object v0
.end method

.method protected abstract getResizingViewResourceId()Ljava/lang/Integer;
.end method

.method protected abstract getViewResourceId()I
.end method

.method protected isLayoutCompleted()Z
    .locals 1

    .line 66
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->availableWidth:Ljava/lang/Integer;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method protected abstract onLayoutCompleted()V
.end method

.method protected resizeView(Landroid/view/View;Ljava/lang/Integer;Ljava/lang/Double;)V
    .locals 3

    if-eqz p1, :cond_0

    if-eqz p2, :cond_0

    if-eqz p3, :cond_0

    .line 178
    invoke-virtual {p2}, Ljava/lang/Integer;->intValue()I

    move-result v0

    .line 179
    invoke-virtual {p2}, Ljava/lang/Integer;->doubleValue()D

    move-result-wide v1

    invoke-virtual {p3}, Ljava/lang/Double;->doubleValue()D

    move-result-wide p2

    div-double/2addr v1, p2

    double-to-int p2, v1

    .line 180
    invoke-virtual {p1}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object p3

    .line 181
    iput v0, p3, Landroid/view/ViewGroup$LayoutParams;->width:I

    .line 182
    iput p2, p3, Landroid/view/ViewGroup$LayoutParams;->height:I

    .line 183
    invoke-virtual {p1, v0}, Landroid/view/View;->setMinimumWidth(I)V

    .line 184
    invoke-virtual {p1, p2}, Landroid/view/View;->setMinimumHeight(I)V

    .line 185
    invoke-virtual {p1}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object p2

    instance-of p2, p2, Landroid/view/ViewGroup;

    if-eqz p2, :cond_0

    .line 186
    invoke-virtual {p1}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object p1

    check-cast p1, Landroid/view/ViewGroup;

    .line 187
    invoke-virtual {p1}, Landroid/view/ViewGroup;->requestLayout()V

    :cond_0
    return-void
.end method

.method public setAspectRatio(D)V
    .locals 2

    .line 172
    invoke-static {p1, p2}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->aspectRatio:Ljava/lang/Double;

    .line 173
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->resizingView:Landroid/view/View;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->availableWidth:Ljava/lang/Integer;

    invoke-static {p1, p2}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object p1

    invoke-virtual {p0, v0, v1, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->resizeView(Landroid/view/View;Ljava/lang/Integer;Ljava/lang/Double;)V

    return-void
.end method

.method public setButtonImage(Landroid/graphics/drawable/Drawable;)V
    .locals 1

    .line 109
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_newsfeed_item_button:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    .line 111
    invoke-virtual {v0, p1}, Landroid/widget/ImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method public setButtonOnClickListener(Landroid/view/View$OnClickListener;)V
    .locals 1

    .line 168
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->getButtonClickResourceIds()[I

    move-result-object v0

    invoke-direct {p0, v0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setOnClickListener([ILandroid/view/View$OnClickListener;)V

    return-void
.end method

.method public setMessage(Ljava/lang/String;)V
    .locals 1

    .line 144
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_newsfeed_item_message:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    if-eqz p1, :cond_0

    .line 147
    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    const/4 p1, 0x0

    .line 148
    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setVisibility(I)V

    goto :goto_0

    :cond_0
    const/16 p1, 0x8

    .line 150
    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setVisibility(I)V

    :goto_0
    return-void
.end method

.method public setOnClickListener(Landroid/view/View$OnClickListener;)V
    .locals 1

    .line 164
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->getClickResourceIds()[I

    move-result-object v0

    invoke-direct {p0, v0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setOnClickListener([ILandroid/view/View$OnClickListener;)V

    return-void
.end method

.method public setPosition(I)V
    .locals 2

    .line 103
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_newsfeed_item_content_background:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    .line 105
    iget v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->contentBackgroundCount:I

    rem-int/2addr p1, v1

    invoke-virtual {v0, p1}, Landroid/widget/ImageView;->setImageLevel(I)V

    return-void
.end method

.method public setTimestamp(Ljava/lang/Long;)V
    .locals 3

    if-nez p1, :cond_0

    const-string p1, ""

    .line 116
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setTimestamp(Ljava/lang/String;)V

    goto :goto_0

    .line 118
    :cond_0
    new-instance v0, Ljava/util/Date;

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-direct {v0, v1, v2}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setTimestamp(Ljava/util/Date;)V

    :goto_0
    return-void
.end method

.method public setTimestamp(Ljava/lang/String;)V
    .locals 1

    .line 133
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_newsfeed_item_timestamp:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    if-eqz p1, :cond_0

    .line 136
    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    const/4 p1, 0x0

    .line 137
    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setVisibility(I)V

    goto :goto_0

    :cond_0
    const/16 p1, 0x8

    .line 139
    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setVisibility(I)V

    :goto_0
    return-void
.end method

.method public setTimestamp(Ljava/util/Date;)V
    .locals 3

    if-nez p1, :cond_0

    const-string p1, ""

    .line 124
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setTimestamp(Ljava/lang/String;)V

    goto :goto_0

    .line 126
    :cond_0
    new-instance v0, Ljava/text/SimpleDateFormat;

    const-string v1, "MMM d, yy h:mm a"

    .line 127
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    .line 128
    invoke-virtual {v0, p1}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setTimestamp(Ljava/lang/String;)V

    :goto_0
    return-void
.end method
