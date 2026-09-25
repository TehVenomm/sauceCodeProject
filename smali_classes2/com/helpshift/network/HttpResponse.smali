.class public Lcom/helpshift/network/HttpResponse;
.super Ljava/lang/Object;
.source "HttpResponse.java"


# instance fields
.field private entity:Lcom/helpshift/network/HttpEntity;

.field private headers:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/network/Header;",
            ">;"
        }
    .end annotation
.end field

.field private helpshiftSSLSocketFactory:Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

.field private statusLine:Lcom/helpshift/network/StatusLine;


# direct methods
.method public constructor <init>(Lcom/helpshift/network/StatusLine;)V
    .locals 1

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    if-eqz p1, :cond_0

    .line 18
    iput-object p1, p0, Lcom/helpshift/network/HttpResponse;->statusLine:Lcom/helpshift/network/StatusLine;

    .line 19
    new-instance p1, Ljava/util/ArrayList;

    const/16 v0, 0x10

    invoke-direct {p1, v0}, Ljava/util/ArrayList;-><init>(I)V

    iput-object p1, p0, Lcom/helpshift/network/HttpResponse;->headers:Ljava/util/List;

    return-void

    .line 16
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Status line may not be null."

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method


# virtual methods
.method public addHeader(Lcom/helpshift/network/Header;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    .line 39
    :cond_0
    iget-object v0, p0, Lcom/helpshift/network/HttpResponse;->headers:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    return-void
.end method

.method public getAllHeaders()[Lcom/helpshift/network/Header;
    .locals 2

    .line 44
    iget-object v0, p0, Lcom/helpshift/network/HttpResponse;->headers:Ljava/util/List;

    const/4 v1, 0x0

    new-array v1, v1, [Lcom/helpshift/network/Header;

    invoke-interface {v0, v1}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/network/Header;

    return-object v0
.end method

.method public getEntity()Lcom/helpshift/network/HttpEntity;
    .locals 1

    .line 23
    iget-object v0, p0, Lcom/helpshift/network/HttpResponse;->entity:Lcom/helpshift/network/HttpEntity;

    return-object v0
.end method

.method public getHelpshiftSSLSocketFactory()Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;
    .locals 1

    .line 48
    iget-object v0, p0, Lcom/helpshift/network/HttpResponse;->helpshiftSSLSocketFactory:Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    return-object v0
.end method

.method public getStatusLine()Lcom/helpshift/network/StatusLine;
    .locals 1

    .line 31
    iget-object v0, p0, Lcom/helpshift/network/HttpResponse;->statusLine:Lcom/helpshift/network/StatusLine;

    return-object v0
.end method

.method public setEntity(Lcom/helpshift/network/HttpEntity;)V
    .locals 0

    .line 27
    iput-object p1, p0, Lcom/helpshift/network/HttpResponse;->entity:Lcom/helpshift/network/HttpEntity;

    return-void
.end method

.method public setHelpshiftSSLSocketFactory(Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;)V
    .locals 0

    .line 52
    iput-object p1, p0, Lcom/helpshift/network/HttpResponse;->helpshiftSSLSocketFactory:Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    return-void
.end method
