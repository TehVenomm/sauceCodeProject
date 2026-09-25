.class public Lcom/helpshift/util/LocationUtil;
.super Ljava/lang/Object;
.source "LocationUtil.java"


# static fields
.field private static final LOCATION_MIN_DISTANCE:F = 10.0f

.field private static final TWO_MINUTES:I = 0x1d4c0


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static isBetterLocation(Landroid/location/Location;Landroid/location/Location;)Z
    .locals 9

    const/4 v0, 0x1

    if-nez p1, :cond_0

    return v0

    .line 26
    :cond_0
    invoke-virtual {p0}, Landroid/location/Location;->getTime()J

    move-result-wide v1

    invoke-virtual {p1}, Landroid/location/Location;->getTime()J

    move-result-wide v3

    sub-long/2addr v1, v3

    const-wide/32 v3, 0x1d4c0

    cmp-long v5, v1, v3

    const/4 v3, 0x0

    if-lez v5, :cond_1

    const/4 v4, 0x1

    goto :goto_0

    :cond_1
    const/4 v4, 0x0

    :goto_0
    const-wide/32 v5, -0x1d4c0

    cmp-long v7, v1, v5

    if-gez v7, :cond_2

    const/4 v5, 0x1

    goto :goto_1

    :cond_2
    const/4 v5, 0x0

    :goto_1
    const-wide/16 v6, 0x0

    cmp-long v8, v1, v6

    if-lez v8, :cond_3

    const/4 v1, 0x1

    goto :goto_2

    :cond_3
    const/4 v1, 0x0

    :goto_2
    if-eqz v4, :cond_4

    return v0

    :cond_4
    if-eqz v5, :cond_5

    return v3

    .line 42
    :cond_5
    invoke-virtual {p0}, Landroid/location/Location;->getAccuracy()F

    move-result v2

    invoke-virtual {p1}, Landroid/location/Location;->getAccuracy()F

    move-result v4

    sub-float/2addr v2, v4

    float-to-int v2, v2

    if-lez v2, :cond_6

    const/4 v4, 0x1

    goto :goto_3

    :cond_6
    const/4 v4, 0x0

    :goto_3
    if-gez v2, :cond_7

    const/4 v5, 0x1

    goto :goto_4

    :cond_7
    const/4 v5, 0x0

    :goto_4
    const/16 v6, 0xc8

    if-le v2, v6, :cond_8

    const/4 v2, 0x1

    goto :goto_5

    :cond_8
    const/4 v2, 0x0

    .line 48
    :goto_5
    invoke-virtual {p0}, Landroid/location/Location;->getProvider()Ljava/lang/String;

    move-result-object p0

    .line 49
    invoke-virtual {p1}, Landroid/location/Location;->getProvider()Ljava/lang/String;

    move-result-object p1

    .line 48
    invoke-static {p0, p1}, Lcom/helpshift/util/LocationUtil;->isSameProvider(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p0

    if-eqz v5, :cond_9

    return v0

    :cond_9
    if-eqz v1, :cond_a

    if-nez v4, :cond_a

    return v0

    :cond_a
    if-eqz v1, :cond_b

    if-nez v2, :cond_b

    if-eqz p0, :cond_b

    return v0

    :cond_b
    return v3
.end method

.method public static isSameLocation(Landroid/location/Location;Landroid/location/Location;)Z
    .locals 2

    const/4 v0, 0x1

    const/4 v1, 0x0

    if-eqz p0, :cond_1

    if-eqz p1, :cond_1

    .line 109
    invoke-virtual {p0, p1}, Landroid/location/Location;->distanceTo(Landroid/location/Location;)F

    move-result p0

    const/high16 p1, 0x41200000    # 10.0f

    cmpg-float p0, p0, p1

    if-gtz p0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0

    :cond_1
    if-nez p0, :cond_2

    if-nez p1, :cond_2

    goto :goto_1

    :cond_2
    const/4 v0, 0x0

    :goto_1
    return v0
.end method

.method private static isSameProvider(Ljava/lang/String;Ljava/lang/String;)Z
    .locals 0

    if-nez p0, :cond_1

    if-nez p1, :cond_0

    const/4 p0, 0x1

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    :goto_0
    return p0

    .line 123
    :cond_1
    invoke-virtual {p0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p0

    return p0
.end method

.method private static limitLatitude(DD)Landroid/location/Location;
    .locals 8

    .line 86
    invoke-static {p0, p1}, Lcom/helpshift/util/LocationUtil;->limitLongitude(D)D

    move-result-wide p0

    const-wide v0, 0x4056800000000000L    # 90.0

    cmpl-double v2, p0, v0

    const-wide v0, -0x3f99800000000000L    # -180.0

    const/4 v3, 0x1

    const-wide v4, 0x4066800000000000L    # 180.0

    if-lez v2, :cond_0

    sub-double p0, v4, p0

    goto :goto_0

    :cond_0
    const-wide v6, -0x3fa9800000000000L    # -90.0

    cmpg-double v2, p0, v6

    if-gez v2, :cond_1

    sub-double p0, v0, p0

    goto :goto_0

    :cond_1
    const/4 v3, 0x0

    :goto_0
    if-eqz v3, :cond_3

    const-wide/16 v2, 0x0

    cmpl-double v6, p2, v2

    if-lez v6, :cond_2

    goto :goto_1

    :cond_2
    move-wide v0, v4

    :goto_1
    const/4 v2, 0x0

    add-double/2addr p2, v0

    .line 99
    :cond_3
    invoke-static {p2, p3}, Lcom/helpshift/util/LocationUtil;->limitLongitude(D)D

    move-result-wide p2

    .line 101
    new-instance v0, Landroid/location/Location;

    const-string v1, ""

    invoke-direct {v0, v1}, Landroid/location/Location;-><init>(Ljava/lang/String;)V

    .line 102
    invoke-virtual {v0, p0, p1}, Landroid/location/Location;->setLatitude(D)V

    .line 103
    invoke-virtual {v0, p2, p3}, Landroid/location/Location;->setLongitude(D)V

    return-object v0
.end method

.method private static limitLongitude(D)D
    .locals 5

    const-wide v0, 0x4076800000000000L    # 360.0

    rem-double/2addr p0, v0

    const-wide v2, 0x4066800000000000L    # 180.0

    cmpl-double v4, p0, v2

    if-lez v4, :cond_0

    sub-double/2addr p0, v0

    goto :goto_0

    :cond_0
    const-wide v2, -0x3f99800000000000L    # -180.0

    cmpg-double v4, p0, v2

    if-gtz v4, :cond_1

    add-double/2addr p0, v0

    :cond_1
    :goto_0
    return-wide p0
.end method

.method public static sanitizeLocation(Landroid/location/Location;)Landroid/location/Location;
    .locals 4

    .line 65
    invoke-virtual {p0}, Landroid/location/Location;->getLatitude()D

    move-result-wide v0

    .line 66
    invoke-virtual {p0}, Landroid/location/Location;->getLongitude()D

    move-result-wide v2

    .line 65
    invoke-static {v0, v1, v2, v3}, Lcom/helpshift/util/LocationUtil;->limitLatitude(DD)Landroid/location/Location;

    move-result-object v0

    .line 67
    invoke-virtual {v0}, Landroid/location/Location;->getLatitude()D

    move-result-wide v1

    invoke-virtual {p0, v1, v2}, Landroid/location/Location;->setLatitude(D)V

    .line 68
    invoke-virtual {v0}, Landroid/location/Location;->getLongitude()D

    move-result-wide v0

    invoke-virtual {p0, v0, v1}, Landroid/location/Location;->setLongitude(D)V

    return-object p0
.end method
