.class public Lcom/helpshift/common/platform/AndroidHTTPTransport;
.super Ljava/lang/Object;
.source "AndroidHTTPTransport.java"

# interfaces
.implements Lcom/helpshift/common/platform/network/HTTPTransport;


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_HTTPTrnsport"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 44
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private closeHelpshiftSSLSocketFactorySockets(Ljavax/net/ssl/HttpsURLConnection;)V
    .locals 2

    .line 207
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x10

    if-lt v0, v1, :cond_0

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x13

    if-gt v0, v1, :cond_0

    if-eqz p1, :cond_0

    .line 210
    invoke-virtual {p1}, Ljavax/net/ssl/HttpsURLConnection;->getSSLSocketFactory()Ljavax/net/ssl/SSLSocketFactory;

    move-result-object p1

    .line 211
    instance-of v0, p1, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    if-eqz v0, :cond_0

    .line 212
    check-cast p1, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    .line 213
    invoke-virtual {p1}, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;->closeSockets()V

    :cond_0
    return-void
.end method

.method private fixSSLSocketProtocols(Ljavax/net/ssl/HttpsURLConnection;)V
    .locals 4

    .line 188
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x10

    if-lt v0, v1, :cond_0

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x13

    if-gt v0, v1, :cond_0

    .line 192
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const-string v1, "TLSv1.2"

    .line 193
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 196
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    const-string v2, "SSLv3"

    .line 197
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 199
    invoke-virtual {p1}, Ljavax/net/ssl/HttpsURLConnection;->getSSLSocketFactory()Ljavax/net/ssl/SSLSocketFactory;

    move-result-object v2

    .line 200
    new-instance v3, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    invoke-direct {v3, v2, v0, v1}, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;-><init>(Ljavax/net/ssl/SSLSocketFactory;Ljava/util/List;Ljava/util/List;)V

    .line 201
    invoke-virtual {p1, v3}, Ljavax/net/ssl/HttpsURLConnection;->setSSLSocketFactory(Ljavax/net/ssl/SSLSocketFactory;)V

    :cond_0
    return-void
.end method

.method private isInvalidKeyForHeader(Ljava/lang/String;)Z
    .locals 1

    const-string v0, "screenshot"

    .line 219
    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    const-string v0, "originalFileName"

    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    :goto_1
    return p1
.end method

.method private makeNetworkRequest(Lcom/helpshift/common/platform/network/Request;)Lcom/helpshift/common/platform/network/Response;
    .locals 11

    const/4 v0, 0x0

    :try_start_0
    const-string v1, "https://"

    .line 63
    sget-object v2, Lcom/helpshift/common/domain/network/NetworkConstants;->scheme:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 64
    new-instance v1, Ljava/net/URL;

    iget-object v2, p1, Lcom/helpshift/common/platform/network/Request;->url:Ljava/lang/String;

    invoke-direct {v1, v2}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v1

    check-cast v1, Ljavax/net/ssl/HttpsURLConnection;
    :try_end_0
    .catch Ljava/net/UnknownHostException; {:try_start_0 .. :try_end_0} :catch_15
    .catch Ljava/net/SocketException; {:try_start_0 .. :try_end_0} :catch_14
    .catch Ljava/lang/SecurityException; {:try_start_0 .. :try_end_0} :catch_14
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_0 .. :try_end_0} :catch_13
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_0 .. :try_end_0} :catch_12
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_11
    .catchall {:try_start_0 .. :try_end_0} :catchall_3

    .line 65
    :try_start_1
    move-object v2, v1

    check-cast v2, Ljavax/net/ssl/HttpsURLConnection;

    invoke-direct {p0, v2}, Lcom/helpshift/common/platform/AndroidHTTPTransport;->fixSSLSocketProtocols(Ljavax/net/ssl/HttpsURLConnection;)V
    :try_end_1
    .catch Ljava/net/UnknownHostException; {:try_start_1 .. :try_end_1} :catch_4
    .catch Ljava/net/SocketException; {:try_start_1 .. :try_end_1} :catch_3
    .catch Ljava/lang/SecurityException; {:try_start_1 .. :try_end_1} :catch_3
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_5

    :catchall_0
    move-exception p1

    move-object v3, v0

    goto/16 :goto_14

    :catch_0
    move-exception v2

    move-object v3, v0

    move-object v5, v3

    :goto_0
    move-object v0, v1

    goto/16 :goto_e

    :catch_1
    move-exception v2

    move-object v3, v0

    move-object v5, v3

    :goto_1
    move-object v0, v1

    goto/16 :goto_f

    :catch_2
    move-exception v2

    move-object v3, v0

    move-object v5, v3

    :goto_2
    move-object v0, v1

    goto/16 :goto_10

    :catch_3
    move-exception v2

    move-object v3, v0

    move-object v5, v3

    :goto_3
    move-object v0, v1

    goto/16 :goto_11

    :catch_4
    move-exception v2

    move-object v3, v0

    move-object v5, v3

    :goto_4
    move-object v0, v1

    goto/16 :goto_12

    .line 68
    :cond_0
    :try_start_2
    new-instance v1, Ljava/net/URL;

    iget-object v2, p1, Lcom/helpshift/common/platform/network/Request;->url:Ljava/lang/String;

    invoke-direct {v1, v2}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v1

    check-cast v1, Ljava/net/HttpURLConnection;
    :try_end_2
    .catch Ljava/net/UnknownHostException; {:try_start_2 .. :try_end_2} :catch_15
    .catch Ljava/net/SocketException; {:try_start_2 .. :try_end_2} :catch_14
    .catch Ljava/lang/SecurityException; {:try_start_2 .. :try_end_2} :catch_14
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_2 .. :try_end_2} :catch_13
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_2 .. :try_end_2} :catch_12
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_11
    .catchall {:try_start_2 .. :try_end_2} :catchall_3

    .line 71
    :goto_5
    :try_start_3
    iget-object v2, p1, Lcom/helpshift/common/platform/network/Request;->method:Lcom/helpshift/common/platform/network/Method;

    invoke-virtual {v2}, Lcom/helpshift/common/platform/network/Method;->name()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    .line 72
    iget v2, p1, Lcom/helpshift/common/platform/network/Request;->timeout:I

    invoke-virtual {v1, v2}, Ljava/net/HttpURLConnection;->setConnectTimeout(I)V

    .line 73
    iget-object v2, p1, Lcom/helpshift/common/platform/network/Request;->headers:Ljava/util/List;

    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_6
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/common/platform/network/KeyValuePair;

    .line 74
    iget-object v4, v3, Lcom/helpshift/common/platform/network/KeyValuePair;->key:Ljava/lang/String;

    iget-object v3, v3, Lcom/helpshift/common/platform/network/KeyValuePair;->value:Ljava/lang/String;

    invoke-virtual {v1, v4, v3}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_6

    .line 77
    :cond_1
    iget-object v2, p1, Lcom/helpshift/common/platform/network/Request;->method:Lcom/helpshift/common/platform/network/Method;

    sget-object v3, Lcom/helpshift/common/platform/network/Method;->POST:Lcom/helpshift/common/platform/network/Method;

    if-eq v2, v3, :cond_3

    iget-object v2, p1, Lcom/helpshift/common/platform/network/Request;->method:Lcom/helpshift/common/platform/network/Method;

    sget-object v3, Lcom/helpshift/common/platform/network/Method;->PUT:Lcom/helpshift/common/platform/network/Method;

    if-ne v2, v3, :cond_2

    goto :goto_7

    :cond_2
    move-object v3, v0

    goto :goto_9

    .line 80
    :cond_3
    :goto_7
    iget-object v2, p1, Lcom/helpshift/common/platform/network/Request;->method:Lcom/helpshift/common/platform/network/Method;

    sget-object v3, Lcom/helpshift/common/platform/network/Method;->PUT:Lcom/helpshift/common/platform/network/Method;

    if-ne v2, v3, :cond_4

    .line 81
    move-object v2, p1

    check-cast v2, Lcom/helpshift/common/platform/network/PUTRequest;

    iget-object v2, v2, Lcom/helpshift/common/platform/network/PUTRequest;->query:Ljava/lang/String;

    goto :goto_8

    .line 84
    :cond_4
    move-object v2, p1

    check-cast v2, Lcom/helpshift/common/platform/network/POSTRequest;

    iget-object v2, v2, Lcom/helpshift/common/platform/network/POSTRequest;->query:Ljava/lang/String;

    :goto_8
    const/4 v3, 0x1

    .line 87
    invoke-virtual {v1, v3}, Ljava/net/HttpURLConnection;->setDoOutput(Z)V

    .line 88
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getOutputStream()Ljava/io/OutputStream;

    move-result-object v3
    :try_end_3
    .catch Ljava/net/UnknownHostException; {:try_start_3 .. :try_end_3} :catch_4
    .catch Ljava/net/SocketException; {:try_start_3 .. :try_end_3} :catch_3
    .catch Ljava/lang/SecurityException; {:try_start_3 .. :try_end_3} :catch_3
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_3 .. :try_end_3} :catch_2
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_3 .. :try_end_3} :catch_1
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    .line 89
    :try_start_4
    new-instance v4, Ljava/io/BufferedWriter;

    new-instance v5, Ljava/io/OutputStreamWriter;

    const-string v6, "UTF-8"

    invoke-direct {v5, v3, v6}, Ljava/io/OutputStreamWriter;-><init>(Ljava/io/OutputStream;Ljava/lang/String;)V

    invoke-direct {v4, v5}, Ljava/io/BufferedWriter;-><init>(Ljava/io/Writer;)V

    .line 90
    invoke-virtual {v4, v2}, Ljava/io/BufferedWriter;->write(Ljava/lang/String;)V

    .line 91
    invoke-virtual {v4}, Ljava/io/BufferedWriter;->flush()V

    .line 92
    invoke-virtual {v4}, Ljava/io/BufferedWriter;->close()V

    .line 93
    invoke-virtual {v3}, Ljava/io/OutputStream;->flush()V

    .line 96
    :goto_9
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v2

    .line 97
    new-instance v4, Ljava/util/ArrayList;

    invoke-direct {v4}, Ljava/util/ArrayList;-><init>()V

    .line 98
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getHeaderFields()Ljava/util/Map;

    move-result-object v5

    .line 99
    invoke-interface {v5}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v6

    invoke-interface {v6}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :cond_5
    :goto_a
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    const/4 v8, 0x0

    if-eqz v7, :cond_6

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/lang/String;

    .line 100
    invoke-static {v7}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_5

    .line 101
    new-instance v9, Lcom/helpshift/common/platform/network/KeyValuePair;

    invoke-interface {v5, v7}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v10

    check-cast v10, Ljava/util/List;

    invoke-interface {v10, v8}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Ljava/lang/String;

    invoke-direct {v9, v7, v8}, Lcom/helpshift/common/platform/network/KeyValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v4, v9}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_a

    :cond_6
    const/16 v6, 0xc8

    if-lt v2, v6, :cond_a

    const/16 v6, 0x12c

    if-ge v2, v6, :cond_a

    .line 106
    new-instance v6, Ljava/io/BufferedInputStream;

    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v7

    invoke-direct {v6, v7}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;)V

    const-string v7, "Content-Encoding"

    .line 108
    invoke-interface {v5, v7}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/util/List;

    if-eqz v5, :cond_7

    .line 109
    invoke-interface {v5}, Ljava/util/List;->size()I

    move-result v7

    if-lez v7, :cond_7

    .line 110
    invoke-interface {v5, v8}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    const-string v7, "gzip"

    invoke-virtual {v5, v7}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_7

    .line 111
    new-instance v5, Ljava/util/zip/GZIPInputStream;

    invoke-direct {v5, v6}, Ljava/util/zip/GZIPInputStream;-><init>(Ljava/io/InputStream;)V

    goto :goto_b

    :cond_7
    move-object v5, v6

    .line 113
    :goto_b
    invoke-direct {p0, v5}, Lcom/helpshift/common/platform/AndroidHTTPTransport;->readInputStream(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object v6

    .line 114
    invoke-virtual {v5}, Ljava/io/InputStream;->close()V

    .line 115
    new-instance v5, Lcom/helpshift/common/platform/network/Response;

    invoke-direct {v5, v2, v6, v4}, Lcom/helpshift/common/platform/network/Response;-><init>(ILjava/lang/String;Ljava/util/List;)V
    :try_end_4
    .catch Ljava/net/UnknownHostException; {:try_start_4 .. :try_end_4} :catch_10
    .catch Ljava/net/SocketException; {:try_start_4 .. :try_end_4} :catch_f
    .catch Ljava/lang/SecurityException; {:try_start_4 .. :try_end_4} :catch_f
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_4 .. :try_end_4} :catch_e
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_4 .. :try_end_4} :catch_d
    .catch Ljava/io/IOException; {:try_start_4 .. :try_end_4} :catch_c
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    .line 153
    invoke-static {v0}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 154
    invoke-static {v3}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 157
    :try_start_5
    instance-of p1, v1, Ljavax/net/ssl/HttpsURLConnection;

    if-eqz p1, :cond_8

    .line 158
    move-object p1, v1

    check-cast p1, Ljavax/net/ssl/HttpsURLConnection;

    invoke-direct {p0, p1}, Lcom/helpshift/common/platform/AndroidHTTPTransport;->closeHelpshiftSSLSocketFactorySockets(Ljavax/net/ssl/HttpsURLConnection;)V

    :cond_8
    if-eqz v1, :cond_9

    .line 161
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_5

    goto :goto_c

    :catch_5
    move-exception p1

    const-string v0, "Helpshift_HTTPTrnsport"

    const-string v1, "Error in finally closing resources"

    .line 165
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_9
    :goto_c
    return-object v5

    :cond_a
    :try_start_6
    const-string v5, "Helpshift_HTTPTrnsport"

    .line 118
    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "Api : "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v7, p1, Lcom/helpshift/common/platform/network/Request;->url:Ljava/lang/String;

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v7, " \t Status : "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v7, "\t Thread : "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 119
    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/Thread;->getName()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    .line 118
    invoke-static {v5, v6}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 121
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getErrorStream()Ljava/io/InputStream;

    move-result-object v5
    :try_end_6
    .catch Ljava/net/UnknownHostException; {:try_start_6 .. :try_end_6} :catch_10
    .catch Ljava/net/SocketException; {:try_start_6 .. :try_end_6} :catch_f
    .catch Ljava/lang/SecurityException; {:try_start_6 .. :try_end_6} :catch_f
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_6 .. :try_end_6} :catch_e
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_6 .. :try_end_6} :catch_d
    .catch Ljava/io/IOException; {:try_start_6 .. :try_end_6} :catch_c
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    .line 122
    :try_start_7
    invoke-direct {p0, v5}, Lcom/helpshift/common/platform/AndroidHTTPTransport;->readInputStream(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object v0

    .line 124
    new-instance v6, Lcom/helpshift/common/platform/network/Response;

    invoke-direct {v6, v2, v0, v4}, Lcom/helpshift/common/platform/network/Response;-><init>(ILjava/lang/String;Ljava/util/List;)V
    :try_end_7
    .catch Ljava/net/UnknownHostException; {:try_start_7 .. :try_end_7} :catch_b
    .catch Ljava/net/SocketException; {:try_start_7 .. :try_end_7} :catch_a
    .catch Ljava/lang/SecurityException; {:try_start_7 .. :try_end_7} :catch_a
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_7 .. :try_end_7} :catch_9
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_7 .. :try_end_7} :catch_8
    .catch Ljava/io/IOException; {:try_start_7 .. :try_end_7} :catch_7
    .catchall {:try_start_7 .. :try_end_7} :catchall_1

    .line 153
    invoke-static {v5}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 154
    invoke-static {v3}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 157
    :try_start_8
    instance-of p1, v1, Ljavax/net/ssl/HttpsURLConnection;

    if-eqz p1, :cond_b

    .line 158
    move-object p1, v1

    check-cast p1, Ljavax/net/ssl/HttpsURLConnection;

    invoke-direct {p0, p1}, Lcom/helpshift/common/platform/AndroidHTTPTransport;->closeHelpshiftSSLSocketFactorySockets(Ljavax/net/ssl/HttpsURLConnection;)V

    :cond_b
    if-eqz v1, :cond_c

    .line 161
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_8
    .catch Ljava/lang/Exception; {:try_start_8 .. :try_end_8} :catch_6

    goto :goto_d

    :catch_6
    move-exception p1

    const-string v0, "Helpshift_HTTPTrnsport"

    const-string v1, "Error in finally closing resources"

    .line 165
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_c
    :goto_d
    return-object v6

    :catchall_1
    move-exception p1

    goto/16 :goto_13

    :catch_7
    move-exception v2

    goto/16 :goto_0

    :catch_8
    move-exception v2

    goto/16 :goto_1

    :catch_9
    move-exception v2

    goto/16 :goto_2

    :catch_a
    move-exception v2

    goto/16 :goto_3

    :catch_b
    move-exception v2

    goto/16 :goto_4

    :catchall_2
    move-exception p1

    goto/16 :goto_14

    :catch_c
    move-exception v2

    move-object v5, v0

    goto/16 :goto_0

    :catch_d
    move-exception v2

    move-object v5, v0

    goto/16 :goto_1

    :catch_e
    move-exception v2

    move-object v5, v0

    goto/16 :goto_2

    :catch_f
    move-exception v2

    move-object v5, v0

    goto/16 :goto_3

    :catch_10
    move-exception v2

    move-object v5, v0

    goto/16 :goto_4

    :catchall_3
    move-exception p1

    move-object v1, v0

    move-object v3, v1

    goto :goto_14

    :catch_11
    move-exception v2

    move-object v3, v0

    move-object v5, v3

    .line 148
    :goto_e
    :try_start_9
    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->GENERIC:Lcom/helpshift/common/exception/NetworkException;

    .line 149
    iget-object p1, p1, Lcom/helpshift/common/platform/network/Request;->url:Ljava/lang/String;

    iput-object p1, v1, Lcom/helpshift/common/exception/NetworkException;->route:Ljava/lang/String;

    const-string p1, "Network error"

    .line 150
    invoke-static {v2, v1, p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    :catch_12
    move-exception v2

    move-object v3, v0

    move-object v5, v3

    .line 143
    :goto_f
    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->SSL_HANDSHAKE:Lcom/helpshift/common/exception/NetworkException;

    .line 144
    iget-object p1, p1, Lcom/helpshift/common/platform/network/Request;->url:Ljava/lang/String;

    iput-object p1, v1, Lcom/helpshift/common/exception/NetworkException;->route:Ljava/lang/String;

    const-string p1, "Network error"

    .line 145
    invoke-static {v2, v1, p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    :catch_13
    move-exception v2

    move-object v3, v0

    move-object v5, v3

    .line 138
    :goto_10
    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->SSL_PEER_UNVERIFIED:Lcom/helpshift/common/exception/NetworkException;

    .line 139
    iget-object p1, p1, Lcom/helpshift/common/platform/network/Request;->url:Ljava/lang/String;

    iput-object p1, v1, Lcom/helpshift/common/exception/NetworkException;->route:Ljava/lang/String;

    const-string p1, "Network error"

    .line 140
    invoke-static {v2, v1, p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    :catch_14
    move-exception v2

    move-object v3, v0

    move-object v5, v3

    .line 133
    :goto_11
    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->NO_CONNECTION:Lcom/helpshift/common/exception/NetworkException;

    .line 134
    iget-object p1, p1, Lcom/helpshift/common/platform/network/Request;->url:Ljava/lang/String;

    iput-object p1, v1, Lcom/helpshift/common/exception/NetworkException;->route:Ljava/lang/String;

    const-string p1, "Network error"

    .line 135
    invoke-static {v2, v1, p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    :catch_15
    move-exception v2

    move-object v3, v0

    move-object v5, v3

    .line 128
    :goto_12
    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->UNKNOWN_HOST:Lcom/helpshift/common/exception/NetworkException;

    .line 129
    iget-object p1, p1, Lcom/helpshift/common/platform/network/Request;->url:Ljava/lang/String;

    iput-object p1, v1, Lcom/helpshift/common/exception/NetworkException;->route:Ljava/lang/String;

    const-string p1, "Network error"

    .line 130
    invoke-static {v2, v1, p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
    :try_end_9
    .catchall {:try_start_9 .. :try_end_9} :catchall_4

    :catchall_4
    move-exception p1

    move-object v1, v0

    :goto_13
    move-object v0, v5

    .line 153
    :goto_14
    invoke-static {v0}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 154
    invoke-static {v3}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 157
    :try_start_a
    instance-of v0, v1, Ljavax/net/ssl/HttpsURLConnection;

    if-eqz v0, :cond_d

    .line 158
    move-object v0, v1

    check-cast v0, Ljavax/net/ssl/HttpsURLConnection;

    invoke-direct {p0, v0}, Lcom/helpshift/common/platform/AndroidHTTPTransport;->closeHelpshiftSSLSocketFactorySockets(Ljavax/net/ssl/HttpsURLConnection;)V

    :cond_d
    if-eqz v1, :cond_e

    .line 161
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_a
    .catch Ljava/lang/Exception; {:try_start_a .. :try_end_a} :catch_16

    goto :goto_15

    :catch_16
    move-exception v0

    const-string v1, "Helpshift_HTTPTrnsport"

    const-string v2, "Error in finally closing resources"

    .line 165
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 167
    :cond_e
    :goto_15
    throw p1
.end method

.method private readInputStream(Ljava/io/InputStream;)Ljava/lang/String;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 174
    :cond_0
    new-instance v0, Ljava/io/InputStreamReader;

    invoke-direct {v0, p1}, Ljava/io/InputStreamReader;-><init>(Ljava/io/InputStream;)V

    .line 175
    new-instance p1, Ljava/io/BufferedReader;

    invoke-direct {p1, v0}, Ljava/io/BufferedReader;-><init>(Ljava/io/Reader;)V

    .line 176
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    .line 179
    :goto_0
    invoke-virtual {p1}, Ljava/io/BufferedReader;->readLine()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_1

    .line 180
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0

    .line 182
    :cond_1
    invoke-virtual {v0}, Ljava/io/InputStreamReader;->close()V

    .line 183
    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private upload(Lcom/helpshift/common/platform/network/UploadRequest;)Lcom/helpshift/common/platform/network/Response;
    .locals 13

    const/4 v0, 0x0

    .line 228
    :try_start_0
    new-instance v1, Ljava/net/URL;

    iget-object v2, p1, Lcom/helpshift/common/platform/network/UploadRequest;->url:Ljava/lang/String;

    invoke-direct {v1, v2}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    const-string v2, "--"

    const-string v3, "*****"

    const-string v4, "\r\n"

    const-string v5, "https://"

    .line 233
    sget-object v6, Lcom/helpshift/common/domain/network/NetworkConstants;->scheme:Ljava/lang/String;

    invoke-virtual {v5, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_0

    .line 234
    invoke-virtual {v1}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v1

    check-cast v1, Ljavax/net/ssl/HttpsURLConnection;
    :try_end_0
    .catch Ljava/net/UnknownHostException; {:try_start_0 .. :try_end_0} :catch_1a
    .catch Ljava/net/SocketException; {:try_start_0 .. :try_end_0} :catch_19
    .catch Ljava/lang/SecurityException; {:try_start_0 .. :try_end_0} :catch_19
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_0 .. :try_end_0} :catch_18
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_0 .. :try_end_0} :catch_17
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_16
    .catchall {:try_start_0 .. :try_end_0} :catchall_4

    .line 235
    :try_start_1
    move-object v5, v1

    check-cast v5, Ljavax/net/ssl/HttpsURLConnection;

    invoke-direct {p0, v5}, Lcom/helpshift/common/platform/AndroidHTTPTransport;->fixSSLSocketProtocols(Ljavax/net/ssl/HttpsURLConnection;)V
    :try_end_1
    .catch Ljava/net/UnknownHostException; {:try_start_1 .. :try_end_1} :catch_4
    .catch Ljava/net/SocketException; {:try_start_1 .. :try_end_1} :catch_3
    .catch Ljava/lang/SecurityException; {:try_start_1 .. :try_end_1} :catch_3
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_5

    :catchall_0
    move-exception p1

    move-object v3, v0

    goto/16 :goto_c

    :catch_0
    move-exception v2

    move-object v3, v0

    move-object v6, v3

    move-object v9, v6

    :goto_0
    move-object v0, v1

    goto/16 :goto_d

    :catch_1
    move-exception v2

    move-object v3, v0

    move-object v6, v3

    move-object v9, v6

    :goto_1
    move-object v0, v1

    goto/16 :goto_e

    :catch_2
    move-exception v2

    move-object v3, v0

    move-object v6, v3

    move-object v9, v6

    :goto_2
    move-object v0, v1

    goto/16 :goto_f

    :catch_3
    move-exception v2

    move-object v3, v0

    move-object v6, v3

    move-object v9, v6

    :goto_3
    move-object v0, v1

    goto/16 :goto_10

    :catch_4
    move-exception v2

    move-object v3, v0

    move-object v6, v3

    move-object v9, v6

    :goto_4
    move-object v0, v1

    goto/16 :goto_11

    .line 238
    :cond_0
    :try_start_2
    invoke-virtual {v1}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v1

    check-cast v1, Ljava/net/HttpURLConnection;
    :try_end_2
    .catch Ljava/net/UnknownHostException; {:try_start_2 .. :try_end_2} :catch_1a
    .catch Ljava/net/SocketException; {:try_start_2 .. :try_end_2} :catch_19
    .catch Ljava/lang/SecurityException; {:try_start_2 .. :try_end_2} :catch_19
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_2 .. :try_end_2} :catch_18
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_2 .. :try_end_2} :catch_17
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_16
    .catchall {:try_start_2 .. :try_end_2} :catchall_4

    :goto_5
    const/4 v5, 0x1

    .line 240
    :try_start_3
    invoke-virtual {v1, v5}, Ljava/net/HttpURLConnection;->setDoInput(Z)V

    .line 241
    invoke-virtual {v1, v5}, Ljava/net/HttpURLConnection;->setDoOutput(Z)V

    const/4 v5, 0x0

    .line 242
    invoke-virtual {v1, v5}, Ljava/net/HttpURLConnection;->setUseCaches(Z)V

    .line 243
    iget-object v6, p1, Lcom/helpshift/common/platform/network/UploadRequest;->method:Lcom/helpshift/common/platform/network/Method;

    invoke-virtual {v6}, Lcom/helpshift/common/platform/network/Method;->name()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v1, v6}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    .line 244
    iget v6, p1, Lcom/helpshift/common/platform/network/UploadRequest;->timeout:I

    invoke-virtual {v1, v6}, Ljava/net/HttpURLConnection;->setConnectTimeout(I)V

    .line 245
    iget v6, p1, Lcom/helpshift/common/platform/network/UploadRequest;->timeout:I

    invoke-virtual {v1, v6}, Ljava/net/HttpURLConnection;->setReadTimeout(I)V

    .line 247
    iget-object v6, p1, Lcom/helpshift/common/platform/network/UploadRequest;->headers:Ljava/util/List;

    .line 248
    invoke-interface {v6}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :goto_6
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_1

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Lcom/helpshift/common/platform/network/KeyValuePair;

    .line 249
    iget-object v8, v7, Lcom/helpshift/common/platform/network/KeyValuePair;->key:Ljava/lang/String;

    iget-object v7, v7, Lcom/helpshift/common/platform/network/KeyValuePair;->value:Ljava/lang/String;

    invoke-virtual {v1, v8, v7}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_6

    .line 252
    :cond_1
    new-instance v6, Ljava/io/DataOutputStream;

    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getOutputStream()Ljava/io/OutputStream;

    move-result-object v7

    invoke-direct {v6, v7}, Ljava/io/DataOutputStream;-><init>(Ljava/io/OutputStream;)V
    :try_end_3
    .catch Ljava/net/UnknownHostException; {:try_start_3 .. :try_end_3} :catch_4
    .catch Ljava/net/SocketException; {:try_start_3 .. :try_end_3} :catch_3
    .catch Ljava/lang/SecurityException; {:try_start_3 .. :try_end_3} :catch_3
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_3 .. :try_end_3} :catch_2
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_3 .. :try_end_3} :catch_1
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    .line 253
    :try_start_4
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v7, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    .line 254
    iget-object v7, p1, Lcom/helpshift/common/platform/network/UploadRequest;->data:Ljava/util/Map;

    .line 256
    invoke-interface {v7}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v8

    invoke-interface {v8}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v8

    :cond_2
    :goto_7
    invoke-interface {v8}, Ljava/util/Iterator;->hasNext()Z

    move-result v9

    if-eqz v9, :cond_3

    invoke-interface {v8}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Ljava/util/Map$Entry;

    .line 257
    invoke-interface {v9}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v10

    check-cast v10, Ljava/lang/String;

    .line 259
    invoke-direct {p0, v10}, Lcom/helpshift/common/platform/AndroidHTTPTransport;->isInvalidKeyForHeader(Ljava/lang/String;)Z

    move-result v11

    if-nez v11, :cond_2

    .line 260
    invoke-interface {v9}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Ljava/lang/String;

    .line 261
    new-instance v11, Ljava/lang/StringBuilder;

    invoke-direct {v11}, Ljava/lang/StringBuilder;-><init>()V

    const-string v12, "Content-Disposition: form-data; name=\""

    invoke-virtual {v11, v12}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v11, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v10, "\"; "

    invoke-virtual {v11, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v11, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v11}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v6, v10}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    .line 263
    new-instance v10, Ljava/lang/StringBuilder;

    invoke-direct {v10}, Ljava/lang/StringBuilder;-><init>()V

    const-string v11, "Content-Type: text/plain;charset=UTF-8"

    invoke-virtual {v10, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v6, v10}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    .line 264
    new-instance v10, Ljava/lang/StringBuilder;

    invoke-direct {v10}, Ljava/lang/StringBuilder;-><init>()V

    const-string v11, "Content-Length: "

    invoke-virtual {v10, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/String;->length()I

    move-result v11

    invoke-virtual {v10, v11}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v10, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v6, v10}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    .line 265
    invoke-virtual {v6, v4}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    .line 266
    new-instance v10, Ljava/lang/StringBuilder;

    invoke-direct {v10}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v10, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v6, v9}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    .line 267
    new-instance v9, Ljava/lang/StringBuilder;

    invoke-direct {v9}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v9, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v6, v9}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    goto/16 :goto_7

    .line 271
    :cond_3
    new-instance v8, Ljava/io/File;

    const-string v9, "screenshot"

    invoke-interface {v7, v9}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Ljava/lang/String;

    invoke-direct {v8, v9}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    const-string v9, "originalFileName"

    .line 272
    invoke-interface {v7, v9}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/lang/String;

    if-nez v7, :cond_4

    .line 275
    invoke-virtual {v8}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v7

    .line 278
    :cond_4
    new-instance v9, Ljava/io/FileInputStream;

    invoke-direct {v9, v8}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V
    :try_end_4
    .catch Ljava/net/UnknownHostException; {:try_start_4 .. :try_end_4} :catch_15
    .catch Ljava/net/SocketException; {:try_start_4 .. :try_end_4} :catch_14
    .catch Ljava/lang/SecurityException; {:try_start_4 .. :try_end_4} :catch_14
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_4 .. :try_end_4} :catch_13
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_4 .. :try_end_4} :catch_12
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_11
    .catchall {:try_start_4 .. :try_end_4} :catchall_3

    .line 279
    :try_start_5
    new-instance v10, Ljava/lang/StringBuilder;

    invoke-direct {v10}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v10, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v6, v10}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    .line 280
    new-instance v10, Ljava/lang/StringBuilder;

    invoke-direct {v10}, Ljava/lang/StringBuilder;-><init>()V

    const-string v11, "Content-Disposition: form-data; name=\"screenshot\"; filename=\""

    invoke-virtual {v10, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v7, "\""

    invoke-virtual {v10, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    .line 282
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v10, "Content-Type: "

    invoke-virtual {v7, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v10, p1, Lcom/helpshift/common/platform/network/UploadRequest;->mimeType:Ljava/lang/String;

    invoke-virtual {v7, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    .line 283
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v10, "Content-Length: "

    invoke-virtual {v7, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8}, Ljava/io/File;->length()J

    move-result-wide v10

    invoke-virtual {v7, v10, v11}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    .line 284
    invoke-virtual {v6, v4}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    const/high16 v7, 0x100000

    .line 290
    invoke-virtual {v9}, Ljava/io/FileInputStream;->available()I

    move-result v8

    .line 292
    invoke-static {v8, v7}, Ljava/lang/Math;->min(II)I

    move-result v8

    .line 293
    new-array v10, v8, [B

    .line 296
    invoke-virtual {v9, v10, v5, v8}, Ljava/io/FileInputStream;->read([BII)I

    move-result v11

    :goto_8
    if-lez v11, :cond_5

    .line 299
    invoke-virtual {v6, v10, v5, v8}, Ljava/io/DataOutputStream;->write([BII)V

    .line 300
    invoke-virtual {v9}, Ljava/io/FileInputStream;->available()I

    move-result v8

    .line 301
    invoke-static {v8, v7}, Ljava/lang/Math;->min(II)I

    move-result v8

    .line 302
    invoke-virtual {v9, v10, v5, v8}, Ljava/io/FileInputStream;->read([BII)I

    move-result v11

    goto :goto_8

    .line 305
    :cond_5
    invoke-virtual {v6, v4}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    .line 306
    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v5, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v6, v2}, Ljava/io/DataOutputStream;->writeBytes(Ljava/lang/String;)V

    .line 307
    invoke-virtual {v6}, Ljava/io/DataOutputStream;->flush()V

    .line 309
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v2

    const/16 v3, 0xc8

    if-lt v2, v3, :cond_8

    const/16 v3, 0x12c

    if-ge v2, v3, :cond_8

    .line 312
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v3
    :try_end_5
    .catch Ljava/net/UnknownHostException; {:try_start_5 .. :try_end_5} :catch_10
    .catch Ljava/net/SocketException; {:try_start_5 .. :try_end_5} :catch_f
    .catch Ljava/lang/SecurityException; {:try_start_5 .. :try_end_5} :catch_f
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_5 .. :try_end_5} :catch_e
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_5 .. :try_end_5} :catch_d
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_c
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    if-eqz v3, :cond_6

    .line 314
    :try_start_6
    invoke-direct {p0, v3}, Lcom/helpshift/common/platform/AndroidHTTPTransport;->readInputStream(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object v4

    goto :goto_9

    :catchall_1
    move-exception p1

    goto/16 :goto_12

    :catch_5
    move-exception v2

    goto/16 :goto_0

    :catch_6
    move-exception v2

    goto/16 :goto_1

    :catch_7
    move-exception v2

    goto/16 :goto_2

    :catch_8
    move-exception v2

    goto/16 :goto_3

    :catch_9
    move-exception v2

    goto/16 :goto_4

    :cond_6
    move-object v4, v0

    .line 316
    :goto_9
    new-instance v5, Lcom/helpshift/common/platform/network/Response;

    invoke-direct {v5, v2, v4, v0}, Lcom/helpshift/common/platform/network/Response;-><init>(ILjava/lang/String;Ljava/util/List;)V
    :try_end_6
    .catch Ljava/net/UnknownHostException; {:try_start_6 .. :try_end_6} :catch_9
    .catch Ljava/net/SocketException; {:try_start_6 .. :try_end_6} :catch_8
    .catch Ljava/lang/SecurityException; {:try_start_6 .. :try_end_6} :catch_8
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_6 .. :try_end_6} :catch_7
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_6 .. :try_end_6} :catch_6
    .catch Ljava/lang/Exception; {:try_start_6 .. :try_end_6} :catch_5
    .catchall {:try_start_6 .. :try_end_6} :catchall_1

    .line 349
    invoke-static {v9}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 350
    invoke-static {v6}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 351
    invoke-static {v3}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    if-eqz v1, :cond_7

    .line 354
    :try_start_7
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_7
    .catch Ljava/lang/Exception; {:try_start_7 .. :try_end_7} :catch_a

    goto :goto_a

    :catch_a
    move-exception p1

    const-string v0, "Helpshift_HTTPTrnsport"

    const-string v1, "Error in finally closing resources"

    .line 358
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_7
    :goto_a
    return-object v5

    .line 319
    :cond_8
    :try_start_8
    new-instance v3, Lcom/helpshift/common/platform/network/Response;

    invoke-direct {v3, v2, v0, v0}, Lcom/helpshift/common/platform/network/Response;-><init>(ILjava/lang/String;Ljava/util/List;)V
    :try_end_8
    .catch Ljava/net/UnknownHostException; {:try_start_8 .. :try_end_8} :catch_10
    .catch Ljava/net/SocketException; {:try_start_8 .. :try_end_8} :catch_f
    .catch Ljava/lang/SecurityException; {:try_start_8 .. :try_end_8} :catch_f
    .catch Ljavax/net/ssl/SSLPeerUnverifiedException; {:try_start_8 .. :try_end_8} :catch_e
    .catch Ljavax/net/ssl/SSLHandshakeException; {:try_start_8 .. :try_end_8} :catch_d
    .catch Ljava/lang/Exception; {:try_start_8 .. :try_end_8} :catch_c
    .catchall {:try_start_8 .. :try_end_8} :catchall_2

    .line 349
    invoke-static {v9}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 350
    invoke-static {v6}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 351
    invoke-static {v0}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    if-eqz v1, :cond_9

    .line 354
    :try_start_9
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_9
    .catch Ljava/lang/Exception; {:try_start_9 .. :try_end_9} :catch_b

    goto :goto_b

    :catch_b
    move-exception p1

    const-string v0, "Helpshift_HTTPTrnsport"

    const-string v1, "Error in finally closing resources"

    .line 358
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_9
    :goto_b
    return-object v3

    :catchall_2
    move-exception p1

    move-object v3, v0

    goto/16 :goto_12

    :catch_c
    move-exception v2

    move-object v3, v0

    goto/16 :goto_0

    :catch_d
    move-exception v2

    move-object v3, v0

    goto/16 :goto_1

    :catch_e
    move-exception v2

    move-object v3, v0

    goto/16 :goto_2

    :catch_f
    move-exception v2

    move-object v3, v0

    goto/16 :goto_3

    :catch_10
    move-exception v2

    move-object v3, v0

    goto/16 :goto_4

    :catchall_3
    move-exception p1

    move-object v3, v0

    goto/16 :goto_13

    :catch_11
    move-exception v2

    move-object v3, v0

    move-object v9, v3

    goto/16 :goto_0

    :catch_12
    move-exception v2

    move-object v3, v0

    move-object v9, v3

    goto/16 :goto_1

    :catch_13
    move-exception v2

    move-object v3, v0

    move-object v9, v3

    goto/16 :goto_2

    :catch_14
    move-exception v2

    move-object v3, v0

    move-object v9, v3

    goto/16 :goto_3

    :catch_15
    move-exception v2

    move-object v3, v0

    move-object v9, v3

    goto/16 :goto_4

    :catchall_4
    move-exception p1

    move-object v1, v0

    move-object v3, v1

    :goto_c
    move-object v6, v3

    goto :goto_13

    :catch_16
    move-exception v2

    move-object v3, v0

    move-object v6, v3

    move-object v9, v6

    .line 343
    :goto_d
    :try_start_a
    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->GENERIC:Lcom/helpshift/common/exception/NetworkException;

    .line 344
    iget-object p1, p1, Lcom/helpshift/common/platform/network/UploadRequest;->url:Ljava/lang/String;

    iput-object p1, v1, Lcom/helpshift/common/exception/NetworkException;->route:Ljava/lang/String;

    const-string p1, "Upload error"

    .line 345
    invoke-static {v2, v1, p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    :catch_17
    move-exception v2

    move-object v3, v0

    move-object v6, v3

    move-object v9, v6

    .line 338
    :goto_e
    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->SSL_HANDSHAKE:Lcom/helpshift/common/exception/NetworkException;

    .line 339
    iget-object p1, p1, Lcom/helpshift/common/platform/network/UploadRequest;->url:Ljava/lang/String;

    iput-object p1, v1, Lcom/helpshift/common/exception/NetworkException;->route:Ljava/lang/String;

    const-string p1, "Upload error"

    .line 340
    invoke-static {v2, v1, p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    :catch_18
    move-exception v2

    move-object v3, v0

    move-object v6, v3

    move-object v9, v6

    .line 333
    :goto_f
    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->SSL_PEER_UNVERIFIED:Lcom/helpshift/common/exception/NetworkException;

    .line 334
    iget-object p1, p1, Lcom/helpshift/common/platform/network/UploadRequest;->url:Ljava/lang/String;

    iput-object p1, v1, Lcom/helpshift/common/exception/NetworkException;->route:Ljava/lang/String;

    const-string p1, "Upload error"

    .line 335
    invoke-static {v2, v1, p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    :catch_19
    move-exception v2

    move-object v3, v0

    move-object v6, v3

    move-object v9, v6

    .line 328
    :goto_10
    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->NO_CONNECTION:Lcom/helpshift/common/exception/NetworkException;

    .line 329
    iget-object p1, p1, Lcom/helpshift/common/platform/network/UploadRequest;->url:Ljava/lang/String;

    iput-object p1, v1, Lcom/helpshift/common/exception/NetworkException;->route:Ljava/lang/String;

    const-string p1, "Upload error"

    .line 330
    invoke-static {v2, v1, p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    :catch_1a
    move-exception v2

    move-object v3, v0

    move-object v6, v3

    move-object v9, v6

    .line 323
    :goto_11
    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->UNKNOWN_HOST:Lcom/helpshift/common/exception/NetworkException;

    .line 324
    iget-object p1, p1, Lcom/helpshift/common/platform/network/UploadRequest;->url:Ljava/lang/String;

    iput-object p1, v1, Lcom/helpshift/common/exception/NetworkException;->route:Ljava/lang/String;

    const-string p1, "Upload error"

    .line 325
    invoke-static {v2, v1, p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
    :try_end_a
    .catchall {:try_start_a .. :try_end_a} :catchall_5

    :catchall_5
    move-exception p1

    move-object v1, v0

    :goto_12
    move-object v0, v9

    .line 349
    :goto_13
    invoke-static {v0}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 350
    invoke-static {v6}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 351
    invoke-static {v3}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    if-eqz v1, :cond_a

    .line 354
    :try_start_b
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_b
    .catch Ljava/lang/Exception; {:try_start_b .. :try_end_b} :catch_1b

    goto :goto_14

    :catch_1b
    move-exception v0

    const-string v1, "Helpshift_HTTPTrnsport"

    const-string v2, "Error in finally closing resources"

    .line 358
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 360
    :cond_a
    :goto_14
    throw p1
.end method


# virtual methods
.method public makeRequest(Lcom/helpshift/common/platform/network/Request;)Lcom/helpshift/common/platform/network/Response;
    .locals 1

    .line 50
    instance-of v0, p1, Lcom/helpshift/common/platform/network/UploadRequest;

    if-eqz v0, :cond_0

    .line 51
    check-cast p1, Lcom/helpshift/common/platform/network/UploadRequest;

    invoke-direct {p0, p1}, Lcom/helpshift/common/platform/AndroidHTTPTransport;->upload(Lcom/helpshift/common/platform/network/UploadRequest;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p1

    return-object p1

    .line 54
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/common/platform/AndroidHTTPTransport;->makeNetworkRequest(Lcom/helpshift/common/platform/network/Request;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p1

    return-object p1
.end method
