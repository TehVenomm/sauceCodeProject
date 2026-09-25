.class final Lcom/appsflyer/h;
.super Ljava/lang/Object;
.source ""

# interfaces
.implements Landroid/hardware/SensorEventListener;


# instance fields
.field private ʻ:J

.field private ʽ:D

.field private final ˊ:I

.field private final ˋ:Ljava/lang/String;
    .annotation build Landroidx/annotation/NonNull;
    .end annotation
.end field

.field private final ˎ:Ljava/lang/String;
    .annotation build Landroidx/annotation/NonNull;
    .end annotation
.end field

.field private final ˏ:[J

.field private final ॱ:[[F

.field private final ᐝ:I


# direct methods
.method private constructor <init>(ILjava/lang/String;Ljava/lang/String;)V
    .locals 2
    .param p2    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 37
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x2

    .line 31
    new-array v1, v0, [[F

    iput-object v1, p0, Lcom/appsflyer/h;->ॱ:[[F

    .line 32
    new-array v0, v0, [J

    iput-object v0, p0, Lcom/appsflyer/h;->ˏ:[J

    .line 38
    iput p1, p0, Lcom/appsflyer/h;->ˊ:I

    if-nez p2, :cond_0

    const-string p2, ""

    .line 39
    :cond_0
    iput-object p2, p0, Lcom/appsflyer/h;->ˋ:Ljava/lang/String;

    if-nez p3, :cond_1

    const-string p3, ""

    .line 40
    :cond_1
    iput-object p3, p0, Lcom/appsflyer/h;->ˎ:Ljava/lang/String;

    .line 41
    iget-object p2, p0, Lcom/appsflyer/h;->ˋ:Ljava/lang/String;

    iget-object p3, p0, Lcom/appsflyer/h;->ˎ:Ljava/lang/String;

    add-int/lit8 p1, p1, 0x1f

    mul-int/lit8 p1, p1, 0x1f

    .line 6052
    invoke-virtual {p2}, Ljava/lang/Object;->hashCode()I

    move-result p2

    add-int/2addr p1, p2

    mul-int/lit8 p1, p1, 0x1f

    .line 6053
    invoke-virtual {p3}, Ljava/lang/Object;->hashCode()I

    move-result p2

    add-int/2addr p1, p2

    .line 41
    iput p1, p0, Lcom/appsflyer/h;->ᐝ:I

    return-void
.end method

.method private ˊ(Ljava/util/Map;Z)V
    .locals 2
    .param p1    # Ljava/util/Map;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Lcom/appsflyer/h;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;>;Z)V"
        }
    .end annotation

    .line 7224
    iget-object v0, p0, Lcom/appsflyer/h;->ॱ:[[F

    const/4 v1, 0x0

    aget-object v0, v0, v1

    if-eqz v0, :cond_0

    const/4 v1, 0x1

    :cond_0
    if-eqz v1, :cond_1

    .line 129
    invoke-direct {p0}, Lcom/appsflyer/h;->ˎ()Ljava/util/Map;

    move-result-object v0

    invoke-interface {p1, p0, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    if-eqz p2, :cond_2

    .line 131
    invoke-direct {p0}, Lcom/appsflyer/h;->ॱ()V

    return-void

    .line 133
    :cond_1
    invoke-interface {p1, p0}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result p2

    if-nez p2, :cond_2

    .line 134
    invoke-direct {p0}, Lcom/appsflyer/h;->ˎ()Ljava/util/Map;

    move-result-object p2

    invoke-interface {p1, p0, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_2
    return-void
.end method

.method private ˊ(ILjava/lang/String;Ljava/lang/String;)Z
    .locals 1
    .param p2    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 139
    iget v0, p0, Lcom/appsflyer/h;->ˊ:I

    if-ne v0, p1, :cond_0

    iget-object p1, p0, Lcom/appsflyer/h;->ˋ:Ljava/lang/String;

    .line 140
    invoke-virtual {p1, p2}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/appsflyer/h;->ˎ:Ljava/lang/String;

    .line 141
    invoke-virtual {p1, p3}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method private static ˋ([F[F)D
    .locals 8
    .param p0    # [F
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # [F
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 70
    array-length v0, p0

    array-length v1, p1

    invoke-static {v0, v1}, Ljava/lang/Math;->min(II)I

    move-result v0

    const-wide/16 v1, 0x0

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v0, :cond_0

    .line 73
    aget v4, p0, v3

    aget v5, p1, v3

    sub-float/2addr v4, v5

    float-to-double v4, v4

    const-wide/high16 v6, 0x4000000000000000L    # 2.0

    invoke-static {v4, v5, v6, v7}, Ljava/lang/StrictMath;->pow(DD)D

    move-result-wide v4

    add-double/2addr v1, v4

    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 75
    :cond_0
    invoke-static {v1, v2}, Ljava/lang/Math;->sqrt(D)D

    move-result-wide p0

    return-wide p0
.end method

.method static ˋ(Landroid/hardware/Sensor;)Lcom/appsflyer/h;
    .locals 3

    .line 58
    invoke-virtual {p0}, Landroid/hardware/Sensor;->getType()I

    move-result v0

    invoke-virtual {p0}, Landroid/hardware/Sensor;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0}, Landroid/hardware/Sensor;->getVendor()Ljava/lang/String;

    move-result-object p0

    .line 6062
    new-instance v2, Lcom/appsflyer/h;

    invoke-direct {v2, v0, v1, p0}, Lcom/appsflyer/h;-><init>(ILjava/lang/String;Ljava/lang/String;)V

    return-object v2
.end method

.method private ˎ()Ljava/util/Map;
    .locals 3
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 188
    new-instance v0, Ljava/util/HashMap;

    const/4 v1, 0x7

    invoke-direct {v0, v1}, Ljava/util/HashMap;-><init>(I)V

    const-string v1, "sT"

    .line 189
    iget v2, p0, Lcom/appsflyer/h;->ˊ:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "sN"

    .line 190
    iget-object v2, p0, Lcom/appsflyer/h;->ˋ:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "sV"

    .line 191
    iget-object v2, p0, Lcom/appsflyer/h;->ˎ:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 192
    iget-object v1, p0, Lcom/appsflyer/h;->ॱ:[[F

    const/4 v2, 0x0

    aget-object v1, v1, v2

    if-eqz v1, :cond_0

    const-string v2, "sVS"

    .line 194
    invoke-static {v1}, Lcom/appsflyer/h;->ˏ([F)Ljava/util/List;

    move-result-object v1

    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 196
    :cond_0
    iget-object v1, p0, Lcom/appsflyer/h;->ॱ:[[F

    const/4 v2, 0x1

    aget-object v1, v1, v2

    if-eqz v1, :cond_1

    const-string v2, "sVE"

    .line 198
    invoke-static {v1}, Lcom/appsflyer/h;->ˏ([F)Ljava/util/List;

    move-result-object v1

    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    return-object v0
.end method

.method private static ˏ([F)Ljava/util/List;
    .locals 4
    .param p0    # [F
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([F)",
            "Ljava/util/List<",
            "Ljava/lang/Float;",
            ">;"
        }
    .end annotation

    .line 84
    new-instance v0, Ljava/util/ArrayList;

    array-length v1, p0

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(I)V

    .line 85
    array-length v1, p0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_0

    aget v3, p0, v2

    .line 86
    invoke-static {v3}, Ljava/lang/Float;->valueOf(F)Ljava/lang/Float;

    move-result-object v3

    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_0
    return-object v0
.end method

.method private ॱ()V
    .locals 5

    const/4 v0, 0x0

    const/4 v1, 0x0

    :goto_0
    const/4 v2, 0x2

    if-ge v1, v2, :cond_0

    .line 209
    iget-object v2, p0, Lcom/appsflyer/h;->ॱ:[[F

    const/4 v3, 0x0

    aput-object v3, v2, v1

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    :goto_1
    const-wide/16 v3, 0x0

    if-ge v0, v2, :cond_1

    .line 212
    iget-object v1, p0, Lcom/appsflyer/h;->ˏ:[J

    aput-wide v3, v1, v0

    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    :cond_1
    const-wide/16 v0, 0x0

    .line 214
    iput-wide v0, p0, Lcom/appsflyer/h;->ʽ:D

    .line 215
    iput-wide v3, p0, Lcom/appsflyer/h;->ʻ:J

    return-void
.end method


# virtual methods
.method public final equals(Ljava/lang/Object;)Z
    .locals 2

    if-ne p1, p0, :cond_0

    const/4 p1, 0x1

    return p1

    .line 241
    :cond_0
    instance-of v0, p1, Lcom/appsflyer/h;

    if-eqz v0, :cond_1

    .line 242
    check-cast p1, Lcom/appsflyer/h;

    .line 243
    iget v0, p1, Lcom/appsflyer/h;->ˊ:I

    iget-object v1, p1, Lcom/appsflyer/h;->ˋ:Ljava/lang/String;

    iget-object p1, p1, Lcom/appsflyer/h;->ˎ:Ljava/lang/String;

    invoke-direct {p0, v0, v1, p1}, Lcom/appsflyer/h;->ˊ(ILjava/lang/String;Ljava/lang/String;)Z

    move-result p1

    return p1

    :cond_1
    const/4 p1, 0x0

    return p1
.end method

.method public final hashCode()I
    .locals 1

    .line 229
    iget v0, p0, Lcom/appsflyer/h;->ᐝ:I

    return v0
.end method

.method public final onAccuracyChanged(Landroid/hardware/Sensor;I)V
    .locals 0

    return-void
.end method

.method public final onSensorChanged(Landroid/hardware/SensorEvent;)V
    .locals 12

    if-eqz p1, :cond_4

    .line 93
    iget-object v0, p1, Landroid/hardware/SensorEvent;->values:[F

    if-eqz v0, :cond_4

    iget-object v0, p1, Landroid/hardware/SensorEvent;->sensor:Landroid/hardware/Sensor;

    const/4 v1, 0x0

    const/4 v2, 0x1

    if-eqz v0, :cond_0

    .line 6079
    invoke-virtual {v0}, Landroid/hardware/Sensor;->getName()Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_0

    invoke-virtual {v0}, Landroid/hardware/Sensor;->getVendor()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    if-eqz v0, :cond_4

    .line 94
    iget-object v0, p1, Landroid/hardware/SensorEvent;->sensor:Landroid/hardware/Sensor;

    invoke-virtual {v0}, Landroid/hardware/Sensor;->getType()I

    move-result v0

    iget-object v3, p1, Landroid/hardware/SensorEvent;->sensor:Landroid/hardware/Sensor;

    invoke-virtual {v3}, Landroid/hardware/Sensor;->getName()Ljava/lang/String;

    move-result-object v3

    iget-object v4, p1, Landroid/hardware/SensorEvent;->sensor:Landroid/hardware/Sensor;

    .line 95
    invoke-virtual {v4}, Landroid/hardware/Sensor;->getVendor()Ljava/lang/String;

    move-result-object v4

    iget-wide v5, p1, Landroid/hardware/SensorEvent;->timestamp:J

    iget-object p1, p1, Landroid/hardware/SensorEvent;->values:[F

    .line 6108
    invoke-direct {p0, v0, v3, v4}, Lcom/appsflyer/h;->ˊ(ILjava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_4

    .line 6146
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v3

    .line 6147
    iget-object v0, p0, Lcom/appsflyer/h;->ॱ:[[F

    aget-object v0, v0, v1

    if-nez v0, :cond_1

    .line 6149
    iget-object v0, p0, Lcom/appsflyer/h;->ॱ:[[F

    array-length v2, p1

    invoke-static {p1, v2}, Ljava/util/Arrays;->copyOf([FI)[F

    move-result-object p1

    aput-object p1, v0, v1

    .line 6150
    iget-object p1, p0, Lcom/appsflyer/h;->ˏ:[J

    aput-wide v3, p1, v1

    return-void

    .line 6152
    :cond_1
    iget-object v1, p0, Lcom/appsflyer/h;->ॱ:[[F

    aget-object v1, v1, v2

    if-nez v1, :cond_2

    .line 6154
    array-length v1, p1

    invoke-static {p1, v1}, Ljava/util/Arrays;->copyOf([FI)[F

    move-result-object p1

    .line 6155
    iget-object v1, p0, Lcom/appsflyer/h;->ॱ:[[F

    aput-object p1, v1, v2

    .line 6156
    iget-object v1, p0, Lcom/appsflyer/h;->ˏ:[J

    aput-wide v3, v1, v2

    .line 6157
    invoke-static {v0, p1}, Lcom/appsflyer/h;->ˋ([F[F)D

    move-result-wide v0

    iput-wide v0, p0, Lcom/appsflyer/h;->ʽ:D

    return-void

    :cond_2
    const-wide/32 v7, 0x2faf080

    .line 6161
    iget-wide v9, p0, Lcom/appsflyer/h;->ʻ:J

    sub-long v9, v5, v9

    cmp-long v11, v7, v9

    if-gtz v11, :cond_4

    .line 6162
    iput-wide v5, p0, Lcom/appsflyer/h;->ʻ:J

    .line 6163
    invoke-static {v1, p1}, Ljava/util/Arrays;->equals([F[F)Z

    move-result v1

    if-eqz v1, :cond_3

    .line 6165
    iget-object p1, p0, Lcom/appsflyer/h;->ˏ:[J

    aput-wide v3, p1, v2

    return-void

    .line 6167
    :cond_3
    invoke-static {v0, p1}, Lcom/appsflyer/h;->ˋ([F[F)D

    move-result-wide v0

    .line 6168
    iget-wide v5, p0, Lcom/appsflyer/h;->ʽ:D

    cmpl-double v7, v0, v5

    if-lez v7, :cond_4

    .line 6169
    iget-object v5, p0, Lcom/appsflyer/h;->ॱ:[[F

    array-length v6, p1

    invoke-static {p1, v6}, Ljava/util/Arrays;->copyOf([FI)[F

    move-result-object p1

    aput-object p1, v5, v2

    .line 6170
    iget-object p1, p0, Lcom/appsflyer/h;->ˏ:[J

    aput-wide v3, p1, v2

    .line 6171
    iput-wide v0, p0, Lcom/appsflyer/h;->ʽ:D

    :cond_4
    return-void
.end method

.method public final ˋ(Ljava/util/Map;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Lcom/appsflyer/h;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;>;)V"
        }
    .end annotation

    const/4 v0, 0x0

    .line 124
    invoke-direct {p0, p1, v0}, Lcom/appsflyer/h;->ˊ(Ljava/util/Map;Z)V

    return-void
.end method

.method final ˎ(Ljava/util/Map;)V
    .locals 1
    .param p1    # Ljava/util/Map;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Lcom/appsflyer/h;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;>;)V"
        }
    .end annotation

    const/4 v0, 0x1

    .line 120
    invoke-direct {p0, p1, v0}, Lcom/appsflyer/h;->ˊ(Ljava/util/Map;Z)V

    return-void
.end method
