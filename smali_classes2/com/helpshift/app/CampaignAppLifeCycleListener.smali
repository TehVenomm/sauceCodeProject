.class public Lcom/helpshift/app/CampaignAppLifeCycleListener;
.super Ljava/lang/Object;
.source "CampaignAppLifeCycleListener.java"

# interfaces
.implements Lcom/helpshift/applifecycle/HSAppLifeCycleListener;


# instance fields
.field private cachedExecutorService:Ljava/util/concurrent/ExecutorService;

.field private lifecycleListeners:Ljava/util/concurrent/LinkedBlockingDeque;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/LinkedBlockingDeque<",
            "Lcom/helpshift/app/LifecycleListener;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 2

    .line 12
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 14
    new-instance v0, Ljava/util/concurrent/LinkedBlockingDeque;

    invoke-direct {v0}, Ljava/util/concurrent/LinkedBlockingDeque;-><init>()V

    iput-object v0, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener;->lifecycleListeners:Ljava/util/concurrent/LinkedBlockingDeque;

    .line 15
    new-instance v0, Lcom/helpshift/common/domain/HSThreadFactory;

    const-string v1, "m-lcycle"

    invoke-direct {v0, v1}, Lcom/helpshift/common/domain/HSThreadFactory;-><init>(Ljava/lang/String;)V

    invoke-static {v0}, Ljava/util/concurrent/Executors;->newCachedThreadPool(Ljava/util/concurrent/ThreadFactory;)Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener;->cachedExecutorService:Ljava/util/concurrent/ExecutorService;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/app/CampaignAppLifeCycleListener;)Ljava/util/concurrent/LinkedBlockingDeque;
    .locals 0

    .line 12
    iget-object p0, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener;->lifecycleListeners:Ljava/util/concurrent/LinkedBlockingDeque;

    return-object p0
.end method


# virtual methods
.method public addLifecycleListener(Lcom/helpshift/app/LifecycleListener;)V
    .locals 2

    .line 23
    iget-object v0, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener;->lifecycleListeners:Ljava/util/concurrent/LinkedBlockingDeque;

    invoke-virtual {v0, p1}, Ljava/util/concurrent/LinkedBlockingDeque;->addFirst(Ljava/lang/Object;)V

    .line 28
    iget-object v0, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener;->cachedExecutorService:Ljava/util/concurrent/ExecutorService;

    new-instance v1, Lcom/helpshift/app/CampaignAppLifeCycleListener$1;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/app/CampaignAppLifeCycleListener$1;-><init>(Lcom/helpshift/app/CampaignAppLifeCycleListener;Lcom/helpshift/app/LifecycleListener;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    return-void
.end method

.method public onAppBackground(Landroid/content/Context;)V
    .locals 1

    .line 67
    iget-object p1, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener;->lifecycleListeners:Ljava/util/concurrent/LinkedBlockingDeque;

    invoke-virtual {p1}, Ljava/util/concurrent/LinkedBlockingDeque;->size()I

    move-result p1

    if-nez p1, :cond_0

    return-void

    .line 72
    :cond_0
    iget-object p1, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener;->cachedExecutorService:Ljava/util/concurrent/ExecutorService;

    new-instance v0, Lcom/helpshift/app/CampaignAppLifeCycleListener$3;

    invoke-direct {v0, p0}, Lcom/helpshift/app/CampaignAppLifeCycleListener$3;-><init>(Lcom/helpshift/app/CampaignAppLifeCycleListener;)V

    invoke-interface {p1, v0}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    return-void
.end method

.method public onAppForeground(Landroid/content/Context;)V
    .locals 1

    .line 45
    iget-object p1, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener;->lifecycleListeners:Ljava/util/concurrent/LinkedBlockingDeque;

    invoke-virtual {p1}, Ljava/util/concurrent/LinkedBlockingDeque;->size()I

    move-result p1

    if-nez p1, :cond_0

    return-void

    .line 50
    :cond_0
    iget-object p1, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener;->cachedExecutorService:Ljava/util/concurrent/ExecutorService;

    new-instance v0, Lcom/helpshift/app/CampaignAppLifeCycleListener$2;

    invoke-direct {v0, p0}, Lcom/helpshift/app/CampaignAppLifeCycleListener$2;-><init>(Lcom/helpshift/app/CampaignAppLifeCycleListener;)V

    invoke-interface {p1, v0}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    return-void
.end method
