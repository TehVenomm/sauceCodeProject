.class public Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;
.super Landroid/view/ViewGroup;
.source "CustomTabbelPanel.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;,
        Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;,
        Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;,
        Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;
    }
.end annotation


# static fields
.field private static final DEFAULT_CHAMFER_SLOPE:F = 45.0f

.field private static final DEFAULT_CLOSE_BUTTON_MARGIN_BOTTOM:F = 2.0f

.field private static final DEFAULT_CLOSE_BUTTON_MARGIN_LEFT:F = 4.0f

.field private static final DEFAULT_CLOSE_BUTTON_MARGIN_RIGHT:F = 0.0f

.field private static final DEFAULT_CLOSE_BUTTON_MARGIN_TOP:F = 0.0f

.field private static final DEFAULT_CLOSE_BUTTON_PADDING_BOTTOM:F = 0.0f

.field private static final DEFAULT_CLOSE_BUTTON_PADDING_LEFT:F = 0.0f

.field private static final DEFAULT_CLOSE_BUTTON_PADDING_RIGHT:F = 0.0f

.field private static final DEFAULT_CLOSE_BUTTON_PADDING_TOP:F = 0.0f

.field private static final DEFAULT_EDGE_SLOPE_HEIGHT_RATIO:F = 0.5f

.field private static final DEFAULT_FILL_COLOR:I = -0x3fd9e8a8

.field private static final DEFAULT_FONT_FAMILY:Ljava/lang/String; = "sans-serif"

.field private static final DEFAULT_INNER_LINE_COLOR:I = -0xd06c60

.field private static final DEFAULT_INNER_LINE_WIDTH:F = 1.0f

.field private static final DEFAULT_OUTER_LINE_CHAMFER:I = 0x8

.field private static final DEFAULT_OUTER_LINE_COLOR:I = -0xb4280d

.field private static final DEFAULT_OUTER_LINE_WIDTH:F = 1.0f

.field private static final DEFAULT_PATH_OFFSET:F = 6.0f

.field private static final DEFAULT_SELECTED_TEXT_COLOR:I = -0x1

.field private static final DEFAULT_SELECTED_TEXT_STYLE:I = 0x1

.field private static final DEFAULT_TAB_FILL_COLOR:I = -0x3fd9e8a8

.field private static final DEFAULT_TAB_LABEL_MARGIN_BOTTOM:F = 16.0f

.field private static final DEFAULT_TAB_LABEL_MARGIN_LEFT:F = 8.0f

.field private static final DEFAULT_TAB_LABEL_MARGIN_RIGHT:F = 8.0f

.field private static final DEFAULT_TAB_LABEL_MARGIN_TOP:F = 16.0f

.field private static final DEFAULT_TAB_LABEL_PADDING_BOTTOM:F = 0.0f

.field private static final DEFAULT_TAB_LABEL_PADDING_LEFT:F = 0.0f

.field private static final DEFAULT_TAB_LABEL_PADDING_RIGHT:F = 0.0f

.field private static final DEFAULT_TAB_LABEL_PADDING_TOP:F = 0.0f

.field private static final DEFAULT_TAB_SLOPE:F = 58.0f

.field private static final DEFAULT_TEXT_COLOR:I = -0x1

.field private static final DEFAULT_TEXT_SIZE:F = 12.0f

.field private static final DEFAULT_TEXT_STYLE:I = 0x0

.field private static final TAG:Ljava/lang/String; = "UI-2017-2-DPRO"


# instance fields
.field private chamferSlope:F

.field private listener:Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;

.field private mBounds:Landroid/graphics/RectF;

.field private mClearPaint:Landroid/graphics/Paint;

.field private mCloseButtonBounds:Landroid/graphics/RectF;

.field private mCloseButtonDrawable:Landroid/graphics/drawable/Drawable;

.field private mCloseButtonMarginBottom:F

.field private mCloseButtonMarginLeft:F

.field private mCloseButtonMarginRight:F

.field private mCloseButtonMarginTop:F

.field private mCloseButtonMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

.field private mCloseButtonPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

.field private mCloseButtonPaddingBottom:F

.field private mCloseButtonPaddingLeft:F

.field private mCloseButtonPaddingRight:F

.field private mCloseButtonPaddingTop:F

.field private mContentBounds:Landroid/graphics/RectF;

.field private mEdgeSlopeHeightRatio:F

.field private mFillColor:I

.field private mFillPaint:Landroid/graphics/Paint;

.field private mFontFamily:Ljava/lang/String;

.field private mInnerLineColor:I

.field private mInnerLinePaint:Landroid/graphics/Paint;

.field private mInnerLinePathBottom:Landroid/graphics/Path;

.field private mInnerLineWidth:F

.field private mOuterChamfer:Landroid/graphics/PointF;

.field private mOuterLineChamfer:F

.field private mOuterLineColor:I

.field private mOuterLinePaint:Landroid/graphics/Paint;

.field private mOuterLinePathBottom:Landroid/graphics/Path;

.field private mOuterLineWidth:F

.field private mPathOffset:F

.field private mSelectedTabIndex:I

.field private mSelectedTabLabelPaint:Landroid/graphics/Paint;

.field private mSelectedTextColor:I

.field private mSelectedTextStyle:I

.field private mTabFillColor:I

.field private mTabFillPaint:Landroid/graphics/Paint;

.field private mTabLabelMarginBottom:F

.field private mTabLabelMarginLeft:F

.field private mTabLabelMarginRight:F

.field private mTabLabelMarginTop:F

.field private mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

.field private mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

.field private mTabLabelPaddingBottom:F

.field private mTabLabelPaddingLeft:F

.field private mTabLabelPaddingRight:F

.field private mTabLabelPaddingTop:F

.field private mTabLabelPaint:Landroid/graphics/Paint;

.field private mTabSlope:F

.field private mTextColor:I

.field private mTextSize:F

.field private mTextStyle:I

.field private final mTmpChildRect:Landroid/graphics/Rect;

.field private final mTmpContainerRect:Landroid/graphics/Rect;

.field private final mTmpTabLabelBounds:Landroid/graphics/Rect;

.field private final tabHolderList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;",
            ">;"
        }
    .end annotation
.end field

.field private final tabLabels:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 3

    .line 129
    invoke-direct {p0, p1}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;)V

    .line 69
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    .line 70
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    .line 71
    new-instance p1, Landroid/graphics/Rect;

    invoke-direct {p1}, Landroid/graphics/Rect;-><init>()V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpContainerRect:Landroid/graphics/Rect;

    .line 72
    new-instance p1, Landroid/graphics/Rect;

    invoke-direct {p1}, Landroid/graphics/Rect;-><init>()V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpChildRect:Landroid/graphics/Rect;

    .line 73
    new-instance p1, Landroid/graphics/Rect;

    invoke-direct {p1}, Landroid/graphics/Rect;-><init>()V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpTabLabelBounds:Landroid/graphics/Rect;

    const/high16 p1, 0x40c00000    # 6.0f

    .line 74
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    const/high16 p1, 0x42340000    # 45.0f

    .line 75
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->chamferSlope:F

    const/high16 p1, 0x42680000    # 58.0f

    .line 76
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabSlope:F

    const p1, -0x3fd9e8a8

    .line 77
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabFillColor:I

    .line 78
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFillColor:I

    const p1, -0xb4280d

    .line 79
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineColor:I

    const/high16 p1, 0x3f800000    # 1.0f

    .line 80
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineWidth:F

    const/high16 v0, 0x41000000    # 8.0f

    .line 81
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineChamfer:F

    const v1, -0xd06c60

    .line 82
    iput v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLineColor:I

    .line 83
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLineWidth:F

    const-string p1, "sans-serif"

    .line 84
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFontFamily:Ljava/lang/String;

    const/high16 p1, 0x41400000    # 12.0f

    .line 85
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextSize:F

    const/4 p1, 0x0

    .line 86
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextStyle:I

    const/4 v1, -0x1

    .line 87
    iput v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextColor:I

    const/4 v2, 0x1

    .line 88
    iput v2, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTextStyle:I

    .line 89
    iput v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTextColor:I

    .line 90
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginLeft:F

    const/high16 v1, 0x41800000    # 16.0f

    .line 91
    iput v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginTop:F

    .line 92
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginRight:F

    .line 93
    iput v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginBottom:F

    const/4 v0, 0x0

    .line 94
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingLeft:F

    .line 95
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingTop:F

    .line 96
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingRight:F

    .line 97
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingBottom:F

    const/high16 v1, 0x40800000    # 4.0f

    .line 98
    iput v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMarginLeft:F

    .line 99
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMarginTop:F

    .line 100
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMarginRight:F

    const/high16 v1, 0x40000000    # 2.0f

    .line 101
    iput v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMarginBottom:F

    .line 102
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingLeft:F

    .line 103
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingTop:F

    .line 104
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingRight:F

    .line 105
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingBottom:F

    const/high16 v0, 0x3f000000    # 0.5f

    .line 106
    iput v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mEdgeSlopeHeightRatio:F

    .line 108
    new-instance v0, Landroid/graphics/RectF;

    invoke-direct {v0}, Landroid/graphics/RectF;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mBounds:Landroid/graphics/RectF;

    .line 126
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabIndex:I

    .line 131
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->init()V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 16

    move-object/from16 v1, p0

    .line 135
    invoke-direct/range {p0 .. p2}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 69
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    .line 70
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    .line 71
    new-instance v0, Landroid/graphics/Rect;

    invoke-direct {v0}, Landroid/graphics/Rect;-><init>()V

    iput-object v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpContainerRect:Landroid/graphics/Rect;

    .line 72
    new-instance v0, Landroid/graphics/Rect;

    invoke-direct {v0}, Landroid/graphics/Rect;-><init>()V

    iput-object v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpChildRect:Landroid/graphics/Rect;

    .line 73
    new-instance v0, Landroid/graphics/Rect;

    invoke-direct {v0}, Landroid/graphics/Rect;-><init>()V

    iput-object v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpTabLabelBounds:Landroid/graphics/Rect;

    const/high16 v0, 0x40c00000    # 6.0f

    .line 74
    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    const/high16 v2, 0x42340000    # 45.0f

    .line 75
    iput v2, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->chamferSlope:F

    const/high16 v2, 0x42680000    # 58.0f

    .line 76
    iput v2, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabSlope:F

    const v3, -0x3fd9e8a8

    .line 77
    iput v3, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabFillColor:I

    .line 78
    iput v3, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFillColor:I

    const v4, -0xb4280d

    .line 79
    iput v4, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineColor:I

    const/high16 v5, 0x3f800000    # 1.0f

    .line 80
    iput v5, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineWidth:F

    const/high16 v6, 0x41000000    # 8.0f

    .line 81
    iput v6, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineChamfer:F

    const v7, -0xd06c60

    .line 82
    iput v7, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLineColor:I

    .line 83
    iput v5, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLineWidth:F

    const-string v8, "sans-serif"

    .line 84
    iput-object v8, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFontFamily:Ljava/lang/String;

    const/high16 v8, 0x41400000    # 12.0f

    .line 85
    iput v8, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextSize:F

    const/4 v9, 0x0

    .line 86
    iput v9, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextStyle:I

    const/4 v10, -0x1

    .line 87
    iput v10, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextColor:I

    const/4 v11, 0x1

    .line 88
    iput v11, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTextStyle:I

    .line 89
    iput v10, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTextColor:I

    .line 90
    iput v6, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginLeft:F

    const/high16 v12, 0x41800000    # 16.0f

    .line 91
    iput v12, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginTop:F

    .line 92
    iput v6, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginRight:F

    .line 93
    iput v12, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginBottom:F

    const/4 v13, 0x0

    .line 94
    iput v13, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingLeft:F

    .line 95
    iput v13, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingTop:F

    .line 96
    iput v13, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingRight:F

    .line 97
    iput v13, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingBottom:F

    const/high16 v14, 0x40800000    # 4.0f

    .line 98
    iput v14, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMarginLeft:F

    .line 99
    iput v13, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMarginTop:F

    .line 100
    iput v13, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMarginRight:F

    const/high16 v14, 0x40000000    # 2.0f

    .line 101
    iput v14, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMarginBottom:F

    .line 102
    iput v13, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingLeft:F

    .line 103
    iput v13, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingTop:F

    .line 104
    iput v13, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingRight:F

    .line 105
    iput v13, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingBottom:F

    const/high16 v14, 0x3f000000    # 0.5f

    .line 106
    iput v14, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mEdgeSlopeHeightRatio:F

    .line 108
    new-instance v14, Landroid/graphics/RectF;

    invoke-direct {v14}, Landroid/graphics/RectF;-><init>()V

    iput-object v14, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mBounds:Landroid/graphics/RectF;

    .line 126
    iput v9, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabIndex:I

    .line 137
    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object v14

    sget-object v15, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel:[I

    move-object/from16 v13, p2

    invoke-virtual {v14, v13, v15, v9, v9}, Landroid/content/res/Resources$Theme;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object v13

    .line 141
    :try_start_0
    sget v14, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_closeButton:I

    invoke-virtual {v13, v14}, Landroid/content/res/TypedArray;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v14

    iput-object v14, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonDrawable:Landroid/graphics/drawable/Drawable;

    .line 142
    sget v14, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_pathOffset:I

    .line 143
    invoke-direct {v1, v0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromDp(F)F

    move-result v0

    .line 142
    invoke-virtual {v13, v14, v0}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    .line 144
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_tabSlope:I

    invoke-virtual {v13, v0, v2}, Landroid/content/res/TypedArray;->getFloat(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabSlope:F

    .line 146
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_tabFillColor:I

    invoke-virtual {v13, v0, v3}, Landroid/content/res/TypedArray;->getColor(II)I

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabFillColor:I

    .line 148
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_fillColor:I

    invoke-virtual {v13, v0, v3}, Landroid/content/res/TypedArray;->getColor(II)I

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFillColor:I

    .line 150
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_outerLineColor:I

    invoke-virtual {v13, v0, v4}, Landroid/content/res/TypedArray;->getColor(II)I

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineColor:I

    .line 152
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_outerLineWidth:I

    .line 153
    invoke-direct {v1, v5}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromDp(F)F

    move-result v2

    .line 152
    invoke-virtual {v13, v0, v2}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineWidth:F

    .line 154
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_outerLineChamfer:I

    .line 155
    invoke-direct {v1, v6}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromDp(F)F

    move-result v2

    .line 154
    invoke-virtual {v13, v0, v2}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineChamfer:F

    .line 157
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_innerLineColor:I

    invoke-virtual {v13, v0, v7}, Landroid/content/res/TypedArray;->getColor(II)I

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLineColor:I

    .line 159
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_innerLineWidth:I

    .line 160
    invoke-direct {v1, v5}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromDp(F)F

    move-result v2

    .line 159
    invoke-virtual {v13, v0, v2}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLineWidth:F

    .line 162
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_fontFamily:I

    invoke-virtual {v13, v0}, Landroid/content/res/TypedArray;->getString(I)Ljava/lang/String;

    move-result-object v0

    iput-object v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFontFamily:Ljava/lang/String;

    .line 163
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_textSize:I

    .line 164
    invoke-direct {v1, v8}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromSp(F)F

    move-result v2

    .line 163
    invoke-virtual {v13, v0, v2}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextSize:F

    .line 165
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_textStyle:I

    invoke-virtual {v13, v0, v9}, Landroid/content/res/TypedArray;->getInteger(II)I

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextStyle:I

    .line 167
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_textColor:I

    invoke-virtual {v13, v0, v10}, Landroid/content/res/TypedArray;->getColor(II)I

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextColor:I

    .line 169
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_selectedTextStyle:I

    invoke-virtual {v13, v0, v11}, Landroid/content/res/TypedArray;->getInteger(II)I

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTextStyle:I

    .line 171
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_selectedTextColor:I

    invoke-virtual {v13, v0, v10}, Landroid/content/res/TypedArray;->getColor(II)I

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTextColor:I

    .line 174
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_tabLabelMarginLeft:I

    .line 175
    invoke-direct {v1, v6}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromDp(F)F

    move-result v2

    .line 174
    invoke-virtual {v13, v0, v2}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginLeft:F

    .line 176
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_tabLabelMarginTop:I

    .line 177
    invoke-direct {v1, v12}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromDp(F)F

    move-result v2

    .line 176
    invoke-virtual {v13, v0, v2}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginTop:F

    .line 178
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_tabLabelMarginRight:I

    .line 179
    invoke-direct {v1, v6}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromDp(F)F

    move-result v2

    .line 178
    invoke-virtual {v13, v0, v2}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginRight:F

    .line 180
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_tabLabelMarginBottom:I

    .line 181
    invoke-direct {v1, v12}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromDp(F)F

    move-result v2

    .line 180
    invoke-virtual {v13, v0, v2}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginBottom:F

    .line 182
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_tabLabelPaddingLeft:I

    const/4 v2, 0x0

    .line 183
    invoke-direct {v1, v2}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromDp(F)F

    move-result v3

    .line 182
    invoke-virtual {v13, v0, v3}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingLeft:F

    .line 184
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_tabLabelPaddingTop:I

    .line 185
    invoke-direct {v1, v2}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromDp(F)F

    move-result v3

    .line 184
    invoke-virtual {v13, v0, v3}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingTop:F

    .line 186
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_tabLabelPaddingRight:I

    .line 187
    invoke-direct {v1, v2}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromDp(F)F

    move-result v3

    .line 186
    invoke-virtual {v13, v0, v3}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingRight:F

    .line 188
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel_tabLabelPaddingBottom:I

    .line 189
    invoke-direct {v1, v2}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->fromDp(F)F

    move-result v2

    .line 188
    invoke-virtual {v13, v0, v2}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result v0

    iput v0, v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingBottom:F
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 191
    invoke-virtual {v13}, Landroid/content/res/TypedArray;->recycle()V

    .line 194
    invoke-direct/range {p0 .. p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->init()V

    return-void

    :catchall_0
    move-exception v0

    .line 191
    invoke-virtual {v13}, Landroid/content/res/TypedArray;->recycle()V

    .line 192
    throw v0
.end method

.method private adjust(Landroid/graphics/PointF;FF)Landroid/graphics/PointF;
    .locals 6

    const/4 v0, 0x0

    cmpl-float v0, p3, v0

    if-nez v0, :cond_0

    return-object p1

    :cond_0
    float-to-double v0, p2

    .line 730
    invoke-static {v0, v1}, Ljava/lang/Math;->toRadians(D)D

    move-result-wide v0

    .line 731
    iget p2, p1, Landroid/graphics/PointF;->x:F

    float-to-double v2, p2

    invoke-static {v0, v1}, Ljava/lang/Math;->cos(D)D

    move-result-wide v4

    float-to-double p2, p3

    invoke-static {p2, p3}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v4, v4, p2

    invoke-static {v2, v3}, Ljava/lang/Double;->isNaN(D)Z

    add-double/2addr v2, v4

    .line 732
    iget p1, p1, Landroid/graphics/PointF;->y:F

    float-to-double v4, p1

    invoke-static {v0, v1}, Ljava/lang/Math;->sin(D)D

    move-result-wide v0

    invoke-static {p2, p3}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v0, v0, p2

    invoke-static {v4, v5}, Ljava/lang/Double;->isNaN(D)Z

    add-double/2addr v4, v0

    .line 733
    new-instance p1, Landroid/graphics/PointF;

    double-to-float p2, v2

    double-to-float p3, v4

    invoke-direct {p1, p2, p3}, Landroid/graphics/PointF;-><init>(FF)V

    return-object p1
.end method

.method private varargs adjustMargins(Landroid/graphics/RectF;[Lnet/gogame/gowrap/ui/dpro/view/Margins;)Landroid/graphics/RectF;
    .locals 3

    .line 703
    array-length v0, p2

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_0

    aget-object v2, p2, v1

    .line 704
    invoke-direct {p0, p1, v2}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->doAdjustMargins(Landroid/graphics/RectF;Lnet/gogame/gowrap/ui/dpro/view/Margins;)Landroid/graphics/RectF;

    move-result-object p1

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    return-object p1
.end method

.method private calculateBounds(II)V
    .locals 3

    .line 487
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingLeft()I

    move-result v0

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingRight()I

    move-result v1

    add-int/2addr v0, v1

    int-to-float v0, v0

    .line 488
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingTop()I

    move-result v1

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingBottom()I

    move-result v2

    add-int/2addr v1, v2

    int-to-float v1, v1

    int-to-float p1, p1

    sub-float/2addr p1, v0

    int-to-float p2, p2

    sub-float/2addr p2, v1

    .line 493
    new-instance v0, Landroid/graphics/RectF;

    const/4 v1, 0x0

    invoke-direct {v0, v1, v1, p1, p2}, Landroid/graphics/RectF;-><init>(FFFF)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mBounds:Landroid/graphics/RectF;

    .line 494
    iget-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mBounds:Landroid/graphics/RectF;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingLeft()I

    move-result p2

    int-to-float p2, p2

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingTop()I

    move-result v0

    int-to-float v0, v0

    invoke-virtual {p1, p2, v0}, Landroid/graphics/RectF;->offsetTo(FF)V

    .line 496
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->calculatePaths()V

    return-void
.end method

.method private calculateMaxTabLabelHeight()F
    .locals 7

    .line 470
    new-instance v0, Landroid/graphics/Rect;

    invoke-direct {v0}, Landroid/graphics/Rect;-><init>()V

    const/4 v1, 0x0

    const/4 v2, 0x0

    const/4 v2, 0x0

    const/4 v3, 0x0

    .line 471
    :goto_0
    iget-object v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    invoke-interface {v4}, Ljava/util/List;->size()I

    move-result v4

    if-ge v2, v4, :cond_2

    .line 472
    iget-object v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    invoke-interface {v4, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    .line 473
    iget v5, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabIndex:I

    if-ne v2, v5, :cond_0

    .line 474
    iget-object v5, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabLabelPaint:Landroid/graphics/Paint;

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v6

    invoke-virtual {v5, v4, v1, v6, v0}, Landroid/graphics/Paint;->getTextBounds(Ljava/lang/String;IILandroid/graphics/Rect;)V

    goto :goto_1

    .line 476
    :cond_0
    iget-object v5, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaint:Landroid/graphics/Paint;

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v6

    invoke-virtual {v5, v4, v1, v6, v0}, Landroid/graphics/Paint;->getTextBounds(Ljava/lang/String;IILandroid/graphics/Rect;)V

    .line 478
    :goto_1
    invoke-virtual {v0}, Landroid/graphics/Rect;->height()I

    move-result v4

    int-to-float v4, v4

    cmpl-float v4, v4, v3

    if-lez v4, :cond_1

    .line 479
    invoke-virtual {v0}, Landroid/graphics/Rect;->height()I

    move-result v3

    int-to-float v3, v3

    :cond_1
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_2
    return v3
.end method

.method private calculatePaths()V
    .locals 20

    move-object/from16 v9, p0

    .line 500
    invoke-direct/range {p0 .. p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->calculateMaxTabLabelHeight()F

    move-result v0

    iget-object v1, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v1, v1, Lnet/gogame/gowrap/ui/dpro/view/Margins;->top:F

    add-float/2addr v0, v1

    iget-object v1, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v1, v1, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    add-float/2addr v0, v1

    iget-object v1, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v1, v1, Lnet/gogame/gowrap/ui/dpro/view/Margins;->top:F

    add-float/2addr v0, v1

    iget-object v1, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v1, v1, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    add-float/2addr v0, v1

    .line 503
    iget v1, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    add-float v10, v0, v1

    const/4 v11, 0x0

    const/4 v12, 0x0

    const/4 v13, 0x0

    const/4 v14, 0x0

    :goto_0
    const/4 v0, 0x2

    if-ge v13, v0, :cond_a

    .line 507
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->clear()V

    const/4 v0, 0x0

    move-object v8, v0

    const/4 v15, 0x0

    const/16 v16, 0x0

    .line 510
    :goto_1
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    const/4 v1, 0x1

    if-ge v15, v0, :cond_5

    .line 511
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    invoke-interface {v0, v15}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 512
    iget v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabIndex:I

    if-ne v15, v2, :cond_0

    .line 513
    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabLabelPaint:Landroid/graphics/Paint;

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v3

    iget-object v4, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpTabLabelBounds:Landroid/graphics/Rect;

    invoke-virtual {v2, v0, v12, v3, v4}, Landroid/graphics/Paint;->getTextBounds(Ljava/lang/String;IILandroid/graphics/Rect;)V

    goto :goto_2

    .line 516
    :cond_0
    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaint:Landroid/graphics/Paint;

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v3

    iget-object v4, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpTabLabelBounds:Landroid/graphics/Rect;

    invoke-virtual {v2, v0, v12, v3, v4}, Landroid/graphics/Paint;->getTextBounds(Ljava/lang/String;IILandroid/graphics/Rect;)V

    :goto_2
    cmpg-float v0, v14, v11

    if-gez v0, :cond_1

    .line 520
    iget v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabIndex:I

    if-eq v15, v0, :cond_2

    .line 521
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpTabLabelBounds:Landroid/graphics/Rect;

    iget v2, v0, Landroid/graphics/Rect;->right:I

    int-to-float v2, v2

    add-float/2addr v2, v14

    float-to-int v2, v2

    iput v2, v0, Landroid/graphics/Rect;->right:I

    goto :goto_3

    :cond_1
    cmpl-float v0, v14, v11

    if-lez v0, :cond_2

    .line 524
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpTabLabelBounds:Landroid/graphics/Rect;

    iget v2, v0, Landroid/graphics/Rect;->right:I

    int-to-float v2, v2

    add-float/2addr v2, v14

    float-to-int v2, v2

    iput v2, v0, Landroid/graphics/Rect;->right:I

    .line 526
    :cond_2
    :goto_3
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpTabLabelBounds:Landroid/graphics/Rect;

    invoke-virtual {v0}, Landroid/graphics/Rect;->width()I

    move-result v0

    int-to-float v0, v0

    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v2, v2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->left:F

    add-float/2addr v0, v2

    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v2, v2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->right:F

    add-float/2addr v0, v2

    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v2, v2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->left:F

    add-float/2addr v0, v2

    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v2, v2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->right:F

    add-float v17, v0, v2

    .line 528
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    sub-int/2addr v0, v1

    if-ne v15, v0, :cond_3

    const/16 v18, 0x1

    goto :goto_4

    :cond_3
    const/16 v18, 0x0

    .line 529
    :goto_4
    iget-object v1, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mBounds:Landroid/graphics/RectF;

    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterChamfer:Landroid/graphics/PointF;

    iget v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    add-float v5, v17, v0

    iget v6, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    const/4 v7, 0x0

    move-object/from16 v0, p0

    move v3, v10

    move/from16 v4, v16

    move-object v12, v8

    move/from16 v8, v18

    invoke-direct/range {v0 .. v8}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->newTopOutlineGeometry(Landroid/graphics/RectF;Landroid/graphics/PointF;FFFFFZ)Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;

    move-result-object v19

    .line 532
    iget-object v1, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mBounds:Landroid/graphics/RectF;

    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterChamfer:Landroid/graphics/PointF;

    iget v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    add-float v5, v17, v0

    iget v6, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    iget v7, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    move-object/from16 v0, p0

    invoke-direct/range {v0 .. v8}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->newTopOutlineGeometry(Landroid/graphics/RectF;Landroid/graphics/PointF;FFFFFZ)Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;

    move-result-object v0

    .line 535
    new-instance v7, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;

    invoke-virtual/range {v19 .. v19}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->getPath()Landroid/graphics/Path;

    move-result-object v2

    .line 536
    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->getPath()Landroid/graphics/Path;

    move-result-object v3

    invoke-virtual/range {v19 .. v19}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->getTabBounds()Landroid/graphics/RectF;

    move-result-object v4

    .line 537
    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->getLabelBounds()Landroid/graphics/RectF;

    move-result-object v5

    .line 538
    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->getContentTop()F

    move-result v6

    move-object v1, v7

    invoke-direct/range {v1 .. v6}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;-><init>(Landroid/graphics/Path;Landroid/graphics/Path;Landroid/graphics/RectF;Landroid/graphics/RectF;F)V

    .line 539
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    invoke-interface {v0, v7}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 540
    invoke-virtual {v7}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->getLabelBounds()Landroid/graphics/RectF;

    move-result-object v0

    iget v0, v0, Landroid/graphics/RectF;->right:F

    iget-object v1, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mBounds:Landroid/graphics/RectF;

    iget v1, v1, Landroid/graphics/RectF;->left:F

    sub-float v16, v0, v1

    if-nez v12, :cond_4

    .line 542
    invoke-virtual/range {v19 .. v19}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->getLabelBounds()Landroid/graphics/RectF;

    move-result-object v0

    move-object v8, v0

    goto :goto_5

    :cond_4
    move-object v8, v12

    :goto_5
    add-int/lit8 v15, v15, 0x1

    const/4 v12, 0x0

    goto/16 :goto_1

    :cond_5
    move-object v12, v8

    if-eqz v12, :cond_6

    .line 546
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonDrawable:Landroid/graphics/drawable/Drawable;

    if-eqz v0, :cond_6

    .line 547
    iget v0, v12, Landroid/graphics/RectF;->bottom:F

    iget v2, v12, Landroid/graphics/RectF;->top:F

    sub-float/2addr v0, v2

    .line 548
    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v2, v2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->top:F

    sub-float/2addr v0, v2

    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v2, v2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    sub-float/2addr v0, v2

    iget v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingTop:F

    sub-float/2addr v0, v2

    iget v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingBottom:F

    sub-float/2addr v0, v2

    .line 551
    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonDrawable:Landroid/graphics/drawable/Drawable;

    invoke-virtual {v2}, Landroid/graphics/drawable/Drawable;->getIntrinsicWidth()I

    move-result v2

    int-to-float v2, v2

    iget-object v3, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonDrawable:Landroid/graphics/drawable/Drawable;

    .line 552
    invoke-virtual {v3}, Landroid/graphics/drawable/Drawable;->getIntrinsicHeight()I

    move-result v3

    int-to-float v3, v3

    div-float/2addr v2, v3

    mul-float v0, v0, v2

    .line 554
    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v2, v2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->left:F

    add-float/2addr v0, v2

    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v2, v2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->right:F

    add-float/2addr v0, v2

    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v2, v2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->left:F

    add-float/2addr v0, v2

    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v2, v2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->right:F

    add-float/2addr v0, v2

    .line 557
    new-instance v2, Landroid/graphics/RectF;

    iget-object v3, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mBounds:Landroid/graphics/RectF;

    iget v3, v3, Landroid/graphics/RectF;->right:F

    sub-float/2addr v3, v0

    iget v0, v12, Landroid/graphics/RectF;->top:F

    iget-object v4, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mBounds:Landroid/graphics/RectF;

    iget v4, v4, Landroid/graphics/RectF;->right:F

    iget v5, v12, Landroid/graphics/RectF;->bottom:F

    invoke-direct {v2, v3, v0, v4, v5}, Landroid/graphics/RectF;-><init>(FFFF)V

    iput-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonBounds:Landroid/graphics/RectF;

    .line 561
    :cond_6
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    invoke-interface {v2}, Ljava/util/List;->size()I

    move-result v2

    sub-int/2addr v2, v1

    invoke-interface {v0, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->getTabBounds()Landroid/graphics/RectF;

    move-result-object v0

    .line 562
    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonBounds:Landroid/graphics/RectF;

    if-eqz v2, :cond_7

    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonBounds:Landroid/graphics/RectF;

    iget v2, v2, Landroid/graphics/RectF;->left:F

    goto :goto_6

    :cond_7
    iget-object v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mBounds:Landroid/graphics/RectF;

    iget v2, v2, Landroid/graphics/RectF;->right:F

    .line 564
    :goto_6
    iget v3, v0, Landroid/graphics/RectF;->right:F

    cmpl-float v3, v3, v2

    if-nez v3, :cond_8

    goto :goto_9

    .line 566
    :cond_8
    iget v3, v0, Landroid/graphics/RectF;->right:F

    cmpg-float v3, v3, v2

    if-gez v3, :cond_9

    .line 567
    iget v0, v0, Landroid/graphics/RectF;->right:F

    sub-float/2addr v2, v0

    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    .line 568
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    int-to-float v0, v0

    div-float/2addr v2, v0

    :goto_7
    move v14, v2

    goto :goto_8

    .line 570
    :cond_9
    iget v0, v0, Landroid/graphics/RectF;->right:F

    sub-float/2addr v2, v0

    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    .line 571
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    sub-int/2addr v0, v1

    int-to-float v0, v0

    div-float/2addr v2, v0

    goto :goto_7

    :goto_8
    add-int/lit8 v13, v13, 0x1

    const/4 v12, 0x0

    goto/16 :goto_0

    .line 575
    :cond_a
    :goto_9
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mBounds:Landroid/graphics/RectF;

    iget-object v1, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterChamfer:Landroid/graphics/PointF;

    invoke-direct {v9, v0, v1, v10, v11}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->newBottomOutlinePath(Landroid/graphics/RectF;Landroid/graphics/PointF;FF)Landroid/graphics/Path;

    move-result-object v0

    iput-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLinePathBottom:Landroid/graphics/Path;

    .line 576
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mBounds:Landroid/graphics/RectF;

    iget-object v1, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterChamfer:Landroid/graphics/PointF;

    iget v2, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    invoke-direct {v9, v0, v1, v10, v2}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->newBottomOutlinePath(Landroid/graphics/RectF;Landroid/graphics/PointF;FF)Landroid/graphics/Path;

    move-result-object v0

    iput-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLinePathBottom:Landroid/graphics/Path;

    .line 579
    new-instance v0, Landroid/graphics/RectF;

    invoke-direct {v0}, Landroid/graphics/RectF;-><init>()V

    iput-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mContentBounds:Landroid/graphics/RectF;

    .line 580
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLinePathBottom:Landroid/graphics/Path;

    iget-object v1, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mContentBounds:Landroid/graphics/RectF;

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/graphics/Path;->computeBounds(Landroid/graphics/RectF;Z)V

    .line 581
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_b

    .line 582
    iget-object v0, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mContentBounds:Landroid/graphics/RectF;

    iget-object v1, v9, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    invoke-interface {v1, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;

    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->getContentTop()F

    move-result v1

    iput v1, v0, Landroid/graphics/RectF;->top:F

    :cond_b
    return-void
.end method

.method private doAdjustMargins(Landroid/graphics/RectF;Lnet/gogame/gowrap/ui/dpro/view/Margins;)Landroid/graphics/RectF;
    .locals 5

    .line 710
    new-instance v0, Landroid/graphics/RectF;

    iget v1, p1, Landroid/graphics/RectF;->left:F

    iget v2, p2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->left:F

    add-float/2addr v1, v2

    iget v2, p1, Landroid/graphics/RectF;->top:F

    iget v3, p2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->top:F

    add-float/2addr v2, v3

    iget v3, p1, Landroid/graphics/RectF;->right:F

    iget v4, p2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->right:F

    sub-float/2addr v3, v4

    iget p1, p1, Landroid/graphics/RectF;->bottom:F

    iget p2, p2, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    sub-float/2addr p1, p2

    invoke-direct {v0, v1, v2, v3, p1}, Landroid/graphics/RectF;-><init>(FFFF)V

    return-object v0
.end method

.method private drawTab(Landroid/graphics/Canvas;I)V
    .locals 7

    .line 669
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    invoke-interface {v0, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 670
    iget-object v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    invoke-interface {v1, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;

    .line 671
    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->getOuterPath()Landroid/graphics/Path;

    move-result-object v2

    iget-object v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mClearPaint:Landroid/graphics/Paint;

    invoke-virtual {p1, v2, v3}, Landroid/graphics/Canvas;->drawPath(Landroid/graphics/Path;Landroid/graphics/Paint;)V

    .line 672
    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->getOuterPath()Landroid/graphics/Path;

    move-result-object v2

    iget-object v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabFillPaint:Landroid/graphics/Paint;

    invoke-virtual {p1, v2, v3}, Landroid/graphics/Canvas;->drawPath(Landroid/graphics/Path;Landroid/graphics/Paint;)V

    .line 673
    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->getOuterPath()Landroid/graphics/Path;

    move-result-object v2

    iget-object v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLinePaint:Landroid/graphics/Paint;

    invoke-virtual {p1, v2, v3}, Landroid/graphics/Canvas;->drawPath(Landroid/graphics/Path;Landroid/graphics/Paint;)V

    .line 674
    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->getInnerPath()Landroid/graphics/Path;

    move-result-object v2

    iget-object v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLinePaint:Landroid/graphics/Paint;

    invoke-virtual {p1, v2, v3}, Landroid/graphics/Canvas;->drawPath(Landroid/graphics/Path;Landroid/graphics/Paint;)V

    .line 676
    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->getLabelBounds()Landroid/graphics/RectF;

    move-result-object v1

    .line 678
    iget v2, v1, Landroid/graphics/RectF;->left:F

    iget-object v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v3, v3, Lnet/gogame/gowrap/ui/dpro/view/Margins;->left:F

    add-float/2addr v2, v3

    iget-object v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v3, v3, Lnet/gogame/gowrap/ui/dpro/view/Margins;->left:F

    add-float/2addr v2, v3

    .line 679
    iget v3, v1, Landroid/graphics/RectF;->bottom:F

    iget-object v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v4, v4, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    sub-float/2addr v3, v4

    iget-object v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v4, v4, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    sub-float/2addr v3, v4

    .line 681
    invoke-virtual {v1}, Landroid/graphics/RectF;->width()F

    move-result v1

    iget-object v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v4, v4, Lnet/gogame/gowrap/ui/dpro/view/Margins;->left:F

    sub-float/2addr v1, v4

    iget-object v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v4, v4, Lnet/gogame/gowrap/ui/dpro/view/Margins;->right:F

    sub-float/2addr v1, v4

    iget-object v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v4, v4, Lnet/gogame/gowrap/ui/dpro/view/Margins;->left:F

    sub-float/2addr v1, v4

    iget-object v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v4, v4, Lnet/gogame/gowrap/ui/dpro/view/Margins;->right:F

    sub-float/2addr v1, v4

    .line 685
    iget v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabIndex:I

    if-eq v4, p2, :cond_0

    .line 686
    new-instance v4, Landroid/text/TextPaint;

    iget-object v5, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaint:Landroid/graphics/Paint;

    invoke-direct {v4, v5}, Landroid/text/TextPaint;-><init>(Landroid/graphics/Paint;)V

    sget-object v5, Landroid/text/TextUtils$TruncateAt;->END:Landroid/text/TextUtils$TruncateAt;

    invoke-static {v0, v4, v1, v5}, Landroid/text/TextUtils;->ellipsize(Ljava/lang/CharSequence;Landroid/text/TextPaint;FLandroid/text/TextUtils$TruncateAt;)Ljava/lang/CharSequence;

    move-result-object v0

    .line 688
    invoke-interface {v0}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object v0

    .line 693
    :cond_0
    iget v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabIndex:I

    if-ne p2, v4, :cond_1

    iget-object p2, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabLabelPaint:Landroid/graphics/Paint;

    goto :goto_0

    :cond_1
    iget-object p2, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaint:Landroid/graphics/Paint;

    :goto_0
    const/4 v4, 0x0

    .line 695
    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v5

    iget-object v6, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpTabLabelBounds:Landroid/graphics/Rect;

    invoke-virtual {p2, v0, v4, v5, v6}, Landroid/graphics/Paint;->getTextBounds(Ljava/lang/String;IILandroid/graphics/Rect;)V

    .line 697
    iget-object v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpTabLabelBounds:Landroid/graphics/Rect;

    invoke-virtual {v4}, Landroid/graphics/Rect;->width()I

    move-result v4

    int-to-float v4, v4

    sub-float/2addr v1, v4

    const/high16 v4, 0x40000000    # 2.0f

    div-float/2addr v1, v4

    add-float/2addr v2, v1

    .line 698
    invoke-virtual {p1, v0, v2, v3, p2}, Landroid/graphics/Canvas;->drawText(Ljava/lang/String;FFLandroid/graphics/Paint;)V

    return-void
.end method

.method private fromDp(F)F
    .locals 1

    .line 737
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0, p1}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result p1

    int-to-float p1, p1

    return p1
.end method

.method private fromSp(F)F
    .locals 1

    .line 741
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0, p1}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->pxFromSp(Landroid/content/Context;F)I

    move-result p1

    int-to-float p1, p1

    return p1
.end method

.method private getComponentId(Landroid/view/MotionEvent;)Ljava/lang/Integer;
    .locals 4

    .line 450
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonBounds:Landroid/graphics/RectF;

    if-eqz v0, :cond_0

    .line 451
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonBounds:Landroid/graphics/RectF;

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v1

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result v2

    invoke-virtual {v0, v1, v2}, Landroid/graphics/RectF;->contains(FF)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 p1, -0x1

    .line 452
    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    return-object p1

    .line 455
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    if-eqz v0, :cond_2

    const/4 v0, 0x0

    .line 456
    :goto_0
    iget-object v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    if-ge v0, v1, :cond_2

    .line 457
    iget-object v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    invoke-interface {v1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;

    if-eqz v1, :cond_1

    .line 458
    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->getLabelBounds()Landroid/graphics/RectF;

    move-result-object v2

    if-eqz v2, :cond_1

    .line 459
    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->getLabelBounds()Landroid/graphics/RectF;

    move-result-object v1

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v2

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result v3

    invoke-virtual {v1, v2, v3}, Landroid/graphics/RectF;->contains(FF)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 460
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    return-object p1

    :cond_1
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_2
    const/4 p1, 0x0

    return-object p1
.end method

.method private init()V
    .locals 5

    const/4 v0, 0x0

    .line 393
    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->setWillNotDraw(Z)V

    .line 395
    new-instance v1, Landroid/graphics/Paint;

    const/4 v2, 0x1

    invoke-direct {v1, v2}, Landroid/graphics/Paint;-><init>(I)V

    iput-object v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mClearPaint:Landroid/graphics/Paint;

    .line 396
    iget-object v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mClearPaint:Landroid/graphics/Paint;

    invoke-virtual {v1, v0}, Landroid/graphics/Paint;->setColor(I)V

    .line 397
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mClearPaint:Landroid/graphics/Paint;

    new-instance v1, Landroid/graphics/PorterDuffXfermode;

    sget-object v3, Landroid/graphics/PorterDuff$Mode;->CLEAR:Landroid/graphics/PorterDuff$Mode;

    invoke-direct {v1, v3}, Landroid/graphics/PorterDuffXfermode;-><init>(Landroid/graphics/PorterDuff$Mode;)V

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setXfermode(Landroid/graphics/Xfermode;)Landroid/graphics/Xfermode;

    .line 398
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mClearPaint:Landroid/graphics/Paint;

    invoke-virtual {v0, v2}, Landroid/graphics/Paint;->setAntiAlias(Z)V

    .line 400
    new-instance v0, Landroid/graphics/Paint;

    invoke-direct {v0, v2}, Landroid/graphics/Paint;-><init>(I)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLinePaint:Landroid/graphics/Paint;

    .line 401
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLinePaint:Landroid/graphics/Paint;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineColor:I

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setColor(I)V

    .line 402
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLinePaint:Landroid/graphics/Paint;

    sget-object v1, Landroid/graphics/Paint$Style;->STROKE:Landroid/graphics/Paint$Style;

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setStyle(Landroid/graphics/Paint$Style;)V

    .line 403
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLinePaint:Landroid/graphics/Paint;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineWidth:F

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setStrokeWidth(F)V

    .line 405
    new-instance v0, Landroid/graphics/Paint;

    invoke-direct {v0, v2}, Landroid/graphics/Paint;-><init>(I)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabFillPaint:Landroid/graphics/Paint;

    .line 406
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabFillPaint:Landroid/graphics/Paint;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabFillColor:I

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setColor(I)V

    .line 407
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabFillPaint:Landroid/graphics/Paint;

    sget-object v1, Landroid/graphics/Paint$Style;->FILL:Landroid/graphics/Paint$Style;

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setStyle(Landroid/graphics/Paint$Style;)V

    .line 409
    new-instance v0, Landroid/graphics/Paint;

    invoke-direct {v0, v2}, Landroid/graphics/Paint;-><init>(I)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFillPaint:Landroid/graphics/Paint;

    .line 410
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFillPaint:Landroid/graphics/Paint;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFillColor:I

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setColor(I)V

    .line 411
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFillPaint:Landroid/graphics/Paint;

    sget-object v1, Landroid/graphics/Paint$Style;->FILL:Landroid/graphics/Paint$Style;

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setStyle(Landroid/graphics/Paint$Style;)V

    .line 413
    new-instance v0, Landroid/graphics/Paint;

    invoke-direct {v0, v2}, Landroid/graphics/Paint;-><init>(I)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLinePaint:Landroid/graphics/Paint;

    .line 414
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLinePaint:Landroid/graphics/Paint;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLineColor:I

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setColor(I)V

    .line 415
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLinePaint:Landroid/graphics/Paint;

    sget-object v1, Landroid/graphics/Paint$Style;->STROKE:Landroid/graphics/Paint$Style;

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setStyle(Landroid/graphics/Paint$Style;)V

    .line 416
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLinePaint:Landroid/graphics/Paint;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLineWidth:F

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setStrokeWidth(F)V

    .line 418
    new-instance v0, Landroid/graphics/PointF;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineChamfer:F

    iget v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLineChamfer:F

    invoke-direct {v0, v1, v3}, Landroid/graphics/PointF;-><init>(FF)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterChamfer:Landroid/graphics/PointF;

    .line 420
    new-instance v0, Landroid/graphics/Paint;

    invoke-direct {v0, v2}, Landroid/graphics/Paint;-><init>(I)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaint:Landroid/graphics/Paint;

    .line 421
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaint:Landroid/graphics/Paint;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFontFamily:Ljava/lang/String;

    iget v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextStyle:I

    invoke-static {v1, v3}, Landroid/graphics/Typeface;->create(Ljava/lang/String;I)Landroid/graphics/Typeface;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setTypeface(Landroid/graphics/Typeface;)Landroid/graphics/Typeface;

    .line 422
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaint:Landroid/graphics/Paint;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextSize:F

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setTextSize(F)V

    .line 423
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaint:Landroid/graphics/Paint;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextColor:I

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setColor(I)V

    .line 425
    new-instance v0, Landroid/graphics/Paint;

    invoke-direct {v0, v2}, Landroid/graphics/Paint;-><init>(I)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabLabelPaint:Landroid/graphics/Paint;

    .line 426
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabLabelPaint:Landroid/graphics/Paint;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFontFamily:Ljava/lang/String;

    iget v2, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTextStyle:I

    invoke-static {v1, v2}, Landroid/graphics/Typeface;->create(Ljava/lang/String;I)Landroid/graphics/Typeface;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setTypeface(Landroid/graphics/Typeface;)Landroid/graphics/Typeface;

    .line 427
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabLabelPaint:Landroid/graphics/Paint;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTextSize:F

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setTextSize(F)V

    .line 428
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabLabelPaint:Landroid/graphics/Paint;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTextColor:I

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setColor(I)V

    .line 430
    new-instance v0, Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginLeft:F

    iget v2, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginTop:F

    iget v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginRight:F

    iget v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMarginBottom:F

    invoke-direct {v0, v1, v2, v3, v4}, Lnet/gogame/gowrap/ui/dpro/view/Margins;-><init>(FFFF)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    .line 432
    new-instance v0, Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingLeft:F

    iget v2, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingTop:F

    iget v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingRight:F

    iget v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPaddingBottom:F

    invoke-direct {v0, v1, v2, v3, v4}, Lnet/gogame/gowrap/ui/dpro/view/Margins;-><init>(FFFF)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    .line 435
    new-instance v0, Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMarginLeft:F

    iget v2, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMarginTop:F

    iget v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMarginRight:F

    iget v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMarginBottom:F

    invoke-direct {v0, v1, v2, v3, v4}, Lnet/gogame/gowrap/ui/dpro/view/Margins;-><init>(FFFF)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    .line 437
    new-instance v0, Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingLeft:F

    iget v2, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingTop:F

    iget v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingRight:F

    iget v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPaddingBottom:F

    invoke-direct {v0, v1, v2, v3, v4}, Lnet/gogame/gowrap/ui/dpro/view/Margins;-><init>(FFFF)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    .line 440
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->isInEditMode()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 441
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    const-string v1, "News"

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 442
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    const-string v1, "Help"

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 444
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    const-string v1, "News"

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 445
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabLabels:Ljava/util/List;

    const-string v1, "Help"

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :goto_0
    return-void
.end method

.method private lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;
    .locals 0

    .line 721
    invoke-direct {p0, p2, p3, p4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->adjust(Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    move-result-object p2

    .line 722
    iget p3, p2, Landroid/graphics/PointF;->x:F

    iget p4, p2, Landroid/graphics/PointF;->y:F

    invoke-virtual {p1, p3, p4}, Landroid/graphics/Path;->lineTo(FF)V

    return-object p2
.end method

.method private moveTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;
    .locals 0

    .line 715
    invoke-direct {p0, p2, p3, p4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->adjust(Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    move-result-object p2

    .line 716
    iget p3, p2, Landroid/graphics/PointF;->x:F

    iget p4, p2, Landroid/graphics/PointF;->y:F

    invoke-virtual {p1, p3, p4}, Landroid/graphics/Path;->moveTo(FF)V

    return-object p2
.end method

.method private newBottomOutlinePath(Landroid/graphics/RectF;Landroid/graphics/PointF;FF)Landroid/graphics/Path;
    .locals 11

    .line 646
    iget v0, p1, Landroid/graphics/RectF;->left:F

    .line 647
    iget v1, p2, Landroid/graphics/PointF;->x:F

    add-float/2addr v1, v0

    .line 648
    iget v2, p1, Landroid/graphics/RectF;->right:F

    .line 649
    iget v3, p2, Landroid/graphics/PointF;->x:F

    sub-float v3, v2, v3

    .line 651
    iget v4, p1, Landroid/graphics/RectF;->top:F

    add-float/2addr v4, p3

    .line 652
    iget p1, p1, Landroid/graphics/RectF;->bottom:F

    .line 653
    iget p2, p2, Landroid/graphics/PointF;->y:F

    sub-float p2, p1, p2

    .line 655
    new-instance p3, Landroid/graphics/Path;

    invoke-direct {p3}, Landroid/graphics/Path;-><init>()V

    .line 656
    new-instance v5, Landroid/graphics/PointF;

    invoke-direct {v5, v2, v4}, Landroid/graphics/PointF;-><init>(FF)V

    const/high16 v6, 0x43340000    # 180.0f

    invoke-direct {p0, p3, v5, v6, p4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->moveTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 657
    new-instance v5, Landroid/graphics/PointF;

    invoke-direct {v5, v2, p2}, Landroid/graphics/PointF;-><init>(FF)V

    iget v7, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->chamferSlope:F

    const/high16 v8, 0x42b40000    # 90.0f

    add-float/2addr v7, v8

    const/high16 v9, 0x40000000    # 2.0f

    div-float/2addr v7, v9

    const/high16 v10, 0x43870000    # 270.0f

    sub-float v7, v10, v7

    invoke-direct {p0, p3, v5, v7, p4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 658
    new-instance v5, Landroid/graphics/PointF;

    invoke-direct {v5, v3, p1}, Landroid/graphics/PointF;-><init>(FF)V

    iget v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->chamferSlope:F

    sub-float v3, v6, v3

    div-float/2addr v3, v9

    add-float/2addr v3, v6

    invoke-direct {p0, p3, v5, v3, p4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 659
    new-instance v3, Landroid/graphics/PointF;

    invoke-direct {v3, v1, p1}, Landroid/graphics/PointF;-><init>(FF)V

    iget p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->chamferSlope:F

    sub-float p1, v6, p1

    div-float/2addr p1, v9

    const/4 v1, 0x0

    sub-float p1, v1, p1

    invoke-direct {p0, p3, v3, p1, p4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 660
    new-instance p1, Landroid/graphics/PointF;

    invoke-direct {p1, v0, p2}, Landroid/graphics/PointF;-><init>(FF)V

    iget p2, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->chamferSlope:F

    add-float/2addr p2, v8

    div-float/2addr p2, v9

    add-float/2addr p2, v10

    invoke-direct {p0, p3, p1, p2, p4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 661
    new-instance p1, Landroid/graphics/PointF;

    invoke-direct {p1, v0, v4}, Landroid/graphics/PointF;-><init>(FF)V

    invoke-direct {p0, p3, p1, v1, p4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 662
    new-instance p1, Landroid/graphics/PointF;

    invoke-direct {p1, v2, v4}, Landroid/graphics/PointF;-><init>(FF)V

    invoke-direct {p0, p3, p1, v6, p4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->moveTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 663
    invoke-virtual {p3}, Landroid/graphics/Path;->close()V

    return-object p3
.end method

.method private newTopOutlineGeometry(Landroid/graphics/RectF;Landroid/graphics/PointF;FFFFFZ)Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;
    .locals 23

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    move-object/from16 v2, p2

    move/from16 v3, p4

    move/from16 v4, p7

    const/4 v6, 0x0

    cmpl-float v7, v3, v6

    if-nez v7, :cond_0

    const/4 v7, 0x1

    goto :goto_0

    :cond_0
    const/4 v7, 0x0

    .line 591
    :goto_0
    iget v8, v1, Landroid/graphics/RectF;->top:F

    add-float v8, v8, p3

    sub-float v9, v8, p6

    .line 593
    iget v10, v2, Landroid/graphics/PointF;->y:F

    sub-float v10, v9, v10

    .line 594
    iget v11, v1, Landroid/graphics/RectF;->top:F

    sub-float v12, v10, v11

    .line 596
    iget v13, v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mEdgeSlopeHeightRatio:F

    mul-float v13, v13, v12

    add-float v14, v11, v13

    .line 599
    iget v15, v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabSlope:F

    move/from16 v16, v7

    float-to-double v6, v15

    invoke-static {v6, v7}, Ljava/lang/Math;->toRadians(D)D

    move-result-wide v6

    invoke-static {v6, v7}, Ljava/lang/Math;->tan(D)D

    move-result-wide v6

    double-to-float v6, v6

    .line 600
    iget v7, v1, Landroid/graphics/RectF;->left:F

    if-eqz v16, :cond_1

    move v15, v7

    goto :goto_1

    .line 601
    :cond_1
    iget v15, v2, Landroid/graphics/PointF;->x:F

    add-float/2addr v15, v7

    :goto_1
    if-eqz v16, :cond_2

    move v3, v7

    move/from16 v17, v12

    goto :goto_2

    :cond_2
    move/from16 v17, v12

    .line 602
    iget v12, v2, Landroid/graphics/PointF;->x:F

    invoke-static {v3, v12}, Ljava/lang/Math;->max(FF)F

    move-result v3

    add-float/2addr v3, v7

    :goto_2
    if-eqz v16, :cond_3

    move v12, v13

    goto :goto_3

    :cond_3
    move/from16 v12, v17

    :goto_3
    div-float/2addr v12, v6

    add-float/2addr v12, v3

    move/from16 v18, v13

    add-float v13, v12, p5

    if-eqz p8, :cond_4

    move/from16 v17, v18

    :cond_4
    div-float v17, v17, v6

    add-float v6, v13, v17

    .line 606
    iget v1, v1, Landroid/graphics/RectF;->right:F

    .line 607
    iget v2, v2, Landroid/graphics/PointF;->x:F

    sub-float v2, v1, v2

    move/from16 v19, v1

    .line 609
    new-instance v1, Landroid/graphics/Path;

    invoke-direct {v1}, Landroid/graphics/Path;-><init>()V

    move/from16 v20, v2

    .line 610
    new-instance v2, Landroid/graphics/PointF;

    invoke-direct {v2, v7, v8}, Landroid/graphics/PointF;-><init>(FF)V

    move/from16 v21, v8

    const/4 v8, 0x0

    invoke-direct {v0, v1, v2, v8, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->moveTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    const/high16 v2, 0x42b40000    # 90.0f

    const/high16 v17, 0x40000000    # 2.0f

    if-eqz v16, :cond_5

    .line 613
    new-instance v15, Landroid/graphics/PointF;

    invoke-direct {v15, v3, v14}, Landroid/graphics/PointF;-><init>(FF)V

    iget v3, v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabSlope:F

    add-float/2addr v3, v2

    div-float v3, v3, v17

    sub-float v3, v2, v3

    invoke-direct {v0, v1, v15, v3, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    move-result-object v3

    move/from16 v22, v7

    const/high16 v15, 0x43340000    # 180.0f

    goto :goto_4

    .line 615
    :cond_5
    new-instance v8, Landroid/graphics/PointF;

    invoke-direct {v8, v7, v9}, Landroid/graphics/PointF;-><init>(FF)V

    move/from16 v22, v7

    iget v7, v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->chamferSlope:F

    add-float/2addr v7, v2

    div-float v7, v7, v17

    sub-float v7, v2, v7

    invoke-direct {v0, v1, v8, v7, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 616
    new-instance v7, Landroid/graphics/PointF;

    invoke-direct {v7, v15, v10}, Landroid/graphics/PointF;-><init>(FF)V

    iget v8, v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->chamferSlope:F

    const/high16 v15, 0x43340000    # 180.0f

    sub-float v8, v15, v8

    div-float v8, v8, v17

    invoke-direct {v0, v1, v7, v8, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 617
    new-instance v7, Landroid/graphics/PointF;

    invoke-direct {v7, v3, v10}, Landroid/graphics/PointF;-><init>(FF)V

    iget v3, v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabSlope:F

    add-float/2addr v3, v15

    div-float v3, v3, v17

    sub-float v8, v15, v3

    invoke-direct {v0, v1, v7, v8, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    move-result-object v3

    .line 619
    :goto_4
    new-instance v7, Landroid/graphics/PointF;

    invoke-direct {v7, v12, v11}, Landroid/graphics/PointF;-><init>(FF)V

    iget v8, v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabSlope:F

    sub-float v8, v15, v8

    div-float v8, v8, v17

    invoke-direct {v0, v1, v7, v8, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    move-result-object v7

    .line 621
    new-instance v8, Landroid/graphics/PointF;

    invoke-direct {v8, v13, v11}, Landroid/graphics/PointF;-><init>(FF)V

    iget v11, v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabSlope:F

    sub-float v11, v15, v11

    div-float v11, v11, v17

    sub-float v11, v15, v11

    invoke-direct {v0, v1, v8, v11, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    move-result-object v8

    if-eqz p8, :cond_6

    .line 625
    new-instance v5, Landroid/graphics/PointF;

    invoke-direct {v5, v6, v14}, Landroid/graphics/PointF;-><init>(FF)V

    iget v11, v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabSlope:F

    add-float/2addr v11, v2

    div-float v11, v11, v17

    add-float/2addr v11, v2

    invoke-direct {v0, v1, v5, v11, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 626
    new-instance v5, Landroid/graphics/PointF;

    invoke-direct {v5, v6, v10}, Landroid/graphics/PointF;-><init>(FF)V

    const/high16 v6, 0x43070000    # 135.0f

    invoke-direct {v0, v1, v5, v6, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    move-result-object v5

    const/high16 v11, 0x43340000    # 180.0f

    goto :goto_5

    .line 628
    :cond_6
    new-instance v5, Landroid/graphics/PointF;

    invoke-direct {v5, v6, v10}, Landroid/graphics/PointF;-><init>(FF)V

    iget v6, v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabSlope:F

    const/high16 v11, 0x43340000    # 180.0f

    add-float/2addr v6, v11

    div-float v6, v6, v17

    invoke-direct {v0, v1, v5, v6, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    move-result-object v5

    .line 630
    :goto_5
    new-instance v6, Landroid/graphics/PointF;

    move/from16 v12, v20

    invoke-direct {v6, v12, v10}, Landroid/graphics/PointF;-><init>(FF)V

    iget v10, v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->chamferSlope:F

    sub-float v10, v11, v10

    div-float v10, v10, v17

    sub-float v10, v11, v10

    invoke-direct {v0, v1, v6, v10, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 631
    new-instance v6, Landroid/graphics/PointF;

    move/from16 v10, v19

    invoke-direct {v6, v10, v9}, Landroid/graphics/PointF;-><init>(FF)V

    iget v9, v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->chamferSlope:F

    add-float/2addr v9, v2

    div-float v9, v9, v17

    add-float/2addr v9, v2

    invoke-direct {v0, v1, v6, v9, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 632
    new-instance v2, Landroid/graphics/PointF;

    move/from16 v6, v21

    invoke-direct {v2, v10, v6}, Landroid/graphics/PointF;-><init>(FF)V

    invoke-direct {v0, v1, v2, v11, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->lineTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 633
    new-instance v2, Landroid/graphics/PointF;

    move/from16 v9, v22

    invoke-direct {v2, v9, v6}, Landroid/graphics/PointF;-><init>(FF)V

    const/4 v6, 0x0

    invoke-direct {v0, v1, v2, v6, v4}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->moveTo(Landroid/graphics/Path;Landroid/graphics/PointF;FF)Landroid/graphics/PointF;

    .line 634
    invoke-virtual {v1}, Landroid/graphics/Path;->close()V

    .line 636
    new-instance v2, Landroid/graphics/RectF;

    iget v3, v3, Landroid/graphics/PointF;->x:F

    iget v4, v7, Landroid/graphics/PointF;->y:F

    iget v6, v5, Landroid/graphics/PointF;->x:F

    iget v9, v5, Landroid/graphics/PointF;->y:F

    invoke-direct {v2, v3, v4, v6, v9}, Landroid/graphics/RectF;-><init>(FFFF)V

    .line 638
    new-instance v3, Landroid/graphics/RectF;

    iget v4, v7, Landroid/graphics/PointF;->x:F

    iget v6, v7, Landroid/graphics/PointF;->y:F

    iget v7, v8, Landroid/graphics/PointF;->x:F

    iget v8, v5, Landroid/graphics/PointF;->y:F

    invoke-direct {v3, v4, v6, v7, v8}, Landroid/graphics/RectF;-><init>(FFFF)V

    .line 641
    new-instance v4, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;

    iget v5, v5, Landroid/graphics/PointF;->y:F

    invoke-direct {v4, v1, v2, v3, v5}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;-><init>(Landroid/graphics/Path;Landroid/graphics/RectF;Landroid/graphics/RectF;F)V

    return-object v4
.end method


# virtual methods
.method protected checkLayoutParams(Landroid/view/ViewGroup$LayoutParams;)Z
    .locals 0

    .line 270
    instance-of p1, p1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;

    return p1
.end method

.method public dispatchTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 4

    .line 230
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->isEnabled()Z

    move-result v0

    if-eqz v0, :cond_2

    .line 231
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getAction()I

    move-result v0

    const/4 v1, 0x1

    if-ne v0, v1, :cond_1

    .line 232
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getComponentId(Landroid/view/MotionEvent;)Ljava/lang/Integer;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 234
    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v2

    const/4 v3, -0x1

    if-ne v2, v3, :cond_0

    .line 235
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->listener:Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;

    if-eqz v0, :cond_1

    .line 237
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->listener:Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;

    invoke-interface {v0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;->onClose()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v2, "UI-2017-2-DPRO"

    const-string v3, "Error"

    .line 239
    invoke-static {v2, v3, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    .line 243
    :cond_0
    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->selectTab(I)V

    .line 247
    :cond_1
    :goto_0
    invoke-super {p0, p1}, Landroid/view/ViewGroup;->dispatchTouchEvent(Landroid/view/MotionEvent;)Z

    return v1

    .line 250
    :cond_2
    invoke-super {p0, p1}, Landroid/view/ViewGroup;->dispatchTouchEvent(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method

.method protected bridge synthetic generateDefaultLayoutParams()Landroid/view/ViewGroup$LayoutParams;
    .locals 1

    .line 32
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->generateDefaultLayoutParams()Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;

    move-result-object v0

    return-object v0
.end method

.method protected generateDefaultLayoutParams()Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;
    .locals 3

    .line 260
    new-instance v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;

    const/4 v1, -0x1

    const/4 v2, -0x2

    invoke-direct {v0, v1, v2}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;-><init>(II)V

    return-object v0
.end method

.method public bridge synthetic generateLayoutParams(Landroid/util/AttributeSet;)Landroid/view/ViewGroup$LayoutParams;
    .locals 0

    .line 32
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->generateLayoutParams(Landroid/util/AttributeSet;)Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;

    move-result-object p1

    return-object p1
.end method

.method protected generateLayoutParams(Landroid/view/ViewGroup$LayoutParams;)Landroid/view/ViewGroup$LayoutParams;
    .locals 1

    .line 265
    new-instance v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;

    invoke-direct {v0, p1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;-><init>(Landroid/view/ViewGroup$LayoutParams;)V

    return-object v0
.end method

.method public generateLayoutParams(Landroid/util/AttributeSet;)Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;
    .locals 2

    .line 255
    new-instance v0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1, p1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-object v0
.end method

.method public getListener()Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;
    .locals 1

    .line 200
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->listener:Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;

    return-object v0
.end method

.method protected onDraw(Landroid/graphics/Canvas;)V
    .locals 5

    .line 277
    invoke-super {p0, p1}, Landroid/view/ViewGroup;->onDraw(Landroid/graphics/Canvas;)V

    .line 279
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLinePathBottom:Landroid/graphics/Path;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mFillPaint:Landroid/graphics/Paint;

    invoke-virtual {p1, v0, v1}, Landroid/graphics/Canvas;->drawPath(Landroid/graphics/Path;Landroid/graphics/Paint;)V

    .line 280
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLinePathBottom:Landroid/graphics/Path;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mOuterLinePaint:Landroid/graphics/Paint;

    invoke-virtual {p1, v0, v1}, Landroid/graphics/Canvas;->drawPath(Landroid/graphics/Path;Landroid/graphics/Paint;)V

    .line 281
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLinePathBottom:Landroid/graphics/Path;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mInnerLinePaint:Landroid/graphics/Paint;

    invoke-virtual {p1, v0, v1}, Landroid/graphics/Canvas;->drawPath(Landroid/graphics/Path;Landroid/graphics/Paint;)V

    .line 283
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    :goto_0
    if-ltz v0, :cond_1

    .line 284
    iget v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabIndex:I

    if-ne v0, v1, :cond_0

    goto :goto_1

    .line 287
    :cond_0
    invoke-direct {p0, p1, v0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->drawTab(Landroid/graphics/Canvas;I)V

    :goto_1
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    .line 289
    :cond_1
    iget v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabIndex:I

    invoke-direct {p0, p1, v0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->drawTab(Landroid/graphics/Canvas;I)V

    .line 291
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonDrawable:Landroid/graphics/drawable/Drawable;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonBounds:Landroid/graphics/RectF;

    if-eqz v0, :cond_2

    .line 292
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonDrawable:Landroid/graphics/drawable/Drawable;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonBounds:Landroid/graphics/RectF;

    iget v1, v1, Landroid/graphics/RectF;->left:F

    float-to-int v1, v1

    iget-object v2, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonBounds:Landroid/graphics/RectF;

    iget v2, v2, Landroid/graphics/RectF;->top:F

    float-to-int v2, v2

    iget-object v3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonBounds:Landroid/graphics/RectF;

    iget v3, v3, Landroid/graphics/RectF;->right:F

    float-to-int v3, v3

    iget-object v4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonBounds:Landroid/graphics/RectF;

    iget v4, v4, Landroid/graphics/RectF;->bottom:F

    float-to-int v4, v4

    invoke-virtual {v0, v1, v2, v3, v4}, Landroid/graphics/drawable/Drawable;->setBounds(IIII)V

    .line 295
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mCloseButtonDrawable:Landroid/graphics/drawable/Drawable;

    invoke-virtual {v0, p1}, Landroid/graphics/drawable/Drawable;->draw(Landroid/graphics/Canvas;)V

    :cond_2
    return-void
.end method

.method protected onLayout(ZIIII)V
    .locals 2

    .line 301
    iget-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpContainerRect:Landroid/graphics/Rect;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingLeft()I

    move-result p2

    iget p3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    float-to-int p3, p3

    add-int/2addr p2, p3

    iput p2, p1, Landroid/graphics/Rect;->left:I

    .line 302
    iget-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpContainerRect:Landroid/graphics/Rect;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingTop()I

    move-result p2

    invoke-direct {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->calculateMaxTabLabelHeight()F

    move-result p3

    iget-object p4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget p4, p4, Lnet/gogame/gowrap/ui/dpro/view/Margins;->top:F

    add-float/2addr p3, p4

    iget-object p4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget p4, p4, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    add-float/2addr p3, p4

    iget-object p4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget p4, p4, Lnet/gogame/gowrap/ui/dpro/view/Margins;->top:F

    add-float/2addr p3, p4

    iget-object p4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget p4, p4, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    add-float/2addr p3, p4

    float-to-int p3, p3

    add-int/2addr p2, p3

    iput p2, p1, Landroid/graphics/Rect;->top:I

    .line 305
    iget-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpContainerRect:Landroid/graphics/Rect;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getMeasuredWidth()I

    move-result p2

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingRight()I

    move-result p3

    sub-int/2addr p2, p3

    iget p3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    float-to-int p3, p3

    sub-int/2addr p2, p3

    iput p2, p1, Landroid/graphics/Rect;->right:I

    .line 306
    iget-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpContainerRect:Landroid/graphics/Rect;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getMeasuredHeight()I

    move-result p2

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingBottom()I

    move-result p3

    sub-int/2addr p2, p3

    iget p3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    float-to-int p3, p3

    sub-int/2addr p2, p3

    iput p2, p1, Landroid/graphics/Rect;->bottom:I

    const/4 p1, 0x0

    .line 309
    :goto_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getChildCount()I

    move-result p2

    if-ge p1, p2, :cond_1

    .line 310
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getChildAt(I)Landroid/view/View;

    move-result-object p2

    .line 311
    invoke-virtual {p2}, Landroid/view/View;->getVisibility()I

    move-result p3

    const/16 p4, 0x8

    if-eq p3, p4, :cond_0

    .line 312
    invoke-virtual {p2}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object p3

    check-cast p3, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;

    .line 314
    invoke-virtual {p2}, Landroid/view/View;->getMeasuredWidth()I

    move-result p4

    .line 315
    invoke-virtual {p2}, Landroid/view/View;->getMeasuredHeight()I

    move-result p5

    .line 317
    iget p3, p3, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$LayoutParams;->gravity:I

    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpContainerRect:Landroid/graphics/Rect;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpChildRect:Landroid/graphics/Rect;

    invoke-static {p3, p4, p5, v0, v1}, Landroid/view/Gravity;->apply(IIILandroid/graphics/Rect;Landroid/graphics/Rect;)V

    .line 319
    iget-object p3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpChildRect:Landroid/graphics/Rect;

    iget p3, p3, Landroid/graphics/Rect;->left:I

    iget-object p4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpChildRect:Landroid/graphics/Rect;

    iget p4, p4, Landroid/graphics/Rect;->top:I

    iget-object p5, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpChildRect:Landroid/graphics/Rect;

    iget p5, p5, Landroid/graphics/Rect;->right:I

    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTmpChildRect:Landroid/graphics/Rect;

    iget v0, v0, Landroid/graphics/Rect;->bottom:I

    invoke-virtual {p2, p3, p4, p5, v0}, Landroid/view/View;->layout(IIII)V

    :cond_0
    add-int/lit8 p1, p1, 0x1

    goto :goto_0

    :cond_1
    return-void
.end method

.method protected onMeasure(II)V
    .locals 14

    move-object v6, p0

    .line 327
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v0

    .line 328
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result v1

    .line 329
    invoke-static/range {p2 .. p2}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v2

    .line 330
    invoke-static/range {p2 .. p2}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result v3

    const/high16 v4, 0x40000000    # 2.0f

    const/high16 v5, 0x40000000    # 2.0f

    const/high16 v7, -0x80000000

    const/4 v8, 0x0

    if-eq v0, v7, :cond_0

    if-eq v0, v5, :cond_0

    .line 342
    invoke-static {v8, v8}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v0

    :goto_0
    move v9, v0

    goto :goto_1

    .line 336
    :cond_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingLeft()I

    move-result v9

    sub-int/2addr v1, v9

    .line 337
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingRight()I

    move-result v9

    sub-int/2addr v1, v9

    iget v9, v6, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    mul-float v9, v9, v4

    float-to-int v9, v9

    sub-int/2addr v1, v9

    .line 336
    invoke-static {v1, v8}, Ljava/lang/Math;->max(II)I

    move-result v1

    .line 338
    invoke-static {v1, v0}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v0

    goto :goto_0

    :goto_1
    if-eq v2, v7, :cond_1

    if-eq v2, v5, :cond_1

    .line 359
    invoke-static {v8, v8}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v0

    :goto_2
    move v7, v0

    goto :goto_3

    .line 350
    :cond_1
    iget-object v0, v6, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v0, v0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->top:F

    iget-object v1, v6, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelMargins:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v1, v1, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    add-float/2addr v0, v1

    iget-object v1, v6, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v1, v1, Lnet/gogame/gowrap/ui/dpro/view/Margins;->top:F

    add-float/2addr v0, v1

    iget-object v1, v6, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mTabLabelPadding:Lnet/gogame/gowrap/ui/dpro/view/Margins;

    iget v1, v1, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    add-float/2addr v0, v1

    .line 352
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->calculateMaxTabLabelHeight()F

    move-result v1

    add-float/2addr v0, v1

    .line 353
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingTop()I

    move-result v1

    sub-int/2addr v3, v1

    .line 354
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getPaddingBottom()I

    move-result v1

    sub-int/2addr v3, v1

    iget v1, v6, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mPathOffset:F

    mul-float v1, v1, v4

    float-to-int v1, v1

    sub-int/2addr v3, v1

    float-to-int v0, v0

    sub-int/2addr v3, v0

    .line 353
    invoke-static {v3, v8}, Ljava/lang/Math;->max(II)I

    move-result v0

    .line 355
    invoke-static {v0, v2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v0

    goto :goto_2

    :goto_3
    const/4 v10, 0x0

    const/4 v11, 0x0

    const/4 v12, 0x0

    .line 365
    :goto_4
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getChildCount()I

    move-result v0

    if-ge v8, v0, :cond_3

    .line 366
    invoke-virtual {p0, v8}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->getChildAt(I)Landroid/view/View;

    move-result-object v13

    .line 367
    invoke-virtual {v13}, Landroid/view/View;->getVisibility()I

    move-result v0

    const/16 v1, 0x8

    if-eq v0, v1, :cond_2

    const/4 v3, 0x0

    const/4 v5, 0x0

    move-object v0, p0

    move-object v1, v13

    move v2, v9

    move v4, v7

    .line 368
    invoke-virtual/range {v0 .. v5}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->measureChildWithMargins(Landroid/view/View;IIII)V

    .line 370
    invoke-virtual {v13}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    check-cast v0, Landroid/view/ViewGroup$MarginLayoutParams;

    .line 371
    invoke-virtual {v13}, Landroid/view/View;->getMeasuredWidth()I

    move-result v1

    iget v2, v0, Landroid/view/ViewGroup$MarginLayoutParams;->leftMargin:I

    add-int/2addr v1, v2

    iget v2, v0, Landroid/view/ViewGroup$MarginLayoutParams;->rightMargin:I

    add-int/2addr v1, v2

    invoke-static {v10, v1}, Ljava/lang/Math;->max(II)I

    move-result v1

    .line 373
    invoke-virtual {v13}, Landroid/view/View;->getMeasuredHeight()I

    move-result v2

    iget v3, v0, Landroid/view/ViewGroup$MarginLayoutParams;->topMargin:I

    add-int/2addr v2, v3

    iget v0, v0, Landroid/view/ViewGroup$MarginLayoutParams;->bottomMargin:I

    add-int/2addr v2, v0

    invoke-static {v12, v2}, Ljava/lang/Math;->max(II)I

    move-result v0

    .line 375
    invoke-virtual {v13}, Landroid/view/View;->getMeasuredState()I

    move-result v2

    invoke-static {v11, v2}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->combineMeasuredStates(II)I

    move-result v2

    move v12, v0

    move v10, v1

    move v11, v2

    :cond_2
    add-int/lit8 v8, v8, 0x1

    goto :goto_4

    :cond_3
    move v0, p1

    .line 379
    invoke-static {v10, p1, v11}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->resolveSizeAndState(III)I

    move-result v0

    shl-int/lit8 v1, v11, 0x10

    move/from16 v2, p2

    .line 380
    invoke-static {v12, v2, v1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->resolveSizeAndState(III)I

    move-result v1

    .line 379
    invoke-virtual {p0, v0, v1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->setMeasuredDimension(II)V

    return-void
.end method

.method protected onSizeChanged(IIII)V
    .locals 0

    .line 386
    invoke-super {p0, p1, p2, p3, p4}, Landroid/view/ViewGroup;->onSizeChanged(IIII)V

    .line 387
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->calculateBounds(II)V

    return-void
.end method

.method public selectTab(I)V
    .locals 3

    if-ltz p1, :cond_3

    .line 208
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->tabHolderList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-lt p1, v0, :cond_0

    goto :goto_1

    .line 211
    :cond_0
    iget v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabIndex:I

    if-ne p1, v0, :cond_1

    return-void

    .line 214
    :cond_1
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->listener:Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;

    if-eqz v0, :cond_2

    .line 216
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->listener:Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;

    invoke-interface {v0, p1}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;->onTabSelected(I)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "UI-2017-2-DPRO"

    const-string v2, "Exception"

    .line 218
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 221
    :cond_2
    :goto_0
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->mSelectedTabIndex:I

    .line 222
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->calculatePaths()V

    .line 223
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->invalidate()V

    return-void

    :cond_3
    :goto_1
    return-void
.end method

.method public setListener(Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;)V
    .locals 0

    .line 204
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->listener:Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;

    return-void
.end method
