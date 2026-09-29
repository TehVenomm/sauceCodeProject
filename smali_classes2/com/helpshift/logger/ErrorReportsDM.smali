.class public Lcom/helpshift/logger/ErrorReportsDM;
.super Ljava/lang/Object;
.source "ErrorReportsDM.java"


# instance fields
.field domain:Lcom/helpshift/common/domain/Domain;

.field platform:Lcom/helpshift/common/platform/Platform;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;)V
    .locals 0

    .line 40
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 41
    iput-object p1, p0, Lcom/helpshift/logger/ErrorReportsDM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 42
    iput-object p2, p0, Lcom/helpshift/logger/ErrorReportsDM;->domain:Lcom/helpshift/common/domain/Domain;

    return-void
.end method


# virtual methods
.method getAuthDataForErrorReports()Ljava/util/Map;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 92
    :try_start_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 93
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    const-string v2, "platform-id=sdk"

    .line 94
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 95
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v2

    invoke-virtual {v2}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v2

    .line 96
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "token="

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v3, "token"

    .line 97
    invoke-interface {v0, v3, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "sm"

    .line 99
    iget-object v3, p0, Lcom/helpshift/logger/ErrorReportsDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v3}, Lcom/helpshift/common/platform/Platform;->getJsonifier()Lcom/helpshift/common/platform/Jsonifier;

    move-result-object v3

    invoke-static {}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getSdkMeta()Ljava/util/Map;

    move-result-object v4

    invoke-interface {v3, v4}, Lcom/helpshift/common/platform/Jsonifier;->jsonify(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v0, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 100
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "sm="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lcom/helpshift/logger/ErrorReportsDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v3}, Lcom/helpshift/common/platform/Platform;->getJsonifier()Lcom/helpshift/common/platform/Jsonifier;

    move-result-object v3

    invoke-static {}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getSdkMeta()Ljava/util/Map;

    move-result-object v4

    invoke-interface {v3, v4}, Lcom/helpshift/common/platform/Jsonifier;->jsonify(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v2, "signature"

    .line 102
    iget-object v3, p0, Lcom/helpshift/logger/ErrorReportsDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v3}, Lcom/helpshift/common/domain/Domain;->getCryptoDM()Lcom/helpshift/crypto/CryptoDM;

    move-result-object v3

    const-string v4, "&"

    invoke-static {v4, v1}, Lcom/helpshift/common/StringUtils;->join(Ljava/lang/CharSequence;Ljava/lang/Iterable;)Ljava/lang/String;

    move-result-object v1

    const-string v4, "sdk"

    invoke-virtual {v3, v1, v4}, Lcom/helpshift/crypto/CryptoDM;->getSignature(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Ljava/security/GeneralSecurityException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    const/4 v1, 0x0

    const-string v2, "SecurityException while creating signature"

    .line 105
    invoke-static {v0, v1, v2}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0
.end method

.method public sendErrorReport(Lcom/helpshift/common/FetchDataFromThread;Ljava/util/List;Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 14
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/FetchDataFromThread<",
            "Lcom/helpshift/common/platform/network/Response;",
            "Ljava/lang/Float;",
            ">;",
            "Ljava/util/List<",
            "Lcom/helpshift/logger/model/LogModel;",
            ">;",
            "Lcom/helpshift/account/domainmodel/UserDM;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    move-object v11, p0

    .line 49
    iget-object v12, v11, Lcom/helpshift/logger/ErrorReportsDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v13, Lcom/helpshift/logger/ErrorReportsDM$1;

    move-object v0, v13

    move-object v1, p0

    move-object/from16 v2, p2

    move-object/from16 v3, p4

    move-object/from16 v4, p7

    move-object/from16 v5, p3

    move-object/from16 v6, p8

    move-object/from16 v7, p9

    move-object/from16 v8, p5

    move-object/from16 v9, p6

    move-object v10, p1

    invoke-direct/range {v0 .. v10}, Lcom/helpshift/logger/ErrorReportsDM$1;-><init>(Lcom/helpshift/logger/ErrorReportsDM;Ljava/util/List;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/common/FetchDataFromThread;)V

    invoke-virtual {v12, v13}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method
