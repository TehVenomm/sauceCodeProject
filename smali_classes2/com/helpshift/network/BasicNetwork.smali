.class public Lcom/helpshift/network/BasicNetwork;
.super Ljava/lang/Object;
.source "BasicNetwork.java"

# interfaces
.implements Lcom/helpshift/network/Network;


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_BasicNetwork"


# instance fields
.field protected final httpStack:Lcom/helpshift/network/HttpStack;


# direct methods
.method public constructor <init>(Lcom/helpshift/network/HttpStack;)V
    .locals 0

    .line 52
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 53
    iput-object p1, p0, Lcom/helpshift/network/BasicNetwork;->httpStack:Lcom/helpshift/network/HttpStack;

    return-void
.end method

.method protected static convertHeaders([Lcom/helpshift/network/Header;)Ljava/util/Map;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([",
            "Lcom/helpshift/network/Header;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 57
    new-instance v0, Ljava/util/TreeMap;

    sget-object v1, Ljava/lang/String;->CASE_INSENSITIVE_ORDER:Ljava/util/Comparator;

    invoke-direct {v0, v1}, Ljava/util/TreeMap;-><init>(Ljava/util/Comparator;)V

    .line 58
    array-length v1, p0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_0

    aget-object v3, p0, v2

    .line 59
    iget-object v4, v3, Lcom/helpshift/network/Header;->name:Ljava/lang/String;

    iget-object v3, v3, Lcom/helpshift/network/Header;->value:Ljava/lang/String;

    invoke-interface {v0, v4, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_0
    return-object v0
.end method


# virtual methods
.method protected entityToBytes(Lcom/helpshift/network/HttpEntity;)[B
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/helpshift/network/errors/NetworkError;
        }
    .end annotation

    .line 154
    new-instance v0, Ljava/io/ByteArrayOutputStream;

    invoke-direct {v0}, Ljava/io/ByteArrayOutputStream;-><init>()V

    .line 157
    :try_start_0
    iget-object v1, p1, Lcom/helpshift/network/HttpEntity;->content:Ljava/io/InputStream;

    if-eqz v1, :cond_1

    const/16 v2, 0x2000

    .line 161
    new-array v2, v2, [B

    .line 163
    :goto_0
    invoke-virtual {v1, v2}, Ljava/io/InputStream;->read([B)I

    move-result v3

    const/4 v4, -0x1

    if-eq v3, v4, :cond_0

    const/4 v4, 0x0

    .line 164
    invoke-virtual {v0, v2, v4, v3}, Ljava/io/ByteArrayOutputStream;->write([BII)V

    goto :goto_0

    .line 166
    :cond_0
    invoke-virtual {v0}, Ljava/io/ByteArrayOutputStream;->toByteArray()[B

    move-result-object v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 171
    :try_start_1
    invoke-virtual {p1}, Lcom/helpshift/network/HttpEntity;->consumeContent()V
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_1

    :catch_0
    move-exception p1

    const-string v2, "Helpshift_BasicNetwork"

    const-string v3, "Error occurred when calling consumingContent"

    .line 176
    invoke-static {v2, v3, p1}, Lcom/helpshift/util/HSLogger;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 178
    :goto_1
    invoke-virtual {v0}, Ljava/io/ByteArrayOutputStream;->close()V

    return-object v1

    .line 159
    :cond_1
    :try_start_2
    new-instance v1, Lcom/helpshift/network/errors/NetworkError;

    sget-object v2, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->SERVER_ERROR:Ljava/lang/Integer;

    invoke-direct {v1, v2}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;)V

    throw v1
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    :catchall_0
    move-exception v1

    .line 171
    :try_start_3
    invoke-virtual {p1}, Lcom/helpshift/network/HttpEntity;->consumeContent()V
    :try_end_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_1

    goto :goto_2

    :catch_1
    move-exception p1

    const-string v2, "Helpshift_BasicNetwork"

    const-string v3, "Error occurred when calling consumingContent"

    .line 176
    invoke-static {v2, v3, p1}, Lcom/helpshift/util/HSLogger;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 178
    :goto_2
    invoke-virtual {v0}, Ljava/io/ByteArrayOutputStream;->close()V

    .line 179
    throw v1
.end method

.method public performRequest(Lcom/helpshift/network/request/Request;)Lcom/helpshift/network/response/NetworkResponse;
    .locals 11
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/network/errors/NetworkError;
        }
    .end annotation

    :cond_0
    :goto_0
    const/4 v0, 0x0

    .line 71
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/network/BasicNetwork;->httpStack:Lcom/helpshift/network/HttpStack;

    invoke-interface {v1, p1}, Lcom/helpshift/network/HttpStack;->performRequest(Lcom/helpshift/network/request/Request;)Lcom/helpshift/network/HttpResponse;

    move-result-object v1
    :try_end_0
    .catch Ljava/net/SocketTimeoutException; {:try_start_0 .. :try_end_0} :catch_9
    .catch Ljava/net/MalformedURLException; {:try_start_0 .. :try_end_0} :catch_8
    .catch Lcom/helpshift/exceptions/InstallException; {:try_start_0 .. :try_end_0} :catch_7
    .catch Ljava/lang/SecurityException; {:try_start_0 .. :try_end_0} :catch_6
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_5
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 72
    :try_start_1
    invoke-virtual {v1}, Lcom/helpshift/network/HttpResponse;->getStatusLine()Lcom/helpshift/network/StatusLine;

    move-result-object v0

    .line 73
    invoke-virtual {v0}, Lcom/helpshift/network/StatusLine;->getStatusCode()I

    move-result v3

    .line 75
    invoke-virtual {v1}, Lcom/helpshift/network/HttpResponse;->getAllHeaders()[Lcom/helpshift/network/Header;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/network/BasicNetwork;->convertHeaders([Lcom/helpshift/network/Header;)Ljava/util/Map;

    move-result-object v7

    if-eqz v7, :cond_1

    const-string v0, "ETag"

    .line 77
    invoke-interface {v7, v0}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 78
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/model/InfoModelFactory;->sdkInfoModel:Lcom/helpshift/model/SdkInfoModel;

    const-string v2, "ETag"

    invoke-interface {v7, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    iget-object v4, p1, Lcom/helpshift/network/request/Request;->url:Ljava/lang/String;

    invoke-virtual {v0, v2, v4}, Lcom/helpshift/model/SdkInfoModel;->addEtag(Ljava/lang/String;Ljava/lang/String;)V

    :cond_1
    const/16 v0, 0x130

    if-ne v3, v0, :cond_3

    .line 82
    new-instance v0, Lcom/helpshift/network/response/NetworkResponse;

    const/16 v5, 0x130

    const/4 v6, 0x0

    const/4 v8, 0x1

    .line 83
    invoke-virtual {p1}, Lcom/helpshift/network/request/Request;->getSequence()I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v9

    move-object v4, v0

    invoke-direct/range {v4 .. v9}, Lcom/helpshift/network/response/NetworkResponse;-><init>(I[BLjava/util/Map;ZLjava/lang/Integer;)V
    :try_end_1
    .catch Ljava/net/SocketTimeoutException; {:try_start_1 .. :try_end_1} :catch_4
    .catch Ljava/net/MalformedURLException; {:try_start_1 .. :try_end_1} :catch_3
    .catch Lcom/helpshift/exceptions/InstallException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljava/lang/SecurityException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    if-eqz v1, :cond_2

    .line 146
    invoke-virtual {v1}, Lcom/helpshift/network/HttpResponse;->getHelpshiftSSLSocketFactory()Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 147
    invoke-virtual {v1}, Lcom/helpshift/network/HttpResponse;->getHelpshiftSSLSocketFactory()Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;->closeSockets()V

    :cond_2
    return-object v0

    .line 86
    :cond_3
    :try_start_2
    invoke-virtual {v1}, Lcom/helpshift/network/HttpResponse;->getEntity()Lcom/helpshift/network/HttpEntity;

    move-result-object v0

    if-eqz v0, :cond_4

    .line 87
    invoke-virtual {v1}, Lcom/helpshift/network/HttpResponse;->getEntity()Lcom/helpshift/network/HttpEntity;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/network/BasicNetwork;->entityToBytes(Lcom/helpshift/network/HttpEntity;)[B

    move-result-object v0

    :goto_1
    move-object v4, v0

    goto :goto_2

    .line 89
    :cond_4
    iget v0, p1, Lcom/helpshift/network/request/Request;->method:I

    if-nez v0, :cond_f

    const/4 v0, 0x0

    .line 90
    new-array v0, v0, [B

    goto :goto_1

    :goto_2
    const/16 v0, 0xc8

    if-lt v3, v0, :cond_6

    const/16 v0, 0x12c

    if-gt v3, v0, :cond_6

    .line 96
    new-instance v0, Lcom/helpshift/network/response/NetworkResponse;

    const/4 v6, 0x0

    invoke-virtual {p1}, Lcom/helpshift/network/request/Request;->getSequence()I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v8

    move-object v2, v0

    move-object v5, v7

    move-object v7, v8

    invoke-direct/range {v2 .. v7}, Lcom/helpshift/network/response/NetworkResponse;-><init>(I[BLjava/util/Map;ZLjava/lang/Integer;)V
    :try_end_2
    .catch Ljava/net/SocketTimeoutException; {:try_start_2 .. :try_end_2} :catch_4
    .catch Ljava/net/MalformedURLException; {:try_start_2 .. :try_end_2} :catch_3
    .catch Lcom/helpshift/exceptions/InstallException; {:try_start_2 .. :try_end_2} :catch_2
    .catch Ljava/lang/SecurityException; {:try_start_2 .. :try_end_2} :catch_1
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz v1, :cond_5

    .line 146
    invoke-virtual {v1}, Lcom/helpshift/network/HttpResponse;->getHelpshiftSSLSocketFactory()Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    move-result-object p1

    if-eqz p1, :cond_5

    .line 147
    invoke-virtual {v1}, Lcom/helpshift/network/HttpResponse;->getHelpshiftSSLSocketFactory()Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;->closeSockets()V

    :cond_5
    return-object v0

    :cond_6
    const/16 v0, 0x1a6

    if-ne v3, v0, :cond_9

    if-eqz v7, :cond_8

    .line 100
    :try_start_3
    invoke-interface {v7}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v0

    .line 101
    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_3
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_8

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    .line 102
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    const-string v4, "HS-UEpoch"

    invoke-virtual {v3, v4}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-nez v3, :cond_7

    goto :goto_3

    .line 103
    :cond_7
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/model/InfoModelFactory;->sdkInfoModel:Lcom/helpshift/model/SdkInfoModel;

    .line 104
    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-static {v2}, Lcom/helpshift/common/util/HSDateFormatSpec;->calculateTimeDelta(Ljava/lang/String;)F

    move-result v2

    invoke-static {v2}, Ljava/lang/Float;->valueOf(F)Ljava/lang/Float;

    move-result-object v2

    invoke-virtual {v0, v2}, Lcom/helpshift/model/SdkInfoModel;->setServerTimeDelta(Ljava/lang/Float;)V

    .line 109
    :cond_8
    new-instance v0, Lcom/helpshift/network/errors/NetworkError;

    sget-object v2, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->TIMESTAMP_MISMATCH:Ljava/lang/Integer;

    invoke-direct {v0, v2}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;)V

    throw v0

    :cond_9
    const/16 v0, 0x19d

    if-eq v3, v0, :cond_e

    const/16 v0, 0x193

    if-eq v3, v0, :cond_d

    const/16 v0, 0x191

    if-eq v3, v0, :cond_d

    const/16 v0, 0x190

    const/16 v2, 0x1f4

    if-lt v3, v0, :cond_b

    if-lt v3, v2, :cond_a

    goto :goto_4

    .line 118
    :cond_a
    new-instance v0, Lcom/helpshift/network/errors/NetworkError;

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-direct {v0, v2}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;)V

    throw v0
    :try_end_3
    .catch Ljava/net/SocketTimeoutException; {:try_start_3 .. :try_end_3} :catch_4
    .catch Ljava/net/MalformedURLException; {:try_start_3 .. :try_end_3} :catch_3
    .catch Lcom/helpshift/exceptions/InstallException; {:try_start_3 .. :try_end_3} :catch_2
    .catch Ljava/lang/SecurityException; {:try_start_3 .. :try_end_3} :catch_1
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    :cond_b
    :goto_4
    if-ge v3, v2, :cond_c

    if-eqz v1, :cond_0

    .line 146
    invoke-virtual {v1}, Lcom/helpshift/network/HttpResponse;->getHelpshiftSSLSocketFactory()Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 147
    invoke-virtual {v1}, Lcom/helpshift/network/HttpResponse;->getHelpshiftSSLSocketFactory()Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;->closeSockets()V

    goto/16 :goto_0

    .line 121
    :cond_c
    :try_start_4
    new-instance v0, Lcom/helpshift/network/errors/NetworkError;

    sget-object v2, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->SERVER_ERROR:Ljava/lang/Integer;

    invoke-direct {v0, v2}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;)V

    throw v0

    .line 115
    :cond_d
    new-instance v0, Lcom/helpshift/network/errors/NetworkError;

    sget-object v2, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->UNAUTHORIZED_ACCESS:Ljava/lang/Integer;

    invoke-direct {v0, v2}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;)V

    throw v0

    .line 112
    :cond_e
    new-instance v0, Lcom/helpshift/network/errors/NetworkError;

    sget-object v2, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->ENTITY_TOO_LARGE:Ljava/lang/Integer;

    invoke-direct {v0, v2}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;)V

    throw v0

    .line 93
    :cond_f
    new-instance v0, Lcom/helpshift/network/errors/NetworkError;

    sget-object v2, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->CONTENT_NOT_FOUND:Ljava/lang/Integer;

    invoke-direct {v0, v2}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;)V

    throw v0
    :try_end_4
    .catch Ljava/net/SocketTimeoutException; {:try_start_4 .. :try_end_4} :catch_4
    .catch Ljava/net/MalformedURLException; {:try_start_4 .. :try_end_4} :catch_3
    .catch Lcom/helpshift/exceptions/InstallException; {:try_start_4 .. :try_end_4} :catch_2
    .catch Ljava/lang/SecurityException; {:try_start_4 .. :try_end_4} :catch_1
    .catch Ljava/io/IOException; {:try_start_4 .. :try_end_4} :catch_0
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    :catch_0
    move-exception p1

    move-object v0, v1

    goto :goto_5

    :catch_1
    move-object v0, v1

    goto :goto_6

    :catch_2
    move-exception p1

    move-object v0, v1

    goto :goto_7

    :catch_3
    move-exception v0

    goto :goto_8

    :catch_4
    move-object v0, v1

    goto :goto_9

    :catchall_0
    move-exception p1

    move-object v1, v0

    goto :goto_a

    :catch_5
    move-exception p1

    :goto_5
    if-nez v0, :cond_10

    .line 139
    :try_start_5
    new-instance v1, Lcom/helpshift/network/errors/NetworkError;

    sget-object v2, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->NO_CONNECTION:Ljava/lang/Integer;

    invoke-virtual {p1}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v1, v2, p1}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;Ljava/lang/String;)V

    throw v1

    .line 142
    :cond_10
    new-instance v1, Lcom/helpshift/network/errors/NetworkError;

    invoke-direct {v1, p1}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Throwable;)V

    throw v1

    .line 135
    :catch_6
    :goto_6
    new-instance p1, Lcom/helpshift/network/errors/NetworkError;

    sget-object v1, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->NO_CONNECTION:Ljava/lang/Integer;

    invoke-direct {p1, v1}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;)V

    throw p1

    :catch_7
    move-exception p1

    .line 132
    :goto_7
    new-instance v1, Lcom/helpshift/network/errors/NetworkError;

    invoke-direct {v1, p1}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Throwable;)V

    throw v1
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_0

    :catch_8
    move-exception v1

    move-object v10, v1

    move-object v1, v0

    move-object v0, v10

    .line 129
    :goto_8
    :try_start_6
    new-instance v2, Lcom/helpshift/network/errors/NetworkError;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Bad URL "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object p1, p1, Lcom/helpshift/network/request/Request;->url:Ljava/lang/String;

    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v2, p1, v0}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v2
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_1

    :catchall_1
    move-exception p1

    goto :goto_a

    .line 126
    :catch_9
    :goto_9
    :try_start_7
    new-instance p1, Lcom/helpshift/network/errors/NetworkError;

    sget-object v1, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->REQUEST_TIMEOUT:Ljava/lang/Integer;

    invoke-direct {p1, v1}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;)V

    throw p1
    :try_end_7
    .catchall {:try_start_7 .. :try_end_7} :catchall_0

    :goto_a
    if-eqz v1, :cond_11

    .line 146
    invoke-virtual {v1}, Lcom/helpshift/network/HttpResponse;->getHelpshiftSSLSocketFactory()Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    move-result-object v0

    if-eqz v0, :cond_11

    .line 147
    invoke-virtual {v1}, Lcom/helpshift/network/HttpResponse;->getHelpshiftSSLSocketFactory()Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;->closeSockets()V

    .line 149
    :cond_11
    throw p1
.end method
