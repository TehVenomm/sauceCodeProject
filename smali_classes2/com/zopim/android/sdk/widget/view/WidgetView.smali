.class public Lcom/zopim/android/sdk/widget/view/WidgetView;
.super Landroid/widget/RelativeLayout;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;
    }
.end annotation


# instance fields
.field private mPosition:I


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    invoke-direct {p0, p1}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;)V

    sget-object p1, Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;->UNKNOWN:Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;->getValue()I

    move-result p1

    iput p1, p0, Lcom/zopim/android/sdk/widget/view/WidgetView;->mPosition:I

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2

    invoke-direct {p0, p1, p2}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    sget-object v0, Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;->UNKNOWN:Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;->getValue()I

    move-result v0

    iput v0, p0, Lcom/zopim/android/sdk/widget/view/WidgetView;->mPosition:I

    invoke-virtual {p1}, Landroid/content/Context;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object p1

    sget-object v0, Lcom/zopim/android/sdk/R$styleable;->WidgetView:[I

    const/4 v1, 0x0

    invoke-virtual {p1, p2, v0, v1, v1}, Landroid/content/res/Resources$Theme;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object p1

    :try_start_0
    sget p2, Lcom/zopim/android/sdk/R$styleable;->WidgetView_anchor:I

    sget-object v0, Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;->UNKNOWN:Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;->getValue()I

    move-result v0

    invoke-virtual {p1, p2, v0}, Landroid/content/res/TypedArray;->getInteger(II)I

    move-result p2

    iput p2, p0, Lcom/zopim/android/sdk/widget/view/WidgetView;->mPosition:I
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    invoke-virtual {p1}, Landroid/content/res/TypedArray;->recycle()V

    return-void

    :catchall_0
    move-exception p2

    invoke-virtual {p1}, Landroid/content/res/TypedArray;->recycle()V

    throw p2
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 0

    invoke-direct {p0, p1, p2, p3}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    sget-object p1, Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;->UNKNOWN:Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;->getValue()I

    move-result p1

    iput p1, p0, Lcom/zopim/android/sdk/widget/view/WidgetView;->mPosition:I

    return-void
.end method


# virtual methods
.method public getAnchor()Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;
    .locals 1

    iget v0, p0, Lcom/zopim/android/sdk/widget/view/WidgetView;->mPosition:I

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;->getType(I)Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;

    move-result-object v0

    return-object v0
.end method
