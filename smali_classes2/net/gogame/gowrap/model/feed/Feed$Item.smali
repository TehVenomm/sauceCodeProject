.class public Lnet/gogame/gowrap/model/feed/Feed$Item;
.super Ljava/lang/Object;
.source "Feed.java"

# interfaces
.implements Ljava/io/Serializable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/model/feed/Feed;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Item"
.end annotation


# static fields
.field private static final KEY_ARTICLE_LINK:Ljava/lang/String; = "articleLink"

.field private static final KEY_CREATED_TIME:Ljava/lang/String; = "createdTime"

.field private static final KEY_ID:Ljava/lang/String; = "id"

.field private static final KEY_LINK:Ljava/lang/String; = "link"

.field private static final KEY_MEDIA_HEIGHT:Ljava/lang/String; = "mediaHeight"

.field private static final KEY_MEDIA_PREVIEW:Ljava/lang/String; = "mediaPreview"

.field private static final KEY_MEDIA_SOURCE:Ljava/lang/String; = "mediaSource"

.field private static final KEY_MEDIA_WIDTH:Ljava/lang/String; = "mediaWidth"

.field private static final KEY_MESSAGE:Ljava/lang/String; = "message"

.field private static final KEY_TYPE:Ljava/lang/String; = "type"

.field private static final KEY_UPDATED_TIME:Ljava/lang/String; = "updatedTime"


# instance fields
.field private articleLink:Ljava/lang/String;

.field private createdTime:Ljava/lang/Long;

.field private id:Ljava/lang/String;

.field private link:Ljava/lang/String;

.field private mediaHeight:Ljava/lang/Integer;

.field private mediaPreview:Ljava/lang/String;

.field private mediaSource:Ljava/lang/String;

.field private mediaWidth:Ljava/lang/Integer;

.field private message:Ljava/lang/String;

.field private type:Ljava/lang/String;

.field private updatedTime:Ljava/lang/Long;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 90
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

    .line 94
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 96
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->BEGIN_OBJECT:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_c

    .line 97
    invoke-virtual {p1}, Landroid/util/JsonReader;->beginObject()V

    .line 98
    :goto_0
    invoke-virtual {p1}, Landroid/util/JsonReader;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_b

    .line 99
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextName()Ljava/lang/String;

    move-result-object v0

    const-string v1, "id"

    .line 100
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 101
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->id:Ljava/lang/String;

    goto :goto_0

    :cond_0
    const-string v1, "createdTime"

    .line 102
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 103
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optLong(Landroid/util/JsonReader;)Ljava/lang/Long;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->createdTime:Ljava/lang/Long;

    goto :goto_0

    :cond_1
    const-string v1, "updatedTime"

    .line 104
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_2

    .line 105
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optLong(Landroid/util/JsonReader;)Ljava/lang/Long;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->updatedTime:Ljava/lang/Long;

    goto :goto_0

    :cond_2
    const-string v1, "type"

    .line 106
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_3

    .line 107
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->type:Ljava/lang/String;

    goto :goto_0

    :cond_3
    const-string v1, "mediaWidth"

    .line 108
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_4

    .line 109
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optInt(Landroid/util/JsonReader;)Ljava/lang/Integer;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->mediaWidth:Ljava/lang/Integer;

    goto :goto_0

    :cond_4
    const-string v1, "mediaHeight"

    .line 110
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_5

    .line 111
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optInt(Landroid/util/JsonReader;)Ljava/lang/Integer;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->mediaHeight:Ljava/lang/Integer;

    goto :goto_0

    :cond_5
    const-string v1, "mediaPreview"

    .line 112
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_6

    .line 113
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->mediaPreview:Ljava/lang/String;

    goto :goto_0

    :cond_6
    const-string v1, "mediaSource"

    .line 114
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_7

    .line 115
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->mediaSource:Ljava/lang/String;

    goto/16 :goto_0

    :cond_7
    const-string v1, "link"

    .line 116
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_8

    .line 117
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->link:Ljava/lang/String;

    goto/16 :goto_0

    :cond_8
    const-string v1, "articleLink"

    .line 118
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_9

    .line 119
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->articleLink:Ljava/lang/String;

    goto/16 :goto_0

    :cond_9
    const-string v1, "message"

    .line 120
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_a

    .line 121
    invoke-static {p1}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Landroid/util/JsonReader;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->message:Ljava/lang/String;

    goto/16 :goto_0

    .line 123
    :cond_a
    invoke-virtual {p1}, Landroid/util/JsonReader;->skipValue()V

    goto/16 :goto_0

    .line 126
    :cond_b
    invoke-virtual {p1}, Landroid/util/JsonReader;->endObject()V

    return-void

    .line 128
    :cond_c
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Invalid news feed"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method


# virtual methods
.method public getArticleLink()Ljava/lang/String;
    .locals 1

    .line 205
    iget-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->articleLink:Ljava/lang/String;

    return-object v0
.end method

.method public getCreatedTime()Ljava/lang/Long;
    .locals 1

    .line 141
    iget-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->createdTime:Ljava/lang/Long;

    return-object v0
.end method

.method public getId()Ljava/lang/String;
    .locals 1

    .line 133
    iget-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->id:Ljava/lang/String;

    return-object v0
.end method

.method public getLink()Ljava/lang/String;
    .locals 1

    .line 197
    iget-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->link:Ljava/lang/String;

    return-object v0
.end method

.method public getMediaHeight()Ljava/lang/Integer;
    .locals 1

    .line 173
    iget-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->mediaHeight:Ljava/lang/Integer;

    return-object v0
.end method

.method public getMediaPreview()Ljava/lang/String;
    .locals 1

    .line 181
    iget-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->mediaPreview:Ljava/lang/String;

    return-object v0
.end method

.method public getMediaSource()Ljava/lang/String;
    .locals 1

    .line 189
    iget-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->mediaSource:Ljava/lang/String;

    return-object v0
.end method

.method public getMediaWidth()Ljava/lang/Integer;
    .locals 1

    .line 165
    iget-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->mediaWidth:Ljava/lang/Integer;

    return-object v0
.end method

.method public getMessage()Ljava/lang/String;
    .locals 1

    .line 213
    iget-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->message:Ljava/lang/String;

    return-object v0
.end method

.method public getType()Ljava/lang/String;
    .locals 1

    .line 157
    iget-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->type:Ljava/lang/String;

    return-object v0
.end method

.method public getUpdatedTime()Ljava/lang/Long;
    .locals 1

    .line 149
    iget-object v0, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->updatedTime:Ljava/lang/Long;

    return-object v0
.end method

.method public setArticleLink(Ljava/lang/String;)V
    .locals 0

    .line 209
    iput-object p1, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->articleLink:Ljava/lang/String;

    return-void
.end method

.method public setCreatedTime(Ljava/lang/Long;)V
    .locals 0

    .line 145
    iput-object p1, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->createdTime:Ljava/lang/Long;

    return-void
.end method

.method public setId(Ljava/lang/String;)V
    .locals 0

    .line 137
    iput-object p1, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->id:Ljava/lang/String;

    return-void
.end method

.method public setLink(Ljava/lang/String;)V
    .locals 0

    .line 201
    iput-object p1, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->link:Ljava/lang/String;

    return-void
.end method

.method public setMediaHeight(Ljava/lang/Integer;)V
    .locals 0

    .line 177
    iput-object p1, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->mediaHeight:Ljava/lang/Integer;

    return-void
.end method

.method public setMediaPreview(Ljava/lang/String;)V
    .locals 0

    .line 185
    iput-object p1, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->mediaPreview:Ljava/lang/String;

    return-void
.end method

.method public setMediaSource(Ljava/lang/String;)V
    .locals 0

    .line 193
    iput-object p1, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->mediaSource:Ljava/lang/String;

    return-void
.end method

.method public setMediaWidth(Ljava/lang/Integer;)V
    .locals 0

    .line 169
    iput-object p1, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->mediaWidth:Ljava/lang/Integer;

    return-void
.end method

.method public setMessage(Ljava/lang/String;)V
    .locals 0

    .line 217
    iput-object p1, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->message:Ljava/lang/String;

    return-void
.end method

.method public setType(Ljava/lang/String;)V
    .locals 0

    .line 161
    iput-object p1, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->type:Ljava/lang/String;

    return-void
.end method

.method public setUpdatedTime(Ljava/lang/Long;)V
    .locals 0

    .line 153
    iput-object p1, p0, Lnet/gogame/gowrap/model/feed/Feed$Item;->updatedTime:Ljava/lang/Long;

    return-void
.end method
