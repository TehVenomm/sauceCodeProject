.class public Lcom/helpshift/network/response/JsonArrayResponseParser;
.super Ljava/lang/Object;
.source "JsonArrayResponseParser.java"

# interfaces
.implements Lcom/helpshift/network/response/ResponseParser;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Lcom/helpshift/network/response/ResponseParser<",
        "Lorg/json/JSONArray;",
        ">;"
    }
.end annotation


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 32
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public parseResponse(Lcom/helpshift/network/response/NetworkResponse;)Lcom/helpshift/network/response/Response;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/network/response/NetworkResponse;",
            ")",
            "Lcom/helpshift/network/response/Response<",
            "Lorg/json/JSONArray;",
            ">;"
        }
    .end annotation

    .line 36
    :try_start_0
    new-instance v0, Ljava/lang/String;

    iget-object v1, p1, Lcom/helpshift/network/response/NetworkResponse;->data:[B

    iget-object v2, p1, Lcom/helpshift/network/response/NetworkResponse;->headers:Ljava/util/Map;

    const-string v3, "utf-8"

    .line 37
    invoke-static {v2, v3}, Lcom/helpshift/network/util/HttpHeaderParser;->parseCharset(Ljava/util/Map;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Ljava/lang/String;-><init>([BLjava/lang/String;)V

    .line 38
    new-instance v1, Lorg/json/JSONArray;

    invoke-direct {v1, v0}, Lorg/json/JSONArray;-><init>(Ljava/lang/String;)V

    iget-object v0, p1, Lcom/helpshift/network/response/NetworkResponse;->requestIdentifier:Ljava/lang/Integer;

    invoke-static {v1, v0}, Lcom/helpshift/network/response/Response;->success(Ljava/lang/Object;Ljava/lang/Integer;)Lcom/helpshift/network/response/Response;

    move-result-object v0
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    .line 44
    new-instance v1, Lcom/helpshift/network/errors/NetworkError;

    sget-object v2, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->PARSE_ERROR:Ljava/lang/Integer;

    invoke-direct {v1, v2, v0}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;Ljava/lang/Throwable;)V

    iget-object p1, p1, Lcom/helpshift/network/response/NetworkResponse;->requestIdentifier:Ljava/lang/Integer;

    invoke-static {v1, p1}, Lcom/helpshift/network/response/Response;->error(Lcom/helpshift/network/errors/NetworkError;Ljava/lang/Integer;)Lcom/helpshift/network/response/Response;

    move-result-object p1

    return-object p1

    :catch_1
    move-exception v0

    .line 41
    new-instance v1, Lcom/helpshift/network/errors/NetworkError;

    sget-object v2, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->PARSE_ERROR:Ljava/lang/Integer;

    invoke-direct {v1, v2, v0}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;Ljava/lang/Throwable;)V

    iget-object p1, p1, Lcom/helpshift/network/response/NetworkResponse;->requestIdentifier:Ljava/lang/Integer;

    invoke-static {v1, p1}, Lcom/helpshift/network/response/Response;->error(Lcom/helpshift/network/errors/NetworkError;Ljava/lang/Integer;)Lcom/helpshift/network/response/Response;

    move-result-object p1

    return-object p1
.end method
