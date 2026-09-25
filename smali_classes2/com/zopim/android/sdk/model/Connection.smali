.class public Lcom/zopim/android/sdk/model/Connection;
.super Ljava/lang/Object;


# annotations
.annotation runtime Lcom/fasterxml/jackson/annotation/JsonIgnoreProperties;
    ignoreUnknown = true
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/model/Connection$Status;
    }
.end annotation


# instance fields
.field private status:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "status$string"
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public constructor <init>(Lcom/zopim/android/sdk/model/Connection$Status;)V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/Connection$Status;->getValue()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/Connection;->status:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getStatus()Lcom/zopim/android/sdk/model/Connection$Status;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/Connection;->status:Ljava/lang/String;

    invoke-static {v0}, Lcom/zopim/android/sdk/model/Connection$Status;->getStatus(Ljava/lang/String;)Lcom/zopim/android/sdk/model/Connection$Status;

    move-result-object v0

    return-object v0
.end method

.method public toString()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/model/Connection;->status:Ljava/lang/String;

    return-object v0
.end method
