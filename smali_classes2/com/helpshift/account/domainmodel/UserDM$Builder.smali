.class public final Lcom/helpshift/account/domainmodel/UserDM$Builder;
.super Ljava/lang/Object;
.source "UserDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/account/domainmodel/UserDM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Builder"
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
.method public constructor <init>(Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 1

    .line 113
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 114
    invoke-static {p1}, Lcom/helpshift/account/domainmodel/UserDM;->access$000(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/Long;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->localId:Ljava/lang/Long;

    .line 115
    invoke-static {p1}, Lcom/helpshift/account/domainmodel/UserDM;->access$100(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->identifier:Ljava/lang/String;

    .line 116
    invoke-static {p1}, Lcom/helpshift/account/domainmodel/UserDM;->access$200(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->email:Ljava/lang/String;

    .line 117
    invoke-static {p1}, Lcom/helpshift/account/domainmodel/UserDM;->access$300(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->name:Ljava/lang/String;

    .line 118
    invoke-static {p1}, Lcom/helpshift/account/domainmodel/UserDM;->access$400(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->deviceId:Ljava/lang/String;

    .line 119
    invoke-static {p1}, Lcom/helpshift/account/domainmodel/UserDM;->access$500(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result v0

    iput-boolean v0, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->isActiveUser:Z

    .line 120
    invoke-static {p1}, Lcom/helpshift/account/domainmodel/UserDM;->access$600(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result v0

    iput-boolean v0, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->isAnonymousUser:Z

    .line 121
    invoke-static {p1}, Lcom/helpshift/account/domainmodel/UserDM;->access$700(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result v0

    iput-boolean v0, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->isPushTokenSynced:Z

    .line 122
    invoke-static {p1}, Lcom/helpshift/account/domainmodel/UserDM;->access$800(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->authToken:Ljava/lang/String;

    .line 123
    invoke-static {p1}, Lcom/helpshift/account/domainmodel/UserDM;->access$900(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result v0

    iput-boolean v0, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->issueExists:Z

    .line 124
    invoke-static {p1}, Lcom/helpshift/account/domainmodel/UserDM;->access$1000(Lcom/helpshift/account/domainmodel/UserDM;)Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->syncState:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    return-void
.end method


# virtual methods
.method public build()Lcom/helpshift/account/domainmodel/UserDM;
    .locals 13

    .line 163
    new-instance v12, Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->localId:Ljava/lang/Long;

    iget-object v2, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->identifier:Ljava/lang/String;

    iget-object v3, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->email:Ljava/lang/String;

    iget-object v4, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->name:Ljava/lang/String;

    iget-object v5, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->deviceId:Ljava/lang/String;

    iget-boolean v6, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->isActiveUser:Z

    iget-boolean v7, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->isAnonymousUser:Z

    iget-boolean v8, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->isPushTokenSynced:Z

    iget-object v9, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->authToken:Ljava/lang/String;

    iget-boolean v10, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->issueExists:Z

    iget-object v11, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->syncState:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-object v0, v12

    invoke-direct/range {v0 .. v11}, Lcom/helpshift/account/domainmodel/UserDM;-><init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZZLjava/lang/String;ZLcom/helpshift/account/domainmodel/UserSyncStatus;)V

    return-object v12
.end method

.method public setAuthToken(Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM$Builder;
    .locals 0

    .line 148
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->authToken:Ljava/lang/String;

    return-object p0
.end method

.method public setEmail(Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM$Builder;
    .locals 0

    .line 133
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->email:Ljava/lang/String;

    return-object p0
.end method

.method public setIsActiveUser(Z)Lcom/helpshift/account/domainmodel/UserDM$Builder;
    .locals 0

    .line 143
    iput-boolean p1, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->isActiveUser:Z

    return-object p0
.end method

.method public setIsPushTokenSynced(Z)Lcom/helpshift/account/domainmodel/UserDM$Builder;
    .locals 0

    .line 153
    iput-boolean p1, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->isPushTokenSynced:Z

    return-object p0
.end method

.method public setIssueExists(Z)Lcom/helpshift/account/domainmodel/UserDM$Builder;
    .locals 0

    .line 138
    iput-boolean p1, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->issueExists:Z

    return-object p0
.end method

.method public setName(Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM$Builder;
    .locals 0

    .line 128
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->name:Ljava/lang/String;

    return-object p0
.end method

.method public setSyncState(Lcom/helpshift/account/domainmodel/UserSyncStatus;)Lcom/helpshift/account/domainmodel/UserDM$Builder;
    .locals 0

    .line 158
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserDM$Builder;->syncState:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    return-object p0
.end method
