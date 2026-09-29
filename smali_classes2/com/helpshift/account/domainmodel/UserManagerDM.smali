.class public Lcom/helpshift/account/domainmodel/UserManagerDM;
.super Ljava/lang/Object;
.source "UserManagerDM.java"

# interfaces
.implements Lcom/helpshift/common/AutoRetriableDM;


# static fields
.field private static final ANONYMOUS_USER_ID_BACKUP_KEY:Ljava/lang/String; = "anonymous_user_id_backup_key"


# instance fields
.field private activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

.field private activeUserSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;

.field private backupDAO:Lcom/helpshift/common/dao/BackupDAO;

.field private clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

.field private device:Lcom/helpshift/common/platform/Device;

.field private domain:Lcom/helpshift/common/domain/Domain;

.field private platform:Lcom/helpshift/common/platform/Platform;

.field private userDAO:Lcom/helpshift/account/dao/UserDAO;

.field private userDMObservers:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Lcom/helpshift/account/UserDMObserver;",
            ">;"
        }
    .end annotation
.end field

.field private userManagerDAO:Lcom/helpshift/account/dao/UserManagerDAO;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;)V
    .locals 0

    .line 59
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 60
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 61
    iput-object p2, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/account/domainmodel/UserManagerDM;Lcom/helpshift/account/domainmodel/ClearedUserDM;)V
    .locals 0

    .line 45
    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearAnonymousUserInternal(Lcom/helpshift/account/domainmodel/ClearedUserDM;)V

    return-void
.end method

.method static synthetic access$100(Lcom/helpshift/account/domainmodel/UserManagerDM;)Lcom/helpshift/common/domain/Domain;
    .locals 0

    .line 45
    iget-object p0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    return-object p0
.end method

.method private declared-synchronized activateUser(Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 2

    monitor-enter p0

    if-nez p1, :cond_0

    .line 143
    monitor-exit p0

    return-void

    .line 147
    :cond_0
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v0

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v0, :cond_1

    .line 148
    monitor-exit p0

    return-void

    .line 152
    :cond_1
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-interface {v0, v1}, Lcom/helpshift/account/dao/UserDAO;->activateUser(Ljava/lang/Long;)Z

    move-result v0

    if-eqz v0, :cond_3

    .line 155
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    if-eqz v0, :cond_2

    .line 156
    new-instance v0, Lcom/helpshift/account/domainmodel/UserDM$Builder;

    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-direct {v0, v1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;-><init>(Lcom/helpshift/account/domainmodel/UserDM;)V

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->setIsActiveUser(Z)Lcom/helpshift/account/domainmodel/UserDM$Builder;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->build()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    .line 157
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-direct {p0, v1, v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->notifyUserDMObservers(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserDM;)V

    .line 161
    :cond_2
    new-instance v0, Lcom/helpshift/account/domainmodel/UserDM$Builder;

    invoke-direct {v0, p1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;-><init>(Lcom/helpshift/account/domainmodel/UserDM;)V

    const/4 p1, 0x1

    invoke-virtual {v0, p1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->setIsActiveUser(Z)Lcom/helpshift/account/domainmodel/UserDM$Builder;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->build()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    const/4 p1, 0x0

    .line 162
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;

    .line 163
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->addUserDMObserver(Lcom/helpshift/account/UserDMObserver;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 165
    :cond_3
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 141
    monitor-exit p0

    throw p1
.end method

.method private declared-synchronized addUserDMObserver(Lcom/helpshift/account/UserDMObserver;)V
    .locals 1

    monitor-enter p0

    if-nez p1, :cond_0

    .line 516
    monitor-exit p0

    return-void

    .line 519
    :cond_0
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDMObservers:Ljava/util/Set;

    if-nez v0, :cond_1

    .line 520
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDMObservers:Ljava/util/Set;

    .line 522
    :cond_1
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDMObservers:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 523
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 514
    monitor-exit p0

    throw p1
.end method

.method private buildCreateProfileNetwork()Lcom/helpshift/common/domain/network/Network;
    .locals 4

    .line 389
    new-instance v0, Lcom/helpshift/common/domain/network/POSTNetwork;

    const-string v1, "/profiles/"

    iget-object v2, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, v1, v2, v3}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 390
    new-instance v1, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;

    invoke-direct {v1, v0}, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 391
    new-instance v0, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v2, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 392
    new-instance v1, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v1, v0}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    return-object v1
.end method

.method private declared-synchronized buildUser(Lcom/helpshift/HelpshiftUser;)Lcom/helpshift/account/domainmodel/UserDM;
    .locals 13

    monitor-enter p0

    .line 169
    :try_start_0
    new-instance v12, Lcom/helpshift/account/domainmodel/UserDM;

    const/4 v1, 0x0

    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getIdentifier()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getEmail()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getName()Ljava/lang/String;

    move-result-object v4

    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->device:Lcom/helpshift/common/platform/Device;

    .line 170
    invoke-interface {v0}, Lcom/helpshift/common/platform/Device;->getDeviceId()Ljava/lang/String;

    move-result-object v5

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    .line 172
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getAuthToken()Ljava/lang/String;

    move-result-object v9

    const/4 v10, 0x1

    sget-object v11, Lcom/helpshift/account/domainmodel/UserSyncStatus;->NOT_STARTED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-object v0, v12

    invoke-direct/range {v0 .. v11}, Lcom/helpshift/account/domainmodel/UserDM;-><init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZZLjava/lang/String;ZLcom/helpshift/account/domainmodel/UserSyncStatus;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 169
    monitor-exit p0

    return-object v12

    :catchall_0
    move-exception p1

    .line 168
    monitor-exit p0

    throw p1
.end method

.method private clearAnonymousUserInternal(Lcom/helpshift/account/domainmodel/ClearedUserDM;)V
    .locals 5

    if-eqz p1, :cond_5

    .line 671
    iget-object v0, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->localId:Ljava/lang/Long;

    if-nez v0, :cond_0

    goto/16 :goto_3

    .line 675
    :cond_0
    iget-object v0, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->syncState:Lcom/helpshift/account/dao/ClearedUserSyncState;

    sget-object v1, Lcom/helpshift/account/dao/ClearedUserSyncState;->COMPLETED:Lcom/helpshift/account/dao/ClearedUserSyncState;

    if-eq v0, v1, :cond_4

    iget-object v0, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->syncState:Lcom/helpshift/account/dao/ClearedUserSyncState;

    sget-object v1, Lcom/helpshift/account/dao/ClearedUserSyncState;->IN_PROGRESS:Lcom/helpshift/account/dao/ClearedUserSyncState;

    if-ne v0, v1, :cond_1

    goto :goto_2

    .line 681
    :cond_1
    new-instance v0, Lcom/helpshift/common/domain/network/PUTNetwork;

    const-string v1, "/clear-profile/"

    iget-object v2, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, v1, v2, v3}, Lcom/helpshift/common/domain/network/PUTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 682
    new-instance v1, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v2, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v1, v0, v2}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 683
    new-instance v0, Lcom/helpshift/common/domain/network/UserNotFoundNetwork;

    invoke-direct {v0, v1}, Lcom/helpshift/common/domain/network/UserNotFoundNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 684
    new-instance v1, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;

    invoke-direct {v1, v0}, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 685
    new-instance v0, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v0, v1}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 688
    invoke-static {p1}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/ClearedUserDM;)Ljava/util/HashMap;

    move-result-object v1

    .line 691
    iget-object v2, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

    iget-object v3, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->localId:Ljava/lang/Long;

    sget-object v4, Lcom/helpshift/account/dao/ClearedUserSyncState;->IN_PROGRESS:Lcom/helpshift/account/dao/ClearedUserSyncState;

    invoke-interface {v2, v3, v4}, Lcom/helpshift/account/dao/ClearedUserDAO;->updateSyncState(Ljava/lang/Long;Lcom/helpshift/account/dao/ClearedUserSyncState;)Z

    .line 693
    :try_start_0
    new-instance v2, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {v2, v1}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    invoke-interface {v0, v2}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    .line 695
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

    iget-object v1, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->localId:Ljava/lang/Long;

    sget-object v2, Lcom/helpshift/account/dao/ClearedUserSyncState;->COMPLETED:Lcom/helpshift/account/dao/ClearedUserSyncState;

    invoke-interface {v0, v1, v2}, Lcom/helpshift/account/dao/ClearedUserDAO;->updateSyncState(Ljava/lang/Long;Lcom/helpshift/account/dao/ClearedUserSyncState;)Z

    .line 697
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

    iget-object v1, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->localId:Ljava/lang/Long;

    invoke-interface {v0, v1}, Lcom/helpshift/account/dao/ClearedUserDAO;->deleteClearedUser(Ljava/lang/Long;)Z
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v0

    .line 705
    iget-object v1, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v2, Lcom/helpshift/common/exception/NetworkException;->USER_NOT_FOUND:Lcom/helpshift/common/exception/NetworkException;

    if-eq v1, v2, :cond_3

    iget-object v1, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v2, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-ne v1, v2, :cond_2

    goto :goto_0

    .line 714
    :cond_2
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

    iget-object p1, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->localId:Ljava/lang/Long;

    sget-object v2, Lcom/helpshift/account/dao/ClearedUserSyncState;->FAILED:Lcom/helpshift/account/dao/ClearedUserSyncState;

    invoke-interface {v1, p1, v2}, Lcom/helpshift/account/dao/ClearedUserDAO;->updateSyncState(Ljava/lang/Long;Lcom/helpshift/account/dao/ClearedUserSyncState;)Z

    .line 715
    throw v0

    .line 708
    :cond_3
    :goto_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

    iget-object v1, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->localId:Ljava/lang/Long;

    sget-object v2, Lcom/helpshift/account/dao/ClearedUserSyncState;->COMPLETED:Lcom/helpshift/account/dao/ClearedUserSyncState;

    invoke-interface {v0, v1, v2}, Lcom/helpshift/account/dao/ClearedUserDAO;->updateSyncState(Ljava/lang/Long;Lcom/helpshift/account/dao/ClearedUserSyncState;)Z

    .line 710
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

    iget-object p1, p1, Lcom/helpshift/account/domainmodel/ClearedUserDM;->localId:Ljava/lang/Long;

    invoke-interface {v0, p1}, Lcom/helpshift/account/dao/ClearedUserDAO;->deleteClearedUser(Ljava/lang/Long;)Z

    :goto_1
    return-void

    :cond_4
    :goto_2
    return-void

    :cond_5
    :goto_3
    return-void
.end method

.method private declared-synchronized generateAnonymousUserId()Ljava/lang/String;
    .locals 5

    monitor-enter p0

    const/4 v0, 0x0

    .line 178
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->backupDAO:Lcom/helpshift/common/dao/BackupDAO;

    const-string v2, "anonymous_user_id_backup_key"

    invoke-interface {v1, v2}, Lcom/helpshift/common/dao/BackupDAO;->getValue(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v1

    .line 179
    instance-of v2, v1, Ljava/lang/String;

    if-eqz v2, :cond_0

    .line 180
    move-object v0, v1

    check-cast v0, Ljava/lang/String;

    .line 184
    :cond_0
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 185
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    .line 186
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "hsft_anon_"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v3, Lcom/helpshift/util/HSFormat;->timeStampAnonymousUserFormat:Lcom/helpshift/common/util/HSSimpleDateFormat;

    new-instance v4, Ljava/util/Date;

    invoke-direct {v4, v0, v1}, Ljava/util/Date;-><init>(J)V

    .line 187
    invoke-virtual {v3, v4}, Lcom/helpshift/common/util/HSSimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "-"

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "abcdefghijklmnopqrstuvwxyz0123456789"

    .line 188
    invoke-virtual {v0}, Ljava/lang/String;->toCharArray()[C

    move-result-object v0

    const/16 v1, 0xf

    invoke-static {v0, v1}, Lcom/helpshift/common/StringUtils;->generateRandomString([CI)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 189
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->backupDAO:Lcom/helpshift/common/dao/BackupDAO;

    const-string v2, "anonymous_user_id_backup_key"

    invoke-interface {v1, v2, v0}, Lcom/helpshift/common/dao/BackupDAO;->storeValue(Ljava/lang/String;Ljava/io/Serializable;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 192
    :cond_1
    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 175
    monitor-exit p0

    throw v0
.end method

.method private declared-synchronized notifyUserDMObservers(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 2

    monitor-enter p0

    .line 526
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDMObservers:Ljava/util/Set;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v0, :cond_0

    .line 527
    monitor-exit p0

    return-void

    .line 529
    :cond_0
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDMObservers:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/account/UserDMObserver;

    .line 530
    invoke-interface {v1, p1, p2}, Lcom/helpshift/account/UserDMObserver;->onUserDataChange(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserDM;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    .line 532
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 525
    monitor-exit p0

    throw p1
.end method

.method private sendPushTokenInternal()V
    .locals 6

    .line 339
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Device;->getPushToken()Ljava/lang/String;

    move-result-object v0

    .line 340
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    .line 348
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_4

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->isPushTokenSynced()Z

    move-result v2

    if-nez v2, :cond_4

    .line 349
    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->issueExists()Z

    move-result v2

    if-eqz v2, :cond_4

    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUserSetupDM()Lcom/helpshift/account/domainmodel/UserSetupDM;

    move-result-object v2

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserSetupDM;->getState()Lcom/helpshift/account/domainmodel/UserSetupState;

    move-result-object v2

    sget-object v3, Lcom/helpshift/account/domainmodel/UserSetupState;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSetupState;

    if-eq v2, v3, :cond_0

    goto :goto_1

    .line 353
    :cond_0
    invoke-static {v1}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object v2

    const-string v3, "token"

    .line 354
    invoke-interface {v2, v3, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 356
    new-instance v0, Lcom/helpshift/common/domain/network/POSTNetwork;

    const-string v3, "/update-push-token/"

    iget-object v4, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v5, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, v3, v4, v5}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 357
    new-instance v3, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v4, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v3, v0, v4}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 358
    new-instance v0, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;

    invoke-direct {v0, v3}, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 359
    new-instance v3, Lcom/helpshift/common/domain/network/UserNotFoundNetwork;

    invoke-direct {v3, v0}, Lcom/helpshift/common/domain/network/UserNotFoundNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 360
    new-instance v0, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;

    invoke-direct {v0, v3}, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 361
    new-instance v3, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v3, v0}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    const/4 v0, 0x1

    .line 364
    :try_start_0
    new-instance v4, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {v4, v2}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    invoke-interface {v3, v4}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    .line 365
    invoke-direct {p0, v1, v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->updateIsPushTokenSynced(Lcom/helpshift/account/domainmodel/UserDM;Z)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 368
    iget-object v3, v2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v4, Lcom/helpshift/common/exception/NetworkException;->USER_NOT_FOUND:Lcom/helpshift/common/exception/NetworkException;

    if-ne v3, v4, :cond_1

    goto :goto_0

    .line 371
    :cond_1
    iget-object v3, v2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v4, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-eq v3, v4, :cond_3

    iget-object v3, v2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v4, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-eq v3, v4, :cond_3

    .line 377
    iget-object v3, v2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v4, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-ne v3, v4, :cond_2

    .line 379
    invoke-direct {p0, v1, v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->updateIsPushTokenSynced(Lcom/helpshift/account/domainmodel/UserDM;Z)V

    :goto_0
    return-void

    .line 383
    :cond_2
    throw v2

    .line 373
    :cond_3
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object v0

    iget-object v3, v2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-virtual {v0, v1, v3}, Lcom/helpshift/account/AuthenticationFailureDM;->notifyAuthenticationFailure(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/common/exception/ExceptionType;)V

    .line 375
    throw v2

    :cond_4
    :goto_1
    return-void
.end method

.method private updateAnonymousIDInBackupDAO()V
    .locals 3

    .line 200
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 203
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->backupDAO:Lcom/helpshift/common/dao/BackupDAO;

    const-string v2, "anonymous_user_id_backup_key"

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v0

    invoke-interface {v1, v2, v0}, Lcom/helpshift/common/dao/BackupDAO;->storeValue(Ljava/lang/String;Ljava/io/Serializable;)V

    :cond_0
    return-void
.end method

.method private declared-synchronized updateIsPushTokenSynced(Lcom/helpshift/account/domainmodel/UserDM;Z)V
    .locals 1

    monitor-enter p0

    .line 500
    :try_start_0
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->isPushTokenSynced()Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-ne v0, p2, :cond_0

    .line 501
    monitor-exit p0

    return-void

    .line 504
    :cond_0
    :try_start_1
    new-instance v0, Lcom/helpshift/account/domainmodel/UserDM$Builder;

    invoke-direct {v0, p1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;-><init>(Lcom/helpshift/account/domainmodel/UserDM;)V

    .line 505
    invoke-virtual {v0, p2}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->setIsPushTokenSynced(Z)Lcom/helpshift/account/domainmodel/UserDM$Builder;

    move-result-object p2

    .line 506
    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->build()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p2

    .line 509
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v0, p2}, Lcom/helpshift/account/dao/UserDAO;->updateUser(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 510
    invoke-direct {p0, p1, p2}, Lcom/helpshift/account/domainmodel/UserManagerDM;->notifyUserDMObservers(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserDM;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 512
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 499
    monitor-exit p0

    throw p1
.end method


# virtual methods
.method public clearAnonymousUser(Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 8

    .line 643
    new-instance v7, Lcom/helpshift/account/domainmodel/ClearedUserDM;

    .line 644
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v2

    .line 645
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getAuthToken()Ljava/lang/String;

    move-result-object v4

    .line 646
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getDeviceId()Ljava/lang/String;

    move-result-object v5

    sget-object v6, Lcom/helpshift/account/dao/ClearedUserSyncState;->NOT_STARTED:Lcom/helpshift/account/dao/ClearedUserSyncState;

    const/4 v1, 0x0

    move-object v0, v7

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/account/domainmodel/ClearedUserDM;-><init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/account/dao/ClearedUserSyncState;)V

    .line 648
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

    invoke-interface {p1, v7}, Lcom/helpshift/account/dao/ClearedUserDAO;->insertClearedUser(Lcom/helpshift/account/domainmodel/ClearedUserDM;)Lcom/helpshift/account/domainmodel/ClearedUserDM;

    move-result-object p1

    .line 651
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/account/domainmodel/UserManagerDM$2;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/account/domainmodel/UserManagerDM$2;-><init>(Lcom/helpshift/account/domainmodel/UserManagerDM;Lcom/helpshift/account/domainmodel/ClearedUserDM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public declared-synchronized createAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;
    .locals 13

    monitor-enter p0

    .line 130
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->generateAnonymousUserId()Ljava/lang/String;

    move-result-object v2

    .line 132
    new-instance v12, Lcom/helpshift/account/domainmodel/UserDM;

    const/4 v1, 0x0

    const/4 v3, 0x0

    const/4 v4, 0x0

    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->device:Lcom/helpshift/common/platform/Device;

    .line 133
    invoke-interface {v0}, Lcom/helpshift/common/platform/Device;->getDeviceId()Ljava/lang/String;

    move-result-object v5

    const/4 v6, 0x0

    const/4 v7, 0x1

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x1

    sget-object v11, Lcom/helpshift/account/domainmodel/UserSyncStatus;->NOT_STARTED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-object v0, v12

    invoke-direct/range {v0 .. v11}, Lcom/helpshift/account/domainmodel/UserDM;-><init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZZLjava/lang/String;ZLcom/helpshift/account/domainmodel/UserSyncStatus;)V

    .line 137
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v0, v12}, Lcom/helpshift/account/dao/UserDAO;->createUser(Lcom/helpshift/account/domainmodel/UserDM;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 138
    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 129
    monitor-exit p0

    throw v0
.end method

.method public deleteUser(Lcom/helpshift/account/domainmodel/UserDM;)Z
    .locals 3

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return p1

    .line 224
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-interface {v0, v1}, Lcom/helpshift/account/dao/UserDAO;->deleteUser(Ljava/lang/Long;)Z

    move-result v0

    if-eqz v0, :cond_3

    .line 226
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->isAnonymousUser()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 227
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->backupDAO:Lcom/helpshift/common/dao/BackupDAO;

    const-string v2, "anonymous_user_id_backup_key"

    invoke-interface {v1, v2}, Lcom/helpshift/common/dao/BackupDAO;->removeKey(Ljava/lang/String;)V

    .line 229
    :cond_1
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    if-eqz v1, :cond_3

    .line 231
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    if-eqz v1, :cond_3

    .line 232
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_3

    .line 234
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDMObservers:Ljava/util/Set;

    if-eqz p1, :cond_2

    .line 235
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDMObservers:Ljava/util/Set;

    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-interface {p1, v1}, Ljava/util/Set;->remove(Ljava/lang/Object;)Z

    :cond_2
    const/4 p1, 0x0

    .line 238
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 239
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;

    :cond_3
    return v0
.end method

.method public declared-synchronized destroyUserSetupDM()V
    .locals 1

    monitor-enter p0

    const/4 v0, 0x0

    .line 638
    :try_start_0
    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 639
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 637
    monitor-exit p0

    throw v0
.end method

.method public getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;
    .locals 1

    .line 81
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    if-eqz v0, :cond_0

    .line 82
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    return-object v0

    .line 86
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v0}, Lcom/helpshift/account/dao/UserDAO;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 89
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    if-nez v0, :cond_1

    .line 90
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->loginWithAnonymousUser()Z

    goto :goto_0

    .line 93
    :cond_1
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-direct {p0, v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->addUserDMObserver(Lcom/helpshift/account/UserDMObserver;)V

    const/4 v0, 0x0

    .line 94
    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;

    .line 96
    :goto_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    return-object v0
.end method

.method public declared-synchronized getActiveUserSetupDM()Lcom/helpshift/account/domainmodel/UserSetupDM;
    .locals 7

    monitor-enter p0

    .line 624
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;

    if-nez v0, :cond_0

    .line 625
    new-instance v0, Lcom/helpshift/account/domainmodel/UserSetupDM;

    iget-object v2, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v3, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 626
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v4

    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 627
    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v1

    .line 628
    invoke-virtual {v1}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;->getActiveConversationInboxDM()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v6

    move-object v1, v0

    move-object v5, p0

    invoke-direct/range {v1 .. v6}, Lcom/helpshift/account/domainmodel/UserSetupDM;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserManagerDM;Lcom/helpshift/account/domainmodel/IUserSyncExecutor;)V

    .line 630
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserSetupDM;->init()V

    .line 632
    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;

    .line 634
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 623
    monitor-exit p0

    throw v0
.end method

.method public getAllUsers()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/account/domainmodel/UserDM;",
            ">;"
        }
    .end annotation

    .line 331
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v0}, Lcom/helpshift/account/dao/UserDAO;->fetchUsers()Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method public getAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;
    .locals 1

    .line 210
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->isAnonymousUser()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 211
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    goto :goto_0

    .line 214
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v0}, Lcom/helpshift/account/dao/UserDAO;->getAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    :goto_0
    return-object v0
.end method

.method public getInactiveLoggedInUsers()Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/account/domainmodel/UserDM;",
            ">;"
        }
    .end annotation

    .line 581
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v0}, Lcom/helpshift/account/dao/UserDAO;->fetchUsers()Ljava/util/List;

    move-result-object v0

    .line 582
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 584
    invoke-static {v0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v2

    if-eqz v2, :cond_0

    return-object v1

    .line 588
    :cond_0
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/account/domainmodel/UserDM;

    .line 589
    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->isAnonymousUser()Z

    move-result v3

    if-nez v3, :cond_1

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->isActiveUser()Z

    move-result v3

    if-nez v3, :cond_1

    .line 590
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_2
    return-object v1
.end method

.method public getUserMetaIdentifier()Ljava/lang/String;
    .locals 2

    .line 313
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    .line 318
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->isAnonymousUser()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 319
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userManagerDAO:Lcom/helpshift/account/dao/UserManagerDAO;

    invoke-interface {v0}, Lcom/helpshift/account/dao/UserManagerDAO;->getUserMetaIdentifier()Ljava/lang/String;

    move-result-object v0

    return-object v0

    .line 322
    :cond_0
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public init()V
    .locals 2

    .line 68
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->device:Lcom/helpshift/common/platform/Device;

    .line 69
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getUserDAO()Lcom/helpshift/account/dao/UserDAO;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    .line 70
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getUserManagerDAO()Lcom/helpshift/account/dao/UserManagerDAO;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userManagerDAO:Lcom/helpshift/account/dao/UserManagerDAO;

    .line 71
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getBackupDAO()Lcom/helpshift/common/dao/BackupDAO;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->backupDAO:Lcom/helpshift/common/dao/BackupDAO;

    .line 72
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getClearedUserDAO()Lcom/helpshift/account/dao/ClearedUserDAO;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

    .line 74
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v0

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->PUSH_TOKEN:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v0, v1, p0}, Lcom/helpshift/common/AutoRetryFailedEventDM;->register(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;Lcom/helpshift/common/AutoRetriableDM;)V

    .line 75
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v0

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->CLEAR_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v0, v1, p0}, Lcom/helpshift/common/AutoRetryFailedEventDM;->register(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;Lcom/helpshift/common/AutoRetriableDM;)V

    .line 76
    invoke-direct {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->updateAnonymousIDInBackupDAO()V

    return-void
.end method

.method public isActiveUser(Lcom/helpshift/HelpshiftUser;)Z
    .locals 4

    .line 536
    invoke-static {p1}, Lcom/helpshift/common/HelpshiftUtils;->isValidHelpshiftUser(Lcom/helpshift/HelpshiftUser;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 540
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    if-nez v0, :cond_1

    .line 542
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v0}, Lcom/helpshift/account/dao/UserDAO;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    :cond_1
    if-nez v0, :cond_2

    return v1

    .line 560
    :cond_2
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getIdentifier()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmptyWithoutTrim(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_3

    .line 561
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmptyWithoutTrim(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_5

    .line 563
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getEmail()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    goto :goto_0

    .line 566
    :cond_3
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getEmail()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmptyWithoutTrim(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_4

    .line 567
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmptyWithoutTrim(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_5

    .line 569
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getIdentifier()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    goto :goto_0

    .line 574
    :cond_4
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getIdentifier()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_5

    .line 575
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getEmail()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_5

    const/4 p1, 0x1

    const/4 v1, 0x1

    :cond_5
    :goto_0
    return v1
.end method

.method public declared-synchronized login(Lcom/helpshift/HelpshiftUser;)V
    .locals 3

    monitor-enter p0

    .line 103
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getIdentifier()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getEmail()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Lcom/helpshift/account/dao/UserDAO;->fetchUser(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    if-nez v0, :cond_0

    .line 108
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-direct {p0, p1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->buildUser(Lcom/helpshift/HelpshiftUser;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p1

    invoke-interface {v0, p1}, Lcom/helpshift/account/dao/UserDAO;->createUser(Lcom/helpshift/account/domainmodel/UserDM;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    :cond_0
    if-eqz v0, :cond_1

    .line 112
    invoke-direct {p0, v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->addUserDMObserver(Lcom/helpshift/account/UserDMObserver;)V

    .line 114
    invoke-direct {p0, v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->activateUser(Lcom/helpshift/account/domainmodel/UserDM;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 116
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 102
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized loginWithAnonymousUser()Z
    .locals 1

    monitor-enter p0

    .line 120
    :try_start_0
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    if-nez v0, :cond_0

    .line 123
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->createAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    .line 125
    :cond_0
    invoke-direct {p0, v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->activateUser(Lcom/helpshift/account/domainmodel/UserDM;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const/4 v0, 0x1

    .line 126
    monitor-exit p0

    return v0

    :catchall_0
    move-exception v0

    .line 119
    monitor-exit p0

    throw v0
.end method

.method public registerUserWithServer(Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 3

    .line 253
    invoke-static {p1}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object v0

    const-string v1, "name"

    .line 254
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 257
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->buildCreateProfileNetwork()Lcom/helpshift/common/domain/network/Network;

    move-result-object v1

    new-instance v2, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {v2, v0}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    invoke-interface {v1, v2}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception v0

    .line 260
    iget-object v1, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v2, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-eq v1, v2, :cond_0

    iget-object v1, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v2, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-ne v1, v2, :cond_1

    .line 262
    :cond_0
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object v1

    iget-object v2, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-virtual {v1, p1, v2}, Lcom/helpshift/account/AuthenticationFailureDM;->notifyAuthenticationFailure(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/common/exception/ExceptionType;)V

    .line 264
    :cond_1
    throw v0
.end method

.method public resetNameAndEmail(Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 2

    .line 721
    new-instance v0, Lcom/helpshift/account/domainmodel/UserDM$Builder;

    invoke-direct {v0, p1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;-><init>(Lcom/helpshift/account/domainmodel/UserDM;)V

    const/4 v1, 0x0

    .line 722
    invoke-virtual {v0, v1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->setEmail(Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM$Builder;

    move-result-object v0

    .line 723
    invoke-virtual {v0, v1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->setName(Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM$Builder;

    move-result-object v0

    .line 724
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->build()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    .line 726
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v1, v0}, Lcom/helpshift/account/dao/UserDAO;->updateUser(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 727
    invoke-direct {p0, p1, v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->notifyUserDMObservers(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserDM;)V

    :cond_0
    return-void
.end method

.method public declared-synchronized resetPushTokenSyncStatusForUsers()V
    .locals 5

    monitor-enter p0

    .line 272
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v0}, Lcom/helpshift/account/dao/UserDAO;->fetchUsers()Ljava/util/List;

    move-result-object v0

    .line 273
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/account/domainmodel/UserDM;

    .line 278
    iget-object v2, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    const/4 v3, 0x0

    if-eqz v2, :cond_0

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    iget-object v4, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v4}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v4

    invoke-virtual {v2, v4}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    .line 279
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->activeUserDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-direct {p0, v1, v3}, Lcom/helpshift/account/domainmodel/UserManagerDM;->updateIsPushTokenSynced(Lcom/helpshift/account/domainmodel/UserDM;Z)V

    goto :goto_0

    .line 282
    :cond_0
    invoke-direct {p0, v1, v3}, Lcom/helpshift/account/domainmodel/UserManagerDM;->updateIsPushTokenSynced(Lcom/helpshift/account/domainmodel/UserDM;Z)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    .line 285
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 271
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized resetSyncState(Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 1

    monitor-enter p0

    .line 490
    :try_start_0
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->NOT_STARTED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    invoke-virtual {p0, p1, v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->updateSyncState(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserSyncStatus;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 491
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 489
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized resetSyncStateForAllUsers()V
    .locals 2

    monitor-enter p0

    .line 475
    :try_start_0
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getAllUsers()Ljava/util/List;

    move-result-object v0

    .line 476
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/account/domainmodel/UserDM;

    .line 477
    invoke-virtual {p0, v1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->resetSyncState(Lcom/helpshift/account/domainmodel/UserDM;)V

    goto :goto_0

    .line 480
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->destroyUserSetupDM()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 481
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 474
    monitor-exit p0

    throw v0
.end method

.method public sendFailedApiCalls(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)V
    .locals 3

    .line 598
    sget-object v0, Lcom/helpshift/account/domainmodel/UserManagerDM$3;->$SwitchMap$com$helpshift$common$AutoRetryFailedEventDM$EventType:[I

    invoke-virtual {p1}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ordinal()I

    move-result p1

    aget p1, v0, p1

    packed-switch p1, :pswitch_data_0

    goto :goto_1

    .line 605
    :pswitch_0
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

    invoke-interface {p1}, Lcom/helpshift/account/dao/ClearedUserDAO;->fetchClearedUsers()Ljava/util/List;

    move-result-object p1

    .line 606
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 609
    :cond_0
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/account/domainmodel/ClearedUserDM;

    .line 611
    iget-object v1, v0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->syncState:Lcom/helpshift/account/dao/ClearedUserSyncState;

    sget-object v2, Lcom/helpshift/account/dao/ClearedUserSyncState;->COMPLETED:Lcom/helpshift/account/dao/ClearedUserSyncState;

    if-ne v1, v2, :cond_1

    .line 612
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

    iget-object v0, v0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->localId:Ljava/lang/Long;

    invoke-interface {v1, v0}, Lcom/helpshift/account/dao/ClearedUserDAO;->deleteClearedUser(Ljava/lang/Long;)Z

    goto :goto_0

    .line 616
    :cond_1
    invoke-direct {p0, v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearAnonymousUserInternal(Lcom/helpshift/account/domainmodel/ClearedUserDM;)V

    goto :goto_0

    .line 601
    :pswitch_1
    invoke-direct {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->sendPushTokenInternal()V

    :cond_2
    :goto_1
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public declared-synchronized sendPushToken()V
    .locals 2

    monitor-enter p0

    .line 300
    :try_start_0
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUserSetupDM()Lcom/helpshift/account/domainmodel/UserSetupDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserSetupDM;->getState()Lcom/helpshift/account/domainmodel/UserSetupState;

    move-result-object v0

    sget-object v1, Lcom/helpshift/account/domainmodel/UserSetupState;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSetupState;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eq v0, v1, :cond_0

    .line 301
    monitor-exit p0

    return-void

    .line 304
    :cond_0
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/account/domainmodel/UserManagerDM$1;

    invoke-direct {v1, p0}, Lcom/helpshift/account/domainmodel/UserManagerDM$1;-><init>(Lcom/helpshift/account/domainmodel/UserManagerDM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 310
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 299
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized sendPushTokenSync()V
    .locals 4

    monitor-enter p0

    .line 289
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->sendPushTokenInternal()V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 296
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 293
    :try_start_1
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v1

    sget-object v2, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->PUSH_TOKEN:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v0}, Lcom/helpshift/common/exception/RootAPIException;->getServerStatusCode()I

    move-result v3

    invoke-virtual {v1, v2, v3}, Lcom/helpshift/common/AutoRetryFailedEventDM;->scheduleRetryTaskForEventType(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;I)V

    .line 294
    throw v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 288
    :goto_0
    monitor-exit p0

    throw v0
.end method

.method public setUserMetaIdentifier(Ljava/lang/String;)V
    .locals 1

    .line 327
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userManagerDAO:Lcom/helpshift/account/dao/UserManagerDAO;

    invoke-interface {v0, p1}, Lcom/helpshift/account/dao/UserManagerDAO;->setUserMetaIdentifier(Ljava/lang/String;)V

    return-void
.end method

.method public declared-synchronized updateAuthToken(Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/String;)V
    .locals 1

    monitor-enter p0

    .line 423
    :try_start_0
    new-instance v0, Lcom/helpshift/account/domainmodel/UserDM$Builder;

    invoke-direct {v0, p1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;-><init>(Lcom/helpshift/account/domainmodel/UserDM;)V

    .line 424
    invoke-virtual {v0, p2}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->setAuthToken(Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM$Builder;

    move-result-object p2

    .line 425
    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->build()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p2

    .line 428
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v0, p2}, Lcom/helpshift/account/dao/UserDAO;->updateUser(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 429
    invoke-direct {p0, p1, p2}, Lcom/helpshift/account/domainmodel/UserManagerDM;->notifyUserDMObservers(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserDM;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 431
    :cond_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 422
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized updateIssueExists(Lcom/helpshift/account/domainmodel/UserDM;Z)V
    .locals 1

    monitor-enter p0

    .line 402
    :try_start_0
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->issueExists()Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-ne v0, p2, :cond_0

    .line 403
    monitor-exit p0

    return-void

    .line 406
    :cond_0
    :try_start_1
    new-instance v0, Lcom/helpshift/account/domainmodel/UserDM$Builder;

    invoke-direct {v0, p1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;-><init>(Lcom/helpshift/account/domainmodel/UserDM;)V

    .line 407
    invoke-virtual {v0, p2}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->setIssueExists(Z)Lcom/helpshift/account/domainmodel/UserDM$Builder;

    move-result-object p2

    .line 408
    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->build()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p2

    .line 411
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v0, p2}, Lcom/helpshift/account/dao/UserDAO;->updateUser(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 412
    invoke-direct {p0, p1, p2}, Lcom/helpshift/account/domainmodel/UserManagerDM;->notifyUserDMObservers(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserDM;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 414
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 401
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized updateSyncState(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserSyncStatus;)V
    .locals 1

    monitor-enter p0

    .line 457
    :try_start_0
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getSyncState()Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-ne v0, p2, :cond_0

    .line 458
    monitor-exit p0

    return-void

    .line 461
    :cond_0
    :try_start_1
    new-instance v0, Lcom/helpshift/account/domainmodel/UserDM$Builder;

    invoke-direct {v0, p1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;-><init>(Lcom/helpshift/account/domainmodel/UserDM;)V

    .line 462
    invoke-virtual {v0, p2}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->setSyncState(Lcom/helpshift/account/domainmodel/UserSyncStatus;)Lcom/helpshift/account/domainmodel/UserDM$Builder;

    move-result-object p2

    .line 463
    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->build()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p2

    .line 466
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v0, p2}, Lcom/helpshift/account/dao/UserDAO;->updateUser(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 467
    invoke-direct {p0, p1, p2}, Lcom/helpshift/account/domainmodel/UserManagerDM;->notifyUserDMObservers(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserDM;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 469
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 456
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized updateUserName(Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/String;)V
    .locals 1

    monitor-enter p0

    .line 440
    :try_start_0
    new-instance v0, Lcom/helpshift/account/domainmodel/UserDM$Builder;

    invoke-direct {v0, p1}, Lcom/helpshift/account/domainmodel/UserDM$Builder;-><init>(Lcom/helpshift/account/domainmodel/UserDM;)V

    .line 441
    invoke-virtual {v0, p2}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->setName(Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM$Builder;

    move-result-object p2

    .line 442
    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserDM$Builder;->build()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p2

    .line 445
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    invoke-interface {v0, p2}, Lcom/helpshift/account/dao/UserDAO;->updateUser(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 446
    invoke-direct {p0, p1, p2}, Lcom/helpshift/account/domainmodel/UserManagerDM;->notifyUserDMObservers(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserDM;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 448
    :cond_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 439
    monitor-exit p0

    throw p1
.end method
