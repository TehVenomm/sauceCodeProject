.class public Lcom/helpshift/network/HurlStack;
.super Ljava/lang/Object;
.source "HurlStack.java"

# interfaces
.implements Lcom/helpshift/network/HttpStack;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 54
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private configureConnectionForRequest(Ljava/net/HttpURLConnection;Lcom/helpshift/network/request/Request;)V
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/exceptions/InstallException;,
            Ljava/io/IOException;
        }
    .end annotation

    .line 164
    invoke-virtual {p2}, Lcom/helpshift/network/request/Request;->getHeaders()Ljava/util/Map;

    move-result-object v0

    .line 165
    invoke-interface {v0}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 166
    invoke-interface {v0, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-virtual {p1, v2, v3}, Ljava/net/HttpURLConnection;->addRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    const/16 v0, 0x1388

    .line 168
    invoke-virtual {p1, v0}, Ljava/net/HttpURLConnection;->setConnectTimeout(I)V

    .line 169
    invoke-virtual {p1, v0}, Ljava/net/HttpURLConnection;->setReadTimeout(I)V

    const/4 v0, 0x0

    .line 170
    invoke-virtual {p1, v0}, Ljava/net/HttpURLConnection;->setUseCaches(Z)V

    const/4 v1, 0x1

    .line 171
    invoke-virtual {p1, v1}, Ljava/net/HttpURLConnection;->setDoInput(Z)V

    .line 172
    sget-object v2, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    const-string v3, "Helpshift-%s/%s/%s"

    const/4 v4, 0x3

    new-array v4, v4, [Ljava/lang/Object;

    .line 174
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object v5

    invoke-interface {v5}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object v5

    invoke-interface {v5}, Lcom/helpshift/common/platform/Device;->getPlatformName()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v0

    .line 175
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/common/platform/Device;->getSDKVersion()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v4, v1

    const/4 v0, 0x2

    .line 176
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object v5

    invoke-interface {v5}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object v5

    invoke-interface {v5}, Lcom/helpshift/common/platform/Device;->getOSVersion()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v0

    .line 172
    invoke-static {v2, v3, v4}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    const-string v2, "User-Agent"

    .line 177
    invoke-virtual {p1, v2, v0}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 178
    invoke-virtual {p2}, Lcom/helpshift/network/request/Request;->getMethodString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    .line 183
    iget v0, p2, Lcom/helpshift/network/request/Request;->method:I

    if-ne v0, v1, :cond_1

    .line 184
    invoke-virtual {p2}, Lcom/helpshift/network/request/Request;->isDoOutput()Z

    move-result v0

    invoke-virtual {p1, v0}, Ljava/net/HttpURLConnection;->setDoOutput(Z)V

    const-string v0, "Content-type"

    const-string v1, "application/x-www-form-urlencoded"

    .line 185
    invoke-virtual {p1, v0, v1}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 187
    invoke-virtual {p1}, Ljava/net/HttpURLConnection;->getOutputStream()Ljava/io/OutputStream;

    move-result-object p1

    .line 188
    new-instance v0, Ljava/io/BufferedWriter;

    new-instance v1, Ljava/io/OutputStreamWriter;

    const-string v2, "UTF-8"

    invoke-direct {v1, p1, v2}, Ljava/io/OutputStreamWriter;-><init>(Ljava/io/OutputStream;Ljava/lang/String;)V

    invoke-direct {v0, v1}, Ljava/io/BufferedWriter;-><init>(Ljava/io/Writer;)V

    .line 189
    invoke-virtual {p2}, Lcom/helpshift/network/request/Request;->getPOSTParametersQuery()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v0, p2}, Ljava/io/BufferedWriter;->write(Ljava/lang/String;)V

    .line 190
    invoke-virtual {v0}, Ljava/io/BufferedWriter;->flush()V

    .line 191
    invoke-virtual {v0}, Ljava/io/BufferedWriter;->close()V

    .line 192
    invoke-virtual {p1}, Ljava/io/OutputStream;->close()V

    :cond_1
    return-void
.end method

.method private static entityFromConnection(Ljava/net/HttpURLConnection;Z)Lcom/helpshift/network/HttpEntity;
    .locals 3

    .line 57
    new-instance v0, Lcom/helpshift/network/HttpEntity;

    invoke-direct {v0}, Lcom/helpshift/network/HttpEntity;-><init>()V

    if-eqz p1, :cond_0

    .line 61
    :try_start_0
    new-instance p1, Ljava/util/zip/GZIPInputStream;

    new-instance v1, Ljava/io/BufferedInputStream;

    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v2

    invoke-direct {v1, v2}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;)V

    invoke-direct {p1, v1}, Ljava/util/zip/GZIPInputStream;-><init>(Ljava/io/InputStream;)V

    goto :goto_0

    .line 64
    :cond_0
    new-instance p1, Ljava/io/BufferedInputStream;

    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v1

    invoke-direct {p1, v1}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 68
    :catch_0
    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getErrorStream()Ljava/io/InputStream;

    move-result-object p1

    .line 70
    :goto_0
    iput-object p1, v0, Lcom/helpshift/network/HttpEntity;->content:Ljava/io/InputStream;

    .line 71
    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getContentLength()I

    move-result p0

    int-to-long p0, p0

    iput-wide p0, v0, Lcom/helpshift/network/HttpEntity;->contentLength:J

    return-object v0
.end method

.method private fixSSLSocketProtocols(Ljavax/net/ssl/HttpsURLConnection;)V
    .locals 4

    .line 142
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x10

    if-lt v0, v1, :cond_0

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x13

    if-gt v0, v1, :cond_0

    .line 146
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const-string v1, "TLSv1.2"

    .line 147
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 150
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    const-string v2, "SSLv3"

    .line 151
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 153
    invoke-virtual {p1}, Ljavax/net/ssl/HttpsURLConnection;->getSSLSocketFactory()Ljavax/net/ssl/SSLSocketFactory;

    move-result-object v2

    .line 154
    new-instance v3, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    invoke-direct {v3, v2, v0, v1}, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;-><init>(Ljavax/net/ssl/SSLSocketFactory;Ljava/util/List;Ljava/util/List;)V

    .line 155
    invoke-virtual {p1, v3}, Ljavax/net/ssl/HttpsURLConnection;->setSSLSocketFactory(Ljavax/net/ssl/SSLSocketFactory;)V

    :cond_0
    return-void
.end method

.method private getHelpshiftSSLSocketFactory(Ljava/net/HttpURLConnection;)Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;
    .locals 2

    .line 128
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x10

    if-lt v0, v1, :cond_0

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x13

    if-gt v0, v1, :cond_0

    instance-of v0, p1, Ljavax/net/ssl/HttpsURLConnection;

    if-eqz v0, :cond_0

    .line 131
    check-cast p1, Ljavax/net/ssl/HttpsURLConnection;

    .line 132
    invoke-virtual {p1}, Ljavax/net/ssl/HttpsURLConnection;->getSSLSocketFactory()Ljavax/net/ssl/SSLSocketFactory;

    move-result-object v0

    .line 133
    instance-of v0, v0, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    if-eqz v0, :cond_0

    .line 134
    invoke-virtual {p1}, Ljavax/net/ssl/HttpsURLConnection;->getSSLSocketFactory()Ljavax/net/ssl/SSLSocketFactory;

    move-result-object p1

    check-cast p1, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    return-object p1

    :cond_0
    const/4 p1, 0x0

    return-object p1
.end method


# virtual methods
.method public performRequest(Lcom/helpshift/network/request/Request;)Lcom/helpshift/network/HttpResponse;
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/helpshift/exceptions/InstallException;
        }
    .end annotation

    .line 78
    invoke-virtual {p1}, Lcom/helpshift/network/request/Request;->getParsedURL()Ljava/net/URL;

    move-result-object v0

    const-string v1, "https://"

    .line 80
    sget-object v2, Lcom/helpshift/common/domain/network/NetworkConstants;->scheme:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 81
    invoke-virtual {v0}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v0

    check-cast v0, Ljavax/net/ssl/HttpsURLConnection;

    .line 82
    move-object v1, v0

    check-cast v1, Ljavax/net/ssl/HttpsURLConnection;

    invoke-direct {p0, v1}, Lcom/helpshift/network/HurlStack;->fixSSLSocketProtocols(Ljavax/net/ssl/HttpsURLConnection;)V

    goto :goto_0

    .line 85
    :cond_0
    invoke-virtual {v0}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v0

    check-cast v0, Ljava/net/HttpURLConnection;

    :goto_0
    const/4 v1, 0x0

    .line 89
    :try_start_0
    invoke-direct {p0, v0, p1}, Lcom/helpshift/network/HurlStack;->configureConnectionForRequest(Ljava/net/HttpURLConnection;Lcom/helpshift/network/request/Request;)V

    .line 93
    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result p1

    const/4 v2, -0x1

    if-eq p1, v2, :cond_4

    .line 99
    new-instance p1, Lcom/helpshift/network/StatusLine;

    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v2

    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->getResponseMessage()Ljava/lang/String;

    move-result-object v3

    invoke-direct {p1, v2, v3}, Lcom/helpshift/network/StatusLine;-><init>(ILjava/lang/String;)V

    .line 100
    new-instance v2, Lcom/helpshift/network/HttpResponse;

    invoke-direct {v2, p1}, Lcom/helpshift/network/HttpResponse;-><init>(Lcom/helpshift/network/StatusLine;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 102
    :try_start_1
    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->getHeaderFields()Ljava/util/Map;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    const/4 v1, 0x0

    const/4 v3, 0x0

    :cond_1
    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_3

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/util/Map$Entry;

    .line 103
    invoke-interface {v4}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v5

    if-eqz v5, :cond_1

    .line 104
    invoke-interface {v4}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    const-string v6, "Content-Encoding"

    invoke-virtual {v5, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_2

    .line 105
    invoke-interface {v4}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/util/List;

    invoke-interface {v5, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    const-string v6, "gzip"

    invoke-virtual {v5, v6}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_2

    const/4 v3, 0x1

    .line 108
    :cond_2
    new-instance v5, Lcom/helpshift/network/Header;

    invoke-interface {v4}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    invoke-interface {v4}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/util/List;

    invoke-interface {v4, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    invoke-direct {v5, v6, v4}, Lcom/helpshift/network/Header;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    .line 109
    invoke-virtual {v2, v5}, Lcom/helpshift/network/HttpResponse;->addHeader(Lcom/helpshift/network/Header;)V

    goto :goto_1

    .line 112
    :cond_3
    invoke-static {v0, v3}, Lcom/helpshift/network/HurlStack;->entityFromConnection(Ljava/net/HttpURLConnection;Z)Lcom/helpshift/network/HttpEntity;

    move-result-object p1

    invoke-virtual {v2, p1}, Lcom/helpshift/network/HttpResponse;->setEntity(Lcom/helpshift/network/HttpEntity;)V

    .line 113
    invoke-direct {p0, v0}, Lcom/helpshift/network/HurlStack;->getHelpshiftSSLSocketFactory(Ljava/net/HttpURLConnection;)Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    move-result-object p1

    invoke-virtual {v2, p1}, Lcom/helpshift/network/HttpResponse;->setHelpshiftSSLSocketFactory(Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    return-object v2

    :catchall_0
    move-exception p1

    goto :goto_2

    .line 97
    :cond_4
    :try_start_2
    new-instance p1, Ljava/io/IOException;

    const-string v2, "Could not retrieve response code from HttpUrlConnection."

    invoke-direct {p1, v2}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw p1
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    :catchall_1
    move-exception p1

    move-object v2, v1

    :goto_2
    if-nez v2, :cond_5

    .line 118
    invoke-direct {p0, v0}, Lcom/helpshift/network/HurlStack;->getHelpshiftSSLSocketFactory(Ljava/net/HttpURLConnection;)Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    move-result-object v0

    if-eqz v0, :cond_5

    .line 120
    invoke-virtual {v0}, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;->closeSockets()V

    .line 123
    :cond_5
    throw p1
.end method
