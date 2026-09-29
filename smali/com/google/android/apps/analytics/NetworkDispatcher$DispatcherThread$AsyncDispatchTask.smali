.class Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "AsyncDispatchTask"
.end annotation


# instance fields
.field private final hits:Ljava/util/LinkedList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/LinkedList<",
            "Lcom/google/android/apps/analytics/Hit;",
            ">;"
        }
    .end annotation
.end field

.field final synthetic this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;


# direct methods
.method public constructor <init>(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;[Lcom/google/android/apps/analytics/Hit;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    new-instance p1, Ljava/util/LinkedList;

    invoke-direct {p1}, Ljava/util/LinkedList;-><init>()V

    iput-object p1, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->hits:Ljava/util/LinkedList;

    iget-object p1, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->hits:Ljava/util/LinkedList;

    invoke-static {p1, p2}, Ljava/util/Collections;->addAll(Ljava/util/Collection;[Ljava/lang/Object;)Z

    return-void
.end method

.method private dispatchSomePendingHits(Z)V
    .locals 9
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lorg/apache/http/ParseException;,
            Lorg/apache/http/HttpException;
        }
    .end annotation

    invoke-static {}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->getInstance()Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->getDebug()Z

    move-result v0

    if-eqz v0, :cond_0

    if-eqz p1, :cond_0

    const-string v0, "GoogleAnalyticsTracker"

    const-string v1, "dispatching hits in dry run mode"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :cond_0
    const/4 v0, 0x0

    const/4 v1, 0x0

    :goto_0
    iget-object v2, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->hits:Ljava/util/LinkedList;

    invoke-virtual {v2}, Ljava/util/LinkedList;->size()I

    move-result v2

    if-ge v1, v2, :cond_a

    iget-object v2, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v2}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$1000(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)I

    move-result v2

    if-ge v1, v2, :cond_a

    iget-object v2, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->hits:Ljava/util/LinkedList;

    invoke-virtual {v2, v1}, Ljava/util/LinkedList;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/google/android/apps/analytics/Hit;

    iget-object v2, v2, Lcom/google/android/apps/analytics/Hit;->hitString:Ljava/lang/String;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v3

    invoke-static {v2, v3, v4}, Lcom/google/android/apps/analytics/Utils;->addQueueTimeParameter(Ljava/lang/String;J)Ljava/lang/String;

    move-result-object v2

    const/16 v3, 0x3f

    invoke-virtual {v2, v3}, Ljava/lang/String;->indexOf(I)I

    move-result v3

    if-gez v3, :cond_1

    const-string v3, ""

    move-object v4, v2

    goto :goto_2

    :cond_1
    if-lez v3, :cond_2

    invoke-virtual {v2, v0, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v4

    goto :goto_1

    :cond_2
    const-string v4, ""

    :goto_1
    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v5

    add-int/lit8 v5, v5, -0x2

    if-ge v3, v5, :cond_3

    add-int/lit8 v3, v3, 0x1

    invoke-virtual {v2, v3}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v3

    goto :goto_2

    :cond_3
    const-string v3, ""

    :goto_2
    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v5

    const/16 v6, 0x7f4

    if-ge v5, v6, :cond_4

    new-instance v4, Lorg/apache/http/message/BasicHttpEntityEnclosingRequest;

    const-string v5, "GET"

    invoke-direct {v4, v5, v2}, Lorg/apache/http/message/BasicHttpEntityEnclosingRequest;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    move-object v2, v4

    goto :goto_3

    :cond_4
    new-instance v2, Lorg/apache/http/message/BasicHttpEntityEnclosingRequest;

    const-string v5, "POST"

    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "/p"

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-direct {v2, v5, v4}, Lorg/apache/http/message/BasicHttpEntityEnclosingRequest;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    const-string v4, "Content-Length"

    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v5

    invoke-static {v5}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v5

    invoke-interface {v2, v4, v5}, Lorg/apache/http/HttpEntityEnclosingRequest;->addHeader(Ljava/lang/String;Ljava/lang/String;)V

    const-string v4, "Content-Type"

    const-string v5, "text/plain"

    invoke-interface {v2, v4, v5}, Lorg/apache/http/HttpEntityEnclosingRequest;->addHeader(Ljava/lang/String;Ljava/lang/String;)V

    new-instance v4, Lorg/apache/http/entity/StringEntity;

    invoke-direct {v4, v3}, Lorg/apache/http/entity/StringEntity;-><init>(Ljava/lang/String;)V

    invoke-interface {v2, v4}, Lorg/apache/http/HttpEntityEnclosingRequest;->setEntity(Lorg/apache/http/HttpEntity;)V

    :goto_3
    iget-object v4, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v4}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$700(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)Lcom/google/android/apps/analytics/NetworkDispatcher;

    move-result-object v4

    invoke-static {v4}, Lcom/google/android/apps/analytics/NetworkDispatcher;->access$200(Lcom/google/android/apps/analytics/NetworkDispatcher;)Lorg/apache/http/HttpHost;

    move-result-object v4

    invoke-virtual {v4}, Lorg/apache/http/HttpHost;->getHostName()Ljava/lang/String;

    move-result-object v4

    iget-object v5, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v5}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$700(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)Lcom/google/android/apps/analytics/NetworkDispatcher;

    move-result-object v5

    invoke-static {v5}, Lcom/google/android/apps/analytics/NetworkDispatcher;->access$200(Lcom/google/android/apps/analytics/NetworkDispatcher;)Lorg/apache/http/HttpHost;

    move-result-object v5

    invoke-virtual {v5}, Lorg/apache/http/HttpHost;->getPort()I

    move-result v5

    const/16 v6, 0x50

    if-eq v5, v6, :cond_5

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v5, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, ":"

    invoke-virtual {v5, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v4, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v4}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$700(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)Lcom/google/android/apps/analytics/NetworkDispatcher;

    move-result-object v4

    invoke-static {v4}, Lcom/google/android/apps/analytics/NetworkDispatcher;->access$200(Lcom/google/android/apps/analytics/NetworkDispatcher;)Lorg/apache/http/HttpHost;

    move-result-object v4

    invoke-virtual {v4}, Lorg/apache/http/HttpHost;->getPort()I

    move-result v4

    invoke-virtual {v5, v4}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    :cond_5
    const-string v5, "Host"

    invoke-interface {v2, v5, v4}, Lorg/apache/http/HttpEntityEnclosingRequest;->addHeader(Ljava/lang/String;Ljava/lang/String;)V

    const-string v4, "User-Agent"

    iget-object v5, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v5}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$1100(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)Ljava/lang/String;

    move-result-object v5

    invoke-interface {v2, v4, v5}, Lorg/apache/http/HttpEntityEnclosingRequest;->addHeader(Ljava/lang/String;Ljava/lang/String;)V

    invoke-static {}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->getInstance()Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    move-result-object v4

    invoke-virtual {v4}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->getDebug()Z

    move-result v4

    if-eqz v4, :cond_7

    new-instance v4, Ljava/lang/StringBuffer;

    invoke-direct {v4}, Ljava/lang/StringBuffer;-><init>()V

    invoke-interface {v2}, Lorg/apache/http/HttpEntityEnclosingRequest;->getAllHeaders()[Lorg/apache/http/Header;

    move-result-object v5

    array-length v6, v5

    const/4 v7, 0x0

    :goto_4
    if-ge v7, v6, :cond_6

    aget-object v8, v5, v7

    invoke-virtual {v8}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v4, v8}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v8, "\n"

    invoke-virtual {v4, v8}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    add-int/lit8 v7, v7, 0x1

    goto :goto_4

    :cond_6
    invoke-interface {v2}, Lorg/apache/http/HttpEntityEnclosingRequest;->getRequestLine()Lorg/apache/http/RequestLine;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v5, "\n"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v5, "GoogleAnalyticsTracker"

    invoke-virtual {v4}, Ljava/lang/StringBuffer;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v5, v4}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    :cond_7
    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v3

    const/16 v4, 0x2000

    if-le v3, v4, :cond_8

    const-string v2, "GoogleAnalyticsTracker"

    const-string v3, "Hit too long (> 8192 bytes)--not sent"

    invoke-static {v2, v3}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :goto_5
    iget-object v2, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v2}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$1200(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$RequesterCallbacks;

    move-result-object v2

    invoke-virtual {v2}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$RequesterCallbacks;->requestSent()V

    goto :goto_6

    :cond_8
    if-eqz p1, :cond_9

    goto :goto_5

    :cond_9
    iget-object v3, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v3}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$800(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)Lcom/google/android/apps/analytics/PipelinedRequester;

    move-result-object v3

    invoke-virtual {v3, v2}, Lcom/google/android/apps/analytics/PipelinedRequester;->addRequest(Lorg/apache/http/HttpEntityEnclosingRequest;)V

    :goto_6
    add-int/lit8 v1, v1, 0x1

    goto/16 :goto_0

    :cond_a
    if-nez p1, :cond_b

    iget-object p1, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {p1}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$800(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)Lcom/google/android/apps/analytics/PipelinedRequester;

    move-result-object p1

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/PipelinedRequester;->sendRequests()V

    :cond_b
    return-void
.end method


# virtual methods
.method public removeNextHit()Lcom/google/android/apps/analytics/Hit;
    .locals 1

    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->hits:Ljava/util/LinkedList;

    invoke-virtual {v0}, Ljava/util/LinkedList;->poll()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/google/android/apps/analytics/Hit;

    return-object v0
.end method

.method public run()V
    .locals 10

    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v0, p0}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$402(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;)Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;

    const/4 v0, 0x0

    :goto_0
    const/4 v1, 0x5

    if-ge v0, v1, :cond_3

    iget-object v1, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->hits:Ljava/util/LinkedList;

    invoke-virtual {v1}, Ljava/util/LinkedList;->size()I

    move-result v1

    if-lez v1, :cond_3

    const-wide/16 v1, 0x0

    :try_start_0
    iget-object v3, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v3}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$500(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)I

    move-result v3

    const/16 v4, 0x1f4

    const-wide/16 v5, 0x2

    if-eq v3, v4, :cond_1

    iget-object v3, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v3}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$500(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)I

    move-result v3

    const/16 v4, 0x1f7

    if-ne v3, v4, :cond_0

    goto :goto_1

    :cond_0
    iget-object v3, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v3, v5, v6}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$602(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;J)J

    goto :goto_2

    :cond_1
    :goto_1
    invoke-static {}, Ljava/lang/Math;->random()D

    move-result-wide v1

    iget-object v3, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v3}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$600(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)J

    move-result-wide v3
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lorg/apache/http/HttpException; {:try_start_0 .. :try_end_0} :catch_0

    long-to-double v3, v3

    invoke-static {v3, v4}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v1, v1, v3

    double-to-long v1, v1

    :try_start_1
    iget-object v3, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v3}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$600(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)J

    move-result-wide v3

    const-wide/16 v7, 0x100

    cmp-long v9, v3, v7

    if-gez v9, :cond_2

    iget-object v3, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v3, v5, v6}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$630(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;J)J

    :cond_2
    :goto_2
    const-wide/16 v3, 0x3e8

    mul-long v1, v1, v3

    invoke-static {v1, v2}, Ljava/lang/Thread;->sleep(J)V

    iget-object v1, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v1}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$700(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)Lcom/google/android/apps/analytics/NetworkDispatcher;

    move-result-object v1

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/NetworkDispatcher;->isDryRun()Z

    move-result v1

    invoke-direct {p0, v1}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->dispatchSomePendingHits(Z)V
    :try_end_1
    .catch Ljava/lang/InterruptedException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Lorg/apache/http/HttpException; {:try_start_1 .. :try_end_1} :catch_0

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "GoogleAnalyticsTracker"

    const-string v2, "Problem with http streams."

    goto :goto_3

    :catch_1
    move-exception v0

    const-string v1, "GoogleAnalyticsTracker"

    const-string v2, "Problem with socket or streams."

    goto :goto_3

    :catch_2
    move-exception v0

    const-string v1, "GoogleAnalyticsTracker"

    const-string v2, "Couldn\'t sleep."

    :goto_3
    invoke-static {v1, v2, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_3
    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v0}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$800(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)Lcom/google/android/apps/analytics/PipelinedRequester;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/PipelinedRequester;->finishedCurrentRequests()V

    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-static {v0}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$900(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;)Lcom/google/android/apps/analytics/Dispatcher$Callbacks;

    move-result-object v0

    invoke-interface {v0}, Lcom/google/android/apps/analytics/Dispatcher$Callbacks;->dispatchFinished()V

    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;->this$0:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    const/4 v1, 0x0

    invoke-static {v0, v1}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->access$402(Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;)Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread$AsyncDispatchTask;

    return-void
.end method
