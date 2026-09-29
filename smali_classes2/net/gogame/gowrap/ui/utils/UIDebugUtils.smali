.class public Lnet/gogame/gowrap/ui/utils/UIDebugUtils;
.super Ljava/lang/Object;
.source "UIDebugUtils.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getModeName(I)Ljava/lang/String;
    .locals 1

    const/high16 v0, -0x80000000

    if-eq p0, v0, :cond_2

    if-eqz p0, :cond_1

    const/high16 v0, 0x40000000    # 2.0f

    if-eq p0, v0, :cond_0

    .line 18
    invoke-static {p0}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object p0

    return-object p0

    :cond_0
    const-string p0, "EXACTLY"

    return-object p0

    :cond_1
    const-string p0, "UNSPECIFIED"

    return-object p0

    :cond_2
    const-string p0, "AT_MOST"

    return-object p0
.end method

.method public static measureSpecToString(I)Ljava/lang/String;
    .locals 5

    .line 23
    invoke-static {p0}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v0

    .line 24
    invoke-static {p0}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result p0

    .line 25
    sget-object v1, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    const-string v2, "%s %d"

    const/4 v3, 0x2

    new-array v3, v3, [Ljava/lang/Object;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/utils/UIDebugUtils;->getModeName(I)Ljava/lang/String;

    move-result-object v0

    const/4 v4, 0x0

    aput-object v0, v3, v4

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p0

    const/4 v0, 0x1

    aput-object p0, v3, v0

    invoke-static {v1, v2, v3}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method
