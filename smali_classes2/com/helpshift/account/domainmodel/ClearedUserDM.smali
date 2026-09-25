.class public Lcom/helpshift/account/domainmodel/ClearedUserDM;
.super Ljava/lang/Object;
.source "ClearedUserDM.java"


# instance fields
.field public final authToken:Ljava/lang/String;

.field public final deviceId:Ljava/lang/String;

.field public final email:Ljava/lang/String;

.field public final identifier:Ljava/lang/String;

.field public final localId:Ljava/lang/Long;

.field public final syncState:Lcom/helpshift/account/dao/ClearedUserSyncState;


# direct methods
.method public constructor <init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/account/dao/ClearedUserSyncState;)V
    .locals 0

    .line 15
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 16
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->localId:Ljava/lang/Long;

    .line 17
    iput-object p2, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->identifier:Ljava/lang/String;

    .line 18
    iput-object p3, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->email:Ljava/lang/String;

    .line 19
    iput-object p4, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->authToken:Ljava/lang/String;

    .line 20
    iput-object p5, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->deviceId:Ljava/lang/String;

    .line 21
    iput-object p6, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->syncState:Lcom/helpshift/account/dao/ClearedUserSyncState;

    return-void
.end method
