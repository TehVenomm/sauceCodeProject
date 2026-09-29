.class public Lnet/gogame/gowrap/model/news/MarkupElement;
.super Lnet/gogame/gowrap/support/BaseJsonObject;
.source "MarkupElement.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;
    }
.end annotation


# static fields
.field private static final KEY_CHILDREN:Ljava/lang/String; = "children"

.field private static final KEY_LINK:Ljava/lang/String; = "link"

.field private static final KEY_SRC:Ljava/lang/String; = "src"

.field private static final KEY_STYLE:Ljava/lang/String; = "style"

.field private static final KEY_TEXT:Ljava/lang/String; = "text"

.field private static final KEY_TEXT_STYLES:Ljava/lang/String; = "textStyles"

.field private static final KEY_TYPE:Ljava/lang/String; = "type"


# instance fields
.field private children:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/MarkupElement;",
            ">;"
        }
    .end annotation
.end field

.field private link:Ljava/lang/String;

.field private src:Ljava/lang/String;

.field private style:Ljava/lang/String;

.field private text:Ljava/lang/String;

.field private textStyles:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;",
            ">;"
        }
    .end annotation
.end field

.field private type:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 33
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

    .line 37
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

    const-string v0, "type"

    .line 42
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    .line 43
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->type:Ljava/lang/String;

    return v1

    :cond_0
    const-string v0, "children"

    .line 45
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    const/4 v2, 0x0

    if-eqz v0, :cond_5

    .line 46
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_1

    .line 47
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextNull()V

    goto :goto_1

    .line 48
    :cond_1
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->BEGIN_ARRAY:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_4

    .line 49
    invoke-virtual {p1}, Landroid/util/JsonReader;->beginArray()V

    .line 50
    new-instance p2, Ljava/util/ArrayList;

    invoke-direct {p2}, Ljava/util/ArrayList;-><init>()V

    iput-object p2, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->children:Ljava/util/List;

    .line 51
    :goto_0
    invoke-virtual {p1}, Landroid/util/JsonReader;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_3

    .line 52
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_2

    .line 53
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextNull()V

    .line 54
    iget-object p2, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->children:Ljava/util/List;

    invoke-interface {p2, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 56
    :cond_2
    iget-object p2, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->children:Ljava/util/List;

    new-instance v0, Lnet/gogame/gowrap/model/news/MarkupElement;

    invoke-direct {v0, p1}, Lnet/gogame/gowrap/model/news/MarkupElement;-><init>(Landroid/util/JsonReader;)V

    invoke-interface {p2, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 59
    :cond_3
    invoke-virtual {p1}, Landroid/util/JsonReader;->endArray()V

    :goto_1
    return v1

    .line 61
    :cond_4
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "array or null expected"

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_5
    const-string v0, "text"

    .line 64
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_6

    .line 65
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->text:Ljava/lang/String;

    return v1

    :cond_6
    const-string v0, "textStyles"

    .line 67
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_e

    .line 68
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_7

    .line 69
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextNull()V

    goto/16 :goto_3

    .line 70
    :cond_7
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->BEGIN_ARRAY:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_d

    .line 71
    invoke-virtual {p1}, Landroid/util/JsonReader;->beginArray()V

    .line 72
    new-instance p2, Ljava/util/ArrayList;

    invoke-direct {p2}, Ljava/util/ArrayList;-><init>()V

    iput-object p2, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->textStyles:Ljava/util/List;

    .line 73
    :goto_2
    invoke-virtual {p1}, Landroid/util/JsonReader;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_c

    .line 74
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_8

    .line 75
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextNull()V

    .line 76
    iget-object p2, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->textStyles:Ljava/util/List;

    invoke-interface {p2, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 77
    :cond_8
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->STRING:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_b

    .line 78
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextString()Ljava/lang/String;

    move-result-object p2

    const-string v0, "bold"

    .line 79
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_9

    .line 80
    iget-object p2, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->textStyles:Ljava/util/List;

    sget-object v0, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->BOLD:Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    invoke-interface {p2, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    :cond_9
    const-string v0, "italic"

    .line 81
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_a

    .line 82
    iget-object p2, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->textStyles:Ljava/util/List;

    sget-object v0, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->ITALIC:Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    invoke-interface {p2, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 84
    :cond_a
    new-instance p1, Ljava/lang/IllegalArgumentException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "unknown textStyle: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 87
    :cond_b
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "string or null expected"

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 90
    :cond_c
    invoke-virtual {p1}, Landroid/util/JsonReader;->endArray()V

    :goto_3
    return v1

    .line 92
    :cond_d
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "array or null expected"

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_e
    const-string v0, "link"

    .line 95
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_f

    .line 96
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->link:Ljava/lang/String;

    return v1

    :cond_f
    const-string v0, "src"

    .line 98
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_10

    .line 99
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->src:Ljava/lang/String;

    return v1

    :cond_10
    const-string v0, "style"

    .line 101
    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p2

    if-eqz p2, :cond_12

    .line 102
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object p2

    sget-object v0, Landroid/util/JsonToken;->NUMBER:Landroid/util/JsonToken;

    if-ne p2, v0, :cond_11

    .line 103
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextLong()J

    move-result-wide p1

    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->style:Ljava/lang/String;

    goto :goto_4

    .line 105
    :cond_11
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->style:Ljava/lang/String;

    :goto_4
    return v1

    :cond_12
    const/4 p1, 0x0

    return p1
.end method

.method public getChildren()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/MarkupElement;",
            ">;"
        }
    .end annotation

    .line 121
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->children:Ljava/util/List;

    return-object v0
.end method

.method public getLink()Ljava/lang/String;
    .locals 1

    .line 145
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->link:Ljava/lang/String;

    return-object v0
.end method

.method public getSrc()Ljava/lang/String;
    .locals 1

    .line 153
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->src:Ljava/lang/String;

    return-object v0
.end method

.method public getStyle()Ljava/lang/String;
    .locals 1

    .line 161
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->style:Ljava/lang/String;

    return-object v0
.end method

.method public getText()Ljava/lang/String;
    .locals 1

    .line 129
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->text:Ljava/lang/String;

    return-object v0
.end method

.method public getTextStyles()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;",
            ">;"
        }
    .end annotation

    .line 137
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->textStyles:Ljava/util/List;

    return-object v0
.end method

.method public getType()Ljava/lang/String;
    .locals 1

    .line 113
    iget-object v0, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->type:Ljava/lang/String;

    return-object v0
.end method

.method public setChildren(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/MarkupElement;",
            ">;)V"
        }
    .end annotation

    .line 125
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->children:Ljava/util/List;

    return-void
.end method

.method public setLink(Ljava/lang/String;)V
    .locals 0

    .line 149
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->link:Ljava/lang/String;

    return-void
.end method

.method public setSrc(Ljava/lang/String;)V
    .locals 0

    .line 157
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->src:Ljava/lang/String;

    return-void
.end method

.method public setStyle(Ljava/lang/String;)V
    .locals 0

    .line 165
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->style:Ljava/lang/String;

    return-void
.end method

.method public setText(Ljava/lang/String;)V
    .locals 0

    .line 133
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->text:Ljava/lang/String;

    return-void
.end method

.method public setTextStyles(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;",
            ">;)V"
        }
    .end annotation

    .line 141
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->textStyles:Ljava/util/List;

    return-void
.end method

.method public setType(Ljava/lang/String;)V
    .locals 0

    .line 117
    iput-object p1, p0, Lnet/gogame/gowrap/model/news/MarkupElement;->type:Ljava/lang/String;

    return-void
.end method
