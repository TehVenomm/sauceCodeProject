.class public Ljp/colopl/libs/LocationExtras;
.super Ljava/lang/Object;
.source "LocationExtras.java"


# static fields
.field private static final ACCURACY_THRESHOLD:F = 500.0f

.field public static final NW_LOCATION_SOURCE_CACHED:I = 0x2

.field private static final NW_LOCATION_SOURCE_CACHED_STRING:Ljava/lang/String; = "cached"

.field public static final NW_LOCATION_SOURCE_SERVER:I = 0x1

.field private static final NW_LOCATION_SOURCE_SERVER_STRING:Ljava/lang/String; = "server"

.field public static final NW_LOCATION_SOURCE_UNKNOWN:I = 0x0

.field public static final NW_LOCATION_TYPE_CELL:I = 0x1

.field private static final NW_LOCATION_TYPE_CELL_STRING:Ljava/lang/String; = "cell"

.field public static final NW_LOCATION_TYPE_UNKNOWN:I = 0x0

.field public static final NW_LOCATION_TYPE_WIFI:I = 0x2

.field private static final NW_LOCATION_TYPE_WIFI_STRING:Ljava/lang/String; = "wifi"

.field private static final TAG:Ljava/lang/String; = "LocationExtras"


# instance fields
.field private locationSource:I

.field private locationType:I


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 30
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 27
    iput v0, p0, Ljp/colopl/libs/LocationExtras;->locationSource:I

    .line 28
    iput v0, p0, Ljp/colopl/libs/LocationExtras;->locationType:I

    return-void
.end method

.method public constructor <init>(Landroid/location/Location;)V
    .locals 1

    .line 33
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 27
    iput v0, p0, Ljp/colopl/libs/LocationExtras;->locationSource:I

    .line 28
    iput v0, p0, Ljp/colopl/libs/LocationExtras;->locationType:I

    .line 34
    invoke-virtual {p0, p1}, Ljp/colopl/libs/LocationExtras;->setLocation(Landroid/location/Location;)V

    return-void
.end method


# virtual methods
.method public getLocationSource()I
    .locals 1

    .line 94
    iget v0, p0, Ljp/colopl/libs/LocationExtras;->locationSource:I

    return v0
.end method

.method public getLocationType()I
    .locals 1

    .line 98
    iget v0, p0, Ljp/colopl/libs/LocationExtras;->locationType:I

    return v0
.end method

.method public setLocation(Landroid/location/Location;)V
    .locals 7

    if-nez p1, :cond_0

    return-void

    .line 41
    :cond_0
    invoke-virtual {p1}, Landroid/location/Location;->getProvider()Ljava/lang/String;

    move-result-object v0

    const-string v1, "network"

    .line 42
    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    return-void

    .line 46
    :cond_1
    invoke-virtual {p1}, Landroid/location/Location;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    if-nez v0, :cond_2

    return-void

    .line 53
    :cond_2
    invoke-virtual {v0}, Landroid/os/Bundle;->keySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    const/4 v2, 0x0

    move-object v3, v2

    :cond_3
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_5

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    .line 54
    invoke-virtual {v0, v4}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v6, "networkLocationSource"

    .line 55
    invoke-virtual {v4, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_4

    move-object v2, v5

    goto :goto_0

    :cond_4
    const-string v6, "networkLocationType"

    .line 58
    invoke-virtual {v4, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_3

    move-object v3, v5

    goto :goto_0

    :cond_5
    const-string v0, "LocationExtras"

    .line 62
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "networkLocationSource = "

    invoke-virtual {v1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    if-nez v2, :cond_6

    const-string v4, ""

    goto :goto_1

    :cond_6
    move-object v4, v2

    :goto_1
    invoke-virtual {v1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, " networkLocationType = "

    invoke-virtual {v1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    if-nez v3, :cond_7

    const-string v4, ""

    goto :goto_2

    :cond_7
    move-object v4, v3

    :goto_2
    invoke-virtual {v1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    if-nez v3, :cond_8

    if-eqz v2, :cond_8

    const-string v0, "server"

    .line 69
    invoke-virtual {v2, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_8

    .line 70
    invoke-virtual {p1}, Landroid/location/Location;->hasAccuracy()Z

    move-result v0

    if-eqz v0, :cond_8

    .line 71
    invoke-virtual {p1}, Landroid/location/Location;->getAccuracy()F

    move-result p1

    const/high16 v0, 0x43fa0000    # 500.0f

    cmpg-float p1, p1, v0

    if-gez p1, :cond_8

    const-string v3, "wifi"

    :cond_8
    const/4 p1, 0x2

    const/4 v0, 0x1

    if-eqz v2, :cond_a

    const-string v1, "server"

    .line 76
    invoke-virtual {v2, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_9

    .line 77
    iput v0, p0, Ljp/colopl/libs/LocationExtras;->locationSource:I

    goto :goto_3

    :cond_9
    const-string v1, "cached"

    .line 79
    invoke-virtual {v2, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_a

    .line 80
    iput p1, p0, Ljp/colopl/libs/LocationExtras;->locationSource:I

    :cond_a
    :goto_3
    if-eqz v3, :cond_c

    const-string v1, "cell"

    .line 84
    invoke-virtual {v3, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_b

    .line 85
    iput v0, p0, Ljp/colopl/libs/LocationExtras;->locationType:I

    goto :goto_4

    :cond_b
    const-string v0, "wifi"

    .line 87
    invoke-virtual {v3, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_c

    .line 88
    iput p1, p0, Ljp/colopl/libs/LocationExtras;->locationType:I

    :cond_c
    :goto_4
    return-void
.end method
