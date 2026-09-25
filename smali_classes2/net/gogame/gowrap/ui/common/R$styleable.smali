.class public final Lnet/gogame/gowrap/ui/common/R$styleable;
.super Ljava/lang/Object;
.source "R.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/common/R;
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

.field public static final FixedAspectRatioFrameLayout:[I

.field public static final FixedAspectRatioFrameLayout_aspectRatioHeight:I = 0x0

.field public static final FixedAspectRatioFrameLayout_aspectRatioWidth:I = 0x1

.field public static final FixedAspectRatioFrameLayout_maxHeight:I = 0x2


# direct methods
.method static constructor <clinit>()V
    .locals 4

    const/4 v0, 0x3

    .line 62
    new-array v1, v0, [I

    fill-array-data v1, :array_0

    sput-object v1, Lnet/gogame/gowrap/ui/common/R$styleable;->CustomGridLayout:[I

    const/4 v1, 0x1

    .line 66
    new-array v1, v1, [I

    const/4 v2, 0x0

    const v3, 0x7f0400c6

    aput v3, v1, v2

    sput-object v1, Lnet/gogame/gowrap/ui/common/R$styleable;->CustomGridLayout_Layout:[I

    .line 68
    new-array v0, v0, [I

    fill-array-data v0, :array_1

    sput-object v0, Lnet/gogame/gowrap/ui/common/R$styleable;->FixedAspectRatioFrameLayout:[I

    return-void

    nop

    :array_0
    .array-data 4
        0x7f040096
        0x7f040101
        0x7f04023f
    .end array-data

    :array_1
    .array-data 4
        0x7f04002b
        0x7f04002c
        0x7f040181
    .end array-data
.end method

.method private constructor <init>()V
    .locals 0

    .line 60
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
