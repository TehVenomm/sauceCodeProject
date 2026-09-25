.class public Ljp/colopl/api/docomo/DoCoMoAsyncTask;
.super Lcom/github/droidfu/concurrent/BetterAsyncTask;
.source "DoCoMoAsyncTask.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/github/droidfu/concurrent/BetterAsyncTask<",
        "Ljava/lang/Void;",
        "Ljava/lang/Void;",
        "Ljp/colopl/api/docomo/DoCoMoLocationInfo;",
        ">;"
    }
.end annotation


# instance fields
.field private delegate:Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 14
    invoke-direct {p0, p1}, Lcom/github/droidfu/concurrent/BetterAsyncTask;-><init>(Landroid/content/Context;)V

    return-void
.end method


# virtual methods
.method protected bridge synthetic after(Landroid/content/Context;Ljava/lang/Object;)V
    .locals 0

    .line 9
    check-cast p2, Ljp/colopl/api/docomo/DoCoMoLocationInfo;

    invoke-virtual {p0, p1, p2}, Ljp/colopl/api/docomo/DoCoMoAsyncTask;->after(Landroid/content/Context;Ljp/colopl/api/docomo/DoCoMoLocationInfo;)V

    return-void
.end method

.method protected after(Landroid/content/Context;Ljp/colopl/api/docomo/DoCoMoLocationInfo;)V
    .locals 1

    .line 26
    iget-object p1, p0, Ljp/colopl/api/docomo/DoCoMoAsyncTask;->delegate:Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;

    if-nez p1, :cond_0

    return-void

    :cond_0
    if-nez p2, :cond_1

    const-string p1, "DoCoMoAsyncTask"

    const-string p2, "doCoMoLocationInfo is null"

    .line 30
    invoke-static {p1, p2}, Ljp/colopl/util/Util;->eLog(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 33
    :cond_1
    invoke-virtual {p2}, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->getResultInfo()Ljp/colopl/api/docomo/ResultInfo;

    move-result-object p1

    if-nez p1, :cond_2

    const-string p1, "DoCoMoAsyncTask"

    const-string p2, "resultInfo is null"

    .line 35
    invoke-static {p1, p2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 38
    :cond_2
    invoke-virtual {p1}, Ljp/colopl/api/docomo/ResultInfo;->isResultCodeOK()Z

    move-result p1

    if-eqz p1, :cond_3

    const-string p1, "DoCoMoAsyncTask"

    const-string v0, "resultCode is not 2000"

    .line 39
    invoke-static {p1, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 40
    iget-object p1, p0, Ljp/colopl/api/docomo/DoCoMoAsyncTask;->delegate:Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;

    invoke-interface {p1, p2}, Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;->receiveSuccessDoCoMoLocationInfo(Ljp/colopl/api/docomo/DoCoMoLocationInfo;)V

    goto :goto_0

    .line 43
    :cond_3
    iget-object p1, p0, Ljp/colopl/api/docomo/DoCoMoAsyncTask;->delegate:Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;

    invoke-interface {p1, p2}, Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;->receiveErrorDoCoMoLocationInfo(Ljp/colopl/api/docomo/DoCoMoLocationInfo;)V

    :goto_0
    return-void
.end method

.method protected bridge synthetic doCheckedInBackground(Landroid/content/Context;[Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 9
    check-cast p2, [Ljava/lang/Void;

    invoke-virtual {p0, p1, p2}, Ljp/colopl/api/docomo/DoCoMoAsyncTask;->doCheckedInBackground(Landroid/content/Context;[Ljava/lang/Void;)Ljp/colopl/api/docomo/DoCoMoLocationInfo;

    move-result-object p1

    return-object p1
.end method

.method protected varargs doCheckedInBackground(Landroid/content/Context;[Ljava/lang/Void;)Ljp/colopl/api/docomo/DoCoMoLocationInfo;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 19
    new-instance p1, Ljp/colopl/api/docomo/DoCoMoAPI;

    invoke-direct {p1}, Ljp/colopl/api/docomo/DoCoMoAPI;-><init>()V

    .line 20
    invoke-virtual {p1}, Ljp/colopl/api/docomo/DoCoMoAPI;->getLocationInfo()Ljp/colopl/api/docomo/DoCoMoLocationInfo;

    move-result-object p1

    return-object p1
.end method

.method public getDelegate()Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;
    .locals 1

    .line 57
    iget-object v0, p0, Ljp/colopl/api/docomo/DoCoMoAsyncTask;->delegate:Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;

    return-object v0
.end method

.method protected handleError(Landroid/content/Context;Ljava/lang/Exception;)V
    .locals 0

    return-void
.end method

.method public setDelegate(Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;)V
    .locals 0

    .line 53
    iput-object p1, p0, Ljp/colopl/api/docomo/DoCoMoAsyncTask;->delegate:Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;

    return-void
.end method
