.class public final Lnet/gogame/gowrap/ui/dpro/R$styleable;
.super Ljava/lang/Object;
.source "R.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/dpro/R;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "styleable"
.end annotation


# static fields
.field public static final CustomGridLayout:[I

.field public static final CustomGridLayout_Layout:[I

.field public static final CustomGridLayout_Layout_dummy:I = 0x0

.field public static final CustomGridLayout_column_count:I = 0x0

.field public static final CustomGridLayout_horizontal_spacing:I = 0x1

.field public static final CustomGridLayout_vertical_spacing:I = 0x2

.field public static final CustomImageButton:[I

.field public static final CustomImageButton_caption:I = 0x0

.field public static final CustomImageButton_image:I = 0x1

.field public static final CustomImageButton_level:I = 0x2

.field public static final CustomImageButton_subcaption:I = 0x3

.field public static final CustomTabbedPanel_Layout:[I

.field public static final CustomTabbedPanel_Layout_dummy:I = 0x0

.field public static final CustomTabbelPanel:[I

.field public static final CustomTabbelPanel_closeButton:I = 0x0

.field public static final CustomTabbelPanel_fillColor:I = 0x1

.field public static final CustomTabbelPanel_fontFamily:I = 0x2

.field public static final CustomTabbelPanel_innerLineColor:I = 0x3

.field public static final CustomTabbelPanel_innerLineWidth:I = 0x4

.field public static final CustomTabbelPanel_outerLineChamfer:I = 0x5

.field public static final CustomTabbelPanel_outerLineColor:I = 0x6

.field public static final CustomTabbelPanel_outerLineWidth:I = 0x7

.field public static final CustomTabbelPanel_pathOffset:I = 0x8

.field public static final CustomTabbelPanel_selectedTextColor:I = 0x9

.field public static final CustomTabbelPanel_selectedTextStyle:I = 0xa

.field public static final CustomTabbelPanel_tabFillColor:I = 0xb

.field public static final CustomTabbelPanel_tabLabelMarginBottom:I = 0xc

.field public static final CustomTabbelPanel_tabLabelMarginLeft:I = 0xd

.field public static final CustomTabbelPanel_tabLabelMarginRight:I = 0xe

.field public static final CustomTabbelPanel_tabLabelMarginTop:I = 0xf

.field public static final CustomTabbelPanel_tabLabelPaddingBottom:I = 0x10

.field public static final CustomTabbelPanel_tabLabelPaddingLeft:I = 0x11

.field public static final CustomTabbelPanel_tabLabelPaddingRight:I = 0x12

.field public static final CustomTabbelPanel_tabLabelPaddingTop:I = 0x13

.field public static final CustomTabbelPanel_tabSlope:I = 0x14

.field public static final CustomTabbelPanel_textColor:I = 0x15

.field public static final CustomTabbelPanel_textSize:I = 0x16

.field public static final CustomTabbelPanel_textStyle:I = 0x17

.field public static final FixedAspectRatioFrameLayout:[I

.field public static final FixedAspectRatioFrameLayout_aspectRatioHeight:I = 0x0

.field public static final FixedAspectRatioFrameLayout_aspectRatioWidth:I = 0x1

.field public static final FixedAspectRatioFrameLayout_maxHeight:I = 0x2


# direct methods
.method static constructor <clinit>()V
    .locals 5

    const/4 v0, 0x3

    .line 413
    new-array v1, v0, [I

    fill-array-data v1, :array_0

    sput-object v1, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomGridLayout:[I

    const/4 v1, 0x1

    .line 417
    new-array v2, v1, [I

    const v3, 0x7f0400c6

    const/4 v4, 0x0

    aput v3, v2, v4

    sput-object v2, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomGridLayout_Layout:[I

    const/4 v2, 0x4

    .line 419
    new-array v2, v2, [I

    fill-array-data v2, :array_1

    sput-object v2, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomImageButton:[I

    .line 424
    new-array v1, v1, [I

    aput v3, v1, v4

    sput-object v1, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbedPanel_Layout:[I

    const/16 v1, 0x18

    .line 426
    new-array v1, v1, [I

    fill-array-data v1, :array_2

    sput-object v1, Lnet/gogame/gowrap/ui/dpro/R$styleable;->CustomTabbelPanel:[I

    .line 451
    new-array v0, v0, [I

    fill-array-data v0, :array_3

    sput-object v0, Lnet/gogame/gowrap/ui/dpro/R$styleable;->FixedAspectRatioFrameLayout:[I

    return-void

    :array_0
    .array-data 4
        0x7f040096
        0x7f040101
        0x7f04023f
    .end array-data

    :array_1
    .array-data 4
        0x7f04005b
        0x7f040147
        0x7f04016c
        0x7f0401d2
    .end array-data

    :array_2
    .array-data 4
        0x7f04007c
        0x7f0400e3
        0x7f0400e7
        0x7f04014d
        0x7f04014e
        0x7f04018b
        0x7f04018c
        0x7f04018d
        0x7f04019b
        0x7f0401b5
        0x7f0401b6
        0x7f0401df
        0x7f0401ea
        0x7f0401eb
        0x7f0401ec
        0x7f0401ed
        0x7f0401ee
        0x7f0401ef
        0x7f0401f0
        0x7f0401f1
        0x7f0401fc
        0x7f040217
        0x7f04021c
        0x7f04021e
    .end array-data

    :array_3
    .array-data 4
        0x7f04002b
        0x7f04002c
        0x7f040181
    .end array-data
.end method

.method private constructor <init>()V
    .locals 0

    .line 411
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
