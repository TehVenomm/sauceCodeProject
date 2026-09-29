.class public Lcom/helpshift/redaction/RedactionManager;
.super Ljava/lang/Object;
.source "RedactionManager.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/redaction/RedactionManager$RedactionManagerListener;
    }
.end annotation


# instance fields
.field private domain:Lcom/helpshift/common/domain/Domain;

.field private redactionDAO:Lcom/helpshift/redaction/RedactionDAO;

.field private redactionManagerListener:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Lcom/helpshift/redaction/RedactionManager$RedactionManagerListener;",
            ">;"
        }
    .end annotation
.end field

.field private userDM:Lcom/helpshift/account/domainmodel/UserDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/redaction/RedactionManager$RedactionManagerListener;)V
    .locals 0

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 23
    iput-object p2, p0, Lcom/helpshift/redaction/RedactionManager;->domain:Lcom/helpshift/common/domain/Domain;

    .line 24
    iput-object p3, p0, Lcom/helpshift/redaction/RedactionManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 25
    new-instance p2, Ljava/lang/ref/WeakReference;

    invoke-direct {p2, p4}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object p2, p0, Lcom/helpshift/redaction/RedactionManager;->redactionManagerListener:Ljava/lang/ref/WeakReference;

    .line 26
    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getRedactionDAO()Lcom/helpshift/redaction/RedactionDAO;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/redaction/RedactionManager;->redactionDAO:Lcom/helpshift/redaction/RedactionDAO;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/redaction/RedactionManager;)Lcom/helpshift/common/domain/Domain;
    .locals 0

    .line 15
    iget-object p0, p0, Lcom/helpshift/redaction/RedactionManager;->domain:Lcom/helpshift/common/domain/Domain;

    return-object p0
.end method

.method static synthetic access$100(Lcom/helpshift/redaction/RedactionManager;)Lcom/helpshift/account/domainmodel/UserDM;
    .locals 0

    .line 15
    iget-object p0, p0, Lcom/helpshift/redaction/RedactionManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    return-object p0
.end method

.method static synthetic access$200(Lcom/helpshift/redaction/RedactionManager;Lcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionState;)V
    .locals 0

    .line 15
    invoke-direct {p0, p1, p2}, Lcom/helpshift/redaction/RedactionManager;->updateUserRedactionState(Lcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionState;)V

    return-void
.end method

.method static synthetic access$300(Lcom/helpshift/redaction/RedactionManager;)Ljava/lang/ref/WeakReference;
    .locals 0

    .line 15
    iget-object p0, p0, Lcom/helpshift/redaction/RedactionManager;->redactionManagerListener:Ljava/lang/ref/WeakReference;

    return-object p0
.end method

.method private updateUserRedactionState(Lcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionState;)V
    .locals 3

    .line 90
    sget-object v0, Lcom/helpshift/redaction/RedactionState;->COMPLETED:Lcom/helpshift/redaction/RedactionState;

    if-ne p2, v0, :cond_0

    .line 92
    iget-object v0, p0, Lcom/helpshift/redaction/RedactionManager;->redactionDAO:Lcom/helpshift/redaction/RedactionDAO;

    iget-object v1, p0, Lcom/helpshift/redaction/RedactionManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/redaction/RedactionDAO;->deleteRedactionDetail(J)V

    goto :goto_0

    .line 96
    :cond_0
    iget-object v0, p0, Lcom/helpshift/redaction/RedactionManager;->redactionDAO:Lcom/helpshift/redaction/RedactionDAO;

    iget-object v1, p0, Lcom/helpshift/redaction/RedactionManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2, p2}, Lcom/helpshift/redaction/RedactionDAO;->updateRedactionState(JLcom/helpshift/redaction/RedactionState;)V

    .line 100
    :goto_0
    iget-object v0, p0, Lcom/helpshift/redaction/RedactionManager;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/redaction/RedactionManager$2;

    invoke-direct {v1, p0, p1, p2}, Lcom/helpshift/redaction/RedactionManager$2;-><init>(Lcom/helpshift/redaction/RedactionManager;Lcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionState;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runSerial(Lcom/helpshift/common/domain/F;)V

    return-void
.end method


# virtual methods
.method public declared-synchronized executeRedaction()V
    .locals 2

    monitor-enter p0

    .line 33
    :try_start_0
    invoke-virtual {p0}, Lcom/helpshift/redaction/RedactionManager;->getRedactionState()Lcom/helpshift/redaction/RedactionState;

    move-result-object v0

    .line 35
    sget-object v1, Lcom/helpshift/redaction/RedactionState;->PENDING:Lcom/helpshift/redaction/RedactionState;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eq v0, v1, :cond_0

    .line 36
    monitor-exit p0

    return-void

    .line 40
    :cond_0
    :try_start_1
    sget-object v1, Lcom/helpshift/redaction/RedactionState;->IN_PROGRESS:Lcom/helpshift/redaction/RedactionState;

    invoke-direct {p0, v0, v1}, Lcom/helpshift/redaction/RedactionManager;->updateUserRedactionState(Lcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionState;)V

    .line 41
    iget-object v0, p0, Lcom/helpshift/redaction/RedactionManager;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/redaction/RedactionManager$1;

    invoke-direct {v1, p0}, Lcom/helpshift/redaction/RedactionManager$1;-><init>(Lcom/helpshift/redaction/RedactionManager;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 59
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 32
    monitor-exit p0

    throw v0
.end method

.method public getRedactionState()Lcom/helpshift/redaction/RedactionState;
    .locals 3

    .line 62
    iget-object v0, p0, Lcom/helpshift/redaction/RedactionManager;->redactionDAO:Lcom/helpshift/redaction/RedactionDAO;

    iget-object v1, p0, Lcom/helpshift/redaction/RedactionManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/redaction/RedactionDAO;->getRedactionDetail(J)Lcom/helpshift/redaction/RedactionDetail;

    move-result-object v0

    if-nez v0, :cond_0

    .line 70
    sget-object v0, Lcom/helpshift/redaction/RedactionState;->COMPLETED:Lcom/helpshift/redaction/RedactionState;

    return-object v0

    .line 72
    :cond_0
    iget-object v0, v0, Lcom/helpshift/redaction/RedactionDetail;->redactionState:Lcom/helpshift/redaction/RedactionState;

    return-object v0
.end method

.method public setAppropriateInitialState()V
    .locals 2

    .line 76
    invoke-virtual {p0}, Lcom/helpshift/redaction/RedactionManager;->getRedactionState()Lcom/helpshift/redaction/RedactionState;

    move-result-object v0

    .line 77
    sget-object v1, Lcom/helpshift/redaction/RedactionState;->IN_PROGRESS:Lcom/helpshift/redaction/RedactionState;

    if-ne v0, v1, :cond_0

    .line 79
    sget-object v1, Lcom/helpshift/redaction/RedactionState;->PENDING:Lcom/helpshift/redaction/RedactionState;

    invoke-direct {p0, v0, v1}, Lcom/helpshift/redaction/RedactionManager;->updateUserRedactionState(Lcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionState;)V

    :cond_0
    return-void
.end method
