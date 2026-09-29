.class public Lnet/gogame/gowrap/model/faq/Article;
.super Ljava/lang/Object;
.source "Article.java"


# static fields
.field private static final KEY_BODY:Ljava/lang/String; = "body"

.field private static final KEY_TITLE:Ljava/lang/String; = "title"


# instance fields
.field private body:Ljava/lang/String;

.field private title:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 20
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

    .line 31
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 33
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->BEGIN_OBJECT:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_3

    .line 34
    invoke-virtual {p1}, Landroid/util/JsonReader;->beginObject()V

    .line 35
    :goto_0
    invoke-virtual {p1}, Landroid/util/JsonReader;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_2

    .line 36
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextName()Ljava/lang/String;

    move-result-object v0

    const-string v1, "title"

    .line 37
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 38
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/faq/Article;->title:Ljava/lang/String;

    goto :goto_0

    :cond_0
    const-string v1, "body"

    .line 39
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 40
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/faq/Article;->body:Ljava/lang/String;

    goto :goto_0

    .line 42
    :cond_1
    invoke-virtual {p1}, Landroid/util/JsonReader;->skipValue()V

    goto :goto_0

    .line 45
    :cond_2
    invoke-virtual {p1}, Landroid/util/JsonReader;->endObject()V

    return-void

    .line 47
    :cond_3
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "object expected"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 24
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 26
    iput-object p1, p0, Lnet/gogame/gowrap/model/faq/Article;->title:Ljava/lang/String;

    .line 27
    iput-object p2, p0, Lnet/gogame/gowrap/model/faq/Article;->body:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getBody()Ljava/lang/String;
    .locals 1

    .line 60
    iget-object v0, p0, Lnet/gogame/gowrap/model/faq/Article;->body:Ljava/lang/String;

    return-object v0
.end method

.method public getTitle()Ljava/lang/String;
    .locals 1

    .line 52
    iget-object v0, p0, Lnet/gogame/gowrap/model/faq/Article;->title:Ljava/lang/String;

    return-object v0
.end method

.method public setBody(Ljava/lang/String;)V
    .locals 0

    .line 64
    iput-object p1, p0, Lnet/gogame/gowrap/model/faq/Article;->body:Ljava/lang/String;

    return-void
.end method

.method public setTitle(Ljava/lang/String;)V
    .locals 0

    .line 56
    iput-object p1, p0, Lnet/gogame/gowrap/model/faq/Article;->title:Ljava/lang/String;

    return-void
.end method
