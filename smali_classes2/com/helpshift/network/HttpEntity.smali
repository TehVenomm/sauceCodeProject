.class public Lcom/helpshift/network/HttpEntity;
.super Ljava/lang/Object;
.source "HttpEntity.java"


# instance fields
.field public content:Ljava/io/InputStream;

.field public contentLength:J


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 6
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public consumeContent()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 11
    iget-object v0, p0, Lcom/helpshift/network/HttpEntity;->content:Ljava/io/InputStream;

    if-eqz v0, :cond_0

    .line 12
    iget-object v0, p0, Lcom/helpshift/network/HttpEntity;->content:Ljava/io/InputStream;

    invoke-virtual {v0}, Ljava/io/InputStream;->close()V

    :cond_0
    return-void
.end method
