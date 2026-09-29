.class public Lnet/gogame/gowrap/ui/layout/CustomGridLayout;
.super Landroid/view/ViewGroup;
.source "CustomGridLayout.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;
    }
.end annotation


# static fields
.field private static final DEFAULT_COLUMN_COUNT:I = 0x2

.field private static final DEFAULT_HORIZONTAL_SPACING:I

.field private static final DEFAULT_VERTICAL_SPACING:I


# instance fields
.field private columnCount:I

.field private horizontalSpacing:I

.field private final mTmpChildRect:Landroid/graphics/Rect;

.field private final mTmpContainerRect:Landroid/graphics/Rect;

.field private verticalSpacing:I


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 25
    invoke-direct {p0, p1}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;)V

    .line 18
    new-instance p1, Landroid/graphics/Rect;

    invoke-direct {p1}, Landroid/graphics/Rect;-><init>()V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpContainerRect:Landroid/graphics/Rect;

    .line 19
    new-instance p1, Landroid/graphics/Rect;

    invoke-direct {p1}, Landroid/graphics/Rect;-><init>()V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpChildRect:Landroid/graphics/Rect;

    const/4 p1, 0x2

    .line 20
    iput p1, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    const/4 p1, 0x0

    .line 21
    iput p1, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->horizontalSpacing:I

    .line 22
    iput p1, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->verticalSpacing:I

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 1

    const/4 v0, 0x0

    .line 29
    invoke-direct {p0, p1, p2, v0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 2

    .line 33
    invoke-direct {p0, p1, p2, p3}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 18
    new-instance p3, Landroid/graphics/Rect;

    invoke-direct {p3}, Landroid/graphics/Rect;-><init>()V

    iput-object p3, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpContainerRect:Landroid/graphics/Rect;

    .line 19
    new-instance p3, Landroid/graphics/Rect;

    invoke-direct {p3}, Landroid/graphics/Rect;-><init>()V

    iput-object p3, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpChildRect:Landroid/graphics/Rect;

    const/4 p3, 0x2

    .line 20
    iput p3, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    const/4 v0, 0x0

    .line 21
    iput v0, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->horizontalSpacing:I

    .line 22
    iput v0, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->verticalSpacing:I

    .line 35
    invoke-virtual {p1}, Landroid/content/Context;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object p1

    sget-object v1, Lnet/gogame/gowrap/ui/common/R$styleable;->CustomGridLayout:[I

    invoke-virtual {p1, p2, v1, v0, v0}, Landroid/content/res/Resources$Theme;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object p1

    .line 39
    :try_start_0
    sget p2, Lnet/gogame/gowrap/ui/common/R$styleable;->CustomGridLayout_column_count:I

    invoke-virtual {p1, p2, p3}, Landroid/content/res/TypedArray;->getInteger(II)I

    move-result p2

    iput p2, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    .line 41
    sget p2, Lnet/gogame/gowrap/ui/common/R$styleable;->CustomGridLayout_horizontal_spacing:I

    invoke-virtual {p1, p2, v0}, Landroid/content/res/TypedArray;->getDimensionPixelSize(II)I

    move-result p2

    iput p2, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->horizontalSpacing:I

    .line 43
    sget p2, Lnet/gogame/gowrap/ui/common/R$styleable;->CustomGridLayout_vertical_spacing:I

    invoke-virtual {p1, p2, v0}, Landroid/content/res/TypedArray;->getDimensionPixelSize(II)I

    move-result p2

    iput p2, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->verticalSpacing:I
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 46
    invoke-virtual {p1}, Landroid/content/res/TypedArray;->recycle()V

    return-void

    :catchall_0
    move-exception p2

    invoke-virtual {p1}, Landroid/content/res/TypedArray;->recycle()V

    .line 47
    throw p2
.end method

.method private getColumnLeft(I)I
    .locals 3

    .line 168
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getMeasuredWidth()I

    move-result v0

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getPaddingLeft()I

    move-result v1

    sub-int/2addr v0, v1

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getPaddingRight()I

    move-result v1

    sub-int/2addr v0, v1

    iget v1, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    add-int/lit8 v1, v1, -0x1

    iget v2, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->horizontalSpacing:I

    mul-int v1, v1, v2

    sub-int/2addr v0, v1

    .line 170
    iget v1, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    div-int/2addr v0, v1

    .line 171
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getPaddingLeft()I

    move-result v1

    iget v2, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->horizontalSpacing:I

    add-int/2addr v0, v2

    mul-int p1, p1, v0

    add-int/2addr v1, p1

    return v1
.end method

.method private getMode(I)Ljava/lang/String;
    .locals 1

    const/high16 v0, -0x80000000

    if-eq p1, v0, :cond_2

    if-eqz p1, :cond_1

    const/high16 v0, 0x40000000    # 2.0f

    if-eq p1, v0, :cond_0

    .line 77
    invoke-static {p1}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object p1

    return-object p1

    :cond_0
    const-string p1, "EXACTLY"

    return-object p1

    :cond_1
    const-string p1, "UNSPECIFIED"

    return-object p1

    :cond_2
    const-string p1, "AT_MOST"

    return-object p1
.end method

.method private resolve(III)I
    .locals 1

    const/4 v0, -0x1

    if-ne p1, v0, :cond_0

    return p3

    :cond_0
    const/4 p3, -0x2

    if-ne p1, p3, :cond_1

    return p2

    :cond_1
    return p1
.end method

.method private toMeasureSpecString(I)Ljava/lang/String;
    .locals 4

    const-string v0, "%s %s"

    const/4 v1, 0x2

    .line 64
    new-array v1, v1, [Ljava/lang/Object;

    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v2

    invoke-direct {p0, v2}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getMode(I)Ljava/lang/String;

    move-result-object v2

    const/4 v3, 0x0

    aput-object v2, v1, v3

    .line 65
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result p1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    const/4 v2, 0x1

    aput-object p1, v1, v2

    .line 64
    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method


# virtual methods
.method protected checkLayoutParams(Landroid/view/ViewGroup$LayoutParams;)Z
    .locals 0

    .line 243
    instance-of p1, p1, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;

    return p1
.end method

.method protected bridge synthetic generateDefaultLayoutParams()Landroid/view/ViewGroup$LayoutParams;
    .locals 1

    .line 14
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->generateDefaultLayoutParams()Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;

    move-result-object v0

    return-object v0
.end method

.method protected generateDefaultLayoutParams()Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;
    .locals 3

    .line 233
    new-instance v0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;

    const/4 v1, -0x1

    const/4 v2, -0x2

    invoke-direct {v0, v1, v2}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;-><init>(II)V

    return-object v0
.end method

.method public bridge synthetic generateLayoutParams(Landroid/util/AttributeSet;)Landroid/view/ViewGroup$LayoutParams;
    .locals 0

    .line 14
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->generateLayoutParams(Landroid/util/AttributeSet;)Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;

    move-result-object p1

    return-object p1
.end method

.method protected generateLayoutParams(Landroid/view/ViewGroup$LayoutParams;)Landroid/view/ViewGroup$LayoutParams;
    .locals 1

    .line 238
    new-instance v0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;

    invoke-direct {v0, p1}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;-><init>(Landroid/view/ViewGroup$LayoutParams;)V

    return-object v0
.end method

.method public generateLayoutParams(Landroid/util/AttributeSet;)Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;
    .locals 2

    .line 228
    new-instance v0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1, p1}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-object v0
.end method

.method public getColumnCount()I
    .locals 1

    .line 51
    iget v0, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    return v0
.end method

.method protected onLayout(ZIIII)V
    .locals 7

    .line 186
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getChildCount()I

    move-result p1

    .line 188
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getPaddingTop()I

    move-result p2

    const/4 p3, 0x0

    move p5, p2

    const/4 p2, 0x0

    const/4 p4, 0x0

    const/4 v0, 0x0

    :goto_0
    if-ge p2, p1, :cond_1

    .line 191
    invoke-virtual {p0, p2}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v1

    .line 192
    invoke-virtual {v1}, Landroid/view/View;->getVisibility()I

    move-result v2

    const/16 v3, 0x8

    if-eq v2, v3, :cond_0

    .line 193
    invoke-virtual {v1}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v2

    check-cast v2, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;

    .line 195
    iget-object v3, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpContainerRect:Landroid/graphics/Rect;

    invoke-direct {p0, p4}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getColumnLeft(I)I

    move-result v4

    iget v5, v2, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->leftMargin:I

    add-int/2addr v4, v5

    iput v4, v3, Landroid/graphics/Rect;->left:I

    .line 196
    iget-object v3, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpContainerRect:Landroid/graphics/Rect;

    add-int/lit8 p4, p4, 0x1

    invoke-direct {p0, p4}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getColumnLeft(I)I

    move-result v4

    iget v5, v2, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->rightMargin:I

    sub-int/2addr v4, v5

    iget v5, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->horizontalSpacing:I

    sub-int/2addr v4, v5

    iput v4, v3, Landroid/graphics/Rect;->right:I

    .line 198
    iget-object v3, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpContainerRect:Landroid/graphics/Rect;

    iget v4, v2, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->topMargin:I

    add-int/2addr v4, p5

    iput v4, v3, Landroid/graphics/Rect;->top:I

    .line 199
    iget-object v3, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpContainerRect:Landroid/graphics/Rect;

    iget-object v4, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpContainerRect:Landroid/graphics/Rect;

    iget v4, v4, Landroid/graphics/Rect;->top:I

    invoke-virtual {v1}, Landroid/view/View;->getMeasuredHeight()I

    move-result v5

    add-int/2addr v4, v5

    iput v4, v3, Landroid/graphics/Rect;->bottom:I

    .line 201
    iget v3, v2, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->width:I

    invoke-virtual {v1}, Landroid/view/View;->getMeasuredWidth()I

    move-result v4

    iget-object v5, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpContainerRect:Landroid/graphics/Rect;

    .line 202
    invoke-virtual {v5}, Landroid/graphics/Rect;->width()I

    move-result v5

    .line 201
    invoke-direct {p0, v3, v4, v5}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->resolve(III)I

    move-result v3

    .line 203
    iget v4, v2, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->height:I

    invoke-virtual {v1}, Landroid/view/View;->getMeasuredHeight()I

    move-result v5

    iget-object v6, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpContainerRect:Landroid/graphics/Rect;

    .line 204
    invoke-virtual {v6}, Landroid/graphics/Rect;->height()I

    move-result v6

    .line 203
    invoke-direct {p0, v4, v5, v6}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->resolve(III)I

    move-result v4

    .line 206
    iget v5, v2, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->topMargin:I

    add-int/2addr v5, v4

    iget v6, v2, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->bottomMargin:I

    add-int/2addr v5, v6

    invoke-static {v0, v5}, Ljava/lang/Math;->max(II)I

    move-result v0

    .line 209
    iget v2, v2, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->gravity:I

    iget-object v5, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpContainerRect:Landroid/graphics/Rect;

    iget-object v6, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpChildRect:Landroid/graphics/Rect;

    invoke-static {v2, v3, v4, v5, v6}, Landroid/view/Gravity;->apply(IIILandroid/graphics/Rect;Landroid/graphics/Rect;)V

    const/high16 v2, -0x80000000

    .line 211
    invoke-static {v3, v2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v3

    .line 212
    invoke-static {v4, v2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v2

    .line 211
    invoke-virtual {v1, v3, v2}, Landroid/view/View;->measure(II)V

    .line 213
    iget-object v2, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpChildRect:Landroid/graphics/Rect;

    iget v2, v2, Landroid/graphics/Rect;->left:I

    iget-object v3, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpChildRect:Landroid/graphics/Rect;

    iget v3, v3, Landroid/graphics/Rect;->top:I

    iget-object v4, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpChildRect:Landroid/graphics/Rect;

    iget v4, v4, Landroid/graphics/Rect;->right:I

    iget-object v5, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->mTmpChildRect:Landroid/graphics/Rect;

    iget v5, v5, Landroid/graphics/Rect;->bottom:I

    invoke-virtual {v1, v2, v3, v4, v5}, Landroid/view/View;->layout(IIII)V

    .line 217
    iget v1, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    if-lt p4, v1, :cond_0

    .line 219
    iget p4, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->verticalSpacing:I

    add-int/2addr v0, p4

    add-int/2addr p5, v0

    const/4 p4, 0x0

    const/4 v0, 0x0

    :cond_0
    add-int/lit8 p2, p2, 0x1

    goto/16 :goto_0

    :cond_1
    return-void
.end method

.method protected onMeasure(II)V
    .locals 19

    move-object/from16 v6, p0

    .line 83
    invoke-static/range {p1 .. p1}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v0

    .line 84
    invoke-static/range {p1 .. p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result v1

    .line 85
    invoke-static/range {p2 .. p2}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v2

    .line 86
    invoke-static/range {p2 .. p2}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result v3

    .line 89
    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getChildCount()I

    move-result v7

    const/4 v8, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    :goto_0
    const/16 v9, 0x8

    if-ge v4, v7, :cond_1

    .line 91
    invoke-virtual {v6, v4}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v10

    .line 92
    invoke-virtual {v10}, Landroid/view/View;->getVisibility()I

    move-result v10

    if-eq v10, v9, :cond_0

    add-int/lit8 v5, v5, 0x1

    :cond_0
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    .line 96
    :cond_1
    iget v4, v6, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    add-int/lit8 v4, v4, -0x1

    add-int/2addr v5, v4

    iget v4, v6, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    div-int/2addr v5, v4

    const/high16 v4, 0x40000000    # 2.0f

    const/high16 v10, -0x80000000

    if-eq v0, v10, :cond_2

    if-eq v0, v4, :cond_2

    .line 108
    invoke-static {v8, v8}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v0

    :goto_1
    move v11, v0

    goto :goto_2

    .line 102
    :cond_2
    iget v11, v6, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    add-int/lit8 v11, v11, -0x1

    iget v12, v6, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->horizontalSpacing:I

    mul-int v11, v11, v12

    sub-int/2addr v1, v11

    .line 103
    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getPaddingLeft()I

    move-result v11

    sub-int/2addr v1, v11

    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getPaddingRight()I

    move-result v11

    sub-int/2addr v1, v11

    iget v11, v6, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    div-int/2addr v1, v11

    .line 104
    invoke-static {v1, v0}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v0

    goto :goto_1

    :goto_2
    if-eq v2, v10, :cond_3

    if-eq v2, v4, :cond_3

    .line 122
    invoke-static {v8, v8}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v0

    :goto_3
    move v10, v0

    goto :goto_4

    :cond_3
    add-int/lit8 v0, v5, -0x1

    .line 116
    iget v1, v6, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->verticalSpacing:I

    mul-int v0, v0, v1

    sub-int/2addr v3, v0

    .line 117
    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getPaddingTop()I

    move-result v0

    sub-int/2addr v3, v0

    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getPaddingBottom()I

    move-result v0

    sub-int/2addr v3, v0

    div-int/2addr v3, v5

    .line 118
    invoke-static {v3, v2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v0

    goto :goto_3

    .line 131
    :goto_4
    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getPaddingTop()I

    move-result v0

    move v13, v0

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v12, 0x0

    const/4 v14, 0x0

    const/4 v15, 0x0

    const/16 v16, 0x0

    :goto_5
    if-ge v12, v7, :cond_6

    .line 134
    invoke-virtual {v6, v12}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v17

    .line 135
    invoke-virtual/range {v17 .. v17}, Landroid/view/View;->getVisibility()I

    move-result v0

    if-eq v0, v9, :cond_5

    const/4 v3, 0x0

    const/16 v18, 0x0

    move-object/from16 v0, p0

    move-object/from16 v1, v17

    move v2, v11

    move v8, v4

    move v4, v10

    move v9, v5

    move/from16 v5, v18

    .line 136
    invoke-virtual/range {v0 .. v5}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->measureChildWithMargins(Landroid/view/View;IIII)V

    .line 138
    invoke-virtual/range {v17 .. v17}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    check-cast v0, Landroid/view/ViewGroup$MarginLayoutParams;

    .line 139
    invoke-virtual/range {v17 .. v17}, Landroid/view/View;->getMeasuredWidth()I

    move-result v1

    iget v2, v0, Landroid/view/ViewGroup$MarginLayoutParams;->leftMargin:I

    add-int/2addr v1, v2

    iget v2, v0, Landroid/view/ViewGroup$MarginLayoutParams;->rightMargin:I

    add-int/2addr v1, v2

    invoke-static {v15, v1}, Ljava/lang/Math;->max(II)I

    move-result v1

    .line 141
    invoke-virtual/range {v17 .. v17}, Landroid/view/View;->getMeasuredHeight()I

    move-result v2

    iget v3, v0, Landroid/view/ViewGroup$MarginLayoutParams;->topMargin:I

    add-int/2addr v2, v3

    iget v3, v0, Landroid/view/ViewGroup$MarginLayoutParams;->bottomMargin:I

    add-int/2addr v2, v3

    invoke-static {v8, v2}, Ljava/lang/Math;->max(II)I

    move-result v2

    .line 143
    invoke-virtual/range {v17 .. v17}, Landroid/view/View;->getMeasuredState()I

    move-result v3

    invoke-static {v9, v3}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->combineMeasuredStates(II)I

    move-result v3

    .line 144
    iget v4, v0, Landroid/view/ViewGroup$MarginLayoutParams;->topMargin:I

    .line 145
    invoke-virtual/range {v17 .. v17}, Landroid/view/View;->getMeasuredHeight()I

    move-result v5

    add-int/2addr v4, v5

    iget v0, v0, Landroid/view/ViewGroup$MarginLayoutParams;->bottomMargin:I

    add-int/2addr v4, v0

    .line 144
    invoke-static {v14, v4}, Ljava/lang/Math;->max(II)I

    move-result v0

    add-int/lit8 v4, v16, 0x1

    .line 148
    iget v5, v6, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    if-lt v4, v5, :cond_4

    .line 150
    iget v4, v6, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->verticalSpacing:I

    add-int/2addr v0, v4

    add-int/2addr v13, v0

    move v15, v1

    move v4, v2

    move v5, v3

    const/4 v14, 0x0

    const/16 v16, 0x0

    goto :goto_6

    :cond_4
    move v14, v0

    move v15, v1

    move v5, v3

    move/from16 v16, v4

    move v4, v2

    goto :goto_6

    :cond_5
    move v8, v4

    move v9, v5

    :goto_6
    add-int/lit8 v12, v12, 0x1

    const/4 v8, 0x0

    const/16 v9, 0x8

    goto :goto_5

    :cond_6
    move v9, v5

    .line 155
    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getPaddingBottom()I

    move-result v0

    add-int/2addr v14, v0

    add-int/2addr v13, v14

    .line 156
    iget v0, v6, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    mul-int v15, v15, v0

    iget v0, v6, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->horizontalSpacing:I

    iget v1, v6, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    add-int/lit8 v1, v1, -0x1

    mul-int v0, v0, v1

    add-int/2addr v15, v0

    .line 157
    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getPaddingLeft()I

    move-result v0

    add-int/2addr v15, v0

    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getPaddingRight()I

    move-result v0

    add-int/2addr v15, v0

    .line 159
    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getSuggestedMinimumWidth()I

    move-result v0

    invoke-static {v15, v0}, Ljava/lang/Math;->max(II)I

    move-result v0

    .line 160
    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getSuggestedMinimumHeight()I

    move-result v1

    invoke-static {v13, v1}, Ljava/lang/Math;->max(II)I

    move-result v1

    move/from16 v2, p1

    .line 162
    invoke-static {v0, v2, v9}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->resolveSizeAndState(III)I

    move-result v0

    shl-int/lit8 v2, v9, 0x10

    move/from16 v3, p2

    .line 163
    invoke-static {v1, v3, v2}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->resolveSizeAndState(III)I

    move-result v1

    .line 162
    invoke-virtual {v6, v0, v1}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->setMeasuredDimension(II)V

    return-void
.end method

.method public setColumnCount(I)V
    .locals 0

    .line 55
    iput p1, p0, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->columnCount:I

    return-void
.end method

.method public shouldDelayChildPressedState()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method
