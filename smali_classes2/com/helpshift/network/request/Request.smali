.class public Lcom/helpshift/network/request/Request;
.super Ljava/lang/Object;
.source "Request.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/network/request/Request$Method;
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "HS_Request"

.field private static sequenceGenerator:Ljava/util/concurrent/atomic/AtomicInteger;


# instance fields
.field private final errorListener:Lcom/helpshift/network/response/Response$ErrorListener;

.field private listener:Lcom/helpshift/network/response/Response$Listener;

.field public final method:I

.field private requestData:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private responseDelivered:Z

.field private responseParser:Lcom/helpshift/network/response/ResponseParser;

.field private sequence:Ljava/lang/Integer;

.field public final url:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 40
    new-instance v0, Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-direct {v0}, Ljava/util/concurrent/atomic/AtomicInteger;-><init>()V

    sput-object v0, Lcom/helpshift/network/request/Request;->sequenceGenerator:Ljava/util/concurrent/atomic/AtomicInteger;

    return-void
.end method

.method public constructor <init>(ILjava/lang/String;Ljava/util/Map;Lcom/helpshift/network/response/Response$Listener;Lcom/helpshift/network/response/Response$ErrorListener;Lcom/helpshift/network/response/ResponseParser;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(I",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Lcom/helpshift/network/response/Response$Listener<",
            "TT;>;",
            "Lcom/helpshift/network/response/Response$ErrorListener;",
            "Lcom/helpshift/network/response/ResponseParser<",
            "TT;>;)V"
        }
    .end annotation

    .line 55
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 45
    iput-boolean v0, p0, Lcom/helpshift/network/request/Request;->responseDelivered:Z

    .line 56
    iput p1, p0, Lcom/helpshift/network/request/Request;->method:I

    .line 57
    invoke-direct {p0, p2}, Lcom/helpshift/network/request/Request;->sanitiseUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/network/request/Request;->url:Ljava/lang/String;

    .line 58
    iput-object p4, p0, Lcom/helpshift/network/request/Request;->listener:Lcom/helpshift/network/response/Response$Listener;

    .line 59
    iput-object p5, p0, Lcom/helpshift/network/request/Request;->errorListener:Lcom/helpshift/network/response/Response$ErrorListener;

    .line 60
    iput-object p3, p0, Lcom/helpshift/network/request/Request;->requestData:Ljava/util/Map;

    .line 61
    sget-object p1, Lcom/helpshift/network/request/Request;->sequenceGenerator:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {p1}, Ljava/util/concurrent/atomic/AtomicInteger;->incrementAndGet()I

    move-result p1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/network/request/Request;->sequence:Ljava/lang/Integer;

    .line 62
    iput-object p6, p0, Lcom/helpshift/network/request/Request;->responseParser:Lcom/helpshift/network/response/ResponseParser;

    return-void
.end method

.method private addAuth()Ljava/util/Map;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/exceptions/InstallException;
        }
    .end annotation

    .line 129
    invoke-direct {p0}, Lcom/helpshift/network/request/Request;->getApiUri()Ljava/lang/String;

    move-result-object v0

    .line 132
    iget-object v1, p0, Lcom/helpshift/network/request/Request;->requestData:Ljava/util/Map;

    if-eqz v1, :cond_0

    .line 133
    new-instance v1, Ljava/util/HashMap;

    iget-object v2, p0, Lcom/helpshift/network/request/Request;->requestData:Ljava/util/Map;

    invoke-direct {v1, v2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    goto :goto_0

    .line 136
    :cond_0
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 138
    :goto_0
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v2

    iget-object v2, v2, Lcom/helpshift/model/InfoModelFactory;->appInfoModel:Lcom/helpshift/model/AppInfoModel;

    invoke-virtual {v2}, Lcom/helpshift/model/AppInfoModel;->isInstalled()Z

    move-result v2

    if-eqz v2, :cond_5

    const-string v2, "platform-id"

    .line 141
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v3

    iget-object v3, v3, Lcom/helpshift/model/InfoModelFactory;->appInfoModel:Lcom/helpshift/model/AppInfoModel;

    iget-object v3, v3, Lcom/helpshift/model/AppInfoModel;->platformId:Ljava/lang/String;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "method"

    .line 142
    invoke-virtual {p0}, Lcom/helpshift/network/request/Request;->getMethodString()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "uri"

    .line 143
    invoke-interface {v1, v2, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 144
    invoke-static {}, Lcom/helpshift/util/TimeUtil;->getCurrentTimestamp()Ljava/lang/String;

    move-result-object v0

    .line 145
    invoke-static {v0}, Lcom/helpshift/util/SchemaUtil;->validateTimestamp(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_1

    const-string v2, "timestamp"

    .line 146
    invoke-interface {v1, v2, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 149
    :cond_1
    new-instance v0, Ljava/util/ArrayList;

    invoke-interface {v1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v2

    invoke-direct {v0, v2}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 150
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 152
    invoke-static {v0}, Ljava/util/Collections;->sort(Ljava/util/List;)V

    .line 153
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_2
    :goto_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_3

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    const-string v4, "screenshot"

    .line 154
    invoke-virtual {v3, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-nez v4, :cond_2

    const-string v4, "meta"

    invoke-virtual {v3, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-nez v4, :cond_2

    .line 155
    invoke-interface {v1, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    invoke-static {v4}, Lcom/helpshift/util/StringUtil;->toString(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v4

    if-eqz v4, :cond_2

    .line 157
    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v5, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "="

    invoke-virtual {v5, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v2, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 162
    :cond_3
    :try_start_0
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/model/InfoModelFactory;->appInfoModel:Lcom/helpshift/model/AppInfoModel;

    iget-object v0, v0, Lcom/helpshift/model/AppInfoModel;->apiKey:Ljava/lang/String;

    .line 163
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v3

    iget-object v3, v3, Lcom/helpshift/model/InfoModelFactory;->appInfoModel:Lcom/helpshift/model/AppInfoModel;

    invoke-virtual {v3}, Lcom/helpshift/model/AppInfoModel;->isInstalled()Z

    move-result v3

    if-eqz v3, :cond_4

    .line 166
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v3

    invoke-interface {v3}, Lcom/helpshift/CoreApi;->getCryptoDM()Lcom/helpshift/crypto/CryptoDM;

    move-result-object v3

    const-string v4, "&"

    invoke-static {v4, v2}, Landroid/text/TextUtils;->join(Ljava/lang/CharSequence;Ljava/lang/Iterable;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v3, v2, v0}, Lcom/helpshift/crypto/CryptoDM;->getSignature(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v2, "signature"

    .line 167
    invoke-interface {v1, v2, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "method"

    .line 168
    invoke-interface {v1, v0}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "uri"

    .line 169
    invoke-interface {v1, v0}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_2

    .line 164
    :cond_4
    new-instance v0, Lcom/helpshift/exceptions/InstallException;

    const-string v2, "Install information missing"

    invoke-direct {v0, v2}, Lcom/helpshift/exceptions/InstallException;-><init>(Ljava/lang/String;)V

    throw v0
    :try_end_0
    .catch Ljava/security/GeneralSecurityException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :goto_2
    return-object v1

    .line 139
    :cond_5
    new-instance v0, Lcom/helpshift/exceptions/InstallException;

    const-string v1, "appId Missing"

    invoke-direct {v0, v1}, Lcom/helpshift/exceptions/InstallException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private encodeGetParameters(Ljava/util/Map;)Ljava/lang/String;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .line 179
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 180
    new-instance v1, Ljava/util/ArrayList;

    invoke-interface {p1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v2

    invoke-direct {v1, v2}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 181
    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 182
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, "="

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-interface {p1, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-static {v2}, Landroid/net/Uri;->encode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_0
    const-string p1, "&"

    .line 184
    invoke-static {p1, v0}, Landroid/text/TextUtils;->join(Ljava/lang/CharSequence;Ljava/lang/Iterable;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private encodePostParameters(Ljava/util/Map;)Ljava/util/List;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/network/NameValuePair;",
            ">;"
        }
    .end annotation

    .line 188
    new-instance v0, Ljava/util/ArrayList;

    invoke-interface {p1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 189
    new-instance v1, Ljava/util/ArrayList;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v2

    invoke-direct {v1, v2}, Ljava/util/ArrayList;-><init>(I)V

    .line 190
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 191
    invoke-interface {p1, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    invoke-static {v3}, Lcom/helpshift/util/StringUtil;->toString(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_0

    .line 193
    new-instance v4, Lcom/helpshift/network/NameValuePair;

    invoke-direct {v4, v2, v3}, Lcom/helpshift/network/NameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v1, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    return-object v1
.end method

.method private getApiUri()Ljava/lang/String;
    .locals 2

    .line 110
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "/api/lib/3"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/network/request/Request;->url:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method private sanitiseUrl(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    const-string v0, "/"

    .line 71
    invoke-virtual {p1, v0}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 72
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    :cond_0
    return-object p1
.end method


# virtual methods
.method public deliverError(Lcom/helpshift/network/errors/NetworkError;)V
    .locals 2

    .line 247
    iget-object v0, p0, Lcom/helpshift/network/request/Request;->errorListener:Lcom/helpshift/network/response/Response$ErrorListener;

    if-eqz v0, :cond_0

    .line 248
    iget-object v0, p0, Lcom/helpshift/network/request/Request;->errorListener:Lcom/helpshift/network/response/Response$ErrorListener;

    invoke-virtual {p0}, Lcom/helpshift/network/request/Request;->getSequence()I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-interface {v0, p1, v1}, Lcom/helpshift/network/response/Response$ErrorListener;->onErrorResponse(Lcom/helpshift/network/errors/NetworkError;Ljava/lang/Integer;)V

    :cond_0
    return-void
.end method

.method public deliverResponse(Ljava/lang/Object;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(TT;)V"
        }
    .end annotation

    .line 242
    iget-object v0, p0, Lcom/helpshift/network/request/Request;->listener:Lcom/helpshift/network/response/Response$Listener;

    .line 243
    invoke-virtual {p0}, Lcom/helpshift/network/request/Request;->getSequence()I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-interface {v0, p1, v1}, Lcom/helpshift/network/response/Response$Listener;->onResponse(Ljava/lang/Object;Ljava/lang/Integer;)V

    return-void
.end method

.method public getFullUri()Ljava/lang/String;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/exceptions/InstallException;
        }
    .end annotation

    .line 114
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/model/InfoModelFactory;->appInfoModel:Lcom/helpshift/model/AppInfoModel;

    invoke-virtual {v0}, Lcom/helpshift/model/AppInfoModel;->isInstalled()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 117
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    sget-object v1, Lcom/helpshift/common/domain/network/NetworkConstants;->scheme:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v1

    iget-object v1, v1, Lcom/helpshift/model/InfoModelFactory;->appInfoModel:Lcom/helpshift/model/AppInfoModel;

    iget-object v1, v1, Lcom/helpshift/model/AppInfoModel;->domainName:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-direct {p0}, Lcom/helpshift/network/request/Request;->getApiUri()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0

    .line 115
    :cond_0
    new-instance v0, Lcom/helpshift/exceptions/InstallException;

    const-string v1, "Install information missing"

    invoke-direct {v0, v1}, Lcom/helpshift/exceptions/InstallException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public getHeaders()Ljava/util/Map;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 96
    invoke-static {}, Lcom/helpshift/network/util/HeaderUtil;->getCommonHeaders()Ljava/util/Map;

    move-result-object v0

    .line 97
    iget v1, p0, Lcom/helpshift/network/request/Request;->method:I

    if-nez v1, :cond_0

    .line 98
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v1

    iget-object v1, v1, Lcom/helpshift/model/InfoModelFactory;->sdkInfoModel:Lcom/helpshift/model/SdkInfoModel;

    iget-object v2, p0, Lcom/helpshift/network/request/Request;->url:Ljava/lang/String;

    invoke-virtual {v1, v2}, Lcom/helpshift/model/SdkInfoModel;->getEtag(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 99
    invoke-static {v1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v2

    if-nez v2, :cond_1

    const-string v2, "If-None-Match"

    .line 100
    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 103
    :cond_0
    iget v1, p0, Lcom/helpshift/network/request/Request;->method:I

    const/4 v2, 0x1

    if-ne v1, v2, :cond_1

    const-string v1, "Content-type"

    const-string v2, "application/x-www-form-urlencoded"

    .line 104
    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    :goto_0
    return-object v0
.end method

.method public getMethodString()Ljava/lang/String;
    .locals 1

    .line 78
    iget v0, p0, Lcom/helpshift/network/request/Request;->method:I

    packed-switch v0, :pswitch_data_0

    const-string v0, ""

    return-object v0

    :pswitch_0
    const-string v0, "POST"

    return-object v0

    :pswitch_1
    const-string v0, "GET"

    return-object v0

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public getPOSTParametersQuery()Ljava/lang/String;
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/exceptions/InstallException;
        }
    .end annotation

    .line 200
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    .line 203
    invoke-direct {p0}, Lcom/helpshift/network/request/Request;->addAuth()Ljava/util/Map;

    move-result-object v1

    invoke-direct {p0, v1}, Lcom/helpshift/network/request/Request;->encodePostParameters(Ljava/util/Map;)Ljava/util/List;

    move-result-object v1

    .line 205
    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    const/4 v2, 0x1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/network/NameValuePair;

    if-eqz v2, :cond_0

    const/4 v2, 0x0

    goto :goto_1

    :cond_0
    const-string v4, "&"

    .line 210
    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 213
    :goto_1
    :try_start_0
    iget-object v4, v3, Lcom/helpshift/network/NameValuePair;->name:Ljava/lang/String;

    const-string v5, "UTF-8"

    invoke-static {v4, v5}, Ljava/net/URLEncoder;->encode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, "="

    .line 214
    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 215
    iget-object v3, v3, Lcom/helpshift/network/NameValuePair;->value:Ljava/lang/String;

    const-string v4, "UTF-8"

    invoke-static {v3, v4}, Ljava/net/URLEncoder;->encode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v3

    const-string v4, "HS_Request"

    const-string v5, "Exception Unsupported Encoding"

    .line 218
    invoke-static {v4, v5, v3}, Lcom/helpshift/util/HSLogger;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0

    .line 221
    :cond_1
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getParsedURL()Ljava/net/URL;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/exceptions/InstallException;,
            Ljava/net/MalformedURLException;
        }
    .end annotation

    .line 121
    invoke-virtual {p0}, Lcom/helpshift/network/request/Request;->getFullUri()Ljava/lang/String;

    move-result-object v0

    .line 122
    iget v1, p0, Lcom/helpshift/network/request/Request;->method:I

    if-nez v1, :cond_0

    .line 123
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "?"

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-direct {p0}, Lcom/helpshift/network/request/Request;->addAuth()Ljava/util/Map;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/helpshift/network/request/Request;->encodeGetParameters(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 125
    :cond_0
    new-instance v1, Ljava/net/URL;

    invoke-direct {v1, v0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    return-object v1
.end method

.method public getRequestData()Ljava/util/Map;
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

    .line 66
    iget-object v0, p0, Lcom/helpshift/network/request/Request;->requestData:Ljava/util/Map;

    return-object v0
.end method

.method public getSequence()I
    .locals 2

    .line 89
    iget-object v0, p0, Lcom/helpshift/network/request/Request;->sequence:Ljava/lang/Integer;

    if-eqz v0, :cond_0

    .line 92
    iget-object v0, p0, Lcom/helpshift/network/request/Request;->sequence:Ljava/lang/Integer;

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    return v0

    .line 90
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "getSequence called before setSequence"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public hasHadResponseDelivered()Z
    .locals 1

    .line 229
    iget-boolean v0, p0, Lcom/helpshift/network/request/Request;->responseDelivered:Z

    return v0
.end method

.method public isDoOutput()Z
    .locals 2

    .line 253
    iget v0, p0, Lcom/helpshift/network/request/Request;->method:I

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method public markDelivered()V
    .locals 1

    const/4 v0, 0x1

    .line 225
    iput-boolean v0, p0, Lcom/helpshift/network/request/Request;->responseDelivered:Z

    return-void
.end method

.method protected parseNetworkError(Lcom/helpshift/network/errors/NetworkError;)Lcom/helpshift/network/errors/NetworkError;
    .locals 0

    return-object p1
.end method

.method protected parseNetworkResponse(Lcom/helpshift/network/response/NetworkResponse;)Lcom/helpshift/network/response/Response;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/helpshift/network/response/NetworkResponse;",
            ")",
            "Lcom/helpshift/network/response/Response<",
            "TT;>;"
        }
    .end annotation

    .line 233
    iget-object v0, p0, Lcom/helpshift/network/request/Request;->responseParser:Lcom/helpshift/network/response/ResponseParser;

    .line 234
    invoke-interface {v0, p1}, Lcom/helpshift/network/response/ResponseParser;->parseResponse(Lcom/helpshift/network/response/NetworkResponse;)Lcom/helpshift/network/response/Response;

    move-result-object p1

    return-object p1
.end method

.method public toString()Ljava/lang/String;
    .locals 2

    .line 258
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/network/request/Request;->url:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "HS_Request"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "  "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/network/request/Request;->sequence:Ljava/lang/Integer;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
