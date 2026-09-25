.class public Lnet/gogame/gowrap/model/news/NewsFeed;
.super Lnet/gogame/gowrap/support/BaseJsonObject;
.source "NewsFeed.java"


# static fields
.field private static final KEY_ARTICLES:Ljava/lang/String; = "articles"

.field private static final KEY_BANNERS:Ljava/lang/String; = "banners"


# instance fields
.field private articles:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/Article;",
            ">;"
        }
    .end annotation
.end field

.field private banners:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/Banner;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 22
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

    .line 26
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/support/BaseJsonObject;-><init>(Landroid/util/JsonReader;)V

    return-void
.end method


# virtual methods
.method protected doParse(Landroid/util/JsonReader;Ljava/lang/String;)Z
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "banners"

    .line 31
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    const/4 v2, 0x1

    if-eqz v0, :cond_4

    .line 32
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_0

    .line 33
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextNull()V

    goto :goto_1

    .line 34
    :cond_0
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->BEGIN_ARRAY:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_3

    .line 35
    invoke-virtual {p1}, Landroid/util/JsonReader;->beginArray()V

    .line 36
    new-instance p2, Ljava/util/ArrayList;

    invoke-direct {p2}, Ljava/util/ArrayList;-><init>()V

    iput-object p2, p0, Lnet/gogame/gowrap/model/news/NewsFeed;->banners:Ljava/util/List;

    .line 37
    :goto_0
    invoke-virtual {p1}, Landroid/util/JsonReader;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_2

    .line 38
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_1

    .line 39
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextNull()V

    .line 40
    iget-object p2, p0, Lnet/gogame/gowrap/model/news/NewsFeed;->banners:Ljava/util/List;

    invoke-interface {p2, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 42
    :cond_1
    iget-object p2, p0, Lnet/gogame/gowrap/model/news/NewsFeed;->banners:Ljava/util/List;

    new-instance v0, Lnet/gogame/gowrap/model/news/Banner;

    invoke-direct {v0, p1}, Lnet/gogame/gowrap/model/news/Banner;-><init>(Landroid/util/JsonReader;)V

    invoke-interface {p2, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 45
    :cond_2
    invoke-virtual {p1}, Landroid/util/JsonReader;->endArray()V

    :goto_1
    return v2

    .line 47
    :cond_3
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "array or null expected"

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_4
    const-string v0, "articles"

    .line 50
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p2

    if-eqz p2, :cond_9

    .line 51
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_5

    .line 52
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextNull()V

    goto :goto_3

    .line 53
    :cond_5
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->BEGIN_ARRAY:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_8

    .line 54
    invoke-virtual {p1}, Landroid/util/JsonReader;->beginArray()V

    .line 55
    new-instance p2, Ljava/util/ArrayList;

    invoke-direct {p2}, Ljava/util/ArrayList;-><init>()V

    iput-object p2, p0, Lnet/gogame/gowrap/model/news/NewsFeed;->articles:Ljava/util/List;

    .line 56
    :goto_2
    invoke-virtual {p1}, Landroid/util/JsonReader;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_7

    .line 57
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_6

    .line 58
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextNull()V

    .line 59
    iget-object p2, p0, Lnet/gogame/gowrap/model/news/NewsFeed;->articles:Ljava/util/List;

    invoke-interface {p2, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 61
    :cond_6
    iget-object p2, p0, Lnet/gogame/gowrap/model/news/NewsFeed;->articles:Ljava/util/List;

    new-instance v0, Lnet/gogame/gowrap/model/news/Article;

    invoke-direct {v0, p1}, Lnet/gogame/gowrap/model/news/Article;-><init>(Landroid/util/JsonReader;)V

    invoke-interface {p2, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 64
    :cond_7
    invoke-virtual {p1}, Landroid/util/JsonReader;->endArray()V

    :goto_3
    return v2

    .line 66
    :cond_8
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "array or null expected"

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_9
    const/4 p1, 0x0

    return p1
.end method

.method public getArticles()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/Article;",
            ">;"
        }
    .end annotation

    .line 82
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/NewsFeed;->articles:Ljava/util/List;

    return-object v0
.end method

.method public getBanners()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/Banner;",
            ">;"
        }
    .end annotation

    .line 74
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/NewsFeed;->banners:Ljava/util/List;

    return-object v0
.end method

.method public setArticles(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/Article;",
            ">;)V"
        }
    .end annotation

    .line 86
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/NewsFeed;->articles:Ljava/util/List;

    return-void
.end method

.method public setBanners(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/Banner;",
            ">;)V"
        }
    .end annotation

    .line 78
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/NewsFeed;->banners:Ljava/util/List;

    return-void
.end method
