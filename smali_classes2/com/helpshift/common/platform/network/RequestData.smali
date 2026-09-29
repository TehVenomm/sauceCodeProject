.class public Lcom/helpshift/common/platform/network/RequestData;
.super Ljava/lang/Object;
.source "RequestData.java"


# instance fields
.field public final body:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private customHeaders:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private requestId:Ljava/lang/String;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/network/RequestData;)V
    .locals 1

    .line 27
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 28
    iget-object v0, p1, Lcom/helpshift/common/platform/network/RequestData;->body:Ljava/util/Map;

    iput-object v0, p0, Lcom/helpshift/common/platform/network/RequestData;->body:Ljava/util/Map;

    .line 29
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/common/platform/network/RequestData;->requestId:Ljava/lang/String;

    .line 30
    iget-object p1, p1, Lcom/helpshift/common/platform/network/RequestData;->customHeaders:Ljava/util/Map;

    iput-object p1, p0, Lcom/helpshift/common/platform/network/RequestData;->customHeaders:Ljava/util/Map;

    return-void
.end method

.method public constructor <init>(Ljava/util/Map;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 23
    iput-object p1, p0, Lcom/helpshift/common/platform/network/RequestData;->body:Ljava/util/Map;

    .line 24
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object p1

    invoke-virtual {p1}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/platform/network/RequestData;->requestId:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getCustomHeaders()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 42
    iget-object v0, p0, Lcom/helpshift/common/platform/network/RequestData;->customHeaders:Ljava/util/Map;

    return-object v0
.end method

.method public getRequestId()Ljava/lang/String;
    .locals 1

    .line 38
    iget-object v0, p0, Lcom/helpshift/common/platform/network/RequestData;->requestId:Ljava/lang/String;

    return-object v0
.end method

.method public overrideRequestId(Ljava/lang/String;)V
    .locals 0

    .line 34
    iput-object p1, p0, Lcom/helpshift/common/platform/network/RequestData;->requestId:Ljava/lang/String;

    return-void
.end method

.method public setCustomHeaders(Ljava/util/Map;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 46
    iput-object p1, p0, Lcom/helpshift/common/platform/network/RequestData;->customHeaders:Ljava/util/Map;

    return-void
.end method
