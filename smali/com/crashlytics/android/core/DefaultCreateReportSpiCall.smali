.class Lcom/crashlytics/android/core/DefaultCreateReportSpiCall;
.super Lio/fabric/sdk/android/services/common/AbstractSpiCall;
.source "DefaultCreateReportSpiCall.java"

# interfaces
.implements Lcom/crashlytics/android/core/CreateReportSpiCall;


# static fields
.field static final FILE_CONTENT_TYPE:Ljava/lang/String; = "application/octet-stream"

.field static final FILE_PARAM:Ljava/lang/String; = "report[file]"

.field static final IDENTIFIER_PARAM:Ljava/lang/String; = "report[identifier]"


# direct methods
.method public constructor <init>(Lio/fabric/sdk/android/Kit;Ljava/lang/String;Ljava/lang/String;Lio/fabric/sdk/android/services/network/HttpRequestFactory;)V
    .locals 6

    .line 37
    sget-object v5, Lio/fabric/sdk/android/services/network/HttpMethod;->POST:Lio/fabric/sdk/android/services/network/HttpMethod;

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-object v4, p4

    invoke-direct/range {v0 .. v5}, Lio/fabric/sdk/android/services/common/AbstractSpiCall;-><init>(Lio/fabric/sdk/android/Kit;Ljava/lang/String;Ljava/lang/String;Lio/fabric/sdk/android/services/network/HttpRequestFactory;Lio/fabric/sdk/android/services/network/HttpMethod;)V

    return-void
.end method

.method constructor <init>(Lio/fabric/sdk/android/Kit;Ljava/lang/String;Ljava/lang/String;Lio/fabric/sdk/android/services/network/HttpRequestFactory;Lio/fabric/sdk/android/services/network/HttpMethod;)V
    .locals 0

    .line 52
    invoke-direct/range {p0 .. p5}, Lio/fabric/sdk/android/services/common/AbstractSpiCall;-><init>(Lio/fabric/sdk/android/Kit;Ljava/lang/String;Ljava/lang/String;Lio/fabric/sdk/android/services/network/HttpRequestFactory;Lio/fabric/sdk/android/services/network/HttpMethod;)V

    return-void
.end method

.method private applyHeadersTo(Lio/fabric/sdk/android/services/network/HttpRequest;Lcom/crashlytics/android/core/CreateReportRequest;)Lio/fabric/sdk/android/services/network/HttpRequest;
    .locals 2

    const-string v0, "X-CRASHLYTICS-API-KEY"

    .line 74
    iget-object v1, p2, Lcom/crashlytics/android/core/CreateReportRequest;->apiKey:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lio/fabric/sdk/android/services/network/HttpRequest;->header(Ljava/lang/String;Ljava/lang/String;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object p1

    const-string v0, "X-CRASHLYTICS-API-CLIENT-TYPE"

    const-string v1, "android"

    invoke-virtual {p1, v0, v1}, Lio/fabric/sdk/android/services/network/HttpRequest;->header(Ljava/lang/String;Ljava/lang/String;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object p1

    const-string v0, "X-CRASHLYTICS-API-CLIENT-VERSION"

    invoke-static {}, Lcom/crashlytics/android/core/CrashlyticsCore;->getInstance()Lcom/crashlytics/android/core/CrashlyticsCore;

    move-result-object v1

    invoke-virtual {v1}, Lcom/crashlytics/android/core/CrashlyticsCore;->getVersion()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v0, v1}, Lio/fabric/sdk/android/services/network/HttpRequest;->header(Ljava/lang/String;Ljava/lang/String;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object p1

    .line 79
    iget-object p2, p2, Lcom/crashlytics/android/core/CreateReportRequest;->report:Lcom/crashlytics/android/core/Report;

    invoke-interface {p2}, Lcom/crashlytics/android/core/Report;->getCustomHeaders()Ljava/util/Map;

    move-result-object p2

    .line 81
    invoke-interface {p2}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p2

    invoke-interface {p2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map$Entry;

    .line 82
    invoke-virtual {p1, v0}, Lio/fabric/sdk/android/services/network/HttpRequest;->header(Ljava/util/Map$Entry;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object p1

    goto :goto_0

    :cond_0
    return-object p1
.end method

.method private applyMultipartDataTo(Lio/fabric/sdk/android/services/network/HttpRequest;Lcom/crashlytics/android/core/CreateReportRequest;)Lio/fabric/sdk/android/services/network/HttpRequest;
    .locals 4

    .line 88
    iget-object p2, p2, Lcom/crashlytics/android/core/CreateReportRequest;->report:Lcom/crashlytics/android/core/Report;

    const-string v0, "report[file]"

    .line 90
    invoke-interface {p2}, Lcom/crashlytics/android/core/Report;->getFileName()Ljava/lang/String;

    move-result-object v1

    const-string v2, "application/octet-stream"

    invoke-interface {p2}, Lcom/crashlytics/android/core/Report;->getFile()Ljava/io/File;

    move-result-object v3

    invoke-virtual {p1, v0, v1, v2, v3}, Lio/fabric/sdk/android/services/network/HttpRequest;->part(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/io/File;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object p1

    const-string v0, "report[identifier]"

    invoke-interface {p2}, Lcom/crashlytics/android/core/Report;->getIdentifier()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, v0, p2}, Lio/fabric/sdk/android/services/network/HttpRequest;->part(Ljava/lang/String;Ljava/lang/String;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object p1

    return-object p1
.end method


# virtual methods
.method public invoke(Lcom/crashlytics/android/core/CreateReportRequest;)Z
    .locals 5

    .line 58
    invoke-virtual {p0}, Lcom/crashlytics/android/core/DefaultCreateReportSpiCall;->getHttpRequest()Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object v0

    .line 59
    invoke-direct {p0, v0, p1}, Lcom/crashlytics/android/core/DefaultCreateReportSpiCall;->applyHeadersTo(Lio/fabric/sdk/android/services/network/HttpRequest;Lcom/crashlytics/android/core/CreateReportRequest;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object v0

    .line 60
    invoke-direct {p0, v0, p1}, Lcom/crashlytics/android/core/DefaultCreateReportSpiCall;->applyMultipartDataTo(Lio/fabric/sdk/android/services/network/HttpRequest;Lcom/crashlytics/android/core/CreateReportRequest;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object p1

    .line 62
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v1, "Fabric"

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Sending report to: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/crashlytics/android/core/DefaultCreateReportSpiCall;->getUrl()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 64
    invoke-virtual {p1}, Lio/fabric/sdk/android/services/network/HttpRequest;->code()I

    move-result v0

    .line 66
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v1

    const-string v2, "Fabric"

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Create report request ID: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, "X-REQUEST-ID"

    invoke-virtual {p1, v4}, Lio/fabric/sdk/android/services/network/HttpRequest;->header(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v1, v2, p1}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 68
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object p1

    const-string v1, "Fabric"

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Result was: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-interface {p1, v1, v2}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 70
    invoke-static {v0}, Lio/fabric/sdk/android/services/common/ResponseParser;->parse(I)I

    move-result p1

    if-nez p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method
