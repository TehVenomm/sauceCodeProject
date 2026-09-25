.class public Lcom/zopim/android/sdk/model/Forms;
.super Ljava/lang/Object;


# annotations
.annotation runtime Lcom/fasterxml/jackson/annotation/JsonIgnoreProperties;
    ignoreUnknown = true
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/model/Forms$FormSubmitted;,
        Lcom/zopim/android/sdk/model/Forms$OfflineForm;
    }
.end annotation


# instance fields
.field offlineForm:Lcom/zopim/android/sdk/model/Forms$OfflineForm;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "offline_form"
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getOfflineForm()Lcom/zopim/android/sdk/model/Forms$OfflineForm;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/model/Forms;->offlineForm:Lcom/zopim/android/sdk/model/Forms$OfflineForm;

    return-object v0
.end method
