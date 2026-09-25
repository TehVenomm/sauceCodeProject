.class public Lcom/helpshift/account/domainmodel/UserDM;
.super Ljava/util/Observable;
.source "UserDM.java"

# interfaces
.implements Lcom/helpshift/account/UserDMObserver;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/account/domainmodel/UserDM$Builder;
    }
.end annotation


# instance fields
.field private authToken:Ljava/lang/String;

.field private deviceId:Ljava/lang/String;

.field private email:Ljava/lang/String;

.field private identifier:Ljava/lang/String;

.field private isActiveUser:Z

.field private isAnonymousUser:Z

.field private isPushTokenSynced:Z

.field private issueExists:Z

.field private localId:Ljava/lang/Long;

.field private name:Ljava/lang/String;

.field private syncState:Lcom/helpshift/account/domainmodel/UserSyncStatus;


# direct methods
.method public constructor <init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZZLjava/lang/String;ZLcom/helpshift/account/domainmodel/UserSyncStatus;)V
    .locals 0

    .line 23
    invoke-direct {p0}, Ljava/util/Observable;-><init>()V

    .line 24
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserDM;->localId:Ljava/lang/Long;

    .line 25
    iput-object p2, p0, Lcom/helpshift/account/domainmodel/UserDM;->identifier:Ljava/lang/String;

    .line 26
    iput-object p3, p0, Lcom/helpshift/account/domainmodel/UserDM;->email:Ljava/lang/String;

    .line 27
    iput-object p4, p0, Lcom/helpshift/account/domainmodel/UserDM;->name:Ljava/lang/String;

    .line 28
    iput-object p5, p0, Lcom/helpshift/account/domainmodel/UserDM;->deviceId:Ljava/lang/String;

    .line 29
    iput-boolean p6, p0, Lcom/helpshift/account/domainmodel/UserDM;->isActiveUser:Z

    .line 30
    iput-boolean p7, p0, Lcom/helpshift/account/domainmodel/UserDM;->isAnonymousUser:Z

    .line 31
    iput-boolean p8, p0, Lcom/helpshift/account/domainmodel/UserDM;->isPushTokenSynced:Z

    .line 32
    iput-object p9, p0, Lcom/helpshift/account/domainmodel/UserDM;->authToken:Ljava/lang/String;

    .line 33
    iput-boolean p10, p0, Lcom/helpshift/account/domainmodel/UserDM;->issueExists:Z

    .line 34
    iput-object p11, p0, Lcom/helpshift/account/domainmodel/UserDM;->syncState:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/Long;
    .locals 0

    .line 7
    iget-object p0, p0, Lcom/helpshift/account/domainmodel/UserDM;->localId:Ljava/lang/Long;

    return-object p0
.end method

.method static synthetic access$100(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;
    .locals 0

    .line 7
    iget-object p0, p0, Lcom/helpshift/account/domainmodel/UserDM;->identifier:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$1000(Lcom/helpshift/account/domainmodel/UserDM;)Lcom/helpshift/account/domainmodel/UserSyncStatus;
    .locals 0

    .line 7
    iget-object p0, p0, Lcom/helpshift/account/domainmodel/UserDM;->syncState:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    return-object p0
.end method

.method static synthetic access$200(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;
    .locals 0

    .line 7
    iget-object p0, p0, Lcom/helpshift/account/domainmodel/UserDM;->email:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$300(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;
    .locals 0

    .line 7
    iget-object p0, p0, Lcom/helpshift/account/domainmodel/UserDM;->name:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$400(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;
    .locals 0

    .line 7
    iget-object p0, p0, Lcom/helpshift/account/domainmodel/UserDM;->deviceId:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$500(Lcom/helpshift/account/domainmodel/UserDM;)Z
    .locals 0

    .line 7
    iget-boolean p0, p0, Lcom/helpshift/account/domainmodel/UserDM;->isActiveUser:Z

    return p0
.end method

.method static synthetic access$600(Lcom/helpshift/account/domainmodel/UserDM;)Z
    .locals 0

    .line 7
    iget-boolean p0, p0, Lcom/helpshift/account/domainmodel/UserDM;->isAnonymousUser:Z

    return p0
.end method

.method static synthetic access$700(Lcom/helpshift/account/domainmodel/UserDM;)Z
    .locals 0

    .line 7
    iget-boolean p0, p0, Lcom/helpshift/account/domainmodel/UserDM;->isPushTokenSynced:Z

    return p0
.end method

.method static synthetic access$800(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;
    .locals 0

    .line 7
    iget-object p0, p0, Lcom/helpshift/account/domainmodel/UserDM;->authToken:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$900(Lcom/helpshift/account/domainmodel/UserDM;)Z
    .locals 0

    .line 7
    iget-boolean p0, p0, Lcom/helpshift/account/domainmodel/UserDM;->issueExists:Z

    return p0
.end method


# virtual methods
.method public getAuthToken()Ljava/lang/String;
    .locals 1

    .line 70
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM;->authToken:Ljava/lang/String;

    return-object v0
.end method

.method public getDeviceId()Ljava/lang/String;
    .locals 1

    .line 54
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM;->deviceId:Ljava/lang/String;

    return-object v0
.end method

.method public getEmail()Ljava/lang/String;
    .locals 1

    .line 46
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM;->email:Ljava/lang/String;

    return-object v0
.end method

.method public getIdentifier()Ljava/lang/String;
    .locals 1

    .line 42
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM;->identifier:Ljava/lang/String;

    return-object v0
.end method

.method public getLocalId()Ljava/lang/Long;
    .locals 1

    .line 38
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM;->localId:Ljava/lang/Long;

    return-object v0
.end method

.method public getName()Ljava/lang/String;
    .locals 1

    .line 50
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM;->name:Ljava/lang/String;

    return-object v0
.end method

.method public getSyncState()Lcom/helpshift/account/domainmodel/UserSyncStatus;
    .locals 1

    .line 78
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM;->syncState:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    return-object v0
.end method

.method public isActiveUser()Z
    .locals 1

    .line 58
    iget-boolean v0, p0, Lcom/helpshift/account/domainmodel/UserDM;->isActiveUser:Z

    return v0
.end method

.method public isAnonymousUser()Z
    .locals 1

    .line 62
    iget-boolean v0, p0, Lcom/helpshift/account/domainmodel/UserDM;->isAnonymousUser:Z

    return v0
.end method

.method public isPushTokenSynced()Z
    .locals 1

    .line 66
    iget-boolean v0, p0, Lcom/helpshift/account/domainmodel/UserDM;->isPushTokenSynced:Z

    return v0
.end method

.method public issueExists()Z
    .locals 1

    .line 74
    iget-boolean v0, p0, Lcom/helpshift/account/domainmodel/UserDM;->issueExists:Z

    return v0
.end method

.method public onUserDataChange(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 0

    .line 83
    invoke-virtual {p0, p1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 84
    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserDM;->issueExists()Z

    move-result p1

    iput-boolean p1, p0, Lcom/helpshift/account/domainmodel/UserDM;->issueExists:Z

    .line 85
    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserDM;->getAuthToken()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserDM;->authToken:Ljava/lang/String;

    .line 87
    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserDM;->getName()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserDM;->name:Ljava/lang/String;

    .line 88
    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserDM;->email:Ljava/lang/String;

    .line 89
    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserDM;->getSyncState()Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserDM;->syncState:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    .line 91
    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserDM;->isActiveUser()Z

    move-result p1

    iput-boolean p1, p0, Lcom/helpshift/account/domainmodel/UserDM;->isActiveUser:Z

    .line 92
    iget-boolean p1, p2, Lcom/helpshift/account/domainmodel/UserDM;->isPushTokenSynced:Z

    iput-boolean p1, p0, Lcom/helpshift/account/domainmodel/UserDM;->isPushTokenSynced:Z

    .line 95
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserDM;->setChanged()V

    .line 96
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserDM;->notifyObservers()V

    :cond_0
    return-void
.end method
