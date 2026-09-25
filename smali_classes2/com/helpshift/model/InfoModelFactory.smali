.class public Lcom/helpshift/model/InfoModelFactory;
.super Ljava/lang/Object;
.source "InfoModelFactory.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/model/InfoModelFactory$LazyHolder;
    }
.end annotation


# instance fields
.field public final appInfoModel:Lcom/helpshift/model/AppInfoModel;

.field public final sdkInfoModel:Lcom/helpshift/model/SdkInfoModel;


# direct methods
.method constructor <init>()V
    .locals 3

    .line 16
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 17
    invoke-static {}, Lcom/helpshift/storage/StorageFactory;->getInstance()Lcom/helpshift/storage/StorageFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/storage/StorageFactory;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    .line 18
    new-instance v1, Lcom/helpshift/model/AppInfoModel;

    invoke-direct {v1, v0}, Lcom/helpshift/model/AppInfoModel;-><init>(Lcom/helpshift/storage/KeyValueStorage;)V

    iput-object v1, p0, Lcom/helpshift/model/InfoModelFactory;->appInfoModel:Lcom/helpshift/model/AppInfoModel;

    .line 19
    new-instance v1, Lcom/helpshift/model/SdkInfoModel;

    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object v2

    invoke-direct {v1, v0, v2}, Lcom/helpshift/model/SdkInfoModel;-><init>(Lcom/helpshift/storage/KeyValueStorage;Lcom/helpshift/common/platform/Platform;)V

    iput-object v1, p0, Lcom/helpshift/model/InfoModelFactory;->sdkInfoModel:Lcom/helpshift/model/SdkInfoModel;

    return-void
.end method

.method public static getInstance()Lcom/helpshift/model/InfoModelFactory;
    .locals 1

    .line 23
    sget-object v0, Lcom/helpshift/model/InfoModelFactory$LazyHolder;->INSTANCE:Lcom/helpshift/model/InfoModelFactory;

    return-object v0
.end method
