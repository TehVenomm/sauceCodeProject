.class public Ljp/colopl/api/docomo/Feature;
.super Ljava/lang/Object;
.source "Feature.java"


# instance fields
.field private address:Ljava/lang/String;

.field private adrCode:Ljava/lang/String;

.field private areaCode:Ljava/lang/String;

.field private areaName:Ljava/lang/String;

.field private latitude:D

.field private longitude:D

.field private postCode:Ljava/lang/String;

.field private time:J

.field private timeStr:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 5
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getAddress()Ljava/lang/String;
    .locals 1

    .line 75
    iget-object v0, p0, Ljp/colopl/api/docomo/Feature;->address:Ljava/lang/String;

    return-object v0
.end method

.method public getAdrCode()Ljava/lang/String;
    .locals 1

    .line 78
    iget-object v0, p0, Ljp/colopl/api/docomo/Feature;->adrCode:Ljava/lang/String;

    return-object v0
.end method

.method public getAreaCode()Ljava/lang/String;
    .locals 1

    .line 69
    iget-object v0, p0, Ljp/colopl/api/docomo/Feature;->areaCode:Ljava/lang/String;

    return-object v0
.end method

.method public getAreaName()Ljava/lang/String;
    .locals 1

    .line 72
    iget-object v0, p0, Ljp/colopl/api/docomo/Feature;->areaName:Ljava/lang/String;

    return-object v0
.end method

.method public getLatitude()D
    .locals 2

    .line 57
    iget-wide v0, p0, Ljp/colopl/api/docomo/Feature;->latitude:D

    return-wide v0
.end method

.method public getLocation()Landroid/location/Location;
    .locals 8

    .line 45
    invoke-virtual {p0}, Ljp/colopl/api/docomo/Feature;->getLatitude()D

    move-result-wide v0

    .line 46
    invoke-virtual {p0}, Ljp/colopl/api/docomo/Feature;->getLongitude()D

    move-result-wide v2

    .line 47
    invoke-virtual {p0}, Ljp/colopl/api/docomo/Feature;->getTime()J

    move-result-wide v4

    .line 48
    new-instance v6, Landroid/location/Location;

    const-string v7, "DoCoMoSPApi"

    invoke-direct {v6, v7}, Landroid/location/Location;-><init>(Ljava/lang/String;)V

    .line 49
    invoke-virtual {v6, v0, v1}, Landroid/location/Location;->setLatitude(D)V

    .line 50
    invoke-virtual {v6, v2, v3}, Landroid/location/Location;->setLongitude(D)V

    .line 51
    invoke-virtual {v6, v4, v5}, Landroid/location/Location;->setTime(J)V

    const/high16 v0, 0x437a0000    # 250.0f

    .line 52
    invoke-virtual {v6, v0}, Landroid/location/Location;->setAccuracy(F)V

    return-object v6
.end method

.method public getLongitude()D
    .locals 2

    .line 60
    iget-wide v0, p0, Ljp/colopl/api/docomo/Feature;->longitude:D

    return-wide v0
.end method

.method public getPostCode()Ljava/lang/String;
    .locals 1

    .line 81
    iget-object v0, p0, Ljp/colopl/api/docomo/Feature;->postCode:Ljava/lang/String;

    return-object v0
.end method

.method public getTime()J
    .locals 2

    .line 63
    iget-wide v0, p0, Ljp/colopl/api/docomo/Feature;->time:J

    return-wide v0
.end method

.method public getTimeStr()Ljava/lang/String;
    .locals 1

    .line 66
    iget-object v0, p0, Ljp/colopl/api/docomo/Feature;->timeStr:Ljava/lang/String;

    return-object v0
.end method

.method public hasAddress()Z
    .locals 1

    .line 91
    iget-object v0, p0, Ljp/colopl/api/docomo/Feature;->address:Ljava/lang/String;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public hasAdrCode()Z
    .locals 1

    .line 94
    iget-object v0, p0, Ljp/colopl/api/docomo/Feature;->adrCode:Ljava/lang/String;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public hasAreaCode()Z
    .locals 1

    .line 85
    iget-object v0, p0, Ljp/colopl/api/docomo/Feature;->areaCode:Ljava/lang/String;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public hasAreaName()Z
    .locals 1

    .line 88
    iget-object v0, p0, Ljp/colopl/api/docomo/Feature;->areaName:Ljava/lang/String;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public hasPostCode()Z
    .locals 1

    .line 97
    iget-object v0, p0, Ljp/colopl/api/docomo/Feature;->postCode:Ljava/lang/String;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public setAddress(Ljava/lang/String;)V
    .locals 0

    .line 35
    iput-object p1, p0, Ljp/colopl/api/docomo/Feature;->address:Ljava/lang/String;

    return-void
.end method

.method public setAdrCode(Ljava/lang/String;)V
    .locals 0

    .line 38
    iput-object p1, p0, Ljp/colopl/api/docomo/Feature;->adrCode:Ljava/lang/String;

    return-void
.end method

.method public setAreaCode(Ljava/lang/String;)V
    .locals 0

    .line 29
    iput-object p1, p0, Ljp/colopl/api/docomo/Feature;->areaCode:Ljava/lang/String;

    return-void
.end method

.method public setAreaName(Ljava/lang/String;)V
    .locals 0

    .line 32
    iput-object p1, p0, Ljp/colopl/api/docomo/Feature;->areaName:Ljava/lang/String;

    return-void
.end method

.method public setLatitude(D)V
    .locals 0

    .line 17
    iput-wide p1, p0, Ljp/colopl/api/docomo/Feature;->latitude:D

    return-void
.end method

.method public setLongitude(D)V
    .locals 0

    .line 20
    iput-wide p1, p0, Ljp/colopl/api/docomo/Feature;->longitude:D

    return-void
.end method

.method public setPostCode(Ljava/lang/String;)V
    .locals 0

    .line 41
    iput-object p1, p0, Ljp/colopl/api/docomo/Feature;->postCode:Ljava/lang/String;

    return-void
.end method

.method public setTime(J)V
    .locals 0

    .line 23
    iput-wide p1, p0, Ljp/colopl/api/docomo/Feature;->time:J

    return-void
.end method

.method public setTimeStr(Ljava/lang/String;)V
    .locals 0

    .line 26
    iput-object p1, p0, Ljp/colopl/api/docomo/Feature;->timeStr:Ljava/lang/String;

    return-void
.end method

.method public toString()Ljava/lang/String;
    .locals 4

    .line 101
    new-instance v0, Ljava/lang/StringBuffer;

    invoke-direct {v0}, Ljava/lang/StringBuffer;-><init>()V

    .line 102
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "[lat: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-wide v2, p0, Ljp/colopl/api/docomo/Feature;->latitude:D

    invoke-virtual {v1, v2, v3}, Ljava/lang/StringBuilder;->append(D)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 103
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ", lon: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-wide v2, p0, Ljp/colopl/api/docomo/Feature;->longitude:D

    invoke-virtual {v1, v2, v3}, Ljava/lang/StringBuilder;->append(D)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 104
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ", time: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-wide v2, p0, Ljp/colopl/api/docomo/Feature;->time:J

    invoke-virtual {v1, v2, v3}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 105
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ", timeStr: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Ljp/colopl/api/docomo/Feature;->timeStr:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 106
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ", areaCode: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Ljp/colopl/api/docomo/Feature;->areaCode:Ljava/lang/String;

    if-nez v2, :cond_0

    const-string v2, "null"

    goto :goto_0

    :cond_0
    iget-object v2, p0, Ljp/colopl/api/docomo/Feature;->areaCode:Ljava/lang/String;

    :goto_0
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 107
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ", areaName: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Ljp/colopl/api/docomo/Feature;->areaName:Ljava/lang/String;

    if-nez v2, :cond_1

    const-string v2, "null"

    goto :goto_1

    :cond_1
    iget-object v2, p0, Ljp/colopl/api/docomo/Feature;->areaName:Ljava/lang/String;

    :goto_1
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 108
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ", address: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Ljp/colopl/api/docomo/Feature;->address:Ljava/lang/String;

    if-nez v2, :cond_2

    const-string v2, "null"

    goto :goto_2

    :cond_2
    iget-object v2, p0, Ljp/colopl/api/docomo/Feature;->address:Ljava/lang/String;

    :goto_2
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 109
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ", adrCode: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Ljp/colopl/api/docomo/Feature;->adrCode:Ljava/lang/String;

    if-nez v2, :cond_3

    const-string v2, "null"

    goto :goto_3

    :cond_3
    iget-object v2, p0, Ljp/colopl/api/docomo/Feature;->adrCode:Ljava/lang/String;

    :goto_3
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 110
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ", postCode: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Ljp/colopl/api/docomo/Feature;->postCode:Ljava/lang/String;

    if-nez v2, :cond_4

    const-string v2, "null"

    goto :goto_4

    :cond_4
    iget-object v2, p0, Ljp/colopl/api/docomo/Feature;->postCode:Ljava/lang/String;

    :goto_4
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v1, "]"

    .line 111
    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 112
    invoke-virtual {v0}, Ljava/lang/StringBuffer;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
