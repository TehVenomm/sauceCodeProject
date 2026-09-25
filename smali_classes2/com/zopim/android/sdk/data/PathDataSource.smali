.class public Lcom/zopim/android/sdk/data/PathDataSource;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/data/DataSource;


# instance fields
.field private mAccountPath:Lcom/zopim/android/sdk/data/LivechatAccountPath;

.field private mAgentsPath:Lcom/zopim/android/sdk/data/LivechatAgentsPath;

.field private mChatLogPath:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

.field private mConnectionPath:Lcom/zopim/android/sdk/data/ConnectionPath;

.field private mDepartmentsPath:Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

.field private mFormsPath:Lcom/zopim/android/sdk/data/LivechatFormsPath;

.field private mProfilePath:Lcom/zopim/android/sdk/data/LivechatProfilePath;


# direct methods
.method public constructor <init>()V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    invoke-static {}, Lcom/zopim/android/sdk/data/ConnectionPath;->getInstance()Lcom/zopim/android/sdk/data/ConnectionPath;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mConnectionPath:Lcom/zopim/android/sdk/data/ConnectionPath;

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatProfilePath;->getInstance()Lcom/zopim/android/sdk/data/LivechatProfilePath;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mProfilePath:Lcom/zopim/android/sdk/data/LivechatProfilePath;

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatAccountPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatAccountPath;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mAccountPath:Lcom/zopim/android/sdk/data/LivechatAccountPath;

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatAgentsPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatAgentsPath;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mAgentsPath:Lcom/zopim/android/sdk/data/LivechatAgentsPath;

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mDepartmentsPath:Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mChatLogPath:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatFormsPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatFormsPath;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mFormsPath:Lcom/zopim/android/sdk/data/LivechatFormsPath;

    return-void
.end method


# virtual methods
.method public addAccountObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mAccountPath:Lcom/zopim/android/sdk/data/LivechatAccountPath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mAccountPath:Lcom/zopim/android/sdk/data/LivechatAccountPath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/LivechatAccountPath;->addObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public addAgentsObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mAgentsPath:Lcom/zopim/android/sdk/data/LivechatAgentsPath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mAgentsPath:Lcom/zopim/android/sdk/data/LivechatAgentsPath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/LivechatAgentsPath;->addObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public addChatLogObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mChatLogPath:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mChatLogPath:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->addObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public addConnectionObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mConnectionPath:Lcom/zopim/android/sdk/data/ConnectionPath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mConnectionPath:Lcom/zopim/android/sdk/data/ConnectionPath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/ConnectionPath;->addObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public addDepartmentsObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mDepartmentsPath:Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mDepartmentsPath:Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->addObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public addFormsObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mFormsPath:Lcom/zopim/android/sdk/data/LivechatFormsPath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mFormsPath:Lcom/zopim/android/sdk/data/LivechatFormsPath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/LivechatFormsPath;->addObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public addProfileObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mProfilePath:Lcom/zopim/android/sdk/data/LivechatProfilePath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mProfilePath:Lcom/zopim/android/sdk/data/LivechatProfilePath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/LivechatProfilePath;->addObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public declared-synchronized clear()V
    .locals 1

    monitor-enter p0

    :try_start_0
    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mConnectionPath:Lcom/zopim/android/sdk/data/ConnectionPath;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/data/ConnectionPath;->clear()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method public deleteAccountObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mAccountPath:Lcom/zopim/android/sdk/data/LivechatAccountPath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mAccountPath:Lcom/zopim/android/sdk/data/LivechatAccountPath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/LivechatAccountPath;->deleteObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public deleteAgentsObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mAgentsPath:Lcom/zopim/android/sdk/data/LivechatAgentsPath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mAgentsPath:Lcom/zopim/android/sdk/data/LivechatAgentsPath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/LivechatAgentsPath;->deleteObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public deleteChatLogObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mChatLogPath:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mChatLogPath:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->deleteObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public deleteConnectionObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mConnectionPath:Lcom/zopim/android/sdk/data/ConnectionPath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mConnectionPath:Lcom/zopim/android/sdk/data/ConnectionPath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/ConnectionPath;->deleteObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public deleteDepartmentsObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mDepartmentsPath:Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mDepartmentsPath:Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->deleteObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public deleteFormsObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mFormsPath:Lcom/zopim/android/sdk/data/LivechatFormsPath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mFormsPath:Lcom/zopim/android/sdk/data/LivechatFormsPath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/LivechatFormsPath;->deleteObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public declared-synchronized deleteObservers()V
    .locals 1

    monitor-enter p0

    :try_start_0
    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mConnectionPath:Lcom/zopim/android/sdk/data/ConnectionPath;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/data/ConnectionPath;->deleteObservers()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method public deleteProfileObserver(Ljava/util/Observer;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mProfilePath:Lcom/zopim/android/sdk/data/LivechatProfilePath;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mProfilePath:Lcom/zopim/android/sdk/data/LivechatProfilePath;

    invoke-virtual {v1, p1}, Lcom/zopim/android/sdk/data/LivechatProfilePath;->deleteObserver(Ljava/util/Observer;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public getAccount()Lcom/zopim/android/sdk/model/Account;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mAccountPath:Lcom/zopim/android/sdk/data/LivechatAccountPath;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/data/LivechatAccountPath;->getData()Lcom/zopim/android/sdk/model/Account;

    move-result-object v0

    return-object v0
.end method

.method public getAgents()Ljava/util/LinkedHashMap;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/Agent;",
            ">;"
        }
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mAgentsPath:Lcom/zopim/android/sdk/data/LivechatAgentsPath;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/data/LivechatAgentsPath;->getData()Ljava/util/LinkedHashMap;

    move-result-object v0

    return-object v0
.end method

.method public getChatLog()Ljava/util/LinkedHashMap;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ">;"
        }
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mChatLogPath:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getData()Ljava/util/LinkedHashMap;

    move-result-object v0

    return-object v0
.end method

.method public getConnection()Lcom/zopim/android/sdk/model/Connection;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mConnectionPath:Lcom/zopim/android/sdk/data/ConnectionPath;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/data/ConnectionPath;->getData()Lcom/zopim/android/sdk/model/Connection;

    move-result-object v0

    return-object v0
.end method

.method public getDepartments()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/Department;",
            ">;"
        }
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mDepartmentsPath:Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->getData()Ljava/util/LinkedHashMap;

    move-result-object v0

    return-object v0
.end method

.method public getForms()Lcom/zopim/android/sdk/model/Forms;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mFormsPath:Lcom/zopim/android/sdk/data/LivechatFormsPath;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/data/LivechatFormsPath;->getData()Lcom/zopim/android/sdk/model/Forms;

    move-result-object v0

    return-object v0
.end method

.method public getProfile()Lcom/zopim/android/sdk/model/Profile;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/data/PathDataSource;->mProfilePath:Lcom/zopim/android/sdk/data/LivechatProfilePath;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/data/LivechatProfilePath;->getData()Lcom/zopim/android/sdk/model/Profile;

    move-result-object v0

    return-object v0
.end method
