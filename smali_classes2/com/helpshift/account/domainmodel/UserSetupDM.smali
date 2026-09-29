.class public Lcom/helpshift/account/domainmodel/UserSetupDM;
.super Ljava/lang/Object;
.source "UserSetupDM.java"

# interfaces
.implements Lcom/helpshift/account/domainmodel/UserSyncDM$UserSyncListener;
.implements Lcom/helpshift/migration/RemoteDataMigrator$RemoteDataMigratorListener;
.implements Lcom/helpshift/redaction/RedactionManager$RedactionManagerListener;
.implements Lcom/helpshift/common/AutoRetriableDM;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/account/domainmodel/UserSetupDM$UserSetupListener;
    }
.end annotation


# instance fields
.field domain:Lcom/helpshift/common/domain/Domain;

.field private redactionManager:Lcom/helpshift/redaction/RedactionManager;

.field private remoteDataMigrator:Lcom/helpshift/migration/RemoteDataMigrator;

.field userDM:Lcom/helpshift/account/domainmodel/UserDM;

.field private userSetupListener:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Lcom/helpshift/account/domainmodel/UserSetupDM$UserSetupListener;",
            ">;"
        }
    .end annotation
.end field

.field private userSyncDM:Lcom/helpshift/account/domainmodel/UserSyncDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserManagerDM;Lcom/helpshift/account/domainmodel/IUserSyncExecutor;)V
    .locals 8

    .line 36
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 37
    iput-object p2, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 38
    iput-object p3, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 41
    new-instance v7, Lcom/helpshift/account/domainmodel/UserSyncDM;

    move-object v0, v7

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-object v4, p4

    move-object v5, p5

    move-object v6, p0

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/account/domainmodel/UserSyncDM;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserManagerDM;Lcom/helpshift/account/domainmodel/IUserSyncExecutor;Lcom/helpshift/account/domainmodel/UserSyncDM$UserSyncListener;)V

    iput-object v7, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->userSyncDM:Lcom/helpshift/account/domainmodel/UserSyncDM;

    .line 42
    new-instance p4, Lcom/helpshift/migration/RemoteDataMigrator;

    invoke-direct {p4, p1, p2, p3, p0}, Lcom/helpshift/migration/RemoteDataMigrator;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/migration/RemoteDataMigrator$RemoteDataMigratorListener;)V

    iput-object p4, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->remoteDataMigrator:Lcom/helpshift/migration/RemoteDataMigrator;

    .line 43
    new-instance p4, Lcom/helpshift/redaction/RedactionManager;

    invoke-direct {p4, p1, p2, p3, p0}, Lcom/helpshift/redaction/RedactionManager;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/redaction/RedactionManager$RedactionManagerListener;)V

    iput-object p4, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->redactionManager:Lcom/helpshift/redaction/RedactionManager;

    return-void
.end method

.method private onMigrationStateChange(Lcom/helpshift/migration/MigrationState;)V
    .locals 1

    .line 157
    sget-object v0, Lcom/helpshift/migration/MigrationState;->COMPLETED:Lcom/helpshift/migration/MigrationState;

    if-ne p1, v0, :cond_2

    .line 158
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->userSyncDM:Lcom/helpshift/account/domainmodel/UserSyncDM;

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserSyncDM;->getSyncState()Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-result-object p1

    .line 159
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    if-eq p1, v0, :cond_1

    sget-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    if-ne p1, v0, :cond_0

    goto :goto_0

    .line 164
    :cond_0
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->userSyncDM:Lcom/helpshift/account/domainmodel/UserSyncDM;

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserSyncDM;->syncUser()V

    goto :goto_1

    .line 161
    :cond_1
    :goto_0
    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->onUserSyncStateChange(Lcom/helpshift/account/domainmodel/UserSyncStatus;)V

    goto :goto_1

    .line 167
    :cond_2
    sget-object v0, Lcom/helpshift/migration/MigrationState;->IN_PROGRESS:Lcom/helpshift/migration/MigrationState;

    if-ne p1, v0, :cond_3

    .line 168
    sget-object p1, Lcom/helpshift/account/domainmodel/UserSetupState;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->updateUserSetupStateChange(Lcom/helpshift/account/domainmodel/UserSetupState;)V

    goto :goto_1

    .line 170
    :cond_3
    sget-object v0, Lcom/helpshift/migration/MigrationState;->FAILED:Lcom/helpshift/migration/MigrationState;

    if-ne p1, v0, :cond_4

    .line 171
    sget-object p1, Lcom/helpshift/account/domainmodel/UserSetupState;->FAILED:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->updateUserSetupStateChange(Lcom/helpshift/account/domainmodel/UserSetupState;)V

    goto :goto_1

    .line 173
    :cond_4
    sget-object v0, Lcom/helpshift/migration/MigrationState;->NOT_STARTED:Lcom/helpshift/migration/MigrationState;

    if-ne p1, v0, :cond_5

    .line 174
    sget-object p1, Lcom/helpshift/account/domainmodel/UserSetupState;->NON_STARTED:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->updateUserSetupStateChange(Lcom/helpshift/account/domainmodel/UserSetupState;)V

    :cond_5
    :goto_1
    return-void
.end method

.method private onRedactionStateChange(Lcom/helpshift/redaction/RedactionState;)V
    .locals 1

    .line 137
    sget-object v0, Lcom/helpshift/redaction/RedactionState;->COMPLETED:Lcom/helpshift/redaction/RedactionState;

    if-ne p1, v0, :cond_2

    .line 138
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->remoteDataMigrator:Lcom/helpshift/migration/RemoteDataMigrator;

    invoke-virtual {p1}, Lcom/helpshift/migration/RemoteDataMigrator;->getProfileMigrationState()Lcom/helpshift/migration/MigrationState;

    move-result-object p1

    .line 140
    sget-object v0, Lcom/helpshift/migration/MigrationState;->COMPLETED:Lcom/helpshift/migration/MigrationState;

    if-eq p1, v0, :cond_1

    sget-object v0, Lcom/helpshift/migration/MigrationState;->IN_PROGRESS:Lcom/helpshift/migration/MigrationState;

    if-ne p1, v0, :cond_0

    goto :goto_0

    .line 145
    :cond_0
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->remoteDataMigrator:Lcom/helpshift/migration/RemoteDataMigrator;

    invoke-virtual {p1}, Lcom/helpshift/migration/RemoteDataMigrator;->startProfileMigration()V

    goto :goto_1

    .line 142
    :cond_1
    :goto_0
    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->onMigrationStateChange(Lcom/helpshift/migration/MigrationState;)V

    goto :goto_1

    .line 148
    :cond_2
    sget-object v0, Lcom/helpshift/redaction/RedactionState;->IN_PROGRESS:Lcom/helpshift/redaction/RedactionState;

    if-ne p1, v0, :cond_3

    .line 149
    sget-object p1, Lcom/helpshift/account/domainmodel/UserSetupState;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->updateUserSetupStateChange(Lcom/helpshift/account/domainmodel/UserSetupState;)V

    goto :goto_1

    .line 151
    :cond_3
    sget-object v0, Lcom/helpshift/redaction/RedactionState;->PENDING:Lcom/helpshift/redaction/RedactionState;

    if-ne p1, v0, :cond_4

    .line 152
    sget-object p1, Lcom/helpshift/account/domainmodel/UserSetupState;->NON_STARTED:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->updateUserSetupStateChange(Lcom/helpshift/account/domainmodel/UserSetupState;)V

    :cond_4
    :goto_1
    return-void
.end method

.method private onUserSyncStateChange(Lcom/helpshift/account/domainmodel/UserSyncStatus;)V
    .locals 1

    .line 179
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    if-ne p1, v0, :cond_0

    .line 180
    sget-object p1, Lcom/helpshift/account/domainmodel/UserSetupState;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->updateUserSetupStateChange(Lcom/helpshift/account/domainmodel/UserSetupState;)V

    goto :goto_0

    .line 182
    :cond_0
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    if-ne p1, v0, :cond_1

    .line 183
    sget-object p1, Lcom/helpshift/account/domainmodel/UserSetupState;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->updateUserSetupStateChange(Lcom/helpshift/account/domainmodel/UserSetupState;)V

    goto :goto_0

    .line 185
    :cond_1
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->FAILED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    if-ne p1, v0, :cond_2

    .line 186
    sget-object p1, Lcom/helpshift/account/domainmodel/UserSetupState;->FAILED:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->updateUserSetupStateChange(Lcom/helpshift/account/domainmodel/UserSetupState;)V

    goto :goto_0

    .line 188
    :cond_2
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->NOT_STARTED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    if-ne p1, v0, :cond_3

    .line 189
    sget-object p1, Lcom/helpshift/account/domainmodel/UserSetupState;->NON_STARTED:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->updateUserSetupStateChange(Lcom/helpshift/account/domainmodel/UserSetupState;)V

    :cond_3
    :goto_0
    return-void
.end method

.method private updateUserSetupStateChange(Lcom/helpshift/account/domainmodel/UserSetupState;)V
    .locals 3

    .line 194
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->userSetupListener:Ljava/lang/ref/WeakReference;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->userSetupListener:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/account/domainmodel/UserSetupDM$UserSetupListener;

    :goto_0
    if-eqz v0, :cond_1

    .line 197
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v2, Lcom/helpshift/account/domainmodel/UserSetupDM$1;

    invoke-direct {v2, p0, v0, p1}, Lcom/helpshift/account/domainmodel/UserSetupDM$1;-><init>(Lcom/helpshift/account/domainmodel/UserSetupDM;Lcom/helpshift/account/domainmodel/UserSetupDM$UserSetupListener;Lcom/helpshift/account/domainmodel/UserSetupState;)V

    invoke-virtual {v1, v2}, Lcom/helpshift/common/domain/Domain;->runSerial(Lcom/helpshift/common/domain/F;)V

    .line 205
    :cond_1
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupState;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSetupState;

    if-ne p1, v0, :cond_2

    .line 207
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v0, Lcom/helpshift/account/domainmodel/UserSetupDM$2;

    invoke-direct {v0, p0}, Lcom/helpshift/account/domainmodel/UserSetupDM$2;-><init>(Lcom/helpshift/account/domainmodel/UserSetupDM;)V

    invoke-virtual {p1, v0}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    :cond_2
    return-void
.end method


# virtual methods
.method public getState()Lcom/helpshift/account/domainmodel/UserSetupState;
    .locals 2

    .line 60
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->redactionManager:Lcom/helpshift/redaction/RedactionManager;

    invoke-virtual {v0}, Lcom/helpshift/redaction/RedactionManager;->getRedactionState()Lcom/helpshift/redaction/RedactionState;

    move-result-object v0

    .line 62
    sget-object v1, Lcom/helpshift/redaction/RedactionState;->PENDING:Lcom/helpshift/redaction/RedactionState;

    if-ne v0, v1, :cond_0

    .line 63
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupState;->NON_STARTED:Lcom/helpshift/account/domainmodel/UserSetupState;

    return-object v0

    .line 66
    :cond_0
    sget-object v1, Lcom/helpshift/redaction/RedactionState;->IN_PROGRESS:Lcom/helpshift/redaction/RedactionState;

    if-ne v0, v1, :cond_1

    .line 67
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupState;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSetupState;

    return-object v0

    .line 72
    :cond_1
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->remoteDataMigrator:Lcom/helpshift/migration/RemoteDataMigrator;

    invoke-virtual {v0}, Lcom/helpshift/migration/RemoteDataMigrator;->getProfileMigrationState()Lcom/helpshift/migration/MigrationState;

    move-result-object v0

    .line 74
    sget-object v1, Lcom/helpshift/migration/MigrationState;->NOT_STARTED:Lcom/helpshift/migration/MigrationState;

    if-ne v0, v1, :cond_2

    .line 75
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupState;->NON_STARTED:Lcom/helpshift/account/domainmodel/UserSetupState;

    return-object v0

    .line 78
    :cond_2
    sget-object v1, Lcom/helpshift/migration/MigrationState;->FAILED:Lcom/helpshift/migration/MigrationState;

    if-ne v0, v1, :cond_3

    .line 79
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupState;->FAILED:Lcom/helpshift/account/domainmodel/UserSetupState;

    return-object v0

    .line 82
    :cond_3
    sget-object v1, Lcom/helpshift/migration/MigrationState;->IN_PROGRESS:Lcom/helpshift/migration/MigrationState;

    if-ne v0, v1, :cond_4

    .line 83
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupState;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSetupState;

    return-object v0

    .line 88
    :cond_4
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->userSyncDM:Lcom/helpshift/account/domainmodel/UserSyncDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserSyncDM;->getSyncState()Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-result-object v0

    .line 90
    sget-object v1, Lcom/helpshift/account/domainmodel/UserSyncStatus;->NOT_STARTED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    if-ne v0, v1, :cond_5

    .line 91
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupState;->NON_STARTED:Lcom/helpshift/account/domainmodel/UserSetupState;

    return-object v0

    .line 94
    :cond_5
    sget-object v1, Lcom/helpshift/account/domainmodel/UserSyncStatus;->FAILED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    if-ne v0, v1, :cond_6

    .line 95
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupState;->FAILED:Lcom/helpshift/account/domainmodel/UserSetupState;

    return-object v0

    .line 98
    :cond_6
    sget-object v1, Lcom/helpshift/account/domainmodel/UserSyncStatus;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    if-ne v0, v1, :cond_7

    .line 99
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupState;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSetupState;

    return-object v0

    .line 102
    :cond_7
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupState;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSetupState;

    return-object v0
.end method

.method public init()V
    .locals 2

    .line 50
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->redactionManager:Lcom/helpshift/redaction/RedactionManager;

    invoke-virtual {v0}, Lcom/helpshift/redaction/RedactionManager;->setAppropriateInitialState()V

    .line 51
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->remoteDataMigrator:Lcom/helpshift/migration/RemoteDataMigrator;

    invoke-virtual {v0}, Lcom/helpshift/migration/RemoteDataMigrator;->setAppropriateInitialState()V

    .line 52
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->userSyncDM:Lcom/helpshift/account/domainmodel/UserSyncDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserSyncDM;->setAppropriateInitialState()V

    .line 54
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v0

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->MIGRATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v0, v1, p0}, Lcom/helpshift/common/AutoRetryFailedEventDM;->register(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;Lcom/helpshift/common/AutoRetriableDM;)V

    .line 55
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v0

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->SYNC_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v0, v1, p0}, Lcom/helpshift/common/AutoRetryFailedEventDM;->register(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;Lcom/helpshift/common/AutoRetriableDM;)V

    return-void
.end method

.method public onMigrationStateChanged(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/migration/MigrationState;Lcom/helpshift/migration/MigrationState;)V
    .locals 0

    .line 133
    invoke-direct {p0, p3}, Lcom/helpshift/account/domainmodel/UserSetupDM;->onMigrationStateChange(Lcom/helpshift/migration/MigrationState;)V

    return-void
.end method

.method public redactionStateChanged(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionState;)V
    .locals 0

    .line 123
    invoke-direct {p0, p3}, Lcom/helpshift/account/domainmodel/UserSetupDM;->onRedactionStateChange(Lcom/helpshift/redaction/RedactionState;)V

    return-void
.end method

.method public registerUserSetupListener(Lcom/helpshift/account/domainmodel/UserSetupDM$UserSetupListener;)V
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    .line 233
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->userSetupListener:Ljava/lang/ref/WeakReference;

    goto :goto_0

    .line 236
    :cond_0
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->userSetupListener:Ljava/lang/ref/WeakReference;

    :goto_0
    return-void
.end method

.method public sendFailedApiCalls(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)V
    .locals 2

    .line 243
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->redactionManager:Lcom/helpshift/redaction/RedactionManager;

    invoke-virtual {v0}, Lcom/helpshift/redaction/RedactionManager;->getRedactionState()Lcom/helpshift/redaction/RedactionState;

    move-result-object v0

    .line 244
    sget-object v1, Lcom/helpshift/redaction/RedactionState;->COMPLETED:Lcom/helpshift/redaction/RedactionState;

    if-eq v0, v1, :cond_0

    return-void

    .line 248
    :cond_0
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupDM$3;->$SwitchMap$com$helpshift$common$AutoRetryFailedEventDM$EventType:[I

    invoke-virtual {p1}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ordinal()I

    move-result p1

    aget p1, v0, p1

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 258
    :pswitch_0
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->remoteDataMigrator:Lcom/helpshift/migration/RemoteDataMigrator;

    invoke-virtual {p1}, Lcom/helpshift/migration/RemoteDataMigrator;->getProfileMigrationState()Lcom/helpshift/migration/MigrationState;

    move-result-object p1

    sget-object v0, Lcom/helpshift/migration/MigrationState;->COMPLETED:Lcom/helpshift/migration/MigrationState;

    if-ne p1, v0, :cond_1

    .line 259
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->userSyncDM:Lcom/helpshift/account/domainmodel/UserSyncDM;

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserSyncDM;->retry()V

    goto :goto_0

    .line 250
    :pswitch_1
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->remoteDataMigrator:Lcom/helpshift/migration/RemoteDataMigrator;

    invoke-virtual {p1}, Lcom/helpshift/migration/RemoteDataMigrator;->retry()V

    .line 252
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->remoteDataMigrator:Lcom/helpshift/migration/RemoteDataMigrator;

    invoke-virtual {p1}, Lcom/helpshift/migration/RemoteDataMigrator;->getProfileMigrationState()Lcom/helpshift/migration/MigrationState;

    move-result-object p1

    sget-object v0, Lcom/helpshift/migration/MigrationState;->COMPLETED:Lcom/helpshift/migration/MigrationState;

    if-ne p1, v0, :cond_1

    .line 253
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->userSyncDM:Lcom/helpshift/account/domainmodel/UserSyncDM;

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserSyncDM;->syncUser()V

    :cond_1
    :goto_0
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public startSetup()V
    .locals 2

    .line 107
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserSetupDM;->getState()Lcom/helpshift/account/domainmodel/UserSetupState;

    move-result-object v0

    .line 108
    sget-object v1, Lcom/helpshift/account/domainmodel/UserSetupState;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSetupState;

    if-eq v0, v1, :cond_2

    sget-object v1, Lcom/helpshift/account/domainmodel/UserSetupState;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSetupState;

    if-ne v0, v1, :cond_0

    goto :goto_0

    .line 113
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->redactionManager:Lcom/helpshift/redaction/RedactionManager;

    invoke-virtual {v0}, Lcom/helpshift/redaction/RedactionManager;->getRedactionState()Lcom/helpshift/redaction/RedactionState;

    move-result-object v0

    .line 114
    invoke-direct {p0, v0}, Lcom/helpshift/account/domainmodel/UserSetupDM;->onRedactionStateChange(Lcom/helpshift/redaction/RedactionState;)V

    .line 116
    sget-object v1, Lcom/helpshift/redaction/RedactionState;->PENDING:Lcom/helpshift/redaction/RedactionState;

    if-ne v0, v1, :cond_1

    .line 117
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM;->redactionManager:Lcom/helpshift/redaction/RedactionManager;

    invoke-virtual {v0}, Lcom/helpshift/redaction/RedactionManager;->executeRedaction()V

    :cond_1
    return-void

    :cond_2
    :goto_0
    return-void
.end method

.method public userSyncStateChanged(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserSyncStatus;Lcom/helpshift/account/domainmodel/UserSyncStatus;)V
    .locals 0

    .line 128
    invoke-direct {p0, p3}, Lcom/helpshift/account/domainmodel/UserSetupDM;->onUserSyncStateChange(Lcom/helpshift/account/domainmodel/UserSyncStatus;)V

    return-void
.end method
