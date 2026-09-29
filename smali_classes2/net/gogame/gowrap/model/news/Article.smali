.class public Lnet/gogame/gowrap/model/news/Article;
.super Lnet/gogame/gowrap/support/BaseJsonObject;
.source "Article.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/model/news/Article$Category;
    }
.end annotation


# static fields
.field private static final KEY_CATEGORY:Ljava/lang/String; = "category"

.field private static final KEY_CONTENT:Ljava/lang/String; = "content"

.field private static final KEY_DATE_TIME:Ljava/lang/String; = "dateTime"

.field private static final KEY_END_DATE_TIME:Ljava/lang/String; = "endDateTime"

.field private static final KEY_ID:Ljava/lang/String; = "id"

.field private static final KEY_START_DATE_TIME:Ljava/lang/String; = "startDateTime"

.field private static final KEY_TITLE:Ljava/lang/String; = "title"


# instance fields
.field private category:Lnet/gogame/gowrap/model/news/Article$Category;

.field private content:Lnet/gogame/gowrap/model/news/MarkupElement;

.field private dateTime:Ljava/lang/Long;

.field private endDateTime:Ljava/lang/Long;

.field private id:J

.field private startDateTime:Ljava/lang/Long;

.field private title:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 31
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

    .line 35
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/support/BaseJsonObject;-><init>(Landroid/util/JsonReader;)V

    return-void
.end method

.method private getCategory(Ljava/lang/String;)Lnet/gogame/gowrap/model/news/Article$Category;
    .locals 6

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return-object v0

    .line 42
    :cond_0
    invoke-static {}, Lnet/gogame/gowrap/model/news/Article$Category;->values()[Lnet/gogame/gowrap/model/news/Article$Category;

    move-result-object v1

    array-length v2, v1

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v2, :cond_2

    aget-object v4, v1, v3

    .line 43
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/news/Article$Category;->name()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {p1, v5}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_1

    return-object v4

    :cond_1
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :cond_2
    return-object v0
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

    .line 52
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    .line 53
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextLong()J

    move-result-wide p1

    iput-wide p1, p0, Lnet/gogame/gowrap/model/news/Article;->id:J

    return v1

    :cond_0
    const-string v0, "startDateTime"

    .line 55
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 56
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optLong(Landroid/util/JsonReader;)Ljava/lang/Long;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Article;->startDateTime:Ljava/lang/Long;

    return v1

    :cond_1
    const-string v0, "endDateTime"

    .line 58
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 59
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optLong(Landroid/util/JsonReader;)Ljava/lang/Long;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Article;->endDateTime:Ljava/lang/Long;

    return v1

    :cond_2
    const-string v0, "dateTime"

    .line 61
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_3

    .line 62
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optLong(Landroid/util/JsonReader;)Ljava/lang/Long;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Article;->dateTime:Ljava/lang/Long;

    return v1

    :cond_3
    const-string v0, "category"

    .line 64
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_4

    .line 65
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Lnet/gogame/gowrap/model/news/Article;->getCategory(Ljava/lang/String;)Lnet/gogame/gowrap/model/news/Article$Category;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Article;->category:Lnet/gogame/gowrap/model/news/Article$Category;

    return v1

    :cond_4
    const-string v0, "title"

    .line 67
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_5

    .line 68
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Article;->title:Ljava/lang/String;

    return v1

    :cond_5
    const-string v0, "content"

    .line 70
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p2

    if-eqz p2, :cond_7

    .line 71
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_6

    .line 72
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextNull()V

    const/4 p1, 0x0

    .line 73
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Article;->content:Lnet/gogame/gowrap/model/news/MarkupElement;

    goto :goto_0

    .line 75
    :cond_6
    new-instance p2, Lnet/gogame/gowrap/model/news/MarkupElement;

    invoke-direct {p2, p1}, Lnet/gogame/gowrap/model/news/MarkupElement;-><init>(Landroid/util/JsonReader;)V

    iput-object p2, p0, Lnet/gogame/gowrap/model/news/Article;->content:Lnet/gogame/gowrap/model/news/MarkupElement;

    :goto_0
    return v1

    :cond_7
    const/4 p1, 0x0

    return p1
.end method

.method public getCategory()Lnet/gogame/gowrap/model/news/Article$Category;
    .locals 1

    .line 115
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/Article;->category:Lnet/gogame/gowrap/model/news/Article$Category;

    return-object v0
.end method

.method public getContent()Lnet/gogame/gowrap/model/news/MarkupElement;
    .locals 1

    .line 131
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/Article;->content:Lnet/gogame/gowrap/model/news/MarkupElement;

    return-object v0
.end method

.method public getDateTime()Ljava/lang/Long;
    .locals 1

    .line 91
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/Article;->dateTime:Ljava/lang/Long;

    return-object v0
.end method

.method public getEndDateTime()Ljava/lang/Long;
    .locals 1

    .line 107
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/Article;->endDateTime:Ljava/lang/Long;

    return-object v0
.end method

.method public getId()J
    .locals 2

    .line 83
    iget-wide v0, p0, Lnet/gogame/gowrap/model/news/Article;->id:J

    return-wide v0
.end method

.method public getStartDateTime()Ljava/lang/Long;
    .locals 1

    .line 99
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/Article;->startDateTime:Ljava/lang/Long;

    return-object v0
.end method

.method public getTitle()Ljava/lang/String;
    .locals 1

    .line 123
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/Article;->title:Ljava/lang/String;

    return-object v0
.end method

.method public setCategory(Lnet/gogame/gowrap/model/news/Article$Category;)V
    .locals 0

    .line 119
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Article;->category:Lnet/gogame/gowrap/model/news/Article$Category;

    return-void
.end method

.method public setContent(Lnet/gogame/gowrap/model/news/MarkupElement;)V
    .locals 0

    .line 135
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Article;->content:Lnet/gogame/gowrap/model/news/MarkupElement;

    return-void
.end method

.method public setDateTime(Ljava/lang/Long;)V
    .locals 0

    .line 95
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Article;->dateTime:Ljava/lang/Long;

    return-void
.end method

.method public setEndDateTime(Ljava/lang/Long;)V
    .locals 0

    .line 111
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Article;->endDateTime:Ljava/lang/Long;

    return-void
.end method

.method public setId(J)V
    .locals 0

    .line 87
    iput-wide p1, p0, Lnet/gogame/gowrap/model/news/Article;->id:J

    return-void
.end method

.method public setStartDateTime(Ljava/lang/Long;)V
    .locals 0

    .line 103
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Article;->startDateTime:Ljava/lang/Long;

    return-void
.end method

.method public setTitle(Ljava/lang/String;)V
    .locals 0

    .line 127
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/Article;->title:Ljava/lang/String;

    return-void
.end method
