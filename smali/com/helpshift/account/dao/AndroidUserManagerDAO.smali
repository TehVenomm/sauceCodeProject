.class public Lcom/helpshift/account/dao/AndroidUserManagerDAO;
.super Ljava/lang/Object;
.source "AndroidUserManagerDAO.java"

# interfaces
.implements Lcom/helpshift/account/dao/UserManagerDAO;


# static fields
.field private static final USER_META_IDENTIFIER:Ljava/lang/String; = "userMetaIdentifier"


# instance fields
.field private final kvStore:Lcom/helpshift/common/platform/KVStore;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/KVStore;)V
    .locals 0

    .line 9
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 10
    iput-object p1, p0, Lcom/helpshift/account/dao/AndroidUserManagerDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    return-void
.end method


# virtual methods
.method public getUserMetaIdentifier()Ljava/lang/String;
    .locals 3

    .line 15
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidUserManagerDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "userMetaIdentifier"

    const-string v2, ""

    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/platform/KVStore;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public setUserMetaIdentifier(Ljava/lang/String;)V
    .locals 2

    .line 20
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidUserManagerDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "userMetaIdentifier"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/common/platform/KVStore;->setString(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
