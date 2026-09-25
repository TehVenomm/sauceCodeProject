.class public Ljp/colopl/libs/CdmaCellLocationRef;
.super Ljava/lang/Object;
.source "CdmaCellLocationRef.java"


# static fields
.field private static final CDMA_CELL_LOCATION_CLASS_NAME:Ljava/lang/String; = "android.telephony.cdma.CdmaCellLocation"

.field private static final INVALID_LAT_LON:I = 0x7fffffff

.field private static final PSEUDO_ACCURACY:F = 350.0f

.field public static final PSEUDO_ACCURACY_FOR_PIN:F = 1500.0f

.field public static final PSEUDO_NAME_AS_LOCATION_PROVIDER:Ljava/lang/String; = "CdmaCellLocationProvider"

.field private static final TAG:Ljava/lang/String; = "CdmaCellLocationRef"

.field static cdmaCellLocationClass:Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation
.end field


# instance fields
.field private getBaseStationLatitude:Ljava/lang/reflect/Method;

.field private getBaseStationLongitude:Ljava/lang/reflect/Method;

.field public isValid:Z

.field private location:Landroid/telephony/CellLocation;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>(Landroid/telephony/CellLocation;)V
    .locals 4

    .line 58
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 27
    iput-boolean v0, p0, Ljp/colopl/libs/CdmaCellLocationRef;->isValid:Z

    .line 59
    invoke-static {p1}, Ljp/colopl/libs/CdmaCellLocationRef;->isCdmaCellLocationInstance(Landroid/telephony/CellLocation;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 60
    invoke-static {}, Ljp/colopl/libs/CdmaCellLocationRef;->getCdmaCellLocationClass()Ljava/lang/Class;

    move-result-object v0

    :try_start_0
    const-string v1, "getBaseStationLatitude"

    const/4 v2, 0x0

    .line 62
    move-object v3, v2

    check-cast v3, [Ljava/lang/Class;

    invoke-virtual {v0, v1, v3}, Ljava/lang/Class;->getMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v1

    iput-object v1, p0, Ljp/colopl/libs/CdmaCellLocationRef;->getBaseStationLatitude:Ljava/lang/reflect/Method;

    const-string v1, "getBaseStationLongitude"

    .line 63
    check-cast v2, [Ljava/lang/Class;

    invoke-virtual {v0, v1, v2}, Ljava/lang/Class;->getMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v0

    iput-object v0, p0, Ljp/colopl/libs/CdmaCellLocationRef;->getBaseStationLongitude:Ljava/lang/reflect/Method;

    .line 64
    iput-object p1, p0, Ljp/colopl/libs/CdmaCellLocationRef;->location:Landroid/telephony/CellLocation;

    const/4 p1, 0x1

    .line 65
    iput-boolean p1, p0, Ljp/colopl/libs/CdmaCellLocationRef;->isValid:Z
    :try_end_0
    .catch Ljava/lang/NoSuchMethodException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :cond_0
    return-void
.end method

.method public static getCastedInstance(Landroid/telephony/CellLocation;)Ljp/colopl/libs/CdmaCellLocationRef;
    .locals 2

    .line 51
    invoke-static {p0}, Ljp/colopl/libs/CdmaCellLocationRef;->isCdmaCellLocationInstance(Landroid/telephony/CellLocation;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return-object v1

    .line 54
    :cond_0
    new-instance v0, Ljp/colopl/libs/CdmaCellLocationRef;

    invoke-direct {v0, p0}, Ljp/colopl/libs/CdmaCellLocationRef;-><init>(Landroid/telephony/CellLocation;)V

    .line 55
    iget-boolean p0, v0, Ljp/colopl/libs/CdmaCellLocationRef;->isValid:Z

    if-eqz p0, :cond_1

    goto :goto_0

    :cond_1
    move-object v0, v1

    :goto_0
    return-object v0
.end method

.method private static getCdmaCellLocationClass()Ljava/lang/Class;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation

    .line 32
    sget-object v0, Ljp/colopl/libs/CdmaCellLocationRef;->cdmaCellLocationClass:Ljava/lang/Class;

    if-nez v0, :cond_0

    :try_start_0
    const-string v0, "android.telephony.cdma.CdmaCellLocation"

    .line 34
    invoke-static {v0}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;

    move-result-object v0

    sput-object v0, Ljp/colopl/libs/CdmaCellLocationRef;->cdmaCellLocationClass:Ljava/lang/Class;
    :try_end_0
    .catch Ljava/lang/ClassNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    .line 39
    :catch_0
    :cond_0
    sget-object v0, Ljp/colopl/libs/CdmaCellLocationRef;->cdmaCellLocationClass:Ljava/lang/Class;

    return-object v0
.end method

.method private static isCdmaCellLocationInstance(Landroid/telephony/CellLocation;)Z
    .locals 1

    .line 43
    invoke-static {}, Ljp/colopl/libs/CdmaCellLocationRef;->getCdmaCellLocationClass()Ljava/lang/Class;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 44
    invoke-virtual {v0, p0}, Ljava/lang/Class;->isInstance(Ljava/lang/Object;)Z

    move-result p0

    if-eqz p0, :cond_0

    const/4 p0, 0x1

    return p0

    :cond_0
    const/4 p0, 0x0

    return p0
.end method


# virtual methods
.method public getBaseStationLatitude()I
    .locals 3

    .line 75
    :try_start_0
    iget-object v0, p0, Ljp/colopl/libs/CdmaCellLocationRef;->getBaseStationLatitude:Ljava/lang/reflect/Method;

    iget-object v1, p0, Ljp/colopl/libs/CdmaCellLocationRef;->location:Landroid/telephony/CellLocation;

    const/4 v2, 0x0

    check-cast v2, [Ljava/lang/Object;

    invoke-virtual {v0, v1, v2}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Integer;

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0
    :try_end_0
    .catch Ljava/lang/NullPointerException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/IllegalAccessException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/IllegalArgumentException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/reflect/InvocationTargetException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const v0, 0x7fffffff

    :goto_0
    return v0
.end method

.method public getBaseStationLongitude()I
    .locals 3

    .line 87
    :try_start_0
    iget-object v0, p0, Ljp/colopl/libs/CdmaCellLocationRef;->getBaseStationLongitude:Ljava/lang/reflect/Method;

    iget-object v1, p0, Ljp/colopl/libs/CdmaCellLocationRef;->location:Landroid/telephony/CellLocation;

    const/4 v2, 0x0

    check-cast v2, [Ljava/lang/Object;

    invoke-virtual {v0, v1, v2}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Integer;

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0
    :try_end_0
    .catch Ljava/lang/NullPointerException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/IllegalAccessException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/IllegalArgumentException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/reflect/InvocationTargetException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const v0, 0x7fffffff

    :goto_0
    return v0
.end method

.method public getLocation()Landroid/location/Location;
    .locals 10

    .line 97
    invoke-virtual {p0}, Ljp/colopl/libs/CdmaCellLocationRef;->getBaseStationLatitude()I

    move-result v0

    .line 98
    invoke-virtual {p0}, Ljp/colopl/libs/CdmaCellLocationRef;->getBaseStationLongitude()I

    move-result v1

    const/4 v2, 0x0

    const v3, 0x7fffffff

    if-eq v0, v3, :cond_3

    if-ne v1, v3, :cond_0

    goto :goto_1

    :cond_0
    int-to-float v0, v0

    const/high16 v3, 0x45610000    # 3600.0f

    div-float/2addr v0, v3

    const/high16 v4, 0x40800000    # 4.0f

    div-float/2addr v0, v4

    float-to-double v5, v0

    int-to-float v0, v1

    div-float/2addr v0, v3

    div-float/2addr v0, v4

    float-to-double v0, v0

    .line 118
    invoke-static {v5, v6}, Ljava/lang/Math;->abs(D)D

    move-result-wide v3

    const-wide v7, 0x3fb999999999999aL    # 0.1

    cmpg-double v9, v3, v7

    if-ltz v9, :cond_2

    invoke-static {v0, v1}, Ljava/lang/Math;->abs(D)D

    move-result-wide v3

    cmpg-double v9, v3, v7

    if-gez v9, :cond_1

    goto :goto_0

    .line 122
    :cond_1
    new-instance v2, Ljava/util/Date;

    invoke-direct {v2}, Ljava/util/Date;-><init>()V

    invoke-virtual {v2}, Ljava/util/Date;->getTime()J

    move-result-wide v2

    .line 123
    new-instance v4, Landroid/location/Location;

    const-string v7, "CdmaCellLocationProvider"

    invoke-direct {v4, v7}, Landroid/location/Location;-><init>(Ljava/lang/String;)V

    .line 124
    invoke-virtual {v4, v5, v6}, Landroid/location/Location;->setLatitude(D)V

    .line 125
    invoke-virtual {v4, v0, v1}, Landroid/location/Location;->setLongitude(D)V

    .line 126
    invoke-virtual {v4, v2, v3}, Landroid/location/Location;->setTime(J)V

    const/high16 v0, 0x43af0000    # 350.0f

    .line 127
    invoke-virtual {v4, v0}, Landroid/location/Location;->setAccuracy(F)V

    return-object v4

    :cond_2
    :goto_0
    return-object v2

    :cond_3
    :goto_1
    const-string v3, "CdmaCellLocationRef"

    .line 100
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Location is invalid. latitude = "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v0, ", longitude = "

    invoke-virtual {v4, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v3, v0}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    return-object v2
.end method
