.class public Lnet/gogame/gowrap/model/faq/Category;
.super Ljava/lang/Object;
.source "Category.java"


# static fields
.field private static final KEY_DESCRIPTION:Ljava/lang/String; = "description"

.field private static final KEY_NAME:Ljava/lang/String; = "name"

.field private static final KEY_SECTIONS:Ljava/lang/String; = "sections"


# instance fields
.field private description:Ljava/lang/String;

.field private name:Ljava/lang/String;

.field private sections:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/faq/Section;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 24
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

    .line 36
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 38
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->BEGIN_OBJECT:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_7

    .line 39
    invoke-virtual {p1}, Landroid/util/JsonReader;->beginObject()V

    .line 40
    :goto_0
    invoke-virtual {p1}, Landroid/util/JsonReader;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_6

    .line 41
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextName()Ljava/lang/String;

    move-result-object v0

    const-string v1, "name"

    .line 42
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 43
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/faq/Category;->name:Ljava/lang/String;

    goto :goto_0

    :cond_0
    const-string v1, "description"

    .line 44
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 45
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/faq/Category;->description:Ljava/lang/String;

    goto :goto_0

    :cond_1
    const-string v1, "sections"

    .line 46
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_5

    .line 47
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->BEGIN_ARRAY:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_3

    .line 48
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/model/faq/Category;->sections:Ljava/util/List;

    .line 49
    invoke-virtual {p1}, Landroid/util/JsonReader;->beginArray()V

    .line 50
    :goto_1
    invoke-virtual {p1}, Landroid/util/JsonReader;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_2

    .line 51
    iget-object v0, p0, Lnet/gogame/gowrap/model/faq/Category;->sections:Ljava/util/List;

    new-instance v1, Lnet/gogame/gowrap/model/faq/Section;

    invoke-direct {v1, p1}, Lnet/gogame/gowrap/model/faq/Section;-><init>(Landroid/util/JsonReader;)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 53
    :cond_2
    invoke-virtual {p1}, Landroid/util/JsonReader;->endArray()V

    goto :goto_0

    .line 54
    :cond_3
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_4

    .line 55
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextNull()V

    const/4 v0, 0x0

    .line 56
    iput-object v0, p0, Lnet/gogame/gowrap/model/faq/Category;->sections:Ljava/util/List;

    goto :goto_0

    .line 58
    :cond_4
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "array or null expected"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 61
    :cond_5
    invoke-virtual {p1}, Landroid/util/JsonReader;->skipValue()V

    goto :goto_0

    .line 64
    :cond_6
    invoke-virtual {p1}, Landroid/util/JsonReader;->endObject()V

    return-void

    .line 66
    :cond_7
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "object expected"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/faq/Section;",
            ">;)V"
        }
    .end annotation

    .line 28
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 30
    iput-object p1, p0, Lnet/gogame/gowrap/model/faq/Category;->name:Ljava/lang/String;

    .line 31
    iput-object p2, p0, Lnet/gogame/gowrap/model/faq/Category;->description:Ljava/lang/String;

    .line 32
    iput-object p3, p0, Lnet/gogame/gowrap/model/faq/Category;->sections:Ljava/util/List;

    return-void
.end method


# virtual methods
.method public getDescription()Ljava/lang/String;
    .locals 1

    .line 79
    iget-object v0, p0, Lnet/gogame/gowrap/model/faq/Category;->description:Ljava/lang/String;

    return-object v0
.end method

.method public getName()Ljava/lang/String;
    .locals 1

    .line 71
    iget-object v0, p0, Lnet/gogame/gowrap/model/faq/Category;->name:Ljava/lang/String;

    return-object v0
.end method

.method public getSections()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/faq/Section;",
            ">;"
        }
    .end annotation

    .line 87
    iget-object v0, p0, Lnet/gogame/gowrap/model/faq/Category;->sections:Ljava/util/List;

    return-object v0
.end method

.method public setDescription(Ljava/lang/String;)V
    .locals 0

    .line 83
    iput-object p1, p0, Lnet/gogame/gowrap/model/faq/Category;->description:Ljava/lang/String;

    return-void
.end method

.method public setName(Ljava/lang/String;)V
    .locals 0

    .line 75
    iput-object p1, p0, Lnet/gogame/gowrap/model/faq/Category;->name:Ljava/lang/String;

    return-void
.end method

.method public setSections(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/faq/Section;",
            ">;)V"
        }
    .end annotation

    .line 91
    iput-object p1, p0, Lnet/gogame/gowrap/model/faq/Category;->sections:Ljava/util/List;

    return-void
.end method
