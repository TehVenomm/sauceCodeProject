.class public Lnet/gogame/gowrap/model/faq/Section;
.super Ljava/lang/Object;
.source "Section.java"


# static fields
.field private static final KEY_ARTICLES:Ljava/lang/String; = "articles"

.field private static final KEY_NAME:Ljava/lang/String; = "name"


# instance fields
.field private articles:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/faq/Article;",
            ">;"
        }
    .end annotation
.end field

.field private name:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public constructor <init>(Landroid/util/JsonReader;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 33
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 35
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->BEGIN_OBJECT:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_6

    .line 36
    invoke-virtual {p1}, Landroid/util/JsonReader;->beginObject()V

    .line 37
    :goto_0
    invoke-virtual {p1}, Landroid/util/JsonReader;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_5

    .line 38
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextName()Ljava/lang/String;

    move-result-object v0

    const-string v1, "name"

    .line 39
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 40
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/faq/Section;->name:Ljava/lang/String;

    goto :goto_0

    :cond_0
    const-string v1, "articles"

    .line 41
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_4

    .line 42
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->BEGIN_ARRAY:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_2

    .line 43
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/model/faq/Section;->articles:Ljava/util/List;

    .line 44
    invoke-virtual {p1}, Landroid/util/JsonReader;->beginArray()V

    .line 45
    :goto_1
    invoke-virtual {p1}, Landroid/util/JsonReader;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 46
    iget-object v0, p0, Lnet/gogame/gowrap/model/faq/Section;->articles:Ljava/util/List;

    new-instance v1, Lnet/gogame/gowrap/model/faq/Article;

    invoke-direct {v1, p1}, Lnet/gogame/gowrap/model/faq/Article;-><init>(Landroid/util/JsonReader;)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 48
    :cond_1
    invoke-virtual {p1}, Landroid/util/JsonReader;->endArray()V

    goto :goto_0

    .line 49
    :cond_2
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_3

    .line 50
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextNull()V

    const/4 v0, 0x0

    .line 51
    iput-object v0, p0, Lnet/gogame/gowrap/model/faq/Section;->articles:Ljava/util/List;

    goto :goto_0

    .line 53
    :cond_3
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "array or null expected"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 56
    :cond_4
    invoke-virtual {p1}, Landroid/util/JsonReader;->skipValue()V

    goto :goto_0

    .line 59
    :cond_5
    invoke-virtual {p1}, Landroid/util/JsonReader;->endObject()V

    return-void

    .line 61
    :cond_6
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "object expected"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/faq/Article;",
            ">;)V"
        }
    .end annotation

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 28
    iput-object p1, p0, Lnet/gogame/gowrap/model/faq/Section;->name:Ljava/lang/String;

    .line 29
    iput-object p2, p0, Lnet/gogame/gowrap/model/faq/Section;->articles:Ljava/util/List;

    return-void
.end method


# virtual methods
.method public getArticles()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/faq/Article;",
            ">;"
        }
    .end annotation

    .line 74
    iget-object v0, p0, Lnet/gogame/gowrap/model/faq/Section;->articles:Ljava/util/List;

    return-object v0
.end method

.method public getName()Ljava/lang/String;
    .locals 1

    .line 66
    iget-object v0, p0, Lnet/gogame/gowrap/model/faq/Section;->name:Ljava/lang/String;

    return-object v0
.end method

.method public setArticles(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/faq/Article;",
            ">;)V"
        }
    .end annotation

    .line 78
    iput-object p1, p0, Lnet/gogame/gowrap/model/faq/Section;->articles:Ljava/util/List;

    return-void
.end method

.method public setName(Ljava/lang/String;)V
    .locals 0

    .line 70
    iput-object p1, p0, Lnet/gogame/gowrap/model/faq/Section;->name:Ljava/lang/String;

    return-void
.end method
