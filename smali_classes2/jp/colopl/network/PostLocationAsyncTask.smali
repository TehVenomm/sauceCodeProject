.class public Ljp/colopl/network/PostLocationAsyncTask;
.super Ljp/colopl/network/PostLocationAsynTaskBase;
.source "PostLocationAsyncTask.java"


# instance fields
.field mDelegate:Ljp/colopl/network/PostLocationAsyncTaskDelegate;


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/util/HashMap;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Landroid/location/Location;",
            ">;)V"
        }
    .end annotation

    .line 20
    invoke-direct {p0, p1, p2, p3}, Ljp/colopl/network/PostLocationAsynTaskBase;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/util/HashMap;)V

    return-void
.end method


# virtual methods
.method protected bridge synthetic after(Landroid/content/Context;Ljava/lang/Object;)V
    .locals 0

    .line 15
    check-cast p2, Ljava/lang/String;

    invoke-virtual {p0, p1, p2}, Ljp/colopl/network/PostLocationAsyncTask;->after(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method

.method protected after(Landroid/content/Context;Ljava/lang/String;)V
    .locals 0

    .line 36
    iget-object p1, p0, Ljp/colopl/network/PostLocationAsyncTask;->mDelegate:Ljp/colopl/network/PostLocationAsyncTaskDelegate;

    invoke-interface {p1, p2}, Ljp/colopl/network/PostLocationAsyncTaskDelegate;->onPostLocation(Ljava/lang/String;)V

    return-void
.end method

.method public getDelegate()Ljp/colopl/network/PostLocationAsyncTaskDelegate;
    .locals 1

    .line 49
    iget-object v0, p0, Ljp/colopl/network/PostLocationAsyncTask;->mDelegate:Ljp/colopl/network/PostLocationAsyncTaskDelegate;

    return-object v0
.end method

.method protected getPostData()Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lorg/apache/http/NameValuePair;",
            ">;"
        }
    .end annotation

    .line 25
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 26
    invoke-virtual {p0}, Ljp/colopl/network/PostLocationAsyncTask;->getLocations()Ljava/util/HashMap;

    move-result-object v1

    invoke-static {v1}, Ljp/colopl/util/LocationUtil;->getMostAccurateLocation(Ljava/util/HashMap;)Landroid/location/Location;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 27
    invoke-static {v0}, Ljp/colopl/util/LocationUtil;->getEncryptedLocations(Ljava/util/List;)Ljava/lang/String;

    move-result-object v0

    .line 29
    new-instance v1, Ljava/util/ArrayList;

    const/4 v2, 0x1

    invoke-direct {v1, v2}, Ljava/util/ArrayList;-><init>(I)V

    .line 30
    new-instance v2, Lorg/apache/http/message/BasicNameValuePair;

    const-string v3, "location"

    invoke-direct {v2, v3, v0}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    return-object v1
.end method

.method protected handleError(Landroid/content/Context;Ljava/lang/Exception;)V
    .locals 0

    .line 41
    iget-object p1, p0, Ljp/colopl/network/PostLocationAsyncTask;->mDelegate:Ljp/colopl/network/PostLocationAsyncTaskDelegate;

    const/4 p2, 0x0

    invoke-interface {p1, p2}, Ljp/colopl/network/PostLocationAsyncTaskDelegate;->onPostLocation(Ljava/lang/String;)V

    return-void
.end method

.method public setDelegate(Ljp/colopl/network/PostLocationAsyncTaskDelegate;)V
    .locals 0

    .line 45
    iput-object p1, p0, Ljp/colopl/network/PostLocationAsyncTask;->mDelegate:Ljp/colopl/network/PostLocationAsyncTaskDelegate;

    return-void
.end method
