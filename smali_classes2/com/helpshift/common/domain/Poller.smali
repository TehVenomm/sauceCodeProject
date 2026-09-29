.class public Lcom/helpshift/common/domain/Poller;
.super Ljava/lang/Object;
.source "Poller.java"


# instance fields
.field private activePollFunction:Lcom/helpshift/common/domain/PollFunction;

.field private aggressiveBackoff:Lcom/helpshift/common/poller/HttpBackoff;

.field private conservativeBackoff:Lcom/helpshift/common/poller/HttpBackoff;

.field private final domain:Lcom/helpshift/common/domain/Domain;

.field private passiveBackoff:Lcom/helpshift/common/poller/HttpBackoff;

.field private final poll:Lcom/helpshift/common/domain/F;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/domain/F;)V
    .locals 7

    .line 40
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 17
    new-instance v0, Lcom/helpshift/common/poller/HttpBackoff$Builder;

    invoke-direct {v0}, Lcom/helpshift/common/poller/HttpBackoff$Builder;-><init>()V

    sget-object v1, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v2, 0x5

    .line 18
    invoke-static {v2, v3, v1}, Lcom/helpshift/common/poller/Delay;->of(JLjava/util/concurrent/TimeUnit;)Lcom/helpshift/common/poller/Delay;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setBaseInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    sget-object v1, Ljava/util/concurrent/TimeUnit;->MINUTES:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v4, 0x1

    .line 19
    invoke-static {v4, v5, v1}, Lcom/helpshift/common/poller/Delay;->of(JLjava/util/concurrent/TimeUnit;)Lcom/helpshift/common/poller/Delay;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setMaxInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    const v1, 0x3dcccccd    # 0.1f

    .line 20
    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setRandomness(F)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    const/high16 v4, 0x40000000    # 2.0f

    .line 21
    invoke-virtual {v0, v4}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setMultiplier(F)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    .line 22
    invoke-direct {p0}, Lcom/helpshift/common/domain/Poller;->getPollerRetryPollicy()Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    move-result-object v4

    invoke-virtual {v0, v4}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setRetryPolicy(Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    .line 23
    invoke-virtual {v0}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->build()Lcom/helpshift/common/poller/HttpBackoff;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/common/domain/Poller;->conservativeBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    .line 24
    new-instance v0, Lcom/helpshift/common/poller/HttpBackoff$Builder;

    invoke-direct {v0}, Lcom/helpshift/common/poller/HttpBackoff$Builder;-><init>()V

    sget-object v4, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v5, 0x3

    .line 25
    invoke-static {v5, v6, v4}, Lcom/helpshift/common/poller/Delay;->of(JLjava/util/concurrent/TimeUnit;)Lcom/helpshift/common/poller/Delay;

    move-result-object v4

    invoke-virtual {v0, v4}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setBaseInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    sget-object v4, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    .line 26
    invoke-static {v5, v6, v4}, Lcom/helpshift/common/poller/Delay;->of(JLjava/util/concurrent/TimeUnit;)Lcom/helpshift/common/poller/Delay;

    move-result-object v4

    invoke-virtual {v0, v4}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setMaxInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    const/4 v4, 0x0

    .line 27
    invoke-virtual {v0, v4}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setRandomness(F)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    const/high16 v4, 0x3f800000    # 1.0f

    .line 28
    invoke-virtual {v0, v4}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setMultiplier(F)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    .line 29
    invoke-direct {p0}, Lcom/helpshift/common/domain/Poller;->getPollerRetryPollicy()Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    move-result-object v4

    invoke-virtual {v0, v4}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setRetryPolicy(Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    .line 30
    invoke-virtual {v0}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->build()Lcom/helpshift/common/poller/HttpBackoff;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/common/domain/Poller;->aggressiveBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    .line 32
    new-instance v0, Lcom/helpshift/common/poller/HttpBackoff$Builder;

    invoke-direct {v0}, Lcom/helpshift/common/poller/HttpBackoff$Builder;-><init>()V

    sget-object v4, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v5, 0x1e

    .line 33
    invoke-static {v5, v6, v4}, Lcom/helpshift/common/poller/Delay;->of(JLjava/util/concurrent/TimeUnit;)Lcom/helpshift/common/poller/Delay;

    move-result-object v4

    invoke-virtual {v0, v4}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setBaseInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    sget-object v4, Ljava/util/concurrent/TimeUnit;->MINUTES:Ljava/util/concurrent/TimeUnit;

    .line 34
    invoke-static {v2, v3, v4}, Lcom/helpshift/common/poller/Delay;->of(JLjava/util/concurrent/TimeUnit;)Lcom/helpshift/common/poller/Delay;

    move-result-object v2

    invoke-virtual {v0, v2}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setMaxInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    .line 35
    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setRandomness(F)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    const/high16 v1, 0x40800000    # 4.0f

    .line 36
    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setMultiplier(F)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    .line 37
    invoke-direct {p0}, Lcom/helpshift/common/domain/Poller;->getPollerRetryPollicy()Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setRetryPolicy(Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    .line 38
    invoke-virtual {v0}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->build()Lcom/helpshift/common/poller/HttpBackoff;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/common/domain/Poller;->passiveBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    .line 41
    iput-object p1, p0, Lcom/helpshift/common/domain/Poller;->domain:Lcom/helpshift/common/domain/Domain;

    .line 42
    iput-object p2, p0, Lcom/helpshift/common/domain/Poller;->poll:Lcom/helpshift/common/domain/F;

    return-void
.end method

.method private getPollerRetryPollicy()Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;
    .locals 1

    .line 87
    new-instance v0, Lcom/helpshift/common/domain/Poller$1;

    invoke-direct {v0, p0}, Lcom/helpshift/common/domain/Poller$1;-><init>(Lcom/helpshift/common/domain/Poller;)V

    return-object v0
.end method


# virtual methods
.method public declared-synchronized start(Lcom/helpshift/common/domain/PollingInterval;JLcom/helpshift/common/domain/PollFunction$PollFunctionListener;)V
    .locals 6

    monitor-enter p0

    .line 53
    :try_start_0
    invoke-virtual {p0}, Lcom/helpshift/common/domain/Poller;->stop()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez p1, :cond_0

    .line 56
    monitor-exit p0

    return-void

    .line 60
    :cond_0
    :try_start_1
    sget-object v0, Lcom/helpshift/common/domain/Poller$2;->$SwitchMap$com$helpshift$common$domain$PollingInterval:[I

    invoke-virtual {p1}, Lcom/helpshift/common/domain/PollingInterval;->ordinal()I

    move-result p1

    aget p1, v0, p1

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 68
    :pswitch_0
    new-instance p1, Lcom/helpshift/common/domain/PollFunction;

    iget-object v1, p0, Lcom/helpshift/common/domain/Poller;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/common/domain/Poller;->conservativeBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    iget-object v3, p0, Lcom/helpshift/common/domain/Poller;->poll:Lcom/helpshift/common/domain/F;

    sget-object v4, Lcom/helpshift/common/domain/PollingInterval;->CONSERVATIVE:Lcom/helpshift/common/domain/PollingInterval;

    move-object v0, p1

    move-object v5, p4

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/common/domain/PollFunction;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/poller/HttpBackoff;Lcom/helpshift/common/domain/F;Lcom/helpshift/common/domain/PollingInterval;Lcom/helpshift/common/domain/PollFunction$PollFunctionListener;)V

    iput-object p1, p0, Lcom/helpshift/common/domain/Poller;->activePollFunction:Lcom/helpshift/common/domain/PollFunction;

    goto :goto_0

    .line 65
    :pswitch_1
    new-instance p1, Lcom/helpshift/common/domain/PollFunction;

    iget-object v1, p0, Lcom/helpshift/common/domain/Poller;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/common/domain/Poller;->passiveBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    iget-object v3, p0, Lcom/helpshift/common/domain/Poller;->poll:Lcom/helpshift/common/domain/F;

    sget-object v4, Lcom/helpshift/common/domain/PollingInterval;->PASSIVE:Lcom/helpshift/common/domain/PollingInterval;

    move-object v0, p1

    move-object v5, p4

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/common/domain/PollFunction;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/poller/HttpBackoff;Lcom/helpshift/common/domain/F;Lcom/helpshift/common/domain/PollingInterval;Lcom/helpshift/common/domain/PollFunction$PollFunctionListener;)V

    iput-object p1, p0, Lcom/helpshift/common/domain/Poller;->activePollFunction:Lcom/helpshift/common/domain/PollFunction;

    goto :goto_0

    .line 62
    :pswitch_2
    new-instance p1, Lcom/helpshift/common/domain/PollFunction;

    iget-object v1, p0, Lcom/helpshift/common/domain/Poller;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/common/domain/Poller;->aggressiveBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    iget-object v3, p0, Lcom/helpshift/common/domain/Poller;->poll:Lcom/helpshift/common/domain/F;

    sget-object v4, Lcom/helpshift/common/domain/PollingInterval;->AGGRESSIVE:Lcom/helpshift/common/domain/PollingInterval;

    move-object v0, p1

    move-object v5, p4

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/common/domain/PollFunction;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/poller/HttpBackoff;Lcom/helpshift/common/domain/F;Lcom/helpshift/common/domain/PollingInterval;Lcom/helpshift/common/domain/PollFunction$PollFunctionListener;)V

    iput-object p1, p0, Lcom/helpshift/common/domain/Poller;->activePollFunction:Lcom/helpshift/common/domain/PollFunction;

    .line 73
    :goto_0
    iget-object p1, p0, Lcom/helpshift/common/domain/Poller;->activePollFunction:Lcom/helpshift/common/domain/PollFunction;

    invoke-virtual {p1, p2, p3}, Lcom/helpshift/common/domain/PollFunction;->start(J)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 74
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 52
    monitor-exit p0

    throw p1

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public declared-synchronized stop()V
    .locals 1

    monitor-enter p0

    .line 80
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Poller;->activePollFunction:Lcom/helpshift/common/domain/PollFunction;

    if-eqz v0, :cond_0

    .line 81
    iget-object v0, p0, Lcom/helpshift/common/domain/Poller;->activePollFunction:Lcom/helpshift/common/domain/PollFunction;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/PollFunction;->stop()V

    const/4 v0, 0x0

    .line 82
    iput-object v0, p0, Lcom/helpshift/common/domain/Poller;->activePollFunction:Lcom/helpshift/common/domain/PollFunction;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 84
    :cond_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 79
    monitor-exit p0

    throw v0
.end method
