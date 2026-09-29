.class Lcom/helpshift/websockets/SocketFactorySettings;
.super Ljava/lang/Object;
.source "SocketFactorySettings.java"


# instance fields
.field private mSSLContext:Ljavax/net/ssl/SSLContext;

.field private mSSLSocketFactory:Ljavax/net/ssl/SSLSocketFactory;

.field private mSocketFactory:Ljavax/net/SocketFactory;


# direct methods
.method constructor <init>()V
    .locals 0

    .line 30
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getSSLContext()Ljavax/net/ssl/SSLContext;
    .locals 1

    .line 57
    iget-object v0, p0, Lcom/helpshift/websockets/SocketFactorySettings;->mSSLContext:Ljavax/net/ssl/SSLContext;

    return-object v0
.end method

.method public getSSLSocketFactory()Ljavax/net/ssl/SSLSocketFactory;
    .locals 1

    .line 47
    iget-object v0, p0, Lcom/helpshift/websockets/SocketFactorySettings;->mSSLSocketFactory:Ljavax/net/ssl/SSLSocketFactory;

    return-object v0
.end method

.method public getSocketFactory()Ljavax/net/SocketFactory;
    .locals 1

    .line 37
    iget-object v0, p0, Lcom/helpshift/websockets/SocketFactorySettings;->mSocketFactory:Ljavax/net/SocketFactory;

    return-object v0
.end method

.method public selectSocketFactory(Z)Ljavax/net/SocketFactory;
    .locals 0

    if-eqz p1, :cond_2

    .line 68
    iget-object p1, p0, Lcom/helpshift/websockets/SocketFactorySettings;->mSSLContext:Ljavax/net/ssl/SSLContext;

    if-eqz p1, :cond_0

    .line 69
    iget-object p1, p0, Lcom/helpshift/websockets/SocketFactorySettings;->mSSLContext:Ljavax/net/ssl/SSLContext;

    invoke-virtual {p1}, Ljavax/net/ssl/SSLContext;->getSocketFactory()Ljavax/net/ssl/SSLSocketFactory;

    move-result-object p1

    return-object p1

    .line 72
    :cond_0
    iget-object p1, p0, Lcom/helpshift/websockets/SocketFactorySettings;->mSSLSocketFactory:Ljavax/net/ssl/SSLSocketFactory;

    if-eqz p1, :cond_1

    .line 73
    iget-object p1, p0, Lcom/helpshift/websockets/SocketFactorySettings;->mSSLSocketFactory:Ljavax/net/ssl/SSLSocketFactory;

    return-object p1

    .line 76
    :cond_1
    invoke-static {}, Ljavax/net/ssl/SSLSocketFactory;->getDefault()Ljavax/net/SocketFactory;

    move-result-object p1

    return-object p1

    .line 79
    :cond_2
    iget-object p1, p0, Lcom/helpshift/websockets/SocketFactorySettings;->mSocketFactory:Ljavax/net/SocketFactory;

    if-eqz p1, :cond_3

    .line 80
    iget-object p1, p0, Lcom/helpshift/websockets/SocketFactorySettings;->mSocketFactory:Ljavax/net/SocketFactory;

    return-object p1

    .line 83
    :cond_3
    invoke-static {}, Ljavax/net/SocketFactory;->getDefault()Ljavax/net/SocketFactory;

    move-result-object p1

    return-object p1
.end method

.method public setSSLContext(Ljavax/net/ssl/SSLContext;)V
    .locals 0

    .line 62
    iput-object p1, p0, Lcom/helpshift/websockets/SocketFactorySettings;->mSSLContext:Ljavax/net/ssl/SSLContext;

    return-void
.end method

.method public setSSLSocketFactory(Ljavax/net/ssl/SSLSocketFactory;)V
    .locals 0

    .line 52
    iput-object p1, p0, Lcom/helpshift/websockets/SocketFactorySettings;->mSSLSocketFactory:Ljavax/net/ssl/SSLSocketFactory;

    return-void
.end method

.method public setSocketFactory(Ljavax/net/SocketFactory;)V
    .locals 0

    .line 42
    iput-object p1, p0, Lcom/helpshift/websockets/SocketFactorySettings;->mSocketFactory:Ljavax/net/SocketFactory;

    return-void
.end method
