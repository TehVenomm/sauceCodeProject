.class public Lcom/zopim/android/sdk/model/Account;
.super Ljava/lang/Object;


# annotations
.annotation runtime Lcom/fasterxml/jackson/annotation/JsonIgnoreProperties;
    ignoreUnknown = true
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/model/Account$Status;
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


# virtual methods
.method public getStatus()Lcom/zopim/android/sdk/model/Account$Status;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/Account;->status:Ljava/lang/String;

    invoke-static {v0}, Lcom/zopim/android/sdk/model/Account$Status;->getStatus(Ljava/lang/String;)Lcom/zopim/android/sdk/model/Account$Status;

    move-result-object v0

    return-object v0
.end method
