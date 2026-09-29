.class public Lcom/github/droidfu/http/CachedHttpRequest;
.super Ljava/lang/Object;
.source "CachedHttpRequest.java"

# interfaces
.implements Lcom/github/droidfu/http/BetterHttpRequest;


# instance fields
.field private url:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 0

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 12
    iput-object p1, p0, Lcom/github/droidfu/http/CachedHttpRequest;->url:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public varargs expecting([Ljava/lang/Integer;)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 0

    return-object p0
.end method

.method public getRequestUrl()Ljava/lang/String;
    .locals 1

    .line 16
    iget-object v0, p0, Lcom/github/droidfu/http/CachedHttpRequest;->url:Ljava/lang/String;

    return-object v0
.end method

.method public retries(I)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 0

    return-object p0
.end method

.method public send()Lcom/github/droidfu/http/BetterHttpResponse;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/net/ConnectException;
        }
    .end annotation

    .line 28
    new-instance v0, Lcom/github/droidfu/http/CachedHttpResponse;

    iget-object v1, p0, Lcom/github/droidfu/http/CachedHttpRequest;->url:Ljava/lang/String;

    invoke-direct {v0, v1}, Lcom/github/droidfu/http/CachedHttpResponse;-><init>(Ljava/lang/String;)V

    return-object v0
.end method

.method public unwrap()Lorg/apache/http/client/methods/HttpUriRequest;
    .locals 1

    const/4 v0, 0x0

    return-object v0
.end method

.method public withTimeout(I)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 0

    return-object p0
.end method
