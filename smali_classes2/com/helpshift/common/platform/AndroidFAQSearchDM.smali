.class public Lcom/helpshift/common/platform/AndroidFAQSearchDM;
.super Ljava/lang/Object;
.source "AndroidFAQSearchDM.java"

# interfaces
.implements Lcom/helpshift/faq/domainmodel/FAQSearchDM;


# instance fields
.field data:Lcom/helpshift/support/HSApiData;


# direct methods
.method public constructor <init>(Lcom/helpshift/support/HSApiData;)V
    .locals 0

    .line 19
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 20
    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidFAQSearchDM;->data:Lcom/helpshift/support/HSApiData;

    return-void
.end method


# virtual methods
.method public getSearchResults(Ljava/lang/String;)Ljava/util/ArrayList;
    .locals 2

    .line 25
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidFAQSearchDM;->data:Lcom/helpshift/support/HSApiData;

    sget-object v1, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->KEYWORD_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    invoke-virtual {v0, p1, v1}, Lcom/helpshift/support/HSApiData;->localFaqSearch(Ljava/lang/String;Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;)Ljava/util/ArrayList;

    move-result-object p1

    return-object p1
.end method

.method public startFAQSearchIndexing()V
    .locals 2

    .line 30
    new-instance v0, Landroid/os/HandlerThread;

    const-string v1, "HS-faqdm-index"

    invoke-direct {v0, v1}, Landroid/os/HandlerThread;-><init>(Ljava/lang/String;)V

    .line 31
    invoke-virtual {v0}, Landroid/os/HandlerThread;->start()V

    .line 32
    new-instance v1, Landroid/os/Handler;

    invoke-virtual {v0}, Landroid/os/HandlerThread;->getLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-direct {v1, v0}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    .line 33
    new-instance v0, Lcom/helpshift/common/platform/AndroidFAQSearchDM$1;

    invoke-direct {v0, p0}, Lcom/helpshift/common/platform/AndroidFAQSearchDM$1;-><init>(Lcom/helpshift/common/platform/AndroidFAQSearchDM;)V

    invoke-virtual {v1, v0}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
