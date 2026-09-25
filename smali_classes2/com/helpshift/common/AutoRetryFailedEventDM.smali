.class public Lcom/helpshift/common/AutoRetryFailedEventDM;
.super Ljava/lang/Object;
.source "AutoRetryFailedEventDM.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;
    }
.end annotation


# instance fields
.field private final domain:Lcom/helpshift/common/domain/Domain;

.field private isBatcherScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

.field private isSendAllEventsScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

.field private listeners:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;",
            "Lcom/helpshift/common/AutoRetriableDM;",
            ">;"
        }
    .end annotation
.end field

.field private pendingRetryEventTypes:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;",
            ">;"
        }
    .end annotation
.end field

.field private final platform:Lcom/helpshift/common/platform/Platform;

.field private final retryBackoff:Lcom/helpshift/common/poller/HttpBackoff;

.field private shouldScheduleAuthenticationEvent:Z


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/poller/HttpBackoff;)V
    .locals 2

    .line 41
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x1

    .line 34
    iput-boolean v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->shouldScheduleAuthenticationEvent:Z

    .line 36
    new-instance v0, Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;-><init>(Z)V

    iput-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->isBatcherScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

    .line 37
    new-instance v0, Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-direct {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;-><init>(Z)V

    iput-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->isSendAllEventsScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

    .line 38
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->listeners:Ljava/util/Map;

    .line 39
    new-instance v0, Ljava/util/LinkedHashSet;

    invoke-direct {v0}, Ljava/util/LinkedHashSet;-><init>()V

    invoke-static {v0}, Ljava/util/Collections;->synchronizedSet(Ljava/util/Set;)Ljava/util/Set;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    .line 42
    iput-object p1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 43
    iput-object p2, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 44
    iput-object p3, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->retryBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/common/AutoRetryFailedEventDM;)Ljava/util/Set;
    .locals 0

    .line 29
    iget-object p0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    return-object p0
.end method

.method static synthetic access$100(Lcom/helpshift/common/AutoRetryFailedEventDM;)Ljava/util/concurrent/atomic/AtomicBoolean;
    .locals 0

    .line 29
    iget-object p0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->isSendAllEventsScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

    return-object p0
.end method

.method static synthetic access$200(Lcom/helpshift/common/AutoRetryFailedEventDM;)Ljava/util/concurrent/atomic/AtomicBoolean;
    .locals 0

    .line 29
    iget-object p0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->isBatcherScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

    return-object p0
.end method

.method private canRetryEventType(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)Z
    .locals 0

    .line 206
    invoke-direct {p0, p1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->isAuthenticatedType(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)Z

    move-result p1

    if-eqz p1, :cond_1

    .line 207
    iget-boolean p1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->shouldScheduleAuthenticationEvent:Z

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

.method private isAuthenticatedType(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)Z
    .locals 1

    .line 192
    sget-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$5;->$SwitchMap$com$helpshift$common$AutoRetryFailedEventDM$EventType:[I

    invoke-virtual {p1}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ordinal()I

    move-result p1

    aget p1, v0, p1

    packed-switch p1, :pswitch_data_0

    const/4 p1, 0x0

    goto :goto_0

    :pswitch_0
    const/4 p1, 0x1

    :goto_0
    return p1

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_0
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method private scheduleSync(ILjava/util/Set;)V
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I",
            "Ljava/util/Set<",
            "Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;",
            ">;)V"
        }
    .end annotation

    .line 110
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->isBatcherScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x1

    const/4 v2, 0x0

    invoke-virtual {v0, v2, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 111
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->retryBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/poller/HttpBackoff;->nextIntervalMillis(I)J

    move-result-wide v3

    const-wide/16 v5, -0x64

    cmp-long p1, v3, v5

    if-eqz p1, :cond_0

    .line 113
    iget-object p1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v0, Lcom/helpshift/common/AutoRetryFailedEventDM$3;

    invoke-direct {v0, p0, p2}, Lcom/helpshift/common/AutoRetryFailedEventDM$3;-><init>(Lcom/helpshift/common/AutoRetryFailedEventDM;Ljava/util/Set;)V

    invoke-virtual {p1, v0, v3, v4}, Lcom/helpshift/common/domain/Domain;->runDelayedInParallel(Lcom/helpshift/common/domain/F;J)V

    goto :goto_0

    .line 122
    :cond_0
    iget-object p1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->isBatcherScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {p1, v1, v2}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    :cond_1
    :goto_0
    return-void
.end method


# virtual methods
.method public onUserAuthenticationUpdated()V
    .locals 2

    .line 176
    iget-boolean v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->shouldScheduleAuthenticationEvent:Z

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x1

    .line 180
    iput-boolean v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->shouldScheduleAuthenticationEvent:Z

    .line 182
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/common/AutoRetryFailedEventDM$4;

    invoke-direct {v1, p0}, Lcom/helpshift/common/AutoRetryFailedEventDM$4;-><init>(Lcom/helpshift/common/AutoRetryFailedEventDM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public register(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;Lcom/helpshift/common/AutoRetriableDM;)V
    .locals 1

    .line 48
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->listeners:Ljava/util/Map;

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method public resetBackoff()V
    .locals 1

    .line 106
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->retryBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    invoke-virtual {v0}, Lcom/helpshift/common/poller/HttpBackoff;->reset()V

    return-void
.end method

.method retryFailedApis(Ljava/util/Set;)V
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Set<",
            "Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;",
            ">;)V"
        }
    .end annotation

    .line 129
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->isOnline()Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 130
    invoke-direct {p0, v1, p1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->scheduleSync(ILjava/util/Set;)V

    return-void

    .line 134
    :cond_0
    new-instance v0, Ljava/util/LinkedList;

    invoke-direct {v0, p1}, Ljava/util/LinkedList;-><init>(Ljava/util/Collection;)V

    .line 136
    :try_start_0
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_5

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    .line 138
    invoke-direct {p0, v2}, Lcom/helpshift/common/AutoRetryFailedEventDM;->canRetryEventType(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)Z

    move-result v3

    if-nez v3, :cond_1

    goto :goto_0

    .line 141
    :cond_1
    iget-object v3, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->listeners:Ljava/util/Map;

    invoke-interface {v3, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/common/AutoRetriableDM;

    if-nez v3, :cond_2

    .line 144
    iget-object v3, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    invoke-interface {v3, v2}, Ljava/util/Set;->remove(Ljava/lang/Object;)Z

    .line 145
    invoke-interface {p1, v2}, Ljava/util/Set;->remove(Ljava/lang/Object;)Z
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_1

    goto :goto_0

    .line 150
    :cond_2
    :try_start_1
    invoke-interface {v3, v2}, Lcom/helpshift/common/AutoRetriableDM;->sendFailedApiCalls(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)V

    .line 151
    iget-object v3, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    invoke-interface {v3, v2}, Ljava/util/Set;->remove(Ljava/lang/Object;)Z

    .line 153
    invoke-interface {p1, v2}, Ljava/util/Set;->remove(Ljava/lang/Object;)Z
    :try_end_1
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 157
    :try_start_2
    iget-object v3, v2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v4, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-eq v3, v4, :cond_4

    iget-object v3, v2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v4, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-ne v3, v4, :cond_3

    goto :goto_1

    .line 162
    :cond_3
    throw v2

    .line 159
    :cond_4
    :goto_1
    iput-boolean v1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->shouldScheduleAuthenticationEvent:Z

    goto :goto_0

    .line 166
    :cond_5
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->retryBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    invoke-virtual {v0}, Lcom/helpshift/common/poller/HttpBackoff;->reset()V
    :try_end_2
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_2 .. :try_end_2} :catch_1

    goto :goto_2

    :catch_1
    move-exception v0

    .line 169
    invoke-virtual {v0}, Lcom/helpshift/common/exception/RootAPIException;->getServerStatusCode()I

    move-result v0

    .line 170
    invoke-direct {p0, v0, p1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->scheduleSync(ILjava/util/Set;)V

    :goto_2
    return-void
.end method

.method public scheduleRetryTaskForEventType(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;I)V
    .locals 1

    .line 52
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 53
    invoke-direct {p0, p1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->isAuthenticatedType(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)Z

    move-result p1

    if-eqz p1, :cond_2

    .line 55
    sget-object p1, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->INVALID_AUTH_TOKEN:Ljava/lang/Integer;

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    if-eq p2, p1, :cond_1

    sget-object p1, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->AUTH_TOKEN_NOT_PROVIDED:Ljava/lang/Integer;

    .line 56
    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    if-ne p2, p1, :cond_0

    goto :goto_0

    .line 60
    :cond_0
    iget-object p1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    invoke-direct {p0, p2, p1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->scheduleSync(ILjava/util/Set;)V

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    .line 57
    iput-boolean p1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->shouldScheduleAuthenticationEvent:Z

    goto :goto_1

    .line 64
    :cond_2
    iget-object p1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    invoke-direct {p0, p2, p1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->scheduleSync(ILjava/util/Set;)V

    :goto_1
    return-void
.end method

.method public sendAllEvents()V
    .locals 3

    .line 69
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->isSendAllEventsScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 73
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->MIGRATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-interface {v0, v1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 74
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->SYNC_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-interface {v0, v1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 75
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->PUSH_TOKEN:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-interface {v0, v1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 76
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->CLEAR_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-interface {v0, v1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 77
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->CONVERSATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-interface {v0, v1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 78
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->FAQ:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-interface {v0, v1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 79
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->pendingRetryEventTypes:Ljava/util/Set;

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ANALYTICS:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-interface {v0, v1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 80
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/common/AutoRetryFailedEventDM$1;

    invoke-direct {v1, p0}, Lcom/helpshift/common/AutoRetryFailedEventDM$1;-><init>(Lcom/helpshift/common/AutoRetryFailedEventDM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public sendEventForcefully(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)V
    .locals 2

    .line 95
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/common/AutoRetryFailedEventDM$2;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/common/AutoRetryFailedEventDM$2;-><init>(Lcom/helpshift/common/AutoRetryFailedEventDM;Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method
