.class public Lnet/gogame/gowrap/model/news/Banner;
.super Lnet/gogame/gowrap/support/BaseJsonObject;
.source "Banner.java"


# static fields
.field private static final KEY_END_DATE_TIME:Ljava/lang/String; = "endDateTime"

.field private static final KEY_ID:Ljava/lang/String; = "id"

.field private static final KEY_IMAGE_URL:Ljava/lang/String; = "imageUrl"

.field private static final KEY_LINK:Ljava/lang/String; = "link"

.field private static final KEY_START_DATE_TIME:Ljava/lang/String; = "startDateTime"


# instance fields
.field private endDateTime:Ljava/lang/Long;

.field private id:J

.field private imageUrl:Ljava/lang/String;

.field private link:Ljava/lang/String;

.field private startDateTime:Ljava/lang/Long;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 26
    invoke-direct {p0}, Lnet/gogame/gowrap/support/BaseJsonObject;-><init>()V

    return-void
.end method

.method public constructor <init>(Landroid/util/JsonReader;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 30
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/support/BaseJsonObject;-><init>(Landroid/util/JsonReader;)V

    return-void
.end method


# virtual methods
.method protected doParse(Landroid/util/JsonReader;Ljava/lang/String;)Z
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "id"

    .line 35
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    .line 36
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextLong()J

    move-result-wide p1

    iput-wide p1, p0, Lnet/gogame/gowrap/model/news/Banner;->id:J

    return v1

    :cond_0
    const-string v0, "startDateTime"

    .line 38
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 39
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optLong(Landroid/util/JsonReader;)Ljava/lang/Long;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Banner;->startDateTime:Ljava/lang/Long;

    return v1

    :cond_1
    const-string v0, "endDateTime"

    .line 41
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 42
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optLong(Landroid/util/JsonReader;)Ljava/lang/Long;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Banner;->endDateTime:Ljava/lang/Long;

    return v1

    :cond_2
    const-string v0, "link"

    .line 44
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_3

    .line 45
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Banner;->link:Ljava/lang/String;

    return v1

    :cond_3
    const-string v0, "imageUrl"

    .line 47
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p2

    if-eqz p2, :cond_4

    .line 48
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Banner;->imageUrl:Ljava/lang/String;

    return v1

    :cond_4
    const/4 p1, 0x0

    return p1
.end method

.method public getEndDateTime()Ljava/lang/Long;
    .locals 1

    .line 71
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/Banner;->endDateTime:Ljava/lang/Long;

    return-object v0
.end method

.method public getId()J
    .locals 2

    .line 55
    iget-wide v0, p0, Lnet/gogame/gowrap/model/news/Banner;->id:J

    return-wide v0
.end method

.method public getImageUrl()Ljava/lang/String;
    .locals 1

    .line 87
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/Banner;->imageUrl:Ljava/lang/String;

    return-object v0
.end method

.method public getLink()Ljava/lang/String;
    .locals 1

    .line 79
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/Banner;->link:Ljava/lang/String;

    return-object v0
.end method

.method public getStartDateTime()Ljava/lang/Long;
    .locals 1

    .line 63
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/Banner;->startDateTime:Ljava/lang/Long;

    return-object v0
.end method

.method public setEndDateTime(Ljava/lang/Long;)V
    .locals 0

    .line 75
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Banner;->endDateTime:Ljava/lang/Long;

    return-void
.end method

.method public setId(J)V
    .locals 0

    .line 59
    iput-wide p1, p0, Lnet/gogame/gowrap/model/news/Banner;->id:J

    return-void
.end method

.method public setImageUrl(Ljava/lang/String;)V
    .locals 0

    .line 91
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Banner;->imageUrl:Ljava/lang/String;

    return-void
.end method

.method public setLink(Ljava/lang/String;)V
    .locals 0

    .line 83
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Banner;->link:Ljava/lang/String;

    return-void
.end method

.method public setStartDateTime(Ljava/lang/Long;)V
    .locals 0

    .line 67
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Banner;->startDateTime:Ljava/lang/Long;

    return-void
.end method
