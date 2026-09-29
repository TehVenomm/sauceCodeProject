.class Lcom/github/droidfu/http/HttpPut;
.super Lcom/github/droidfu/http/BetterHttpRequestBase;
.source "HttpPut.java"


# direct methods
.method constructor <init>(Lorg/apache/http/impl/client/AbstractHttpClient;Ljava/lang/String;Ljava/util/HashMap;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lorg/apache/http/impl/client/AbstractHttpClient;",
            "Ljava/lang/String;",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 27
    invoke-direct {p0, p1}, Lcom/github/droidfu/http/BetterHttpRequestBase;-><init>(Lorg/apache/http/impl/client/AbstractHttpClient;)V

    .line 28
    new-instance p1, Lorg/apache/http/client/methods/HttpPut;

    invoke-direct {p1, p2}, Lorg/apache/http/client/methods/HttpPut;-><init>(Ljava/lang/String;)V

    iput-object p1, p0, Lcom/github/droidfu/http/HttpPut;->request:Lorg/apache/http/client/methods/HttpUriRequest;

    .line 29
    invoke-virtual {p3}, Ljava/util/HashMap;->keySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Ljava/lang/String;

    .line 30
    iget-object v0, p0, Lcom/github/droidfu/http/HttpPut;->request:Lorg/apache/http/client/methods/HttpUriRequest;

    invoke-virtual {p3, p2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    invoke-interface {v0, p2, v1}, Lorg/apache/http/client/methods/HttpUriRequest;->setHeader(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method constructor <init>(Lorg/apache/http/impl/client/AbstractHttpClient;Ljava/lang/String;Lorg/apache/http/HttpEntity;Ljava/util/HashMap;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lorg/apache/http/impl/client/AbstractHttpClient;",
            "Ljava/lang/String;",
            "Lorg/apache/http/HttpEntity;",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 36
    invoke-direct {p0, p1}, Lcom/github/droidfu/http/BetterHttpRequestBase;-><init>(Lorg/apache/http/impl/client/AbstractHttpClient;)V

    .line 37
    new-instance p1, Lorg/apache/http/client/methods/HttpPut;

    invoke-direct {p1, p2}, Lorg/apache/http/client/methods/HttpPut;-><init>(Ljava/lang/String;)V

    iput-object p1, p0, Lcom/github/droidfu/http/HttpPut;->request:Lorg/apache/http/client/methods/HttpUriRequest;

    .line 38
    iget-object p1, p0, Lcom/github/droidfu/http/HttpPut;->request:Lorg/apache/http/client/methods/HttpUriRequest;

    check-cast p1, Lorg/apache/http/HttpEntityEnclosingRequest;

    invoke-interface {p1, p3}, Lorg/apache/http/HttpEntityEnclosingRequest;->setEntity(Lorg/apache/http/HttpEntity;)V

    .line 40
    iget-object p1, p0, Lcom/github/droidfu/http/HttpPut;->request:Lorg/apache/http/client/methods/HttpUriRequest;

    const-string p2, "Content-Type"

    invoke-interface {p3}, Lorg/apache/http/HttpEntity;->getContentType()Lorg/apache/http/Header;

    move-result-object p3

    invoke-interface {p3}, Lorg/apache/http/Header;->getValue()Ljava/lang/String;

    move-result-object p3

    invoke-interface {p1, p2, p3}, Lorg/apache/http/client/methods/HttpUriRequest;->setHeader(Ljava/lang/String;Ljava/lang/String;)V

    .line 41
    invoke-virtual {p4}, Ljava/util/HashMap;->keySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Ljava/lang/String;

    .line 42
    iget-object p3, p0, Lcom/github/droidfu/http/HttpPut;->request:Lorg/apache/http/client/methods/HttpUriRequest;

    invoke-virtual {p4, p2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    invoke-interface {p3, p2, v0}, Lorg/apache/http/client/methods/HttpUriRequest;->setHeader(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    return-void
.end method
