.class public abstract Lcom/helpshift/listeners/SyncListener;
.super Ljava/lang/Object;
.source "SyncListener.java"


# instance fields
.field private dataType:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 0

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 23
    iput-object p1, p0, Lcom/helpshift/listeners/SyncListener;->dataType:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public fullSync()V
    .locals 0

    return-void
.end method

.method public getDataType()Ljava/lang/String;
    .locals 1

    .line 32
    iget-object v0, p0, Lcom/helpshift/listeners/SyncListener;->dataType:Ljava/lang/String;

    return-object v0
.end method

.method public getDependentChildDataTypes()Ljava/util/Set;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    const/4 v0, 0x0

    return-object v0
.end method

.method public abstract isFullSyncEnabled()Z
.end method

.method public abstract sync()V
.end method
