.class final Ljp/colopl/util/DoneHandlerInputStream;
.super Ljava/io/FilterInputStream;
.source "HTTP.java"


# instance fields
.field private done:Z


# direct methods
.method public constructor <init>(Ljava/io/InputStream;)V
    .locals 0

    .line 66
    invoke-direct {p0, p1}, Ljava/io/FilterInputStream;-><init>(Ljava/io/InputStream;)V

    return-void
.end method


# virtual methods
.method public read([BII)I
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 71
    iget-boolean v0, p0, Ljp/colopl/util/DoneHandlerInputStream;->done:Z

    const/4 v1, -0x1

    if-nez v0, :cond_0

    .line 72
    invoke-super {p0, p1, p2, p3}, Ljava/io/FilterInputStream;->read([BII)I

    move-result p1

    if-eq p1, v1, :cond_0

    return p1

    :cond_0
    const/4 p1, 0x1

    .line 77
    iput-boolean p1, p0, Ljp/colopl/util/DoneHandlerInputStream;->done:Z

    return v1
.end method
