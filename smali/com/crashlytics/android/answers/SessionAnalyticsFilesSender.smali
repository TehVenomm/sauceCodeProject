.class Lcom/crashlytics/android/answers/SessionAnalyticsFilesSender;
.super Lio/fabric/sdk/android/services/common/AbstractSpiCall;
.source "SessionAnalyticsFilesSender.java"

# interfaces
.implements Lio/fabric/sdk/android/services/events/FilesSender;


# static fields
.field static final FILE_CONTENT_TYPE:Ljava/lang/String; = "application/vnd.crashlytics.android.events"

.field static final FILE_PARAM_NAME:Ljava/lang/String; = "session_analytics_file_"


# instance fields
.field private final apiKey:Ljava/lang/String;


# direct methods
.method public constructor <init>(Lio/fabric/sdk/android/Kit;Ljava/lang/String;Ljava/lang/String;Lio/fabric/sdk/android/services/network/HttpRequestFactory;Ljava/lang/String;)V
    .locals 6

    .line 28
    sget-object v5, Lio/fabric/sdk/android/services/network/HttpMethod;->POST:Lio/fabric/sdk/android/services/network/HttpMethod;

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-object v4, p4

    invoke-direct/range {v0 .. v5}, Lio/fabric/sdk/android/services/common/AbstractSpiCall;-><init>(Lio/fabric/sdk/android/Kit;Ljava/lang/String;Ljava/lang/String;Lio/fabric/sdk/android/services/network/HttpRequestFactory;Lio/fabric/sdk/android/services/network/HttpMethod;)V

    .line 29
    iput-object p5, p0, Lcom/crashlytics/android/answers/SessionAnalyticsFilesSender;->apiKey:Ljava/lang/String;

    return-void
.end method

.method private applyHeadersTo(Lio/fabric/sdk/android/services/network/HttpRequest;Ljava/lang/String;)Lio/fabric/sdk/android/services/network/HttpRequest;
    .locals 2

    const-string v0, "X-CRASHLYTICS-API-CLIENT-TYPE"

    const-string v1, "android"

    .line 48
    invoke-virtual {p1, v0, v1}, Lio/fabric/sdk/android/services/network/HttpRequest;->header(Ljava/lang/String;Ljava/lang/String;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object p1

    const-string v0, "X-CRASHLYTICS-API-CLIENT-VERSION"

    invoke-static {}, Lcom/crashlytics/android/answers/Answers;->getInstance()Lcom/crashlytics/android/answers/Answers;

    move-result-object v1

    invoke-virtual {v1}, Lcom/crashlytics/android/answers/Answers;->getVersion()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v0, v1}, Lio/fabric/sdk/android/services/network/HttpRequest;->header(Ljava/lang/String;Ljava/lang/String;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object p1

    const-string v0, "X-CRASHLYTICS-API-KEY"

    invoke-virtual {p1, v0, p2}, Lio/fabric/sdk/android/services/network/HttpRequest;->header(Ljava/lang/String;Ljava/lang/String;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object p1

    return-object p1
.end method

.method private applyMultipartDataTo(Lio/fabric/sdk/android/services/network/HttpRequest;Ljava/util/List;)Lio/fabric/sdk/android/services/network/HttpRequest;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lio/fabric/sdk/android/services/network/HttpRequest;",
            "Ljava/util/List<",
            "Ljava/io/File;",
            ">;)",
            "Lio/fabric/sdk/android/services/network/HttpRequest;"
        }
    .end annotation

    .line 56
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    const/4 v0, 0x0

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/io/File;

    .line 57
    invoke-static {}, Lcom/crashlytics/android/answers/Answers;->getInstance()Lcom/crashlytics/android/answers/Answers;

    move-result-object v2

    invoke-virtual {v2}, Lcom/crashlytics/android/answers/Answers;->getContext()Landroid/content/Context;

    move-result-object v2

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Adding analytics session file "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, " to multipart POST"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Lio/fabric/sdk/android/services/common/CommonUtils;->logControlled(Landroid/content/Context;Ljava/lang/String;)V

    .line 59
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "session_analytics_file_"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v3

    const-string v4, "application/vnd.crashlytics.android.events"

    invoke-virtual {p1, v2, v3, v4, v1}, Lio/fabric/sdk/android/services/network/HttpRequest;->part(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/io/File;)Lio/fabric/sdk/android/services/network/HttpRequest;

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_0
    return-object p1
.end method


# virtual methods
.method public send(Ljava/util/List;)Z
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/io/File;",
            ">;)Z"
        }
    .end annotation

    .line 34
    invoke-virtual {p0}, Lcom/crashlytics/android/answers/SessionAnalyticsFilesSender;->getHttpRequest()Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object v0

    .line 35
    iget-object v1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsFilesSender;->apiKey:Ljava/lang/String;

    invoke-direct {p0, v0, v1}, Lcom/crashlytics/android/answers/SessionAnalyticsFilesSender;->applyHeadersTo(Lio/fabric/sdk/android/services/network/HttpRequest;Ljava/lang/String;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object v0

    .line 36
    invoke-direct {p0, v0, p1}, Lcom/crashlytics/android/answers/SessionAnalyticsFilesSender;->applyMultipartDataTo(Lio/fabric/sdk/android/services/network/HttpRequest;Ljava/util/List;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object v0

    .line 38
    invoke-static {}, Lcom/crashlytics/android/answers/Answers;->getInstance()Lcom/crashlytics/android/answers/Answers;

    move-result-object v1

    invoke-virtual {v1}, Lcom/crashlytics/android/answers/Answers;->getContext()Landroid/content/Context;

    move-result-object v1

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Sending "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result p1

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string p1, " analytics files to "

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/crashlytics/android/answers/SessionAnalyticsFilesSender;->getUrl()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v1, p1}, Lio/fabric/sdk/android/services/common/CommonUtils;->logControlled(Landroid/content/Context;Ljava/lang/String;)V

    .line 40
    invoke-virtual {v0}, Lio/fabric/sdk/android/services/network/HttpRequest;->code()I

    move-result p1

    .line 41
    invoke-static {}, Lcom/crashlytics/android/answers/Answers;->getInstance()Lcom/crashlytics/android/answers/Answers;

    move-result-object v0

    invoke-virtual {v0}, Lcom/crashlytics/android/answers/Answers;->getContext()Landroid/content/Context;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Response code for analytics file send is "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lio/fabric/sdk/android/services/common/CommonUtils;->logControlled(Landroid/content/Context;Ljava/lang/String;)V

    .line 44
    invoke-static {p1}, Lio/fabric/sdk/android/services/common/ResponseParser;->parse(I)I

    move-result p1

    if-nez p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method
