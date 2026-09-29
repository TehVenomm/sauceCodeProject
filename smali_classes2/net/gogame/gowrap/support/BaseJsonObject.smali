.class public abstract Lnet/gogame/gowrap/support/BaseJsonObject;
.super Ljava/lang/Object;
.source "BaseJsonObject.java"

# interfaces
.implements Ljava/io/Serializable;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 12
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

    .line 16
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 18
    invoke-virtual {p1}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->BEGIN_OBJECT:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_2

    .line 19
    invoke-virtual {p1}, Landroid/util/JsonReader;->beginObject()V

    .line 20
    :goto_0
    invoke-virtual {p1}, Landroid/util/JsonReader;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 21
    invoke-virtual {p1}, Landroid/util/JsonReader;->nextName()Ljava/lang/String;

    move-result-object v0

    .line 22
    invoke-virtual {p0, p1, v0}, Lnet/gogame/gowrap/support/BaseJsonObject;->doParse(Landroid/util/JsonReader;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 25
    :cond_0
    invoke-virtual {p1}, Landroid/util/JsonReader;->skipValue()V

    goto :goto_0

    .line 28
    :cond_1
    invoke-virtual {p1}, Landroid/util/JsonReader;->endObject()V

    return-void

    .line 30
    :cond_2
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Invalid object"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method


# virtual methods
.method protected abstract doParse(Landroid/util/JsonReader;Ljava/lang/String;)Z
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation
.end method
