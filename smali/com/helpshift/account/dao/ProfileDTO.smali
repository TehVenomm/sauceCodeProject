.class public Lcom/helpshift/account/dao/ProfileDTO;
.super Ljava/lang/Object;
.source "ProfileDTO.java"

# interfaces
.implements Ljava/io/Serializable;


# instance fields
.field public final did:Ljava/lang/String;

.field public final email:Ljava/lang/String;

.field public final identifier:Ljava/lang/String;

.field public final isPushTokenSynced:Z

.field public final localId:Ljava/lang/Long;

.field public final name:Ljava/lang/String;

.field public final saltedIdentifier:Ljava/lang/String;

.field public final serverId:Ljava/lang/String;

.field public final uid:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)V
    .locals 0

    .line 28
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 29
    iput-object p1, p0, Lcom/helpshift/account/dao/ProfileDTO;->localId:Ljava/lang/Long;

    .line 30
    iput-object p3, p0, Lcom/helpshift/account/dao/ProfileDTO;->serverId:Ljava/lang/String;

    .line 31
    iput-object p2, p0, Lcom/helpshift/account/dao/ProfileDTO;->identifier:Ljava/lang/String;

    .line 32
    iput-object p4, p0, Lcom/helpshift/account/dao/ProfileDTO;->name:Ljava/lang/String;

    .line 33
    iput-object p5, p0, Lcom/helpshift/account/dao/ProfileDTO;->email:Ljava/lang/String;

    .line 34
    iput-object p6, p0, Lcom/helpshift/account/dao/ProfileDTO;->saltedIdentifier:Ljava/lang/String;

    .line 35
    iput-object p7, p0, Lcom/helpshift/account/dao/ProfileDTO;->uid:Ljava/lang/String;

    .line 36
    iput-object p8, p0, Lcom/helpshift/account/dao/ProfileDTO;->did:Ljava/lang/String;

    .line 37
    iput-boolean p9, p0, Lcom/helpshift/account/dao/ProfileDTO;->isPushTokenSynced:Z

    return-void
.end method
