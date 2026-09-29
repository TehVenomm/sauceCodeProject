.class public Lcom/helpshift/common/domain/network/MetaCorrectedNetwork;
.super Ljava/lang/Object;
.source "MetaCorrectedNetwork.java"

# interfaces
.implements Lcom/helpshift/common/domain/network/Network;


# instance fields
.field private final jsonifier:Lcom/helpshift/common/platform/Jsonifier;

.field private final network:Lcom/helpshift/common/domain/network/Network;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V
    .locals 0

    .line 19
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 20
    iput-object p1, p0, Lcom/helpshift/common/domain/network/MetaCorrectedNetwork;->network:Lcom/helpshift/common/domain/network/Network;

    .line 21
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getJsonifier()Lcom/helpshift/common/platform/Jsonifier;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/domain/network/MetaCorrectedNetwork;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    return-void
.end method

.method private makeRequest(Lcom/helpshift/common/platform/network/RequestData;I)Lcom/helpshift/common/platform/network/Response;
    .locals 3

    .line 30
    iget-object v0, p0, Lcom/helpshift/common/domain/network/MetaCorrectedNetwork;->network:Lcom/helpshift/common/domain/network/Network;

    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object v0

    .line 31
    iget v1, v0, Lcom/helpshift/common/platform/network/Response;->status:I

    const/16 v2, 0x19d

    if-ne v1, v2, :cond_1

    if-lez p2, :cond_0

    add-int/lit8 p2, p2, -0x1

    .line 34
    new-instance v0, Ljava/util/HashMap;

    iget-object p1, p1, Lcom/helpshift/common/platform/network/RequestData;->body:Ljava/util/Map;

    invoke-direct {v0, p1}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 35
    invoke-direct {p0, v0}, Lcom/helpshift/common/domain/network/MetaCorrectedNetwork;->removeCIFAndCustomMetadata(Ljava/util/Map;)V

    .line 36
    new-instance p1, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {p1, v0}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    .line 37
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/domain/network/MetaCorrectedNetwork;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;I)Lcom/helpshift/common/platform/network/Response;

    move-result-object p1

    return-object p1

    :cond_0
    const/4 p1, 0x0

    .line 40
    sget-object p2, Lcom/helpshift/common/exception/NetworkException;->ENTITY_TOO_LARGE_RETRIES_EXHAUSTED:Lcom/helpshift/common/exception/NetworkException;

    invoke-static {p1, p2}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    :cond_1
    return-object v0
.end method

.method private removeCIFAndCustomMetadata(Ljava/util/Map;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    const-string v0, "meta"

    .line 49
    invoke-interface {p1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    const-string v1, "meta"

    .line 50
    iget-object v2, p0, Lcom/helpshift/common/domain/network/MetaCorrectedNetwork;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    const-string v3, "custom_meta"

    invoke-interface {v2, v0, v3}, Lcom/helpshift/common/platform/Jsonifier;->removeKeyFromJsonObjString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-interface {p1, v1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "custom_fields"

    .line 51
    invoke-interface {p1, v0}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method


# virtual methods
.method public makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;
    .locals 1

    const/4 v0, 0x1

    .line 26
    invoke-direct {p0, p1, v0}, Lcom/helpshift/common/domain/network/MetaCorrectedNetwork;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;I)Lcom/helpshift/common/platform/network/Response;

    move-result-object p1

    return-object p1
.end method
